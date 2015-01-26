using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NotUniqueLshetError: ErrorClass
    {
        public NotUniqueLshetError()
        {
            this.ErrorName = "В таблице ABONENT.DBF встречаются не уникальные лицевые счета";
            this.IsTerminating = true;

            Statistic ss = new DbfStatistic("Задвоенные лицевые счета",
                "select lshet, count(*) from abonent group by lshet having count(*) > 1",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
