using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using NeedyNest.Data;
using NeedyNest.UI;

namespace NeedyNest
{
    /// <summary>
    /// Dedicated approval screen. Lists ONLY pending members (status = 0) so an
    /// already-approved user can never be approved again. Approving/rejecting
    /// happens here and the list refreshes immediately.
    /// </summary>
    public partial class ApproveMembers : BaseForm
    {
        private readonly string uName;

        private DataGridView grid;
        private Button btnApprove;
        private Button btnReject;
        private Button btnRefresh;
        private Button btnBack;
        private Label lblEmpty;

        public ApproveMembers(string uName)
        {
            this.uName = uName;
            BuildUi();
            this.Load += (s, e) => LoadPending();
        }

        /// <summary>Count of members still waiting for approval (status = 0).</summary>
        public static int GetPendingCount()
        {
            try { return UserRepository.CountPending(); }
            catch { return 0; } // DB offline or unreachable — show no badge
        }

        private void BuildUi()
        {
            this.Text          = "Approve Members";
            this.ClientSize    = new Size(1120, 720);
            this.MinimumSize   = new Size(960, 600);
            this.Padding       = new Padding(0);

            // Header
            var header = new Panel { Dock = DockStyle.Top, Height = 76, BackColor = ThemeManager.PrimaryColor };
            var title = new Label
            {
                Text      = "Approve Members",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(28, 22)
            };
            header.Controls.Add(title);

            // Footer with actions
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = ThemeManager.BackgroundColor };

            btnApprove = MakeButton("Approve", ThemeManager.SuccessColor);
            btnReject  = MakeButton("Reject",  ThemeManager.DangerColor);
            btnRefresh = MakeButton("Refresh", ThemeManager.SecondaryColor);
            btnBack    = MakeButton("Back",    ThemeManager.SecondaryColor);

            btnApprove.Click += (s, e) => UpdateStatus(1, "approved");
            btnReject.Click  += (s, e) => UpdateStatus(2, "rejected");
            btnRefresh.Click += (s, e) => LoadPending();
            btnBack.Click    += (s, e) => { new manageuserdashboardform(uName).Show(); this.Hide(); };

            footer.Controls.AddRange(new Control[] { btnApprove, btnReject, btnRefresh, btnBack });
            footer.Resize += (s, e) => LayoutFooter(footer);

            // Grid
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20), BackColor = ThemeManager.BackgroundColor };
            grid = new DataGridView
            {
                Dock                = DockStyle.Fill,
                AllowUserToAddRows  = false,
                ReadOnly            = true,
                SelectionMode       = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            lblEmpty = new Label
            {
                Text      = "No members are waiting for approval.",
                ForeColor = ThemeManager.SubtleText,
                Font      = new Font("Segoe UI", 12F, FontStyle.Regular),
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock      = DockStyle.Fill,
                Visible   = false
            };
            body.Controls.Add(grid);
            body.Controls.Add(lblEmpty);

            this.Controls.Add(body);
            this.Controls.Add(header);
            this.Controls.Add(footer);

            LayoutFooter(footer);
        }

        private Button MakeButton(string text, Color color)
        {
            return new Button
            {
                Text      = text,
                Size      = new Size(140, 42),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
        }

        private void LayoutFooter(Panel footer)
        {
            // Approve / Reject on the left, Refresh / Back on the right
            btnApprove.Location = new Point(20, 14);
            btnReject.Location  = new Point(170, 14);
            btnBack.Location    = new Point(footer.Width - 160, 14);
            btnRefresh.Location = new Point(footer.Width - 310, 14);
        }

        private void LoadPending()
        {
            try
            {
                DataTable dt = UserRepository.GetPending(); // data-access layer
                grid.DataSource = dt;

                if (grid.Columns["username"] != null)
                    grid.Columns["username"].Visible = false; // keep for lookup, hide from view

                bool any = dt.Rows.Count > 0;
                grid.Visible       = any;
                lblEmpty.Visible   = !any;
                btnApprove.Enabled = any;
                btnReject.Enabled  = any;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending members: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Best-effort notify: looks up the member's email and sends a note.
        /// Silently skips if the email column doesn't exist, is empty, or SMTP is off.</summary>
        private void NotifyMember(string username, string actionWord)
        {
            string email = null;
            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                using (var cmd = new SqlCommand("SELECT email FROM signup WHERE username = @u", con))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    con.Open();
                    object r = cmd.ExecuteScalar();
                    email = (r == null || r == DBNull.Value) ? null : r.ToString();
                }
            }
            catch { return; } // email column may not exist yet — that's fine

            EmailHelper.Send(email,
                $"NeedyNest — your account has been {actionWord}",
                $"Hello {username},\n\nYour NeedyNest account has been {actionWord}." +
                (actionWord == "approved" ? "\nYou can now log in and start using the system." : "") +
                "\n\n— The NeedyNest Team");
        }

        private void UpdateStatus(int status, string actionWord)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = grid.SelectedRows[0].Cells["username"].Value?.ToString();
            if (string.IsNullOrEmpty(username)) return;

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to mark '{username}' as {actionWord}?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                UserRepository.SetStatus(username, status); // data-access layer

                // Best-effort email notification (no-op unless SMTP + email are set up).
                NotifyMember(username, actionWord);

                MessageBox.Show($"Member '{username}' has been {actionWord}.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPending(); // pending list shrinks immediately
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating member: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
