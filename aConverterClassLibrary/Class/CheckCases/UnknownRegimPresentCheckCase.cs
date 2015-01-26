using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class UnknownRegimPresentCheckCase: CheckCase
    {
        public UnknownRegimPresentCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка, что в целевой базе существует режим с кодом 10 (Неизвестен)");
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
                    command.CommandText = "select count(*) from RESOURCESREGIMSLIST rrl where rrl.kodregim = 10";
                    connection.Open();
                    int cnt = Convert.ToInt32(command.ExecuteScalar());
                    if (cnt == 0)
                    {
                        UnknownRegimNotPresentError urnpe = new UnknownRegimNotPresentError();
                        this.ErrorList.Add(urnpe);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
