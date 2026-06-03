using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    /// <summary>
    /// Converts a legacy absolute-positioned dashboard into a clean, centered,
    /// resizable layout:
    ///   • a coloured header with the screen title and a "Welcome, {user}" greeting
    ///   • a vertically stacked set of uniform action buttons (auto-centered)
    ///   • a footer holding the logout / back button
    /// The existing buttons (with their Click handlers) are reused — only their
    /// size and position change — so no event wiring is lost.
    /// </summary>
    public static class DashboardLayout
    {
        public static void Apply(
            Form form,
            string title,
            string welcomeName,
            Button[] actions,
            Button footerButton,
            Control[] hideControls = null,
            Button badgeButton = null,
            int badgeCount = 0)
        {
            form.SuspendLayout();

            // ── Window chrome: resizable + maximizable ───────────────────────────
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox     = true;
            form.MinimumSize     = new Size(780, 600);
            form.ClientSize      = new Size(980, 660);
            form.StartPosition   = FormStartPosition.CenterScreen;
            form.BackColor       = ThemeManager.BackgroundColor;
            form.Padding         = new Padding(0);

            // Remove the base status strip (the header shows the same info, cleaner)
            for (int i = form.Controls.Count - 1; i >= 0; i--)
                if (form.Controls[i] is StatusStrip) form.Controls.RemoveAt(i);

            // Hide legacy welcome / marketing / duplicate controls
            if (hideControls != null)
                foreach (var c in hideControls)
                    if (c != null) c.Visible = false;

            // ── Header (gradient banner) ───────────────────────────────────────────
            var header = new Panel { Dock = DockStyle.Top, Height = 86, BackColor = ThemeManager.PrimaryColor };
            header.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(header.ClientRectangle,
                           ThemeManager.PrimaryColor, ThemeManager.HoverColor, LinearGradientMode.Horizontal))
                    e.Graphics.FillRectangle(brush, header.ClientRectangle);
                // thin accent strip along the bottom edge
                using (var accent = new SolidBrush(ThemeManager.AccentColor))
                    e.Graphics.FillRectangle(accent, 0, header.Height - 3, header.Width, 3);
            };

            var titleLabel = new Label
            {
                Text      = title,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font      = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(30, 18)
            };
            var subtitleLabel = new Label
            {
                Text      = "NeedyNest • Community Resource System",
                ForeColor = Color.FromArgb(170, 210, 228),
                BackColor = Color.Transparent,
                Font      = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                AutoSize  = true,
                Location  = new Point(32, 52)
            };
            var welcomeLabel = new Label
            {
                Text      = "Welcome, " + welcomeName,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font      = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                AutoSize  = true,
                Anchor    = AnchorStyles.Top | AnchorStyles.Right
            };
            header.Controls.Add(titleLabel);
            header.Controls.Add(subtitleLabel);
            header.Controls.Add(welcomeLabel);
            header.Resize += (s, e) =>
                welcomeLabel.Location = new Point(header.Width - welcomeLabel.Width - 30, 30);

            // ── Footer ────────────────────────────────────────────────────────────
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 66, BackColor = ThemeManager.BackgroundColor };
            if (footerButton != null)
            {
                ThemeManager.StyleButton(footerButton);
                form.Controls.Remove(footerButton);
                footerButton.Size   = new Size(160, 42);
                footerButton.Anchor = AnchorStyles.Right;
                footerButton.Font   = new Font("Segoe UI", 10F, FontStyle.Bold);
                footer.Controls.Add(footerButton);
                footer.Resize += (s, e) =>
                    footerButton.Location = new Point(footer.Width - footerButton.Width - 30, 12);
            }

            // ── Content (fills the middle, centers the button stack) ───────────────
            var content = new Panel { Dock = DockStyle.Fill, BackColor = ThemeManager.BackgroundColor };

            foreach (var b in actions)
            {
                if (b == null) continue;
                ThemeManager.StyleButton(b); // ensure code-created buttons are themed too
                form.Controls.Remove(b);
                content.Controls.Add(b);
            }

            // Optional red pending-count badge pinned to a button's top-right corner.
            Label badge = null;
            if (badgeButton != null && badgeCount > 0)
            {
                badge = new Label
                {
                    Text      = badgeCount > 99 ? "99+" : badgeCount.ToString(),
                    BackColor = ThemeManager.DangerHover,
                    ForeColor = Color.White,
                    Font      = new Font("Segoe UI", 9F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize  = false,
                    Size      = new Size(26, 26)
                };
                content.Controls.Add(badge);
            }

            void LayoutButtons()
            {
                int count = 0;
                foreach (var b in actions) if (b != null) count++;
                if (count == 0) return;

                int w   = Math.Min(460, Math.Max(260, content.Width - 120));
                int h   = 56;
                int gap = 16;
                int totalH = count * h + (count - 1) * gap;
                int x = (content.Width - w) / 2;
                int y = Math.Max(28, (content.Height - totalH) / 2);

                foreach (var b in actions)
                {
                    if (b == null) continue;
                    b.Size     = new Size(w, h);
                    b.Location = new Point(x, y);
                    b.Font     = new Font("Segoe UI", 11F, FontStyle.Bold);
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    y += h + gap;
                }

                if (badge != null && badgeButton != null)
                {
                    badge.Location = new Point(
                        badgeButton.Right - 14,
                        badgeButton.Top - 12);
                    badge.BringToFront();
                }
            }
            content.Resize += (s, e) => LayoutButtons();

            // Dock order matters: add the Fill control FIRST (it sits at the back and
            // takes whatever space the docked header/footer leave behind).
            form.Controls.Add(content);
            form.Controls.Add(header);
            form.Controls.Add(footer);

            LayoutButtons();
            form.ResumeLayout(true);
        }
    }
}
