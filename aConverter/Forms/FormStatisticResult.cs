using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverter.Utils;
using System.Drawing.Printing;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormStatisticResult : Form
    {
        DataGridViewPrinter MyDataGridViewPrinter;

        private DataTable dataTable;
        private Dictionary<string, string> fieldRecodeList = new Dictionary<string,string>();

        public FormStatisticResult(string AStatisticName, DataTable ADataTable, List<string> AFieldRecodeList)
        {
            InitializeComponent();
            this.Text = "Статистика [" + AStatisticName + "]";
            dataTable = ADataTable;

            if (AFieldRecodeList == null)
                fieldRecodeList = null;
            else
            {
                foreach (string s in AFieldRecodeList)
                {
                    string[] sa = s.Split(';');
                    fieldRecodeList.Add(sa[0], sa[1]);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormStatisticSet_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = dataTable;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = bindingSource1;

            if (fieldRecodeList != null)
            {
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    string newName = "";
                    if (fieldRecodeList.TryGetValue(dgvc.HeaderText.ToUpper().Trim(), out newName)) dgvc.HeaderText = newName;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = this.Text;
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

            MyDataGridViewPrinter = new DataGridViewPrinter(dataGridView1, MyPrintDocument, true, true, 
                this.Text, new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

        private void toolStripButtonPrintPreview_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;
                MyPrintPreviewDialog.ShowDialog();
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
                MyPrintDocument.Print();
        }

        private void toolStripButtonExportCsv_Click(object sender, EventArgs e)
        {
            saveFileDialogCsv.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialogCsv.FilterIndex = 1;
            saveFileDialogCsv.RestoreDirectory = true;

            if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialogCsv.FileName;
                    Statistic.ExportToCsv(dataTable, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("В процессе сохранения файла возникла ошибка:\r\n" +
                        ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
