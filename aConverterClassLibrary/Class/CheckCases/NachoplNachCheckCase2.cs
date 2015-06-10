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
    public class NachoplNachCheckCase2 : CheckCase
    {
        public NachoplNachCheckCase2()
        {
            this.CheckCaseName = "В NACHOPL значение в поле FNATH не совпадает с суммой полученной по файлу NACH";
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
                dbConn.Open();
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    //command.CommandText = "select count(*) " +
                    //    "from nachopl n inner join nach nc on " +
                    //    "    n.lshet+STR(n.year,4)+STR(n.month,2) =  " +
                    //    "    nc.lshet+STR(YEAR(nc.date_vv),4)+STR(MONTH(nc.date_vv),2)  " +
                    //    "group BY n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata " +
                    //    "HAVING n.oplata <> SUM(nc.FNATH)";
                    command.CommandText = "select count(*) from nachopl n inner join nach nc on n.lshet = nc.lshet and convert(n.year,char) = convert(YEAR(nc.date_vv),char) and  convert(n.month,char) = convert(MONTH(nc.date_vv),char) group BY n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata HAVING n.oplata <> SUM(nc.FNATH)";
 

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        NachRowMissingError er = new NachRowMissingError();
                        er.ParentCheckCase = this;
                        this.ErrorList.Add(er);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
