using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NotUniqueCounteridCheckCase : CheckCase
    {
        public NotUniqueCounteridCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность кода счетчика COUNTERID в таблице COUNTERS.DBF");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            #region Проверяем, является ли COUNTERID в таблице COUNTERS.DBF уникальным
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select counterid, count(*) as cnt from counters group by counterid having count(*) > 1";
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueCounteridError nule = new NotUniqueCounteridError();
                        this.ErrorList.Add(nule);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
