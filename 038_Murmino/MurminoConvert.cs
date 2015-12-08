using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _038_Murmino
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\038_Murmino\Source\Таблица перекодировки2.xls";
        public static string GetLs(long intls)
        {
            return String.Format("86{0:D6}", intls);
        }
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
                    DUCD = Convert.ToInt32(abonent.Ducd),
                    DUNAME = abonent.Duname.Trim(),
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    PRIM_ = abonent.Prim_.Trim(),
                    F = abonent.Fio.Trim()
                };

                // Парсинг ФИО
                var regex = new Regex(@"(\.|\,| )+((([^., ]+)(\.|\,| )*)?){2}$", RegexOptions.Multiline);
                var matches = regex.Matches(a.F);
                if (matches.Count > 0)
                {
                    a.F = regex.Replace(a.F, "");
                    var captures = matches[0].Groups[4].Captures;
                    if (captures.Count > 0)
                    {
                        a.I = captures[0].Value;
                        if (captures.Count > 1) a.O = captures[1].Value;
                    }
                }

                #region Парсинг адреса
                string clearedAddress = abonent.Address.Trim().ToLower();

                // Парсинг индекса
                regex = new Regex("39[0-9]{4}");
                a.POSTINDEX = regex.Match(clearedAddress).Value;

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

                regex = new Regex(@"([0-9]+)(-?([0-9а-яё]+))?[^0-9-]*([0-9]+)?");
                var match = regex.Match(clearedAddress);
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

            StepStart(lca.Count / Consts.InsertRecordCount + 1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < lca.Count; i++)
                {
                    acem.Add(lca[i]);
                    if ((i != 0 && i % Consts.InsertRecordCount == 0) || i == lca.Count - 1)
                    {
                        acem.SaveChanges();
                        Iterate();
                    }
                }
            }
            StepFinish();
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

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Cchars");
            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var chars = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                chars.ReadDataRow(dataRow);

                int charCd = 0;
                string charName = null;
                foreach (DataRow row in recodeTable.Rows)
                {
                    if (Convert.ToInt64(row["FIELDVALUE"]) != chars.Charcd) continue;
                    charCd = Convert.ToInt32(row["CCHARCD"]);
                    charName = row["CCHARNAME"].ToString();
                    break;
                }

                if (charCd == 0 || String.IsNullOrWhiteSpace(charName))
                    throw new Exception("Неизвестный идентификатор количественной характеристики " + chars.Charcd);

                var c = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(chars.Lshet)),
                    CHARCD = charCd,
                    CHARNAME = charName,
                    DATE_ = chars.Date,
                    VALUE_ = chars.Value_
                };

                lcc.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(lcc.Count / Consts.InsertRecordCount + 1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < lcc.Count; i++)
                {
                    acem.Add(lcc[i]);
                    if ((i != 0 && i % Consts.InsertRecordCount == 0) || i == lcc.Count - 1)
                    {
                        acem.SaveChanges();
                        Iterate();
                    }
                }
            }
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

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.GetDataTable("ABTARIFS");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Lchars");
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            var lchar = new AbtarifsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lchar.ReadDataRow(dataRow);

                bool recodeFound = false;
                foreach (DataRow row in recodeTable.Rows)
                {
                    if (Convert.ToInt64(row["FIELDVALUE"]) != lchar.Grtarifcd)
                    {
                        if (recodeFound) break;
                        continue;
                    }
                    recodeFound = true;
                    var lc = new CNV_LCHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(lchar.Lshet)),
                        LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                        LCHARNAME = row["LCHARNAME"].ToString(),
                        VALUEDESC = row["LCHARVALUEDESC"].ToString(),
                        VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                        DATE_ = new DateTime(2015, 1, 1)
                    };
                    llc.Add(lc);
                }
                Iterate();
            }
            StepFinish();

            StepStart(llc.Count / Consts.InsertRecordCount + 1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < llc.Count; i++)
                {
                    acem.Add(llc[i]);
                    if ((i != 0 && i % Consts.InsertRecordCount == 0) || i == llc.Count - 1)
                    {
                        acem.SaveChanges();
                        Iterate();
                    }
                }
            }
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
            var lcn = new List<CNV_COUNTER>();

            StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);

                var c = new CNV_COUNTER
                {
                    COUNTERID = counter.Lshet.Trim() + "_" + counter.Counterid.Trim(),
                    LSHET = Consts.GetLs(Convert.ToInt64(counter.Lshet)),
                    CNTTYPE = 106,
                    CNTNAME = "Счетчик холодной воды",
                    SETUPDATE = new DateTime(2014,1,1),
                    //SERIALNUM = ,
                    //SETUPPLACE = ,
                    //PLOMBDATE = ,
                    //PLOMBNAME = ,
                    //LASTPOV = ,
                    //NEXTPOV = ,
                    //PRIM_ = ,
                    //DEACTDATE = ,
                    //TAG = ,
                    NAME = counter.Name
                };

                lcn.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(lcn.Count / Consts.InsertRecordCount + 1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < lcn.Count; i++)
                {
                    acem.Add(lcn[i]);
                    if ((i != 0 && i % Consts.InsertRecordCount == 0) || i == lcn.Count - 1)
                    {
                        acem.SaveChanges();
                        Iterate();
                    }
                }
            }
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DataTable dt = Tmsource.GetDataTable("CNTRSIND");
            var lci = new List<CNV_CNTRSIND>();

            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);

                var c = new CNV_CNTRSIND
                {
                    COUNTERID = counterind.Lshet.Trim() + "_" + counterind.Counterid.Trim(),
                    //DOCUMENTCD = ,
                    //OLDIND = ,
                    //OB_EM = ,
                    INDICATION = counterind.Indication,
                    INDDATE = counterind.Inddate,
                    INDTYPE = 0,
                };

                lci.Add(c);
                Iterate();
            }
            StepFinish();

            StepStart(lci.Count / Consts.InsertRecordCount + 1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < lci.Count; i++)
                {
                    acem.Add(lci[i]);
                    if ((i != 0 && i % Consts.InsertRecordCount == 0) || i == lci.Count - 1)
                    {
                        acem.SaveChanges();
                        Iterate();
                    }
                }
            }
            StepFinish();
        }
    }

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
            new KnownAddress {ParsingNames = new[] {"мурмино"}, TrueName = "Мурмино", TruePrefix = RpPrefix},
            new KnownAddress {ParsingNames = new[] {"дубровичи"}, TrueName = "Дубровичи", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"казарь"}, TrueName = "Казарь", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"долгинино"}, TrueName = "Долгинино", TruePrefix = VillagePrefix},
            new KnownAddress {ParsingNames = new[] {"сёмкино", "семкино"}, TrueName = "Сёмкино", TruePrefix = VillagePrefix}
        };

        public const string VillagePrefix = "с.";
        public const string RpPrefix = "рп";
        #endregion

        #region Список известных улиц
        public static readonly KnownAddress[] KnownStreets =
        {
            new KnownAddress{ParsingNames = new[] {"лесная"},  TrueName = "Лесная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"верхне-Садовая"},  TrueName = "Верхне-Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"нижне-Садовая"},  TrueName = "Нижне-Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовая"},  TrueName = "Садовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"школьная"},  TrueName = "Школьная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"советская пл"},  TrueName = "Советская", TruePrefix = SquarePrefix},  
            new KnownAddress{ParsingNames = new[] {"советская","советсая"},  TrueName = "Советская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"колхозная","колхлзная","кохозная"},  TrueName = "Колхозная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"озерная","озёрная"},  TrueName = "Озёрная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"молодежная","молодёжная"},  TrueName = "Молодёжная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"1 мая","1мая","1-ое мая"},  TrueName = "1 Мая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"новая"},  TrueName = "Новая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"центральная"},  TrueName = "Центральная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"рабочая"},  TrueName = "Рабочая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"горка"},  TrueName = "Горка", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"бутырки"},  TrueName = "Бутырки", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"родниковая"},  TrueName = "Родниковая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"фабричная"},  TrueName = "Фабричная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"полевая"},  TrueName = "Полевая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"городцова"},  TrueName = "Городцова", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"левобережная"},  TrueName = "Левобережная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"весовая"},  TrueName = "Весовая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"пионерская"},  TrueName = "Пионерская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовый тупик"},  TrueName = "Садовый тупик", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"советской Армии"},  TrueName = "Советской Армии", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"луговая"},  TrueName = "Луговая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"садовый переулок"},  TrueName = "Садовый переулок", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"нижняя"},  TrueName = "Нижняя", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"комсомольская"},  TrueName = "Комсомольская", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"комсомола"},  TrueName = "Комсомола", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"береговая"},  TrueName = "Береговая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"берегова"},  TrueName = "Берегова", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"запрудная"},  TrueName = "Запрудная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"песчаная"},  TrueName = "Песчаная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"заречная"},  TrueName = "Заречная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"есенина"},  TrueName = "Есенина", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"революции"},  TrueName = "Революции", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"зеленая"},  TrueName = "Зеленая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"кооперативная"},  TrueName = "Кооперативная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"кооператив"},  TrueName = "Кооператив", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"радищева"},  TrueName = "Радищева", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"свободы"},  TrueName = "Свободы", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"сиреневая"},  TrueName = "Сиреневая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"охотничий ряд"},  TrueName = "Охотничий ряд", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"охотничий пер"},  TrueName = "Охотничий пер", TruePrefix =""},  
            new KnownAddress{ParsingNames = new[] {"владимировка"},  TrueName = "Владимировка", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"моховой"},  TrueName = "Моховой", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"птичий"},  TrueName = "Птичий", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"широкий"},  TrueName = "Широкий", TruePrefix =AlleyPrefix},  
            new KnownAddress{ParsingNames = new[] {"долина"},  TrueName = "Долина", TruePrefix =StreetPrefix}  
        };

        public const string StreetPrefix = "ул.";
        public const string AlleyPrefix = "пер.";
        public const string SquarePrefix = "пл.";
        #endregion
    }
}
