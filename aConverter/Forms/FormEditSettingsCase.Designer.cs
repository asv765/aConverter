namespace aConverter.Forms
{
    partial class FormEditSettingsCase
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
            this.labelSettingsCaseName = new System.Windows.Forms.Label();
            this.textBoxSettingsCaseName = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSettingsCaseName
            // 
            this.labelSettingsCaseName.AutoSize = true;
            this.labelSettingsCaseName.Location = new System.Drawing.Point(12, 9);
            this.labelSettingsCaseName.Name = "labelSettingsCaseName";
            this.labelSettingsCaseName.Size = new System.Drawing.Size(192, 13);
            this.labelSettingsCaseName.TabIndex = 0;
            this.labelSettingsCaseName.Text = "Наименование варианта настройки:";
            // 
            // textBoxSettingsCaseName
            // 
            this.textBoxSettingsCaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSettingsCaseName.Location = new System.Drawing.Point(15, 25);
            this.textBoxSettingsCaseName.Name = "textBoxSettingsCaseName";
            this.textBoxSettingsCaseName.Size = new System.Drawing.Size(569, 20);
            this.textBoxSettingsCaseName.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(508, 51);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 22);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(426, 51);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(76, 22);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormEditSettingsCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 81);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxSettingsCaseName);
            this.Controls.Add(this.labelSettingsCaseName);
            this.Name = "FormEditSettingsCase";
            this.Text = "Вариант настройки";
            this.Load += new System.EventHandler(this.FormEditSettingsCase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSettingsCaseName;
        private System.Windows.Forms.TextBox textBoxSettingsCaseName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}