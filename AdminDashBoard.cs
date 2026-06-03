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
    public partial class admindashboardform : BaseForm
    {
        string uName;
        public admindashboardform(string uName)
        {
            this.uName = uName;
            InitializeComponent();
          
        }

        private void admindashboardform_Load(object sender, EventArgs e)
        {
            DashboardLayout.Apply(
                this,
                "Admin Dashboard",
                uName,
                new[] { manageruserbutton, managercatagoriesbutton, Addpaidcoursebutton,
                        button_delete, viewpaymentbutton, viewheartfulsharingbutton },
                button_logout,
                new Control[] { welcomeadminform, logoutbutton },
                badgeButton: null,
                badgeCount: 0,
                topContent: AdminAnalytics.Build(),
                topContentHeight: AdminAnalytics.PanelHeight);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Distribution pdf = new Distribution(uName);
            this.Hide();
            pdf.Show();


        }


        private void manageruserbutton_Click(object sender, EventArgs e)
        {
            manageuserdashboardform form4 = new manageuserdashboardform(uName);
            this.Hide();
            form4.Show();
        }

        private void managercatagoriesbutton_Click(object sender, EventArgs e)
        {
            DistributionForModerator form5 = new DistributionForModerator(uName);
            this.Hide();
            form5.Show();
        }

        private void logoutbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Want to Log Out?", "Log out Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Session.Clear();
                new Login().Show();
                this.Hide();
            }
        }

        private void button_logout_Click(object sender, EventArgs e)
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

        private void Addpaidcoursebutton_Click(object sender, EventArgs e)
        {
            this.Close();
            Course course = new Course(uName);
            course.Show();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            new CourseDetails(uName).Show();
            this.Hide();
        }

        private void viewpaymentbutton_Click(object sender, EventArgs e)
        {
            new payment_history(uName).Show();
            this.Hide();
        }
    }
}

