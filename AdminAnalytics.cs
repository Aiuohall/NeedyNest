using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    /// <summary>
    /// Builds the Admin dashboard analytics strip: four KPI cards
    /// (members, pending approvals, courses, revenue) plus a small
    /// "Members by Role" bar chart. All queries are guarded so the panel
    /// still renders (with zeros) if the database is unavailable.
    /// </summary>
    internal static class AdminAnalytics
    {
        public const int PanelHeight = 250;

        public static Panel Build()
        {
            var s = LoadStats();
            var byRole = LoadUsersByRole();

            var root = new Panel
            {
                BackColor = ThemeManager.BackgroundColor,
                Padding   = new Padding(28, 14, 28, 6),
                Height    = PanelHeight
            };

            // ── KPI cards row ────────────────────────────────────────────────────
            var cardsRow = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = ThemeManager.BackgroundColor };
            var cards = new[]
            {
                MakeCard("Total Members",      s.Users.ToString(),        ThemeManager.PrimaryColor),
                MakeCard("Pending Approvals",  s.Pending.ToString(),      s.Pending > 0 ? ThemeManager.DangerColor : ThemeManager.SuccessColor),
                MakeCard("Courses",            s.Courses.ToString(),      ThemeManager.HoverColor),
                MakeCard("Revenue (BDT)",      s.Revenue.ToString("N0"),  ThemeManager.SuccessColor),
            };
            foreach (var c in cards) cardsRow.Controls.Add(c);

            void LayoutCards()
            {
                int n = cards.Length, gap = 16;
                int w = (cardsRow.ClientSize.Width - (n - 1) * gap) / n;
                if (w <= 0) return;
                for (int i = 0; i < n; i++)
                    cards[i].SetBounds(i * (w + gap), 0, w, cardsRow.ClientSize.Height);
            }
            cardsRow.Resize += (s2, e2) => LayoutCards();

            // ── "Members by Role" bar chart ──────────────────────────────────────
            var chart = new Panel { Dock = DockStyle.Fill, BackColor = ThemeManager.SurfaceColor, Margin = new Padding(0, 10, 0, 0) };
            var chartTitle = new Label
            {
                Text = "Members by Role",
                Dock = DockStyle.Top,
                Height = 28,
                ForeColor = ThemeManager.PrimaryColor,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(12, 6, 0, 0),
                BackColor = Color.Transparent
            };
            chart.Paint += (s2, e2) => DrawBars(e2.Graphics, chart.ClientRectangle, byRole);
            chart.Resize += (s2, e2) => chart.Invalidate();
            chart.Controls.Add(chartTitle);

            root.Controls.Add(chart);     // Fill (added first)
            root.Controls.Add(cardsRow);  // Top
            root.HandleCreated += (s2, e2) => LayoutCards();

            return root;
        }

        // ── Card factory ─────────────────────────────────────────────────────────
        private static Panel MakeCard(string caption, string value, Color color)
        {
            var card = new Panel { BackColor = color };
            card.Controls.Add(new Label
            {
                Text = caption,
                Dock = DockStyle.Bottom,
                Height = 30,
                ForeColor = Color.FromArgb(232, 242, 247),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 8)
            });
            card.Controls.Add(new Label
            {
                Text = value,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(14, 0, 0, 0)
            });
            return card;
        }

        // ── Bar-chart painter ─────────────────────────────────────────────────────
        private static void DrawBars(Graphics g, Rectangle area, Dictionary<string, int> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (data == null || data.Count == 0) return;

            int padL = 24, padR = 24, padTop = 40, padBottom = 34;
            var plot = new Rectangle(area.Left + padL, area.Top + padTop,
                                     area.Width - padL - padR, area.Height - padTop - padBottom);
            if (plot.Width <= 0 || plot.Height <= 0) return;

            int max = Math.Max(1, data.Values.Max());
            int n = data.Count;
            int slot = plot.Width / n;
            int barW = Math.Min(90, (int)(slot * 0.55));

            var palette = new[]
            {
                ThemeManager.PrimaryColor, ThemeManager.HoverColor,
                ThemeManager.AccentColor, ThemeManager.SuccessColor, ThemeManager.SecondaryColor
            };

            using (var axis = new Pen(ThemeManager.BorderColor))
                g.DrawLine(axis, plot.Left, plot.Bottom, plot.Right, plot.Bottom);

            int i = 0;
            using (var labelFont = new Font("Segoe UI", 9F, FontStyle.Regular))
            using (var valueFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var labelBrush = new SolidBrush(ThemeManager.SubtleText))
            using (var valueBrush = new SolidBrush(ThemeManager.ForegroundColor))
            {
                var fmt = new StringFormat { Alignment = StringAlignment.Center };
                foreach (var kv in data)
                {
                    int cx = plot.Left + slot * i + slot / 2;
                    int barH = (int)((double)kv.Value / max * (plot.Height - 6));
                    var barRect = new Rectangle(cx - barW / 2, plot.Bottom - barH, barW, barH);
                    using (var b = new SolidBrush(palette[i % palette.Length]))
                        g.FillRectangle(b, barRect);

                    g.DrawString(kv.Value.ToString(), valueFont, valueBrush,
                                 new RectangleF(cx - slot / 2, barRect.Top - 18, slot, 16), fmt);
                    g.DrawString(kv.Key, labelFont, labelBrush,
                                 new RectangleF(cx - slot / 2, plot.Bottom + 6, slot, 16), fmt);
                    i++;
                }
            }
        }

        // ── Data ──────────────────────────────────────────────────────────────────
        private struct Stats { public int Users, Pending, Courses; public decimal Revenue; }

        private static Stats LoadStats()
        {
            var s = new Stats();
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();
                    s.Users    = Scalar(con, "SELECT COUNT(*) FROM signup");
                    s.Pending  = Scalar(con, "SELECT COUNT(*) FROM signup WHERE status = 0");
                    s.Courses  = Scalar(con, "SELECT COUNT(*) FROM Course");
                    using (var cmd = new SqlCommand("SELECT ISNULL(SUM(transaction_amount), 0) FROM Payments", con))
                    {
                        object r = cmd.ExecuteScalar();
                        s.Revenue = (r == null || r == DBNull.Value) ? 0 : Convert.ToDecimal(r);
                    }
                }
            }
            catch { /* DB offline — leave zeros */ }
            return s;
        }

        private static Dictionary<string, int> LoadUsersByRole()
        {
            var d = new Dictionary<string, int>();
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                using (var cmd = new SqlCommand("SELECT role, COUNT(*) FROM signup GROUP BY role", con))
                {
                    con.Open();
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            d[r.IsDBNull(0) ? "Unknown" : r.GetString(0)] = Convert.ToInt32(r.GetValue(1));
                }
            }
            catch { /* DB offline */ }
            return d;
        }

        private static int Scalar(SqlConnection con, string sql)
        {
            using (var cmd = new SqlCommand(sql, con))
            {
                object r = cmd.ExecuteScalar();
                return (r == null || r == DBNull.Value) ? 0 : Convert.ToInt32(r);
            }
        }
    }
}
