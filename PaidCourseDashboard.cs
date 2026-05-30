using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class PaidCourseDashboard : BaseForm
    {
        private string uName;
        private string selectedCourseId;
        private decimal coursePrice;
       
     

       private string connectionString = @"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;";

        public PaidCourseDashboard(string uName)
        {
            InitializeComponent();
            LoadCourses();
            this.uName = uName;
            AddOpenButtonColumn();
            InitializePaymentControls();

            
            dataGridView1.CellClick += DataGridView1_CellClick;
        }
        // Add these new methods to the class
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridView1.Columns["Open"].Index)
                return;

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            string courseId = selectedRow.Cells["course id"].Value?.ToString();

            if (string.IsNullOrEmpty(courseId))
            {
                MessageBox.Show("Invalid course selection.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsUserEnrolled(courseId))
            {
                MessageBox.Show("Please enroll in the course before accessing it.", "Enrollment Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenCourseContent(courseId);
        }

        private bool IsUserEnrolled(string courseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT COUNT(*) FROM Enrolled 
                           WHERE username = @username AND [course id] = @courseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", uName);
                        cmd.Parameters.AddWithValue("@courseId", courseId);
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking enrollment: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void OpenCourseContent(string courseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT materials FROM Course WHERE [course id] = @courseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@courseId", courseId);
                        conn.Open();
                        var materials = cmd.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(materials))
                        {
                           
                            MessageBox.Show($"Opening course materials: {materials}", "Course Content",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Course materials not found.", "Information",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing course content: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddOpenButtonColumn()
        {
            DataGridViewButtonColumn openBtn = new DataGridViewButtonColumn
            {
                Name = "Open",
                Text = "Open",
                HeaderText = "Action",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(openBtn);
        }

        private void InitializePaymentControls()
        {
            //hide all payment controls initially
            TogglePaymentControlsVisibility(false);
            HideAllPaymentFields();

            //set up checkbox behavior
            bkash.CheckedChanged += (s, e) =>
            {
                if (bkash.Checked) card.Checked = false;
                UpdatePaymentFieldsVisibility();
            };

            card.CheckedChanged += (s, e) =>
            {
                if (card.Checked) bkash.Checked = false;
                UpdatePaymentFieldsVisibility();
            };

            // Configure pay button
            paynowbutton.Click += Paynowbutton_Click;
        }

        private void LoadCourses()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT [course id], [course name], [Description], [Price], [materials] FROM Course";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TogglePaymentControlsVisibility(bool visible)
        {
            bkash.Visible = visible;
            card.Visible = visible;
            paynowbutton.Visible = visible;
        }

        private void HideAllPaymentFields()
        {
            expirationdatetime.Visible = cvvtextbox.Visible = entercardinfotextbox.Visible = false;
            expirationdatelabel.Visible = cvvlabel.Visible = entercardnumberlabel.Visible = false;
            bkashnuminputtextbox.Visible = bkashnumberinputlabel.Visible = false;
            bkashpininputtextbox.Visible = label2.Visible = false;
        }

        private void UpdatePaymentFieldsVisibility()
        {
            HideAllPaymentFields();
            if (bkash.Checked) ShowBkashFields();
            if (card.Checked) ShowCardFields();
        }

        private void ShowBkashFields()
        {
            bkashnuminputtextbox.Visible = true;
            bkashnumberinputlabel.Visible = true;
            bkashpininputtextbox.Visible = true;
            label2.Visible = true;
        }

        private void ShowCardFields()
        {
            entercardinfotextbox.Visible = true;
            entercardnumberlabel.Visible = true;
            expirationdatetime.Visible = true;
            expirationdatelabel.Visible = true;
            cvvtextbox.Visible = true;
            cvvlabel.Visible = true;
        }

        private void button_Enroll_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to enroll.", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            selectedCourseId = selectedRow.Cells["course id"].Value?.ToString();

            if (string.IsNullOrEmpty(selectedCourseId))
            {
                MessageBox.Show("Invalid course selection.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Price FROM Course WHERE [course id] = @courseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@courseId", selectedCourseId);
                        conn.Open();
                        coursePrice = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }

                TogglePaymentControlsVisibility(true);
                button_Enroll.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving course price: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        

        private void Paynowbutton_Click(object sender, EventArgs e)
        {
            if (!bkash.Checked && !card.Checked)
            {
                MessageBox.Show("Please select a payment method.", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool paymentValid = false;
            string paymentMethod = bkash.Checked ? "Bkash" : "Card";

            if (bkash.Checked) paymentValid = ValidateBkashPayment();
            if (card.Checked) paymentValid = ValidateCardPayment();

            if (paymentValid && ProcessPayment(paymentMethod))
            {
                ProcessEnrollment();
            }
        
        }

        private bool ValidateBkashPayment()
        {
            string bkashNumber = bkashnuminputtextbox.Text.Trim();
            string pin = bkashpininputtextbox.Text.Trim();

            if (!Regex.IsMatch(bkashNumber, @"^\d{11}$"))
            {
                MessageBox.Show("Bkash number must be 11 digits!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(pin, @"^\d{5}$"))
            {
                MessageBox.Show("PIN must be 5 digits!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateCardPayment()
        {
            string cardNumber = entercardinfotextbox.Text.Trim();
            string cvc = cvvtextbox.Text.Trim();

            if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
            {
                MessageBox.Show("Card number must be 16 digits!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(cvc, @"^\d{3}$"))
            {
                MessageBox.Show("CVC must be 3 digits!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (expirationdatetime.Value < DateTime.Today)
            {
                MessageBox.Show("Card has expired!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool ProcessPayment(string paymentMethod)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Payments 
                           (username, course_id, payment_method, transaction_amount, 
                            bkash_number, card_number, cvc)
                           VALUES (@username, @courseId, @paymentMethod, @amount, 
                                   @bkash, @card, @cvc)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", uName);
                        cmd.Parameters.AddWithValue("@courseId", selectedCourseId);
                        cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@amount", coursePrice);

                        if (paymentMethod == "Bkash")
                        {
                            cmd.Parameters.AddWithValue("@bkash", bkashnuminputtextbox.Text.Trim());
                            cmd.Parameters.AddWithValue("@card", DBNull.Value);
                            cmd.Parameters.AddWithValue("@cvc", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@bkash", DBNull.Value);
                            cmd.Parameters.AddWithValue("@card", entercardinfotextbox.Text.Trim());
                            cmd.Parameters.AddWithValue("@cvc", cvvtextbox.Text.Trim());
                        }

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Payment processing failed: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ProcessEnrollment()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if already enrolled
                    string checkEnrollmentQuery = @"SELECT COUNT(*) FROM Enrolled WHERE username = @user AND [course id] = @courseId";
                    using (SqlCommand checkCmd = new SqlCommand(checkEnrollmentQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@user", uName);
                        checkCmd.Parameters.AddWithValue("@courseId", selectedCourseId);

                        int enrollmentCount = (int)checkCmd.ExecuteScalar();
                        if (enrollmentCount > 0)
                        {
                            MessageBox.Show("You are already enrolled in this course.", "Information",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetUI();
                            return; // Exit if already enrolled
                        }
                    }

                    // Proceed with enrollment
                    string insertQuery = @"INSERT INTO Enrolled (username, [course id]) 
                                   VALUES (@user, @courseId)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@user", uName);
                        insertCmd.Parameters.AddWithValue("@courseId", selectedCourseId);
                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Enrollment successful!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetUI();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing enrollment: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ResetUI()
        {
            TogglePaymentControlsVisibility(false);
            HideAllPaymentFields();
            bkash.Checked = card.Checked = false;
            button_Enroll.Enabled = true;
            dataGridView1.ClearSelection();
            bkashnuminputtextbox.Clear();
            bkashpininputtextbox.Clear();
            entercardinfotextbox.Clear();
            cvvtextbox.Clear();
        }

        

        private void button_Back_Click(object sender, EventArgs e)
        {
            new userdashboard(uName).Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void PaidCourseDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button_open_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course first.", "Selection Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string courseId = selectedRow.Cells["course id"].Value?.ToString();

            if (string.IsNullOrEmpty(courseId))
            {
                MessageBox.Show("Invalid course selection.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsUserEnrolled(courseId))
            {
                MessageBox.Show("Please enroll in the course before accessing it.", "Enrollment Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get materials path from database
            string materialsPath = GetCourseMaterialsPath(courseId);

            if (!string.IsNullOrEmpty(materialsPath))
            {
                try
                {
                    if (File.Exists(materialsPath))
                    {
                        // Open with default associated program
                        System.Diagnostics.Process.Start(materialsPath);
                    }
                    else
                    {
                        MessageBox.Show("Course file not found at:\n" + materialsPath, "File Missing",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No materials available for this course.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string GetCourseMaterialsPath(string courseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT materials FROM Course WHERE [course id] = @courseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@courseId", courseId);
                        conn.Open();
                        return cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving materials: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
            
        
    }
}