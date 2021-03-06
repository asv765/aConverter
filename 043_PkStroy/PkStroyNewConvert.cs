﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

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
            get
            {
                return new[]
                {
                    Vishetravino, /*VoenniyGorodok,*/ DmitrievkaLugki, Pushino, Rovnoe, Rubcovo, Dashki2, /*Dadkovo,*/
                    NovoselkiVishnevka, Lgovo, Stenkino, Dubrovichi, /*VoenniyGorodokNewAbonents*/
                };
            }
        }

        public static ExcelFileInfo[] MatchingFiles
        {
            get { return new[] {VoenniyGorodok, Dadkovo}; }
        }

        public static ExcelFileInfo[] TestFile
        {
            get { return new[] {Vishetravino}; }
        }
        
        public static readonly ExcelFileInfo NewDubrovichi = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дубровичи 01.11.2016.xlsx"),
            ListInfo = new []
            {
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Школьная", ListName = "Школьная"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Совхозная", ListName = "Совхозная"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Советской армии", ListName = "Советской армии"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Советская", ListName = "Советская"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Свобода", ListName = "Свобода"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Садовая", ListName = "Садовая"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Революции", ListName = "Революции"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Радищева", ListName = "Радищева"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Новая", ListName = "Новая"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Молодежная", ListName = "Молодежная"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Кооперативная", ListName = "Кооперативная"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Комсомольская", ListName = "Комсомольская"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Колхозная", ListName = "Колхозная"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Есенина", ListName = "Есенина"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. 1 мая", ListName = "1 мая"}, 
                new ExcelListInfo{HasContract = true, TownName = "Дубровичи", StreetName = "ул. Городцова", ListName = "Городцова"}, 
            }
        };

        public static readonly ExcelFileInfo RecodeTable1C = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("Таблица перекодировкиv1С1.1 (1).xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 18,
            ListInfo = new[]
            {
                new ExcelListInfo{ListName = "Лист1"}, 
            }
        };

        public static readonly ExcelFileInfo Vishetravino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Вышетравино.xlsx"),
            RecodeStartRow = 10,
            RecodeEndRow = 19,
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
                new ExcelListInfo
                {
                    ListName = "ул. Солнечная",
                    TownName = "с. Вышетравино",
                    StreetName = "ул. Солнечная"
                },
                new ExcelListInfo
                {
                    ListName = "ул. Солнечная",
                    TownName = "с. Вышетравино",
                    StreetName = "ул. Солнечная",
                    StartRow = 15
                },
            }
        };

        public static readonly ExcelFileInfo VoenniyGorodok = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База военный городок.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 9,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом 1", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом2", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом 3", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo {ListName = "дом 4,5 ", TownName = "Дашки военные2", HasContract = true},
                new ExcelListInfo
                {
                    ListName = "дом 4,5 ",
                    TownName = "Дашки военные2",
                    HasContract = true,
                    StartRow = 25
                },
            }
        };

        public static readonly ExcelFileInfo DmitrievkaLugki = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. Дмитриевка, д. Лужки.xlsx"),
            RecodeStartRow = 20,
            RecodeEndRow = 25,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "д. дмитриевка", TownName = "д. Дмитриевка", HasContract = true},
                new ExcelListInfo {ListName = "д. лужки", TownName = "д. Лужки", HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Pushino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. пущино.xlsx"),
            RecodeStartRow = 26,
            RecodeEndRow = 27,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "Лист1", TownName = "д. Пущино"},
            }
        };

        public static readonly ExcelFileInfo Rovnoe = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. ровное.xlsx"),
            RecodeStartRow = 28,
            RecodeEndRow = 38,
            ListInfo = new[]
            {
                new ExcelListInfo
                {
                    ListName = "ул школьная,1",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная,2",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная,3",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная,4",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул.школьная,5",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул.школьная,6",
                    TownName = "д. Ровное",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo {ListName = "час.дома", TownName = "д. Ровное", HasContract = true},
                new ExcelListInfo
                {
                    ListName = "ул. дубовая роща",
                    TownName = "д. Ровное",
                    StreetName = "ул. Дубовая роща",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. новоселов",
                    TownName = "д. Ровное",
                    StreetName = "ул. Новоселов",
                    HasContract = true
                },
            }
        };

        public static readonly ExcelFileInfo Rubcovo = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База д. рубцово.xlsx"),
            RecodeStartRow = 39,
            RecodeEndRow = 45,
            ListInfo = new[]
            {
                new ExcelListInfo
                {
                    ListName = "ул. луговая",
                    TownName = "д. Рубцово",
                    StreetName = "ул. Луговая",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. садовая",
                    TownName = "д. Рубцово",
                    StreetName = "ул. Садовая",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. строителей",
                    TownName = "д. Рубцово",
                    StreetName = "ул. Строителей",
                    HasContract = true
                },
            }
        };

        public static readonly ExcelFileInfo Dashki2 = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дашки2-2.xlsx"),
            RecodeStartRow = 46,
            RecodeEndRow = 57,
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
            RecodeStartRow = 58,
            RecodeEndRow = 65,
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
            RecodeStartRow = 66,
            RecodeEndRow = 75,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "ул. Вишневая", TownName = "п. Новоселки", StreetName = "ул. Вишневая"},
                new ExcelListInfo {ListName = "ул. Зеленая", TownName = "п. Новоселки", StreetName = "ул. Зеленая"},
                new ExcelListInfo {ListName = "ул. Мичурина", TownName = "п. Новоселки", StreetName = "ул. Мичурина"},
                new ExcelListInfo
                {
                    ListName = "ул. Молодежная дом 3",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Молодежная"
                },
                new ExcelListInfo
                {
                    ListName = "ул.Молодежная дом 4",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Молодежная"
                },
                new ExcelListInfo
                {
                    ListName = "ул.Молодежная дом№5",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Молодежная"
                },
                new ExcelListInfo
                {
                    ListName = "ул. Молодежная дом№6,7",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Молодежная"
                },
                new ExcelListInfo
                {
                    ListName = "ул. Молодежная дом№6,7",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Молодежная",
                    StartRow = 36
                },
                new ExcelListInfo
                {
                    ListName = "1-й молодежный, березовая",
                    TownName = "п. Новоселки",
                    StreetName = "ул. 1-й Молодежный проезд"
                },
                new ExcelListInfo
                {
                    ListName = "1-й молодежный, березовая",
                    TownName = "п. Новоселки",
                    StreetName = "ул. 1-й Молодежный проезд",
                    StartRow = 18
                },
                new ExcelListInfo
                {
                    ListName = "ул. Нефтезаводская д.1",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская"
                },
                new ExcelListInfo
                {
                    ListName = "ул. Нефтезаводская д.2,4",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская"
                },
                new ExcelListInfo
                {
                    ListName = "ул. Нефтезаводская д.2,4",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская",
                    StartRow = 17
                },
                new ExcelListInfo
                {
                    ListName = "ул.Нефтезаводская д.3,5",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская"
                },
                new ExcelListInfo
                {
                    ListName = "ул.Нефтезаводская д.3,5",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская",
                    StartRow = 18
                },
                new ExcelListInfo {ListName = "ул. Новая ,1.. 3", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo
                {
                    ListName = "ул. Новая ,1.. 3",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Новая",
                    StartRow = 19
                },
                new ExcelListInfo {ListName = "ул.Новая д.3а", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo {ListName = "Новая, 4", TownName = "п. Новоселки", StreetName = "ул. Новая"},
                new ExcelListInfo
                {
                    ListName = "ул. нефтезаводская ч. дома ",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Нефтезаводская",
                    HasContract = true
                },
                new ExcelListInfo {ListName = "ул. Полевая", TownName = "п. Новоселки", StreetName = "ул. Полевая"},
                new ExcelListInfo {ListName = "ул. Приокская", TownName = "п. Новоселки", StreetName = "ул. Приокская"},
                new ExcelListInfo {ListName = "ул. Садовая", TownName = "п. Новоселки", StreetName = "ул. Садовая"},
                new ExcelListInfo {ListName = "ул.Родниковая", TownName = "п. Новоселки", StreetName = "ул. Родниковая"},
                new ExcelListInfo
                {
                    ListName = "ул. Юбилейная",
                    TownName = "п. Новоселки",
                    StreetName = "ул. Юбилейная",
                    HasContract = true
                },
                new ExcelListInfo {ListName = "д.Вишневка", TownName = "д. Вишневка"},
            }
        };

        public static readonly ExcelFileInfo Lgovo = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База с. Льгово.xlsx"),
            RecodeStartRow = 76,
            RecodeEndRow = 83,
            ListInfo = new[]
            {
                new ExcelListInfo
                {
                    ListName = "ул. 3-я линия",
                    TownName = "с. Льгово",
                    StreetName = "ул. 3-я Линия",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. 60 лет СССР",
                    TownName = "с. Льгово",
                    StreetName = "ул. 60 лет СССР",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. колхозная",
                    TownName = "с. Льгово",
                    StreetName = "ул. Колхозная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. ленпоселок",
                    TownName = "с. Льгово",
                    StreetName = "ул. Ленпоселок",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. новая",
                    TownName = "с. Льгово",
                    StreetName = "ул. Новая",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. полевая",
                    TownName = "с. Льгово",
                    StreetName = "ул. Полевая",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. полевая 2",
                    TownName = "с. Льгово",
                    StreetName = "ул. Полевая",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная",
                    TownName = "с. Льгово",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная 2",
                    TownName = "с. Льгово",
                    StreetName = "ул. Школьная",
                    HasContract = true
                },
                new ExcelListInfo
                {
                    ListName = "ул. школьная 2",
                    TownName = "с. Льгово",
                    StreetName = "ул. Школьная",
                    HasContract = true,
                    StartRow = 87
                },
            }
        };

        public static readonly ExcelFileInfo Stenkino = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База с. стенькино.xlsx"),
            RecodeStartRow = 84,
            RecodeEndRow = 89,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "Лист1", TownName = "п. Стенькино", HasContract = true},
            }
        };

        public static readonly ExcelFileInfo Dubrovichi = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База Дубровичи 07.10.2016.xlsx"),
            RecodeStartRow = 90,
            RecodeEndRow = 97,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "дом 1", TownName = "Дубровичи", HasContract = true, PrimCol = 13},
            }
        };

        public static readonly ExcelFileInfo VoenniyGorodokNewAbonents = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("База военный городок.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 9,
            ListInfo = new[]
            {
                new ExcelListInfo
                {
                    ListName = "newAbonents",
                    HasContract = true,
                    TownName = "Дашки военные2",
                    StreetName = "1"
                },
            }
        };

        public static readonly ExcelFileInfo RecodeTable = new ExcelFileInfo
        {
            FileName = Consts.FormFullFilePath("Таблица перекодировкиv3.0.xlsx"),
            RecodeStartRow = 2,
            RecodeEndRow = 97,
            ListInfo = new[]
            {
                new ExcelListInfo {ListName = "Лист1"},
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

        public static string GetLs1С(string ls)
        {
            return String.Format("9621{0:D4}", Int32.Parse(ls));
        }

        public static int GetCounterId1C(string counterid)
        {
            return 17000 + Int32.Parse(counterid);
        }

        public static readonly Dictionary<string, string> MathcedLs = new Dictionary<string, string>
        {
{"96210255",	"96000094"},
{"96210142",	"96000191"},
{"96210318",	"96000303"},
{"96210319",	"96000302"},
{"96210680",	"96000855"},
{"96210594",	"96000001"},
{"96210595",	"96000002"},
{"96210596",	"96000003"},
{"96210597",	"96000004"},
{"96210598",	"96000005"},
{"96210599",	"96000006"},
{"96210600",	"96000007"},
{"96210601",	"96000008"},
{"96210602",	"96000009"},
{"96210603",	"96000010"},
{"96210604",	"96000011"},
{"96210605",	"96000012"},
{"96210606",	"96000013"},
{"96210607",	"96000014"},
{"96210608",	"96000015"},
{"96210609",	"96000016"},
{"96210610",	"96000017"},
{"96210611",	"96000018"},
{"96210612",	"96000019"},
{"96210613",	"96000020"},
{"96210614",	"96000021"},
{"96210615",	"96000022"},
{"96210616",	"96000023"},
{"96210617",	"96000024"},
{"96210618",	"96000025"},
{"96210619",	"96000026"},
{"96210620",	"96000027"},
{"96210621",	"96000028"},
{"96210622",	"96000029"},
{"96210623",	"96000030"},
{"96210624",	"96000031"},
{"96210625",	"96000032"},
{"96210626",	"96000033"},
{"96210627",	"96000034"},
{"96210628",	"96000035"},
{"96210629",	"96000036"},
{"96210630",	"96000037"},
{"96210631",	"96000038"},
{"96210632",	"96000039"},
{"96210633",	"96000040"},
{"96210634",	"96000041"},
{"96210635",	"96000042"},
{"96210636",	"96000043"},
{"96210637",	"96000044"},
{"96210638",	"96000045"},
{"96210639",	"96000046"},
{"96210640",	"96000047"},
{"96210641",	"96000048"},
{"96210210",	"96000049"},
{"96210211",	"96000050"},
{"96210212",	"96000051"},
{"96210213",	"96000052"},
{"96210214",	"96000053"},
{"96210215",	"96000054"},
{"96210216",	"96000055"},
{"96210217",	"96000056"},
{"96210218",	"96000057"},
{"96210219",	"96000058"},
{"96210220",	"96000059"},
{"96210221",	"96000060"},
{"96210222",	"96000061"},
{"96210223",	"96000062"},
{"96210224",	"96000063"},
{"96210225",	"96000064"},
{"96210226",	"96000065"},
{"96210227",	"96000066"},
{"96210228",	"96000067"},
{"96210229",	"96000068"},
{"96210230",	"96000069"},
{"96210231",	"96000070"},
{"96210232",	"96000071"},
{"96210233",	"96000072"},
{"96210234",	"96000073"},
{"96210235",	"96000074"},
{"96210236",	"96000075"},
{"96210237",	"96000076"},
{"96210238",	"96000077"},
{"96210239",	"96000078"},
{"96210240",	"96000079"},
{"96210241",	"96000080"},
{"96210242",	"96000081"},
{"96210243",	"96000082"},
{"96210244",	"96000083"},
{"96210245",	"96000084"},
{"96210246",	"96000085"},
{"96210247",	"96000086"},
{"96210248",	"96000087"},
{"96210249",	"96000088"},
{"96210250",	"96000089"},
{"96210251",	"96000090"},
{"96210252",	"96000091"},
{"96210253",	"96000092"},
{"96210254",	"96000093"},
{"96210256",	"96000095"},
{"96210257",	"96000096"},
{"96210048",	"96000097"},
{"96210049",	"96000098"},
{"96210050",	"96000099"},
{"96210051",	"96000100"},
{"96210052",	"96000101"},
{"96210053",	"96000102"},
{"96210054",	"96000103"},
{"96210055",	"96000104"},
{"96210056",	"96000105"},
{"96210057",	"96000106"},
{"96210058",	"96000107"},
{"96210059",	"96000109"},
{"96210060",	"96000110"},
{"96210061",	"96000111"},
{"96210062",	"96000112"},
{"96210063",	"96000113"},
{"96210064",	"96000114"},
{"96210065",	"96000115"},
{"96210066",	"96000116"},
{"96210067",	"96000117"},
{"96210068",	"96000118"},
{"96210069",	"96000119"},
{"96210070",	"96000120"},
{"96210071",	"96000121"},
{"96210072",	"96000123"},
{"96210073",	"96000124"},
{"96210074",	"96000125"},
{"96210075",	"96000126"},
{"96210076",	"96000127"},
{"96210077",	"96000128"},
{"96210078",	"96000129"},
{"96210079",	"96000130"},
{"96210080",	"96000131"},
{"96210081",	"96000132"},
{"96210082",	"96000133"},
{"96210083",	"96000134"},
{"96210084",	"96000135"},
{"96210085",	"96000136"},
{"96210086",	"96000137"},
{"96210087",	"96000138"},
{"96210088",	"96000139"},
{"96210089",	"96000140"},
{"96210090",	"96000141"},
{"96210091",	"96000142"},
{"96210092",	"96000143"},
{"96210093",	"96000144"},
{"96210094",	"96000122"},
{"96210095",	"96000108"},
{"96210096",	"96000145"},
{"96210097",	"96000146"},
{"96210098",	"96000147"},
{"96210099",	"96000148"},
{"96210100",	"96000149"},
{"96210101",	"96000150"},
{"96210102",	"96000151"},
{"96210103",	"96000152"},
{"96210104",	"96000153"},
{"96210105",	"96000154"},
{"96210106",	"96000155"},
{"96210107",	"96000156"},
{"96210108",	"96000157"},
{"96210109",	"96000158"},
{"96210110",	"96000159"},
{"96210111",	"96000160"},
{"96210112",	"96000161"},
{"96210113",	"96000162"},
{"96210114",	"96000163"},
{"96210115",	"96000164"},
{"96210116",	"96000165"},
{"96210117",	"96000166"},
{"96210118",	"96000167"},
{"96210119",	"96000168"},
{"96210120",	"96000169"},
{"96210121",	"96000170"},
{"96210122",	"96000171"},
{"96210123",	"96000172"},
{"96210124",	"96000173"},
{"96210125",	"96000174"},
{"96210126",	"96000175"},
{"96210127",	"96000176"},
{"96210128",	"96000177"},
{"96210129",	"96000178"},
{"96210130",	"96000179"},
{"96210131",	"96000180"},
{"96210132",	"96000181"},
{"96210133",	"96000182"},
{"96210134",	"96000183"},
{"96210135",	"96000184"},
{"96210136",	"96000185"},
{"96210137",	"96000186"},
{"96210138",	"96000187"},
{"96210139",	"96000188"},
{"96210140",	"96000189"},
{"96210141",	"96000833"},
{"96210143",	"96000193"},
{"96210144",	"96000194"},
{"96210145",	"96000195"},
{"96210146",	"96000196"},
{"96210147",	"96000197"},
{"96210148",	"96000198"},
{"96210149",	"96000199"},
{"96210150",	"96000200"},
{"96210151",	"96000201"},
{"96210152",	"96000202"},
{"96210153",	"96000203"},
{"96210154",	"96000204"},
{"96210155",	"96000205"},
{"96210156",	"96000206"},
{"96210157",	"96000207"},
{"96210158",	"96000208"},
{"96210159",	"96000209"},
{"96210160",	"96000210"},
{"96210161",	"96000211"},
{"96210162",	"96000212"},
{"96210163",	"96000213"},
{"96210164",	"96000214"},
{"96210165",	"96000215"},
{"96210166",	"96000216"},
{"96210167",	"96000217"},
{"96210169",	"96000218"},
{"96210171",	"96000219"},
{"96210172",	"96000220"},
{"96210173",	"96000221"},
{"96210174",	"96000222"},
{"96210175",	"96000223"},
{"96210176",	"96000224"},
{"96210177",	"96000226"},
{"96210178",	"96000227"},
{"96210179",	"96000228"},
{"96210180",	"96000229"},
{"96210181",	"96000230"},
{"96210182",	"96000231"},
{"96210183",	"96000232"},
{"96210184",	"96000233"},
{"96210185",	"96000234"},
{"96210186",	"96000235"},
{"96210850",	"96000225"},
{"96210187",	"96000236"},
{"96210188",	"96000238"},
{"96210189",	"96000239"},
{"96210190",	"96000240"},
{"96210258",	"96000237"},
{"96210259",	"96000241"},
{"96210260",	"96000242"},
{"96210261",	"96000243"},
{"96210262",	"96000244"},
{"96210263",	"96000245"},
{"96210264",	"96000246"},
{"96210265",	"96000247"},
{"96210266",	"96000248"},
{"96210267",	"96000249"},
{"96210268",	"96000250"},
{"96210269",	"96000251"},
{"96210270",	"96000252"},
{"96210271",	"96000253"},
{"96210272",	"96000254"},
{"96210273",	"96000255"},
{"96210274",	"96000256"},
{"96210275",	"96000257"},
{"96210276",	"96000258"},
{"96210277",	"96000259"},
{"96210278",	"96000260"},
{"96210279",	"96000262"},
{"96210280",	"96000263"},
{"96210281",	"96000264"},
{"96210282",	"96000265"},
{"96210283",	"96000266"},
{"96210284",	"96000267"},
{"96210285",	"96000269"},
{"96210286",	"96000270"},
{"96210287",	"96000271"},
{"96210288",	"96000272"},
{"96210289",	"96000273"},
{"96210290",	"96000274"},
{"96210291",	"96000275"},
{"96210292",	"96000276"},
{"96210293",	"96000277"},
{"96210294",	"96000278"},
{"96210295",	"96000279"},
{"96210296",	"96000280"},
{"96210297",	"96000281"},
{"96210298",	"96000282"},
{"96210299",	"96000283"},
{"96210851",	"96000268"},
{"96211639",	"96000261"},
{"96210300",	"96000284"},
{"96210301",	"96000285"},
{"96210302",	"96000286"},
{"96210303",	"96000287"},
{"96210304",	"96000288"},
{"96210305",	"96000289"},
{"96210306",	"96000290"},
{"96210307",	"96000291"},
{"96210308",	"96000292"},
{"96210309",	"96000293"},
{"96210310",	"96000294"},
{"96210311",	"96000295"},
{"96210312",	"96000296"},
{"96210313",	"96000297"},
{"96210314",	"96000298"},
{"96210315",	"96000299"},
{"96210316",	"96000300"},
{"96210317",	"96000301"},
{"96210320",	"96000304"},
{"96210321",	"96000305"},
{"96210322",	"96000306"},
{"96210323",	"96000307"},
{"96210324",	"96000308"},
{"96210325",	"96000309"},
{"96210326",	"96000310"},
{"96210327",	"96000311"},
{"96210328",	"96000312"},
{"96210329",	"96000313"},
{"96210330",	"96000314"},
{"96210331",	"96000315"},
{"96210332",	"96000316"},
{"96210333",	"96000317"},
{"96210334",	"96000318"},
{"96210335",	"96000319"},
{"96210336",	"96000320"},
{"96210337",	"96000321"},
{"96210338",	"96000322"},
{"96210339",	"96000323"},
{"96210340",	"96000324"},
{"96210341",	"96000325"},
{"96210342",	"96000326"},
{"96210343",	"96000327"},
{"96210344",	"96000328"},
{"96210345",	"96000329"},
{"96210346",	"96000330"},
{"96210347",	"96000331"},
{"96210348",	"96000332"},
{"96210349",	"96000333"},
{"96210350",	"96000335"},
{"96210351",	"96000336"},
{"96210352",	"96000337"},
{"96210353",	"96000338"},
{"96210354",	"96000339"},
{"96210355",	"96000340"},
{"96210356",	"96000341"},
{"96210357",	"96000342"},
{"96210358",	"96000343"},
{"96210359",	"96000344"},
{"96210360",	"96000345"},
{"96210361",	"96000346"},
{"96210362",	"96000347"},
{"96210363",	"96000348"},
{"96210364",	"96000349"},
{"96210365",	"96000350"},
{"96210366",	"96000351"},
{"96210367",	"96000352"},
{"96210368",	"96000353"},
{"96210369",	"96000354"},
{"96210370",	"96000355"},
{"96210371",	"96000356"},
{"96210372",	"96000357"},
{"96210373",	"96000358"},
{"96210374",	"96000359"},
{"96210375",	"96000360"},
{"96210376",	"96000361"},
{"96210377",	"96000362"},
{"96210378",	"96000363"},
{"96210379",	"96000364"},
{"96210380",	"96000365"},
{"96210381",	"96000366"},
{"96210382",	"96000367"},
{"96210383",	"96000368"},
{"96211636",	"96000334"},
{"96210384",	"96000369"},
{"96210385",	"96000370"},
{"96210386",	"96000371"},
{"96210387",	"96000372"},
{"96210388",	"96000373"},
{"96210390",	"96000374"},
{"96210391",	"96000375"},
{"96210392",	"96000376"},
{"96210393",	"96000377"},
{"96210394",	"96000378"},
{"96210395",	"96000379"},
{"96210396",	"96000380"},
{"96210397",	"96000381"},
{"96210398",	"96000382"},
{"96210399",	"96000383"},
{"96210400",	"96000384"},
{"96210401",	"96000385"},
{"96210402",	"96000386"},
{"96210403",	"96000387"},
{"96210404",	"96000388"},
{"96210405",	"96000389"},
{"96210406",	"96000390"},
{"96210407",	"96000391"},
{"96210408",	"96000392"},
{"96210409",	"96000393"},
{"96210410",	"96000394"},
{"96210411",	"96000395"},
{"96210412",	"96000396"},
{"96210413",	"96000397"},
{"96210414",	"96000398"},
{"96210415",	"96000399"},
{"96210416",	"96000400"},
{"96210417",	"96000401"},
{"96210418",	"96000402"},
{"96210419",	"96000403"},
{"96210420",	"96000404"},
{"96210421",	"96000405"},
{"96210422",	"96000406"},
{"96210423",	"96000407"},
{"96210424",	"96000408"},
{"96210425",	"96000409"},
{"96210426",	"96000410"},
{"96210427",	"96000411"},
{"96210428",	"96000412"},
{"96210429",	"96000413"},
{"96210430",	"96000414"},
{"96210431",	"96000415"},
{"96210432",	"96000416"},
{"96210433",	"96000417"},
{"96210434",	"96000418"},
{"96210435",	"96000419"},
{"96210436",	"96000420"},
{"96210437",	"96000421"},
{"96210438",	"96000422"},
{"96210439",	"96000423"},
{"96210440",	"96000424"},
{"96210441",	"96000425"},
{"96210442",	"96000426"},
{"96210443",	"96000427"},
{"96210444",	"96000428"},
{"96210445",	"96000429"},
{"96210446",	"96000430"},
{"96210447",	"96000431"},
{"96210448",	"96000432"},
{"96210449",	"96000433"},
{"96210450",	"96000434"},
{"96210451",	"96000435"},
{"96210452",	"96000436"},
{"96210453",	"96000437"},
{"96210454",	"96000438"},
{"96210455",	"96000439"},
{"96210456",	"96000440"},
{"96210457",	"96000441"},
{"96210458",	"96000442"},
{"96210459",	"96000443"},
{"96210460",	"96000444"},
{"96210461",	"96000445"},
{"96210462",	"96000446"},
{"96210463",	"96000447"},
{"96210464",	"96000448"},
{"96210465",	"96000449"},
{"96210466",	"96000450"},
{"96210467",	"96000451"},
{"96210468",	"96000452"},
{"96210469",	"96000453"},
{"96210470",	"96000454"},
{"96210471",	"96000455"},
{"96210472",	"96000456"},
{"96210473",	"96000457"},
{"96210474",	"96000458"},
{"96210475",	"96000459"},
{"96210476",	"96000460"},
{"96210477",	"96000461"},
{"96210478",	"96000462"},
{"96210479",	"96000463"},
{"96210480",	"96000464"},
{"96210481",	"96000465"},
{"96210482",	"96000466"},
{"96210483",	"96000467"},
{"96210484",	"96000468"},
{"96210485",	"96000469"},
{"96210486",	"96000470"},
{"96210487",	"96000471"},
{"96210488",	"96000472"},
{"96210489",	"96000473"},
{"96210490",	"96000474"},
{"96210492",	"96000475"},
{"96210493",	"96000476"},
{"96210494",	"96000477"},
{"96210495",	"96000478"},
{"96210496",	"96000479"},
{"96210497",	"96000480"},
{"96210498",	"96000481"},
{"96210499",	"96000482"},
{"96210500",	"96000483"},
{"96210501",	"96000484"},
{"96210502",	"96000485"},
{"96210503",	"96000486"},
{"96210504",	"96000487"},
{"96210505",	"96000488"},
{"96210506",	"96000489"},
{"96210507",	"96000490"},
{"96210508",	"96000491"},
{"96210509",	"96000492"},
{"96210510",	"96000493"},
{"96210511",	"96000494"},
{"96210512",	"96000495"},
{"96210513",	"96000496"},
{"96210514",	"96000497"},
{"96210515",	"96000498"},
{"96210516",	"96000499"},
{"96210517",	"96000500"},
{"96210518",	"96000501"},
{"96210519",	"96000502"},
{"96210520",	"96000503"},
{"96210521",	"96000504"},
{"96210522",	"96000505"},
{"96210523",	"96000506"},
{"96210524",	"96000507"},
{"96210525",	"96000508"},
{"96210526",	"96000509"},
{"96210527",	"96000510"},
{"96210528",	"96000512"},
{"96210529",	"96000513"},
{"96210530",	"96000514"},
{"96210531",	"96000515"},
{"96210532",	"96000516"},
{"96210533",	"96000517"},
{"96210534",	"96000518"},
{"96210535",	"96000519"},
{"96210536",	"96000520"},
{"96210537",	"96000521"},
{"96210538",	"96000522"},
{"96210539",	"96000523"},
{"96210540",	"96000524"},
{"96210541",	"96000525"},
{"96210542",	"96000528"},
{"96210543",	"96000529"},
{"96210544",	"96000530"},
{"96210545",	"96000531"},
{"96210546",	"96000532"},
{"96210547",	"96000533"},
{"96210548",	"96000534"},
{"96210853",	"96000526"},
{"96210854",	"96000527"},
{"96210855",	"96000511"},
{"96210549",	"96000535"},
{"96210550",	"96000536"},
{"96210551",	"96000537"},
{"96210552",	"96000538"},
{"96210553",	"96000539"},
{"96210554",	"96000540"},
{"96210555",	"96000541"},
{"96210556",	"96000542"},
{"96210557",	"96000543"},
{"96210558",	"96000544"},
{"96210559",	"96000545"},
{"96210560",	"96000546"},
{"96210561",	"96000547"},
{"96210562",	"96000548"},
{"96210563",	"96000549"},
{"96210564",	"96000550"},
{"96210565",	"96000551"},
{"96210566",	"96000552"},
{"96210567",	"96000554"},
{"96210568",	"96000555"},
{"96210569",	"96000556"},
{"96210570",	"96000557"},
{"96210571",	"96000558"},
{"96210572",	"96000559"},
{"96210573",	"96000553"},
{"96210574",	"96000560"},
{"96210575",	"96000561"},
{"96210576",	"96000562"},
{"96210577",	"96000563"},
{"96210578",	"96000564"},
{"96210579",	"96000565"},
{"96210580",	"96000566"},
{"96210581",	"96000567"},
{"96210582",	"96000568"},
{"96210583",	"96000569"},
{"96210584",	"96000570"},
{"96210585",	"96000571"},
{"96210586",	"96000572"},
{"96210587",	"96000573"},
{"96210588",	"96000574"},
{"96210589",	"96000575"},
{"96210590",	"96000576"},
{"96210591",	"96000577"},
{"96210592",	"96000578"},
{"96210593",	"96000579"},
{"96211641",	"96000535"},
{"96210004",	"96000580"},
{"96210005",	"96000581"},
{"96210006",	"96000582"},
{"96210007",	"96000583"},
{"96210008",	"96000584"},
{"96210009",	"96000585"},
{"96210010",	"96000586"},
{"96210011",	"96000587"},
{"96210012",	"96000588"},
{"96210013",	"96000589"},
{"96210014",	"96000590"},
{"96210015",	"96000591"},
{"96210016",	"96000592"},
{"96210017",	"96000593"},
{"96210018",	"96000594"},
{"96210019",	"96000595"},
{"96210020",	"96000596"},
{"96210021",	"96000597"},
{"96210022",	"96000598"},
{"96210023",	"96000599"},
{"96210024",	"96000600"},
{"96210025",	"96000601"},
{"96210026",	"96000602"},
{"96210027",	"96000603"},
{"96210028",	"96000604"},
{"96210029",	"96000605"},
{"96210030",	"96000606"},
{"96210031",	"96000607"},
{"96210032",	"96000608"},
{"96210033",	"96000609"},
{"96210034",	"96000610"},
{"96210035",	"96000611"},
{"96210036",	"96000612"},
{"96210037",	"96000613"},
{"96210038",	"96000614"},
{"96210039",	"96000615"},
{"96210040",	"96000616"},
{"96210041",	"96000618"},
{"96210042",	"96000619"},
{"96210043",	"96000620"},
{"96210044",	"96000621"},
{"96210045",	"96000622"},
{"96210046",	"96000623"},
{"96210047",	"96000624"},
{"96211640",	"96000617"},
{"96210642",	"96000625"},
{"96210643",	"96000626"},
{"96210644",	"96000628"},
{"96210645",	"96000630"},
{"96210646",	"96000631"},
{"96210647",	"96000632"},
{"96210648",	"96000633"},
{"96210649",	"96000634"},
{"96210650",	"96000635"},
{"96210651",	"96000636"},
{"96210652",	"96000637"},
{"96210653",	"96000638"},
{"96210654",	"96000639"},
{"96210655",	"96000640"},
{"96210656",	"96000641"},
{"96210657",	"96000642"},
{"96210658",	"96000643"},
{"96210659",	"96000644"},
{"96210660",	"96000645"},
{"96210661",	"96000646"},
{"96210662",	"96000647"},
{"96210663",	"96000648"},
{"96210664",	"96000649"},
{"96210665",	"96000650"},
{"96210666",	"96000651"},
{"96210667",	"96000652"},
{"96210668",	"96000653"},
{"96210669",	"96000654"},
{"96210670",	"96000655"},
{"96210671",	"96000656"},
{"96210672",	"96000657"},
{"96210673",	"96000658"},
{"96210674",	"96000659"},
{"96210675",	"96000660"},
{"96210676",	"96000661"},
{"96210677",	"96000662"},
{"96210678",	"96000663"},
{"96210679",	"96000664"},
{"96210681",	"96000666"},
{"96210682",	"96000667"},
{"96210683",	"96000668"},
{"96210684",	"96000669"},
{"96210685",	"96000670"},
{"96210686",	"96000671"},
{"96210687",	"96000672"},
{"96210688",	"96000673"},
{"96210689",	"96000674"},
{"96210690",	"96000675"},
{"96210691",	"96000676"},
{"96210692",	"96000677"},
{"96210693",	"96000678"},
{"96210694",	"96000679"},
{"96210695",	"96000680"},
{"96210696",	"96000681"},
{"96210697",	"96000682"},
{"96210698",	"96000683"},
{"96210699",	"96000684"},
{"96210700",	"96000685"},
{"96210701",	"96000686"},
{"96210702",	"96000687"},
{"96210703",	"96000688"},
{"96210704",	"96000689"},
{"96210705",	"96000690"},
{"96210706",	"96000691"},
{"96210707",	"96000692"},
{"96210708",	"96000693"},
{"96210709",	"96000694"},
{"96211631",	"96000627"},
{"96211632",	"96000629"},
{"96210710",	"96000695"},
{"96210711",	"96000696"},
{"96210712",	"96000697"},
{"96210713",	"96000698"},
{"96210714",	"96000699"},
{"96210715",	"96000700"},
{"96210716",	"96000701"},
{"96210717",	"96000702"},
{"96210718",	"96000703"},
{"96210719",	"96000704"},
{"96210720",	"96000705"},
{"96210721",	"96000706"},
{"96210722",	"96000707"},
{"96210723",	"96000708"},
{"96210724",	"96000709"},
{"96210725",	"96000710"},
{"96210726",	"96000711"},
{"96210727",	"96000712"},
{"96210728",	"96000713"},
{"96210729",	"96000714"},
{"96210730",	"96000715"},
{"96210731",	"96000716"},
{"96210732",	"96000717"},
{"96210733",	"96000718"},
{"96210734",	"96000719"},
{"96210735",	"96000720"},
{"96210736",	"96000721"},
{"96210737",	"96000722"},
{"96210738",	"96000723"},
{"96210739",	"96000724"},
{"96210740",	"96000725"},
{"96210741",	"96000726"},
{"96210742",	"96000727"},
{"96210743",	"96000728"},
{"96210744",	"96000729"},
{"96210745",	"96000730"},
{"96210746",	"96000731"},
{"96210747",	"96000732"},
{"96210748",	"96000733"},
{"96210749",	"96000735"},
{"96210750",	"96000736"},
{"96210751",	"96000737"},
{"96210752",	"96000738"},
{"96210753",	"96000739"},
{"96210754",	"96000740"},
{"96210755",	"96000741"},
{"96210756",	"96000742"},
{"96210757",	"96000743"},
{"96210758",	"96000744"},
{"96210759",	"96000745"},
{"96210760",	"96000746"},
{"96210761",	"96000747"},
{"96210762",	"96000748"},
{"96210763",	"96000749"},
{"96210764",	"96000750"},
{"96210765",	"96000751"},
{"96210766",	"96000752"},
{"96210767",	"96000753"},
{"96210768",	"96000754"},
{"96210769",	"96000755"},
{"96210770",	"96000756"},
{"96210771",	"96000757"},
{"96210772",	"96000758"},
{"96210773",	"96000759"},
{"96210774",	"96000760"},
{"96210775",	"96000761"},
{"96210776",	"96000762"},
{"96210847",	"96000734"},
{"96210777",	"96000763"},
{"96210778",	"96000764"},
{"96210779",	"96000765"},
{"96210780",	"96000766"},
{"96210781",	"96000767"},
{"96210782",	"96000768"},
{"96210783",	"96000769"},
{"96210784",	"96000770"},
{"96210785",	"96000771"},
{"96210786",	"96000772"},
{"96210787",	"96000774"},
{"96210788",	"96000775"},
{"96210789",	"96000776"},
{"96210790",	"96000777"},
{"96210791",	"96000778"},
{"96210792",	"96000779"},
{"96210793",	"96000780"},
{"96210794",	"96000781"},
{"96210795",	"96000782"},
{"96210796",	"96000783"},
{"96210797",	"96000784"},
{"96210798",	"96000785"},
{"96210799",	"96000786"},
{"96210800",	"96000787"},
{"96210801",	"96000788"},
{"96210802",	"96000789"},
{"96210803",	"96000790"},
{"96210804",	"96000791"},
{"96210805",	"96000792"},
{"96210806",	"96000793"},
{"96210807",	"96000794"},
{"96210808",	"96000795"},
{"96210809",	"96000796"},
{"96210810",	"96000797"},
{"96210811",	"96000798"},
{"96210812",	"96000799"},
{"96210813",	"96000800"},
{"96210814",	"96000801"},
{"96210815",	"96000802"},
{"96210816",	"96000803"},
{"96210817",	"96000804"},
{"96210818",	"96000805"},
{"96210819",	"96000806"},
{"96210820",	"96000807"},
{"96210821",	"96000808"},
{"96210822",	"96000809"},
{"96210823",	"96000810"},
{"96210824",	"96000811"},
{"96210825",	"96000812"},
{"96210826",	"96000813"},
{"96210827",	"96000814"},
{"96210828",	"96000815"},
{"96210829",	"96000816"},
{"96210830",	"96000817"},
{"96210831",	"96000818"},
{"96210832",	"96000819"},
{"96210833",	"96000820"},
{"96210834",	"96000821"},
{"96210835",	"96000822"},
{"96210836",	"96000823"},
{"96210837",	"96000824"},
{"96210838",	"96000825"},
{"96210839",	"96000826"},
{"96210840",	"96000827"},
{"96210841",	"96000828"},
{"96210842",	"96000829"},
{"96210843",	"96000830"},
{"96210844",	"96000831"},
{"96210845",	"96000832"},
{"96210846",	"96000773"},
        };
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
            Prim = listInfo.PrimCol.HasValue && !String.IsNullOrWhiteSpace(dr[listInfo.PrimCol.Value - 1].ToString())
                ? dr[listInfo.PrimCol.Value - 1].ToString().Trim()
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
                    "Произошла ошибка при чтении файла {0} ({1})\r\n{2}\r\nStackTrace:{3}", fileInfo.FileName,
                    listInfo.ListName,
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

        private static readonly Regex FioRegex = new Regex(@"([а-я]+)[^а-я]+([а-я]+)[^а-я]*([а-я]*)[^а-я]*",
            RegexOptions.IgnoreCase);

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
                        throw new Exception("При сопоставлении дашков военных не расшифрован дом " +
                                            excelData.HouseNumber);
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
                    abonent.HOUSEPOSTFIX = String.IsNullOrWhiteSpace(match.Groups[2].Value)
                        ? null
                        : match.Groups[2].Value;
                    if (abonent.HOUSEPOSTFIX != null && abonent.HOUSEPOSTFIX.Length > 10)
                        abonent.HOUSEPOSTFIX = abonent.HOUSEPOSTFIX.Substring(0, 9);
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
                    abonent.FLATPOSTFIX = String.IsNullOrWhiteSpace(match.Groups[2].Value)
                        ? null
                        : match.Groups[2].Value;
                    if (abonent.FLATPOSTFIX != null && abonent.FLATPOSTFIX.Length > 10)
                        abonent.FLATPOSTFIX = abonent.FLATPOSTFIX.Substring(0, 9);
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
            AbonentRecordUtils.SetUniqueUlicakod(Abonents, 80);
            AbonentRecordUtils.SetUniqueHouseCd(Abonents, 2000);
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
                    SETUPDATE = new DateTime(2016, 10, 01),
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
                uniqId = (int) _lastUniqId;
            }
            if (String.IsNullOrWhiteSpace(excelData.MatchedLshet))
                LChars.Add(new CNV_LCHAR
                {
                    LSHET =
                        String.IsNullOrWhiteSpace(excelData.MatchedLshet)
                            ? Consts.GetLs(uniqId)
                            : excelData.MatchedLshet,
                    DATE_ = new DateTime(2016, 10, 01),
                    LCHARCD = 35,
                    LCHARNAME = "Поставщик",
                    VALUE_ = 0,
                    VALUEDESC = "МПК ЖКХ Рязанское"
                });
            for (int i = 0; i < RecodeTableData.Length; i++)
            {
                if (!(_fileInfo.RecodeStartRow <= i + 2 && i + 2 <= _fileInfo.RecodeEndRow)) continue;
                var recode = RecodeTableData[i];
                if (poliv != recode.Poliv) continue;
                if (recode.EmptyField.HasValue && recode.ShoudByEmpty)
                {
                    switch (recode.EmptyField)
                    {
                        case RecodeTable.CheckEmptyField.NoVsAndnoVo:
                            if (excelData.NoCntVO != null || excelData.NoCntVS != null) continue;
                            break;
                        case RecodeTable.CheckEmptyField.Vs:
                            if (excelData.CntVS != null) continue;
                            break;
                        default:
                            throw new Exception("Неизвестный тип значения перекодировки " + recode.EmptyField);
                    }
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
                    LSHET =
                        String.IsNullOrWhiteSpace(excelData.MatchedLshet)
                            ? Consts.GetLs(uniqId)
                            : excelData.MatchedLshet,
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
                                    throw new Exception("Необработанный населенный пункт для сопоставления " +
                                                        listInfo.TownName);
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
            public CheckEmptyField? EmptyField;
            public double Value;
            public bool ShoudByEmpty;
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
                ShoudByEmpty = dr[4].ToString().ToLower() == "\"пусто\"";
                Poliv = dr[5].ToString().Trim().ToLower() == "полив";
                LcharCd = Int32.Parse(dr[6].ToString());
                LcharName = dr[7].ToString().Trim();
                LcharValue = Int32.Parse(dr[8].ToString());
                LcharValueName = dr[9].ToString().Trim();
                if (!String.IsNullOrWhiteSpace(dr[3].ToString()))
                    switch (dr[3].ToString().Trim().ToLower())
                    {
                        case "без счетчика во и без счетчика вс":
                            EmptyField = CheckEmptyField.NoVsAndnoVo;
                            break;
                        case "по счетчику вс":
                            EmptyField = CheckEmptyField.Vs;
                            break;
                        default:
                            throw new Exception("Неизвестный тип пустого поля в таблице перекодировке " + dr[3]);
                    }
            }


            public enum CounterRecode
            {
                Vs,
                Vo,
                NoVs,
                NoVo
            }

            public enum CheckEmptyField
            {
                NoVsAndnoVo,
                Vs,
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

    public class ConvertAbonentDubrovichiNew : ConvertCase
    {
        public ConvertAbonentDubrovichiNew()
        {
            ConvertCaseName = "ABONENTS - Дубровичи new";
            Position = 11;
            IsChecked = true;
        }

        private class DuplicatedAbonent
        {
            public string fio { get; set; }
            public string name { get; set; }
            public string second_name { get; set; }
            public int housecd { get; set; }
            public float? cc { get; set; }
            public float? lc { get; set; }
        }

        public override void DoConvert()
        {
//            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
//            {
//                var result = context.ExecuteQuery<DuplicatedAbonent>(
//                    @"select a.fio, a.name, a.second_name, a.housecd, ca.significance as cc, la.significance as lc from abonents a
//                        inner join houses h on h.housecd = a.housecd and h.punktcd = 22
//                        left join ccharsabonentlist ca on ca.lshet = a.lshet and ca.kodccharslist = 1
//                        left join lcharsabonentlist la on la.lshet = a.lshet and la.kodlcharslist = 22
//                        group by a.housecd, a.fio, a.name, a.second_name, ca.significance, la.significance
//                        having count(*) > 1");

//                string duplicatedAbonents = "";
//                for (int i = 0; i < result.Count; i++)
//                {
//                    var da = result[i];
//                    string sql = String.Format(
//                        @"select first 1 a.lshet from abonents a
//                        inner join houses h on h.housecd = a.housecd and h.punktcd = 22
//                        left join ccharsabonentlist ca on ca.lshet = a.lshet and ca.kodccharslist = 1
//                        left join lcharsabonentlist la on la.lshet = a.lshet and la.kodlcharslist = 22
//                        where a.fio = '{0}' and a.name = '{1}' and a.second_name = '{2}' and a.housecd = {3}",
//                        da.fio, da.name, da.second_name, da.housecd);


//                    if (da.cc.HasValue) sql += " and ca.significance = " + da.cc.Value;
//                    if (da.lc.HasValue) sql += " and la.significance = " + da.lc.Value;

//            var r = context.ExecuteQuery<string>(sql);
//                    if (r.Count == 0)
//                    {
//                        int a = 10;
//                    }
//                    duplicatedAbonents += r[0] + "\r\n";
//                }
//            }



            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            var convertType = ExcelListReader.ConvertType.Abonents;
            ExcelListReader.Initialize(convertType);
            SetStepsCount(6);
            var files = new [] {ExcelFileInfo.NewDubrovichi};

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

            for (int i = 0; i < ExcelListReader.Abonents.Count; i++)
            {
                ExcelListReader.Abonents[i].TOWNSKOD = 22;
            }

            StepStart(1);
            ExcelListReader.FinishRead(convertType);
            StepFinish();

            StepStart(1);
            ExcelListReader.Save(this, convertType);
            StepFinish();

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

            StepStart(ExcelListReader.Abonents.Count);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                for (int i = 0; i < ExcelListReader.Abonents.Count; i++)
                {
                    var abonent = ExcelListReader.Abonents[i];

                    string sql;
                    if (abonent.F == "Ермишин" && abonent.I == "Анатолий" && abonent.O == "Иванович")
                    {
                        sql = String.Format(@"update abonents set housecd = {0} where lshet = '{1}'", abonent.HOUSECD, "96202621");
                        context.ExecuteNonQuery(sql);
                        context.SaveChanges();
                        continue;
                    }

                    sql = String.Format(@"select a.lshet from abonents a
inner join houses h on h.housecd = a.housecd
where h.punktcd = 22 and
    a.fio = '{0}'", abonent.F);
                    if (abonent.I == null) sql += " and a.name is null";
                    else sql += String.Format(" and a.name = '{0}'", abonent.I);
                    if (abonent.O == null) sql += " and a.second_name is null";
                    else sql += String.Format(" and a.second_name = '{0}'", abonent.O);
                    if (abonent.HOUSENO == null) sql += " and h.houseno is null";
                    else sql += String.Format(" and h.houseno = {0}", abonent.HOUSENO);
                    if (abonent.HOUSEPOSTFIX == null) sql += " and h.housepostfix is null";
                    else sql += String.Format(" and h.housepostfix = '{0}'", abonent.HOUSEPOSTFIX);

                    var result = context.ExecuteQuery<string>(sql);
                    for (int j = 0; j < result.Count; j++)
                    {
                        sql = String.Format(@"update abonents set housecd = {0} where lshet = '{1}'", abonent.HOUSECD, result[j]);
                        context.ExecuteNonQuery(sql);
                        if (abonent.FLATNO != null)
                        {
                            sql = String.Format(@"update abonents set FLATNO = {0} where lshet = '{1}'", abonent.FLATNO, result[j]);
                            context.ExecuteNonQuery(sql);
                        }
                        if (!String.IsNullOrWhiteSpace(abonent.FLATPOSTFIX))
                        {
                            sql = String.Format(@"update abonents set FLATPOSTFIX = {0} where lshet = '{1}'", abonent.FLATPOSTFIX, result[j]);
                            context.ExecuteNonQuery(sql);
                        }
                        context.SaveChanges();
                    }
                    if (result.Count != 1)
                    {
                        int a = 10;
                    }
                    Iterate();
                }
            }
            StepFinish();
        }
    }

    public class ConvertAbonents : ConvertCase
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

    public class ConvertAbonents1C : ConvertCase
    {
        public ConvertAbonents1C()
        {
            ConvertCaseName = "1C ABONENTS";
            Position = 100;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            string notMatched = "";
            string matched = "";
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();
            StepStart(dt.Rows.Count);
            var regex = new Regex(@"(\d+)(.*)");
            var abonentRecord = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonentRecord.ReadDataRow(dataRow);

                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs1С(abonentRecord.Lshet),
                    RAYONKOD = 2,
                    RAYONNAME = "Рязанский район",
                    DUCD = 2,
                    DUNAME = "МКП \"ЖКХ Рязанское\"",
                    TOWNSNAME = abonentRecord.Townsname.Trim(),
                    ULICANAME = abonentRecord.Ulicaname.Trim(),
                    ISDELETED = (int) abonentRecord.Isdeleted,
                    PRIM_ = abonentRecord.Prim.Trim(),
                    KORPUSNO = String.IsNullOrWhiteSpace(abonentRecord.Korpus)
                        ? (int?) null
                        : Int32.Parse(abonentRecord.Korpus),
                    F = abonentRecord.F.Trim(),
                    I = abonentRecord.I.Trim(),
                    O = abonentRecord.O.Trim(),
                };


                if (Consts.MathcedLs.ContainsKey(abonent.LSHET)) continue;


                switch (abonentRecord.Townskod)
                {
                    case 940:
                        abonent.TOWNSKOD = 1;
                        switch (abonentRecord.Ulicakod)
                        {
                            case 2:
                                abonent.ULICAKOD = 1;
                                break;
                            case 3:
                                abonent.ULICAKOD = 2;
                                break;
                            case 192:
                                abonent.ULICAKOD = 3;
                                break;
                        }
                        break;
                }

                if (!String.IsNullOrWhiteSpace(abonentRecord.Ndoma))
                {
                    string house = abonentRecord.Ndoma.Trim();
                    var matches = regex.Matches(house);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) abonent.HOUSEPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int houseno;
                            if (Int32.TryParse(groups[1].Value, out houseno)) abonent.HOUSENO = groups[1].Value;
                            else abonent.HOUSEPOSTFIX = groups[0].Value;
                        }
                    }
                    else
                    {
                        abonent.HOUSEPOSTFIX = house;
                    }
                }

                if (!String.IsNullOrWhiteSpace(abonentRecord.Kvartira))
                {
                    string flat = abonentRecord.Kvartira.Trim();
                    var matches = regex.Matches(flat);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) abonent.FLATPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int flatno;
                            if (Int32.TryParse(groups[1].Value, out flatno)) abonent.FLATNO = flatno;
                            else abonent.FLATPOSTFIX = groups[0].Value;
                        }
                    }
                    else
                    {
                        abonent.FLATPOSTFIX = flat;
                    }
                }

//                    string query = String.Format(@"select ab.lshet from abonents ab
//                                            inner join houses h on h.housecd = ab.housecd and
//                                                                (h.punktcd = '{0}' and
//                                                                h.streetcd = '{1}' and
//                                                                h.houseno = '{2}' and                                                                
//                                                                ab.flatno = '{3}')",
//                                    abonent.TOWNSKOD ?? 0, abonent.ULICAKOD ?? 0, abonent.HOUSENO ?? "", abonent.FLATNO ?? 0);
//                    var result = context.ExecuteQuery<string>(query);
//                    if (result.Count != 1)
//                    {
//                        if (abonent.TOWNSKOD == 1 &&
//                            (abonent.ULICAKOD == 1 || abonent.ULICAKOD == 2 || abonent.ULICAKOD == 3))
//                            notMatched += abonent.LSHET + "\t" + query + "\r\n";
//                        else if (result.Count > 0)
//                            throw new Exception("O_o");
//                    }
//                    else
//                    {
//                        matched += abonent.LSHET + "\t" + result[0] + "\r\n";
//                    }




                lca.Add(abonent);
                Iterate();
            }

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                StepStart(1);
                AbonentRecordUtils.SetUniqueTownskod(lca, context.ExecuteQuery<int>("select gen_id(PUNKT_G,1) from rdb$database")[0]);
                AbonentRecordUtils.SetUniqueUlicakod(lca, context.ExecuteQuery<int>("select gen_id(STREET_G,1) from rdb$database")[0]);
                AbonentRecordUtils.SetUniqueHouseCd(lca, context.ExecuteQuery<int>("select gen_id(HOUSES_G,1) from rdb$database")[0]);
                StepFinish();
            }

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    public class ConvertChars1C : ConvertCase
    {
        public ConvertChars1C()
        {
            ConvertCaseName = "1C CHARS";
            Position = 110;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            DataTable dt = Tmsource.GetDataTable("CHARS");
            var lcc = new List<CNV_CHAR>();

            StepStart(dt.Rows.Count);
            var charsRecord = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                charsRecord.ReadDataRow(dataRow);

                var cchar = new CNV_CHAR
                {
                    LSHET = Consts.GetLs1С(charsRecord.Lshet),
                    VALUE_ = charsRecord.Value_,
                    DATE_ = charsRecord.Date
                };

                if (Consts.MathcedLs.ContainsKey(cchar.LSHET)) continue;
            

                switch (charsRecord.Charcd)
                {
                    case 1:
                        cchar.CHARCD = 4;
                        cchar.CHARNAME = "Отапливаемая площадь";
                        break;
                    case 2:
                        cchar.CHARCD = 24;
                        cchar.CHARNAME = "Жилая площадь";
                        break;
                    case 3:
                        cchar.CHARCD = 3;
                        cchar.CHARNAME = "Число прописанных";
                        break;
                    case 4:
                        cchar.CHARCD = 1;
                        cchar.CHARNAME = "Число проживающих";
                        break;
                    default:
                        throw new Exception(String.Format("Незивестная характеристика {0} {1}", charsRecord.Charname,
                            charsRecord.Charcd));
                }
                lcc.Add(cchar);
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

    public class ConvertLchars1C : ConvertCase
    {
        public ConvertLchars1C()
        {
            ConvertCaseName = "1C LCHARS";
            Position = 120;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$LCHARS");
            DataTable dt = Tmsource.GetDataTable("LCHARS");
            var fileInfo = ExcelFileInfo.RecodeTable1C;
            DataTable recodeTable = Utils.ReadExcelFile(fileInfo.FileName, fileInfo.ListInfo[0].ListName);
            var llc = new List<CNV_LCHAR>();

            StepStart(dt.Rows.Count);
            var lcharRecord = new LcharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                lcharRecord.ReadDataRow(dataRow);

                string lshet = Consts.GetLs1С(lcharRecord.Lshet);
                string matchedLshet;
                if (Consts.MathcedLs.TryGetValue(lshet, out matchedLshet)) 
                    lshet = matchedLshet;

                for (int i = fileInfo.RecodeStartRow - 2; i <= fileInfo.RecodeEndRow -2; i++)
                {
                    var row = recodeTable.Rows[i];

                    if (Convert.ToInt32(row[1]) != lcharRecord.Tarifcd ||
                        Convert.ToInt32(row[3]) != lcharRecord.Value_) continue;

                    var lc = new CNV_LCHAR
                    {
                        LSHET = lshet,
                        LCHARCD = Convert.ToInt32(row[5]),
                        LCHARNAME = row[6].ToString(),
                        VALUEDESC = row[8].ToString(),
                        VALUE_ = Convert.ToInt32(row[7]),
                        DATE_ = lcharRecord.Date
                    };

                    if (llc.Any(c => c.LSHET == lc.LSHET && c.LCHARCD == lc.LCHARCD && c.VALUE_ == lc.VALUE_)) continue;

                    llc.Add(lc);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            llc = LcharsRecordUtils.ThinOutList(llc);
            StepFinish();

            SaveList(llc, Consts.InsertRecordCount);
        }
    }

    public class ConvertCounters1C : ConvertCase
    {
        public ConvertCounters1C()
        {
            ConvertCaseName = "1C COUNTERS";
            Position = 130;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$COUNTERS");
            DataTable dt = Tmsource.GetDataTable("COUNTERS");

            var lcn = new List<CNV_COUNTER>();
            StepStart(dt.Rows.Count);
            var counterRecord = new CountersRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterRecord.ReadDataRow(dataRow);
                if (counterRecord.Isdeleted == 1) continue;

                string lshet = Consts.GetLs1С(counterRecord.Lshet);
                string matchedLshet;
                if (Consts.MathcedLs.TryGetValue(lshet, out matchedLshet))
                    lshet = matchedLshet;

                int cnttype;
                string cntName = counterRecord.Name.ToLower();
                if (cntName.Contains("полив")) cnttype = 3179;
                else if (cntName.Contains("г/в")) cnttype = 3177;
                else if (cntName.Contains("х/в")) cnttype = 1;
                else throw new Exception(String.Format("Неизсветный тип стчетчика {0}", counterRecord.Name));

                if (!String.IsNullOrWhiteSpace(matchedLshet) && cnttype == 3177) continue;

                var counter = new CNV_COUNTER
                {
                    LSHET = lshet,
                    CNTTYPE = cnttype,
                    NAME = counterRecord.Name.Trim(),
                    COUNTERID = Consts.GetCounterId1C(counterRecord.Counterid).ToString(),
                    SERIALNUM = String.IsNullOrWhiteSpace(counterRecord.Serialnum) ? null : counterRecord.Serialnum.Trim()
                };

                var oldCnt = lcn.SingleOrDefault(cnt => cnt.COUNTERID == counter.COUNTERID);
                if (oldCnt != null)
                {
                    if (oldCnt.CNTTYPE != counter.CNTTYPE) throw new Exception("Задублированный счетчик имеет другой тип\r\nЛС: "+counter.LSHET);
                    Iterate();
                    continue;
                }

                lcn.Add(counter);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            SaveList(lcn, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    public class ConvertCntind1C : ConvertCase
    {
        public ConvertCntind1C()
        {
            ConvertCaseName = "1C CNTINDS";
            Position = 140;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CNTRSIND");
            DataTable dt = Tmsource.ExecuteQuery(@"SELECT i.*, RECNO() AS RECNO FROM CNTRSIND i");

            string[] deletedCounters;
            using (DataTable dtDeletedCounters = Tmsource.ExecuteQuery(@"select counterid from counters where isdeleted = 1"))
            {
                var tempList = new List<string>();
                for (int i = 0; i < dtDeletedCounters.Rows.Count; i++)
                {
                    tempList.Add(dtDeletedCounters.Rows[i][0].ToString().Trim());
                }
                deletedCounters = tempList.ToArray();
            }

            var lci = new List<CNV_CNTRSIND>();
            StepStart(dt.Rows.Count);
            var counterind = new CntrsindRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                counterind.ReadDataRow(dataRow);

                if (deletedCounters.Contains(counterind.Counterid.Trim())) continue;

                var c = new CNV_CNTRSIND
                {
                    COUNTERID = Consts.GetCounterId1C(counterind.Counterid).ToString(),
                    DOCUMENTCD = String.Format("{0}_{1}", counterind.Counterid.Trim().TrimStart('0'), dataRow["RECNO"]),
                    INDDATE = counterind.Inddate,
                    OLDIND = counterind.Previndic,
                    INDICATION = counterind.Indication,
                    OB_EM = counterind.Count,
                    INDTYPE = 0
                };

                lci.Add(c);

                Iterate();
            }
            StepFinish();

            SaveList(lci, Consts.InsertRecordCount);
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
            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] {"0"});
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
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] {"0", "0"});
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
            fbm.ExecuteProcedure("CNV$CNV_00900_LCHARS", new[] {"0"});
            Iterate();
            StepFinish();
        }
    }


    #endregion
}