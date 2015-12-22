using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
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

        public static readonly int CurrentMonth = 12;

        public static readonly int CurrentYear = 2015;

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
                if (String.IsNullOrWhiteSpace(a.TOWNSNAME))
                    a.TOWNSNAME = Consts.UnknownTown;

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
                if (String.IsNullOrWhiteSpace(a.ULICANAME))
                    a.ULICANAME = Consts.UnknownStreet;

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

            SaveList(lcc, Consts.InsertRecordCount);

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

                int cnttype;
                string cntname;
                if (counter.Name.Contains("гор.вода"))
                {
                    cnttype = 112;
                    cntname = "Сч. гор. в";
                }
                else
                {
                    cnttype = 106;
                    cntname = "Счетчик холодной воды";
                }

                var c = new CNV_COUNTER
                {
                    COUNTERID = counter.Lshet.Trim() + "_" + counter.Counterid.Trim(),
                    LSHET = Consts.GetLs(Convert.ToInt64(counter.Lshet)),
                    CNTTYPE = cnttype,
                    CNTNAME = cntname,
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

            SaveList(lcn, Consts.InsertRecordCount);
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
                    COUNTERID = counterind.Lshet.Trim() + "_" + counterind.Counterid.Trim(),
                    DOCUMENTCD = String.Format("{0}_{1}", counterind.Counterid.Trim().TrimStart('0'), dataRow["RECNO"]),
                    //OB_EM = ,
                    INDDATE = counterind.Inddate,
                    INDTYPE = 0,
                };

                var existedInd = lci.Find(cind =>
                    cind.COUNTERID == c.COUNTERID && cind.INDDATE.Value.Year == c.INDDATE.Value.Year &&
                    cind.INDDATE.Value.Month == c.INDDATE.Value.Month);
                if (existedInd == null)
                {
                    existedInd = c;
                    lci.Add(existedInd);
                }

                if (c.INDDATE.Value.Day == 1)
                    existedInd.OLDIND = counterind.Indication;
                else
                    existedInd.INDICATION = counterind.Indication;
                
                Iterate();
            }
            StepFinish();

            StepStart(lci.Count);
            foreach (var cntrsind in lci)
            {
                if (cntrsind.OLDIND == null)
                    cntrsind.OLDIND = cntrsind.INDICATION;
                else if (cntrsind.INDICATION == null)
                    cntrsind.INDICATION = cntrsind.OLDIND;

                Iterate();
            }
            StepFinish();

            SaveList(lci, Consts.InsertRecordCount);
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(11);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            DataTable dtNach = Tmsource.ExecuteQuery("SELECT *, RECNO() AS RECNO FROM NACH");
            DataTable dtOpl = Tmsource.ExecuteQuery("SELECT *, RECNO() AS RECNO FROM OPLATA");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            var nach = new NachRecord();
            var oplata = new OplataRecord();

            #region Начисления
            StepStart(dtNach.Rows.Count);
            foreach (DataRow dataRow in dtNach.Rows)
            {
                nach.ReadDataRow(dataRow);

                string documentcd = String.Format("{0}_{1}", nach.Lshet.Trim().TrimStart('0'), dataRow["RECNO"]);

                int regimcd;
                string regimname;
                int servicecd;
                string servicename;
                DifineServiceType((int) nach.Servicecd, out regimcd, out regimname, out servicecd, out servicename);

                var ndef = new CNV_NACH
                {
                    VOLUME = nach.Volume,
                    REGIMCD = regimcd,
                    REGIMNAME = regimname,
                    SERVICECD = servicecd,
                    SERVICENAME = servicename,
                    TYPE_ = 0
                };
                nm.RegisterNach(ndef, Consts.GetLs(Convert.ToInt64(nach.Lshet)), (int) nach.Month, (int) nach.Year,
                    nach.Fnath, nach.Prochl, nach.Date_vv, documentcd);

                Iterate();
            }
            StepFinish();
            #endregion

            #region Оплаты
            StepStart(dtOpl.Rows.Count);
            foreach (DataRow dataRow in dtOpl.Rows)
            {
                oplata.ReadDataRow(dataRow);

                string documentcd = String.Format("{0}_{1}", oplata.Lshet.Trim().TrimStart('0'), dataRow["RECNO"]);

                int regimcd;
                string regimname;
                int servicecd;
                string servicename;
                DifineServiceType((int)oplata.Servicecd, out regimcd, out regimname, out servicecd, out servicename);

                int sourcecd;
                string sourcename;
                switch (oplata.Sourcecd)
                {
                    case 1:
                        sourcecd = 17;
                        sourcename = "Касса";
                        break;

                    case 4:
                        sourcecd = 19;
                        sourcename = "Прочие";
                        break;

                    default:
                        throw new Exception("Неизвестый источник оплат и сдентификатором " + oplata.Sourcecd);
                }

                var odef = new CNV_OPLATA
                {
                    SERVICECD = servicecd, 
                    SERVICENAME = servicename,
                    SOURCECD = sourcecd,
                    SOURCENAME = sourcename
                };
                nm.RegisterOplata(odef, Consts.GetLs(Convert.ToInt64(oplata.Lshet)), (int) oplata.Month,
                    (int) oplata.Year, oplata.Summa, oplata.Docdate, oplata.Date_vv, documentcd);

                Iterate();
            }
            StepFinish();
            #endregion

            #region Сальдо
            ConvertSaldoByServices(new[] { 1 }, 2, "Содержание жилья", nm);
            ConvertSaldoByServices(new[] { 8, 9, 10, 11, 12, 17, 20, 21 }, 4, "Холодная вода", nm);
            ConvertSaldoByServices(new[] { 4, 13, 14, 15 }, 8, "Водоотведение", nm);
            ConvertSaldoByServices(new[] { 6 }, 6, "Вывоз ТБО", nm);
            ConvertSaldoByServices(new[] { 2 }, 3, "Отопление", nm);
            ConvertSaldoByServices(new[] { 5 }, 5, "Горячая вода", nm);
            #endregion

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }
        
        private void ConvertSaldoByServices(IEnumerable<int> originServices, int servicecd, string servicename, NachoplManager nm)
        {
            string services = "";
            foreach (var originService in originServices)
            {
                services += originService + ",";
            }
            services = services.Substring(0, services.Length - 1);
            DataTable dtNachopl =
                Tmsource.ExecuteQuery(String.Format(@"SELECT
                                                      n.LSHET,
                                                      n.MONTH,
                                                      n.YEAR,
                                                      SUM(n.BDEBET) AS BDEBET,
                                                      SUM(n.EDEBET) AS EDEBET,
                                                      {0} AS SERVICECD,
                                                      '{1}' AS SERVICENAM,
                                                      RECNO() AS RECNO
                                                    FROM NACHOPL n
                                                    WHERE n.SERVICECD IN ({2})
                                                    GROUP BY n.LSHET,
                                                             n.MONTH,
                                                             n.YEAR",
                    servicecd, servicename, services));
            var nachopl = new NachoplRecord();
            StepStart(dtNachopl.Rows.Count);
            foreach (DataRow dataRow in dtNachopl.Rows)
            {
                nachopl.ReadDataRow(dataRow);

                string lshet = Consts.GetLs(Convert.ToInt64(nachopl.Lshet));
                nm.RegisterBeginSaldo(lshet, (int)nachopl.Month, (int)nachopl.Year, servicecd, servicename,
                    nachopl.Bdebet);
                nm.RegisterEndSaldo(lshet, (int)nachopl.Month, (int)nachopl.Year, servicecd, servicename,
                    nachopl.Edebet);

                Iterate();
            }
            StepFinish();
        }

        private void DifineServiceType(int originServicecd, out int regimcd, out string regimname, out int servicecd,
            out string servicename)
        {
            switch (originServicecd)
            {
                case 1:
                    regimcd = 31;
                    regimname = "Содержание жилья для конвертации";
                    servicecd = 2;
                    servicename = "Содержание жилья";
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 17:
                case 20:
                case 21:
                    regimcd = 32;
                    regimname = "Холодная вода для конвертации";
                    servicecd = 4;
                    servicename = "Холодная вода";
                    break;

                case 6:
                    regimcd = 30;
                    regimname = "Вывоз ТБО для конвертации";
                    servicecd = 6;
                    servicename = "Вывоз ТБО";
                    break;

                case 4:
                case 13:
                case 14:
                case 15:
                    regimcd = 29;
                    regimname = "Водоотведение для конвертации";
                    servicecd = 8;
                    servicename = "Водоотведение";
                    break;

                case 2:
                    regimcd = 36;
                    regimname = "Отопление для конвертации";
                    servicecd = 3;
                    servicename = "Отопление";
                    break;

                case 5:
                    regimcd = 35;
                    regimname = "Горячая вода для конвертации";
                    servicecd = 5;
                    servicename = "Горячая вода";
                    break;

                default:
                    throw new Exception(String.Format("Неизвестная услуга с идентификатором {0}", originServicecd));
            }
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
            new KnownAddress{ParsingNames = new[] {"советской армии", "советсой армии"},  TrueName = "Советской Армии", TruePrefix = SquarePrefix},  
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
            new KnownAddress{ParsingNames = new[] {"песчаная", "песчанная"},  TrueName = "Песчаная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"заречная"},  TrueName = "Заречная", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"есенина"},  TrueName = "Есенина", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"революции"},  TrueName = "Революции", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"зеленая"},  TrueName = "Зеленая", TruePrefix =StreetPrefix},  
            new KnownAddress{ParsingNames = new[] {"кооперативная", "коопнративная"},  TrueName = "Кооперативная", TruePrefix =StreetPrefix},  
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
