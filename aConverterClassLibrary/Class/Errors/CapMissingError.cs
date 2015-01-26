using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class CapMissingError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public CapMissingError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей о групповых счетчиках (ABONENT.CAPCD и ABONENT.CAPNAME) отсутствуют в таблице RESOURCECOUNTERS");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            PossibleErrorParams.Add(ErrorParam.Код_марки_группового_счетчика_из_таблицы_COUNTERSTYPES);

            Statistic ss = new DbfStatistic("Записи о групповых установках",
                "SELECT CapCD, Max(CapName) as CapName, Count(*) as Cnt FROM ABONENT group by CapCd order by CapCd",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица RESOURCECOUNTERS, записи с COUNTER_LEVEL = 1",
                "SELECT * FROM RESOURCECOUNTERS WHERE COUNTER_LEVEL = 1 ORDER BY KOD",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица RESOURCECOUNTERS",
                "SELECT * FROM RESOURCECOUNTERS ORDER BY KOD",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица COUNTERSTYPES",
                "SELECT * FROM COUNTERSTYPES ORDER BY KOD",
                null);
            StatisticSets.Add(ss);

        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            if (!AllParamsPresent())
            {
                string message = String.Format("Для ошибки \"{0}\" заданы не все параметры",
                    this.ToString());
                throw new Exception(message);
            }
            int value = Convert.ToInt32(ErrorParams[ErrorParam.Код_марки_группового_счетчика_из_таблицы_COUNTERSTYPES]);
            CapMissingCorrectionCase rdcc = new CapMissingCorrectionCase(missingValues, value);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
