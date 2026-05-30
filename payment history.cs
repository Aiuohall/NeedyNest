using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class payment_history : BaseForm
    {
        private string uName;
        private string connectionString = @"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;";

        public payment_history(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            this.Load += payment_history_Load;
        }

        private void payment_history_Load(object sender, EventArgs e)
        {
            LoadPaymentData();
            FormatGridView();
        }

        private void LoadPaymentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TOP 1000 payment_id, username, course_id, payment_method, transaction_amount, bkash_number, card_number, cvc, payment_date FROM Payments";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payment data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["payment_id"].HeaderText = "Payment ID";
                dataGridView1.Columns["username"].HeaderText = "Username";
                dataGridView1.Columns["course_id"].HeaderText = "Course ID";
                dataGridView1.Columns["payment_method"].HeaderText = "Payment Method";
                dataGridView1.Columns["transaction_amount"].HeaderText = "Amount";
                dataGridView1.Columns["bkash_number"].HeaderText = "Bkash Number";
                dataGridView1.Columns["card_number"].HeaderText = "Card Number";
                dataGridView1.Columns["cvc"].HeaderText = "CVC";
                dataGridView1.Columns["payment_date"].HeaderText = "Payment Date";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Add logic for cell content clicks
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            new admindashboardform(uName).Show();
            this.Hide();
        }

        private void payment_history_Load_1(object sender, EventArgs e)
        {

        }
    }
}
