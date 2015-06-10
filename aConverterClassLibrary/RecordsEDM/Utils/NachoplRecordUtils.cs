using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Globalization;
using MySql.Data.MySqlClient;

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
        /// <param name="servicename"></param>
        /// <returns></returns>
        private nachopl GetActiveNachoplRecord(string lshet, int month, int year, long servicecd, string servicename)
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
                nr.SERVICENAM = servicename;
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
            var nr = new nach()
            {
                TYPE = defaultNachRecord.TYPE,
                VOLUME = defaultNachRecord.VOLUME,
                REGIMCD = defaultNachRecord.REGIMCD,
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
                DATE_VV = dateVv,
                DOCUMENTCD = documentcd
            };
            NachRecords.Add(nr);
            UpdateNachoplDicByNachRecord(nr);
        }

        private void UpdateNachoplDicByNachRecord(nach nachRecord)
        {
            var nr = GetActiveNachoplRecord(nachRecord.LSHET,
                nachRecord.DATE_VV.Month,
                nachRecord.DATE_VV.Year,
                nachRecord.SERVICECD,
                nachRecord.SERVICENAM);

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
            var or = new oplata()
            {
                SERVICECD = defaultOplataRecord.SERVICECD,
                SERVICENAM = defaultOplataRecord.SERVICENAM,
                SOURCECD = defaultOplataRecord.SOURCECD,
                SOURCENAME = defaultOplataRecord.SOURCENAME,
                LSHET = lshet,
                SUMMA = summa,
                MONTH = month,
                YEAR = year,
                DATE = date,
                DATE_VV = dateVv,
                DOCUMENTCD = documentcd
            };
            OplataRecords.Add(or);
            UpdateNachoplDicByOplataRecord(or);
        }

        private void UpdateNachoplDicByOplataRecord(oplata oplataRecord)
        {
            var nr = GetActiveNachoplRecord(oplataRecord.LSHET,
                oplataRecord.DATE_VV.Month,
                oplataRecord.DATE_VV.Year,
                oplataRecord.SERVICECD,
                oplataRecord.SERVICENAM);

            nr.OPLATA += oplataRecord.SUMMA;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.EDEBET = nr.CalculatedEdebet;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.BDEBET = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на начало
        /// </summary>
        public void RegisterBeginSaldo(string lshet, int month, int year, int servicecd, string servicename, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd, servicename);
            nr.BDEBET += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.EDEBET = nr.CalculatedEdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на конец
        /// </summary>
        public void RegisterEndSaldo(string lshet, int month, int year, int servicecd, string servicename, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd, servicename);
            nr.EDEBET += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.BDEBET = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Сохранить данные из NachoplRecords----------------------------------------------------------16
        /// </summary>
        public void SaveNachoplRecords(string connectionString)
        {
            var context = new ConverterdbEntities(connectionString);
            int i = 0;
            foreach (nachopl nor in NachoplRecords.Values)
            {
                if ((++i%1000) == 0)
                {
                    context.SaveChanges();
                    context.Dispose();
                    context = new ConverterdbEntities(connectionString);
                }
                context.nachopls.AddObject(nor);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Сохранить данные из NachRecords-------------------------------------------------------------15
        /// </summary>
        public void SaveNachRecords(string connectionString)
        {
            //var context = new ConverterdbEntities(connectionString);
            //int step = 1000;
            //var cloneList = new List<nach>();
            //for (int i = 0; i < NachRecords.Count; i++)
            //{
            //    nach cloneNach = NachRecords[i].Clone();
            //    cloneList.Add(cloneNach);
            //    context.naches.AddObject(cloneNach);
            //    if (((i+1) % step) == 0)
            //    {
            //        context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            //        cloneList.Clear();
            //        cloneList = new List<nach>();
            //        var entityConnection = context.Connection;
            //        entityConnection.Dispose();
            //        context.Dispose();
            //        context = new ConverterdbEntities(connectionString);
            //    }
            //}
            //context.SaveChanges();
            using (MySqlConnection msconn = new MySqlConnection(aConverter_RootSettings.DestMySqlConnectionString))
            {
                msconn.Open();
                var msc = new MySqlCommand {Connection = msconn};
                foreach (nach nachRecord in NachRecords)
                {
                    msc.CommandText = nachRecord.InsertCommand;
                    msc.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Сохранить данные из OplataRecords--------------------------------------------------------17
        /// </summary>
        public void SaveOplataRecords(string connectionString)
        {
            var context = new ConverterdbEntities(connectionString);
            int i = 1;
            foreach (oplata or in OplataRecords)
            {
                if ((++i%1000) == 0)
                {
                    context.SaveChanges();
                    context.Dispose();
                    context = new ConverterdbEntities(connectionString);
                }
                context.oplatas.AddObject(or);
            }
            context.SaveChanges();
        }
    }
}

