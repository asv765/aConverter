namespace aConverter.Forms
{
    partial class FormStatistic
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelStatisticName = new System.Windows.Forms.Label();
            this.textBoxStatisticName = new System.Windows.Forms.TextBox();
            this.labelStatisticType = new System.Windows.Forms.Label();
            this.comboBoxStatisticType = new System.Windows.Forms.ComboBox();
            this.labelSQL = new System.Windows.Forms.Label();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.labelStatisticClass = new System.Windows.Forms.Label();
            this.radioButtonDbfClass = new System.Windows.Forms.RadioButton();
            this.radioButtonFdbClass = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(540, 439);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(459, 439);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelStatisticName
            // 
            this.labelStatisticName.AutoSize = true;
            this.labelStatisticName.Location = new System.Drawing.Point(12, 9);
            this.labelStatisticName.Name = "labelStatisticName";
            this.labelStatisticName.Size = new System.Drawing.Size(146, 13);
            this.labelStatisticName.TabIndex = 2;
            this.labelStatisticName.Text = "Наименование статистики:";
            // 
            // textBoxStatisticName
            // 
            this.textBoxStatisticName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatisticName.Location = new System.Drawing.Point(15, 25);
            this.textBoxStatisticName.Name = "textBoxStatisticName";
            this.textBoxStatisticName.Size = new System.Drawing.Size(600, 20);
            this.textBoxStatisticName.TabIndex = 3;
            // 
            // labelStatisticType
            // 
            this.labelStatisticType.AutoSize = true;
            this.labelStatisticType.Location = new System.Drawing.Point(12, 84);
            this.labelStatisticType.Name = "labelStatisticType";
            this.labelStatisticType.Size = new System.Drawing.Size(89, 13);
            this.labelStatisticType.TabIndex = 4;
            this.labelStatisticType.Text = "Тип статистики:";
            // 
            // comboBoxStatisticType
            // 
            this.comboBoxStatisticType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStatisticType.FormattingEnabled = true;
            this.comboBoxStatisticType.Location = new System.Drawing.Point(12, 100);
            this.comboBoxStatisticType.Name = "comboBoxStatisticType";
            this.comboBoxStatisticType.Size = new System.Drawing.Size(603, 21);
            this.comboBoxStatisticType.TabIndex = 5;
            // 
            // labelSQL
            // 
            this.labelSQL.AutoSize = true;
            this.labelSQL.Location = new System.Drawing.Point(12, 124);
            this.labelSQL.Name = "labelSQL";
            this.labelSQL.Size = new System.Drawing.Size(85, 13);
            this.labelSQL.TabIndex = 6;
            this.labelSQL.Text = "Текст запроса:";
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCheck.Location = new System.Drawing.Point(12, 439);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(117, 23);
            this.buttonCheck.TabIndex = 7;
            this.buttonCheck.Text = "Сформировать";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSQL.Location = new System.Drawing.Point(12, 140);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.Size = new System.Drawing.Size(603, 293);
            this.textBoxSQL.TabIndex = 8;
            // 
            // labelStatisticClass
            // 
            this.labelStatisticClass.AutoSize = true;
            this.labelStatisticClass.Location = new System.Drawing.Point(12, 48);
            this.labelStatisticClass.Name = "labelStatisticClass";
            this.labelStatisticClass.Size = new System.Drawing.Size(101, 13);
            this.labelStatisticClass.TabIndex = 9;
            this.labelStatisticClass.Text = "Класс статистики:";
            // 
            // radioButtonDbfClass
            // 
            this.radioButtonDbfClass.AutoSize = true;
            this.radioButtonDbfClass.Checked = true;
            this.radioButtonDbfClass.Location = new System.Drawing.Point(15, 64);
            this.radioButtonDbfClass.Name = "radioButtonDbfClass";
            this.radioButtonDbfClass.Size = new System.Drawing.Size(145, 17);
            this.radioButtonDbfClass.TabIndex = 10;
            this.radioButtonDbfClass.TabStop = true;
            this.radioButtonDbfClass.Text = "исходные данные (DBF)";
            this.radioButtonDbfClass.UseVisualStyleBackColor = true;
            // 
            // radioButtonFdbClass
            // 
            this.radioButtonFdbClass.AutoSize = true;
            this.radioButtonFdbClass.Location = new System.Drawing.Point(166, 64);
            this.radioButtonFdbClass.Name = "radioButtonFdbClass";
            this.radioButtonFdbClass.Size = new System.Drawing.Size(164, 17);
            this.radioButtonFdbClass.TabIndex = 11;
            this.radioButtonFdbClass.Text = "целевая база данных (FDB)";
            this.radioButtonFdbClass.UseVisualStyleBackColor = true;
            // 
            // FormStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 474);
            this.Controls.Add(this.radioButtonFdbClass);
            this.Controls.Add(this.radioButtonDbfClass);
            this.Controls.Add(this.labelStatisticClass);
            this.Controls.Add(this.textBoxSQL);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.labelSQL);
            this.Controls.Add(this.comboBoxStatisticType);
            this.Controls.Add(this.labelStatisticType);
            this.Controls.Add(this.textBoxStatisticName);
            this.Controls.Add(this.labelStatisticName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Name = "FormStatistic";
            this.Text = "Статистика";
            this.Load += new System.EventHandler(this.FormStatistic_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelStatisticName;
        private System.Windows.Forms.TextBox textBoxStatisticName;
        private System.Windows.Forms.Label labelStatisticType;
        private System.Windows.Forms.ComboBox comboBoxStatisticType;
        private System.Windows.Forms.Label labelSQL;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.TextBox textBoxSQL;
        private System.Windows.Forms.Label labelStatisticClass;
        private System.Windows.Forms.RadioButton radioButtonDbfClass;
        private System.Windows.Forms.RadioButton radioButtonFdbClass;
    }
}