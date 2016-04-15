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

        public static Regex FioRegex = new Regex(@"([а-я]+) ([а-я]+)\.([а-я]+)\.", RegexOptions.IgnoreCase);
        public static Regex digitRegex = new Regex(@"(\d+)(.*)");
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
                    if (i >= sheet.maxRecord-2) break;
                    var dr = dataTable.Rows[i];
                    var fioGroups = Consts.FioRegex.Match(dr[2].ToString()).Groups;
                    var flatGroups = Consts.digitRegex.Match(dr[1].ToString()).Groups;
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
                        TOWNSNAME = "Дядьково",
                        ULICANAME = "Юбилейная",
                        HOUSENO = dr[0].ToString(),
                        FLATNO = Int32.Parse(flatGroups[1].Value),
                        FLATPOSTFIX = flatGroups.Count > 2 ? flatGroups[2].Value : null,
                        ISDELETED = 0
                    };
                    abonents.Add(abonent);

                    if (!dr.IsNull(3))
                    {
                        cchars.Add(new CNV_CHAR
                        {
                            LSHET = abonent.LSHET,
                            CHARCD = 0,
                            VALUE_ = Decimal.Parse(dr[3].ToString()),
                            DATE_ = new DateTime(2016, 5, 1)
                        });
                    }
                    Iterate();
                }
                StepFinish();
            }
        }
    }
}
