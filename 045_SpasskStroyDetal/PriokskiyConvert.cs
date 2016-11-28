using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using DbfClassLibrary;

namespace _046_Priokskiy
{
    public static class Consts
    {

        public static int CurrentYear = 2016;
        public static int CurrentMonth = 10;
        public const int InsertRecordCount = 1;

        public static List<Record> GetAllRecords(TableManager tmsource)
        {
            var allRecrods = new List<Record>();
            //var files = Directory.GetFiles(aConverter_RootSettings.SourceDbfFilePath);
            var files = new[] { aConverter_RootSettings.SourceDbfFilePath + "\\1503_01_7960_0_missed.dbf" };
            foreach (var file in files)
            {
                string fileName = new FileInfo(file).Name;
                string[] info = fileName.Split('_');
                var fileDate = new DateTime(Int32.Parse(info[0].Substring(0, 2)) + 2000,
                    Int32.Parse(info[0].Substring(2, 2)), 1);
                ushort ducd = UInt16.Parse(info[2]);

                DataTable dt = tmsource.GetDataTable(fileName);
                foreach (DataRow dr in dt.Rows)
                {
                    allRecrods.Add(new Record(dr, fileDate, ducd));
                }
            }
            return allRecrods;
        }
    }

    #region Конвертация
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
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);
            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            var allRecrods = Consts.GetAllRecords(Tmsource);
            allRecrods.Reverse();
            var lca = new List<CNV_ABONENT>();
            StepStart(allRecrods.Count + 1);
            foreach (Record record in allRecrods)
            {
                Iterate();
                if (record.StringNumber > 1 || lca.Any(ab => ab.LSHET == record.Lshet)) continue;

                var a = new CNV_ABONENT
                {
                    LSHET = record.Lshet,
                    DISTKOD = 1,
                    DISTNAME = "РЯЗАНЬ",
                    DUCD = record.Ducd,
                    DUNAME = @"ООО ""ЖКО Приокский""",
                    //RAYONKOD = 1,
                    //RAYONNAME = "Спасский р-н",
                    //PRIM_ = abonentRec.Prim_.Trim(),
                    F = record.F,
                    I = record.I,
                    O = record.O,
                    TOWNSKOD = 1,
                    TOWNSNAME = "РЯЗАНЬ",
                    ULICAKOD = record.StreetId,
                    //ULICANAME = abonentRec.Ulicaname.Trim(),
                    ISDELETED = record.FileDate.Year < 2016 || (record.FileDate.Year == 2016 && record.FileDate.Month < 05) ? 1 : 0,
                    HOUSENO = record.HouseId.ToString(),
                    FLATNO = record.FlatId,
                    KORPUSNO = record.Korpusid,
                    ROOMNO = (short)record.RoomNumber,
                    EXTLSHET = record.Lshet + String.Format("{0:D2}", record.AddressHash)
                };
                lca.Add(a);
            }
            StepFinish();


            StepStart(1);
            int houseMaxId = 80000;
            AbonentRecordUtils.SetUniqueHouseCd(lca, houseMaxId + 1);
            StepFinish();

            SaveList(lca, Consts.InsertRecordCount);
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
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(3);

            BufferEntitiesManager.DropTableData("CNV$CHARS");
            var allRecrods = Consts.GetAllRecords(Tmsource);
            var lcc = new List<CNV_CHAR>();

            StepStart(allRecrods.Count);
            foreach (Record record in allRecrods)
            {
                if (record.StringNumber > 1) continue;

                var cPMG = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.PMG,
                    CHARCD = 3,
                    DATE_ = record.FileDate
                };

                var cVo = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.Vo,
                    CHARCD = 10,
                    DATE_ = record.FileDate
                };

                var cPMP = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.PMP,
                    CHARCD = 12,
                    DATE_ = record.FileDate
                };

                var LivingCount = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.PMG - record.Vo + record.PMP,
                    CHARCD = 1,
                    DATE_ = record.FileDate
                };

                var totalSquare = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = (decimal?)record.TotalSquare,
                    CHARCD = 2,
                    DATE_ = record.FileDate
                };

                var livingSquare = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = (decimal?)record.LivingSquare,
                    CHARCD = 14,
                    DATE_ = record.FileDate
                };

                var tariff = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.Tariff,
                    CHARCD = 20,
                    DATE_ = record.FileDate
                };

                var summa = new CNV_CHAR
                {
                    LSHET = record.Lshet,
                    VALUE_ = record.Tariff * (decimal)record.TotalSquare,
                    CHARCD = 21,
                    DATE_ = record.FileDate
                };


                lcc.Add(cPMG);
                lcc.Add(cVo);
                lcc.Add(cPMP);
                lcc.Add(LivingCount);
                lcc.Add(totalSquare);
                lcc.Add(livingSquare);
                lcc.Add(tariff);
                lcc.Add(summa);
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

    public class ConvertADDChars : ConvertCase
    {
        public ConvertADDChars()
        {
            ConvertCaseName = "ADDCHARS - дополнительные характеристики";
            Position = 40;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(2);

            BufferEntitiesManager.DropTableData("CNV$AADDCHAR");
            var allRecrods = Consts.GetAllRecords(Tmsource);
            allRecrods.Reverse();
            var lcc = new List<CNV_AADDCHAR>();

            StepStart(allRecrods.Count + 1);
            foreach (Record record in allRecrods)
            {
                Iterate();
                if (record.StringNumber > 1 || lcc.Any(ab => ab.LSHET == record.Lshet)) continue;

                var roomsType = new CNV_AADDCHAR
                {
                    LSHET = record.Lshet,
                    ADDCHARCD = 752,
                    VALUE = (record.RoomsType + 1).ToString()
                };

                var housing = new CNV_AADDCHAR
                {
                    LSHET = record.Lshet,
                    ADDCHARCD = 751,
                    VALUE = (record.HousingType + 1).ToString()
                };

                var ownership = new CNV_AADDCHAR
                {
                    LSHET = record.Lshet,
                    ADDCHARCD = 753,
                    VALUE = (record.OwnershipType - 1).ToString()
                };
                if (ownership.VALUE == "0") ownership.VALUE = "7";

                lcc.Add(roomsType);
                lcc.Add(housing);
                lcc.Add(ownership);
            }
            StepFinish();

            StepStart(1);
            SaveList(lcc, Consts.InsertRecordCount);
            StepFinish();
        }
    }

    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "Nachopl - история оплат и начислений";
            Position = 50;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
            tms.Init();

            SetStepsCount(5);

            BufferEntitiesManager.DropTableData("CNV$NACH");
            BufferEntitiesManager.DropTableData("CNV$OPLATA");
            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
            var allRecrods = Consts.GetAllRecords(Tmsource);

            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);

            long[] housesToConvert = { 29700500L, 29700700L, 29701000L, 49803400L, 49803800L, 49804000L, 84203100L };

            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                StepStart(allRecrods.Count + 1);
                for (int i = 0; i < allRecrods.Count; i++)
                {
                    Iterate();
                    int regimcd = 10;
                    string regimname = "Неизвестен";
                    int servicecd = 1002;
                    string servicename = @"Содержание жилья (ООО ""ЖКО Приокский"")";

                    Record record = allRecrods[i];

                    if (!housesToConvert.Contains(record.Adr1)) continue;

                    if (record.StringNumber == 1)
                    {
                        string query = String.Format(
                            "select eo.lshet from extorgaccounts eo where eo.extlshet = '{0}' and eo.extorgcd = 5",
                            record.Lshet);
                        var result = context.ExecuteQuery<string>(query);
                        if (!result.Any() || result.Count > 1)
                            throw new Exception("asdas");
                        record.Lshet = result[0];
                    }

                    if (record.StringNumber == 1)
                    {
                        var ndef = new CNV_NACH
                        {
                            REGIMCD = regimcd,
                            REGIMNAME = regimname,
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            TYPE_ = 0
                        };
                        nm.RegisterNach(ndef, record.Lshet, record.FileDate.Month,
                            record.FileDate.Year, record.NachSum, record.Reculc, record.FileDate,
                            String.Format("N{0}{1}", i, record.Lshet));

                        nm.RegisterBeginSaldo(record.Lshet, record.FileDate.Month, record.FileDate.Year, servicecd,
                            servicename,
                            record.BegSaldo);
                        nm.RegisterEndSaldo(record.Lshet, record.FileDate.Month, record.FileDate.Year, servicecd,
                            servicename,
                            record.EndSaldo);
                    }
                    else
                    {
                        Record prevRecord = allRecrods[i - (record.StringNumber - 1)];
                        record.FileDate = prevRecord.FileDate;
                        record.Lshet = prevRecord.Lshet;
                    }

                    if (record.PaySum != 0)
                    {
                        var odef = new CNV_OPLATA
                        {
                            SERVICECD = servicecd,
                            SERVICENAME = servicename,
                            SOURCECD = 999,
                            SOURCENAME = "Корректировка"
                        };

                        DateTime payTo = record.PayTo ?? record.FileDate;
                        DateTime payDate = record.PayDate ?? record.FileDate;

                        nm.RegisterOplata(odef, record.Lshet, payTo.Month, payTo.Year,
                            record.PaySum, payDate, payDate, String.Format("P{0}{1}", i, record.Lshet));
                    }
                }
            }
            StepFinish();

            SaveList(nm.NachRecords, Consts.InsertRecordCount);
            SaveList(nm.OplataRecords, Consts.InsertRecordCount);
            SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
        }
    }
    #endregion

    #region Перенос данных из временных таблиц
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
            StepStart(2);

            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

            //fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
            //Iterate();
            //fbm.ExecuteProcedure("CNV$CNV_00200_PUNKT");
            //Iterate();
            //fbm.ExecuteProcedure("CNV$CNV_00300_STREET");
            //Iterate();
            //fbm.ExecuteProcedure("CNV$CNV_00400_DISTRICT");
            //Iterate();
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

    public class TransferNachopl : ConvertCase
    {
        public TransferNachopl()
        {
            ConvertCaseName = "Перенос данных о истории оплат и начислений";
            Position = 1070;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            StepStart(6);
            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
                Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture) });
            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
            Iterate();
            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
            StepFinish();
        }
    }
    #endregion

    public class Record
    {
        public ushort StreetId;
        public ushort HouseId;
        public ushort Korpusid;
        public ushort FlatId;
        public ushort RoomNumber;
        public ushort AddressHash;

        /// <summary>
        /// Состояние лицевого счета
        /// </summary>
        public byte LsState;
        /// <summary>
        /// Способ начислений
        /// </summary>
        public byte NachType;
        /// <summary>
        /// Тип помещения
        /// </summary>
        public byte RoomsType;
        /// <summary>
        /// Вид жилого помещения
        /// </summary>
        public byte HousingType;
        /// <summary>
        /// Форма собственности
        /// </summary>
        public byte OwnershipType;

        /// <summary>
        /// Зарегистрированных по месту жительства
        /// </summary>
        public byte? PMG;
        /// <summary>
        /// Временно отсутсвующих
        /// </summary>
        public byte? Vo;
        /// <summary>
        /// Зарегестрированных по месту пребывания
        /// </summary>
        public byte? PMP;

        public double TotalSquare;
        public double LivingSquare;

        public decimal Tariff;
        public decimal BegSaldo;
        public decimal NachSum;
        public decimal Reculc;
        public DateTime? PayTo;
        public DateTime? PayDate;
        public decimal PaySum;
        public decimal EndSaldo;

        public byte StringNumber;
        public byte Bank;

        public string F;
        public string I;
        public string O;

        public string Lshet;
        public DateTime FileDate;
        public ushort Ducd;

        public long Adr1;
        public long Adr2;

        public Record(DataRow dr, DateTime fileDate, ushort ducd)
        {
            try
            {
                Adr1 = Int64.Parse(dr["adr1"].ToString());
                string adr1 = String.Format("{0:D8}", Adr1);
                StreetId = UInt16.Parse(adr1.Substring(0, 3));
                HouseId = UInt16.Parse(adr1.Substring(3, 3));
                Korpusid = UInt16.Parse(adr1.Substring(6, 2));
                Adr2 = Int64.Parse(dr["adr2"].ToString());
                string adr2 = String.Format("{0:D6}", Adr2);
                FlatId = UInt16.Parse(adr2.Substring(0, 3));
                RoomNumber = UInt16.Parse(adr2.Substring(3, 1));
                AddressHash = UInt16.Parse(adr2.Substring(4, 2));

                LsState = Byte.Parse(dr["sostls"].ToString());
                NachType = Byte.Parse(dr["spsnac"].ToString());
                RoomsType = Byte.Parse(dr["tippom"].ToString());
                HousingType = Byte.Parse(dr["vidpom"].ToString());
                OwnershipType = Byte.Parse(dr["formsb"].ToString());

                PMG = /*dr.IsNull("gr_08") ? (byte?)null :*/ Convert.ToByte(Double.Parse(dr["gr_08"].ToString()));
                Vo = /*dr.IsNull("gr_09") ? (byte?)null :*/ Convert.ToByte(Double.Parse(dr["gr_09"].ToString()));
                PMP = /*dr.IsNull("gr_10") ? (byte?)null :*/ Convert.ToByte(Double.Parse(dr["gr_10"].ToString()));

                TotalSquare = Double.Parse(dr["gr_12"].ToString());
                LivingSquare = Double.Parse(dr["gr_14"].ToString());

                Tariff = Decimal.Parse(dr["tarif"].ToString());
                BegSaldo = Decimal.Parse(dr["dv"].ToString()) - Decimal.Parse(dr["kv"].ToString());
                NachSum = Decimal.Parse(dr["nac"].ToString());
                Reculc = Decimal.Parse(dr["izm"].ToString());
                string payTo = String.Format("{0:D4}", Convert.ToUInt16(dr["zam"].ToString()));
                if (payTo != "0000")
                    PayTo = new DateTime(Int32.Parse(payTo.Substring(0, 2)) + 2000, Int32.Parse(payTo.Substring(2, 2)), 1);
                string payDate = String.Format("{0:D6}", Convert.ToUInt32(dr["dat"].ToString()));
                if (payDate != "000000")
                    PayDate = new DateTime(Int32.Parse(payDate.Substring(0, 2)) + 2000, Int32.Parse(payDate.Substring(2, 2)),
                        Int32.Parse(payDate.Substring(4, 2)));
                PaySum = Decimal.Parse(dr["opl"].ToString());
                EndSaldo = Decimal.Parse(dr["di"].ToString()) - Decimal.Parse(dr["ki"].ToString());

                StringNumber = Byte.Parse(dr["numstr"].ToString());
                Bank = Byte.Parse(dr["bank"].ToString());

                string fio = dr["fio"].ToString();
                fio = fio.Trim();
                if (!String.IsNullOrWhiteSpace(fio))
                {
                    string[] splittedFio = fio.Split(' ');
                    if (splittedFio.Length > 2) O = splittedFio[2];
                    if (splittedFio.Length > 1) I = splittedFio[1];
                    F = splittedFio[0];
                }

                Lshet = adr1 + adr2;
                FileDate = fileDate;
                Ducd = ducd;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
