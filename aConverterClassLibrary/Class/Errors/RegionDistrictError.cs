using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace aConverterClassLibrary
{
    public class RegionDistrictError: ErrorClass
    {
        private List<DataRow> missingValues = new List<DataRow>();

        public RegionDistrictError(List<DataRow> AMissingValues)
        {
            this.ErrorName = String.Format("Часть записей о районах области (ABONENT.RAYONKOD и ABONENT.RAYONNAME) отсутствуют в таблице REGIONDISTRICTS");
            missingValues = AMissingValues;
            this.IsTerminating = false;

            Statistic ss = new MySQLStatistic("Записи о районах области",
                "SELECT RayonKod, Max(RayonName) as RayonName, Count(*) FROM ABONENT GROUP BY RayonKod",
                null);
            StatisticSets.Add(ss);
            ss = new FdbStatistic("Таблица REGIONDISTRICTS",
                "SELECT * FROM REGIONDISTRICTS",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            RegionDistrictCorrectionCase rdcc = new RegionDistrictCorrectionCase(missingValues);
            rdcc.ParentError = this;
            CorrectionCases.Add(rdcc);
        }
    }
}
