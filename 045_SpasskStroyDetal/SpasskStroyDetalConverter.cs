using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\045_SpasskStroyDetal\Sources\Таблица перекодировки (4).xlsx";

        public static string GetLs(string grkod, string lshet)
        {
            return String.Format("95{0}{1}", String.IsNullOrWhiteSpace(grkod) ? "9999" : grkod.Substring(4, 4),
                lshet.Substring(4,4));
        }

        public static string GetLs(long lshet)
        {
            return String.Format("95{0:D6}", lshet);
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
    public class ConvertAbonentOnly1C : ConvertCase
    {
        public ConvertAbonentOnly1C()
        {
            ConvertCaseName = "ABONENT - данные об абонентах ТОЛЬКО ИЗ 1С";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(4);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();
            var regex = new Regex(@"(\d+)(.*)");
            StepStart(dt.Rows.Count);
            var abonentRec = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonentRec.ReadDataRow(dataRow);

                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(abonentRec.Grkod.Trim(), abonentRec.Lshet.Trim()),
                    EXTLSHET = String.Format("{0}{1}", abonentRec.Grkod.Trim(), abonentRec.Lshet.Trim()),
                    DISTKOD = (int)abonentRec.Distkod,
                    DISTNAME = abonentRec.Distname.Trim(),
                    DUCD = (int)abonentRec.Ducd,
                    DUNAME = abonentRec.Duname.Trim(),
                    RAYONKOD = 1,
                    RAYONNAME = "Спасский р-н",
                    PRIM_ = abonentRec.Prim_.Trim(),
                    F = abonentRec.F.Trim(),
                    I = abonentRec.I.Trim(),
                    O = abonentRec.O.Trim(),
                    POSTINDEX = abonentRec.Postindex.Trim(),
                    //TOWNSKOD = (int) abonentRec.Townskod,
                    TOWNSNAME = abonentRec.Townsname.Trim(),
                    //ULICAKOD = (int) abonentRec.Ulicakod,
                    ULICANAME = abonentRec.Ulicaname.Trim(),
                    ISDELETED = (int)abonentRec.Isdeleted,
                    PHONENUM = abonentRec.Phonenum
                };

                string house = abonentRec.Ndoma.Trim();
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

                string kvartira = abonentRec.Kvartira.Trim();
                matches = regex.Matches(kvartira);
                if (matches.Count > 0)
                {
                    var groups = matches[0].Groups;
                    if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                    if (groups.Count > 1)
                    {
                        int flatno;
                        if (Int32.TryParse(groups[1].Value, out flatno))
                            a.FLATNO = Convert.ToInt32(groups[1].Value);
                        else a.FLATPOSTFIX = groups[0].Value;
                    }
                }

                if (a.HOUSEPOSTFIX != null && a.HOUSEPOSTFIX.Length > 10)
                    a.HOUSEPOSTFIX = a.HOUSEPOSTFIX.Substring(0, 10);
                lca.Add(a);
                Iterate();

            }
            StepFinish();


            StepStart(1);
            AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(4);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            var aList = Abonent.ReadFile();
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();
            var regex = new Regex(@"(\d+)(.*)");
            StepStart(dt.Rows.Count);
            var abonentRec = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                    abonentRec.ReadDataRow(dataRow);

                if (aList.Any(al => al.Uklshet == Consts.GetLs(abonentRec.Grkod.Trim(), abonentRec.Lshet.Trim())))
                {
                    Iterate();
                    continue;
                }

                var a = new CNV_ABONENT
                    {
                        LSHET = Consts.GetLs(abonentRec.Grkod.Trim(), abonentRec.Lshet.Trim()),
                        EXTLSHET = String.Format("{0}{1}", abonentRec.Grkod.Trim(), abonentRec.Lshet.Trim()),
                        DISTKOD = (int) abonentRec.Distkod,
                        DISTNAME = abonentRec.Distname.Trim(),
                        DUCD = (int) abonentRec.Ducd,
                        DUNAME = abonentRec.Duname.Trim(),
                        RAYONKOD = 1,
                        RAYONNAME = "Спасский р-н",
                        PRIM_ = abonentRec.Prim_.Trim(),
                        F = abonentRec.F.Trim(),
                        I = abonentRec.I.Trim(),
                        O = abonentRec.O.Trim(),
                        POSTINDEX = abonentRec.Postindex.Trim(),
                        //TOWNSKOD = (int) abonentRec.Townskod,
                        TOWNSNAME = abonentRec.Townsname.Trim(),
                        //ULICAKOD = (int) abonentRec.Ulicakod,
                        ULICANAME = abonentRec.Ulicaname.Trim(),
                        ISDELETED = (int) abonentRec.Isdeleted,
                        PHONENUM = abonentRec.Phonenum
                    };

                    string house = abonentRec.Ndoma.Trim();
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

                    string kvartira = abonentRec.Kvartira.Trim();
                    matches = regex.Matches(kvartira);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int flatno;
                            if (Int32.TryParse(groups[1].Value, out flatno))
                                a.FLATNO = Convert.ToInt32(groups[1].Value);
                            else a.FLATPOSTFIX = groups[0].Value;
                        }
                    }

                    if (a.HOUSEPOSTFIX != null && a.HOUSEPOSTFIX.Length > 10)
                        a.HOUSEPOSTFIX = a.HOUSEPOSTFIX.Substring(0, 10);
                    lca.Add(a);
                    Iterate();

            }
            StepFinish();

            
            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                if (abonent.Ls == 0) continue;
                string f = null;
                string i = null;
                string o = null;
                if (!String.IsNullOrWhiteSpace(abonent.Fio))
                {
                    f = abonent.Fio.Split('.')[0].Trim();
                    i = abonent.Fio.Split('.')[1].Trim();
                    o = abonent.Fio.Split('.')[2].Trim();
                }
                var a = new CNV_ABONENT
                    {
                        LSHET = Consts.GetLs(abonent.Ls),
                        EXTLSHET = String.IsNullOrWhiteSpace(abonent.Uklshet) ? abonent.Ls.ToString() : abonent.Uklshet,
                        DISTKOD = 1,
                        DISTNAME = "Рязанская обл.",
                        DUCD = 1,
                        DUNAME = @"ООО ""МРС-Регион""",
                        RAYONKOD = 1,
                        RAYONNAME = "Спасский р-н",
                        F = f,
                        I = i,
                        O = o,
                        //TOWNSKOD = abonent.PunktCd,
                        TOWNSNAME = abonent.PunktName,
                        ULICANAME = abonent.Streetnm,
                        ISDELETED = 0
                    };

                if (!String.IsNullOrWhiteSpace(abonent.Ndoma))
                {
                    string house = abonent.Ndoma;
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
                }

                if (!String.IsNullOrWhiteSpace(abonent.Kvartira))
                {
                    string kvartira = abonent.Kvartira;
                    var matches = regex.Matches(kvartira);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int flatno;
                            if (Int32.TryParse(groups[1].Value, out flatno))
                                a.FLATNO = Convert.ToInt32(groups[1].Value);
                            else a.FLATPOSTFIX = groups[0].Value;
                        }
                    }

                    if (a.HOUSEPOSTFIX != null && a.HOUSEPOSTFIX.Length > 10)
                        a.HOUSEPOSTFIX = a.HOUSEPOSTFIX.Substring(0, 10);
                }
                lca.Add(a);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
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
            var aList = Abonent.ReadFile();

            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var chars = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                chars.ReadDataRow(dataRow);
                var kosheAbonent =
                    aList.SingleOrDefault(al => al.Uklshet == Consts.GetLs(chars.Grkod.Trim(), chars.Lshet.Trim()));

                var lshet = kosheAbonent != null
                    ? Consts.GetLs(kosheAbonent.Ls)
                    : Consts.GetLs(chars.Grkod, chars.Lshet);

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
                        continue;
                    default:
                        throw new Exception("Неизвестная характеристика " + chars.Charcd);
                }

                var c = new CNV_CHAR
                {
                    LSHET = lshet,
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
    /// Конвертирует данные о количественных характеристиках
    /// </summary>
    public class ConvertCharsKoshe : ConvertCase
    {
        public ConvertCharsKoshe()
        {
            ConvertCaseName = "CHARS Koshe - данные о количественных характеристиках";
            Position = 31;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            var aList = Abonent.ReadFile();
            var lcc = new List<CNV_CHAR>();

            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                var c1 = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(abonent.Ls),
                    CHARCD = 13,
                    VALUE_ = abonent.Thprogiv,
                    DATE_ = new DateTime(2016,04,01)
                };
                lcc.Add(c1);

                var c2 = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(abonent.Ls),
                    CHARCD = 14,
                    VALUE_ = abonent.Square,
                    DATE_ = new DateTime(2016, 04, 01)
                };
                lcc.Add(c2);

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
            DataTable dt =
                Tmsource.ExecuteQuery(
                    "select distinct * from contr_1 c inner join lchars l on (c.grkod = l.grkod) and (c.lshet = l.lshet) and	(c.CONTRACTCD = l.LCHARCD) order by l.grkod, l.lshet, l.date desc, contractcd desc, l.value desc");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист1");
            var aList = Abonent.ReadFile();

            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            int i = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                //if (dataRow[0].ToString().Contains("00017") && dataRow[1].ToString().Contains("00013") &&
                //    dataRow[4].ToString() == "10")
                //{
                //    int a = 10;
                //}

                var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == Consts.GetLs(dataRow[0].ToString(), dataRow[1].ToString()));

                var lshet = kosheAbonent != null
                    ? Consts.GetLs(kosheAbonent.Ls)
                    : Consts.GetLs(dataRow[0].ToString(), dataRow[1].ToString());

                if (i > 0 && dataRow[0].ToString() == dt.Rows[i - 1][0].ToString() &&
                    dataRow[1].ToString() == dt.Rows[i - 1][1].ToString() &&
                    dataRow["SERVICECD"].ToString() == dt.Rows[i - 1]["SERVICECD"].ToString() &&
                    dataRow["TARIFCD"].ToString() == dt.Rows[i - 1]["TARIFCD"].ToString() &&
                    dataRow[15].ToString() == dt.Rows[i - 1][15].ToString())
                {
                    if (Convert.ToInt32(dataRow[2].ToString()) < Convert.ToInt32(dt.Rows[i - 1][2].ToString()))
                    {
                        Iterate();
                        i++;
                        continue;
                    }
                    if (Convert.ToInt32(dataRow[2].ToString()) == Convert.ToInt32(dt.Rows[i - 1][2].ToString()))
                    {
                        if (Convert.ToInt32(dataRow["value"].ToString()) < Convert.ToInt32(dt.Rows[i - 1]["value"].ToString()))
                        {
                            Iterate();
                            i++;
                            continue;
                        }
                    }
                }

                //bool notFound = true;
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
                            LSHET = lshet,
                            LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                            VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                            DATE_ = DateTime.Parse(dataRow[15].ToString())
                        };
                        if (llc.Any(l => l.LSHET == lc.LSHET && l.LCHARCD == lc.LCHARCD && l.DATE_ == lc.DATE_))
                            continue;

                        llc.Add(lc);
                        //notFound = false;
                    }
                }
                //if (notFound)
                //{
                //    notIn += String.Format("{0}\t{1}\t{2}\t{3}\t{4}\r\n", dataRow[0], dataRow[1], dataRow["SERVICECD"],
                //        dataRow["TARIFCD"], dataRow["VALUE"]);
                //}
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
    /// Конвертация качественных характеристик
    /// Данные в таблице кодировки должны быть отсортированы по исходному ID!
    /// </summary>
    public class ConvertLCharsKoshe : ConvertCase
    {
        public ConvertLCharsKoshe()
        {
            ConvertCaseName = "LCHARS Koshe - данные о параметрах потребления";
            Position = 41;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);

            var aList = Abonent.ReadFile();
            foreach (var abonent in aList)
            {
                if (abonent.Ls == 939)
                {
                    int abs = 10;
                }
                abonent.MoneyList = abonent.MoneyList.Where(m => m.Nath > 0 || m.ServiceKod.Trim() == "5.5" || m.ServiceKod.Trim() == "8.2").ToList();
                var grouped = abonent.MoneyList.GroupBy(m => m.ServiceKod[0]);
                foreach (var group in grouped)
                {
                    if (group.Count() < 2) continue;
                    var notNull = group.Where(m => m.Nath > 0).ToList();
                    if (notNull.Any())
                    {
                        var services = notNull.Select(s => s.ServiceKod).ToList();
                        var listToRemove = new List<string>();
                        foreach (var money in abonent.MoneyList)
                        {
                            if (money.ServiceKod[0] == group.Key && !services.Contains(money.ServiceKod))
                                listToRemove.Add(money.ServiceKod);
                        }
                        abonent.MoneyList.RemoveAll(m => listToRemove.Contains(m.ServiceKod));
                    }
                }
            }

            DataTable recodeTable = Utils.ReadExcelFile(@"D:\Work\C#\C#Projects\aConverter\045_SpasskStroyDetal\Sources\Таблица перекодировки_Каше.xlsx", "Лист1");
            var llc = new List<CNV_LCHAR>();

            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                if (abonent.Ls == 3362)
                {
                    int abs = 10;
                }
                foreach (var money in abonent.MoneyList)
                {
                    decimal value;
                    if (String.IsNullOrWhiteSpace(money.ServiceKod) || !decimal.TryParse(money.ServiceKod.Replace(".", ","), out value)) continue;
                    foreach (DataRow row in recodeTable.Rows)
                    {
                        try
                        {

                        
                        if (row["Value"] == DBNull.Value) break;
                        
                        if (row["Value"].ToString().Replace(".", ",").Trim() == money.ServiceKod.Replace(".", ",").Trim())
                        {

                            var lc = new CNV_LCHAR
                            {
                                LSHET = Consts.GetLs(abonent.Ls),
                                LCHARCD = Convert.ToInt32(row["LCHARCD"]),
                                VALUE_ = Convert.ToInt32(row["LCHARVALUE"]),
                                DATE_ = new DateTime(2016,04,01)
                            };
                            if (llc.Any(l => l.LSHET == lc.LSHET && l.LCHARCD == lc.LCHARCD && l.DATE_ == lc.DATE_))
                                continue;

                            llc.Add(lc);
                        }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt =
                Tmsource.ExecuteQuery(
                    "select distinct c.*, c2.SERVICECD, c2.SERVICENM from counters c inner join contr_2 c2 on c2.GRKOD = c.GRKOD and c2.lshet = c.lshet and c2.counterid = c.counterid");
            var lcn = new List<CNV_COUNTER>();
            var aList = Abonent.ReadFile();

            StepStart(dt.Rows.Count);
            var counter = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counter.ReadDataRow(dataRow);

                var kosheAbonent =
                    aList.SingleOrDefault(al => al.Uklshet == Consts.GetLs(counter.Grkod.Trim(), counter.Lshet.Trim()));

                var lshet = kosheAbonent != null
                    ? Consts.GetLs(kosheAbonent.Ls)
                    : Consts.GetLs(counter.Grkod, counter.Lshet);

                int cnttype;
                switch (dataRow["SERVICECD"].ToString())
                {
                    case "10":
                        cnttype = 3177;
                        break;

                    case "27":
                        continue;
                        cnttype = 1;
                        break;

                    default:
                        throw new Exception("Неизвестный тип счетчика " + dataRow["SERVICECD"]);
                }

                var c = new CNV_COUNTER
                {
                    LSHET = lshet,
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

    public class ConvertCountersKoshe : ConvertCase
    {
        public ConvertCountersKoshe()
        {
            ConvertCaseName = "COUNTERS Koshe - данные о счетчиках";
            Position = 51;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);
     
            var lcn = new List<CNV_COUNTER>();
            var aList = Abonent.ReadFile();

            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                foreach (var counter in abonent.CounterList)
                {
                    int prefix = counter.CounterType == 0 ? 9 : 8;
                    int type = counter.CounterType == 0 ? 1 : 64;
                    var c = new CNV_COUNTER
                    {
                        LSHET = Consts.GetLs(abonent.Ls),
                        CNTTYPE = type,
                        COUNTERID = String.Format("{2}{0}{1}", abonent.Ls, counter.CounterNum, prefix)
                    };
                    lcn.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();

            //  0 - voda, 1 - poliv
        }
    }

    public class ConvertCountersKoshePoliv : ConvertCase
    {
        public ConvertCountersKoshePoliv()
        {
            ConvertCaseName = "COUNTERS Koshe - данные о счетчиках POLIV";
            Position = 52;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            SetStepsCount(2);

            var lcn = new List<CNV_COUNTER>();
            var aList = Abonent.ReadFile();

            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                foreach (var counter in abonent.CounterList.Where(c => c.CounterType == 1))
                {
                    var c = new CNV_COUNTER
                    {
                        LSHET = Consts.GetLs(abonent.Ls),
                        CNTTYPE = 64,
                        COUNTERID = String.Format("8{0}{1}", abonent.Ls, counter.CounterNum)
                    };
                    lcn.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();

            //  0 - voda, 1 - poliv
        }
    }

    public class ConvertCountersKosheNew : ConvertCase
    {
        public ConvertCountersKosheNew()
        {
            ConvertCaseName = "COUNTERS Koshe - данные о счетчиках НОВЫЕ СЧЕТЧИКИ";
            Position = 53;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");

            SetStepsCount(3);

            var lcn = new List<CNV_COUNTER>();
            var lci = new List<CNV_CNTRSIND>();
            var aList = Abonent.ReadFile();

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                StepStart(aList.Count);
                foreach (Abonent abonent in aList)
                {
                    foreach (var counter in abonent.CounterList)
                    {
                        int prefix = counter.CounterType == 0 ? 9 : 8;
                        int type = counter.CounterType == 0 ? 1 : 64;
                        var c = new CNV_COUNTER
                        {
                            LSHET = Consts.GetLs(abonent.Ls),
                            CNTTYPE = type,
                            COUNTERID = String.Format("{2}{0}{1}", abonent.Ls, counter.CounterNum, prefix)
                        };

                        string sql = String.Format("SELECT * FROM parentequipment PQ WHERE PQ.importtag = '{0}'",c.COUNTERID);

                        if (context.ExecuteQuery<object>(sql,CommandType.Text, new DbParameter[]{}).Count > 0) continue;

                        sql = String.Format("select * from abonentsequipment ae where ae.lshet = '{0}'", c.LSHET);

                        if (context.ExecuteQuery<object>(sql, CommandType.Text, new DbParameter[]{}).Count == abonent.CounterList.Count) continue;

                        var cin = new CNV_CNTRSIND
                        {
                            COUNTERID = String.Format("{2}{0}{1}", abonent.Ls, counter.CounterNum, prefix),
                            DOCUMENTCD = String.Format("{0}_{1}", c.COUNTERID, abonent.Ls),
                            INDDATE = counter.PayDate,
                            INDTYPE = 0,
                            OLDIND = counter.CounterValue,
                            INDICATION = counter.CounterValue
                        };

                        lcn.Add(c);
                        lci.Add(cin);
                    }
                    Iterate();
                }
                StepFinish();
            }

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();
            StepStart(1);
            SaveList(lci, Consts.InsertRecordCount);
            StepFinish();

            //  0 - voda, 1 - poliv
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
                    COUNTERID =
                        String.Format("{0}{1}{2}", counterind.Grkod.Trim('0').Trim(), counterind.Lshet.Trim('0').Trim(),
                            counterind.Counterid.ToString().Trim()),
                    DOCUMENTCD =
                        String.Format("{0}_{1}",
                            String.Format("{0}{1}{2}", counterind.Grkod.Trim('0').Trim(),
                                counterind.Lshet.Trim('0').Trim(),
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

    public class ConvertCntrsindKoshe : ConvertCase
    {
        public ConvertCntrsindKoshe()
        {
            ConvertCaseName = "CNTRSIND Koshe - данные о показаниях счетчиках";
            Position = 61;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);
            var aList = Abonent.ReadFile();
            
            var lci = new List<CNV_CNTRSIND>();

            int i = 0;
            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                foreach (var counter in abonent.CounterList)
                {
                    i++;
                    int prefix = counter.CounterType == 0 ? 9 : 8;
                    var c = new CNV_CNTRSIND
                    {
                        COUNTERID = String.Format("{2}{0}{1}", abonent.Ls, counter.CounterNum, prefix),
                        DOCUMENTCD =
                            String.Format("{0}_{1}",abonent.Ls, i),
                        INDDATE = counter.PayDate,
                        INDTYPE = 0,
                        OLDIND = counter.CounterValue,
                        INDICATION = counter.CounterValue
                    };
                    lci.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lci, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    public class ConvertCntrsindKoshePoliv : ConvertCase
    {
        public ConvertCntrsindKoshePoliv()
        {
            ConvertCaseName = "CNTRSIND Koshe - данные о показаниях счетчиках POLIV";
            Position = 61;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");

            SetStepsCount(3);
            var aList = Abonent.ReadFile();

            var lci = new List<CNV_CNTRSIND>();

            int i = 0;
            StepStart(aList.Count);
            foreach (Abonent abonent in aList)
            {
                foreach (var counter in abonent.CounterList.Where(c => c.CounterType == 1))
                {
                    i++;
                    var c = new CNV_CNTRSIND
                    {
                        COUNTERID = String.Format("8{0}{1}", abonent.Ls, counter.CounterNum),
                        DOCUMENTCD =
                            String.Format("8c{0}_{1}", abonent.Ls, i),
                        INDDATE = counter.PayDate,
                        INDTYPE = 0,
                        OLDIND = counter.CounterValue,
                        INDICATION = counter.CounterValue
                    };
                    lci.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lci, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    ///// <summary>
    ///// Конвертация данных истории начислений
    ///// </summary>
    //public class ConvertNachopl1 : ConvertCase
    //{
    //    public ConvertNachopl1()
    //    {
    //        ConvertCaseName = "NACHOPL1 - данные истории начислений до 2012";
    //        Position = 70;
    //        IsChecked = false;
    //    }

    //    public override void DoConvert()
    //    {
    //        var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
    //        tms.Init();

    //        SetStepsCount(2);

    //        BufferEntitiesManager.DropTableData("CNV$NACH");
    //        BufferEntitiesManager.DropTableData("CNV$OPLATA");
    //        BufferEntitiesManager.DropTableData("CNV$NACHOPL");
    //        var aList = Abonent.ReadFile();
    //        NachoplManager nm = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_начало);
    //        using (DataTable dt =
    //            Tmsource.ExecuteQuery(
    //                "SELECT *, RECNO() as RECNO FROM register where (dt_schet = '62.СО.1' or kt_schet = '62.СО.1') and year(date) <= 2012"))
    //        {

    //            Regex dateRegex = new Regex(@"([а-я]+) (\d+)[^\d]*", RegexOptions.IgnoreCase);
    //            var reg = new RegisterRecord();
    //            StepStart(dt.Rows.Count);
    //            foreach (DataRow dataRow in dt.Rows)
    //            {
    //                reg.ReadDataRow(dataRow);

    //                if (reg.Dt_schet.Trim() != "62.СО.1" && reg.Kt_schet.Trim() != "62.СО.1") continue;

    //                string documentcd;
    //                string lshet;
    //                int servicecd;
    //                string servicename;
    //                if (reg.Dt_schet.Trim() == "62.СО.1")
    //                {
    //                    lshet = Consts.GetLs(reg.Dt_subk1gr.Trim(), reg.Dt_subk1cd.Trim());

    //                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

    //                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

    //                    documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);
                        
    //                    switch (reg.Dt_subk2cd.Trim())
    //                    {
    //                        case "1":
    //                            continue;
    //                        case "10":
    //                            servicecd = 9;
    //                            servicename = "Электроэнергия";
    //                            break;
    //                        case "26":
    //                            servicecd = 19;
    //                            servicename = "Электроэнергия ОДН";
    //                            break;
    //                        case "4":
    //                            servicecd = 2;
    //                            servicename = "Содержание жилья";
    //                            break;
    //                        case "6":
    //                            servicecd = 1;
    //                            servicename = "Найм";
    //                            break;
    //                        case "8":
    //                            servicecd = 6;
    //                            servicename = "Вывоз отходов";
    //                            break;
    //                        default:
    //                            throw new Exception("Неизвестная услуга " + reg.Dt_subk2cd);
    //                    }

    //                    var ndef = new CNV_NACH
    //                    {
    //                        //VOLUME = tarif == 0 ? 0 : nachopl.Fnath / tarif,
    //                        VOLUME = 0,
    //                        REGIMCD = 10,
    //                        REGIMNAME = "Неизвестен",
    //                        SERVICECD = servicecd,
    //                        SERVICENAME = servicename,
    //                        TYPE_ = 0
    //                    };

    //                    var groups = dateRegex.Match(reg.Dt_subk3nm.Trim()).Groups;
    //                    DateTime date = DateTime.Parse(String.Format("01.{0}.{1}", groups[1], groups[2]));


    //                    if (reg.Doctype == "УК_СрокИсковойДавности")
    //                        nm.RegisterNach(ndef, lshet, date.Month, date.Year, 0, reg.Summa, reg.Date, documentcd);
    //                    else
    //                        nm.RegisterNach(ndef, lshet, date.Month, date.Year, reg.Summa, 0, reg.Date, documentcd);
    //                }
    //                else
    //                {
    //                    lshet = Consts.GetLs(reg.Kt_subk1gr.Trim(), reg.Kt_subk1cd.Trim());
    //                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

    //                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);
    //                    documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);
                        
    //                    switch (reg.Kt_subk2cd.Trim())
    //                    {
    //                        case "1":
    //                            continue;
    //                        case "10":
    //                            servicecd = 9;
    //                            servicename = "Электроэнергия";
    //                            break;
    //                        case "26":
    //                            servicecd = 19;
    //                            servicename = "Электроэнергия ОДН";
    //                            break;
    //                        case "4":
    //                            servicecd = 2;
    //                            servicename = "Содержание жилья";
    //                            break;
    //                        case "6":
    //                            servicecd = 1;
    //                            servicename = "Найм";
    //                            break;
    //                        case "8":
    //                            servicecd = 6;
    //                            servicename = "Вывоз отходов";
    //                            break;
    //                        default:
    //                            throw new Exception("Неизвестная услуга " + reg.Dt_subk2cd);
    //                    }

    //                    var odef = new CNV_OPLATA
    //                    {
    //                        SERVICECD = servicecd,
    //                        SERVICENAME = servicename,
    //                        SOURCECD = 17,
    //                        SOURCENAME = "Касса"
    //                    };

    //                    var groups = dateRegex.Match(reg.Kt_subk3nm).Groups;
    //                    DateTime date = DateTime.Parse(String.Format("01.{0}.{1}", groups[1], groups[2]));

    //                    nm.RegisterOplata(odef, lshet, date.Month, date.Year, reg.Summa, reg.Date, reg.Date, documentcd);
    //                }

    //                Iterate();
    //            }
    //            StepFinish();

    //        }



    //        SaveList(nm.NachRecords, Consts.InsertRecordCount);
    //        SaveList(nm.OplataRecords, Consts.InsertRecordCount);
    //        SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
    //    }
    //}

    ///// <summary>
    ///// Конвертация данных истории начислений
    ///// </summary>
    //public class ConvertNachopl2 : ConvertCase
    //{
    //    public ConvertNachopl2()
    //    {
    //        ConvertCaseName = "NACHOPL2 - данные истории начислений с 2013";
    //        Position = 70;
    //        IsChecked = false;
    //    }

    //    public override void DoConvert()
    //    {
    //        var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
    //        tms.Init();

    //        SetStepsCount(2);
    //        var aList = Abonent.ReadFile();
    //        NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
    //        using (DataTable dt =
    //            Tmsource.ExecuteQuery(
    //                "SELECT *, RECNO() as RECNO FROM register where (dt_schet = '62.СО.1' or kt_schet = '62.СО.1') and year(date) > 2012"))
    //        {

    //            Regex dateRegex = new Regex(@"([а-я]+) (\d+)[^\d]*", RegexOptions.IgnoreCase);
    //            var reg = new RegisterRecord();
    //            StepStart(dt.Rows.Count);
    //            foreach (DataRow dataRow in dt.Rows)
    //            {
    //                reg.ReadDataRow(dataRow);

    //                if (reg.Dt_schet.Trim() != "62.СО.1" && reg.Kt_schet.Trim() != "62.СО.1") continue;

    //                string documentcd;
    //                string lshet;
    //                int servicecd;
    //                string servicename;
    //                if (reg.Dt_schet.Trim() == "62.СО.1")
    //                {
    //                    lshet = Consts.GetLs(reg.Dt_subk1gr.Trim(), reg.Dt_subk1cd.Trim());
    //                                            var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

    //                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);
    //                    documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);
                        
    //                    switch (reg.Dt_subk2cd.Trim())
    //                    {
    //                        case "1":
    //                            continue;
    //                        case "10":
    //                            servicecd = 9;
    //                            servicename = "Электроэнергия";
    //                            break;
    //                        case "26":
    //                            servicecd = 19;
    //                            servicename = "Электроэнергия ОДН";
    //                            break;
    //                        case "4":
    //                            servicecd = 2;
    //                            servicename = "Содержание жилья";
    //                            break;
    //                        case "6":
    //                            servicecd = 1;
    //                            servicename = "Найм";
    //                            break;
    //                        case "8":
    //                            servicecd = 6;
    //                            servicename = "Вывоз отходов";
    //                            break;
    //                        default:
    //                            throw new Exception("Неизвестная услуга " + reg.Dt_subk2cd);
    //                    }

    //                    var ndef = new CNV_NACH
    //                    {
    //                        //VOLUME = tarif == 0 ? 0 : nachopl.Fnath / tarif,
    //                        VOLUME = 0,
    //                        REGIMCD = 10,
    //                        REGIMNAME = "Неизвестен",
    //                        SERVICECD = servicecd,
    //                        SERVICENAME = servicename,
    //                        TYPE_ = 0
    //                    };

    //                    var groups = dateRegex.Match(reg.Dt_subk3nm.Trim()).Groups;
    //                    DateTime date = DateTime.Parse(String.Format("01.{0}.{1}", groups[1], groups[2]));


    //                    if (reg.Doctype == "УК_СрокИсковойДавности")
    //                        nm.RegisterNach(ndef, lshet, date.Month, date.Year, 0, reg.Summa, reg.Date, documentcd);
    //                    else
    //                        nm.RegisterNach(ndef, lshet, date.Month, date.Year, reg.Summa, 0, reg.Date, documentcd);
    //                }
    //                else
    //                {
    //                    lshet = Consts.GetLs(reg.Kt_subk1gr.Trim(), reg.Kt_subk1cd.Trim());
    //                                            var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

    //                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);
    //                    documentcd = String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]);
                        
    //                    switch (reg.Kt_subk2cd.Trim())
    //                    {
    //                        case "1":
    //                            continue;
    //                        case "10":
    //                            servicecd = 9;
    //                            servicename = "Электроэнергия";
    //                            break;
    //                        case "26":
    //                            servicecd = 19;
    //                            servicename = "Электроэнергия ОДН";
    //                            break;
    //                        case "4":
    //                            servicecd = 2;
    //                            servicename = "Содержание жилья";
    //                            break;
    //                        case "6":
    //                            servicecd = 1;
    //                            servicename = "Найм";
    //                            break;
    //                        case "8":
    //                            servicecd = 6;
    //                            servicename = "Вывоз отходов";
    //                            break;
    //                        default:
    //                            throw new Exception("Неизвестная услуга " + reg.Dt_subk2cd);
    //                    }

    //                    var odef = new CNV_OPLATA
    //                    {
    //                        SERVICECD = servicecd,
    //                        SERVICENAME = servicename,
    //                        SOURCECD = 17,
    //                        SOURCENAME = "Касса"
    //                    };

    //                    var groups = dateRegex.Match(reg.Kt_subk3nm).Groups;
    //                    DateTime date = DateTime.Parse(String.Format("01.{0}.{1}", groups[1], groups[2]));

    //                    nm.RegisterOplata(odef, lshet, date.Month, date.Year, reg.Summa, reg.Date, reg.Date, documentcd);
    //                }

    //                Iterate();
    //            }
    //            StepFinish();

    //        }



    //        SaveList(nm.NachRecords, Consts.InsertRecordCount);
    //        SaveList(nm.OplataRecords, Consts.InsertRecordCount);
    //        SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
    //    }
    //}

    public class ConvertNachoplNew : ConvertCase
    {
        public ConvertNachoplNew()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений по годам";
            Position = 69;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            StepStart(1);
            //BufferEntitiesManager.DropTableData("CNV$NACH");
            //BufferEntitiesManager.DropTableData("CNV$OPLATA");
            //BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            StepFinish();
            StepStart(1);
            StepFinish();
            StepStart(1);
            StepFinish();
            int year = 16;
            for (int i = 04; i <= year; i++)
            {
                //InitializeManager(aConverter_RootSettings.SourceDbfFilePath);
                var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
                tms.Init();
                string sql = String.Format(
                    "select *, RECNO() as RECNO from nachopl where year = 20{0:D2} and servicecd <> 1 and servicecd <> 0",
                    i);
                using (DataTable dt = Tmsource.ExecuteQuery(sql))
                {
                    int j = 0;
                    StepStart(dt.Rows.Count);
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        j++;
                        //if (j > 10000) break;
                        nachopl.ReadDataRow(dataRow);

                        string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                        var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                        if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                        int servicecd;
                        string servicename;

                        switch (nachopl.Servicecd)
                        {
                            case 1:
                                continue;
                            case 10:
                                servicecd = 9;
                                servicename = "Электроэнергия";
                                break;
                            case 26:
                                servicecd = 19;
                                servicename = "Электроэнергия ОДН";
                                break;
                            case 4:
                                servicecd = 2;
                                servicename = "Содержание жилья";
                                break;
                            case 6:
                                servicecd = 1;
                                servicename = "Найм";
                                break;
                            case 8:
                                servicecd = 6;
                                servicename = "Вывоз отходов";
                                break;
                            default:
                                throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                        }

                        DateTime uchetDate = new DateTime((int) nachopl.Year, (int) nachopl.Month, 1);
                        DateTime forDate = new DateTime((int) nachopl.Year2, (int) nachopl.Month2, 1);
                        //uchetDate = forDate;
                        forDate = uchetDate;

                        //if (nachopl.Month == nachopl.Month2 && nachopl.Year == nachopl.Year2)
                        //{
                            var ndef = new CNV_NACH
                            {
                                REGIMCD = 10,
                                REGIMNAME = "Неизвестен",
                                SERVICECD = servicecd,
                                SERVICENAME = servicename,
                                TYPE_ = 0
                            };

                            nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                                nachopl.Prochl, uchetDate,
                                String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                            nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                                nachopl.Bdebet);
                            nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                                nachopl.Edebet);
                            //}

                            //if (nachopl.Oplata != 0)
                            //{
                            var odef = new CNV_OPLATA
                            {
                                SERVICECD = servicecd,
                                SERVICENAME = servicename,
                                SOURCECD = 17,
                                SOURCENAME = "Касса"
                            };
                            nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                                uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                        if (nachopl.Lgnach != 0)
                        {
                            var odefl = new CNV_OPLATA
                            {
                                SERVICECD = servicecd,
                                SERVICENAME = servicename,
                                SOURCECD = 111,
                                SOURCENAME = "Льготы"
                            };
                            nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                                uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                        }
                            //}
                        }
                        Iterate();
                    }

                    SaveList(nm.NachRecords, Consts.InsertRecordCount);
                    SaveList(nm.OplataRecords, Consts.InsertRecordCount);
                    SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

                    StepFinish();
                }
            }
        }

        public class ConvertNachoplKoshe : ConvertCase
        {
            public ConvertNachoplKoshe()
            {
                ConvertCaseName = "NACHOPL Koshe - данные истории начислений";
                Position = 70;
                IsChecked = false;
            }

            public override void DoConvert()
            {
                var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
                tms.Init();

                SetStepsCount(2);

                NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

                var aList = Abonent.ReadFile();
                StepStart(aList.Count);
                int i = 0;
                foreach (Abonent abonent in aList)
                {
                    foreach (var money in abonent.MoneyList)
                    {


                        i++;
                        int servicecd;
                        string servicename;
                        if (String.IsNullOrWhiteSpace(money.ServiceKod)) continue;

                        var lshet = Consts.GetLs(abonent.Ls);

                        switch (money.ServiceKod[0])
                        {
                            case '5':
                                servicecd = 4;
                                servicename = "Хол. водоснабжение";
                                break;
                            case '8':
                                servicecd = 12;
                                servicename = "Полив";
                                break;
                            case '9':
                                servicecd = 13;
                                servicename = "Выпойка";
                                break;
                            default:
                                continue;
                                throw new Exception("Неизвестная услуга " + money.ServiceKod);
                        }

                        var ndef = new CNV_NACH
                        {
                            //VOLUME = tarif == 0 ? 0 : nachopl.Fnath / tarif,
                            VOLUME = 0,
                            REGIMCD = 10,
                            REGIMNAME = "Неизвестен",
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            TYPE_ = 0
                        };

                        nm.RegisterNach(ndef, lshet, money.Month, money.Year, money.Nath, money.Prochl, new DateTime(money.Year, money.Month, 01), String.Format("N{0}_{1}", lshet.TrimStart('0'), i));

                        var odef = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 17,
                            SOURCENAME = "Касса"
                        };
                        nm.RegisterOplata(odef, lshet, money.Month, money.Year, money.Oplata,
                            new DateTime(money.Year, money.Month, 01), new DateTime(money.Year, money.Month, 01), String.Format("O{0}_{1}", lshet.TrimStart('0'), i));

                        nm.RegisterBeginSaldo(lshet, money.Month, money.Year, servicecd, servicename, money.SaldoBeg);
                        nm.RegisterEndSaldo(lshet, money.Month, money.Year, servicecd, servicename, money.SaldoEnd);
                    }
                    Iterate();
                }
                StepFinish();

                SaveList(nm.NachRecords, Consts.InsertRecordCount);
                SaveList(nm.OplataRecords, Consts.InsertRecordCount);
                SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
            }
        }

        /// <summary>
        /// Конвертация качественных характеристик
        /// Данные в таблице кодировки должны быть отсортированы по исходному ID!
        /// </summary>
        public class ConvertCharsNew : ConvertCase
        {
            public ConvertCharsNew()
            {
                ConvertCaseName = "ConvertCharsNew - данные о параметрах потребления";
                Position = 32;
                IsChecked = false;
            }

            public override void DoConvert()
            {
                var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
                tms.Init();

                SetStepsCount(4);
                var aList = Abonent.ReadFile();
                DataTable dt =
                    Tmsource.ExecuteQuery(
                        @"select distinct l.grkod as grkod, l.lshet as lshet, contractcd, servicecd, tarifcd, value, l.date as date_ from contr_1 c inner join lchars l on (c.grkod = l.grkod) and	(c.lshet = l.lshet) and	(c.CONTRACTCD = l.LCHARCD) order by l.grkod, l.lshet, servicecd, l.date desc, tarifcd desc, l.value desc");
                DataTable recodeTable =
                    Utils.ReadExcelFile(
                        @"D:\Work\C#\C#Projects\aConverter\045_SpasskStroyDetal\Sources\Таблица перекодировки1С_chars.xlsx",
                        "Лист1");
                var lcc = new List<CNV_CHAR>();

                StepStart(dt.Rows.Count);
                int i = 0;
                foreach (DataRow dataRow in dt.Rows)
                {
                    //            if (dataRow[0].ToString().Contains("00005") && dataRow[1].ToString().Contains("00001") &&
                    //dataRow["SERVICECD"].ToString() == "4")
                    //            {
                    //                int a = 10;
                    //            }

                    var kosheAbonent =
                        aList.SingleOrDefault(
                            al => al.Uklshet == Consts.GetLs(dataRow[0].ToString(), dataRow[1].ToString()));

                    var lshet = kosheAbonent != null
                        ? Consts.GetLs(kosheAbonent.Ls)
                        : Consts.GetLs(dataRow[0].ToString(), dataRow[1].ToString());

                    if (i > 0 && dataRow[0].ToString() == dt.Rows[i - 1][0].ToString() &&
                        dataRow[1].ToString() == dt.Rows[i - 1][1].ToString() &&
                        dataRow["SERVICECD"].ToString() == dt.Rows[i - 1]["SERVICECD"].ToString() &&
                        dataRow["date_"].ToString() == dt.Rows[i - 1]["date_"].ToString())
                    {
                        if (Convert.ToInt32(dataRow["TARIFCD"].ToString()) <
                            Convert.ToInt32(dt.Rows[i - 1]["TARIFCD"].ToString()))
                        {
                            Iterate();
                            i++;
                            continue;
                        }
                        if (Convert.ToInt32(dataRow["TARIFCD"].ToString()) ==
                            Convert.ToInt32(dt.Rows[i - 1]["TARIFCD"].ToString()))
                        {
                            if (Convert.ToInt32(dataRow["VALUE"].ToString()) <
                                Convert.ToInt32(dt.Rows[i - 1]["VALUE"].ToString()))
                            {
                                Iterate();
                                i++;
                                continue;
                            }
                        }
                    }

                    foreach (DataRow row in recodeTable.Rows)
                    {
                        if (row["Value1"] == DBNull.Value && row["Value2"] == DBNull.Value &&
                            row["Value3"] == DBNull.Value) break;

                        if (Int32.Parse(row["Value1"].ToString()) == Int32.Parse(dataRow["SERVICECD"].ToString()) &&
                            Int32.Parse(row["Value2"].ToString()) == Int32.Parse(dataRow["TARIFCD"].ToString()) &&
                            Int32.Parse(row["Value3"].ToString()) == Int32.Parse(dataRow["VALUE"].ToString()))
                        {

                            var lc = new CNV_CHAR
                            {
                                LSHET = lshet,
                                CHARCD = Convert.ToInt32(row["СCHARCD"]),
                                VALUE_ = Convert.ToDecimal(row["СCHARVALUE"]),
                                DATE_ = DateTime.Parse(dataRow["date_"].ToString())
                            };
                            if (lcc.Any(l => l.LSHET == lc.LSHET && l.CHARCD == lc.CHARCD && l.DATE_ == lc.DATE_))
                                continue;

                            lcc.Add(lc);
                        }
                    }
                    Iterate();
                    i++;
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


    public class ConvertNachoplNew04 : ConvertCase
    {
        public ConvertNachoplNew04()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2004";
            Position = 71;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2004 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int) nachopl.Year, (int) nachopl.Month, 1);
                    DateTime forDate = new DateTime((int) nachopl.Year2, (int) nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew05 : ConvertCase
    {
        public ConvertNachoplNew05()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2005";
            Position = 72;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2005 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew06 : ConvertCase
    {
        public ConvertNachoplNew06()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2006";
            Position = 73;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2006 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew07 : ConvertCase
    {
        public ConvertNachoplNew07()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2007";
            Position = 74;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2007 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew08 : ConvertCase
    {
        public ConvertNachoplNew08()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2008";
            Position = 75;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2008 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew09 : ConvertCase
    {
        public ConvertNachoplNew09()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2009";
            Position = 76;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2009 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew10 : ConvertCase
    {
        public ConvertNachoplNew10()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2010";
            Position = 77;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2010 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew11 : ConvertCase
    {
        public ConvertNachoplNew11()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2011";
            Position = 78;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2011 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew12 : ConvertCase
    {
        public ConvertNachoplNew12()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2012";
            Position = 79;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2012 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew13 : ConvertCase
    {
        public ConvertNachoplNew13()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2013";
            Position = 80;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2013 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew14 : ConvertCase
    {
        public ConvertNachoplNew14()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2014";
            Position = 81;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2014 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }

    public class ConvertNachoplNew15 : ConvertCase
    {
        public ConvertNachoplNew15()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2015";
            Position = 82;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2015 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

            StepFinish();
        }
    }
    
    public class ConvertNachoplNew16 : ConvertCase
    {
        public ConvertNachoplNew16()
        {
            ConvertCaseName = "NACHOPL new - данные истории начислений 2016";
            Position = 83;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);
            var aList = Abonent.ReadFile();
            NachoplManager nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
            var nachopl = new NachoplRecord();
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();
            string sql =
                "select *, RECNO() as RECNO from nachopl where year = 2016 and servicecd <> 1 and servicecd <> 0";
            using (DataTable dt = Tmsource.ExecuteQuery(sql))
            {
                StepStart(dt.Rows.Count);
                foreach (DataRow dataRow in dt.Rows)
                {
                    nachopl.ReadDataRow(dataRow);

                    string lshet = Consts.GetLs(nachopl.Grkod.Trim(), nachopl.Lshet.Trim());

                    var kosheAbonent = aList.SingleOrDefault(al => al.Uklshet == lshet);

                    if (kosheAbonent != null) lshet = Consts.GetLs(kosheAbonent.Ls);

                    int servicecd;
                    string servicename;

                    switch (nachopl.Servicecd)
                    {
                        case 1:
                            continue;
                        case 10:
                            servicecd = 9;
                            servicename = "Электроэнергия";
                            break;
                        case 26:
                            servicecd = 19;
                            servicename = "Электроэнергия ОДН";
                            break;
                        case 4:
                            servicecd = 2;
                            servicename = "Содержание жилья";
                            break;
                        case 6:
                            servicecd = 1;
                            servicename = "Найм";
                            break;
                        case 8:
                            servicecd = 6;
                            servicename = "Вывоз отходов";
                            break;
                        default:
                            throw new Exception("Неизвестная услуга " + nachopl.Servicecd);
                    }

                    DateTime uchetDate = new DateTime((int)nachopl.Year, (int)nachopl.Month, 1);
                    DateTime forDate = new DateTime((int)nachopl.Year2, (int)nachopl.Month2, 1);
                    forDate = uchetDate;

                    var ndef = new CNV_NACH
                    {
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        TYPE_ = 0
                    };

                    nm.RegisterNach(ndef, lshet, forDate.Month, forDate.Year, nachopl.Fnathall,
                        nachopl.Prochl, uchetDate,
                        String.Format("N{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));

                    nm.RegisterBeginSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Bdebet);
                    nm.RegisterEndSaldo(lshet, forDate.Month, forDate.Year, servicecd, servicename,
                        nachopl.Edebet);

                    var odef = new CNV_OPLATA
                    {
                        SERVICECD = servicecd,
                        SERVICENAME = servicename,
                        SOURCECD = 17,
                        SOURCENAME = "Касса"
                    };
                    nm.RegisterOplata(odef, lshet, forDate.Month, forDate.Year, nachopl.Oplata,
                        uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    if (nachopl.Lgnach != 0)
                    {
                        var odefl = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 111,
                            SOURCENAME = "Льготы"
                        };
                        nm.RegisterOplata(odefl, lshet, forDate.Month, forDate.Year, nachopl.Lgnach,
                            uchetDate, uchetDate, String.Format("O{0}_{1}", lshet.TrimStart('0'), dataRow["RECNO"]));
                    }
                }
                Iterate();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);

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
                fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] {"1"});
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
                fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] {"1"});
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
                fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] {"2"});
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
                fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[]
                {
                    Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
                    Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture)
                });
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

        public class Abonent
        {
            public int Ls;
            public string Fio;
            public int PunktCd;
            public string PunktName;
            public string Address;
            public string Streetnm;
            public string Ndoma;
            public string Kvartira;
            public string VidGf;
            public decimal Thprogiv;
            public decimal Square;

            public string Uklshet;

            public List<Money> MoneyList = new List<Money>();

            public List<Counter> CounterList = new List<Counter>();

            public static List<Abonent> ReadFile()
            {
                return (new XmlSerializer(typeof (List<Abonent>)).Deserialize(
                    new StringReader(File.ReadAllText(
                        @"D:\Work\C#\C#Projects\aConverter\045_SpasskStroyDetal\Sources\AbonentList.xml",
                        Encoding.GetEncoding(1251)))) as List<Abonent>).Where(a => a.Ls != 0).ToList();
            }
        }

        //public class Money
        //{
        //    public int ls;
        //    public string ServiceKod;
        //    public decimal Saldo;
        //    public decimal Nath;
        //}
        public class Money
        {
            public int Ls;
            public string ServiceKod;
            public int Month;
            public int Year;
            public decimal SaldoBeg;
            public decimal Nath;
            public decimal Prochl;
            public decimal Oplata;
            public decimal SaldoEnd;
        }

        public class Counter
        {
            public int Ls;
            public int CounterNum;
            public decimal CounterValue;
            public int CounterType;
            public DateTime PayDate;
        }
    }


