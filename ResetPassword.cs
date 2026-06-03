using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    /// <summary>
    /// Self-service password reset. The member proves ownership by entering their
    /// username together with the contact number they registered with; on a match
    /// they may set a new password (stored hashed).
    /// </summary>
    public partial class ResetPassword : BaseForm
    {
        private TextBox txtUser, txtContact, txtNew, txtConfirm;

        public ResetPassword()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text         = "Reset Password";
            ClientSize   = new Size(560, 460);
            MinimumSize  = new Size(480, 420);
            MaximizeBox  = false;
            Padding      = new Padding(0);

            var header = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = ThemeManager.PrimaryColor };
            header.Controls.Add(new Label
            {
                Text = "Reset Password", ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), AutoSize = true, Location = new Point(22, 18)
            });
            Controls.Add(header);

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(40, 24, 40, 16), BackColor = ThemeManager.BackgroundColor };
            Controls.Add(body);
            body.BringToFront();

            int y = 10;
            txtUser    = AddField(body, "Username", ref y, false);
            txtContact = AddField(body, "Registered Contact Number", ref y, false);
            txtNew     = AddField(body, "New Password", ref y, true);
            txtConfirm = AddField(body, "Confirm New Password", ref y, true);

            var btnReset = new Button { Text = "Reset Password", Size = new Size(200, 44), Location = new Point(40, y + 14) };
            ThemeManager.StyleButton(btnReset);
            btnReset.Click += (s, e) => DoReset();

            var btnBack = new Button { Text = "Back to Login", Size = new Size(160, 44), Location = new Point(252, y + 14) };
            ThemeManager.StyleButton(btnBack);
            btnBack.Click += (s, e) => { new Login().Show(); this.Hide(); };

            body.Controls.Add(btnReset);
            body.Controls.Add(btnBack);
        }

        private TextBox AddField(Panel parent, string caption, ref int y, bool password)
        {
            parent.Controls.Add(new Label
            {
                Text = caption, AutoSize = true, ForeColor = ThemeManager.ForegroundColor,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), Location = new Point(40, y)
            });
            var tb = new TextBox
            {
                Location = new Point(40, y + 22), Width = 460,
                Font = new Font("Segoe UI", 11F), UseSystemPasswordChar = password
            };
            parent.Controls.Add(tb);
            y += 64;
            return tb;
        }

        private void DoReset()
        {
            string user    = txtUser.Text.Trim();
            string contact = txtContact.Text.Trim();
            string pass    = txtNew.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(contact) ||
                string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Please fill in all fields.", "Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pass != confirm)
            {
                MessageBox.Show("The new passwords do not match.", "Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();

                    // Verify identity: username + registered contact number must match.
                    int matches;
                    using (var check = new SqlCommand(
                        "SELECT COUNT(*) FROM signup WHERE username = @u AND contact_number = @c", con))
                    {
                        check.Parameters.AddWithValue("@u", user);
                        check.Parameters.AddWithValue("@c", contact);
                        matches = (int)check.ExecuteScalar();
                    }

                    if (matches == 0)
                    {
                        MessageBox.Show("No account matches that username and contact number.",
                            "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (var upd = new SqlCommand(
                        "UPDATE signup SET password = @p WHERE username = @u", con))
                    {
                        upd.Parameters.AddWithValue("@p", PasswordHelper.Hash(pass));
                        upd.Parameters.AddWithValue("@u", user);
                        upd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Your password has been reset. You can now log in.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new Login().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "ResetPassword");
                MessageBox.Show("Error resetting password: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
