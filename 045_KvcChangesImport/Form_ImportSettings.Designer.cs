namespace _045_KvcChangesImport
{
    partial class Form_ImportSettings
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
            this.textBox_ImportYear = new System.Windows.Forms.TextBox();
            this.label_ImportYear = new System.Windows.Forms.Label();
            this.textBox_ImportMonth = new System.Windows.Forms.TextBox();
            this.label_ImportMonth = new System.Windows.Forms.Label();
            this.button_Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_ImportYear
            // 
            this.textBox_ImportYear.Location = new System.Drawing.Point(104, 12);
            this.textBox_ImportYear.Name = "textBox_ImportYear";
            this.textBox_ImportYear.Size = new System.Drawing.Size(168, 20);
            this.textBox_ImportYear.TabIndex = 1;
            // 
            // label_ImportYear
            // 
            this.label_ImportYear.AutoSize = true;
            this.label_ImportYear.Location = new System.Drawing.Point(12, 15);
            this.label_ImportYear.Name = "label_ImportYear";
            this.label_ImportYear.Size = new System.Drawing.Size(71, 13);
            this.label_ImportYear.TabIndex = 0;
            this.label_ImportYear.Text = "Год импорта";
            // 
            // textBox_ImportMonth
            // 
            this.textBox_ImportMonth.Location = new System.Drawing.Point(104, 38);
            this.textBox_ImportMonth.Name = "textBox_ImportMonth";
            this.textBox_ImportMonth.Size = new System.Drawing.Size(168, 20);
            this.textBox_ImportMonth.TabIndex = 2;
            // 
            // label_ImportMonth
            // 
            this.label_ImportMonth.AutoSize = true;
            this.label_ImportMonth.Location = new System.Drawing.Point(12, 41);
            this.label_ImportMonth.Name = "label_ImportMonth";
            this.label_ImportMonth.Size = new System.Drawing.Size(86, 13);
            this.label_ImportMonth.TabIndex = 0;
            this.label_ImportMonth.Text = "Месяц импорта";
            // 
            // button_Ok
            // 
            this.button_Ok.Location = new System.Drawing.Point(94, 226);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(75, 23);
            this.button_Ok.TabIndex = 3;
            this.button_Ok.Text = "OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // Form_ImportSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.label_ImportMonth);
            this.Controls.Add(this.label_ImportYear);
            this.Controls.Add(this.textBox_ImportMonth);
            this.Controls.Add(this.textBox_ImportYear);
            this.Name = "Form_ImportSettings";
            this.Text = "Form_ImportSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ImportYear;
        private System.Windows.Forms.Label label_ImportYear;
        private System.Windows.Forms.TextBox textBox_ImportMonth;
        private System.Windows.Forms.Label label_ImportMonth;
        private System.Windows.Forms.Button button_Ok;
    }
}