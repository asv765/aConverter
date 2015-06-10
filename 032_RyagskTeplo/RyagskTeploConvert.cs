using System.Collections.Generic;
using System.Data.EntityClient;
using System.Globalization;
using System.Linq;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsEDM;
using aConverterClassMariaDB;
using System;
using System.Data;
using DbfClassLibrary;
using MySql.Data.MySqlClient;

namespace _032_RyagskTeplo
{
    public static class Consts
    {
        public static DateTime StartDate = new DateTime(2010,1,1);

        public static int CurrentYear = 2015;

        public static int CurrentMonth = 6;

        public static DateTime CurrentDateTime = new DateTime(CurrentYear, CurrentMonth, 1);

        public static int LastYear = CurrentMonth == 1 ? CurrentYear - 1 : CurrentYear;

        public static int LastMonth = CurrentMonth == 1 ? 12 : CurrentMonth - 1;

        public const string SourceDir = @"C:\Work\aConverter_Data\032_RyagskTeplo\Source\dbf";

        public static string GetLs(long intls)
        {
            return String.Format("79{0:D6}", intls);
        }

        public static string ConnectionString
        {
            get
            {
                var ecsb = new EntityConnectionStringBuilder
                {
                    Provider = "MySql.Data.MySqlClient",
                    ProviderConnectionString = aConverter_RootSettings.DestMySqlConnectionString,
                    Metadata = @".\Metadata\MariadbEDM.csdl|.\Metadata\MariadbEDM.ssdl|.\Metadata\MariadbEDM.msl"
                };
                return ecsb.ConnectionString;
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
            ConvertCaseName = "Создать базу данных для конвертации";
            Position = 10;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            ExecuteScript.CreateDatabaseMariaDb(aConverter_RootSettings.DestMySqlDatabaseName);

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
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
            tms.Init();

            #region 1. Грузим справочник абонентов

            var dsprrab = new Dictionary<long, SprrabRecord>();
            var dt = tms.GetDataTable("SPRRAB");
            StepStart(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                var sprrab = new SprrabRecord();
                sprrab.ReadDataRow(dr);
                dsprrab.Add(sprrab.Ls, sprrab);
                Iterate();
            }
            StepFinish();

            #endregion

            #region 2. Грузим справочник домов

            var dsprdom = new Dictionary<long, SprdomRecord>();
            dt = tms.GetDataTable("SPRDOM");
            StepStart(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                var sprdom = new SprdomRecord();
                sprdom.ReadDataRow(dr);
                dsprdom.Add(sprdom.Codc, sprdom);
                Iterate();
            }
            StepFinish();

            #endregion

            #region 3. Заполняем справочник абонентов
            var labonent = new List<abonent>();
            var dhaddchar = new Dictionary<string,haddchar>();
            var dstreet = new Dictionary<string, int>();
            StepStart(dsprrab.Count);
            foreach (SprrabRecord sr in dsprrab.Values)
            {
                var ar = new abonent();
                ar.LSHET = Consts.GetLs(sr.Ls);
                ar.EXTLSHET = sr.Ls.ToString(CultureInfo.InvariantCulture);
                ar.F = sr.Fam;
                ar.I = sr.Imia;
                ar.O = sr.Otch;

                ar.DISTKOD = ar.RAYONKOD = ar.TOWNSKOD = 1;
                ar.DISTNAME = ar.RAYONNAME = ar.TOWNSNAME = "Ряжск";

                ar.HOUSECD = (int)sr.Addres;

                var sd = dsprdom[sr.Addres];
                ar.NDOMA = sd.Domc.ToString(CultureInfo.InvariantCulture) + sd.Dom_alpha;
                ar.KORPUS = 0;

                if (sd.Sob != 0)
                {
                    haddchar hac;
                    string key = String.Format("{0}_{1}", ar.HOUSECD, 206001);
                    if (!dhaddchar.TryGetValue(key, out hac))
                    {
                        hac = new haddchar()
                        {
                            ADDCHARCD = 206001,
                            HOUSECD = ar.HOUSECD,
                            VALUE = sd.Sob.ToString(CultureInfo.InvariantCulture)
                        };
                        dhaddchar.Add(key, hac);
                    }
                }
                if (sd.Sscale != 0)
                {
                    haddchar hac;
                    string key = String.Format("{0}_{1}", ar.HOUSECD, 32010);
                    if (!dhaddchar.TryGetValue(key, out hac))
                    {
                        hac = new haddchar()
                        {
                            ADDCHARCD = 32010,
                            HOUSECD = ar.HOUSECD,
                            VALUE = sd.Sscale.ToString(CultureInfo.InvariantCulture)
                        };
                        dhaddchar.Add(key, hac);
                    }
                }

                int ulicakod;
                if (!dstreet.TryGetValue(sd.Ulic, out ulicakod))
                {
                    if (dstreet.Count == 0)
                        ulicakod = 1;
                    else
                        ulicakod = dstreet.Values.Max() + 1;
                    dstreet.Add(sd.Ulic, ulicakod);
                }
                ar.ULICAKOD = ulicakod;
                ar.ULICANAME = sd.Ulic;

                ar.DUCD = (int)sd.Uchac;
                ar.DUNAME = String.Format("Участок {0}", sd.Uchac);
                ar.KVARTIRA = sr.Kvar.ToString(CultureInfo.InvariantCulture) + sr.Kvar_a;
                ar.ISDELETED = sr.Close ? 1 : 0;
                ar.PRIM_ = sr.Close ? "Закрыт " + sr.Datclose.ToString(CultureInfo.InvariantCulture) : "";

                labonent.Add(ar);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 4. Сохраняем справочник абонентов в базу
            StepStart(labonent.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE ABONENT");
                foreach (var abonentRecord in labonent)
                {
                    testcontext.abonents.AddObject(abonentRecord);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion

            #region 5. Сохраняем справочник дополнительных характеристик дома
            StepStart(dhaddchar.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE HADDCHAR");
                foreach (var haddchar in dhaddchar.Values)
                {
                    testcontext.haddchars.AddObject(haddchar);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion

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
            IsChecked = true;
        }

        public uint CurrentLshet;

        public override void DoConvert()
        {
            SetStepsCount(5);

            var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
            tms.Init();

            #region 1. Грузим справочник абонентов (для конвертации площади)
            var dsprrab = new Dictionary<long, SprrabRecord>();
            var dt = tms.GetDataTable("SPRRAB");
            StepStart(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                var sprrab = new SprrabRecord();
                sprrab.ReadDataRow(dr);
                dsprrab.Add(sprrab.Ls, sprrab);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 2. Заполняем справочник площадей и текущее значение числа проживающих
            var lchars = new List<@char>();
            StepStart(dsprrab.Values.Count);
            foreach (SprrabRecord dr in dsprrab.Values)
            {
                var c = new @char
                {
                    CHARCD = 2,
                    CHARNAME = "Общая площадь",
                    DATE = Consts.StartDate,
                    LSHET = Consts.GetLs(dr.Ls),
                    VALUE = dr.S2
                };
                lchars.Add(c);

                c = new @char
                {
                    CHARCD = 1,
                    CHARNAME = "Число проживающих",
                    DATE = Consts.CurrentDateTime,
                    LSHET = Consts.GetLs(dr.Ls),
                    VALUE = dr.Kol
                };
                lchars.Add(c);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 3. Грузим справочник ArcSr и заполняем по нему данные в списке характеристик
            dt = tms.GetDataTable("ARCSR");
            StepStart(dt.Rows.Count);
            var asr = new ArcsrRecord();
            foreach (DataRow row in dt.Rows)
            {
                asr.ReadDataRow(row);
                string year = asr.Dat.Substring(0, 4);
                string month = asr.Dat.Substring(4, 2);
                var c = new @char
                {
                    CHARCD = 1,
                    CHARNAME = "Число проживающих",
                    LSHET = Consts.GetLs(asr.Ls),
                    DATE = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1),
                    VALUE = asr.Kol
                };
                lchars.Add(c);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 4. Прореживаем список характеристик
            StepStart(1);
            var lcharsto = CharsRecordUtils.ThinOutList(lchars);
            StepFinish();
            #endregion

            #region 5. Сохраняем справочник количественных характеристик в базу
            StepStart(lcharsto.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE CHARS");
                foreach (var charRecord in lcharsto)
                {
                    testcontext.chars.AddObject(charRecord);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion
        }
    }

    /// <summary>
    /// Конвертирует данные о качественных характеристиках
    /// </summary>
    public class ConvertLChars : ConvertCase
    {
        public ConvertLChars()
        {
            ConvertCaseName = "LCHARS - данные о качественных характеристиках";
            Position = 40;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            var tms = new TableManager(aConverter_RootSettings.SourceDBFFilePath);
            tms.Init();

            #region 1. Проходим по справочнику абонентов
            var llchars = new List<@lchar>();
            var dt = tms.GetDataTable("SPRRAB");
            StepStart(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                var sprrab = new SprrabRecord();
                sprrab.ReadDataRow(dr);
                string lshet = Consts.GetLs(sprrab.Ls);
                for (int i = 0; i < 5; i++)
                {
                    string tarif = sprrab.Tarifi.Substring(i*3, 3);
                    if (!String.IsNullOrWhiteSpace(tarif))
                        llchars.AddRange(GetLcharListByTarif(Convert.ToInt32(tarif), lshet));
                }
                Iterate();
            }
            StepFinish();
            #endregion

            #region 2. Сохраняем справочник качественных характеристик в базу

            llchars = LcharsRecordUtils.ThinOutList(llchars);
            StepStart(llchars.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE LCHARS");
                foreach (var lcharRecord in llchars)
                {
                    testcontext.lchars.AddObject(lcharRecord);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion
        }

        public List<lchar> GetLcharListByTarif(int tarifCd, string lshet)
        {
            var llchar = new List<lchar>();
            if (tarifCd == 4)
            {
                llchar.Add(new lchar()
                {
                    LCHARCD = 11,
                    LCHARNAME = "Режим центрального отопления",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 1,
                    VALUEDESC = "Наличие центрального отопления"
                });
            }
            else if (tarifCd == 36 || tarifCd == 38)
            {
                llchar.Add(new lchar()
                {
                    LCHARCD = 6,
                    LCHARNAME = "ЦГВС",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 1,
                    VALUEDESC = "ГВС включено"
                });
                llchar.Add(new lchar()
                {
                    LCHARCD = 19,
                    LCHARNAME = "Счетчик горячей воды",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 0,
                    VALUEDESC = "Без счетчика"
                });
            }
            else if (tarifCd == 37 || tarifCd == 39)
            {
                llchar.Add(new lchar()
                {
                    LCHARCD = 6,
                    LCHARNAME = "ЦГВС",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 1,
                    VALUEDESC = "ГВС включено"
                });
                llchar.Add(new lchar()
                {
                    LCHARCD = 19,
                    LCHARNAME = "Счетчик горячей воды",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 1,
                    VALUEDESC = "По счетчику"
                });
            }
            else if (tarifCd == 40)
            {
                llchar.Add(new lchar()
                {
                    LCHARCD = 118,
                    LCHARNAME = "ГВС общедомовые нужды",
                    LSHET = lshet,
                    DATE = Consts.StartDate,
                    VALUE = 1,
                    VALUEDESC = "ГВС ОДН"
                });
            }
            return llchar;
        }
    }

    public class ConvertNachopl : ConvertCase
    {
        public ConvertNachopl()
        {
            ConvertCaseName = "NACHOPL - данные об истории оплат/начислений";
            Position = 50;
            IsChecked = true;            
        }

        public override void DoConvert()
        {
            SetStepsCount(7);

            var nom = new NachoplManager(NachoplCorrectionType.Пересчитать_сальдо_на_конец);

            #region 1. Данные из файла сальдо ARCSR.DBF
            var dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM ARCSR");
            StepStart(dt.Rows.Count);
            var asr = new ArcsrRecord();
            foreach (DataRow row in dt.Rows)
            {
                asr.ReadDataRow(row);
                string lshet = Consts.GetLs(asr.Ls);
                int month = Convert.ToInt32(asr.Dat.Substring(4, 2));
                int year = Convert.ToInt32(asr.Dat.Substring(0, 4));
                
                int servicecd;
                string servicename;

                getNewServiceData(4, out servicecd, out servicename);
                nom.RegisterBeginSaldo(lshet, month, year, servicecd, servicename, -asr.Saldo4);

                getNewServiceData(6, out servicecd, out servicename);
                nom.RegisterBeginSaldo(lshet, month, year, servicecd, servicename, -asr.Saldo6);

                getNewServiceData(7, out servicecd, out servicename);
                nom.RegisterBeginSaldo(lshet, month, year, servicecd, servicename, -asr.Saldo7);

                getNewServiceData(9, out servicecd, out servicename);
                nom.RegisterBeginSaldo(lshet, month, year, servicecd, servicename, -asr.Saldo9);

                getNewServiceData(11, out servicecd, out servicename);
                nom.RegisterBeginSaldo(lshet, month, year, servicecd, servicename, -asr.Saldo11);

                Iterate();
            }
            StepFinish();

            #endregion

            #region 2. Данные из файла начислений ROF.DBF
            dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM ROF");
            StepStart(dt.Rows.Count);
            var rr = new RofRecord();
            var defaultNachRecord = new nach()
            {
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                TYPE = 0,
                VOLUME = 0
            };
            foreach (DataRow row in dt.Rows)
            {
                rr.ReadDataRow(row);
                string lshet = Consts.GetLs(rr.Ls);
                int month = Convert.ToInt32(rr.Dat.Substring(4, 2));
                int year = Convert.ToInt32(rr.Dat.Substring(0, 4));
                var dateVv = new DateTime(year, month, 1);
                string documentcd = String.Format("ROF_") + row["RECNO"].ToString();
                int servicecd = -1;
                string servicename = "Неизвестна";

                if (getNewServiceData(rr.Usluga1, out servicecd, out servicename) && rr.Sum1 != 0)
                {
                    defaultNachRecord.SERVICECD = servicecd;
                    defaultNachRecord.SERVICENAM = servicename;
                    nom.RegisterNach(defaultNachRecord, lshet, month, year, rr.Sum1, 0, dateVv, documentcd);
                }

                if (getNewServiceData(rr.Usluga2, out servicecd, out servicename) && rr.Sum2 != 0)
                {
                    defaultNachRecord.SERVICECD = servicecd;
                    defaultNachRecord.SERVICENAM = servicename;
                    nom.RegisterNach(defaultNachRecord, lshet, month, year, rr.Sum2, 0, dateVv, documentcd);
                }

                if (getNewServiceData(rr.Usluga3, out servicecd, out servicename) && rr.Sum3 != 0)
                {
                    defaultNachRecord.SERVICECD = servicecd;
                    defaultNachRecord.SERVICENAM = servicename;
                    nom.RegisterNach(defaultNachRecord, lshet, month, year, rr.Sum3, 0, dateVv, documentcd);
                }

                Iterate();
            }
            StepFinish();
            #endregion

            #region 3. Данные из файла корректировок KORRAS.DBF
            dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM KORRAS");
            StepStart(dt.Rows.Count);
            var krr = new KorrasRecord();
            defaultNachRecord = new nach()
            {
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                TYPE = 0,
                VOLUME = 0
            };
            foreach (DataRow row in dt.Rows)
            {
                krr.ReadDataRow(row);
                string lshet = Consts.GetLs(krr.Ls);
                int month = Convert.ToInt32(krr.Dat.Substring(4, 2));
                int year = Convert.ToInt32(krr.Dat.Substring(0, 4));
                int month2 = month;
                int year2 = year;
                if (!String.IsNullOrWhiteSpace(krr.Godmes))
                {
                    month2 = Convert.ToInt32(krr.Godmes.Substring(4, 2));
                    year2 = Convert.ToInt32(krr.Godmes.Substring(0, 4));
                }
                var dateVv = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                string documentcd = String.Format("KORRAS_") + row["RECNO"].ToString();
                int servicecd = -1;
                string servicename = "Неизвестна";

                if (getNewServiceData(krr.Usluga, out servicecd, out servicename) && krr.Sum != 0)
                {
                    defaultNachRecord.SERVICECD = servicecd;
                    defaultNachRecord.SERVICENAM = servicename;
                    defaultNachRecord.MONTH2 = month2;
                    defaultNachRecord.YEAR2 = year2;
                    defaultNachRecord.DOCDATE = krr.Datras;
                    defaultNachRecord.DOCNAME = krr.Info;
                    nom.RegisterNach(defaultNachRecord, lshet, month, year, 0, krr.Sum, dateVv, documentcd);
                }

                Iterate();
            }
            StepFinish();
            #endregion

            #region 4. Данные из файла оплат PAY.DBF
            dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM RIF");
            StepStart(dt.Rows.Count);
            var rifr = new RifRecord();
            var defaultOplataRecord = new oplata()
            {
                SOURCECD = 17,
                SOURCENAME = "Касса"
            };
            foreach (DataRow row in dt.Rows)
            {
                rifr.ReadDataRow(row);
                string lshet = Consts.GetLs(rifr.Ls);
                string documentcd = String.Format("PAY_") + row["RECNO"].ToString();
                int servicecd = -1;
                string servicename = "Неизвестна";

                if (getNewServiceData(rifr.Usluga, out servicecd, out servicename) && rifr.Sum != 0)
                {
                    defaultOplataRecord.SERVICECD = servicecd;
                    defaultOplataRecord.SERVICENAM = servicename;
                    nom.RegisterOplata(defaultOplataRecord, lshet, rifr.Dat.Month, rifr.Dat.Year, rifr.Sum, rifr.Dat, rifr.Dat, documentcd);
                }
                Iterate();
            }
            StepFinish();
            #endregion

            #region 5. Сохраняем данные в таблицу NACH
            StepStart(1);
            (new ConverterdbEntities(Consts.ConnectionString)).ExecuteStoreCommand("TRUNCATE TABLE NACH");
            nom.SaveNachRecords(Consts.ConnectionString);
            StepFinish();
            #endregion

            #region 6. Сохраняем данные в таблицу NACHOPL
            StepStart(1);
            (new ConverterdbEntities(Consts.ConnectionString)).ExecuteStoreCommand("TRUNCATE TABLE NACHOPL");
            nom.SaveNachoplRecords(Consts.ConnectionString);
            StepFinish();
            #endregion

            #region 7. Сохраняем данные в таблицу OPLATA
            StepStart(1);
            (new ConverterdbEntities(Consts.ConnectionString)).ExecuteStoreCommand("TRUNCATE TABLE OPLATA");
            nom.SaveOplataRecords(Consts.ConnectionString);
            StepFinish();
            #endregion

        }

        private bool getNewServiceData(long usluga, out int servicecd, out string servicename)
        {
            servicecd = -1;
            servicename = "Неизвестна";
            if (usluga == 4)
            {
                servicecd = 3;
                servicename = "Отопление";
                return true;
            }
            if (usluga == 6)
            {
                servicecd = 5;
                servicename = "ХВС для ГВС";
                return true;
            }
            if (usluga == 11)
            {
                servicecd = 15;
                servicename = "Подогрев";
                return true;
            }
            if (usluga == 7)
            {
                servicecd = 105;
                servicename = "ГВС на общедомовые нужды";
                return true;
            }
            if (usluga == 9)
            {
                servicecd = 500;
                servicename = "Горячая вода (старые долги)";
                return true;
            }
            return false;
        }
    }

    public class ConvertCounters : ConvertCase
    {
        public ConvertCounters()
        {
            ConvertCaseName = "COUNTERS - данные о счетчиках";
            Position = 60;
            IsChecked = true;            
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            #region 1. Данные о счетчиках помещаем в список
            DataTable dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM COUNTERS");
            StepStart(dt.Rows.Count);
            var cr = new CountersRecord();

            var cnttypedic = new Dictionary<string, int>();
            var counterlist = new List<counter>();

            foreach (DataRow dataRow in dt.Rows)
            {
                cr.ReadDataRow(dataRow);
                if (cr.Ls == 0)
                {
                    Iterate();
                    continue;
                }
                string lshet = Consts.GetLs(cr.Ls);
                int cntkod = -1;
                if (!cnttypedic.TryGetValue(cr.Type.Trim(), out cntkod))
                {
                    if (cnttypedic.Count > 0)
                        cntkod = cnttypedic.Values.Max() + 1;
                    else
                        cntkod = 1;
                    cnttypedic.Add(cr.Type.Trim(), cntkod);
                }
                var c = new counter()
                {
                    LSHET = lshet,
                    CNTTYPE = cntkod,
                    CNTNAME = cr.Type,
                    COUNTERID = cr.Ls + "_" + cr.Code.ToString(CultureInfo.InvariantCulture),
                    DEACTDATE = cr.Date,
                    LASTPOV = cr.Datcheck1,
                    NEXTPOV = cr.Datcheck2,
                    NAME = cr.Place,
                    PLACE = cr.Place,
                    SETUPPLACE = 3, // Индивидуальный
                    PLOMBDATE = cr.Dats,
                    PLOMBNAME = cr.Stamp,
                    SERIALNUM = cr.Sn,
                    SETUPDATE = cr.Dats,
                    TAG = "COUNTERS_" + dataRow["RECNO"].ToString()
                };
                counterlist.Add(c);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 2. Сохраняем данные в таблицу
            StepStart(counterlist.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE COUNTERS");
                foreach (var counterRecord in counterlist)
                {
                    testcontext.counters.AddObject(counterRecord);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion

        }
    }

    public class ConvertCntrsind : ConvertCase
    {
        public ConvertCntrsind()
        {
            ConvertCaseName = "CNTRSIND - данные о показаниях счетчиков";
            Position = 70;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(2);

            #region 1. Данные о показаниях счетчиков помещаем в список
            DataTable dt = tmsource.ExecuteQuery("SELECT RECNO() AS RECNO, * FROM COUNTIND");
            StepStart(dt.Rows.Count);
            var cir = new CountindRecord();

            var cntrsindlist = new List<cntrsind>();

            foreach (DataRow dataRow in dt.Rows)
            {
                cir.ReadDataRow(dataRow);
                if (cir.Ls == 0)
                {
                    Iterate();
                    continue;
                }
                string lshet = Consts.GetLs(cir.Ls);
                var ci = new cntrsind()
                {
                    COUNTERID = cir.Ls + "_" + cir.Code.ToString(CultureInfo.InvariantCulture),
                    DOCUMENTCD = "COUNTIND_" + dataRow["RECNO"].ToString(),
                    INDDATE = cir.Datf,
                    INDICATION = cir.Indication,
                    INDTYPE = 0, // 0 - обычные, 1 - контрольные
                    OB_EM = 0,
                    OLDIND = 0
                };
                cntrsindlist.Add(ci);
                Iterate();
            }
            StepFinish();
            #endregion

            #region 2. Сохраняем данные в таблицу

            CntrsindRecordUtils.RestoreHistory(ref cntrsindlist, RestoreHistoryType.С_конца_по_конечным_показаниям);
            cntrsindlist = CntrsindRecordUtils.ThinOutList(cntrsindlist);

            StepStart(cntrsindlist.Count);
            using (var testcontext = new ConverterdbEntities(Consts.ConnectionString))
            {
                testcontext.ExecuteStoreCommand("TRUNCATE TABLE CNTRSIND");
                foreach (var cirRecord in cntrsindlist)
                {
                    testcontext.cntrsinds.AddObject(cirRecord);
                    Iterate();
                }
                testcontext.SaveChanges();
                StepFinish();
            }
            #endregion

        }
    }

    public class ConvertPeni : ConvertCase
    {
        public ConvertPeni()
        {
            ConvertCaseName = "PENI - данные о пени";
            Position = 80;
            IsChecked = true;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);

            #region 1. Данные о пени

            DataTable dt = tmsource.ExecuteQuery("SELECT * FROM SPRRAB WHERE PENI <> 0");
            StepStart(dt.Rows.Count);
            var sr = new SprrabRecord();

            using (MySqlConnection msconn = new MySqlConnection(aConverter_RootSettings.DestMySqlConnectionString))
            {
                msconn.Open();
                var msc = new MySqlCommand { Connection = msconn };

                foreach (DataRow dataRow in dt.Rows)
                {
                    sr.ReadDataRow(dataRow); 
                    string lshet = Consts.GetLs(sr.Ls);

                    string query = String.Format("INSERT INTO `peni` (`LSHET`, `SERVICECD`, `DOLGDATE`, `DOLG`) VALUES ('{0}', {1}, '{2}', {3})",
                        lshet, 3, getDataForSql(Consts.CurrentDateTime), sr.Peni);

                    //Consts.CurrentDateTime.ToString("yyyy-M-d");

                    msc.CommandText = query;
                    msc.ExecuteNonQuery();

                    Iterate();
                }
            }

            StepFinish();
            #endregion
        }

        private string getDataForSql(DateTime dt)
        {
            return String.Format("{0:D4}-{1:D2}-{2:D2}", dt.Year, dt.Month, dt.Day);
        }
    }


}


