namespace RsnReader
{
    partial class ReadChangesGGMMForm
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
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.button_ReadFile = new System.Windows.Forms.Button();
            this.button_Test = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Location = new System.Drawing.Point(12, 12);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(109, 23);
            this.button_OpenFile.TabIndex = 2;
            this.button_OpenFile.Text = "Открыть файл";
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // button_ReadFile
            // 
            this.button_ReadFile.Location = new System.Drawing.Point(12, 41);
            this.button_ReadFile.Name = "button_ReadFile";
            this.button_ReadFile.Size = new System.Drawing.Size(109, 23);
            this.button_ReadFile.TabIndex = 3;
            this.button_ReadFile.Text = "Прочитать файл";
            this.button_ReadFile.UseVisualStyleBackColor = true;
            this.button_ReadFile.Click += new System.EventHandler(this.button_ReadFile_Click);
            // 
            // button_Test
            // 
            this.button_Test.Location = new System.Drawing.Point(83, 167);
            this.button_Test.Name = "button_Test";
            this.button_Test.Size = new System.Drawing.Size(109, 23);
            this.button_Test.TabIndex = 4;
            this.button_Test.Text = "Тест";
            this.button_Test.UseVisualStyleBackColor = true;
            this.button_Test.Click += new System.EventHandler(this.button_Test_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // ReadChangesGGMMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_Test);
            this.Controls.Add(this.button_ReadFile);
            this.Controls.Add(this.button_OpenFile);
            this.Name = "ReadChangesGGMMForm";
            this.Text = "ReadChangesGGMMForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_OpenFile;
        private System.Windows.Forms.Button button_ReadFile;
        private System.Windows.Forms.Button button_Test;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}