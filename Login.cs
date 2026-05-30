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
        public Login()
        {
            InitializeComponent();
            //this.uName = uName;
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;"))
                {
                    try
                    {
                        con.Open();
                        //MessageBox.Show("Database Connected Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Database Connection Failed!\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

            // Query to check if the username exists
            string userQuery = "SELECT * FROM signup WHERE username = @Username";

            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;"))
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
                            // Store the logged-in username globally
                            Session.LoggedInUsername = username;
                            uName=username;

                            // Redirect to respective dashboards
                            if (role == "Admin")
                            {
                                new admindashboardform(uName).Show();
                            }
                            else if (role == "Moderator")
                            {
                                new moderatordash(uName).Show();
                            }
                            else if (role == "User")
                            {
                                new userdashboard(uName).Show();
                            }
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