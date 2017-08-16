using System;
using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_NACHOPL : IOrmRecord
    {

        public NachoplKeySet NachoplKeySet
        {
            get
            {
                var nks = new NachoplKeySet()
                {
                    Lshet = LSHET,
                    Servicecd = SERVICECD,
                    Year = YEAR_,
                    Month = MONTH_
                };
                return nks;
            }
        }

        public static CNV_NACHOPL CreateFromKeySet(NachoplKeySet noks)
        {
            var nor = new CNV_NACHOPL()
            {
                LSHET = noks.Lshet,
                MONTH_ = (Int32)noks.Month,
                MONTH2 = (Int32)noks.Month,
                YEAR_ = (Int32)noks.Year,
                YEAR2 = (Int32)noks.Year,
                SERVICECD = (Int32)noks.Servicecd,
                SERVICENAME = String.Format("Услуга с кодом {0}", noks.Servicecd)
            };
            return nor;
        }

        public decimal CalculatedEdebet
        {
            get
            {
                return this.BDEBET + FNATH + PROCHL - OPLATA;
            }
        }

        public decimal CalculatedBdebet
        {
            get
            {
                return EDEBET - FNATH - PROCHL + OPLATA;
            }
        }

        public const string InsertSqlTemplate =
            "INSERT INTO CNV$NACHOPL (LSHET, MONTH_, YEAR_, MONTH2, YEAR2, BDEBET, FNATH, PROCHL, OPLATA, EDEBET, SERVICECD, SERVICENAME) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            _mONTH_,
            _yEAR_,
            _mONTH2,
            _yEAR2,
            ToSql(_bDEBET),
            ToSql(_fNATH),
            ToSql(_pROCHL),
            ToSql(_oPLATA),
            ToSql(_eDEBET),
            _sERVICECD,
            ToSql(_sERVICENAME));
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

}
