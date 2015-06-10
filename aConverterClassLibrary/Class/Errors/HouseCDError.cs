using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Не уникальные значения HouseCD в таблице ABONENT.DBF
    /// </summary>
    public class HouseCDError : ErrorClass
    {
        public const string FindQuery = "select HOUSECD, TOWNSKOD, MAX(TOWNSNAME) as TOWNASNAME, " +
                "RAYONKOD, MAX(RAYONNAME) as RAYONNAME, " +
                "ULICAKOD, MAX(ULICANAME) as ULICANAME, NDOMA, KORPUS, COUNT(*) AS CNT " +
                "from ABONENT " +
                "WHERE HOUSECD IN " +
                "(SELECT HOUSECD from (select HOUSECD, TOWNSKOD, RAYONKOD, ULICAKOD, NDOMA, KORPUS " +
                    "from ABONENT group by 1, 2, 3, 4, 5, 6) b group by 1 having count(*) > 1) " +
                "group by HOUSECD, TOWNSKOD, RAYONKOD, ULICAKOD, NDOMA, KORPUS " +
                "ORDER BY HOUSECD, CNT DESC";

        public HouseCDError()
        {

            this.ErrorName = String.Format("В таблице ABONENT.DBF для домов встречаются неуникальные значения HouseCD");
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Дома с неуникальнымми значениями HOUSECD", FindQuery, null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            MakeHouseCDUniqueCorrectionCase cc = new MakeHouseCDUniqueCorrectionCase(FindQuery);
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
