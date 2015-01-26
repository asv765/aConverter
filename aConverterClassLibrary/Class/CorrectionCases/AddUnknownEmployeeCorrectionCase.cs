using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class AddUnknownEmployeeCorrectionCase: CorrectionCase
    {
        public AddUnknownEmployeeCorrectionCase()
        {
            this.CorrectionCaseName = String.Format("Добавить сотрудника с кодом 1 (Неизвестен)");
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
                        command.CommandText = String.Format("INSERT INTO EMPLOYEES (TABNUMBER, SERVICEID, POSTID, EMPLOYEESURNAME, EMPLOYEENAME, EMPLOYEEPATRONYMIC, EMPLOYEEADDRESS, EMPLOYEEPHONE, BOSS) VALUES (1, NULL, NULL, 'Неопределен', '', '', '', '', NULL);");
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
