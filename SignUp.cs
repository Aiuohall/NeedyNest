using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class signupform : BaseForm
    {
        
        private SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;");

        public signupform()
        {
            InitializeComponent();
            usernametextbox.TextChanged += usernametextbox_TextChanged;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

        }

        private void usernametextbox_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null && usernametextbox.Text.Length > 0)
            {
                MessageBox.Show("Please select a role first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernametextbox.Text = "";
                comboBox1.Focus();
            }
        }

        public bool IsUsernameExists(string username)
        {
            // SQL query to check if the username exists in your database
            string query = "SELECT COUNT(*) FROM [NeedyNest].[dbo].[signup] WHERE [username] = @Username";

            // Assuming you have a SqlConnection setup
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string rolePrefix = "";
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Admin":
                    rolePrefix = "ad-";
                    break;
                case "Mod":
                    rolePrefix = "mod-";  // Ensure "mod-" is applied correctly
                    break;
                case "User":
                    rolePrefix = "us-";
                    break;
            }

            // Remove any existing prefix before applying a new one
            string currentUsername = usernametextbox.Text.Trim();

            if (currentUsername.Contains("-"))
            {
                int index = currentUsername.IndexOf("-") + 1;
                currentUsername = currentUsername.Substring(index); // Remove the old prefix
            }

            // Ensure the correct prefix is applied
            usernametextbox.Text = rolePrefix + currentUsername;
            usernametextbox.SelectionStart = usernametextbox.Text.Length;
        }

        private void button1_Click(object sender, EventArgs e)
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
                    @"INSERT INTO signup 
                    (first_name, last_name, role, username, uni_name, contact_number, password, status) 
                    VALUES 
                    (@FirstName, @LastName, @Role, @Username, @UniName, @ContactNumber, @Password, 0)", con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstnametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", lastnametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@Role", comboBox1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Username", usernametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@UniName", uninametextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactNumber", contactnumtextbox.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", passtextbox.Text.Trim());

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful! Await admin approval.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        new Login().Show();
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

        private void frstnamelabel_Click(object sender, EventArgs e)
        {

        }

        private void passlabel_Click(object sender, EventArgs e)
        {

        }

        private void signupform_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void backbutton_Click(object sender, EventArgs e)
        {
            new Login().Show();
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
    }
}