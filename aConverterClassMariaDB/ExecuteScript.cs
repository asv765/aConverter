using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace aConverterClassMariaDB
{
    public class ExecuteScript
    {
        public static void CreateDatabaseMariaDb(string databaseName, string server = "localhost", string user="root", string password = "masterkey", string port="3307")
        {
            string connectionString = String.Format("server='{0}';user id='{1}';password='{2}';port='{3}';database='{4}'",
                server,
                user,
                password,
                port,
                databaseName);
            var mon = new MySqlConnection(connectionString);
            string createDatabaseString = aConverterClassMariaDB.Resource1.ScriptMariaDB;
            createDatabaseString = createDatabaseString.Replace("%databasename%", databaseName);
            var script = new MySqlScript(mon, createDatabaseString);
            script.Execute();                  
        }      
    }
}
