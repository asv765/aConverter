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
    public class NachoplYearMonthCheckCase : CheckCase
    {
        public NachoplYearMonthCheckCase()
        {
            this.CheckCaseName = String.Format("Проверяется, что в файле NACHOPL значение в полях MONTH, MONTH2 находится в диапазоне от 1 до 12, а значение в полях YEAR, YEAR2 находится в диапазоне от 2000 до {0}",
                DateTime.Now.Year);
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            MariaDbConnection smon = new MariaDbConnection(aConverter_RootSettings.DestMySqlConnectionString);
            MySqlConnection dbConn = smon.Connection;
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT COUNT(*) FROM NACHOPL WHERE MONTH not BETWEEN 1 and 12 OR MONTH2 not BETWEEN 1 and 12 OR YEAR not BETWEEN 2000 and {0} OR YEAR2 not BETWEEN 2000 and {0}",
                        DateTime.Now.Year);
                    dbConn.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                        return;
                    else
                    {
                        NachoplYearMonthErrorClass ec = new NachoplYearMonthErrorClass(count);
                        ec.ParentCheckCase = this;
                        this.ErrorList.Add(ec);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
