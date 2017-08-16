using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace RsnReader
{
    public partial class ReadCcForm : Form
    {
        public static List<CcAbonent> Abonents;
        private string _fileName;

        public ReadCcForm()
        {
            InitializeComponent();

        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            var ccAbonent = Abonents.FirstOrDefault(a => a.LsKvc.Ls == LsKvc_textBox.Text);
        }

        private void test_button_Click(object sender, EventArgs e)
        {
            var zero = Abonents.SelectMany(c => c.Жители).Where(c => c.НомерЖителя == 0).ToArray();
            return;

            //var egrp = Abonents.SelectMany(c => c.Жители).Where(c => !String.IsNullOrWhiteSpace(c.НомерЕГРП)).ToArray();
            //return;

            //MessageBox.Show(Abonents.Count(a => a.ХозяинЛс != 9300).ToString());
            //return;

            //var test = Abonents.SelectMany(c => c.Жители).Select(c => c.ДоляПлощади).Distinct().ToArray();
                //.Where(d =>
                //{
                //    if (String.IsNullOrWhiteSpace(d)) return false;
                //    var splitted = d.Split('/');
                //    if (splitted.Length > 1)
                //        return splitted[1] == "100";
                //    else return false;
                //}).ToArray();
            //return;

            //var cityzens = Abonents.SelectMany(c => c.Жители).ToArray();
            //var test =
            //    cityzens.Where(
            //        c =>
            //            c.ДатаСнятияСРегистрации.HasValue && c.ДатаСнятияСРегистрации.Value.Year == 2017 &&
            //            c.ДатаСнятияСРегистрации.Value.Month == 05 && c.ДатаСнятияСРегистрации.Value.Day == 01)
            //        .ToArray();
            //var test2 =
            //   cityzens.Where(
            //       c =>
            //           c.ДатаОкончанияВремРегистрации.HasValue && c.ДатаОкончанияВремРегистрации.Value.Year == 2017 &&
            //           c.ДатаОкончанияВремРегистрации.Value.Month == 04 && c.ДатаОкончанияВремРегистрации.Value.Day == 01)
            //       .ToArray();
            //return;

            //var test = Abonents.Where(a => a.Жители.Any(c => c.ДатаСмерти != null)).First(a => RsnAbonent.Abonents.Any(ra => ra.LsKvc.CombinedLs == a.LsKvc.CombinedLs));
            //return;

            //var test = Abonents.Where(a => a.Жители.Any(c => c.ДатаОкончанияВремРегистрации != null)).ToArray();
            //return;

            //var test = Abonents.Where(a => a.Жители.Any(c => c.ДатаОкончанияВыбытия != null)).ToArray();
            //var test2 = Abonents.SelectMany(a => a.Жители).Where(c => c.ДатаОкончанияВыбытия != null).ToArray();
            //return;

            var outCitizens = new Dictionary<string, byte[]>();
            for (int i = 0; i < Abonents.Count; i++)
            {
                var abonent = Abonents[i];
                var outCit = abonent.Жители.Where(a => a.СтатусРегистрации == 3).ToArray();
                if (outCit.Length > 0) outCitizens.Add(abonent.LsKvc.Ls, outCit.Select(c => c.НомерЖителя).ToArray());
            }

            List<object> missingCitizens = new List<object>();

            foreach (var date in new[] { new DateTime(2017, 02, 01), new DateTime(2017, 01, 01) })
            {
                string fileName = @"D:\dk\Projects\aConverter-master\RsnReader\Sources" + @"\cc0" + RsnHelper.GetShortDate(date);
                var ccFile = new CcFileInfo(fileName);

                using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), Encoding.GetEncoding(1251)))
                {
                    reader.ReadBytes(8); // пропуск первой служебной 001
                    var bytes = new List<byte[]>();
                    bytes.Add(reader.ReadBytes(8));
                    if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                    long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                    while (reader.BaseStream.Position < lastPos)
                    {
                        var octet = reader.ReadBytes(8);
                        if (octet[0] == 010)
                        {
                            byte[] cit;
                            if (outCitizens.TryGetValue(new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32()).Ls, out cit))
                            {
                                var abonent = new CcAbonent(bytes, ccFile);
                                foreach (var cc in cit)
                                {
                                    var citizen = abonent.Жители.FirstOrDefault(c => c.НомерЖителя == cc);
                                    if (citizen == null) continue;
                                    if (citizen.СтатусРегистрации != 3)
                                        missingCitizens.Add(new { abonent.LsKvc.Ls, citizen.НомерЖителя, date });
                                }
                            }
                            bytes = new List<byte[]> { octet };
                            continue;
                        }
                        bytes.Add(octet);
                    }
                    //byte[] lastcit;
                    //if (outCitizens.TryGetValue(new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32()).Ls, out cit))
                    //{
                    //    var lastabonent = new CcAbonent(bytes, ccFile);
                    //}
                }
            }
        }

        private void OpenFile_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            _fileName = openFileDialog1.FileName;
        }

        private void openTestFile_button_Click(object sender, EventArgs e)
        {
            _fileName = @"D:\dk\Projects\aConverter-master\RsnReader\Sources\CC31703";
        }

        private void ReadFile_button_Click(object sender, EventArgs e)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                var ccFileInfo = new CcFileInfo(_fileName);
                var octet = reader.ReadBytes(8);
                ccFileInfo.СостояниеМассива = octet[1];
                octet.ParseShortDate(out ccFileInfo.РасчетныйГод, out ccFileInfo.РасчетныйМесяц);
                ccFileInfo.КоличествоЛс = octet.ToUInt32();
                Abonents = new List<CcAbonent>();

                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var abonent = new CcAbonent(bytes, ccFileInfo);
                        //if (abonent.LsKvc.StreetId == 226)
                        Abonents.Add(abonent);
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastabonent = new CcAbonent(bytes, ccFileInfo);
                //if (lastabonent.LsKvc.StreetId == 226)
                Abonents.Add(lastabonent);

                //var aa = Abonents.Where(a => a.Жители.Any(c => c.РодственныеСвязи.Any())).ToArray();

                //var aa = Abonents.Where(a => a.Жители.Count > 70).ToArray();

                //var aa = Abonents.Where(a => a.Жители.Any(c => c.СтатусСобственности == 9)).ToArray();

                //return;
                //var test = Abonents.SelectMany(a => a.Жители).Select(c => c.Гражданство).Distinct().Select(c => new { Гражданство = c, Колво = Abonents.SelectMany(a => a.Жители).Count(cc => cc.Гражданство == c) }).ToArray();
                //var test2 = Abonents.SelectMany(a => a.Жители).Select(c => c.КемВыданДокумент).Distinct().OrderBy(c => c).ToArray();
                //var test3 = Abonents.SelectMany(a => a.Жители).Where(c => c.ДатаОкончанияВыбытия != null).ToArray();
                //var test4 = Abonents.SelectMany(a => a.Жители).Where(c => c.ДатаОкончанияРегистрации != null).ToArray();
                //var test5 = Abonents.SelectMany(a => a.Жители).Where(c => c.СтатусРегистрации == 3).ToArray();
                //var test6 = Abonents.SelectMany(a => a.Жители).Select(c => c.ДоляПлощади).Distinct().OrderBy(c => c).ToArray();
                //var test7 = Abonents.SelectMany(a => a.Жители).Select(c => c.МестоРождения).Where(p => !String.IsNullOrWhiteSpace(p)).Select(p => p.Split(';').Length).Distinct().ToArray();
                //Regex OwnerPartRegex = new Regex($@"(.*[^\d])?(?<num>([^0]|[1-9]\d+))[^\d]*(\/|\\)[^\d]*(?<denum>\d+)([^\d].*)?");
                //Regex IntPartRegex = new Regex($@"(?<num>\d+)%?");
                //List<string> uns = new List<string>();
                //for(int i = 0; i < Abonents.Count; i++)
                //{
                //    var ab = Abonents[i];
                //    for(int j = 0; j < ab.Жители.Count; j++)
                //    {
                //        var cit = ab.Жители[j];
                //        if (String.IsNullOrWhiteSpace(cit.ДоляПлощади)) continue;
                //        string part = cit.ДоляПлощади.Trim();
                //        if (part.ToLower().Contains("сов") || part.ToLower().Contains("общ")) continue;
                //        if (OwnerPartRegex.Match(part).Success) continue;
                //        if (IntPartRegex.Match(part).Success) continue;
                //        decimal divi;
                //        if (Decimal.TryParse(part, out divi) && divi < 1) continue;

                //        uns.Add(part);
                //    }
                //}
                //uns = uns.Distinct().OrderBy(c => c).ToList();
                //var cici = Abonents.SelectMany(a => a.Жители).ToArray();
                //var tt = uns.Select(u => new { Соб = u, Колво = cici.Count(c => c.ДоляПлощади == u) }).ToArray();

                //                File.Create("insertAllOrgPass.sql").Close();
                //                var enc = Encoding.GetEncoding(1251);
                //                for(int i = 0; i < test2.Length; i++)
                //                {
                //                    File.AppendAllText("insertAllOrgPass.sql", $@"INSERT INTO EXTORGSPR(EXTORGCD, EXTORGROLEGISZKHID, BANK, BIK, CANGIVELGOT, CANISSUEPASSP, CHARSIMPORT, DEPARTMENTCD, DIRECTOR, EMAIL, EQUIPMENTMAKE, EQUIPMENTSALE, EXPORTTOPAYSYSTEM, EXTORGNM, HASOWNADRESSCD, INN, ISACCOUNT, ISALTERNATIVEACCOUNT, ISBASEORGANIZATION, ISDEFAULTORG, ISEXTERNALCALC, ISMANAGMENTCOMPANY, ISMILITARYCOMISSION, ISPROVIDER, ISREGISTRATIONAUTHORITY, ISTAX, JURIDICALADDRESS, KORACCOUNT, KPP, LSHETFORMAT, MAINACCOUNTANT, NOTE, OGRN, OKPO, PAYIMPORT, PHONE, POSTADDRESS, RS, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE) 
                //                                VALUES(gen_id(extorgspr_g, 1) ,NULL ,NULL ,NULL ,'0' ,'1' ,'0' ,NULL ,NULL ,NULL ,'0' ,'0' ,NULL ,'{test2[i]}' ,'0' ,NULL ,'0' ,NULL ,'0' ,'0' ,'0' ,NULL ,'0' ,'0' ,NULL ,'0' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,'0' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL  );
                //", enc);
                //                }
                //                return;
            }
        }
    }
}
