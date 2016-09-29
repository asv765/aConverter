using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _043_PkStroy
{
    public static class Consts
    {
        public const int InsertRecordCount = 1000;
        public static readonly int CurrentMonth = 10;
        public static readonly int CurrentYear = 2016;

        public static string FormFullFilePath(string fileName)
        {
            return String.Format(@"{0}\{1}", aConverter_RootSettings.SourceDbfFilePath, fileName);
        }

        public static string GetLs(int ls)
        {
            return String.Format("96{0:D6}", ls);
        }

        public static int FormUniqLshetPrefix(int fileUniqId, int listNumber)
        {
            return 200000 + fileUniqId*10000 + listNumber*100;
        }
    }

    public class ExcelData
    {
        public int UniqId;
        public string HouseNumber;
        public string FlatNumber;
        public string FIO;
        public string PeopleCount;
        public string Norm;
        public string CounterNorm;
        public string WaterNorm;

        public ExcelData(DataRow dr)
        {
            
            HouseNumber = String.IsNullOrWhiteSpace(dr[0].ToString()) ? null : dr[0].ToString().Trim();
            if (HouseNumber != null) HouseNumber = HouseNumber.Replace("л", "");
            FlatNumber = String.IsNullOrWhiteSpace(dr[1].ToString()) ? null : dr[1].ToString().Trim();
            if (FlatNumber != null) FlatNumber = FlatNumber.Replace("л", "");
            FIO = String.IsNullOrWhiteSpace(dr[2].ToString()) ? null : dr[2].ToString().Trim();
            PeopleCount = String.IsNullOrWhiteSpace(dr[3].ToString()) ? null : dr[3].ToString().Trim();
            Norm = String.IsNullOrWhiteSpace(dr[4].ToString()) ? null : dr[4].ToString().Trim();
            CounterNorm = String.IsNullOrWhiteSpace(dr[5].ToString()) ? null : dr[5].ToString().Trim();
            WaterNorm = String.IsNullOrWhiteSpace(dr[6].ToString()) ? null : dr[6].ToString().Trim();
        }
    }

    public class ExcelListReader
    {
        public List<CNV_ABONENT> Abonents;
        public List<CNV_CHAR> CChars;
        public List<CNV_LCHAR> LChars;
        public List<CNV_COUNTER> Counters;

        private readonly ExcelData[] _excelData;
        private readonly ExcelListInfo _listInfo;
        private readonly string _fileName;

        private int? _lastUniqId;

        public ExcelListReader(string fileName, ExcelListInfo listInfo, int lshetPrefix)
        {
            Abonents = null;
            CChars = null;
            LChars = null;
            Counters = null;
            _lastUniqId = null;

            _fileName = fileName;
            _listInfo = listInfo;

            using (DataTable dt = Utils.ReadExcelFile(fileName, listInfo.ListName))
            {
                var tempExcelDataList = new List<ExcelData>();
                int end = listInfo.EndRow == 0 ? dt.Rows.Count : listInfo.EndRow - 1;
                for (int i = listInfo.StartRow - 2; i < end; i++)
                {
                    if (dt.Rows[i][0].ToString().ToUpper() == "ИТОГО") break;
                    var excelData = new ExcelData(dt.Rows[i]) {UniqId = lshetPrefix + i + 2};
                    if (excelData.FIO == null && excelData.PeopleCount == null && excelData.Norm == null &&
                        excelData.CounterNorm == null && excelData.WaterNorm == null)
                        continue;
                    tempExcelDataList.Add(excelData);
                }
                _excelData = tempExcelDataList.ToArray();
            }
        }

        private delegate void ConvertFunc(ExcelData excelData);
        private delegate void FinishFunc();

        public void Read(ReadType? readType = null)
        {
            ConvertFunc convertFunc = data => { };
            FinishFunc finishFunc = () => { };
            if (!readType.HasValue)
            {
                Abonents = new List<CNV_ABONENT>();
                convertFunc += ConvertAbonent;
                finishFunc += FinishAbonent;
                CChars = new List<CNV_CHAR>();
                convertFunc += ConvertChars;
                finishFunc += FinishChars;
                Counters = new List<CNV_COUNTER>();
                convertFunc += ConvertCounters;
                finishFunc += FinishCounters;
            }
            else
            {
                if (readType.Value.HasFlag(ReadType.Abonents))
                {
                    Abonents = new List<CNV_ABONENT>();
                    convertFunc += ConvertAbonent;
                    finishFunc += FinishAbonent;
                }
                if (readType.Value.HasFlag(ReadType.CChars))
                {
                    CChars = new List<CNV_CHAR>();
                    convertFunc += ConvertChars;
                    finishFunc += FinishChars;
                }
                if (readType.Value.HasFlag(ReadType.Counters))
                {
                    Counters = new List<CNV_COUNTER>();
                    convertFunc += ConvertCounters;
                    finishFunc += FinishCounters;
                }
            }

            for (int i = 0; i < _excelData.Length; i++)
            {
                var data = _excelData[i];
                convertFunc(data);
                _lastUniqId = data.UniqId;
            }
            finishFunc();
        }

        private static readonly Regex DigitPrefixRegex = new Regex(@"^(\d+)([^\d]+.*)?");
        private static readonly Regex FioRegex = new Regex(@"([а-я]+)[^а-я]+([а-я]+)[^а-я]*([а-я]*)[^а-я]*", RegexOptions.IgnoreCase);
        private void ConvertAbonent(ExcelData excelData)
        {
            if (excelData.FIO != null && excelData.FIO.ToLower() == "полив") return;
            var abonent = new CNV_ABONENT
            {
                LSHET = Consts.GetLs(excelData.UniqId),
                RAYONKOD = 1,
                RAYONNAME = "Рязанский район",
                DUCD = 2,
                DUNAME = "МКП \"ЖКХ Рязанское\"",
                TOWNSNAME = _listInfo.TownName,
                ULICANAME = _listInfo.StreetName,
                ISDELETED = 0,
            };

            if (excelData.HouseNumber != null)
            {
                var match = DigitPrefixRegex.Match(excelData.HouseNumber);
                if (!match.Success) abonent.HOUSEPOSTFIX = excelData.HouseNumber;
                else
                {
                    abonent.HOUSENO = match.Groups[1].Value;
                    abonent.HOUSEPOSTFIX = match.Groups[2].Value;
                    if (abonent.HOUSEPOSTFIX.Length > 10) abonent.HOUSEPOSTFIX = abonent.HOUSEPOSTFIX.Substring(0, 10);
                }
            }
            if (excelData.FlatNumber != null)
            {
                var match = DigitPrefixRegex.Match(excelData.FlatNumber);
                if (!match.Success) abonent.FLATPOSTFIX = excelData.FlatNumber;
                else
                {
                    abonent.FLATNO = Convert.ToInt32(match.Groups[1].Value);
                    abonent.FLATPOSTFIX = match.Groups[2].Value;
                    if (abonent.FLATPOSTFIX.Length > 10) abonent.FLATPOSTFIX = abonent.FLATPOSTFIX.Substring(0, 10);
                }
            }
            if (excelData.FIO != null)
            {
                var match = FioRegex.Match(excelData.FIO);
                if (!match.Success) abonent.F = excelData.FIO;
                else
                {
                    var fioGroups = match.Groups;
                    abonent.F = fioGroups[1].Value;
                    abonent.I = fioGroups.Count > 2 ? fioGroups[2].Value : null;
                    abonent.O = fioGroups.Count > 3 ? fioGroups[3].Value : null;
                }
            }
            Abonents.Add(abonent);
        }

        private void FinishAbonent()
        {
            AbonentRecordUtils.SetUniqueTownskod(Abonents, 0);
            AbonentRecordUtils.SetUniqueUlicakod(Abonents, 0);
            AbonentRecordUtils.SetUniqueHouseCd(Abonents, 0);
        }

        private static readonly Regex LivingRegex = new Regex(@"(\d) *(\(\d\))");
        public void ConvertChars(ExcelData excelData)
        {
            if (excelData.PeopleCount == null) return;
            if (excelData.FIO != null && excelData.FIO.ToLower() == "полив")
            {
                excelData.PeopleCount = excelData.PeopleCount.Replace("сот", "");
                int sot;
                if (!Int32.TryParse(excelData.PeopleCount, out sot))
                    throw new Exception(String.Format("Некорреткная качественная характеристика полива {0}. {1} {2}",
                        excelData.PeopleCount, _fileName, _listInfo));
                if (!_lastUniqId.HasValue)
                    throw new Exception(
                        String.Format("Обрабатывается характеристика полива {0}, но нет предыдущего лс. {1} {2}",
                            excelData.UniqId, _fileName, _listInfo.ListName));
                CChars.Add(new CNV_CHAR
                {
                    LSHET = Consts.GetLs(_lastUniqId.Value),
                    CHARCD = 101006,
                    CHARNAME = "Число соток",
                    VALUE_ = sot,
                    DATE_ = new DateTime(2106, 09, 01)
                });
            }
            else
            {
                int cchar;
                if (Int32.TryParse(excelData.PeopleCount, out cchar))
                {
                    CChars.Add(new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(excelData.UniqId),
                        CHARCD = 1,
                        CHARNAME = "Число проживающих",
                        VALUE_ = cchar,
                        DATE_ = new DateTime(2106, 09, 01)
                    });
                }
                else
                {
                    var match = LivingRegex.Match(excelData.PeopleCount);
                    if (match.Success)
                    {
                        CChars.Add(new CNV_CHAR
                        {
                            LSHET = Consts.GetLs(excelData.UniqId),
                            CHARCD = 1,
                            CHARNAME = "Число проживающих",
                            VALUE_ = Int32.Parse(match.Groups[1].Value),
                            DATE_ = new DateTime(2106, 09, 01)
                        });
                        CChars.Add(new CNV_CHAR
                        {
                            LSHET = Consts.GetLs(excelData.UniqId),
                            CHARCD = 30001,
                            CHARNAME = "Число прописаных",
                            VALUE_ = Int32.Parse(match.Groups[2].Value),
                            DATE_ = new DateTime(2106, 09, 01)
                        });
                    }
                    else if (excelData.PeopleCount.ToLower() == "полив") return;
                    else
                        throw new Exception(String.Format("Некорреткная качественная характеристика {0}. {1} {2}",
                            excelData.PeopleCount, _fileName, _listInfo));
                }
            }
        }

        public void FinishChars()
        {
            CChars = CharsRecordUtils.ThinOutList(CChars);
        }

        public void ConvertCounters(ExcelData excelData)
        {
            if (excelData.Norm != null) return;
            if ((excelData.FIO != null && excelData.FIO.ToLower() == "полив"))
            {
                if (!_lastUniqId.HasValue)
                    throw new Exception(
                        String.Format("Обрабатывается характеристика полива {0}, но нет предыдущего лс. {1} {2}",
                            excelData.UniqId, _fileName, _listInfo.ListName));
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(_lastUniqId.Value),
                    COUNTERID = excelData.UniqId.ToString(),
                    SETUPDATE = new DateTime(2016,09,01),
                    CNTTYPE = 2,
                    CNTNAME = "Счетчик на полив" // убрать
                });
            }
            else if (excelData.PeopleCount != null && excelData.PeopleCount.ToLower() == "полив")
            {
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(excelData.UniqId),
                    COUNTERID = excelData.UniqId.ToString(),
                    SETUPDATE = new DateTime(2016, 09, 01),
                    CNTTYPE = 2,
                    CNTNAME = "Счетчик на полив" // убрать
                });
            }
            else
            {
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = Consts.GetLs(excelData.UniqId),
                    COUNTERID = excelData.UniqId.ToString(),
                    SETUPDATE = new DateTime(2016, 09, 01),
                    CNTTYPE = 1,
                    CNTNAME = "Счетчик на человека" // убрать
                });
            }
        }

        private void FinishCounters()
        {
            
        }

        [Flags]
        public enum ReadType
        {
            Abonents,
            CChars,
            LChars,
            Counters
        }
    }

    public class ExcelListInfo
    {
        public string ListName;
        public int StartRow = 6;
        public int EndRow;
        public string TownName;
        public string StreetName;
        public int CanalizationTariff = 7;
        public int WaterTariff = 6;
    }

    public class ConvertVishetravino :ConvertCase
    {
        public ConvertVishetravino()
        {
            ConvertCaseName = "с. Вышетравино";
            Position = 10;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            string fileName = Consts.FormFullFilePath("База Вышетравино.xlsx");
            var lists = new[]
            {
                new ExcelListInfo {ListName = "дом1", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 2", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом4", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом3", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом5", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом6", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом7", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом8", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом9", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 10", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 11", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 12", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 13", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 14", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 15", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 16", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 17 ", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 18", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 19", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "дом 20", TownName = "с. Вышетравино"},
                new ExcelListInfo {ListName = "ул. Полевая", TownName = "с. Вышетравино", StreetName = "ул. Полевая"},
                new ExcelListInfo {ListName = "ул. Овражная", TownName = "с. Вышетравино", StreetName = "ул. Овражная"},
                new ExcelListInfo {ListName = "ул. Бутырки", TownName = "с. Вышетравино", StreetName = "ул. Бутырки"},
                new ExcelListInfo {ListName = "ул. Садовая", TownName = "с. Вышетравино", StreetName = "ул. Садовая"},
                new ExcelListInfo {ListName = "ул.Новая", TownName = "с. Вышетравино", StreetName = "ул.Новая"},
                new ExcelListInfo {ListName = "ул. прудная", TownName = "с. Вышетравино", StreetName = "ул. Прудная"},
                new ExcelListInfo {ListName = "ул. дачная", TownName = "с. Вышетравино", StreetName = "ул. Дачная"},
                new ExcelListInfo {ListName = "ул. Солнечная", TownName = "с. Вышетравино", StreetName = "ул. Солнечная"},
                new ExcelListInfo {ListName = "ул. Солнечная", TownName = "с. Вышетравино", StreetName = "ул. Солнечная", StartRow = 15},
            };

            SetStepsCount(1);
            StepStart(lists.Length);
            for (int i = 0; i < lists.Length; i++)
            {
                var reader = new ExcelListReader(fileName, lists[i], Consts.FormUniqLshetPrefix(Position/10, i + 1));
                reader.Read(ExcelListReader.ReadType.Abonents);
                //string result = "";
                //foreach (var counter in reader.Counters)
                //{
                //    result += String.Format("{0}\t{1}\r\n", counter.LSHET, counter.CNTNAME);
                //}
                Iterate();
            }
            StepFinish();
        }
    }
}
