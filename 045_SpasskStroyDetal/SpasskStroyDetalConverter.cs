using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using _044_TeplComp;

namespace _045_SpasskStroyDetal
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\045_SpasskStroyDetal\Sources\Таблица перекодировки (4).xlsx";

        public static string GetLs(string grkod, string lshet)
        {
            return String.Format("95{0}{1}", String.IsNullOrWhiteSpace(grkod) ? "9999" : grkod.Substring(4, 4), lshet.Substring(4, 4));
        }

        public static readonly int CurrentMonth = 06;

        public static readonly int CurrentYear = 2016;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";
    }

    /// <summary>
    /// Создать базу данных для конвертации
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

    #region Конвертация

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();
            var regex = new Regex(@"(\d+)(.*)");
            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                try
                {
                    abonent.ReadDataRow(dataRow);

                    var a = new CNV_ABONENT
                    {
                        LSHET = Consts.GetLs(abonent.Grkod.Trim(), abonent.Lshet.Trim()),
                        EXTLSHET = String.Format("{0}_{1}", abonent.Grkod.Trim(), abonent.Lshet.Trim()),
                        DISTKOD = (int) abonent.Distkod,
                        DISTNAME = abonent.Distname.Trim(),
                        DUCD = (int) abonent.Ducd,
                        DUNAME = abonent.Duname.Trim(),
                        RAYONKOD = 1,
                        RAYONNAME = "Спасский р-н",
                        PRIM_ = abonent.Prim_.Trim(),
                        F = abonent.F.Trim(),
                        I = abonent.I.Trim(),
                        O = abonent.O.Trim(),
                        POSTINDEX = abonent.Postindex.Trim(),
                        TOWNSKOD = (int) abonent.Townskod,
                        TOWNSNAME = abonent.Townsname.Trim(),
                        ULICAKOD = (int) abonent.Ulicakod,
                        ULICANAME = abonent.Ulicaname.Trim(),
                        ISDELETED = (int) abonent.Isdeleted,
                        PHONENUM = abonent.Phonenum
                    };

                    string house = abonent.Ndoma.Trim();
                    var matches = regex.Matches(house);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) a.HOUSEPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int houseno;
                            if (Int32.TryParse(groups[1].Value, out houseno)) a.HOUSENO = groups[1].Value;
                            else a.HOUSEPOSTFIX = groups[0].Value;
                        }
                    }

                    string kvartira = abonent.Kvartira.Trim();
                    matches = regex.Matches(kvartira);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int flatno;
                            if (Int32.TryParse(groups[1].Value, out flatno)) a.FLATNO = Convert.ToInt32(groups[1].Value);
                            else a.FLATPOSTFIX = groups[0].Value;
                        }
                    }

                    if (a.HOUSEPOSTFIX != null && a.HOUSEPOSTFIX.Length > 10) a.HOUSEPOSTFIX = a.HOUSEPOSTFIX.Substring(0, 10);
                    lca.Add(a);
                    Iterate();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            StepFinish();

            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    /// <summary>
    /// Конвертирует данные о количественных характеристиках
    /// </summary>
    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - данные о количественных характеристиках";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var chars = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                chars.ReadDataRow(dataRow);

                int charcd;
                switch (chars.Charcd)
                {
                    case 1:
                        charcd = 1;
                        break;
                    case 2:
                        charcd = 2;
                        break;
                    case 3:
                        charcd = 4;
                        break;
                    default:
                        throw new Exception("Неизвестная характеристика " + chars.Charcd);
                }

                var c = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(chars.Grkod, chars.Lshet),
                    CHARCD = charcd,
                    VALUE_ = chars.Value_,
                    DATE_ = chars.Date
                };
                lcc.Add(c);

                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc = CharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            StepStart(1);
            SaveList(lcc, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    /// <summary>
    /// Конвертация качественных характеристик
    /// Данные в таблице кодировки должны быть отсортированы по исходному ID!
    /// </summary>
    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - данные о параметрах потребления";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(4);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.ExecuteQuery("select distinct * from contr_1 c inner join lchars l on (c.grkod = l.grkod) and (c.lshet = l.lshet) and	(c.CONTRACTCD = l.LCHARCD) order by l.grkod, l.lshet, l.date desc, contractcd desc");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист1");
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            string notIn = "";
            int i = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow[0].ToString().Contains("00017") && dataRow[1].ToString().Contains("00013") && dataRow[4].ToString() == "10")
                {
                    int a = 10;
                }
                if (i > 0 && dataRow[0].ToString() == dt.Rows[i - 1][0].ToString() && dataRow[1].ToString() == dt.Rows[i - 1][1].ToString() &&
                    dataRow["SERVICECD"].ToString() == dt.Rows[i - 1]["SERVICECD"].ToString() &&
                    dataRow["TARIFCD"].ToString() == dt.Rows[i - 1]["TARIFCD"].ToString() && dataRow[15].ToString() == dt.Rows[i - 1][15].ToString())
                {
                    if (Convert.ToInt32(dataRow[2].ToString()) < Convert.ToInt32(dt.Rows[i - 1][2].ToString()))
                    {
                        Iterate();
                        i++;
                        continue;
                    }
                }
                bool notFound =true;
                foreach (DataRow row in recodeTable.Rows)
                {
                    if (row["Value1"] == DBNull.Value && row["Value2"] == DBNull.Value &&
                        row["Value3"] == DBNull.Value) break;

                    if (Int32.Parse(row["Value1"].ToString()) == Int32.Parse(dataRow["SERVICECD"].ToString()) && 
                        Int32.Parse(row["Value2"].ToString()) == Int32.Parse(dataRow["TARIFCD"].ToString()) && 
                        Int32.Parse(row["Value3"].ToString()) == Int32.Parse(dataRow["VALUE"].ToString()))
                    {
                       
                        var lc = new CNV_LCHAR
                        {
                            LSHET = Consts.GetLs(dataRow[0].ToString(), dataRow[1].ToString()),
                            LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                            VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                            DATE_ = DateTime.Parse(dataRow[15].ToString())
                        };
                        llc.Add(lc);
                        notFound = false;
                    }
                }
                if (notFound)
                {
                    notIn += String.Format("{0}\t{1}\t{2}\t{3}\t{4}\r\n", dataRow[0], dataRow[1], dataRow["SERVICECD"],
                        dataRow["TARIFCD"], dataRow["VALUE"]);
                }
                Iterate();
                i++;
            }
            StepFinish();

            StepStart(1);
            llc = LcharsRecordUtils.ThinOutList(llc);
            StepFinish();

            StepStart(1);
            SaveList(llc, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    /// <summary>
    /// Конвертация данных о счетчиках
    /// </summary>
    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - данные о счетчиках";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.ExecuteQuery("select distinct c.*, c2.SERVICECD, c2.SERVICENM from counters c inner join contr_2 c2 on c2.GRKOD = c.GRKOD and c2.lshet = c.lshet and c2.counterid = c.counterid");
            var lcn = new List<CNV_COUNTER>();

            StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);

                int cnttype;
                switch (dataRow["SERVICECD"].ToString())
                {
                    case "10":
                        cnttype = 3177;
                        break;

                    case "27":
                        cnttype = 1;
                        break;
                        
                    default:
                        throw new Exception("Неизвестный тип счетчика " + dataRow["SERVICECD"]);
                }

                var c = new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(counter.Grkod, counter.Lshet),
                    CNTTYPE = cnttype,
                    NAME = counter.Name.Trim(),
                    PLOMBDATE = counter.Plombdate,
                    COUNTERID =
                        String.Format("{0}{1}{2}", counter.Grkod.Trim('0').Trim(), counter.Lshet.Trim('0').Trim(),
                            counter.Counterid.ToString().Trim())
                };

                lcn.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    /// <summary>
    /// Конвертация данных о показаниях счетчиков
    /// </summary>
    public class ConvertCntrsind : ConvertCase
    {
        public ConvertCntrsind()
        {
            ConvertCaseName = "CNTRSIND - данные о показаниях счетчиках";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DataTable dt = Tmsource.ExecuteQuery("SELECT *, RECNO() AS RECNO FROM CNTRSIND");
            var lci = new List<CNV_CNTRSIND>();

            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);

                var c = new CNV_CNTRSIND
                {
                    COUNTERID = String.Format("{0}{1}{2}", counterind.Grkod.Trim('0').Trim(), counterind.Lshet.Trim('0').Trim(),
                            counterind.Counterid.ToString().Trim()),
                    DOCUMENTCD = String.Format("{0}_{1}", String.Format("{0}{1}{2}", counterind.Grkod.Trim('0').Trim(), counterind.Lshet.Trim('0').Trim(),
                            counterind.Counterid.ToString().Trim()), dataRow["RECNO"]),
                    INDDATE = counterind.Inddate,
                    INDTYPE = 0,
                    OLDIND = counterind.Indication - counterind.Count,
                    INDICATION = counterind.Indication
                };
                lci.Add(c);

                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lci, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    /// <summary>
    /// Конвертация данных истории начислений
    /// </summary>
    //public class ConvertNachopl : ConvertCase
    //{
    //    public ConvertNachopl()
    //    {
    //        ConvertCaseName = "NACHOPL - данные истории начислений";
    //        Position = 70;
    //        IsChecked = false;
    //    }

    //    public override void DoConvert()
    //    {
    //        var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
    //        tms.Init();

    //        SetStepsCount(2);

    //        BufferEntitiesManager.DropTableData("CNV$NACH");
    //        DataTable dt = Tmsource.ExecuteQuery("SELECT *, RECNO() as RECNO FROM register");

    //        NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
    //        var reg = new RegisterRecord();
    //        StepStart(dt.Rows.Count);
    //        foreach (DataRow dataRow in dt.Rows)
    //        {
    //            reg.ReadDataRow(dataRow);

    //            if (reg.Dt_schet != "62.СО.1" || reg.Kt_schet != "62.СО.1") continue;

    //            string documentcd;
    //            string lshet;
    //            int servicecd;
    //            string servicename;
    //            if (reg.Dt_schet == "62.СО.1")
    //            {
    //                lshet = "";
    //                documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);;
    //                servicecd = 0;
    //                servicename = "";

    //            var ndef = new CNV_NACH
    //            {
    //                //VOLUME = tarif == 0 ? 0 : nachopl.Fnath / tarif,
    //                VOLUME = 0,
    //                REGIMCD = 10,
    //                REGIMNAME = "Неизвестен",
    //                SERVICECD = servicecd,
    //                SERVICENAME = servicename,
    //                TYPE_ = 0
    //            };
    //            nm.RegisterNach(ndef, Consts.GetLs(Convert.ToInt64(lshet), (int)nachopl.Month,
    //                (int)nachopl.Year, nachopl.Fnath, nachopl.Prochl,
    //                new DateTime((int)nachopl.Year, (int)nachopl.Month, 1), documentcd));
    //            }
    //            else
    //            {
    //                lshet = "";
    //                documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);;
    //                servicecd = 0;
    //                servicename = "";

    //            var odef = new CNV_OPLATA
    //            {
    //                SERVICECD = servicecd,
    //                SERVICENAME = servicename,
    //                SOURCECD = 17,
    //                SOURCENAME = "Касса"
    //            };
    //            nm.RegisterOplata(odef, Consts.GetLs(Convert.ToInt64(lshet), (int) nachopl.Month,
    //                (int) nachopl.Year, nachopl.Oplata, new DateTime((int) nachopl.Year, (int) nachopl.Month, 1),
    //                new DateTime((int) nachopl.Year, (int) nachopl.Month, 1), documentcd));
    //            }


    //            Iterate();
    //        }
    //        StepFinish();

    //        //SaveList(lnh, Consts.InsertRecordCount);
    //    }
    //}
#endregion

    #region Перенос данных из временных таблиц
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
            StepStart(6);

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
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
            Iterate();

            StepFinish();
        }
    }

    public class TransferAbonents : ConvertCase
    {
        public TransferAbonents()
        {
            ConvertCaseName = "Перенос данных об абонентах";
            Position = 1010;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
            Iterate();
            StepFinish();
        }
    }

    public class TransferChars : ConvertCase
    {
        public TransferChars()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках";
            Position = 1020;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferLchars : ConvertCase
    {
        public TransferLchars()
        {
            ConvertCaseName = "Перенос данных о качественных характеристиках";
            Position = 1030;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferCounters : ConvertCase
    {
        public TransferCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках";
            Position = 1040;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            //fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
            //Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "1" });
            Iterate();
        }
    }

    public class TransferNachisl : ConvertCase
    {
        public TransferNachisl()
        {
            ConvertCaseName = "Перенос данных о начислениях";
            Position = 1070;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
        }
    }

    public class TransferOplata : ConvertCase
    {
        public TransferOplata()
        {
            ConvertCaseName = "Перенос данных об оплате";
            Position = 1050;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            Iterate();
        }
    }

    public class TransferSaldo : ConvertCase
    {
        public TransferSaldo()
        {
            ConvertCaseName = "Перенос данных о сальдо";
            Position = 1060;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
                Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
        }
    }

    public class TransferPererashet : ConvertCase
    {
        public TransferPererashet()
        {
            ConvertCaseName = "Перерасчет";
            Position = 1080;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            Iterate();
        }
    }

    #endregion
}
