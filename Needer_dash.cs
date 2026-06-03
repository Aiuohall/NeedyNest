using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeedyNest.UI;

namespace NeedyNest
{
    public partial class Needer_DashBoard : BaseForm
    {
        string uName;
        public Needer_DashBoard(string uName)
        {
            InitializeComponent();
            this.uName = uName;
        }

        private void button_Books_Click(object sender, EventArgs e)
        {
            Form_ReceiveBooks neederBooks = new Form_ReceiveBooks(uName);
            this.Hide();
            neederBooks.ShowDialog();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            userdashboard userdashboard = new userdashboard(uName);
            this.Hide();
            userdashboard.Show();
        }

        private void button_Slides_Click(object sender, EventArgs e)
        {
            new Distribution(uName).Show();
            this.Hide();
        }

        private void Needer_DashBoard_Load(object sender, EventArgs e)
        {
            DashboardLayout.Apply(this, "Needer Dashboard", uName,
                new[] { button_Books }, button_back, null);
        }
    }
}
