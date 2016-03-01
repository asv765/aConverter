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

            var fioRegex = new Regex(@"([A-ЯA-Z]{1}[а-яa-z]+)[^А-ЯA-Z]*([А-ЯA-Z]{1}[а-яa-z]*)?[^А-ЯA-Z]*([А-ЯA-Z]{1}[а-яa-z]*)?");
            var postIndexRegex = new Regex("39[0-9]{4}");
            var houseFlatRegex = new Regex(@"([0-9]+)(-?([0-9а-яё]+))?[^0-9-]*([0-9]+)?");
            var houseFromStateRege = new Regex(@".*дом (\d+).*");

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
                if (a.F.Length > 50) a.F = a.F.Substring(0, 49);
                
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
                if (!String.IsNullOrWhiteSpace(a.POSTINDEX)) clearedAddress = clearedAddress.Replace(a.POSTINDEX, "");

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
                if (!addressFound)
                {
                    foreach (var knownTown in KnownAddress.KnownTowns)
                    {
                        foreach (var name in knownTown.ParsingNames)
                        {
                            if (!abonent.Sectorname.ToLower().Contains(name)) continue;
                            a.TOWNSNAME = knownTown.TrueName + " " + knownTown.TruePrefix;
                            addressFound = true;
                            break;
                        }
                        if (addressFound) break;
                    }
                    if (!addressFound) a.TOWNSNAME = Consts.UnknownTown;
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
                else
                {
                    match = houseFromStateRege.Match(abonent.Sectorname);
                    if (match.Groups.Count > 1)
                        a.HOUSENO = match.Groups[1].Value;
                    else if (abonent.Sectorname.Contains(@"Ч\Д"))
                        a.HOUSEPOSTFIX = @"Ч\Д";
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.ExecuteQuery("SELECT * FROM LCHARS a where a.nachcd = (select max(f.nachcd) from LCHARS f where f.lshet = a.lshet and f.parentcd = a.parentcd and a.lcharcd = f.lcharcd and f.date = a.date)");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист2");
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            var lchar = new LcharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lchar.ReadDataRow(dataRow);
                // У абонента разные характеристики в исходной базе, но при перекодировке они сворачиваются в одну
                if (lchar.Lshet.Trim() == "00001665" && lchar.Lcharcd == 2)
                {
                    lchar.Date = new DateTime(lchar.Date.Year, lchar.Date.Month, 2);
                }

                foreach (DataRow row in recodeTable.Rows)
                {
                    if (row["Value1"] == DBNull.Value || row["Value2"] == DBNull.Value) continue;

                    if (Convert.ToInt32(row["Value1"]) != lchar.Parentcd ||
                        Convert.ToInt32(row["Value2"]) != lchar.Lcharcd) continue;

                    var lc = new CNV_LCHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(lchar.Lshet)),
                        LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                        LCHARNAME = row["LCHARNAME"].ToString(),
                        VALUEDESC = row["LCHARVALUEDESC"].ToString(),
                        VALUE_ = lchar.Value_ == 0 ? 0 : Convert.ToInt32(row["LCHARVALUE"]),
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DataTable dt = Tmsource.ExecuteQuery(@"SELECT ci.*, c.counterid, RECNO() AS RECNO
                                                    FROM CNTRSIND ci
                                                    left join counters c on c.lshet = ci.lshet and c.parentcd=ci.parentcd and c.tarifcd = ci.tarifcd
                                                    order by ci.lshet, ci.tarifnm, ci.inddate");
            int maxCounterId =
                Int32.Parse(Tmsource.ExecuteQuery("select max(counterid) from counters").Rows[0][0].ToString());
            var lci = new List<CNV_CNTRSIND>();
            var lcc = new List<CNV_COUNTER>();

            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);

                if (counterind.Tarifnm.Trim() == "Водоотведение" ||
                    dataRow["counterid"].ToString().Trim() == "000014742" ||
                    dataRow["counterid"].ToString().Trim() == "000015843") continue;

                string counterid;
                if (String.IsNullOrWhiteSpace(dataRow["counterid"].ToString()))
                {
                    string lshet = Consts.GetLs(Convert.ToInt64(counterind.Lshet));

                    var newCounter = new CNV_COUNTER
                    {
                        LSHET = lshet,
                            SETUPDATE = new DateTime(2016, 1, 1),
                    };

                    switch (counterind.Tarifnm.Trim())
                    {
                        case "Водоснабжение":
                            if (counterind.Indication == 0) continue;
                            newCounter.NAME = "Водоснабжение";
                            newCounter.CNTTYPE = 1;
                            break;
                        case "Горячая вода":
                            newCounter.NAME = "Горячая вода";
                            newCounter.CNTTYPE = 3;
                            break;
                        default:
                            throw new Exception("Неизвестный тип счетчика " + counterind.Tarifnm);
                    }

                    var newExCounter =
                        lcc.SingleOrDefault(cnt => cnt.LSHET == newCounter.LSHET && cnt.CNTTYPE == newCounter.CNTTYPE);

                    if (newExCounter == null)
                    {
                        maxCounterId++;
                        newCounter.COUNTERID = maxCounterId.ToString();
                        lcc.Add(newCounter);
                        counterid = newCounter.COUNTERID;
                    }
                    else counterid = newExCounter.COUNTERID;
                }
                else
                {
                    counterid = dataRow["counterid"].ToString();
                }

                var lastInd = lci.LastOrDefault(ci => ci.COUNTERID == counterid);

                var c = new CNV_CNTRSIND
                {
                    COUNTERID = counterid,
                    DOCUMENTCD = String.Format("{0}_{1}", counterind.Lshet.Trim(), dataRow["RECNO"]),
                    INDDATE = counterind.Inddate,
                    INDTYPE = 0,
                    OLDIND = lastInd == null ? 0 : lastInd.INDICATION,
                    INDICATION = counterind.Indication
                };
                lci.Add(c);

                Iterate();
            }
            StepFinish();

            SaveList(lcc, Consts.InsertRecordCount);
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(7);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            DataTable dtNach = Tmsource.ExecuteQuery(
                    @"select n.lshet, n.month, n.year, n.month2, n.year2, n.fnath, n.prochl, n.parentcd, n.tarifcd, n.tarifnm, iif(t.value = 0, t.value_c, t.value) as tarif, RECNO() as RECNO
                                                        from Nachopl n
                                                        left join tarifs t on t.parentcd = n.parentcd and t.tarifcd = n.tarifcd
                                                        where (n.fnath <> 0 or n.prochl <> 0)
                                                        and( n.parentcd <> 0 and n.tarifcd <> 0)");
            DataTable dtOplata = Tmsource.ExecuteQuery(@"select lshet, month, year, oplata, parentcd, tarifcd, tarifnm, RECNO() as RECNO from Nachopl where oplata <> 0 and parentcd <> 0 and tarifcd <> 0");
            DataTable dtNachopl = Tmsource.ExecuteQuery(@"select lshet, month, year, bdebet, edebet, parentcd, tarifcd, tarifnm from Nachopl where parentcd <> 0 and tarifcd <> 0");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            var nachopl = new NachoplRecord();

            #region Начисления
            StepStart(dtNach.Rows.Count);
            foreach (DataRow dataRow in dtNach.Rows)
            {
                nachopl.ReadDataRow(dataRow);
                if (nachopl.Tarifnm.Trim() == "Пени") continue;
                string documentcd = String.Format("N{0}_{1}", nachopl.Lshet.Trim().TrimStart('0'), dataRow["RECNO"]);

                int regimcd;
                string regimname;
                int servicecd;
                string servicename;
                DefineServiceType(nachopl.Tarifnm, out servicecd, out servicename, out regimcd, out regimname, (int)nachopl.Parentcd, (int)nachopl.Tarifcd);

                //decimal tarif = (decimal)dataRow["tarif"];
                var ndef = new CNV_NACH
                {
                    //VOLUME = tarif == 0 ? 0 : nachopl.Fnath / tarif,
                    VOLUME = 0,
                    REGIMCD = regimcd,
                    REGIMNAME = regimname,
                    SERVICECD = servicecd,
                    SERVICENAME = servicename,
                    TYPE_ = 0
                };
                nm.RegisterNach(ndef, Consts.GetLs(Convert.ToInt64(nachopl.Lshet)), (int) nachopl.Month,
                    (int) nachopl.Year, nachopl.Fnath, nachopl.Prochl,
                    new DateTime((int) nachopl.Year, (int) nachopl.Month, 1), documentcd);

                Iterate();
            }
            StepFinish();
            #endregion 

            #region Оплаты
            StepStart(dtOplata.Rows.Count);
            foreach (DataRow dataRow in dtOplata.Rows)
            {
                nachopl.ReadDataRow(dataRow);
                if (nachopl.Tarifnm.Trim() == "Пени") continue;
                string documentcd = String.Format("O{0}_{1}", nachopl.Lshet.Trim().TrimStart('0'), dataRow["RECNO"]);

                int regimcd;
                string regimname;
                int servicecd;
                string servicename;
                DefineServiceType(nachopl.Tarifnm, out servicecd, out servicename, out regimcd, out regimname, (int)nachopl.Parentcd, (int)nachopl.Tarifcd);

                var odef = new CNV_OPLATA
                {
                    SERVICECD = servicecd,
                    SERVICENAME = servicename,
                    SOURCECD = 17,
                    SOURCENAME = "Касса"
                };
                nm.RegisterOplata(odef, Consts.GetLs(Convert.ToInt64(nachopl.Lshet)), (int) nachopl.Month,
                    (int) nachopl.Year, nachopl.Oplata, new DateTime((int) nachopl.Year, (int) nachopl.Month, 1),
                    new DateTime((int) nachopl.Year, (int) nachopl.Month, 1), documentcd);

                Iterate();
            }
            StepFinish();
            #endregion

            #region Сальдо
            StepStart(dtNachopl.Rows.Count);
            foreach (DataRow dataRow in dtNachopl.Rows)
            {
                nachopl.ReadDataRow(dataRow);
                if (nachopl.Tarifnm.Trim() == "Пени") continue;
                string lshet = Consts.GetLs(Convert.ToInt64(nachopl.Lshet));
                int regimcd;
                string regimname;
                int servicecd;
                string servicename;
                DefineServiceType(nachopl.Tarifnm, out servicecd, out servicename, out regimcd, out regimname, (int)nachopl.Parentcd, (int)nachopl.Tarifcd);
                nm.RegisterBeginSaldo(lshet, (int) nachopl.Month, (int) nachopl.Year, servicecd, servicename,
                    nachopl.Bdebet);
                nm.RegisterEndSaldo(lshet, (int)nachopl.Month, (int)nachopl.Year, servicecd, servicename,
                    nachopl.Edebet);
                Iterate();
            }
            StepFinish();
            #endregion

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }

        private void DefineServiceType(string tarifName, out int servicecd, out string servicename, out int regimcd,
            out string regimname, int parentcd = 0, int tarifcd = 0)
        {
            regimcd = 10;
            regimname = "Неизвестен";
            if ((parentcd == 203 && tarifcd == 204) || (parentcd == 232 && tarifcd == 2))
            {
                servicecd = 6;
                servicename = "Вывоз отходов";
                return;
            }
            switch (tarifName.Trim())
            {
                case "Водоотведение":
                    servicecd = 8;
                    servicename = "Канализация";
                    break;
                case "Водоснабжение":
                case "Уличные колонки":
                    servicecd = 4;
                    servicename = "Холодная вода";
                    break;
                case "Горячая вода":
                    servicecd = 5;
                    servicename = "Горячая вода";
                    break;
                case "Общедомовые нужды по ХВС":
                    servicecd = 1014;
                    servicename = "Хол. водоснабжение ОДН";
                    break;
                case "Отопление":
                    servicecd = 3;
                    servicename = "Отопление";
                    break;
                case "Содержание жилья":
                    servicecd = 2;
                    servicename = "Содержание жилья";
                    break;
                default:
                    throw new Exception(String.Format("Неизвестное имя тарифа '{0}'", tarifName));
            }
        }
    }

    /// <summary>
    /// Конвертация данных о характерстиках домов
    /// </summary>
    public class ConvertHChars : ConvertCase
    {
        public ConvertHChars()
        {
            ConvertCaseName = "HADDCHARS - характеристики домов";
            Position = 80;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$HADDCHAR");
            DataTable dt = Tmsource.GetDataTable("CHARS2");
            var lch = new List<CNV_HADDCHAR>();

            StepStart(dt.Rows.Count);
            var hchar = new Chars2Record();
            foreach (DataRow dataRow in dt.Rows)
            {
                hchar.ReadDataRow(dataRow);

                if (hchar.Isdeleted == 1 || hchar.Psectorcd == 0 || (hchar.Sectorcd == 26 && hchar.Psectorcd == 6))
                    continue;

                DataTable abonents = Tmsource.ExecuteQuery(
                    String.Format("SELECT lshet FROM ABONENT where psectorcd = {0} and sectorcd = {1}",
                        hchar.Psectorcd, hchar.Sectorcd));

                var la = new List<string>();
                foreach (DataRow abonent in abonents.Rows)
                {
                    la.Add(Consts.GetLs(Convert.ToInt64(abonent[0])));
                }

                var houses = new List<int?>();
                using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
                {
                    houses = context.CNV_ABONENTs.Where(a => la.Contains(a.LSHET)).Select(a => a.HOUSECD).ToList();
                }

                foreach (var housecd in houses)
                {
                    if (housecd == null || housecd == 0) continue;
                    var haddchar1 = new CNV_HADDCHAR
                    {
                        HOUSECD = housecd,
                        ADDCHARCD = 32001,
                        VALUE_ = hchar.Livearea.ToString().Replace(',','.')
                    };
                    var haddchar2 = new CNV_HADDCHAR
                    {
                        HOUSECD = housecd,
                        ADDCHARCD = 32010,
                        VALUE_ = hchar.Comearea.ToString().Replace(',', '.')
                    };
                    lch.Add(haddchar1);
                    lch.Add(haddchar2);
                }
                Iterate();
            }
            
            SaveList(lch, Consts.InsertRecordCount);
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

    public class TransferHchars : ConvertCase
    {
        public TransferHchars()
        {
            ConvertCaseName = "Характеристики домов";
            Position = 1090;
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
            new KnownAddress {ParsingNames = new[] {"госплемстанция", "госплемстанции"}, TrueName = "Госплемстанции", TruePrefix = PoselPrefix},
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