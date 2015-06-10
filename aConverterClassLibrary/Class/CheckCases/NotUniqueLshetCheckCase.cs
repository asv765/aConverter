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
    public class NotUniqueLshetCheckCase: CheckCase
    {
        public NotUniqueLshetCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность лицевых счетов в таблице ABONENT");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            MariaDbConnection smon = new MariaDbConnection(aConverter_RootSettings.DestMySqlConnectionString);
            MySqlConnection dbConn = smon.Connection;

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF уникальным
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select lshet, count(*) as cnt from abonent group by lshet having count(*) > 1";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueLshetError nule = new NotUniqueLshetError();
                        this.ErrorList.Add(nule);
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
