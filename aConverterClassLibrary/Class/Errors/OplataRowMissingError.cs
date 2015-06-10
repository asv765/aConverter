using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class OplataRowMissingError: ErrorClass
    {
        public OplataRowMissingError()
        {
            this.ErrorName = "В некоторых строках NACHOPL.DBF значение в поле OPLATA не равно нулю, но не расшифровывется в файле OPLATA.DBF";
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи NACHOPL.DBF в которых значение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF",
                "select * from nachopl n where n.oplata <> 0 and " +
                        "n.lshet + str(n.ServiceCD,5) + str(n.Year,4) + str(n.month,2) not in " +
                        "(select o.lshet + str(o.ServiceCD,5) + str(Year(o.Date_vv),4) + str(Month(o.Date_Vv),2) from oplata o)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            NachoplOplataSetNullCorrectionCase cc = new NachoplOplataSetNullCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
