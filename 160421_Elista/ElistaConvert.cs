using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _160421_Elista
{
    public static class Consts
    {
        public const string ExcelFileName = @"D:\Work\C#\C#Projects\aConverter\160421_Elista\Sources\приложение 3 с изменениями.xlsx";
        public const int MinRecord = 6;
        public const int MaxRecord = 1203;
    }

    public class DeleteAllFiles : ConvertCase
    {
        public DeleteAllFiles()
        {
            ConvertCaseName = "Удалить таблицы для конвертации";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

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

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "Создание счетчиков";
            Position = 20;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(5);

            DataTable dataTable = Utils.ReadExcelFile(Consts.ExcelFileName, "приложение");

            string abonentsNotFound = "";
            var counters = new List<CNV_COUNTER>();
            var cinds = new List<CNV_CNTRSIND>();
            var extorgaccounts = new List<EXTORGACCOUNT>();
            StepStart(Consts.MaxRecord - Consts.MinRecord);
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {

                int maxId = context.ExecuteQuery<int>("SELECT MAX(C.KOD) FROM resourcecounters C", CommandType.Text).First();

                for (int i = Consts.MinRecord - 2; i < Consts.MaxRecord - 2; i++)
                {
                    var abonent = new AbonentRow(dataTable.Rows[i]);

                    if (counters.Any(c => c.LSHET == abonent.Lshet)) continue;

                    var result = context.ExecuteQuery<ABONENT>(
                        String.Format("SELECT * FROM ABONENTS A WHERE A.LSHET = '{0}'", abonent.Lshet),
                        CommandType.Text);

                    if (result.Count == 0)
                    {
                        abonentsNotFound += String.Format("{0}\t{1}\t{2}\r\n", abonent.Lshet, abonent.Address,
                            abonent.FIO);
                        continue;
                    }

                    extorgaccounts.Add(new EXTORGACCOUNT
                    {
                        LSHET = abonent.Lshet,
                        EXTLSHET = abonent.ExtLshet,
                        EXTORGCD = 623010
                    });

                    maxId++;
                    counters.Add(new CNV_COUNTER
                    {
                        LSHET = abonent.Lshet,
                        COUNTERID = maxId.ToString(),
                        SERIALNUM = abonent.CounterNumber,
                        SETUPDATE = new DateTime(2016,4,1),
                        CNTTYPE = 199,
                        CNTNAME = "СВ-15Х",
                    });

                    if (abonent.Indication != null)
                    {
                        cinds.Add(new CNV_CNTRSIND
                        {
                            COUNTERID = maxId.ToString(),
                            OLDIND = abonent.Indication,
                            INDICATION = abonent.Indication,
                            DOCUMENTCD = String.Format("{0}_{1}", maxId, abonent.ExtLshet),
                            INDDATE = new DateTime(2016,4,1),
                        });
                    }

                    Iterate();
                }
            }
            Clipboard.SetText(abonentsNotFound);

            SaveList(counters, 1000);
            SaveList(cinds, 1000);
            SaveList(extorgaccounts, 1000);
        }

        public class AbonentRow
        {
            public int Number { get; set; }
            public string ExtLshet { get; set; }
            public string Lshet { get; set; }
            public string Address { get; set; }
            public int? FlatNumber { get; set; }
            public string Town { get; set; }
            public string Streer { get; set; }
            public string House { get; set; }
            public int? Flat { get; set; }
            public string FIO { get; set; }
            public string CounterNumber { get; set; }
            public decimal? Indication { get; set; }
            public int? PersonsCount { get; set; }

            public AbonentRow(DataRow dataRow)
            {
                Number = Int32.Parse(dataRow[0].ToString().Trim());
                ExtLshet = dataRow[1].ToString().Trim();
                Lshet = dataRow[2].ToString().Trim();
                Address = dataRow[3].ToString().Trim();
                FlatNumber = dataRow.IsNull(4) || String.IsNullOrWhiteSpace(dataRow[4].ToString()) ? null : (int?)Int32.Parse(dataRow[4].ToString().Trim());
                Town = dataRow[6].ToString().Trim();
                Streer = dataRow[7].ToString().Trim();
                House = dataRow[8].ToString().Trim();
                FlatNumber = dataRow.IsNull(9) || String.IsNullOrWhiteSpace(dataRow[9].ToString()) ? null : (int?)Int32.Parse(dataRow[9].ToString().Trim());
                FIO = dataRow[10].ToString().Trim();
                CounterNumber = dataRow.IsNull(11) ? null : dataRow[11].ToString().Trim();
                Indication = dataRow.IsNull(12) || String.IsNullOrWhiteSpace(dataRow[12].ToString()) ? null : (decimal?)Decimal.Parse(dataRow[12].ToString().Trim().Replace('.', ','));
                PersonsCount = dataRow.IsNull(13) || String.IsNullOrWhiteSpace(dataRow[13].ToString()) ? null : (int?)Int32.Parse(dataRow[13].ToString().Trim());
            }
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
            fbm.ExecuteProcedure("CNV$CNV_01000_COUNTERS", new[] { "0" });
            Iterate();
        }
    }
}
