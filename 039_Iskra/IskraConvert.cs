using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _039_Iskra
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\039_Iskra\Source\Таблица перекодировки.xls.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("88{0:D6}", intls);
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
            IsChecked = true;
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            var fioRegex = new Regex(@"([A-ЯA-Z]{1}[а-яa-z]+)[^А-ЯA-Z]*([А-ЯA-Z]{1}[а-яa-z]*)?[^А-ЯA-Z]*([А-ЯA-Z]{1}[а-яa-z]*)?");
            var postIndexRegex = new Regex("39[0-9]{4}");
            var houseFlatRegex = new Regex(@"([0-9]+)(-?([0-9а-яё]+))?[^0-9-]*([0-9]+)?");

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

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
                    DUCD = 1,
                    DUNAME = "МУП ЖКХ Горизонт",
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    PRIM_ = abonent.Prim_.Trim(),
                    F = String.IsNullOrWhiteSpace(abonent.Fio.Trim()) ? "" : abonent.Fio.Trim()
                };

                // Парсинг фио
                var match = fioRegex.Match(a.F);
                if (a.F.ToLower().Contains("ооо") || a.F.ToLower().Contains("общество"))
                {
                    a.F = a.F.Replace("Общество с ограниченной ответственностью", "ООО");
                }
                else if (!a.F.ToLower().Contains("ооо") && !a.F.ToLower().Contains("общество") && match.Groups.Count > 0)
                {
                    var groups = match.Groups;
                    if (groups.Count > 3) a.O = groups[3].Value;
                    if (groups.Count > 2) a.I = groups[2].Value;
                    if (groups.Count > 1) a.F = groups[1].Value;
                }

                #region Парсинг адреса
                string clearedAddress = abonent.Address.Trim().ToLower();

                a.POSTINDEX = postIndexRegex.Match(clearedAddress).Value;

                // Парсинг населенного пункта
                bool addressFound = false;
                foreach (var knownTown in KnownAddress.KnownTowns)
                {
                    foreach (var name in knownTown.ParsingNames)
                    {
                        if (!clearedAddress.Contains(name)) continue;
                        a.TOWNSNAME = knownTown.TrueName + " " + knownTown.TruePrefix;
                        addressFound = true;
                        clearedAddress = new Regex(String.Format(".*({0})", name),
                            RegexOptions.IgnoreCase).Replace(clearedAddress, "");
                        break;
                    }
                    if (addressFound) break;
                }
                if (!addressFound) a.TOWNSNAME = Consts.UnknownTown;

                // Парсинг улицы
                addressFound = false;
                foreach (var knownStreet in KnownAddress.KnownStreets)
                {
                    foreach (var name in knownStreet.ParsingNames)
                    {
                        if (!clearedAddress.Contains(name)) continue;
                        a.ULICANAME = knownStreet.TrueName + " " + knownStreet.TruePrefix;
                        addressFound = true;
                        clearedAddress = new Regex(String.Format(@".*({0})(.*(ул|пер|пл))?", name),
                            RegexOptions.IgnoreCase).Replace(clearedAddress, "");
                        break;
                    }
                    if (addressFound) break;
                }
                if (!addressFound) a.ULICANAME = Consts.UnknownStreet;

                // Парсинг дома и квартиры
                match = houseFlatRegex.Match(clearedAddress);
                if (match.Groups.Count > 1)
                {
                    a.HOUSENO = match.Groups[1].Value;
                    a.HOUSEPOSTFIX = match.Groups[3].Value;
                    int flatno;
                    Int32.TryParse(match.Groups[4].Value, out flatno);
                    if (flatno == 0) a.FLATNO = null;
                    else a.FLATNO = flatno;
                }
                #endregion

                lca.Add(a);
                Iterate();
            }
            StepFinish();

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            Iterate();
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
            IsChecked = true;
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

                var c = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(chars.Lshet)),
                    DATE_ = chars.Date,
                    VALUE_ = chars.Value_
                };

                switch (chars.Charcd)
                {
                    case 3:
                        c.CHARCD = 1;
                        c.CHARNAME = "Число проживающих";
                        break;
                    case 1:
                        c.CHARCD = 2;
                        c.CHARNAME = "Площадь";
                        break;
                    case 2:
                        continue;
                    default:
                        throw new Exception("Неизвестный идентификатор количественной характеристики " + chars.Charcd);
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

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
                    if (row["FIELDVALUE"] == DBNull.Value ||
                        Convert.ToInt64(row["LCHARVALUE"]) == 0) continue;
                    
                    if (Convert.ToInt64(row["FIELDVALUE"]) == lchar.Lcharcd)
                    {
                        var lc = new CNV_LCHAR
                        {
                            LSHET = Consts.GetLs(Convert.ToInt64(lchar.Lshet)),
                            LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                            LCHARNAME = row["LCHARNAME"].ToString(),
                            VALUEDESC = row["LCHARVALUEDESC"].ToString(),
                            VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                            DATE_ = lchar.Date
                        };
                        llc.Add(lc);
                    }
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            DataTable polivAbonets = Tmsource.ExecuteQuery(
                "SELECT lshet FROM ABONENT where fio like '%полив%' or prim_ like '%полив%'");
            var lcn = new List<CNV_COUNTER>();

            StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);

                var c = new CNV_COUNTER
                {
                    COUNTERID = counter.Counterid.Trim(),
                    LSHET = Consts.GetLs(Convert.ToInt64(counter.Lshet)),
                    SETUPDATE = new DateTime(2016, 1, 1),
                    NAME = counter.Name.Trim()
                    //SERIALNUM = ,
                    //SETUPPLACE = ,
                    //PLOMBDATE = ,
                    //PLOMBNAME = ,
                    //LASTPOV = ,
                    //NEXTPOV = ,
                    //PRIM_ = ,
                    //DEACTDATE = ,
                    //TAG = ,
                };

                switch (counter.Servicecd)
                {
                    case 2:
                        c.CNTTYPE = polivAbonets.Select(String.Format("lshet = '{0}'", counter.Lshet)).Length > 0
                            ? 14
                            : 1;
                        break;
                    case 3:
                        continue;
                    default:
                        throw new Exception(String.Format("Неизвестная услуга {0} {1}", counter.Servicecd,
                            counter.Servicenm));
                }
                lcn.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();
        }
    }
    #endregion

    /// <summary>
    /// Представляет известный элемент адреса
    /// </summary>
    public class KnownAddress
    {
        /// <summary>
        /// Список известных имен
        /// </summary>
        public string[] ParsingNames;

        /// <summary>
        /// Используемое имя
        /// </summary>
        public string TrueName;
        /// <summary>
        /// Используемый префикс
        /// </summary>
        public string TruePrefix;

        #region Список известных городов
        public static readonly KnownAddress[] KnownTowns =
        {
            new KnownAddress {ParsingNames = new[] {"букрино"}, TrueName = "Букрино", TruePrefix = SeloPrefix},
            new KnownAddress {ParsingNames = new[] {"шевцово"}, TrueName = "Шевцово", TruePrefix = DerevPrefix},
            new KnownAddress {ParsingNames = new[] {"ялино"}, TrueName = "Ялино", TruePrefix = DerevPrefix},
            new KnownAddress {ParsingNames = new[] {"госплемстанция", "госплемстанции"}, TrueName = "Госплемстанция", TruePrefix = PoselPrefix},
            new KnownAddress {ParsingNames = new[] {"искра"}, TrueName = "Искра", TruePrefix = PoselPrefix},
        };

        public const string SeloPrefix = "с";
        public const string DerevPrefix = "д";
        public const string PoselPrefix = "п";
        #endregion

        #region Список известных улиц
        public static readonly KnownAddress[] KnownStreets =
        { 
            new KnownAddress{ParsingNames = new[] {"прудная"},  TrueName = "Прудная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"центральная"},  TrueName = "Центральная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"железнодорожная"},  TrueName = "Железнодорожная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"школьная"},  TrueName = "Школьная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"молодежная"},  TrueName = "Молодежная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"черновицкая"},  TrueName = "Черновицкая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"комсомольская"},  TrueName = "Комсомольская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"мира"},  TrueName = "Мира", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"юбилейная"},  TrueName = "Юбилейная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"полевая"},  TrueName = "Полевая", TruePrefix =StreetPrefix},  
        };

        public const string StreetPrefix = "ул";
        #endregion
    }
}