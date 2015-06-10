using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using DbfClassLibrary;
using MySql.Data.MySqlClient;
using System.Data;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class FioCheckCase : CheckCase
    {
        public FioCheckCase()
        {
            this.CheckCaseName = "Проверяется, заполненны ли надлежащим образом поля F, I и O";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            MariaDbConnection smon = new MariaDbConnection(aConverter_RootSettings.DestMySqlConnectionString);
            MySqlConnection dbConn = smon.Connection;
            //TableManager tm = new TableManager(aConverter_RootSettings.DestDBFFilePath);
            using (dbConn)
            {
                dbConn.Open();

                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    //tm.Init();
                    //try
                    //{
                        //int fcount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(F)"));
                        //int icount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(I)"));
                        //int ocount = Convert.ToInt32(tm.ExecuteScalar("SELECT COUNT(*) FROM abonent WHERE !EMPTY(O)"));
                        command.CommandText = "SELECT COUNT(*) FROM abonent WHERE COALESCE(F,0)";

                        //DataTable dt = new DataTable();
                        //MySqlDataAdapter da = new MySqlDataAdapter(command);
                        //da.Fill(dt);

                       // int fcount = dt.Rows.Count;
                        int fcount = Convert.ToInt32(command.ExecuteScalar());
                    
                        command.CommandText = "SELECT COUNT(*) FROM abonent WHERE COALESCE(I,0)";
                        int icount = Convert.ToInt32(command.ExecuteScalar());
                        command.CommandText = "SELECT COUNT(*) FROM abonent WHERE COALESCE(O,0)";
                        int ocount = Convert.ToInt32(command.ExecuteScalar());


                        if (fcount != 0 && icount == 0 && ocount == 0)
                        //if (fcount != 0)
                        {
                            FioErrorClass ec = new FioErrorClass(fcount);
                            ec.ParentCheckCase = this;
                            this.ErrorList.Add(ec);
                            this.Result = CheckCaseStatus.Выявлена_ошибка;
                        }
                    //}
                    //finally
                    //{
                    //    command.Dispose();
                    //}

                }
            }
        }
    }
}
