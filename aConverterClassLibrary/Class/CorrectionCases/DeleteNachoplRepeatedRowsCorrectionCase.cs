using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class DeleteNachoplRepeatedRowsCorrectionCase: CorrectionCase
    {
        public DeleteNachoplRepeatedRowsCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Удалить из файла NACHOPL.DBF повторяющиеся записи");
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
                        command.CommandText = String.Format("EXECSCRIPT('" +
                            "use nachopl orde tag complex\r" +
                            "scat to a1 blank\r" +
                            "scan\r" +
                              "scat to a2\r" +
                              "flag = .T.\r" +
                              "for i=1 to alen(a1)\r" +
                                "if a1(i) <> a2(i)\r" +
                                  "flag = .F.\r" +
                                "endif\r" +
                              "endfor\r" +
                              "if flag\r" +
                                "delete\r" +
                              "endif\r" +
                              "scat to a1\r" +
                            "endscan\r')");
                        command.ExecuteNonQuery();
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
