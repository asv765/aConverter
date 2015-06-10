using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace aConverterClassLibrary.Class.CheckCases
{
    public class MariaDbConnection
    {
        public MySqlConnection Connection = new MySqlConnection();
        public MariaDbConnection(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
    }
}
