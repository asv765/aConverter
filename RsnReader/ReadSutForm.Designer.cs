namespace RsnReader
{
    partial class ReadSutForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.button_ReadFile = new System.Windows.Forms.Button();
            this.textBox_LsKvc = new System.Windows.Forms.TextBox();
            this.button_PaysByLsKvc = new System.Windows.Forms.Button();
            this.button_Test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Location = new System.Drawing.Point(12, 12);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(103, 23);
            this.button_OpenFile.TabIndex = 0;
            this.button_OpenFile.Text = "Открыть файл";
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // button_ReadFile
            // 
            this.button_ReadFile.Location = new System.Drawing.Point(12, 41);
            this.button_ReadFile.Name = "button_ReadFile";
            this.button_ReadFile.Size = new System.Drawing.Size(103, 23);
            this.button_ReadFile.TabIndex = 0;
            this.button_ReadFile.Text = "Прочитать файл";
            this.button_ReadFile.UseVisualStyleBackColor = true;
            this.button_ReadFile.Click += new System.EventHandler(this.button_ReadFile_Click);
            // 
            // textBox_LsKvc
            // 
            this.textBox_LsKvc.Location = new System.Drawing.Point(138, 88);
            this.textBox_LsKvc.Name = "textBox_LsKvc";
            this.textBox_LsKvc.Size = new System.Drawing.Size(134, 20);
            this.textBox_LsKvc.TabIndex = 1;
            // 
            // button_PaysByLsKvc
            // 
            this.button_PaysByLsKvc.Location = new System.Drawing.Point(12, 86);
            this.button_PaysByLsKvc.Name = "button_PaysByLsKvc";
            this.button_PaysByLsKvc.Size = new System.Drawing.Size(120, 23);
            this.button_PaysByLsKvc.TabIndex = 0;
            this.button_PaysByLsKvc.Text = "Платежи по ЛС КВЦ";
            this.button_PaysByLsKvc.UseVisualStyleBackColor = true;
            this.button_PaysByLsKvc.Click += new System.EventHandler(this.button_PaysByLsKvc_Click);
            // 
            // button_Test
            // 
            this.button_Test.Location = new System.Drawing.Point(77, 226);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(120, 23);
            this.button_Test.TabIndex = 0;
            this.button_Test.Text = "Тест";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // ReadSutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBox_LsKvc);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.button_PaysByLsKvc);
            this.Controls.Add(this.button_ReadFile);
            this.Controls.Add(this.button_OpenFile);
            this.Name = "ReadSutForm";
            this.Text = "ReadSutForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_OpenFile;
        private System.Windows.Forms.Button button_ReadFile;
        private System.Windows.Forms.TextBox textBox_LsKvc;
        private System.Windows.Forms.Button button_PaysByLsKvc;
        private System.Windows.Forms.Button button_Test;
    }
}