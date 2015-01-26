using System;
using System.Collections.Generic;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    public class NachoplRecordUtils
    {
        public static NachoplRecord UpdateOrInsertNachoplRecord(NachoplRecord nr, ref Dictionary<NachoplKeySet, NachoplRecord> dnr)
        {
            NachoplRecord storedNachopl;
            if (!dnr.TryGetValue(nr.NachoplKeySet, out storedNachopl))
            {
                dnr.Add(nr.NachoplKeySet, nr);
                storedNachopl = nr;
            }
            else
            {
                storedNachopl.Bdebet += nr.Bdebet;
                storedNachopl.Fnath += nr.Fnath;
                storedNachopl.Prochl += nr.Prochl;
                storedNachopl.Oplata += nr.Oplata;
                storedNachopl.Edebet += nr.Edebet;
            }
            return storedNachopl;
        }
    }

    public partial class NachoplRecord
    {
        public NachoplKeySet NachoplKeySet
        {
            get
            {
                var nks = new NachoplKeySet() { Lshet = this.Lshet, 
                    Servicecd = this.Servicecd, 
                    Year = this.Year, 
                    Month = this.Month };
                return nks;
            }
        }

        public static NachoplRecord CreateFromKeySet(NachoplKeySet noks)
        {
            var nor = new NachoplRecord()
            {
                Lshet = noks.Lshet,
                Month = noks.Month,
                Month2 = noks.Month,
                Year = noks.Year,
                Year2 = noks.Year,
                Servicecd = noks.Servicecd,
                Servicenam = String.Format("Услуга с кодом {0}", noks.Servicecd)
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

        private Dictionary<NachoplKeySet, NachoplRecord> _nachoplRecords = new Dictionary<NachoplKeySet, NachoplRecord>();
        // Записи истории оплат/начислений
        public Dictionary<NachoplKeySet, NachoplRecord> NachoplRecords
        {
            get { return _nachoplRecords; }
            set { _nachoplRecords = value; }
        }

        private List<NachRecord> _nachRecords = new List<NachRecord>();
        // Записи начислений
        public List<NachRecord> NachRecords
        {
            get { return _nachRecords; }
            set { _nachRecords = value; }
        }

        private List<OplataRecord> _oplataRecords = new List<OplataRecord>();
        // Записи оплат
        public List<OplataRecord> OplataRecords
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
        private NachoplRecord GetActiveNachoplRecord(string lshet, int month, int year, long servicecd)
        {
            var noks = new NachoplKeySet()
            {
                Lshet = lshet,
                Month = month,
                Year = year,
                Servicecd = servicecd
            };

            NachoplRecord nr;
            if (!NachoplRecords.TryGetValue(noks, out nr))
            {
                nr = NachoplRecord.CreateFromKeySet(noks);
                NachoplRecords.Add(noks, nr);
            }

            return nr;
        }

        /// <summary>
        /// Зарегистрировать новый факт начисления
        /// </summary>
        public void RegisterNach(NachRecord defaultNachRecord, string lshet, int month, int year, decimal fnath, decimal prochl, 
            DateTime dateVv, string documentcd)
        {
            var nr = (NachRecord)defaultNachRecord.Clone();
            nr.Lshet = lshet;
            nr.Fnath = fnath;
            nr.Prochl = prochl;
            nr.Month = nr.Month2 = month;
            nr.Year = nr.Year2 = year;
            nr.Date_vv = dateVv;
            nr.Documentcd = documentcd;
            NachRecords.Add(nr);
            UpdateNachoplDicByNachRecord(nr);
        }

        private void UpdateNachoplDicByNachRecord(NachRecord nachRecord)
        {
            var nr = GetActiveNachoplRecord(nachRecord.Lshet,
                nachRecord.Date_vv.Month,
                nachRecord.Date_vv.Year,
                nachRecord.Servicecd);

            nr.Fnath += nachRecord.Fnath;
            nr.Prochl += nachRecord.Prochl;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.Edebet = nr.CalculatedEdebet;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.Bdebet = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Зарегистрировать новый факт оплаты
        /// </summary>
        public void RegisterOplata(OplataRecord defaultOplataRecord, string lshet, int month, int year, decimal summa, DateTime date, DateTime dateVv, string documentcd)
        {
            var or = (OplataRecord)defaultOplataRecord.Clone();
            or.Lshet = lshet;
            or.Summa = summa;
            or.Month = month;
            or.Year = year;
            or.Date = date;
            or.Date_vv = dateVv;
            or.Documentcd = documentcd;
            OplataRecords.Add(or);
            UpdateNachoplDicByOplataRecord(or);
        }

        private void UpdateNachoplDicByOplataRecord(OplataRecord oplataRecord)
        {
            var nr = GetActiveNachoplRecord(oplataRecord.Lshet,
                oplataRecord.Date_vv.Month, 
                oplataRecord.Date_vv.Year, 
                oplataRecord.Servicecd);

            nr.Oplata += oplataRecord.Summa;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.Edebet = nr.CalculatedEdebet;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.Bdebet = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на начало
        /// </summary>
        public void RegisterBeginSaldo(string lshet, int month, int year, int servicecd, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd);
            nr.Bdebet += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_конец)
                nr.Edebet = nr.CalculatedEdebet;
        }

        /// <summary>
        /// Зарегистрировать сальдо на конец
        /// </summary>
        public void RegisterEndSaldo(string lshet, int month, int year, int servicecd, decimal saldo)
        {
            var nr = GetActiveNachoplRecord(lshet, month, year, servicecd);
            nr.Edebet += saldo;
            if (_saldoCorrectionType == NachoplCorrectionType.Пересчитать_сальдо_на_начало)
                nr.Bdebet = nr.CalculatedBdebet;
        }

        /// <summary>
        /// Сохранить данные из NachoplRecords
        /// </summary>
        /// <param name="tableManager"></param>
        public void SaveNachoplRecords(TableManager tableManager)
        {
            foreach (NachoplRecord nor in NachoplRecords.Values)
            {
                tableManager.InsertRecord(nor.GetInsertScript());
            }            
        }

        /// <summary>
        /// Сохранить данные из NachRecords
        /// </summary>
        public void SaveNachRecords(TableManager tableManager)
        {
            foreach (NachRecord or in NachRecords)
            {
                tableManager.InsertRecord(or.GetInsertScript());
            }
        }

        /// <summary>
        /// Сохранить данные из OplataRecords
        /// </summary>
        public void SaveOplataRecords(TableManager tableManager)
        {
            foreach (OplataRecord or in OplataRecords)
            {
                tableManager.InsertRecord(or.GetInsertScript());
            }
        }
    }
}
