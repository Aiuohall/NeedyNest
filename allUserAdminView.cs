using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class allUserAdminView : BaseForm
    {
        string uName;

        string userName;
        public allUserAdminView(string uName)
        {
            InitializeComponent();
            Loaduserboxes();
            this.uName = uName;
        }

        private void Loaduserboxes()
        {
            userpanel.Controls.Clear();

            string query = @"SELECT first_name, last_name, username, status 
                           FROM signup";

            using (SqlConnection connection = new SqlConnection(
                   @"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userName = reader["username"].ToString();
                            string fullName = $"{reader["first_name"]} {reader["last_name"]}";
                            string status = reader["status"].ToString();

                            CreateUserBox(userName, fullName, status);
                        }
                    }
                }
            }
        }

        private void CreateUserBox(string userName, string fullName, string status)
        {
            int panelWidth = userpanel.Width - 21;
            int panelHeight = 70;

            Panel everyuser = new Panel
            {
                Size = new Size(panelWidth, panelHeight),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 5)
            };

            Label nameLabel = new Label
            {
                Text = fullName,
                AutoSize = false,
                Size = new Size(280, 25),
                ForeColor = Color.Black,
                Location = new Point(3, 5)
            };

            Label manageLabel = new Label
            {
                Text = "Manage User",
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(340, 5),
                AutoSize = false,
                Size = new Size(100, 29),
                ForeColor = Color.Green
            };

            manageLabel.Click += (sender, e) => {
                UserApproval m1 = new UserApproval(userName);
                this.Hide();
                m1.StartPosition = FormStartPosition.CenterParent;
                m1.ShowDialog();
            };

            Label usernameLabel = new Label
            {
                Text = userName,
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(475, 5),
                AutoSize = false,
                Size = new Size(120, 25),
                ForeColor = Color.DarkBlue
            };

            Label statusLabel = new Label
            {
                Text = status,
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(650, 5),
                AutoSize = false,
                Size = new Size(70, 25),
                ForeColor = Color.Peru
            };

            everyuser.Controls.Add(nameLabel);
            everyuser.Controls.Add(manageLabel);
            everyuser.Controls.Add(usernameLabel);
            everyuser.Controls.Add(statusLabel);

            userpanel.Controls.Add(everyuser);
        }

        private void allUserAdminView_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}