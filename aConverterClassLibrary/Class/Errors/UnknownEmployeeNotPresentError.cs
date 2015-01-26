using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Class.Errors
{
    public class UnknownEmployeeNotPresentError: ErrorClass
    {
        public UnknownEmployeeNotPresentError()
        {
            this.ErrorName = String.Format("В целевой БД отсутствует сотрудник с кодом 1 (Неизвестен или SYSDBA)");
            this.IsTerminating = false;

            Statistic ss = new FdbStatistic("Таблица EMPLOYEES",
                "SELECT * FROM EMPLOYEES ORDER BY TABNUMBER",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            AddUnknownEmployeeCorrectionCase auecc = new AddUnknownEmployeeCorrectionCase();
            auecc.ParentError = this;
            CorrectionCases.Add(auecc);
        }
    }
}
