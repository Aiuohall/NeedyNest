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
    /// Dashboard analytics widgets. <see cref="Build"/> returns the full admin
    /// strip (KPI cards + revenue-trend line chart + top-courses bar chart);
    /// <see cref="BuildModeratorWidget"/> returns a compact KPI row for the
    /// moderator dashboard. All queries are guarded (zeros if DB is unavailable).
    /// </summary>
    internal static class AdminAnalytics
    {
        public const int PanelHeight          = 360;
        public const int ModeratorPanelHeight = 132;

        // ───────────────────────────────────────────────────────────────────────
        public static Panel Build()
        {
            var s         = LoadStats();
            var topCourse = LoadTopCourses();
            var revenue   = LoadRevenueTrend();

            var root = new Panel { BackColor = ThemeManager.BackgroundColor, Padding = new Padding(28, 14, 28, 8), Height = PanelHeight };

            // KPI cards
            var cardsRow = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = ThemeManager.BackgroundColor };
            var cards = new[]
            {
                MakeCard("Total Members",     s.Users.ToString(),       ThemeManager.PrimaryColor),
                MakeCard("Pending Approvals", s.Pending.ToString(),     s.Pending > 0 ? ThemeManager.DangerColor : ThemeManager.SuccessColor),
                MakeCard("Courses",           s.Courses.ToString(),     ThemeManager.HoverColor),
                MakeCard("Revenue (BDT)",     s.Revenue.ToString("N0"), ThemeManager.SuccessColor),
            };
            foreach (var c in cards) cardsRow.Controls.Add(c);
            cardsRow.Resize += (s2, e2) => LayoutRow(cardsRow, cards);

            // Two charts side by side
            var charts  = new Panel { Dock = DockStyle.Fill, BackColor = ThemeManager.BackgroundColor };
            var revCard = ChartCard("Revenue Trend", (g, r) => DrawLine(g, r, revenue));
            var crsCard = ChartCard("Top Courses by Enrollment", (g, r) => DrawBars(g, r, topCourse));
            charts.Controls.Add(revCard);
            charts.Controls.Add(crsCard);
            void LayoutCharts()
            {
                int gap = 16, top = 10;
                int w = (charts.ClientSize.Width - gap) / 2;
                int h = charts.ClientSize.Height - top;
                if (w <= 0 || h <= 0) return;
                revCard.SetBounds(0, top, w, h);
                crsCard.SetBounds(w + gap, top, charts.ClientSize.Width - w - gap, h);
            }
            charts.Resize += (s2, e2) => LayoutCharts();

            root.Controls.Add(charts);    // Fill (added first)
            root.Controls.Add(cardsRow);  // Top
            root.HandleCreated += (s2, e2) => { LayoutRow(cardsRow, cards); LayoutCharts(); };

            return root;
        }

        // ───────────────────────────────────────────────────────────────────────
        public static Panel BuildModeratorWidget()
        {
            var s = LoadStats();
            int books     = CountSafe("SELECT COUNT(*) FROM Books");
            int materials = CountSafe("SELECT COUNT(*) FROM Slides");

            var root = new Panel { BackColor = ThemeManager.BackgroundColor, Padding = new Padding(28, 14, 28, 6), Height = ModeratorPanelHeight };
            var row  = new Panel { Dock = DockStyle.Fill, BackColor = ThemeManager.BackgroundColor };
            var cards = new[]
            {
                MakeCard("Courses",   s.Courses.ToString(), ThemeManager.PrimaryColor),
                MakeCard("Books",     books.ToString(),     ThemeManager.HoverColor),
                MakeCard("Materials", materials.ToString(), ThemeManager.AccentColor),
                MakeCard("Members",   s.Users.ToString(),   ThemeManager.SuccessColor),
            };
            foreach (var c in cards) row.Controls.Add(c);
            row.Resize += (s2, e2) => LayoutRow(row, cards);
            root.Controls.Add(row);
            root.HandleCreated += (s2, e2) => LayoutRow(row, cards);
            return root;
        }

        // ── Shared helpers ─────────────────────────────────────────────────────
        private static void LayoutRow(Panel row, Panel[] cards)
        {
            int n = cards.Length, gap = 16;
            int w = (row.ClientSize.Width - (n - 1) * gap) / n;
            if (w <= 0) return;
            for (int i = 0; i < n; i++) cards[i].SetBounds(i * (w + gap), 0, w, row.ClientSize.Height);
        }

        private static Panel MakeCard(string caption, string value, Color color)
        {
            var card = new Panel { BackColor = color };
            card.Controls.Add(new Label
            {
                Text = caption, Dock = DockStyle.Bottom, Height = 28,
                ForeColor = Color.FromArgb(232, 242, 247), BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(16, 0, 0, 8)
            });
            card.Controls.Add(new Label
            {
                Text = value, Dock = DockStyle.Fill, ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft, Padding = new Padding(14, 0, 0, 0)
            });
            return card;
        }

        private static Panel ChartCard(string title, Action<Graphics, Rectangle> drawBody)
        {
            var card = new Panel { BackColor = ThemeManager.SurfaceColor };
            card.Controls.Add(new Label
            {
                Text = title, Dock = DockStyle.Top, Height = 28, ForeColor = ThemeManager.PrimaryColor,
                BackColor = Color.Transparent, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Padding = new Padding(12, 6, 0, 0)
            });
            card.Paint += (s, e) =>
                drawBody(e.Graphics, new Rectangle(0, 30, card.ClientSize.Width, card.ClientSize.Height - 30));
            card.Resize += (s, e) => card.Invalidate();
            return card;
        }

        // ── Bar chart ──────────────────────────────────────────────────────────
        private static void DrawBars(Graphics g, Rectangle area, Dictionary<string, int> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (data == null || data.Count == 0) { DrawNoData(g, area); return; }

            int padL = 18, padR = 18, padTop = 16, padBottom = 34;
            var plot = new Rectangle(area.Left + padL, area.Top + padTop, area.Width - padL - padR, area.Height - padTop - padBottom);
            if (plot.Width <= 0 || plot.Height <= 0) return;

            int max = Math.Max(1, data.Values.Max());
            int n = data.Count, slot = plot.Width / n, barW = Math.Min(70, (int)(slot * 0.55));
            var palette = new[] { ThemeManager.PrimaryColor, ThemeManager.HoverColor, ThemeManager.AccentColor, ThemeManager.SuccessColor, ThemeManager.SecondaryColor };

            using (var axis = new Pen(ThemeManager.BorderColor))
                g.DrawLine(axis, plot.Left, plot.Bottom, plot.Right, plot.Bottom);

            int i = 0;
            using (var labelFont = new Font("Segoe UI", 8.25F))
            using (var valueFont = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            using (var labelBrush = new SolidBrush(ThemeManager.SubtleText))
            using (var valueBrush = new SolidBrush(ThemeManager.ForegroundColor))
            {
                var fmt = new StringFormat { Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
                foreach (var kv in data)
                {
                    int cx = plot.Left + slot * i + slot / 2;
                    int barH = (int)((double)kv.Value / max * (plot.Height - 6));
                    var bar = new Rectangle(cx - barW / 2, plot.Bottom - barH, barW, barH);
                    using (var b = new SolidBrush(palette[i % palette.Length])) g.FillRectangle(b, bar);
                    g.DrawString(kv.Value.ToString(), valueFont, valueBrush, new RectangleF(cx - slot / 2f, bar.Top - 16, slot, 14), fmt);
                    g.DrawString(kv.Key, labelFont, labelBrush, new RectangleF(cx - slot / 2f, plot.Bottom + 4, slot, 26), fmt);
                    i++;
                }
            }
        }

        // ── Line chart ─────────────────────────────────────────────────────────
        private static void DrawLine(Graphics g, Rectangle area, List<KeyValuePair<string, decimal>> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (data == null || data.Count == 0) { DrawNoData(g, area); return; }

            int padL = 18, padR = 18, padTop = 16, padBottom = 34;
            var plot = new Rectangle(area.Left + padL, area.Top + padTop, area.Width - padL - padR, area.Height - padTop - padBottom);
            if (plot.Width <= 0 || plot.Height <= 0) return;

            decimal max = Math.Max(1m, data.Max(d => d.Value));
            int n = data.Count;
            var pts = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                float x = plot.Left + (n == 1 ? plot.Width / 2f : (float)i / (n - 1) * plot.Width);
                float y = plot.Bottom - (float)(data[i].Value / max) * (plot.Height - 6);
                pts[i] = new PointF(x, y);
            }

            using (var axis = new Pen(ThemeManager.BorderColor))
                g.DrawLine(axis, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
            if (n >= 2)
                using (var line = new Pen(ThemeManager.AccentColor, 2.5f))
                    g.DrawLines(line, pts);
            using (var dot = new SolidBrush(ThemeManager.PrimaryColor))
                foreach (var p in pts) g.FillEllipse(dot, p.X - 3, p.Y - 3, 6, 6);

            using (var labelFont = new Font("Segoe UI", 7.5F))
            using (var labelBrush = new SolidBrush(ThemeManager.SubtleText))
            {
                var fmt = new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap };
                int slot = plot.Width / Math.Max(1, n);
                for (int i = 0; i < n; i++)
                    g.DrawString(data[i].Key, labelFont, labelBrush, new RectangleF(pts[i].X - slot / 2f, plot.Bottom + 4, slot, 14), fmt);
            }
        }

        private static void DrawNoData(Graphics g, Rectangle area)
        {
            using (var f = new Font("Segoe UI", 9F, FontStyle.Italic))
            using (var b = new SolidBrush(ThemeManager.SubtleText))
                g.DrawString("No data yet", f, b,
                    new RectangleF(area.Left, area.Top + area.Height / 2f - 8, area.Width, 18),
                    new StringFormat { Alignment = StringAlignment.Center });
        }

        // ── Data access (guarded) ──────────────────────────────────────────────
        private struct Stats { public int Users, Pending, Courses; public decimal Revenue; }

        private static Stats LoadStats()
        {
            var s = new Stats();
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();
                    s.Users   = Scalar(con, "SELECT COUNT(*) FROM signup");
                    s.Pending = Scalar(con, "SELECT COUNT(*) FROM signup WHERE status = 0");
                    s.Courses = Scalar(con, "SELECT COUNT(*) FROM Course");
                    using (var cmd = new SqlCommand("SELECT ISNULL(SUM(transaction_amount), 0) FROM Payments", con))
                    {
                        object r = cmd.ExecuteScalar();
                        s.Revenue = (r == null || r == DBNull.Value) ? 0 : Convert.ToDecimal(r);
                    }
                }
            }
            catch (Exception ex) { Logger.Log(ex, "AdminAnalytics.LoadStats"); }
            return s;
        }

        private static Dictionary<string, int> LoadTopCourses()
        {
            var d = new Dictionary<string, int>();
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                using (var cmd = new SqlCommand(
                    "SELECT TOP 5 c.[course name] AS name, COUNT(*) AS cnt " +
                    "FROM Enrolled e JOIN Course c ON e.[course id] = c.[course id] " +
                    "GROUP BY c.[course name] ORDER BY cnt DESC", con))
                {
                    con.Open();
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            d[r["name"].ToString()] = Convert.ToInt32(r["cnt"]);
                }
            }
            catch (Exception ex) { Logger.Log(ex, "AdminAnalytics.LoadTopCourses"); }
            return d;
        }

        private static List<KeyValuePair<string, decimal>> LoadRevenueTrend()
        {
            var list = new List<KeyValuePair<string, decimal>>();
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                using (var cmd = new SqlCommand(
                    "SELECT TOP 7 CAST(payment_date AS DATE) AS d, SUM(transaction_amount) AS total " +
                    "FROM Payments GROUP BY CAST(payment_date AS DATE) ORDER BY d DESC", con))
                {
                    con.Open();
                    using (var r = cmd.ExecuteReader())
                        while (r.Read())
                            list.Add(new KeyValuePair<string, decimal>(
                                Convert.ToDateTime(r["d"]).ToString("MM/dd"),
                                r["total"] == DBNull.Value ? 0 : Convert.ToDecimal(r["total"])));
                }
                list.Reverse(); // chronological order for the line
            }
            catch (Exception ex) { Logger.Log(ex, "AdminAnalytics.LoadRevenueTrend"); }
            return list;
        }

        private static int CountSafe(string sql)
        {
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();
                    return Scalar(con, sql);
                }
            }
            catch { return 0; }
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
