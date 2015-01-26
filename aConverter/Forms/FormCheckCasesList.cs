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
    public partial class FormCheckCasesList : Form
    {
        private List<CheckCase> checkCaseList = new List<CheckCase>();

        public FormCheckCasesList()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CheckCase ccc1 = checkCaseList.First();
            bool needAnalize = true;
            if (ccc1.NeedAnalize) needAnalize = false;
            foreach (CheckCase ccc2 in checkCaseList)
            {
                ccc2.Result = CheckCaseStatus.Анализ_не_проводился;
                ccc2.NeedAnalize = needAnalize;
            }
            SendKeys.Send("{RIGHT}{LEFT}");
            dataGridView1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            foreach (CheckCase ccc in checkCaseList) ccc.Result = CheckCaseStatus.Анализ_не_проводился;
            int errorsCount = 0;
            foreach (CheckCase ccc in checkCaseList)
            {
                if (ccc.NeedAnalize)
                {
                    ccc.Result = CheckCaseStatus.Выполняется_анализ;
                    dataGridView1.Refresh();
                    ccc.Analize();
                    errorsCount += ccc.ErrorList.Count;
                    if (ccc.Result == CheckCaseStatus.Ошибок_не_выявлено) ccc.NeedAnalize = false;
                    dataGridView1.Refresh();
                    if (ccc.Result == CheckCaseStatus.Выявлена_терминальная_ошибка)
                    {
                        MessageBox.Show("Выявлена терминальная ошибка. Анализ прерван. Чтобы продолжить анализ, необходимо исправить выявленные ошибки.",
                            "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
            if (errorsCount > 0)
            {
                DialogResult dr = MessageBox.Show(String.Format("Обнаружено {0} ошибок. Сформировать список?", errorsCount),
                    "Результат", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    List<ErrorClass> lec = new List<ErrorClass>();
                    foreach (CheckCase ccc in checkCaseList)
                    {
                        foreach (ErrorClass ec in ccc.ErrorList)
                        {
                            lec.Add(ec);
                        }
                    }
                    FormErrorsList fel = new FormErrorsList(lec);
                    fel.MdiParent = this.MdiParent;
                    fel.Show();
                }
            }
            else
                MessageBox.Show("Ошибок не обнаружено!",
                    "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormCheckCasesList_Load(object sender, EventArgs e)
        {
            toolStripComboBoxCheckCaseClass.Items.Add("Все");
            string[] names = Enum.GetNames(typeof(CheckCaseClass));
            foreach (string s in names)
            {
                toolStripComboBoxCheckCaseClass.Items.Add(s.Replace('_', ' '));
            }
            toolStripComboBoxCheckCaseClass.Text = "Все";
            this.Text = "Варианты проверки (" + aConverter_RootSettings.DestDBFFilePath + ")";
            checkCaseList = CheckCaseFactory.GenerateCheckCases();
            dataGridView1.DataSource = checkCaseList;
        }

        private void проверитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                CheckCase ccc = (CheckCase)dataGridView1.CurrentRow.DataBoundItem;
                ccc.Analize();
                if (ccc.ErrorList.Count > 0)
                {
                    DialogResult dr = MessageBox.Show(String.Format("Обнаружено {0} ошибок. Сформировать список?", ccc.ErrorList.Count),
                        "Результат", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        List<ErrorClass> lec = new List<ErrorClass>();
                        foreach (ErrorClass ec in ccc.ErrorList)
                        {
                            lec.Add(ec);
                        }
                        FormErrorsList fel = new FormErrorsList(lec);
                        fel.MdiParent = this.MdiParent;
                        fel.Show();
                    }
                }
                else
                    MessageBox.Show("Ошибок не обнаружено!",
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.Rows[e.RowIndex];
            CheckCase ccc = (CheckCase)dgvr.DataBoundItem;
            if (ccc != null)
            {
                DataGridViewCellStyle newStyle = dataGridView1.DefaultCellStyle.Clone();
                if (ccc.Result == CheckCaseStatus.Выявлена_ошибка ||
                    ccc.Result == CheckCaseStatus.Выявлена_терминальная_ошибка)
                    newStyle.BackColor = Color.LightCoral;
                else if (ccc.Result == CheckCaseStatus.Ошибок_не_выявлено)
                    newStyle.BackColor = Color.LightGreen;
                dgvr.DefaultCellStyle = newStyle;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // MessageBox.Show(String.Format("{0} {1} {2}", e.FormattedValue, e.ColumnIndex, e.RowIndex));

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Подавляем ошибки преобразования логического типа
            if (e.ColumnIndex == 0)
            {
                CheckCase ccc = (CheckCase)dataGridView1.CurrentRow.DataBoundItem;
                ccc.NeedAnalize = false;
                e.ThrowException = false;
            }
        }

        private void toolStripComboBoxCheckCaseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxCheckCaseClass.SelectedItem.ToString() == "Все")
            {
                checkCaseList = CheckCaseFactory.GenerateCheckCases();
            }
            else
            {
                string enumValue = toolStripComboBoxCheckCaseClass.SelectedItem.ToString().Replace(' ', '_');
                CheckCaseClass ccc = (CheckCaseClass)Enum.Parse(typeof(CheckCaseClass), enumValue);
                checkCaseList = CheckCaseFactory.GenerateCheckCases(ccc);
            }
            dataGridView1.DataSource = checkCaseList;
            dataGridView1.Refresh();
        }
    }
}
