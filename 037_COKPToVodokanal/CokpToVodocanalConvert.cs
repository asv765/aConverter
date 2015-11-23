using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _037_COKPToVodokanal
{
    public static class Consts
    {
        /// <summary>
        /// Начальный номер лицевого счета
        /// </summary>
        // public const string StartLshet = "62209438";
        public static int NewLshetCounter = 1;

        public static string GetLs(string oldls, Dictionary<string, string> dic, IQueryable<aConverterClassLibrary.RecordsDataAccessORM.ABONENT> abonents = null)
        {
            string newls;
            if (dic.TryGetValue(oldls, out newls)) return newls;
            if (abonents == null) throw new ArgumentNullException("abonents");
            string maxLshet = abonents.Max(p => p.LSHET);
            newls = (Convert.ToInt32(maxLshet) + NewLshetCounter++).ToString(CultureInfo.InvariantCulture);
            dic.Add(oldls, newls);
            return newls;
        }

        public static Dictionary<int, int> GetHouses()
        {
            string[] ha = File.ReadAllLines(@"C:\Work\aConverter_Data\037_COKPToVodokanal\Документы\СписокДомов.csv");
            var rl = new Dictionary<int, int>();
            foreach (var s in ha)
            {
                string leftpart = s.Split(';')[0];
                int toadd1;
                Int32.TryParse(leftpart, out toadd1);

                string rightpart = s.Split(';')[2];
                int toadd2;
                Int32.TryParse(rightpart, out toadd2);

                rl.Add(toadd1, toadd2);
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

        public static string DocumentsDir = @"C:\Work\aConverter_Data\037_COKPToVodokanal\Документы";

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
                dic.Add(s.Split(';')[0], s.Split(';')[1]);
            }
            return dic;
        }

        public static void InitializeDataSource(out AbonentEntitiesModel source, out AbonentConvertationEntitiesModel destination)
        {
            var fcsbSource = new FbConnectionStringBuilder(aConverter_RootSettings.FirebirdStringConnection)
            {
                Database = @"D:\Work\aConverter_Data\037_COKPToVodokanal\Source\Cokp\ABONENT.FDB"
            };
            source = new AbonentEntitiesModel(fcsbSource.ToString());

            var fcsbDestination = new FbConnectionStringBuilder(aConverter_RootSettings.FirebirdStringConnection)
            {
                Database = @"D:\Work\aConverter_Data\037_COKPToVodokanal\Database\Vodokanal\ABONENT.FDB"
            };
            // destination = new AbonentEntitiesModel(fcsbDestination.ToString());
            destination = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection);
        }

    }

    /// <summary>
    /// Создать буфферные таблицы для конвертации
    /// </summary>
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();
            BufferEntitiesManager.CreateAllEntities();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }
    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "CNV$ABONENT - данные об абонентах";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            AbonentEntitiesModel source;
            AbonentConvertationEntitiesModel destination;
            Consts.InitializeDataSource(out source, out destination);

            #region Заполняем словарь лицевых счетов КВЦ
            var destKvcLshetDic = new Dictionary<string, string>();
            var reverseKvcLshetDic = new Dictionary<string, string>();
            StepStart(destination.EXTORGACCOUNTs.Count(p => p.EXTORGCD == 5));
            foreach (var er in destination.EXTORGACCOUNTs.Where(p => p.EXTORGCD == 5))
            {
                if (!destKvcLshetDic.ContainsKey(er.EXTLSHET.Substring(0, 12)))
                {
                    destKvcLshetDic.Add(er.EXTLSHET.Substring(0, 12), er.LSHET);
                    reverseKvcLshetDic.Add(er.LSHET, er.EXTLSHET);
                }
                Iterate();
            }
            StepFinish();
            #endregion

            var housecdDic = Consts.GetHouses();

            var suitablehouses = Consts.GetHouses().Keys.ToList();

            var q = from a in source.ABONENTs
                    join ea in source.EXTORGACCOUNTs on a.LSHET equals ea.LSHET
                    join h in source.HOUSEs on a.HOUSECD equals h.HOUSECD
                    join s in source.STREETs on h.STREETCD equals s.STREETCD
                    where ea.EXTORGCD == 5 && a.DELETED == 0 && suitablehouses.Contains(h.HOUSECD)
                    select new { ABONENTS = a, HOUSES = h, STREET = s, ea.EXTLSHET };

            var lshetDic = new Dictionary<string, string>();
            var lca = new List<CNV_ABONENT>();
            StepStart(q.Count());
            foreach (var lq in q)
            {
                Debug.Assert(lq.ABONENTS.HOUSECD != null, "lq.ABONENTS.HOUSECD != null");

                var ar = new CNV_ABONENT();

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

                ar.LSHET = lshet;
                ar.EXTLSHET = kvclshet;
                ar.EXTLSHET2 = lq.ABONENTS.LSHET;

                ar.F = lq.ABONENTS.FIO;
                ar.I = lq.ABONENTS.NAME;
                ar.O = lq.ABONENTS.SECOND_NAME;
                ar.DUCD = 9700;
                ar.DUNAME = "ООО ЦОКП";
                if (lq.HOUSES.DISTCD == null)
                    ar.DISTKOD = 0;
                else
                    ar.DISTKOD = Convert.ToInt32(lq.HOUSES.DISTCD);
                if (ar.DISTKOD == 0) ar.DISTNAME = "Неизвестен";
                else if (ar.DISTKOD == 1) ar.DISTNAME = "Октябрьский";
                else if (ar.DISTKOD == 2) ar.DISTNAME = "Московский";
                else if (ar.DISTKOD == 3) ar.DISTNAME = "Советский";
                else if (ar.DISTKOD == 4) ar.DISTNAME = "Железнодорожный";

                ar.TOWNSKOD = 1;
                ar.TOWNSNAME = "г.Рязань";
                ar.RAYONKOD = 1;
                ar.RAYONNAME = "г.Рязань";

                // ar.Phonenum = AdvancedTableAdapterManager.GetString(lq.ABONENTS["TEL"]);
                // ar.Postindex = AdvancedTableAdapterManager.GetString(lq.HOUSES["POSTINDEX"]);
                // ar.Ndoma = (AdvancedTableAdapterManager.GetString(lq.HOUSES["HOUSENO"]) + " " + 
                // AdvancedTableAdapterManager.GetString(lq.HOUSES["HOUSEPOSTFIX"])).Trim();
                // ar.Korpus = AdvancedTableAdapterManager.GetInteger(lq.HOUSES["KORPUSNO"]);

                if (lq.ABONENTS.ABONENTPHONEs.Count > 0) ar.PHONENUM = lq.ABONENTS.ABONENTPHONEs[0].PHONENUMBER;
                if (!String.IsNullOrEmpty(lq.HOUSES.POSTINDEX)) ar.POSTINDEX = lq.HOUSES.POSTINDEX;

                ar.HOUSENO = lq.HOUSES.HOUSENO;
                ar.HOUSEPOSTFIX = lq.HOUSES.HOUSEPOSTFIX;
                ar.KORPUSNO = lq.HOUSES.KORPUSNO;
                ar.KORPUSPOSTFIX = lq.HOUSES.KORPUSPOSTFIX;
                ar.HOUSECD = housecdDic[(int)lq.ABONENTS.HOUSECD];
                ar.FLATNO = lq.ABONENTS.FLATNO;
                ar.FLATPOSTFIX = lq.ABONENTS.FLATPOSTFIX;
                ar.ROOMNO = lq.ABONENTS.ROOMNO;

                // Debug.Assert(lq.HOUSES.STREETCD != null, "lq.HOUSES.STREETCD != null");
                ar.ULICAKOD = lq.HOUSES.HOUSECD == 352 ? 916  : lq.HOUSES.STREETCD;
                ar.ULICANAME = lq.STREET.STREETNM;
                ar.ISDELETED = lq.ABONENTS.DELETED;

                lca.Add(ar);

                Iterate();
            }
            StepFinish();
            Consts.SaveDic(lshetDic);

            StepStart(1);
            destination.Add(lca);
            destination.SaveChanges();
            Iterate();
            StepFinish();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CNV$CHARS - данные о характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            BufferEntitiesManager.DropTableData("CNV$CHARS");

            AbonentEntitiesModel source;
            AbonentConvertationEntitiesModel destination;
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
                var cr = new CNV_CHAR();
                if (!lshetDic.ContainsKey(lq.LSHET))
                {
                    Iterate();
                    continue;
                }
                Debug.Assert(lq.HOUSECD != null, "lq.HOUSECD != null");

                cr.LSHET = Consts.GetLs(lq.LSHET, lshetDic);
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

                cr.CHARCD = charcd;
                cr.CHARNAME = charname;

                cr.DATE_ = lq.ABONENTCCHARDATE;
                Debug.Assert(lq.SIGNIFICANCE != null, "lq.SIGNIFICANCE != null");
                cr.VALUE_ = lq.SIGNIFICANCE.Value;

                destination.Add(cr);
                
                Iterate();
            }
            source.Log.Close();
            source.Log = null;
            StepFinish();

            destination.SaveChanges();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "CNV$LCHARS - данные о характеристиках";
            Position = 50;
            IsChecked = false;
        }


        public override void DoConvert()
        {
            SetStepsCount(1);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");

            AbonentEntitiesModel source;
            AbonentConvertationEntitiesModel destination;
            Consts.InitializeDataSource(out source, out destination);

            SetStepsCount(1);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");

            var lshetDic = Consts.LoadDic();

            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in (" +
                Consts.GetHouseCdList() + "));");
            source.ExecuteNonQuery("execute procedure rep_gt_lcharsabonent(1, '01.11.2015', '', -1);");
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
                DateTime startdate = new DateTime(2015, 11, 1);

                Debug.Assert(lc.SIGNIFICANCE != null, "lc.SIGNIFICANCE != null");

                CNV_LCHAR lcr;

                if (lcharcd == 18)
                {
                    lcr = new CNV_LCHAR
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        DATE_ = startdate,
                        LCHARCD = 18,
                        LCHARNAME = "Тип учета ХВС",
                        LSHET = lshet,
                        VALUE_ = Convert.ToInt32(lc.SIGNIFICANCE.Value),
                        VALUEDESC = lc.LOGICSIGNIFICANCE
                    };
                    destination.Add(lcr);
                }

                if (lcharcd == 3)
                {
                    int lcr2Value = Convert.ToInt32(lc.SIGNIFICANCE.Value) < 0
                        ? 0
                        : Convert.ToInt32(lc.SIGNIFICANCE.Value);
                    int lcr3Value = Convert.ToInt32(lc.SIGNIFICANCE.Value) > 0
                        ? 1
                        : Convert.ToInt32(lc.SIGNIFICANCE.Value);

                    lcr = new CNV_LCHAR
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        DATE_ = startdate,
                        LCHARCD = 2,
                        LCHARNAME = "Благоустройство",
                        LSHET = lshet,
                        VALUE_ = lcr2Value,
                        VALUEDESC = lc.LOGICSIGNIFICANCE
                    };
                    destination.Add(lcr);

                    lcr = new CNV_LCHAR
                    {
                        //Date = lc.ABONENTLCHARDATE,
                        DATE_ = startdate,
                        LCHARCD = 3,
                        LCHARNAME = "Водоснабжение",
                        LSHET = lshet,
                        VALUE_ = lcr3Value,
                        VALUEDESC = lcr3Dic[lcr3Value]
                    };
                    destination.Add(lcr);
                }
            }
            destination.SaveChanges();
            Iterate();
            //source.ExecuteNonQuery("commit work;");
            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "CNV$CONTERS, CNV$CNTRSIND - данные о счетчиках и их показаниях";
            Position = 60;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");

            AbonentEntitiesModel source;
            AbonentConvertationEntitiesModel destination;
            Consts.InitializeDataSource(out source, out destination);

            StepStart(1);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");
            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in (" + Consts.GetHouseCdList() + "));");
            source.ExecuteNonQuery("execute procedure rep_gt_counters(1, '01.10.2015', '', -1);");
            var q = source.ExecuteQuery<GT_COUNTER>("select * from gt_counters where markcd IN (99, 109, 102, 108) order by lshet;");

            var lshetDic = Consts.LoadDic();

            var dic = new Dictionary<string, List<CNV_COUNTER>>();

            StepStart(q.Count());
            foreach (GT_COUNTER gtCounter in q)
            {
                if (!lshetDic.ContainsKey(gtCounter.LSHET))
                {
                    Iterate();
                    continue;
                }
                var cr = new CNV_COUNTER();
                var cir = new CNV_CNTRSIND();

                cr.TAG = gtCounter.EQUIPMENTIDENTIFIER;
                cr.LSHET = Consts.GetLs(gtCounter.LSHET, lshetDic);
                if (gtCounter.MARKCD == 99 || gtCounter.MARKCD == 109)
                {
                    cr.CNTTYPE = 99;
                    cr.CNTNAME = "Счетчик холодной воды";
                }
                else if (gtCounter.MARKCD == 102 || gtCounter.MARKCD == 108)
                {
                    cr.CNTTYPE = 109;
                    cr.CNTNAME = "СГВ-15";
                }
                else
                {
                    throw new Exception("Неизвестный код типа счетчика");
                }

                cr.COUNTERID = gtCounter.COUNTERCD.ToString(CultureInfo.InvariantCulture);
                cr.CNTNAME = String.IsNullOrEmpty(gtCounter.COUNTERNAME) ? "" : gtCounter.COUNTERNAME;
                if (!String.IsNullOrEmpty(gtCounter.SERIALNUMBER)) cr.SERIALNUM = gtCounter.SERIALNUMBER;

                Debug.Assert(gtCounter.STATUSCD != null, "gtCounter.STATUSCD != null");
                if (gtCounter.STATUSCD.Value > 0)
                {
                    if (gtCounter.SETUPDATE != null) cr.SETUPDATE = gtCounter.SETUPDATE.Value;
                }
                else
                {
                    if (gtCounter.SETUPDATE.HasValue) cr.SETUPDATE = gtCounter.SETUPDATE.Value;
                    if (gtCounter.STATUSDATE.HasValue) cr.DEACTDATE = gtCounter.STATUSDATE.Value;
                }

                List<CNV_COUNTER> lcr;
                if (!dic.TryGetValue(cr.LSHET, out lcr))
                {
                    lcr = new List<CNV_COUNTER>();
                    dic.Add(cr.LSHET, lcr);
                }
                lcr.Add(cr);

                if (gtCounter.INDICATIONDATE.HasValue)
                {
                    cir.COUNTERID = cr.COUNTERID;
                    cir.INDDATE = gtCounter.INDICATIONDATE.Value;
                    Debug.Assert(gtCounter.INDICATIONVALUE != null, "gtCounter.INDICATIONVALUE != null");
                    cir.INDICATION = (gtCounter.INDICATIONVALUE.Value % 100000);
                    cir.INDTYPE = 0;
                    cir.OB_EM = 0;
                    cir.OLDIND = 0;
                    cir.DOCUMENTCD = "CI_FACTID_" + gtCounter.INDICATIONCD;

                    destination.Add(cir);
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
                    foreach (CNV_COUNTER cr in kvp.Value)
                    {
                        if (String.IsNullOrEmpty(cr.SERIALNUM))
                        {
                            cr.SETUPPLACE = setupcounter;
                            setupcounter++;
                        }
                        else if (cr.SERIALNUM.Trim().ToUpper().Contains("ВАННАЯ"))
                        {
                            cr.SETUPPLACE = (int)CounterSetupPlace.Ванна_туалет;
                            setupcounter = 2;
                        }
                        else if (cr.SERIALNUM.Trim().ToUpper().Contains("КУХНЯ"))
                        {
                            cr.SETUPPLACE = (int)CounterSetupPlace.Кухня;
                            setupcounter = 3;
                        }
                        else if (cr.SERIALNUM.Trim().ToUpper().Contains("ДУШ"))
                        {
                            cr.SETUPPLACE = (int)CounterSetupPlace.Душ;
                            setupcounter = 4;
                        }
                        else
                        {
                            cr.SETUPPLACE = setupcounter;
                            setupcounter++;
                        }

                    }
                }

                foreach (CNV_COUNTER cr in kvp.Value)
                {
                    destination.Add(cr);
                }
                destination.SaveChanges();
                Iterate();
            }
            // source.ExecuteNonQuery("commit work;");
            StepFinish();
        }
    }

    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "CNV$NACHOPL, CNV$NACH - текущее сальдо";
            IsChecked = false;
            Position = 70;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");

            AbonentEntitiesModel source;
            AbonentConvertationEntitiesModel destination;
            Consts.InitializeDataSource(out source, out destination);

            SetStepsCount(3);

            source.ExecuteNonQuery("update abn$uf_1 au set au.isactive = 1 where au.isactive = 0;");

            var lshetDic = Consts.LoadDic();

            source.ExecuteNonQuery(
                "update abn$uf_1 au set au.isactive = 0 where lshet in (select lshet from abonents where housecd in (" +
                Consts.GetHouseCdList() + "));");
            source.ExecuteNonQuery("execute procedure rep_gt_saldo_itog(1, 10, 2015, 10, 2015, '8,108,208,308,408,508,608,708,998,1008,1108,1208,1308,4,104,204,304,404,504,604,704,994,1004,1104,1204,1304', '', -1);");
            var q =
                source.ExecuteQuery<GT_SALDO_ITOG>("select * from gt_saldo_itog where endsaldo <> 0 order by lshet;");

            var nm = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_конец);

            var nr = new CNV_NACH
            {
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                TYPE_ = 0
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

                nr.SERVICECD = balancekod;
                nr.SERVICENAME = balancename;

                if (Math.Abs(si.ENDSALDO.Value) < 1000000)
                {
                    nm.RegisterNach(nr, lshet, 10, 2015, 0, si.ENDSALDO.Value, new DateTime(2015, 10, 31), "gsi_" + lshet + "_" + si.BALANCE_KOD);
                }
                else
                {
                    File.AppendAllText(Consts.DocumentsDir + "\\BigSaldoLshets.txt",
                        String.Format("Для лицевого счета {0} обнаружено нереальное значение сальдо ({1}) по услуге {2}\r\n",
                        si.LSHET, si.ENDSALDO.Value, si.BALANCE_KOD));
                }
                Iterate();
            }
            StepFinish();
            // source.ExecuteNonQuery("commit work;");

            StepStart(1);
            nm.SaveNachRecords(aConverter_RootSettings.FirebirdStringConnection);
            Iterate();
            StepFinish();

            StepStart(1);
            nm.SaveNachoplRecords(aConverter_RootSettings.FirebirdStringConnection);
            Iterate();
            StepFinish();

        }
    }
}
