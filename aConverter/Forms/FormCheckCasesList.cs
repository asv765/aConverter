using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormCheckCasesList : Form
    {
        private List<CheckCase> _checkCaseList = new List<CheckCase>();

        public FormCheckCasesList()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var ccc1 = _checkCaseList.First();
            var needAnalize = !ccc1.NeedTest;
            foreach (var ccc2 in _checkCaseList)
            {
                ccc2.Result = CheckCaseStatus.Анализ_не_проводился;
                ccc2.NeedTest = needAnalize;
            }
            SendKeys.Send("{RIGHT}{LEFT}");
            dataGridView1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            foreach (CheckCase ccc in _checkCaseList) ccc.Result = CheckCaseStatus.Анализ_не_проводился;
            int errorsCount = 0;
            foreach (var ccc in _checkCaseList)
            {
                if (!ccc.NeedTest || !ccc.CanTest) continue;
                ccc.Result = CheckCaseStatus.Выполняется_анализ;
                dataGridView1.Refresh();
                bool errorsPresent = ccc.Test();
                if (errorsPresent)
                    errorsCount++;
                else
                    ccc.NeedTest = false;
                dataGridView1.Refresh();
                if (ccc.Result == CheckCaseStatus.Выявлена_терминальная_ошибка)
                {
                    MessageBox.Show(@"Выявлена терминальная ошибка. Анализ прерван. Чтобы продолжить анализ, необходимо исправить выявленные ошибки.",
                        @"Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
            if (errorsCount > 0)
                MessageBox.Show(String.Format("Обнаружено {0} ошибок.", errorsCount), @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(@"Ошибок не обнаружено!",
                    @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormCheckCasesList_Load(object sender, EventArgs e)
        {
            Text = @"Варианты проверки (" + aConverter_RootSettings.FirebirdStringConnection + @")";
            _checkCaseList = CheckCaseFactory.GenerateCheckCases();
            dataGridView1.DataSource = _checkCaseList;
        }

        private void тестироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var ccc = (CheckCase)dataGridView1.CurrentRow.DataBoundItem;
                if (!ccc.CanTest)
                {
                    MessageBox.Show(@"Вариант не может быть вызван для тестирования", @"Внимание!", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                var errorsPresent = ccc.Test();
                if (errorsPresent)
                {
                    var dr = MessageBox.Show(@"Выявлена ошибка. Выполнить анализ?", @"Внимание!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        DataTable dt = ccc.Analize();
                        var fdt = new FormDataTable("Результаты проверки " + ccc.StoredProcName, dt);
                        fdt.ShowDialog();
                    }
                }
                else
                    MessageBox.Show(@"Ошибок не обнаружено!", @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.Rows[e.RowIndex];
            var ccc = (CheckCase)dgvr.DataBoundItem;
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
                var ccc = (CheckCase)dataGridView1.CurrentRow.DataBoundItem;
                ccc.NeedTest = false;
                e.ThrowException = false;
            }
        }

        private void анализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var ccc = (CheckCase) dataGridView1.CurrentRow.DataBoundItem;
                if (!ccc.CanAnalyze)
                {
                    MessageBox.Show(@"Вариант не может быть вызван для анализа", @"Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                DataTable dt = ccc.Analize();
                var fdt = new FormDataTable("Результаты проверки " + ccc.StoredProcName, dt);
                fdt.ShowDialog();
            }
        }

        private void исправлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var ccc = (CheckCase)dataGridView1.CurrentRow.DataBoundItem;
                if (!ccc.CanFix)
                {
                    MessageBox.Show(@"Вариант не предусматривает исправления", @"Внимание!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                ccc.Fix();
                MessageBox.Show(@"Выполнено.", @"Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
