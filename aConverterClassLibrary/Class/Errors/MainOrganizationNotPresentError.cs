using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary.Class.CorrectionCases;

namespace aConverterClassLibrary
{
    public class MainOrganizationNotPresentError: ErrorClass
    {
        public MainOrganizationNotPresentError()
        {
            this.ErrorName = String.Format("В целевой БД в таблице EXTORGSPR отсутствует организация с кодом 1");
            this.IsTerminating = false;

            Statistic ss = new FdbStatistic("Таблица EXTORGSPR",
                "SELECT * FROM EXTORGSPR",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            AddMainOrganizationCorrectionCase cc = new AddMainOrganizationCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
