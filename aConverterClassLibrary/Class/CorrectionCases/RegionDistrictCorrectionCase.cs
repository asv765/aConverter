using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class RegionDistrictCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;

        public RegionDistrictCorrectionCase(List<DataRow> AMissingValues)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу REGIONDISTRICTS {0} записей", AMissingValues.Count);
            missingValues = AMissingValues;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        foreach (DataRow dr in missingValues)
                        {
                            command.CommandText = String.Format("INSERT INTO REGIONDISTRICTS (REGIONDISTRICTCD, REGIONDISTRICTNM) VALUES ({0}, '{1}');",
                                dr["RayonKod"], Convert.ToString(dr["RayonName"]).Trim());
                            command.ExecuteNonQuery();
                            CorrectionCase.SetFdbGenerator("REGIONDISTRICTS_G", "REGIONDISTRICTS", "REGIONDISTRICTCD");
                        }
                    }
                    catch(Exception ex)
                    {
                        this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                        this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
                    }
                }
            }
        }
    }
}
