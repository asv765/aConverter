using System;
using System.Collections.Generic;
using aConverterClassLibrary.RecordsEDM;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{

    public class NachoplRecordUtils
    {
        public static NACHOPL UpdateOrInsertNachoplRecord(NACHOPL nr, ref Dictionary<NachoplKeySet, NACHOPL> dnr)
        {
            NACHOPL storedNachopl;
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


    public class NachoplManager
    {
        private readonly NachoplCorrectionType _saldoCorrectionType;

        private Dictionary<NachoplKeySet, NACHOPL> _nachoplRecords = new Dictionary<NachoplKeySet, NACHOPL>();
        // Записи истории оплат/начислений
        public Dictionary<NachoplKeySet, NACHOPL> NachoplRecords
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
        private NACHOPL GetActiveNachoplRecord(string lshet, int month, int year, long servicecd, string servicename)
        {
            var noks = new NachoplKeySet
            {
                Lshet = lshet,
                Month = month,
                Year = year,
                Servicecd = servicecd
            };

            NACHOPL nr;
            if (!NachoplRecords.TryGetValue(noks, out nr))
            {
                nr = NACHOPL.CreateFromKeySet(noks);
                nr.SERVICENAM = servicename;
                NachoplRecords.Add(noks, nr);
            }

            return nr;
        }

        /// <summary>
        /// Зарегистрировать новый факт начисления
        /// </summary>
        public void RegisterNach(nach defaultNachRecord, string lshet, int month, int year, decimal fnath,
            decimal prochl,
            DateTime dateVv, string documentcd)
        {
            var nr = new nach
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
        public void RegisterOplata(oplata defaultOplataRecord, string lshet, int month, int year, decimal summa,
            DateTime date, DateTime dateVv, string documentcd)
        {
            var or = new oplata
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
        public void RegisterBeginSaldo(string lshet, int month, int year, int servicecd, string servicename,
            decimal saldo)
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
            using (var context = new AbonentConvertationEntitiesModel(connectionString))
            {
                context.Add(NachoplRecords.Values);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Сохранить данные из NachRecords-------------------------------------------------------------15
        /// </summary>
        public void SaveNachRecords(string connectionString)
        {
            using(var context = new AbonentConvertationEntitiesModel(connectionString))
            {
                context.Add(NachRecords);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Сохранить данные из OplataRecords--------------------------------------------------------17
        /// </summary>
        public void SaveOplataRecords(string connectionString)
        {
            using (var context = new AbonentConvertationEntitiesModel(connectionString))
            {
                context.Add(OplataRecords);
                context.SaveChanges();
            }
        }
    }
}
