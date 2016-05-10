using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _043_PkStroy
{
    public static class Consts
    {
        public const int InsertRecordCount = 1000;
        public static string GetLs
        {
            get
            {
                var result = String.Format("94{0:D6}", MaxId);
                MaxId++;
                return result;
            }
        }
        public static readonly int CurrentMonth = 04;

        public static readonly int CurrentYear = 2016;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";

        public static List<CNV_ABONENT> Abonents;
        public static int MaxId;

        public static Regex FioRegex = new Regex(@"([а-я]+)[^а-я]+([а-я]+)[^а-я]*([а-я]*)[^а-я]*", RegexOptions.IgnoreCase);
        public static Regex DigitRegex = new Regex(@"(\d*)(.*)");

        public static CNV_ABONENT CreateAbonent(DataRow dr, int primColumn, string townName, string streetName, ref List<CNV_CHAR> cchars,  bool withChars = true)
        {
            if (dr.IsNull(2) && dr.IsNull(3) && dr.IsNull(4)) return null;
            var fioGroups = FioRegex.Match(dr[2].ToString()).Groups;
            var flatGroups = DigitRegex.Match(dr[1].ToString()).Groups;
            var houseGroups = DigitRegex.Match(dr[0].ToString()).Groups;
            var fioMatches = FioRegex.Matches(dr[2].ToString());
            string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
            if (primColumn != 0 && !dr.IsNull(primColumn - 1)) prim += dr[primColumn - 1].ToString();
            string flatnopostfix;
            int? flatno;
            if (!dr.IsNull(1))
            {
                int flatnoparse;
                if (!Int32.TryParse(flatGroups[1].Value, out flatnoparse))
                {
                    flatno = null;
                    flatnopostfix = !String.IsNullOrWhiteSpace(flatGroups[2].Value) ? flatGroups[2].Value : null;
                }
                else
                {
                    flatno = flatnoparse;
                    flatnopostfix = flatGroups[2].Value;
                }
            }
            else
            {
                flatno = null;
                flatnopostfix = null;
            }

            var abonent = new CNV_ABONENT
            {
                LSHET = GetLs,
                DISTKOD = 1,
                DISTNAME = "Рязанская область",
                DUCD = 1,
                DUNAME = "ПК-Строй",
                RAYONKOD = 1,
                RAYONNAME = "Рязанский район",
                F = fioGroups[1].Value,
                I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                TOWNSNAME = townName,
                ULICANAME = streetName ?? "",
                HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                FLATNO = flatno,
                FLATPOSTFIX = flatnopostfix,
                ISDELETED = 0,
                PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
            };

            if (withChars)
            {
                int value;
                if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                {
                    cchars.Add(new CNV_CHAR
                    {
                        LSHET = abonent.LSHET,
                        CHARCD = 1,
                        VALUE_ = value,
                        DATE_ = new DateTime(2016, 5, 1)
                    });
                }
            }

            return abonent;
        }

        public static void AddLchar(string lshet, DataRow dr, ref List<CNV_LCHAR> lchars, int columnNumber = 5)
        {
            if (dr.IsNull(columnNumber - 1) || String.IsNullOrWhiteSpace(dr[columnNumber - 1].ToString())) return;
            var recode = RecodeTable.Where(r => dr[columnNumber - 1].ToString().Replace(" ", "").Contains(r.OldValue.Replace(" ", ""))).ToList();
            if (lshet == "94001405")
            {
                int a = 10;
            }
            //if (recode.Count == 0) throw new Exception("В таблице перекодировки не найден элемент " + dr[columnNumber - 1].ToString().Trim());
            foreach (var r in recode)
            {
                lchars.Add(new CNV_LCHAR
                {
                    LSHET = lshet,
                    VALUE_ = r.Lcharvalue,
                    LCHARCD = r.Lcharcd,
                    DATE_ = new DateTime(2016, 5, 1)
                });
            }
        }

        private static List<Recode> RecodeTable;

        private class Recode
        {
            public string OldValue { get; set; }
            public int Lcharcd { get; set; }
            public int Lcharvalue { get; set; }

            public Recode(DataRow dr)
            {
                OldValue = dr[1].ToString();
                Lcharcd = Int32.Parse(dr[4].ToString());
                Lcharvalue = Int32.Parse(dr[6].ToString());
            }
        }

        public static void ReadRecodeTable()
        {
            DataTable recodeTable = Utils.ReadExcelFile(@"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\Таблица перекодировки.xlsx", "Лист1");
            RecodeTable = new List<Recode>();
            int i = 0;
            foreach (DataRow dataRow in recodeTable.Rows)
            {
                i++;
                if (i == 39) return;
                RecodeTable.Add(new Recode(dataRow));
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

            Consts.Abonents = new List<CNV_ABONENT>();
            Consts.MaxId = 0;
            Consts.ReadRecodeTable();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    public class ConvertDyadkovo : ConvertCase
    {
        public ConvertDyadkovo()
        {
            ConvertCaseName = "Дьдьково";
            Position = 20;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\дядьково.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "Дом№1", maxRecord = 17}, new {name = "Дом № 2", maxRecord = 17},
                new {name = "Дом №3", maxRecord = 27}, new {name = "Дом №4", maxRecord = 27},
                new {name = "Дом №5", maxRecord = 27}, new {name = "Дом №6", maxRecord = 27},
                new {name = "Дом № 7", maxRecord = 32}, new {name = "Дом №8", maxRecord = 32},
                new {name = "Дом №9", maxRecord = 30}, new {name = "Дом № 10", maxRecord = 66}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 6 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Дядьково с.", "Юбилейная ул.", ref cchars);
                    Consts.Abonents.Add(abonent);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertEkimovka : ConvertCase
    {
        public ConvertEkimovka()
        {
            ConvertCaseName = "Екимовка";
            Position = 30;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\оплата Екимовка.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "дом 1", maxRecord = 21}, new {name = "дом 2", maxRecord = 23},
                new {name = "дом 3", maxRecord = 21}, new {name = "дом 4", maxRecord = 33},
                new {name = "дом 5", maxRecord = 32}, new {name = "дом 6", maxRecord = 33},
                new {name = "дом 7", maxRecord = 33}, new {name = "2-х кв. дома", maxRecord = 35},
                new {name = "коттеджи", maxRecord = 29}, new {name = "коттеджи №2", maxRecord = 23},
                new {name = "част. дома", maxRecord = 25}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 6 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Екимовка с.", null, ref cchars);
                    Consts.Abonents.Add(abonent);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertRubcovo : ConvertCase
    {
        public ConvertRubcovo()
        {
            ConvertCaseName = "Рубцово";
            Position = 40;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\д. рубцово.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "ул. луговая", maxRecord = 56, streetname = "Луговая ул.", primColumn = 10}, 
                new {name = "ул. садовая", maxRecord = 93, streetname = "Садовая ул.", primColumn = 0}, 
                new {name = "ул. строителей", maxRecord = 39, streetname = "Строителей ул.", primColumn = 10}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 6 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, sheet.primColumn, "Рубцово д.", sheet.streetname, ref cchars);
                    Consts.Abonents.Add(abonent);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertLgovo : ConvertCase
    {
        public ConvertLgovo()
        {
            ConvertCaseName = "Льгово";
            Position = 50;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\платежи с. Льгово.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "ул. 3-я линия", maxRecord = 95, streetname = "3-я Линия ул.", primColumn = 0, minRecord = 6}, 
                new {name = "ул. 60 лет СССР", maxRecord = 87, streetname = "60 лет СССР ул.", primColumn = 0, minRecord = 6}, 
                new {name = "ул. колхозная", maxRecord = 27, streetname = "Колхозная ул.", primColumn = 0, minRecord = 6},
                new {name = "ул. ленпоселок", maxRecord = 42, streetname = "Ленпоселок ул.", primColumn = 10, minRecord = 6},
                new {name = "ул. новая", maxRecord = 46, streetname = "Новая ул.", primColumn = 10, minRecord = 6},
                new {name = "ул. полевая", maxRecord = 98, streetname = "Полевая ул.", primColumn = 0, minRecord = 6},
                new {name = "ул. полевая 2", maxRecord = 109, streetname = "Полевая 2 ул.", primColumn = 10, minRecord = 6},
                new {name = "ул. школьная", maxRecord = 103, streetname = "Школьная ул.", primColumn = 0, minRecord = 6},
                new {name = "ул. школьная 2", maxRecord = 76, streetname = "Школьная 2 ул.", primColumn = 10, minRecord = 6},
                //new {name = "ул. школьная 2", maxRecord = 92, streetname = "", primColumn = 0, minRecord = 86}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = sheet.minRecord - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, sheet.primColumn, "Льгово с.", sheet.streetname == "" ? null : sheet.streetname, ref cchars);
                    Consts.Abonents.Add(abonent);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertDmitreevkaAndLujki : ConvertCase
    {
        public ConvertDmitreevkaAndLujki()
        {
            ConvertCaseName = "Дмитреевка и Лужки";
            Position = 60;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\платежи д. Дмитриевка, д. Лужки.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "д. дмитриевка", maxRecord = 40, townname = "Дмитриевка д.", primColumn = 0}, 
                new {name = "д. лужки", maxRecord = 74, townname = "Лужки д.", primColumn = 10}, 
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 4 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, sheet.primColumn, sheet.townname, null, ref cchars);
                    Consts.Abonents.Add(abonent);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertNovoselki : ConvertCase
    {
        public ConvertNovoselki()
        {
            ConvertCaseName = "Новоселки";
            Position = 70;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\новоселки, вишневка.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "ул. Вишневая", maxRecord = 28, minRecord = 6, streetname = "Вишневая ул."},
                new {name = "ул. Зеленая", maxRecord = 33, minRecord = 6, streetname = "Зеленая ул."},
                new {name = "ул. Мичурина", maxRecord = 18, minRecord = 6, streetname = "Мичурина ул."},
                new {name = "ул. Молодежная дом 3", maxRecord = 27, minRecord = 6, streetname = "Молодежная ул."},
                new {name = "ул.Молодежная дом 4", maxRecord = 34, minRecord = 6, streetname = "Молодежная ул."},
                new {name = "ул.Молодежная дом№5", maxRecord = 34, minRecord = 6, streetname = "Молодежная ул."},
                new {name = "ул. Молодежная дом№6,7", maxRecord = 32, minRecord = 6, streetname = "Молодежная ул."},
                new {name = "ул. Молодежная дом№6,7", maxRecord = 37, minRecord = 36, streetname = "Молодежная ул."},
                new {name = "1-й молодежный, березовая", maxRecord = 9, minRecord = 6, streetname = "1-йМолодежный проезд ул."},
                new {name = "1-й молодежный, березовая", maxRecord = 21, minRecord = 18, streetname = "Березовая ул."},
                new {name = "ул. Нефтезаводская д.1", maxRecord = 17, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул. Нефтезаводская д.2,4", maxRecord = 13, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул. Нефтезаводская д.2,4", maxRecord = 18, minRecord = 17, streetname = "Нефтезаводская ул."},
                new {name = "ул.Нефтезаводская д.3,5", maxRecord = 13, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул.Нефтезаводская д.3,5", maxRecord = 29, minRecord = 18, streetname = "Нефтезаводская ул."},
                new {name = "ул. Новая ,1.. 3", maxRecord = 10, minRecord = 6, streetname = "Новая ул."},
                new {name = "ул. Новая ,1.. 3", maxRecord = 30, minRecord = 19, streetname = "Новая ул."},
                new {name = "ул.Новая д.3а", maxRecord = 32, minRecord = 6, streetname = "Новая ул."},
                new {name = "Новая, 4", maxRecord = 32, minRecord = 6, streetname = "Новая ул."},
                new {name = "ул. нефтезаводская ч. дома ", maxRecord = 31, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул. Полевая", maxRecord = 12, minRecord = 6, streetname = "Полевая ул."},
                new {name = "ул. Приокская", maxRecord = 49, minRecord = 6, streetname = "Приокская ул."},
                new {name = "ул. Садовая", maxRecord = 24, minRecord = 6, streetname = "Садовая ул."},
                new {name = "ул.Родниковая", maxRecord = 18, minRecord = 6, streetname = "Родниковая ул."},
                new {name = "ул. Юбилейная", maxRecord = 23, minRecord = 6, streetname = "Юбилейная ул."},
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = sheet.minRecord - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Новоселки п.", sheet.streetname, ref cchars);
                    if (sheet.name == "ул. Садовая")
                    {
                        if (dr[2].ToString() == "Епифанцева М.Г.") abonent.PRIM_ = "умерла";
                    }
                    Consts.Abonents.Add(abonent);
                    if (abonent != null)
                    {
                        try
                        {
                            Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertVishnevka : ConvertCase
    {
        public ConvertVishnevka()
        {
            ConvertCaseName = "Вишневка";
            Position = 80;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\новоселки, вишневка.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "д.Вишневка");
            StepStart(dataTable.Rows.Count);
            for (int i = 7 - 2; i < 34 - 2; i++)
            {
                var dr = dataTable.Rows[i];
                var abonent = Consts.CreateAbonent(dr, 0, "Вишневка д.", null, ref cchars);
                Consts.Abonents.Add(abonent);
                if (abonent != null)
                {
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                }
                Iterate();
            }
            StepFinish();
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertPushino : ConvertCase
    {
        public ConvertPushino()
        {
            ConvertCaseName = "Пущино";
            Position = 90;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\д. пущино.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "Лист1");
            StepStart(dataTable.Rows.Count);
            for (int i = 6 - 2; i < 42 - 2; i++)
            {
                var dr = dataTable.Rows[i];
                var abonent = Consts.CreateAbonent(dr, 0, "Пущино д.", null, ref cchars);
                Consts.Abonents.Add(abonent);
                if (abonent != null)
                {
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                }
                Iterate();
            }
            StepFinish();
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertRovnoe : ConvertCase
    {
        public ConvertRovnoe()
        {
            ConvertCaseName = "Ровное";
            Position = 100;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\ровное.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "ул школьная,1", maxRecord = 21, streetname = "Школьная ул."}, 
                new {name = "ул. школьная,2", maxRecord = 21, streetname = "Школьная ул."}, 
                new {name = "ул. школьная,3", maxRecord = 22, streetname = "Школьная ул."},
                new {name = "ул. школьная,4", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "ул.школьная,5", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "ул.школьная,6", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "час.дома", maxRecord = 16, streetname = ""},
                new {name = "ул. дубовая роща", maxRecord = 16, streetname = "Дубовая роща ул."},
                new {name = "ул. новоселов", maxRecord = 28, streetname = "Новоселов ул."}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 4 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Ровное д.", sheet.streetname == "" ? null : sheet.streetname, ref cchars);
                    Consts.Abonents.Add(abonent);
                    if (abonent != null)
                    {
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars, 6);
                    }
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertStenkeno : ConvertCase
    {
        public ConvertStenkeno()
        {
            ConvertCaseName = "Стенькино";
            Position = 110;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\стенькино.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "Лист1");
            StepStart(dataTable.Rows.Count);
            for (int i = 6 - 2; i < 133 - 2; i++)
            {
                var dr = dataTable.Rows[i];
                var abonent = Consts.CreateAbonent(dr, 0, "Стенькино п.", null, ref cchars);
                Consts.Abonents.Add(abonent);
                if (abonent != null)
                {
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    Consts.AddLchar(abonent.LSHET, dr, ref lchars, 6);
                }
                Iterate();
            }
            StepFinish();
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertVishetravino : ConvertCase
    {
        public ConvertVishetravino()
        {
            ConvertCaseName = "Вышетравино";
            Position = 120;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\вышетравино.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "дом1", maxRecord = 13, streetname = ""},
                new {name = "дом 2", maxRecord = 13, streetname = ""},
                new {name = "дом3", maxRecord = 13, streetname = ""},
                new {name = "дом4", maxRecord = 12, streetname = ""},
                new {name = "дом5", maxRecord = 12, streetname = ""},
                new {name = "дом6", maxRecord = 13, streetname = ""},
                new {name = "дом8", maxRecord = 13, streetname = ""},
                new {name = "дом7", maxRecord = 13, streetname = ""},
                new {name = "дом9", maxRecord = 13, streetname = ""},
                new {name = "дом 10", maxRecord = 13, streetname = ""},
                new {name = "дом 11", maxRecord = 11, streetname = ""},
                new {name = "дом 12", maxRecord = 13, streetname = ""},
                new {name = "дом 13", maxRecord = 13, streetname = ""},
                new {name = "дом 14", maxRecord = 12, streetname = ""},
                new {name = "дом 15", maxRecord = 13, streetname = ""},
                new {name = "дом 16", maxRecord = 13, streetname = ""},
                new {name = "дом 17 ", maxRecord = 17, streetname = ""},
                new {name = "дом 18", maxRecord = 29, streetname = ""},
                new {name = "дом 19", maxRecord = 32, streetname = ""},
                new {name = "дом 20", maxRecord = 32, streetname = ""},
                new {name = "ул. Полевая", maxRecord = 25, streetname = "Полевая ул."},
                new {name = "ул. Овражная", maxRecord = 10, streetname = "Овражная ул."},
                new {name = "ул. Бутырки", maxRecord = 9, streetname = "Бутырки ул."},
                new {name = "ул. Садовая", maxRecord = 14, streetname = "Садовая ул."},
                new {name = "ул.Новая", maxRecord = 12, streetname = "Новая ул."},
                new {name = "ул. прудная", maxRecord = 17, streetname = "Прудная ул."},
                new {name = "ул. дачная", maxRecord = 11, streetname = "Дачная ул."},
                //new {name = "ул. Солнечная", maxRecord = 6, streetname = "Солнечная ул."},
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 6 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Вышетравино с.", sheet.streetname == "" ? null : sheet.streetname, ref cchars);
                    Consts.Abonents.Add(abonent);
                    if (abonent != null)
                    {
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                    }
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertVoenniiGorodok : ConvertCase
    {
        public ConvertVoenniiGorodok()
        {
            ConvertCaseName = "Военный городок";
            Position = 130;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\оплата военный городок.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "дом 1", maxRecord = 47, minRecord = 6},
                new {name = "дом2", maxRecord = 50, minRecord = 6},
                new {name = "дом 3", maxRecord = 27, minRecord = 6},
                new {name = "дом 4,5 ", maxRecord = 15, minRecord = 6},
                new {name = "дом 4,5 ", maxRecord = 34, minRecord = 25},
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = sheet.minRecord - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Рязань г.", "Военный городок №20", ref cchars);
                    Consts.Abonents.Add(abonent);
                    if (abonent != null)
                    {
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars);
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars, 6);
                    }
                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }

    public class ConvertDashki : ConvertCase
    {
        public ConvertDashki()
        {
            ConvertCaseName = "Дашки 2";
            Position = 140;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\оплата Дашки2.xlsx";
            var cchars = new List<CNV_CHAR>();
            var lchars = new List<CNV_LCHAR>();
            dynamic[] sheets =
            {
                new {name = "дом 34", maxRecord = 32},
                new {name = "дом 35", maxRecord = 32},
                new {name = "дом 36", maxRecord = 32},
                new {name = "дом 37", maxRecord = 32},
                new {name = "дом38", maxRecord = 32},
                new {name = "част.дома", maxRecord = 33},
                new {name = "част. дома", maxRecord = 75},
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 6 - 2; i < sheet.maxRecord - 2; i++)
                {
                    var dr = dataTable.Rows[i];
                    var abonent = Consts.CreateAbonent(dr, 0, "Дашки 2 п.", null, ref cchars, false);
                    Consts.Abonents.Add(abonent);
                    if (abonent != null)
                    {
                        Consts.AddLchar(abonent.LSHET, dr, ref lchars);


                        if (!dr.IsNull(3))
                        {
                            var regex = new Regex(@"(\d+)([^\(]*\((\d+)\))?");

                            var groups = regex.Match(dr[3].ToString()).Groups;

                            bool added = false;
                            int value;
                            if (Int32.TryParse(groups[3].Value, out value))
                            {
                                cchars.Add(new CNV_CHAR
                                {
                                    LSHET = abonent.LSHET,
                                    CHARCD = 1,
                                    VALUE_ = value,
                                    DATE_ = new DateTime(2016, 5, 1)
                                });
                                added = true;
                            }
                            if (!added)
                                cchars.Add(new CNV_CHAR
                                {
                                    LSHET = abonent.LSHET,
                                    CHARCD = 1,
                                    VALUE_ = Int32.Parse(groups[1].Value),
                                    DATE_ = new DateTime(2016, 5, 1)
                                });
                        }
                    }

                    Iterate();
                }
                StepFinish();
            }
            Consts.Abonents = Consts.Abonents.Where(a => a != null).ToList();
            SaveList(cchars, 1000);
            SaveList(lchars, 1000);
        }
    }
    
    public class SaveAbonents : ConvertCase
    {
        public SaveAbonents()
        {
            ConvertCaseName = "Перенести абонентов во временные таблицы";
            Position = 1000;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);
            StepStart(1);
            AbonentRecordUtils.SetUniqueTownskod(Consts.Abonents, 0);
            StepFinish();
            StepStart(1);
            AbonentRecordUtils.SetUniqueUlicakod(Consts.Abonents, 0);
            StepFinish();
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
            SaveList(Consts.Abonents, 1000);
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
}
