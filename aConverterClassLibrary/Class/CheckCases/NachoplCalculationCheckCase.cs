using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class NachoplCalculationCheckCase: CheckCase
    {
        public NachoplCalculationCheckCase()
        {
            this.CheckCaseName = "Проверяется арифметика в каждой строке таблицы NACHOPL";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (MySqlConnection dbConn = new MySqlConnection("server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'"))

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM NACHOPL WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        return;
                    else
                    {
                        NachoplCalculationErrorClass ncec = new NachoplCalculationErrorClass();
                        ncec.ParentCheckCase = this;
                        this.ErrorList.Add(ncec);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}

