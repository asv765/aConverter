using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _040_Skopin
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\040_Skopin\Sources\Таблица перекодировки.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("89{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 02;

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
            IsChecked = true;
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

                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(abonent.Lshet)),
                    EXTLSHET = abonent.Lshet.Trim(),
                    ISDELETED = Convert.ToInt32(abonent.Isdeleted),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    DUCD = Convert.ToInt32(abonent.Ducd),
                    DUNAME = abonent.Duname,
                    RAYONKOD = 1,
                    RAYONNAME = "Рязанский район",
                    TOWNSNAME = String.IsNullOrWhiteSpace(abonent.Townsname) ? Consts.UnknownTown : abonent.Townsname.Trim(),
                    ULICANAME = abonent.Ulicaname.Trim().Replace(" ул.", ""),
                    F = abonent.F.Trim(),
                    I = abonent.I.Trim(),
                    O = abonent.O.Trim(),
                    PRIM_ = abonent.Prim
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

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(lca, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

    #endregion
}
