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
    public class NachoplNachCheckCase1 : CheckCase
    {
        public NachoplNachCheckCase1()
        {
            this.CheckCaseName = "Проверяется, что для всех ненулевых сумм в полях FNATH или PROCHL в файле NACHOPL существует расшифровка в файле NACH";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            KoneksiMariaDB smon = new KoneksiMariaDB();
            MySqlConnection dbConn = smon.mon;
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)
            {
                dbConn.Open();
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    // В файле NACHOPL.DBF сумма не равна нулю, а в NACH.DBF записи отсутствуют
                    //command.CommandText = "select COUNT(*) from nachopl n where (n.Fnath <> 0 or n.Prochl <> 0) and " +
                    //    "n.lshet + str(n.Year,4) + str(n.month,2) not in " +
                    //    "(select nc.lshet + str(Year(nc.Date_vv),4) + str(Month(nc.Date_Vv),2) from nach nc)";
                    command.CommandText = "select COUNT(*) from nachopl n where (n.Fnath <> 0 or n.Prochl <> 0) and n.lshet + convert(n.Year,char) + convert(n.month,char) not in (select nc.lshet + convert(Year(nc.Date_vv),char) + convert(Month(nc.Date_Vv),char) from nach nc)";
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
