using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormErrorDetail : Form
    {
        private List<stringWrapper> detail = new List<stringWrapper>();

        public FormErrorDetail(string errorMessage, List<string> ADetail)
        {
            InitializeComponent();
            this.Text = "Детализация ошибки [" + errorMessage + "]";
            foreach (string s in ADetail) detail.Add(new stringWrapper(s));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormErrorDetail_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = detail;
            this.WindowState = FormWindowState.Maximized;
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            saveFileDialogCsv.Filter = @"csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialogCsv.FilterIndex = 1;
            saveFileDialogCsv.RestoreDirectory = true;

            if (saveFileDialogCsv.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialogCsv.FileName;
                    var sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1251));
                    foreach (stringWrapper s in detail)
                    {
                        sw.WriteLine(s.Value + ";");
                    }
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("В процессе сохранения файла возникла ошибка:\r\n" +
                        ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class stringWrapper
    {
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public stringWrapper(string AValue)
        {
            value = AValue;
        }
    }
}
