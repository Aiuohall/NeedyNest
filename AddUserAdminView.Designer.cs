namespace NeedyNest
{
    partial class manageuserdashboardform
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
            this.alluser = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // alluser
            // 
            this.alluser.AccessibleName = "allusersinfobutton";
            this.alluser.ForeColor = System.Drawing.SystemColors.Highlight;
            this.alluser.Location = new System.Drawing.Point(53, 35);
            this.alluser.Name = "alluser";
            this.alluser.Size = new System.Drawing.Size(249, 73);
            this.alluser.TabIndex = 0;
            this.alluser.Text = "All users Info";
            this.alluser.UseVisualStyleBackColor = true;
            this.alluser.Click += new System.EventHandler(this.alluser_Click);
            // 
            // button3
            // 
            this.button3.AccessibleName = "adduserbutton";
            this.button3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button3.Location = new System.Drawing.Point(272, 142);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(249, 76);
            this.button3.TabIndex = 2;
            this.button3.Text = "Add User ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.AccessibleName = "deleteuserbutton";
            this.button4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button4.Location = new System.Drawing.Point(410, 245);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(249, 77);
            this.button4.TabIndex = 3;
            this.button4.Text = "Delete User ";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.AccessibleName = "promotemoderatorbutton";
            this.button5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button5.Location = new System.Drawing.Point(599, 356);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(249, 76);
            this.button5.TabIndex = 5;
            this.button5.Text = "Promote Moderator";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.AccessibleName = "backbutton";
            this.button6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button6.Location = new System.Drawing.Point(937, 485);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(142, 76);
            this.button6.TabIndex = 6;
            this.button6.Text = "Back";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // manageuserdashboardform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.alluser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "manageuserdashboardform";
            this.Text = "Manage User Dashboard";
            this.Load += new System.EventHandler(this.manageuserdashboardform_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button alluser;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}