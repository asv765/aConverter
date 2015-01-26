using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplNachCheckCase1 : CheckCase
    {
        public NachoplNachCheckCase1()
        {
            this.CheckCaseName = "Проверяется, что для всех ненулевых сумм в полях FNATH или PROCHL в файле NACHOPL.DBF существует расшифровка в файле NACH.DBF";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                dbConn.Open();
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    // В файле NACHOPL.DBF сумма не равна нулю, а в NACH.DBF записи отсутствуют
                    command.CommandText = "select COUNT(*) from nachopl n where (n.Fnath <> 0 or n.Prochl <> 0) and " +
                        "n.lshet + str(n.Year,4) + str(n.month,2) not in " +
                        "(select nc.lshet + str(Year(nc.Date_vv),4) + str(Month(nc.Date_Vv),2) from nach nc)";
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
