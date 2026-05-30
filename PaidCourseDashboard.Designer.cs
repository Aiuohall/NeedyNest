namespace NeedyNest
{
    partial class PaidCourseDashboard
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Enroll = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.paynowbutton = new System.Windows.Forms.Button();
            this.expirationdatetime = new System.Windows.Forms.DateTimePicker();
            this.cvvtextbox = new System.Windows.Forms.TextBox();
            this.entercardinfotextbox = new System.Windows.Forms.TextBox();
            this.expirationdatelabel = new System.Windows.Forms.Label();
            this.cvvlabel = new System.Windows.Forms.Label();
            this.entercardnumberlabel = new System.Windows.Forms.Label();
            this.bkash = new System.Windows.Forms.CheckBox();
            this.card = new System.Windows.Forms.CheckBox();
            this.bkashpininputtextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bkashnuminputtextbox = new System.Windows.Forms.TextBox();
            this.bkashnumberinputlabel = new System.Windows.Forms.Label();
            this.button_open = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(149, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(658, 304);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button_Enroll
            // 
            this.button_Enroll.Location = new System.Drawing.Point(667, 447);
            this.button_Enroll.Name = "button_Enroll";
            this.button_Enroll.Size = new System.Drawing.Size(140, 49);
            this.button_Enroll.TabIndex = 1;
            this.button_Enroll.Text = "Enroll";
            this.button_Enroll.UseVisualStyleBackColor = true;
            this.button_Enroll.Click += new System.EventHandler(this.button_Enroll_Click);
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(3, 12);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(140, 49);
            this.button_Back.TabIndex = 2;
            this.button_Back.Text = "Back";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // paynowbutton
            // 
            this.paynowbutton.Location = new System.Drawing.Point(1075, 349);
            this.paynowbutton.Name = "paynowbutton";
            this.paynowbutton.Size = new System.Drawing.Size(108, 23);
            this.paynowbutton.TabIndex = 15;
            this.paynowbutton.Text = "Pay Now";
            this.paynowbutton.UseVisualStyleBackColor = true;
            // 
            // expirationdatetime
            // 
            this.expirationdatetime.Location = new System.Drawing.Point(868, 294);
            this.expirationdatetime.Name = "expirationdatetime";
            this.expirationdatetime.Size = new System.Drawing.Size(200, 22);
            this.expirationdatetime.TabIndex = 14;
            // 
            // cvvtextbox
            // 
            this.cvvtextbox.Location = new System.Drawing.Point(1125, 292);
            this.cvvtextbox.Name = "cvvtextbox";
            this.cvvtextbox.Size = new System.Drawing.Size(100, 22);
            this.cvvtextbox.TabIndex = 13;
            // 
            // entercardinfotextbox
            // 
            this.entercardinfotextbox.Location = new System.Drawing.Point(908, 212);
            this.entercardinfotextbox.Name = "entercardinfotextbox";
            this.entercardinfotextbox.Size = new System.Drawing.Size(266, 22);
            this.entercardinfotextbox.TabIndex = 12;
            // 
            // expirationdatelabel
            // 
            this.expirationdatelabel.AutoSize = true;
            this.expirationdatelabel.Location = new System.Drawing.Point(865, 256);
            this.expirationdatelabel.Name = "expirationdatelabel";
            this.expirationdatelabel.Size = new System.Drawing.Size(101, 16);
            this.expirationdatelabel.TabIndex = 11;
            this.expirationdatelabel.Text = "Expiration Date:";
            // 
            // cvvlabel
            // 
            this.cvvlabel.AutoSize = true;
            this.cvvlabel.Location = new System.Drawing.Point(1122, 256);
            this.cvvlabel.Name = "cvvlabel";
            this.cvvlabel.Size = new System.Drawing.Size(34, 16);
            this.cvvlabel.TabIndex = 10;
            this.cvvlabel.Text = "CVV";
            // 
            // entercardnumberlabel
            // 
            this.entercardnumberlabel.AutoSize = true;
            this.entercardnumberlabel.Location = new System.Drawing.Point(920, 178);
            this.entercardnumberlabel.Name = "entercardnumberlabel";
            this.entercardnumberlabel.Size = new System.Drawing.Size(226, 16);
            this.entercardnumberlabel.TabIndex = 9;
            this.entercardnumberlabel.Text = "Enter Card Information:(Card number)";
            // 
            // bkash
            // 
            this.bkash.AutoSize = true;
            this.bkash.Location = new System.Drawing.Point(908, 69);
            this.bkash.Name = "bkash";
            this.bkash.Size = new System.Drawing.Size(67, 20);
            this.bkash.TabIndex = 16;
            this.bkash.Text = "Bkash";
            this.bkash.UseVisualStyleBackColor = true;
            // 
            // card
            // 
            this.card.AutoSize = true;
            this.card.Location = new System.Drawing.Point(1051, 69);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(58, 20);
            this.card.TabIndex = 17;
            this.card.Text = "Card";
            this.card.UseVisualStyleBackColor = true;
            // 
            // bkashpininputtextbox
            // 
            this.bkashpininputtextbox.Location = new System.Drawing.Point(936, 532);
            this.bkashpininputtextbox.Name = "bkashpininputtextbox";
            this.bkashpininputtextbox.Size = new System.Drawing.Size(100, 22);
            this.bkashpininputtextbox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(919, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Enter your Bkash Pin";
            // 
            // bkashnuminputtextbox
            // 
            this.bkashnuminputtextbox.Location = new System.Drawing.Point(910, 431);
            this.bkashnuminputtextbox.Name = "bkashnuminputtextbox";
            this.bkashnuminputtextbox.Size = new System.Drawing.Size(163, 22);
            this.bkashnuminputtextbox.TabIndex = 19;
            // 
            // bkashnumberinputlabel
            // 
            this.bkashnumberinputlabel.AutoSize = true;
            this.bkashnumberinputlabel.Location = new System.Drawing.Point(890, 381);
            this.bkashnumberinputlabel.Name = "bkashnumberinputlabel";
            this.bkashnumberinputlabel.Size = new System.Drawing.Size(162, 16);
            this.bkashnumberinputlabel.TabIndex = 18;
            this.bkashnumberinputlabel.Text = "Enter your Bkash Number ";
            // 
            // button_open
            // 
            this.button_open.Location = new System.Drawing.Point(3, 87);
            this.button_open.Name = "button_open";
            this.button_open.Size = new System.Drawing.Size(140, 49);
            this.button_open.TabIndex = 22;
            this.button_open.Text = "Open";
            this.button_open.UseVisualStyleBackColor = true;
            this.button_open.Click += new System.EventHandler(this.button_open_Click);
            // 
            // PaidCourseDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 793);
            this.Controls.Add(this.button_open);
            this.Controls.Add(this.bkashpininputtextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bkashnuminputtextbox);
            this.Controls.Add(this.bkashnumberinputlabel);
            this.Controls.Add(this.card);
            this.Controls.Add(this.bkash);
            this.Controls.Add(this.paynowbutton);
            this.Controls.Add(this.expirationdatetime);
            this.Controls.Add(this.cvvtextbox);
            this.Controls.Add(this.entercardinfotextbox);
            this.Controls.Add(this.expirationdatelabel);
            this.Controls.Add(this.cvvlabel);
            this.Controls.Add(this.entercardnumberlabel);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.button_Enroll);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PaidCourseDashboard";
            this.Text = "PaidCourseDashboard";
            this.Load += new System.EventHandler(this.PaidCourseDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Enroll;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button paynowbutton;
        private System.Windows.Forms.DateTimePicker expirationdatetime;
        private System.Windows.Forms.TextBox cvvtextbox;
        private System.Windows.Forms.TextBox entercardinfotextbox;
        private System.Windows.Forms.Label expirationdatelabel;
        private System.Windows.Forms.Label cvvlabel;
        private System.Windows.Forms.Label entercardnumberlabel;
        private System.Windows.Forms.CheckBox bkash;
        private System.Windows.Forms.CheckBox card;
        private System.Windows.Forms.TextBox bkashpininputtextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bkashnuminputtextbox;
        private System.Windows.Forms.Label bkashnumberinputlabel;
        private System.Windows.Forms.Button button_open;
    }
}