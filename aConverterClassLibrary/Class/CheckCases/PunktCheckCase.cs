using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class PunktCheckCase: CheckCase
    {
        public PunktCheckCase()
        {
            this.CheckCaseName = "Проверяется, что данные о населенных пунктах (ABONENT.TOWNSKOD и ABONENT.TOWNSNAME) расшифровываются в PUNKT";
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
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
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT TownsKod, Min(RayonKod) as RayonKod, Max(TownsName) as TownsName, Count(*) FROM ABONENT GROUP BY TownsKod");
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    List<string> errorCodes = new List<string>();
                    List<DataRow> missingValues = new List<DataRow>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod = Convert.ToInt32(dr["TownsKod"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT COUNT(*) FROM PUNKT WHERE PUNKTCD = {0}", searchKod);
                                connection.Open();
                                int count = Convert.ToInt32(fbc.ExecuteScalar());
                                if (count == 0)
                                {
                                    errorCodes.Add(String.Format("Не расшифровывается значение TownsKod = {0} (TownsName = \"{1}\")", searchKod, Convert.ToString(dr["TownsName"]).Trim()));
                                    missingValues.Add(dr);
                                }
                            }
                        }
                    }
                    if (errorCodes.Count > 0)
                    {
                        PunktError rde = new PunktError(missingValues);
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
