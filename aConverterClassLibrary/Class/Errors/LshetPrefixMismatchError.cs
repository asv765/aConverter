using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary;

namespace aConverterClassLibrary
{
    public class LshetPrefixMismatchError: ErrorClass
    {
        public LshetPrefixMismatchError(string Alshet_prefix)
        {
            this.ErrorName = String.Format("Не все лицевые счета в в таблице ABONENT.DBF начинаются с {0}",
                Alshet_prefix);
            this.IsTerminating = false;

            Statistic s = new DbfStatistic("Таблица ABONENT.DBF",
                "select * from abonent",
                null);
            this.StatisticSets.Add(s);

            PossibleErrorParams.Add(ErrorParam.Префикс_лицевого_счета);
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
            string lshet_prefix = Convert.ToString(ErrorParams[ErrorParam.Префикс_лицевого_счета]);
            LshetPrefixCorrectionCase lpcc = new LshetPrefixCorrectionCase(lshet_prefix);
            lpcc.ParentError = this;
            CorrectionCases.Add(lpcc);

        }
    }
}
