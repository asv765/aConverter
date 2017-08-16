using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Newtonsoft.Json;

namespace RsnReader
{
    public partial class ReadRsnForm : Form
    {
        public static string Debug;

        private string _filePath;
        private string _directoryPath;

        public ReadRsnForm()
        {
            InitializeComponent();
        }

        private void OpenFile_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _filePath = openFileDialog.FileName;
            FileName_label.Text = $"Выбран файл: {_filePath}";
            Read_button.Enabled = true;
        }

        private void OpenTestFile_Button_Click(object sender, EventArgs e)
        {
            _filePath = @"D:\dk\Sources\rsn01702";
            FileName_label.Text = $"Выбран файл: {_filePath}";
            Read_button.Enabled = true;
        }

        private void Read_button_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            var rsnFile = new RsnFile(_filePath);
            sw.Stop();
            ReadFileTime_Label.Text = sw.Elapsed.ToString();
            ReadFileTime_Label.Refresh();
            ReadResult_Label.Text = "Информация о файле успешно прочитана";
            ReadResult_Label.Visible = true;

            RsnFile.OnProgressChanged += RsnFile_OnProgressChanged;
            sw.Restart();
            RsnAbonent.Abonents = rsnFile.ExtractAbonents();
            sw.Stop();
            ReadTime_Label.Text = sw.Elapsed.ToString();
            ReadResult_Label.Text = "Файл успешно распарсен";
            ReadResult_Label.Visible = true;

            //RsnAbonent.Abonents = abonents.ToArray();
            //abonents.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SaveToXml_button.Enabled = true;
        }

        private void RsnFile_OnProgressChanged(long complited, long total)
        {
            RsnFileCount_label.Text = $"{complited}/{total}";
            RsnFileCount_label.Refresh();
        }

        private void OpenDirectory_Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _directoryPath = new FileInfo(openFileDialog.FileName).DirectoryName;
            DirectoryName_label.Text = $"Выбрана папка: {_directoryPath}";
            ReadAllRsnInFolder_button.Enabled = true;
        }

        private void ReadAllRsnInFolder_button_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(_directoryPath, "rsn*");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int i = 0;
            foreach (var file in files)
            {
                var rsnFile = new RsnFile(file);
                //if (rsnFile.FileYear * 12 + rsnFile.FileMonth < 2014 * 12 + 03) continue;
                Debug = rsnFile.FilePath;
                var abonents = rsnFile.ExtractAbonents();
                RsnFileCount_label.Text = $"{++i}/{files.Length}";
                RsnFileCount_label.Refresh();
            }
            sw.Stop();
            AllRsnFileReadTime_label.Text = sw.Elapsed.ToString();
            RsnFileCount_label.Text = files.Length.ToString();
            ReadAllRsnResult_label.Visible = true;
        }

        public const string RsnAbonentsJsonFileName = @"D:\dk\Projects\aConverter-master\RsnReader\Sources\RsnAbonents.json";
        private void SaveToXml_button_Click(object sender, EventArgs e)
        {
            ushort streetId = UInt16.Parse(Street_textBox.Text);
            var abonetnsOnStreet = RsnAbonent.Abonents.Where(a => a.LsKvc.StreetId == streetId);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            string json = JsonConvert.SerializeObject(abonetnsOnStreet, 
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            File.WriteAllText(RsnAbonentsJsonFileName, json);
            sw.Stop();
            ReadTime_Label.Text = sw.Elapsed.ToString();
        }

        private void ReadFromXML_button_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RsnAbonent.Abonents = ReadFromXML();
            sw.Stop();
            ReadTime_Label.Text = sw.Elapsed.ToString();
        }

        public static RsnAbonent[] ReadFromXML(int? year = null, int? month = null)
        {
            string fileName = RsnAbonentsJsonFileName;
            if (year != null && month != null)
                fileName += $"_{year.ToString().Substring(2, 2)}{month:D2}";
            return JsonConvert.DeserializeObject<RsnAbonent[]>(File.ReadAllText(fileName));
        }

        public static RsnAbonent[] ReadAllAbonents(int? year = null, int? month = null)
        {
            string fileName = RsnFilePath + @"\rsn0";
            if (year != null && month != null)
                fileName += $"{year.ToString().Substring(2, 2)}{month:D2}";
            return new RsnFile(fileName).ExtractAbonents()/*.ToArray()*/;
        }

        public static string RsnFilePath = @"D:\Work\C#\C#Projects\aConverter-Kvc\Sources";
        public static void FillLsDic(out Dictionary<string, string> lsDic, out List<LsNotInFile> lsNotInLastFile, bool withRsn3, params DateTime[] dates)
        {
            var allDates = dates.ToList();
            if (withRsn3) allDates.Insert(0, dates.Max().AddMonths(1));
            bool firstDate = withRsn3;
            lsNotInLastFile = new List<LsNotInFile>();
            var lsArr = new string[0].AsEnumerable();
            foreach(var date in allDates)
            {
                var tempLsArr = GetAllLs(date, firstDate);
                if (firstDate) firstDate = false;
                var notExits = tempLsArr.Except(lsArr).ToArray();
                lsNotInLastFile.Add(new LsNotInFile
                {
                    FileYear = date.Year,
                    FileMonth = date.Month,
                    LsList = notExits
                });
                lsArr = lsArr.Concat(notExits);
            }
            lsNotInLastFile.RemoveAt(0);
            lsDic = new Dictionary<string, string>();
            int i = 0;
            foreach(var ls in lsArr)
            {
                lsDic.Add(ls, (99000000 + ++i).ToString());
            }
        }

        public class LsNotInFile
        {
            public int FileYear;
            public int FileMonth;
            public string[] LsList;
        }

        public static string[] GetAllLs(DateTime date, bool file3 = false)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(RsnFilePath + @"\rsn" + (file3 ? "3" : "0") + RsnHelper.GetShortDate(date)), Encoding.GetEncoding(1251)))
            {          
                var octet = reader.ReadBytes(8);
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                string[] lsArr = new string[octet.ToUInt32()];
                int i = 0;
                uint adr1;
                while (reader.BaseStream.Position < lastPos)
                {
                    octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        adr1 = octet.ToUInt32();
                        octet = reader.ReadBytes(8);
                        lsArr[i] = new LsKvc(adr1, octet.ToUInt32()).Ls;
                        i++;
                    }
                }
                return lsArr;
            }
        }
    }
}
