using System;
using System.Data;
using _048_Rgmek.Records;

namespace _048_Rgmek.NachImport
{
    public class OldFileNachImport : INachImport
    {
        private readonly NachExcelRecord.TarifType _tarif;
        private readonly NachExcelRecord.ZoneType _zone;

        public OldFileNachImport(NachExcelRecord.TarifType tarif, NachExcelRecord.ZoneType zone)
        {
            _tarif = tarif;
            _zone = zone;
        }

        public void Import(DataRow dr, Action<NachExcelRecord> importAction)
        {
            string ls = dr[1].ToString().Trim();
            if (!Consts.lsKvcRegex.IsMatch(ls)) return;

            // Индивидуальное потребление.
            var nachRecord = new NachExcelRecord
            {
                LsKvc = ls,
                Service = NachExcelRecord.ServiceType.Living,
                Nach = NachExcelRecord.NachType.WithoutDevice,
                Tarif = _tarif,
                Zone = _zone,
                Price = String.IsNullOrWhiteSpace(dr[3].ToString()) ? 0 : Convert.ToDecimal(dr[3].ToString()),
                People = 0,
                Volume = String.IsNullOrWhiteSpace(dr[2].ToString()) ? 0 : Convert.ToDecimal(dr[2].ToString()),
                Sum = String.IsNullOrWhiteSpace(dr[4].ToString()) ? 0 : Convert.ToDecimal(dr[4].ToString()),
                SumCoef = 0
            };
            importAction(nachRecord);

            // ОДН.
            nachRecord.Service = NachExcelRecord.ServiceType.Odn;
            nachRecord.Price = String.IsNullOrWhiteSpace(dr[6].ToString()) ? 0 : Convert.ToDecimal(dr[6].ToString());
            nachRecord.Volume = String.IsNullOrWhiteSpace(dr[5].ToString()) ? 0 : Convert.ToDecimal(dr[5].ToString());
            nachRecord.Sum = String.IsNullOrWhiteSpace(dr[7].ToString()) ? 0 : Convert.ToDecimal(dr[7].ToString());
            importAction(nachRecord);
        }
    }
}
