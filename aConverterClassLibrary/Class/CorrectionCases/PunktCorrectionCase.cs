using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class PunktCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;

        public PunktCorrectionCase(List<DataRow> AMissingValues)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу PUNKT {0} записей", AMissingValues.Count);
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
                            command.CommandText = String.Format("INSERT INTO PUNKT (PUNKTCD, PUNKTNM, REGIONDISTRICTCD, SETTLEMENTID) VALUES ({0}, '{1}',{2}, NULL);",
                                dr["TownsKod"], Convert.ToString(dr["TownsName"]).Trim(), dr["RayonKod"]);
                            command.ExecuteNonQuery();
                            CorrectionCase.SetFdbGenerator("PUNKT_G", "PUNKT", "PUNKTCD");
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
