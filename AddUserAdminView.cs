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
    public partial class manageuserdashboardform : BaseForm
    {
        string uName;
        public manageuserdashboardform(string uName)
        {
            this.uName = uName;
            InitializeComponent();
           
        }

        private void manageuserdashboardform_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            admindashboardform form3 = new admindashboardform(uName);
            this.Hide();
            form3.Show();
        }

        private void alluser_Click(object sender, EventArgs e)
        {
            allUserAdminView a1 = new allUserAdminView(uName);
            a1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AddUser(uName).Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Deleteuser(uName).Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new PromoteModerator(uName).Show();
            this.Hide();    




        }
    }
}
