using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplOplataCorrectionCase: CorrectionCase
    {
        public NachoplOplataCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Обновить значение поля OPLATA в файле NACHOPL.DBF по данным из OPLATA.DBF");
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
                        command.CommandText = String.Format("EXECSCRIPT('sele lshet+str(servicecd,5)+str(year,4)+str(month,2) as c, sum(summa) as s from oplata group by 1 into cursor q\r" +
                            "sele q\rinde on c tag c')");
                        command.ExecuteNonQuery();
                        command.CommandText = String.Format("EXECSCRIPT('sele 0\ruse nachopl\rset rela to lshet+str(servicecd,5)+str(year,4)+str(month,2) into q\rrepl oplata with q.s for oplata<>q.s')");
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
