using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _047_Tver
{
    public class Consts
    {
        public const string RoomingReportFile =
            @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\отчет учет жильцов.xlsx";

        public const string RoomersReportList = "6618116";

        public const string LsInfoFile = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ЛС.xls";
        public const string LsInfoList = "66184";
    }

    public class TestLsInfoConvert : ConvertCase
    {
        public TestLsInfoConvert()
        {
            ConvertCaseName = "Тест конвертации информации о ЛС";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\047_Tver\Sources\информация по ЛС.xls";
            DataTable infoTable = Utils.ReadExcelFile(fileName, "66184");
            StepStart(infoTable.Rows.Count);
            var lsList = new List<LsInfo>();
            for (int i = 0; i < infoTable.Rows.Count - 1; i++)
            {
                var lsInfo = new LsInfo(infoTable.Rows[i]);
                if (!lsInfo.Address.Contains("Тверь")) lsList.Add(lsInfo);
                Iterate();
            }
            StepFinish();
        }
    }

    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENTS - конвертация информации об абонентах";
            Position = 20;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            DataTable reportTable = Utils.ReadExcelFile(Consts.RoomingReportFile, Consts.RoomersReportList);

            var lsInfoList = new List<LsInfo>();
            DataTable lsInfoTable = Utils.ReadExcelFile(Consts.LsInfoFile, Consts.LsInfoList);
            StepStart(lsInfoTable.Rows.Count);
            for (int i = 2-2; i <= 13390-2; i++)
            {
                lsInfoList.Add(new LsInfo(lsInfoTable.Rows[i]));
                Iterate();   
            }
            lsInfoTable.Dispose();
            StepFinish();
            var la = new List<Listitem>();
            StepStart(reportTable.Rows.Count + 1);
            //var dic = new List<Test>();
            RoomerInfo.OnlyHouse = "";
            for (int i = 38976-2; i >= 6-2; i--)
            {
                if (i == 37227)
                {
                    int a = 10;
                }
                Iterate();
                if (String.IsNullOrWhiteSpace(reportTable.Rows[i][0].ToString())) continue;
                var roomerInfo = new RoomerInfo(reportTable.Rows[i]);
                if (!roomerInfo.IsOwner) continue;


                //var postIndexRegex = new Regex(@"\d{6}");
                //string postIndex = postIndexRegex.Match(roomerInfo.Address).Value;
                //string clearedAddress = roomerInfo.Address.Trim();
                //if (!String.IsNullOrWhiteSpace(postIndex))
                //    clearedAddress = roomerInfo.Address.Replace(postIndex, "").Trim();
                //string[] separatedAddress = clearedAddress.Split(',');
               


                //dic.Add(new Test{Count = separatedAddress.Length, Info = roomerInfo});
                var abonent = new CNV_ABONENT
                {
                    LSHET = roomerInfo.Lshet,
                    EXTLSHET = roomerInfo.Lshet,
                    ISDELETED = roomerInfo.CloseLsDate == null ? 1 : 0,
                    PHONENUM = roomerInfo.PhoneNumber
                };
                roomerInfo.ExtractFio(ref abonent);
                //roomerInfo.ExctractAddress(ref abonent);

                var lsInfo = lsInfoList.SingleOrDefault(ls => ls.Lshet == abonent.LSHET);
                if (lsInfo != null)
                {
                    abonent.ISDELETED = lsInfo.Active ? 0 : 1;
                }

                try
                {
                    var lastInfo = la.SingleOrDefault(l => l.Abonent.LSHET == roomerInfo.Lshet);
                    if (lastInfo != null )
                    {
                        if (lastInfo.Roomer.RegisteredStartDate < roomerInfo.RegisteredStartDate)
                        {
                            lastInfo.Roomer = roomerInfo;
                            lastInfo.Abonent = abonent;
                        }
                    }
                    else
                        la.Add(new Listitem {Abonent = abonent, Roomer = roomerInfo});
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            var test = la.Single(l => l.Abonent.LSHET == "4804013");
            var test2 = la.Single(l => l.Abonent.LSHET == "4918104");
            //var t1 = dic.Where(d => d.Count == 1).ToList();
            //var t2 = dic.Where(d => d.Count == 2).ToList();
            //var t3 = dic.Where(d => d.Count == 3).ToList();
            //var t4 = dic.Where(d => d.Count == 4).ToList();
            //var t5 = dic.Where(d => d.Count == 5).ToList();
            //var t6 = dic.Where(d => d.Count == 6).ToList();
            //var t7 = dic.Where(d => d.Count == 7).ToList();
            //var t8 = dic.Where(d => d.Count == 8).ToList();
            //var t9 = dic.Where(d => d.Count == 9).ToList();

            StepFinish();
        }

        private class Listitem
        {
            public RoomerInfo Roomer;
            public CNV_ABONENT Abonent;
        }
    }

    public class ConvertRoomer : ConvertCase
    {
        public ConvertRoomer()
        {
            ConvertCaseName = "Информация о проживающих";
            Position = 30;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(3);
            DataTable reportTable = Utils.ReadExcelFile(Consts.RoomingReportFile, Consts.RoomersReportList);
            string currentLs = null;
            RoomerInfo lastRommerInfo = null;
            var roomers = new Dictionary<string, List<RoomerInfo>>();
            var lc = new List<CNV_CHAR>(); 
            StepStart(reportTable.Rows.Count + 1);
            for (int i = 4; i <= 38974; i++)
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
        }
    }


    public class Test
    {
        public int Count;
        public RoomerInfo Info;
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
            try
            {
                Lshet = dr[0].ToString();
                OpenLsDate = DateTime.Parse(dr[1].ToString());
                CloseLsDate = String.IsNullOrWhiteSpace(dr[2].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(dr[2].ToString());
                Address = dr[3].ToString();
                FIO = dr[4].ToString();
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ReadMissingInfo(DataRow dr)
        {
            if (String.IsNullOrWhiteSpace(dr[12].ToString())) return;
            if (MissingInfoList == null) MissingInfoList = new List<MissingInfo>();
            MissingInfoList.Add(new MissingInfo(dr));
        }

        public void ExtractFio(ref CNV_ABONENT abonent)
        {
            if (String.IsNullOrWhiteSpace(FIO)) return;
            string[] splittedFio = FIO.Split(' ');
            if (splittedFio.Length == 0)
            {
                abonent.F = FIO;
                return;
            }
            abonent.F = splittedFio[0];
            if (splittedFio.Length > 1) abonent.I = splittedFio[1];
            if (splittedFio.Length > 2)
            {
                abonent.O = "";
                for (int i = 2; i < splittedFio.Length; i++)
                {
                    abonent.O += splittedFio[i] + " ";
                }
                abonent.O = abonent.O.TrimEnd(' ');
            }
        }

        public static string OnlyHouse;

        public void ExctractAddress(ref CNV_ABONENT abonent)
        {
            var postIndexRegex = new Regex(@"\d{6}");
            string postIndex = postIndexRegex.Match(Address).Value;
            string clearedAddress = Address.Trim();
            if (!String.IsNullOrWhiteSpace(postIndex))
            {
                abonent.POSTINDEX = postIndex.Trim();
                clearedAddress = Address.Replace(postIndex, "").Trim();
            }
            string[] separatedAddress = clearedAddress.Split(',');
            
            var digitsRegex = new Regex(@"^\d[^а-я]*[а-я]?[^а-я]*", RegexOptions.IgnoreCase);
            bool hasBothHouseAndFlat = digitsRegex.IsMatch(separatedAddress[separatedAddress.Length - 1].Trim()) &&
                                       digitsRegex.IsMatch(separatedAddress[separatedAddress.Length - 2].Trim());
            if (!hasBothHouseAndFlat)
            {
                OnlyHouse += String.Format("{0}    {1}\r\n", Lshet, Address);

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

        public LsInfo(DataRow dr)
        {
            try
            {
                HouseGroupId = dr.IsNull(0) ? (int?) null : Int32.Parse(dr[0].ToString());
                InformationOwner = dr[2].ToString();
                IOContractNumber = Int64.Parse(dr[3].ToString());
                Lshet = dr[4].ToString();
                Distict = String.IsNullOrWhiteSpace(dr[7].ToString()) ? null : dr[7].ToString();
                ContractDate = DateTime.Parse(dr[9].ToString());
                ContratExpireDate = String.IsNullOrWhiteSpace(dr[10].ToString()) ? (DateTime?) null : DateTime.Parse(dr[10].ToString());
                IndividualNorm = dr.IsNull(13) ? (decimal?) null : Decimal.Parse(dr[13].ToString());
                Square = Double.Parse(dr[15].ToString());
                LivingCount = Int32.Parse(dr[16].ToString());

                Active = dr[8].ToString().ToLower().Trim() == "действующий";
                HaveCountres = dr[11].ToString().ToLower().Trim() == "да";
                HaveHvs = dr[12].ToString().ToLower().Trim() == "есть";
                WaterCaptureTypeOpen = dr[14].ToString().ToLower().Trim() == "открытая";
                Address = dr[6].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
