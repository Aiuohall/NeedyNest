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
    public partial class form_distributor : BaseForm
    {
        string uName;
        public form_distributor(string uName)
        {
            InitializeComponent();
            this.uName = uName;
        }

        private void coursematerialbutton_Click(object sender, EventArgs e)
        {

        }

        private void heartfulsharingform_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            userdashboard form6 = new userdashboard(uName);
            this.Hide();
            form6.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMaterials form10 = new AddMaterials(uName); 
            this.Hide();
            form10.ShowDialog();
        }
    }
}
