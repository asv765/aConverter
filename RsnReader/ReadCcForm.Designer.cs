namespace RsnReader
{
    partial class ReadCcForm
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
            this.Search_button = new System.Windows.Forms.Button();
            this.LsKvc_textBox = new System.Windows.Forms.TextBox();
            this.test_button = new System.Windows.Forms.Button();
            this.OpenFile_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openTestFile_button = new System.Windows.Forms.Button();
            this.ReadFile_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(12, 105);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(114, 23);
            this.Search_button.TabIndex = 0;
            this.Search_button.Text = "Найти по ЛС КВЦ";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // LsKvc_textBox
            // 
            this.LsKvc_textBox.Location = new System.Drawing.Point(132, 107);
            this.LsKvc_textBox.Name = "LsKvc_textBox";
            this.LsKvc_textBox.Size = new System.Drawing.Size(125, 20);
            this.LsKvc_textBox.TabIndex = 1;
            // 
            // test_button
            // 
            this.test_button.Location = new System.Drawing.Point(90, 226);
            this.test_button.Name = "test_button";
            this.test_button.Size = new System.Drawing.Size(114, 23);
            this.test_button.TabIndex = 0;
            this.test_button.Text = "Тест";
            this.test_button.UseVisualStyleBackColor = true;
            this.test_button.Click += new System.EventHandler(this.test_button_Click);
            // 
            // OpenFile_button
            // 
            this.OpenFile_button.Location = new System.Drawing.Point(12, 12);
            this.OpenFile_button.Name = "OpenFile_button";
            this.OpenFile_button.Size = new System.Drawing.Size(114, 23);
            this.OpenFile_button.TabIndex = 0;
            this.OpenFile_button.Text = "Открыть файл";
            this.OpenFile_button.UseVisualStyleBackColor = true;
            this.OpenFile_button.Click += new System.EventHandler(this.OpenFile_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openTestFile_button
            // 
            this.openTestFile_button.Location = new System.Drawing.Point(143, 12);
            this.openTestFile_button.Name = "openTestFile_button";
            this.openTestFile_button.Size = new System.Drawing.Size(114, 23);
            this.openTestFile_button.TabIndex = 0;
            this.openTestFile_button.Text = "Открыть тестовый файл";
            this.openTestFile_button.UseVisualStyleBackColor = true;
            this.openTestFile_button.Click += new System.EventHandler(this.openTestFile_button_Click);
            // 
            // ReadFile_button
            // 
            this.ReadFile_button.Location = new System.Drawing.Point(12, 41);
            this.ReadFile_button.Name = "ReadFile_button";
            this.ReadFile_button.Size = new System.Drawing.Size(114, 23);
            this.ReadFile_button.TabIndex = 0;
            this.ReadFile_button.Text = "Прочитать файл";
            this.ReadFile_button.UseVisualStyleBackColor = true;
            this.ReadFile_button.Click += new System.EventHandler(this.ReadFile_button_Click);
            // 
            // ReadCcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 261);
            this.Controls.Add(this.LsKvc_textBox);
            this.Controls.Add(this.openTestFile_button);
            this.Controls.Add(this.ReadFile_button);
            this.Controls.Add(this.OpenFile_button);
            this.Controls.Add(this.test_button);
            this.Controls.Add(this.Search_button);
            this.Name = "ReadCcForm";
            this.Text = "ReadCcForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.TextBox LsKvc_textBox;
        private System.Windows.Forms.Button test_button;
        private System.Windows.Forms.Button OpenFile_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openTestFile_button;
        private System.Windows.Forms.Button ReadFile_button;
    }
}