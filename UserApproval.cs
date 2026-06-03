using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class UserApproval : BaseForm
    {
        
        private readonly string _userName;
        private readonly SqlConnection con = DbHelper.GetConnection();

        public UserApproval(string userName)
        {
            InitializeComponent();
            _userName = userName; // Store the username
            this.Load += UserApproval_Load;
            this.Load += (s, e) => PageChrome.Apply(this, "Member Approval");
            // Disable editing of fields (if needed)
            //SetControlsReadOnly();
        }

        private void UserApproval_Load(object sender, EventArgs e)
        {
            LoadUserDetails(_userName);
        }

        private void LoadUserDetails(string username)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT first_name, last_name, role, username, uni_name, contact_number, password " +
                    "FROM signup WHERE username = @Username", con)) // Removed "AND status = 0"
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate form controls with data
                        firstnametextbox.Text = reader["first_name"].ToString();
                        lastnametextbox.Text = reader["last_name"].ToString();
                        comboBox1.SelectedItem = reader["role"].ToString();
                        usernametextbox.Text = reader["username"].ToString();
                        uninametextbox.Text = reader["uni_name"].ToString();
                        contactnumtextbox.Text = reader["contact_number"].ToString();
                        // Passwords are hashed — never displayed during review.
                        passtextbox.Text = "";
                        confirmpasstextbox.Text = "";
                        passtextbox.ReadOnly = true;
                        confirmpasstextbox.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        private void SetControlsReadOnly()
        {
            // Make all fields read-only except buttons
            firstnametextbox.ReadOnly = true;
            lastnametextbox.ReadOnly = true;
            comboBox1.Enabled = false; // Disable role selection
            usernametextbox.ReadOnly = true;
            uninametextbox.ReadOnly = true;
            contactnumtextbox.ReadOnly = true;
            passtextbox.ReadOnly = true;
            confirmpasstextbox.ReadOnly = true;
        }

        // Approve button click
        private void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateUserStatus(1); // Set status to "Approved"
        }

        // Reject button click
        private void btnReject_Click(object sender, EventArgs e)
        {
            UpdateUserStatus(2); // Set status to "Rejected" (or delete)
        }

        private void UpdateUserStatus(int status)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE signup SET status = @Status WHERE username = @Username", con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Username", _userName);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        string action = status == 1 ? "approved" : "rejected";
                        MessageBox.Show($"User {_userName} has been {action}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            new manageuserdashboardform(_userName).Show();
            this.Hide();
            
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            firstnametextbox.Clear();
            lastnametextbox.Clear();
            usernametextbox.Clear();
            uninametextbox.Clear();
            contactnumtextbox.Clear();
            passtextbox.Clear();
            confirmpasstextbox.Clear();
            comboBox1.SelectedItem = null;
            comboBox1.Text = "Select Role";
            firstnametextbox.Focus();
        }

        private void UserApproval_Load_1(object sender, EventArgs e)
        {

        }
    }
}