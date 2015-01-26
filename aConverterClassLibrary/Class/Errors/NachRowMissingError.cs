using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NachRowMissingError : ErrorClass
    {
        public NachRowMissingError()
        {
            this.ErrorName = "В некоторых строках NACHOPL.DBF значение в полях FNATH либо PROCHL не равно нулю, но не расшифровывются в файле NACH.DBF";
            this.IsTerminating = false;

            Statistic ss = new DbfStatistic("Записи NACHOPL.DBF в которых значение в поле FNATH либо PROCHL не равны нуль, не расшифровывются в файле NACH.DBF",
                "select * from nachopl n where (n.fnath <> 0 or n.prochl<>0) and " +
                        "n.lshet + str(n.Year,4) + str(n.month,2) not in " +
                        "(select nc.lshet + str(Year(nc.Date_vv),4) + str(Month(nc.Date_Vv),2) from nach nc)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            NachoplNachSetNullCorrectionCase cc = new NachoplNachSetNullCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
