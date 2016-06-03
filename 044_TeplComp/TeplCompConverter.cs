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

namespace _044_TeplComp
{
    public class Consts
    {
        public static string GetLs(string intls)
        {
            string s = String.Format("96{0:D6}", Convert.ToInt64(intls));
            return s.Substring(0, 8);
        }

        public static readonly int CurrentMonth = 06;

        public static readonly int CurrentYear = 2016;

        public const string SvodTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Копия svod_lMZxP 04.05.2016.xlsx";

        public const string SvodSheetName = "6618110";
        public const string OborotTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\1.xls";

        public const string RecodeTableFileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Таблица перекодировки (3).xlsx";

        public const string OborotSheetName = "662209 (2)";

        public const string CountersFileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Приборы учета_12.15.xls";
        public const string CounterSheetName = "662209";

        public const int InsertRecordCount = 1000;

        public static dynamic[] SvodFiles =
        {
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_10.15.xlsx", SheetName = "662208", Date = new DateTime(2015,10,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_11.15.xlsx", SheetName = "662208", Date = new DateTime(2015,11,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_12.15.xlsx", SheetName = "662208", Date = new DateTime(2015,12,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_01.16.xlsx", SheetName = "662208", Date = new DateTime(2016,01,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_02.16.xlsx", SheetName = "662208", Date = new DateTime(2016,02,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_03.16.xlsx", SheetName = "662208", Date = new DateTime(2016,03,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_04.16.xlsx", SheetName = "662208", Date = new DateTime(2016,04,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\свод_05.16.xlsx", SheetName = "662208", Date = new DateTime(2016,05,01)},
        };

        public static dynamic[] CounterFiles =
        {
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_10.15.xls", SheetName = "662209", Date = new DateTime(2015,10,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_11.15.xls", SheetName = "662209", Date = new DateTime(2015,11,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_12.15.xls", SheetName = "662209", Date = new DateTime(2015,12,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_01.16.xls", SheetName = "662209", Date = new DateTime(2016,01,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_02.16.xls", SheetName = "662209", Date = new DateTime(2016,02,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_03.16.xls", SheetName = "662209", Date = new DateTime(2016,03,01)},
            new {FileName = @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Показания_04.16.xls", SheetName = "662209", Date = new DateTime(2016,04,01)},
        };
    }

    public class RecordSvod
    {
        public string TownName { get; set; }
        public string Lshet { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public int? RegCount { get; set; }
        public int? LiveCount { get; set; }
        public decimal? Square { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string HousePostrifx { get; set; }
        public int? FlatNumber { get; set; }
        public string FlatPostrifx { get; set; }
        public ServiceRecord Otoplenie { get; set; }
        public ServiceRecord GVS { get; set; }
        public ServiceRecord HVS { get; set; }
        public ServiceRecord Vodootvedenie { get; set; }
        public string Dogovor { get; set; }

        public RecordSvod(DataRow row)
        {
            TownName = row[0].ToString();
            Lshet = row[1].ToString();

            var fioRegex = new Regex(@"([а-я]+)[^а-я]*([а-я]*)[^а-я]*([а-я]*)", RegexOptions.IgnoreCase);
            var fioGroups = fioRegex.Match(row[3].ToString()).Groups;
            if (fioGroups[1].Success) F = fioGroups[1].Value;
            if (fioGroups[2].Success) I = fioGroups[2].Value;
            if (fioGroups[3].Success) O = fioGroups[3].Value;

            Dogovor = row.IsNull(3) || String.IsNullOrWhiteSpace(row[3].ToString()) ? @"""""" : row[3].ToString();
            RegCount = String.IsNullOrWhiteSpace(row[4].ToString()) ? (int?) null : Int32.Parse(row[4].ToString());
            LiveCount = String.IsNullOrWhiteSpace(row[5].ToString()) ? (int?) null : Int32.Parse(row[5].ToString());
            Square = String.IsNullOrWhiteSpace(row[6].ToString()) ? (decimal?) null : Decimal.Parse(row[6].ToString());

            StreetName = String.IsNullOrWhiteSpace(row[7].ToString()) ? "" : row[7].ToString();
            var houseRegex = new Regex(@"(\d+)(.*)");
            var houseGroups = houseRegex.Match(row[8].ToString()).Groups;
            if (houseGroups[1].Success) HouseNumber = Int32.Parse(houseGroups[1].Value);
            if (houseGroups[2].Success) HousePostrifx = houseGroups[2].Value;

            var flatGroups = houseRegex.Match(row[9].ToString()).Groups;
            if (flatGroups[1].Success) FlatNumber = Int32.Parse(flatGroups[1].Value);
            if (flatGroups[2].Success) FlatPostrifx = flatGroups[2].Value;

            try
            {
                Otoplenie = new ServiceRecord(row, 11, "Отопление");
            }
            catch (MissingFieldException)
            {
                Otoplenie = null;
            }
            try
            {
                GVS = new ServiceRecord(row, 22, "ГВС");
            }
            catch (MissingFieldException)
            {
                GVS = null;
            }
            try
            {
                HVS = new ServiceRecord(row, 33, "ХВС");
            }
            catch (MissingFieldException)
            {
                HVS = null;
            }
            try
            {
                Vodootvedenie = new ServiceRecord(row, 44, "ВОДООТВЕДЕНИЕ");
            }
            catch (MissingFieldException)
            {
                Vodootvedenie = null;
            }
        }

        public class ServiceRecord
        {
            public string ServiceName { get; set; }
            public decimal NachVolume { get; set; }
            public decimal Nach { get; set; }
            public decimal Pere { get; set; }
            public decimal Opl { get; set; }
            public decimal EndSaldo { get; set; }
            public decimal BeginSaldo { get; set; }


            public ServiceRecord(DataRow row, int startIndex, string name)
            {
                startIndex++;
                ServiceName = name;
                NachVolume = String.IsNullOrWhiteSpace(row[0 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[0 + startIndex].ToString());
                Nach = String.IsNullOrWhiteSpace(row[1 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[1 + startIndex].ToString());
                Pere = String.IsNullOrWhiteSpace(row[3 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[3 + startIndex].ToString());
                Opl = String.IsNullOrWhiteSpace(row[4 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[4 + startIndex].ToString());
                EndSaldo = String.IsNullOrWhiteSpace(row[7 + startIndex].ToString()) 
                    ? 0
                    : Decimal.Parse(row[7 + startIndex].ToString());
                BeginSaldo = EndSaldo - Nach - Pere + Opl;

                //if (Nach == 0 && Pere == 0 && Opl == 0 && EndSaldo == 0) throw new MissingFieldException();
            }
        }
    }

    public class RecordOborot
    {
        public string Lshet { get; set; }
        public string Address { get; set; }
        public string Rayon { get; set; }
        public decimal BeginSaldo { get; set; }
        public decimal Nach { get; set; }
        public decimal Opl { get; set; }
        public decimal EndSaldo { get; set; }

        public RecordOborot(DataRow row)
        {
            Lshet = row[1].ToString();
            Address = String.IsNullOrWhiteSpace(row[2].ToString()) ? null : row[2].ToString();
            Rayon = String.IsNullOrWhiteSpace(row[3].ToString()) ? null : row[3].ToString();
            BeginSaldo = Decimal.Parse(row[4].ToString());
            BeginSaldo = Decimal.Parse(row[5].ToString());
            BeginSaldo = Decimal.Parse(row[6].ToString());
            BeginSaldo = Decimal.Parse(row[7].ToString());
        }

        public static RecordOborot FindRecord(DataTable oborotTable, string lshet)
        {
            foreach (DataRow dataRow in oborotTable.Rows)
            {
                var record = new RecordOborot(dataRow);
                if (record.Lshet == lshet) return record;
            }
            return null;
        }
    }

    public class RecordCounter
    {
        public string Lshet { get; set; }
        public int Type { get; set; }
        public string SerialNumber { get; set; }
        public double? BegInit { get; set; }
        public double? EndInit { get; set; }
        public string Prim { get; set; }
        public DateTime? Pov { get; set; }

        public RecordCounter(DataRow row)
        {
            try
            {
                Lshet = row[1].ToString();
                SerialNumber = row[5].ToString();
                Prim = row[9].ToString();
                Pov = row.IsNull(8) || String.IsNullOrWhiteSpace(row[8].ToString()) ? null : row[8] as DateTime?;

                switch (row[3].ToString().Trim().ToLower())
                {
                    case "горячее водоснабжение":
                        Type = 3177;
                        break;
                    case "хов":
                        Type = 1;
                        break;
                    case "водотведение":
                        Type = 999;
                        break;
                    case "хвс юр.":
                        Type = 999;
                        break;
                    default:
                        throw new Exception("Неизвестная услуга " + row[3]);
                }

                double? init = row.IsNull(6) || String.IsNullOrWhiteSpace(row[6].ToString())
                    ? (double?)null : Double.Parse(row[6].ToString().Replace('.',','));
                double? end = row.IsNull(7) || String.IsNullOrWhiteSpace(row[7].ToString())
                    ? (double?)null : Double.Parse(row[7].ToString().Replace('.', ','));

                if (init == null && end == null) BegInit = null;
                else if (init == null) BegInit = end;
                else BegInit = init;

                if (init == null && end == null) EndInit = null;
                else if (end == null) EndInit = init;
                else EndInit = end;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
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

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();
            BufferEntitiesManager.CreateAllEntities();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

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
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            dynamic lastSvodFile = Consts.SvodFiles.Last();
            DataTable svodTable = Utils.ReadExcelFile(lastSvodFile.FileName, lastSvodFile.SheetName);

            var la = new List<CNV_ABONENT>();
            StepStart(svodTable.Rows.Count);
            for (int i = 0; i < svodTable.Rows.Count - 1; i++)
            {
                if (i < 5) continue;

                var recordSvod = new RecordSvod(svodTable.Rows[i]);
                if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(recordSvod.Lshet),
                    EXTLSHET = recordSvod.Lshet,
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 2,
                    RAYONNAME = "Рязанский район",
                    DUCD = 1,
                    DUNAME = "Тепловая компания Рязанского района",
                    ISDELETED = 0,
                    F = recordSvod.F,
                    I = recordSvod.I,
                    O = recordSvod.O,
                    TOWNSNAME = recordSvod.TownName,
                    ULICANAME = recordSvod.StreetName,
                    HOUSENO = recordSvod.HouseNumber == null ? null : recordSvod.HouseNumber.ToString(),
                    HOUSEPOSTFIX = recordSvod.HousePostrifx,
                    FLATNO = recordSvod.FlatNumber,
                    FLATPOSTFIX = recordSvod.FlatPostrifx
                };
                la.Add(abonent);
                Iterate();
            }
            StepFinish();

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(la, 0);
            StepFinish();

            SaveList(la, Consts.InsertRecordCount);
        }
    }

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
            SetStepsCount(9999);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            var lcc = new List<CNV_CHAR>();

            foreach (dynamic svodFile in Consts.SvodFiles)
            {
                DataTable svodTable = Utils.ReadExcelFile(svodFile.FileName, svodFile.SheetName);
                StepStart(svodTable.Rows.Count);
                for (int i = 0; i < svodTable.Rows.Count - 1; i++)
                {
                    if (i < 5) continue;
                    var recordSvod = new RecordSvod(svodTable.Rows[i]);
                    if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                    if (recordSvod.LiveCount != null)
                    {
                        var c = new CNV_CHAR
                        {
                            LSHET = Consts.GetLs(recordSvod.Lshet),
                            CHARCD = 1,
                            VALUE_ = recordSvod.LiveCount,
                            DATE_ = svodFile.Date
                        };
                        lcc.Add(c);
                    }
                    if (recordSvod.Square != null)
                    {
                        var c = new CNV_CHAR
                        {
                            LSHET = Consts.GetLs(recordSvod.Lshet),
                            CHARCD = 4,
                            VALUE_ = recordSvod.Square,
                            DATE_ = svodFile.Date
                        };
                        lcc.Add(c);
                    }
                    Iterate();
                }
                StepFinish();
            }

            StepStart(1);
            lcc = CharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            SaveList(lcc, Consts.InsertRecordCount);
        }
    }

    public class ConvertLchars : ConvertCase
    {
        public ConvertLchars()
        {
            ConvertCaseName = "LCHARS - данные о качественных характеристиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable recodeTable = Utils.ReadExcelFile(Consts.RecodeTableFileName, "Лист1");

            var llc = new List<CNV_LCHAR>();

            foreach (dynamic svodFile in Consts.SvodFiles)
            {
                DataTable svodTable = Utils.ReadExcelFile(svodFile.FileName, svodFile.SheetName);
                StepStart(svodTable.Rows.Count);
                for (int i = 0; i < svodTable.Rows.Count - 1; i++)
                {
                    if (i < 5) continue;

                    var recordSvod = new RecordSvod(svodTable.Rows[i]);
                    if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;

                    if (recordSvod.Lshet.Contains("040260"))
                    {
                        int a = 10;
                    }
                    foreach (DataRow row in recodeTable.Rows)
                    {
                        //try
                        //{
                        if (row.IsNull(0) || String.IsNullOrWhiteSpace(row[0].ToString())) break;
                        object checkingValue;
                        switch (row[0].ToString().Trim())
                        {
                            case "W":
                                if (recordSvod.GVS == null || recordSvod.GVS.NachVolume == 0)
                                    checkingValue = null;
                                else checkingValue = (double) recordSvod.GVS.NachVolume;
                                break;
                            case "W/E":
                                if (recordSvod.GVS == null || recordSvod.GVS.NachVolume == 0)
                                    checkingValue = null;
                                else if (recordSvod.LiveCount == 0)
                                    checkingValue = 999;
                                else
                                    checkingValue =
                                        (double) (recordSvod.GVS.NachVolume/recordSvod.LiveCount);
                                break;
                            case "A":
                                if (String.IsNullOrWhiteSpace(recordSvod.TownName)) checkingValue = null;
                                else checkingValue = recordSvod.TownName;
                                break;
                            case "L":
                                if (recordSvod.Otoplenie == null || recordSvod.Otoplenie.NachVolume == 0)
                                    checkingValue = null;
                                else checkingValue = (double) recordSvod.Otoplenie.NachVolume;
                                break;
                            case "AH/E":
                                if (recordSvod.HVS == null || recordSvod.HVS.NachVolume == 0)
                                    checkingValue = null;
                                else if (recordSvod.LiveCount == 0)
                                    checkingValue = 999;
                                else
                                    checkingValue =
                                        (double) (recordSvod.HVS.NachVolume/recordSvod.LiveCount);
                                break;
                            case "AS":
                                if (recordSvod.Vodootvedenie == null ||
                                    recordSvod.Vodootvedenie.NachVolume == 0)
                                    checkingValue = null;
                                else checkingValue = (double) recordSvod.Vodootvedenie.NachVolume;
                                break;
                            case "C":
                                checkingValue = recordSvod.Dogovor;
                                break;
                            default:
                                throw new Exception("Неизвестная колонка " + row[0]);
                        }
                        if (checkingValue == null) continue;

                        Func<object, object, bool> checkingFunc;
                        switch (row[1].ToString().Trim())
                        {
                            case ">":
                                checkingFunc =
                                    (check, etalon) =>
                                        Double.Parse(check.ToString()) > Double.Parse(etalon.ToString());
                                break;
                            case "=":
                                checkingFunc = (check, etalon) =>
                                {
                                    if (check is double)
                                        return
                                            Math.Abs(Double.Parse(check.ToString()) -
                                                     Double.Parse(etalon.ToString())) <
                                            0.01;
                                    else
                                        return check.ToString().Trim() == etalon.ToString().Trim();
                                };
                                break;
                            case "<>":
                                checkingFunc = (check, etalon) =>
                                {
                                    if (check is double)
                                        return
                                            Math.Abs(Double.Parse(check.ToString()) -
                                                     Double.Parse(etalon.ToString())) >
                                            0.01;
                                    else
                                        return check.ToString().Trim() != etalon.ToString().Trim();
                                };
                                break;
                            case "Not in":
                                checkingFunc = (check, etalon) =>
                                {
                                    double[] arr = etalon.
                                        ToString()
                                        .Replace("(", "")
                                        .Replace(")", "")
                                        .Split(';')
                                        .Select(s => Convert.ToDouble(s))
                                        .ToArray();
                                    return !arr.Contains(Double.Parse(check.ToString()));
                                };
                                break;
                            default:
                                throw new Exception("Неизвестная операция " + row[1]);
                        }

                        if (!checkingFunc(checkingValue, row[2])) continue;

                        var c = new CNV_LCHAR
                        {
                            LSHET = Consts.GetLs(recordSvod.Lshet),
                            LCHARCD = Convert.ToInt32(row[5].ToString()),
                            VALUE_ = Convert.ToInt32(row[7].ToString()),
                            DATE_ = svodFile.Date
                        };

                        llc.Add(c);
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                    }
                    Iterate();
                }
                StepFinish();
            }

            llc = LcharsRecordUtils.ThinOutList(llc);

            SaveList(llc, Consts.InsertRecordCount);
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - создание счетчиков и их показания";
            Position = 60;
            IsChecked = false;
        }

        private static List<CNV_COUNTER> Counters = new List<CNV_COUNTER>();

        public override void DoConvert()
        {
            SetStepsCount(9999);
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");

            var lci = new List<CNV_CNTRSIND>();
            foreach (dynamic counterFile in Consts.CounterFiles)
            {
                int i = 0;
                DataTable counterTable = Utils.ReadExcelFile(counterFile.FileName, counterFile.SheetName);
                StepStart(counterTable.Rows.Count);
                foreach (DataRow counterRow in counterTable.Rows)
                {
                    i++;
                    if (i < 2) continue;
                    if (counterRow.IsNull(1) || String.IsNullOrWhiteSpace(counterRow[1].ToString())) break;
                    var counterRecord = new RecordCounter(counterRow);
                    if (counterRecord.Type == 999) continue;
                    var counter = Counters.SingleOrDefault(
                        c => c.LSHET == Consts.GetLs(counterRecord.Lshet) && c.CNTTYPE == counterRecord.Type);
                    if (counter == null)
                    {
                        counter = new CNV_COUNTER
                        {
                            LSHET = Consts.GetLs(counterRecord.Lshet),
                            COUNTERID = String.Format("{0:D8}", Counters.Count + 1),
                            CNTTYPE = counterRecord.Type,
                            PRIM_ = counterRecord.Prim,
                            SERIALNUM = counterRecord.SerialNumber,
                        };
                        if (counterRecord.Pov != null) counter.LASTPOV = counterRecord.Pov;
                        Counters.Add(counter);
                    }
                    else if (counter.LASTPOV == null && counterRecord.Pov != null)
                    {
                        counter.LASTPOV = counterRecord.Pov;
                    }
                    if (counterRecord.BegInit == null || counterRecord.EndInit == null) continue;
                    lci.Add(new CNV_CNTRSIND
                    {
                        COUNTERID = counter.COUNTERID,
                        DOCUMENTCD = String.Format("{0}_{1}", counterRecord.Lshet, i),
                        INDDATE = counterFile.Date,
                        INDTYPE = 0,
                        OLDIND = (decimal) counterRecord.BegInit,
                        INDICATION = (decimal) counterRecord.EndInit
                    });
                    Iterate();
                }
                StepFinish();
            }

            SaveList(lci, Consts.InsertRecordCount);
            SaveList(Counters, Consts.InsertRecordCount);
        }
    }

    public class ConvertNach : ConvertCase
    {
        public ConvertNach()
        {
            ConvertCaseName = "NACHOPL - данные истории начислений. Первая половина";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            long recno = 0;
            foreach (dynamic svodFile in Consts.SvodFiles)
            {
                DataTable svodTable = Utils.ReadExcelFile(svodFile.FileName, svodFile.SheetName);
                StepStart(svodTable.Rows.Count);
                for (int i = 0; i < svodTable.Rows.Count - 1; i++)
                {
                    try
                    {
                        if (i < 5) continue;
                        var recordSvod = new RecordSvod(svodTable.Rows[i]);
                        if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                        //var recordOborot = RecordOborot.FindRecord(oborotTable, recordSvod.Lshet);
                        string lshet = Consts.GetLs(recordSvod.Lshet);
                        {
                            if (recordSvod.Otoplenie != null)
                                RigesterNachopl(recordSvod.Otoplenie, 3, "Отопление", lshet, nm, svodFile.Date);
                            if (recordSvod.GVS != null)
                                RigesterNachopl(recordSvod.GVS, 5, "Горячая вода", lshet, nm, svodFile.Date);
                            if (recordSvod.HVS != null)
                                RigesterNachopl(recordSvod.HVS, 4, "Хол. водоснабжение", lshet, nm, svodFile.Date);
                            if (recordSvod.Vodootvedenie != null)
                                RigesterNachopl(recordSvod.Vodootvedenie, 8, "Водоотведение", lshet, nm, svodFile.Date);

                            Iterate();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                StepFinish();
            }

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }

        private static int recno = 0;
        private void RigesterNachopl(RecordSvod.ServiceRecord record, int servicecd, string servicename,
            string lshet ,NachoplManager nm, DateTime date)
        {
            recno++;
            var ndef = new CNV_NACH
            {
                VOLUME = record.NachVolume,
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                TYPE_ = 0,
                SERVICECD = servicecd,
                SERVICENAME = servicename
            };
            nm.RegisterNach(ndef, lshet, date.Month, date.Year, record.Nach, record.Pere,
                date, String.Format("{0}_{1}", lshet, recno));

            recno++;
            var odef = new CNV_OPLATA
            {
                SERVICECD = servicecd,
                SERVICENAME = servicename,
                SOURCECD = 17,
                SOURCENAME = "Касса"
            };
            nm.RegisterOplata(odef, lshet, date.Month, date.Year, record.Opl,
                date, date,
                String.Format("{0}_{1}", lshet, recno));

            nm.RegisterBeginSaldo(lshet, date.Month, date.Year, servicecd, servicename, record.BeginSaldo);
            nm.RegisterEndSaldo(lshet, date.Month, date.Year, servicecd, servicename, record.EndSaldo);
        }
    }

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
}
