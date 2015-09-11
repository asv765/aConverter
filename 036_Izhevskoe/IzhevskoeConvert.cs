using System.Globalization;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;

namespace _036_Izhevskoe
{
    public static class Consts
    {
        public static string GetLs(long intls)
        {
            return String.Format("82{0:D6}", intls);
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

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

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

        public uint CurrentLshet;

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");
            DataTable dt = Tmsource.GetDataTable("F_LITS");
            // DataTable dt = Tmsource.ExecuteQuery("SELECT N_Lits,  ")
            var lca = new List<CNV_ABONENT>();

            StepStart(dt.Rows.Count);
            var fLits = new F_litsRecord();
            foreach (DataRow dataRow in dt.Rows)
            {
                fLits.ReadDataRow(dataRow);
                var a = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(fLits.N_lits)),
                    EXTLSHET = fLits.N_lits.ToString(CultureInfo.InvariantCulture),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 1,
                    RAYONNAME = "Спасский район",
                    TOWNSKOD = 1,
                    TOWNSNAME = "Ижевское",
                    DUCD = 1,
                    DUNAME = "МУП 'Ижевское коммунальное хозяйство'",
                    ULICAKOD = (int) fLits.Kst,
                    ULICANAME = fLits.Street,
                    HOUSENO = fLits.N_dom.ToString(CultureInfo.InvariantCulture),
                    FLATNO = (int) fLits.N_kw,
                    ISDELETED = 0
                };
                // if (!String.IsNullOrEmpty(fLits.Prim)) a.PRIM_ = fLits.Prim;
                if (fLits.Drob != 0) a.HOUSEPOSTFIX += "/" + fLits.Drob.ToString(CultureInfo.InvariantCulture);
                if (!String.IsNullOrWhiteSpace(fLits.Lit))
                {
                    if (!String.IsNullOrEmpty(a.HOUSEPOSTFIX)) a.HOUSEPOSTFIX += " ";
                    a.HOUSEPOSTFIX += "лит." + fLits.Lit.ToString(CultureInfo.InvariantCulture);
                }
                string[] fio = Utils.SplitFio(fLits.Fio);
                a.F = fio[0];
                a.I = fio[1];
                a.O = fio[2];

                lca.Add(a);
             
                Iterate();
            }

            StepFinish();


            StepStart(1);
            using (var acem = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                acem.Add(lca);
                acem.SaveChanges();
            }
            Iterate();
            StepFinish();
        }


    }
}
