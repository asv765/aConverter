using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class CapWrongError : ErrorClass
    {
        private List<DataRow> wrongValues = new List<DataRow>();

        public CapWrongError(List<DataRow> AWrongValues)
        {
            this.ErrorName = String.Format("Часть записей о групповых счетчиках (ABONENT.CAPCD и ABONENT.CAPNAME) присутствуют в таблице RESOURCECOUNTERS, но имеют неверный COUNTER_LEVEL = 1");
            wrongValues = AWrongValues;
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи о групповых установках",
                "SELECT CapCD, Max(CapName) as CapName, Count(*) as Cnt FROM ABONENT group by CapCd order by CapCD",
                null);
            StatisticSets.Add(ss);

            ss = new FdbStatistic("Таблица RESOURCECOUNTERS, записи с COUNTER_LEVEL = 0",
                "SELECT * FROM RESOURCECOUNTERS WHERE COUNTER_LEVEL = 0",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            CapWrongCorrectionCase rdcc = new CapWrongCorrectionCase(wrongValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
