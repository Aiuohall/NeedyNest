using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    /// <summary>
    /// Gives any content form (grids, input screens) the same professional
    /// gradient header bar used by the dashboards, and shifts the form's existing
    /// controls down so nothing hides behind it. Call from the form's Load event
    /// (so it runs after the theme has been applied).
    /// </summary>
    public static class PageChrome
    {
        public const string HeaderName = "pageChromeHeader";

        public static void Apply(Form form, string title, int headerHeight = 60)
        {
            // Guard against running twice (Load can fire again on re-show).
            foreach (Control existing in form.Controls)
                if (existing.Name == HeaderName) return;

            form.SuspendLayout();
            form.BackColor = ThemeManager.BackgroundColor;

            // Push existing top-level controls down (leave docked / status strip alone).
            foreach (var c in form.Controls.Cast<Control>().ToList())
            {
                if (c is StatusStrip) continue;
                if (c.Dock != DockStyle.None) continue;
                c.Top += headerHeight;
            }

            var header = new Panel
            {
                Name      = HeaderName,
                Dock      = DockStyle.Top,
                Height    = headerHeight,
                BackColor = ThemeManager.PrimaryColor
            };
            header.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(header.ClientRectangle,
                           ThemeManager.PrimaryColor, ThemeManager.HoverColor, LinearGradientMode.Horizontal))
                    e.Graphics.FillRectangle(brush, header.ClientRectangle);
                using (var accent = new SolidBrush(ThemeManager.AccentColor))
                    e.Graphics.FillRectangle(accent, 0, header.Height - 3, header.Width, 3);
            };
            header.Controls.Add(new Label
            {
                Text      = title,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font      = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(24, headerHeight / 2 - 15)
            });
            form.Controls.Add(header);
            header.BringToFront();

            form.ResumeLayout(true);
        }
    }
}
