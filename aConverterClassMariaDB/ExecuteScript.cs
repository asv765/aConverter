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
        public static void CreateDatabaseMariaDB()
        {
            MySqlConnection mon = new MySqlConnection();
            string StrKon = "server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'";
            mon = new MySqlConnection(StrKon);          
            try
            {
                //MySqlScript script = new MySqlScript(mon, File.ReadAllText(@"d:\GitDiplom\aConverter\aConverterClassMariaDB\scriptMariaDBnew.mysql"));
                MySqlScript script = new MySqlScript(mon,aConverterClassMariaDB.Resource1.ScriptMariaDB);
                script.Execute();                  
            }
            catch
            {
                MessageBox.Show("Error accessing resources!");
            }                   
        }      
    }
}
