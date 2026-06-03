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
            // Dedicated approval entry point (created in code so the Designer is untouched).
            var approveButton = new System.Windows.Forms.Button { Text = "Approve Members" };
            approveButton.Click += (s, ev) =>
            {
                new ApproveMembers(uName).Show();
                this.Hide();
            };

            int pending = ApproveMembers.GetPendingCount();

            DashboardLayout.Apply(
                this,
                "Manage Users",
                uName,
                new[] { approveButton, alluser, button3, button4, button5 },
                button6,
                null,
                approveButton,
                pending);
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
