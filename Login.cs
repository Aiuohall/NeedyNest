using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;
using System.Data.SqlClient;

namespace NeedyNest
{
    public partial class Login : BaseForm
    {

        string uName;

        // ── Built-in offline test accounts ───────────────────────────────────────
        // Lets you verify the UI / navigation when the database is unavailable.
        // Format: username -> (password, role).  Remove or disable before release.
        private static readonly Dictionary<string, (string Password, string Role)> TestAccounts =
            new Dictionary<string, (string, string)>(StringComparer.OrdinalIgnoreCase)
        {
            { "admin",       ("admin123", "Admin") },
            { "user",        ("user123",  "User") },
            { "moderator",   ("mod123",   "Moderator") },
            { "distributor", ("dist123",  "Distributor") },
        };

        // Hint banner shown when the database is offline, listing the test logins.
        private readonly Label lblTestHint = new Label();

        public Login()
        {
            InitializeComponent();
            //this.uName = uName;

            lblTestHint.Dock = DockStyle.Bottom;
            lblTestHint.Height = 64;
            lblTestHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTestHint.BackColor = System.Drawing.Color.FromArgb(255, 248, 225);
            lblTestHint.ForeColor = System.Drawing.Color.FromArgb(120, 80, 0);
            lblTestHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            lblTestHint.Text =
                "Database offline — use a test account:   " +
                "admin / admin123   •   user / user123   •   moderator / mod123   •   distributor / dist123";
            lblTestHint.Visible = false;
            this.Controls.Add(lblTestHint);
            lblTestHint.BringToFront();
        }

        SqlConnection con = DbHelper.GetConnection();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Silently probe the database. If it's offline we don't nag the user
            // with a popup — the built-in test accounts still allow login.
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                {
                    con.Open();
                    lblTestHint.Visible = false; // DB is up; hide the offline hint
                }
            }
            catch
            {
                lblTestHint.Visible = true; // DB is down; show the test-account hint
            }
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Check if fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ── Offline test-account bypass (no database required) ───────────────
            if (TestAccounts.TryGetValue(username, out var account))
            {
                if (account.Password != password)
                {
                    MessageBox.Show("Password incorrect.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Session.LoggedInUsername = username;
                Session.LoggedInRole = account.Role;
                uName = username;
                OpenDashboard(account.Role);
                this.Hide();
                return;
            }

            // Query to check if the username exists
            string userQuery = "SELECT * FROM signup WHERE username = @Username";

            try
            {
            using (SqlConnection con = DbHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(userQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        // If no user found
                        MessageBox.Show("User not found.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // User found, now check the password
                        if (dt.Rows[0]["password"].ToString() != password)
                        {
                            MessageBox.Show("Password incorrect.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Check if account is enabled
                        if (dt.Rows[0]["status"].ToString() == "1")
                        {
                            string role = dt.Rows[0]["role"].ToString();

                            // Log the successful login attempt in the database
                            string insertLoginQuery = "INSERT INTO login (username, password, role, login_time) VALUES (@Username, @Password, @Role, GETDATE())";

                            using (SqlCommand insertCmd = new SqlCommand(insertLoginQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@Username", username);
                                insertCmd.Parameters.AddWithValue("@Password", password); // Consider hashing passwords before storing
                                insertCmd.Parameters.AddWithValue("@Role", role);
                                insertCmd.ExecuteNonQuery();
                            }
                            Session.LoggedInUsername = username;
                            Session.LoggedInRole = role;
                            uName = username;

                            OpenDashboard(role);
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Your account is disabled. Please contact the admin.", "Account Disabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Could not reach the database.\n\n" +
                    "The database appears to be offline. You can still verify the app " +
                    "using a built-in test account (see the hint on the login screen).\n\n" +
                    "Details: " + ex.Message,
                    "Database Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Routes a logged-in user to the dashboard for their role.
        private void OpenDashboard(string role)
        {
            switch (role)
            {
                case "Admin":
                    new admindashboardform(uName).Show();
                    break;
                case "Moderator":
                    new moderatordash(uName).Show();
                    break;
                case "User":
                    new userdashboard(uName).Show();
                    break;
                case "Distributor":
                    new form_distributor(uName).Show();
                    break;
                default:
                    MessageBox.Show("Unknown role. Please contact the admin.", "Login Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }








        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void signupbutton_Click(object sender, EventArgs e)
        { signupform form2 = new signupform();
            this.Hide();
            form2.Show();
            

        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Confirmation",
                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            
                    
                   



            }
    }
}