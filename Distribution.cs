using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class Distribution : BaseForm
    {
        string uName;
        public Distribution(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Existing code
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && File.Exists(textBox1.Text))
            {
                if (saveFile(textBox1.Text))
                {
                    MessageBox.Show("File saved successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid file.");
            }
        }

        private bool saveFile(string filepath)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(filepath);
                string extn = Path.GetExtension(filepath);
                string name = Path.GetFileName(filepath);

                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();

                    // Retrieve role from signup table
                    string role;
                    using (SqlCommand roleCmd = new SqlCommand(
                        "SELECT role FROM signup WHERE username = @username", cn))
                    {
                        roleCmd.Parameters.AddWithValue("@username", uName);
                        object result = roleCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("User role not found in signup table.");
                            return false;
                        }
                        role = result.ToString();
                    }

                    // Insert into Distribution table
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Distribution (Data, FileName, extension, username, role) " +
                        "VALUES (@data, @name, @extn, @username, @role)", cn))
                    {
                        cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                        cmd.Parameters.Add("@extn", SqlDbType.Char, 10).Value = extn;
                        cmd.Parameters.AddWithValue("@username", uName);
                        cmd.Parameters.AddWithValue("@role", role);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;");
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.FileName;
            }
        }

        private void Pdf_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection cn = GetConnection())
            {
                string query = "SELECT id, FileName, extension FROM Distribution";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                OpenFile(id);
            }
            else
            {
                MessageBox.Show("Please select a file to open.");
            }
        }

        private void OpenFile(int id)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT data, FileName, extension FROM Distribution WHERE id = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] data = (byte[])reader["data"];
                            string name = reader["FileName"].ToString();
                            string extn = reader["extension"].ToString();

                            string newFileName = $"{Path.GetFileNameWithoutExtension(name)}_{DateTime.Now:yyyyMMddHHmmss}{extn}";
                            File.WriteAllBytes(newFileName, data);
                            System.Diagnostics.Process.Start(newFileName);
                        }
                        else
                        {
                            MessageBox.Show("File not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            try
            {
                string role = GetUserRole(uName);

                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Error: User role not found. Redirecting to the login page.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    new Login().Show(); // Redirect to login if the role is not found
                    this.Close();
                    return;
                }

                // Navigate based on role
                Form dashboard;
                switch (role.ToLower()) // Convert to lowercase to avoid case sensitivity issues
                {
                    case "admin":
                        dashboard = new admindashboardform(uName);
                        break;
                    case "moderator":
                        dashboard = new moderatordash(uName);
                        break;
                    case "user":
                        dashboard = new userdashboard(uName);
                        break;
                    default:
                        MessageBox.Show("Error: Unrecognized role. Redirecting to login.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        new Login().Show();
                        this.Close();
                        return;
                }

                dashboard.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while navigating: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetUserRole(string username)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT role FROM signup WHERE username = @username", cn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? ""; // Return empty string if role is null
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching user role: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
    }
}