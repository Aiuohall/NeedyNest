using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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
                   DbHelper.ConnectionString))
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

            // Only PENDING members (status 0) can be approved. Approved/rejected
            // members just show their status — no clickable approve link.
            bool isPending = status == "0";

            Label manageLabel = new Label
            {
                Text = isPending ? "Approve / Manage" : "—",
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(330, 5),
                AutoSize = false,
                Size = new Size(140, 29),
                ForeColor = isPending ? Color.SeaGreen : Color.Silver,
                Cursor = isPending ? Cursors.Hand : Cursors.Default
            };

            if (isPending)
            {
                manageLabel.Click += (sender, e) => {
                    UserApproval m1 = new UserApproval(userName);
                    this.Hide();
                    m1.StartPosition = FormStartPosition.CenterParent;
                    m1.ShowDialog();
                };
            }

            Label usernameLabel = new Label
            {
                Text = userName,
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(475, 5),
                AutoSize = false,
                Size = new Size(120, 25),
                ForeColor = Color.DarkBlue
            };

            string statusText;
            Color statusColor;
            switch (status)
            {
                case "1": statusText = "Approved"; statusColor = Color.SeaGreen; break;
                case "2": statusText = "Rejected"; statusColor = Color.Firebrick; break;
                default:  statusText = "Pending";  statusColor = Color.DarkOrange; break;
            }

            Label statusLabel = new Label
            {
                Text = statusText,
                Font = new Font("Sans Serif Collection", 11, FontStyle.Bold),
                Location = new Point(650, 5),
                AutoSize = false,
                Size = new Size(90, 25),
                ForeColor = statusColor
            };

            everyuser.Controls.Add(nameLabel);
            everyuser.Controls.Add(manageLabel);
            everyuser.Controls.Add(usernameLabel);
            everyuser.Controls.Add(statusLabel);

            userpanel.Controls.Add(everyuser);
        }

        private void allUserAdminView_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            BackColor    = ThemeManager.BackgroundColor;
            Padding      = new Padding(0);
            ClientSize   = new Size(1180, 740);
            MinimumSize  = new Size(960, 620);

            for (int i = Controls.Count - 1; i >= 0; i--)
                if (Controls[i] is StatusStrip) Controls.RemoveAt(i);

            // Gradient header
            var header = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = ThemeManager.PrimaryColor };
            header.Paint += (s, ev) =>
            {
                using (var b = new LinearGradientBrush(header.ClientRectangle,
                           ThemeManager.PrimaryColor, ThemeManager.HoverColor, LinearGradientMode.Horizontal))
                    ev.Graphics.FillRectangle(b, header.ClientRectangle);
                using (var a = new SolidBrush(ThemeManager.AccentColor))
                    ev.Graphics.FillRectangle(a, 0, header.Height - 3, header.Width, 3);
            };
            header.Controls.Add(new Label
            {
                Text = "All Users", ForeColor = Color.White, BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), AutoSize = true, Location = new Point(24, 15)
            });

            // Footer with a themed Back button
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 64, BackColor = ThemeManager.BackgroundColor };
            var back = new Button { Text = "Back", Size = new Size(140, 40) };
            ThemeManager.StyleButton(back);
            back.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            back.Click += (s, ev) => { new manageuserdashboardform(uName).Show(); this.Hide(); };
            footer.Controls.Add(back);
            footer.Resize += (s, ev) => back.Location = new Point(footer.Width - back.Width - 24, 12);

            // Make the white card + the user list fill the available space
            panel3.Dock    = DockStyle.Fill;
            panel3.Padding = new Padding(16);
            userpanel.Dock = DockStyle.Fill;

            Controls.Add(header);
            Controls.Add(footer);
            header.BringToFront();

            back.Location = new Point(footer.Width - back.Width - 24, 12);
            ResumeLayout(true);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}