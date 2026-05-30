namespace NeedyNest
{
    partial class Needer_DashBoard
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
            this.components = new System.ComponentModel.Container();
            this.button_Books = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button_Books
            // 
            this.button_Books.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Books.Location = new System.Drawing.Point(470, 180);
            this.button_Books.Name = "button_Books";
            this.button_Books.Size = new System.Drawing.Size(191, 89);
            this.button_Books.TabIndex = 0;
            this.button_Books.Text = "CourseMatrials ";
            this.button_Books.UseVisualStyleBackColor = true;
            this.button_Books.Click += new System.EventHandler(this.button_Books_Click);
            // 
            // button_back
            // 
            this.button_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_back.Location = new System.Drawing.Point(1099, 564);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(91, 55);
            this.button_back.TabIndex = 1;
            this.button_back.Text = "Back";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // Needer_DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_Books);
            this.Name = "Needer_DashBoard";
            this.Text = "Needer_Dasboard";
            this.Load += new System.EventHandler(this.Needer_DashBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Books;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Timer timer1;
    }
}