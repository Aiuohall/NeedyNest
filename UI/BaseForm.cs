using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NeedyNest.UI
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            DoubleBuffered      = true;
            Padding             = new Padding(12);
            Font                = ThemeManager.DefaultFont;
            BackColor           = ThemeManager.BackgroundColor;
            ForeColor           = ThemeManager.ForegroundColor;
            StartPosition       = FormStartPosition.CenterScreen;
            FormBorderStyle     = FormBorderStyle.Sizable; // allow resize + maximize
            MaximizeBox         = true;
            AutoScaleMode       = AutoScaleMode.Font;

            Load += BaseForm_Load;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTo(this);
            AddStatusStrip();
        }

        /// <summary>
        /// When this form becomes visible, tidy up any forms left in the background
        /// by the app's "show next / hide current" navigation, so they don't pile up.
        /// Deferred via BeginInvoke so the caller's own <c>this.Hide()</c> runs first.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (Modal) return;            // never disturb a modal dialog or its parent
            BeginInvoke((Action)CloseBackgroundForms);
        }

        private void CloseBackgroundForms()
        {
            foreach (Form f in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (f == this || f.Modal || f.Owner != null) continue;
                // Keep the login window alive (it owns the app's lifetime) but hidden.
                if (f.GetType().Name == "Login") { f.Hide(); continue; }
                f.Close();
            }
        }

        private void AddStatusStrip()
        {
            if (string.IsNullOrEmpty(Session.LoggedInUsername)) return;

            // Check if one already exists (avoid duplicates on re-load)
            foreach (Control c in Controls)
            {
                if (c is StatusStrip) return;
            }

            var strip = new StatusStrip
            {
                BackColor  = ThemeManager.PrimaryColor,
                ForeColor  = Color.White,
                Font       = ThemeManager.SmallFont,
                SizingGrip = false,
                Padding    = new Padding(4, 0, 4, 0)
            };

            var userLabel = new ToolStripStatusLabel
            {
                Text      = $"  Logged in as:  {Session.LoggedInUsername}",
                ForeColor = Color.White,
                Font      = ThemeManager.SmallFont
            };

            var roleLabel = new ToolStripStatusLabel
            {
                Text      = $"Role: {Session.LoggedInRole}",
                ForeColor = Color.FromArgb(200, 230, 240),
                Font      = ThemeManager.SmallFont,
                Spring    = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            strip.Items.Add(userLabel);
            strip.Items.Add(roleLabel);
            Controls.Add(strip);
        }
    }
}
