using System;
using System.Drawing;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    public static class ThemeManager
    {
        // Colors chosen for a professional, modern .NET look
        public static Color PrimaryColor { get; private set; } = Color.FromArgb(15, 76, 129);
        public static Color AccentColor { get; private set; } = Color.FromArgb(0, 150, 199);
        public static Color BackgroundColor { get; private set; } = Color.FromArgb(250, 251, 252);
        public static Color SurfaceColor { get; private set; } = Color.White;
        public static Color ForegroundColor { get; private set; } = Color.FromArgb(32, 33, 36);
        public static Font DefaultFont { get; private set; } = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        public static void Initialize() {
            // Placeholder for future theme loading (config, user prefs, etc.)
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        public static void ApplyTo(Form form)
        {
            if (form == null) return;
            form.BackColor = BackgroundColor;
            form.ForeColor = ForegroundColor;
            form.Font = DefaultFont;
            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                try
                {
                    if (ctrl is Panel)
                    {
                        ctrl.BackColor = Color.Transparent;
                        ctrl.ForeColor = ForegroundColor;
                        ctrl.Font = DefaultFont;
                    }
                    else if (ctrl is Label)
                    {
                        ctrl.ForeColor = ForegroundColor;
                        ctrl.Font = DefaultFont;
                    }
                    else if (ctrl is Button btn)
                    {
                        btn.BackColor = PrimaryColor;
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Font = new Font(DefaultFont.FontFamily, 9.25F, FontStyle.SemiBold);
                    }
                    else if (ctrl is TextBox tb)
                    {
                        tb.BackColor = SurfaceColor;
                        tb.ForeColor = ForegroundColor;
                        tb.BorderStyle = BorderStyle.FixedSingle;
                        tb.Font = DefaultFont;
                    }
                    else if (ctrl is ComboBox cb)
                    {
                        cb.BackColor = SurfaceColor;
                        cb.ForeColor = ForegroundColor;
                        cb.Font = DefaultFont;
                    }
                    else if (ctrl is DataGridView dgv)
                    {
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.BackgroundColor = SurfaceColor;
                        dgv.BorderStyle = BorderStyle.None;
                        dgv.GridColor = Color.FromArgb(230, 230, 230);
                        dgv.ForeColor = ForegroundColor;
                        dgv.DefaultCellStyle.Font = DefaultFont;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgv.RowHeadersVisible = false;
                    }
                    else if (ctrl is MenuStrip ms)
                    {
                        ms.BackColor = PrimaryColor;
                        ms.ForeColor = Color.White;
                        ms.RenderMode = ToolStripRenderMode.System;
                        ms.Font = DefaultFont;
                    }
                    else if (ctrl is ToolStrip ts)
                    {
                        ts.BackColor = PrimaryColor;
                        ts.ForeColor = Color.White;
                        ts.Font = DefaultFont;
                    }
                    else if (ctrl is CheckBox cbx)
                    {
                        cbx.ForeColor = ForegroundColor;
                        cbx.Font = DefaultFont;
                    }
                    else
                    {
                        ctrl.ForeColor = ForegroundColor;
                        ctrl.Font = DefaultFont;
                    }

                    if (ctrl.HasChildren)
                        ApplyToControls(ctrl.Controls);
                }
                catch
                {
                    // Safely ignore controls that can't be styled
                }
            }
        }
    }
}
