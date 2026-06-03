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
    public partial class CourseDetails : BaseForm
    {
        private string connectionString = DbHelper.ConnectionString;
        private string uName;

        public CourseDetails(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            LoadCourses();
            this.Load += (s, e) => PageChrome.Apply(this, "Course Details");
        }
        private void LoadCourses()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Added [username] and [role] to the SELECT query
                    string query = "SELECT [course id], [course name], [Description], [Price], [materials], [username], [role] FROM Course";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_back_Click(object sender, EventArgs e)
        {
            // Return to the dashboard of whoever is logged in (admin stays admin).
            NavigationHelper.GoToDashboard(this, uName);
        }

        private string GetUserRole(string username)
        {
            string role = "moderator"; //default role
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT role FROM signup WHERE username = @username"; // Fixed table name
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string selectedCourseId = selectedRow.Cells["course id"].Value?.ToString();

            if (string.IsNullOrEmpty(selectedCourseId))
            {
                MessageBox.Show("Invalid course selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteCourse(selectedCourseId);
            }
        }
        private void DeleteCourse(string courseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //first, delete related payments
                    string deletePaymentsQuery = "DELETE FROM Payments WHERE course_id = @courseId";
                    using (SqlCommand cmdPayments = new SqlCommand(deletePaymentsQuery, conn))
                    {
                        cmdPayments.Parameters.AddWithValue("@courseId", courseId);
                        cmdPayments.ExecuteNonQuery();
                    }

                    //now, delete the course
                    string deleteQuery = "DELETE FROM Course WHERE [course id] = @courseId";
                    using (SqlCommand cmdCourse = new SqlCommand(deleteQuery, conn))
                    {
                        cmdCourse.Parameters.AddWithValue("@courseId", courseId);
                        int rowsAffected = cmdCourse.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCourses(); 
                        }
                        else
                        {
                            MessageBox.Show("Error deleting course.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting course: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CourseDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
