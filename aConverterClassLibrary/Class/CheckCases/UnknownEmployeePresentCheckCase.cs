using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using aConverterClassLibrary.Class.Errors;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class UnknownEmployeePresentCheckCase: CheckCase
    {
        public UnknownEmployeePresentCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка, что в целевой базе существует сотрудник с кодом 1 (Неизвестен или SYSDBA)");
            this.CheckCaseClass = CheckCaseClass.Целостность_целевой_БД;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            #region Проверяем, существует ли в целевой БД сотрудник с кодом 1 (неизвестен или SYSDBA)
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select count(*) from EMPLOYEES e where e.tabnumber = 1";
                    connection.Open();
                    int cnt = Convert.ToInt32(command.ExecuteScalar());
                    if (cnt == 0)
                    {
                        UnknownEmployeeNotPresentError uenpe = new UnknownEmployeeNotPresentError();
                        this.ErrorList.Add(uenpe);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
