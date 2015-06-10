using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NotUniqueCounteridError : ErrorClass
    {
        public NotUniqueCounteridError()
        {
            this.ErrorName = "В таблице COUNTERS.DBF встречаются не уникальные идентификаторы счетчиков (Counterid)";
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Задвоенные серийные номера",
                "select counterid, count(*) from counters group by counterid having count(*) > 1",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
