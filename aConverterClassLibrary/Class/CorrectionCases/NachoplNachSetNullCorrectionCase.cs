using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class NachoplNachSetNullCorrectionCase : CorrectionCase
    {
        public NachoplNachSetNullCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Установить в ноль значение полей FNATH и PROCHL в файле NACHOPL.DBF для записей, которые не расшифровываются в NACH.DBF");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                dbConn.Open();
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    try
                    {
                        command.CommandText = "update nachopl set fnath = 0, prochl = 0 where nachopl.fnath <> 0 and " +
                                                        "nachopl.lshet + str(nachopl.Year,4) + str(nachopl.month,2) not in " +
                                                        "(select nc.lshet + str(Year(nc.Date_vv),4) + str(Month(nc.Date_Vv),2) from nach nc)";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                        this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
                    }
                }
            }
        }
    }
}
