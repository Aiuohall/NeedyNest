using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class payment_history : BaseForm
    {
        private string uName;
        private string connectionString = DbHelper.ConnectionString;
        private DataTable _data = new DataTable();
        private GridPager _pager;

        public payment_history(string uName)
        {
            InitializeComponent();
            this.uName = uName;
            this.Load += payment_history_Load;
        }

        private void payment_history_Load(object sender, EventArgs e)
        {
            LoadPaymentData();
            BuildLayout();
        }

        private void BuildLayout()
        {
            SuspendLayout();
            BackColor   = ThemeManager.BackgroundColor;
            Padding     = new Padding(0);
            ClientSize  = new Size(1180, 720);
            MinimumSize = new Size(960, 600);

            for (int i = Controls.Count - 1; i >= 0; i--)
                if (Controls[i] is StatusStrip) Controls.RemoveAt(i);

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = ThemeManager.PrimaryColor };
            header.Paint += (s, ev) =>
            {
                using (var b = new LinearGradientBrush(header.ClientRectangle, ThemeManager.PrimaryColor, ThemeManager.HoverColor, LinearGradientMode.Horizontal))
                    ev.Graphics.FillRectangle(b, header.ClientRectangle);
                using (var a = new SolidBrush(ThemeManager.AccentColor))
                    ev.Graphics.FillRectangle(a, 0, header.Height - 3, header.Width, 3);
            };
            header.Controls.Add(new Label
            {
                Text = "Payment History", ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), AutoSize = true, Location = new Point(24, 15)
            });

            // Toolbar (hosts the pager's search + paging controls)
            var toolbar = new Panel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(24, 0, 24, 0), BackColor = ThemeManager.BackgroundColor };

            // Footer (export + back)
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = ThemeManager.BackgroundColor };
            var btnExport = new Button { Text = "Export to CSV", Size = new Size(150, 40), Location = new Point(24, 10) };
            ThemeManager.StyleButton(btnExport);
            btnExport.Click += (s, ev) => CsvExporter.Export(dataGridView1, "payment_history");
            footer.Controls.Add(btnExport);

            Controls.Remove(button_back);
            ThemeManager.StyleButton(button_back);
            button_back.Size = new Size(140, 40);
            button_back.Anchor = AnchorStyles.Right;
            footer.Controls.Add(button_back);
            footer.Resize += (s, ev) => button_back.Location = new Point(footer.Width - button_back.Width - 24, 10);

            // Grid body
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24, 8, 24, 8), BackColor = ThemeManager.BackgroundColor };
            Controls.Remove(dataGridView1);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataBindingComplete += (s, ev) => FormatGridView();
            body.Controls.Add(dataGridView1);

            Controls.Add(body);
            Controls.Add(footer);
            Controls.Add(toolbar);
            Controls.Add(header);

            // Search + pagination over the loaded data
            _pager = new GridPager(dataGridView1, _data, 15, toolbar,
                "username", "payment_method", "card_number", "bkash_number");

            button_back.Location = new Point(footer.Width - button_back.Width - 24, 10);
            ResumeLayout(true);
        }

        private void LoadPaymentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT payment_id, username, course_id, payment_method, transaction_amount, bkash_number, card_number, cvc, payment_date FROM Payments";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    _data = new DataTable();
                    da.Fill(_data); // the GridPager pages/filters over this
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "payment_history.LoadPaymentData");
                MessageBox.Show("Error loading payment data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridView()
        {
            if (dataGridView1.Columns.Count == 0) return;
            void H(string c, string text) { if (dataGridView1.Columns.Contains(c)) dataGridView1.Columns[c].HeaderText = text; }
            H("payment_id", "Payment ID");
            H("username", "Username");
            H("course_id", "Course ID");
            H("payment_method", "Payment Method");
            H("transaction_amount", "Amount");
            H("bkash_number", "Bkash Number");
            H("card_number", "Card Number");
            H("cvc", "CVC");
            H("payment_date", "Payment Date");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
