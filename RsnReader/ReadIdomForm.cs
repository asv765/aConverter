using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsnReader
{
    public partial class ReadIdomForm : Form
    {
        private string _fileName;
        public List<IdomRecord> Houses;

        public ReadIdomForm()
        {
            InitializeComponent();
        }

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _fileName = openFileDialog.FileName;
            }
        }

        private void button_ReadFile_Click(object sender, EventArgs e)
        {
            using (var reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                var fileInfo = new IdomFileInfo(_fileName);
                var octet = reader.ReadBytes(8);
                Houses = new List<IdomRecord>();

                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                if (bytes[0][0] == 255) return;
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var idom = new IdomRecord(bytes, fileInfo);
                        Houses.Add(idom);
                        bytes = new List<byte[]> {octet};
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastidom = new IdomRecord(bytes, fileInfo);
                Houses.Add(lastidom);
            }

            var test = Houses.Where(h => h.ПотреблениеПоНежилым.Any()).ToArray();
        }
    }

    public class IdomRecord
    {
        public uint Адр1;
        public LsKvc LsKvc;
        public ushort ХозяинДома;
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;

        public List<Record_200> ПараметрыДома = new List<Record_200>();
        public List<Record_201> ОПУ = new List<Record_201>();
        public List<Record_202> ПоказанияОПУ = new List<Record_202>();
        public List<Record_202> ПотреблениеОПУ = new List<Record_202>();
        public List<Record_202_Date> ДатаПотребления = new List<Record_202_Date>();
        public List<Record_203> ПотреблениеПоНежилым = new List<Record_203>();

        public IdomRecord(List<byte[]> bytes, IdomFileInfo fileInfo)
        {
            РасчетныйГод = fileInfo.РасчетныйГод;
            РасчетныйМесяц = fileInfo.РасчетныйМесяц;
            int octecCount = bytes.Count;
            for (int i = 0; i < octecCount; i++)
            {
                var octet = bytes[i];
                var type = octet[0];
                switch (type)
                {
                    case 010:
                        Адр1 = octet.ToUInt32();
                        break;
                    case 011:
                        ХозяинДома = octet.ToUInt16();
                        break;
                    case 200:
                        ПараметрыДома.Add(new Record_200(octet));
                        break;
                    case 201:
                        var type201 = octet[1];
                        switch (type201)
                        {
                            case 20:
                                ОПУ.Add(new Record_201(octet));
                                break;
                        }
                        break;
                    case 202:
                        var ns = octet[3];
                        if (ns == 1) ПоказанияОПУ.Add(new Record_202(octet));
                        else if (ns == 2) ПотреблениеОПУ.Add(new Record_202(octet));
                        else if (ns == 3) ДатаПотребления.Add(new Record_202_Date(octet));
                        else throw new Exception("Неизвестный тип 202 строки");
                        break;
                    case 203:
                        ПотреблениеПоНежилым.Add(new Record_203(octet));
                        break;
                }
            }
            LsKvc = new LsKvc(Адр1);
        }

        public class Record_200 : RsnReader.Record_200
        {
            public DateTime ДатаИзменения;

            public Record_200(byte[] octet) : base(octet)
            {
                ДатаИзменения = octet.GetDateFromStart(RsnHelper.StartDate1999, 2, false);
            }
        }

        public class Record_202_Date
        {
            public byte Вид;
            public byte НомерСчетчика;
            public DateTime ДатаИзменения;

            public Record_202_Date(byte[] octet)
            {
                Вид = octet[1];
                НомерСчетчика = octet[2];
                ДатаИзменения = octet.GetDateFromStart(RsnHelper.StartDate1999, 4, true);
            }
        }

        public class Record_203
        {
            public byte Вид;
            public DateTime ДатаИзменения;
            public decimal Потребление;

            public Record_203(byte[] octet)
            {
                Вид = octet[1];
                ДатаИзменения = octet.GetDateFromStart(RsnHelper.StartDate1999);
                Потребление = octet.ToUInt32()/RsnHelper.GetDigitsCount(Вид, true);
            }
        }
    }

    public class IdomFileInfo
    {
        public string FileName;
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public uint КоличествоЛс;

        public IdomFileInfo(string fileName)
        {
            FileName = fileName;
            using (var reader = new BinaryReader(File.OpenRead(fileName), Encoding.GetEncoding(1251)))
            {
                var octet = reader.ReadBytes(8);
                octet.ParseShortDate(out РасчетныйГод, out РасчетныйМесяц);
                КоличествоЛс = octet.ToUInt32();
            }
        }
    }
}
