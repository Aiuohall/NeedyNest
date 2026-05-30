
namespace NeedyNest
{
    partial class UserApproval
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
            this.reject = new System.Windows.Forms.Button();
            this.approve = new System.Windows.Forms.Button();
            this.rolelabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.clearbutton = new System.Windows.Forms.Button();
            this.backbutton = new System.Windows.Forms.Button();
            this.confirmpasstextbox = new System.Windows.Forms.TextBox();
            this.passtextbox = new System.Windows.Forms.TextBox();
            this.contactnumtextbox = new System.Windows.Forms.TextBox();
            this.uninametextbox = new System.Windows.Forms.TextBox();
            this.usernametextbox = new System.Windows.Forms.TextBox();
            this.lastnametextbox = new System.Windows.Forms.TextBox();
            this.firstnametextbox = new System.Windows.Forms.TextBox();
            this.uninamelbel = new System.Windows.Forms.Label();
            this.phonelabel = new System.Windows.Forms.Label();
            this.confirmpasslabel = new System.Windows.Forms.Label();
            this.passlabel = new System.Windows.Forms.Label();
            this.usernamelabel = new System.Windows.Forms.Label();
            this.lastnamelabel = new System.Windows.Forms.Label();
            this.frstnamelabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reject
            // 
            this.reject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reject.ForeColor = System.Drawing.SystemColors.Highlight;
            this.reject.Location = new System.Drawing.Point(885, 472);
            this.reject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reject.Name = "reject";
            this.reject.Size = new System.Drawing.Size(189, 32);
            this.reject.TabIndex = 43;
            this.reject.Text = "Reject User";
            this.reject.UseVisualStyleBackColor = true;
            this.reject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // approve
            // 
            this.approve.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.approve.ForeColor = System.Drawing.SystemColors.Highlight;
            this.approve.Location = new System.Drawing.Point(622, 472);
            this.approve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.approve.Name = "approve";
            this.approve.Size = new System.Drawing.Size(201, 32);
            this.approve.TabIndex = 42;
            this.approve.Text = "Approve User";
            this.approve.UseVisualStyleBackColor = true;
            this.approve.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // rolelabel
            // 
            this.rolelabel.AutoSize = true;
            this.rolelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolelabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rolelabel.Location = new System.Drawing.Point(761, 139);
            this.rolelabel.Name = "rolelabel";
            this.rolelabel.Size = new System.Drawing.Size(118, 20);
            this.rolelabel.TabIndex = 41;
            this.rolelabel.Text = "Select Role :";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Admin",
            "Moderator",
            "User"});
            this.comboBox1.Location = new System.Drawing.Point(885, 139);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 40;
            // 
            // clearbutton
            // 
            this.clearbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearbutton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.clearbutton.Location = new System.Drawing.Point(225, 472);
            this.clearbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearbutton.Name = "clearbutton";
            this.clearbutton.Size = new System.Drawing.Size(136, 32);
            this.clearbutton.TabIndex = 39;
            this.clearbutton.Text = "Clear Form";
            this.clearbutton.UseVisualStyleBackColor = true;
            this.clearbutton.Click += new System.EventHandler(this.clearbutton_Click);
            // 
            // backbutton
            // 
            this.backbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbutton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.backbutton.Location = new System.Drawing.Point(1047, 584);
            this.backbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backbutton.Name = "backbutton";
            this.backbutton.Size = new System.Drawing.Size(119, 32);
            this.backbutton.TabIndex = 38;
            this.backbutton.Text = "Back";
            this.backbutton.UseVisualStyleBackColor = true;
            this.backbutton.Click += new System.EventHandler(this.backbutton_Click);
            // 
            // confirmpasstextbox
            // 
            this.confirmpasstextbox.Location = new System.Drawing.Point(308, 390);
            this.confirmpasstextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.confirmpasstextbox.Name = "confirmpasstextbox";
            this.confirmpasstextbox.Size = new System.Drawing.Size(288, 22);
            this.confirmpasstextbox.TabIndex = 36;
            // 
            // passtextbox
            // 
            this.passtextbox.Location = new System.Drawing.Point(308, 348);
            this.passtextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passtextbox.Name = "passtextbox";
            this.passtextbox.Size = new System.Drawing.Size(288, 22);
            this.passtextbox.TabIndex = 35;
            // 
            // contactnumtextbox
            // 
            this.contactnumtextbox.Location = new System.Drawing.Point(308, 307);
            this.contactnumtextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.contactnumtextbox.Name = "contactnumtextbox";
            this.contactnumtextbox.Size = new System.Drawing.Size(288, 22);
            this.contactnumtextbox.TabIndex = 34;
            // 
            // uninametextbox
            // 
            this.uninametextbox.Location = new System.Drawing.Point(308, 257);
            this.uninametextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uninametextbox.Name = "uninametextbox";
            this.uninametextbox.Size = new System.Drawing.Size(288, 22);
            this.uninametextbox.TabIndex = 33;
            // 
            // usernametextbox
            // 
            this.usernametextbox.Location = new System.Drawing.Point(308, 218);
            this.usernametextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usernametextbox.Name = "usernametextbox";
            this.usernametextbox.Size = new System.Drawing.Size(288, 22);
            this.usernametextbox.TabIndex = 32;
            // 
            // lastnametextbox
            // 
            this.lastnametextbox.Location = new System.Drawing.Point(308, 175);
            this.lastnametextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lastnametextbox.Name = "lastnametextbox";
            this.lastnametextbox.Size = new System.Drawing.Size(288, 22);
            this.lastnametextbox.TabIndex = 31;
            // 
            // firstnametextbox
            // 
            this.firstnametextbox.Location = new System.Drawing.Point(308, 137);
            this.firstnametextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.firstnametextbox.Name = "firstnametextbox";
            this.firstnametextbox.Size = new System.Drawing.Size(288, 22);
            this.firstnametextbox.TabIndex = 30;
            // 
            // uninamelbel
            // 
            this.uninamelbel.AutoSize = true;
            this.uninamelbel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uninamelbel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.uninamelbel.Location = new System.Drawing.Point(109, 260);
            this.uninamelbel.Name = "uninamelbel";
            this.uninamelbel.Size = new System.Drawing.Size(137, 20);
            this.uninamelbel.TabIndex = 29;
            this.uninamelbel.Text = "University Name:";
            // 
            // phonelabel
            // 
            this.phonelabel.AutoSize = true;
            this.phonelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phonelabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.phonelabel.Location = new System.Drawing.Point(109, 308);
            this.phonelabel.Name = "phonelabel";
            this.phonelabel.Size = new System.Drawing.Size(141, 20);
            this.phonelabel.TabIndex = 28;
            this.phonelabel.Text = "Contact Number: ";
            // 
            // confirmpasslabel
            // 
            this.confirmpasslabel.AutoSize = true;
            this.confirmpasslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmpasslabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.confirmpasslabel.Location = new System.Drawing.Point(109, 390);
            this.confirmpasslabel.Name = "confirmpasslabel";
            this.confirmpasslabel.Size = new System.Drawing.Size(157, 20);
            this.confirmpasslabel.TabIndex = 27;
            this.confirmpasslabel.Text = "Confirm Password: ";
            // 
            // passlabel
            // 
            this.passlabel.AutoSize = true;
            this.passlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passlabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.passlabel.Location = new System.Drawing.Point(109, 348);
            this.passlabel.Name = "passlabel";
            this.passlabel.Size = new System.Drawing.Size(88, 20);
            this.passlabel.TabIndex = 26;
            this.passlabel.Text = "Password:";
            // 
            // usernamelabel
            // 
            this.usernamelabel.AutoSize = true;
            this.usernamelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernamelabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.usernamelabel.Location = new System.Drawing.Point(109, 218);
            this.usernamelabel.Name = "usernamelabel";
            this.usernamelabel.Size = new System.Drawing.Size(96, 20);
            this.usernamelabel.TabIndex = 25;
            this.usernamelabel.Text = "Username: ";
            // 
            // lastnamelabel
            // 
            this.lastnamelabel.AutoSize = true;
            this.lastnamelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastnamelabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lastnamelabel.Location = new System.Drawing.Point(109, 175);
            this.lastnamelabel.Name = "lastnamelabel";
            this.lastnamelabel.Size = new System.Drawing.Size(101, 20);
            this.lastnamelabel.TabIndex = 24;
            this.lastnamelabel.Text = "Last Name: ";
            // 
            // frstnamelabel
            // 
            this.frstnamelabel.AutoSize = true;
            this.frstnamelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frstnamelabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.frstnamelabel.Location = new System.Drawing.Point(109, 137);
            this.frstnamelabel.Name = "frstnamelabel";
            this.frstnamelabel.Size = new System.Drawing.Size(102, 20);
            this.frstnamelabel.TabIndex = 23;
            this.frstnamelabel.Text = "First Name: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(426, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "Manage User Details";
            // 
            // UserApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.reject);
            this.Controls.Add(this.approve);
            this.Controls.Add(this.rolelabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.clearbutton);
            this.Controls.Add(this.backbutton);
            this.Controls.Add(this.confirmpasstextbox);
            this.Controls.Add(this.passtextbox);
            this.Controls.Add(this.contactnumtextbox);
            this.Controls.Add(this.uninametextbox);
            this.Controls.Add(this.usernametextbox);
            this.Controls.Add(this.lastnametextbox);
            this.Controls.Add(this.firstnametextbox);
            this.Controls.Add(this.uninamelbel);
            this.Controls.Add(this.phonelabel);
            this.Controls.Add(this.confirmpasslabel);
            this.Controls.Add(this.passlabel);
            this.Controls.Add(this.usernamelabel);
            this.Controls.Add(this.lastnamelabel);
            this.Controls.Add(this.frstnamelabel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserApproval";
            this.Text = "UserApproval";
            this.Load += new System.EventHandler(this.UserApproval_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button reject;
        private System.Windows.Forms.Button approve;
        private System.Windows.Forms.Label rolelabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button clearbutton;
        private System.Windows.Forms.Button backbutton;
        private System.Windows.Forms.TextBox confirmpasstextbox;
        private System.Windows.Forms.TextBox passtextbox;
        private System.Windows.Forms.TextBox contactnumtextbox;
        private System.Windows.Forms.TextBox uninametextbox;
        private System.Windows.Forms.TextBox usernametextbox;
        private System.Windows.Forms.TextBox lastnametextbox;
        private System.Windows.Forms.TextBox firstnametextbox;
        private System.Windows.Forms.Label uninamelbel;
        private System.Windows.Forms.Label phonelabel;
        private System.Windows.Forms.Label confirmpasslabel;
        private System.Windows.Forms.Label passlabel;
        private System.Windows.Forms.Label usernamelabel;
        private System.Windows.Forms.Label lastnamelabel;
        private System.Windows.Forms.Label frstnamelabel;
        private System.Windows.Forms.Label label1;
    }
}