using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _045_SpasskStroyDetal
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\042_Kirici\Sources\Таблица перекодировки.xlsx";

        public static string GetLs(string grkod, string lshet)
        {
            return String.Format("97{0}{1}", String.IsNullOrWhiteSpace(grkod) ? "" : grkod.Substring(3, 4), lshet.Substring(3, 4));
        }

        public static readonly int CurrentMonth = 06;

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
                try
                {
                    abonent.ReadDataRow(dataRow);

                    var a = new CNV_ABONENT
                    {
                        LSHET = Consts.GetLs(abonent.Grkod.Trim(), abonent.Lshet.Trim()),
                        EXTLSHET = String.Format("{0}_{1}", abonent.Grkod.Trim(), abonent.Lshet.Trim()),
                        DISTKOD = (int) abonent.Distkod,
                        DISTNAME = abonent.Distname.Trim(),
                        DUCD = (int) abonent.Ducd,
                        DUNAME = abonent.Duname.Trim(),
                        RAYONKOD = 1,
                        RAYONNAME = "Спасский р-н",
                        PRIM_ = abonent.Prim_.Trim(),
                        F = abonent.F.Trim(),
                        I = abonent.I.Trim(),
                        O = abonent.O.Trim(),
                        POSTINDEX = abonent.Postindex.Trim(),
                        TOWNSKOD = (int) abonent.Townskod,
                        TOWNSNAME = abonent.Townsname.Trim(),
                        ULICAKOD = (int) abonent.Ulicakod,
                        ULICANAME = abonent.Ulicaname.Trim(),
                        ISDELETED = (int) abonent.Isdeleted,
                        PHONENUM = abonent.Phonenum
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

                    string kvartira = abonent.Kvartira.Trim();
                    matches = regex.Matches(kvartira);
                    if (matches.Count > 0)
                    {
                        var groups = matches[0].Groups;
                        if (groups.Count > 2) a.FLATPOSTFIX = groups[2].Value;
                        if (groups.Count > 1)
                        {
                            int flatno;
                            if (Int32.TryParse(groups[1].Value, out flatno)) a.FLATNO = Convert.ToInt32(groups[1].Value);
                            else a.FLATPOSTFIX = groups[0].Value;
                        }
                    }

                    if (a.HOUSEPOSTFIX != null && a.HOUSEPOSTFIX.Length > 10) a.HOUSEPOSTFIX = a.HOUSEPOSTFIX.Substring(0, 10);
                    lca.Add(a);
                    Iterate();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            StepFinish();

            StepStart(1);
            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
        }
    }

#endregion
}
