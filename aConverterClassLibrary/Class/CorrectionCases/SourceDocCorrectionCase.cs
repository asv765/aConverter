using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class SourceDocCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;

        public SourceDocCorrectionCase(List<DataRow> AMissingValues)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу SOURCEDOC {0} записей", AMissingValues.Count);
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
                            command.CommandText = String.Format("INSERT INTO SOURCEDOC (SOURCEDOCCD, PAYMETHODCD, SOURCEDOCNM, EXTORGCD, ISSUBS, SHOWASSUBSIDY) " +
                                "VALUES ({0}, {1}, '{2}', {3}, {4}, {5})",
                                dr["SourceCD"], 1, Convert.ToString(dr["SourceName"]).Trim(), 
                                "(select first 1 extorgcd from extorgspr where extorgspr.isdefaultorg = 1)", 
                                0, 0);
                            command.ExecuteNonQuery();
                            CorrectionCase.SetFdbGenerator("SOURCEDOC_G", "SOURCEDOC", "SOURCEDOCCD");
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
