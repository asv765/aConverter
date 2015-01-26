using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class DistrictError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public DistrictError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей об районах города (ABONENT.DISTKOD, ABONENT.TOWNSKOD и ABONENT.DISTNAME) отсутствуют в таблице DISTRICT");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            Statistic ss = new DbfStatistic("Записи о районах города",
                "SELECT TownsKod, DistKod, Max(DistName) as DistName, Count(*) FROM ABONENT GROUP BY TownsKod, DistKod",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица DISTRICT",
                "SELECT * FROM DISTRICT",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            DistrictCorrectionCase rdcc = new DistrictCorrectionCase(missingValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
