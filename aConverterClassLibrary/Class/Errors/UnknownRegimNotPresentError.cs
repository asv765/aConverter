using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class UnknownRegimNotPresentError: ErrorClass
    {
        public UnknownRegimNotPresentError()
        {
            this.ErrorName = String.Format("В целевой БД отсутствует режим с кодом 10 (Неизвестен)");
            this.IsTerminating = false;

            Statistic ss = new FdbStatistic("Таблица RESOURCESREGIMSLIST",
                "SELECT * FROM RESOURCESREGIMSLIST",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            AddUnknownRegimCorrectionCase aurcc = new AddUnknownRegimCorrectionCase();
            aurcc.ParentError = this;
            CorrectionCases.Add(aurcc);
        }
    }
}
