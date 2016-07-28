using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _047_Tver
{
    public class Consts
    {
        public static ExcelFileInfo RoomingReportFile  = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\учет жильцов на 25.07.16 для загрузки (2).xls",
            ListName = "паспортный стол",
            StartDataRow = 6,
            EndDataRow = 38722
        };
        public static ExcelFileInfo RoomingReportFIOFile = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\учет жильцов на 25.07.16 для загрузки (2).xls",
            ListName = "ФИО плательщика",
            StartDataRow = 2,
            EndDataRow = 17509
        };

        public static ExcelFileInfo LsInfoFile = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ЛС.xls",
            ListName = "66184",
            StartDataRow = 2,
            EndDataRow = 13390
        };

        public static ExcelFileInfo CountersInfoFile = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ИПУ с историей показаний_для загрузки.xls",
            ListName = "610",
            StartDataRow = 3,
            EndDataRow = 4878
        };

        public const int InsertRecordCount = 1000;
    }

    public class ExcelFileInfo
    {
        public string FileName;
        public string ListName;
        public int StartDataRow;
        public int EndDataRow;
    }

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
            ConvertCaseName = "ABONENTS - конвертация информации об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(5);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            ExcelFileInfo fileInfo = Consts.RoomingReportFile;
            DataTable reportTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);

            var lsInfoList = new List<LsInfo>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                lsInfoList.Add(new LsInfo(lsInfoTable.Rows[i]));
                Iterate();
            }
            lsInfoTable.Dispose();
            StepFinish();

            var roomersFio = new Dictionary<string, string>();
            ExcelFileInfo roomerFioFileInfo = Consts.RoomingReportFIOFile;
            DataTable roomersFioTable = Utils.ReadExcelFile(roomerFioFileInfo.FileName, roomerFioFileInfo.ListName);
            StepStart(roomersFioTable.Rows.Count);
            for (int i = roomerFioFileInfo.StartDataRow - 2; i <= roomerFioFileInfo.EndDataRow - 2; i++)
            {
                roomersFio.Add(roomersFioTable.Rows[i][0].ToString(), roomersFioTable.Rows[i][1].ToString().Replace("  ", " "));
                Iterate();
            }
            roomersFioTable.Dispose();
            StepFinish();

            var la = new List<CNV_ABONENT>();
            StepStart(reportTable.Rows.Count + 1);
            for (int i = fileInfo.EndDataRow - 2; i >= fileInfo.StartDataRow - 2; i--)
            {
                Iterate();
                if (String.IsNullOrWhiteSpace(reportTable.Rows[i][0].ToString())) continue;
                var roomerInfo = new RoomerInfo(reportTable.Rows[i]);
                if (!roomerInfo.IsOwner || la.Any(a => a.LSHET == roomerInfo.Lshet)) continue;

                var abonent = new CNV_ABONENT
                {
                    LSHET = roomerInfo.Lshet,
                    EXTLSHET = roomerInfo.Lshet,
                    //PHONENUM = roomerInfo.PhoneNumber
                };

                if (!roomersFio.ContainsKey(abonent.LSHET))
                    throw new Exception("В таблице с ФИО отсутствует ЛС " + abonent.LSHET);
    
                roomerInfo.ExtractFio(roomersFio[abonent.LSHET], ref abonent);
                roomerInfo.ExctractAddress(ref abonent);

                abonent.ISDELETED = roomerInfo.CloseLsDate.HasValue && roomerInfo.CloseLsDate.Value < DateTime.Now
                    ? 1
                    : 0;

                var lsInfo = lsInfoList.SingleOrDefault(l => l.Lshet == abonent.LSHET);
                if (lsInfo == null)
                {
                    abonent.DUNAME = LsInfo.UnknownInformationOnwer;
                    abonent.DUCD = LsInfo.UnknowInformationOwnerId;
                }
                else
                {
                    abonent.DUNAME = lsInfo.InformationOwner;
                    abonent.DUCD = lsInfo.InformationOwnerId;
                }

                la.Add(abonent);
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

    public class ConvertRoomer : ConvertCase
    {
        public ConvertRoomer()
        {
            ConvertCaseName = "Информация о проживающих";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);
            BufferEntitiesManager.DropTableData("CNV$CHARS");
            ExcelFileInfo fileInfo = Consts.RoomingReportFile;
            DataTable reportTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            string currentLs = null;
            RoomerInfo lastRommerInfo = null;
            var roomers = new Dictionary<string, List<RoomerInfo>>();
            var lc = new List<CNV_CHAR>();
            StepStart(reportTable.Rows.Count + 1);
            for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            {
                Iterate();
                bool empty = false;
                RoomerInfo roomerInfo;
                if (String.IsNullOrWhiteSpace(reportTable.Rows[i][0].ToString()))
                {
                    roomerInfo = lastRommerInfo;
                    roomerInfo.ReadMissingInfo(reportTable.Rows[i]);
                    empty = true;
                }
                else 
                    roomerInfo = new RoomerInfo(reportTable.Rows[i]);

                if (roomerInfo.Lshet == currentLs)
                {
                    if (!empty) roomers[currentLs].Add(roomerInfo);
                }
                else
                    roomers.Add(roomerInfo.Lshet, new List<RoomerInfo> {roomerInfo});

                lastRommerInfo = roomerInfo;
                currentLs = lastRommerInfo.Lshet;
            }
            StepFinish();

            StepStart(roomers.Count + 1);
            foreach (var roomer in roomers)
            {
                Iterate();
                var allStartDates = roomer.Value
                    .Select(r => r.RegisteredStartDate)
                    .Distinct();
                var allEndDates = roomer.Value
                    .Where(r => r.RegisteredEndDate != null)
                    .Select(r => (DateTime) r.RegisteredEndDate)
                    .Distinct();
                var allMissingStartDates = roomer.Value
                    .Where(r => r.MissingInfoList != null)
                    .SelectMany(r =>r.MissingInfoList
                        .Where(m => m.MissingStartDate != null)
                        .Select(m => (DateTime) m.MissingStartDate))
                    .Distinct();
                var allMissingEndDates = roomer.Value
                    .Where(r => r.MissingInfoList != null)
                    .SelectMany(r => r.MissingInfoList
                        .Where(m => m.MissingEndDate != null)
                        .Select(m => (DateTime) m.MissingEndDate))
                    .Distinct();
                var allDates = allStartDates
                    .Union(allEndDates)
                    .Union(allMissingStartDates)
                    .Union(allMissingEndDates)
                    .OrderBy(d => d)
                    .ToList();

                foreach (var date in allDates)
                {
                    DateTime checkingDate = date;

                    var roomersToDate = roomer.Value
                        .Where(r => r.RegisteredStartDate <= checkingDate &&
                                    (r.RegisteredEndDate == null || checkingDate < r.RegisteredEndDate))
                        .ToList();
                    var missingCount = roomer.Value
                        .Where(r => r.MissingInfoList != null)
                        .SelectMany(r =>
                            r.MissingInfoList
                                .Where(m => m.MissingStartDate <= checkingDate && checkingDate < m.MissingEndDate))
                        .Count();

                    lc.Add(new CNV_CHAR
                    {
                        LSHET = roomer.Key,
                        DATE_ = checkingDate,
                        CHARCD = 3,
                        CHARNAME = "Число зарегистрированных",
                        VALUE_ = roomersToDate.Count(r => r.IsRegistered)
                    });
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = roomer.Key,
                        DATE_ = checkingDate,
                        CHARCD = 1,
                        CHARNAME = "Число проживающих",
                        VALUE_ = roomersToDate.Count(r => r.IsLiving) - missingCount
                    });
                }
            }
            StepFinish();

            StepStart(1);
            lc = CharsRecordUtils.ThinOutList(lc);
            StepFinish();

            SaveList(lc, Consts.InsertRecordCount);
        }
    }


    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - информация о счетчиках";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            ExcelFileInfo fileInfo = Consts.CountersInfoFile;
            DataTable countersInfoTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            var lc = new List<CounterInfo>();
            var lcc = new List<CNV_COUNTER>();
            var lci = new List<CNV_CNTRSIND>();
            StepStart(countersInfoTable.Rows.Count + 1);
            for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            {
                Iterate();
                var counterInfo = new CounterInfo(countersInfoTable.Rows[i], new DateTime(2015, 09, 01));
                if (lc.Any(c => c.Lshet == counterInfo.Lshet && c.SerialNumber == counterInfo.SerialNumber &&
                                c.Model == counterInfo.Model))
                    continue;
                lc.Add(counterInfo);
                var counter = new CNV_COUNTER
                {
                    LSHET = counterInfo.Lshet,
                    COUNTERID = counterInfo.CounterId,
                    SETUPDATE = counterInfo.SetupDate,
                    NEXTPOV = counterInfo.NextPovDate,
                    SERIALNUM = counterInfo.SerialNumber,
                    PRIM_ = counterInfo.Model,
                    CNTTYPE = counterInfo.DigitCount == 5 ? 1 : 300
                };
                if (counter.COUNTERID.Length > 20) counter.COUNTERID = counter.COUNTERID.Substring(0, 20);
                lcc.Add(counter);
                lci.Add(new CNV_CNTRSIND
                {
                    COUNTERID = counter.COUNTERID,
                    DOCUMENTCD = "Конвертация Нач пок",
                    OLDIND = counterInfo.InitialIndication,
                    INDICATION = counterInfo.InitialIndication,
                    INDDATE = counterInfo.SetupDate,
                    INDTYPE = 0
                });

                if (counterInfo.Indicatios.Count == 0) continue;
                decimal lastIndication = counterInfo.InitialIndication;
                var indication = counterInfo.Indicatios[0];
                lci.Add(new CNV_CNTRSIND
                {
                    COUNTERID = counter.COUNTERID,
                    DOCUMENTCD = "Конвертация Ист пок",
                    OLDIND = lastIndication,
                    INDICATION = indication.Value,
                    INDDATE = indication.Date,
                    INDTYPE = 0
                });
                lastIndication = indication.Value;
                for (int j = 1; j < counterInfo.Indicatios.Count; j++)
                {
                    indication = counterInfo.Indicatios[j];
                    if (counterInfo.SetupDate < indication.Date) 
                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = counter.COUNTERID,
                            DOCUMENTCD = "Конвертация Ист пок",
                            OLDIND = lastIndication,
                            INDICATION = indication.Value,
                            INDDATE = indication.Date,
                            INDTYPE = 0
                        });
                    lastIndication = indication.Value;
                }
            }
            StepFinish();

            SaveList(lcc, Consts.InsertRecordCount);
            SaveList(lci, Consts.InsertRecordCount);
        }
    }


    public class RoomerInfo
    {
        public string Lshet;
        public DateTime OpenLsDate;
        public DateTime? CloseLsDate;
        public string Address;
        public string FIO;
        public string PhoneNumber;
        public string Email;
        public DateTime RegisteredStartDate;
        public DateTime? RegisteredEndDate;
        public bool IsRegistered;
        public bool IsLiving;
        public bool IsOwner;
        public List<MissingInfo> MissingInfoList;

        public RoomerInfo(DataRow dr)
        {
            Lshet = dr[0].ToString();
            OpenLsDate = DateTime.Parse(dr[1].ToString());
            CloseLsDate = String.IsNullOrWhiteSpace(dr[2].ToString())
                ? (DateTime?) null
                : DateTime.Parse(dr[2].ToString());
            Address = dr[3].ToString().Replace("  ", " ");
            FIO = dr[4].ToString().Replace("  ", " ");
            PhoneNumber = dr[5].ToString();
            Email = dr[6].ToString();
            RegisteredStartDate = DateTime.Parse(dr[7].ToString());
            RegisteredEndDate = String.IsNullOrWhiteSpace(dr[8].ToString())
                ? (DateTime?) null
                : DateTime.Parse(dr[8].ToString());
            IsRegistered = dr[9].ToString().ToLower() == "да";
            IsLiving = dr[10].ToString().ToLower() == "да";
            IsOwner = dr[11].ToString().ToLower() == "да";
            ReadMissingInfo(dr);
        }

        public void ReadMissingInfo(DataRow dr)
        {
            if (String.IsNullOrWhiteSpace(dr[12].ToString())) return;
            if (MissingInfoList == null) MissingInfoList = new List<MissingInfo>();
            MissingInfoList.Add(new MissingInfo(dr));
        }

        public void ExtractFio(string fio, ref CNV_ABONENT abonent)
        {
            if (String.IsNullOrWhiteSpace(fio)) return;
            string[] splittedFio = fio.Split(' ');
            if (splittedFio.Length == 0)
            {
                abonent.F = fio;
            }
            else if (splittedFio.Length == 1)
            {
                abonent.F = splittedFio[0];
            }
            else if (splittedFio.Length == 2)
            {
                abonent.F = splittedFio[0];
                abonent.I = splittedFio[1];
            }
            else if (splittedFio.Length == 3)
            {
                abonent.F = splittedFio[0];
                abonent.I = splittedFio[1];
                abonent.O = splittedFio[2];
            }
            else
            {
                abonent.O = splittedFio[splittedFio.Length - 1];
                abonent.I = splittedFio[splittedFio.Length - 2];
                abonent.F = "";
                for (int i = 0; i < splittedFio.Length - 2; i++)
                {
                    abonent.F += splittedFio[i] + " ";
                }
                abonent.F = abonent.F.Trim();
            }
        }

        public void ExctractAddress(ref CNV_ABONENT abonent)
        {
            if (Lshet == "64981039")
            {
                int a = 10;
            }
            var postIndexRegex = new Regex(@"\d{6}");
            string postIndex = postIndexRegex.Match(Address).Value;
            string clearedAddress = Address.Trim();
            if (!String.IsNullOrWhiteSpace(postIndex))
            {
                abonent.POSTINDEX = postIndex.Trim();
                clearedAddress = Address.Replace(postIndex, "").Trim();
            }

            abonent.TOWNSNAME = "Тверь г";
            abonent.RAYONNAME = "Тверская область";

            clearedAddress = clearedAddress.Replace("Тверь", "");
            clearedAddress = clearedAddress.Replace("тверь", "");
            clearedAddress = clearedAddress.Replace(" г", "");

            var distRegex = new Regex(@", ([^,]*р-н),");
            var distMatch = distRegex.Match(clearedAddress);
            if (distMatch.Success)
            {
                abonent.DISTNAME = distMatch.Groups[1].Value.Trim();
                clearedAddress = clearedAddress.Replace(distMatch.Groups[1].Value, "");
            }
            var clearRegex = new Regex(@"(.*?)[^ ,\.]");
            string clear = clearRegex.Match(clearedAddress).Groups[1].Value;
            if (!String.IsNullOrWhiteSpace(clear))
                clearedAddress = clearedAddress.Replace(clear, "");

            var roomRegex = new Regex(@"ком[^\d]*(\d+)");
            var match = roomRegex.Match(clearedAddress);
            if (match.Success)
            {
                abonent.ROOMNO = Int16.Parse(match.Groups[1].Value);
                clearedAddress = clearedAddress.Replace(match.Value, "");
            }

            if (String.IsNullOrWhiteSpace(clearedAddress)) return;

            string[] separatedAddress = clearedAddress.Split(',');

            var digitRegex = new Regex(@"(\d+)([^\d]+\d*)");
            if (separatedAddress.Length > 2)
            {
                // flat
                match = digitRegex.Match(separatedAddress[2]);
                if (match.Success)
                {
                    abonent.FLATNO = Int32.Parse(match.Groups[1].Value);
                    abonent.FLATPOSTFIX = match.Groups[2].Value;
                }
            }
            if (separatedAddress.Length > 1)
            {
                //house
                match = digitRegex.Match(separatedAddress[1]);
                if (match.Success)
                {
                    abonent.HOUSENO = match.Groups[1].Value;
                    abonent.HOUSEPOSTFIX = match.Groups[2].Value;
                }
            }
            if (separatedAddress.Length > 0)
            {
                //street
                abonent.ULICANAME = separatedAddress[0].Trim();
            }
        }

        public class MissingInfo
        {
            public DateTime? MissingStartDate;
            public DateTime? MissingEndDate;
            public string MissingReason;

            public MissingInfo(DataRow dr)
            {
                MissingStartDate = String.IsNullOrWhiteSpace(dr[12].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(dr[12].ToString());
                MissingEndDate = String.IsNullOrWhiteSpace(dr[13].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(dr[13].ToString());
                MissingReason = dr[14].ToString();
            }
        }
    }

    public class LsInfo
    {
        public int? HouseGroupId;
        public string Address;
        public string InformationOwner;
        public int InformationOwnerId;
        public long IOContractNumber;
        public string Lshet;
        public string F;
        public string I;
        public string O;
        public string Distict;
        public bool Active;
        public DateTime ContractDate;
        public DateTime? ContratExpireDate;
        public bool HaveCountres;
        public bool HaveHvs;
        public decimal? IndividualNorm;
        public bool WaterCaptureTypeOpen;
        public double Square;
        public int LivingCount;

        public const int UnknowInformationOwnerId = 0;
        public const string UnknownInformationOnwer = "неизвестно";

        public LsInfo(DataRow dr)
        {
            HouseGroupId = dr.IsNull(0) ? (int?) null : Int32.Parse(dr[0].ToString());
            InformationOwner = dr[2].ToString();
            IOContractNumber = Int64.Parse(dr[3].ToString());
            Lshet = dr[4].ToString();
            Distict = String.IsNullOrWhiteSpace(dr[7].ToString()) ? null : dr[7].ToString();
            ContractDate = DateTime.Parse(dr[9].ToString());
            ContratExpireDate = String.IsNullOrWhiteSpace(dr[10].ToString())
                ? (DateTime?) null
                : DateTime.Parse(dr[10].ToString());
            IndividualNorm = dr.IsNull(13) ? (decimal?) null : Decimal.Parse(dr[13].ToString());
            Square = Double.Parse(dr[15].ToString());
            LivingCount = Int32.Parse(dr[16].ToString());

            Active = dr[8].ToString().ToLower().Trim() == "действующий";
            HaveCountres = dr[11].ToString().ToLower().Trim() == "да";
            HaveHvs = dr[12].ToString().ToLower().Trim() == "есть";
            WaterCaptureTypeOpen = dr[14].ToString().ToLower().Trim() == "открытая";
            Address = dr[6].ToString();

            switch (InformationOwner.Trim().ToLower())
            {
                case "непосредственное управление":
                    InformationOwnerId = 1;
                    break;
                case "нереализованный способ управления":
                    InformationOwnerId = 8;
                    break;
                case "ооо фирма \"энергия\"":
                    InformationOwnerId = 2;
                    break;
                case "ооо \"новый дом\"":
                    InformationOwnerId = 3;
                    break;
                case "ооо \"фаворит+\"":
                    InformationOwnerId = 4;
                    break;
                case "ооо \"фаворит\"":
                    InformationOwnerId = 7;
                    break;
                case "ооо ук \"рэп-17\"":
                    InformationOwnerId = 5;
                    break;
                case "ооо ук \"верхневолжский град\"":
                    InformationOwnerId = 6;
                    break;
                default:
                    throw new Exception("Неизвестная УК " + InformationOwner);
            }
        }
    }

    public class CounterInfo
    {
        public string Lshet;
        public DateTime SetupDate;
        public string SetupPlace;
        public int SetupPlaceId;
        public string SerialNumber;
        public string Model;
        public byte DigitCount;
        public DateTime? NextPovDate;
        public decimal InitialIndication;
        public string CounterId;
        public List<Indication> Indicatios;

        public CounterInfo(DataRow row, DateTime firstIndicationDate)
        {
            try
            {
                Lshet = row[0].ToString();
                SetupDate = DateTime.Parse(row[1].ToString());
                SetupPlace = row[2].ToString();
                SerialNumber = row[3].ToString();
                Model = row[4].ToString();
                DigitCount = Byte.Parse(row[5].ToString());
                NextPovDate = String.IsNullOrWhiteSpace(row[6].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(row[6].ToString());
                InitialIndication = Decimal.Parse(row[7].ToString().Replace('.', ','));
                CounterId = row[8].ToString();

                switch (SetupPlace.ToLower())
                {
                    case "":
                        SetupPlaceId = 0;
                        break;
                    case "ванная":
                        SetupPlaceId = 11;
                        break;
                    case "санузел":
                        SetupPlaceId = 12;
                        break;
                    case "туалет":
                        SetupPlaceId = 13;
                        break;
                    case "кухня":
                        SetupPlaceId = 14;
                        break;
                    case "в квартире":
                        SetupPlaceId = 15;
                        break;
                    case "прихожая":
                        SetupPlaceId = 16;
                        break;
                    case "площадка":
                        SetupPlaceId = 17;
                        break;
                    case "в кармане":
                        SetupPlaceId = 18;
                        break;
                    default:
                        throw new Exception("Неизвестное место установки " + SetupPlace);
                }

                Indicatios = new List<Indication>();
                DateTime currentIndicationDate = firstIndicationDate;
                for (int i = row.ItemArray.Length - 1; i >= 9; i--)
                {
                    string rowData = row[i].ToString();
                    if (!String.IsNullOrWhiteSpace(rowData))
                        Indicatios.Add(new Indication
                        {
                            Date = new DateTime(currentIndicationDate.Year, currentIndicationDate.Month,
                                DateTime.DaysInMonth(currentIndicationDate.Year, currentIndicationDate.Month)),
                            Value = Decimal.Parse(rowData.Replace('.', ','))
                        });
                    currentIndicationDate = currentIndicationDate.AddMonths(1);
                }
                Indicatios = Indicatios.OrderBy(i => i.Date).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public class Indication
        {
            public DateTime Date;
            public decimal Value;
        }
    }

    public class Test : ConvertCase
    {
        public Test()
        {
            ConvertCaseName = "ТЕСТ";
            Position = 1;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            ExcelFileInfo fileInfo = Consts.CountersInfoFile;
            DataTable counterTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            var test = new List<CounterInfo>(); 
            var test2 = new List<CounterInfo>(); 
            StepStart(counterTable.Rows.Count + 1);
            for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            {
                Iterate();
                if (counterTable.Rows[i][0].ToString() == "4954015")
                {
                    int a = 10;
                }
                var counterInfo = new CounterInfo(counterTable.Rows[i], new DateTime(2015, 09, 01));
                if (counterInfo.Indicatios.Any(ind => (ind.Date.Year*12)+ind.Date.Month < (counterInfo.SetupDate.Year*12)+counterInfo.SetupDate.Month))
                    test.Add(counterInfo);
                if (counterInfo.Indicatios.Any(ind => (ind.Date.Year * 12) + ind.Date.Month == (counterInfo.SetupDate.Year * 12) + counterInfo.SetupDate.Month))
                    test2.Add(counterInfo);
            }
            StepFinish();


            //SetStepsCount(4);
            //ExcelFileInfo fileInfo = Consts.RoomingReportFile;
            //DataTable reportTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //string currentLs = null;
            //RoomerInfo lastRommerInfo = null;
            //var roomers = new Dictionary<string, List<RoomerInfo>>();
            //StepStart(reportTable.Rows.Count + 1);
            //for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //{
            //    Iterate();
            //    bool empty = false;
            //    RoomerInfo roomerInfo;
            //    if (String.IsNullOrWhiteSpace(reportTable.Rows[i][0].ToString()))
            //    {
            //        roomerInfo = lastRommerInfo;
            //        roomerInfo.ReadMissingInfo(reportTable.Rows[i]);
            //        empty = true;
            //    }
            //    else
            //        roomerInfo = new RoomerInfo(reportTable.Rows[i]);

            //    if (roomerInfo.Lshet == currentLs)
            //    {
            //        if (!empty) roomers[currentLs].Add(roomerInfo);
            //    }
            //    else
            //        roomers.Add(roomerInfo.Lshet, new List<RoomerInfo> { roomerInfo });

            //    lastRommerInfo = roomerInfo;
            //    currentLs = lastRommerInfo.Lshet;
            //}
            //StepFinish();

            //var test = roomers.Values
            //    .Where(r => r
            //        .Count(rr => rr.IsOwner &&
            //                     (rr.RegisteredEndDate == null || rr.RegisteredEndDate > DateTime.Now))
            //                > 1)
            //    .ToList();
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
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "1" });
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

    public class TransferExtlshet : ConvertCase
    {
        public TransferExtlshet()
        {
            ConvertCaseName = "Перенос данных о внешних лицевых счетах";
            Position = 1090;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            //fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
            //Iterate();
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "513","0" });
            Iterate();
        }
    }
}
