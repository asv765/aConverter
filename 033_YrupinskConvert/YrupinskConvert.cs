using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System;
using _033_Yrupinsk.Records;
using _033_YrupinskConvert;

namespace _033_YrupinskConvert
{
    public static class Consts
    {
        public static DateTime StartDate = new DateTime(2010, 1, 1);

        public static int CurrentYear = 2015;

        public static int CurrentMonth = 6;

        public static DateTime CurrentDateTime = new DateTime(CurrentYear, CurrentMonth, 1);

        public static int LastYear = CurrentMonth == 1 ? CurrentYear - 1 : CurrentYear;

        public static int LastMonth = CurrentMonth == 1 ? 12 : CurrentMonth - 1;

        public const string SourceDir = @"C:\Work\aConverter_Data\033_Yrupinsk\Source";

        public static string GetLs(long intls)
        {
            return String.Format("{0:D8}", intls);
        }
    }

    public static class ConvertUtils
    {
        public static Dictionary<long, long> GetStreetDictionary()
        {
            var d = new Dictionary<long, long>();
            DataTable dt = Utils.ReadExcelFile(@"C:\Work\aConverter_Data\033_Yrupinsk\Service\StreetRecode.xlsx",
                "Лист1");

            foreach (DataRow row in dt.Rows)
            {
                int sourcecd = Convert.ToInt32(row["SOURCECD"]);
                int abonentcd = row["ABONENTCD"] is DBNull ? 0 : Convert.ToInt32(row["ABONENTCD"]);
                if (abonentcd > 0) d.Add(sourcecd, abonentcd);
            }
            return d;
        }

        public static Dictionary<string, string> GetLshetDictionary()
        {
            var d = new Dictionary<string, string>();
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                foreach (CNV_ABONENT cnvAbonent in acem.CNV_ABONENTs)
                {
                    d.Add(cnvAbonent.EXTLSHET, cnvAbonent.LSHET);
                }
            }
            return d;
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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.ReCreateAllEntities();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    public class DropAllData : ConvertCase
    {
        public DropAllData()
        {
            ConvertCaseName = "Очистить таблицы конвертации";
            Position = 15;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllData();

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

        public uint CurrentLshet;

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);
            var streed = ConvertUtils.GetStreetDictionary();

            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();

            StepStart(dt.Rows.Count);
            var ar = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                ar.ReadDataRow(dataRow);
                long abonentStreetCd;
                if (streed.TryGetValue(ar.Ulicakod, out abonentStreetCd))
                {
                    var a = new CNV_ABONENT()
                    {
                        EXTLSHET = ar.Lshet,
                        DISTKOD = 1,
                        DISTNAME = "Волгоградская область",
                        RAYONKOD = 1,
                        RAYONNAME = "Урюпинск",
                        TOWNSKOD = 1,
                        TOWNSNAME = "Урюпинск",
                        KVARTIRA = ar.Kvartira,
                        PRIM_ = ar.Prim,
                        POSTINDEX = ar.Postindex,
                        DUCD = 8080,
                        DUNAME = "Частный сектор",
                        ULICAKOD = (int)abonentStreetCd,
                        ULICANAME = ar.Ulicaname
                    };
                    
                    Match m = Regex.Match(ar.Ndoma, @"^[0-9]+");
                    if (m.Success) a.HOUSENO = m.Value;
                    m = Regex.Match(ar.Ndoma, @"(?<=^[0-9]+)[^0-9]\w*");
                    if (m.Success) a.HOUSEPOSTFIX = m.Value.Trim().ToUpper();

                    int korpus;
                    if (Int32.TryParse(ar.Korpus, out korpus))
                        a.KORPUSNO = korpus;
                    else
                        a.HOUSEPOSTFIX += ar.Korpus.Trim();
                    if (a.HOUSEPOSTFIX == "") a.HOUSEPOSTFIX = null;
                    string[] fio = Utils.SplitFio(ar.Fio);
                    a.F = fio[0];
                    a.I = fio[1];
                    a.O = fio[2];

                    lca.Add(a);
                }
                Iterate();
            }
            StepFinish();


            StepStart(1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(lca);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }


    }

    /// <summary>
    /// Ищет существующие дома
    /// </summary>
    public class FindHouses : ConvertCase
    {
        public FindHouses()
        {
            ConvertCaseName = "Поиск домов и заполнение HOUSECD по найденным домам";
            Position = 30;
            IsChecked = true;            
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                StepStart(acem.HOUSEs.Count());
                foreach (var house in acem.HOUSEs)
                {
                    if (!String.IsNullOrEmpty(house.HOUSEPOSTFIX)) house.HOUSEPOSTFIX = house.HOUSEPOSTFIX.ToUpper();
                    Iterate();
                }
                StepFinish();

                StepStart(acem.CNV_ABONENTs.Count());
                acem.Log = new StreamWriter(@"housefind.log");
                foreach (CNV_ABONENT cnvAbonent in acem.CNV_ABONENTs)
                {
                    int? streetcd = cnvAbonent.ULICAKOD;
                    string houseno = String.IsNullOrEmpty(cnvAbonent.HOUSENO) ? null : cnvAbonent.HOUSENO.ToUpper();
                    string housepostfix = String.IsNullOrEmpty(cnvAbonent.HOUSEPOSTFIX) ? null : cnvAbonent.HOUSEPOSTFIX.ToUpper();
                    int? korpusno = cnvAbonent.KORPUSNO;

                    HOUSE h = acem.HOUSEs.FirstOrDefault(p =>
                        p.STREETCD == streetcd &&
                        p.HOUSENO == houseno &&
                        p.HOUSEPOSTFIX == housepostfix &&
                        p.KORPUSNO == korpusno);
                    if (h != null) 
                        cnvAbonent.HOUSECD = h.HOUSECD;
                }
                acem.SaveChanges();
                Iterate();
            }
            StepFinish();   
        }
    }

    public class FindAbonents : ConvertCase
    {
        public FindAbonents() 
        {
            ConvertCaseName = "Поиск абонентов и заполнение LSHET по найденным абонентам";
            Position = 40;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                StepStart(acem.CNV_ABONENTs.Count());
                acem.Log = new StreamWriter(@"abonentsfind.log");
                foreach (CNV_ABONENT cnvAbonent in acem.CNV_ABONENTs)
                {
                    ABONENT a = acem.ABONENTs.FirstOrDefault(p =>
                        p.HOUSECD == cnvAbonent.HOUSECD &&
                        p.FIO == cnvAbonent.F &&
                        p.NAME == cnvAbonent.I &&
                        p.SECOND_NAME == cnvAbonent.O);
                    if (a != null)
                        cnvAbonent.LSHET = a.LSHET;
                }
                acem.SaveChanges();
                Iterate();
            }
            StepFinish();
        }
    }

    public class FillEmptyLshetAndHousecd : ConvertCase
    {
        public FillEmptyLshetAndHousecd()
        {
            ConvertCaseName = "Заполняем пустые лицевые счета и HOUSECD";
            Position = 50;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                SetStepsCount(2);
                // Заполняем HOUSECD

                StepStart(1);
                int maxhousecd = acem.HOUSEs.Max(p => p.HOUSECD);
                AbonentRecordUtils.SetUniqueHouseCd(acem.CNV_ABONENTs, maxhousecd);
                StepFinish();

                int maxlshet = Convert.ToInt32(acem.ABONENTs.Max(p => p.LSHET));
                var q = from a in acem.CNV_ABONENTs
                    where a.LSHET == null 
                    select a;
                StepStart(q.Count());
                foreach (var cnvAbonent in q)
                {
                    if (cnvAbonent.LSHET == null) cnvAbonent.LSHET = Consts.GetLs(++maxlshet);
                    Iterate();
                }
                StepFinish();
                acem.SaveChanges();
                
            }
            StepFinish();
            
        }
    }

    public class CharsConvert : ConvertCase
    {
        public CharsConvert()
        {
            ConvertCaseName = "CHARS - данные о количественных характеристиках";
            Position = 60;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            StepStart(dt.Rows.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var cr = new CharsRecord();
                var ld = ConvertUtils.GetLshetDictionary();
                foreach (DataRow dataRow in dt.Rows)
                {
                    cr.ReadDataRow(dataRow);
                    string lshet;
                    if (ld.TryGetValue(cr.Lshet, out lshet))
                    {
                        var c = new CNV_CHAR()
                        {
                            CHARCD = (int) cr.Charcd,
                            CHARNAME = cr.Charname,
                            DATE_ = cr.Date,
                            VALUE_ = cr.Value_,
                            LSHET = lshet
                        };
                        acem.Add(c);
                    }
                    Iterate();
                }
                acem.SaveChanges();
            }
            StepFinish();
        }
    }

    public class LCharsConvert : ConvertCase
    {
        public LCharsConvert()
        {
            ConvertCaseName = "LCHARS - данные о количественных характеристиках";
            Position = 65;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.ExecuteQuery("SELECT * FROM LCHARS ORDER BY LSHET, LCHARCD, `DATE` DESC ");
            DataTable dtrecode = Utils.ReadExcelFile(
                @"C:\Work\aConverter_Data\033_Yrupinsk\Service\LcharsRecode.xlsx",
                "Лист1");
            StepStart(dt.Rows.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var lcr = new LcharsRecord();
                var ld = ConvertUtils.GetLshetDictionary();
                string oldlshet = "-1";
                long oldlcharcd = -1;
                var errors = new List<string>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    lcr.ReadDataRow(dataRow);
                    string lshet;
                    if (ld.TryGetValue(lcr.Lshet, out lshet))
                    {
                        // if (lcr.Lshet == oldlshet && lcr.Lcharcd == oldlcharcd) continue;
                        oldlshet = lcr.Lshet;
                        oldlcharcd = lcr.Lcharcd;
                        bool found = false;
                        foreach (DataRow recodeDataRow in dtrecode.Rows)
                        {
                            long lcharcd = Convert.ToInt64(recodeDataRow["LCHARCD"]);
                            long value = Convert.ToInt64(recodeDataRow["VALUE"]);
                            if (lcharcd  == lcr.Lcharcd &&
                                value == lcr.Value_)
                            {
                                int ablcharcd = Convert.ToInt32(recodeDataRow["ABLCHARCD"]);
                                int abvalue = Convert.ToInt32(recodeDataRow["ABVALUE"]);
                                string comment = recodeDataRow["COMMENT"].ToString();
                                var lc = new CNV_LCHAR()
                                {
                                    LCHARCD = ablcharcd,
                                    LCHARNAME = "Характеристика с кодом " + ablcharcd,
                                    DATE_ = lcr.Date,
                                    VALUE_ = abvalue,
                                    VALUEDESC = comment,
                                    LSHET = lshet
                                };
                                acem.Add(lc);
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            string message =
                                String.Format(
                                    "В таблице перекодировки не найдено значения для lcharcd = {0} ({1}), value = {2} ({3})",
                                    lcr.Lcharcd, 
                                    lcr.Lcharname.Trim(),
                                    lcr.Value_,
                                    lcr.Valuedesc.Trim());
                            if (!errors.Exists(p => p == message))
                                errors.Add(message);
                        }
                    }
                    Iterate();
                }
                File.WriteAllLines("errors.txt", errors, Encoding.GetEncoding(1251));
                acem.SaveChanges();
            }
            StepFinish();
        }
    }

    public class CountersConvert : ConvertCase
    {
        public CountersConvert()
        {
            ConvertCaseName = "COUNTERS - даннные о счетчиках";
            Position = 70;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");
            StepStart(dt.Rows.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var cr = new CountersRecord();
                var ld = ConvertUtils.GetLshetDictionary();
                foreach (DataRow dataRow in dt.Rows)
                {
                    cr.ReadDataRow(dataRow);
                    string lshet;
                    if (ld.TryGetValue(cr.Lshet, out lshet))
                    {
                        var c = new CNV_COUNTER()
                        {
                            LSHET = lshet,
                            CNTTYPE = 10, 
                            CNTNAME = "Счетчик холодной воды",
                            COUNTERID = cr.Kod.ToString(CultureInfo.InvariantCulture),
                            NAME = cr.Cntname
                        };
                        if (cr.Setupdate > new DateTime(2000, 1, 1))
                            c.SETUPDATE = cr.Setupdate;
                        if (!String.IsNullOrEmpty(cr.Serialnum))
                            c.SERIALNUM = cr.Serialnum;
                        acem.Add(c);
                    }
                    Iterate();
                }
                acem.SaveChanges();
            }
            StepFinish();
        }
    }

    public class CntrsindConvert : ConvertCase
    {
        public CntrsindConvert()
        {
            ConvertCaseName = "CNTRSIND - даннные о показаниях счетчиков";
            Position = 80;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DataTable dt = Tmsource.GetDataTable("CNTRSIND");
            StepStart(dt.Rows.Count);
            
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var q = (from c in acem.CNV_COUNTERs select c.COUNTERID).ToList();

                var cr = new CntrsindRecord();
                int i = 0;
                foreach (DataRow dataRow in dt.Rows)
                {
                    cr.ReadDataRow(dataRow);
                    string counterid = cr.Kod.ToString(CultureInfo.InvariantCulture);
                    if (q.Contains(counterid))
                    {
                        var c = new CNV_CNTRSIND()
                        {
                            COUNTERID = counterid,
                            DOCUMENTCD = (i++).ToString(CultureInfo.InvariantCulture),
                            INDDATE = cr.Inddate,
                            INDICATION = cr.Indication,
                            INDTYPE = 0,
                            OB_EM = cr.Volume,
                            OLDIND = cr.Indication - cr.Volume
                        };
                        acem.Add(c);
                    }
                    Iterate();
                }
                acem.SaveChanges();
            }
            StepFinish();
        }
    }

}
