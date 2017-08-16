//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using aConverterClassLibrary;
//using aConverterClassLibrary.Class;
//using aConverterClassLibrary.RecordsDataAccessORM;
//using aConverterClassLibrary.RecordsDataAccessORM.Utils;
//using DbfClassLibrary;
//using FirebirdSql.Data.FirebirdClient;
//using static _049_Zheu18.Consts;

//namespace _049_Zheu18
//{
//    //TODO сделать наследование в классах записей. Сделать метод, получающий все данные и пишущий инфу в запись
//    public static class Consts
//    {
//        public const int InsertRecordCount = 1000;

//        public static Dictionary<string, string> ConvertedLs;

//        public static string[] GetFiles(ServiceType? services = null)
//        {
//            var allFiles = Directory.GetFiles(aConverter_RootSettings.SourceDbfFilePath).Select(fp => new FileInfo(fp));
//            if (services.Value.HasFlag(ServiceType.Base))
//                allFiles = allFiles.Where(f =>
//                {
//                    var fileInfo = new KvcFileInfo(f);
//                    return new[] {1, 3, 5, 41, 43, 45, 57, 73, 75}.Contains(fileInfo.ServiceId) && !fileInfo.IsCounter;
//                });
//            if (services.Value.HasFlag(ServiceType.Cnt))
//                allFiles = allFiles.Where(f =>
//                {
//                    var fileInfo = new KvcFileInfo(f);
//                    return new[] {3, 5}.Contains(fileInfo.ServiceId) && fileInfo.IsCounter;
//                });
//            if (services.Value.HasFlag(ServiceType.Vodootv))
//                allFiles = allFiles.Where(f =>
//                {
//                    var fileInfo = new KvcFileInfo(f);
//                    return new[] {17}.Contains(fileInfo.ServiceId) && !fileInfo.IsCounter;
//                });
//            if (services.Value.HasFlag(ServiceType.OdnNach))
//                allFiles = allFiles.Where(f =>
//                {
//                    var fileInfo = new KvcFileInfo(f);
//                    return new[] {83, 85}.Contains(fileInfo.ServiceId) && !fileInfo.IsCounter;
//                });
//            if (services.Value.HasFlag(ServiceType.OdnCnt))
//                allFiles = allFiles.Where(f =>
//                {
//                    var fileInfo = new KvcFileInfo(f);
//                    return new[] {83, 85}.Contains(fileInfo.ServiceId) && fileInfo.IsCounter;
//                });

//            return allFiles.Select(f => f.FullName).ToArray();
//        }

//        public static RecordBase[] GetAllRecords(string[] files)
//        {
//            var tmpRecords = new List<RecordBase>();
//            foreach (var file in files)
//            {
//                var fileInfo = new KvcFileInfo(new FileInfo(file));
//                var tms = new TableManager(aConverter_RootSettings.SourceDbfFilePath);
//                tms.Init();
//                DataTable dt = tms.GetDataTable(file);
//                foreach (DataRow dr in dt.Rows)
//                {
//                    var record = new RecordBase(dr, file)
//                    {
//                        FileDu = fileInfo.Du,
//                        FileYear = fileInfo.Year,
//                        FileMonth = fileInfo.Month,
//                        FileName = file,
//                        FileServiceId = fileInfo.ServiceId
//                    };
//                    tmpRecords.Add(record);
//                }
//            }
//            tmpRecords.Reverse();
//            return tmpRecords.ToArray();
//        }

//        public static RecordBase[] AllRecords;

//        public const int CurrentYear = 2017;
//        public const int CurrentMonth = 1;
//    }

//    public class CreateAllFiles : ConvertCase
//    {
//        public CreateAllFiles()
//        {
//            ConvertCaseName = "Создать таблицы для конвертации";
//            Position = 10;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);

//            BufferEntitiesManager.DropAllProcedures();
//            BufferEntitiesManager.DropAllEntities();
//            BufferEntitiesManager.CreateAllEntities();
//            BufferEntitiesManager.CreateAllProcedures();

//            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
//            Iterate();
//        }
//    }

//    public class PrepareLs : ConvertCase
//    {
//        public PrepareLs()
//        {
//            ConvertCaseName = "Формирование лицевых счетов";
//            Position = 11;
//            IsChecked = true;
//        }

//        public override void DoConvert()
//        {
//            ConvertedLs = new Dictionary<string, string>();
//            AllRecords = GetAllRecords(GetFiles(ServiceType.Base));
//            AllRecords = AllRecords.Where(r => r.FileServiceId == 1).ToArray(); //TODO убрать
//            int i = 0;
//            foreach (var record in AllRecords)
//            {
//                string ls;
//                if (!ConvertedLs.TryGetValue(record.LsKvc, out ls))
//                    ConvertedLs.Add(record.LsKvc, String.Format("98{0:D6}", ++i));
//            }
//        }
//    }

//    public class ConvertAbonent : ConvertCase
//    {
//        public ConvertAbonent()
//        {
//            ConvertCaseName = "ABONENT - данные об абонентах";
//            Position = 20;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            if (ConvertedLs == null) throw new Exception("Необходимо заполнить список лицевых счетов");

//            var duDictionary = new Dictionary<int, string>
//            {
//                {7230, "ООО \"ГУЖК Октябрьского р-на г.Рязани\""},
//                {1330, "ООО\"РГМЭК\""},
//                {0015, "МП \"Водоканал города Рязани\""},
//                {5160, "ООО \"ЖЭУ-18\""},
//                {7480, "ООО \"ЖЭУ-18\""},
//                {7300, "ООО \"ЖЭУ-18+\""},
//            };
//            var streetDictionary = new Dictionary<int, string>()
//            {
//                {19, "улица Бабушкина"},
//                {86, "улица Гагарина"},
//                {105, "улица Горького"},
//                {170, "улица Есенина"},
//                {179, "1-я Железнодорожная улица"},
//                {258, "улица Космодемьянской"},
//                {299, "Куйбышевское шоссе"},
//                {348, "улица Ломоносова"},
//                {373, "улица Матросова"},
//                {460, "Новая улица"},
//                {593, "улица Пушкина"},
//                {603, "улица Разина"},
//                {610, "улица Рытикова"},
//                {632, "Северный переулок"},
//                {641, "улица Семашко"},
//                {692, "Театральная площадь"},
//                {693, "Телевизионная улица"},
//                {710, "Трудовая улица"},
//                {723, "улица Урицкого"},
//                {750, "улица Халтурина"},
//                {773, "улица Циолковского"},
//                {853, "Южный переулок"},
//                {854, "улица Юннатов"},
//                {863, "проезд Яблочкова"},
//            };

//            SetStepsCount(2);
//            var lca = new List<CNV_ABONENT>();
//            var lcaD = new Dictionary<string, CNV_ABONENT>();
//            StepStart(AllRecords.Length + 1);
//            foreach (var record in AllRecords)
//            {
//                Iterate();
//                if (record.NumStr > 1 || lcaD.ContainsKey(record.LsKvc)) continue;

//                //if (!duDictionary.ContainsKey(record.FileDu)) throw new Exception("Не найден ДУ " + record.FileDu);
//                //if (!ConvertedLs.ContainsKey(record.LsKvc)) throw new Exception("Не найден ЛС " + record.LsKvc);
//                //if (!streetDictionary.ContainsKey(record.StreetId)) throw new Exception("Не найден StreetId " + record.StreetId);

//                var a = new CNV_ABONENT
//                {
//                    LSHET = ConvertedLs[record.LsKvc],
//                    DUCD = record.FileDu,
//                    DUNAME = duDictionary[record.FileDu],
//                    RAYONKOD = 1,
//                    RAYONNAME = "Рязанская область",
//                    TOWNSKOD = 1,
//                    TOWNSNAME = "Рязань",
//                    ULICANAME = streetDictionary[record.StreetId],
//                    HOUSENO = record.HouseId.ToString(),
//                    FLATNO = record.FlatId,
//                    KORPUSNO = record.Korpusid,
//                    ROOMNO = (short)record.RoomNumber,
//                    ISDELETED = 0,
//                    EXTLSHET = record.LsKvc,
//                };
//                if (!String.IsNullOrWhiteSpace(record.Fio))
//                {
//                    string[] splittedFio = record.Fio.Split(' ');
//                    if (splittedFio.Length > 2) a.O = splittedFio[2];
//                    if (splittedFio.Length > 1) a.I = splittedFio[1];
//                    a.F = splittedFio[0];
//                }
//                lcaD.Add(record.LsKvc, a);
//                lca.Add(a);
//            }

//            AbonentRecordUtils.SetUniqueUlicakod(lca, 0);
//            AbonentRecordUtils.SetUniqueHouseCd(lca, 0);

//            SaveList(lca, InsertRecordCount);
//        }
//    }

//    public class ConvertChars : ConvertCase
//    {
//        public ConvertChars()
//        {
//            ConvertCaseName = "CHARS - данные о количественных характеристиках";
//            Position = 30;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            if (ConvertedLs == null) throw new Exception("Необходимо заполнить список лицевых счетов");
//            BufferEntitiesManager.DropTableData("CNV$CHARS");
//            SetStepsCount(3);

//            var lcc = new List<CNV_CHAR>();
//            StepStart(AllRecords.Length);
//            foreach (var record in AllRecords)
//            {
//                if (record.NumStr > 1) continue;

//                string lshet = ConvertedLs[record.LsKvc];
//                var fileDate = new DateTime(record.FileYear, record.FileMonth, 1);

//                var cPMG = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.PmgCount,
//                    CHARCD = 3,
//                    DATE_ = fileDate
//                };

//                var cVo = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.VoCount,
//                    CHARCD = 10,
//                    DATE_ = fileDate
//                };

//                var cPMP = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.PmpCount,
//                    CHARCD = 12,
//                    DATE_ = fileDate
//                };

//                var LivingCount = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.PmgCount - record.VoCount + record.PmpCount,
//                    CHARCD = 1,
//                    DATE_ = fileDate
//                };

//                var totalSquare = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = (decimal?)record.TotalSquare,
//                    CHARCD = 2,
//                    DATE_ = fileDate
//                };

//                var livingSquare = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.LivingSquare,
//                    CHARCD = 14,
//                    DATE_ = fileDate
//                };

//                var tariff = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.Tarif,
//                    CHARCD = 20,
//                    DATE_ = fileDate
//                };

//                var summa = new CNV_CHAR
//                {
//                    LSHET = lshet,
//                    VALUE_ = record.Tarif * record.TotalSquare,
//                    CHARCD = 21,
//                    DATE_ = fileDate
//                };


//                lcc.Add(cPMG);
//                lcc.Add(cVo);
//                lcc.Add(cPMP);
//                lcc.Add(LivingCount);
//                lcc.Add(totalSquare);
//                lcc.Add(livingSquare);
//                lcc.Add(tariff);
//                lcc.Add(summa);
//                Iterate();
//            }
//            StepFinish();

//            StepStart(1);
//            lcc = CharsRecordUtils.ThinOutList(lcc);
//            StepFinish();

//            StepStart(1);
//            SaveList(lcc, Consts.InsertRecordCount);
//            StepFinish();
//        }
//    }

//    public class ConvertADDChars : ConvertCase
//    {
//        public ConvertADDChars()
//        {
//            ConvertCaseName = "ADDCHARS - дополнительные характеристики";
//            Position = 40;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            if (ConvertedLs == null) throw new Exception("Необходимо заполнить список лицевых счетов");

//            SetStepsCount(3);
//            BufferEntitiesManager.DropTableData("CNV$AADDCHAR");

//            var lccD = new Dictionary<string, string>();
//            var lcc = new List<CNV_AADDCHAR>();
//            StepStart(AllRecords.Length);
//            foreach (var record in AllRecords)
//            {
//                Iterate();
//                if (record.NumStr > 1 || lccD.ContainsKey(record.LsKvc)) continue;
//                string lshet = ConvertedLs[record.LsKvc];
//                var roomsType = new CNV_AADDCHAR
//                {
//                    LSHET = lshet,
//                    ADDCHARCD = 752,
//                    VALUE = ((int)(record.PlacementType) + 1).ToString()
//                };

//                var housing = new CNV_AADDCHAR
//                {
//                    LSHET = lshet,
//                    ADDCHARCD = 751,
//                    VALUE = ((int)(record.LivingPlacementType) + 1).ToString()
//                };

//                var ownership = new CNV_AADDCHAR
//                {
//                    LSHET = lshet,
//                    ADDCHARCD = 753,
//                    VALUE = ((int)(record.OwnershipType) - 1).ToString()
//                };
//                if (ownership.VALUE == "0") ownership.VALUE = "7";

//                lcc.Add(roomsType);
//                lcc.Add(housing);
//                lcc.Add(ownership);
//                lccD.Add(record.LsKvc, lshet);
//            }
//            StepFinish();

//            StepStart(1);
//            SaveList(lcc, Consts.InsertRecordCount);
//            StepFinish();
//        }
//    }

//    public class ConvertNachopl : ConvertCase
//    {
//        public ConvertNachopl()
//        {
//            ConvertCaseName = "Nachopl - история оплат и начислений";
//            Position = 50;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            if (ConvertedLs == null) throw new Exception("Необходимо заполнить список лицевых счетов");
//            BufferEntitiesManager.DropTableData("CNV$NACH");
//            BufferEntitiesManager.DropTableData("CNV$OPLATA");
//            BufferEntitiesManager.DropTableData("CNV$NACHOPL");
//            SetStepsCount(4);
//            var nm = new NachoplManager(NachoplCorrectionType.Не_корректировать_сальдо);
//            string currentLs = null;
//            StepStart(AllRecords.Length);
//            AllRecords = AllRecords.Reverse().ToArray();
//            int j = 0;
//            foreach (var record in AllRecords)
//            {
//                Iterate();
//                int regimcd = 10;
//                string regimname = "Неизвестен";
//                int servicecd = 2;
//                string servicename = @"Содержание жилья";
//                if (record.NumStr == 1)
//                {
//                    currentLs = ConvertedLs[record.LsKvc];
//                    var ndef = new CNV_NACH
//                    {
//                        REGIMCD = regimcd,
//                        REGIMNAME = regimname,
//                        SERVICECD = servicecd,
//                        SERVICENAME = servicename,
//                        TYPE_ = 0
//                    };
//                    nm.RegisterNach(ndef, currentLs, record.FileMonth,
//                        record.FileYear, record.NachSum, record.RecalcSum, new DateTime(record.FileYear, record.FileMonth, 1),
//                        //String.Format("N{0}-{1}-{2}", record.LsKvc, new DateTime(record.FileYear, record.FileMonth, 1).ToString("yyyyMM"), record.NumStr)
//                        (++j).ToString());
//                    nm.RegisterBeginSaldo(currentLs, record.FileMonth, record.FileYear, servicecd, servicename,
//                        record.BeginSaldo);
//                    nm.RegisterEndSaldo(currentLs, record.FileMonth, record.FileYear, servicecd, servicename,
//                        record.EndSaldo);
//                }
//                if (record.PaySum != 0)
//                {
//                    var odef = new CNV_OPLATA
//                    {
//                        SERVICECD = servicecd,
//                        SERVICENAME = servicename,
//                        SOURCECD = 1,
//                        SOURCENAME = "Касса"
//                    };

//                    if (record.PayYear == null) throw new Exception("Год оплаты нулл");
//                    if (record.PayMonth == null) throw new Exception("Месяц оплаты нулл");
//                    if (record.PayDate == null) throw new Exception("Дата оплаты нулл");

//                    //DateTime payTo = record.PayTo ?? record.FileDate;
//                    //DateTime payDate = record.PayDate ?? record.FileDate;

//                    nm.RegisterOplata(odef, currentLs, (int)record.PayMonth, (int)record.PayYear,
//                        record.PaySum, (DateTime)record.PayDate, (DateTime)record.PayDate,
//                        //String.Format("P{0}-{1}-{2}", record.LsKvc, new DateTime((int)record.PayYear, (int)record.PayMonth, 1).ToString("yyyyMM") ,record.NumStr)
//                        (++j).ToString());
//                }
//            }
//            StepFinish();

//            StepStart((nm.NachRecords.Count + InsertRecordCount + 1) / InsertRecordCount);
//            int counter = 0;
//            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
//            {
//                fbc.Open();
//                var fbt = fbc.BeginTransaction();
//                var command = fbc.CreateCommand();
//                command.Transaction = fbt;
//                for (int i = 0; i < nm.NachRecords.Count; i++)
//                {
//                    try
//                    {
//                        command.CommandText = nm.NachRecords[i].InsertSQL;
//                        command.ExecuteNonQuery();
//                    }
//                    catch (Exception ex)
//                    {

//                    }
//                    if ((++counter % InsertRecordCount) == 0)
//                    {
//                        fbt.CommitRetaining();
//                        Iterate();
//                    }
//                }
//                fbt.Commit();
//            }
//            StepFinish();

//            StepStart((nm.OplataRecords.Count + InsertRecordCount + 1) / InsertRecordCount);
//            counter = 0;
//            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
//            {
//                fbc.Open();
//                var fbt = fbc.BeginTransaction();
//                var command = fbc.CreateCommand();
//                command.Transaction = fbt;
//                for (int i = 0; i < nm.OplataRecords.Count; i++)
//                {
//                    command.CommandText = nm.OplataRecords[i].InsertSQL;
//                    command.ExecuteNonQuery();
//                    if ((++counter % InsertRecordCount) == 0)
//                    {
//                        fbt.CommitRetaining();
//                        Iterate();
//                    }
//                }
//                fbt.Commit();
//            }
//            StepFinish();

//            StepStart((nm.NachoplRecords.Count + InsertRecordCount + 1) / InsertRecordCount);
//            counter = 0;
//            using (var fbc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
//            {
//                fbc.Open();
//                var fbt = fbc.BeginTransaction();
//                var command = fbc.CreateCommand();
//                command.Transaction = fbt;
//                foreach (CNV_NACHOPL nor in nm.NachoplRecords.Values)
//                {
//                    command.CommandText = nor.InsertSQL;
//                    command.ExecuteNonQuery();
//                    if ((++counter % InsertRecordCount) == 0)
//                    {
//                        fbt.CommitRetaining();
//                        Iterate();
//                    }
//                }
//                fbt.Commit();
//            }
//            StepFinish();
//            //nm.SaveNachRecords(aConverter_RootSettings.FirebirdStringConnection);
//            //nm.SaveOplataRecords(aConverter_RootSettings.FirebirdStringConnection);
//            //nm.SaveNachoplRecords(aConverter_RootSettings.FirebirdStringConnection);
//            //SaveList(nm.NachRecords, Consts.InsertRecordCount);
//            //SaveList(nm.OplataRecords, Consts.InsertRecordCount);
//            //SaveList(nm.NachoplRecords.Values, Consts.InsertRecordCount);
//        }
//    }

//    #region Перенос данных из временных таблиц
//    public class TransferAddressObjects : ConvertCase
//    {
//        public TransferAddressObjects()
//        {
//            ConvertCaseName = "Перенос данных об адресных объектах";
//            Position = 1000;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(7);

//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);

//            fbm.ExecuteProcedure("CNV$CNV_00100_REGIONDISTRICTS");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_00200_PUNKT");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_00300_STREET");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_00400_DISTRICT");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_00500_INFORMATIONOWNERS");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_00600_HOUSES");
//            Iterate();

//            StepFinish();
//        }
//    }

//    public class TransferAbonents : ConvertCase
//    {
//        public TransferAbonents()
//        {
//            ConvertCaseName = "Перенос данных об абонентах";
//            Position = 1010;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteProcedure("CNV$CNV_00700_ABONENTS");
//            Iterate();
//            StepFinish();
//        }
//    }

//    public class TransferChars : ConvertCase
//    {
//        public TransferChars()
//        {
//            ConvertCaseName = "Перенос данных о количественных характеристиках";
//            Position = 1020;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteProcedure("CNV$CNV_00800_CHARS", new[] { "0" });
//            Iterate();
//            StepFinish();
//        }
//    }

//    public class TransferNachisl : ConvertCase
//    {
//        public TransferNachisl()
//        {
//            ConvertCaseName = "Перенос данных о начислениях";
//            Position = 1070;
//            IsChecked = false;

//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteProcedure("CNV$CNV_01600_NACHISLIMPORT");
//            Iterate();
//        }
//    }

//    public class TransferOplata : ConvertCase
//    {
//        public TransferOplata()
//        {
//            ConvertCaseName = "Перенос данных об оплате";
//            Position = 1050;
//            IsChecked = false;

//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(2);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteProcedure("CNV$CNV_01300_SOURCEDOC");
//            Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_01400_OPLATA");
//            Iterate();
//        }
//    }

//    public class TransferSaldo : ConvertCase
//    {
//        public TransferSaldo()
//        {
//            ConvertCaseName = "Перенос данных о сальдо";
//            Position = 1060;
//            IsChecked = false;

//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert inactive");
//            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate inactive");
//            fbm.ExecuteProcedure("CNV$CNV_01500_SALDO", new[] { Consts.CurrentYear.ToString(CultureInfo.InvariantCulture),
//                Consts.CurrentMonth.ToString(CultureInfo.InvariantCulture) });
//            fbm.ExecuteNonQuery("ALTER trigger saldocheckupdate active");
//            fbm.ExecuteNonQuery("ALTER trigger saldocheckinsert active");
//            Iterate();
//        }
//    }

//    public class TransferPererashet : ConvertCase
//    {
//        public TransferPererashet()
//        {
//            ConvertCaseName = "Перерасчет";
//            Position = 1080;
//            IsChecked = false;

//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteProcedure("CNV$CNV_01700_PERERASHETIMPORT");
//            Iterate();
//        }
//    }

//    public class TransferExtlshet : ConvertCase
//    {
//        public TransferExtlshet()
//        {
//            ConvertCaseName = "Перенос данных о внешних лицевых счетах";
//            Position = 1090;
//            IsChecked = false;

//        }

//        public override void DoConvert()
//        {
//            SetStepsCount(1);
//            StepStart(1);
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            //fbm.ExecuteProcedure("CNV$CNV_00950_COUNTERSTYPES");
//            //Iterate();
//            fbm.ExecuteProcedure("CNV$CNV_02100_EXTLSHETS", new[] { "1", "0" });
//            Iterate();
//        }
//    }

//    public class TransferAddchars : ConvertCase
//    {
//        public TransferAddchars()
//        {
//            ConvertCaseName = "Перенос дополнительных характеристик";
//            Position = 1100;
//            IsChecked = false;
//        }

//        public override void DoConvert()
//        {
//            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
//            fbm.ExecuteQuery(@"insert into abonentadditionalchars (ADDITIONALCHARCD, LSHET, SIGNIFICANCE)
//                            select ADDCHARCD, lshet, ""VALUE"" from cnv$aaddchar");
//        }
//    }
//    #endregion

//    //Сод. жилья Эл. снаб. (нач) ХВС (нач) Сод. жилья (пени) Эл.снаб (пени) ХВС (пени) Водоотвед. (пени) ОДН эл.сн. (пени) ОДН ХВС (пени)
//    public class RecordBase
//    {
//        public int FileDu;
//        public int FileYear;
//        public int FileMonth;
//        public string FileName;
//        public int FileServiceId;

//        public int StreetId;
//        public int HouseId;
//        public int Korpusid;
//        public int FlatId;
//        public int RoomNumber;
//        public int AddressHash;

//        public int NumLs;
//        public int Du;
//        public long Adr1;
//        public long Adr2;
//        public LsType LsType;
//        public NachType NachType;
//        public PlacementType PlacementType;
//        public LivingPlacementType LivingPlacementType;
//        public OwnershipType OwnershipType;
//        public int PmgCount;
//        public int VoCount;
//        public int PmpCount;
//        public decimal TotalSquare;
//        public decimal LivingSquare;
//        public decimal Tarif;
//        public decimal BeginDebet;
//        public decimal BeginKredit;
//        public decimal NachSum;
//        public decimal RecalcSum;
//        public int? PayYear;
//        public int? PayMonth;
//        public DateTime? PayDate;
//        public decimal PaySum;
//        public decimal EndDebet;
//        public decimal EndKredit;
//        public string Fio;
//        public int NumStr;
//        public decimal PkNachSum;

//        public string LsKvc
//        {
//            get
//            {
//                if (_lsKvc == null)
//                {
//                    string adr1 = String.Format("{0:D8}", Adr1);
//                    string adr2 = String.Format("{0:D6}", Adr2);
//                    _lsKvc = adr1 + adr2;
//                }
//                return _lsKvc;
//            }
//        }

//        private string _lsKvc;

//        public decimal BeginSaldo => BeginDebet - BeginKredit;
//        public decimal EndSaldo => EndDebet - EndKredit;

//        public RecordBase(DataRow dr, string fileName)
//        {
//            try
//            {
//                string tmpStr;
//                NumLs = Int32.Parse(dr["NUMLS"].ToString());
//                Du = Int32.Parse(dr["HOZKART"].ToString());
//                Adr1 = Int64.Parse(dr["ADR1"].ToString());
//                Adr2 = Int64.Parse(dr["ADR2"].ToString());
//                string adr1 = String.Format("{0:D8}", Adr1);
//                StreetId = UInt16.Parse(adr1.Substring(0, 3));
//                HouseId = UInt16.Parse(adr1.Substring(3, 3));
//                Korpusid = UInt16.Parse(adr1.Substring(6, 2));
//                string adr2 = String.Format("{0:D6}", Adr2);
//                FlatId = UInt16.Parse(adr2.Substring(0, 3));
//                RoomNumber = UInt16.Parse(adr2.Substring(3, 1));
//                AddressHash = UInt16.Parse(adr2.Substring(4, 2));
//                LsType = (LsType) (Int32.Parse(dr["SOSTLS"].ToString()));
//                NachType = (NachType) (Int32.Parse(dr["SPSNAC"].ToString()));
//                PlacementType = (PlacementType) (Int32.Parse(dr["TIPPOM"].ToString()));
//                LivingPlacementType = (LivingPlacementType) (Int32.Parse(dr["VIDPOM"].ToString()));
//                OwnershipType = (OwnershipType) (Int32.Parse(dr["FORMSB"].ToString()));
//                PmgCount = Convert.ToInt32(dr["GR_08"]);
//                VoCount = Convert.ToInt32(dr["GR_09"]);
//                PmpCount = Convert.ToInt32(dr["GR_10"]);
//                TotalSquare = Decimal.Parse(dr["GR_12"].ToString());
//                LivingSquare = Decimal.Parse(dr["GR_14"].ToString());
//                Tarif = Decimal.Parse(dr["TARIF"].ToString());
//                BeginDebet = Decimal.Parse(dr["DV"].ToString());
//                BeginKredit = Decimal.Parse(dr["KV"].ToString());
//                NachSum = Decimal.Parse(dr["NAC"].ToString());
//                RecalcSum = Decimal.Parse(dr["IZM"].ToString());
//                tmpStr = dr["ZAM"].ToString().Trim();
//                if (tmpStr.Length > 1)
//                {
//                    PayYear = Int32.Parse("20" + tmpStr.Substring(0, 2));
//                    PayMonth = Int32.Parse(tmpStr.Substring(2, 2));
//                }
//                tmpStr = dr["DAT"].ToString().Trim();
//                if (tmpStr.Length > 1)
//                {
//                    PayDate = new DateTime(
//                        Int32.Parse("20" + tmpStr.Substring(0, 2)),
//                        Int32.Parse(tmpStr.Substring(2, 2)),
//                        Int32.Parse(tmpStr.Substring(4, 2)));
//                }
//                PaySum = Decimal.Parse(dr["OPL"].ToString());
//                EndDebet = Decimal.Parse(dr["DI"].ToString());
//                EndKredit = Decimal.Parse(dr["KI"].ToString());
//                Fio = dr["FIO"].ToString().Trim();
//                NumStr = Int32.Parse(dr["NUMSTR"].ToString());
//                PkNachSum = Decimal.Parse(dr["KOFNAC"].ToString());
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public RecordBase()
//        {

//        }
//    }

//    //Эл. снаб. (сч) ХВС(сч)
//    public class RecordCnt
//    {
        
//    }

//    //Водоотвед.
//    public class RecordVodoot
//    {
        
//    }

//    //ОДН эл.сн. (нач) ОДН ХВС (нач)
//    public class RecordOdnNach
//    {
        
//    }

//    //ОДН эл.сн. (сч) ОДН ХВС (сч)
//    public class RecordOdnCnt
//    {
        
//    }

//    public enum LsType
//    {
//        Common = 0,
//        Opened = 2,
//        Closed = 5,
//        Recoded = 6,
//        Merged = 7
//    }

//    public enum NachType
//    {
//        Auto = 1,
//        Manual = 13
//    }

//    public enum PlacementType
//    {
//        Living = 0,
//        NotLiving = 1,
//        Stead = 2
//    }

//    public enum LivingPlacementType
//    {
//        Flat = 1,
//        CommunalFlat = 2,
//        FormerHostel = 3,
//        House = 4,
//        Room = 5,
//        Hostel = 6
//    }

//    public enum OwnershipType
//    {
//        Citizen = 1,
//        Private = 2,
//        LegalEntity = 3,
//        RF = 4,
//        RFSubject = 5,
//        Municipal = 6
//    }

//    [Flags]
//    public enum ServiceType
//    {
//        Base,
//        Cnt,
//        Vodootv,
//        OdnNach,
//        OdnCnt
//    }

//    public class KvcFileInfo
//    {
//        public int Year;
//        public int Month;
//        public int ServiceId;
//        public int Du;
//        public bool IsCounter;

//        public KvcFileInfo(FileInfo fileInfo)
//        {
//            int[] fileParams = fileInfo.Name.Replace(fileInfo.Extension, "").Split('_').Select(Int32.Parse).ToArray();
//            Year = Int32.Parse("20" + fileParams[0].ToString().Substring(0, 2));
//            Month = Int32.Parse(fileParams[0].ToString().Substring(2, 2));
//            ServiceId = fileParams[1];
//            Du = fileParams[2];
//            IsCounter = fileParams[3] == 1;
//        }
//    }
//}
