using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class DeleteOplataExcessRowCorrectionCase: CorrectionCase
    {
        public DeleteOplataExcessRowCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Удалить из файла OPLATA.DBF записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в NACHOPL.DBF");
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
                        command.CommandText = String.Format("delete from oplata  where " +
                                "oplata.lshet + str(oplata.ServiceCD,5) + str(oplata.Year,4) + str(oplata.month,2) not in " +
                                "(select n.lshet + str(n.ServiceCD,5) + str(n.Year,4) + str(n.month,2) from nachopl n)");
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
