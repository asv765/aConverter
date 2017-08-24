using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.Class.ConvertCases;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using static _048_Rgmek.Consts;

namespace _048_Rgmek
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public static readonly string LsRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\lsrecode.csv";
        public static readonly string DuRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\durecode.csv";
        public static readonly string HouseRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\houserecode.csv";
        public static readonly string CnttypeRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\cnttyperecode.csv";
        public static readonly string CounterIdRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\counteridrecode.csv";
        public static readonly string IndtypeRecodeFileName = aConverter_RootSettings.SourceDbfFilePath + @"\Docs\indtyperecode.csv";

        public static readonly Dictionary<long, int> CcharRecode = new Dictionary<long, int>
        {
            {1, 2}, // Общая площадь
            {2, 4}, // Полезная площадь
            {3, 23}, // Число комнат
            {4, 5}, // Площадь нежилых помещений
            {5, 1}, // Число зарегистрированных
            {6, 3}, // Число проживающих
        };

        public static readonly int CurrentMonth = 09;

        public static readonly int CurrentYear = 2017;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";
    }

    public static class Utils
    {
        public static long GetValue(string key, Dictionary<string, long> dic, ref long lastvalue)
        {
            long value;
            if (!dic.TryGetValue(key, out value))
            {
                value = ++lastvalue;
                dic.Add(key, value);
            }
            return value;
        }

        public static void SaveDictionary(Dictionary<string, long> recodedic, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (var kvp in recodedic) sw.WriteLine(kvp.Key + ";" + kvp.Value);
            }
        }

        public static Dictionary<string, long> ReadDictionary(string filename)
        {
            var dic = new Dictionary<string, long>();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    dic.Add(line.Split(';')[0], Convert.ToInt64(line.Split(';')[1]));
                }
            }
            return dic;
        }
    }

    #region Конвертация во временные таблицы

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

    public class ConvertAbonent : DbfConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENTS - конвертация информации об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = new Dictionary<string, long>();
            long lastls = 1010000000;
            var durecode = new Dictionary<string, long>();
            long lastducd = 0;
            var houserecode = new Dictionary<string, long>();
            long lasthousecd = 0;

            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);

                var a = new CNV_ABONENT
                {
                    LSHET = Utils.GetValue(abonent.Lshet, lsrecode, ref lastls).ToString(),
                    EXTLSHET = abonent.Lshet.Trim(),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    PRIM_ = abonent.Address.Trim(),
                    POSTINDEX = abonent.Postindex.Trim()
                };

                a.TOWNSKOD = (int)abonent.Townskod;
                a.TOWNSNAME = abonent.Townsname.Trim();
                a.ULICAKOD = (int)abonent.Ulicakod;
                a.ULICANAME = abonent.Ulicaname.Trim();

                if (abonent.Townskod == 1000)
                {
                    a.RAYONKOD = 1;
                    a.RAYONNAME = "г.Рязань";
                }
                else if (abonent.Townskod == 107)
                {
                    a.RAYONKOD = 2;
                    a.RAYONNAME = "Рязанский район";
                }
                else
                {
                    a.RAYONKOD = 3;
                    a.RAYONNAME = "Неизвестен";
                }

                a.DUCD = (int)Utils.GetValue(abonent.Ducd, durecode, ref lastducd);
                a.DUNAME = abonent.Duname.Trim();

                a.HOUSECD = (int)Utils.GetValue(abonent.Housecd, houserecode, ref lasthousecd);
                a.HOUSENO = abonent.Ndoma.Trim();

                int korpusno;
                if (!String.IsNullOrEmpty(abonent.Korpustip.Trim()))
                {
                    a.HOUSEPOSTFIX = abonent.Korpustip.Substring(0, 3).Trim() + " " + abonent.Korpus.Trim();
                }
                else if (Int32.TryParse(abonent.Korpus, out korpusno))
                {
                    a.KORPUSNO = korpusno;
                }
                else
                {
                    a.HOUSEPOSTFIX = abonent.Korpus.Trim();
                }
                int kvartira;
                if (!Int32.TryParse(abonent.Kvartira, out kvartira))
                    a.FLATNO = kvartira;
                a.ROOMNO = (short)abonent.Komnata;

                lca.Add(a);
                Iterate();
            }
            StepFinish();

            // StepStart(3);
            //AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            //StepFinish();

            Utils.SaveDictionary(lsrecode, LsRecodeFileName);
            Utils.SaveDictionary(durecode, DuRecodeFileName);
            Utils.SaveDictionary(houserecode, HouseRecodeFileName);

            StepStart(1);
            SaveListInsertSQL(lca, InsertRecordCount);
            Iterate();

            //StepStart(lca.Count);
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    foreach (var ca in lca)
            //    {
            //        context.Add(ca);
            //        context.SaveChanges();
            //        Iterate();
            //    }
            //}

            StepFinish();
        }
    }

    public class ConvertChars : DbfConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - конвертация информации об количественных характеристиках";
            Position = 30;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(3);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var cold = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                cold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(cold.Lshet, out lshet))
                {
                    var c = new CNV_CHAR()
                    {
                        LSHET = lshet.ToString(),
                        CHARCD = CcharRecode[cold.Charcd],
                        CHARNAME = cold.Charname.Trim(),
                        DATE_ = cold.Date,
                        VALUE_ = cold.Value_
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            var lccto = CharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lccto);
            StepFinish();
        }
    }

    public class ConvertLChars : DbfConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - конвертация информации о качественных характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(3);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.GetDataTable("LCHARS");
            var lcc = new List<CNV_LCHAR>();

            //SaveList(lca, Consts.InsertRecordCount);

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var lcold = new LcharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lcold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                {
                    var c = new CNV_LCHAR()
                    {
                        LSHET = lshet.ToString(),
                        LCHARCD = (int)lcold.Lcharcd,
                        LCHARNAME = lcold.Lcharname.Trim(),
                        DATE_ = lcold.Date,
                        VALUE_ = (int)lcold.Value_,
                        VALUEDESC = lcold.Valuedesc
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            var lccto = LcharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            StepStart(1);
            BufferEntitiesManager.SaveDataToBufferIBScript(lccto);
            StepFinish();
        }
    }

    public class ConvertCounters : DbfConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - конвертация информации о качественных характеристиках";
            Position = 50;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            var lc = new List<CNV_COUNTER>();

            var cnttyperecode = new Dictionary<string, long>();
            long cnttypecd = 0;
            var counteridrecode = new Dictionary<string, long>();
            long counterid = 0;

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            StepStart(dt.Rows.Count);
            var lcold = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lcold.ReadDataRow(dataRow);

                long lshet;
                if (lsrecode.TryGetValue(lcold.Lshet, out lshet))
                {
                    var c = new CNV_COUNTER()
                    {
                        LSHET = lshet.ToString(),
                        NAME = lcold.Name.Trim(),
                        SERIALNUM = lcold.Serialnum.Trim(),
                        CNTNAME = lcold.Cntname,
                        SETUPDATE = lcold.Setupdate,
                        DEACTDATE = lcold.Deactdate,
                        GUID_ = lcold.Counterid.Trim()
                    };
                    c.CNTTYPE = (int)Utils.GetValue(lcold.Cnttype, cnttyperecode, ref cnttypecd);
                    c.COUNTERID = Utils.GetValue(lcold.Counterid, counteridrecode, ref counterid).ToString();
                    if (lcold.Lastpov.Year > 1950)
                    {
                        c.LASTPOV = lcold.Lastpov;
                        if (lcold.Periodpov > 0)
                            c.NEXTPOV = lcold.Lastpov.AddMonths((int)lcold.Periodpov);
                    }
                    string prim = "";
                    if (lcold.Rgresid > 0)
                        prim += (prim == "" ? "" : ", ") + "RGRESID=" + lcold.Rgresid.ToString();
                    if (lcold.Precision > 0)
                        prim += (prim == "" ? "" : ", ") + "PRECISION=" + lcold.Precision.ToString().Replace(',', '.');
                    if (!String.IsNullOrEmpty(lcold.Amperage))
                        prim += (prim == "" ? "" : ", ") + "AMPERAG=" + lcold.Amperage.Trim();
                    if (!String.IsNullOrEmpty(lcold.Instplace))
                        prim += (prim == "" ? "" : ", ") + "INSTPLACE=" + lcold.Instplace.Trim();
                    c.PRIM_ = prim;

                    //CounterSetupPlace 
                    // c.SETUPPLACE 
                    lc.Add(c);
                }
                Iterate();
            }
            Utils.SaveDictionary(cnttyperecode, CnttypeRecodeFileName);
            Utils.SaveDictionary(counteridrecode, CounterIdRecodeFileName);
            StepFinish();

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }
    }

    public class ConvertCntrsind : DbfConvertCase
    {
        public ConvertCntrsind()
        {
            ConvertCaseName = "CNTRSIND - конвертация информации о показаниях счетчиков";
            Position = 60;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(4);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            var lc = new List<CNV_CNTRSIND>();

            var indtyperecode = new Dictionary<string, long>();
            long indtypecd = 0;
            var counteridrecode = Utils.ReadDictionary(CounterIdRecodeFileName);
            long counterid = 0;

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM CNTRSIND WHERE Indication > 0")));
            using (var reader = Tmsource.ExecuteQueryToReader("SELECT * FROM CNTRSIND WHERE Indication > 0"))
            {
                //StepStart(dt.Rows.Count);
                var cr = new CntrsindRecord();
                while (reader.Read())
                // foreach (DataRow dataRow in dt.Rows)
                {
                    //cr.ReadDataRow(dataRow);
                    cr.Counterid = reader["CounterID"].ToString().Trim();
                    cr.Doc = reader["Doc"].ToString().Trim();
                    cr.Date = Convert.ToDateTime(reader["Date"].ToString());
                    cr.Indication = Convert.ToDecimal(reader["Indication"]);
                    cr.Indtype = reader["Indtype"].ToString().Trim();

                    if (counteridrecode.TryGetValue(cr.Counterid, out counterid))
                    {
                        var c = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid.ToString(),
                            DOCUMENTCD = GetDocumentCd(cr.Doc),
                            INDDATE = cr.Date,
                            INDICATION = cr.Indication
                        };
                        c.INDTYPE = (int)Utils.GetValue(cr.Indtype, indtyperecode, ref indtypecd);
                        //CounterSetupPlace 
                        // c.SETUPPLACE 
                        lc.Add(c);
                    }
                    Iterate();
                }
            }
            Utils.SaveDictionary(indtyperecode, IndtypeRecodeFileName);
            StepFinish();

            StepStart(1);
            var lc2 = CntrsindRecordUtils.ThinOutList(lc);
            StepFinish();

            StepStart(1);
            CntrsindRecordUtils.RestoreHistory(ref lc2, RestoreHistoryType.С_конца_по_конечным_показаниям);
            StepFinish();

            StepStart(1);
            string s = lc[0].InsertSql;
            BufferEntitiesManager.SaveDataToBufferIBScript(lc);
            StepFinish();
        }

        private string GetDocumentCd(string doc)
        {
            if (doc.StartsWith("Акт на установку"))
            {
                return "АУ " + doc.Substring(49, 10);
            }
            else if (doc.StartsWith("Акт приемки"))
            {
                return "АП " + doc.Substring(32, 10);
            }
            else if (doc.StartsWith("Акт снятия"))
                return "АС " + doc.Substring(21, 10);
            else
                throw new Exception("Неожиданное наименование документа о снятии показаний:\r\n" +
                    doc);
        }
    }

    public class ConvertMoney : DbfConvertCase
    {
        public ConvertMoney()
        {
            ConvertCaseName = "SUMS, PAYMENT, NACH - конвертация денег";
            Position = 70;
            IsChecked = false;
        }

        public override void DoDbfConvert()
        {
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$NACH");
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");

            var nom = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_конец);

            var lsrecode = Utils.ReadDictionary(LsRecodeFileName);

            CNV_NACH defcn = new CNV_NACH();
            defcn.REGIMCD = 10;
            defcn.REGIMNAME = "Неизвестен";
            defcn.SERVICECD = 9;
            defcn.SERVICENAME = "Электроэнергия";

            StepStart(Convert.ToInt32(Tmsource.ExecuteScalar("SELECT COUNT(*) FROM NACH")));
            using (var reader = Tmsource.ExecuteQueryToReader("SELECT * FROM NACH"))
            {
                var nr = new NachRecord();
                while (reader.Read())
                {
                    //cr.ReadDataRow(dataRow);
                    nr.Lshet = reader["Lshet"].ToString().Trim();
                    string doc = reader["Doc"].ToString().Trim();

                    nr.Doc = Regex.Match(doc, "(?<= счета).*(?= от)").Value;

                    nr.Date = Convert.ToDateTime(reader["Date"].ToString());
                    nr.Date_rasch = Convert.ToDateTime(reader["Date_rasch"].ToString());
                    nr.Resourcd = Convert.ToInt32(reader["Resourcd"]);
                    nr.Resournm = reader["Resournm"].ToString().Trim();
                    nr.Nachtype = reader["Nachtype"].ToString().Trim();
                    nr.Summa = Convert.ToDecimal(reader["Summa"]);
                    nr.Rasctype = reader["Rasctype"].ToString().Trim();

                    long lshet;

                    if (lsrecode.TryGetValue(nr.Lshet, out lshet))
                    {
                        decimal fnath = 0;
                        decimal prochl = 0;
                        if (nr.Nachtype == "Основной")
                            fnath = nr.Summa;
                        else
                            prochl = nr.Summa;

                        if (nr.Rasctype == "Аналитический" ||
                            nr.Rasctype == "Общедомовые нужды" ||
                            nr.Rasctype == "Без прибора")
                            defcn.TYPE_ = 0;
                        else
                            defcn.TYPE_ = 1;

                        nom.RegisterNach(defcn, lshet.ToString(), nr.Date.Month, nr.Date.Year,
                            fnath, prochl, nr.Date, nr.Doc);
                    }
                    Iterate();
                }
            }
            StepFinish();

            StepStart(nom.NachRecords.Count);
            BufferEntitiesManager.SaveDataToBufferIBScript(nom.NachRecords);
            StepFinish();
        }
    }

    #endregion

    #region Перенос данных в целевые таблицы

    public class SplitterTransfer : ConvertCase
    {
        public SplitterTransfer()
        {
            ConvertCaseName = "";
            Position = 998;
            IsChecked = false;
        }

        public override void DoConvert()
        {

        }
    }

    public class TransferAddressObjects : ConvertCase
    {
        public TransferAddressObjects()
        {
            ConvertCaseName = "Перенос данных об адресных объектах";
            Position = 1000;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(7);

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00150_SETTLEMENT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00200_PUNKT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00300_STREET");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00400_DISTRICT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00500_INFORMATIONOWNERS");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_00600_HOUSES");
            StepFinish();
        }
    }

    public class TransferAbonents : KvcConvertCase
    {
        public TransferAbonents()
        {
            ConvertCaseName = "Перенос данных об абонентах";
            Position = 1010;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
            Iterate();
            StepFinish();
        }
    }


    public class TransferExtlshet : KvcConvertCase
    {
        public TransferExtlshet()
        {
            ConvertCaseName = "Перенос данных о внешних лицевых счетах";
            Position = 1015;
            IsChecked = false;

        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "1", "0" });
            Iterate();
        }
    }

    public class TransferChars : KvcConvertCase
    {
        public TransferChars()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках";
            Position = 1020;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferLchars : KvcConvertCase
    {
        public TransferLchars()
        {
            ConvertCaseName = "Перенос данных о качественных характеристиках";
            Position = 1030;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "0", "0" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferCounters : KvcConvertCase
    {
        public TransferCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках и показаниях";
            Position = 1040;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0", "1", "0" });
            StepFinish();
        }
    }

    public class TransferNachopl : KvcConvertCase
    {
        public TransferNachopl()
        {
            ConvertCaseName = "Перенос данных о истории оплат и начислений";
            Position = 1070;
            IsChecked = false;
        }

        public override void DoKvcConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(5);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            Iterate();
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { CurrentYear.ToString(CultureInfo.InvariantCulture),
                CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            StepFinish();
        }
    }

    #endregion
}
