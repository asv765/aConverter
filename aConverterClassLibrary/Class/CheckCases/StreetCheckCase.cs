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
    public class StreetCheckCase: CheckCase
    {
        public StreetCheckCase()
        {
            this.CheckCaseName = "Проверяется, что данные об улицах (ABONENT.ULICAKOD и ABONENT.ULICANAME) расшифровываются в STREET";
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
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
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT UlicaKod, TownsKod, Max(UlicaName) as UlicaName, Count(*) FROM ABONENT GROUP BY UlicaKod, TownsKod");
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    List<string> errorCodes = new List<string>();
                    List<DataRow> missingValues = new List<DataRow>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod = Convert.ToInt32(dr["UlicaKod"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT COUNT(*) FROM STREET WHERE STREETCD = {0}", searchKod);
                                connection.Open();
                                int count = Convert.ToInt32(fbc.ExecuteScalar());
                                if (count == 0)
                                {
                                    errorCodes.Add(String.Format("Не расшифровывается значение UlicaKod = {0} (UlicaName = \"{1}\")", searchKod, Convert.ToString(dr["UlicaName"]).Trim()));
                                    missingValues.Add(dr);
                                }
                            }
                        }
                    }
                    if (errorCodes.Count > 0)
                    {
                        StreetError rde = new StreetError(missingValues);
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
