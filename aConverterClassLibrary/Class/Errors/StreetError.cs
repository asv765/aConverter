using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class StreetError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public StreetError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей об улицах (ABONENT.ULICAKOD и ABONENT.ULICANAME) отсутствуют в таблице STREET");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            Statistic ss = new DbfStatistic("Записи об улицах",
                "SELECT UlicaKod, TownsKod, Max(UlicaName) as UlicaName, Count(*) FROM ABONENT GROUP BY UlicaKod, TownsKod",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица STREET",
                "SELECT * FROM STREET",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            StreetCorrectionCase rdcc = new StreetCorrectionCase(missingValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
