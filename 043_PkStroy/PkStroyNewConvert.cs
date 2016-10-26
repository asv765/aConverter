using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _043_PkStroy
{
    #region Исходные файлы

    public class ExcelFileInfo
    {
        public string FileName;
        public ExcelListInfo[] ListInfo;
        public int RecodeStartRow;
        public int RecodeEndRow;

        public static ExcelFileInfo[] AllFiles
        {
            get { return new[] { Vishetravino, /*VoenniyGorodok,*/ DmitrievkaLugki, Pushino, Rovnoe, Rubcovo, Dashki2, /*Dadkovo,*/ NovoselkiVishnevka, Lgovo, Stenkino, Dubrovichi, /*VoenniyGorodokNewAbonents*/ }; }
        }

        public static ExcelFileInfo[] MatchingFiles
        {
            get { return new[] {VoenniyGorodok, Dadkovo}; }
        }

        public static ExcelFileInfo[] TestFile
        {
            get { return new[] { Vishetravino }; }
        }

        public static readonly ExcelFileInfo Vishetravino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Вышетравино.xlsx"),
            RecodeStartRow = 7, 
            RecodeEndRow = 11,
            ListInfo = new[]
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
            }
        };

        public static readonly ExcelFileInfo VoenniyGorodok = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База военный городок.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 6,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом 1", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом2", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом 3", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом 4,5 ", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом 4,5 ", TownName = "Дашки военные2", HasContract = true, StartRow = 25},
            }
        };

        public static readonly ExcelFileInfo DmitrievkaLugki = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. Дмитриевка, д. Лужки.xlsx"),
            RecodeStartRow = 12,
            RecodeEndRow = 17,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "д. дмитриевка", TownName = "д. Дмитриевка", HasContract = true},
                new ExcelListInfo {ListName = "д. лужки", TownName = "д. Лужки", HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Pushino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. пущино.xlsx"),
            RecodeStartRow = 18,
            RecodeEndRow = 19,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "Лист1", TownName = "д. Пущино"},
            }
        };

        public static readonly ExcelFileInfo Rovnoe = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. ровное.xlsx"),
            RecodeStartRow = 20,
            RecodeEndRow = 24,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "ул школьная,1", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная,2", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная,3", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная,4", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "ул.школьная,5", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "ул.школьная,6", TownName = "д. Ровное", StreetName = "ул. Школьная" ,HasContract = true},
                new ExcelListInfo {ListName = "час.дома", TownName = "д. Ровное", HasContract = true},
                new ExcelListInfo {ListName = "ул. дубовая роща", TownName = "д. Ровное", StreetName = "ул. Дубовая роща", HasContract = true},
                new ExcelListInfo {ListName = "ул. новоселов", TownName = "д. Ровное", StreetName = "ул. Новоселов", HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Rubcovo = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. рубцово.xlsx"),
            RecodeStartRow = 25,
            RecodeEndRow = 31,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "ул. луговая", TownName = "д. Рубцово", StreetName = "ул. Луговая" ,HasContract = true},
                new ExcelListInfo {ListName = "ул. садовая", TownName = "д. Рубцово", StreetName = "ул. Садовая" ,HasContract = true},
                new ExcelListInfo {ListName = "ул. строителей", TownName = "д. Рубцово", StreetName = "ул. Строителей" ,HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Dashki2 = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дашки2-2.xlsx"),
            RecodeStartRow = 32,
            RecodeEndRow = 38,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом 34", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "дом 35", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "дом 36", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "дом 37", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "дом38", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "част.дома", TownName = "п. Дашки 2"},
                new ExcelListInfo {ListName = "част. дома", TownName = "п. Дашки 2"},
            }
        };  

        public static readonly ExcelFileInfo Dadkovo = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дядьково.xlsx"),
            RecodeStartRow = 39,
            RecodeEndRow = 44,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом№1", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом № 2", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №3", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №4", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №5", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №6", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом № 7", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №8", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом №9", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
                new ExcelListInfo {ListName = "дом № 10", TownName = "Дядьково с", StreetName = "ул. Юбилейная"},
            }
        };

        public static readonly ExcelFileInfo NovoselkiVishnevka = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База новоселки, вишневка.xlsx"),
            RecodeStartRow = 45,
            RecodeEndRow = 51,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "ул. Вишневая", TownName = "п. Новоселки", StreetName = "ул. Вишневая"},
                new ExcelListInfo {ListName = "ул. Зеленая", TownName = "п. Новоселки", StreetName = "ул. Зеленая"},
                new ExcelListInfo {ListName = "ул. Мичурина", TownName = "п. Новоселки", StreetName = "ул. Мичурина"},
                new ExcelListInfo {ListName = "ул. Молодежная дом 3", TownName = "п. Новоселки", StreetName = "ул. Молодежная"},
                new ExcelListInfo {ListName = "ул.Молодежная дом 4", TownName = "п. Новоселки", StreetName = "ул. Молодежная"},
                new ExcelListInfo {ListName = "ул.Молодежная дом№5", TownName = "п. Новоселки", StreetName = "ул. Молодежная"},
                new ExcelListInfo {ListName = "ул. Молодежная дом№6,7", TownName = "п. Новоселки", StreetName = "ул. Молодежная"},
                new ExcelListInfo {ListName = "ул. Молодежная дом№6,7", TownName = "п. Новоселки", StreetName = "ул. Молодежная", StartRow = 36},
                new ExcelListInfo {ListName = "1-й молодежный, березовая", TownName = "п. Новоселки", StreetName = "ул. 1-й Молодежный проезд"},
                new ExcelListInfo {ListName = "1-й молодежный, березовая", TownName = "п. Новоселки", StreetName = "ул. 1-й Молодежный проезд", StartRow = 18},
                new ExcelListInfo {ListName = "ул. Нефтезаводская д.1", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская"},
                new ExcelListInfo {ListName = "ул. Нефтезаводская д.2,4", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская"},
                new ExcelListInfo {ListName = "ул. Нефтезаводская д.2,4", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская", StartRow = 17},
                new ExcelListInfo {ListName = "ул.Нефтезаводская д.3,5", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская"},
                new ExcelListInfo {ListName = "ул.Нефтезаводская д.3,5", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская", StartRow = 18},
                new ExcelListInfo {ListName = "ул. Новая ,1.. 3", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo {ListName = "ул. Новая ,1.. 3", TownName = "п. Новоселки", StreetName = "ул. Новая", StartRow = 19},
                new ExcelListInfo {ListName = "ул.Новая д.3а", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo {ListName = "Новая, 4", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo {ListName = "ул. нефтезаводская ч. дома ", TownName = "п. Новоселки", StreetName = "ул. Нефтезаводская", HasContract = true},
                new ExcelListInfo {ListName = "ул. Полевая", TownName = "п. Новоселки", StreetName = "ул. Полевая"},
                new ExcelListInfo {ListName = "ул. Приокская", TownName = "п. Новоселки", StreetName = "ул. Приокская"},
                new ExcelListInfo {ListName = "ул. Садовая", TownName = "п. Новоселки", StreetName = "ул. Садовая"},
                new ExcelListInfo {ListName = "ул.Родниковая", TownName = "п. Новоселки", StreetName = "ул. Родниковая"},
                new ExcelListInfo {ListName = "ул. Юбилейная", TownName = "п. Новоселки", StreetName = "ул. Юбилейная", HasContract = true},
                new ExcelListInfo {ListName = "д.Вишневка", TownName = "д. Вишневка"},
            }
        };

        public static readonly ExcelFileInfo Lgovo = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База с. Льгово.xlsx"),
            RecodeStartRow = 52,
            RecodeEndRow = 57,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "ул. 3-я линия", TownName = "с. Льгово", StreetName = "ул. 3-я Линия", HasContract = true},
                new ExcelListInfo {ListName = "ул. 60 лет СССР", TownName = "с. Льгово", StreetName = "ул. 60 лет СССР", HasContract = true},
                new ExcelListInfo {ListName = "ул. колхозная", TownName = "с. Льгово", StreetName = "ул. Колхозная", HasContract = true},
                new ExcelListInfo {ListName = "ул. ленпоселок", TownName = "с. Льгово", StreetName = "ул. Ленпоселок", HasContract = true},
                new ExcelListInfo {ListName = "ул. новая", TownName = "с. Льгово", StreetName = "ул. Новая", HasContract = true},
                new ExcelListInfo {ListName = "ул. полевая", TownName = "с. Льгово", StreetName = "ул. Полевая", HasContract = true},
                new ExcelListInfo {ListName = "ул. полевая 2", TownName = "с. Льгово", StreetName = "ул. Полевая", HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная", TownName = "с. Льгово", StreetName = "ул. Школьная", HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная 2", TownName = "с. Льгово", StreetName = "ул. Школьная", HasContract = true},
                new ExcelListInfo {ListName = "ул. школьная 2", TownName = "с. Льгово", StreetName = "ул. Школьная", HasContract = true, StartRow = 87},
            }
        };

        public static readonly ExcelFileInfo Stenkino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База с. стенькино.xlsx"),
            RecodeStartRow = 58,
            RecodeEndRow = 60,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "Лист1", TownName = "п. Стенькино", HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Dubrovichi = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дубровичи 07.10.2016.xlsx"),
            RecodeStartRow = 61,
            RecodeEndRow = 67,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом 1", TownName = "Дубровичи", HasContract = true, PrimCol = 13},
            }
        };

        public static readonly ExcelFileInfo VoenniyGorodokNewAbonents = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База военный городок.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 6,
            ListInfo = new[]
            {
                new ExcelListInfo{ListName = "newAbonents", HasContract = true, TownName = "Дашки военные2", StreetName = "1"},
            }
        };

        public static readonly ExcelFileInfo RecodeTable = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("Таблица перекодировкиv2.2.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 67,
            ListInfo = new[]
            {
                new ExcelListInfo{ListName = "Лист1"}, 
            }
        };
    }

    #endregion

    #region Базовые классы

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
            return String.Format("962{0:D5}", ls);
        }
    }

    public class ExcelData
    {
        public string MatchedLshet;
        public int UniqId;
        public string HouseNumber;
        public string FlatNumber;
        public string FIO;
        public string PeopleCount;
        public string Prim;
        public string Contract;
        public string NoCntVO;
        public string NoCntVS;
        public string CntVO;
        public string CntVS;

        private readonly bool _hasContract;

        public ExcelData(DataRow dr, ExcelListInfo listInfo)
        {
            _hasContract = listInfo.HasContract;
            if (_hasContract) Contract = String.IsNullOrWhiteSpace(dr[2].ToString()) ? null : dr[2].ToString().Trim();
            HouseNumber = String.IsNullOrWhiteSpace(dr[0].ToString()) ? null : dr[0].ToString().Trim();
            if (HouseNumber != null) HouseNumber = HouseNumber.Replace("л", "");
            FlatNumber = String.IsNullOrWhiteSpace(dr[1].ToString()) ? null : dr[1].ToString().Trim();
            if (FlatNumber != null) FlatNumber = FlatNumber.Replace("л", "");
            FIO = String.IsNullOrWhiteSpace(dr[Ind(2)].ToString()) ? null : dr[Ind(2)].ToString().Trim();
            PeopleCount = String.IsNullOrWhiteSpace(dr[Ind(3)].ToString()) ? null : dr[Ind(3)].ToString().Trim();
            NoCntVO = String.IsNullOrWhiteSpace(dr[Ind(4)].ToString()) ? null : dr[Ind(4)].ToString().Trim();
            NoCntVS = String.IsNullOrWhiteSpace(dr[Ind(5)].ToString()) ? null : dr[Ind(5)].ToString().Trim();
            CntVO = String.IsNullOrWhiteSpace(dr[Ind(6)].ToString()) ? null : dr[Ind(6)].ToString().Trim();
            CntVS = String.IsNullOrWhiteSpace(dr[Ind(7)].ToString()) ? null : dr[Ind(7)].ToString().Trim();
            Prim = listInfo.PrimCol.HasValue && !String.IsNullOrWhiteSpace(dr[listInfo.PrimCol.Value-1].ToString())
                ? dr[listInfo.PrimCol.Value-1].ToString().Trim()
                : null;
        }

        private int Ind(int index)
        {
            return _hasContract ? index + 1 : index;
        }
    }

    public class ExcelListReader
    {
        public static int UniqueAbonentId = 0;
        public static List<CNV_ABONENT> Abonents;
        public static List<CNV_CHAR> CChars;
        public static List<CNV_LCHAR> LChars;
        public static List<CNV_COUNTER> Counters;

        private static RecodeTable[] RecodeTableData;

        public readonly ExcelData[] ExcelData;
        private readonly ExcelListInfo _listInfo;
        private readonly ExcelFileInfo _fileInfo;

        private int? _lastUniqId;

        public ExcelListReader(ExcelFileInfo fileInfo, ExcelListInfo listInfo)
        {
            _lastUniqId = null;

            _fileInfo = fileInfo;
            _listInfo = listInfo;

            try
            {
                using (DataTable dt = Utils.ReadExcelFile(fileInfo.FileName, listInfo.ListName))
                {
                    var tempExcelDataList = new List<ExcelData>();
                    int end = listInfo.EndRow == 0 ? dt.Rows.Count : listInfo.EndRow - 1;
                    for (int i = listInfo.StartRow - 2; i < end; i++)
                    {
                        if (dt.Rows[i][0].ToString().ToUpper() == "ИТОГО") break;
                        var excelData = new ExcelData(dt.Rows[i], listInfo) {UniqId = ++UniqueAbonentId};
                        if (excelData.FIO == null && excelData.PeopleCount == null && excelData.NoCntVO == null &&
                            excelData.NoCntVS == null && excelData.CntVO == null && excelData.CntVS == null)
                            continue;
                        tempExcelDataList.Add(excelData);
                    }
                    ExcelData = tempExcelDataList.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(
                    "Произошла ошибка при чтении файла {0} ({1})\r\n{2}\r\nStackTrace:{3}", fileInfo.FileName, listInfo.ListName,
                    ex.Message, ex.ToString()));
            }
        }

        public static void Initialize(ConvertType? convertType = null)
        {
            UniqueAbonentId = 0;
            if (!convertType.HasValue)
            {
                Abonents = new List<CNV_ABONENT>();
                CChars = new List<CNV_CHAR>();
                Counters = new List<CNV_COUNTER>();
                LChars = new List<CNV_LCHAR>();
                ReadRecodeTable();
            }
            else
            {
                if (convertType.Value.HasFlag(ConvertType.Abonents)) Abonents = new List<CNV_ABONENT>();
                if (convertType.Value.HasFlag(ConvertType.CChars)) CChars = new List<CNV_CHAR>();
                if (convertType.Value.HasFlag(ConvertType.Counters)) Counters = new List<CNV_COUNTER>();
                if (convertType.Value.HasFlag(ConvertType.LChars))
                {
                    LChars = new List<CNV_LCHAR>();
                    ReadRecodeTable();
                }
            }
        }

        private delegate void ConvertFunc(ExcelData excelData);
        private delegate void FinishFunc();
        private delegate void SaveFunc(ConvertCase convert);

        private static void ReadRecodeTable()
        {
            var file = ExcelFileInfo.RecodeTable;
            using (DataTable dt = Utils.ReadExcelFile(file.FileName, file.ListInfo[0].ListName))
            {
                var tempTable = new List<RecodeTable>();
                for (int i = file.RecodeStartRow - 2; i < file.RecodeEndRow - 2; i++)
                {
                    tempTable.Add(new RecodeTable(dt.Rows[i]));
                }
                RecodeTableData = tempTable.ToArray();
            }
        }

        public void Read(ConvertType? convertType = null)
        {
            ConvertFunc convertFunc = data => { };
            if (!convertType.HasValue)
            {
                convertFunc += ConvertAbonent;
                convertFunc += ConvertCChars;
                convertFunc += ConvertCounters;
                convertFunc += ConvertLChars;
            }
            else
            {
                if (convertType.Value.HasFlag(ConvertType.Abonents)) convertFunc += ConvertAbonent;
                if (convertType.Value.HasFlag(ConvertType.CChars)) convertFunc += ConvertCChars;
                if (convertType.Value.HasFlag(ConvertType.Counters)) convertFunc += ConvertCounters; 
                if (convertType.Value.HasFlag(ConvertType.LChars)) convertFunc += ConvertLChars;
            }

            try
            {
                for (int i = 0; i < ExcelData.Length; i++)
                {
                    var data = ExcelData[i];
                    convertFunc(data);
                    _lastUniqId = data.UniqId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format("Произошла ошибка при конвертации файла {0} ({1})\r\n{2}\r\nStackTrace:{3}",
                        _fileInfo.FileName, _listInfo.ListName, ex.Message, ex.ToString()));
            }
        }

        public static void FinishRead(ConvertType? convertType = null)
        {
            FinishFunc finishFunc = () => { };
            if (!convertType.HasValue)
            {
                finishFunc += FinishAbonent;
                finishFunc += FinishCChars;
                finishFunc += FinishCounters;
                finishFunc += FinishLChars;
            }
            else
            {
                if (convertType.Value.HasFlag(ConvertType.Abonents)) finishFunc += FinishAbonent;
                if (convertType.Value.HasFlag(ConvertType.CChars)) finishFunc += FinishCChars;
                if (convertType.Value.HasFlag(ConvertType.Counters)) finishFunc += FinishCounters;
                if (convertType.Value.HasFlag(ConvertType.LChars)) finishFunc += FinishLChars;
            }

            try
            {
                finishFunc();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format("Произошла ошибка при завершении конвертации \r\n{0}\r\nStackTrace:{1}",
                        ex.Message, ex.ToString()));
            }
        }

        public static void Save(ConvertCase convert, ConvertType? convertType = null)
        {
            SaveFunc saveFunc = cnv => { };
            if (!convertType.HasValue)
            {
                saveFunc += SaveAbonent;
                saveFunc += SaveCChars;
                saveFunc += SaveCounters;
                saveFunc += SaveLChars;
            }
            else
            {
                if (convertType.Value.HasFlag(ConvertType.Abonents)) saveFunc += SaveAbonent;
                if (convertType.Value.HasFlag(ConvertType.CChars)) saveFunc += SaveCChars;
                if (convertType.Value.HasFlag(ConvertType.Counters)) saveFunc += SaveCounters;
                if (convertType.Value.HasFlag(ConvertType.LChars)) saveFunc += SaveLChars;
            }

            try
            {
                saveFunc(convert);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format("Произошла ошибка при сохранении данных\r\n{0}\r\nStackTrace:{1}",
                        ex.Message, ex.ToString()));
            }
        }

        private static readonly Regex DigitPrefixRegex = new Regex(@"^(\d+)([^\d]+.*)?");
        private static readonly Regex FioRegex = new Regex(@"([а-я]+)[^а-я]+([а-я]+)[^а-я]*([а-я]*)[^а-я]*", RegexOptions.IgnoreCase);
        private void ConvertAbonent(ExcelData excelData)
        {
            if (excelData.FIO != null && excelData.FIO.ToLower() == "полив") return;
            var abonent = new CNV_ABONENT
            {
                LSHET = Consts.GetLs(excelData.UniqId),
                RAYONKOD = 2,
                RAYONNAME = "Рязанский район",
                DUCD = 2,
                DUNAME = "МКП \"ЖКХ Рязанское\"",
                TOWNSNAME = _listInfo.TownName,
                ULICANAME = _listInfo.StreetName,
                ISDELETED = 0,
                PRIM_ = excelData.Prim
            };

            if (abonent.TOWNSNAME == "Дашки военные2")
            {
                abonent.TOWNSKOD = 3;
                abonent.ULICAKOD = 5;
                switch (excelData.HouseNumber)
                {
                    case "2":
                        abonent.HOUSECD = 27;
                        break;
                    case "3":
                        abonent.HOUSECD = 28;
                        break;
                    case "5":
                        abonent.HOUSECD = 30;
                        break;
                    default:
                        throw new Exception("При сопоставлении дашков военных не расшифрован дом " + excelData.HouseNumber);
                }
            }

            if (excelData.HouseNumber != null)
            {
                if (excelData.HouseNumber[0] == '-') excelData.HouseNumber = excelData.HouseNumber.Remove(0, 1);
                var match = DigitPrefixRegex.Match(excelData.HouseNumber);
                if (!match.Success) abonent.HOUSEPOSTFIX = excelData.HouseNumber;
                else
                {
                    abonent.HOUSENO = match.Groups[1].Value;
                    abonent.HOUSEPOSTFIX = String.IsNullOrWhiteSpace(match.Groups[2].Value) ? null : match.Groups[2].Value;
                    if (abonent.HOUSEPOSTFIX != null && abonent.HOUSEPOSTFIX.Length > 10) abonent.HOUSEPOSTFIX = abonent.HOUSEPOSTFIX.Substring(0, 9);
                }
            }
            if (excelData.FlatNumber != null)
            {
                if (excelData.FlatNumber[0] == '-') excelData.FlatNumber = excelData.FlatNumber.Remove(0, 1);
                var match = DigitPrefixRegex.Match(excelData.FlatNumber);
                if (!match.Success) abonent.FLATPOSTFIX = excelData.FlatNumber;
                else
                {
                    abonent.FLATNO = System.Convert.ToInt32(match.Groups[1].Value);
                    abonent.FLATPOSTFIX = String.IsNullOrWhiteSpace(match.Groups[2].Value) ? null : match.Groups[2].Value;
                    if (abonent.FLATPOSTFIX != null && abonent.FLATPOSTFIX.Length > 10) abonent.FLATPOSTFIX = abonent.FLATPOSTFIX.Substring(0, 9);
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

        private static void FinishAbonent()
        {
            AbonentRecordUtils.SetUniqueTownskod(Abonents, 10);
            AbonentRecordUtils.SetUniqueUlicakod(Abonents, 30);
            AbonentRecordUtils.SetUniqueHouseCd(Abonents, 200);
        }

        private static void SaveAbonent(ConvertCase convert)
        {
            convert.SaveList(Abonents, Consts.InsertRecordCount, false);
        }

        private static readonly Regex LivingRegex = new Regex(@"(\d) *(\((\d)\))");
        private void ConvertCChars(ExcelData excelData)
        {
            if (excelData.PeopleCount == null) return;
            if (excelData.FIO != null && excelData.FIO.ToLower() == "полив")
            {
                excelData.PeopleCount = excelData.PeopleCount.Replace("сот", "");
                decimal sot;
                if (!decimal.TryParse(excelData.PeopleCount, out sot))
                    throw new Exception(String.Format("Некорреткная качественная характеристика полива {0}. {1} {2}",
                        excelData.PeopleCount, _fileInfo.FileName, _listInfo));
                if (!_lastUniqId.HasValue)
                    throw new Exception(
                        String.Format("Обрабатывается характеристика полива {0}, но нет предыдущего лс. {1} {2}",
                            excelData.UniqId, _fileInfo.FileName, _listInfo.ListName));
                CChars.Add(new CNV_CHAR
                {
                    LSHET = Consts.GetLs(_lastUniqId.Value),
                    CHARCD = 20,
                    CHARNAME = "Количество соток площади",
                    VALUE_ = sot,
                    DATE_ = new DateTime(2016, 10, 01)
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
                        DATE_ = new DateTime(2016, 10, 01)
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
                            DATE_ = new DateTime(2016, 10, 01)
                        });
                        CChars.Add(new CNV_CHAR
                        {
                            LSHET = Consts.GetLs(excelData.UniqId),
                            CHARCD = 3,
                            CHARNAME = "Число прописанных",
                            VALUE_ = Int32.Parse(match.Groups[3].Value),
                            DATE_ = new DateTime(2016, 10, 01)
                        });
                    }
                    else if (excelData.PeopleCount.ToLower() == "полив") return;
                    else
                        throw new Exception(String.Format("Некорреткная качественная характеристика {0}. {1} {2}",
                            excelData.PeopleCount, _fileInfo.FileName, _listInfo));
                }
            }
        }

        private static void FinishCChars()
        {
            CChars = CharsRecordUtils.ThinOutList(CChars);
        }

        private static void SaveCChars(ConvertCase convert)
        {
            convert.SaveList(CChars, Consts.InsertRecordCount, false);
        }

        private static int _couunterCd = 0;
        public void ConvertCounters(ExcelData excelData)
        {
            if (excelData.NoCntVO != null || excelData.NoCntVS != null) return;
            if ((excelData.FIO != null && excelData.FIO.ToLower() == "полив"))
            {
                if (!_lastUniqId.HasValue)
                    throw new Exception(
                        String.Format("Обрабатывается характеристика полива {0}, но нет предыдущего лс. {1} {2}",
                            excelData.UniqId, _fileInfo.FileName, _listInfo.ListName));
                string lshet = String.IsNullOrWhiteSpace(excelData.MatchedLshet)
                    ? Consts.GetLs(_lastUniqId.Value)
                    : excelData.MatchedLshet;
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = lshet,
                    COUNTERID = (++_couunterCd).ToString(),
                    SETUPDATE = new DateTime(2016,10,01),
                    CNTTYPE = 3179,
                    CNTNAME = "Счетчик на полив" // убрать
                });
            }
            else if (excelData.PeopleCount != null && excelData.PeopleCount.ToLower() == "полив")
            {
                string lshet = String.IsNullOrWhiteSpace(excelData.MatchedLshet)
                    ? Consts.GetLs(excelData.UniqId)
                    : excelData.MatchedLshet;
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = lshet,
                    COUNTERID = (++_couunterCd).ToString(),
                    SETUPDATE = new DateTime(2016, 10, 01),
                    CNTTYPE = 3179,
                    CNTNAME = "Счетчик на полив" // убрать
                });
            }
            else
            {
                string lshet = String.IsNullOrWhiteSpace(excelData.MatchedLshet)
                    ? Consts.GetLs(excelData.UniqId)
                    : excelData.MatchedLshet;
                Counters.Add(new CNV_COUNTER
                {
                    LSHET = lshet,
                    COUNTERID = (++_couunterCd).ToString(),
                    SETUPDATE = new DateTime(2016, 10, 01),
                    CNTTYPE = 1,
                    CNTNAME = "Счетчик на человека" // убрать
                });
            }
        }

        private static void FinishCounters()
        {
            
        }

        private static void SaveCounters(ConvertCase convert)
        {
            convert.SaveList(Counters, Consts.InsertRecordCount, false);
        }

        private void ConvertLChars(ExcelData excelData)
        {
            bool poliv = false;
            int uniqId = excelData.UniqId;
            if ((excelData.FIO != null && excelData.FIO.ToLower() == "полив"))
            {
                if (!_lastUniqId.HasValue)
                    throw new Exception(
                        String.Format("Обрабатывается характеристика полива {0}, но нет предыдущего лс. {1} {2}",
                            excelData.UniqId, _fileInfo.FileName, _listInfo.ListName));
                poliv = true;
                uniqId = (int)_lastUniqId;
            }
            if (String.IsNullOrWhiteSpace(excelData.MatchedLshet))
                LChars.Add(new CNV_LCHAR
                {
                    LSHET = String.IsNullOrWhiteSpace(excelData.MatchedLshet) ? Consts.GetLs(uniqId) : excelData.MatchedLshet,
                    DATE_ = new DateTime(2016, 10, 01),
                    LCHARCD = 35,
                    LCHARNAME = "Поставщик",
                    VALUE_ = 0,
                    VALUEDESC = "МПК ЖКХ Рязанское"
                });
            for (int i = 0; i < RecodeTableData.Length; i++)
            {
                if (!(_fileInfo.RecodeStartRow <= i+2 && i+2 <= _fileInfo.RecodeEndRow)) continue;
                var recode = RecodeTableData[i];
                if (poliv != recode.Poliv) continue;
                if (recode.EmptyWithoutCounter)
                {
                    if (excelData.NoCntVO != null || excelData.NoCntVS != null) continue;
                }
                double checkValue;
                switch (recode.CounterType)
                {
                    case RecodeTable.CounterRecode.Vs:
                        checkValue = Double.Parse(excelData.CntVS ?? "-1");
                        break;
                    case RecodeTable.CounterRecode.Vo:
                        checkValue = Double.Parse(excelData.CntVO ?? "-1");
                        break;
                    case RecodeTable.CounterRecode.NoVs:
                        checkValue = Double.Parse(excelData.NoCntVS ?? "-1");
                        break;
                    case RecodeTable.CounterRecode.NoVo:
                        checkValue = Double.Parse(excelData.NoCntVO ?? "-1");
                        break;
                    default:
                        throw new Exception("Неизвестный тип значения перекодировки " + recode.CounterType);
                }
                if (Math.Abs(checkValue - recode.Value) > 0.01) continue;
                
                LChars.Add(new CNV_LCHAR
                {
                    LSHET = String.IsNullOrWhiteSpace(excelData.MatchedLshet) ?Consts.GetLs(uniqId) : excelData.MatchedLshet,
                    DATE_ = new DateTime(2016, 10, 01),
                    LCHARCD = recode.LcharCd,
                    LCHARNAME = recode.LcharName,
                    VALUE_ = recode.LcharValue,
                    VALUEDESC = recode.LcharValueName
                });
            }
        }

        private static void FinishLChars()
        {
            LChars = LcharsRecordUtils.ThinOutList(LChars);
        }

        private static void SaveLChars(ConvertCase convert)
        {
            convert.SaveList(LChars, Consts.InsertRecordCount, false);
        }

        public static void MatchAbonents(ConvertCase convert, ConvertType convertType)
        {
            Initialize(convertType);
            var files = ExcelFileInfo.MatchingFiles;
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < files.Length; i++)
                {
                    var fileInfo = files[i];
                    for (int j = 0; j < fileInfo.ListInfo.Length; j++)
                    {
                        var listInfo = fileInfo.ListInfo[j];
                        var reader = new ExcelListReader(fileInfo, listInfo);
                        for (int k = 0; k < reader.ExcelData.Length; k++)
                        {
                            var data = reader.ExcelData[k];
                            int townCd;
                            int streetCd;
                            switch (listInfo.TownName)
                            {
                                case "Дашки военные2":
                                    townCd = 3;
                                    streetCd = 5;
                                    break;
                                case "Дядьково с":
                                    townCd = 6;
                                    streetCd = 17;
                                    break;
                                default:
                                    throw new Exception("Необработанный населенный пункт для сопоставления " + listInfo.TownName);
                            }

                            string query = String.Format(@"select ab.lshet from abonents ab
                                            inner join houses h on h.housecd = ab.housecd and
                                                                (h.punktcd = '{0}' and
                                                                h.streetcd = '{1}' and
                                                                h.houseno = '{2}' and
                                                                ab.flatno = '{3}')",
                                townCd, streetCd, data.HouseNumber, data.FlatNumber);
                            var result = context.ExecuteQuery<string>(query);
                            if (result.Count != 1)
                                throw new Exception("Запрос на сопоставление вернул 0 или более 1 результатов\r\n" +
                                                    query);
                            data.MatchedLshet = result[0];
                        }
                        reader.Read(convertType);
                    }
                }
            }

            FinishRead(convertType);

            Save(convert, convertType);
        }

        [Flags]
        public enum ConvertType
        {
            Abonents = 1,
            CChars = 2,
            LChars = 4,
            Counters = 8
        }

        public class RecodeTable
        {
            public CounterRecode CounterType;
            public double Value;
            public bool EmptyWithoutCounter;
            public bool Poliv;
            public int LcharCd;
            public string LcharName;
            public int LcharValue;
            public string LcharValueName;

            public RecodeTable(DataRow dr)
            {
                switch (dr[1].ToString().Trim().ToLower())
                {
                    case "по счетчику вс":
                        CounterType = CounterRecode.Vs;
                        break;
                    case "по счетчику во":
                        CounterType = CounterRecode.Vo;
                        break;
                    case "без счетчика вс":
                        CounterType = CounterRecode.NoVs;
                        break;
                    case "без счетчика во":
                        CounterType = CounterRecode.NoVo;
                        break;
                    default:
                        throw new Exception("Неизвестный тип в таблице перекодировке " + dr[1]);
                }
                Value = Double.Parse(dr[2].ToString());
                EmptyWithoutCounter = dr[4].ToString().ToLower() == "\"пусто\"";
                Poliv = dr[5].ToString().Trim().ToLower() == "полив";
                LcharCd = Int32.Parse(dr[6].ToString());
                LcharName = dr[7].ToString().Trim();
                LcharValue = Int32.Parse(dr[8].ToString());
                LcharValueName = dr[9].ToString().Trim();
            }


            public enum CounterRecode
            {
                Vs,
                Vo,
                NoVs,
                NoVo
            }
        }
    }

    public class ExcelListInfo
    {
        public string ListName;
        public int StartRow = 6;
        public int EndRow;
        public string TownName;
        public string StreetName = "";
        public int CanalizationTariff = 7;
        public int WaterTariff = 6;
        public bool HasContract = false;
        public int? PrimCol;
    }

    #endregion

    #region Конвертация

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

    public class ConvertAbonents :ConvertCase
    {
        public ConvertAbonents()
        {
            ConvertCaseName = "ABONENTS - информация об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            var convertType = ExcelListReader.ConvertType.Abonents;
            ExcelListReader.Initialize(convertType);
            SetStepsCount(3);
            var files = ExcelFileInfo.AllFiles;
            //var files = ExcelFileInfo.TestFile;
            StepStart(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                var fileInfo = files[i];
                for (int j = 0; j < fileInfo.ListInfo.Length; j++)
                {
                    var reader = new ExcelListReader(fileInfo, fileInfo.ListInfo[j]);
                    reader.Read(ExcelListReader.ConvertType.Abonents);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            ExcelListReader.FinishRead(convertType);
            StepFinish();

            StepStart(1);
            ExcelListReader.Save(this, convertType);
            StepFinish();
        }
    }

    public class ConvertCchars : ConvertCase
    {
        public ConvertCchars()
        {
            ConvertCaseName = "CCHARS - Качественные характеристики абонентов";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$CHARS");
            var convertType = ExcelListReader.ConvertType.CChars;
            ExcelListReader.Initialize(convertType);
            SetStepsCount(3);
            var files = ExcelFileInfo.AllFiles;
            //var files = ExcelFileInfo.TestFile;
            StepStart(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                var fileInfo = files[i];
                for (int j = 0; j < fileInfo.ListInfo.Length; j++)
                {
                    var reader = new ExcelListReader(fileInfo, fileInfo.ListInfo[j]);
                    reader.Read(convertType);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            ExcelListReader.FinishRead(convertType);
            StepFinish();

            StepStart(1);
            ExcelListReader.Save(this, convertType);
            StepFinish();
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - Счетчики абонентов";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            var convertType = ExcelListReader.ConvertType.Counters;
            ExcelListReader.Initialize(convertType);
            SetStepsCount(3);
            var files = ExcelFileInfo.AllFiles;
            //var files = ExcelFileInfo.TestFile;
            StepStart(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                var fileInfo = files[i];
                for (int j = 0; j < fileInfo.ListInfo.Length; j++)
                {
                    var reader = new ExcelListReader(fileInfo, fileInfo.ListInfo[j]);
                    reader.Read(convertType);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            ExcelListReader.FinishRead(convertType);
            StepFinish();

            StepStart(1);
            ExcelListReader.Save(this, convertType);
            StepFinish();
        }
    }

    public class ConvertLchars : ConvertCase
    {
        public ConvertLchars()
        {
            ConvertCaseName = "LCHARS - количественные характеристики абонентов.";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            var convertType = ExcelListReader.ConvertType.LChars;
            ExcelListReader.Initialize(convertType);
            SetStepsCount(3);
            var files = ExcelFileInfo.AllFiles;
            //var files = ExcelFileInfo.TestFile;
            StepStart(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                var fileInfo = files[i];
                for (int j = 0; j < fileInfo.ListInfo.Length; j++)
                {
                    var reader = new ExcelListReader(fileInfo, fileInfo.ListInfo[j]);
                    reader.Read(convertType);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            ExcelListReader.FinishRead(convertType);
            StepFinish();

            StepStart(1);
            ExcelListReader.Save(this, convertType);
            StepFinish();
        }
    }

    public class ConvertMatchingLchars : ConvertCase
    {
        public ConvertMatchingLchars()
        {
            ConvertCaseName = "LCHARS - количественные характеристики абонентов. Сопоставление";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            ExcelListReader.MatchAbonents(this, ExcelListReader.ConvertType.LChars);
        }
    }

    public class ConvertMatchingCounters : ConvertCase
    {
        public ConvertMatchingCounters()
        {
            ConvertCaseName = "COUNTERS - счетчики абонентов. Сопоставление";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);
            ExcelListReader.MatchAbonents(this, ExcelListReader.ConvertType.Counters);
        }
    }

    #endregion

    #region Перенос в целевые таблицы

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
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0", "0" });
            Iterate();
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


    #endregion
}
