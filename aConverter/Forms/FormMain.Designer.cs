namespace aConverter
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.анализToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.конвертацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверкаЦелостностиДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.индексироватьИсходныФайлыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.описаниеТиповойСтруктурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шаблоныToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.генерацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспериментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.анализИсходныхФайловToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.анализToolStripMenuItem,
            this.генерацияToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.выходToolStripMenuItem,
            this.экспериментыToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(791, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // анализToolStripMenuItem
            // 
            this.анализToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.конвертацияToolStripMenuItem,
            this.проверкаЦелостностиДанныхToolStripMenuItem,
            this.индексироватьИсходныФайлыToolStripMenuItem,
            this.описаниеТиповойСтруктурыToolStripMenuItem,
            this.шаблоныToolStripMenuItem1,
            this.статистикиToolStripMenuItem,
            this.анализИсходныхФайловToolStripMenuItem});
            this.анализToolStripMenuItem.Name = "анализToolStripMenuItem";
            this.анализToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.анализToolStripMenuItem.Text = "Операции";
            // 
            // конвертацияToolStripMenuItem
            // 
            this.конвертацияToolStripMenuItem.Name = "конвертацияToolStripMenuItem";
            this.конвертацияToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.конвертацияToolStripMenuItem.Text = "Конвертация";
            this.конвертацияToolStripMenuItem.Click += new System.EventHandler(this.конвертацияToolStripMenuItem_Click);
            // 
            // проверкаЦелостностиДанныхToolStripMenuItem
            // 
            this.проверкаЦелостностиДанныхToolStripMenuItem.Name = "проверкаЦелостностиДанныхToolStripMenuItem";
            this.проверкаЦелостностиДанныхToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.проверкаЦелостностиДанныхToolStripMenuItem.Text = "Проверка целостности данных";
            this.проверкаЦелостностиДанныхToolStripMenuItem.Click += new System.EventHandler(this.проверкаЦелостностиДанныхToolStripMenuItem_Click);
            // 
            // индексироватьИсходныФайлыToolStripMenuItem
            // 
            this.индексироватьИсходныФайлыToolStripMenuItem.Name = "индексироватьИсходныФайлыToolStripMenuItem";
            this.индексироватьИсходныФайлыToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.индексироватьИсходныФайлыToolStripMenuItem.Text = "Индексировать исходные файлы";
            this.индексироватьИсходныФайлыToolStripMenuItem.Click += new System.EventHandler(this.индексироватьИсходныеФайлыToolStripMenuItem_Click);
            // 
            // описаниеТиповойСтруктурыToolStripMenuItem
            // 
            this.описаниеТиповойСтруктурыToolStripMenuItem.Name = "описаниеТиповойСтруктурыToolStripMenuItem";
            this.описаниеТиповойСтруктурыToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.описаниеТиповойСтруктурыToolStripMenuItem.Text = "Описание типовой структуры";
            this.описаниеТиповойСтруктурыToolStripMenuItem.Click += new System.EventHandler(this.описаниеТиповойСтруктурыToolStripMenuItem_Click);
            // 
            // шаблоныToolStripMenuItem1
            // 
            this.шаблоныToolStripMenuItem1.Name = "шаблоныToolStripMenuItem1";
            this.шаблоныToolStripMenuItem1.Size = new System.Drawing.Size(255, 22);
            this.шаблоныToolStripMenuItem1.Text = "Шаблоны";
            this.шаблоныToolStripMenuItem1.Click += new System.EventHandler(this.шаблоныToolStripMenuItem1_Click);
            // 
            // статистикиToolStripMenuItem
            // 
            this.статистикиToolStripMenuItem.Name = "статистикиToolStripMenuItem";
            this.статистикиToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.статистикиToolStripMenuItem.Text = "Статистики";
            this.статистикиToolStripMenuItem.Click += new System.EventHandler(this.статистикиToolStripMenuItem_Click);
            // 
            // генерацияToolStripMenuItem
            // 
            this.генерацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem,
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem,
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem,
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem,
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem,
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem,
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem});
            this.генерацияToolStripMenuItem.Name = "генерацияToolStripMenuItem";
            this.генерацияToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.генерацияToolStripMenuItem.Text = "Генерация";
            // 
            // файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem
            // 
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem.Name = "файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem";
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem.Text = "Файла .cs перечислений для LCHARS.DBF";
            this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem.Click += new System.EventHandler(this.файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem_Click);
            // 
            // файлаcsПеречисленияДляCHARSDBFToolStripMenuItem
            // 
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem.Name = "файлаcsПеречисленияДляCHARSDBFToolStripMenuItem";
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem.Text = "Файла .cs перечисления для CHARS.DBF";
            this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem.Click += new System.EventHandler(this.файлаcsПеречисленияДляCHARSDBFToolStripMenuItem_Click);
            // 
            // файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem
            // 
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem.Name = "файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem";
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem.Text = "Файл .cs перечисления со списком источников платежей";
            this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem.Click += new System.EventHandler(this.файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem_Click);
            // 
            // файлcaПеречисленияСоСпискомУслугToolStripMenuItem
            // 
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem.Name = "файлcaПеречисленияСоСпискомУслугToolStripMenuItem";
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem.Text = "Файл .cs перечисления со списком услуг";
            this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem.Click += new System.EventHandler(this.файлcaПеречисленияСоСпискомУслугToolStripMenuItem_Click);
            // 
            // файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem
            // 
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem.Name = "файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem";
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem.Text = "Файла .cs обертка для работы с DBF-таблицей";
            this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem.Click += new System.EventHandler(this.файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem_Click);
            // 
            // файлcsПеречисленияПараметровГражданToolStripMenuItem
            // 
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem.Name = "файлcsПеречисленияПараметровГражданToolStripMenuItem";
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem.Text = "Файл .cs перечисления параметров граждан";
            this.файлcsПеречисленияПараметровГражданToolStripMenuItem.Click += new System.EventHandler(this.файлcsПеречисленияПараметровГражданToolStripMenuItem_Click);
            // 
            // сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem
            // 
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem.Name = "сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem";
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem.Size = new System.Drawing.Size(393, 22);
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem.Text = "Сгенерировать обертку по одному DBF-файлу";
            this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem.Click += new System.EventHandler(this.сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // экспериментыToolStripMenuItem
            // 
            this.экспериментыToolStripMenuItem.Name = "экспериментыToolStripMenuItem";
            this.экспериментыToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.экспериментыToolStripMenuItem.Text = "Эксперименты";
            this.экспериментыToolStripMenuItem.Click += new System.EventHandler(this.экспериментыToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // анализИсходныхФайловToolStripMenuItem
            // 
            this.анализИсходныхФайловToolStripMenuItem.Name = "анализИсходныхФайловToolStripMenuItem";
            this.анализИсходныхФайловToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.анализИсходныхФайловToolStripMenuItem.Text = "Анализ исходных файлов";
            this.анализИсходныхФайловToolStripMenuItem.Click += new System.EventHandler(this.анализИсходныхФайловToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 448);
            this.Controls.Add(this.menuStripMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.Text = "aConverter";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem анализToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспериментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem генерацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлаcsПеречисленияДляCHARSDBFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлcaПеречисленияСоСпискомУслугToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверкаЦелостностиДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem файлcsПеречисленияПараметровГражданToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem индексироватьИсходныФайлыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem описаниеТиповойСтруктурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шаблоныToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem статистикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem конвертацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem анализИсходныхФайловToolStripMenuItem;
    }
}

