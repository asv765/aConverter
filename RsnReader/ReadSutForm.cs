using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RsnReader
{
    public partial class ReadSutForm : Form
    {
        private SutRecord[] _suts;

        public ReadSutForm()
        {
            InitializeComponent();
        }

        private string _fileName;

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            _fileName = openFileDialog1.FileName;
        }

        private void button_ReadFile_Click(object sender, EventArgs e)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                int totalCount = (int)(reader.BaseStream.Length / 39);
                int j = 1;
                reader.ReadBytes(39);
                var tempSuts = new List<SutRecord>();
                while (j < totalCount)
                {
                    j++;
                    tempSuts.Add(new SutRecord(reader.ReadBytes(39)));
                }
                _suts = tempSuts.ToArray();
            }
            MessageBox.Show("Успешно " + _fileName);
        }

        private void button_PaysByLsKvc_Click(object sender, EventArgs e)
        {
            var sutsByLs = _suts.Where(s => s.LsKvc.Ls == textBox_LsKvc.Text).ToArray();
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            var test = _suts.Where(s => s.БанкПП == 13 && s.ПодразделениеПП == 70).ToArray();
        }
    }
}
