using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class CapWrongCorrectionCase: CorrectionCase
    {
        private List<DataRow> wrongValues;

        public CapWrongCorrectionCase(List<DataRow> AWrongValues)
        {
            this.CorrectionCaseName = String.Format("Изменить COUNTER_LEVEL в таблице RESOURCECOUNTERS на значение 1 у {0} записей", AWrongValues.Count);
            wrongValues = AWrongValues;
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
                        foreach (DataRow dr in wrongValues)
                        {
                            command.CommandText = String.Format("UPDATE ResourceCounters  SET COUNTER_LEVEL = 1 WHERE Kod = {0}",
                                dr["CapCD"]);
                            command.ExecuteNonQuery();
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
