using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NachoplYearMonthErrorClass : ErrorClass
    {
        public NachoplYearMonthErrorClass(int count)
        {
            this.ErrorName = String.Format("В {0} строках NACHOPL.DBF значение в полях MONTH, MONTH2 не находится в диапазоне от 1 до 12, либо значение в полях YEAR, YEAR2 не находится в диапазоне от 2000 до {1}",
                count, DateTime.Now.Year);
            this.IsTerminating = false;

            List<string> rl = new List<string>();
            Statistic ss = new MySQLStatistic("Записи с ошибочными значениями в полях MONTH, MONTH2, YEAR, YEAR2 в файле NACHOPL.DBF",
                String.Format("SELECT * FROM OPLATA WHERE !BETWEEN(MONTH,1,12) OR !BETWEEN(MONTH2,1,12) OR !BETWEEN(YEAR,2000,{0}) OR !BETWEEN(YEAR2,2000,{0})",
                   DateTime.Now.Year),
                rl);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
        }
    }
}
