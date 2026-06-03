using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class Course : BaseForm
    {
        private string uName;
        private readonly string connectionString = DbHelper.ConnectionString;

        public Course(string uName)
        {
            this.uName = uName;
            InitializeComponent();
        }

        private void button_browse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Select Course Material",
                Filter = "PDF Files|*.pdf|Word Documents|*.doc;*.docx|Text Files|*.txt|Audio Files|*.mp3|Video Files|*.mp4;*.mkv|All Files|*.*",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath.Text = openFileDialog1.FileName;
                filepath_label.Text = openFileDialog1.FileName;
            }
        }

        private void button_add_Click_1(object sender, EventArgs e)
        {
            string courseName = textBox_coursename.Text.Trim();
            string description = textBox_description.Text.Trim();
            string price = textBoxprice.Text.Trim();
            string materials = filepath.Text.Trim();
            string role = GetUserRole(uName);  // Fetch role from corrected table

            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Course Name and Price are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = DbHelper.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Course ([course name], [Description], [Price], [materials], [username], [role]) " +
                               "VALUES (@name, @desc, @price, @materials, @username, @role)";  // No 'course id'

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", courseName);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@materials", materials);
                cmd.Parameters.AddWithValue("@username", uName);
                cmd.Parameters.AddWithValue("@role", role);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Course added successfully!" : "Error adding course.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            string userRole = GetUserRole(uName);
            if (userRole == "admin")
            {
                new admindashboardform(uName).Show();
            }
            else
            {
                new moderatordash(uName).Show();
            }
            this.Hide();
        }

        private string GetUserRole(string username)
        {
            string role = "moderator";  // Default role if not found
            try
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT role FROM [dbo].[signup] WHERE username = @username";  // Use the correct table

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            role = result.ToString().ToLower();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching user role: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return role;
        }

        private void LoadCourses()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1000 [course id], [course name], [Description], [Price], [materials], [username], [role] FROM Course";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Example of how to retrieve values:
                                string courseName = reader["course name"].ToString();
                                string coursePrice = reader["Price"].ToString();
                                // You can populate a ListView, DataGridView, or any other UI element here
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_coursename.Clear();
            textBox_description.Clear();
            textBoxprice.Clear();
            filepath.Clear();
            filepath_label.Text = "";
        }

        private void filepath_label_Click(object sender, EventArgs e)
        {

        }

        private void Course_Load(object sender, EventArgs e)
        {

        }
    }
}
