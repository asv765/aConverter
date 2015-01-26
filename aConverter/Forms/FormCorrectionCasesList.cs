using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormCorrectionCasesList : Form
    {
        private List<CorrectionCase> correctionCaseList;

        public FormCorrectionCasesList(List<CorrectionCase> ACorrectionCaseList)
        {
            InitializeComponent();
            correctionCaseList = ACorrectionCaseList;
        }

        private void FormCorrectionCasesList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = correctionCaseList;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int successfull = 0;
            int error = 0;
            int counter = 0;
            toolStripLabelEnd.Text = correctionCaseList.Count.ToString();
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = correctionCaseList.Count;
            toolStripLabelStart.Text = counter.ToString();
            foreach (CorrectionCase ccc in correctionCaseList)
            {
                ccc.Correct();
                if (ccc.Result == CorrectionCaseStatus.Корректировка_выполнена_успешно)
                    successfull++;
                else if (ccc.Result == CorrectionCaseStatus.Ошибка_при_выполнении_корректировки)
                    error++;
                if (++counter % 100 == 0)
                {
                    toolStripLabelStart.Text = counter.ToString();
                    toolStripProgressBar.Value = counter;
                    dataGridView1.Refresh();
                    Application.DoEvents();
                }
            }
            if (successfull > 0 && error == 0)
                MessageBox.Show(String.Format("Успешно выполнено {0} вариантов исправления", successfull),
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (successfull > 0 && error > 0)
                MessageBox.Show(String.Format("Выполнено {0} вариантов исправления. {1} вариантов исправления выполнены с ошибкой!", successfull, error),
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (successfull ==0  && error > 0)
                MessageBox.Show(String.Format("{0} вариантов исправления выполнены с ошибкой!", error),
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dataGridView1.Refresh();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.Rows[e.RowIndex];
            CorrectionCase cc = (CorrectionCase)dgvr.DataBoundItem;
            if (cc != null)
            {
                DataGridViewCellStyle newStyle = dataGridView1.DefaultCellStyle.Clone();
                if (cc.Result == CorrectionCaseStatus.Корректировка_выполнена_успешно)
                    newStyle.BackColor = Color.LightGreen;
                else if (cc.Result == CorrectionCaseStatus.Ошибка_при_выполнении_корректировки)
                    newStyle.BackColor = Color.LightCoral;
                else if (cc.Result == CorrectionCaseStatus.Корректировка_не_производилась)
                    newStyle.BackColor = Color.White;
                dgvr.DefaultCellStyle = newStyle;
            }
        }
    }
}
