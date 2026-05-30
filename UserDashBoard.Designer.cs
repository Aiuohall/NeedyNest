namespace NeedyNest
{
    partial class userdashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Needer = new System.Windows.Forms.Button();
            this.button_Distributor = new System.Windows.Forms.Button();
            this.paidcoursebutton = new System.Windows.Forms.Button();
            this.logoutbutton = new System.Windows.Forms.Button();
            this.userwelcomelabel = new System.Windows.Forms.Label();
            this.editprofilebutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Needer
            // 
            this.button_Needer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Needer.Location = new System.Drawing.Point(116, 106);
            this.button_Needer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Needer.Name = "button_Needer";
            this.button_Needer.Size = new System.Drawing.Size(295, 67);
            this.button_Needer.TabIndex = 0;
            this.button_Needer.Text = "For Needer";
            this.button_Needer.UseVisualStyleBackColor = true;
            this.button_Needer.Click += new System.EventHandler(this.button_Needer_Click);
            // 
            // button_Distributor
            // 
            this.button_Distributor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Distributor.Location = new System.Drawing.Point(355, 226);
            this.button_Distributor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Distributor.Name = "button_Distributor";
            this.button_Distributor.Size = new System.Drawing.Size(295, 71);
            this.button_Distributor.TabIndex = 1;
            this.button_Distributor.Text = "For Distributor";
            this.button_Distributor.UseVisualStyleBackColor = true;
            this.button_Distributor.Click += new System.EventHandler(this.heartfulsharingbutton_Click);
            // 
            // paidcoursebutton
            // 
            this.paidcoursebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidcoursebutton.Location = new System.Drawing.Point(564, 378);
            this.paidcoursebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.paidcoursebutton.Name = "paidcoursebutton";
            this.paidcoursebutton.Size = new System.Drawing.Size(295, 60);
            this.paidcoursebutton.TabIndex = 2;
            this.paidcoursebutton.Text = "Paid Course";
            this.paidcoursebutton.UseVisualStyleBackColor = true;
            this.paidcoursebutton.Click += new System.EventHandler(this.paidcoursebutton_Click);
            // 
            // logoutbutton
            // 
            this.logoutbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutbutton.Location = new System.Drawing.Point(1026, 567);
            this.logoutbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logoutbutton.Name = "logoutbutton";
            this.logoutbutton.Size = new System.Drawing.Size(172, 52);
            this.logoutbutton.TabIndex = 5;
            this.logoutbutton.Text = "Log Out";
            this.logoutbutton.UseVisualStyleBackColor = true;
            this.logoutbutton.Click += new System.EventHandler(this.logoutbutton_Click);
            // 
            // userwelcomelabel
            // 
            this.userwelcomelabel.AutoSize = true;
            this.userwelcomelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userwelcomelabel.Location = new System.Drawing.Point(480, 32);
            this.userwelcomelabel.Name = "userwelcomelabel";
            this.userwelcomelabel.Size = new System.Drawing.Size(170, 25);
            this.userwelcomelabel.TabIndex = 6;
            this.userwelcomelabel.Text = "Welcome ,[user]";
            // 
            // editprofilebutton
            // 
            this.editprofilebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editprofilebutton.Location = new System.Drawing.Point(1026, 71);
            this.editprofilebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.editprofilebutton.Name = "editprofilebutton";
            this.editprofilebutton.Size = new System.Drawing.Size(181, 59);
            this.editprofilebutton.TabIndex = 7;
            this.editprofilebutton.Text = "Edit Profile";
            this.editprofilebutton.UseVisualStyleBackColor = true;
            this.editprofilebutton.Click += new System.EventHandler(this.editprofilebutton_Click);
            // 
            // userdashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.editprofilebutton);
            this.Controls.Add(this.userwelcomelabel);
            this.Controls.Add(this.logoutbutton);
            this.Controls.Add(this.paidcoursebutton);
            this.Controls.Add(this.button_Distributor);
            this.Controls.Add(this.button_Needer);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "userdashboard";
            this.Text = "User Dashboard";
            this.Load += new System.EventHandler(this.userdashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Needer;
        private System.Windows.Forms.Button button_Distributor;
        private System.Windows.Forms.Button paidcoursebutton;
        private System.Windows.Forms.Button logoutbutton;
        private System.Windows.Forms.Label userwelcomelabel;
        private System.Windows.Forms.Button editprofilebutton;
    }
}