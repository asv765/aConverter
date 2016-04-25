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

        public static CNV_ABONENT CreateAbonent(DataRow dr, int primColumn, string townName, string streetName, ref List<CNV_CHAR> cchars)
        {
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
                ULICANAME = streetName,
                HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                FLATNO = flatno,
                FLATPOSTFIX = flatnopostfix,
                ISDELETED = 0,
                PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
            };

            int value;
            if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
            {
                cchars.Add(new CNV_CHAR
                {
                    LSHET = abonent.LSHET,
                    CHARCD = 0,
                    VALUE_ = value,
                    DATE_ = new DateTime(2016, 5, 1)
                });
            }

            return abonent;
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

            Consts.Abonents = new List<CNV_ABONENT>();
            Consts.MaxId = 0;

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
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\дядьково.xlsx";
            var cchars = new List<CNV_CHAR>();
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
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
        }
    }

    public class ConvertEkimovka : ConvertCase
    {
        public ConvertEkimovka()
        {
            ConvertCaseName = "Екимовка";
            Position = 30;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\оплата Екимовка.xlsx";
            var cchars = new List<CNV_CHAR>();
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
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();            
        }
    }

    public class ConvertRubcovo : ConvertCase
    {
        public ConvertRubcovo()
        {
            ConvertCaseName = "Рубцово";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\д. рубцово.xlsx";
            var cchars = new List<CNV_CHAR>();
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
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
        }
    }

    public class ConvertLgovo : ConvertCase
    {
        public ConvertLgovo()
        {
            ConvertCaseName = "Льгово";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\платежи с. Льгово.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            dynamic[] sheets =
            {
                new {name = "ул. 3-я линия", maxRecord = 95, streetname = "3-я Линия ул."}, 
                new {name = "ул. 60 лет СССР", maxRecord = 87, streetname = "60 лет СССР ул."}, 
                new {name = "ул. колхозная", maxRecord = 27, streetname = "Колхозная ул."},
                new {name = "ул. ленпоселок", maxRecord = 42, streetname = "Ленпоселок ул."},
                new {name = "ул. новая", maxRecord = 46, streetname = "Новая ул."},
                new {name = "ул. полевая", maxRecord = 98, streetname = "Полевая ул."},
                new {name = "ул. полевая 2", maxRecord = 109, streetname = "Полевая 2 ул."},
                new {name = "ул. школьная", maxRecord = 102, streetname = "Школьная ул."},
                new {name = "ул. школьная 2", maxRecord = 76, streetname = "Школьная 2 ул."}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i < 4) continue;
                    if (i > sheet.maxRecord - 2) break;
                    var dr = dataTable.Rows[i];
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                    if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                        LSHET = Consts.GetLs,
                        DISTKOD = 1,
                        DISTNAME = "Рязанская область",
                        DUCD = 1,
                        DUNAME = "ПК-Строй",
                        RAYONKOD = 1,
                        RAYONNAME = "Рязанский район",
                        F = fioGroups[1].Value,
                        I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                        O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                        TOWNSNAME = "Льгово с.",
                        ULICANAME = sheet.streetname,
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };
                    if (!dr.IsNull(9))
                        abonent.PRIM_ = abonent.PRIM_ == null ? dr[9].ToString() : abonent.PRIM_ += dr[9].ToString();

                    Consts.Abonents.Add(abonent);

                    int value;
                    if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                    {
                        cchars.Add(new CNV_CHAR
                        {
                            LSHET = abonent.LSHET,
                            CHARCD = 0,
                            VALUE_ = value,
                            DATE_ = new DateTime(2016, 5, 1)
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();

            
        }
    }

    public class ConvertDmitreevkaAndLujki : ConvertCase
    {
        public ConvertDmitreevkaAndLujki()
        {
            ConvertCaseName = "Дмитреевка и Лужки";
            Position = 60;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\платежи д. Дмитриевка, д. Лужки.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            dynamic[] sheets =
            {
                new {name = "д. дмитриевка", maxRecord = 40, townname = "Дмитриевка д."}, 
                new {name = "д. лужки", maxRecord = 73, townname = "Лужки д."}, 
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i < 4) continue;
                    if (i > sheet.maxRecord - 2) break;
                    var dr = dataTable.Rows[i];
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                    if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                        LSHET = Consts.GetLs,
                        DISTKOD = 1,
                        DISTNAME = "Рязанская область",
                        DUCD = 1,
                        DUNAME = "ПК-Строй",
                        RAYONKOD = 1,
                        RAYONNAME = "Рязанский район",
                        F = fioGroups[1].Value,
                        I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                        O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                        TOWNSNAME = sheet.townname,
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };

                    if (!dr.IsNull(9))
                        abonent.PRIM_ = abonent.PRIM_ == null ? dr[9].ToString() : abonent.PRIM_ += dr[9].ToString();
                    Consts.Abonents.Add(abonent);

                    int value;
                    if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                    {
                        cchars.Add(new CNV_CHAR
                        {
                            LSHET = abonent.LSHET,
                            CHARCD = 0,
                            VALUE_ = value,
                            DATE_ = new DateTime(2016, 5, 1)
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();

            
        }
    }

    public class ConvertNovoselki : ConvertCase
    {
        public ConvertNovoselki()
        {
            ConvertCaseName = "Новоселки";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\новоселки, вишневка.xlsx";

            
            var cchars = new List<CNV_CHAR>();
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
                new {name = "1-й молодежный проезд", maxRecord = 9, minRecord = 6, streetname = "1-йМолодежный проезд ул."},
                new {name = "1-й молодежный проезд", maxRecord = 18, minRecord = 18, streetname = "Березовая ул."},
                new {name = "ул. Нефтезаводская д.1", maxRecord = 17, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул. Нефтезаводская д.2,4", maxRecord = 13, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул. Нефтезаводская д.2,4", maxRecord = 18, minRecord = 17, streetname = "Нефтезаводская ул."},
                new {name = "ул.Нефтезаводская д.3,5", maxRecord = 13, minRecord = 6, streetname = "Нефтезаводская ул."},
                new {name = "ул.Нефтезаводская д.3,5", maxRecord = 29, minRecord = 18, streetname = "Нефтезаводская ул."},
                new {name = "ул. Новая ,1.. 3", maxRecord = 10, minRecord = 6, streetname = "Новая ул."},
                new {name = "ул. Новая ,1.. 3", maxRecord = 30, minRecord = 19, streetname = "Новая ул."},
                new {name = "ул.Новая д.3а", maxRecord = 32, minRecord = 6, streetname = "Новая ул."},
                new {name = "Новая, 4", maxRecord = 32, minRecord = 6, streetname = "Новая ул."},
                new {name = "ул. нефтезаводская ч. дома ", maxRecord = 30, minRecord = 6, streetname = "Нефтезаводская ул."},
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
                    if (dr.IsNull(2) && dr.IsNull(3) && dr.IsNull(4) && dr.IsNull(5)) return;
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                    if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                        LSHET = Consts.GetLs,
                        DISTKOD = 1,
                        DISTNAME = "Рязанская область",
                        DUCD = 1,
                        DUNAME = "ПК-Строй",
                        RAYONKOD = 1,
                        RAYONNAME = "Рязанский район",
                        F = fioGroups[1].Value,
                        I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                        O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                        TOWNSNAME = "Новоселки п.",
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };

                    if (sheet.name == "ул. Садовая")
                    {
                        if (dr[2].ToString() == "Епифанцева М.Г.") abonent.PRIM_ = dr[20].ToString();
                    }

                    Consts.Abonents.Add(abonent);

                    int value;
                    if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                    {
                        cchars.Add(new CNV_CHAR
                        {
                            LSHET = abonent.LSHET,
                            CHARCD = 0,
                            VALUE_ = value,
                            DATE_ = new DateTime(2016, 5, 1)
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();

            
        }
    }

    public class ConvertVishnevka : ConvertCase
    {
        public ConvertVishnevka()
        {
            ConvertCaseName = "Вишневка";
            Position = 80;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\новоселки, вишневка.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "д.Вишневка");
            StepStart(dataTable.Rows.Count);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i < 5) continue;
                if (i > 24 - 2) break;
                var dr = dataTable.Rows[i];
                var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                    LSHET = Consts.GetLs,
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = 1,
                    DUNAME = "ПК-Строй",
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    F = fioGroups[1].Value,
                    I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                    O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                    TOWNSNAME = "Вишневка д.",
                    HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                    HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                    FLATNO = flatno,
                    FLATPOSTFIX = flatnopostfix,
                    ISDELETED = 0,
                    PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                };

                Consts.Abonents.Add(abonent);

                int value;
                if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                {
                    cchars.Add(new CNV_CHAR
                    {
                        LSHET = abonent.LSHET,
                        CHARCD = 0,
                        VALUE_ = value,
                        DATE_ = new DateTime(2016, 5, 1)
                    });
                }
                Iterate();
            }
            StepFinish();
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
            
        }
    }

    public class ConvertPushino : ConvertCase
    {
        public ConvertPushino()
        {
            ConvertCaseName = "Пущино";
            Position = 90;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\д. пущино.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "Лист1");
            StepStart(dataTable.Rows.Count);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i < 4) continue;
                if (i > 13 - 2) break;
                var dr = dataTable.Rows[i];
                var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                    LSHET = Consts.GetLs,
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = 1,
                    DUNAME = "ПК-Строй",
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    F = fioGroups[1].Value,
                    I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                    O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                    TOWNSNAME = "Пущино д.",
                    HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                    HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                    FLATNO = flatno,
                    FLATPOSTFIX = flatnopostfix,
                    ISDELETED = 0,
                    PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                };

                Consts.Abonents.Add(abonent);

                int value;
                if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                {
                    cchars.Add(new CNV_CHAR
                    {
                        LSHET = abonent.LSHET,
                        CHARCD = 0,
                        VALUE_ = value,
                        DATE_ = new DateTime(2016, 5, 1)
                    });
                }
                Iterate();
            }
            StepFinish();
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
            
        }
    }

    public class ConvertRovnoe : ConvertCase
    {
        public ConvertRovnoe()
        {
            ConvertCaseName = "Ровное";
            Position = 100;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\ровное.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            dynamic[] sheets =
            {
                new {name = "ул школьная,1", maxRecord = 21, streetname = "Школьная ул."}, 
                new {name = "ул. школьная,2", maxRecord = 21, streetname = "Школьная ул."}, 
                new {name = "ул. школьная,3", maxRecord = 22, streetname = "Школьная ул."},
                new {name = "ул. школьная,4", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "ул.школьная,5", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "ул.школьная,6", maxRecord = 32, streetname = "Школьная ул."},
                new {name = "час.дома", maxRecord = 12, streetname = ""},
                new {name = "ул. дубовая роща", maxRecord = 11, streetname = "Дубовая роща ул."},
                new {name = "ул. новоселов", maxRecord = 20, streetname = "Новоселов ул."}
            };
            foreach (var sheet in sheets)
            {
                DataTable dataTable = Utils.ReadExcelFile(fileName, sheet.name);
                StepStart(dataTable.Rows.Count);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i < 4) continue;
                    if (i > sheet.maxRecord - 2) break;
                    var dr = dataTable.Rows[i];
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                    if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                        LSHET = Consts.GetLs,
                        DISTKOD = 1,
                        DISTNAME = "Рязанская область",
                        DUCD = 1,
                        DUNAME = "ПК-Строй",
                        RAYONKOD = 1,
                        RAYONNAME = "Рязанский район",
                        F = fioGroups[1].Value,
                        I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                        O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                        TOWNSNAME = "Ровное д.",
                        ULICANAME = sheet.streetname == "" ? null : sheet.streetname,
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };

                    Consts.Abonents.Add(abonent);

                    int value;
                    if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                    {
                        cchars.Add(new CNV_CHAR
                        {
                            LSHET = abonent.LSHET,
                            CHARCD = 0,
                            VALUE_ = value,
                            DATE_ = new DateTime(2016, 5, 1)
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
            
        }
    }

    public class ConvertStenkeno : ConvertCase
    {
        public ConvertStenkeno()
        {
            ConvertCaseName = "Стенькино";
            Position = 110;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(99);
            const string fileName = @"D:\Work\C#\C#Projects\aConverter\043_PkStroy\Sources\стенькино.xlsx";

            
            var cchars = new List<CNV_CHAR>();
            DataTable dataTable = Utils.ReadExcelFile(fileName, "Лист1");
            StepStart(dataTable.Rows.Count);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i < 4) continue;
                if (i > 89 - 2) break;
                var dr = dataTable.Rows[i];
                var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                var flatGroups = Consts.DigitRegex.Match(dr[1].ToString()).Groups;
                var houseGroups = Consts.DigitRegex.Match(dr[0].ToString()).Groups;
                var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
                if (!dr.IsNull(9)) prim += dr[9].ToString();
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
                    LSHET = Consts.GetLs,
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = 1,
                    DUNAME = "ПК-Строй",
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    F = fioGroups[1].Value,
                    I = fioGroups.Count > 2 ? fioGroups[2].Value : null,
                    O = fioGroups.Count > 3 ? fioGroups[3].Value : null,
                    TOWNSNAME = "Стенькино п.",
                    HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                    HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                    FLATNO = flatno,
                    FLATPOSTFIX = flatnopostfix,
                    ISDELETED = 0,
                    PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                };

                Consts.Abonents.Add(abonent);

                int value;
                if (!dr.IsNull(3) && Int32.TryParse(dr[3].ToString(), out value))
                {
                    cchars.Add(new CNV_CHAR
                    {
                        LSHET = abonent.LSHET,
                        CHARCD = 0,
                        VALUE_ = value,
                        DATE_ = new DateTime(2016, 5, 1)
                    });
                }
                Iterate();
            }
            StepFinish();
            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(Consts.Abonents, 0);
            StepFinish();
            
        }
    }

    public class SaveAbonents : ConvertCase
    {
        public SaveAbonents()
        {
            ConvertCaseName = "Перенести абонентов во временные таблицы";
            Position = 120;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            SaveList(Consts.Abonents, Consts.InsertRecordCount);
        }
    }
}
