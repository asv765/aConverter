using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class OplataYearMonthErrorClass : ErrorClass
    {
        public OplataYearMonthErrorClass(int count)
        {
            this.ErrorName = String.Format("В {0} строках OPLATA.DBF значение в поле MONTH не находится в диапазоне от 1 до 12, либо значение в поле YEAR не находится в диапазоне от 2000 до {1}",
                count, DateTime.Now.Year);
            this.IsTerminating = false;

            List<string> rl = new List<string>();
            Statistic ss = new MySQLStatistic("Записи с ошибочными значениями в полях MONTH, YEAR в файле OPLATA.DBF",
                String.Format("SELECT * FROM OPLATA WHERE !BETWEEN(MONTH,1,12) OR !BETWEEN(YEAR,2000,{0})",
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
