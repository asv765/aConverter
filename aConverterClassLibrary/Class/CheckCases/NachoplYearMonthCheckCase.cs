using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplYearMonthCheckCase : CheckCase
    {
        public NachoplYearMonthCheckCase()
        {
            this.CheckCaseName = String.Format("Проверяется, что в файле NACHOPL.DBF значение в полях MONTH, MONTH2 находится в диапазоне от 1 до 12, а значение в полях YEAR, YEAR2 находится в диапазоне от 2000 до {0}",
                DateTime.Now.Year);
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT COUNT(*) FROM NACHOPL WHERE !BETWEEN(MONTH,1,12) OR !BETWEEN(MONTH2,1,12) OR !BETWEEN(YEAR,2000,{0}) OR !BETWEEN(YEAR2,2000,{0})",
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
