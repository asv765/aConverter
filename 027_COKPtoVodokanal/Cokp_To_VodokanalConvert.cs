using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace _027_COKPToVodokanal
{
    public static class Consts
    {
        /// <summary>
        /// Начальный номер лицевого счета
        /// </summary>
        // public const string StartLshet = "62209438";
        public static int NewLshetCounter = 1;

        public static string GetLs(string oldls, Dictionary<string,string> dic, IQueryable<ABONENT> abonents = null)
        {
            string newls;
            if (dic.TryGetValue(oldls, out newls)) return newls;
            if (abonents == null) throw new ArgumentNullException("abonents");
            string maxLshet = abonents.Max(p => p.LSHET);
            newls = (Convert.ToInt32(maxLshet) + NewLshetCounter++).ToString(CultureInfo.InvariantCulture);
            dic.Add(oldls, newls);
            return newls;
        }

        public static Dictionary<int,int> GetHouses()
        {
            string[] ha = File.ReadAllLines(@"C:\Work\aConverter_Data\027_COKPtoVodokanal\Документы\СписокДомов.csv");
            var rl = new Dictionary<int,int>();
            foreach (var s in ha)
            {
                string leftpart = s.Split(';')[0];
                int toadd1;
                Int32.TryParse(leftpart, out toadd1);

                string rightpart = s.Split(';')[2];
                int toadd2;
                Int32.TryParse(rightpart, out toadd2);

                rl.Add(toadd1,toadd2);
            }
            return rl;
        }

        public static string GetHouseCdList()
        {
            var suitablehouses = GetHouses();

            string housecdlist = "";
            foreach (int code in suitablehouses.Keys)
            {
                if (!String.IsNullOrEmpty(housecdlist)) housecdlist += ", ";
                housecdlist += code.ToString(CultureInfo.InvariantCulture);
            }

            return housecdlist;
        }


        //public static bool IsSuitableHouseCD(int housecd)
        //{
        //    foreach (int i in Houses)
        //    {
        //        if (housecd == i) return true;
        //    }
        //    return false;
        //}

        public static DateTime StartDate = new DateTime(2013, 9, 1);

        public static string DocumentsDir = @"C:\Work\aConverter_Data\027_COKPToVodokanal\Документы";

        public static void SaveDic(Dictionary<string, string> dic)
        {
            string[] tosave = new string[dic.Count];
            int i = 0;
            foreach (var kvp in dic)
            {
                tosave[i++] = kvp.Key + ";" + kvp.Value;
            }
            File.WriteAllLines(DocumentsDir + "\\" + "LsDic.txt", tosave);
        }

        public static Dictionary<string, string> LoadDic()
        {
            var dic = new Dictionary<string, string>();
            string[] a = File.ReadAllLines(DocumentsDir + "\\" + "LsDic.txt");
            foreach (var s in a)
            {
                dic.Add(s.Split(';')[0],s.Split(';')[1]);
            }
            return dic;
        }

        public static void InitializeDataSource(out AbonentEntitiesModel source, out AbonentEntitiesModel destination)
        {
            var fcsbSource = new FbConnectionStringBuilder(aConverter_RootSettings.FirebirdStringConnection)
            {
                Database = @"C:\Work\aConverter_Data\027_COKPToVodokanal\Database\Cokp\ABONENT.FDB"
            };
            source = new AbonentEntitiesModel(fcsbSource.ToString());

            var fcsbDestination = new FbConnectionStringBuilder(aConverter_RootSettings.FirebirdStringConnection)
            {
                Database = @"C:\Work\aConverter_Data\027_COKPToVodokanal\Database\ABONENT_EMPTY.FDB"
            };
            destination = new AbonentEntitiesModel(fcsbDestination.ToString());            
        }

    }

    /// <summary>
    /// Удаляет все файлы в целевой директории
    /// </summary>
    public class DeleteAllFiles : ConvertCase
    {
        public DeleteAllFiles()
        {
            ConvertCaseName = "Удалить все файлы в целевой директории";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            List<string> fa = Directory.GetFiles(DestDir, "*.DBF").ToList();
            fa.AddRange(Directory.GetFiles(DestDir, "*.CDX").ToList());
            fa.AddRange(Directory.GetFiles(DestDir, "*.FPT").ToList());
            StepStart(fa.Count);
            foreach (string f in fa)
            {
                File.Delete(f);
                Iterate();
            }
            StepFinish();
        }
    }

    /// <summary>
    /// Создать все файлы для конвертации в целевой директории
    /// </summary>
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать файлы для конвертации в целевой директории";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            FactoryRecord.CreateAllTables(tmdest);
            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
            StepFinish();
        }
    }

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT.DBF - данные об абонентах";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            tmdest.CleanTable(typeof(AbonentRecord));

            AbonentEntitiesModel source, destination;
            Consts.InitializeDataSource(out source, out destination);

            #region Заполняем словарь лицевых счетов КВЦ
            var destKvcLshetDic = new Dictionary<string, string>();
            var reverseKvcLshetDic = new Dictionary<string, string>();
            StepStart(destination.EXTORGACCOUNTs.Count(p => p.EXTORGCD == 5));
            foreach (EXTORGACCOUNT er in destination.EXTORGACCOUNTs.Where(p => p.EXTORGCD == 5))
            {
                if (!destKvcLshetDic.ContainsKey(er.EXTLSHET.Substring(0, 12)))
                {
                    destKvcLshetDic.Add(er.EXTLSHET.Substring(0,12), er.LSHET);
                    reverseKvcLshetDic.Add(er.LSHET, er.EXTLSHET);
                }
                Iterate();
            }
            StepFinish();
            #endregion

            // List<string> legalHouses = getLegalHouses();

            //int maxDestHouseCd = destination.HOUSEs.Max(p => p.HOUSECD);
            //int housecdCounter = 1;
            //var housecdDic = new Dictionary<int, int>();
            var housecdDic = Consts.GetHouses();

            var suitablehouses = Consts.GetHouses().Keys.ToList();

            var q = from a in source.ABONENTs
                    join ea in source.EXTORGACCOUNTs on a.LSHET equals ea.LSHET
                    join h in source.HOUSEs on a.HOUSECD equals h.HOUSECD
                    join s in source.STREETs on h.STREETCD equals s.STREETCD
                    where ea.EXTORGCD == 5 && a.DELETED == 0 && suitablehouses.Contains(h.HOUSECD)
                    select new { ABONENTS = a, HOUSES = h, STREET = s, ea.EXTLSHET};
            
            var lshetDic = new Dictionary<string, string>();
            StepStart(q.Count());
            foreach (var lq in q)
            {
                Debug.Assert(lq.ABONENTS.HOUSECD != null, "lq.ABONENTS.HOUSECD != null");

                var ar = new AbonentRecord();

                string lshet, kvclshet;
                if (destKvcLshetDic.TryGetValue(lq.EXTLSHET.Substring(0, 12), out lshet))
                {
                    lshetDic.Add(lq.ABONENTS.LSHET, lshet);
                    kvclshet = reverseKvcLshetDic[lshet];
                }
                else
                {
                    lshet = Consts.GetLs(lq.ABONENTS.LSHET, lshetDic, destination.ABONENTs);
                    kvclshet = lq.EXTLSHET;
                }

                ar.Lshet = lshet;
                ar.Extlshet = kvclshet;
                ar.Extlshet2 = lq.ABONENTS.LSHET;

                ar.F = lq.ABONENTS.FIO;
                ar.I = lq.ABONENTS.NAME;
                ar.O = lq.ABONENTS.SECOND_NAME;
                ar.Ducd = 9700;
                ar.Duname = "ООО ЦОКП";
                if (lq.HOUSES.DISTCD == null)
                    ar.Distkod = 0;
                else
                    ar.Distkod = Convert.ToInt32(lq.HOUSES.DISTCD);
                if (ar.Distkod == 0) ar.Distname = "Неизвестен";
                else if (ar.Distkod == 1) ar.Distname = "Октябрьский";
                else if (ar.Distkod == 2) ar.Distname = "Московский";
                else if (ar.Distkod == 3) ar.Distname = "Советский";
                else if (ar.Distkod == 4) ar.Distname = "Железнодорожный";

                ar.Townskod = 1;
                ar.Townsname = "г.Рязань";
                ar.Rayonkod = 1;
                ar.Rayonname = "г.Рязань";

                // ar.Phonenum = AdvancedTableAdapterManager.GetString(lq.ABONENTS["TEL"]);
                // ar.Postindex = AdvancedTableAdapterManager.GetString(lq.HOUSES["POSTINDEX"]);
                // ar.Ndoma = (AdvancedTableAdapterManager.GetString(lq.HOUSES["HOUSENO"]) + " " + 
                // AdvancedTableAdapterManager.GetString(lq.HOUSES["HOUSEPOSTFIX"])).Trim();
                // ar.Korpus = AdvancedTableAdapterManager.GetInteger(lq.HOUSES["KORPUSNO"]);

                if (!String.IsNullOrEmpty(lq.ABONENTS.TEL)) ar.Phonenum = lq.ABONENTS.TEL;
                if (!String.IsNullOrEmpty(lq.HOUSES.POSTINDEX)) ar.Postindex = lq.HOUSES.POSTINDEX;

                ar.Ndoma = lq.HOUSES.HOUSENO + lq.HOUSES.HOUSEPOSTFIX;
                if (lq.HOUSES.KORPUSNO == null)
                    ar.Korpus = 0;
                else
                    ar.Korpus = (int)lq.HOUSES.KORPUSNO;

                //var lq1 = lq;
                //var hRmpts = from h in destination.HOUSEs
                //                where h.STREETCD == lq1.HOUSES.STREETCD &&
                //                h.HOUSENO == lq1.HOUSES.HOUSENO &&
                //                h.HOUSEPOSTFIX == lq1.HOUSES.HOUSEPOSTFIX &&
                //                h.KORPUSNO == lq1.HOUSES.KORPUSNO &&
                //                h.KORPUSPOSTFIX == lq1.HOUSES.KORPUSPOSTFIX
                //                select h;
                //if (hRmpts.Any())
                //    ar.Housecd = hRmpts.First().HOUSECD;
                //else
                //{
                //    int newHouseCd;
                //    if (!housecdDic.TryGetValue((int) lq.ABONENTS.HOUSECD, out newHouseCd))
                //    {
                //        //newHouseCd = maxDestHouseCd + housecdCounter;
                //        //housecdCounter++;
                //        //housecdDic.Add((int) lq.ABONENTS.HOUSECD, newHouseCd);
                //        throw new Exception("В словаре домов не найден код " + (int)lq.ABONENTS.HOUSECD);
                //    }
                //    ar.Housecd = newHouseCd;
                //}

                ar.Housecd = housecdDic[(int) lq.ABONENTS.HOUSECD];

                ar.Kvartira = lq.ABONENTS.FLATNO + lq.ABONENTS.FLATPOSTFIX;
                if (lq.ABONENTS.ROOMNO == null)
                    ar.Komnata = 0;
                else
                    ar.Komnata = Convert.ToInt32(lq.ABONENTS.ROOMNO);

                Debug.Assert(lq.HOUSES.STREETCD != null, "lq.HOUSES.STREETCD != null");
                ar.Ulicakod = (long) lq.HOUSES.STREETCD;
                ar.Ulicaname = lq.STREET.STREETNM;

                //string _houseAddressComplexString = ar.Extlshet.Substring(0,8);
                // if (legalHouses.Contains(_houseAddressComplexString)) 
                tmdest.InsertRecord(ar.GetInsertScript());

                Iterate();
            }
            StepFinish();
            Consts.SaveDic(lshetDic);
            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS.DBF - данные о характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            tmdest.CleanTable(typeof(CharsRecord));

            var cr = new CharsRecord();

            AbonentEntitiesModel source, destination;
            Consts.InitializeDataSource(out source, out destination);
            source.Log = new StreamWriter(Consts.DocumentsDir + "\\" + "Chars.log", false, Encoding.GetEncoding(1251));

            var lshetDic = Consts.LoadDic();
            var suitablehouses = Consts.GetHouses().Keys.ToList();

            var q = from c in source.CCHARSABONENTLISTs
                    join a in source.ABONENTs on c.LSHET equals a.LSHET
                    join h in source.HOUSEs on a.HOUSECD equals h.HOUSECD
                    where suitablehouses.Contains(h.HOUSECD)
                    select new { a.LSHET, a.HOUSECD, c.KODCCHARSLIST, c.ABONENTCCHARDATE, c.SIGNIFICANCE };

            StepStart(q.Count());
            foreach (var lq in q)
            {
                if (!lshetDic.ContainsKey(lq.LSHET))
                {
                    Iterate();
                    continue;
                }
                Debug.Assert(lq.HOUSECD != null, "lq.HOUSECD != null");

                //if (suitablehouses.Contains((int)lq.ABONENTS.HOUSECD))
                //{
                    cr.Lshet = Consts.GetLs(lq.LSHET, lshetDic);
                    int charcd = lq.KODCCHARSLIST;
                    string charname;
                    if (charcd == 1)
                        charname = "Число проживающих";
                    else if (charcd == 3)
                        charname = "Число зарегистрированных";
                    else if (charcd == 10)
                        charname = "Число временно выбывших";
                    else if (charcd == 12)
                        charname = "Число временно прибывших";
                    else if (charcd == 4)
                    {
                        charcd = 2;
                        charname = "Площадь";
                    }
                    else
                        continue;

                    cr.Charcd = charcd;
                    cr.Charname = charname;

                    cr.Date = lq.ABONENTCCHARDATE;
                    Debug.Assert(lq.SIGNIFICANCE != null, "lq.SIGNIFICANCE != null");
                    cr.Value_ = lq.SIGNIFICANCE.Value;

                    tmdest.InsertRecord(cr.GetInsertScript());
                //}
                Iterate();
            }
            source.Log.Close();
            source.Log = null;
            StepFinish();
            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS.DBF - данные о характеристиках";
            Position = 50;
            IsChecked = false;
        }


        public override void DoConvert()
        {
            SetStepsCount(1);

            tmdest.CleanTable(typeof (LcharsRecord));

            AbonentEntitiesModel source, destination;
            Consts.InitializeDataSource(out source, out destination);

            SetStepsCount(1);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");

            var lshetDic = Consts.LoadDic();

            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in (" +
                Consts.GetHouseCdList() + "));");
            source.ExecuteNonQuery("execute procedure rep_gt_lcharsabonent(1, '01.04.2014', '', -1);");
            var q =
                source.ExecuteQuery<GT_LCHARSABONENT>("select * from gt_lcharsabonent order by lshet, kodlcharslist;");

            var lcr3Dic = new Dictionary<int, string>
            {
                {-1, "Вода отключена"},
                {0, "Вода отсутствует"},
                {1, "Вода присутствует"},
            };

            foreach (GT_LCHARSABONENT lc in q)
            {
                if (!lshetDic.ContainsKey(lc.LSHET))
                {
                    Iterate();
                    continue;
                }
                string lshet = Consts.GetLs(lc.LSHET, lshetDic);
                int lcharcd = lc.KODLCHARSLIST;
                DateTime startdate = new DateTime(2014,4,1);

                Debug.Assert(lc.SIGNIFICANCE != null, "lc.SIGNIFICANCE != null");

                LcharsRecord lcr;

                if (lcharcd == 18)
                {
                    lcr = new LcharsRecord
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        Date = startdate,
                        Lcharcd = 18,
                        Lcharname = "Тип учета ХВС",
                        Lshet = lshet,
                        Value_ = Convert.ToInt64(lc.SIGNIFICANCE.Value),
                        Valuedesc = lc.LOGICSIGNIFICANCE
                    };
                    tmdest.InsertRecord(lcr.GetInsertScript());
                }

                if (lcharcd == 3)
                {
                    int lcr2Value = Convert.ToInt32(lc.SIGNIFICANCE.Value) < 0
                        ? 0
                        : Convert.ToInt32(lc.SIGNIFICANCE.Value);
                    int lcr3Value = Convert.ToInt32(lc.SIGNIFICANCE.Value) > 0
                        ? 1
                        : Convert.ToInt32(lc.SIGNIFICANCE.Value);

                    lcr = new LcharsRecord
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        Date = startdate,
                        Lcharcd = 2,
                        Lcharname = "Благоустройство",
                        Lshet = lshet,
                        Value_ = lcr2Value,
                        Valuedesc = lc.LOGICSIGNIFICANCE
                    };
                    tmdest.InsertRecord(lcr.GetInsertScript());

                    lcr = new LcharsRecord
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        Date = startdate,
                        Lcharcd = 3,
                        Lcharname = "Водоснабжение",
                        Lshet = lshet,
                        Value_ = lcr3Value,
                        Valuedesc = lcr3Dic[lcr3Value]
                    };
                    tmdest.InsertRecord(lcr.GetInsertScript());
                }
            }
            Iterate();
            source.ExecuteNonQuery("commit work;");
            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "CONTERS.DBF, CNTRSIND.DBF - данные о счетчиках и их показаниях";
            Position = 60;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            tmdest.CleanTable(typeof(CountersRecord));
            tmdest.CleanTable(typeof(CntrsindRecord));

            AbonentEntitiesModel source, destination;
            Consts.InitializeDataSource(out source, out destination);

            StepStart(1);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");
            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in ("+Consts.GetHouseCdList()+"));");
            source.ExecuteNonQuery("execute procedure rep_gt_counters(1, '01.03.2014', '', -1);");
            var q = source.ExecuteQuery<GT_COUNTER>("select * from gt_counters where markcd IN (99, 109) order by lshet;");

            var lshetDic = Consts.LoadDic();

            var dic = new Dictionary<string, List<CountersRecord>>();

            StepStart(q.Count());
            foreach (GT_COUNTER gtCounter in q)
            {
                if (!lshetDic.ContainsKey(gtCounter.LSHET))
                {
                    Iterate();
                    continue;
                }
                var cr = new CountersRecord();
                var cir = new CntrsindRecord();

                cr.Tag = gtCounter.EQUIPMENTIDENTIFIER;
                cr.Lshet = Consts.GetLs(gtCounter.LSHET, lshetDic);
                cr.Cnttype = 99;
                cr.Cntname = "Код марки неопределен";

                cr.Counterid = gtCounter.COUNTERCD.ToString();
                cr.Name = String.IsNullOrEmpty(gtCounter.COUNTERNAME) ? "" : gtCounter.COUNTERNAME;
                if (!String.IsNullOrEmpty(gtCounter.SERIALNUMBER)) cr.Serialnum = gtCounter.SERIALNUMBER;
                
                Debug.Assert(gtCounter.STATUSCD != null, "gtCounter.STATUSCD != null");
                if (gtCounter.STATUSCD.Value > 0)
                {
                    if (gtCounter.SETUPDATE != null) cr.Setupdate = gtCounter.SETUPDATE.Value;
                }
                else
                {
                    if (gtCounter.SETUPDATE.HasValue) cr.Setupdate = gtCounter.SETUPDATE.Value;
                    if (gtCounter.STATUSDATE.HasValue) cr.Deactdate = gtCounter.STATUSDATE.Value;
                }

                List<CountersRecord> lcr;
                if (!dic.TryGetValue(cr.Lshet, out lcr))
                {
                    lcr = new List<CountersRecord>();
                    dic.Add(cr.Lshet, lcr);
                }
                lcr.Add(cr);

                if (gtCounter.INDICATIONDATE.HasValue)
                {
                    cir.Counterid = cr.Counterid;
                    cir.Inddate = gtCounter.INDICATIONDATE.Value;
                    Debug.Assert(gtCounter.INDICATIONVALUE != null, "gtCounter.INDICATIONVALUE != null");
                    cir.Indication = gtCounter.INDICATIONVALUE.Value;
                    cir.Indtype = IndicationType.Обычные;
                    cir.Ob_em = 0;
                    cir.Oldind = 0;

                    tmdest.InsertRecord(cir.GetInsertScript());
                }
                Iterate();
            }
            StepFinish();

            StepStart(dic.Count);
            foreach (var kvp in dic)
            {
                if (kvp.Value.Count > 1)
                {
                    int setupcounter = 1;
                    foreach (CountersRecord cr in kvp.Value)
                    {
                        if (String.IsNullOrEmpty(cr.Serialnum))
                        {
                            cr.Setupplace = (CounterSetupPlace)setupcounter;
                            setupcounter++;
                        }
                        else if (cr.Serialnum.Trim().ToUpper().Contains("ВАННАЯ"))
                        {
                            cr.Setupplace = CounterSetupPlace.Ванна_туалет;
                            setupcounter = 2;
                        }
                        else if (cr.Serialnum.Trim().ToUpper().Contains("КУХНЯ"))
                        {
                            cr.Setupplace = CounterSetupPlace.Кухня;
                            setupcounter = 3;
                        }
                        else if (cr.Serialnum.Trim().ToUpper().Contains("ДУШ"))
                        {
                            cr.Setupplace = CounterSetupPlace.Душ;
                            setupcounter = 4;
                        }
                        else
                        {
                            cr.Setupplace = (CounterSetupPlace) setupcounter;
                            setupcounter++;
                        }

                    }
                }

                foreach (CountersRecord cr in kvp.Value)
                {
                    tmdest.InsertRecord(cr);
                }
                Iterate();
            }
            source.ExecuteNonQuery("commit work;");
            StepFinish();
        }
    }

    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL.DBF, NACH.DBF - текущее сальдо";
            IsChecked = true;
            Position = 70;
        }

        public override void DoConvert()
        {
            tmdest.CleanTable(typeof(NachoplRecord));

            AbonentEntitiesModel source, destination;
            Consts.InitializeDataSource(out source, out destination);

            SetStepsCount(3);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");

            var lshetDic = Consts.LoadDic();

            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in (" +
                Consts.GetHouseCdList() + "));");
            source.ExecuteNonQuery("execute procedure rep_gt_saldo_itog(1, 3, 2014, 3, 2014, '8,608,208,908,998,708,308,508,108,408,4,404,994,204,904,704,604,304,504,104', '', -1);");
            var q =
                source.ExecuteQuery<GT_SALDO_ITOG>("select * from gt_saldo_itog where endsaldo <> 0 order by lshet;");

            var nm = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_конец);

            var nr = new NachRecord
            {
                Regimcd = 10,
                Regimname = "Неизвестен",
                Type = 0
            };

            StepStart(q.Count());
            foreach (GT_SALDO_ITOG si in q)
            {
                if (!lshetDic.ContainsKey(si.LSHET))
                {
                    Iterate();
                    continue;
                }
                string lshet = Consts.GetLs(si.LSHET, lshetDic);
                int balancekod = si.BALANCE_KOD % 10;
                string balancename = balancekod == 4 ? "Холодное водоснабжение" : "Водоотведение";

                Debug.Assert(si.ENDSALDO != null, "si.ENDSALDO != null");

                nr.Servicecd = balancekod;
                nr.Servicenam = balancename;

                nm.RegisterNach(nr, lshet, 3, 2014, 0, si.ENDSALDO.Value, new DateTime(2014,3,31), "gsi_" + lshet + "_" + si.BALANCE_KOD);
                Iterate();
            }
            StepFinish();
            source.ExecuteNonQuery("commit work;");

            StepStart(1);
            nm.SaveNachRecords(tmdest);
            StepFinish();

            StepStart(1);
            nm.SaveNachoplRecords(tmdest);
            StepFinish();

        }
    }
}
