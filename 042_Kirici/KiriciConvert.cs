using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _042_Kirici
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\039_Iskra\Source\Таблица перекодировки.xls.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("88{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 03;

        public static readonly int CurrentYear = 2016;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";
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

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

    #region Конвертация

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("ABONENT");
            var lca = new List<CNV_ABONENT>();
            var regex = new Regex(@"(\d+)(.*)");
            StepStart(dt.Rows.Count);
            var abonent = new AbonentRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                abonent.ReadDataRow(dataRow);
                if (abonent.Closedate > new DateTime(2000,1,1)) continue;
                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(abonent.Lshet_kod)),
                    EXTLSHET = abonent.Lshet.Trim(),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = 1,
                    DUNAME = "МБУ \"Кирицкое\"",
                    RAYONKOD = 1,
                    RAYONNAME = "Спасский р-н",
                    PRIM_ = abonent.Prim.Trim(),
                    F = abonent.F.Trim(),
                    I = abonent.I.Trim(),
                    O = abonent.O.Trim(),
                    POSTINDEX = abonent.Postindex.Trim(),
                    TOWNSKOD = (int)abonent.Townskod,
                    TOWNSNAME = String.IsNullOrWhiteSpace(abonent.Townsname.Trim()) ? Consts.UnknownTown : abonent.Townsname.Trim(),
                    ULICAKOD = (int)abonent.Ulicakod,
                    ULICANAME = String.IsNullOrWhiteSpace(abonent.Ulicaname.Trim()) ? Consts.UnknownStreet : abonent.Ulicaname.Trim(),
                    FLATNO = Convert.ToInt32(abonent.Kvartira),
                    ISDELETED = (int)abonent.Isdeleted
                };

                string house = abonent.Ndoma.Trim();
                var matches = regex.Matches(house);
                if (matches.Count > 0)
                {
                    var groups = matches[0].Groups;
                    if (groups.Count > 2) a.HOUSEPOSTFIX = groups[2].Value;
                    if (groups.Count > 1)
                    {
                        int houseno;
                        if (Int32.TryParse(groups[1].Value, out houseno)) a.HOUSENO = groups[1].Value;
                        else a.HOUSEPOSTFIX = groups[0].Value;
                    }
                }

                string flat = abonent.Kvartira.Trim();
                matches = regex.Matches(flat);
                if (matches.Count > 0)
                {
                    var groups = matches[0].Groups;
                    if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                    if (groups.Count > 1)
                    {
                        int flatno;
                        if (Int32.TryParse(groups[1].Value, out flatno)) a.FLATNO = flatno;
                        else a.FLATPOSTFIX = groups[0].Value;
                    }
                }
                lca.Add(a);
                Iterate();
            }
            StepFinish();

            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    /// <summary>
    /// Конвертирует данные о количественных характеристиках
    /// </summary>
    public class ConvertChars : ConvertCase
    {
        public ConvertChars()
        {
            ConvertCaseName = "CHARS - данные о количественных характеристиках";
            Position = 30;
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
            var chars = new CharsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                chars.ReadDataRow(dataRow);

                if (String.IsNullOrWhiteSpace(chars.Lshet_kod)) continue;
                if (chars.Value_ < 0) continue;
                if (chars.Date.Year == 14) chars.Date = new DateTime(2014, chars.Date.Month, chars.Date.Day);

                var c = new CNV_CHAR
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(chars.Lshet_kod)),
                    VALUE_ = chars.Value_,
                    DATE_ = chars.Date
                };
                switch (chars.Charcd)
                {
                    case 1:
                        c.CHARCD = 2;
                        break;
                    case 2:
                        c.CHARCD = 14;
                        break;
                    case 7:
                        c.CHARCD = 1;
                        break;
                    case 4:
                    case 5:
                    case 8:
                        continue;
                    default:
                        throw new Exception(String.Format("Незивестная характеристика {0} {1}", chars.Charname,
                            chars.Charcd));
                }
                lcc.Add(c);
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
    #endregion
}
