using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class StreetCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;

        public StreetCorrectionCase(List<DataRow> AMissingValues)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу STREET {0} записей", AMissingValues.Count);
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
                            command.CommandText = String.Format("INSERT INTO STREET (STREETCD, PUNKTCD, STREETNM, NOTE) VALUES ({0}, {1}, '{2}', NULL);",
                                dr["UlicaKod"], dr["TownsKod"], Convert.ToString(dr["UlicaName"]).Trim().Replace("'","\""));
                            command.ExecuteNonQuery();
                            CorrectionCase.SetFdbGenerator("STREET_G", "STREET", "STREETCD");
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
