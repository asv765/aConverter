using System;
using System.ComponentModel;
using System.Data;

namespace _048_Rgmek.Records
{
    public class ExtSaldo
    {
        public string LsKvc;
        public decimal Saldo;
        public DateTime Date;

        public ExtSaldo(DataRow dr, ExternalType type)
        {
            switch (type)
            {
                case ExternalType.NotFromKvc:
                    LsKvc = Convert.ToString(dr["ACCOUNT"]);
                    Saldo = Convert.ToDecimal(dr["DEBT"]);
                    Date = Convert.ToDateTime(dr["DATE"]);
                    break;
                case ExternalType.Acts:
                    LsKvc = Convert.ToString(dr[0]);
                    Saldo = Convert.ToDecimal(dr[1]);
                    Date = Convert.ToDateTime(dr[2]);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ExternalType));
            }
        }

        public enum ExternalType
        {
            NotFromKvc,
            Acts
        }
    }
}
