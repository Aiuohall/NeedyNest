using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class AddBooks : BaseForm

    {
        string uName;
        public AddBooks(string uName)
        {
            this.uName = uName;
            InitializeComponent();


        }

        private void Form_Book_Load(object sender, EventArgs e)
        {
            LoadData();  // Load books when the form loads
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_OpenSlide_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                OpenFile(id);
            }
            else
            {
                MessageBox.Show("Please select a file to open.");
            }
        }


        private void OpenFile(int id)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT data, FileName, extension FROM Books WHERE id = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] data = (byte[])reader["data"];
                            string name = reader["FileName"].ToString();
                            string extn = reader["extension"].ToString();

                            string newFileName = $"{Path.GetFileNameWithoutExtension(name)}_{DateTime.Now:yyyyMMddHHmmss}{extn}";
                            File.WriteAllBytes(newFileName, data);
                            System.Diagnostics.Process.Start(newFileName);
                        }
                        else
                        {
                            MessageBox.Show("File not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && File.Exists(textBox1.Text))
            {
                saveFile(textBox1.Text);
                MessageBox.Show("File saved successfully!");
            }
            else
            {
                MessageBox.Show("Please select a valid file.");
            }
        }
        private void saveFile(string filepath)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(filepath);  // Read entire file as byte array
                string extn = Path.GetExtension(filepath); // Get file extension
                string name = Path.GetFileName(filepath);

                string query = "INSERT INTO Books(data,FileName,extension) VALUES (@data,@name,@extn)";

                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@extn", SqlDbType.Char, 10).Value = extn;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }




        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=LAPTOP-MSIETGV1\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;");



        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            textBox1.Text = dlg.FileName;
        }

        private void Pdf_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (SqlConnection cn = GetConnection())

            {
                string query = "select id,FileName,extension from Books ";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;

                }

            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button_ClosePanel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new moderatordash(uName).Show();
            moderatordash moderatordash = new moderatordash(uName);
            
            this.Hide();
            moderatordash.Show();



        }

        

       

    }
}

