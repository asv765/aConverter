using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class UncertaintyError: ErrorClass
    {
        private string tableName;
        private string fieldCD;
        private string fieldDescription;
        private UncertaintyCDType uncertaintyCDType;

        public UncertaintyError(string ATableName, string AFieldCD, string AFieldDescription, UncertaintyCDType AUncertaintyCDType)
        {
            tableName = ATableName;
            fieldCD = AFieldCD;
            fieldDescription = AFieldDescription;
            uncertaintyCDType = AUncertaintyCDType;

            this.ErrorName = String.Format("Для некоторых одинаковых значений кодов {0}.{1} встречаются разные значения {0}.{2}",
                tableName, fieldCD, fieldDescription);

            Statistic ss = new MySQLStatistic(String.Format("Список различных значений {0}.{2} для кода {0}.{1}",
                tableName, fieldCD, fieldDescription),
                String.Format(
                "select {1}, {2}, count(*) as cnt " +
                "from {0} c " +
                "where {1} in ( " +
                "select cd " +
                "from " +
                "(select {1} as cd, {2} as desc " +
                "from {0} a " +
                "group by 1,2 " +
                ") b " +
                "group by 1 " +
                "having count(*) > 1 " +
                ") " +
                "group by 1, 2 " +
                "order by 1, cnt DESCENDING",
                tableName, fieldCD, fieldDescription),
                null);
            StatisticSets.Add(ss);

            this.IsTerminating = false;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            FixUncertaintyCorrectionCase cc = new FixUncertaintyCorrectionCase(tableName, fieldCD, fieldDescription, uncertaintyCDType);
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
