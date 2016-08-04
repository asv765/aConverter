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
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\учет жильцов на 25.07.16 для загрузки (3).xls",
            ListName = "паспортный стол",
            StartDataRow = 6,
            EndDataRow = 38722
        };

        public static ExcelFileInfo LsInfoFile = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ЛС на 29.07.16_для загрузки.xls",
            ListName = "66184",
            StartDataRow = 3,
            EndDataRow = 17616
        };

        public static ExcelFileInfo CountersInfoFile = new ExcelFileInfo
        {
            FileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ИПУ с историей показаний_для загрузки.xls",
            ListName = "610",
            StartDataRow = 3,
            EndDataRow = 4878
        };

        public const int InsertRecordCount = 1000;

        public static string GetLs(string ls)
        {
            return String.Format("01{0:D8}",Convert.ToInt64(ls));
        }
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
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            var la = new List<CNV_ABONENT>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(lsInfo.Lshet),
                    EXTLSHET = lsInfo.Lshet,
                    ISDELETED = lsInfo.LsCloseDate.HasValue && lsInfo.LsCloseDate.Value < DateTime.Now
                        ? 1
                        : 0,
                    DUCD = lsInfo.InformationOwnerId,
                    DUNAME = lsInfo.InformationOwner
                };
                lsInfo.ExtractFio(ref abonent);
                lsInfo.ExctractAddress(ref abonent);
                la.Add(abonent);
                Iterate();
            }
            StepFinish();

            StepStart(4);
            AbonentRecordUtils.SetUniqueTownskod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueDistkod(la, 0);
            StepFinish();

            SaveList(la, Consts.InsertRecordCount);
        }
    }

    public class ConvetCchars : ConvertCase
    {
        public ConvetCchars()
        {
            ConvertCaseName = "CCHAR - количественный характеристики";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$CHARS");
            var lc = new List<CNV_CHAR>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
                if (lsInfo.Square == null) continue;
                lc.Add(new CNV_CHAR
                {
                    CHARCD = 2,
                    CHARNAME = "Общая площадь",
                    LSHET = Consts.GetLs(lsInfo.Lshet),
                    DATE_ = new DateTime(2014,07,01),
                    VALUE_ = lsInfo.Square
                });
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lc = CharsRecordUtils.ThinOutList(lc);
            StepFinish();

            SaveList(lc, Consts.InsertRecordCount);
        }
    }

    public class ConvertLivingCchar : ConvertCase
    {
        public ConvertLivingCchar()
        {
            ConvertCaseName = "CCHAR - информация о проживающих";
            Position = 35;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);
            var lc = new List<CNV_CHAR>();
            StepStart(1);
            var roomers = RoomerInfo.ReadRoomers(Consts.RoomingReportFile);
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
                        LSHET = Consts.GetLs(roomer.Key),
                        DATE_ = checkingDate,
                        CHARCD = 11,
                        CHARNAME = "Число зарегистрированных",
                        VALUE_ = roomersToDate.Count(r => r.IsRegistered)
                    });
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(roomer.Key),
                        DATE_ = checkingDate,
                        CHARCD = 1,
                        CHARNAME = "Число проживающих",
                        VALUE_ = roomersToDate.Count(r => r.IsLiving) - missingCount
                    });
                    lc.Add(new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(roomer.Key),
                        DATE_ = checkingDate,
                        CHARCD = 12,
                        CHARNAME = "Число временно выбывших",
                        VALUE_ = missingCount
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
            var lcc = new List<CNV_COUNTER>();
            var lci = new List<CNV_CNTRSIND>();
            StepStart(countersInfoTable.Rows.Count + 1);
            for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            {
                Iterate();
                var counterInfo = new CounterInfo(countersInfoTable.Rows[i], new DateTime(2015, 09, 01));
                var counter = new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(counterInfo.Lshet),
                    COUNTERID = counterInfo.CounterId,
                    SETUPDATE = counterInfo.SetupDate,
                    NEXTPOV = counterInfo.NextPovDate,
                    SERIALNUM = counterInfo.SerialNumber,
                    PRIM_ = counterInfo.Model,
                    CNTTYPE = counterInfo.DigitCount == 5 ? 108 : 300
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

    public class ConvertRoomer : ConvertCase
    {
        public ConvertRoomer()
        {
            ConvertCaseName = "CITYZEN - информация о гражданах";
            Position = 100;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);
            BufferEntitiesManager.DropTableData("CNV$citizens");
            BufferEntitiesManager.DropTableData("CNV$CITIZENMIGRATION");

            StepStart(1);
            var roomers = RoomerInfo.ReadRoomers(Consts.RoomingReportFile);
            StepFinish();

            var cityzens = new List<CNV_CITIZEN>();
            var migrationList = new List<CNV_CITIZENMIGRATION>();
            StepStart(roomers.Count + 1);
            int cityzenId = 0;
            foreach (var roomer in roomers)
            {
                Iterate();
                foreach (var roomerInfo in roomer.Value)
                {
                    cityzenId++;
                    var cityzen = new CNV_CITIZEN
                    {
                        LSHET = Consts.GetLs(roomerInfo.Lshet),
                        CITIZENID = cityzenId,
                        STARTDATE = roomerInfo.RegisteredStartDate,
                        ENDDATE = roomerInfo.RegisteredEndDate,
                        VREMREG = roomerInfo.IsRegistered ? "1" : "0",
                        PRIBYT = roomerInfo.IsLiving ? "1" : "0",
                        COMMENT_ = roomerInfo.IsOwner ? "1" : "0",
                        DOCUMENTNAME = String.IsNullOrWhiteSpace(roomerInfo.PhoneNumber) ? null : roomerInfo.PhoneNumber,
                        DORGNAME = String.IsNullOrWhiteSpace(roomerInfo.Email) ? null : roomerInfo.Email,
                    };
                    roomerInfo.ExtractFio(ref cityzen);
                    cityzens.Add(cityzen);

                    if (roomerInfo.MissingInfoList == null) continue;
                    foreach (var missingInfo in roomerInfo.MissingInfoList)
                    {
                        if (missingInfo.MissingStartDate.HasValue)
                            migrationList.Add(new CNV_CITIZENMIGRATION
                            {
                                CITIZENID = cityzenId,
                                DATE_ = missingInfo.MissingStartDate.Value,
                                DIRECTION = 2,
                                MIGRATIONTYPE = 3
                            });
                        if (missingInfo.MissingEndDate.HasValue)
                        {
                            migrationList.Add(new CNV_CITIZENMIGRATION
                            {
                                CITIZENID = cityzenId,
                                DATE_ = missingInfo.MissingEndDate.Value,
                                DIRECTION = 1,
                                MIGRATIONTYPE = roomerInfo.IsRegistered ? 1 : 0
                            });
                        }
                    }
                }
            }
            StepFinish();

            SaveList(cityzens, Consts.InsertRecordCount);
            SaveList(migrationList, Consts.InsertRecordCount);
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

        public void ExtractFio(ref CNV_CITIZEN cityzen)
        {
            if (String.IsNullOrWhiteSpace(FIO)) return;
            string[] splittedFio = FIO.Split(' ');
            if (splittedFio.Length == 0)
            {
                cityzen.F = FIO;
            }
            else if (splittedFio.Length == 1)
            {
                cityzen.F = splittedFio[0];
            }
            else if (splittedFio.Length == 2)
            {
                cityzen.F = splittedFio[0];
                cityzen.I = splittedFio[1];
            }
            else if (splittedFio.Length == 3)
            {
                cityzen.F = splittedFio[0];
                cityzen.I = splittedFio[1];
                cityzen.O = splittedFio[2];
            }
            else
            {
                cityzen.O = splittedFio[splittedFio.Length - 1];
                cityzen.I = splittedFio[splittedFio.Length - 2];
                cityzen.F = "";
                for (int i = 0; i < splittedFio.Length - 2; i++)
                {
                    cityzen.F += splittedFio[i] + " ";
                }
                cityzen.F = cityzen.F.Trim();
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

        public static Dictionary<string, List<RoomerInfo>> ReadRoomers(ExcelFileInfo fileInfo)
        {
            DataTable reportTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            string currentLs = null;
            RoomerInfo lastRommerInfo = null;
            var roomers = new Dictionary<string, List<RoomerInfo>>();
            for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            {
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
                    roomers.Add(roomerInfo.Lshet, new List<RoomerInfo> { roomerInfo });

                lastRommerInfo = roomerInfo;
                currentLs = lastRommerInfo.Lshet;
            }
            return roomers;
        }
    }

    public class LsInfo
    {
        public string Lshet;
        public string FIO;
        public string InformationOwner;
        public int InformationOwnerId;
        public int? ContractNumber;
        public string Address;
        public bool Active;
        public DateTime LsOpenDate;
        public DateTime? LsCloseDate;
        public bool HasCounter;
        public bool HasHVS;
        public decimal? IndividualNorm;
        public bool GVSTypeOpen;
        public decimal? Square;

        public const int UnknowInformationOwnerId = 0;
        public const string UnknownInformationOnwer = "неизвестно";

        public LsInfo(DataRow dr)
        {
            Lshet = dr[0].ToString();
            FIO = dr[1].ToString();
            InformationOwner = dr[2].ToString();
            ContractNumber = String.IsNullOrWhiteSpace(dr[3].ToString()) ? (int?) null : Int32.Parse(dr[3].ToString());
            Address = dr[5].ToString();
            Active = dr[6].ToString().ToLower().Trim() == "действующий";
            LsOpenDate = DateTime.Parse(dr[7].ToString());
            LsCloseDate = String.IsNullOrWhiteSpace(dr[8].ToString())
                ? (DateTime?) null
                : DateTime.Parse(dr[8].ToString());
            HasCounter = dr[9].ToString().ToLower().Trim() == "да";
            HasHVS = dr[10].ToString().ToLower().Trim() == "есть";
            IndividualNorm = String.IsNullOrWhiteSpace(dr[11].ToString())
                ? (decimal?) null
                : Decimal.Parse(dr[11].ToString());
            GVSTypeOpen = dr[12].ToString().ToLower().Trim() == "открытая";
            Square = String.IsNullOrWhiteSpace(dr[13].ToString())
                ? (decimal?) null
                : Decimal.Parse(dr[13].ToString());

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
                    if (String.IsNullOrWhiteSpace(InformationOwner))
                    {
                        InformationOwnerId = UnknowInformationOwnerId;
                        InformationOwner = UnknownInformationOnwer;
                        break;
                    }
                    throw new Exception("Неизвестная УК " + InformationOwner);
            }
        }

        public void ExtractFio(ref CNV_ABONENT abonent)
        {
            if (String.IsNullOrWhiteSpace(FIO)) return;
            string[] splittedFio = FIO.Split(' ');
            if (splittedFio.Length == 0)
            {
                abonent.F = FIO;
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
            if (!String.IsNullOrWhiteSpace(abonent.F)) abonent.F = abonent.F.Trim();
            if (!String.IsNullOrWhiteSpace(abonent.I)) abonent.I = abonent.I.Trim();
            if (!String.IsNullOrWhiteSpace(abonent.O)) abonent.O = abonent.O.Trim();
        }

        private static Regex addressRegex = new Regex(@"(?<index>\d{6}).+г\. Тверь,(?<district>[^,]+),(?<street>[^,]+)");
        private static Regex houseRegex = new Regex(@"д[\., ]+([^ ,]+)");
        private static Regex korpusRegex = new Regex(@"корп[\., ]+([^ ,]+)");
        private static Regex flatRegex = new Regex(@"кв[\., ]+([^ ,]+)");
        private static Regex roomRegex = new Regex(@"комната[\., ]+([^ ,]+)");
        private static Regex digitRegex = new Regex(@"(\d+)([^\d]+\d*)*");

        public void ExctractAddress(ref CNV_ABONENT abonent)
        {
            if (String.IsNullOrWhiteSpace(Address)) return;
            abonent.TOWNSNAME = "Тверь";
            abonent.RAYONNAME = "Тверская область";
            abonent.RAYONKOD = 69;
            if (Lshet == "999601")
            {
                abonent.ULICANAME = "Невыясненная оплата";
                return;
            }

            var match = addressRegex.Match(Address);
            abonent.POSTINDEX = match.Groups["index"].Value.Trim();
            abonent.DISTNAME = match.Groups["district"].Value.Trim();
            abonent.ULICANAME = match.Groups["street"].Value.Trim();

            if (Lshet == "4886004")
            {
                abonent.HOUSENO = "5";
                abonent.FLATNO = 1;
                abonent.ROOMPOSTFIX = "4,5";
                return;
            }
            if (Lshet == "300")
            {
                abonent.HOUSENO = "5";
                abonent.FLATPOSTFIX = "6, 1";
                abonent.ROOMPOSTFIX = "5, 6, 7";
                return;
            }

            string house = houseRegex.Match(Address).Groups[1].Value;
            if (String.IsNullOrWhiteSpace(house)) throw new Exception("no house " + Address);
            match = digitRegex.Match(house);
            abonent.HOUSENO = match.Groups[1].Value;
            if (!String.IsNullOrWhiteSpace(match.Groups[2].Value))
                abonent.HOUSEPOSTFIX = match.Groups[2].Value;

            string korpus = korpusRegex.Match(Address).Groups[1].Value;
            if (!String.IsNullOrWhiteSpace(korpus))
            {
                match = digitRegex.Match(korpus);
                abonent.KORPUSNO = Int32.Parse(match.Groups[1].Value);
                if (!String.IsNullOrWhiteSpace(match.Groups[2].Value))
                    abonent.KORPUSPOSTFIX = match.Groups[2].Value;
            }

            string flat = flatRegex.Match(Address).Groups[1].Value;
            if (!String.IsNullOrWhiteSpace(flat))
            {
                match = digitRegex.Match(flat);
                abonent.FLATNO = Int32.Parse(match.Groups[1].Value);
                if (!String.IsNullOrWhiteSpace(match.Groups[2].Value))
                    abonent.FLATPOSTFIX = match.Groups[2].Value;
            }

            string room = roomRegex.Match(Address).Groups[1].Value;
            if (!String.IsNullOrWhiteSpace(room))
            {
                match = digitRegex.Match(room);
                abonent.ROOMNO = Int16.Parse(match.Groups[1].Value);
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

            // несколько жильцов с одинаковым ФИО
            ExcelFileInfo fileInfo = Consts.RoomingReportFile;
            DataTable reportTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            string currentLs = null;
            RoomerInfo lastRommerInfo = null;
            var roomers = new Dictionary<string, List<RoomerInfo>>();
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
                    roomers.Add(roomerInfo.Lshet, new List<RoomerInfo> { roomerInfo });

                lastRommerInfo = roomerInfo;
                currentLs = lastRommerInfo.Lshet;
            }
            StepFinish();

            StepStart(roomers.Count + 1);
            var test = new List<string>();
            foreach (var roomer in roomers)
            {
                Iterate();
                var fioGroup = roomer.Value.Where(r => !String.IsNullOrWhiteSpace(r.FIO)).GroupBy(r => r.FIO);
                foreach (var group in fioGroup)
                {
                    if (group.Count() > 1)
                    {
                        test.Add(roomer.Key);
                        break;
                    }
                }
            }
            StepFinish();

            string result = "";
            for (int i = 0; i < test.Count; i++)
            {
                result += test[i] + "\r\n";
            }
            int a = 10;


            //// абоненты с признаком счетчика, но в файле нет счетчика
            //ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            //DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            //var lsInfos = new List<string>();
            //StepStart(lsInfoTable.Rows.Count);
            //for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            //{
            //    var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
            //    if (lsInfo.HasCounter) lsInfos.Add(lsInfo.Lshet);
            //    Iterate();
            //}
            //lsInfoTable.Dispose();
            //StepFinish();

            //ExcelFileInfo countersFileInfo = Consts.CountersInfoFile;
            //DataTable countersInfoTable = Utils.ReadExcelFile(countersFileInfo.FileName, countersFileInfo.ListName);
            //var counterInfos = new List<string>();
            //StepStart(countersInfoTable.Rows.Count);
            //for (int i = countersFileInfo.StartDataRow - 2; i <= countersFileInfo.EndDataRow - 2; i++)
            //{
            //    counterInfos.Add(new CounterInfo(countersInfoTable.Rows[i], new DateTime(2015, 09, 01)).Lshet);
            //    Iterate();
            //}
            //StepFinish();

            //var test = lsInfos.Except(counterInfos).ToArray();
            //string result = "";
            //for (int i = 0; i < test.Length; i++)
            //{
            //    result += test[i] + "\r\n";
            //}
            //int a = 10;


            //// тест парсинга индекса, региона, улицы
            //ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            //DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            //StepStart(lsInfoTable.Rows.Count + 1);
            //var addressRegex = new Regex(@"(?<index>\d{6}).+г\. Тверь,(?<region>[^,]+),(?<street>[^,]+)");
            //var notMatch = new List<LsInfo>();
            //var indexes = new List<string>();
            //var regions = new List<string>();
            //var streets = new List<string>();
            //for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            //{
            //    Iterate();
            //    var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
            //    var match = addressRegex.Match(lsInfo.Address);
            //    if (String.IsNullOrWhiteSpace(match.Value)) notMatch.Add(lsInfo);
            //    else
            //    {
            //        string index = match.Groups["index"].Value.Trim();
            //        if (!indexes.Contains(index)) indexes.Add(index);
            //        string region = match.Groups["region"].Value.Trim();
            //        if (!regions.Contains(region)) regions.Add(region);
            //        string street = match.Groups["street"].Value.Trim();
            //        if (!streets.Contains(street)) streets.Add(street);
            //    }
            //}
            //StepFinish();


            //// поиск счетчиков, у которых есть показания раньше даты установки
            //ExcelFileInfo fileInfo = Consts.CountersInfoFile;
            //DataTable counterTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //var test = new List<CounterInfo>(); 
            //var test2 = new List<CounterInfo>(); 
            //StepStart(counterTable.Rows.Count + 1);
            //for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //{
            //    Iterate();
            //    if (counterTable.Rows[i][0].ToString() == "4954015")
            //    {
            //        int a = 10;
            //    }
            //    var counterInfo = new CounterInfo(counterTable.Rows[i], new DateTime(2015, 09, 01));
            //    if (counterInfo.Indicatios.Any(ind => (ind.Date.Year*12)+ind.Date.Month < (counterInfo.SetupDate.Year*12)+counterInfo.SetupDate.Month))
            //        test.Add(counterInfo);
            //    if (counterInfo.Indicatios.Any(ind => (ind.Date.Year * 12) + ind.Date.Month == (counterInfo.SetupDate.Year * 12) + counterInfo.SetupDate.Month))
            //        test2.Add(counterInfo);
            //}
            //StepFinish();


            //// странные владельцы
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
            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "2","0" });
            Iterate();
        }
    }

    public class TransferCityzens : ConvertCase
    {
        public TransferCityzens()
        {
            ConvertCaseName = "Перенос данных о гражданах";
            Position = 1320;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_03000_CITIZENS_TVER", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }
}
