using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{

    public class MainOrganizationCheckCase: CheckCase
    {
        public MainOrganizationCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка, что в целевой базе существует организация с кодом 1");
            this.CheckCaseClass = CheckCaseClass.Целостность_целевой_БД;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            #region Проверяем, существует ли в целевой БД режим с кодом 10 (неизвестен)
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select count(*) from EXTORGSPR where EXTORGCD = 1";
                    connection.Open();
                    int cnt = Convert.ToInt32(command.ExecuteScalar());
                    if (cnt == 0)
                    {
                        MainOrganizationNotPresentError err = new MainOrganizationNotPresentError();
                        this.ErrorList.Add(err);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
