using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DbfClassLibrary;

namespace RsnReader
{
    public partial class AnalyzeForm : Form
    {
        private Spr1 _spr1;
        private Spr4 _spr4;

        public AnalyzeForm()
        {
            InitializeComponent();
            _spr1 = new Spr1(2017, 05);
            _spr4 = new Spr4(2017, 05);
        }

        private void Resources_button_Click(object sender, EventArgs e)
        {
            var vids = RsnAbonent.Abonents.SelectMany(a => a.Алгоритмы).Select(a => a.Вид).Distinct();

            var видыУслуг = _spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);

            var result = vids.Select(v => new
            {
                Наименование = видыУслуг.Single(vd => vd.R1 == v).Sr40,
                Код = v
            });

            Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}", v.Наименование, v.Код))));
            MessageBox.Show("Скопировано в буфер обмена");
        }

        private void Owners_button_Click(object sender, EventArgs e)
        {
            var owners = RsnAbonent.Abonents.SelectMany(a => a.Алгоритмы).Select(a => a.ХозяинВида).Distinct();

            var хозяева = _spr1.GetSubSpr(Spr1.SubSpr.Хозяева);
            var продолжениеХозяев = _spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);
            var счетаДляБезналичных = _spr1.GetSubSpr(Spr1.SubSpr.СчетаДляБезналичных);
            var адресаИТелефоныХозяев = _spr1.GetSubSpr(Spr1.SubSpr.АдресаИТелефоныХозяев);

            //File.Delete(@"D:\dk\Data\extorginfo.sql");
            //foreach (var owner in хозяева)
            //{
            //    File.AppendAllLines(@"D:\dk\Data\extorginfo.sql", new[]
            //    {
            //        new OwnerSpr(owner, продолжениеХозяев, счетаДляБезналичных, адресаИТелефоныХозяев).Sql
            //    }, System.Text.Encoding.UTF8);
            //}

            var allOwners = owners.Select(o => new
            {
                Наименование = хозяева.Single(od => od.R1 == o).Sr40 + продолжениеХозяев.SingleOrDefault(od => od.R1 == o)?.Sr40,
                Код = o,
                ИНН = хозяева.Single(od => od.R1 == o).R4

            }).ToArray();

            Clipboard.SetText(String.Join("\r\n", allOwners.Select(v => String.Format("{0}\t{1}\t{2}", v.Наименование, v.Код, v.ИНН))));
            MessageBox.Show("Скопированов в буфер обмена");
        }
        
        private class OwnerSpr
        {
            public int Id;
            public string KPP;
            public string BIK;
            public string INN;
            public string Name;
            public string CheckingAccount;
            public string BIKbn;
            public string CheckingAccountbn;
            public string Prim;
            public string Address;
            public string PhoneNumber;

            public OwnerSpr(Spr owner, Spr[] addOwners, Spr[] cashless, Spr[] addrAndPhones)
            {
                int id = owner.R1;
                var addOwner = addOwners.SingleOrDefault(ao => ao.R1 == id);
                var cashlessAcc = cashless.SingleOrDefault(c => c.R1 == id);
                var addrAndPhone = addrAndPhones.SingleOrDefault(ap => ap.R1 == id);

                Id = id;
                KPP = owner.R2 == 0 ? null : owner.R2.ToString();
                BIK = owner.R3 == 0 ? null : owner.R3.ToString();
                INN = owner.R4 == 0 ? null : owner.R4.ToString();
                Name = (owner.Sr40 + addOwner?.Sr40).Trim();
                if (String.IsNullOrWhiteSpace(Name)) Name = null;
                CheckingAccount = String.IsNullOrWhiteSpace(owner.Sr20) ? null : owner.Sr20.Trim();
                if (cashlessAcc != null)
                {
                    BIKbn = cashlessAcc.R3 == 0 ? null : cashlessAcc.R3.ToString();
                    CheckingAccountbn = String.IsNullOrWhiteSpace(cashlessAcc.Sr20) ? null : cashlessAcc.Sr20.Trim();
                }
                if (addOwner != null && !String.IsNullOrWhiteSpace(addOwner.Sr20))
                    Prim = addOwner.Sr20.Trim();
                if (addrAndPhone != null)
                {
                    Address = String.IsNullOrWhiteSpace(addrAndPhone.Sr40) ? null : addrAndPhone.Sr40.Trim();
                    PhoneNumber = String.IsNullOrWhiteSpace(addrAndPhone.Sr20) ? null : addrAndPhone.Sr20.Trim();
                }
            }

            protected string ConvertFloatingPoint(decimal input)
            {
                return input.ToString(System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.');
            }

            protected string ConvertDateTime(DateTime input)
            {
                return $"'{input.ToString("d.MM.yyyy HH:mm:ss")}'"; ;
            }

            protected string NullableString(string input)
            {
                return input == null ? "NULL" : $"'{input.Replace("'", "''")}'";
            }

            protected string NullableInt(int? input)
            {
                return input == null ? "NULL" : input.ToString();
            }

            protected string NullableDecimal(decimal? input)
            {
                return input == null ? "NULL" : ConvertFloatingPoint(input.Value);
            }

            protected string NullableDateTime(DateTime? input)
            {
                return input == null ? "NULL" : ConvertDateTime(input.Value);
            }

            public string Sql => $@"update or insert into EXTORGSPR (EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, ISACCOUNT, LSHETFORMAT, HASOWNADRESSCD, EQUIPMENTSALE, EQUIPMENTMAKE, ISPROVIDER, JURIDICALADDRESS, POSTADDRESS, PHONE, INN, KPP, RS, DIRECTOR, MAINACCOUNTANT, NOTE, OKPO, BANK, KORACCOUNT, BIK, ISEXTERNALCALC, CANISSUEPASSP, ISBASEORGANIZATION, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE, ISDEFAULTORG, ISMILITARYCOMISSION, ISTAX, ISREGISTRATIONAUTHORITY, DEPARTMENTCD, EMAIL, ISMANAGMENTCOMPANY, OGRN, EXPORTTOPAYSYSTEM, ISALTERNATIVEACCOUNT, EXTORGROLEGISZKHID)
                                    values ({Id},
                                            {NullableString(Name)},
                                            0, 0, 0, 0, null, 0, 0, 0, 1,
                                            {NullableString(Address)},
                                            {NullableString(Address)},
                                            {NullableString(PhoneNumber)},
                                            {NullableString(INN)},
                                            {NullableString(KPP)},
                                            {NullableString(CheckingAccount)},
                                            {NullableString(CheckingAccountbn)},
                                            {NullableString(BIKbn)},
                                            {NullableString(Prim)},
                                            null, null, null,
                                            {NullableString(BIK)},
                                            0, 0, 0,
                                            {NullableString(Address)},
                                            {NullableString(PhoneNumber)},
                                            null, 0, 0, 0, 0, null, null, 0, null, 0, 0, null)
                                    matching(EXTORGCD);";
        }

        private void Services_button_Click(object sender, EventArgs e)
        {
            var vidsOwnres = RsnAbonent.Abonents.SelectMany(a => a.Алгоритмы).Select(a => new { a.Вид, a.ХозяинВида }).Distinct();

            var видыУслуг = _spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var хозяева = _spr1.GetSubSpr(Spr1.SubSpr.Хозяева);
            var продолжениеХозяев = _spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);

            var result = vidsOwnres.Select(vo => new
            {
                КодВида = vo.Вид,
                НаименованиеВида = видыУслуг.Single(vd => vd.R1 == vo.Вид).Sr40,
                НаименованиеХозяина = хозяева.Single(od => od.R1 == vo.ХозяинВида).Sr40 + продолжениеХозяев.SingleOrDefault(od => od.R1 == vo.ХозяинВида)?.Sr40,
                КодХозяина = vo.ХозяинВида
            });

            Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}\t{2}\t{3}", v.КодВида, v.НаименованиеВида, v.НаименованиеХозяина, v.КодХозяина))));
            MessageBox.Show("Скопировано в буфер обмена");
        }

        private void Regims_button_Click(object sender, EventArgs e)
        {
            var vidAlgorithms = RsnAbonent.Abonents.SelectMany(a => a.Алгоритмы).Select(a => new { a.Вид, a.Алгоритм }).Distinct();

            var видыУслуг = _spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);
            var алгоритмы = _spr4.GetSubSpr(Spr4.SubSpr.Алгоритмы);

            var result = vidAlgorithms.Select(va => new
            {
                КодВида = va.Вид,
                НаименованиеВида = видыУслуг.Single(vd => vd.R1 == va.Вид).Sr40,
                НаименованиеАлгоритма = va.Алгоритм == 0 ? "" : алгоритмы.Single(ad => ad.R1 == va.Вид && ad.R2 == va.Алгоритм).Sr40,
                ДополнениеАлгоритма = va.Алгоритм == 0 ? "" : алгоритмы.Single(ad => ad.R1 == va.Вид && ad.R2 == va.Алгоритм).Sr20,
                КодАлгоритма = va.Алгоритм
            });

            Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}\t{2}\t{3}\t{4}", v.КодВида, v.НаименованиеВида, v.НаименованиеАлгоритма, v.ДополнениеАлгоритма, v.КодАлгоритма))));
            MessageBox.Show("Скопировано в буфер обмена");
        }

        private void Street_button_Click(object sender, EventArgs e)
        {
            ushort streetId = UInt16.Parse(street_textBox.Text);
            var abonetnsOnStreet = RsnAbonent.Abonents.Where(a => a.LsKvc.StreetId == streetId);

            var abonentCount = abonetnsOnStreet.Count();
            var vidCount = abonetnsOnStreet.SelectMany(a => a.Алгоритмы).Select(a => a.Вид).Distinct().Count();
            var ownerCount = abonetnsOnStreet.SelectMany(a => a.Алгоритмы).Select(a => a.ХозяинВида).Distinct().Count();
            var algorithmsCount = abonetnsOnStreet.SelectMany(a => a.Алгоритмы).Select(a => new { a.Вид, a.Алгоритм }).Distinct().Count();

            MessageBox.Show(String.Format("Абоненты: {0}\r\nВиды: {1}\r\nХозяева: {2}\r\nАлгоритмы: {3}",
                abonentCount, vidCount, ownerCount, algorithmsCount));
        }

        private void Fio_button_Click(object sender, EventArgs e)
        {
            var fios = RsnAbonent.Abonents.Select(a => a.FIO).ToArray();

            int splittedOn3 = 0, splittedOn2 = 0, splittedOn1 = 0, splittedOn0 = 0, splittedOnMore = 0;
            foreach(var fio in fios)
            {
                if (String.IsNullOrWhiteSpace(fio))
                {
                    splittedOn0++;
                    continue;
                }
                int count = fio.Split(' ').Length;
                if (count == 0) throw new Exception("0?");
                else if (count == 1) splittedOn1++;
                else if (count == 2) splittedOn2++;
                else if (count == 3) splittedOn3++;
                else splittedOnMore++;
            }
            MessageBox.Show(String.Format("Пустое: {0}\r\n1: {1}\r\n2: {2}\r\n3: {3}\r\nБольше 3: {4}"
                , splittedOn0, splittedOn1, splittedOn2, splittedOn3, splittedOnMore));
        }

        private void Tarifs_button_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            var хозяева = _spr1.GetSubSpr(Spr1.SubSpr.Хозяева);
            var продолжениеХозяев = _spr1.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);
            var analyzeResult = new List<TarifAnalyze>();
            List<string> test = new List<string>();
            File.Delete("Tarifs.csv");
            File.AppendAllText("Tarifs.csv", "Лс;Вид;Хозяин;Алгоритм;Тариф;Норма1;Норма2;По Счетчику;Число проживающих;Количество владельцев;Общая площадь;Жилая площадь;Сумма начислений\r\n");
            foreach (var abonent in RsnAbonent.Abonents)
            {
                foreach(var al in abonent.Алгоритмы)
                {
                    var tarif = abonent.Тарифы.SingleOrDefault(t => t.Вид == al.Вид);
                    var nomrs = abonent.Нормативы.Where(n => n.Вид == al.Вид);
                    var norm = nomrs.FirstOrDefault();
                    if (nomrs.Count() > 1) { test.Add(abonent.LsKvc.Ls); }
                    var nach = abonent.НачисленияПоСчетчикам.Where(n => n.Вид == al.Вид).Select(n => n.Сумма)
                        .Concat(abonent.НачисленияПоНормативам.Where(n => n.Вид == al.Вид).Select(n => n.Значение)).Sum();
                    i++;
                    if (norm != null) j++;

                    File.AppendAllText("Tarifs.csv", new TarifAnalyze
                    {
                        Ls = abonent.LsKvc.Ls,
                        VidId = al.Вид,
                        OwnerId = al.ХозяинВида,
                        AlgorithmId = al.Алгоритм,

                        Tarif = (float?)tarif?.Значение,
                        Norm1 = (float?)norm?.Значение1,
                        Norm2 = (float?)norm?.Значение2,
                        //IsCounter = abonent.ТарифыПоСчетчикам.Any(t => t.Вид == al.Вид),

                        LivingPeopleCount = abonent.ЧислоПроживающих,
                        OwnerPeopleCount = abonent.ПризнакиЖителей.Count(c => c.ПризнакНанимателяИлиСобственника == 1),
                        TotalSquare = (float)abonent.ОбщаяПлощадь,
                        LivingSquare = (float)abonent.ЖилаяПлощадь,
                        NachSum = (float)nach
                    }.ToString());
                }
            }
            //Clipboard.SetText(String.Join("\r\n", analyzeResult.Select(a => a.ToString())));
            MessageBox.Show("Скопировано в буфер обмена");

            #region Tarif tests
            //int i = 0;
            //int j = 0;
            //List<string> lsDoubleTarifCounter = new List<string>();
            //foreach (var abonent in RsnAbonent.Abonents)
            //{
            //    if (abonent.ТарифыПоСчетчикам.GroupBy(t => t.Вид).Where(t => t.Count() > 1).Any())
            //    {
            //        int a = 10;
            //        i++;
            //    }
            //    else continue;
            //    var tt = abonent.ТарифыПоСчетчикам.GroupBy(t => t.Вид).Where(t => t.Count() > 1);
            //    foreach (var ttt in tt)
            //    {
            //        if (ttt.Select(t => t.Значение).Distinct().Count() > 1)
            //        {
            //            int a = 10;
            //            j++;
            //            lsDoubleTarifCounter.Add(abonent.LsKvc.Ls);
            //        }
            //    }
            //}
            //Clipboard.SetText(String.Join(" , ", lsDoubleTarifCounter));

            //int i = 0;
            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    if (abonent.Алгоритмы.Any()) continue;
            //    i++;
            //    if (abonent.НачисленияПоНормативам.Any() || abonent.НачисленияПоСчетчикам.Any())
            //    {
            //        int a = 10;
            //    }
            //}

            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    var tC = abonent.ТарифыПоСчетчикам.Select(t => new { t.Вид, t.Значение }).ToArray();
            //    var tN = abonent.ТарифыПоНормативам.Select(t => new { t.Вид, t.Значение }).ToArray();

            //    foreach(var t in tC)
            //    {
            //        // нет таких, у кого было бы два тарифа по одному виду с разным значением
            //        //if (tN.Any(tt => tt.Вид == t.Вид && tt.Значение != t.Значение))
            //        //{
            //        //    int a = 10;
            //        //}
            //    }
            //}

            //foreach (var abonent in RsnAbonent.Abonents)
            //{
            //    var t1 = abonent.ПлощадиНормативы.Where(p => p.Вид == 6).ToArray();
            //    var t2 = abonent.Нормативы.Where(n => n.Вид == 6).ToArray();

            //    if (t1.Any() && !t2.Any())
            //    {
            //        int a = 10;
            //    }
            //    if (t1.Any() && t2.Any() && t2[0].Значение1 != 0 && t1[0].Значение1 != t2[0].Значение1)
            //    {
            //        int a = 10;
            //    }
            //}



            //var vidTarifs = RsnAbonent.Abonents.SelectMany(a => a.Тарифы).Select(t => new { t.Вид, t.Значение }).Distinct();

            //var видыУслуг = _spr1.GetSubSpr(Spr1.SubSpr.ВидыУслуг);

            //var result = vidTarifs.Select(va => new
            //{
            //    КодВида = va.Вид,
            //    НаименованиеВида = видыУслуг.Single(vd => vd.R1 == va.Вид).Sr40,
            //    Тариф = va.Значение
            //});

            //Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}\t{2}", v.КодВида, v.НаименованиеВида, v.Тариф))));


            //var plnorm = RsnAbonent.Abonents.SelectMany(a => a.ПлощадиНормативы).Select(p => new { p.Вид, p.Значение1, p.Значение2 }).Distinct();
            //var result = plnorm.Select(va => new
            //{
            //    КодВида = va.Вид,
            //    НаименованиеВида = видыУслуг.Single(vd => vd.R1 == va.Вид).Sr40,
            //    Значение1 = va.Значение1,
            //    Значение2 = va.Значение2,
            //});
            //Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}\t{2}\t{3}", v.КодВида, v.НаименованиеВида, v.Значение1, v.Значение2))));

            //var norm = RsnAbonent.Abonents.SelectMany(a => a.Нормативы).Select(p => new { p.Вид, p.Значение1, p.Значение2, p.КодПараметра }).Distinct();
            //var result = norm.Select(va => new
            //{
            //    КодВида = va.Вид,
            //    НаименованиеВида = видыУслуг.Single(vd => vd.R1 == va.Вид).Sr40,
            //    Значение1 = va.Значение1,
            //    Значение2 = va.Значение2,
            //    КодПараметра = va.КодПараметра,
            //});
            //Clipboard.SetText(String.Join("\r\n", result.Select(v => String.Format("{0}\t{1}\t{2}\t{3}\t{4}", v.КодВида, v.НаименованиеВида, v.Значение1, v.Значение2, v.КодПараметра))));

            //MessageBox.Show("Скопировано в буфер обмена");
            #endregion
        }

        private class TarifAnalyze
        {
            public string Ls { get; set; }
            public int VidId { get; set; }
            public int OwnerId { get; set; }
            public int AlgorithmId { get; set; }

            public float? Tarif { get; set; }
            public float? Norm1 { get; set; }
            public float? Norm2 { get; set; }
            public bool IsCounter { get; set; }

            public int LivingPeopleCount { get; set; }
            public int OwnerPeopleCount { get; set; }
            public float TotalSquare { get; set; }
            public float LivingSquare { get; set; }
            public float NachSum { get; set; }

            public override string ToString()
            {
                return String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}\r\n"
                    , Ls, VidId, OwnerId, AlgorithmId, Tarif, Norm1, Norm2, IsCounter ? 1 : 0, LivingPeopleCount, OwnerPeopleCount, TotalSquare, LivingSquare, NachSum);
            }
        }

        private class TarifAnalyzeSodJil : TarifAnalyze
        {
            public float? TarifEl { get; set; }
            public float? NachEl { get; set; }
            public float? TarifHv { get; set; }
            public float? NachHv { get; set; }
            public float? TarifGv { get; set; }
            public float? NachGv { get; set; }

            public override string ToString()
            {
                return String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18}"
                    , Ls, VidId, OwnerId, AlgorithmId, Tarif, Norm1, Norm2, IsCounter ? 1 : 0, LivingPeopleCount, OwnerPeopleCount, TotalSquare, LivingSquare, NachSum, TarifEl, NachEl, TarifHv, NachHv, TarifGv, NachGv);
            }
        }

        private void TestTarifAl_button_Click(object sender, EventArgs e)
        {
            var vat = RsnAbonent.Abonents.SelectMany(a =>
                a.Алгоритмы.Select(al => new
                {
                    al.Вид,
                    al.Алгоритм,
                    Тариф = a.Тарифы.SingleOrDefault(t => t.Вид == al.Вид)?.Значение
                }))
                .Distinct()
                .ToArray();

            var test =vat.GroupBy(o => new { o.Вид, o.Алгоритм }).Where(g => g.Count() > 1);
        }

        //private class TestGCounter
        //{
        //    public uint Adr1;
        //    public byte Vid;

        //    public Own[] Owners;

        //    public class Own
        //    {
        //        public ushort? OwnerId;
        //        public byte? Algorithm;
        //    }
        //}

        private class TestGCounter
        {
            //public ushort? OwnerId;
            public byte? Algorithm;
            public byte? Vid;

            public override string ToString()
            {
                return $"Vid: {Vid} Algorithm: {Algorithm}"/*; Owner: {OwnerId};"*/;
            }

            public override bool Equals(object obj)
            {
                var x = this;
                var y = (TestGCounter)obj;
                if (x.Vid != y.Vid) return false; 
                if (x.Algorithm != y.Algorithm) return false;
                //if (x.OwnerId != y.OwnerId) return false;
                return true;
            }

            public override int GetHashCode()
            {
                return Int32.Parse($"{Vid ?? 0:D3}{Algorithm ?? 0:D3}"/*{OwnerId ?? 0:D4}"*/);
            }
        }

        private class MayStafTarifs
        {
            public string Ls;
            public decimal Tarif1;
            public decimal Tarif2;
            public decimal Tarif3;
        }
        private void Test_button_Click(object sender, EventArgs e)
        {
            List<string> notFoundedLs = new List<string>
            {
                "006-999-00-001-0-98",
"009-005-02-001-0-59",
"042-005-00-033-0-07",
"047-006-12-001-0-66",
"047-006-12-004-0-54",
"047-006-12-005-0-50",
"047-006-12-009-0-34",
"047-006-12-020-0-89",
"047-006-12-040-0-09",
"047-006-12-041-0-05",
"047-006-12-048-0-76",
"047-006-12-050-0-68",
"047-006-12-051-0-64",
"047-006-12-053-0-56",
"047-006-12-058-0-36",
"047-006-12-059-0-32",
"047-006-12-061-0-24",
"047-006-12-063-0-16",
"047-006-12-075-0-67",
"047-006-12-077-0-59",
"047-006-12-080-0-47",
"047-006-12-082-0-39",
"047-006-12-085-0-27",
"047-006-12-090-0-07",
"047-006-12-093-0-94",
"047-006-12-094-0-90",
"047-006-12-100-0-66",
"047-006-12-101-0-62",
"047-006-12-105-0-46",
"047-006-12-106-0-42",
"047-006-12-107-0-38",
"047-006-12-108-0-34",
"047-006-12-109-0-30",
"047-006-12-110-0-26",
"047-006-12-111-0-22",
"047-006-12-113-0-14",
"047-006-12-114-0-10",
"047-006-12-115-0-06",
"047-006-12-116-0-02",
"047-006-12-118-0-93",
"047-006-12-119-0-89",
"047-006-12-120-0-85",
"047-006-12-121-0-81",
"047-006-12-125-0-65",
"047-006-12-126-0-61",
"047-006-12-129-0-49",
"047-006-12-130-0-45",
"047-006-12-131-0-41",
"047-006-12-134-0-29",
"047-006-12-135-0-25",
"047-006-12-136-0-21",
"047-006-12-137-0-17",
"047-006-12-140-0-05",
"047-006-12-141-0-01",
"047-006-12-144-0-88",
"047-006-12-145-0-84",
"047-006-12-149-0-68",
"047-006-12-150-0-64",
"047-006-12-155-0-44",
"047-006-12-157-0-36",
"047-006-12-160-0-24",
"047-006-12-165-0-04",
"047-006-12-166-0-99",
"047-006-12-168-0-91",
"047-006-12-169-0-87",
"047-006-12-170-0-83",
"047-006-12-171-0-79",
"047-006-12-172-0-75",
"047-006-12-175-0-63",
"047-006-12-180-0-43",
"047-006-12-185-0-23",
"047-006-12-187-0-15",
"047-006-12-190-0-03",
"047-029-00-040-1-27",
"067-011-01-023-1-58",
"100-037-00-021-2-94",
"112-013-00-001-1-22",
"113-998-00-001-0-48",
"113-999-00-001-0-42",
"155-079-00-002-0-17",
"180-003-01-001-0-99",
"192-024-00-025-0-63",
"212-013-00-032-1-29",
"212-017-01-172-1-34",
"212-017-01-172-3-28",
"258-042-00-003-0-38",
"310-026-00-006-1-76",
"310-026-00-006-2-73",
"331-006-00-004-0-07",
"348-032-00-001-0-20",
"348-032-00-002-0-13",
"376-008-00-001-1-57",
"376-008-00-001-2-54",
"377-003-00-002-1-76",
"377-003-00-002-2-73",
"377-004-00-003-1-66",
"377-004-00-003-2-63",
"377-009-00-002-0-43",
"377-009-00-002-2-65",
"382-011-11-001-0-72",
"391-004-00-002-0-03",
"402-024-00-936-0-30",
"402-024-01-929-0-53",
"402-024-01-950-0-68",
"402-024-04-964-0-96",
"429-057-00-058-1-83",
"443-009-00-968-0-02",
"453-018-00-001-0-83",
"458-134-00-004-0-33",
"475-035-02-010-1-75",
"475-038-01-006-0-58",
"509-014-00-010-0-75",
"516-055-00-001-0-81",
"575-997-00-001-0-87",
"596-999-00-001-0-27",
"605-014-02-001-0-23",
"635-996-00-001-0-69",
"636-022-00-007-1-38",
"640-002-01-042-1-58",
"646-059-00-002-0-34",
"669-014-00-028-6-53",
"675-064-00-043-0-65",
"690-018-00-901-0-05",
"690-018-00-903-0-63",
"690-035-00-005-0-88",
"692-001-00-014-0-07",
"692-999-00-001-0-48",
"695-005-01-054-0-08",
"696-015-00-001-2-31",
"705-008-00-001-0-62",
"730-998-00-001-0-85",
"759-021-00-001-1-98",
"759-066-00-001-1-26",
"830-034-00-001-6-03",
"842-022-02-901-0-30",
"845-024-00-001-0-18",
"904-014-00-001-0-19",
"100-009-00-003-1-23",
"154-019-01-001-0-86",
"155-011-00-002-0-29",
"190-002-02-001-0-30",
"190-002-05-001-0-15",
"200-021-02-004-0-12",
"272-002-01-085-1-56",
"272-002-01-085-2-53",
"408-002-00-002-1-95",
"418-004-00-050-1-15",
"418-006-00-025-1-04",
"418-006-00-027-2-92",
"418-006-00-028-1-03",
"418-006-00-056-1-78",
"418-006-00-060-2-59",
"418-006-00-065-1-42",
"418-006-00-066-1-38",
"418-006-00-069-1-26",
"418-006-00-074-1-06",
"418-006-00-085-1-61",
"418-006-00-098-1-09",
"418-006-00-100-2-97",
"418-006-00-103-2-85",
"418-006-00-110-1-60",
"418-006-00-118-1-28",
"475-042-00-062-1-09",
"475-042-00-476-1-38",
"475-042-00-476-2-35",
"498-065-12-001-0-20",
"498-065-12-002-0-16",
"498-065-12-003-0-12",
"498-065-12-004-0-08",
"498-065-12-005-0-04",
"498-065-12-006-0-99",
"498-065-12-007-0-95",
"498-065-12-008-0-91",
"498-065-12-009-0-87",
"498-065-12-010-0-83",
"498-065-12-011-0-79",
"498-065-12-012-0-75",
"498-065-12-013-0-71",
"498-065-12-014-0-67",
"498-065-12-015-0-63",
"498-065-12-016-0-59",
"498-065-12-017-0-55",
"498-065-12-018-0-51",
"498-065-12-019-0-47",
"498-065-12-020-0-43",
"498-065-12-021-0-39",
"498-065-12-022-0-35",
"498-065-12-023-0-31",
"498-065-12-024-0-27",
"498-065-12-025-0-23",
"498-065-12-026-0-19",
"498-065-12-027-0-15",
"498-065-12-028-0-11",
"498-065-12-029-0-07",
"498-065-12-030-0-03",
"498-065-12-031-0-98",
"498-065-12-032-0-94",
"498-065-12-033-0-90",
"498-065-12-034-0-86",
"498-065-12-035-0-82",
"498-065-12-036-0-78",
"498-065-12-037-0-74",
"498-065-12-038-0-70",
"498-065-12-039-0-66",
"498-065-12-040-0-62",
"498-065-12-041-0-58",
"498-065-12-042-0-54",
"498-065-12-043-0-50",
"498-065-12-044-0-46",
"498-065-12-045-0-42",
"498-065-12-046-0-38",
"498-065-12-047-0-34",
"498-065-12-048-0-30",
"498-065-12-049-0-26",
"498-065-12-050-0-22",
"498-065-12-051-0-18",
"498-065-12-052-0-14",
"498-065-12-053-0-10",
"498-065-12-054-0-06",
"498-065-12-055-0-02",
"498-065-12-056-0-97",
"498-065-12-057-0-93",
"498-065-12-058-0-89",
"498-065-12-059-0-85",
"498-065-12-060-0-81",
"498-065-12-061-0-77",
"498-065-12-062-0-73",
"498-065-12-063-0-69",
"498-065-12-064-0-65",
"498-065-12-065-0-61",
"498-065-12-066-0-57",
"498-065-12-067-0-53",
"498-065-12-068-0-49",
"498-065-12-069-0-45",
"498-065-12-070-0-41",
"498-065-12-071-0-37",
"498-065-12-072-0-33",
"498-065-12-073-0-29",
"498-065-12-074-0-25",
"498-065-12-075-0-21",
"498-065-12-076-0-17",
"498-065-12-077-0-13",
"498-065-12-078-0-09",
"498-065-12-079-0-05",
"498-065-12-080-0-01",
"498-065-12-081-0-96",
"498-065-12-082-0-92",
"498-065-12-083-0-88",
"498-065-12-084-0-84",
"498-065-12-085-0-80",
"498-065-12-086-0-76",
"498-065-12-087-0-72",
"498-065-12-088-0-68",
"498-065-12-089-0-64",
"498-065-12-090-0-60",
"498-065-12-091-0-56",
"498-065-12-092-0-52",
"498-065-12-093-0-48",
"498-065-12-094-0-44",
"498-065-12-095-0-40",
"498-065-12-096-0-36",
"498-065-12-097-0-32",
"498-065-12-098-0-28",
"498-065-12-099-0-24",
"498-065-12-100-0-20",
"498-065-12-101-0-16",
"498-065-12-102-0-12",
"498-065-12-103-0-08",
"498-065-12-104-0-04",
"498-065-12-105-0-99",
"498-065-12-106-0-95",
"498-065-12-107-0-91",
"498-065-12-108-0-87",
"498-065-12-109-0-83",
"498-065-12-110-0-79",
"498-065-12-111-0-75",
"498-065-12-112-0-71",
"498-065-12-113-0-67",
"498-065-12-114-0-63",
"498-065-12-115-0-59",
"498-065-12-116-0-55",
"498-065-12-117-0-51",
"498-065-12-118-0-47",
"498-065-12-119-0-43",
"498-065-12-120-0-39",
"498-065-12-121-0-35",
"498-065-12-122-0-31",
"498-065-12-123-0-27",
"498-065-12-124-0-23",
"498-065-12-125-0-19",
"498-065-12-126-0-15",
"498-065-12-127-0-11",
"498-065-12-128-0-07",
"498-065-12-129-0-03",
"498-065-12-130-0-98",
"498-065-12-131-0-94",
"498-065-12-132-0-90",
"498-065-12-133-0-86",
"498-065-12-134-0-82",
"498-065-12-135-0-78",
"498-065-12-136-0-74",
"498-065-12-137-0-70",
"498-065-12-138-0-66",
"498-065-12-139-0-62",
"498-065-12-140-0-58",
"507-034-00-074-1-34",
"507-034-00-074-2-31",
"513-027-00-001-0-87",
"523-070-00-041-2-06",
"578-001-00-030-1-63",
"578-001-00-030-3-57",
"593-018-00-902-0-53",
"600-001-00-002-0-43",
"685-076-01-001-0-86",
"690-013-01-903-0-88",
"692-001-00-014-2-04",
"692-001-00-014-3-01",
"694-006-00-038-1-39",
"694-006-00-038-2-36",
"807-021-00-001-0-20"
            };




              int vid = 43;
              var debet = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == vid && s.Сумма > 0).Select(s => s.Сумма).Sum();
              var kredit = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == vid && s.Сумма < 0).Select(s => s.Сумма).Sum();
            var ls = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).Where(a => a.СальдоНаНачало.Any(s => s.Вид == vid && s.Сумма != 0)).Select(a => $"{a.LsKvc.Ls}\t{a.СальдоНаНачало.First(s => s.Вид == vid).Сумма}").ToArray();
            string ss = String.Join("\r\n", ls);

            vid = 73;
            var debet2 = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == vid && s.Сумма > 0).Select(s => s.Сумма).Sum();
            var kredit2 = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == vid && s.Сумма < 0).Select(s => s.Сумма).Sum();
            var ls2 = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).Where(a => a.СальдоНаНачало.Any(s => s.Вид == vid && s.Сумма != 0)).Select(a => $"{a.LsKvc.Ls}\t{a.СальдоНаНачало.First(s => s.Вид == vid).Сумма}").ToArray();
            string ss2 = String.Join("\r\n", ls2);

            //            var sum = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls))/*.Where(a => long.Parse(a.LsKvc.CombinedLs) >= 04300000000000 && long.Parse(a.LsKvc.CombinedLs) <= 04499999999999)*/.SelectMany(a => a.СальдоНаНачало).Where(s => RsnHelper.PeniResources.Contains(s.Вид))/*.Where(s => s.Вид == vid && s.Сумма > 0)*/.Select(s => s.Сумма).Sum();
            //            var ss = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls)).SelectMany(a => a.СальдоНаНачало.Select(s => new {Alg = a.Алгоритмы.FirstOrDefault(al => al.Вид == s.Вид), s.Сумма})).Where(s => s.Alg != null && RsnHelper.PeniResources.Contains(s.Alg.Вид)).Select(s => s.Сумма).Sum();
            //            var sum2 = RsnAbonent.Abonents.Where(a => notFoundedLs.Contains(a.LsKvc.Ls))/*.Where(a => long.Parse(a.LsKvc.CombinedLs) >= 04300000000000 && long.Parse(a.LsKvc.CombinedLs) <= 04499999999999)*/.Where(a => a.Алгоритмы.Any(al => al.Вид == vid)).SelectMany(a => a.СальдоНаНачало).Where(s => RsnHelper.PeniResources.Contains(s.Вид))/*.Where(s => s.Вид == vid && s.Сумма > 0)*/.Select(s => s.Сумма).Sum();
            return;

            //var sum = RsnAbonent.Abonents.SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == 93).Select(s => s.Сумма).Sum();
            //return;

            //var abonents = RsnAbonent.Abonents.Where(a => a.СальдоНаНачало.Any(s => s.Сумма != 0 && a.Алгоритмы.FirstOrDefault(al => al.Вид == s.Вид)?.Алгоритм == 0)).ToArray();
            //return;

            //var counterTarifs = RsnAbonent.Abonents
            //    .SelectMany(a => a.ТарифыПоСчетчикам)
            //    .Where(c => c.Вид == 3 && c.КодСчетчика > 20)
            //    .Select(c => new {c.КодСчетчика, c.Значение})
            //    .Distinct()
            //    .ToArray();

            //Clipboard.SetText(string.Join("\r\n", counterTarifs.Select(t => $"{t.КодСчетчика}\t{t.Значение}")));


            //var abonents = RsnAbonent.Abonents.Where(a => a.Алгоритмы.Any(al => al.Вид == 2)).ToArray();
            //var test = abonents
            //    .SelectMany(a => a.Алгоритмы)
            //    .Where(al => al.Вид == 2)
            //    .GroupBy(al => al.Алгоритм)
            //    .Select(g => new {Alg = g.Key, Count = g.Count()});
            //return;
            //var abonents = RsnAbonent.Abonents.Where(a => a.LsKvc.Adr1 == 54900600).ToArray();
            //var el = abonents.Where(a => a.Алгоритмы.Any(al => al.Вид == 3)).Select(a => a.Алгоритмы.First(al => al.Вид == 3).Алгоритм).Distinct().ToArray();
            //var odnel = abonents.Where(a => a.Алгоритмы.Any(al => al.Вид == 03)).Select(a => a.Алгоритмы.First(al => al.Вид == 03).Алгоритм).Distinct().ToArray();
            //return;

            //var abonents = RsnAbonent.Abonents.Where(a =>
            //    a.НецОтоплТарифЭнергия.Any(n => n.Значение != 0) ||
            //    a.НецОтоплТарифТеплонос.Any(n => n.Значение != 0) ||
            //    a.НецОтоплПотреблТеплоносДом.Any(n => n.Значение != 0) ||
            //    a.НецОтоплПотреблЭнергияДом.Any(n => n.Значение != 0)).ToArray();

            //var houses = abonents.Select(a => a.LsKvc.Adr1).Distinct().ToArray();


            //var a2 = RsnAbonent.Abonents.Where(a =>
            //    a.НецОтоплПотреблТеплоносДом.Any(n => n.Значение != 0) ||
            //    a.НецОтоплПотреблЭнергияДом.Any(n => n.Значение != 0)).ToArray();

            //var h2 = a2.Select(a => a.LsKvc.Adr1).Distinct().ToArray(); //901801,22601500,63602200,86613500
            //return;

            //foreach (var rsnAbonent in RsnAbonent.Abonents)
            //{
            //    if (rsnAbonent.Алгоритмы.Any(al => al.Вид == 41 && al.ХозяинВида == 7241 && al.Алгоритм == 96))
            //    {
            //        int a = 10;
            //    }
            //}
            //return;

            //foreach (var rsnAbonent in RsnAbonent.Abonents)
            //{
            //    var test = rsnAbonent.Алгоритмы.Where(al => RsnHelper.PeniResources.Contains(al.Вид) && al.Алгоритм == 96 && al.ХозяинВида == 6310);
            //    if (test.Any())
            //    {
            //        int a = 10;
            //    }
            //}
            //return;

            //var abonentsWithManyStavCounter = RsnAbonent.Abonents.Where(a => a.ТарифыПоСчетчикам.Any(c => c.Вид == 3 && c.КодСчетчика >= 21 && c.КодСчетчика <= 37)).ToArray();
            //var tarifs = new List<MayStafTarifs>();
            //foreach (var abonent in abonentsWithManyStavCounter)
            //{
            //    var tarif = new MayStafTarifs
            //    {
            //        Ls = abonent.LsKvc.Ls
            //    };
            //    foreach (var cntTarif in abonent.ТарифыПоСчетчикам)
            //    {
            //        if (cntTarif.Вид != 3) continue;

            //    }
            //    tarifs.Add(tarif);
            //}
            //Clipboard.SetText(String.Join("\r\n", tarifs.Select(t => $"{t.Ls};{t.Tarif1};{t.Tarif2};{t.Tarif3}")));

            //var alldif = RsnAbonent.Abonents.Select(a => a.КолвоДнейСОтоплением).Distinct().ToArray();
            //var test = alldif.Select(c => new {days = c, count = RsnAbonent.Abonents.Count(a => a.КолвоДнейСОтоплением == c)}).ToArray();
            //var o0 = RsnAbonent.Abonents.First(a => a.КолвоДнейСОтоплением == 0);
            //var o1 = RsnAbonent.Abonents.First(a => a.КолвоДнейСОтоплением > 0 && a.КолвоДнейСОтоплением < 30);
            //return;


            //var test = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Any(c => c.Вид == 5 && c.Счетчик.Код >= 9 && c.Счетчик.Код <= 12)).ToArray();

            //string output = "";
            //foreach (var abonent in test)
            //{
            //    var counters = abonent.УЗСнаНачМес.Where(c => c.Вид == 5 && c.Счетчик.Код >= 9 && c.Счетчик.Код <= 12).ToArray();
            //    foreach (var counter in counters)
            //    {
            //        string cntName = "";
            //        switch (counter.Счетчик.Код)
            //        {
            //            case 9:
            //                cntName = "Н.постр. (водоотв.)";
            //                break;
            //            case 10:
            //                cntName = "Н.постр. (без водоотв.)";
            //                break;
            //            case 11:
            //                cntName = "Полив (без водоотв.)";
            //                break;
            //            case 12:
            //                cntName = "Животные (без водоотв.)";
            //                break;
            //        }
            //        output += $"{abonent.LsKvc.Ls};{cntName}\r\n";
            //    }
            //}
            //Clipboard.SetText(output);

            //return;


            //var test = RsnAbonent.Abonents.Where(a => a.СостояниеЛС == 5).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.НачисленияПоСчетчикам.Any(n => n.Вид == 1)).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Any(c => c.Вид == 03 && c.Счетчик.Код >= 21 && c.Счетчик.Код <= 39)).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.ИстИнфПоСчетчикам.Any(c => c.ИстИнф == 3)).ToArray();
            //return;

            //var testNorm = RsnAbonent.Abonents.SelectMany(a => a.Нормативы).Where(n => n.Вид == 83 && n.Значение1 != 0).Select(n => n.Значение1).Distinct().OrderBy(n => n).ToArray();
            //return;

            //var cntAbonents = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Any(c => c.Вид == 3)).ToArray();
            //foreach(var ab in cntAbonents)
            //{
            //    var counters = ab.УЗСнаНачМес.Where(c => c.Вид == 3).Select(c => c.Счетчик.Код).Distinct().ToArray();
            //    bool one = false;
            //    bool two = false;
            //    bool three = false;
            //    foreach(var cnt in counters)
            //    {
            //        if (cnt >= 0 && cnt <= 19) one = true;
            //        else if (cnt >= 21 && cnt <= 28) two = true;
            //        else if (cnt >= 31 && cnt <= 39) three = true;
            //        else
            //        {
            //            int wtf = 10;
            //        }
            //    }
            //    int types = 0;
            //    if (one) types++;
            //    if (two) types++;
            //    if (three) types++;

            //    if (types > 1)
            //    {
            //        int fewtypes = 10;
            //    }
            //}
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.Алгоритмы.Any(al => al.ХозяинВида == 8890)).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.ОПУ.Any()).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.ПараметрыУчастка.Count(p => p.КодПараметра >= 212 && p.КодПараметра <= 214 && p.Значение > 0) > 0).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Any(c => c.Счетчик.Код == 9 && c.Вид == 5)).ToArray();
            //var test2 = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Any(c => c.Счетчик.Код == 10 && c.Вид == 5)).ToArray();
            //return;

            //var test6 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 206).Select(p => p.Значение).Distinct().ToArray();
            //var test7 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 207).Select(p => p.Значение).Distinct().ToArray();
            //var test8 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 208).Select(p => p.Значение).Distinct().ToArray();
            //var test9 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 209).Select(p => p.Значение).Distinct().ToArray();
            //var test10 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 210).Select(p => p.Значение).Distinct().ToArray();
            //var test11 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 211).Select(p => p.Значение).Distinct().ToArray();
            //var test12 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 212).Select(p => p.Значение).Distinct().ToArray();
            //var test13 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 213).Select(p => p.Значение).Distinct().ToArray();
            //var test14 = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Where(p => p.КодПараметра == 214).Select(p => p.Значение).Distinct().ToArray();

            //return;
            //var test = RsnAbonent.Abonents.Where(a => a.РучныеНачисления.Any(n => n.Вид == 6) && a.Алгоритмы.Any(al => al.Вид == 6 && al.Алгоритм == 1)).ToArray();
            //var test = RsnAbonent.Abonents.Where(a => a.РучныеНачисления.Any(n => n.Вид == 6)).SelectMany(a => a.Алгоритмы).Where(al => al.Вид == 6).Select(al => al.Алгоритм).Distinct().ToArray();
            //return;

            //var spr = new Spr1(2017, 02);
            //var owners = spr.GetSubSpr(Spr1.SubSpr.Хозяева);
            //var ownersent = spr.GetSubSpr(Spr1.SubSpr.ВходимостьХозяев);
            //var ownersAdd = spr.GetSubSpr(Spr1.SubSpr.ПродолжениеХозяев);

            //var allMikroOwners = RsnAbonent.Abonents.SelectMany(a => a.Алгоритмы).Select(al => new { al.Вид, al.ХозяинВида }).Distinct().OrderBy(o => o.ХозяинВида).ToArray();
            //var allMakroOwnres = allMikroOwners.Select(mo => ownersent.Single(oe => oe.R1 == mo.Вид && oe.R3 == mo.ХозяинВида).R2).Distinct().ToArray();

            //var allOwners = allMakroOwnres.Select(mo => owners.Single(o => o.R1 == mo)).Select(o => new { Id = o.R1, Name = o.Sr40.Trim() + ownersAdd.FirstOrDefault(od => od.R1 == o.R1)?.Sr40 }).OrderBy(o => o.Id).ToArray();

            //var uniqueOwners = allOwners.GroupBy(o => o.Name).Select(o => new { Id = o.First().Id, Name = o.Key }).ToArray();

            ////Clipboard.SetText(String.Join("\r\n", allOwners.Select(o => $"{o.Id}\t{o.Name}")));
            //Clipboard.SetText(String.Join("\r\n", uniqueOwners.OrderBy(o => o.Name).Select(o => $"{o.Id}\t{o.Name}")));

            //return;

            //var test = RsnAbonent.Abonents.SelectMany(a => a.РучныеНачисления).Select(n => n.Вид).Distinct().ToArray();
            //return;

            //var t1 = RsnAbonent.Abonents.Where(a => a.КолвоДнейСОтоплением == 0 && a.Алгоритмы.Any(al => al.Вид == 6 && al.Алгоритм == 1) && a.Нормативы.Any(n => n.Вид == 6 && n.Значение1 > 0)).ToArray();
            //var t2 = RsnAbonent.Abonents.Where(a => a.КолвоДнейСОтоплением == 0 && a.Алгоритмы.Any(al => al.Вид == 6 && al.Алгоритм == 1) && a.НачисленияПоНормативам.Any(n => n.Вид == 6 && n.Значение > 0)).ToArray();

            //var test = RsnAbonent.Abonents.Select(a => a.КолвоДнейСОтоплением).Distinct().Select(c => new { Days = c, Count = RsnAbonent.Abonents.Count(a => a.КолвоДнейСОтоплением == c) }).OrderByDescending(c => c.Count).ToArray();
            //return;

            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    if (!abonent.Алгоритмы.Any(al => al.Вид == 6 && al.Алгоритм == 1)) continue;
            //    var norm = abonent.Нормативы.FirstOrDefault(n => n.Вид == 6);
            //    if (norm == null) continue;
            //    if (norm.Значение2 == 0) continue; // проблема, когда нет начислений

            //    decimal square = 0;
            //    if (abonent.ВидЖилогоПомещения == 3 || abonent.ВидЖилогоПомещения == 6)
            //        square = abonent.ПлощадьКомнат == 0 ? abonent.ОбщаяПлощадь : abonent.ПлощадьКомнат;
            //    else
            //        square = abonent.ОбщаяПлощадь;

            //    if (Math.Abs(square * norm.Значение1 - norm.Значение2) > 0.01m)
            //    {
            //        int a = 10;
            //    }
            //}
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.ЖилаяПлощадь != a.ПлощадьКомнат).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.Алгоритмы.Any(al => al.Вид == 03 && new[] { 11, 12, 13 }.Contains(al.Алгоритм))).ToArray();
            //var t1 = test.Where(t => t.ПокНаКонецМесяца.Any()).ToArray();
            //var t2 = test.Where(t => t.СчЗамУЗСнаНачМес.Any()).ToArray();
            //return;

            //var abWithNach = RsnAbonent.Abonents.Where(a => a.НачисленияПоНормативам.Any(n => n.Вид == 6 && n.Значение > 0)).ToArray();

            //var total = abWithNach.Where(a => Math.Abs(a.Нормативы.First(n => n.Вид == 6).Значение2 / a.ОбщаяПлощадь - a.Нормативы.First(n => n.Вид == 6).Значение1) < 0.01m).ToArray();
            //var liv = abWithNach.Where(a => Math.Abs(a.Нормативы.First(n => n.Вид == 6).Значение2 / a.ЖилаяПлощадь - a.Нормативы.First(n => n.Вид == 6).Значение1) < 0.01m).ToArray();
            //var rom = abWithNach.Where(a => Math.Abs(a.Нормативы.First(n => n.Вид == 6).Значение2 / (decimal)a.ПлощадьКомнат - a.Нормативы.First(n => n.Вид == 6).Значение1) < 0.01m).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.Алгоритмы.Any(al => al.Вид == 3 && new[] { 11,12,13}.Contains( al.Алгоритм)) && a.УЗСнаКонМес.Any(p => p.Вид == 3)).ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Select(a => a.ПараметрыДома.Count).Distinct().ToArray();
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.ПараметрыДома.Any(p => p.КодРеквезита == 6)).ToArray();
            //var tt = test.SelectMany(a => a.ПараметрыДома).Where(p => p.КодРеквезита == 6).Select(p => p.Значение).Distinct().OrderBy(p => p).Select(p => $"{Convert.ToInt64(Convert.ToString(Convert.ToInt32(p),2))}:D6").ToArray();
            //return;
            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    if (!abonent.ТарифыНаГаз.Any()) continue;
            //    if (abonent.ТарифыНаГаз.Count == 1) continue;

            //    if (abonent.ТарифыНаГаз.Where(t => t.Значение != 0).Select(t => t.Значение).Distinct().Count() > 1)
            //    {
            //        int a = 10;
            //    }
            //}
            //return;

            //var t = RsnAbonent.Abonents.Select(a => a.ХарактеристикаКвартиры).Distinct().ToArray();
            //return;

            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    var totalNach = abonent.НачисленияПоНормативам
            //        .Where(n => n.Значение != 0)
            //        .Select(n => new { Vid = n.Вид, Sum = n.Значение })
            //        .Concat(abonent.НачисленияПоСчетчикам
            //            .Where(n => n.Сумма != 0)
            //            .Select(n => new { Vid = n.Вид, Sum = n.Сумма }))
            //        .ToArray();

            //    foreach(var nach in totalNach)
            //    {
            //        if (!abonent.Алгоритмы.Any(al => al.Вид == nach.Vid))
            //        {
            //            int aa = 10;
            //        }
            //    }
            //}
            //return;

            //var test = RsnAbonent.Abonents.Where(a => a.СальдоНаНачало.Count > a.СальдоНаКонец.Count).ToArray();
            //var test2 = RsnAbonent.Abonents.Where(a => a.СальдоНаНачало.Count < a.СальдоНаКонец.Count).ToArray();

            //var test3 = RsnAbonent.Abonents.Where(a => a.СальдоНаНачало.Any(s => s.Сумма == 0) || a.СальдоНаКонец.Any(s => s.Сумма == 0)).ToArray();
            //var test4 = RsnAbonent.Abonents.Where(a => a.СальдоНаНачало.GroupBy(s => s.Вид).Where(s => s.Count() > 1).Any() || a.СальдоНаКонец.GroupBy(s => s.Вид).Where(s => s.Count() > 1).Any()).ToArray();
            //return;

            //var testl = new List<TestGCounter>();
            //foreach(var ab in RsnAbonent.Abonents.Where(a => a.ОПУ.Any()))
            //{
            //    foreach(var opu in ab.ОПУ)
            //    {
            //        var alg = ab.Алгоритмы.FirstOrDefault(al => al.Вид == opu.Вид);
            //        testl.Add(alg == null 
            //            ? new TestGCounter { /*OwnerId = null,*/ Algorithm = null, Vid = null } 
            //            : new TestGCounter { /*OwnerId = alg.ХозяинВида,*/ Algorithm = alg.Алгоритм, Vid = opu.Вид });
            //    }
            //}

            //var ditTest = new List<TestGCounter>();
            //foreach(var tt in testl)
            //{
            //    if (ditTest.Any(t => t.Equals(tt))) continue;
            //    ditTest.Add(tt);
            //}

            //var res = ditTest.Select(t => new { t, cnt = testl.Count(tl => tl.Equals(t)) }).ToArray();
            //return;


            //var t1 = RsnAbonent.Abonents.Where(a => a.LsKvc.Adr1 == 64000414).ToArray();
            //var t2 = RsnAbonent.Abonents.Where(a => a.LsKvc.Adr1 == 73601401).ToArray();

            //List<TestGCounter> testcounters = new List<TestGCounter>();
            //var abWithCounters = RsnAbonent.Abonents.Where(a => a.ПоказанияОПУ.Any()).ToArray();
            //var houses = abWithCounters.Select(a => a.LsKvc.Adr1).Distinct().ToArray();
            //foreach (var house in houses)
            //{
            //    var abInHouse = RsnAbonent.Abonents.Where(a => a.LsKvc.Adr1 == house && a.ПоказанияОПУ.Any()).ToArray();
            //    var vids = abInHouse.SelectMany(a => a.ПоказанияОПУ).Select(a => a.Вид).Distinct().ToArray();
            //    foreach (var vid in vids)
            //    {
            //        var owns = abInHouse.Select(a => a.Алгоритмы.SingleOrDefault(al => al.Вид == vid)).Where(al => al != null).Select(al => al.ХозяинВида).Distinct().ToArray();
            //        if (owns.Length > 1)
            //        {
            //            testcounters.Add(new TestGCounter
            //            {
            //                Adr1 = house,
            //                Vid = vid,
            //                Owners = owns.Select(o => new TestGCounter.Own { OwnerId = o }).ToArray()
            //            });
            //        }
            //    }
            //}


            //var mn = RsnAbonent.Abonents.Count(a => a.РучныеНачисления.Any(m => m.Вид == 1 && m.Значение != 0));
            //return;

            //var tt = RsnAbonent.Abonents.Where(a => a.УЗСнаКонМес.Any(n => a.УЗСнаНачМес.Select(ee => ee.Счетчик.Значение).Contains(n.Счетчик.Значение))).ToArray();
            //var tt = RsnAbonent.Abonents.Where(a => a.ПотребленияПоСчетчикам.Any(p => p.Потребление == 0)).ToArray();
            //return;

            //var t1 = RsnAbonent.Abonents.Count(a => a.ПотреблениеОПУ.Count > a.ПоказанияОПУ.Count);
            //var t2 = RsnAbonent.Abonents.Count(a => a.ПотреблениеОПУ.Count < a.ПоказанияОПУ.Count);
            //return;

            //var ab = RsnAbonent.Abonents.Where(a => a.ПоказанияОПУ.Any() && a.ПоказанияОПУ.All(p => p.Вид != 6)).ToArray();
            //return;

            //var grh = RsnAbonent.Abonents.GroupBy(a => a.LsKvc.Adr1).ToArray();
            //foreach(var h in grh)
            //{
            //    int grcount = h.ElementAt(0).ОПУ.Count;
            //    foreach(var ab in h)
            //    {
            //        if (grcount != ab.ОПУ.Count)
            //        {
            //            int a = 10;
            //        }
            //    }
            //}
            //return;

            //var opu = RsnAbonent.Abonents.SelectMany(a => a.ОПУ).ToArray();
            //var vids = opu.Select(c => c.Вид).Distinct().Select(c => new { Вид = c, Колво = opu.Count(o => o.Вид == c) }).ToArray();
            //var koef = opu.Select(c => c.Коэффициент).Distinct().Select(c => new { Коэффициент = c, Колво = opu.Count(o => o.Коэффициент == c) }).ToArray();
            //var stat = opu.Select(c => c.СтатусСчетчика).Distinct().Select(c => new { СтатусСчетчика = c, Колво = opu.Count(o => o.СтатусСчетчика == c) }).ToArray();
            //var nal = opu.Select(c => c.НаличиеСчетчика).Distinct().Select(c => new { НаличиеСчетчика = c, Колво = opu.Count(o => o.НаличиеСчетчика == c) }).ToArray();


            //var t1 = RsnAbonent.Abonents.Where(a => a.ОПУ.Any(o => o.СтатусСчетчика == 1)).ToArray();

            //return;

            //var param = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).ToArray();
            //var group = param.GroupBy(p => p.КодПараметра, p => p.Значение).ToArray();
            //return;
            //List<decimal> koefList = new List<decimal>();
            //foreach(var abonent in RsnAbonent.Abonents)
            //{
            //    foreach(var nachPok in abonent.УЗСнаНачМес)
            //    {
            //        var endPok = abonent.УЗСнаКонМес.Single(ep => ep.Вид == nachPok.Вид && ep.Счетчик.Код == nachPok.Счетчик.Код);
            //        var diff = endPok.Счетчик.Значение - nachPok.Счетчик.Значение;
            //        if (diff <= 0) continue;
            //        var vol = abonent.ПотребленияПоСчетчикам.Single(p => p.Вид == nachPok.Вид && p.КодСчетчика == nachPok.Счетчик.Код);
            //        if (Math.Abs(diff - vol.Потребление) > 0.01m)
            //            koefList.Add(diff / vol.Потребление);
            //    }
            //}
            //koefList = koefList.OrderBy(k => k).ToList();
            //return;

            //var hvsParams = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыУчастка).Distinct().ToArray();
            //var groups = hvsParams.GroupBy(p => p.КодПараметра).ToArray();
            //var types = groups.Select(g => g.Key).ToArray();
            //return;

            //var hchars = RsnAbonent.Abonents.SelectMany(a => a.ПараметрыДома).Distinct().GroupBy(h => h.КодРеквезита).Distinct().ToArray();
            //var ms = hchars.Single(h => h.Key == 26).Select(h => h.ShortInt56).Distinct();
            //var ss = hchars.Single(h => h.Key == 28).Select(h => h.ShortInt56).Distinct();
            //return;

            //var citi = RsnAbonent.Abonents.Where(a => a.ПризнакиЖителей.Any()).ToArray();
            //return;
            //var abonentsWithManNach = RsnAbonent.Abonents.Where(a => a.РучныеНачисления.Any()).ToArray();
            //return;

            //foreach (var abonent in RsnAbonent.Abonents)
            //{
            //    foreach (var nach in abonent.НачисленияПоНормативам.Where(n => n.Коэффециент > 0))
            //    {
            //        if (!abonent.ПризнакиСнятияПовышКоэффициента.Any(p => p.Вид == nach.Вид))
            //        {
            //            int a = 10;
            //        }
            //    }
            //}
            //return;

            //var counters = RsnAbonent.Abonents.SelectMany(a => a.УЗСнаКонМес).Select(c => new { c.Вид, c.Счетчик.Код, c.Счетчик.Разрядность }).Distinct().ToArray();
            //Clipboard.SetText(String.Join("\r\n", counters.Select(c => $"{c.Вид}\t{c.Код}\t{c.Разрядность}")));

            //var strange = RsnAbonent.Abonents.Where(a => a.ПризнакиЖителей.Count(p => p.ПризнакНанимателяИлиСобственника == 0) > 0 && a.ПризнакиЖителей.Count(p => p.ПризнакНанимателяИлиСобственника == 1) > 0);

            //var types = RsnAbonent.Abonents.SelectMany(a => a.ПризнакиЖителей).Select(c => c.ПризнакНанимателяИлиСобственника).Distinct().ToArray();
            //var typess = RsnAbonent.Abonents.SelectMany(a => a.ПризнакиЖителей).Select(c => c.Статус).Distinct().ToArray();

            //var test = RsnAbonent.Abonents.Where(a => a.ПризнакиЖителей.Count(p => p.ПризнакНанимателяИлиСобственника != 0) > 1).ToArray();
            //var citizens = RsnAbonent.Abonents.Select(a => new { Ls = a.LsKvc, CChars = a.ПризнакиЖителей, CFio = a.Жители }).ToArray();
            //var moreP = citizens.Where(c => c.CChars.Count > c.CFio.Count).ToArray();
            //var moreF = citizens.Where(c => c.CChars.Count < c.CFio.Count).ToArray();

            //var ttt = RsnAbonent.Abonents.Where(a => a.ПризнакиЖителей.Count > 0 && a.ПризнакиЖителей.Count != a.Жители.Count);

            //var hasP = moreF.Where(f => f.CChars.Count > 0).ToArray();
            //var moreFF = moreF.Where(f => f.CFio.Count > 1).ToArray();

            //return;

            //var РучноеНачислине = RsnAbonent.Abonents.SelectMany(a => a.РучныеНачисления).Where(s => s.Вид == 1).Sum(n => n.Значение);
            //var СуммаРучныхПрошлых = RsnAbonent.Abonents.SelectMany(a => a.Суммы_ручныхПрошлых).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var СписаниеСальдо = RsnAbonent.Abonents.SelectMany(a => a.СписанныеСальдо).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var СуммаАвтопрошлых = RsnAbonent.Abonents.SelectMany(a => a.Суммы_автопрошлых).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var КорректировкаОплаты = RsnAbonent.Abonents.SelectMany(a => a.КорректировкиОплат).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var КоррНачислПоСч = RsnAbonent.Abonents.SelectMany(a => a.КорректНачислПоСчетчикам).Where(s => s.Вид == 1).Sum(s => s.Сумма);

            //var saldoB = RsnAbonent.Abonents.SelectMany(a => a.СальдоНаНачало).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var saldoE = RsnAbonent.Abonents.SelectMany(a => a.СальдоНаКонец).Where(s => s.Вид == 1).Sum(s => s.Сумма);
            //var paysum = RsnAbonent.Abonents.SelectMany(a => a.ОплатыЗаПериод).Where(s => s.Вид == 1).Sum(p => p.Сумма);

            //foreach (var rsnAbonent in RsnAbonent.Abonents)
            //{
            //    if (rsnAbonent.КорректНачислПоСчетчикам.Any() || rsnAbonent.КорректПотребПоСчетчикам.Any()) // ниодного
            //    {
            //        int a = 10;
            //    }
            //}

            //var counterTypes = RsnAbonent.Abonents.SelectMany(a => a.УЗСнаНачМес).Select(c => new { c.Вид, c.Счетчик.Код, c.Счетчик.Разрядность }).Distinct().OrderBy(c => c.Вид).ThenBy(c => c.Код).ThenBy(c => c.Разрядность);
            //Clipboard.SetText(String.Join("\r\n", counterTypes.Select(n => String.Format("{0}\t{1}\t{2}", n.Вид, n.Код, n.Разрядность))));

            //var test1 = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Count != a.УЗСнаКонМес.Count).ToArray(); таких нет
            //var test2 = RsnAbonent.Abonents.Where(a => a.УЗСнаНачМес.Count < a.ПотребленияПоСчетчикам.Where(p => p.Вид != 17).Count()).ToArray(); таких нет (40 есть. у кого вид 83 ОДН эл.)

            //var naemKv = RsnAbonent.Abonents
            //    .Where(a => a.Тарифы.Any(t => t.Вид == 16 && t.Значение > 0))
            //    .Select(a => new { Характеристика = a.ХарактеристикаКвартиры, Тариф = a.Тарифы.Single(t => t.Вид == 16).Значение })
            //    .Distinct();
            //Clipboard.SetText(String.Join("\r\n", naemKv.Select(n => String.Format("{0}\t{1}", n.Характеристика, n.Тариф))));

            //MessageBox.Show("Скопировано в буфер обмена");
        }

        private void SodJilKom_button_Click(object sender, EventArgs e)
        {
            var analyzeResult = new List<TarifAnalyze>();
            List<string> test = new List<string>();
            foreach (var abonent in RsnAbonent.Abonents)
            {
                foreach (var al in abonent.Алгоритмы)
                {
                    if (al.Вид != 1) continue;
                    var tarif = abonent.Тарифы.SingleOrDefault(t => t.Вид == al.Вид);
                    var nomrs = abonent.Нормативы.Where(n => n.Вид == al.Вид);
                    var norm = nomrs.FirstOrDefault();
                    if (nomrs.Count() > 1) { test.Add(abonent.LsKvc.Ls); }
                    var nach = abonent.НачисленияПоСчетчикам.Where(n => n.Вид == al.Вид).Select(n => n.Сумма)
                        .Concat(abonent.НачисленияПоНормативам.Where(n => n.Вид == al.Вид).Select(n => n.Значение)).Sum();
                    var an = new TarifAnalyzeSodJil
                    {
                        Ls = abonent.LsKvc.Ls,
                        VidId = al.Вид,
                        OwnerId = al.ХозяинВида,
                        AlgorithmId = al.Алгоритм,

                        Tarif = (float?)tarif?.Значение,
                        Norm1 = (float?)norm?.Значение1,
                        Norm2 = (float?)norm?.Значение2,
                        //IsCounter = abonent.ТарифыПоСчетчикам.Any(t => t.Вид == al.Вид),

                        LivingPeopleCount = abonent.ЧислоПроживающих,
                        OwnerPeopleCount = abonent.ПризнакиЖителей.Count(c => c.ПризнакНанимателяИлиСобственника == 1),
                        TotalSquare = (float)abonent.ОбщаяПлощадь,
                        LivingSquare = (float)abonent.ЖилаяПлощадь,
                        NachSum = (float)nach,
                    };

                    if (abonent.ТарифыСодЖилКомп.Count > 0)
                    {
                        an.TarifEl = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 103)?.Тариф;
                        an.NachEl = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 103)?.Начисление;
                        an.TarifHv = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 105)?.Тариф;
                        an.NachHv = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 105)?.Начисление;
                        an.TarifGv = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 107)?.Тариф;
                        an.NachGv = (float?)abonent.ТарифыСодЖилКомп.SingleOrDefault(t => t.Вид == 107)?.Начисление;
                    }
                    analyzeResult.Add(an);
                }
            }
            var result = new List<string> { "Лс;Вид;Хозяин;Алгоритм;Тариф;Норма1;Норма2;По Счетчику;Число проживающих;Количество владельцев;Общая площадь;Жилая площадь;Сумма начислений;ТарифЭл;НачЭл;ТарифХв;НачХв;ТарифГв;НачГв" };
            result.AddRange(analyzeResult.Select(ar => ar.ToString()));
            File.WriteAllLines("TarifsSod.csv", result);
            MessageBox.Show("Скопировано в буфер обмена");
        }

        private void AbonentByLsKvcbutton_Click(object sender, EventArgs e)
        {
            RsnAbonent rsnAbonent = null;
            int year = 2017;
            int month = 05;
            string fileName = ReadRsnForm.RsnFilePath + @"\rsn0";
            fileName += $"{year.ToString().Substring(2, 2)}{month:D2}";
            var rsnFile = new RsnFile(fileName);
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), System.Text.Encoding.GetEncoding(1251)))
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
                        if (new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32()).Ls == LsKvc_textBox.Text.Trim())
                        {
                            rsnAbonent = new RsnAbonent(bytes, rsnFile);
                            break;
                        }
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                if (rsnAbonent == null)
                {
                    if (new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32()).Ls == LsKvc_textBox.Text.Trim())
                    {
                        rsnAbonent = new RsnAbonent(bytes, rsnFile);
                    }
                }
            }
        }

        private void NotExistedAbonentsInOldFiles_button_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic;
            List<ReadRsnForm.LsNotInFile> lsNotInlastFile;
            ReadRsnForm.FillLsDic(out dic, out lsNotInlastFile, false, new[] { new DateTime(2017, 02, 01), new DateTime(2017, 01, 01), new DateTime(2016, 12, 01), new DateTime(2016, 11, 01) });
        }

        private void Necentralotopl_button_Click(object sender, EventArgs e)
        {
            var abonents = RsnAbonent.Abonents.Where(a =>
                a.НецОтоплТарифТеплонос.Any(n => n.Значение != 0) ||
                a.НецОтоплТарифЭнергия.Any(n => n.Значение != 0)).ToArray();
            var necList = new List<Nec>();
            foreach (var abonent in abonents)
            {
                foreach (var nec in abonent.НецОтоплТарифЭнергия)
                {
                    if (nec.Значение == 0) continue;
                    if (necList.Any(n => n.Adr1 == abonent.LsKvc.Adr1 && n.MajorType == 0 && n.Type == nec.Вид && n.Value == nec.Значение)) continue;
                    string typeName;
                    switch (nec.Вид)
                    {
                        case 2: typeName = "Газ"; break;
                        case 3: typeName = "Электроэнергия"; break;
                        case 6: typeName = "Тепло от отопления"; break;
                        case 100: typeName = "Мазут"; break;
                        case 101: typeName = "Уголь"; break;
                        case 102: typeName = "Дрова"; break;
                        default: typeName = "Неизвестно"; break;
                    }
                    necList.Add(new Nec
                    {
                        Adr1 = abonent.LsKvc.Adr1,
                        MajorType = 0,
                        MajorTypeName = "Энергия/Топливо",
                        Type = nec.Вид,
                        TypeName = typeName,
                        Value = nec.Значение
                    });
                }

                foreach (var nec in abonent.НецОтоплТарифТеплонос)
                {
                    if (nec.Значение == 0) continue;
                    if (necList.Any(n => n.Adr1 == abonent.LsKvc.Adr1 && n.MajorType == 1 && n.Type == nec.Вид && n.Value == nec.Значение)) continue;
                    string typeName;
                    switch (nec.Вид)
                    {
                        case 5: typeName = "Вода"; break;
                        case 100: typeName = "Антифриз"; break;
                        default: typeName = "Неизвестно"; break;
                    }
                    necList.Add(new Nec
                    {
                        Adr1 = abonent.LsKvc.Adr1,
                        MajorType = 1,
                        MajorTypeName = "Теплоноситель",
                        Type = nec.Вид,
                        TypeName = typeName,
                        Value = nec.Значение
                    });
                }
            }

            Clipboard.SetText(String.Join("\r\n", necList.Select(n => $"{n.Adr1};{n.MajorType};{n.MajorTypeName};{n.Type};{n.TypeName};{n.Value}")));
        }

        private class Nec
        {
            public uint Adr1;
            public int MajorType; // 0 - энергия, 1 - теплонос
            public string MajorTypeName; 
            public int Type;
            public string TypeName;
            public decimal Value;
        }

        private void Peni_button_Click(object sender, EventArgs e)
        {
            var peniRecode = _spr1.GetSubSpr(Spr1.SubSpr.ВидыДляПени).Select(s => new { s.R1, s.R2 }).ToDictionary(s => (byte)s.R1, s => (byte)s.R2);

            var resourceRecode = new Dictionary<byte, int>();
            using (TableManager tm = new TableManager(@"D:\dk\Data\dbf\convert"))
            {
                tm.Init();
                var rows = tm.GetDataTable("ResAll").Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];
                    resourceRecode.Add(Convert.ToByte(row[1]), Convert.ToInt32(row[2]));
                }
            }
            var services = new List<int>();
            for (int i = 0; i < RsnAbonent.Abonents.Length; i++)
            {
                var abonent = RsnAbonent.Abonents[i];
                for(int j = 0; j< abonent.Алгоритмы.Count; j++)
                {
                    var alg = abonent.Алгоритмы[j];
                    if (!RsnHelper.PeniResources.Contains(alg.Вид) || alg.Алгоритм != 96) continue;

                    services.Add(resourceRecode[peniRecode[alg.Вид]] * 10000 + alg.ХозяинВида);
                }
            }

            Clipboard.SetText(String.Join("\r\n", services.Distinct().Select(s => s.ToString())));
            MessageBox.Show("Скопировано в буфер обмена");
        }
    }
}

