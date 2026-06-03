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
    public partial class AddMaterials : BaseForm
    {
        string uName;
        public AddMaterials(string uName)
        {
            this.uName = uName;
            InitializeComponent();

            panel1.Visible = true;  //panel hide
            LoadSlides();  //load stored data in gridview
            this.uName = uName;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            AddBooks form_Book = new AddBooks(uName);
            this.Hide();
            form_Book.ShowDialog();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            form_distributor form7 = new form_distributor(uName);
            this.Hide();
            form7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
           // dlg.Filter = "PDF Files|.pdf|PowerPoint Files|.pptx|All Files|.";
            dlg.Filter = "PDF Files (*.pdf)|*.pdf|PowerPoint Files (*.pptx)|*.pptx|All Files (*.*)|*.*";


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filepath = dlg.FileName;
                SaveSlide(filepath);
                MessageBox.Show("Slide added successfully!");
            }

            }

        private void SaveSlide(string filepath)
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(filepath);
                string fileName = Path.GetFileName(filepath);
                string extension = Path.GetExtension(filepath);

                string query = "INSERT INTO Slides (FileName, Extension, Data) VALUES (@name, @extn, @data)";

                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = fileName;
                    cmd.Parameters.Add("@extn", SqlDbType.NVarChar).Value = extension;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = fileData;

                    cn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        // Refresh and close panel only after successful save
                        LoadSlides();
                        panel1.Visible = true;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private SqlConnection GetConnection() => DbHelper.GetConnection();

        private void heartfulsharingdash_Load(object sender, EventArgs e)
        {
            LoadSlides();
        }

        private void LoadSlides()
        {
            using (SqlConnection cn = GetConnection())
            {
                string query = "SELECT id, FileName, Extension FROM Slides";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private void button_AddSlides_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;  //ahow the panel
            LoadSlides();  //load stored data in gridview

        }

        private void button_ClosePanel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            LoadSlides();
        }

        private void panelSlides(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                int slideId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                OpenSlide(slideId);
            }
        }


        private void button_OpenSlide_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                int slideId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                OpenSlide(slideId);
            }
            else
            {
                MessageBox.Show("Please select a slide first.");
            }
        }

        private void OpenSlide(int id)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT Data, FileName, Extension FROM Slides WHERE id = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] data = (byte[])reader["Data"];
                            string name = reader["FileName"].ToString();
                            string extn = reader["Extension"].ToString();

                            string filePath = Path.Combine(Path.GetTempPath(), name);
                            File.WriteAllBytes(filePath, data);
                            System.Diagnostics.Process.Start(filePath);
                        }
                        else
                        {
                            MessageBox.Show("Slide not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OpenFile(int id)
        {
            try
            {
                using (SqlConnection cn = GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT data, FileName, extension FROM Slides WHERE id = @id", cn))
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

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            LoadSlides();
            MessageBox.Show("Slides list refreshed!");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
