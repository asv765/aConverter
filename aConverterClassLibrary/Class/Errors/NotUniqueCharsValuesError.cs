using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NotUniqueCharsValuesError : ErrorClass
    {
        public NotUniqueCharsValuesError()
        {
            this.ErrorName = "В таблице CHARS.DBF встречаются не уникальные задвоенные значения характеристик для одного LSHET, CHARCD, DATE";
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Дублирующиеся записи в CHARS.DBF",
                "SELECT * FROM chars WHERE CONCAT(lshet,CAST(charcd AS char),CAST(date as DATE)) IN ( " +
                "select (CONCAT(lshet,CAST(charcd AS char),CAST(date as DATE))) from chars group by 1 having count(*) > 1)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }

    }
}
