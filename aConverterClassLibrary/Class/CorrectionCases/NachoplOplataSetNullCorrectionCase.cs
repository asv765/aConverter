using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class NachoplOplataSetNullCorrectionCase: CorrectionCase
    {
        public NachoplOplataSetNullCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Установить в ноль значение поля OPLATA в файле NACHOPL.DBF для записей, которые не расшифровываются в OPLATA.DBF");
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
                        command.CommandText = "update nachopl set oplata = 0 where nachopl.oplata <> 0 and " +
                                            "nachopl.lshet + str(nachopl.ServiceCD,5) + str(nachopl.Year,4) + str(nachopl.month,2) not in " +
                                            "(select o.lshet + str(o.ServiceCD,5) + str(o.Year,4) + str(o.month,2) from oplata o)";
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
