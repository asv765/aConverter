using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NachoplOplataSummaMismatchError: ErrorClass
    {
        public NachoplOplataSummaMismatchError()
        {
            this.ErrorName = "В некоторых строках NACHOPL.DBF значение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF";
            this.IsTerminating = false;

            Statistic ss = new DbfStatistic("Записи NACHOPL.DBF в которых значение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF",
               "select n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata, SUM(o.summa) as summa " +
                                            "from nachopl n inner join oplata o on " +
                                            "    n.lshet+STR(n.servicecd,5)+STR(n.year,4)+STR(n.month,2) =  " +
                                            "    o.lshet+STR(o.servicecd,5)+STR(YEAR(o.date_vv),4)+STR(MONTH(o.date_vv),2)  " +
                                            "group BY n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata " +
                                            "HAVING n.oplata <> SUM(o.summa)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            NachoplOplataCorrectionCase nocc = new NachoplOplataCorrectionCase();
            nocc.ParentError = this;
            CorrectionCases.Add(nocc);
        }
    }
}
