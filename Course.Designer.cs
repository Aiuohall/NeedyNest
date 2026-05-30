namespace NeedyNest
{
    partial class Course
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
            this.textBox_coursename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.filepath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxprice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_add = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.filepath_label = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_coursename
            // 
            this.textBox_coursename.Location = new System.Drawing.Point(424, 85);
            this.textBox_coursename.Multiline = true;
            this.textBox_coursename.Name = "textBox_coursename";
            this.textBox_coursename.Size = new System.Drawing.Size(271, 37);
            this.textBox_coursename.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(251, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Course name  :";
            // 
            // textBox_description
            // 
            this.textBox_description.Location = new System.Drawing.Point(424, 153);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(271, 38);
            this.textBox_description.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(268, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description  :";
            // 
            // filepath
            // 
            this.filepath.Location = new System.Drawing.Point(424, 243);
            this.filepath.Multiline = true;
            this.filepath.Name = "filepath";
            this.filepath.Size = new System.Drawing.Size(279, 39);
            this.filepath.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(288, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = " Materials :";
            // 
            // textBoxprice
            // 
            this.textBoxprice.Location = new System.Drawing.Point(424, 315);
            this.textBoxprice.Name = "textBoxprice";
            this.textBoxprice.Size = new System.Drawing.Size(271, 22);
            this.textBoxprice.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(323, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Price:";
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(596, 490);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(156, 54);
            this.button_add.TabIndex = 10;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click_1);
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(972, 490);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(147, 54);
            this.button_Back.TabIndex = 11;
            this.button_Back.Text = "back";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(759, 247);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(122, 44);
            this.button_browse.TabIndex = 14;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // filepath_label
            // 
            this.filepath_label.AutoSize = true;
            this.filepath_label.Location = new System.Drawing.Point(432, 247);
            this.filepath_label.Name = "filepath_label";
            this.filepath_label.Size = new System.Drawing.Size(44, 16);
            this.filepath_label.TabIndex = 15;
            this.filepath_label.Text = "label1";
            this.filepath_label.Click += new System.EventHandler(this.filepath_label_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(106, 490);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(154, 54);
            this.button_Clear.TabIndex = 16;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // Course
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.filepath_label);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.textBoxprice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.filepath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_coursename);
            this.Controls.Add(this.label2);
            this.Name = "Course";
            this.Text = "Course";
            this.Load += new System.EventHandler(this.Course_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_coursename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filepath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label filepath_label;
        private System.Windows.Forms.Button button_Clear;
    }
}