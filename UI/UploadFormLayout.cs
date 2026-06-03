using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    /// <summary>
    /// One consistent, professional layout for the "upload + list" screens
    /// (AddBooks, Distribution, DistributionForModerator). Reuses the form's
    /// existing controls (so all handlers stay wired) and arranges them as:
    ///   header  →  toolbar (path + Browse + Save + Refresh)  →  grid  →  footer (Open / extra / Back)
    /// </summary>
    public static class UploadFormLayout
    {
        public static void Apply(
            Form form, string title,
            TextBox pathBox, Button browse, Button save,
            DataGridView grid, Button open, Button refresh, Button back,
            Button extra = null)
        {
            form.SuspendLayout();
            form.BackColor  = ThemeManager.BackgroundColor;
            form.Padding    = new Padding(0);
            form.ClientSize = new Size(1000, 660);
            form.MinimumSize = new Size(840, 560);

            for (int i = form.Controls.Count - 1; i >= 0; i--)
                if (form.Controls[i] is StatusStrip) form.Controls.RemoveAt(i);

            // ── Header ───────────────────────────────────────────────────────────
            var header = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = ThemeManager.PrimaryColor };
            header.Paint += (s, e) =>
            {
                using (var b = new LinearGradientBrush(header.ClientRectangle,
                           ThemeManager.PrimaryColor, ThemeManager.HoverColor, LinearGradientMode.Horizontal))
                    e.Graphics.FillRectangle(b, header.ClientRectangle);
                using (var a = new SolidBrush(ThemeManager.AccentColor))
                    e.Graphics.FillRectangle(a, 0, header.Height - 3, header.Width, 3);
            };
            header.Controls.Add(new Label
            {
                Text = title, ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), AutoSize = true, Location = new Point(24, 15)
            });

            // ── Toolbar (path + Browse + Save + Refresh) ─────────────────────────
            var toolbar = new Panel { Dock = DockStyle.Top, Height = 74, BackColor = ThemeManager.BackgroundColor };

            void Move(Control c, Control parent) { if (c != null) { form.Controls.Remove(c); parent.Controls.Add(c); } }

            Move(pathBox, toolbar);
            Move(browse, toolbar);
            Move(save, toolbar);
            Move(refresh, toolbar);

            if (pathBox != null)
            {
                pathBox.Multiline = false;
                pathBox.SetBounds(24, 22, 460, 30);
                pathBox.Font = new Font("Segoe UI", 10F);
                pathBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
            StyleToolbarButton(browse,  "Browse");
            StyleToolbarButton(save,    "save");
            StyleToolbarButton(refresh, "Refresh");
            if (browse  != null) { browse.SetBounds(498, 18, 110, 38);  browse.Anchor = AnchorStyles.Top | AnchorStyles.Right; }
            if (save    != null) { save.SetBounds(616, 18, 110, 38);    save.Anchor = AnchorStyles.Top | AnchorStyles.Right; }
            if (refresh != null) { refresh.SetBounds(734, 18, 110, 38); refresh.Anchor = AnchorStyles.Top | AnchorStyles.Right; }

            // keep toolbar buttons pinned to the right as the form resizes
            toolbar.Resize += (s, e) =>
            {
                int right = toolbar.Width - 24;
                if (refresh != null) { refresh.Left = right - refresh.Width; right -= refresh.Width + 8; }
                if (save    != null) { save.Left    = right - save.Width;    right -= save.Width + 8; }
                if (browse  != null) { browse.Left  = right - browse.Width;  right -= browse.Width + 8; }
                if (pathBox != null) pathBox.Width = right - pathBox.Left - 8;
            };

            // ── Footer (Open / extra / Back) ─────────────────────────────────────
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 64, BackColor = ThemeManager.BackgroundColor };
            Move(open, footer);
            Move(extra, footer);
            Move(back, footer);
            StyleToolbarButton(open,  "Open");
            StyleToolbarButton(extra, extra?.Text);
            StyleToolbarButton(back,  "Back");
            if (open  != null) open.SetBounds(24, 12, 130, 40);
            if (extra != null) extra.SetBounds(164, 12, 130, 40);
            if (back  != null) { back.Size = new Size(140, 40); back.Anchor = AnchorStyles.Top | AnchorStyles.Right; }
            footer.Resize += (s, e) => { if (back != null) back.Location = new Point(footer.Width - back.Width - 24, 12); };

            // ── Grid body ────────────────────────────────────────────────────────
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24, 12, 24, 12), BackColor = ThemeManager.BackgroundColor };
            if (grid != null)
            {
                form.Controls.Remove(grid);
                grid.Dock = DockStyle.Fill;
                body.Controls.Add(grid);
            }

            // dock order: Fill first (back), then bottom, then the two tops (toolbar, header-last = topmost)
            form.Controls.Add(body);
            form.Controls.Add(footer);
            form.Controls.Add(toolbar);
            form.Controls.Add(header);

            if (back != null) back.Location = new Point(footer.Width - back.Width - 24, 12);
            form.ResumeLayout(true);
        }

        private static void StyleToolbarButton(Button b, string text)
        {
            if (b == null) return;
            if (text != null) b.Text = text;
            ThemeManager.StyleButton(b);
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
    }
}
