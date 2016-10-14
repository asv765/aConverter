using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _047_Tver
{
    public class Consts
    {
        public static readonly ExcelFileInfo RoomingReportFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\учет жильцов на 09.09.16 для загрузки.xls",
            ListName = "учет жильцов",
            StartDataRow = 6,
            EndDataRow = 41026
        };

        public static readonly ExcelFileInfo LsInfoFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\информация по ЛС на 09.09.16_для загрузки_кор1.xls",
            ListName = "66184",
            StartDataRow = 3,
            EndDataRow = 18449
        };

        public static readonly ExcelFileInfo CountersInfoFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\информация по ИПУ с историей показаний_для загрузки кор1.xls",
            ListName = "610",
            StartDataRow = 3,
            EndDataRow = 5190
        };

        public static readonly ExcelFileInfo RecodeTableFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Таблица перекодировкиv1.7.xlsx",
            ListName = "Лист1",
            StartDataRow = 2,
            EndDataRow = 27
        };

        public static readonly ExcelFileInfo NewRecodeTableFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Таблица перекодировки_newv2.xlsx",
            ListName = "Лист1",
            StartDataRow = 2,
            EndDataRow = 91
        };

        public static readonly ExcelFileInfo RecodeEmptyHousesTableFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Таблица перекодировки_юрv1.1.xlsx",
            ListName = "Лист1",
            StartDataRow = 2,
            EndDataRow = 10
        };

        public static readonly ExcelFileInfo OplataFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Оплата с 01.07.14-31.07.16 общ. кор2.xls",
            StartDataRow = 3,
        };

        public static readonly ExcelFileInfo HousesCharsFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\общие данные по жилым домам и нежилым помещениям для загрузки_кор3.xls",
            ListName = "общие данные по жилым домам",
            StartDataRow = 5,
            EndDataRow = 242
        };

        public static readonly ExcelFileInfo EmptyHousesCharsFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\данные по жилым домам ФЛ с нежилыми помещениями_без ОДПУ_кор3.xls",
            ListName = "без ОДПУ для загрузки",
            StartDataRow = 7,
            EndDataRow = 250
        };

        public static readonly ExcelFileInfo LgotaReportFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\1.1 Отчет по льготникам за август 2016.xls",
            ListName = "66181",
            StartDataRow = 3,
            EndDataRow = 2462
        };

        //public static readonly ExcelFileInfo LgotaReportFile = new ExcelFileInfo
        //{
        //    FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Отчет по льготникам за июль 2016 (2).xls",
        //    ListName = "66181",
        //    StartDataRow = 3,
        //    EndDataRow = 2433
        //};

        public static readonly ExcelFileInfo LgotaRecodeTableFile = new ExcelFileInfo
        {
            FileName = aConverter_RootSettings.SourceDbfFilePath + @"\Таблица перекодировки льгот 2.xlsx",
            ListName = "Лист1",
            StartDataRow = 2,
            EndDataRow = 18
        };

        public static readonly string SpravkaFolder = aConverter_RootSettings.SourceDbfFilePath + "\\";

        public const int InsertRecordCount = 1000;

        public static string GetLs(string ls)
        {
            return String.Format("01{0:D8}", Convert.ToInt64(ls));
        }

        public static int CurrentYear = 2016;
        public static int CurrentMonth = 09;

        public const int BaseServiceCd = 3;
        public const string BaseServiceName = "Отопление";

        public const int BaseCoefCd = 105;
        public const string BaseCoefName = "Гор. водоснабжение пов. коэф.";

        public static string FixLs(string lshet, DateTime date)
        {
            long lshetLg = Int64.Parse(lshet);

            //if (date.Year == 2015 &&
            //    (lshetLg >= 5000001 &&
            //     (lshetLg < 5007001 || lshetLg > 5007021) &&
            //     (lshetLg < 5011001 || lshetLg > 5011019) &&
            //     lshetLg <= 5016062))
            //    lshet = "5" + lshet;

            //if (date.Year == 2015 &&
            //    (lshetLg >= 5000001 && lshetLg <= 5016062) &&
            //    (lshetLg <= 5007001 || lshetLg >= 5007021) &&
            //    (lshetLg <= 5011001 || lshetLg >= 5011019) &&
            //    (lshetLg <= 5017001 || lshetLg >= 5017006))
            //    lshet = "5" + lshet;
            return GetLs(lshet);
        }

        public static readonly DateTime FirstDate = new DateTime(2014, 07, 01);
        public static readonly string[] Prefixes = { "улица", "ул.", "пр-д.", "б-р.", "ш.", "пр-т.", "пер.", "ул", "проезд.", "пр.", "пр-т" };
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
            SetStepsCount(4);
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
                    DUNAME = lsInfo.InformationOwner,
                    CLOSEDATE = lsInfo.LsCloseDate
                };
                lsInfo.ExtractFio(ref abonent);
                lsInfo.ExctractAddress(ref abonent);
                la.Add(abonent);
                Iterate();
            }
            StepFinish();

            ExcelFileInfo emptyHousesFileInfo = Consts.EmptyHousesCharsFile;
            DataTable emptyHousesInfoTable = Utils.ReadExcelFile(emptyHousesFileInfo.FileName, emptyHousesFileInfo.ListName);
            StepStart(emptyHousesInfoTable.Rows.Count);
            for (int i = emptyHousesFileInfo.StartDataRow - 2; i <= emptyHousesFileInfo.EndDataRow - 2; i++)
            {
                var houseInfo = new EmptyHouseInfo(emptyHousesInfoTable.Rows[i]);
                var house = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    EXTLSHET = houseInfo.ContractId.ToString(),
                    ISDELETED = 0,
                };

                houseInfo.ExtractAddress(ref house);
                var abonentsAtHouse =
                    la.Where(a => a.ULICANAME.Contains(house.ULICANAME) && a.HOUSENO == house.HOUSENO);
                abonentsAtHouse = !String.IsNullOrWhiteSpace(house.HOUSEPOSTFIX)
                    ? abonentsAtHouse.Where(a => a.HOUSEPOSTFIX == house.HOUSEPOSTFIX)
                    : abonentsAtHouse.Where(a => String.IsNullOrWhiteSpace(a.HOUSEPOSTFIX));
                abonentsAtHouse = house.KORPUSNO.HasValue
                    ? abonentsAtHouse.Where(a => a.KORPUSNO == house.KORPUSNO)
                    : abonentsAtHouse.Where(a => a.KORPUSNO == null);


               var abonentEtalon = abonentsAtHouse.First();
               house.TOWNSNAME = abonentEtalon.TOWNSNAME;
               house.RAYONNAME = abonentEtalon.RAYONNAME;
               house.RAYONKOD = abonentEtalon.RAYONKOD;
               house.DISTNAME = abonentEtalon.DISTNAME;
               house.ULICANAME = abonentEtalon.ULICANAME;


                houseInfo.ExtractFio(ref house);
                la.Add(house);
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
            SetStepsCount(5);
            BufferEntitiesManager.DropTableData("CNV$CHARS");
            var lc = new List<CNV_CHAR>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
                string lshet = Consts.GetLs(lsInfo.Lshet);
                if (lsInfo.LsCloseDate.HasValue)
                {
                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = 1,
                        CHARNAME = "Число проживающих",
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        VALUE_ = 0
                    });
                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = 2,
                        CHARNAME = "Общая площадь",
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        VALUE_ = 0
                    });
                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = 150,
                        CHARNAME = "Доля площади МОП",
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        VALUE_ = 0
                    });
                }
                if (lsInfo.Square == null) continue;
                lc.Add(new CNV_CHAR
                {
                    CHARCD = 2,
                    CHARNAME = "Общая площадь",
                    LSHET = lshet,
                    DATE_ = Consts.FirstDate,
                    VALUE_ = lsInfo.Square
                });
                Iterate();
            }
            StepFinish();

            ExcelFileInfo emptyHousesFileInfo = Consts.EmptyHousesCharsFile;
            DataTable emptyHousesInfoTable = Utils.ReadExcelFile(emptyHousesFileInfo.FileName, emptyHousesFileInfo.ListName);
            StepStart(emptyHousesInfoTable.Rows.Count);
            for (int i = emptyHousesFileInfo.StartDataRow - 2; i <= emptyHousesFileInfo.EndDataRow - 2; i++)
            {
                var houseInfo = new EmptyHouseInfo(emptyHousesInfoTable.Rows[i]);
                if (houseInfo.Square.HasValue)
                    lc.Add(new CNV_CHAR //Отапливаемая площадь арендаторы
                    {
                        CHARCD = 2,
                        CHARNAME = "Общая площадь",
                        LSHET = Consts.GetLs(houseInfo.Lshet),
                        DATE_ = Consts.FirstDate,
                        VALUE_ = houseInfo.Square
                    });
                if (houseInfo.HoursPerDay.HasValue && houseInfo.HoursPerWeek.HasValue)
                    lc.Add(new CNV_CHAR //Часы
                    {
                        CHARCD = 231,
                        CHARNAME = "Часы",
                        LSHET = Consts.GetLs(houseInfo.Lshet),
                        DATE_ = Consts.FirstDate,
                        VALUE_ = houseInfo.HoursPerWeek*houseInfo.HoursPerDay
                    });
                lc.Add(new CNV_CHAR //Дог.нагрузка на ГВС по закр.схеме (Гкал/ч) 
                {
                    CHARCD = 232,
                    CHARNAME = "Дог.нагрузка на ГВС по закр.схеме (Гкал/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopGVSCloseGKal
                });
                lc.Add(new CNV_CHAR //Дог.нагрузка на ГВС по закр.схеме (Т/ч) 
                {
                    CHARCD = 233,
                    CHARNAME = "Дог.нагрузка на ГВС по закр.схеме (Т/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopGVSCloseTn
                });
                lc.Add(new CNV_CHAR //Дог.нагрузка на ГВС по откр.схем (Гкал/ч) 
                {
                    CHARCD = 234,
                    CHARNAME = "Дог.нагрузка на ГВС по откр.схем (Гкал/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopGVSOpenGKal
                });
                lc.Add(new CNV_CHAR //Дог.нагрузка на ГВС по откр.схеме (Т/ч)
                {
                    CHARCD = 235,
                    CHARNAME = "Дог.нагрузка на ГВС по откр.схеме (Т/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopGVSOpenTn
                });
                lc.Add(new CNV_CHAR //Дог.нагрузка на отопление (Гкал/ч)
                {
                    CHARCD = 236,
                    CHARNAME = "Дог.нагрузка на отопление (Гкал/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopOtoplGKal
                });
                lc.Add(new CNV_CHAR //Дог.нагрузка на отопление (Т/ч) 
                {
                    CHARCD = 237,
                    CHARNAME = "Дог.нагрузка на отопление (Т/ч)",
                    LSHET = Consts.GetLs(houseInfo.Lshet),
                    DATE_ = Consts.FirstDate,
                    VALUE_ = houseInfo.DopOtoplTn
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
            SetStepsCount(5);
            var lc = new List<CNV_CHAR>();
            StepStart(1);
            var roomers = RoomerInfo.ReadRoomers(Consts.RoomingReportFile);
            StepFinish();

            StepStart(1);
            var abonentCloseDates = new Dictionary<string, DateTime?>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
                abonentCloseDates.Add(lsInfo.Lshet, lsInfo.LsCloseDate);
            }
            lsInfoTable.Dispose();
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

                DateTime? abonentCloseDate;
                abonentCloseDates.TryGetValue(roomer.Key, out abonentCloseDate);

                foreach (var date in allDates)
                {
                    DateTime checkingDate = date;
                    if (checkingDate == abonentCloseDate) continue;

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

    public class ConvetCcharsHours : ConvertCase
    {
        public ConvetCcharsHours()
        {
            ConvertCaseName = "CCHAR - часы";
            Position = 36;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(5);
            var lc = new List<CNV_CHAR>();

            var minDate = new DateTime(2014, 07, 01);
            var maxDate = new DateTime(2016, 08, 01);


            DateTime[] dates = { /*new DateTime(2016, 03, 01), new DateTime(2016, 04, 01),*/ new DateTime(2016, 08, 01) };

            StepStart((maxDate.Month - minDate.Month) + 12 * (maxDate.Year - minDate.Year) + 1);
            //StepStart(dates.Length);
            for (var date = minDate; date <= maxDate; date = date.AddMonths(1))
            //foreach (var date in dates)
            {
                DataTable moneyTable = Utils.ReadExcelFile(
                    Consts.SpravkaFolder + Spravka.GetFileName(date), "66186");
                for (int i = 0; i < moneyTable.Rows.Count; i++)
                {
                    long ls;
                    if (!Int64.TryParse(moneyTable.Rows[i][0].ToString(), out ls)) continue;

                    var money = new Spravka(moneyTable.Rows[i], date);

                    string lshet = Consts.FixLs(money.Lshet, date);
                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = 200,
                        CHARNAME = "Часы ГВС",
                        LSHET = lshet,
                        DATE_ = date,
                        VALUE_ = DateTime.DaysInMonth(date.Year, date.Month)*24 - money.HoursGVS
                    });

                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = 220,
                        CHARNAME = "Часы отопление",
                        LSHET = lshet,
                        DATE_ = date,
                        VALUE_ = DateTime.DaysInMonth(date.Year, date.Month)*24 - money.HoursOtopl
                    });
                }
                Iterate();
            }
            var lcArray = lc.OrderBy(c => c.CHARCD).ThenBy(c => c.LSHET).ThenBy(c => c.DATE_).ToArray();
            StepFinish();

            StepStart(lcArray.Length);
            for (int i = 0; i < lcArray.Length - 1; i++)
            {
                var currentChar = lcArray[i];
                CNV_CHAR nextChar = lcArray[i + 1];
                if (currentChar.LSHET != nextChar.LSHET || currentChar.CHARCD != nextChar.CHARCD) continue;
                int monthDiff = (nextChar.DATE_.Value.Year * 12 + nextChar.DATE_.Value.Month) -
                                (currentChar.DATE_.Value.Year * 12 + currentChar.DATE_.Value.Month);
                for (int j = 1; j < monthDiff; j++)
                {
                    lc.Add(new CNV_CHAR
                    {
                        CHARCD = currentChar.CHARCD,
                        CHARNAME = currentChar.CHARNAME,
                        LSHET = currentChar.LSHET,
                        DATE_ = currentChar.DATE_.Value.AddMonths(j),
                        VALUE_ = 0
                    });
                }
                Iterate();
            }
            StepFinish();

            var allLs = lc.Select(c => c.LSHET).Distinct().ToArray();
            StepStart(allLs.Length);
            for (int i = 0; i < allLs.Length; i++)
            {
                string ls = allLs[i];
                var date = lc.Where(c => c.LSHET == ls).Select(c => c.DATE_).Max();
                if (date == null) continue;
                lc.Add(new CNV_CHAR
                {
                    CHARCD = 200,
                    CHARNAME = "Часы ГВС",
                    LSHET = ls,
                    DATE_ = date.Value.AddMonths(1),
                    VALUE_ = 0
                });

                lc.Add(new CNV_CHAR
                {
                    CHARCD = 220,
                    CHARNAME = "Часы отопление",
                    LSHET = ls,
                    DATE_ = date.Value.AddMonths(1),
                    VALUE_ = 0
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

    public class ConvetLchars : ConvertCase
    {
        public ConvetLchars()
        {
            ConvertCaseName = "LCHAR - качественные характеристики";
            Position = 39;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);
            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            var lc = new List<CNV_LCHAR>();
            ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
            DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
            ExcelFileInfo recodeTableInfo = Consts.RecodeTableFile;
            DataTable recodeDataTable = Utils.ReadExcelFile(recodeTableInfo.FileName, recodeTableInfo.ListName);
            var recodeTable = new List<Recode>();
            for (int i = recodeTableInfo.StartDataRow - 2; i <= recodeTableInfo.EndDataRow - 2; i++)
            {
                recodeTable.Add(new Recode(recodeDataTable.Rows[i]));
            }
            recodeDataTable.Dispose();
            StepStart(lsInfoTable.Rows.Count);
            for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
            {
                var lsInfo = new LsInfo(lsInfoTable.Rows[i]);
                string lshet = Consts.GetLs(lsInfo.Lshet);
                if (lsInfo.LsCloseDate.HasValue)
                {
                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        LCHARCD = 40,
                        LCHARNAME = "Закрытый л/сч",
                        VALUE_ = 1
                    });
                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        LCHARCD = 6,
                        LCHARNAME = "ЦГВС",
                        VALUE_ = 0
                    });
                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        LCHARCD = 25,
                        LCHARNAME = "Включено отопление",
                        VALUE_ = 0
                    });
                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        DATE_ = lsInfo.LsCloseDate,
                        LCHARCD = 11,
                        LCHARNAME = "Тип отопления",
                        VALUE_ = 0
                    });
                }

                for (int j = 0; j < recodeTable.Count; j++)
                {
                    var recode = recodeTable[j];
                    object checkingValue;
                    switch (recode.CheckField1)
                    {
                        case "L":
                            checkingValue = lsInfo.IndividualNorm;
                            break;
                        case "J":
                            checkingValue = lsInfo.HasCounter;
                            break;
                        case "M":
                            checkingValue = lsInfo.GVSTypeOpen;
                            break;
                        case "O":
                            checkingValue = lsInfo.NachGvsOdn;
                            break;
                        case "P":
                            checkingValue = lsInfo.NachOtopl;
                            break;
                        case "Q":
                            checkingValue = lsInfo.NachPkInd;
                            break;
                        case "R":
                            checkingValue = lsInfo.NachPkOdn;
                            break;
                        default:
                            throw new Exception("Неизвестное 1 поле для проверки: " + recode.CheckField1);
                    }
                    object chekingValue2 = null;
                    if (recode.CheckField2 != null)
                    {
                        switch (recode.CheckField2)
                        {
                            case "K":
                                chekingValue2 = lsInfo.HasHVS;
                                break;
                            default:
                                throw new Exception("Неизвестное 2 поле для проверки: " + recode.CheckField2);
                        }
                    }

                    if (checkingValue == null || (!checkingValue.Equals(recode.Value1)) || (chekingValue2 != null && !chekingValue2.Equals(recode.Value2))) continue;

                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = lshet,
                        DATE_ = Consts.FirstDate,
                        LCHARCD = recode.LcharCd,
                        VALUE_ = recode.LcharValue
                    });
                }
                Iterate();
            }
            StepFinish();

            ExcelFileInfo emptyHousesFileInfo = Consts.EmptyHousesCharsFile;
            DataTable emptyHousesInfoTable = Utils.ReadExcelFile(emptyHousesFileInfo.FileName, emptyHousesFileInfo.ListName);
            ExcelFileInfo recodeEmptyTableInfo = Consts.RecodeEmptyHousesTableFile;
            DataTable recodeEmptyDataTable = Utils.ReadExcelFile(recodeEmptyTableInfo.FileName, recodeEmptyTableInfo.ListName);
            var recodeEmptyTable = new List<Recode>();
            for (int i = recodeEmptyTableInfo.StartDataRow - 2; i <= recodeEmptyTableInfo.EndDataRow - 2; i++)
            {
                recodeEmptyTable.Add(new Recode(recodeEmptyDataTable.Rows[i]));
            }
            recodeEmptyDataTable.Dispose();
            StepStart(emptyHousesInfoTable.Rows.Count);
            for (int i = emptyHousesFileInfo.StartDataRow - 2; i <= emptyHousesFileInfo.EndDataRow - 2; i++)
            {
                var houseInfo = new EmptyHouseInfo(emptyHousesInfoTable.Rows[i]);
                for (int j = 0; j < recodeEmptyTable.Count; j++)
                {
                    var recode = recodeEmptyTable[j];
                    object checkingValue;
                    switch (recode.CheckField1)
                    {
                        case "T":
                            checkingValue = houseInfo.HasIPU;
                            break;
                        case "S":
                            checkingValue = houseInfo.WatercaptureType;
                            break;
                        case "U":
                            checkingValue = houseInfo.NachODPU;
                            break;
                        case "Y":
                            checkingValue = houseInfo.HouseType;
                            break;
                        default:
                            throw new Exception("Неизвестное 1 поле для проверки: " + recode.CheckField1);
                    }

                    if (checkingValue == null || (!checkingValue.Equals(recode.Value1))) continue;

                    lc.Add(new CNV_LCHAR
                    {
                        LSHET = Consts.GetLs(houseInfo.Lshet),
                        DATE_ = Consts.FirstDate,
                        LCHARCD = recode.LcharCd,
                        VALUE_ = recode.LcharValue
                    });
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lc = LcharsRecordUtils.ThinOutList(lc);
            StepFinish();

            SaveList(lc, Consts.InsertRecordCount);
        }

        private class Recode
        {
            public string CheckField1;
            public object Value1;
            public string CheckField2;
            public object Value2;

            public int LcharCd;
            public string LcharName;
            public int LcharValue;
            public string LcharValueDesc;

            public Recode(DataRow dr)
            {
                CheckField1 = dr[0].ToString().ToUpper().Trim();
                decimal decVal;
                if (!Decimal.TryParse(dr[1].ToString(), out decVal))
                    Value1 = dr[1].ToString().ToLower().Trim();
                else Value1 = decVal;
                CheckField2 = String.IsNullOrWhiteSpace(dr[2].ToString()) ? null : dr[2].ToString();
                if (!String.IsNullOrWhiteSpace(dr[3].ToString()))
                    Value2 = dr[3].ToString().ToLower().Trim();
                else Value2 = null;

                LcharCd = Int32.Parse(dr[4].ToString());
                LcharName = dr[5].ToString();
                LcharValue = Int32.Parse(dr[6].ToString());
                LcharValueDesc = dr[7].ToString();
            }
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
            long docId = 0;
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
                    DOCUMENTCD = "КонНачПок_"+ counter.COUNTERID,
                    OLDIND = counterInfo.InitialIndication,
                    INDICATION = counterInfo.InitialIndication,
                    INDDATE = counterInfo.SetupDate,
                    INDTYPE = 0
                });

                if (counterInfo.Indicatios.Count == 0) continue;
                decimal lastIndication = counterInfo.InitialIndication;
                var indication = counterInfo.Indicatios[0];
                docId++;
                lci.Add(new CNV_CNTRSIND
                {
                    COUNTERID = counter.COUNTERID,
                    DOCUMENTCD = "КонИстПок_"+docId,
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
                    {
                        docId++;
                        lci.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = counter.COUNTERID,
                            DOCUMENTCD = "КонИстПок_"+docId,
                            OLDIND = lastIndication,
                            INDICATION = indication.Value,
                            INDDATE = indication.Date,
                            INDTYPE = 0
                        });
                    }
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

                    if (!roomerInfo.RegisteredEndDate.HasValue && !roomerInfo.IsLiving)
                        migrationList.Add(new CNV_CITIZENMIGRATION
                        {
                            CITIZENID = cityzenId,
                            DATE_ = roomerInfo.RegisteredStartDate,
                            DIRECTION = 2,
                            MIGRATIONTYPE = roomerInfo.IsRegistered ? 1 : 0
                        });

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

    public class ConvertNachopl : ConvertCase
    {
        public static LsInfo[] Abonents;
        public static Recode[] RecodeTable;
        public static string NotFoundedAbonents = "";

        public ConvertNachopl()
        {
            ConvertCaseName = "Nachopl - история оплат и начислений";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(5);

            BufferEntitiesManager.DropTableData("CNV$NACH");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");

            var minDate = new DateTime(2014, 07, 01);
            var maxDate = new DateTime(2016, 08, 01);

            //DateTime[] dates = { new DateTime(2016, 08, 01) };
            DateTime[] dates =
            {
                new DateTime(2016, 06, 01),
            };

            var minPayDate = new DateTime(2014, 07, 01);
            var maxPayDate = new DateTime(2016, 08, 01);
            var minPkDate = new DateTime(2015, 07, 01);
            var maxPkDate = new DateTime(2016, 08, 01);

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            {
                var tempList = new List<LsInfo>();
                ExcelFileInfo lsFileInfo = Consts.LsInfoFile;
                DataTable lsInfoTable = Utils.ReadExcelFile(lsFileInfo.FileName, lsFileInfo.ListName);
                StepStart(lsInfoTable.Rows.Count);
                for (int i = lsFileInfo.StartDataRow - 2; i <= lsFileInfo.EndDataRow - 2; i++)
                {
                    tempList.Add(new LsInfo(lsInfoTable.Rows[i]));
                }
                Abonents = tempList.ToArray();
                StepFinish();
            }

            {
                ExcelFileInfo recodeTableInfo = Consts.NewRecodeTableFile;
                DataTable recodeDataTable = Utils.ReadExcelFile(recodeTableInfo.FileName, recodeTableInfo.ListName);
                var tempRecode = new List<Recode>();
                for (int i = recodeTableInfo.StartDataRow - 2; i <= recodeTableInfo.EndDataRow - 2; i++)
                {
                    tempRecode.Add(new Recode(recodeDataTable.Rows[i]));
                }
                recodeDataTable.Dispose();
                RecodeTable = tempRecode.ToArray();
            }

            long recno = 0;
            //StepStart(dates.Length);
            StepStart((maxDate.Month - minDate.Month) + 12 * (maxDate.Year - minDate.Year) + 1);
            for (var date = minDate; date <= maxDate; date = date.AddMonths(1))
            //foreach (var date in dates)
            {
                DataTable moneyTable = Utils.ReadExcelFile(Consts.SpravkaFolder + Spravka.GetFileName(date), "66186");

                var pkList = new List<PKCoef>();
                PKCoef.Date = date;
                if (minPkDate <= date && date <= maxPkDate)
                {
                    DataTable pkTable = Utils.ReadExcelFile(Consts.SpravkaFolder + PKCoef.GetFileName(date), "Лист1");
                    for (int j = 0; j < pkTable.Rows.Count; j++)
                    {
                        long tempLs;
                        if (!Int64.TryParse(moneyTable.Rows[j][0].ToString(), out tempLs)) continue;

                        if (String.IsNullOrWhiteSpace(pkTable.Rows[j][0].ToString()) ||
                            pkTable.Rows[j][0].ToString().Trim().ToLower() == "общий итог") break;
                        pkList.Add(new PKCoef(pkTable.Rows[j]));
                    }
                    pkTable.Dispose();
                }
                for (int i = 0; i < moneyTable.Rows.Count; i++)
                {
                    long ls;
                    if (!Int64.TryParse(moneyTable.Rows[i][0].ToString(), out ls)) continue;

                    var money = new Spravka(moneyTable.Rows[i], date);

                    var pk = pkList.SingleOrDefault(p => p.Lshet == money.Lshet);
                    if (pk == null)
                    {
                        pk = new PKCoef
                        {
                            Lshet = money.Lshet,
                            EndSaldoInd = 0,
                            EndSaldoOdn = 0,
                            PereOdn = 0,
                            PereInd = 0
                        };
                    }

                    string lshet = Consts.FixLs(money.Lshet, date);

                    for (int j = 0; j < money.Services.Length; j++)
                    {
                        var serviceMoney = money.Services[j];

                        var reculc = serviceMoney.RecalcSum;
                        if (minPkDate <= date && date <= maxPkDate)
                        {
                            if (serviceMoney.ServiceCd == 105) reculc = pk.PereInd;
                            if (serviceMoney.ServiceCd == 115) reculc = pk.PereOdn;
                        }
                        if (serviceMoney.Volume != 0 ||
                            serviceMoney.RecalcVol != 0 ||
                            serviceMoney.Nach != 0 ||
                            reculc != 0)
                        {
                            nm.RegisterNach(new CNV_NACH
                            {
                                REGIMCD = serviceMoney.RegimCd,
                                REGIMNAME = serviceMoney.RegimName,
                                SERVICECD = serviceMoney.ServiceCd,
                                SERVICENAME = serviceMoney.ServiceName,
                                TYPE_ = serviceMoney.SummaType,
                                VTYPE_ = serviceMoney.VolumeType,
                                VOLUME = Math.Round(serviceMoney.Volume, 4),
                                PROCHLVOLUME = Math.Round(serviceMoney.RecalcVol, 4)
                            }, lshet, date.Month, date.Year, serviceMoney.Nach, reculc, date,
                                String.Format("{0}_{1}", money.Lshet, date.ToString("yyMMdd"))); // TODO Документ должен быть по услуге, а не по дате
                        }
                    }

                    nm.RegisterEndSaldo(lshet, date.Month, date.Year, Consts.BaseServiceCd, Consts.BaseServiceName,
                        money.EndSaldo - (pk.EndSaldoInd + pk.EndSaldoOdn));
                    nm.RegisterEndSaldo(lshet, date.Month, date.Year, 105, "Гор. водоснабжение пов. коэф.",
                        pk.EndSaldoInd);
                    nm.RegisterEndSaldo(lshet, date.Month, date.Year, 115, "Гор. водоснабжение ОДН пов. коэф",
                        pk.EndSaldoOdn);
                }
                if (minPayDate <= date && date <= maxPayDate)
                {
                    Oplata.Date = date;
                    DataTable oplataTable = Utils.ReadExcelFile(Consts.OplataFile.FileName, Oplata.GetListName(date));
                    for (int i = 0; i < oplataTable.Rows.Count; i++)
                    {
                        long ls;
                        if (!Int64.TryParse(oplataTable.Rows[i][0].ToString(), out ls)) continue;
                        recno++;
                        if (String.IsNullOrWhiteSpace(oplataTable.Rows[i][0].ToString()) ||
                            oplataTable.Rows[i][0].ToString().Trim().ToUpper() == "ИТОГО") break;
                        var oplata = new Oplata(oplataTable.Rows[i]);
                        string lshet = Consts.FixLs(oplata.Lshet, date);
                        if (lshet.Length <= 10)
                        {
                            var odef = new CNV_OPLATA
                            {
                                SERVICECD = Consts.BaseServiceCd,
                                SERVICENAME = Consts.BaseServiceName,
                                SOURCECD = oplata.SourceCd,
                                SOURCENAME = oplata.Source
                            };

                            DateTime payDate = oplata.PayDate ?? date;

                            nm.RegisterOplata(odef, lshet, date.Month, date.Year,
                                oplata.ServicesSumma, payDate, date,
                                String.Format("{0}_{1}", oplata.Lshet, recno));

                            odef = new CNV_OPLATA
                            {
                                SERVICECD = 105,
                                SERVICENAME = "Гор. водоснабжение пов. коэф.",
                                SOURCECD = oplata.SourceCd,
                                SOURCENAME = oplata.Source
                            };

                            nm.RegisterOplata(odef, lshet, date.Month, date.Year,
                                oplata.IndCoefSumma, payDate, date,
                                String.Format("{0}_{1}", oplata.Lshet, recno));

                            odef = new CNV_OPLATA
                            {
                                SERVICECD = 115,
                                SERVICENAME = "Гор. водоснабжение ОДН пов. коэф",
                                SOURCECD = oplata.SourceCd,
                                SOURCENAME = oplata.Source
                            };

                            nm.RegisterOplata(odef, lshet, date.Month, date.Year,
                                oplata.OdnCoefSumma, payDate, date,
                                String.Format("{0}_{1}", oplata.Lshet, recno));
                        }
                    }
                }

                // SaveList(nm.NachRecords, Consts.InsertRecordCount, false);
                // SaveList(nm.OplataRecords, Consts.InsertRecordCount, false);
                // SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount, false);

                Iterate();
            }

            StepStart(3);
            nm.SaveNachRecords(aConverter_RootSettings.FirebirdStringConnection);
            //Iterate();
            //nm.SaveOplataRecords(aConverter_RootSettings.FirebirdStringConnection);
            //Iterate();
            //nm.SaveNachoplRecords(aConverter_RootSettings.FirebirdStringConnection);
            StepFinish();

            StepFinish();

            StepStart(1);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                // Снятие удаления у абонентов
                context.ExecuteNonQuery(
                    @"update CNV$ABONENT
                    set ISDELETED = 0
                    where LSHET in (select NOP.LSHET
                                    from CNV$NACHOPL NOP
                                    inner join CNV$ABONENT AB on AB.LSHET = NOP.LSHET and
                                          AB.ISDELETED = 1
                                    where (NOP.YEAR_ * 12 + NOP.MONTH_) = (select max(NOP1.YEAR_ * 12 + NOP1.MONTH_)
                                                                           from CNV$NACHOPL NOP1
                                                                           where NOP1.LSHET = NOP.LSHET)
                                    group by NOP.LSHET
                                    having sum(NOP.EDEBET) <> 0);");
                context.SaveChanges();
            }
            StepFinish();
        }

        public class Recode
        {
            public string Scheme;
            public decimal Blago;
            public string HasCounter;
            public string Unknown;
            
            public bool GKal;
            public int ServiceCD;

            public int Regim;
            public int SummaType;
            public int VolumeType;

            public Recode(DataRow dr)
            {
                Scheme = String.IsNullOrWhiteSpace(dr[1].ToString()) ? null : dr[1].ToString().ToLower().Trim();
                Blago = Decimal.Parse(dr[3].ToString());
                HasCounter = String.IsNullOrWhiteSpace(dr[5].ToString()) ? null : dr[5].ToString().ToLower().Trim();
                Unknown = String.IsNullOrWhiteSpace(dr[7].ToString()) ? null : dr[7].ToString().ToLower().Trim();

                GKal = Int32.Parse(dr[8].ToString()) == 1;
                ServiceCD = Int32.Parse(dr[9].ToString());

                Regim = Int32.Parse(dr[10].ToString());
                SummaType = Int32.Parse(dr[11].ToString());
                VolumeType = Int32.Parse(dr[12].ToString());
            }
        }
    }

    public class ConvetHouseChars : ConvertCase
    {
        public ConvetHouseChars()
        {
            ConvertCaseName = "HOUSECHAR - характеристики домов";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$CHARSHOUSES");
            var lch = new List<CNV_CHARSHOUSES>();
            var lca = new List<CNV_LCHAR>();
            ExcelFileInfo houseFileInfo = Consts.HousesCharsFile;
            DataTable houseCharsTable = Utils.ReadExcelFile(houseFileInfo.FileName, houseFileInfo.ListName);
            StepStart(houseCharsTable.Rows.Count);
            string notFounded = "";
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = houseFileInfo.StartDataRow - 2; i <= houseFileInfo.EndDataRow - 2; i++)
                {
                    var houseChars = new HouseChars(houseCharsTable.Rows[i]);
                    string query = String.Format(
                        "SELECT * FROM CNV$ABONENT WHERE ULICANAME LIKE '%{0}%' AND HOUSENO = '{1}'",
                        houseChars.Street, houseChars.HouseNo);
                    if (!String.IsNullOrWhiteSpace(houseChars.HousePostfix))
                        query += String.Format(" AND HOUSEPOSTFIX = '{0}'", houseChars.HousePostfix);
                    else query += @" AND (HOUSEPOSTFIX is null or HOUSEPOSTFIX = '')";
                    if (houseChars.KorpusNo.HasValue)
                        query += String.Format(" AND KORPUSNO = '{0}'", houseChars.KorpusNo.Value);
                    else
                        query += @" AND (KORPUSNO is null)";
                    var result = context.ExecuteQuery<CNV_ABONENT>(query);
                    if (result.Any())
                    {
                        context.ExecuteNonQuery(String.Format(
                            "UPDATE CNV$ABONENT SET HOUSECD = {0} WHERE HOUSECD = {1}", houseChars.HouseId,
                            result[0].HOUSECD));
                        context.SaveChanges();
                        if (houseChars.TotalSquare.HasValue)
                            lch.Add(new CNV_CHARSHOUSES
                            {
                                CHARCD = 206001,
                                DATE_ = Consts.FirstDate,
                                VALUE_ = houseChars.TotalSquare.Value,
                                HOUSECD = houseChars.HouseId
                            });
                        if (houseChars.HeatingSquare.HasValue)
                            lch.Add(new CNV_CHARSHOUSES
                            {
                                CHARCD = 32010,
                                DATE_ = Consts.FirstDate,
                                VALUE_ = houseChars.HeatingSquare.Value,
                                HOUSECD = houseChars.HouseId
                            });

                        if (houseChars.HasODNCounter)
                            for (int j = 0; j < result.Count; j++)
                            {
                                var abonent = result[j];
                                lca.Add(new CNV_LCHAR
                                {
                                    LSHET = abonent.LSHET,
                                    DATE_ = Consts.FirstDate,
                                    VALUE_ = 1,
                                    LCHARCD = 119,
                                });
                            }
                    }
                    else
                    {
                        notFounded += houseChars.Address + "\r\n";
                    }

                    Iterate();
                }
            }
            if (!String.IsNullOrWhiteSpace(notFounded))
            {
                File.WriteAllText(aConverter_RootSettings.SourceDbfFilePath + "\\housesNotFounded.txt", notFounded);
                Task.Run(() => MessageBox.Show(
                    "Обнаружены дома, в которых отсутсвуют абоненты (см. файл в директории с исходными данными housesNotFounded.txt)"));
            }
            StepFinish();

            SaveList(lch, Consts.InsertRecordCount);
            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    public class ConvetLgotaPeople : ConvertCase
    {
        public ConvetLgotaPeople()
        {
            ConvertCaseName = "LGOTA - информация о льготниках";
            Position = 110;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            BufferEntitiesManager.DropTableData("CNV$CITYZENLGOTA");
            var lg = new List<CNV_CITYZENLGOTA>();
            ExcelFileInfo lgotaFileInfo = Consts.LgotaReportFile;
            DataTable lgotaTable = Utils.ReadExcelFile(lgotaFileInfo.FileName, lgotaFileInfo.ListName);

            ExcelFileInfo recodeTableFile = Consts.LgotaRecodeTableFile;
            DataTable recodeDt = Utils.ReadExcelFile(recodeTableFile.FileName, recodeTableFile.ListName);
            var recodeTable = new List<Recode>();
            for (int i = recodeTableFile.StartDataRow - 2; i <= recodeTableFile.EndDataRow - 2; i++)
            {
                recodeTable.Add(new Recode(recodeDt.Rows[i]));
            }
            recodeDt.Dispose();
            string badPeople = "";
            StepStart(lgotaTable.Rows.Count);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = lgotaFileInfo.StartDataRow - 2; i <= lgotaFileInfo.EndDataRow - 2; i++)
                {
                    var lgota = new Lgota(lgotaTable.Rows[i]);

                    var l = new CNV_CITYZENLGOTA
                    {
                        STARTDATE = lgota.StartDate,
                        ENDDATE = lgota.EndDate,
                        //ISDELETED = lgota.EndDate.HasValue && lgota.EndDate.Value < DateTime.Now ? 1 : 0,
                    };

                    bool founded = false;
                    for (int j = 0; j < recodeTable.Count; j++)
                    {
                        if (recodeTable[j].LgotaName == lgota.Category)
                        {
                            l.LGOTACD = recodeTable[j].LgotaId;
                            founded = true;
                            break;
                        }
                    }
                    if (!founded) throw new Exception("В таблице перекодировке не найдена льгота " + lgota.Category);

                    string sql = String.Format(
                        @"select CITIZENID from cnv$citizens cn where cn.lshet = '{0}' and (cn.f || ' ' || cn.i || iif(cn.o is not null, ' ' || cn.o, '')) = '{1}'",
                        Consts.GetLs(lgota.Lshet), lgota.FIO.Replace("  ", " "));
                    var result = context.ExecuteQuery<int>(sql, CommandType.Text, null);
                    if (result.Count == 0)
                    {
                        badPeople += String.Format("Не найден льготник {0} {1}\r\n", lgota.Lshet, lgota.FIO);
                        continue;
                    }
                    if (result.Count > 1)
                    {
                        badPeople += String.Format("Найдено несколько льготников ({2}) {0} {1}\r\n", lgota.Lshet,
                            lgota.FIO, result.Count);
                        continue;
                    }

                    l.CITYZENID = result[0];
                    lg.Add(l);

                    sql = String.Format(
                        @"UPDATE CNV$CITIZENS SET NOMER = '{0}' WHERE CITIZENID = {1}",
                        lgota.PKU, l.CITYZENID);
                    context.ExecuteNonQuery(sql);
                    context.SaveChanges();

                    Iterate();
                }
            }
            if (!String.IsNullOrWhiteSpace(badPeople))
            {
                File.WriteAllText(aConverter_RootSettings.SourceDbfFilePath + "\\badLgotaPeople.txt", badPeople);
                Task.Run(() => MessageBox.Show(
                    "Обнаружены проблемные льготники (см. файл в директории с исходными данными badLgotaPeople.txt)"));
            }
            StepFinish();

            SaveList(lg, Consts.InsertRecordCount);
        }

        private class Recode
        {
            public string LgotaName;
            public int LgotaId;

            public Recode(DataRow dr)
            {
                LgotaName = dr[0].ToString().Trim();
                LgotaId = Int32.Parse(dr[1].ToString());
            }
        }
    }

    public class ConvertGPU : ConvertCase
    {
        public ConvertGPU()
        {
            ConvertCaseName = "ГПУ - групповые приборы учета";
            Position = 120;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            var lca = new List<CNV_LCHAR>();
            ExcelFileInfo houseFileInfo = Consts.HousesCharsFile;
            DataTable houseCharsTable = Utils.ReadExcelFile(houseFileInfo.FileName, houseFileInfo.ListName);
            StepStart(houseCharsTable.Rows.Count);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = houseFileInfo.StartDataRow - 2; i <= houseFileInfo.EndDataRow - 2; i++)
                {
                    var houseChars = new HouseChars(houseCharsTable.Rows[i]);
                    if (!houseChars.HasODNCounter || (
                        String.IsNullOrWhiteSpace(houseChars.AlgorithmInNeOtoplPeriod) ||
                        houseChars.AlgorithmInNeOtoplPeriod.ToLower() == "счетчик ипу" ||
                        houseChars.AlgorithmInNeOtoplPeriod.ToLower() == "нет")) continue;
                    string query = String.Format(@" select * from cnv$abonent where housecd = {0}", houseChars.HouseId);
                    var result = context.ExecuteQuery<CNV_ABONENT>(query);
                    for (int j = 0; j < result.Count; j++)
                    {
                        var abonent = result[j];
                        var l = new CNV_LCHAR
                        {
                            LSHET = abonent.LSHET,
                            DATE_ = new DateTime(2016, 06, 01),
                            LCHARCD = 28,
                            LCHARNAME = "Алгоритм ГПУ"
                        };
                        switch (houseChars.AlgorithmInNeOtoplPeriod.ToLower())
                        {
                            case "не указан":
                                l.VALUE_ = 0;
                                break;
                            case "1,2":
                                l.VALUE_ = 7;
                                break;
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                                l.VALUE_ = Int32.Parse(houseChars.AlgorithmInNeOtoplPeriod);
                                break;
                            default:
                                throw new Exception("Неизвестный алгоритм ГПУ " + houseChars.AlgorithmInNeOtoplPeriod);
                        }
                        lca.Add(l);
                    }
                    Iterate();
                }
            }
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    public class ConvetLgotaTest : ConvertCase
    {
        public ConvetLgotaTest()
        {
            ConvertCaseName = "LGOTA TEST - начисления по льготам";
            Position = 120;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            BufferEntitiesManager.DropTableData("CNV$LGOTSUMMA");
            var lg = new List<CNV_LGOTSUMMA>();
            ExcelFileInfo lgotaFileInfo = Consts.LgotaReportFile;
            DataTable lgotaTable = Utils.ReadExcelFile(lgotaFileInfo.FileName, lgotaFileInfo.ListName);

            ExcelFileInfo recodeTableFile = Consts.LgotaRecodeTableFile;
            DataTable recodeDt = Utils.ReadExcelFile(recodeTableFile.FileName, recodeTableFile.ListName);
            var recodeTable = new List<Recode>();
            for (int i = recodeTableFile.StartDataRow - 2; i <= recodeTableFile.EndDataRow - 2; i++)
            {
                recodeTable.Add(new Recode(recodeDt.Rows[i]));
            }
            recodeDt.Dispose();

            StepStart(lgotaTable.Rows.Count);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = lgotaFileInfo.StartDataRow - 2; i <= lgotaFileInfo.EndDataRow - 2; i++)
                {
                    var lgota = new Lgota(lgotaTable.Rows[i]);

                    DateTime date = new DateTime(2016,08,01);
                    var l = new CNV_LGOTSUMMA
                    {
                        LSHET = Consts.GetLs(lgota.Lshet),
                        MONTH_ = date.Month,
                        YEAR_ = date.Year,
                        MONTH2 = date.Month,
                        YEAR2 = date.Year,
                        DATE_VV = date,
                        TYPE_ = 0,
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен"
                    };

                    bool founded = false;
                    for (int j = 0; j < recodeTable.Count; j++)
                    {
                        if (recodeTable[j].LgotaName == lgota.Category)
                        {
                            l.LGOTACD = recodeTable[j].LgotaId;
                            //l.LGOTANAME = recodeTable[j].LgotaName;
                            founded = true;
                            break;
                        }
                    }
                    if (!founded) throw new Exception("В таблице перекодировке не найдена льгота " + lgota.Category);

                    string sql = String.Format(
                        @"select CITIZENID from cnv$citizens cn where cn.lshet = '{0}' and (cn.f || ' ' || cn.i || iif(cn.o is not null, ' ' || cn.o, '')) = '{1}'",
                        Consts.GetLs(lgota.Lshet), lgota.FIO.Replace("  ", " "));
                    var result = context.ExecuteQuery<int>(sql, CommandType.Text, null);
                    if (result.Count == 0)
                    {
                        Task.Run(
                            () => MessageBox.Show(String.Format("Не найден льготник {0} {1}", lgota.Lshet, lgota.FIO)));
                        continue;
                    }
                    if (result.Count > 1)
                    {
                        Task.Run(
                            () =>
                                MessageBox.Show(String.Format("Найдено несколько льготников ({2}) {0} {1}", lgota.Lshet,
                                    lgota.FIO, result.Count)));
                        continue;
                    }

                    l.CITYZENID = result[0];

                    l.SERVICECD = 3;
                    l.SUMMA = lgota.SummaOtopl;
                    l.SERVICENAME = "Отопление";
                    lg.Add(l);

                    lg.Add(new CNV_LGOTSUMMA
                    {
                        SERVICECD = 5,
                        SUMMA = lgota.SummaGVS,
                        SERVICENAME = "Гор. водоснабжение",
                        LSHET = l.LSHET,
                        MONTH_ = date.Month,
                        YEAR_ = date.Year,
                        MONTH2 = date.Month,
                        YEAR2 = date.Year,
                        DATE_VV = date,
                        TYPE_ = 0,
                        REGIMCD = 10,
                        REGIMNAME = "Неизвестен",
                        CITYZENID = l.CITYZENID,
                        LGOTACD = l.LGOTACD,
                        LGOTANAME = l.LGOTANAME
                    });;

                    Iterate();
                }
            }
            StepFinish();

            SaveList(lg, Consts.InsertRecordCount);
        }

        private class Recode
        {
            public string LgotaName;
            public int LgotaId;

            public Recode(DataRow dr)
            {
                LgotaName = dr[0].ToString().Trim();
                LgotaId = Int32.Parse(dr[1].ToString());
            }
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
            var missingInfo = new MissingInfo(dr);
            if (missingInfo.MissingStartDate.HasValue && missingInfo.MissingStartDate.Value < RegisteredStartDate)
                missingInfo.MissingStartDate = RegisteredStartDate;
            if (missingInfo.MissingEndDate.HasValue && RegisteredEndDate.HasValue && missingInfo.MissingEndDate.Value > RegisteredEndDate)
                missingInfo.MissingEndDate = RegisteredEndDate;
            MissingInfoList.Add(missingInfo);
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
        public int? InformationOwnerId;
        public int? ContractNumber;
        public string Address;
        public bool Active;
        public DateTime LsOpenDate;
        public DateTime? LsCloseDate;
        public string HasCounter;
        public string HasHVS;
        public decimal? IndividualNorm;
        public string GVSTypeOpen;
        public decimal? Square;
        public string NachGvsOdn;
        public string NachOtopl;
        public string NachPkTeplEnergy;
        public string NachPkInd;
        public string NachPkOdn;

        public LsInfo(DataRow dr)
        {
            try
            {
                Lshet = dr[0].ToString().Trim();
                FIO = dr[1].ToString();
                InformationOwner = dr[2].ToString();
                ContractNumber = String.IsNullOrWhiteSpace(dr[3].ToString())
                    ? (int?) null
                    : Int32.Parse(dr[3].ToString());
                Address = dr[5].ToString();
                Active = dr[6].ToString().ToLower().Trim() == "действующий";
                LsOpenDate = DateTime.Parse(dr[7].ToString());
                LsCloseDate = String.IsNullOrWhiteSpace(dr[8].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(dr[8].ToString());
                HasCounter = dr[9].ToString().ToLower().Trim();
                HasHVS = dr[10].ToString().ToLower().Trim();
                IndividualNorm = String.IsNullOrWhiteSpace(dr[11].ToString())
                    ? (decimal?) null
                    : Decimal.Parse(dr[11].ToString());
                GVSTypeOpen = dr[12].ToString().ToLower().Trim();
                Square = String.IsNullOrWhiteSpace(dr[13].ToString())
                    ? (decimal?) null
                    : Decimal.Parse(dr[13].ToString());
                NachGvsOdn = dr[14].ToString().ToLower().Trim();
                NachOtopl = dr[15].ToString().ToLower().Trim();
                NachPkTeplEnergy = dr[16].ToString().ToLower().Trim();
                NachPkInd = dr[17].ToString().ToLower().Trim();
                NachPkOdn = dr[18].ToString().ToLower().Trim();

                switch (InformationOwner.Trim().ToLower())
                {
                    case "непосредственное управление":
                        InformationOwnerId = 225;
                        break;
                    //case "нереализованный способ управления":
                    //    InformationOwnerId = 8;
                    //    break;
                    case "ооо фирма \"энергия\"":
                        InformationOwnerId = 203;
                        break;
                    case "ооо \"новый дом\"":
                        InformationOwnerId = 224;
                        break;
                    case "ооо \"фаворит+\"":
                        InformationOwnerId = 243;
                        break;
                    case "ооо \"фаворит\"":
                        InformationOwnerId = 242;
                        break;
                    case "ооо ук \"рэп-17\"":
                        InformationOwnerId = 244;
                        break;
                    case "ооо ук \"верхневолжский град\"":
                        InformationOwnerId = 202;
                        break;
                    case "общество с ограниченной ответственностью \"ук расцвет\"":
                        InformationOwner = "ООО \"УК Расцвет\"";
                        InformationOwnerId = 11;
                        break;
                    default:
                        if (String.IsNullOrWhiteSpace(InformationOwner))
                        {
                            InformationOwnerId = null;
                            InformationOwner = null;
                            break;
                        }
                        throw new Exception("Неизвестная УК " + InformationOwner);
                }
            }
            catch (Exception ex)
            {
                
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

        private static Regex addressRegex = new Regex(@"(?<index>\d{6}).+г\. ?Тверь,(?<district>[^,]+),(?<street>[^,]+)");
        private static Regex houseRegex = new Regex(@"д[\., ]+([^ ,]+)");
        private static Regex korpusRegex = new Regex(@"корп[\., ]+([^ ,]+)");
        private static Regex flatRegex = new Regex(@"кв[\., ]+([^ ,]+)");
        private static Regex roomRegex = new Regex(@"комната[\., ]+([^ ,]+)");
        private static Regex digitRegex = new Regex(@"(\d+)([^\d]+\d*)*");

        public void ExctractAddress(ref CNV_ABONENT abonent)
        {
            if (String.IsNullOrWhiteSpace(Address)) return;
            Address = Address.Replace("  ", " ");
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
                    case "в рукаве":
                        SetupPlaceId = 19;
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

    public class Spravka
    {
        public DateTime Date;
        public string Lshet;
        public ServiceMoney[] Services;
        public decimal HoursGVS;
        public decimal HoursOtopl;
        public decimal EndSaldo;

        public Spravka(DataRow dr, DateTime fileDate)
        {
            FileDate = fileDate;
            Date = fileDate;
            Lshet = dr[0].ToString().Trim();

            LsInfo abonent = null;
            for (int i = 0; i < ConvertNachopl.Abonents.Length; i++)
            {
                var abonentCheck = ConvertNachopl.Abonents[i];
                if (abonentCheck.Lshet == Lshet) abonent = abonentCheck;
            }
            if (abonent == null) ConvertNachopl.NotFoundedAbonents += Lshet + "\r\n";
            Services = new[]
            {
                new ServiceMoney(dr, 12, 26, 5, "Гор. водоснабжение", 10, "Неизвестен", true, true, abonent), // ГВС Гкал
                new ServiceMoney(dr, 14, 28, 5, "Гор. водоснабжение", 10, "Неизвестен", false, true, abonent), // Гвс Тн
                new ServiceMoney(dr, 16, 30, 5, "Гор. водоснабжение", 10, "Неизвестен", false, true, abonent), // ХВС куб.м
                new ServiceMoney(dr, 18, 33, 3, "Отопление", 10, "Неизвестен", true, true, abonent), // Отопление Гкал
                new ServiceMoney(dr, 20, 0, 15, "Гор. водоснабжение ОДН", 10, "Неизвестен", true, true, abonent), // ОДН Гкал
                new ServiceMoney(dr, 22, 0, 15, "Гор. водоснабжение ОДН", 10, "Неизвестен", false, true, abonent), // ОДН Тн
                new ServiceMoney(dr, 24, 0, 15, "Гор. водоснабжение ОДН", 10, "Неизвестен", false, true, abonent), // ОДН куб.м
                new ServiceMoney(dr, 39, 41, 105, "Гор. водоснабжение пов. коэф.", 10, "Неизвестен", true, false, null), // коеф. инд.
                new ServiceMoney(dr, 40, 0, 115, "Гор. водоснабжение ОДН пов. коэф", 10, "Неизвестен", true, false, null), // коеф. одн
            };
            
            EndSaldo = String.IsNullOrWhiteSpace(dr[45 - 1].ToString()) ? 0 : Decimal.Parse(dr[45 - 1].ToString().Replace('.', ','));
            HoursGVS = String.IsNullOrWhiteSpace(dr[6 - 1].ToString()) ? 0 : Decimal.Parse(dr[6 - 1].ToString().Replace('.',','));
            HoursOtopl = String.IsNullOrWhiteSpace(dr[7 - 1].ToString()) ? 0 : Decimal.Parse(dr[7 - 1].ToString().Replace('.',','));
        }

        public static DateTime FileDate;

        public class ServiceMoney
        {
            public int ServiceCd;
            public string ServiceName;
            public int RegimCd;
            public string RegimName;
            public int SummaType;
            public int? VolumeType;

            public decimal Volume;
            public decimal Nach;
            public decimal RecalcVol;
            public decimal RecalcSum;

            public ServiceMoney(DataRow dr, int volumeId, int recalcId, int servicecd, string servicename, int regimcd,
                string regimname, bool gkal, bool withVolume, LsInfo abonent = null)
            {
                ServiceCd = servicecd;
                ServiceName = servicename;
                RegimCd = regimcd;
                RegimName = regimname;
                SummaType = 0;

                if (withVolume)
                {
                    Volume = String.IsNullOrWhiteSpace(dr[volumeId - 1].ToString())
                        ? 0
                        : Decimal.Parse(dr[volumeId - 1].ToString(), NumberStyles.Float);
                    VolumeType = 1;
                }
                Nach = String.IsNullOrWhiteSpace(dr[volumeId].ToString())
                    ? 0
                    : Decimal.Parse(dr[volumeId].ToString(), NumberStyles.Float);
                if (recalcId != 0)
                {
                    if (withVolume)
                        RecalcVol = String.IsNullOrWhiteSpace(dr[recalcId - 1].ToString())
                            ? 0
                            : Decimal.Parse(dr[recalcId - 1].ToString(), NumberStyles.Float);
                    RecalcSum = String.IsNullOrWhiteSpace(dr[recalcId].ToString())
                        ? 0
                        : Decimal.Parse(dr[recalcId].ToString(), NumberStyles.Float);
                }

                if ((Nach != 0 || Volume != 0 || RecalcSum != 0 || RecalcVol != 0) && abonent != null && abonent.IndividualNorm != null)
                {
                    if (abonent.IndividualNorm.Value == 0.0257m || abonent.IndividualNorm.Value == 0) return;
                    if (servicecd == 3)
                    {
                        RegimCd = 301;
                        SummaType = 0;
                        VolumeType = 1;
                        return;
                    }
                    bool founded = false;
                    for (int i = 0; i < ConvertNachopl.RecodeTable.Length; i++)
                    {
                        var recode = ConvertNachopl.RecodeTable[i];
                        if ((recode.Scheme == null || abonent.GVSTypeOpen == recode.Scheme) &&
                            abonent.IndividualNorm == recode.Blago &&
                            (recode.HasCounter == null || abonent.HasCounter == recode.HasCounter) &&
                            gkal == recode.GKal &&
                            servicecd == recode.ServiceCD)
                        {
                            founded = true;
                            RegimCd = recode.Regim;
                            SummaType = recode.SummaType;
                            VolumeType = recode.VolumeType;
                            break;
                        }
                    }
                    if (!founded)
                        throw new Exception(String.Format("Не найдено соответствие в таблице перекодировки\r\nЛС: {0}; Услуга: {1}; Гкал: {2}", abonent.Lshet, servicecd, gkal));
                }
            }
        }

        public static string GetFileName(DateTime date)
        {
            return String.Format("справка расчет {0:D2}.{1}.xls",date.Month, date.Year);
        }
    }

    public class PKCoef
    {
        public string Lshet;
        public decimal PereInd;
        public decimal PereOdn;
        public decimal EndSaldoInd;
        public decimal EndSaldoOdn;

        public PKCoef() { }
        public static DateTime Date;
        public PKCoef(DataRow dr)
        {
            try
            {
                Lshet = dr[0].ToString().Trim();
                PereInd = Math.Round(Decimal.Parse(dr[17].ToString(), NumberStyles.Float), 4);
                PereOdn = Math.Round(Decimal.Parse(dr[18].ToString(), NumberStyles.Float), 4);
                EndSaldoInd = Math.Round(Decimal.Parse(dr[24].ToString(), NumberStyles.Float), 4);
                EndSaldoOdn = Math.Round(Decimal.Parse(dr[25].ToString(), NumberStyles.Float), 4);
            }
            catch (Exception xe)
            {
                throw;
            }
        }

        public static string GetFileName(DateTime date)
        {
            return String.Format("ПК_{0:D2}_{1}.xlsx", date.Month, date.Year);
        }
    }

    public class Oplata
    {
        public string Lshet;
        public string Source;
        public int SourceCd;
        public DateTime? PayDate;
        public decimal ServicesSumma;
        public decimal CoefSumma;
        public decimal Summa;
        public decimal IndCoefSumma;
        public decimal OdnCoefSumma;

        public static DateTime Date;

        public Oplata(DataRow dr)
        {
            try
            {
                Lshet = dr[0].ToString().Trim();
                Source = dr[3].ToString().Trim();
                if (String.IsNullOrWhiteSpace(dr[4].ToString()))
                {
                    if (String.IsNullOrWhiteSpace(dr[6].ToString()))
                        PayDate = null;
                    else
                        PayDate = DateTime.Parse(dr[6].ToString());
                }
                else PayDate = DateTime.Parse(dr[4].ToString());
                ServicesSumma = String.IsNullOrWhiteSpace(dr[7].ToString()) ? 0 : Decimal.Parse(dr[7].ToString());
                IndCoefSumma = String.IsNullOrWhiteSpace(dr[8].ToString()) ? 0 : Decimal.Parse(dr[8].ToString());
                OdnCoefSumma = String.IsNullOrWhiteSpace(dr[9].ToString()) ? 0 : Decimal.Parse(dr[9].ToString());
                CoefSumma = String.IsNullOrWhiteSpace(dr[10].ToString()) ? 0 : Decimal.Parse(dr[10].ToString());
                Summa = String.IsNullOrWhiteSpace(dr[11].ToString()) ? 0 : Decimal.Parse(dr[11].ToString());

                if (String.IsNullOrWhiteSpace(Source))
                {
                    Source = "Неизвестен";
                    SourceCd = 9;
                }
                else
                    switch (Source)
                    {
                        case "Касса":
                            SourceCd = 11;
                            break;
                        case "Сбербанк":
                            SourceCd = 2;
                            break;
                        case "Почта":
                            SourceCd = 4;
                            break;
                        case "Бин Банк":
                            SourceCd = 5;
                            break;
                        case "ТГБ":
                            SourceCd = 6;
                            break;
                        case "Выписка":
                            SourceCd = 8;
                            break;
                        case "Уралсиб":
                            SourceCd = 7;
                            break;
                        default:
                            throw new Exception("Неизвестный источник оплаты: " + Source);
                    }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string GetListName(DateTime date)
        {
            return String.Format("{0}.{1}", date.ToString("MM"), date.ToString("yy"));
        }
    }

    public class HouseChars
    {
        public string District;
        public string Address;
        public decimal? TotalSquare;
        public decimal? HeatingSquare;
        public int HouseId;
        public bool HasODNCounter;
        public string AlgorithmInOtoplPeriod;
        public string AlgorithmInNeOtoplPeriod;

        public string Street;
        public string HouseNo;
        public string HousePostfix;
        public int? KorpusNo;

        private static readonly Regex HouseRegex = new Regex(@"(\d+)(.*)");
        private static readonly Regex KorpusRegex = new Regex(@"к(\d+)");

        public HouseChars(DataRow dr)
        {

            District = dr[2].ToString().Trim();
            Address = dr[4].ToString().Trim();
            TotalSquare = String.IsNullOrWhiteSpace(dr[16].ToString())
                ? (decimal?)null
                : Decimal.Parse(dr[16].ToString());
            HeatingSquare = String.IsNullOrWhiteSpace(dr[17].ToString())
                ? (decimal?) null
                : Decimal.Parse(dr[17].ToString());
            HouseId = Int32.Parse(dr[33].ToString());
            HasODNCounter = dr[13].ToString().ToLower().Trim() == "да";
            AlgorithmInOtoplPeriod = dr[11].ToString().Trim();
            AlgorithmInNeOtoplPeriod = dr[12].ToString().Trim();

            string[] separetedAddress = Address.Split(',');
            if (separetedAddress.Length != 2) throw new Exception("Запятая не одна " + Address);
            Street = separetedAddress[0].Trim();
            for (int i = 0; i < Consts.Prefixes.Length; i++)
            {
                Street = Street.Replace(Consts.Prefixes[i], "");
            }
            Street = Street.Replace(".", "").Trim();
            string house = separetedAddress[1];
            var korpusMatch = KorpusRegex.Match(house);
            if (korpusMatch.Success)
            {
                KorpusNo = Int32.Parse(korpusMatch.Groups[1].Value);
                house = house.Replace(korpusMatch.Value, "").Trim();
            }
            var groups = HouseRegex.Match(house).Groups;
            HouseNo = groups[1].Value.Trim();
            HousePostfix = groups[2].Value.Trim();
        }
    }

    public class EmptyHouseInfo
    {
        public int Id;
        public int ContractId;
        public string FullName;
        public string Address;
        public string WatercaptureType;
        public string HasIPU;
        public string NachODPU;
        public decimal? Square;
        public int? HoursPerWeek;
        public int? HoursPerDay;
        public string HouseType;

        public decimal DopGVSCloseGKal;
        public decimal DopGVSCloseTn;
        public decimal DopGVSOpenGKal;
        public decimal DopGVSOpenTn;
        public decimal DopOtoplGKal;
        public decimal DopOtoplTn;

        public string Lshet;

        public EmptyHouseInfo(DataRow dr)
        {
            Id = Int32.Parse(dr[0].ToString());
            ContractId = Int32.Parse(dr[1].ToString());
            FullName = dr[2].ToString();
            Address = dr[3].ToString();
            WatercaptureType = dr[19].ToString().ToLower().Trim();
            HasIPU = dr[20].ToString().ToLower().Trim();
            NachODPU = dr[21].ToString().ToLower().Trim();
            Square = String.IsNullOrWhiteSpace(dr[22].ToString())
                ? (decimal?) null
                : Decimal.Parse(dr[22].ToString().Replace('.',','), NumberStyles.Float);
            HoursPerWeek = String.IsNullOrWhiteSpace(dr[17].ToString())
                ? (int?) null
                : Int32.Parse(dr[17].ToString());
            HoursPerDay = String.IsNullOrWhiteSpace(dr[18].ToString())
                ? (int?) null
                : Int32.Parse(dr[18].ToString());
            HouseType = dr[23].ToString().ToLower().Trim();

            DopGVSCloseGKal = String.IsNullOrWhiteSpace(dr[11].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[11].ToString(), NumberStyles.Float), 4);
            DopGVSCloseTn = String.IsNullOrWhiteSpace(dr[12].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[12].ToString(), NumberStyles.Float), 4);
            DopGVSOpenGKal = String.IsNullOrWhiteSpace(dr[13].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[13].ToString(), NumberStyles.Float), 4);
            DopGVSOpenTn = String.IsNullOrWhiteSpace(dr[14].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[14].ToString(), NumberStyles.Float), 4);
            DopOtoplGKal = String.IsNullOrWhiteSpace(dr[15].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[15].ToString(), NumberStyles.Float), 4);
            DopOtoplTn = String.IsNullOrWhiteSpace(dr[16].ToString())
                ? 0
                : Math.Round(Decimal.Parse(dr[16].ToString(), NumberStyles.Float), 4);
            Lshet = Id.ToString();
        }

        private static readonly Regex HouseRegex = new Regex(@"(\d+)(.*)");
        private static readonly Regex KorpusRegex = new Regex(@"к(\d+)");

        public void ExtractAddress(ref CNV_ABONENT abonent)
        {
            string[] separetedAddress = Address.Split(',');
            if (separetedAddress.Length != 2) throw new Exception("Запятая не одна " + Address);
            abonent.ULICANAME = separetedAddress[0].Trim();
            for (int i = 0; i < Consts.Prefixes.Length; i++)
            {
                abonent.ULICANAME = abonent.ULICANAME.Replace(Consts.Prefixes[i], "");
            }
            abonent.ULICANAME = abonent.ULICANAME.Replace(".", "").Trim();
            string house = separetedAddress[1];
            var korpusMatch = KorpusRegex.Match(house);
            if (korpusMatch.Success)
            {
                abonent.KORPUSNO = Int32.Parse(korpusMatch.Groups[1].Value);
                house = house.Replace(korpusMatch.Value, "").Trim();
            }
            var groups = HouseRegex.Match(house).Groups;
            abonent.HOUSENO = groups[1].Value.Trim();
            abonent.HOUSEPOSTFIX = groups[2].Value.Trim();
            if (String.IsNullOrWhiteSpace(abonent.HOUSENO)) abonent.HOUSENO = null;
            if (String.IsNullOrWhiteSpace(abonent.HOUSEPOSTFIX)) abonent.HOUSEPOSTFIX = null;
        }

        public void ExtractFio(ref CNV_ABONENT house)
        {
            //house.F = FullName.Split(',')[0].Trim();
            house.F = FullName;
        }
    }

    public class Lgota
    {
        public string Lshet;
        public string FIO;
        public string Category;
        public int LgotaPeopleCount;
        public long PKU;
        public DateTime StartDate;
        public DateTime? EndDate;

        public decimal SummaGVS;
        public decimal SummaOtopl;
        public decimal SummaTotal;

        public Lgota(DataRow dr)
        {
            Lshet = dr[1].ToString().Trim();
            FIO = dr[3].ToString().Trim();
            Category = dr[4].ToString().Trim();
            LgotaPeopleCount = Int32.Parse(dr[5].ToString());
            PKU = Int64.Parse(dr[6].ToString());
            StartDate = DateTime.Parse(dr[7].ToString());
            EndDate = String.IsNullOrWhiteSpace(dr[8].ToString()) ? (DateTime?) null : DateTime.Parse(dr[8].ToString());
            SummaGVS = Decimal.Parse(dr[9].ToString());
            SummaOtopl = Decimal.Parse(dr[10].ToString());
            SummaTotal = Decimal.Parse(dr[11].ToString());
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

            // поиск дубликатов по инициалам граждарн
            StepStart(1);
            var roomers = RoomerInfo.ReadRoomers(Consts.RoomingReportFile);
            StepFinish();

            var fewFounded = new List<KeyValuePair<string, List<RoomerInfo>>>();

            foreach (var roomer in roomers)
            {
                foreach (var roomerInfo in roomer.Value)
                {
                    int fouded = 0;
                    var cityzen = new CNV_CITIZEN();
                    roomerInfo.ExtractFio(ref cityzen);
                    if ((!String.IsNullOrWhiteSpace(cityzen.I) && cityzen.I.Length == 1) &&
                        (!String.IsNullOrWhiteSpace(cityzen.O) && cityzen.O.Length == 1))
                    {
                        foreach (var checkRoomer in roomer.Value)
                        {
                            var checkCityzen = new CNV_CITIZEN();
                            checkRoomer.ExtractFio(ref checkCityzen);
                            if ((!String.IsNullOrWhiteSpace(checkCityzen.I) && checkCityzen.I[0] == cityzen.I[0]) &&
                                (!String.IsNullOrWhiteSpace(checkCityzen.O) && checkCityzen.O[0] == cityzen.O[0]))
                                fouded++;
                        }
                    }
                    if (fouded > 2) fewFounded.Add(roomer);
                }
            }
            int a = 10;

            //// поиск граждан по льготам
            //ExcelFileInfo fileInfo = Consts.LgotaReportFile;
            //DataTable lgotaTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //StepStart(lgotaTable.Rows.Count + 1);
            //var correctFounded = "";
            //var notFounded = "";
            //var fewFounded = "";
            //var endedLgots = new List<Lgota>();
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //    {
            //        Iterate();
            //        var lgota = new Lgota(lgotaTable.Rows[i]);

            //        // поиск деактивированных льгот
            //        //if (lgota.EndDate.HasValue && lgota.EndDate < DateTime.Now)
            //        //    endedLgots.Add(lgota);

            //        //continue;
            //        string sql = String.Format(
            //            @"select CITIZENID from cnv$citizens cn where cn.lshet = '{0}' and (cn.f || ' ' || cn.i || iif(cn.o is not null, ' ' || cn.o, '')) = '{1}'",
            //            Consts.GetLs(lgota.Lshet), lgota.FIO.Replace("  ", " "));
            //        var result = context.ExecuteQuery<int>(sql, CommandType.Text, null);
            //        if (result.Count == 1)
            //            correctFounded += lgota.Lshet + "\t" + lgota.FIO + "\r\n";
            //        if (result.Count == 0)
            //            notFounded += lgota.Lshet + "\t" + lgota.FIO + "\r\n";
            //        if (result.Count > 1)
            //            fewFounded += lgota.Lshet + "\t" + lgota.FIO + "\r\n";
            //    }
            //}

            //// список льгот
            //ExcelFileInfo fileInfo = Consts.LgotaReportFile;
            //DataTable lgotaTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //StepStart(lgotaTable.Rows.Count + 1);
            //var ll = new List<string>();
            //for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //{
            //    Iterate();
            //    string lgota = lgotaTable.Rows[i][4].ToString();
            //    ll.Add(lgota);
            //}

            //string lgotaList = "";
            //foreach (var lgota in ll.Distinct().ToArray())
            //{
            //    lgotaList += lgota + "\r\n";
            //}
            //int a = 10;


            //// тест измененных ЛС
            //var minDate = new DateTime(2015, 05, 01);
            //var maxDate = new DateTime(2016, 07, 01);

            //StepStart(1);
            //for (var date = minDate; date <= maxDate; date = date.AddMonths(1))
            //{
            //    DataTable moneyTable = Utils.ReadExcelFile(Consts.SpravkaFolder + Spravka.GetFileName(date), "66186");
            //    for (int i = 0; i < moneyTable.Rows.Count; i++)
            //    {
            //        long ls;
            //        if (!Int64.TryParse(moneyTable.Rows[i][0].ToString(), out ls)) continue;

            //        var money = new Spravka(moneyTable.Rows[i], date);
            //        if (money.Lshet == "5007001")
            //        {
            //            int a = 10;
            //        }
            //        string lshet = Consts.FixLs(money.Lshet, date);
            //    }
            //}
            //StepFinish();



            // проверка дублирования районов
            //ExcelFileInfo fileInfo = Consts.HousesCharsFile;
            //DataTable houseTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //StepStart(houseTable.Rows.Count + 1);
            //var lh = new List<HouseChars>();
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //    {
            //        var houseChars = new HouseChars(houseTable.Rows[i]);
            //        string query = String.Format(
            //            "SELECT DISTINCT DISTNAME FROM CNV$ABONENT WHERE ULICANAME LIKE '%{0}%' AND HOUSENO = '{1}'",
            //            houseChars.Street, houseChars.HouseNo);
            //        if (!String.IsNullOrWhiteSpace(houseChars.HousePostfix))
            //            query += String.Format(" AND HOUSEPOSTFIX = '{0}'", houseChars.HousePostfix);
            //        if (houseChars.KorpusNo.HasValue)
            //            query += String.Format(" AND KORPUSNO = '{0}'", houseChars.KorpusNo.Value);
            //        var result = context.ExecuteQuery<string>(query);
            //        if (result.Count > 1) 
            //        {
            //            lh.Add(houseChars);
            //        }
            //        Iterate();
            //    }
            //}
            //StepFinish();

            //// проверка сопоставления адресов в данных о домах и о ЛС
            //ExcelFileInfo fileInfo = Consts.HousesCharsFile;
            //DataTable houseTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListName);
            //StepStart(houseTable.Rows.Count + 1);
            //var lhUnknown = new List<HouseChars>();
            //var notEqualDistricts = "";
            //using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            //{
            //    for (int i = fileInfo.StartDataRow - 2; i <= fileInfo.EndDataRow - 2; i++)
            //    {
            //        var houseChars = new HouseChars(houseTable.Rows[i]);
            //        string query = String.Format(
            //            "SELECT FIRST 1 * FROM CNV$ABONENT WHERE ULICANAME LIKE '%{0}%' AND HOUSENO = '{1}'",
            //            houseChars.Street, houseChars.HouseNo);
            //        if (!String.IsNullOrWhiteSpace(houseChars.HousePostfix))
            //            query += String.Format(" AND HOUSEPOSTFIX = '{0}'", houseChars.HousePostfix);
            //        if (houseChars.KorpusNo.HasValue)
            //            query += String.Format(" AND KORPUSNO = '{0}'", houseChars.KorpusNo.Value);
            //        var result = context.ExecuteQuery<CNV_ABONENT>(query);
            //        if (!result.Any()) lhUnknown.Add(houseChars);
            //        else
            //        {
            //            var abonent = result[0];
            //            if (abonent.DISTNAME != houseChars.District)
            //                notEqualDistricts += String.Format("{0,-30}\t{1, -20}\t{2}\r\n", houseChars.Address,
            //                    houseChars.District, abonent.DISTNAME);
            //        }
            //        Iterate();
            //    }
            //}
            //StepFinish();


            //// несколько жильцов с одинаковым ФИО
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

            //StepStart(roomers.Count + 1);
            //var test = new List<string>();
            //foreach (var roomer in roomers)
            //{
            //    Iterate();
            //    var fioGroup = roomer.Value.Where(r => !String.IsNullOrWhiteSpace(r.FIO)).GroupBy(r => r.FIO);
            //    foreach (var group in fioGroup)
            //    {
            //        if (group.Count() > 1)
            //        {
            //            test.Add(roomer.Key);
            //            break;
            //        }
            //    }
            //}
            //StepFinish();

            //string result = "";
            //for (int i = 0; i < test.Count; i++)
            //{
            //    result += test[i] + "\r\n";
            //}
            //int a = 10;


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
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] { "1" });
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
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "1", "0" });
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

    public class TransferNachopl : ConvertCase
    {
        public TransferNachopl()
        {
            ConvertCaseName = "Перенос данных о истории оплат и начислений";
            Position = 1070;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(6);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
                Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            StepFinish();
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
            fbm.ExecuteProcedure("CNV$CNV_03050_CITIZENSMIGR_TVER", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }

    public class TransferCharsHouses : ConvertCase
    {
        public TransferCharsHouses()
        {
            ConvertCaseName = "Перенос данных о количественных характеристиках домов";
            Position = 1080;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_00850_CHARSHOUSES", new[] { "1" });
            Iterate();
            StepFinish();
        }
    }

    public class CalculateAbonentDolya : ConvertCase
    {
        public CalculateAbonentDolya()
        {
            ConvertCaseName = "Подсчет доли абонента";
            Position = 1090;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_03100_TVER_ABONENTDOLYA");
            Iterate();
            StepFinish();
        }
    }

    public class TransferLgota : ConvertCase
    {
        public TransferLgota()
        {
            ConvertCaseName = "Перенос данных о льготах";
            Position = 1330;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_03200_CITYZENLGOTA_TVER", new []{"1"});
            Iterate();
            StepFinish();
        }
    }
}
