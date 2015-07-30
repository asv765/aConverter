namespace aConverter.Forms
{
    partial class FormSettings
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelCoverFileBodyPattern = new System.Windows.Forms.Label();
            this.textBoxCoverFileBodyPattern = new System.Windows.Forms.TextBox();
            this.textBoxGeneratedFilePath = new System.Windows.Forms.TextBox();
            this.labelGeneratedFilePath = new System.Windows.Forms.Label();
            this.textBoxSourceDBFFilePath = new System.Windows.Forms.TextBox();
            this.labelSourceDBFFiles = new System.Windows.Forms.Label();
            this.labelConvertPath = new System.Windows.Forms.Label();
            this.textBoxConvertPath = new System.Windows.Forms.TextBox();
            this.buttonEditSettingsCase = new System.Windows.Forms.Button();
            this.buttonAddSettingsCase = new System.Windows.Forms.Button();
            this.buttonDeleteSettingsCase = new System.Windows.Forms.Button();
            this.labelSettingsCase = new System.Windows.Forms.Label();
            this.comboBoxSettingsCase = new System.Windows.Forms.ComboBox();
            this.buttonCheckConnection = new System.Windows.Forms.Button();
            this.textBoxFirebirdConnectionString = new System.Windows.Forms.TextBox();
            this.labelFirebirdConnectionString = new System.Windows.Forms.Label();
            this.labelPatternsPath = new System.Windows.Forms.Label();
            this.textBoxPatternsPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(552, 571);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(633, 571);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelCoverFileBodyPattern
            // 
            this.labelCoverFileBodyPattern.AutoSize = true;
            this.labelCoverFileBodyPattern.Location = new System.Drawing.Point(12, 239);
            this.labelCoverFileBodyPattern.Name = "labelCoverFileBodyPattern";
            this.labelCoverFileBodyPattern.Size = new System.Drawing.Size(334, 13);
            this.labelCoverFileBodyPattern.TabIndex = 44;
            this.labelCoverFileBodyPattern.Text = "Шаблон для содержимого .cs файлов (%s - содержимое класса):";
            // 
            // textBoxCoverFileBodyPattern
            // 
            this.textBoxCoverFileBodyPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCoverFileBodyPattern.Location = new System.Drawing.Point(15, 255);
            this.textBoxCoverFileBodyPattern.Multiline = true;
            this.textBoxCoverFileBodyPattern.Name = "textBoxCoverFileBodyPattern";
            this.textBoxCoverFileBodyPattern.Size = new System.Drawing.Size(693, 273);
            this.textBoxCoverFileBodyPattern.TabIndex = 43;
            // 
            // textBoxGeneratedFilePath
            // 
            this.textBoxGeneratedFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGeneratedFilePath.Location = new System.Drawing.Point(15, 546);
            this.textBoxGeneratedFilePath.Name = "textBoxGeneratedFilePath";
            this.textBoxGeneratedFilePath.Size = new System.Drawing.Size(690, 20);
            this.textBoxGeneratedFilePath.TabIndex = 42;
            // 
            // labelGeneratedFilePath
            // 
            this.labelGeneratedFilePath.AutoSize = true;
            this.labelGeneratedFilePath.Location = new System.Drawing.Point(12, 531);
            this.labelGeneratedFilePath.Name = "labelGeneratedFilePath";
            this.labelGeneratedFilePath.Size = new System.Drawing.Size(172, 13);
            this.labelGeneratedFilePath.TabIndex = 41;
            this.labelGeneratedFilePath.Text = "Путь для генерируемых файлов:";
            // 
            // textBoxSourceDBFFilePath
            // 
            this.textBoxSourceDBFFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceDBFFilePath.Location = new System.Drawing.Point(15, 133);
            this.textBoxSourceDBFFilePath.Name = "textBoxSourceDBFFilePath";
            this.textBoxSourceDBFFilePath.Size = new System.Drawing.Size(693, 20);
            this.textBoxSourceDBFFilePath.TabIndex = 40;
            // 
            // labelSourceDBFFiles
            // 
            this.labelSourceDBFFiles.AutoSize = true;
            this.labelSourceDBFFiles.Location = new System.Drawing.Point(12, 117);
            this.labelSourceDBFFiles.Name = "labelSourceDBFFiles";
            this.labelSourceDBFFiles.Size = new System.Drawing.Size(166, 13);
            this.labelSourceDBFFiles.TabIndex = 39;
            this.labelSourceDBFFiles.Text = "Путь к DBF-файлам заказчика:";
            // 
            // labelConvertPath
            // 
            this.labelConvertPath.AutoSize = true;
            this.labelConvertPath.Location = new System.Drawing.Point(12, 195);
            this.labelConvertPath.Name = "labelConvertPath";
            this.labelConvertPath.Size = new System.Drawing.Size(156, 13);
            this.labelConvertPath.TabIndex = 38;
            this.labelConvertPath.Text = "Путь к модулям импорта (dll):";
            // 
            // textBoxConvertPath
            // 
            this.textBoxConvertPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConvertPath.Location = new System.Drawing.Point(15, 211);
            this.textBoxConvertPath.Name = "textBoxConvertPath";
            this.textBoxConvertPath.Size = new System.Drawing.Size(693, 20);
            this.textBoxConvertPath.TabIndex = 36;
            // 
            // buttonEditSettingsCase
            // 
            this.buttonEditSettingsCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditSettingsCase.Location = new System.Drawing.Point(656, 24);
            this.buttonEditSettingsCase.Name = "buttonEditSettingsCase";
            this.buttonEditSettingsCase.Size = new System.Drawing.Size(52, 21);
            this.buttonEditSettingsCase.TabIndex = 32;
            this.buttonEditSettingsCase.Text = "Edit";
            this.buttonEditSettingsCase.UseVisualStyleBackColor = true;
            this.buttonEditSettingsCase.Click += new System.EventHandler(this.buttonEditSettingsCase_Click);
            // 
            // buttonAddSettingsCase
            // 
            this.buttonAddSettingsCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddSettingsCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddSettingsCase.Location = new System.Drawing.Point(550, 24);
            this.buttonAddSettingsCase.Name = "buttonAddSettingsCase";
            this.buttonAddSettingsCase.Size = new System.Drawing.Size(47, 21);
            this.buttonAddSettingsCase.TabIndex = 33;
            this.buttonAddSettingsCase.Text = "Add";
            this.buttonAddSettingsCase.UseVisualStyleBackColor = true;
            this.buttonAddSettingsCase.Click += new System.EventHandler(this.buttonAddSettingsCase_Click);
            // 
            // buttonDeleteSettingsCase
            // 
            this.buttonDeleteSettingsCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteSettingsCase.Location = new System.Drawing.Point(603, 24);
            this.buttonDeleteSettingsCase.Name = "buttonDeleteSettingsCase";
            this.buttonDeleteSettingsCase.Size = new System.Drawing.Size(47, 21);
            this.buttonDeleteSettingsCase.TabIndex = 34;
            this.buttonDeleteSettingsCase.Text = "Del";
            this.buttonDeleteSettingsCase.UseVisualStyleBackColor = true;
            this.buttonDeleteSettingsCase.Click += new System.EventHandler(this.buttonDeleteSettingsCase_Click);
            // 
            // labelSettingsCase
            // 
            this.labelSettingsCase.AutoSize = true;
            this.labelSettingsCase.Location = new System.Drawing.Point(12, 9);
            this.labelSettingsCase.Name = "labelSettingsCase";
            this.labelSettingsCase.Size = new System.Drawing.Size(108, 13);
            this.labelSettingsCase.TabIndex = 31;
            this.labelSettingsCase.Text = "Вариант настройки:";
            // 
            // comboBoxSettingsCase
            // 
            this.comboBoxSettingsCase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSettingsCase.FormattingEnabled = true;
            this.comboBoxSettingsCase.Location = new System.Drawing.Point(15, 25);
            this.comboBoxSettingsCase.Name = "comboBoxSettingsCase";
            this.comboBoxSettingsCase.Size = new System.Drawing.Size(526, 21);
            this.comboBoxSettingsCase.TabIndex = 30;
            this.comboBoxSettingsCase.SelectedIndexChanged += new System.EventHandler(this.comboBoxSettingsCase_SelectedIndexChanged);
            // 
            // buttonCheckConnection
            // 
            this.buttonCheckConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckConnection.Location = new System.Drawing.Point(15, 91);
            this.buttonCheckConnection.Name = "buttonCheckConnection";
            this.buttonCheckConnection.Size = new System.Drawing.Size(693, 23);
            this.buttonCheckConnection.TabIndex = 27;
            this.buttonCheckConnection.Text = "Проверить соединение";
            this.buttonCheckConnection.UseVisualStyleBackColor = true;
            this.buttonCheckConnection.Click += new System.EventHandler(this.buttonCheckConnection_Click);
            // 
            // textBoxFirebirdConnectionString
            // 
            this.textBoxFirebirdConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFirebirdConnectionString.Location = new System.Drawing.Point(15, 65);
            this.textBoxFirebirdConnectionString.Name = "textBoxFirebirdConnectionString";
            this.textBoxFirebirdConnectionString.Size = new System.Drawing.Size(693, 20);
            this.textBoxFirebirdConnectionString.TabIndex = 26;
            // 
            // labelFirebirdConnectionString
            // 
            this.labelFirebirdConnectionString.AutoSize = true;
            this.labelFirebirdConnectionString.Location = new System.Drawing.Point(12, 49);
            this.labelFirebirdConnectionString.Name = "labelFirebirdConnectionString";
            this.labelFirebirdConnectionString.Size = new System.Drawing.Size(181, 13);
            this.labelFirebirdConnectionString.TabIndex = 25;
            this.labelFirebirdConnectionString.Text = "Строка подключения к БД Firebird:";
            // 
            // labelPatternsPath
            // 
            this.labelPatternsPath.AutoSize = true;
            this.labelPatternsPath.Location = new System.Drawing.Point(12, 156);
            this.labelPatternsPath.Name = "labelPatternsPath";
            this.labelPatternsPath.Size = new System.Drawing.Size(98, 13);
            this.labelPatternsPath.TabIndex = 35;
            this.labelPatternsPath.Text = "Путь к шаблонам:";
            // 
            // textBoxPatternsPath
            // 
            this.textBoxPatternsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatternsPath.Location = new System.Drawing.Point(15, 172);
            this.textBoxPatternsPath.Name = "textBoxPatternsPath";
            this.textBoxPatternsPath.Size = new System.Drawing.Size(693, 20);
            this.textBoxPatternsPath.TabIndex = 37;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 606);
            this.Controls.Add(this.labelCoverFileBodyPattern);
            this.Controls.Add(this.textBoxCoverFileBodyPattern);
            this.Controls.Add(this.textBoxGeneratedFilePath);
            this.Controls.Add(this.labelGeneratedFilePath);
            this.Controls.Add(this.textBoxSourceDBFFilePath);
            this.Controls.Add(this.labelSourceDBFFiles);
            this.Controls.Add(this.labelConvertPath);
            this.Controls.Add(this.textBoxConvertPath);
            this.Controls.Add(this.textBoxPatternsPath);
            this.Controls.Add(this.labelPatternsPath);
            this.Controls.Add(this.buttonEditSettingsCase);
            this.Controls.Add(this.buttonAddSettingsCase);
            this.Controls.Add(this.buttonDeleteSettingsCase);
            this.Controls.Add(this.labelSettingsCase);
            this.Controls.Add(this.comboBoxSettingsCase);
            this.Controls.Add(this.buttonCheckConnection);
            this.Controls.Add(this.textBoxFirebirdConnectionString);
            this.Controls.Add(this.labelFirebirdConnectionString);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormSettings";
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelCoverFileBodyPattern;
        private System.Windows.Forms.TextBox textBoxCoverFileBodyPattern;
        private System.Windows.Forms.TextBox textBoxGeneratedFilePath;
        private System.Windows.Forms.Label labelGeneratedFilePath;
        private System.Windows.Forms.TextBox textBoxSourceDBFFilePath;
        private System.Windows.Forms.Label labelSourceDBFFiles;
        private System.Windows.Forms.Label labelConvertPath;
        private System.Windows.Forms.TextBox textBoxConvertPath;
        private System.Windows.Forms.Button buttonEditSettingsCase;
        private System.Windows.Forms.Button buttonAddSettingsCase;
        private System.Windows.Forms.Button buttonDeleteSettingsCase;
        private System.Windows.Forms.Label labelSettingsCase;
        private System.Windows.Forms.ComboBox comboBoxSettingsCase;
        private System.Windows.Forms.Button buttonCheckConnection;
        private System.Windows.Forms.TextBox textBoxFirebirdConnectionString;
        private System.Windows.Forms.Label labelFirebirdConnectionString;
        private System.Windows.Forms.Label labelPatternsPath;
        private System.Windows.Forms.TextBox textBoxPatternsPath;
    }
}