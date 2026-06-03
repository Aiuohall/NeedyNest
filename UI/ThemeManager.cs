using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    public static class ThemeManager
    {
        // ── Palette ─────────────────────────────────────────────────────────────
        public static Color PrimaryColor   { get; } = Color.FromArgb(11, 57, 84);   // deep teal-navy
        public static Color HoverColor     { get; } = Color.FromArgb(8, 126, 139);  // mid teal
        public static Color AccentColor    { get; } = Color.FromArgb(23, 168, 178); // light teal
        public static Color BackgroundColor{ get; } = Color.FromArgb(240, 244, 248);
        public static Color SurfaceColor   { get; } = Color.White;
        public static Color ForegroundColor{ get; } = Color.FromArgb(29, 41, 57);
        public static Color SubtleText     { get; } = Color.FromArgb(102, 112, 133);
        public static Color DangerColor    { get; } = Color.FromArgb(180, 35, 24);  // delete/logout
        public static Color DangerHover    { get; } = Color.FromArgb(211, 47, 47);
        public static Color SecondaryColor { get; } = Color.FromArgb(84, 110, 122); // back/cancel
        public static Color SecondaryHover { get; } = Color.FromArgb(96, 125, 139);
        public static Color SuccessColor   { get; } = Color.FromArgb(27, 94, 32);
        public static Color GridRowAlt     { get; } = Color.FromArgb(245, 248, 250);
        public static Color GridLine       { get; } = Color.FromArgb(216, 225, 232);
        public static Color BorderColor    { get; } = Color.FromArgb(208, 219, 230);

        public static Font DefaultFont     { get; } = new Font("Segoe UI", 9.25F, FontStyle.Regular, GraphicsUnit.Point);
        public static Font BoldFont        { get; } = new Font("Segoe UI", 9.25F, FontStyle.Bold,    GraphicsUnit.Point);
        public static Font TitleFont       { get; } = new Font("Segoe UI", 13F,   FontStyle.Bold,    GraphicsUnit.Point);
        public static Font SmallFont       { get; } = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);

        // ── Initialise ──────────────────────────────────────────────────────────
        public static void Initialize()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        // ── Entry point: called by BaseForm.Load ─────────────────────────────────
        public static void ApplyTo(Form form)
        {
            if (form == null) return;
            form.BackColor  = BackgroundColor;
            form.ForeColor  = ForegroundColor;
            form.Font       = DefaultFont;
            ApplyToControls(form.Controls);
        }

        // ── Recursive control styler ─────────────────────────────────────────────
        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                try { StyleControl(ctrl); }
                catch { /* safely skip controls that throw */ }

                if (ctrl.HasChildren)
                    ApplyToControls(ctrl.Controls);
            }
        }

        private static void StyleControl(Control ctrl)
        {
            ctrl.Font = DefaultFont;

            if (ctrl is Button btn)
            {
                StyleButton(btn);
                return;
            }

            if (ctrl is TextBox tb)
            {
                tb.BackColor    = SurfaceColor;
                tb.ForeColor    = ForegroundColor;
                tb.BorderStyle  = BorderStyle.FixedSingle;
                tb.Font         = DefaultFont;
                return;
            }

            if (ctrl is ComboBox cb)
            {
                cb.BackColor = SurfaceColor;
                cb.ForeColor = ForegroundColor;
                cb.FlatStyle = FlatStyle.Flat;
                return;
            }

            if (ctrl is DataGridView dgv)
            {
                StyleDataGridView(dgv);
                return;
            }

            if (ctrl is Label lbl)
            {
                lbl.ForeColor = ForegroundColor;
                // Detect title labels by larger font size set in Designer
                if (lbl.Font != null && lbl.Font.Size >= 14)
                    lbl.Font = TitleFont;
                return;
            }

            if (ctrl is Panel pnl)
            {
                // Header panels (named *header* or *title*) get primary colour
                string n = pnl.Name.ToLower();
                if (n.Contains("header") || n.Contains("title") || n.Contains("top"))
                {
                    pnl.BackColor = PrimaryColor;
                    pnl.ForeColor = Color.White;
                }
                else
                {
                    pnl.BackColor = Color.Transparent;
                    pnl.ForeColor = ForegroundColor;
                }
                return;
            }

            if (ctrl is GroupBox gb)
            {
                gb.ForeColor = SubtleText;
                gb.Font      = SmallFont;
                return;
            }

            if (ctrl is CheckBox chk)
            {
                chk.ForeColor = ForegroundColor;
                return;
            }

            if (ctrl is RadioButton rb)
            {
                rb.ForeColor = ForegroundColor;
                return;
            }

            if (ctrl is MenuStrip ms)
            {
                ms.BackColor  = PrimaryColor;
                ms.ForeColor  = Color.White;
                ms.RenderMode = ToolStripRenderMode.System;
                return;
            }

            if (ctrl is ToolStrip ts)
            {
                ts.BackColor = PrimaryColor;
                ts.ForeColor = Color.White;
                return;
            }

            if (ctrl is ListBox lb)
            {
                lb.BackColor  = SurfaceColor;
                lb.ForeColor  = ForegroundColor;
                lb.BorderStyle = BorderStyle.FixedSingle;
                return;
            }

            if (ctrl is DateTimePicker dtp)
            {
                dtp.CalendarMonthBackground = SurfaceColor;
                dtp.CalendarForeColor       = ForegroundColor;
                return;
            }

            // Default
            ctrl.ForeColor = ForegroundColor;
        }

        // ── Button styling with hover effects ────────────────────────────────────
        public static void StyleButton(Button btn)
        {
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize  = 0;
            btn.ForeColor   = Color.White;
            btn.Cursor      = Cursors.Hand;
            btn.Font        = BoldFont;
            btn.Padding     = new Padding(6, 3, 6, 3);

            // Categorise by button name / text
            Color baseColor = ClassifyButton(btn);

            btn.BackColor = baseColor;
            btn.Tag       = baseColor; // remember for hover-out

            // Remove stale handlers before adding (safe to call multiple times)
            btn.MouseEnter -= OnButtonEnter;
            btn.MouseLeave -= OnButtonLeave;
            btn.MouseEnter += OnButtonEnter;
            btn.MouseLeave += OnButtonLeave;
        }

        private static Color ClassifyButton(Button btn)
        {
            string key = (btn.Name + " " + btn.Text).ToLower();

            bool isDanger    = key.Contains("logout") || key.Contains("delete") || key.Contains("exit")
                            || key.Contains("remove") || key.Contains("reject") || key.Contains("delet");
            bool isSecondary = key.Contains("back")   || key.Contains("cancel") || key.Contains("clear")
                            || key.Contains("close");
            bool isSuccess   = key.Contains("approv") || key.Contains("save")   || key.Contains("confirm");

            if (isDanger)    return DangerColor;
            if (isSecondary) return SecondaryColor;
            if (isSuccess)   return SuccessColor;
            return PrimaryColor;
        }

        private static void OnButtonEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                Color base_ = btn.Tag is Color c ? c : PrimaryColor;
                if (base_ == DangerColor)    btn.BackColor = DangerHover;
                else if (base_ == SecondaryColor) btn.BackColor = SecondaryHover;
                else if (base_ == SuccessColor)   btn.BackColor = Color.FromArgb(56, 142, 60);
                else                              btn.BackColor = HoverColor;
            }
        }

        private static void OnButtonLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Color c)
                btn.BackColor = c;
        }

        // ── DataGridView ──────────────────────────────────────────────────────────
        private static void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor           = SurfaceColor;
            dgv.BorderStyle               = BorderStyle.None;
            dgv.GridColor                 = GridLine;
            dgv.ForeColor                 = ForegroundColor;
            dgv.CellBorderStyle           = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible         = false;
            dgv.SelectionMode             = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows     = false;
            dgv.MultiSelect               = false;

            // Header row
            dgv.ColumnHeadersDefaultCellStyle.BackColor  = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font       = BoldFont;
            dgv.ColumnHeadersDefaultCellStyle.Padding    = new Padding(6, 0, 6, 0);
            dgv.ColumnHeadersHeight                      = 36;
            dgv.ColumnHeadersBorderStyle                 = DataGridViewHeaderBorderStyle.None;

            // Data rows
            dgv.DefaultCellStyle.BackColor               = SurfaceColor;
            dgv.DefaultCellStyle.ForeColor               = ForegroundColor;
            dgv.DefaultCellStyle.Font                    = DefaultFont;
            dgv.DefaultCellStyle.SelectionBackColor      = AccentColor;
            dgv.DefaultCellStyle.SelectionForeColor      = Color.White;
            dgv.DefaultCellStyle.Padding                 = new Padding(4, 0, 4, 0);
            dgv.RowTemplate.Height                       = 30;

            // Alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor          = GridRowAlt;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = AccentColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }
    }
}
