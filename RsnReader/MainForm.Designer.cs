namespace RsnReader
{
    partial class MainForm
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
            this.OpenReadRsnForm_button = new System.Windows.Forms.Button();
            this.OpenAnalyzeForm_button = new System.Windows.Forms.Button();
            this.ReadSut_button = new System.Windows.Forms.Button();
            this.ReadCc_button = new System.Windows.Forms.Button();
            this.button_ReadIdom = new System.Windows.Forms.Button();
            this.button_Isch = new System.Windows.Forms.Button();
            this.button_ReadChanges = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenReadRsnForm_button
            // 
            this.OpenReadRsnForm_button.Location = new System.Drawing.Point(12, 12);
            this.OpenReadRsnForm_button.Name = "OpenReadRsnForm_button";
            this.OpenReadRsnForm_button.Size = new System.Drawing.Size(109, 23);
            this.OpenReadRsnForm_button.TabIndex = 0;
            this.OpenReadRsnForm_button.Text = "Прочитать RSN";
            this.OpenReadRsnForm_button.UseVisualStyleBackColor = true;
            this.OpenReadRsnForm_button.Click += new System.EventHandler(this.OpenReadRsnForm_button_Click);
            // 
            // OpenAnalyzeForm_button
            // 
            this.OpenAnalyzeForm_button.Location = new System.Drawing.Point(70, 95);
            this.OpenAnalyzeForm_button.Name = "OpenAnalyzeForm_button";
            this.OpenAnalyzeForm_button.Size = new System.Drawing.Size(131, 23);
            this.OpenAnalyzeForm_button.TabIndex = 1;
            this.OpenAnalyzeForm_button.Text = "Проанализировать";
            this.OpenAnalyzeForm_button.UseVisualStyleBackColor = true;
            this.OpenAnalyzeForm_button.Click += new System.EventHandler(this.OpenAnalyzeForm_button_Click);
            // 
            // ReadSut_button
            // 
            this.ReadSut_button.Location = new System.Drawing.Point(141, 12);
            this.ReadSut_button.Name = "ReadSut_button";
            this.ReadSut_button.Size = new System.Drawing.Size(131, 23);
            this.ReadSut_button.TabIndex = 1;
            this.ReadSut_button.Text = "Прочитать SUT";
            this.ReadSut_button.UseVisualStyleBackColor = true;
            this.ReadSut_button.Click += new System.EventHandler(this.ReadSut_button_Click);
            // 
            // ReadCc_button
            // 
            this.ReadCc_button.Location = new System.Drawing.Point(12, 41);
            this.ReadCc_button.Name = "ReadCc_button";
            this.ReadCc_button.Size = new System.Drawing.Size(109, 23);
            this.ReadCc_button.TabIndex = 0;
            this.ReadCc_button.Text = "Прочитать CC";
            this.ReadCc_button.UseVisualStyleBackColor = true;
            this.ReadCc_button.Click += new System.EventHandler(this.ReadCc_button_Click);
            // 
            // button_ReadIdom
            // 
            this.button_ReadIdom.Location = new System.Drawing.Point(12, 156);
            this.button_ReadIdom.Name = "button_ReadIdom";
            this.button_ReadIdom.Size = new System.Drawing.Size(109, 23);
            this.button_ReadIdom.TabIndex = 2;
            this.button_ReadIdom.Text = "Прочитать Idom";
            this.button_ReadIdom.UseVisualStyleBackColor = true;
            this.button_ReadIdom.Click += new System.EventHandler(this.button_ReadIdom_Click);
            // 
            // button_Isch
            // 
            this.button_Isch.Location = new System.Drawing.Point(12, 185);
            this.button_Isch.Name = "button_Isch";
            this.button_Isch.Size = new System.Drawing.Size(109, 23);
            this.button_Isch.TabIndex = 3;
            this.button_Isch.Text = "Прочитать Isch";
            this.button_Isch.UseVisualStyleBackColor = true;
            this.button_Isch.Click += new System.EventHandler(this.button_Isch_Click);
            // 
            // button_ReadChanges
            // 
            this.button_ReadChanges.Location = new System.Drawing.Point(12, 214);
            this.button_ReadChanges.Name = "button_ReadChanges";
            this.button_ReadChanges.Size = new System.Drawing.Size(109, 23);
            this.button_ReadChanges.TabIndex = 3;
            this.button_ReadChanges.Text = "Прочитать ГГММ";
            this.button_ReadChanges.UseVisualStyleBackColor = true;
            this.button_ReadChanges.Click += new System.EventHandler(this.button_ReadChanges_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_ReadChanges);
            this.Controls.Add(this.button_Isch);
            this.Controls.Add(this.button_ReadIdom);
            this.Controls.Add(this.ReadSut_button);
            this.Controls.Add(this.OpenAnalyzeForm_button);
            this.Controls.Add(this.ReadCc_button);
            this.Controls.Add(this.OpenReadRsnForm_button);
            this.Name = "MainForm";
            this.Text = "КВЦ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenReadRsnForm_button;
        private System.Windows.Forms.Button OpenAnalyzeForm_button;
        private System.Windows.Forms.Button ReadSut_button;
        private System.Windows.Forms.Button ReadCc_button;
        private System.Windows.Forms.Button button_ReadIdom;
        private System.Windows.Forms.Button button_Isch;
        private System.Windows.Forms.Button button_ReadChanges;
    }
}