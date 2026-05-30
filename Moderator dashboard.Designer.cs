namespace NeedyNest
{
    partial class moderatordash
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
            this.welcomemoderatorlabel = new System.Windows.Forms.Label();
            this.button_managecatagory = new System.Windows.Forms.Button();
            this.button_addbooks = new System.Windows.Forms.Button();
            this.AddPaidcourse = new System.Windows.Forms.Button();
            this.Deletepaidcoursebutton = new System.Windows.Forms.Button();
            this.logoutmoderatorbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // welcomemoderatorlabel
            // 
            this.welcomemoderatorlabel.AutoSize = true;
            this.welcomemoderatorlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomemoderatorlabel.Location = new System.Drawing.Point(915, 30);
            this.welcomemoderatorlabel.Name = "welcomemoderatorlabel";
            this.welcomemoderatorlabel.Size = new System.Drawing.Size(256, 29);
            this.welcomemoderatorlabel.TabIndex = 0;
            this.welcomemoderatorlabel.Text = "Welcome, Moderator";
            // 
            // button_managecatagory
            // 
            this.button_managecatagory.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_managecatagory.Location = new System.Drawing.Point(32, 30);
            this.button_managecatagory.Name = "button_managecatagory";
            this.button_managecatagory.Size = new System.Drawing.Size(481, 77);
            this.button_managecatagory.TabIndex = 1;
            this.button_managecatagory.Text = "ManageCatagory";
            this.button_managecatagory.UseVisualStyleBackColor = true;
            this.button_managecatagory.Click += new System.EventHandler(this.button_managecatagory_Click);
            // 
            // button_addbooks
            // 
            this.button_addbooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_addbooks.Location = new System.Drawing.Point(234, 140);
            this.button_addbooks.Name = "button_addbooks";
            this.button_addbooks.Size = new System.Drawing.Size(481, 79);
            this.button_addbooks.TabIndex = 3;
            this.button_addbooks.Text = "Add Course materals ";
            this.button_addbooks.UseVisualStyleBackColor = true;
            this.button_addbooks.Click += new System.EventHandler(this.button_addbooks_Click);
            // 
            // AddPaidcourse
            // 
            this.AddPaidcourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddPaidcourse.Location = new System.Drawing.Point(426, 256);
            this.AddPaidcourse.Name = "AddPaidcourse";
            this.AddPaidcourse.Size = new System.Drawing.Size(481, 79);
            this.AddPaidcourse.TabIndex = 4;
            this.AddPaidcourse.Text = "Add Paid course";
            this.AddPaidcourse.UseVisualStyleBackColor = true;
            this.AddPaidcourse.Click += new System.EventHandler(this.AddPaidcourse_Click);
            // 
            // Deletepaidcoursebutton
            // 
            this.Deletepaidcoursebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deletepaidcoursebutton.Location = new System.Drawing.Point(661, 372);
            this.Deletepaidcoursebutton.Name = "Deletepaidcoursebutton";
            this.Deletepaidcoursebutton.Size = new System.Drawing.Size(481, 79);
            this.Deletepaidcoursebutton.TabIndex = 5;
            this.Deletepaidcoursebutton.Text = "RemovePaidcourse";
            this.Deletepaidcoursebutton.UseVisualStyleBackColor = true;
            this.Deletepaidcoursebutton.Click += new System.EventHandler(this.allreviewlogsbutton_Click);
            // 
            // logoutmoderatorbutton
            // 
            this.logoutmoderatorbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutmoderatorbutton.Location = new System.Drawing.Point(996, 573);
            this.logoutmoderatorbutton.Name = "logoutmoderatorbutton";
            this.logoutmoderatorbutton.Size = new System.Drawing.Size(226, 65);
            this.logoutmoderatorbutton.TabIndex = 7;
            this.logoutmoderatorbutton.Text = "Log Out";
            this.logoutmoderatorbutton.UseVisualStyleBackColor = true;
            this.logoutmoderatorbutton.Click += new System.EventHandler(this.logoutmoderatorbutton_Click);
            // 
            // moderatordash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.logoutmoderatorbutton);
            this.Controls.Add(this.Deletepaidcoursebutton);
            this.Controls.Add(this.AddPaidcourse);
            this.Controls.Add(this.button_addbooks);
            this.Controls.Add(this.button_managecatagory);
            this.Controls.Add(this.welcomemoderatorlabel);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "moderatordash";
            this.Text = "Moderator Dashboard";
            this.Load += new System.EventHandler(this.moderatordash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcomemoderatorlabel;
        private System.Windows.Forms.Button button_managecatagory;
        private System.Windows.Forms.Button button_addbooks;
        private System.Windows.Forms.Button AddPaidcourse;
        private System.Windows.Forms.Button Deletepaidcoursebutton;
        private System.Windows.Forms.Button logoutmoderatorbutton;
    }
}