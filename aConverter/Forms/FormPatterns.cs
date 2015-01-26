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

namespace aConverter.Forms
{
    public partial class FormPatterns : Form
    {
        private const string labelStepsText = "Шаг {0} из {1}";
        private const string labelProcessText = "Обработано {0} из {1}";

        List<Pattern> lp = new List<Pattern>();

        public FormPatterns()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormPatterns_Load(object sender, EventArgs e)
        {
            textBoxPatternsPath.Text = aConverter_RootSettings.PatternsPath;

            string[] fileNames = Directory.GetFiles(textBoxPatternsPath.Text, "*.ptr");
            lp.Clear();
            foreach (string s in fileNames)
            {
                Pattern p = new Pattern(s);
                p.LoadPattern();
                p.onCounted += new Pattern.Counted(p_onCounted);
                p.onIterate += new Pattern.Iterate(p_onIterate);
                lp.Add(p);
            }
            // MessageBox.Show("Загружено " + lp.Count.ToString() + " шаблонов!");
            patternBindingSource.DataSource = lp;

            this.WindowState = FormWindowState.Maximized;
        }

        // Реальное значение ProgressBar. Нужно для оптимизации работы
        private int realProgressBarValue = 0;

        void p_onIterate()
        {
            realProgressBarValue++;
            if ((realProgressBarValue % progressBarProcess.Step) == 0 ||
                realProgressBarValue == progressBarProcess.Maximum)
            {
                progressBarProcess.Value = realProgressBarValue;
                progressBarProcess.Refresh();
                labelProcess.Text = String.Format(labelProcessText, progressBarProcess.Value, progressBarProcess.Maximum);
                labelProcess.Refresh();
                Application.DoEvents();
            }
            if (realProgressBarValue == progressBarProcess.Maximum)
            {
                progressBarSteps.Increment(1);
                progressBarSteps.Refresh();
                labelSteps.Text = String.Format(labelStepsText, progressBarSteps.Value, progressBarSteps.Maximum);
                Application.DoEvents();
            }
        }

        void p_onCounted(int Count)
        {
            progressBarProcess.Value = 0;
            realProgressBarValue = 0;
            progressBarProcess.Maximum = Count;
            progressBarProcess.Step = (int)Math.Round(Convert.ToDecimal(Count / 100)) + 1;
        }

        private void buttonGetScripts_Click(object sender, EventArgs e)
        {
            progressBarSteps.Maximum = lp.Count(p => p.IsActive);
            progressBarSteps.Value = 0;
            labelSteps.Text = String.Format(labelStepsText, progressBarSteps.Value, progressBarSteps.Maximum);
            DateTime startTime = DateTime.Now;
            foreach (Pattern p in lp)
            {
                if (p.IsActive)
                {
                    labelSteps.Text += " (" + p.Description + ")...";
                    p.ExecutePattern();
                }
            }
            string finalMessage = "Генерация скриптов выполнена успешно!\r\n" +
                "Затраченное время: " + (DateTime.Now - startTime).ToString();
            MessageBox.Show(finalMessage, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Pattern p = lp.First();
            bool isActive = true;
            if (p.IsActive) isActive = false;
            foreach (Pattern pp in lp) pp.IsActive = isActive;
            SendKeys.Send("{RIGHT}{LEFT}");
            dataGridView1.Refresh();
        }
    }
}
