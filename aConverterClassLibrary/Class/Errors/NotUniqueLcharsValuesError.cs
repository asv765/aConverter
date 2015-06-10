using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    class NotUniqueLcharsValuesError : ErrorClass
    {
        public NotUniqueLcharsValuesError()
        {
            this.ErrorName = "В таблице LCHARS.DBF встречаются не уникальные задвоенные значения характеристик для одного LSHET, LCHARCD, DATE";
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Дублирующиеся записи в LCHARS.DBF",
                "SELECT * FROM lchars WHERE CONCAT(lshet,CAST(lcharcd AS char),CAST(date as DATE)) IN ( " +
                    "select CONCAT(lshet,CAST(lcharcd AS char),CAST(date as DATE)) from lchars group by 1 having count(*) > 1) order by lshet, lcharcd, date",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
