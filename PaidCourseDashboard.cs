using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private Panel pnlBkash;   // holds the Bkash fields (shown only when Bkash is picked)
        private Panel pnlCard;    // holds the Card fields  (shown only when Card is picked)
       
     

       private string connectionString = DbHelper.ConnectionString;

        public PaidCourseDashboard(string uName)
        {
            InitializeComponent();
            this.uName = uName;          // set BEFORE loading data (was a bug: used uninitialized)
            LoadCourses();
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
            OpenCourseMaterial(courseId);
        }

        /// <summary>
        /// Opens a course's material: prefers the file stored in the database
        /// (works on any machine), falling back to the legacy file path.
        /// </summary>
        private void OpenCourseMaterial(string courseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT materialData, materialName, materialExt, materials FROM Course WHERE [course id] = @courseId", conn))
                {
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    conn.Open();
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            MessageBox.Show("Course not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        bool hasBlob = r["materialData"] != DBNull.Value;
                        if (hasBlob)
                        {
                            byte[] data = (byte[])r["materialData"];
                            string name = r["materialName"] != DBNull.Value ? r["materialName"].ToString() : "course_material";
                            string ext  = r["materialExt"]  != DBNull.Value ? r["materialExt"].ToString()  : "";
                            string path = Path.Combine(Path.GetTempPath(),
                                $"{Path.GetFileNameWithoutExtension(name)}_{DateTime.Now:yyyyMMddHHmmss}{ext}");
                            File.WriteAllBytes(path, data);
                            System.Diagnostics.Process.Start(path);
                        }
                        else
                        {
                            // Legacy course saved as a local path.
                            string legacy = r["materials"]?.ToString();
                            if (!string.IsNullOrEmpty(legacy) && File.Exists(legacy))
                                System.Diagnostics.Process.Start(legacy);
                            else
                                MessageBox.Show("No materials available for this course.", "Information",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "OpenCourseMaterial");
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
            // Null-guarded: this is called from the constructor before the panels
            // are built in BuildPaymentLayout().
            if (pnlBkash != null) pnlBkash.Visible = false;
            if (pnlCard != null) pnlCard.Visible = false;
        }

        private void UpdatePaymentFieldsVisibility()
        {
            HideAllPaymentFields();
            if (bkash.Checked) ShowBkashFields();
            if (card.Checked) ShowCardFields();
        }

        private void ShowBkashFields()
        {
            if (pnlBkash != null) pnlBkash.Visible = true;
        }

        private void ShowCardFields()
        {
            if (pnlCard != null) pnlCard.Visible = true;
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
                            // PCI: never store the full card number or the CVC.
                            cmd.Parameters.AddWithValue("@bkash", DBNull.Value);
                            cmd.Parameters.AddWithValue("@card", MaskCard(entercardinfotextbox.Text.Trim()));
                            cmd.Parameters.AddWithValue("@cvc", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "ProcessPayment");
                MessageBox.Show("Payment processing failed: " + ex.Message, "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>Returns a masked card number that only keeps the last 4 digits.</summary>
        private static string MaskCard(string card)
        {
            string digits = new string((card ?? "").Where(char.IsDigit).ToArray());
            return digits.Length >= 4 ? "**** **** **** " + digits.Substring(digits.Length - 4) : "****";
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
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // PK_Enrolled is defined on [course id] alone, so a course can only
                // ever hold ONE enrollment row. Explain it instead of a raw SQL error.
                MessageBox.Show(
                    "This course is already enrolled and cannot accept another enrollment.\n\n" +
                    "Admin note: the 'Enrolled' table's primary key is on the course id only, " +
                    "which limits each course to a single enrollment. Run the script " +
                    "DB\\FixEnrolledPrimaryKey.sql once to allow multiple members per course.",
                    "Already Enrolled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetUI();
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
            BuildPaymentLayout();
        }

        /// <summary>
        /// Reorganizes the previously scattered payment controls (which sat off to
        /// the far right at coordinates like X≈1075 and were effectively invisible)
        /// into a clean, contained "Payment Details" card with a prominent Pay Now
        /// button.
        /// </summary>
        private void BuildPaymentLayout()
        {
            SuspendLayout();

            this.Text        = "Paid Courses";
            this.ClientSize  = new Size(1200, 780);
            this.MinimumSize = new Size(1000, 700);
            this.BackColor   = ThemeManager.BackgroundColor;
            this.Padding     = new Padding(0);

            // Remove the base status strip so it can't overlap the payment card.
            for (int i = Controls.Count - 1; i >= 0; i--)
                if (Controls[i] is StatusStrip) Controls.RemoveAt(i);

            // ── Header ───────────────────────────────────────────────────────────
            var header = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = ThemeManager.PrimaryColor };
            header.Controls.Add(new Label
            {
                Text = "Paid Courses",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 16)
            });
            Controls.Add(header);

            // ── Top toolbar (Back / Open) ────────────────────────────────────────
            button_Back.SetBounds(24, 78, 120, 38);
            button_open.SetBounds(156, 78, 120, 38);
            button_Enroll.SetBounds(288, 78, 120, 38);
            button_Back.Anchor   = AnchorStyles.Top | AnchorStyles.Left;
            button_open.Anchor   = AnchorStyles.Top | AnchorStyles.Left;
            button_Enroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // ── Course grid ──────────────────────────────────────────────────────
            dataGridView1.SetBounds(24, 128, ClientSize.Width - 48, 300);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ── Payment card ─────────────────────────────────────────────────────
            var pay = new Panel
            {
                Name      = "pnlPaymentCard",
                BackColor = ThemeManager.SurfaceColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            pay.SetBounds(24, 444, ClientSize.Width - 48, 200);
            pay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(pay);

            var payTitle = new Label
            {
                Text = "Payment Details",
                ForeColor = ThemeManager.PrimaryColor,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(18, 12)
            };
            var payHint = new Label
            {
                Text = "Choose a payment method to continue",
                ForeColor = ThemeManager.SubtleText,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(20, 38)
            };
            pay.Controls.Add(payTitle);
            pay.Controls.Add(payHint);

            // helper: move an existing control into a new parent at a local position
            void MoveTo(Control c, Control parent, int x, int y)
            {
                if (c == null) return;
                Controls.Remove(c);
                c.Location = new Point(x, y);
                // Only labels/checkboxes support a transparent background; TextBox and
                // DateTimePicker throw ArgumentException if you try.
                if (c is Label || c is CheckBox) c.BackColor = Color.Transparent;
                parent.Controls.Add(c);
            }

            // Method selection (styled like pill toggles)
            Controls.Remove(bkash);
            Controls.Remove(card);
            bkash.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            card.Font  = new Font("Segoe UI", 10F, FontStyle.Bold);
            bkash.Location = new Point(22, 64);
            card.Location  = new Point(140, 64);
            pay.Controls.Add(bkash);
            pay.Controls.Add(card);

            // ── Bkash sub-panel ──────────────────────────────────────────────────
            pnlBkash = new Panel { BackColor = Color.FromArgb(247, 250, 252) };
            pnlBkash.SetBounds(18, 100, 560, 86);
            pnlBkash.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pay.Controls.Add(pnlBkash);
            MoveTo(bkashnumberinputlabel, pnlBkash, 14, 10);
            MoveTo(bkashnuminputtextbox, pnlBkash, 14, 36); bkashnuminputtextbox.Width = 220;
            MoveTo(label2, pnlBkash, 290, 10);
            MoveTo(bkashpininputtextbox, pnlBkash, 290, 36); bkashpininputtextbox.Width = 170;
            pnlBkash.Visible = false;

            // ── Card sub-panel (occupies the same spot; only one ever visible) ────
            pnlCard = new Panel { BackColor = Color.FromArgb(247, 250, 252) };
            pnlCard.SetBounds(18, 100, 720, 86);
            pnlCard.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pay.Controls.Add(pnlCard);
            MoveTo(entercardnumberlabel, pnlCard, 14, 10);
            MoveTo(entercardinfotextbox, pnlCard, 14, 36); entercardinfotextbox.Width = 250;
            MoveTo(expirationdatelabel, pnlCard, 300, 10);
            MoveTo(expirationdatetime, pnlCard, 300, 36); expirationdatetime.Width = 180;
            MoveTo(cvvlabel, pnlCard, 510, 10);
            MoveTo(cvvtextbox, pnlCard, 510, 36); cvvtextbox.Width = 120;
            pnlCard.Visible = false;

            // ── Prominent Pay Now button, pinned bottom-right of the card ─────────
            Controls.Remove(paynowbutton);
            paynowbutton.Size = new Size(180, 50);
            paynowbutton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            paynowbutton.Text = "Pay Now";
            paynowbutton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pay.Controls.Add(paynowbutton);
            void PlacePayNow() =>
                paynowbutton.Location = new Point(pay.Width - paynowbutton.Width - 24, pay.Height - paynowbutton.Height - 18);
            pay.Resize += (s, e) => PlacePayNow();
            PlacePayNow();

            ResumeLayout(true);
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

            // Open the material stored in the DB (falls back to a legacy path).
            OpenCourseMaterial(courseId);
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