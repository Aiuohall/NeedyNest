using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NeedyNest
{
    public partial class EditProfiledashboard: BaseForm
    {

        private string currentUsername;
        private string connectionString= DbHelper.ConnectionString;

        public EditProfiledashboard(String username)
        {
            InitializeComponent();
            currentUsername = username;
            LoadDetails();
            this.Load += (s, e) => PageChrome.Apply(this, "Edit Profile");
        }

        private void LoadDetails()
        {
            try
            {
                string query = "SELECT username, password, contact_number, uni_name FROM signup WHERE username = @username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", currentUsername);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtUsername.Text = reader["username"].ToString();
                            // Passwords are hashed and cannot be shown. Leave blank;
                            // the user only types here to CHANGE it.
                            txtPassword.Text = "";
                            txtContact.Text = reader["contact_number"].ToString();
                            txtUni.Text = reader["uni_name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }



        private void EditProfiledashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_back_Click(object sender, EventArgs e)
        {
            new userdashboard(currentUsername).Show();
            this.Hide();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            string newUsername = txtUsername.Text.Trim();
            string newPassword = txtPassword.Text.Trim();
            string newContact = txtContact.Text.Trim();
            string newUni = txtUni.Text.Trim();

            if (string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Username is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password is optional — only changed when the user types a new one.
            bool changePassword = !string.IsNullOrEmpty(newPassword);
            string newHash = changePassword ? PasswordHelper.Hash(newPassword) : null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Update signup table (password only if a new one was entered)
                            string updateSignup = changePassword
                                ? @"UPDATE signup SET username=@newUsername, password=@newPassword, contact_number=@contact, uni_name=@uni WHERE username=@oldUsername"
                                : @"UPDATE signup SET username=@newUsername, contact_number=@contact, uni_name=@uni WHERE username=@oldUsername";

                            using (SqlCommand cmd = new SqlCommand(updateSignup, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@newUsername", newUsername);
                                if (changePassword) cmd.Parameters.AddWithValue("@newPassword", newHash);
                                cmd.Parameters.AddWithValue("@contact", string.IsNullOrEmpty(newContact) ? DBNull.Value : (object)newContact);
                                cmd.Parameters.AddWithValue("@uni", string.IsNullOrEmpty(newUni) ? DBNull.Value : (object)newUni);
                                cmd.Parameters.AddWithValue("@oldUsername", currentUsername);
                                cmd.ExecuteNonQuery();
                            }

                            // Update login table
                            string updateLogin = changePassword
                                ? @"UPDATE login SET username=@newUsername, password=@newPassword WHERE username=@oldUsername"
                                : @"UPDATE login SET username=@newUsername WHERE username=@oldUsername";

                            using (SqlCommand cmd = new SqlCommand(updateLogin, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@newUsername", newUsername);
                                if (changePassword) cmd.Parameters.AddWithValue("@newPassword", newHash);
                                cmd.Parameters.AddWithValue("@oldUsername", currentUsername);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            // Update current username if changed
                            if (currentUsername != newUsername)
                            {
                                currentUsername = newUsername;
                            }

                            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }


            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}\nCode: {sqlEx.Number}", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
    }

