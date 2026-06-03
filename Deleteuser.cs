using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;
using System.Data.SqlClient;

namespace NeedyNest
{
    public partial class Deleteuser : BaseForm
    {
        string uName;
        private SqlConnection con = DbHelper.GetConnection();
        private ToolTip toolTip = new ToolTip();


        public Deleteuser(string uName)
        {
            InitializeComponent();
            textBox_Search.TextChanged += SearchTextBox_TextChanged;
            LoadAllUsers(); //cann load all user initially
            this.uName = uName;
            this.Load += (s, e) => PageChrome.Apply(this, "Delete User");
        }

        private void LoadAllUsers()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT first_name, last_name, role, username, contact_number, password, status, uni_name FROM signup";
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



        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Search.Text.Length >= 1)
            {
                FetchUserData();
            }
        }

        private void FetchUserData()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string query = "SELECT first_name, last_name, role, username, contact_number, password, status, uni_name FROM signup WHERE username LIKE @Username";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@Username", textBox_Search.Text.Trim() + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (con.State == ConnectionState.Open)
                    con.Close();

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                   toolTip1.SetToolTip(textBox_Search, ""); //clear tooltip if match will found
                }
                else
                {
                    dataGridView1.DataSource = null;
                    toolTip1.Show("No matched item", textBox_Search, 0, textBox_Search.Height, 2000); //will show ttoltip under the textbox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }



        private void button_back_Click(object sender, EventArgs e)
        {

            new manageuserdashboardform(uName).Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_search_Click(object sender, EventArgs e)
        {
           
        }
       


        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string username = dataGridView1.SelectedRows[0].Cells["username"].Value.ToString();

                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        if (con.State == ConnectionState.Closed)
                            con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM signup WHERE username = @Username", con);
                        cmd.Parameters.AddWithValue("@Username", username);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (con.State == ConnectionState.Open)
                            con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllUsers(); // Refresh the DataGridView with all users
                        }
                        else
                        {
                            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Deleteuser_Load(object sender, EventArgs e)
        {

        }
    }
}
