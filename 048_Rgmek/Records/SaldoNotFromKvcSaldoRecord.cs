using System;
using System.Data;

namespace _048_Rgmek.Records
{
    public class SaldoNotFromKvcSaldoRecord
    {
        public string LsKvc;
        public decimal Saldo;
        public DateTime Date;

        public SaldoNotFromKvcSaldoRecord(DataRow dr)
        {
            LsKvc = Convert.ToString(dr["ACCOUNT"]);
            Saldo = Convert.ToDecimal(dr["DEBT"]);
            Date = Convert.ToDateTime(dr["DATE"]);
        }
    }
}
