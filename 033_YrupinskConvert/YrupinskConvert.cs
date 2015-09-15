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
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System;
using _033_Yrupinsk;
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
            return String.Format("01{0:D6}", intls);
        }
    }

    public static class ConvertUtils
    {
        public static Dictionary<long, long> GetStreetRecodeDictionary()
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

        public static Dictionary<StreetKey, string> GetStreetNameDictionary()
        {
            var d = new Dictionary<StreetKey, string>();
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                foreach (STREET s in acem.STREETs)
                {
                    var sk = new StreetKey()
                    {
                        Punktcd = s.PUNKTCD,
                        Streetcd = s.STREETCD
                    };
                    d.Add(sk, s.STREETNM);
                }
            }
            return d;
        }
    }

    public struct StreetKey
    {
        public int Streetcd;
        public int Punktcd;
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

    public class ReCreateProcedures : ConvertCase
    {
        public ReCreateProcedures()
        {
            ConvertCaseName = "Пересоздать хранимые процедуры";
            Position = 15;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    //public class DropAllData : ConvertCase
    //{
    //    public DropAllData()
    //    {
    //        ConvertCaseName = "Очистить таблицы конвертации";
    //        Position = 15;
    //        IsChecked = false;
    //    }

    //    public override void DoConvert()
    //    {
    //        SetStepsCount(1);
    //        StepStart(1);

    //        BufferEntitiesManager.DropAllData();

    //        Result = ConvertCaseStatus.Шаг_выполнен_успешно;
    //        Iterate();
    //    }
    //}

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
            var streed = ConvertUtils.GetStreetRecodeDictionary();
            var streetnamed = ConvertUtils.GetStreetNameDictionary();

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
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
                        LSHET = Consts.GetLs(Convert.ToInt64(ar.Lshet)),
                        EXTLSHET = ar.Lshet,
                        DISTKOD = 1,
                        DISTNAME = "Волгоградская область",
                        RAYONKOD = 1,
                        RAYONNAME = "Урюпинск",
                        TOWNSKOD = 1,
                        TOWNSNAME = "Урюпинск",
                        PRIM_ = ar.Prim,
                        POSTINDEX = ar.Postindex,
                        DUCD = 1,
                        DUNAME = "МУП 'Водоканал'",
                        ULICAKOD = (int)abonentStreetCd,
                        ISDELETED = 0
                    };

                    int roomno;
                    if (Int32.TryParse(ar.Kvartira, out roomno))
                        a.ROOMNO = (short) roomno;
                    string ulicaname;
                    var sk = new StreetKey()
                    {
                        Streetcd = (int)abonentStreetCd,
                        Punktcd = 1
                    };
                    a.ULICANAME = streetnamed.TryGetValue(sk, out ulicaname) ? ulicaname : ar.Ulicaname;
                    
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

    //public class FindAbonents : ConvertCase
    //{
    //    public FindAbonents() 
    //    {
    //        ConvertCaseName = "Поиск абонентов и заполнение LSHET по найденным абонентам";
    //        Position = 40;
    //        IsChecked = true;
    //    }

    //    public override void DoConvert()
    //    {
    //        SetStepsCount(1);

    //        using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
    //        {
    //            StepStart(acem.CNV_ABONENTs.Count());
    //            acem.Log = new StreamWriter(@"abonentsfind.log");
    //            foreach (CNV_ABONENT cnvAbonent in acem.CNV_ABONENTs)
    //            {
    //                string extlshet = cnvAbonent.EXTLSHET;
    //                int extnumlshet;
    //                if (int.TryParse(extlshet, out extnumlshet))
    //                {
    //                    string lshet = Consts.GetLs(1000000 + extnumlshet);
    //                    ABONENT a = acem.ABONENTs.FirstOrDefault(p => p.LSHET == lshet);
    //                    //ABONENT a = acem.ABONENTs.FirstOrDefault(p =>
    //                    //    p.HOUSECD == cnvAbonent.HOUSECD &&
    //                    //    p.FIO == cnvAbonent.F &&
    //                    //    p.NAME == cnvAbonent.I &&
    //                    //    p.SECOND_NAME == cnvAbonent.O);
    //                    if (a != null)
    //                        cnvAbonent.LSHET = a.LSHET;
    //                }
    //            }
    //            acem.SaveChanges();
    //            Iterate();
    //        }
    //        StepFinish();
    //    }
    //}

    public class FillEmptyHousecd : ConvertCase
    {
        public FillEmptyHousecd()
        {
            ConvertCaseName = "Заполняем пустые HOUSECD";
            Position = 50;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                SetStepsCount(1);
                // Заполняем HOUSECD

                StepStart(1);
                int maxhousecd = acem.HOUSEs.Max(p => p.HOUSECD);
                AbonentRecordUtils.SetUniqueHouseCd(acem.CNV_ABONENTs, maxhousecd);
                StepFinish();

                //int maxlshet = Convert.ToInt32(acem.ABONENTs.Max(p => p.LSHET));
                //var q = from a in acem.CNV_ABONENTs
                //    where a.LSHET == null 
                //    select a;
                //StepStart(q.Count());
                //foreach (var cnvAbonent in q)
                //{
                //    if (cnvAbonent.LSHET == null) cnvAbonent.LSHET = Consts.GetLs(++maxlshet);
                //    Iterate();
                //}
                //StepFinish();
                acem.SaveChanges();
                
            }
            StepFinish();
            
        }
    }

    public struct CharKey
    {
        public string Lshet;
        public long Cd;
        public DateTime Date_;
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
                var chard = new Dictionary<CharKey, CNV_CHAR>();
                var crl = new List<CNV_CHAR>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    cr.ReadDataRow(dataRow);
                    string lshet;
                    if (ld.TryGetValue(cr.Lshet, out lshet))
                    {
                        var ck = new CharKey()
                        {
                            Lshet = lshet,
                            Cd = cr.Charcd,
                            Date_ = cr.Date
                        };
                        if (!chard.ContainsKey(ck))
                        {
                            var charcd = (int) cr.Charcd;
                            if (charcd == 2) charcd = 15;
                            var c = new CNV_CHAR()
                            {
                                CHARCD = charcd,
                                CHARNAME = cr.Charname,
                                DATE_ = cr.Date,
                                VALUE_ = cr.Value_,
                                LSHET = lshet
                            };
                            crl.Add(c);
                            chard.Add(ck, c);
                        }
                    }
                    Iterate();
                }
                crl = CharsRecordUtils.ThinOutList(crl);
                acem.Add(crl);
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
            DataTable dt = Tmsource.ExecuteQuery("SELECT * FROM LCHARS WHERE EMPTY(ENDDATE) ORDER BY LSHET, LCHARCD, `DATE` DESC ");
            DataTable dtrecode = Utils.ReadExcelFile(
                @"C:\Work\aConverter_Data\033_Yrupinsk\Service\LcharsRecode.xlsx",
                "Лист1");
            StepStart(dt.Rows.Count);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                var lcr = new LcharsRecord();
                var ld = ConvertUtils.GetLshetDictionary();
                var lcharkeys = new List<CharKey>();
                var lcl = new List<CNV_LCHAR>();
                //string oldlshet = "-1";
                //long oldlcharcd = -1;
                var errors = new List<string>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    lcr.ReadDataRow(dataRow);
                    string lshet;
                    if (ld.TryGetValue(lcr.Lshet, out lshet))
                    {
                        // if (lcr.Lshet == oldlshet && lcr.Lcharcd == oldlcharcd) continue;
                        //oldlshet = lcr.Lshet;
                        //oldlcharcd = lcr.Lcharcd;
                        bool found = false;
                        foreach (DataRow recodeDataRow in dtrecode.Rows)
                        {
                            long lcharcd = Convert.ToInt64(recodeDataRow["LCHARCD"]);
                            long value = Convert.ToInt64(recodeDataRow["VALUE"]);
                            if (lcharcd == lcr.Lcharcd && value == lcr.Value_)
                            {
                                int ablcharcd = Convert.ToInt32(recodeDataRow["ABLCHARCD"]);
                                int abvalue = Convert.ToInt32(recodeDataRow["ABVALUE"]);
                                string comment = recodeDataRow["COMMENT"].ToString();
                                var ck = new CharKey()
                                {
                                    Lshet = lshet,
                                    Cd = ablcharcd,
                                    Date_ = lcr.Date
                                };
                                if (!lcharkeys.Contains(ck))
                                {
                                    lcharkeys.Add(ck);
                                    var lc = new CNV_LCHAR()
                                    {
                                        LCHARCD = ablcharcd,
                                        LCHARNAME = "Характеристика с кодом " + ablcharcd,
                                        DATE_ = lcr.Date,
                                        VALUE_ = abvalue,
                                        VALUEDESC = comment,
                                        LSHET = lshet
                                    };
                                    lcl.Add(lc);
                                }
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
                lcl = LcharsRecordUtils.ThinOutList(lcl);
                acem.Add(lcl);
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
            // По таблице LCHARS.DBF получаем словарь <номер счетчика>, <код типа счетчика>
            // Код типа счетчика определяется в зависимости от того, является ли услуга "поливом" (код типа счетчика 105)
            // либо это водоснабжение/водоотведение (код 10)

            DataTable dt1 = Tmsource.ExecuteQuery("SELECT * FROM LCHARS WHERE KOD > 0");
            var polivCodes = new[] {12, 13, 35, 37, 61, 62, 69, 73};
            var counterTypeDic = new Dictionary<int, int>();
            foreach (DataRow dr in dt1.Rows)
            {
                int counterTypeId;
                int lcharsKod = Convert.ToInt32(dr["KOD"]);
                int lcharsValue = Convert.ToInt32(dr["VALUE"]);
                if (!counterTypeDic.TryGetValue(lcharsKod, out counterTypeId))
                {
                    if (polivCodes.Contains(lcharsValue))
                        counterTypeDic.Add(lcharsKod, 105);
                    else
                        counterTypeDic.Add(lcharsKod, 10);
                }
            }
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
                        int counterTypeId;
                        if (!counterTypeDic.TryGetValue((int) cr.Kod, out counterTypeId))
                            counterTypeId = 10;
                        string counterName;
                        if (counterTypeId == 10)
                            counterName = "Счетчик холодной воды";
                        else
                            counterName = "Счетчик на полив";
                        var c = new CNV_COUNTER()
                        {
                            LSHET = lshet,
                            CNTTYPE = counterTypeId, 
                            CNTNAME = counterName,
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
                int counter = 0;
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
                    if (counter++ % 1000 == 0) 
                        acem.SaveChanges();
                }
                acem.SaveChanges();
            }
            StepFinish();
        }
    }

    public class ConvertationAddressObjects : ConvertCase
    {
        public ConvertationAddressObjects()
        {
            ConvertCaseName = "Перенос данных об адресных объектах";
            Position = 1000;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(6);

            using (var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection))
            {
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
            }
            StepFinish();
        }
    }

    public class ConvertAbonents : ConvertCase
    {
        public ConvertAbonents()
        {
            ConvertCaseName = "Перенос данных об абонентах";
            Position = 1010;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            using (var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
                Iterate();
            }
            StepFinish();
        }
    }

    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках";
            Position = 1020;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            using (var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "1" });
                Iterate();
            }
            StepFinish();
        }
    }

    public class ConvertLchars : ConvertCase
    {
        public ConvertLchars()
        {
            ConvertCaseName = "Перенос данных о качественных характеристиках";
            Position = 1030;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            using (var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "1" });
                Iterate();
            }
            StepFinish();
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "Перенос данных о счетчиках";
            Position = 1040;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            using (var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection))
            {
                fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
                Iterate();
                fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "1" });
                Iterate();
            }
            StepFinish();
        }
    }

}
