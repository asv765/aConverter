using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class SourceDocError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public SourceDocError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей об источниках оплаты (OPLATA.SOURCECD и OPLATA.SOURCENAME) отсутствуют в таблице SOURCEDOC");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи об источниках оплаты",
                "SELECT SourceCD, Max(SourceName) as SourceName, Count(*) FROM OPLATA group by SourceCD",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица SOURCEDOC",
                "SELECT * FROM SOURCEDOC",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            SourceDocCorrectionCase rdcc = new SourceDocCorrectionCase(missingValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
