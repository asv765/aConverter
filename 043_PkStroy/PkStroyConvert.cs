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
                var result = String.Format("93{0:D6}", MaxId);
                MaxId++;
                return result;
            }
        }
        public static readonly int CurrentMonth = 04;

        public static readonly int CurrentYear = 2016;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";

        public static int MaxId;

        public static Regex FioRegex = new Regex(@"([а-я]+)[^а-я]+([а-я]+)[^а-я]*([а-я]*)[^а-я]*", RegexOptions.IgnoreCase);
        public static Regex digitRegex = new Regex(@"(\d*)(.*)");
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

            var abonents = new List<CNV_ABONENT>();
            var cchars = new List<CNV_CHAR>();
            dynamic[] sheets =
            {
                new {name = "Дом№1", maxRecord = 17}, new {name = "Дом № 2", maxRecord = 17},
                new {name = "Дом №3", maxRecord = 27}, new {name = "Дом №4", maxRecord = 27},
                new {name = "Дом №5", maxRecord = 27}, new {name = "Дом №6", maxRecord = 27},
                new {name = "Дом № 7", maxRecord = 32}, new {name = "Дом №8", maxRecord = 32},
                new {name = "Дом №9", maxRecord = 30}, new {name = "Дом № 10", maxRecord = 65}
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
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.digitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
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
                        TOWNSNAME = "Дядьково с.",
                        ULICANAME = "Юбилейная ул.",
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };
                    abonents.Add(abonent);

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
            AbonentRecordUtils.SetUniqueHouseCd(abonents, 0);
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

            var abonents = new List<CNV_ABONENT>();
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
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i < 4) continue;
                    if (i > sheet.maxRecord - 2) break;
                    var dr = dataTable.Rows[i];
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.digitRegex.Match(dr[0].ToString()).Groups;
                    var fioMatches = Consts.FioRegex.Matches(dr[2].ToString());
                    string prim = fioMatches.Count > 1 ? fioMatches[1].Value : "";
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
                        TOWNSNAME = "Екимовка с.",
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };
                    abonents.Add(abonent);

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
            AbonentRecordUtils.SetUniqueHouseCd(abonents, 0);
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

            var abonents = new List<CNV_ABONENT>();
            var cchars = new List<CNV_CHAR>();
            dynamic[] sheets =
            {
                new {name = "ул. луговая", maxRecord = 56, streetname = "Луговая ул."}, 
                new {name = "ул. садовая", maxRecord = 93, streetname = "Садовая ул."}, 
                new {name = "ул. строителей", maxRecord = 39, streetname = "Строителей ул."}
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
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.digitRegex.Match(dr[0].ToString()).Groups;
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
                        TOWNSNAME = "Рубцово д.",
                        ULICANAME = sheet.streetname,
                        HOUSENO = String.IsNullOrWhiteSpace(houseGroups[1].Value) ? null : houseGroups[1].Value,
                        HOUSEPOSTFIX = !String.IsNullOrWhiteSpace(houseGroups[2].Value) ? houseGroups[2].Value : null,
                        FLATNO = flatno,
                        FLATPOSTFIX = flatnopostfix,
                        ISDELETED = 0,
                        PRIM_ = String.IsNullOrWhiteSpace(prim) ? null : prim
                    };

                    abonents.Add(abonent);

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
            AbonentRecordUtils.SetUniqueHouseCd(abonents, 0);
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

            var abonents = new List<CNV_ABONENT>();
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
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.digitRegex.Match(dr[0].ToString()).Groups;
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

                    abonents.Add(abonent);

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
            AbonentRecordUtils.SetUniqueHouseCd(abonents, 0);
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

            var abonents = new List<CNV_ABONENT>();
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
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
                    var houseGroups = Consts.digitRegex.Match(dr[0].ToString()).Groups;
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

                    abonents.Add(abonent);

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
            AbonentRecordUtils.SetUniqueHouseCd(abonents, 0);
            StepFinish();
        }
    }
}
