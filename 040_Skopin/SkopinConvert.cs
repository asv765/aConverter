using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _040_Skopin
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\040_Skopin\Sources\Таблица перекодировки.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("89{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 02;

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

                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(abonent.Lshet)),
                    EXTLSHET = abonent.Lshet.Trim(),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = Convert.ToInt32(abonent.Ducd),
                    DUNAME = abonent.Duname,
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    TOWNSNAME = String.IsNullOrWhiteSpace(abonent.Townsname) ? Consts.UnknownTown : abonent.Townsname.Trim(),
                    TOWNSKOD = (int)abonent.Townskod,
                    ULICANAME = abonent.Ulicaname.Trim().Replace(" ул.", ""),
                    ULICAKOD = (int)abonent.Ulicakod,
                    F = abonent.F.Trim(),
                    I = abonent.I.Trim(),
                    O = abonent.O.Trim(),
                    PRIM_ = abonent.Prim
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
            //AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            //Iterate();
            //AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            //Iterate();
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
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Характеристики");
            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var chars = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                chars.ReadDataRow(dataRow);

                foreach (DataRow row in recodeTable.Rows)
                {
                    if (row["Старая система"].ToString() != chars.Charcd.ToString() || row["Абонент"] == DBNull.Value) continue;

                    var c = new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(chars.Lshet)),
                        CHARCD = Convert.ToInt32(row["Абонент"]),
                        VALUE_ = chars.Value_,
                        DATE_ = chars.Date
                    };
                    lcc.Add(c);
                    break;
                }

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
            DataTable dt = Tmsource.ExecuteQuery("SELECT * FROM AB_TARIF a where a.nachcd = (select max(f.nachcd) from ab_tarif f where f.lshet = a.lshet and f.grtarifcd = a.grtarifcd and f.date = a.date)");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Параметры");
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            var abtarif = new Ab_tarifRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abtarif.ReadDataRow(dataRow);

                foreach (DataRow row in recodeTable.Rows)
                {
                    if (row["FIELDVALUE1"] == DBNull.Value || row["FIELDVALUE2"] == DBNull.Value ||
                        row["LCHARNAME"] == DBNull.Value || row["LCHARVALUEDESC"] == DBNull.Value) continue;

                    if (Convert.ToInt32(row["FIELDVALUE1"]) != abtarif.Grtarifcd ||
                        Convert.ToInt32(row["FIELDVALUE2"]) != abtarif.Tarifcd ||
                        Convert.ToInt32(row["FIELDVALUE3"]) != abtarif.Value_) continue;

                    var lc = new CNV_LCHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(abtarif.Lshet)),
                        LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                        LCHARNAME = row["LCHARNAME"].ToString(),
                        VALUE_ = Convert.ToInt32(row["FIELDVALUE3"]) == 1 ? 0 : Convert.ToInt32(row["LCHARVALUE"]),
                        VALUEDESC = row["LCHARVALUEDESC"].ToString(),
                        DATE_ = abtarif.Date
                    };
                    llc.Add(lc);
                }
                Iterate();
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
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            DataTable disabledCounters = Tmsource.ExecuteQuery("SELECT * FROM CNTState where isenable = 0");
            var lcn = new List<CNV_COUNTER>();

            StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);

                if (counter.Isdeleted == 1) continue;

                var disabledCounter = disabledCounters.Select(String.Format("counterid = '{0}'", counter.Counterid));
                DateTime? deactdate = disabledCounter.Length > 0 ? (DateTime?) disabledCounter[0]["date"] : null;
                var c = new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(counter.Lshet)),
                    CNTTYPE = 1,
                    NAME = counter.Name.Trim(),
                    PRIM_ = counter.Prim.Trim(),
                    COUNTERID = counter.Counterid.Trim(),
                    DEACTDATE = deactdate,
                    SERIALNUM = counter.Serialnum.Trim(),
                    SETUPDATE = counter.Reldate
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

            DataTable badCounters = Tmsource.ExecuteQuery("SELECT counterid FROM COUNTERS where isdeleted = 1");

            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);

                if (badCounters.Select(String.Format("COUNTERID = '{0}'", counterind.Counterid)).Length > 0) continue;

                var c = new CNV_CNTRSIND
                {
                    COUNTERID = counterind.Counterid.Trim(),
                    DOCUMENTCD = String.Format("{0}_{1}", counterind.Counterid.Trim(), dataRow["RECNO"]),
                    INDDATE = counterind.Inddate,
                    INDTYPE = 0,
                    OLDIND = counterind.Begind,
                    INDICATION = counterind.Indication + counterind.Begind
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
    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL - данные истории начислений";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$NACH");
            DataTable dt = Tmsource.ExecuteQuery("SELECT *, RECNO() as RECNO FROM Summs where rastypecd = 5");

            var lnh = new List<CNV_NACH>();
            var sum = new SummsRecord();
            StepStart(dt.Rows.Count);
            foreach (DataRow dataRow in dt.Rows)
            {
                sum.ReadDataRow(dataRow);

                int servicecd;
                string servicename;
                switch (sum.Servicecd)
                {
                    case 0:
                        continue;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        servicecd = 13;
                        servicename = "Выпойка";
                        break;
                    case 8:
                        servicecd = 8;
                        servicename = "Канализация";
                        break;
                    case 11:
                        servicecd = 6;
                        servicename = "Вывоз отходов";
                        break;
                    case 12:
                        servicecd = 1;
                        servicename = "Найм";
                        break;
                    case 13:
                        servicecd = 3;
                        servicename = "Отопление";
                        break;
                    case 14:
                        servicecd = 12;
                        servicename = "Полив";
                        break;
                    case 15:
                        servicecd = 9;
                        servicename = "Электроэнергия";
                        break;
                    case 16:
                        servicecd = 2;
                        servicename = "Содержание жилья";
                        break;
                    case 17:
                        servicecd = 4;
                        servicename = "Холодная вода";
                        break;
                    default:
                        throw new Exception("Неизвестная услуга с кодом " + sum.Servicecd);
                }

                var nach = new CNV_NACH
                {
                    SERVICECD = servicecd,
                    TYPE_ = 0,
                    LSHET = Consts.GetLs(Convert.ToInt64(sum.Lshet)),
                    DOCUMENTCD = String.Format("{0}_{1}", sum.Lshet.Trim(), dataRow["RECNO"]),
                    DATE_VV = sum.Date,
                    FNATH = sum.Summa,
                    REGIMCD = 10,
                    MONTH_ = sum.Date.Month,
                    MONTH2 = sum.Date.Month,
                    YEAR2 = sum.Date.Year,
                    YEAR_ = sum.Date.Year,
                    REGIMNAME = "Неизвестен",
                    SERVICENAME = servicename
                };

                lnh.Add(nach);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lnh, Consts.InsertRecordCount);
            StepFinish();
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
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0" });
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
