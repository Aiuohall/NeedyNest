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

namespace NeedyNest
{
    
    public partial class AddUser : BaseForm
    {
        string uName;

        private SqlConnection con = DbHelper.GetConnection();

        public AddUser(string uName)
        {
            this.uName= uName;
            InitializeComponent();
            this.Load += (s, e) => PageChrome.Apply(this, "Add User");
        }

        private void uninametextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstnametextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Approvebutton_Click(object sender, EventArgs e)
        {
            if (firstnametextbox.Text == "" || lastnametextbox.Text == "" || usernametextbox.Text == "" ||
               uninametextbox.Text == "" || contactnumtextbox.Text == "" || passtextbox.Text == "" || confirmpasstextbox.Text == "")
            {
                MessageBox.Show("Please fill up all the information.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (passtextbox.Text != confirmpasstextbox.Text)
            {
                MessageBox.Show("Password doesn't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!long.TryParse(contactnumtextbox.Text, out _) || contactnumtextbox.Text.Length != 11)
            {
                MessageBox.Show("Contact number must be 11 digits and numeric only.", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string contactNumber = contactnumtextbox.Text.Trim();
            if (!contactNumber.StartsWith("+880"))
            {
                if (contactNumber.Length == 11)
                {
                    contactNumber = "+880" + contactNumber.Substring(1);
                }
                else
                {
                    MessageBox.Show("Contact number must be 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            contactnumtextbox.Text = contactNumber;

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO signup (first_name, last_name, role, username, uni_name, contact_number, password, status) " +
                    "VALUES (@FirstName, @LastName, @Role, @Username, @UniName, @ContactNumber, @Password, 1)", con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstnametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", lastnametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@Role", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Username", usernametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@UniName", uninametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNumber", contactnumtextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", PasswordHelper.Hash(passtextbox.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Status", 1);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        new manageuserdashboardform(uName).Show();
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open) con.Close();
            }
        }

        private void lastnametextbox_TextChanged(object sender, EventArgs e)
        {

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

        private void backbutton_Click(object sender, EventArgs e)
        {
            new manageuserdashboardform(uName).Show();
            this.Hide();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }
    }
}
