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

namespace _042_Kirici
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\042_Kirici\Sources\Таблица перекодировки.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("90{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 03;

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
                abonent.ReadDataRow(dataRow);
                if (abonent.Closedate > new DateTime(2000,1,1)) continue;
                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(abonent.Lshet_kod)),
                    EXTLSHET = abonent.Lshet.Trim(),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = 1,
                    DUNAME = "МБУ \"Кирицкое\"",
                    RAYONKOD = 1,
                    RAYONNAME = "Спасский р-н",
                    PRIM_ = abonent.Prim.Trim(),
                    F = abonent.F.Trim(),
                    I = abonent.I.Trim(),
                    O = abonent.O.Trim(),
                    POSTINDEX = abonent.Postindex.Trim(),
                    TOWNSKOD = (int)abonent.Townskod,
                    TOWNSNAME = String.IsNullOrWhiteSpace(abonent.Townsname.Trim()) ? Consts.UnknownTown : abonent.Townsname.Trim(),
                    ULICAKOD = (int)abonent.Ulicakod,
                    ULICANAME = String.IsNullOrWhiteSpace(abonent.Ulicaname.Trim()) ? Consts.UnknownStreet : abonent.Ulicaname.Trim(),
                    FLATNO = Convert.ToInt32(abonent.Kvartira),
                    ISDELETED = (int)abonent.Isdeleted
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

                string flat = abonent.Kvartira.Trim();
                matches = regex.Matches(flat);
                if (matches.Count > 0)
                {
                    var groups = matches[0].Groups;
                    if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                    if (groups.Count > 1)
                    {
                        int flatno;
                        if (Int32.TryParse(groups[1].Value, out flatno)) a.FLATNO = flatno;
                        else a.FLATPOSTFIX = groups[0].Value;
                    }
                }
                lca.Add(a);
                Iterate();
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

                if (String.IsNullOrWhiteSpace(chars.Lshet_kod)) continue;
                if (chars.Value_ < 0) continue;
                if (chars.Date.Year == 14) chars.Date = new DateTime(2014, chars.Date.Month, chars.Date.Day);

                var c = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(chars.Lshet_kod)),
                    VALUE_ = chars.Value_,
                    DATE_ = chars.Date
                };
                switch (chars.Charcd)
                {
                    case 1:
                        c.CHARCD = 2;
                        break;
                    case 2:
                        c.CHARCD = 14;
                        break;
                    case 7:
                        c.CHARCD = 1;
                        break;
                    case 4:
                    case 5:
                    case 8:
                        continue;
                    default:
                        throw new Exception(String.Format("Незивестная характеристика {0} {1}", chars.Charname,
                            chars.Charcd));
                }
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

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.GetDataTable("LCHARS");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист2");
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            var lchar = new LcharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lchar.ReadDataRow(dataRow);

                foreach (DataRow row in recodeTable.Rows)
                {
                    if (row["Value1"] == DBNull.Value || row["Value2"] == DBNull.Value) continue;

                    if (Convert.ToInt32(row["Value1"]) != lchar.Lcharcd ||
                        Convert.ToInt32(row["Value2"]) != lchar.Tarifcd) continue;

                    var lc = new CNV_LCHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(lchar.Lshet_kod)),
                        LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                        LCHARNAME = row["LCHARNAME"].ToString(),
                        VALUEDESC = row["LCHARVALUEDESC"].ToString(),
                        VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                        DATE_ = lchar.Date
                    };
                    llc.Add(lc);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            llc = LcharsRecordUtils.ThinOutList(llc);
            StepFinish();
            SaveList(llc, Consts.InsertRecordCount);
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
            DoubledCID = new List<string>();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            
            var lcn = new List<CNV_COUNTER>();
             StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);
                if (counter.Isdeleted == 1) continue;

                int cnttype;
                switch (counter.Servicecd)
                {
                    case 3:
                        cnttype = 1;
                        break;
                    case 34:
                        cnttype = 3;
                        break;
                    case 0:
                    case 6:
                        continue;
                    default:
                        throw new Exception(String.Format("Неизсветный тип стчетчика {0} {1}", counter.Serialnum,
                            counter.Servicecd));
                }

                var c = new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(counter.Lshet_kod)),
                    CNTTYPE = cnttype,
                    NAME = counter.Name.Trim(),
                    COUNTERID = counter.Counterid.Trim(),
                    DEACTDATE = counter.Deactdate.Year > 2000 ? (DateTime?)counter.Deactdate : null,
                    SETUPDATE = counter.Setupdate.Year > 2000 ? counter.Setupdate : new DateTime(2016,1,1),
                    SERIALNUM = counter.Serialnum.Trim()
                };

                if (lcn.Any(cnt => cnt.COUNTERID == c.COUNTERID))
                {
                    DoubledCID.Add(c.COUNTERID);
                    c.COUNTERID = (Convert.ToInt32(c.COUNTERID) + 2000).ToString("D9");
                }

                lcn.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();
        }

        public static List<string> DoubledCID;
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
            DataTable dt = Tmsource.ExecuteQuery(@"SELECT i.*, RECNO() AS RECNO FROM CNTRSIND i where i.is_rn = 0
                                                    and i.counterid not in (select c.counterid from counters c where c.servicecd = 0 or c.servicecd = 6)");
            var lci = new List<CNV_CNTRSIND>();
            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);
                
                var c = new CNV_CNTRSIND
                {
                    COUNTERID = counterind.Counterid.Trim(),
                    DOCUMENTCD = String.Format("{0}_{1}", counterind.Counterid.Trim().TrimStart('0'), dataRow["RECNO"]),
                    INDDATE = counterind.Inddate,
                    OLDIND = counterind.Previndic,
                    INDICATION = counterind.Indication
                };

                lci.Add(c);

                if (ConvertCounters.DoubledCID.Contains(c.COUNTERID))
                {
                    c.COUNTERID = (Convert.ToInt32(c.COUNTERID) + 2000).ToString("D9");
                    lci.Add(c);
                }

                Iterate();
            }
            StepFinish();

            SaveList(lci, Consts.InsertRecordCount);
        }
    }

    public class ConvertNachisl : ConvertCase
    {
        public ConvertNachisl()
        {
            ConvertCaseName = "Nach - данные о начислениях";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$NACH");
            DataTable dt = Tmsource.ExecuteQuery("select *, RECNO() as RECNO from SUMMS");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            var nach = new SummsRecord();
            StepStart(dt.Rows.Count);
            foreach (DataRow dataRow in dt.Rows)
            {
                nach.ReadDataRow(dataRow);

                string documentcd = String.Format("N{0}_{1}", nach.Lshet_kod.Trim().TrimStart('0'), dataRow["RECNO"]);
                int regimcd = 10;
                string regimname = "Неизвестен";
                int servicecd;
                string servicename;

                switch (nach.Lcharcd)
                {
                    case 2:
                    case 8:
                    case 14:
                    case 15:
                        servicecd = 4;
                        servicename = "Холодная вода";
                        break;
                    case 4:
                    case 12:
                        servicecd = 6;
                        servicename = "Вывоз ТБО";
                        break;
                    case 3:
                    case 13:
                    case 16:
                    case 7:
                        servicecd = 8;
                        servicename = "Канализация";
                        break;
                    case 17:
                        servicecd = 5;
                        servicename = "Горячая вода";
                        break;
                    case 1:
                        continue;
                    default:
                        throw new Exception(String.Format("Неизвестная услуга {0} {1}", nach.Lcharname, nach.Lcharcd));
                }

                var ndef = new CNV_NACH
                {
                    VOLUME = nach.Serv_col,
                    REGIMCD = regimcd,
                    REGIMNAME = regimname,
                    SERVICECD = servicecd,
                    SERVICENAME = servicename,
                    TYPE_ = 0
                };
                nm.RegisterNach(ndef, Consts.GetLs(Convert.ToInt64(nach.Lshet_kod)), nach.Date_deist.Month,
                    nach.Date_deist.Year, nach.Summa, 0, nach.Date_deist, documentcd);
                Iterate();
            }
            StepFinish();

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }
    }

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
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "0" });
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
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "0" });
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
    #endregion
}
