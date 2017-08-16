using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsnReader
{
    public partial class ReadChangesGGMMForm : Form
    {
        private string _fileName;
        public List<ChangeGGMMRecord> Changes;

        public ReadChangesGGMMForm()
        {
            InitializeComponent();
        }

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                _fileName = openFileDialog.FileName;
        }

        private void button_ReadFile_Click(object sender, EventArgs e)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                var fileInfo = new ChangeFileInfo(_fileName);
                int totalCount = (int)(reader.BaseStream.Length / 24);
                int j = 1;
                reader.ReadBytes(24);
                Changes = new List<ChangeGGMMRecord>();
                while (j < totalCount)
                {
                    j++;
                    Changes.Add(new ChangeGGMMRecord(reader.ReadBytes(24), fileInfo));
                }
            }
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            var test = Changes.Where(c => c.LsKvc.CombinedLs == "69401400041027").ToArray();
            //var test = Changes.Where(c => c.ХозяинЛс != 9300).ToArray();
        }
    }

    public class ChangeGGMMRecord
    {
        public LsKvc LsKvc;
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public DateTime FileDate;

        public int ХозяинЛс;
        public int ХозяинИзменения;
        public byte ТипНачисления;
        public byte Графа;
        public byte КодСчетчика;
        public int Значение;

        public ChangeGGMMRecord() { }

        public ChangeGGMMRecord(byte[] bytes, ChangeFileInfo fileInfo)
        {
            РасчетныйГод = fileInfo.РасчетныйГод;
            РасчетныйМесяц = fileInfo.РасчетныйМесяц;
            FileDate = new DateTime(РасчетныйГод, РасчетныйМесяц, 1);
            LsKvc = new LsKvc(bytes.ToUInt32(1), bytes.ToUInt32(5));
            ХозяинЛс = bytes.ToInt32(9);
            ХозяинИзменения = bytes.ToInt32(13);
            ТипНачисления = bytes[17];
            Графа = bytes[18];
            КодСчетчика = bytes[19];
            Значение = bytes.ToInt32(20);
        }
    }

    public class ChangeFileInfo
    {
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public string FileName;

        public ChangeFileInfo() { }

        public ChangeFileInfo(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            РасчетныйГод = (ushort)(ushort.Parse(fileInfo.Name.Substring(0, 2)) + 2000);
            РасчетныйМесяц = ushort.Parse(fileInfo.Name.Substring(2, 2));
            FileName = fileName;
        }
    }
}
