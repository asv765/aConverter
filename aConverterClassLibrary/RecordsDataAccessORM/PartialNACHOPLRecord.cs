using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class NACHOPL
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

        public static NACHOPL CreateFromKeySet(NachoplKeySet noks)
        {
            var nor = new NACHOPL()
            {
                LSHET = noks.Lshet,
                MONTH_ = (Int32)noks.Month,
                MONTH2 = (Int32)noks.Month,
                YEAR_ = (Int32)noks.Year,
                YEAR2 = (Int32)noks.Year,
                SERVICECD = (Int32)noks.Servicecd,
                SERVICENAM = String.Format("Услуга с кодом {0}", noks.Servicecd)
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
