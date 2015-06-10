using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class PunktError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public PunktError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей о населенных пунктах (ABONENT.TOWNSKOD и ABONENT.TOWNSNAME) отсутствуют в таблице PUNKT");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи о населенных пунктах",
                "SELECT TownsKod, Min(RayonKod) as RayonKod, Max(TownsName) as TownsName, Count(*) FROM ABONENT GROUP BY TownsKod",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица PUNKT",
                "SELECT * FROM PUNKT",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            PunktCorrectionCase rdcc = new PunktCorrectionCase(missingValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
