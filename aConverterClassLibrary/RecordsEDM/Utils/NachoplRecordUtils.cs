using System;
using System.Collections.Generic;
using DbfClassLibrary;
using System.Windows.Forms;
using System.Text;






namespace aConverterClassLibrary.RecordsEDM
{
    
    public class NachoplRecordUtils
    {
        public static nachopl UpdateOrInsertNachoplRecord(nachopl nr, ref Dictionary<NachoplKeySet, nachopl> dnr)
        {
            nachopl storedNachopl;
            if (!dnr.TryGetValue(nr.NachoplKeySet, out storedNachopl))
            {
                dnr.Add(nr.NachoplKeySet, nr);
                storedNachopl = nr;
            }
            else
            {
                storedNachopl.BDEBET += nr.BDEBET;
                storedNachopl.FNATH += nr.FNATH;
                storedNachopl.PROCHL += nr.PROCHL;
                storedNachopl.OPLATA += nr.OPLATA;
                storedNachopl.EDEBET += nr.EDEBET;
            }
            return storedNachopl;
        }
    }



   

    public partial class nachopl
    {
       
        public NachoplKeySet NachoplKeySet
        {
            get
            {
                var nks = new NachoplKeySet() { Lshet = this._LSHET, 
                    Servicecd = this._SERVICECD, 
                    Year = this._YEAR, 
                    Month = this._MONTH };
                return nks;
            }
        }

        public static nachopl CreateFromKeySet(NachoplKeySet noks)
        {
            var nor = new nachopl()
            {
                LSHET = noks.Lshet,
                MONTH = (Int32)noks.Month,
                MONTH2 = (Int32)noks.Month,
                YEAR = (Int32)noks.Year,
                YEAR2 = (Int32)noks.Year,
                SERVICECD = (Int32)noks.Servicecd,
                SERVICENAM = String.Format("Услуга с кодом {0}", noks.Servicecd)
            };
            return nor;
        }
    }

    public struct NachoplKeySet
    {
        // <summary>
        // LSHET C(10)
        // </summary>
        public string Lshet;

        // <summary>
        // SERVICECD N(5)
        // </summary>
        public Int64 Servicecd;

        // <summary>
        // YEAR N(6)
        // </summary>
        public Int64 Year;

        // <summary>
        // MONTH N(4)
        // </summary>
        public Int64 Month;
    }

    public class NachoplManager
    {
        private readonly NachoplCorrectionType _saldoCorrectionType;

        private Dictionary<NachoplKeySet, nachopl> _nachoplRecords = new Dictionary<NachoplKeySet, nachopl>();
        // Записи истории оплат/начислений
        public Dictionary<NachoplKeySet, nachopl> NachoplRecords
        {
            get { return _nachoplRecords; }
            set { _nachoplRecords = value; }
        }

        private List<nach> _nachRecords = new List<nach>();
        // Записи начислений
        public List<nach> NachRecords
        {
            get { return _nachRecords; }
            set { _nachRecords = value; }
        }

        private List<oplata> _oplataRecords = new List<oplata>();
        // Записи оплат
        public List<oplata> OplataRecords
        {
            get { return _oplataRecords; }
            set { _oplataRecords = value; }
        }

        public NachoplManager(NachoplCorrectionType saldoCorrectionType)
        {
            if (saldoCorrectionType == NachoplCorrectionType.Скорректировать_суммой_изменений)
                throw new Exception("При заполненнии списков корректировать сальдо суммой изменений нет возможности");
            _saldoCorrectionType = saldoCorrectionType;
        }

        /// <summary>
        /// Находит в словаре или создает и добавляет в словарь запись с данными по оплате/начислению для заданного абонента, месяца, года и услуги
        /// </summary>
        /// <param name="lshet"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="servicecd"></param>
        /// <returns></returns>
        private nachopl GetActiveNachoplRecord(string lshet, int month, int year, long servicecd)
        {
            var noks = new NachoplKeySet()
            {
                Lshet = lshet,
                Month = month,
                Year = year,
                Servicecd = servicecd
            };

            nachopl nr;
            if (!NachoplRecords.TryGetValue(noks, out nr))
            {
                nr = nachopl.CreateFromKeySet(noks);
                NachoplRecords.Add(noks, nr);
            }

            return nr;
        }

        /// <summary>
        /// Зарегистрировать новый факт начисления
        /// </summary>
        public void RegisterNach(nach defaultNachRecord, string lshet, int month, int year, decimal fnath, decimal prochl,
            DateTime dateVv, string documentcd)
        {
            NachRecords.Add(new nach()
            {
                TYPE = defaultNachRecord.TYPE,
                VOLUME = defaultNachRecord.VOLUME,
                REGIMCD = 10,
                REGIMNAME = defaultNachRecord.REGIMNAME,
                SERVICECD = defaultNachRecord.SERVICECD,
                SERVICENAM = defaultNachRecord.SERVICENAM,
                LSHET = lshet,
                FNATH = fnath,
                PROCHL = prochl,
                MONTH = month,
                MONTH2 = month,
                YEAR = year,
                YEAR2 = year,
                DATE_VV = dateVv
            });
            nach nr = defaultNachRecord;
            //var nr = (nach)defaultNachRecord;
            nr.LSHET = lshet;
            nr.FNATH = fnath;
            nr.PROCHL = prochl;
            nr.MONTH = nr.MONTH2 = month;
            nr.YEAR = nr.YEAR2 = year;
            nr.DATE_VV = dateVv;
            nr.DOCUMENTCD = documentcd;           
            //NachRecords.Add(nr);
            UpdateNachoplDicByNachRecord(nr);
        }

        private void UpdateNachoplDicByNachRecord(nach nachRecord)
        {
            var nr = GetActiveNachoplRecord(nachRecord.LSHET,
                nachRecord.DATE_VV.Month,
                nachRecord.DATE_VV.Year,
                nachRecord.SERVICECD);

            nr.FNATH += nachRecord.FNATH;
            nr.PROCHL += nachRecord.PROCHL;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.EDEBET = nr.CalculatedEdebet;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.BDEBET = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Зарегистрировать новый факт оплаты
        /// </summary>
        public void RegisterOplata(oplata defaultOplataRecord, string lshet, int month, int year, decimal summa, DateTime date, DateTime dateVv, string documentcd)
        {
            OplataRecords.Add(new oplata()
            {
                SERVICECD = defaultOplataRecord.SERVICECD,
                SERVICENAM = defaultOplataRecord.SERVICENAM,
                SOURCECD = defaultOplataRecord.SOURCECD,
                SOURCENAME= defaultOplataRecord.SOURCENAME,
                LSHET = lshet,
                SUMMA = summa,
                MONTH = month,
                YEAR = year,
                DATE = date,
                DATE_VV = dateVv,
                DOCUMENTCD = documentcd
            });            
            
            oplata or = defaultOplataRecord;
            //var or = (oplata)defaultOplataRecord;
            or.LSHET = lshet;
            or.SUMMA = summa;
            or.MONTH = month;
            or.YEAR = year;
            or.DATE = date;
            or.DATE_VV = dateVv;
            or.DOCUMENTCD = documentcd;
            //OplataRecords.Add(or);
            UpdateNachoplDicByOplataRecord(or);
        }

        private void UpdateNachoplDicByOplataRecord(oplata oplataRecord)
        {
            var nr = GetActiveNachoplRecord(oplataRecord.LSHET,
                oplataRecord.DATE_VV.Month,
                oplataRecord.DATE_VV.Year,
                oplataRecord.SERVICECD);

            nr.OPLATA += oplataRecord.SUMMA;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.EDEBET = nr.CalculatedEdebet;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.BDEBET = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на начало
        /// </summary>
        public void RegisterBeginSaldo(string lshet, int month, int year, int servicecd, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd);
            nr.BDEBET += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.EDEBET = nr.CalculatedEdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на конец
        /// </summary>
        public void RegisterEndSaldo(string lshet, int month, int year, int servicecd, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd);
            nr.EDEBET += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.BDEBET = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Сохранить данные из NachoplRecords----------------------------------------------------------16
        /// </summary>
        /// <param name="tableManager"></param>
        public void SaveNachoplRecords(TableManager tableManager)
        {
            using(ConverterdbEntities testcontext = new ConverterdbEntities())
            {
            foreach (nachopl nor in NachoplRecords.Values)
            {                
                try
                {
                    nachopl nachislopl = new nachopl
                    {
                        LSHET = String.IsNullOrEmpty(nor.LSHET) ? "" : nor.LSHET.Trim(),
                        MONTH = nor.MONTH,
                        YEAR = nor.YEAR,
                        MONTH2 = nor.MONTH2,
                        YEAR2 = nor.YEAR2,
                        BDEBET = nor.BDEBET,
                        FNATH = nor.FNATH,
                        PROCHL = nor.PROCHL,
                        OPLATA = nor.OPLATA,
                        EDEBET = nor.EDEBET,
                        SERVICECD = nor.SERVICECD,
                        SERVICENAM = String.IsNullOrEmpty(nor.SERVICENAM) ? "" : nor.SERVICENAM.Trim()
                    };
                    testcontext.nachopls.AddObject(nachislopl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
            testcontext.SaveChanges();
        }
        }

        /// <summary>
        /// Сохранить данные из NachRecords-------------------------------------------------------------15
        /// </summary>
        public void SaveNachRecords(TableManager tableManager)
        {
            using (ConverterdbEntities testcontext = new ConverterdbEntities()) 
            {
                foreach (nach or in NachRecords)
                {
                    //tableManager.InsertRecord(or.GetInsertScript());
                    try
                    {
                       // string myString = "Антон";
                        //byte[] bytes = Encoding.Default.GetBytes(myString);
                        nach nachisl = new nach
                        {
                            LSHET = String.IsNullOrEmpty(or.LSHET) ? "" : or.LSHET.Trim(),
                            DOCUMENTCD = String.IsNullOrEmpty(or.DOCUMENTCD) ? "" : or.DOCUMENTCD.Trim(),
                            MONTH = or.MONTH,
                            YEAR = or.YEAR,
                            MONTH2 = or.MONTH2,
                            YEAR2 = or.YEAR2,
                            FNATH = or.FNATH,
                            PROCHL = or.PROCHL,
                            VOLUME = or.VOLUME,
                            REGIMCD = or.REGIMCD,
                            //REGIMNAME = Encoding.UTF8.GetString(bytes),
                            REGIMNAME = String.IsNullOrEmpty(or.REGIMNAME) ? "" : or.REGIMNAME.Trim(),                            
                            SERVICECD = or.SERVICECD,
                            SERVICENAM = String.IsNullOrEmpty(or.SERVICENAM) ? "" : or.SERVICENAM.Trim(),
                            DATE_VV = or.DATE_VV,
                            TYPE = or.TYPE,
                            DOCNAME = String.IsNullOrEmpty(or.DOCNAME) ? "" : or.DOCNAME.Trim(),
                            DOCNUMBER = String.IsNullOrEmpty(or.DOCNUMBER) ? "" : or.DOCNUMBER.Trim(),
                            DOCDATE = or.DOCDATE
                        };
                        testcontext.naches.AddObject(nachisl);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                testcontext.SaveChanges();
            }
        }


        /// <summary>
        /// Сохранить данные из OplataRecords--------------------------------------------------------17
        /// </summary>
        public void SaveOplataRecords(TableManager tableManager)
        {
            using (ConverterdbEntities testcontext = new ConverterdbEntities())
            {
                foreach (oplata or in OplataRecords)
                {
                    //tableManager.InsertRecord(or.GetInsertScript());
                    try
                    {
                        oplata oplat = new oplata
                        {
                            LSHET = String.IsNullOrEmpty(or.LSHET) ? "" : or.LSHET.Trim(),
                            DOCUMENTCD = String.IsNullOrEmpty(or.DOCUMENTCD) ? "" : or.DOCUMENTCD.Trim(),
                            MONTH = or.MONTH,
                            YEAR = or.YEAR,
                            SUMMA = or.SUMMA,
                            DATE = or.DATE,
                            DATE_VV = or.DATE_VV,
                            DATETIND = or.DATETIND,
                            SOURCECD = or.SOURCECD,
                            SOURCENAME = String.IsNullOrEmpty(or.SOURCENAME) ? "" : or.SOURCENAME.Trim(),
                            SERVICECD = or.SERVICECD,
                            SERVICENAM = String.IsNullOrEmpty(or.SERVICENAM) ? "" : or.SERVICENAM.Trim(),
                            PRIM_ = String.IsNullOrEmpty(or.PRIM_) ? "" : or.PRIM_.Trim()
                        };
                        testcontext.oplatas.AddObject(oplat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
                testcontext.SaveChanges();
            }
        }        
    }
}

