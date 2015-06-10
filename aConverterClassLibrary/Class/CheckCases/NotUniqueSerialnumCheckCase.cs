using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class NotUniqueCounteridCheckCase : CheckCase
    {
        public NotUniqueCounteridCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность кода счетчика COUNTERID в таблице COUNTERS");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            MariaDbConnection smon = new MariaDbConnection(aConverter_RootSettings.DestMySqlConnectionString);
            MySqlConnection dbConn = smon.Connection;

            #region Проверяем, является ли COUNTERID в таблице COUNTERS.DBF уникальным
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select counterid, count(*) as cnt from counters group by counterid having count(*) > 1";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
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
