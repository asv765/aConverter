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
using System.Reflection;
using System.Threading;

namespace aConverter.Forms
{
    public partial class FormConvert : Form
    {
        private const string labelStepsText = "Шаг {0} из {1}:";
        private const string labelProcessText = "Обработано {0} из {1}:";

        List<ConvertCase> ListConvertCase = new List<ConvertCase>();

        public FormConvert()
        {
            InitializeComponent();
        }

        private void FormConvert_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(aConverter_RootSettings.ConvertPath))
            {
                DialogResult dr = MessageBox.Show(String.Format("Не найден каталог c dll-библиотеками шагов конвертации {0}. Создать каталог?",
                    aConverter_RootSettings.ConvertPath), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                    Directory.CreateDirectory(aConverter_RootSettings.ConvertPath);
                else
                {
                    textBoxSourceDBFFilePath.BackColor = Color.WhiteSmoke;
                    textBoxConvertPath.ForeColor = Color.Red;
                }
            }
            textBoxConvertPath.Text = aConverter_RootSettings.ConvertPath;

            if (!Directory.Exists(aConverter_RootSettings.SourceDBFFilePath))
            {
                DialogResult dr = MessageBox.Show(String.Format("Не найден каталог с исходными файлами для конвертации {0}. Создать каталог?",
                    aConverter_RootSettings.SourceDBFFilePath), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                    Directory.CreateDirectory(aConverter_RootSettings.SourceDBFFilePath);
                else
                {
                    textBoxSourceDBFFilePath.BackColor = Color.WhiteSmoke;
                    textBoxSourceDBFFilePath.ForeColor = Color.Red;
                }
            }
            textBoxSourceDBFFilePath.Text = aConverter_RootSettings.SourceDBFFilePath;

            if (!Directory.Exists(aConverter_RootSettings.DestDBFFilePath))
            {
                DialogResult dr = MessageBox.Show(String.Format("Не найден каталог для размещения результатов конвертации {0}. Создать каталог?",
                    aConverter_RootSettings.DestDBFFilePath), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                    Directory.CreateDirectory(aConverter_RootSettings.DestDBFFilePath);
                else
                {
                    textBoxSourceDBFFilePath.BackColor = Color.WhiteSmoke;
                 
                    textBoxDestDBFFilePath.ForeColor = Color.Red;
                }
            }
            textBoxDestDBFFilePath.Text = aConverter_RootSettings.DestDBFFilePath;

            labelSteps.Text = String.Format(labelStepsText, 0, 0);
            labelProcess.Text = String.Format(labelProcessText, 0, 0);

            #region Заполняем список шагов конвертации
            string[] fa = Directory.GetFiles(aConverter_RootSettings.ConvertPath, "*.dll");
            foreach (string s in fa)
            {
                //try
                //{
                    Assembly extAssemblyFile = Assembly.LoadFrom(s);
                    foreach (Type t in extAssemblyFile.GetTypes())
                    {
                        if (t.IsClass)
                        {
                            if (t.IsSubclassOf(typeof (ConvertCase)))
                            {
                                ConvertCase cc = (ConvertCase) extAssemblyFile.CreateInstance(t.FullName);
                                if (cc.Visible)
                                {
                                    cc.onStepStart += new ConvertCase.StepStartHandler(cc_onCountMaximumSteps);
                                    cc.onIterate += new ConvertCase.IterateHandler(cc_onIterate);
                                    cc.onSetStepsCount += new ConvertCase.SetStepsCountHandler(cc_onStepsMaximum);
                                    cc.ErrorOpenFileEvent += new ConvertCase.ErrorOpenFile(cc_ErrorOpenFileEvent);
                                    cc.onStepFinish += cc_onStepFinish;
                                    ListConvertCase.Add(cc);
                                }
                            }
                        }
                    }
                //}
                //catch
                //{
                //    // Подавляем возможные ошибки загрузки
                //}
            }
            ListConvertCase.Sort(
                delegate(ConvertCase c1, ConvertCase c2)   
                { return c1.Position.CompareTo(c2.Position); });
            bindingSourceConvertCase.DataSource = ListConvertCase;
            #endregion

            this.WindowState = FormWindowState.Maximized;
        }

        bool cc_ErrorOpenFileEvent(object sender, Exception errorMessage)
        {
            DialogResult dr = MessageBox.Show(errorMessage.Message + "\r\nПовторить?", "Ошибка выполнения запроса", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            return dr == System.Windows.Forms.DialogResult.Yes;
        }

        /// <summary>
        /// Текущий шаг варианта конвертации
        /// </summary>
        private int currentStep = 1;
        /// <summary>
        /// Максимальное количество шагов в конвертации
        /// </summary>
        private int maximumStep = 1;

        void cc_onStepsMaximum(int MaximumSteps)
        {
            maximumStep = MaximumSteps;
            currentStep = 1;
            labelSteps.Text = String.Format(labelStepsText, currentStep, maximumStep);
            // labelSteps.Refresh();
            progressBarSteps.Maximum = maximumStep;
            progressBarSteps.Value = 0;
            Application.DoEvents();
        }

        // Реальное значение ProgressBar. Нужно для оптимизации работы
        private int realProgressBarValue = 0;

        void cc_onIterate()
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
            //if (realProgressBarValue == progressBarProcess.Maximum)
            //{
            //    progressBarSteps.Increment(1);
            //    progressBarSteps.Refresh();
            //}
        }

        void cc_onStepFinish()
        {
            progressBarSteps.Increment(1);
            // progressBarSteps.Refresh();
            Application.DoEvents();
            // Thread.Sleep(5000);
        }

        void cc_onCountMaximumSteps(int MaximumSteps)
        {
            progressBarProcess.Maximum = MaximumSteps;
            progressBarProcess.Step = (int)Math.Round(Convert.ToDecimal(MaximumSteps / 100)) + 1;
            progressBarProcess.Value = 0;
            // progressBarProcess.Refresh();
            realProgressBarValue = 0;
            labelProcess.Text = String.Format(labelProcessText, progressBarProcess.Value, progressBarProcess.Maximum);
            // labelProcess.Refresh();
            labelSteps.Text = String.Format(labelStepsText, currentStep++, maximumStep);
            // labelSteps.Refresh();
            Application.DoEvents();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridViewConvertCase_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgvr = dataGridViewConvertCase.Rows[e.RowIndex];
            ConvertCase cc = (ConvertCase)dgvr.DataBoundItem;
            if (cc != null)
            {
                DataGridViewCellStyle newStyle = dataGridViewConvertCase.DefaultCellStyle.Clone();
                if (cc.IsInitial)
                    newStyle.BackColor = Color.LightGray;
                else if (cc.Result == ConvertCaseStatus.Шаг_выполнен_успешно)
                    newStyle.BackColor = Color.LightGreen;
                else if (cc.Result == ConvertCaseStatus.Ошибка_при_выполнении_шага)
                    newStyle.BackColor = Color.LightCoral;
                else if (cc.Result == ConvertCaseStatus.Шаг_не_выполнен)
                    newStyle.BackColor = Color.White;
                dgvr.DefaultCellStyle = newStyle;
            }
        }

        private void dataGridViewConvertCase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridViewConvertCase.Rows[e.RowIndex];
            ConvertCase cc = (ConvertCase)dgvr.DataBoundItem;
            if (cc.Result == ConvertCaseStatus.Шаг_не_выполнен)
                MessageBox.Show("Шаг не выполнен!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (cc.Result == ConvertCaseStatus.Шаг_выполнен_успешно)
                MessageBox.Show("Шаг выполнен успешно!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (cc.Result == ConvertCaseStatus.Ошибка_при_выполнении_шага)
                MessageBox.Show("Ошибка при выполнении шага:\r\n"+cc.ErrorMessage, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ConvertCase ccc1 = ListConvertCase.First();
            bool isChecked = true;
            if (ccc1.IsChecked) isChecked = false;
            foreach (ConvertCase ccc2 in ListConvertCase)
            {
                ccc2.Result = ConvertCaseStatus.Шаг_не_выполнен;
                ccc2.IsChecked = isChecked;
            }
            SendKeys.Send("{RIGHT}{LEFT}");
            dataGridViewConvertCase.Refresh();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            progressBarProcess.Step = 1000;

            int count = ListConvertCase.Count(p => p.IsChecked);
            progressBarSteps.Maximum = 1;
            // int counter = 1;

            foreach (ConvertCase cc in ListConvertCase) cc.Result = ConvertCaseStatus.Шаг_не_выполнен;
                
            dataGridViewConvertCase.Refresh();

            DateTime startTime = DateTime.Now;
            // bool isfirst = true;
            int volume = Convert.ToInt32(numericUpDown1.Value);
            foreach (ConvertCase cc in ListConvertCase)
            {
                if (cc.IsChecked)
                {
                    labelSteps.Text = String.Format(labelStepsText, 1, 1);
                    labelSteps.Refresh();
                    try
                    {
                        cc.SetStepsCount(1);
                        cc.InitializeManager(aConverter_RootSettings.SourceDBFFilePath, aConverter_RootSettings.DestDBFFilePath);//////////
                        if (checkBoxToFile.Checked)
                        {

                            cc.tmdest.FileToSave = textBoxToFile.Text;
                            cc.tmdest.SaveToFile = true;
                            cc.tmdest.OpenFileToWrite(volume++);
                        }
                        else
                            cc.tmdest.SaveToFile = false;

                        cc.DoConvert();

                        cc.Dispose();
                        cc.Result = ConvertCaseStatus.Шаг_выполнен_успешно;
                    }
                    catch (Exception ex)
                    {
                        cc.Result = ConvertCaseStatus.Ошибка_при_выполнении_шага;
                        cc.ErrorMessage = ex.ToString();
                    }
                    dataGridViewConvertCase.Refresh();
                }
            }
            string finalMessage = "Конвертация выполнена успешно!\r\n" +
                "Затраченное время: " + (DateTime.Now - startTime).ToString();
            MessageBox.Show(finalMessage, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxToFile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxToFile.Checked)
            {
                textBoxToFile.ReadOnly = false;
                numericUpDown1.ReadOnly = false;
            }
            else
            {
                textBoxToFile.ReadOnly = true;
                numericUpDown1.ReadOnly = true;
            }
        }
    }
}
