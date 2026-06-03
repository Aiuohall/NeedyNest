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
    public partial class Form_ReceiveBooks : BaseForm
    {
        private string uName;
        private static string ConnectionString => DbHelper.ConnectionString;

        private Timer searchTimer;
        private Label lblNoMatches;

        public Form_ReceiveBooks(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            searchTimer = new Timer { Interval = 300 };
            searchTimer.Tick += SearchTimer_Tick;

            // Create no matches label
            lblNoMatches = new Label
            {
                Text = "No matched items found",
                ForeColor = Color.Red,
                Visible = false,
                Location = new Point(textBox1.Left, textBox1.Bottom + 5),
                AutoSize = true
            };
            this.Controls.Add(lblNoMatches);
            lblNoMatches.BringToFront();
            this.Load += (s, e) => PageChrome.Apply(this, "Receive Books");
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void PerformSearch()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        "SELECT id, FileName, extension FROM Books WHERE FileName LIKE @search", conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox1.Text.Trim() + "%");

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    lblNoMatches.Visible = dt.Rows.Count == 0;

                    if (dataGridView1.Columns["id"] != null)
                        dataGridView1.Columns["id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching books: " + ex.Message);
            }
        }





        private void button_back_Click(object sender, EventArgs e)
        {
            Needer_DashBoard needer_DashBoard = new Needer_DashBoard(uName);
            this.Hide();
            needer_DashBoard.Show();
        }

        private void Form_ReceiveBooks_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            string query = "SELECT id, FileName, extension FROM Books";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns["id"] != null)
                    {
                        dataGridView1.Columns["id"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading books: " + ex.Message);
                }
            }
        }

        private void button2_search_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            string query = "SELECT id, FileName, extension FROM Books WHERE FileName LIKE @search";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchText + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns["id"] != null)
                    {
                        dataGridView1.Columns["id"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching books: " + ex.Message);
                }
            }
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                string fileName = string.Empty;
                byte[] fileData = null;

                // First get the file data
                string query = "SELECT Data, FileName FROM Books WHERE id = @bookId";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@bookId", bookId);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    fileData = (byte[])reader["Data"];
                                    fileName = reader["FileName"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("File not found.");
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error downloading file: " + ex.Message);
                        return;
                    }
                }

                // Now save the file
                if (fileData != null && fileData.Length > 0)
                {
                    if (SaveFile(fileData, fileName))
                    {
                        // Only log the download if file save was successful
                        LogDownload(bookId, this.uName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a file to download.");
            }
        }

        private void LogDownload(int bookId, string username)
        {
            string query = @"INSERT INTO Downloads (Username, BookID, DownloadDate) 
                           VALUES (@username, @bookId, GETDATE())";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error logging download: " + ex.Message);
                }
            }
        }

        private bool SaveFile(byte[] fileData, string fileName)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = fileName;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(saveFileDialog.FileName, fileData);
                        MessageBox.Show("File downloaded successfully!");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message);
            }
            return false;
        }
        private void Form_ReceiveBooks_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
            lblNoMatches.Visible = false;
        }
    }
}