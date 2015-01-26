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
    public partial class FormErrorsList : Form
    {
        DataGridViewPrinter MyDataGridViewPrinter;

        private List<ErrorClass> errorList;

        public FormErrorsList(List<ErrorClass> AErrorList)
        {
            InitializeComponent();
            errorList = AErrorList;
        }

        private void FormErrorsList_Load(object sender, EventArgs e)
        {
            errorClassBindingSource.DataSource = errorList;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = errorClassBindingSource;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!allParamsPresent()) editErrorParams();
            if (!allParamsPresent())
            {
                MessageBox.Show("Не все обязательные параметры заданы. Генерация вариантов исправления невозможна.",
                    "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int correctionCaseCount = 0;
            foreach (ErrorClass ec in errorList)
            {
                ec.GenerateCorrectionCases();
                correctionCaseCount += ec.CorrectionCases.Count;
            }
            if (correctionCaseCount > 0)
            {
                DialogResult dr = MessageBox.Show(String.Format("Подготовлено {0} вариантов исправления. Сформировать список?", correctionCaseCount),
                    "Результат", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    List<CorrectionCase> lccc = new List<CorrectionCase>();
                    foreach (ErrorClass ec in errorList)
                    {
                        foreach (CorrectionCase ccc in ec.CorrectionCases) lccc.Add(ccc); 
                    }
                    FormCorrectionCasesList fccc = new FormCorrectionCasesList(lccc);
                    fccc.MdiParent = this.MdiParent;
                    fccc.Show();
                }
            }
            else
                MessageBox.Show("Вариантов исправления сгенерировано не было!",
                    "Результат", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            dataGridView1.Refresh();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            editErrorParams();
        }

        private void editErrorParams()
        {
            // Собираем все параметры со всех ошибок
            List<ErrorParam> lep = new List<ErrorParam>();
            Dictionary<ErrorParam, object> dep = new Dictionary<ErrorParam, object>();
            foreach (ErrorClass ec in errorList)
            {
                lep.AddRange(ec.PossibleErrorParams);
                foreach(KeyValuePair<ErrorParam, object>  kvp in ec.ErrorParams)
                {
                    object value;
                    if (!dep.TryGetValue(kvp.Key, out value)) dep.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (ErrorParam ep in lep.Distinct())
            {
                object value;
                if (!dep.TryGetValue(ep, out value)) dep.Add(ep, 0);
            }

            FormErrorParams fep = new FormErrorParams(dep);
            DialogResult dr = fep.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // По имеющимуся списку параметров устанавливаем параметры в объектах-ошибках
                foreach (ErrorClass ec in errorList)
                {
                    foreach (ErrorParam ep in ec.PossibleErrorParams)
                    {
                        object value;
                        if (ec.ErrorParams.TryGetValue(ep, out value))
                        {
                            ec.ErrorParams[ep] = dep[ep];
                        }
                        else
                            ec.ErrorParams.Add(ep, dep[ep]);
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет, все ли необходимые параметры заданы
        /// </summary>
        /// <returns></returns>
        private bool allParamsPresent()
        {
            foreach (ErrorClass ec in errorList) if (!ec.AllParamsPresent()) return false;
            return true;
        }

        private void детализацияОшибкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ErrorClass ec = (ErrorClass)dataGridView1.CurrentRow.DataBoundItem;
                if (ec.Detail.Count > 0)
                {
                    FormErrorDetail fed = new FormErrorDetail(ec.ErrorName, ec.Detail);
                    fed.ShowDialog();
                }
                else
                    MessageBox.Show("Список детализации пуст",
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ErrorClass ec = (ErrorClass)dataGridView1.CurrentRow.DataBoundItem;
                if (ec.StatisticSets.Count > 0)
                {
                    foreach (Statistic ss in ec.StatisticSets) { ShowStatisticClass.ShowStatistic(ss); }
                }
                else
                    MessageBox.Show("Не найдено связанных статистик",
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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

        private void toolStripButtonPreview_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;
                MyPrintPreviewDialog.ShowDialog();
            }
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
                MyPrintDocument.Print();
        }

        private void MyPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
    }
}
