using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class DistrictCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;

        public DistrictCorrectionCase(List<DataRow> AMissingValues)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу DISTRICT {0} записей", AMissingValues.Count);
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
                            command.CommandText = String.Format("INSERT INTO DISTRICT (DISTCD, DISTNM, PUNKTCD) VALUES ({0}, '{2}', {1});",
                                dr["DistKod"], dr["TownsKod"], Convert.ToString(dr["DistName"]).Trim());
                            command.ExecuteNonQuery();
                            CorrectionCase.SetFdbGenerator("DISTRICT_G", "DISTRICT", "DISTCD");
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
