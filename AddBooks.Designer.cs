namespace NeedyNest
{
    partial class AddBooks
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
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_OpenSlide = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_browse = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(150, 148);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 403);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button_Refresh
            // 
            this.button_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Refresh.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button_Refresh.Location = new System.Drawing.Point(25, 64);
            this.button_Refresh.Margin = new System.Windows.Forms.Padding(4);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(151, 51);
            this.button_Refresh.TabIndex = 6;
            this.button_Refresh.Text = "Refresh";
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // button_OpenSlide
            // 
            this.button_OpenSlide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_OpenSlide.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button_OpenSlide.Location = new System.Drawing.Point(217, 64);
            this.button_OpenSlide.Margin = new System.Windows.Forms.Padding(4);
            this.button_OpenSlide.Name = "button_OpenSlide";
            this.button_OpenSlide.Size = new System.Drawing.Size(135, 54);
            this.button_OpenSlide.TabIndex = 7;
            this.button_OpenSlide.Text = "Open";
            this.button_OpenSlide.UseVisualStyleBackColor = true;
            this.button_OpenSlide.Click += new System.EventHandler(this.button_OpenSlide_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(438, 68);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(372, 38);
            this.textBox1.TabIndex = 9;
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(892, 64);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(135, 42);
            this.button_browse.TabIndex = 10;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(1066, 62);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(133, 44);
            this.button_save.TabIndex = 11;
            this.button_save.Text = "save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button1.Location = new System.Drawing.Point(1006, 577);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 60);
            this.button1.TabIndex = 12;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_OpenSlide);
            this.Name = "AddBooks";
            this.Text = "Add_books";
            this.Load += new System.EventHandler(this.Form_Book_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Button button_OpenSlide;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button1;
    }
}