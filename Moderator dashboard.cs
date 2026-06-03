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
    public partial class moderatordash : BaseForm
    {
        string uName;
        public moderatordash(string uName)
        {
            InitializeComponent();
            this.uName = uName;
        }

        private void allreviewlogsbutton_Click(object sender, EventArgs e)
        {
            /*  Distribution pdf = new Distribution(uName);
              this.Hide();
              pdf.Show();*/
            new CourseDetails(uName).Show();
            this.Hide();
        }

        private void button_managecatagory_Click(object sender, EventArgs e)
        {
            DistributionForModerator d1 = new DistributionForModerator(uName);
            d1.Show();
            this.Hide();
        }

        private void AddPaidcourse_Click(object sender, EventArgs e)
        {
            this.Close();
            Course course = new Course(uName);
            course.Show();
        }

        private void button_addbooks_Click(object sender, EventArgs e)
        {
            new AddBooks(uName).Show();
            this.Hide();
        }

        private void moderatordash_Load(object sender, EventArgs e)
        {
            DashboardLayout.Apply(
                this,
                "Moderator Dashboard",
                uName,
                new[] { button_managecatagory, button_addbooks, AddPaidcourse, Deletepaidcoursebutton },
                logoutmoderatorbutton,
                new Control[] { welcomemoderatorlabel });
        }

        private void logoutmoderatorbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Session.Clear();
                new Login().Show();
                this.Close();
            }
        }
    }
}
