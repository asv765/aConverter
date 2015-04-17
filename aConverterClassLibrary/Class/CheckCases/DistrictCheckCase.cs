using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class DistrictCheckCase: CheckCase
    {
        public DistrictCheckCase()
        {
            this.CheckCaseName = "Проверяется, что данные о районах города (ABONENT.DISTKOD, ABONENT.TOWNSKOD и ABONENT.DISTNAME) расшифровываются в таблице DISTRICT";
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
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
                    command.CommandText = String.Format("SELECT TownsKod, DistKod, Max(DistName) as DistName, Count(*) FROM ABONENT GROUP BY TownsKod, DistKod");
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    List<string> errorCodes = new List<string>();
                    List<DataRow> missingValues = new List<DataRow>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod1 = Convert.ToInt32(dr["TownsKod"]);
                        int searchKod2 = Convert.ToInt32(dr["DistKod"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT COUNT(*) FROM DISTRICT WHERE PUNKTCD = {0} AND DISTCD = {1}", searchKod1, searchKod2);
                                connection.Open();
                                int count = Convert.ToInt32(fbc.ExecuteScalar());
                                if (count == 0)
                                {
                                    errorCodes.Add(String.Format("Не расшифровывается значение TownsKod = {0}, DistKod = {1} (DistName = \"{2}\")", searchKod1, searchKod2, Convert.ToString(dr["DistName"]).Trim()));
                                    missingValues.Add(dr);
                                }
                            }
                        }
                    }
                    if (errorCodes.Count > 0)
                    {
                        DistrictError rde = new DistrictError(missingValues);
                        rde.ParentCheckCase = this;
                        rde.Detail = errorCodes;
                        this.ErrorList.Add(rde);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
