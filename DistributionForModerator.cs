using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class DistributionForModerator : BaseForm
    {
        string uName;
        public DistributionForModerator(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            this.Load += (s, e) =>
            {
                LoadData();
                UploadFormLayout.Apply(this, "Manage Distribution Materials",
                    textBox1, button_browse, button_save,
                    dataGridView1, button_open, button_refresh, button_back, delete);
            };
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

                    // Use the role captured at login (works for every account,
                    // including ones not present in the signup table).
                    string role = string.IsNullOrEmpty(Session.LoggedInRole) ? "Unknown" : Session.LoggedInRole;

                    // Insert into Distribution table
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Distribution (Data, FileName, extension, username, role) " +
                        "VALUES (@data, @name, @extn, @username, @role)", cn))
                    {
                        cmd.Parameters.Add("@data", SqlDbType.VarBinary, -1).Value = buffer; // -1 = MAX (large files)
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

        private SqlConnection GetConnection() => DbHelper.GetConnection();

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
                string query = "SELECT id, FileName, extension, username, role FROM Distribution";
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

                            string newFileName = Path.Combine(Path.GetTempPath(),
                                $"{Path.GetFileNameWithoutExtension(name)}_{DateTime.Now:yyyyMMddHHmmss}{extn.Trim()}");
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
            // Return to the dashboard of whoever is logged in (admin stays admin).
            NavigationHelper.GoToDashboard(this, uName);
        }
        private string GetUserRole(string username)
        {
            string role = "";

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT role FROM signup WHERE username = @username", cn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            role = result.ToString().Trim(); // Ensure no extra spaces
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }

            return role;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection cn = GetConnection())
                        {
                            cn.Open();
                            string query = "DELETE FROM Distribution WHERE id = @id";
                            using (SqlCommand cmd = new SqlCommand(query, cn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Record deleted successfully!");
                            }
                        }
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting record: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }
    }
}