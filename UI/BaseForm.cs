using System;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            DoubleBuffered = true;
            Padding = new Padding(10);
            Font = ThemeManager.DefaultFont;
            BackColor = ThemeManager.BackgroundColor;
            ForeColor = ThemeManager.ForegroundColor;
            StartPosition = FormStartPosition.CenterScreen;
            Load += BaseForm_Load;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTo(this);
        }
    }
}
