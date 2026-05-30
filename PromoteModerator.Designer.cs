namespace NeedyNest
{
    partial class PromoteModerator
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
            this.button_back = new System.Windows.Forms.Button();
            this.button_promote = new System.Windows.Forms.Button();
            this.label_ModInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(198, 127);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(853, 319);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_back
            // 
            this.button_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_back.Location = new System.Drawing.Point(1044, 549);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(114, 48);
            this.button_back.TabIndex = 1;
            this.button_back.Text = "Back";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_promote
            // 
            this.button_promote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_promote.Location = new System.Drawing.Point(218, 549);
            this.button_promote.Name = "button_promote";
            this.button_promote.Size = new System.Drawing.Size(148, 48);
            this.button_promote.TabIndex = 2;
            this.button_promote.Text = "Promote";
            this.button_promote.UseVisualStyleBackColor = true;
            this.button_promote.Click += new System.EventHandler(this.button_promote_Click);
            // 
            // label_ModInfo
            // 
            this.label_ModInfo.AutoSize = true;
            this.label_ModInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ModInfo.Location = new System.Drawing.Point(469, 36);
            this.label_ModInfo.Name = "label_ModInfo";
            this.label_ModInfo.Size = new System.Drawing.Size(230, 25);
            this.label_ModInfo.TabIndex = 3;
            this.label_ModInfo.Text = "Moderator Information:";
            // 
            // PromoteModerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.label_ModInfo);
            this.Controls.Add(this.button_promote);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PromoteModerator";
            this.Text = "PromoteModerator";
            this.Load += new System.EventHandler(this.PromoteModerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Button button_promote;
        private System.Windows.Forms.Label label_ModInfo;
    }
}