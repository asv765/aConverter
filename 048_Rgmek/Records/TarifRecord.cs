using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace _048_Rgmek.Records
{
    public class TarifRecord
    {
        public string Lshet;
        public int TarifCd;
        public string TarifName;
        public DateTime Date;

        public TarifRecord(DataRow dr)
        {
            Lshet = dr["lshet"].ToString();
            TarifCd = Convert.ToInt32(dr["tarifcd_c"]);
            TarifName = dr["tarifnm_c"].ToString();
            Date = Convert.ToDateTime(dr["date"]);
        }
    }
}