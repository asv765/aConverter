using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.Records;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using aConverterClassLibrary.Records.Utils;

namespace _029_Kandalaksha
{
    public static class Consts
    {
        /// <summary>
        /// Начальный номер лицевого счета
        /// </summary>
        public const int StartLshet = 200000;

        /// <summary>
        /// Начальный номер улицы
        /// </summary>
        public const int StartStreetId = 100;

        /// <summary>
        /// Начальный номер кода дома
        /// </summary>
        public const int StartHouseCd = 1000;

        /// <summary>
        /// Начальный номер Id гражданина
        /// </summary>
        public const int StartCitizenId = 40000;

        public static int CurrentYear = 2014;

        public static int CurrentMonth = 12;

        public static int LastYear = CurrentMonth == 1 ? CurrentYear - 1 : CurrentYear;

        public static int LastMonth = CurrentMonth == 1 ? 12 : CurrentMonth - 1;

        public const string SourceDir = @"C:\Work\aConverter_Data\029_Kandalaksha\Source";

        public static string GetLs(int intls)
        {
            return String.Format("{0:D5}", intls);
        }

        public static DataTable GetTable(string tableName, OleDbConnection connection)
        {
            return ExecuteQuery("select * from " + tableName, connection);
        }

        public static DataTable ExecuteQuery(string query, OleDbConnection connection)
        {
            var adapter = new OleDbDataAdapter(query, connection);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

    }


    /// <summary>
    /// Удаляет все файлы в целевой директории
    /// </summary>
    public class DeleteAllFiles : ConvertCase
    {
        public DeleteAllFiles()
        {
            ConvertCaseName = "Удалить все файлы в целевой директории";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            List<string> fa = Directory.GetFiles(DestDir, "*.DBF").ToList();
            fa.AddRange(Directory.GetFiles(DestDir, "*.CDX").ToList());
            fa.AddRange(Directory.GetFiles(DestDir, "*.FPT").ToList());
            StepStart(fa.Count);
            foreach (string f in fa)
            {
                File.Delete(f);
                Iterate();
            }
        }
    }

    /// <summary>
    /// Создать все файлы для конвертации в целевой директории
    /// </summary>
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать файлы для конвертации в целевой директории";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            FactoryRecord.CreateAllTables(tmdest);
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
            ConvertCaseName = "ABONENT.DBF - данные об абонентах";
            Position = 30;
            IsChecked = true;
        }

        public uint CurrentLshet;

        public override void DoConvert()
        {
            tmdest.CleanTable(typeof(AbonentRecord));
            tmdest.CleanTable(typeof(LgotaRecord));
            tmdest.CleanTable(typeof(CountersRecord));
            tmdest.CleanTable(typeof(CntrsindRecord));
            tmdest.CleanTable(typeof(CharsRecord));
            tmdest.CleanTable(typeof(LcharsRecord));
            tmdest.CleanTable(typeof(NachoplRecord));
            tmdest.CleanTable(typeof(NachRecord));
            tmdest.CleanTable(typeof(OplataRecord));

            var odcsb = new OleDbConnectionStringBuilder
            {
                DataSource = Consts.SourceDir + "\\abonent.mdb",
                Provider = "Microsoft.Jet.OLEDB.4.0"
            };

            AbonentRecord ar;
            var arlist = new List<AbonentRecord>();
            // Словарь - список абонентов с ключом "ID лицевого счета"
            var ardic = new Dictionary<int, AbonentRecord>();
            // Словарь - список абонентов с ключом "лицевой счет"
            var ardic2 = new Dictionary<string, AbonentRecord>();
            // Список жителей
            var ctlist = new List<LgotaRecord>();

            // Словарь со счетчиками
            Dictionary<int,CountersRecord> dcounters = new Dictionary<int, CountersRecord>();
            // Список показаний счетчиков
            List<CntrsindRecord> lcntrsind = new List<CntrsindRecord>();

            // Качественные характеристики
            List<LcharsRecord> listLcharRecord = new List<LcharsRecord>();
            // Количественные характеристики
            List<CharsRecord> listCharsRecord = new List<CharsRecord>();

            SetStepsCount(1 +               // Список абонентов
                1 +                         // Список граждан
                (Consts.CurrentMonth-1) +     // Квитки
                1 +                         // Оплата
                1 +                         // Сохранения начислений
                1 +                         // Сохранения оплат
                1 +                         // Сохранения истории оплат-начислений
                1 +                         // Сохранения абонентов
                1 +                         // Сохранение граждан
                1 +                         // Сохранение счетчиков
                1 +                         // Сохранение показаний счетчиков
                1 +                         // Сохранение качественных характеристик
                1                           // Сохранение количественных характеристик
                );

            using (var connection = new OleDbConnection(odcsb.ConnectionString))
            {
                #region Список абонентов
                // Словарь улиц (таблица dcStreet)
                DataTable streetsTable = Consts.GetTable("dcStreet", connection);
                var streets = streetsTable.Rows.Cast<DataRow>().ToDictionary(row => Convert.ToInt32(row["ID"]));
                
                var adapter = new OleDbDataAdapter("select * from dbAdresLs", connection);
                var abonents = new DataTable();
                adapter.Fill(abonents);
                StepStart(abonents.Rows.Count);
                foreach (DataRow dataRow in abonents.Rows)
                {
                    ar = new AbonentRecord();
                    ar.Lshet = Consts.GetLs(Consts.StartLshet + Convert.ToInt32(dataRow["ID"]));
                    ar.Extlshet = dataRow["ЛС"].ToString();

                    ar.Townskod = 7;
                    ar.Townsname = "Кандалакша";

                    ar.Distkod = 7;
                    ar.Distname = "Кандалакша";

                    ar.Rayonkod = 2;
                    ar.Rayonname = "Кандалакша";

                    ar.Ducd = 1;
                    ar.Duname = "Кандалакша";

                    int ulicaId = Convert.ToInt32(dataRow["УлицаID"]);
                    ar.Ulicakod = Consts.StartStreetId + ulicaId;
                    ar.Ulicaname = streets[ulicaId]["Street"].ToString();
                    ar.Ndoma = dataRow["Дом"].ToString();
                    ar.Kvartira = dataRow["Кв"].ToString();

                    arlist.Add(ar);
                    ardic.Add(Convert.ToInt32(dataRow["ID"]), ar);
                    ardic2.Add(dataRow["ЛС"].ToString(), ar);


                    if (dataRow["Заглушка"].ToString() != "Нет")
                    {
                        LcharsRecord lcr = new LcharsRecord()
                        {
                            Lshet = ar.Lshet,
                            Date = new DateTime(Consts.CurrentYear, Consts.CurrentMonth, 1),
                            Lcharcd = 1,
                            Lcharname = "Тип газа",
                            Value_ = 8,
                            Valuedesc = "Газ отключен"
                        };
                        listLcharRecord.Add(lcr);
                    }

                }
                StepFinish();
                #endregion

                #region Заполняем список граждан
                DataTable citizenTable = Consts.GetTable("dbLSFio", connection);
                StepStart(citizenTable.Rows.Count);
                // var citizens = new List<LgotaRecord>();
                foreach (DataRow dr in citizenTable.Rows)
                {
                    var lr = new LgotaRecord();
                    lr.Citizenid = Consts.StartCitizenId + Convert.ToInt32(dr["ID"]);
                    int lsid = Convert.ToInt32(dr["ЛСID"]);

                    lr.Lshet = ardic[lsid].Lshet;
                    string f = dr["Фамилия"].ToString().Trim();
                    string i = dr["Имя"].ToString().Trim();
                    string o = dr["Отчество"].ToString().Trim();

                    // string fio = String.Format("{0} {1} {2}", f, i, o);
                    // if (fio.Length > 50) fio = fio.Substring(0, 50);
                    lr.F = f.Length > 30 ? f.Substring(0, 30) : f;
                    lr.I = i.Length > 20 ? i.Substring(0, 20) : i;
                    lr.O = o.Length > 20 ? o.Substring(0, 20) : o;
                    lr.Comment = dr["Льгота"].ToString();
                    if (!(dr["Год"] is DBNull))
                    {
                        int year = Convert.ToInt32(dr["Год"]);
                        if (year > 1900) lr.Birthdate = new DateTime(year, 1, 1);
                    }

                    if (!(dr["Номер"] is DBNull))
                    {
                        if (Convert.ToInt32(dr["Номер"]) > 0)
                        {
                            
                            ardic[lsid].F = f;
                            ardic[lsid].I = i;
                            ardic[lsid].O = o;

                            lr.Hoz = 1;
                        }
                    }

                    int isactive = 0;
                    if (!(dr["Байт"] is DBNull)) isactive  = Convert.ToInt32(dr["Байт"]);
                    if (isactive != 1) lr.Enddate = new DateTime(2000, 1, 1);

                    ctlist.Add(lr);
                    Iterate();
                    
                }
                StepFinish();
                
                
                #endregion

                NachoplManager manager = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_начало);


                #region История начислений-перерасчетов
                //nr.Lshet = lshet;
                //nr.Fnath = fnath;
                //nr.Prochl = prochl;
                //nr.Month = nr.Month2 = month;
                //nr.Year = nr.Year2 = year;
                //nr.Date_vv = dateVv;
                //nr.Documentcd = documentcd;
                var defaultNachRecord = new NachRecord()
                {
                    Regimcd = 10,
                    Regimname = "Неизвестен",
                    Servicecd = 1,
                    Servicenam = "Сжиженный газ"
                };

                var prevpeni = new Dictionary<string, decimal>();
                
                for (int i = 1; i < Consts.CurrentMonth; i++)
                {
                    DataTable kvit = Consts.GetTable("dbKvitki" + i.ToString(CultureInfo.InvariantCulture), connection);
                    StepStart(kvit.Rows.Count);
                    foreach (DataRow dr in kvit.Rows)
                    {
                        if (!ardic2.ContainsKey(dr["ЛС"].ToString())) continue;
                        string lshet = ardic2[dr["ЛС"].ToString()].Lshet;

                        if (lshet == "30005" && (i == 9 || i == 10))
                            lshet = lshet.Trim();

                        decimal saldobeg = Convert.ToDecimal(dr["Долг"]);
                        decimal saldoend = Convert.ToDecimal(dr["К_Оплате"]);
                        decimal nath = Convert.ToDecimal(dr["Начислено"]);
                        decimal prochl = Convert.ToDecimal(dr["Перерасчет"]);
                        decimal volume = Convert.ToDecimal(dr["Расход"]);
                        decimal peni = Convert.ToDecimal(dr["Пени"]);
                        decimal oplataDecimal = Convert.ToDecimal(dr["ОплатаПрош"]);
                        decimal prevpenid = prevpeni.ContainsKey(lshet) ? prevpeni[lshet] : 0;
                        if (oplataDecimal == 0) prevpenid = 0;
                        string documentcd = "dbKvitki" + i + "_" + dr["ID"];
                        DateTime endMonthDate = new DateTime(Consts.CurrentYear, i, DateTime.DaysInMonth(Consts.CurrentYear, i));

                        // manager.RegisterBeginSaldo(lshet, i, Consts.CurrentYear, 1, saldobeg - prevpenid);
                        manager.RegisterEndSaldo(lshet, i, Consts.CurrentYear, 1, saldoend);

                        bool counterPresent = false;
                        if (!(dr["СчетчикID"] is DBNull))
                            if (Convert.ToInt32(dr["СчетчикID"]) > 0) 
                                counterPresent = true;
                        LcharsRecord lcr = new LcharsRecord()
                        {
                            Lshet = lshet,
                            Date = new DateTime(Consts.CurrentYear, i, 1),
                            Lcharcd = 10,
                            Lcharname = "Тип учета"
                        };
                        if (counterPresent)
                        {
                            defaultNachRecord.Type = 1;
                            defaultNachRecord.Volume = volume;
                            lcr.Value_ = 1;
                            lcr.Valuedesc = "Со счетчиком";
                        }
                        else
                        {
                            defaultNachRecord.Type = 0;
                            defaultNachRecord.Volume = 0;
                            lcr.Value_ = 0;
                            lcr.Valuedesc = "Без счетчика";
                        }
                        var dateVv = endMonthDate;
                        if (nath > 0 || (prochl+prevpenid) > 0 || volume > 0)
                            manager.RegisterNach(defaultNachRecord, lshet, i, Consts.CurrentYear, nath, prochl+prevpenid, dateVv, documentcd);
                        if (prevpeni.ContainsKey(lshet))
                            prevpeni[lshet] = peni;
                        else
                            prevpeni.Add(lshet, peni);
                        listLcharRecord.Add(lcr);

                        lcr = new LcharsRecord()
                        {
                            Lshet = lshet,
                            Date = new DateTime(Consts.CurrentYear, i, 1),
                            Lcharcd = 1,
                            Lcharname = "Тип газа",
                            Value_ = 1,
                            Valuedesc = "Сжиженный"
                        };
                        listLcharRecord.Add(lcr);

                        CharsRecord cr = new CharsRecord()
                        {
                            Lshet = lshet,
                            Date = new DateTime(Consts.CurrentYear, i, 1),
                            Charcd = 1,
                            Charname = "Число проживающих",
                            Value_ = Convert.ToInt32(dr["КО"])
                        };
                        listCharsRecord.Add(cr);

                        Iterate();
                    }
                    StepFinish();
                }
                #endregion

                #region История оплат
                // Подготавливаем словарь источников оплаты
                DataTable sd = Consts.ExecuteQuery(
                    "SELECT `Откуда`, COUNT(*) AS CNT " +
                    "FROM `dbOplata` " +
                    "WHERE `Откуда` <> '' " +
                    "GROUP BY `Откуда` " +
                    "HAVING COUNT(*) > 10", connection
                    );
                Dictionary<string, int> sddic = new Dictionary<string, int>();
                int counter = 1;
                foreach (DataRow dr in sd.Rows)
                {
                    sddic.Add(dr["Откуда"].ToString(), 200 + counter++);
                }

                // string lshet, int month, int year, decimal summa, DateTime date, DateTime dateVv, string documentcd
                OplataRecord defaultOplataRecord = new OplataRecord()
                {
                    Servicecd = 1,
                    Servicenam = "Сжиженный газ"
                };

                DataTable oplata = Consts.ExecuteQuery("select * from dbOplata where YEAR(`Дата1`) = 2014", connection);
                StepStart(oplata.Rows.Count);
                foreach (DataRow dr in oplata.Rows)
                {
                    Iterate();
                    if (!ardic2.ContainsKey(dr["ЛС"].ToString())) continue;
                    string lshet = ardic2[dr["ЛС"].ToString()].Lshet;

                    int sourceDocCd = 200;
                    string sourceDocName = "Кандалакша, прочее";
                    if (sddic.TryGetValue(dr["Откуда"].ToString(), out sourceDocCd))
                    {
                        sourceDocName = dr["Откуда"].ToString();
                    }
                    else
                    {
                        sourceDocCd = 200;
                    }
                    defaultOplataRecord.Sourcecd = sourceDocCd;
                    defaultOplataRecord.Sourcename = sourceDocName;

                    decimal summa = Convert.ToDecimal(dr["Оплата"]);
                    DateTime date = Convert.ToDateTime(dr["Дата"]);
                    DateTime dateVv = Convert.ToDateTime(dr["Дата1"]);

                    int month = date.Month;
                    int year = date.Year;

                    string destination = dr["Назначение"].ToString();

                    if (!String.IsNullOrWhiteSpace(destination))
                    {
                        month = DateTime.Parse(destination).Month;
                        year = DateTime.Parse(destination).Year;
                    }

                    string documentcd = "dbOplata_" + dr["ID"].ToString();

                    manager.RegisterOplata(defaultOplataRecord, lshet, month, year, summa, date, dateVv, documentcd);

                    // Заполняем счетчики
                    int counterid = 0;
                    if (!(dr["Счетчик"] is DBNull)) counterid = Convert.ToInt32(dr["Счетчик"]);
                    if (counterid > 0)
                    {
                        if (!dcounters.ContainsKey(counterid))
                        {
                            CountersRecord cr = new CountersRecord()
                            {
                                Cnttype = 112,
                                Cntname = "Марка не указана",
                                Counterid = counterid.ToString(CultureInfo.InvariantCulture),
                                Lshet = lshet,
                                Setupdate = new DateTime(2000, 1, 1)
                            };
                            dcounters.Add(counterid, cr);
                        }

                        int indication = Convert.ToInt32(dr["После"]);
                        int oldind = Convert.ToInt32(dr["До"]);
                        int obEm = indication - oldind;
                        if (obEm != 0 || indication !=0 || oldind != 0)
                        {
                            CntrsindRecord cir = new CntrsindRecord()
                            {
                                Counterid = counterid.ToString(CultureInfo.InvariantCulture),
                                Inddate = Convert.ToDateTime(dr["Дата1"]),
                                Documentcd = documentcd,
                                Indication = indication,
                                Indtype = 0,
                                Ob_em = obEm,
                                Oldind = oldind
                            };
                            lcntrsind.Add(cir);
                        };
                    }


                }
                StepFinish();
                #endregion

                StepStart(1);
                manager.SaveNachRecords(tmdest);
                Iterate();
                StepFinish();

                StepStart(1);
                manager.SaveNachoplRecords(tmdest);
                Iterate();
                StepFinish();

                StepStart(1);
                manager.SaveOplataRecords(tmdest);
                Iterate();
                StepFinish();

            }

            AbonentRecordUtils.SetUniqueHouseCd(ref arlist, Consts.StartHouseCd);

            // Сбрасываем список абонентов в файл
            StepStart(arlist.Count);
            foreach (var abonentRecord in arlist)
            {
                tmdest.InsertRecord(abonentRecord.GetInsertScript());
                Iterate();
            }
            StepFinish();

            // Сбрасываем список граждан в файл
            StepStart(ctlist.Count);
            foreach (var ctr in ctlist)
            {
                tmdest.InsertRecord(ctr.GetInsertScript());
                Iterate();
            }
            StepFinish();

            // Сбрасываем список счетчиков в файл
            StepStart(dcounters.Count);
            foreach (CountersRecord cr in dcounters.Values)
            {
                tmdest.InsertRecord(cr.GetInsertScript());
                Iterate();
            }
            StepFinish();

            // Сбрасываем показания счетчиков в файл
            StepStart(lcntrsind.Count);
            foreach (CntrsindRecord cir in lcntrsind)
            {
                tmdest.InsertRecord(cir.GetInsertScript());
                Iterate();
            }
            StepFinish();

            // Сбрасываем качественные характеристики в файл
            var thinedOutLcharsRecordList = LcharsRecordUtils.ThinOutList(listLcharRecord);
            StepStart(thinedOutLcharsRecordList.Count);
            foreach (LcharsRecord lcr in thinedOutLcharsRecordList)
            {
                tmdest.InsertRecord(lcr.GetInsertScript());
                Iterate();
            }
            StepFinish();

            // Сбрасываем количественные характеристики в файл
            var thinedOutCharsRecordList = CharsRecordUtils.ThinOutList(listCharsRecord);
            StepStart(thinedOutCharsRecordList.Count);
            foreach (CharsRecord cr in thinedOutCharsRecordList)
            {
                tmdest.InsertRecord(cr.GetInsertScript());
                Iterate();
            }
            StepFinish();

        }
    }
}
