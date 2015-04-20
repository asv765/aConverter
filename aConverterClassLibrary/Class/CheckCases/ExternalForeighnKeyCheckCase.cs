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
    public class ExternalForeighnKeyCheckCase: CheckCase
    {
        private string dbfTable;
        private string dbfKey;
        private string fdbTable;
        private string fdbKey;

        public ExternalForeighnKeyCheckCase(string DbfTable, string DbfKey, 
            string FdbTable, string FdbKey)
        {
            this.CheckCaseName = String.Format("Проверяется, что {0}.{1} расшифровывается в {2}.{3}",
                DbfTable, DbfKey, FdbTable, FdbKey);
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
            
            this.dbfTable = DbfTable;
            this.dbfKey = DbfKey;
            this.fdbTable = FdbTable;
            this.fdbKey = FdbKey;
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
                    command.CommandText = String.Format("SELECT DISTINCT {0} AS NKEY FROM {1}", dbfKey, dbfTable);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    List<string> errorCodes = new List<string>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod = Convert.ToInt32(dr["NKEY"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT COUNT(*) FROM {0} WHERE {1} = {2}",
                                    fdbTable, fdbKey, searchKod);
                                connection.Open();
                                int count = Convert.ToInt32(fbc.ExecuteScalar());
                                if (count == 0) errorCodes.Add(String.Format("Не расшифровывается значение {0}.{1} = {2}", dbfTable, dbfKey, searchKod));
                            }
                        }
                    }
                    if (errorCodes.Count > 0)
                    {
                        ExternalIntegrityErrorClass eiec = new ExternalIntegrityErrorClass(dbfTable, dbfKey, fdbTable, fdbKey);
                        eiec.ParentCheckCase = this;
                        eiec.Detail = errorCodes;
                        this.ErrorList.Add(eiec);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
