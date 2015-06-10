using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class LshetLengthError: ErrorClass
    {
        public LshetLengthError(int minLength, int maxLength)
        {
            this.ErrorName = String.Format("В таблице ABONENT.DBF встречаются лицевые счета разной длины: {0} и {1} ",
                minLength, maxLength);
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Файл ABONENT.DBF, длина лицевых счетов",
                "select LEN(ALLT(LSHET)) as len, ABONENT.LSHET from ABONENT order by 1",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
