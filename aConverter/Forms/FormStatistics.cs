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
    public partial class FormStatistics : Form
    {
        private List<Statistic> statisticList;

        public FormStatistics(List<Statistic> AStatisticList)
        {
            InitializeComponent();
            statisticList = AStatisticList;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                Statistic ss = (Statistic)bindingSource1.Current;
                ShowStatisticClass.ShowStatistic(ss);
                dataGridView1.Refresh();
            }
        }

        private void FormStatisticSets_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = filteredStatisticList();
            this.WindowState = FormWindowState.Maximized;
            toolStripComboBoxSelectType1.SelectedIndex = 0;
            toolStripComboBoxSelectType2.SelectedIndex = 0;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                Statistic ss = (Statistic)bindingSource1.Current;
                DialogResult dr = MessageBox.Show("Удаляется статистика [" + ss.StatisticName + "]. Вы уверены?",
                    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    statisticList.Remove(ss);
                    aConverter_RootSettings.WriteStatistics(statisticList);
                    bindingSource1.DataSource = filteredStatisticList();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FormStatistic fs = new FormStatistic(null);
            fs.ShowDialog();
            if (fs.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                statisticList.Add(fs.Statistic);
                aConverter_RootSettings.WriteStatistics(statisticList);
                bindingSource1.DataSource = filteredStatisticList();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                Statistic ss = (Statistic)bindingSource1.Current;
                int index = -1;
                for(int i = 0; i < statisticList.Count; i++)
                {
                    if (statisticList[i].Equals(ss)) 
                    {
                        index = i;
                        break;
                    }
                }
                FormStatistic fs = new FormStatistic(ss);
                fs.ShowDialog();
                if (fs.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    statisticList[index] = fs.Statistic;
                    aConverter_RootSettings.WriteStatistics(statisticList);
                    bindingSource1.DataSource = filteredStatisticList();
                }
            }
        }

        private void toolStripComboBoxSelectType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSource1.DataSource = filteredStatisticList();
            dataGridView1.Refresh();
        }

        private List<Statistic> filteredStatisticList()
        {
            IEnumerable<Statistic> ies = statisticList;

            if (toolStripComboBoxSelectType1.SelectedIndex == 1)
            {
                ies = ies.Where(s => s is DbfStatistic);
            }
            else if (toolStripComboBoxSelectType1.SelectedIndex == 2)
            {
                ies = ies.Where(s => s is FdbStatistic);
            }

            if (toolStripComboBoxSelectType2.SelectedIndex == 1)
            {
                ies = ies.Where(s => s.StatisticType == StatisticType.Таблица);
            }
            else if (toolStripComboBoxSelectType2.SelectedIndex == 2)
            {
                ies = ies.Where(s => s.StatisticType == StatisticType.Одиночное_значение);
            }
            else if (toolStripComboBoxSelectType2.SelectedIndex == 3)
            {
                ies = ies.Where(s => s.StatisticType == StatisticType.Не_возвращает_значений);
            }

            return ies.OrderBy(p => p.StatisticName).ToList<Statistic>();
        }

        private void toolStripComboBoxSelectType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSource1.DataSource = filteredStatisticList();
            dataGridView1.Refresh();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            foreach (Statistic s in bindingSource1.List)
            {
                ShowStatisticClass.ShowStatistic(s);
                dataGridView1.Refresh();
            }
            MessageBox.Show("Формирование успешно завершено!", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                Statistic ss = (Statistic)bindingSource1.Current;
                ShowStatisticClass.ShowStatistic(ss);
                dataGridView1.Refresh();
            }
        }
    }
}
