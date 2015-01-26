using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class CapCheckCase: CheckCase
    {
        public CapCheckCase()
        {
            this.CheckCaseName = "Проверяется, что данные о групповых счетчиках (ABONENT.CAPCD и ABONENT.CAPNAME) расшифровываются в таблице RESOURCECOUNTERS";
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    command.CommandText = String.Format("SELECT CapCD, Max(CapName) as CapName FROM ABONENT WHERE CapCD > 0 group by CapCD order by CapCD");
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);

                    List<string> missing = new List<string>();
                    List<DataRow> missingValues = new List<DataRow>();
                    List<string> wrong = new List<string>();
                    List<DataRow> wrongErrorLevel = new List<DataRow>();


                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod = Convert.ToInt32(dr["CapCD"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            connection.Open();
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT * FROM RESOURCECOUNTERS WHERE KOD = {0}", searchKod);
                                FbDataAdapter fda = new FbDataAdapter(fbc);
                                DataTable rc = new DataTable();
                                fda.Fill(rc);
                                if (rc.Rows.Count == 0)
                                {
                                    missing.Add(String.Format("Не расшифровывается значение CapCD = {0} (CapName = \"{1}\")", searchKod, Convert.ToString(dr["CapName"]).Trim()));
                                    missingValues.Add(dr);
                                }
                                else if (Convert.ToInt32(rc.Rows[0]["COUNTER_LEVEL"]) == 0)
                                {
                                    wrong.Add(String.Format("Для значения CapCD = {0} (CapName = \"{1}\") в таблице RESOURCECOUNTERS существует только запись с COUNTER_LEVEL = 0", searchKod, Convert.ToString(dr["CapName"]).Trim()));
                                    wrongErrorLevel.Add(dr);
                                }
                            }
                        }
                    }
                    if (missing.Count > 0)
                    {
                        CapMissingError rde = new CapMissingError(missingValues);
                        rde.ParentCheckCase = this;
                        rde.Detail = missing;
                        this.ErrorList.Add(rde);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    if (wrong.Count > 0)
                    {
                        CapWrongError cwle = new CapWrongError(wrongErrorLevel);
                        cwle.ParentCheckCase = this;
                        cwle.Detail = wrong;
                        this.ErrorList.Add(cwle);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
