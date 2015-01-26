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
    public partial class FormStatistic : Form
    {
        private Statistic statistic;
        /// <summary>
        /// Редактируемая статистика
        /// </summary>
        public Statistic Statistic
        {
            get { return statistic; }
            set { statistic = value; }
        }

        public FormStatistic(Statistic AStatistic)
        {
            InitializeComponent();
            statistic = AStatistic;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Statistic = getStatistic();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FormStatistic_Load(object sender, EventArgs e)
        {
            string[] sa = Enum.GetNames(typeof(StatisticType));
            foreach (string s in sa)
            {
                comboBoxStatisticType.Items.Add(s.Replace('_', ' '));
            }
            comboBoxStatisticType.SelectedIndex = 0;
            if (statistic != null)
            {
                textBoxStatisticName.Text = statistic.StatisticName;
                if (statistic is DbfStatistic) radioButtonDbfClass.Checked = true;
                if (statistic is FdbStatistic) radioButtonFdbClass.Checked = true;
                comboBoxStatisticType.SelectedIndex = statistic.StatisticTypeId;
                textBoxSQL.Text = statistic.Sql.Replace("\r\n","\n").Replace("\n","\r\n");
            }
        }

        private Statistic getStatistic()
        {
            Statistic s;

            // Собираем данные с полей
            if (radioButtonDbfClass.Checked)
                s = new DbfStatistic();
            else if (radioButtonFdbClass.Checked)
                s = new FdbStatistic();
            else
                throw new Exception("Неизвестный класс статистики.");

            s.StatisticName = textBoxStatisticName.Text;
            s.StatisticTypeId = comboBoxStatisticType.SelectedIndex;
            s.Sql = textBoxSQL.Text;

            return s;
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            Statistic s = getStatistic();
            ShowStatisticClass.ShowStatistic(s);
        }
    }
}
