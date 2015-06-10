using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    [Serializable]
    public class MySQLStatistic : Statistic
    {
        public MySQLStatistic() { }

        public MySQLStatistic(string AName, string ASql, List<string> AFieldRecodeList) :
            base(AName, ASql, AFieldRecodeList)
        {
        }

        public override System.Data.DataTable GenerateStatistic()
        {
            {
                using (var dbConn = new MySqlConnection(aConverter_RootSettings.DestMySqlConnectionString))
                {
                    using (MySqlCommand command = dbConn.CreateCommand())
                    {
                        command.CommandText = Sql;
                        var dt = new DataTable();
                        var da = new MySqlDataAdapter(command);
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
