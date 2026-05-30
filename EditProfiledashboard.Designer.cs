namespace NeedyNest
{
    partial class EditProfiledashboard
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
            this.button1_back = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label_newusername = new System.Windows.Forms.Label();
            this.label_newPassword = new System.Windows.Forms.Label();
            this.label_newContact = new System.Windows.Forms.Label();
            this.label_newUni = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtUni = new System.Windows.Forms.TextBox();
            this.button_update = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1_back
            // 
            this.button1_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1_back.Location = new System.Drawing.Point(1019, 565);
            this.button1_back.Name = "button1_back";
            this.button1_back.Size = new System.Drawing.Size(148, 54);
            this.button1_back.TabIndex = 0;
            this.button1_back.Text = "Back";
            this.button1_back.UseVisualStyleBackColor = true;
            this.button1_back.Click += new System.EventHandler(this.button1_back_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(415, 110);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(312, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // label_newusername
            // 
            this.label_newusername.AutoSize = true;
            this.label_newusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newusername.Location = new System.Drawing.Point(259, 110);
            this.label_newusername.Name = "label_newusername";
            this.label_newusername.Size = new System.Drawing.Size(144, 22);
            this.label_newusername.TabIndex = 2;
            this.label_newusername.Text = "New Usename:";
            // 
            // label_newPassword
            // 
            this.label_newPassword.AutoSize = true;
            this.label_newPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newPassword.Location = new System.Drawing.Point(255, 175);
            this.label_newPassword.Name = "label_newPassword";
            this.label_newPassword.Size = new System.Drawing.Size(148, 22);
            this.label_newPassword.TabIndex = 3;
            this.label_newPassword.Text = "New Password:";
            // 
            // label_newContact
            // 
            this.label_newContact.AutoSize = true;
            this.label_newContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newContact.Location = new System.Drawing.Point(273, 251);
            this.label_newContact.Name = "label_newContact";
            this.label_newContact.Size = new System.Drawing.Size(130, 22);
            this.label_newContact.TabIndex = 4;
            this.label_newContact.Text = "New Contact:";
            // 
            // label_newUni
            // 
            this.label_newUni.AutoSize = true;
            this.label_newUni.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newUni.Location = new System.Drawing.Point(241, 318);
            this.label_newUni.Name = "label_newUni";
            this.label_newUni.Size = new System.Drawing.Size(162, 22);
            this.label_newUni.TabIndex = 5;
            this.label_newUni.Text = "Uinveristy Name:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(415, 175);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(312, 22);
            this.txtPassword.TabIndex = 6;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(415, 253);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(312, 22);
            this.txtContact.TabIndex = 7;
            // 
            // txtUni
            // 
            this.txtUni.Location = new System.Drawing.Point(415, 320);
            this.txtUni.Name = "txtUni";
            this.txtUni.Size = new System.Drawing.Size(312, 22);
            this.txtUni.TabIndex = 8;
            // 
            // button_update
            // 
            this.button_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.Location = new System.Drawing.Point(557, 565);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(205, 54);
            this.button_update.TabIndex = 9;
            this.button_update.Text = "Update Profile";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // EditProfiledashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.txtUni);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label_newUni);
            this.Controls.Add(this.label_newContact);
            this.Controls.Add(this.label_newPassword);
            this.Controls.Add(this.label_newusername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.button1_back);
            this.Name = "EditProfiledashboard";
            this.Text = "EditProfiledashboard";
            this.Load += new System.EventHandler(this.EditProfiledashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_back;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label_newusername;
        private System.Windows.Forms.Label label_newPassword;
        private System.Windows.Forms.Label label_newContact;
        private System.Windows.Forms.Label label_newUni;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtUni;
        private System.Windows.Forms.Button button_update;
    }
}