using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RsnReader
{
    partial class ReadRsnForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenFile_button = new System.Windows.Forms.Button();
            this.FileName_label = new System.Windows.Forms.Label();
            this.Read_button = new System.Windows.Forms.Button();
            this.OpenTestFile_Button = new System.Windows.Forms.Button();
            this.ReadResult_Label = new System.Windows.Forms.Label();
            this.ReadTime_Label = new System.Windows.Forms.Label();
            this.ParseAbonents_Label_info = new System.Windows.Forms.Label();
            this.ReadFileTime_Label = new System.Windows.Forms.Label();
            this.ReadFile_Label_info = new System.Windows.Forms.Label();
            this.openFileDialogDirectory = new System.Windows.Forms.OpenFileDialog();
            this.OpenDirectory_Button = new System.Windows.Forms.Button();
            this.DirectoryName_label = new System.Windows.Forms.Label();
            this.ReadAllRsnInFolder_button = new System.Windows.Forms.Button();
            this.ReadAllRsnResult_label = new System.Windows.Forms.Label();
            this.RsnFileCount_label_info = new System.Windows.Forms.Label();
            this.ReadAllRsnTime_label_info = new System.Windows.Forms.Label();
            this.RsnFileCount_label = new System.Windows.Forms.Label();
            this.AllRsnFileReadTime_label = new System.Windows.Forms.Label();
            this.SaveToXml_button = new System.Windows.Forms.Button();
            this.ReadFromXML_button = new System.Windows.Forms.Button();
            this.Street_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // OpenFile_button
            // 
            this.OpenFile_button.Location = new System.Drawing.Point(12, 12);
            this.OpenFile_button.Name = "OpenFile_button";
            this.OpenFile_button.Size = new System.Drawing.Size(92, 23);
            this.OpenFile_button.TabIndex = 0;
            this.OpenFile_button.Text = "Открыть файл";
            this.OpenFile_button.UseVisualStyleBackColor = true;
            this.OpenFile_button.Click += new System.EventHandler(this.OpenFile_button_Click);
            // 
            // FileName_label
            // 
            this.FileName_label.AutoSize = true;
            this.FileName_label.Location = new System.Drawing.Point(110, 17);
            this.FileName_label.Name = "FileName_label";
            this.FileName_label.Size = new System.Drawing.Size(92, 13);
            this.FileName_label.TabIndex = 1;
            this.FileName_label.Text = "Файл не выбран";
            // 
            // Read_button
            // 
            this.Read_button.Enabled = false;
            this.Read_button.Location = new System.Drawing.Point(12, 70);
            this.Read_button.Name = "Read_button";
            this.Read_button.Size = new System.Drawing.Size(75, 23);
            this.Read_button.TabIndex = 2;
            this.Read_button.Text = "Прочитать";
            this.Read_button.UseVisualStyleBackColor = true;
            this.Read_button.Click += new System.EventHandler(this.Read_button_Click);
            // 
            // OpenTestFile_Button
            // 
            this.OpenTestFile_Button.Location = new System.Drawing.Point(12, 41);
            this.OpenTestFile_Button.Name = "OpenTestFile_Button";
            this.OpenTestFile_Button.Size = new System.Drawing.Size(142, 23);
            this.OpenTestFile_Button.TabIndex = 3;
            this.OpenTestFile_Button.Text = "Открыть тестовый файл";
            this.OpenTestFile_Button.UseVisualStyleBackColor = true;
            this.OpenTestFile_Button.Click += new System.EventHandler(this.OpenTestFile_Button_Click);
            // 
            // ReadResult_Label
            // 
            this.ReadResult_Label.AutoSize = true;
            this.ReadResult_Label.Location = new System.Drawing.Point(12, 105);
            this.ReadResult_Label.Name = "ReadResult_Label";
            this.ReadResult_Label.Size = new System.Drawing.Size(53, 13);
            this.ReadResult_Label.TabIndex = 4;
            this.ReadResult_Label.Text = "Успешно";
            this.ReadResult_Label.Visible = false;
            // 
            // ReadTime_Label
            // 
            this.ReadTime_Label.AutoSize = true;
            this.ReadTime_Label.Location = new System.Drawing.Point(167, 154);
            this.ReadTime_Label.Name = "ReadTime_Label";
            this.ReadTime_Label.Size = new System.Drawing.Size(13, 13);
            this.ReadTime_Label.TabIndex = 5;
            this.ReadTime_Label.Text = "0";
            // 
            // ParseAbonents_Label_info
            // 
            this.ParseAbonents_Label_info.AutoSize = true;
            this.ParseAbonents_Label_info.Location = new System.Drawing.Point(12, 154);
            this.ParseAbonents_Label_info.Name = "ParseAbonents_Label_info";
            this.ParseAbonents_Label_info.Size = new System.Drawing.Size(149, 13);
            this.ParseAbonents_Label_info.TabIndex = 6;
            this.ParseAbonents_Label_info.Text = "Время парсинга абонентов:";
            // 
            // ReadFileTime_Label
            // 
            this.ReadFileTime_Label.AutoSize = true;
            this.ReadFileTime_Label.Location = new System.Drawing.Point(209, 128);
            this.ReadFileTime_Label.Name = "ReadFileTime_Label";
            this.ReadFileTime_Label.Size = new System.Drawing.Size(13, 13);
            this.ReadFileTime_Label.TabIndex = 5;
            this.ReadFileTime_Label.Text = "0";
            // 
            // ReadFile_Label_info
            // 
            this.ReadFile_Label_info.AutoSize = true;
            this.ReadFile_Label_info.Location = new System.Drawing.Point(12, 128);
            this.ReadFile_Label_info.Name = "ReadFile_Label_info";
            this.ReadFile_Label_info.Size = new System.Drawing.Size(191, 13);
            this.ReadFile_Label_info.TabIndex = 6;
            this.ReadFile_Label_info.Text = "Время чтения информации о файле:";
            // 
            // openFileDialogDirectory
            // 
            this.openFileDialogDirectory.FileName = "openFileDialog1";
            // 
            // OpenDirectory_Button
            // 
            this.OpenDirectory_Button.Location = new System.Drawing.Point(15, 215);
            this.OpenDirectory_Button.Name = "OpenDirectory_Button";
            this.OpenDirectory_Button.Size = new System.Drawing.Size(101, 23);
            this.OpenDirectory_Button.TabIndex = 7;
            this.OpenDirectory_Button.Text = "Выбрать папку";
            this.OpenDirectory_Button.UseVisualStyleBackColor = true;
            this.OpenDirectory_Button.Click += new System.EventHandler(this.OpenDirectory_Button_Click);
            // 
            // DirectoryName_label
            // 
            this.DirectoryName_label.AutoSize = true;
            this.DirectoryName_label.Location = new System.Drawing.Point(121, 220);
            this.DirectoryName_label.Name = "DirectoryName_label";
            this.DirectoryName_label.Size = new System.Drawing.Size(101, 13);
            this.DirectoryName_label.TabIndex = 8;
            this.DirectoryName_label.Text = "Папка не выбрана";
            // 
            // ReadAllRsnInFolder_button
            // 
            this.ReadAllRsnInFolder_button.Enabled = false;
            this.ReadAllRsnInFolder_button.Location = new System.Drawing.Point(15, 244);
            this.ReadAllRsnInFolder_button.Name = "ReadAllRsnInFolder_button";
            this.ReadAllRsnInFolder_button.Size = new System.Drawing.Size(165, 23);
            this.ReadAllRsnInFolder_button.TabIndex = 9;
            this.ReadAllRsnInFolder_button.Text = "Прочитать все RSN в папке";
            this.ReadAllRsnInFolder_button.UseVisualStyleBackColor = true;
            this.ReadAllRsnInFolder_button.Click += new System.EventHandler(this.ReadAllRsnInFolder_button_Click);
            // 
            // ReadAllRsnResult_label
            // 
            this.ReadAllRsnResult_label.AutoSize = true;
            this.ReadAllRsnResult_label.Location = new System.Drawing.Point(12, 279);
            this.ReadAllRsnResult_label.Name = "ReadAllRsnResult_label";
            this.ReadAllRsnResult_label.Size = new System.Drawing.Size(53, 13);
            this.ReadAllRsnResult_label.TabIndex = 10;
            this.ReadAllRsnResult_label.Text = "Успешно";
            this.ReadAllRsnResult_label.Visible = false;
            // 
            // RsnFileCount_label_info
            // 
            this.RsnFileCount_label_info.AutoSize = true;
            this.RsnFileCount_label_info.Location = new System.Drawing.Point(12, 301);
            this.RsnFileCount_label_info.Name = "RsnFileCount_label_info";
            this.RsnFileCount_label_info.Size = new System.Drawing.Size(110, 13);
            this.RsnFileCount_label_info.TabIndex = 11;
            this.RsnFileCount_label_info.Text = "Количество файлов:";
            // 
            // ReadAllRsnTime_label_info
            // 
            this.ReadAllRsnTime_label_info.AutoSize = true;
            this.ReadAllRsnTime_label_info.Location = new System.Drawing.Point(12, 325);
            this.ReadAllRsnTime_label_info.Name = "ReadAllRsnTime_label_info";
            this.ReadAllRsnTime_label_info.Size = new System.Drawing.Size(147, 13);
            this.ReadAllRsnTime_label_info.TabIndex = 12;
            this.ReadAllRsnTime_label_info.Text = "Время чтения всех файлов:";
            // 
            // RsnFileCount_label
            // 
            this.RsnFileCount_label.AutoSize = true;
            this.RsnFileCount_label.Location = new System.Drawing.Point(124, 301);
            this.RsnFileCount_label.Name = "RsnFileCount_label";
            this.RsnFileCount_label.Size = new System.Drawing.Size(13, 13);
            this.RsnFileCount_label.TabIndex = 13;
            this.RsnFileCount_label.Text = "0";
            // 
            // AllRsnFileReadTime_label
            // 
            this.AllRsnFileReadTime_label.AutoSize = true;
            this.AllRsnFileReadTime_label.Location = new System.Drawing.Point(165, 325);
            this.AllRsnFileReadTime_label.Name = "AllRsnFileReadTime_label";
            this.AllRsnFileReadTime_label.Size = new System.Drawing.Size(13, 13);
            this.AllRsnFileReadTime_label.TabIndex = 14;
            this.AllRsnFileReadTime_label.Text = "0";
            // 
            // SaveToXml_button
            // 
            this.SaveToXml_button.Location = new System.Drawing.Point(289, 41);
            this.SaveToXml_button.Name = "SaveToXml_button";
            this.SaveToXml_button.Size = new System.Drawing.Size(215, 23);
            this.SaveToXml_button.TabIndex = 15;
            this.SaveToXml_button.Text = "Сохранить список абонентов в XML";
            this.SaveToXml_button.UseVisualStyleBackColor = true;
            this.SaveToXml_button.Click += new System.EventHandler(this.SaveToXml_button_Click);
            // 
            // ReadFromXML_button
            // 
            this.ReadFromXML_button.Location = new System.Drawing.Point(289, 70);
            this.ReadFromXML_button.Name = "ReadFromXML_button";
            this.ReadFromXML_button.Size = new System.Drawing.Size(215, 23);
            this.ReadFromXML_button.TabIndex = 15;
            this.ReadFromXML_button.Text = "Прочитать список абонентов из XML";
            this.ReadFromXML_button.UseVisualStyleBackColor = true;
            this.ReadFromXML_button.Click += new System.EventHandler(this.ReadFromXML_button_Click);
            // 
            // Street_textBox
            // 
            this.Street_textBox.Location = new System.Drawing.Point(510, 43);
            this.Street_textBox.Name = "Street_textBox";
            this.Street_textBox.Size = new System.Drawing.Size(100, 20);
            this.Street_textBox.TabIndex = 16;
            this.Street_textBox.Text = "Улица";
            // 
            // ReadRsnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 362);
            this.Controls.Add(this.Street_textBox);
            this.Controls.Add(this.ReadFromXML_button);
            this.Controls.Add(this.SaveToXml_button);
            this.Controls.Add(this.AllRsnFileReadTime_label);
            this.Controls.Add(this.RsnFileCount_label);
            this.Controls.Add(this.ReadAllRsnTime_label_info);
            this.Controls.Add(this.RsnFileCount_label_info);
            this.Controls.Add(this.ReadAllRsnResult_label);
            this.Controls.Add(this.ReadAllRsnInFolder_button);
            this.Controls.Add(this.DirectoryName_label);
            this.Controls.Add(this.OpenDirectory_Button);
            this.Controls.Add(this.ReadFile_Label_info);
            this.Controls.Add(this.ReadFileTime_Label);
            this.Controls.Add(this.ParseAbonents_Label_info);
            this.Controls.Add(this.ReadTime_Label);
            this.Controls.Add(this.ReadResult_Label);
            this.Controls.Add(this.OpenTestFile_Button);
            this.Controls.Add(this.Read_button);
            this.Controls.Add(this.FileName_label);
            this.Controls.Add(this.OpenFile_button);
            this.Name = "ReadRsnForm";
            this.Text = "Чтение RSN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button OpenFile_button;
        private Label FileName_label;
        private Button Read_button;
        private Button OpenTestFile_Button;
        private Label ReadResult_Label;
        private Label ReadTime_Label;
        private Label ParseAbonents_Label_info;
        private Label ReadFileTime_Label;
        private Label ReadFile_Label_info;
        private OpenFileDialog openFileDialogDirectory;
        private Button OpenDirectory_Button;
        private Label DirectoryName_label;
        private Button ReadAllRsnInFolder_button;
        private Label ReadAllRsnResult_label;
        private Label RsnFileCount_label_info;
        private Label ReadAllRsnTime_label_info;
        private Label RsnFileCount_label;
        private Label AllRsnFileReadTime_label;
        private Button SaveToXml_button;
        private Button ReadFromXML_button;
        private TextBox Street_textBox;
    }
}

