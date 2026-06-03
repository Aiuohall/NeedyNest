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
    public partial class PromoteModerator : BaseForm

    {
        string uName;

        private SqlConnection con = DbHelper.GetConnection();

        public PromoteModerator(string uName)
        {
            InitializeComponent();
            LoadModerators(); //function delclare to load moderator initially
            this.uName = uName;
            this.Load += (s, e) => PageChrome.Apply(this, "Promote Moderator");
        }

        private void LoadModerators()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT first_name, last_name, username, contact_number, uni_name FROM signup WHERE role = 'Moderator'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (con.State == ConnectionState.Open)
                    con.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }




        private void PromoteModerator_Load(object sender, EventArgs e)
        {

        }

        private void button_promote_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string username = dataGridView1.SelectedRows[0].Cells["username"].Value.ToString();

                DialogResult confirm = MessageBox.Show($"Are you sure you want to promote {username} to Admin?",
                    "Confirm Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        SqlCommand cmd = new SqlCommand("UPDATE signup SET role = 'Admin' WHERE username = @Username", con);
                        cmd.Parameters.AddWithValue("@Username", username);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (con.State == ConnectionState.Open)
                            con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Promote To Admin Succesful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadModerators(); //for refresh the list
                        }
                        else
                        {
                            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error promoting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to promote.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            new manageuserdashboardform(uName).Show();
            this.Hide();
        }
    }
}
