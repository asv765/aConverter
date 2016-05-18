using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _044_TeplComp
{
    public class Consts
    {
        public static string GetLs(long intls)
        {
            string s = String.Format("96{0:D6}", intls);
            return s.Substring(0, 8);
        }

        public static readonly int CurrentMonth = 05;

        public static readonly int CurrentYear = 2016;

        public const string SvodTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Копия svod_lMZxP 04.05.2016.xlsx";

        public const string SvodSheetName = "6618110";
        public const string OborotTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\1.xls";

        public const string OborotSheetName = "662209 (2)";

        public const int InsertRecordCount = 1;
    }

    public class RecordSvod
    {
        public string TownName { get; set; }
        public string Lshet { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public int? RegCount { get; set; }
        public int? LiveCount { get; set; }
        public decimal? Square { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string HousePostrifx { get; set; }
        public int? FlatNumber { get; set; }
        public string FlatPostrifx { get; set; }
        public ServiceRecord Otoplenie { get; set; }
        public ServiceRecord GVS { get; set; }
        public ServiceRecord HVS { get; set; }
        public ServiceRecord Vodootvedenie { get; set; }

        public RecordSvod(DataRow row)
        {
            TownName = row[0].ToString();
            Lshet = row[1].ToString();

            var fioRegex = new Regex(@"([а-я]+)[^а-я]*([а-я]*)[^а-я]*([а-я]*)", RegexOptions.IgnoreCase);
            var fioGroups = fioRegex.Match(row[2].ToString()).Groups;
            if (fioGroups[1].Success) F = fioGroups[1].Value;
            if (fioGroups[2].Success) I = fioGroups[2].Value;
            if (fioGroups[3].Success) O = fioGroups[3].Value;

            RegCount = String.IsNullOrWhiteSpace(row[3].ToString()) ? (int?) null : Int32.Parse(row[3].ToString());
            LiveCount = String.IsNullOrWhiteSpace(row[4].ToString()) ? (int?) null : Int32.Parse(row[4].ToString());
            Square = String.IsNullOrWhiteSpace(row[5].ToString()) ? (decimal?) null : Decimal.Parse(row[5].ToString());

            StreetName = String.IsNullOrWhiteSpace(row[6].ToString()) ? "" : row[6].ToString();
            var houseRegex = new Regex(@"(\d+)(.*)");
            var houseGroups = houseRegex.Match(row[7].ToString()).Groups;
            if (houseGroups[1].Success) HouseNumber = Int32.Parse(houseGroups[1].Value);
            if (houseGroups[2].Success) HousePostrifx = houseGroups[2].Value;

            var flatGroups = houseRegex.Match(row[8].ToString()).Groups;
            if (flatGroups[1].Success) FlatNumber = Int32.Parse(flatGroups[1].Value);
            if (flatGroups[2].Success) FlatPostrifx = flatGroups[2].Value;

            try
            {
                Otoplenie = new ServiceRecord(row, 11, "Отопление");
            }
            catch (MissingFieldException)
            {
                Otoplenie = null;
            }
            try
            {
                GVS = new ServiceRecord(row, 22, "ГВС");
            }
            catch (MissingFieldException)
            {
                GVS = null;
            }
            try
            {
                HVS = new ServiceRecord(row, 32, "ХВС");
            }
            catch (MissingFieldException)
            {
                HVS = null;
            }
            try
            {
                Vodootvedenie = new ServiceRecord(row, 44, "ВОДООТВЕДЕНИЕ");
            }
            catch (MissingFieldException)
            {
                Vodootvedenie = null;
            }
        }

        public class ServiceRecord
        {
            public string ServiceName { get; set; }
            public decimal Nach { get; set; }
            public decimal Pere { get; set; }
            public decimal Opl { get; set; }
            public decimal EndSaldo { get; set; }
            public decimal BeginSaldo { get; set; }

            public ServiceRecord(DataRow row, int startIndex, string name)
            {
                ServiceName = name;
                Nach = String.IsNullOrWhiteSpace(row[1 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[1 + startIndex].ToString());
                Pere = String.IsNullOrWhiteSpace(row[3 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[3 + startIndex].ToString());
                Opl = String.IsNullOrWhiteSpace(row[4 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[4 + startIndex].ToString());
                EndSaldo = String.IsNullOrWhiteSpace(row[7 + startIndex].ToString())
                    ? 0
                    : Decimal.Parse(row[7 + startIndex].ToString());
                BeginSaldo = EndSaldo - Nach - Pere + Opl;

                if (Nach == 0 && Pere == 0 && Opl == 0 && EndSaldo == 0) throw new MissingFieldException();
            }
        }
    }

    public class RecordOborot
    {
        public string Lshet { get; set; }
        public string Address { get; set; }
        public string Rayon { get; set; }
        public decimal BeginSaldo { get; set; }
        public decimal Nach { get; set; }
        public decimal Opl { get; set; }
        public decimal EndSaldo { get; set; }

        public RecordOborot(DataRow row)
        {
            Lshet = row[1].ToString();
            Address = String.IsNullOrWhiteSpace(row[2].ToString()) ? null : row[2].ToString();
            Rayon = String.IsNullOrWhiteSpace(row[3].ToString()) ? null : row[3].ToString();
            BeginSaldo = Decimal.Parse(row[4].ToString());
            BeginSaldo = Decimal.Parse(row[5].ToString());
            BeginSaldo = Decimal.Parse(row[6].ToString());
            BeginSaldo = Decimal.Parse(row[7].ToString());
        }

        public static RecordOborot FindRecord(DataTable oborotTable, string lshet)
        {
            foreach (DataRow dataRow in oborotTable.Rows)
            {
                var record = new RecordOborot(dataRow);
                if (record.Lshet == lshet) return record;
            }
            return null;
        }
    }

    public class SvodConvert : ConvertCase
    {
        public SvodConvert()
        {
            ConvertCaseName = "Конвертация сводного отчета по потребителям";
            Position = 999;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            DataTable svodTable = Utils.ReadExcelFile(
                @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\Копия svod_lMZxP 04.05.2016.xlsx", "6618110");
            DataTable oborotTable = Utils.ReadExcelFile(
                @"D:\Work\C#\C#Projects\aConverter\044_TeplComp\Sources\1.xls", "662209 (2)");


            StepStart(9999);
            for (int i = 5; i < svodTable.Rows.Count; i++)
            {
                try
                {
                    if (svodTable.Rows[i][1].ToString() == "000192")
                    {
                        int a = 10;
                    }
                    var recordSvod = new RecordSvod(svodTable.Rows[i]);
                    var recordOborot = RecordOborot.FindRecord(oborotTable, recordSvod.Lshet);


                }
                catch (Exception e)
                {
                    throw e;
                }
                Iterate();
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
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            DataTable svodTable = Utils.ReadExcelFile(Consts.SvodTableFileName, Consts.SvodSheetName);

            var la = new List<CNV_ABONENT>();
            StepStart(svodTable.Rows.Count);
            for (int i = 0; i < svodTable.Rows.Count - 1; i++)
            {
                if (i < 5) continue;

                var recordSvod = new RecordSvod(svodTable.Rows[i]);
                if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(recordSvod.Lshet)),
                    EXTLSHET = recordSvod.Lshet,
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 2,
                    RAYONNAME = "Рязанский район",
                    DUCD = 1,
                    DUNAME = "Тепловая компания Рязанского района",
                    ISDELETED = 0,
                    F = recordSvod.F,
                    I = recordSvod.I,
                    O = recordSvod.O,
                    TOWNSNAME = recordSvod.TownName,
                    ULICANAME = recordSvod.StreetName,
                    HOUSENO = recordSvod.HouseNumber == null ? null : recordSvod.HouseNumber.ToString(),
                    HOUSEPOSTFIX = recordSvod.HousePostrifx,
                    FLATNO = recordSvod.FlatNumber,
                    FLATPOSTFIX = recordSvod.FlatPostrifx
                };
                la.Add(abonent);
                Iterate();
            }
            StepFinish();

            StepStart(3);
            AbonentRecordUtils.SetUniqueTownskod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueUlicakod(la, 0);
            Iterate();
            AbonentRecordUtils.SetUniqueHouseCd(la, 0);
            StepFinish();

            SaveList(la, Consts.InsertRecordCount);
        }
    }

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
            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CHARS");

            DataTable svodTable = Utils.ReadExcelFile(Consts.SvodTableFileName, Consts.SvodSheetName);
            var lcc = new List<CNV_CHAR>();
            StepStart(svodTable.Rows.Count);
            for (int i = 0; i < svodTable.Rows.Count - 1; i++)
            {
                if (i < 5) continue;

                var recordSvod = new RecordSvod(svodTable.Rows[i]);
                if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                if (recordSvod.LiveCount != null)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(recordSvod.Lshet)),
                        CHARCD = 1,
                        VALUE_ = recordSvod.LiveCount,
                        DATE_ = new DateTime(2016, 06, 01)
                    };
                    lcc.Add(c);
                }
                if (recordSvod.Square != null)
                {
                    var c = new CNV_CHAR
                    {
                        LSHET = Consts.GetLs(Convert.ToInt64(recordSvod.Lshet)),
                        CHARCD = 2,
                        VALUE_ = recordSvod.Square,
                        DATE_ = new DateTime(2016, 06, 01)
                    };
                    lcc.Add(c);
                }
                Iterate();
            }
            StepFinish();

            StepStart(1);
            lcc = CharsRecordUtils.ThinOutList(lcc);
            StepFinish();

            SaveList(lcc, Consts.InsertRecordCount);
        }
    }

    public class ConvertNach : ConvertCase
    {
        public ConvertNach()
        {
            ConvertCaseName = "NACHOPL - данные истории начислений. Первая половина";
            Position = 70;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(9999);

            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACH");

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            DataTable svodTable = Utils.ReadExcelFile(Consts.SvodTableFileName, Consts.SvodSheetName);
            DataTable oborotTable = Utils.ReadExcelFile(Consts.OborotTableFileName, Consts.OborotSheetName);
            long recno = 0;
            StepStart(svodTable.Rows.Count);
            for (int i = 0; i < svodTable.Rows.Count - 1; i++)
            {
                try
                {
                    if (i < 5) continue;
                    var recordSvod = new RecordSvod(svodTable.Rows[i]);
                    if (recordSvod.Lshet == "000190" || recordSvod.Lshet == "010280") continue;
                    //var recordOborot = RecordOborot.FindRecord(oborotTable, recordSvod.Lshet);
                    string lshet = Consts.GetLs(Convert.ToInt64(recordSvod.Lshet));
                    {
                        if (recordSvod.Otoplenie != null)
                            RigesterNachopl(recordSvod.Otoplenie, 3, "Отопление", lshet, nm);
                        if (recordSvod.GVS != null)
                            RigesterNachopl(recordSvod.GVS, 5, "Горячая вода", lshet, nm);
                        if (recordSvod.HVS != null)
                            RigesterNachopl(recordSvod.HVS, 4, "Хол. водоснабжение", lshet, nm);
                        if (recordSvod.Vodootvedenie != null)
                            RigesterNachopl(recordSvod.Vodootvedenie, 8, "Водоотведение", lshet, nm);
                        
                        Iterate();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            StepFinish();

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }

        private static int recno = 0;
        private void RigesterNachopl(RecordSvod.ServiceRecord record, int servicecd, string servicename,
            string lshet ,NachoplManager nm)
        {
            recno++;
            var ndef = new CNV_NACH
            {
                //VOLUME = ,
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                TYPE_ = 0,
                SERVICECD = servicecd,
                SERVICENAME = servicename
            };
            nm.RegisterNach(ndef, lshet, 03, 2016, record.Nach, record.Pere,
                new DateTime(2016, 03, 01), String.Format("{0}_{1}", lshet, recno));

            recno++;
            var odef = new CNV_OPLATA
            {
                SERVICECD = servicecd,
                SERVICENAME = servicename,
                SOURCECD = 17,
                SOURCENAME = "Касса"
            };
            nm.RegisterOplata(odef, lshet, 03, 2016, record.Opl,
                new DateTime(2016, 03, 01), new DateTime(2016, 03, 01),
                String.Format("{0}_{1}", lshet, recno));

            nm.RegisterBeginSaldo(lshet, 03, 2016, servicecd, servicename, record.BeginSaldo);
            nm.RegisterEndSaldo(lshet, 03, 2016, servicecd, servicename, record.EndSaldo);
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

    public class TransferNachisl : ConvertCase
    {
        public TransferNachisl()
        {
            ConvertCaseName = "Перенос данных о начислениях";
            Position = 1070;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
        }
    }

    public class TransferOplata : ConvertCase
    {
        public TransferOplata()
        {
            ConvertCaseName = "Перенос данных об оплате";
            Position = 1050;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(2);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            Iterate();
        }
    }

    public class TransferSaldo : ConvertCase
    {
        public TransferSaldo()
        {
            ConvertCaseName = "Перенос данных о сальдо";
            Position = 1060;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
                Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
        }
    }

    public class TransferPererashet : ConvertCase
    {
        public TransferPererashet()
        {
            ConvertCaseName = "Перерасчет";
            Position = 1080;
            IsChecked = false;

        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            Iterate();
        }
    }
}
