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

            Statistic ss = new DbfStatistic("Дублирующиеся записи в CHARS.DBF",
                "SELECT * FROM chars WHERE (lshet+STR(charcd,5)+DTOS(Date)) IN ( " +
                "select (lshet+STR(charcd,5)+DTOS(Date)) from chars group by 1 having count(*) > 1)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }

    }
}
