using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;
using System.IO;
using System.Diagnostics;
using DbfClassLibrary;

namespace aConverter.Forms
{
    public partial class FormSourceAnalize : Form
    {
        private List<FileStatisticBuilder> lfsb = new List<FileStatisticBuilder>();

        public FormSourceAnalize()
        {
            InitializeComponent();
            tableLayoutPanelMain.SetColumnSpan(dataGridViewFile, 2);
        }

        private void reloadFileStatisticList()
        {
            // 1. Получаем список всех файлов в исходном каталоге
            string[] files = Directory.GetFiles(aConverter_RootSettings.SourceDbfFilePath, "*.DBF");
            TableManager tm = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tm.Init();
            tm.ErrorOpenFileEvent += new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
            // 2. Запускаем цикл по файлам
            foreach (string fn in files)
            {
                FileStatisticBuilder fsb = new FileStatisticBuilder(tm) { FileName = fn };
                lfsb.Add(fsb);
            }
            tm.ErrorOpenFileEvent -= new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
        }

        bool tm_ErrorOpenFileEvent(object sender, Exception errorMessage)
        {
            DialogResult dr = MessageBox.Show(errorMessage.Message, "Ошибка выполнения запроса", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            return dr == System.Windows.Forms.DialogResult.Yes;
        }
        /// <summary>
        /// Удаляет из списка записи с файлами, не имеющими записей
        /// </summary>
        private void rebuildFileStatisticList()
        {
            List<FileStatisticBuilder> lfsbNew = new List<FileStatisticBuilder>();
            foreach (FileStatisticBuilder fsb in lfsb) if (!(fsb.HasStatistic && fsb.RowCount == 0)) lfsbNew.Add(fsb);
            lfsbNew.Sort(FileStatisticBuilder.CompareByRowCount);
            lfsbNew.Reverse();
            lfsb = lfsbNew;
        }

        private void FormSourceAnalize_Load(object sender, EventArgs e)
        {
            textBoxPath.Text = aConverter_RootSettings.SourceDbfFilePath;
            reloadFileStatisticList();
            bindingSourceFile.DataSource = lfsb;
            this.WindowState = FormWindowState.Maximized;

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCalcStatistic_Click(object sender, EventArgs e)
        {
            foreach (FileStatisticBuilder fsb in lfsb)
            {
                fsb.CalcStatistic();
            }
            rebuildFileStatisticList();
            bindingSourceFile.DataSource = lfsb;
            dataGridViewFile.Refresh();
            updateFieldList();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgvr = dataGridViewFile.Rows[e.RowIndex];
            FileStatisticBuilder dsb = (FileStatisticBuilder)dgvr.DataBoundItem;
            if (dsb != null)
            {
                DataGridViewCellStyle newStyle = dataGridViewFile.DefaultCellStyle.Clone();
                newStyle.BackColor = Color.White;
                if (dsb.HasStatistic) newStyle.BackColor = Color.LightGreen;
                if (dsb.HasStatistic && dsb.RowCount == 0) newStyle.BackColor = Color.LightGray;
                dgvr.DefaultCellStyle = newStyle;
            }
        }

        private void dataGridViewFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateFieldList();
        }

        private void updateFieldList()
        {
            FileStatisticBuilder fsb = (FileStatisticBuilder)bindingSourceFile.Current;
            if (fsb != null)
            {
                bindingSourceField.DataSource = fsb.FieldList;
                updateDetailList();
            }
        }

        private void buttonDetailStatisticCalc_Click(object sender, EventArgs e)
        {
            FileStatisticBuilder fsb = (FileStatisticBuilder)bindingSourceFile.Current;
            if (fsb != null)
            {
                List<string> ls = fsb.GetHtmlReport();
                string fileName = String.Format("{0}Report.html",fsb.ShortFileName);
                File.WriteAllLines(fileName, ls.ToArray(), Encoding.GetEncoding(1251));
                Process.Start(fileName); 
            }
        }

        private void updateDetailList()
        {
            FieldStatistic fs = (FieldStatistic)bindingSourceField.Current;
            if (fs != null)
            {
                bindingSourceFieldValues.DataSource = fs.CountStatistic;
            }
        }

        private void dataGridViewFields_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateDetailList();
        }

        private void dataGridViewFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStatisticBuilder fsb = (FileStatisticBuilder)bindingSourceFile.Current;
            if (fsb != null)
            {
                FormManualAnalize fma = new FormManualAnalize(fsb.TableManager, fsb.ShortFileName);
                // fma.MdiParent = this.MdiParent;
                fma.Show();
            }
            
        }
    }
}
