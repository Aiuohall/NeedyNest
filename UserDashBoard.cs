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
    public partial class userdashboard : BaseForm
    {
        string uName;
        public userdashboard(string uName)
        {
            InitializeComponent();
            this.uName = uName;
        }

        private void button_Needer_Click(object sender, EventArgs e)
        {
            Needer_DashBoard Needer = new Needer_DashBoard(uName);
            this.Hide();

            Needer.Show();
            

        }

        private void heartfulsharingbutton_Click(object sender, EventArgs e)
        {
            new Distribution(uName).Show();
            this.Hide();
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

        private void paidcoursebutton_Click(object sender, EventArgs e)
        {
            //paidcourseform form11 = new paidcourseform();   
            //this.Hide();
            //   form11 .Show();


            new PaidCourseDashboard(uName).Show();
            this.Hide();
        }

        private void editprofilebutton_Click(object sender, EventArgs e)
        {
            EditProfiledashboard profile = new EditProfiledashboard(uName);  
            this.Hide();
            profile.Show();
        }

        private void userdashboard_Load(object sender, EventArgs e)
        {
            DashboardLayout.Apply(
                this,
                "User Dashboard",
                uName,
                new[] { button_Needer, button_Distributor, paidcoursebutton, editprofilebutton },
                logoutbutton,
                new Control[] { userwelcomelabel });
        }
    }
}
