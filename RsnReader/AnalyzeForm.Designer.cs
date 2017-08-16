namespace RsnReader
{
    partial class AnalyzeForm
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
            this.Resources_button = new System.Windows.Forms.Button();
            this.Owners_button = new System.Windows.Forms.Button();
            this.Services_button = new System.Windows.Forms.Button();
            this.Regims_button = new System.Windows.Forms.Button();
            this.Street_button = new System.Windows.Forms.Button();
            this.street_textBox = new System.Windows.Forms.TextBox();
            this.Fio_button = new System.Windows.Forms.Button();
            this.Tarifs_button = new System.Windows.Forms.Button();
            this.TestTarifAl_button = new System.Windows.Forms.Button();
            this.Test_button = new System.Windows.Forms.Button();
            this.SodJilKom_button = new System.Windows.Forms.Button();
            this.AbonentByLsKvcbutton = new System.Windows.Forms.Button();
            this.LsKvc_textBox = new System.Windows.Forms.TextBox();
            this.NotExistedAbonentsInOldFiles_button = new System.Windows.Forms.Button();
            this.Necentralotopl_button = new System.Windows.Forms.Button();
            this.Peni_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Resources_button
            // 
            this.Resources_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Resources_button.Location = new System.Drawing.Point(12, 12);
            this.Resources_button.Name = "Resources_button";
            this.Resources_button.Size = new System.Drawing.Size(260, 23);
            this.Resources_button.TabIndex = 0;
            this.Resources_button.Text = "Ресурсы (виды)";
            this.Resources_button.UseVisualStyleBackColor = true;
            this.Resources_button.Click += new System.EventHandler(this.Resources_button_Click);
            // 
            // Owners_button
            // 
            this.Owners_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Owners_button.Location = new System.Drawing.Point(12, 41);
            this.Owners_button.Name = "Owners_button";
            this.Owners_button.Size = new System.Drawing.Size(260, 23);
            this.Owners_button.TabIndex = 1;
            this.Owners_button.Text = "Поставщики (хозяева)";
            this.Owners_button.UseVisualStyleBackColor = true;
            this.Owners_button.Click += new System.EventHandler(this.Owners_button_Click);
            // 
            // Services_button
            // 
            this.Services_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Services_button.Location = new System.Drawing.Point(12, 70);
            this.Services_button.Name = "Services_button";
            this.Services_button.Size = new System.Drawing.Size(260, 23);
            this.Services_button.TabIndex = 1;
            this.Services_button.Text = "Услуги (вид + хозяин)";
            this.Services_button.UseVisualStyleBackColor = true;
            this.Services_button.Click += new System.EventHandler(this.Services_button_Click);
            // 
            // Regims_button
            // 
            this.Regims_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Regims_button.Location = new System.Drawing.Point(12, 99);
            this.Regims_button.Name = "Regims_button";
            this.Regims_button.Size = new System.Drawing.Size(260, 23);
            this.Regims_button.TabIndex = 1;
            this.Regims_button.Text = "Режимы (вид + алгоритм)";
            this.Regims_button.UseVisualStyleBackColor = true;
            this.Regims_button.Click += new System.EventHandler(this.Regims_button_Click);
            // 
            // Street_button
            // 
            this.Street_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Street_button.Location = new System.Drawing.Point(12, 372);
            this.Street_button.Name = "Street_button";
            this.Street_button.Size = new System.Drawing.Size(186, 23);
            this.Street_button.TabIndex = 1;
            this.Street_button.Text = "Анализ улицы";
            this.Street_button.UseVisualStyleBackColor = true;
            this.Street_button.Click += new System.EventHandler(this.Street_button_Click);
            // 
            // street_textBox
            // 
            this.street_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.street_textBox.Location = new System.Drawing.Point(204, 374);
            this.street_textBox.Name = "street_textBox";
            this.street_textBox.Size = new System.Drawing.Size(68, 20);
            this.street_textBox.TabIndex = 2;
            // 
            // Fio_button
            // 
            this.Fio_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fio_button.Location = new System.Drawing.Point(12, 401);
            this.Fio_button.Name = "Fio_button";
            this.Fio_button.Size = new System.Drawing.Size(260, 23);
            this.Fio_button.TabIndex = 1;
            this.Fio_button.Text = "Анализ фио";
            this.Fio_button.UseVisualStyleBackColor = true;
            this.Fio_button.Click += new System.EventHandler(this.Fio_button_Click);
            // 
            // Tarifs_button
            // 
            this.Tarifs_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tarifs_button.Location = new System.Drawing.Point(12, 128);
            this.Tarifs_button.Name = "Tarifs_button";
            this.Tarifs_button.Size = new System.Drawing.Size(260, 23);
            this.Tarifs_button.TabIndex = 1;
            this.Tarifs_button.Text = "Тарифы (вид + тариф)";
            this.Tarifs_button.UseVisualStyleBackColor = true;
            this.Tarifs_button.Click += new System.EventHandler(this.Tarifs_button_Click);
            // 
            // TestTarifAl_button
            // 
            this.TestTarifAl_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestTarifAl_button.Location = new System.Drawing.Point(12, 430);
            this.TestTarifAl_button.Name = "TestTarifAl_button";
            this.TestTarifAl_button.Size = new System.Drawing.Size(260, 23);
            this.TestTarifAl_button.TabIndex = 1;
            this.TestTarifAl_button.Text = "Анализ алгоритм + тариф";
            this.TestTarifAl_button.UseVisualStyleBackColor = true;
            this.TestTarifAl_button.Click += new System.EventHandler(this.TestTarifAl_button_Click);
            // 
            // Test_button
            // 
            this.Test_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Test_button.Location = new System.Drawing.Point(12, 459);
            this.Test_button.Name = "Test_button";
            this.Test_button.Size = new System.Drawing.Size(260, 23);
            this.Test_button.TabIndex = 1;
            this.Test_button.Text = "Тест";
            this.Test_button.UseVisualStyleBackColor = true;
            this.Test_button.Click += new System.EventHandler(this.Test_button_Click);
            // 
            // SodJilKom_button
            // 
            this.SodJilKom_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SodJilKom_button.Location = new System.Drawing.Point(12, 177);
            this.SodJilKom_button.Name = "SodJilKom_button";
            this.SodJilKom_button.Size = new System.Drawing.Size(260, 23);
            this.SodJilKom_button.TabIndex = 1;
            this.SodJilKom_button.Text = "Содержание жилья (компоненты)";
            this.SodJilKom_button.UseVisualStyleBackColor = true;
            this.SodJilKom_button.Click += new System.EventHandler(this.SodJilKom_button_Click);
            // 
            // AbonentByLsKvcbutton
            // 
            this.AbonentByLsKvcbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AbonentByLsKvcbutton.Location = new System.Drawing.Point(12, 343);
            this.AbonentByLsKvcbutton.Name = "AbonentByLsKvcbutton";
            this.AbonentByLsKvcbutton.Size = new System.Drawing.Size(122, 23);
            this.AbonentByLsKvcbutton.TabIndex = 1;
            this.AbonentByLsKvcbutton.Text = "Абонент по лс квц";
            this.AbonentByLsKvcbutton.UseVisualStyleBackColor = true;
            this.AbonentByLsKvcbutton.Click += new System.EventHandler(this.AbonentByLsKvcbutton_Click);
            // 
            // LsKvc_textBox
            // 
            this.LsKvc_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LsKvc_textBox.Location = new System.Drawing.Point(140, 345);
            this.LsKvc_textBox.Name = "LsKvc_textBox";
            this.LsKvc_textBox.Size = new System.Drawing.Size(132, 20);
            this.LsKvc_textBox.TabIndex = 2;
            // 
            // NotExistedAbonentsInOldFiles_button
            // 
            this.NotExistedAbonentsInOldFiles_button.Location = new System.Drawing.Point(12, 206);
            this.NotExistedAbonentsInOldFiles_button.Name = "NotExistedAbonentsInOldFiles_button";
            this.NotExistedAbonentsInOldFiles_button.Size = new System.Drawing.Size(260, 23);
            this.NotExistedAbonentsInOldFiles_button.TabIndex = 3;
            this.NotExistedAbonentsInOldFiles_button.Text = "Абоненты, отсутствувающие в старых файлах";
            this.NotExistedAbonentsInOldFiles_button.UseVisualStyleBackColor = true;
            this.NotExistedAbonentsInOldFiles_button.Click += new System.EventHandler(this.NotExistedAbonentsInOldFiles_button_Click);
            // 
            // Necentralotopl_button
            // 
            this.Necentralotopl_button.Location = new System.Drawing.Point(12, 235);
            this.Necentralotopl_button.Name = "Necentralotopl_button";
            this.Necentralotopl_button.Size = new System.Drawing.Size(260, 23);
            this.Necentralotopl_button.TabIndex = 3;
            this.Necentralotopl_button.Text = "Нецентрализованное отопление";
            this.Necentralotopl_button.UseVisualStyleBackColor = true;
            this.Necentralotopl_button.Click += new System.EventHandler(this.Necentralotopl_button_Click);
            // 
            // Peni_button
            // 
            this.Peni_button.Location = new System.Drawing.Point(12, 264);
            this.Peni_button.Name = "Peni_button";
            this.Peni_button.Size = new System.Drawing.Size(260, 23);
            this.Peni_button.TabIndex = 3;
            this.Peni_button.Text = "Пени";
            this.Peni_button.UseVisualStyleBackColor = true;
            this.Peni_button.Click += new System.EventHandler(this.Peni_button_Click);
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 485);
            this.Controls.Add(this.Peni_button);
            this.Controls.Add(this.Necentralotopl_button);
            this.Controls.Add(this.NotExistedAbonentsInOldFiles_button);
            this.Controls.Add(this.LsKvc_textBox);
            this.Controls.Add(this.street_textBox);
            this.Controls.Add(this.Test_button);
            this.Controls.Add(this.TestTarifAl_button);
            this.Controls.Add(this.Fio_button);
            this.Controls.Add(this.AbonentByLsKvcbutton);
            this.Controls.Add(this.Street_button);
            this.Controls.Add(this.SodJilKom_button);
            this.Controls.Add(this.Tarifs_button);
            this.Controls.Add(this.Regims_button);
            this.Controls.Add(this.Services_button);
            this.Controls.Add(this.Owners_button);
            this.Controls.Add(this.Resources_button);
            this.Name = "AnalyzeForm";
            this.Text = "AnalyzeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Resources_button;
        private System.Windows.Forms.Button Owners_button;
        private System.Windows.Forms.Button Services_button;
        private System.Windows.Forms.Button Regims_button;
        private System.Windows.Forms.Button Street_button;
        private System.Windows.Forms.TextBox street_textBox;
        private System.Windows.Forms.Button Fio_button;
        private System.Windows.Forms.Button Tarifs_button;
        private System.Windows.Forms.Button TestTarifAl_button;
        private System.Windows.Forms.Button Test_button;
        private System.Windows.Forms.Button SodJilKom_button;
        private System.Windows.Forms.Button AbonentByLsKvcbutton;
        private System.Windows.Forms.TextBox LsKvc_textBox;
        private System.Windows.Forms.Button NotExistedAbonentsInOldFiles_button;
        private System.Windows.Forms.Button Necentralotopl_button;
        private System.Windows.Forms.Button Peni_button;
    }
}