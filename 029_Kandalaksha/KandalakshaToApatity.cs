using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
//using aConverterClassLibrary.Records;//----------------------------------------------------------------------
using aConverterClassLibrary.RecordsEDM;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using aConverterClassLibrary.Records.Utils;//----------------------------------------------------------------
using aConverterClassLibrary.RecordsEDM.Utils;
using aConverterClassMariaDB;
using System.Windows.Forms;



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

        //public const string SourceDir = @"C:\Work\aConverter_Data\029_Kandalaksha\Source";
        public const string SourceDir = @"D:\GitDiplom\aConverter\029_Kandalaksha\Source\";

        public static string GetLs(int intls)
        {
            return String.Format("{0:D5}", intls);
        }

        public static DataTable GetTable(string tableName, OleDbConnection connection)//--------------наверно менять
        {
            return ExecuteQuery("select * from " + tableName, connection);
        }

        public static DataTable ExecuteQuery(string query, OleDbConnection connection)
        {
            var adapter = new OleDbDataAdapter(query, connection);//------------------менять
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
            IsChecked = true;
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
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);
            //
            ExecuteScript.CreateDatabaseMariaDB();
            //
            //FactoryRecord.CreateAllTables(tmdest); //--------------создались все таблици 
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
            IsChecked = false;
        }

        public uint CurrentLshet;

        public override void DoConvert()
        {
            //tmdest.CleanTable(typeof(AbonentRecord));
            //tmdest.CleanTable(typeof(LgotaRecord));
            //tmdest.CleanTable(typeof(CountersRecord));
            //tmdest.CleanTable(typeof(CntrsindRecord));
            //tmdest.CleanTable(typeof(CharsRecord));
            //tmdest.CleanTable(typeof(LcharsRecord));
            //tmdest.CleanTable(typeof(NachoplRecord));
            //tmdest.CleanTable(typeof(NachRecord));//------------------------------------------------------------------------------
            //tmdest.CleanTable(typeof(nach));
            //tmdest.CleanTable(typeof(OplataRecord));

            var odcsb = new OleDbConnectionStringBuilder// менять на edm
            {
                DataSource = Consts.SourceDir + "\\abonent.mdb",
                Provider = "Microsoft.Jet.OLEDB.4.0"
            };

            //AbonentRecord ar;
            abonent ar;
            //var arlist = new List<AbonentRecord>();
            var arlist = new List<abonent>();
            // Словарь - список абонентов с ключом "ID лицевого счета"
            //var ardic = new Dictionary<int, AbonentRecord>();
            var ardic = new Dictionary<int, abonent>();
            // Словарь - список абонентов с ключом "лицевой счет"
            //var ardic2 = new Dictionary<string, AbonentRecord>();
            var ardic2 = new Dictionary<string, abonent>();
            // Список жителей
            var ctlist = new List<lgota>();

            // Словарь со счетчиками
            //Dictionary<int,CountersRecord> dcounters = new Dictionary<int, CountersRecord>();
            Dictionary<int, counter> dcounters = new Dictionary<int, counter>();

            // Список показаний счетчиков
            //List<CntrsindRecord> lcntrsind = new List<CntrsindRecord>();
            List<cntrsind> lcntrsind = new List<cntrsind>();//----------------------------------------------

            // Качественные характеристики
            //List<LcharsRecord> listLcharRecord = new List<LcharsRecord>();
            List<lchar> listLcharRecord = new List<lchar>();
            // Количественные характеристики
            //List<CharsRecord> listCharsRecord = new List<CharsRecord>();//------------------
            List<@char> listCharsRecord = new List<@char>();

            SetStepsCount(1 +               // Список абонентов
                1 +                         // Список граждан
                (Consts.CurrentMonth - 1) +     // Квитки
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
                    ar = new abonent(); //-------------------------------------------------------------------------------------------
                    ar.LSHET = Consts.GetLs(Consts.StartLshet + Convert.ToInt32(dataRow["ID"]));//------------------------------------------------
                    ar.EXTLSHET = dataRow["ЛС"].ToString();//-------------------------------------------------------------------------------------

                    ar.TOWNSKOD = 7;//------------------------------------------------------------------------------------------
                    ar.TOWNSNAME= "Кандалакша";//-------------------------------------------------------------------------------

                    ar.DISTKOD = 7;//-------------------------------------------------------------------------------------
                    ar.DISTNAME = "Кандалакша";//----------------------------------------------------------------------------

                    ar.RAYONKOD = 2;//----------------------------------------------------------------------------------------
                    ar.RAYONNAME = "Кандалакша";//----------------------------------------------------------------------------

                    ar.DUCD = 1;//----------------------------------
                    ar.DUNAME = "Кандалакша";//---------------------------------------------------

                    int ulicaId = Convert.ToInt32(dataRow["УлицаID"]);
                    ar.ULICAKOD = Consts.StartStreetId + ulicaId;//---------------------------------------------
                    ar.ULICANAME = streets[ulicaId]["Street"].ToString();
                    ar.NDOMA = dataRow["Дом"].ToString();
                    ar.KVARTIRA = dataRow["Кв"].ToString();//-----------------------------------------------------

                    arlist.Add(ar);
                    ardic.Add(Convert.ToInt32(dataRow["ID"]), ar);
                    ardic2.Add(dataRow["ЛС"].ToString(), ar);


                    if (dataRow["Заглушка"].ToString() != "Нет")
                    {
                        lchar lcr = new lchar()//------------------------------------------------------
                        {
                            LSHET = ar.LSHET,//-------------------------------------------------------------------------------
                            DATE = new DateTime(Consts.CurrentYear, Consts.CurrentMonth, 1),
                            LCHARCD = 1,
                            LCHARNAME = "Тип газа",
                            VALUE = 8,
                            VALUEDESC = "Газ отключен"
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
                    var lr = new lgota();
                    lr.CITIZENID = Consts.StartCitizenId + Convert.ToInt32(dr["ID"]);//------------------------------------------
                    int lsid = Convert.ToInt32(dr["ЛСID"]);

                    lr.LSHET = ardic[lsid].LSHET;//-------------------------------------------------------------------------------
                    string f = dr["Фамилия"].ToString().Trim();
                    string i = dr["Имя"].ToString().Trim();
                    string o = dr["Отчество"].ToString().Trim();

                    // string fio = String.Format("{0} {1} {2}", f, i, o);
                    // if (fio.Length > 50) fio = fio.Substring(0, 50);
                    lr.F = f.Length > 30 ? f.Substring(0, 30) : f;
                    lr.I = i.Length > 20 ? i.Substring(0, 20) : i;
                    lr.O = o.Length > 20 ? o.Substring(0, 20) : o;
                    lr.COMMENT = dr["Льгота"].ToString();//------------------------------------------------
                    if (!(dr["Год"] is DBNull))
                    {
                        int year = Convert.ToInt32(dr["Год"]);
                        if (year > 1900) lr.BIRTHDATE = new DateTime(year, 1, 1);//------------------------------------------
                    }

                    if (!(dr["Номер"] is DBNull))
                    {
                        if (Convert.ToInt32(dr["Номер"]) > 0)
                        {
                            
                            ardic[lsid].F = f;
                            ardic[lsid].I = i;
                            ardic[lsid].O = o;

                            lr.HOZ = 1;//-------------------------------------------------------------------
                        }
                    }

                    int isactive = 0;
                    if (!(dr["Байт"] is DBNull)) isactive  = Convert.ToInt32(dr["Байт"]);
                    if (isactive != 1) lr.ENDDATE = new DateTime(2000, 1, 1);//---------------------------------------------------

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

                //var defaultNachRecord = new NachRecord()//----------------------------------------------------------------------------------
                var defaultNachRecord = new nach()
                {
                    //Regimcd = 10,
                    //Regimname = "Неизвестен",
                    //Servicecd = 1,
                    //Servicenam = "Сжиженный газ"

                    REGIMCD = 10,
                    REGIMNAME = "Неизвестен",
                    SERVICECD = 1,
                    SERVICENAM = "Сжиженный газ"
                };

                var prevpeni = new Dictionary<string, decimal>();
                
                for (int i = 1; i < Consts.CurrentMonth; i++) //---------------------------------------------5,6,7,8,9,10,11,12,13
                {
                    DataTable kvit = Consts.GetTable("dbKvitki" + i.ToString(CultureInfo.InvariantCulture), connection);
                    StepStart(kvit.Rows.Count);
                    foreach (DataRow dr in kvit.Rows)
                    {
                        if (!ardic2.ContainsKey(dr["ЛС"].ToString())) continue;
                        string lshet = ardic2[dr["ЛС"].ToString()].LSHET;//-----------------------------------------------------------------

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
                        lchar lcr = new lchar()//-------------------------------------------------------------
                        {
                            LSHET = lshet,
                            DATE = new DateTime(Consts.CurrentYear, i, 1),
                            LCHARCD = 10,
                            LCHARNAME = "Тип учета"
                        };
                        if (counterPresent)
                        {
                            defaultNachRecord.TYPE = 1;//----------------------------------------------------------------------------
                            defaultNachRecord.VOLUME = volume;
                            lcr.VALUE = 1;
                            lcr.VALUEDESC = "Со счетчиком";
                        }
                        else
                        {
                            defaultNachRecord.TYPE = 0;//--------------------------------------------------------------------------------
                            defaultNachRecord.VOLUME = 0;
                            lcr.VALUE = 0;
                            lcr.VALUEDESC = "Без счетчика";
                        }
                        var dateVv = endMonthDate;
                        if (nath > 0 || (prochl+prevpenid) > 0 || volume > 0)
                            manager.RegisterNach(defaultNachRecord, lshet, i, Consts.CurrentYear, nath, prochl+prevpenid, dateVv, documentcd);
                        if (prevpeni.ContainsKey(lshet))
                            prevpeni[lshet] = peni;
                        else
                            prevpeni.Add(lshet, peni);
                        listLcharRecord.Add(lcr);

                        lcr = new lchar()
                        {
                            LSHET = lshet,
                            DATE = new DateTime(Consts.CurrentYear, i, 1),
                            LCHARCD = 1,
                            LCHARNAME = "Тип газа",
                            VALUE = 1,
                            VALUEDESC = "Сжиженный"
                        };
                        listLcharRecord.Add(lcr);

                        @char cr = new @char()//--------------------------------------------------------------------
                        {
                            LSHET = lshet,
                            DATE = new DateTime(Consts.CurrentYear, i, 1),
                            CHARCD = 1,
                            CHARNAME = "Число проживающих",
                            VALUE = Convert.ToInt32(dr["КО"])
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
                oplata defaultOplataRecord = new oplata()//----------------------------------------------------------------------------------------------------
                {
                    //Servicecd = 1,
                    //Servicenam = "Сжиженный газ"
                    SERVICECD = 1,//-----------------------------------------------------------------------------------------------------------------
                    SERVICENAM = "Сжиженный газ"
                };

                DataTable oplata = Consts.ExecuteQuery("select * from dbOplata where YEAR(`Дата1`) = 2014", connection);
                StepStart(oplata.Rows.Count);
                foreach (DataRow dr in oplata.Rows)
                {
                    Iterate();
                    if (!ardic2.ContainsKey(dr["ЛС"].ToString())) continue;
                    string lshet = ardic2[dr["ЛС"].ToString()].LSHET;//--------------------------------------------------------------------------------

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
                    //defaultOplataRecord.Sourcecd = sourceDocCd;
                    //defaultOplataRecord.Sourcename = sourceDocName;
                    defaultOplataRecord.SOURCECD = sourceDocCd;//------------------------------------------------------------------------------
                    defaultOplataRecord.SOURCENAME = sourceDocName;//--------------------------------------------------------------------------

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
                            counter cr = new counter()
                            {
                                CNTTYPE = 112,//-----------------------------------------------------------------
                                CNTNAME = "Марка не указана",
                                COUNTERID = counterid.ToString(CultureInfo.InvariantCulture),
                                LSHET = lshet,
                                SETUPDATE = new DateTime(2000, 1, 1)
                            };
                            dcounters.Add(counterid, cr);
                        }

                        int indication = Convert.ToInt32(dr["После"]);
                        int oldind = Convert.ToInt32(dr["До"]);
                        int obEm = indication - oldind;
                        if (obEm != 0 || indication !=0 || oldind != 0)
                        {
                            cntrsind cir = new cntrsind()
                            {
                                COUNTERID = counterid.ToString(CultureInfo.InvariantCulture),
                                INDDATE = Convert.ToDateTime(dr["Дата1"]),
                                DOCUMENTCD = documentcd,
                                INDICATION = indication,
                                INDTYPE = 0,
                                OB_EM = obEm,
                                OLDIND = oldind
                            };
                            lcntrsind.Add(cir);
                        };
                    }


                }
                StepFinish();
                #endregion

                StepStart(1);
                manager.SaveNachRecords(tmdest);//---------------------------------15 цикла
                Iterate();
                StepFinish();

                StepStart(1);
                manager.SaveNachoplRecords(tmdest);//------------------------------16 после цикла началось отсюда
                Iterate();
                StepFinish();

                StepStart(1);
                manager.SaveOplataRecords(tmdest);//-----------------------------17 после цикла
                Iterate();
                StepFinish();

            }

            AbonentRecordUtils.SetUniqueHouseCd(ref arlist, Consts.StartHouseCd);//-----------------------17

            // Сбрасываем список абонентов в файл ??????????????????????????????????????????------------------------------18
            StepStart(arlist.Count);
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                foreach (var abonentRecord in arlist)
                {
                    //tmdest.InsertRecord(abonentRecord.GetInsertScript());
                    try
                    {
                        abonent abon = new abonent
                        {
                            LSHET = String.IsNullOrEmpty(abonentRecord.LSHET) ? "" : abonentRecord.LSHET.Trim(),
                            HOUSECD = abonentRecord.HOUSECD,
                            DISTKOD = abonentRecord.DISTKOD,
                            DISTNAME = String.IsNullOrEmpty(abonentRecord.DISTNAME) ? "" : abonentRecord.DISTNAME.Trim(),
                            RAYONKOD = abonentRecord.RAYONKOD,
                            RAYONNAME = String.IsNullOrEmpty(abonentRecord.RAYONNAME) ? "" : abonentRecord.RAYONNAME.Trim(),
                            TOWNSKOD = abonentRecord.TOWNSKOD,
                            TOWNSNAME = String.IsNullOrEmpty(abonentRecord.TOWNSNAME) ? "" : abonentRecord.TOWNSNAME.Trim(),
                            ULICAKOD = abonentRecord.ULICAKOD,
                            ULICANAME = String.IsNullOrEmpty(abonentRecord.ULICANAME) ? "" : abonentRecord.ULICANAME.Trim(),
                            NDOMA = String.IsNullOrEmpty(abonentRecord.NDOMA) ? "" : abonentRecord.NDOMA.Trim(),
                            KORPUS = abonentRecord.KORPUS,
                            KVARTIRA = String.IsNullOrEmpty(abonentRecord.KVARTIRA) ? "" : abonentRecord.KVARTIRA.Trim(),
                            KOMNATA = abonentRecord.KOMNATA,
                            F = String.IsNullOrEmpty(abonentRecord.F) ? "" : abonentRecord.F.Trim(),
                            I = String.IsNullOrEmpty(abonentRecord.I) ? "" : abonentRecord.I.Trim(),
                            O = String.IsNullOrEmpty(abonentRecord.O) ? "" : abonentRecord.O.Trim(),
                            PRIM_ = String.IsNullOrEmpty(abonentRecord.PRIM_) ? "" : abonentRecord.PRIM_.Trim(),
                            EXTLSHET = String.IsNullOrEmpty(abonentRecord.EXTLSHET) ? "" : abonentRecord.EXTLSHET.Trim(),
                            EXTLSHET2 = String.IsNullOrEmpty(abonentRecord.EXTLSHET2) ? "" : abonentRecord.EXTLSHET2.Trim(),
                            PHONENUM = String.IsNullOrEmpty(abonentRecord.PHONENUM) ? "" : abonentRecord.PHONENUM.Trim(),
                            POSTINDEX = String.IsNullOrEmpty(abonentRecord.POSTINDEX) ? "" : abonentRecord.POSTINDEX.Trim(),
                            DUCD = abonentRecord.DUCD,
                            DUNAME = String.IsNullOrEmpty(abonentRecord.DUNAME) ? "" : abonentRecord.DUNAME.Trim(),
                            ISDELETED = abonentRecord.ISDELETED

                        };
                        testcontext.abonents.AddObject(abon);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();

                StepFinish();
            }

            // Сбрасываем список граждан в файл--------------------------------------------------19
            StepStart(ctlist.Count);
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                foreach (var ctr in ctlist)
                {
                    //tmdest.InsertRecord(ctr.GetInsertScript());
                    try
                    {
                        lgota lgot = new lgota
                        {
                            LSHET = String.IsNullOrEmpty(ctr.LSHET) ? "" : ctr.LSHET.Trim(),
                            CITIZENID = ctr.CITIZENID,
                            F = String.IsNullOrEmpty(ctr.F) ? "" : ctr.F.Trim(),
                            I = String.IsNullOrEmpty(ctr.I) ? "" : ctr.I.Trim(),
                            O = String.IsNullOrEmpty(ctr.O) ? "" : ctr.O.Trim(),
                            BIRTHDATE = ctr.BIRTHDATE,
                            STARTDATE = ctr.STARTDATE,
                            ENDDATE = ctr.ENDDATE,
                            LGOTA1 = ctr.LGOTA1,
                            LGOTANAME = String.IsNullOrEmpty(ctr.LGOTANAME) ? "" : ctr.LGOTANAME.Trim(),
                            DATWP = ctr.DATWP,
                            DATUP = ctr.DATUP,
                            NAIM1 = String.IsNullOrEmpty(ctr.NAIM1) ? "" : ctr.NAIM1.Trim(),
                            SERIA1 = String.IsNullOrEmpty(ctr.SERIA1) ? "" : ctr.SERIA1.Trim(),
                            NOMER1 = String.IsNullOrEmpty(ctr.NOMER1) ? "" : ctr.NOMER1.Trim(),
                            DATDN1 = ctr.DATDN1,
                            DORGNAME1 = String.IsNullOrEmpty(ctr.DORGNAME1) ? "" : ctr.DORGNAME1.Trim(),
                            NAIM2 = String.IsNullOrEmpty(ctr.NAIM1) ? "" : ctr.NAIM1.Trim(),
                            SERIA2 = String.IsNullOrEmpty(ctr.SERIA2) ? "" : ctr.SERIA2.Trim(),
                            NOMER2 = String.IsNullOrEmpty(ctr.NOMER2) ? "" : ctr.NOMER2.Trim(),
                            DATDN2 = ctr.DATDN2,
                            DORGNAME2 = String.IsNullOrEmpty(ctr.DORGNAME2) ? "" : ctr.DORGNAME2.Trim(),
                            NAIM3 = String.IsNullOrEmpty(ctr.NAIM3) ? "" : ctr.NAIM3.Trim(),
                            SERIA3 = String.IsNullOrEmpty(ctr.SERIA3) ? "" : ctr.SERIA3.Trim(),
                            NOMER3 = String.IsNullOrEmpty(ctr.NOMER3) ? "" : ctr.NOMER3.Trim(),
                            DATDN3 = ctr.DATDN3,
                            DORGNAME3 = String.IsNullOrEmpty(ctr.DORGNAME3) ? "" : ctr.DORGNAME3.Trim(),
                            KOLLG = ctr.KOLLG,
                            HOZ = ctr.HOZ,
                            BIRTHPLACE = String.IsNullOrEmpty(ctr.BIRTHPLACE) ? "" : ctr.BIRTHPLACE.Trim(),
                            SOB = ctr.SOB,
                            DOLYA = ctr.DOLYA,
                            COMMENT = String.IsNullOrEmpty(ctr.COMMENT) ? "" : ctr.COMMENT.Trim(),
                            PRIBYT = String.IsNullOrEmpty(ctr.PRIBYT) ? "" : ctr.PRIBYT.Trim(),
                            VREMREG = String.IsNullOrEmpty(ctr.VREMREG) ? "" : ctr.VREMREG.Trim()

                        };
                        testcontext.lgotas.AddObject(lgot);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }

            // Сбрасываем список счетчиков в файл--------------------------------------------------20
            StepStart(dcounters.Count);
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                foreach (counter cr in dcounters.Values)
                {
                    //tmdest.InsertRecord(cr.GetInsertScript());               
                    try
                    {
                        counter count = new counter
                        {
                            COUNTERID = String.IsNullOrEmpty(cr.COUNTERID) ? "" : cr.COUNTERID.Trim(),
                            LSHET = String.IsNullOrEmpty(cr.LSHET) ? "" : cr.LSHET.Trim(),
                            CNTTYPE = cr.CNTTYPE,
                            CNTNAME = String.IsNullOrEmpty(cr.CNTNAME) ? "" : cr.CNTNAME.Trim(),
                            SETUPDATE = cr.SETUPDATE,
                            SERIALNUM = String.IsNullOrEmpty(cr.SERIALNUM) ? "" : cr.SERIALNUM.Trim(),
                            SETUPPLACE = Convert.ToInt32(cr.SETUPPLACE),
                            PLACE = String.IsNullOrEmpty(cr.PLACE) ? "" : cr.PLACE.Trim(),
                            PLOMBDATE = cr.PLOMBDATE,
                            PLOMBNAME = String.IsNullOrEmpty(cr.PLOMBNAME) ? "" : cr.PLOMBNAME.Trim(),
                            LASTPOV = cr.LASTPOV,
                            NEXTPOV = cr.NEXTPOV,
                            PRIM_ = String.IsNullOrEmpty(cr.PRIM_) ? "" : cr.PRIM_.Trim(),
                            DEACTDATE = cr.DEACTDATE,
                            TAG = String.IsNullOrEmpty(cr.TAG) ? "" : cr.TAG.Trim(),
                            NAME = String.IsNullOrEmpty(cr.NAME) ? "" : cr.NAME.Trim()

                        };
                        testcontext.counters.AddObject(count);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }

            // Сбрасываем показания счетчиков в файл---------------------------------------------------------------------------21
            StepStart(lcntrsind.Count);
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                foreach (cntrsind cir in lcntrsind)
                {
                    //tmdest.InsertRecord(cir.GetInsertScript());                
                    try
                    {
                        cntrsind cntrs = new cntrsind
                        {
                            COUNTERID = String.IsNullOrEmpty(cir.COUNTERID) ? "" : cir.COUNTERID.Trim(),
                            DOCUMENTCD = String.IsNullOrEmpty(cir.DOCUMENTCD) ? "" : cir.DOCUMENTCD.Trim(),
                            OLDIND = cir.OLDIND,
                            OB_EM = cir.OB_EM,
                            INDICATION = cir.INDICATION,
                            INDDATE = cir.INDDATE,
                            INDTYPE = cir.INDTYPE

                        };
                        testcontext.cntrsinds.AddObject(cntrs);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }

            // Сбрасываем качественные характеристики в файл -------------------------------------------------------------22
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                var thinedOutLcharsRecordList = LcharsRecordUtils.ThinOutList(listLcharRecord);
                StepStart(thinedOutLcharsRecordList.Count);
                foreach (lchar lcr in thinedOutLcharsRecordList)
                {
                    //tmdest.InsertRecord(lcr.GetInsertScript());                
                    try
                    {
                        lchar lch = new lchar
                        {
                            LSHET = String.IsNullOrEmpty(lcr.LSHET) ? "" : lcr.LSHET.Trim(),
                            LCHARCD = lcr.LCHARCD,
                            LCHARNAME = String.IsNullOrEmpty(lcr.LCHARNAME) ? "" : lcr.LCHARNAME.Trim(),
                            VALUE = lcr.VALUE,
                            VALUEDESC = String.IsNullOrEmpty(lcr.VALUEDESC) ? "" : lcr.VALUEDESC.Trim(),
                            DATE = lcr.DATE
                        };
                        testcontext.lchars.AddObject(lch);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }

            // Сбрасываем количественные характеристики в файл -----------------------------------------------------------23
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                var thinedOutCharsRecordList = CharsRecordUtils.ThinOutList(listCharsRecord);
                StepStart(thinedOutCharsRecordList.Count);
                foreach (@char cr in thinedOutCharsRecordList)
                {
                    //tmdest.InsertRecord(cr.GetInsertScript());
                    try
                    {
                        @char ch = new @char
                        {
                            LSHET = String.IsNullOrEmpty(cr.LSHET) ? "" : cr.LSHET.Trim(),
                            CHARCD = cr.CHARCD,
                            CHARNAME = String.IsNullOrEmpty(cr.CHARNAME) ? "" : cr.CHARNAME.Trim(),
                            VALUE = cr.VALUE,
                            DATE = cr.DATE
                        };
                        testcontext.chars.AddObject(ch);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            

        }
    }
}
