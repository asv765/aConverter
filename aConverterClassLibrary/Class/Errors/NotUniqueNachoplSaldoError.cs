using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NotUniqueNachoplSaldoError: ErrorClass
    {
        public NotUniqueNachoplSaldoError()
        {
            this.ErrorName = "В таблице NACHOPL.DBF встречаются несколько записей по одному лицевому счету в одном месяце по одной услуге";
            this.IsTerminating = true;

            Statistic ss = new DbfStatistic("Задвоенные записи в NACHOPL.DBF",
                "select * " +
                "from nachopl " +
                "where lshet + STR(servicecd, 6) + Str(month, 2) + str(year, 4) in " +
                "(select lshet + STR(servicecd, 6) + Str(month, 2) + str(year, 4) " +
                "from nachopl " +
                "group by lshet, servicecd, month, year " +
                "having count(*) > 1) " +
                "order by lshet, servicecd, year, month",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            DeleteNachoplRepeatedRowsCorrectionCase cc = new DeleteNachoplRepeatedRowsCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
