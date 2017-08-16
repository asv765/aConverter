using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsnReader
{
    public partial class ReadIschForm : Form
    {
        private string _fileName;
        public List<IschRecord> Abonents;

        public ReadIschForm()
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
                var fileInfo = new ISchFileInfo();
                var octet = reader.ReadBytes(8);
                octet.ParseShortDate(out fileInfo.РасчетныйГод, out fileInfo.РасчетныйМесяц);
                fileInfo.КоличествоЛс = octet.ToUInt32();
                Abonents = new List<IschRecord>();

                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var isch = new IschRecord(bytes, fileInfo);
                        Abonents.Add(isch);
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastisch = new IschRecord(bytes, fileInfo);
                Abonents.Add(lastisch);
            }
        }

        public class IschRecord
        {
            public LsKvc LsKvc;
            public ushort РасчетныйГод;
            public ushort РасчетныйМесяц;

            public List<IschCounterKodRaz> ПоказанияПоСчетчикам = new List<IschCounterKodRaz>();
            public List<IschCounterKod> ПотребленияПоСчетчикам = new List<IschCounterKod>();

            public IschRecord(List<byte[]> bytes, ISchFileInfo fileInfo)
            {
                uint Adr1 = 0, Adr2 = 0;
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
                            Adr1 = octet.ToUInt32();
                            break;
                        case 011:
                            Adr2 = octet.ToUInt32();
                            break;
                        case 017:
                            ПоказанияПоСчетчикам.Add(new IschCounterKodRaz(octet.ToUInt32(), octet[1]));
                            break;
                        case 018:
                            ПотребленияПоСчетчикам.Add(new IschCounterKod(octet.ToUInt32(), octet[1]));
                            break;
                    }
                }
                LsKvc = new LsKvc(Adr1, Adr2);
            }
        }

        public class ISchFileInfo
        {
            public ushort РасчетныйГод;
            public ushort РасчетныйМесяц;
            public uint КоличествоЛс;
        }

        public class IschCounterKod : RsnCounterKod
        {
            public byte Вид;

            public IschCounterKod(uint info, byte vid) : base(info, vid)
            {
                Вид = vid;
            }
        }

        public class IschCounterKodRaz : RsnCounterKodRaz
        {
            public byte Вид;

            public IschCounterKodRaz(uint info, byte vid) : base(info, vid)
            {
                Вид = vid;
            }
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            var result = Abonents.Where(a => a.ПотребленияПоСчетчикам.Any()).ToArray();
        }
    }
}
