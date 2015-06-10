﻿using System;
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
    public class SourceDocCheckCase: CheckCase
    {
        public SourceDocCheckCase()
        {
            this.CheckCaseName = "Проверяется, что данные об источниках оплаты (OPLATA.SOURCECD и OPLATA.SOURCENAME) расшифровываются в таблице SOURCEDOC";
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
                    command.CommandText = String.Format("SELECT SourceCD, Max(SourceName) as SourceName, Count(*) FROM OPLATA group by SourceCD");
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    List<string> errorCodes = new List<string>();
                    List<DataRow> missingValues = new List<DataRow>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        int searchKod = Convert.ToInt32(dr["SourceCD"]);
                        using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
                        {
                            using (FbCommand fbc = connection.CreateCommand())
                            {
                                fbc.CommandText = String.Format("SELECT COUNT(*) FROM SOURCEDOC WHERE SOURCEDOCCD = {0}", searchKod);
                                connection.Open();
                                int count = Convert.ToInt32(fbc.ExecuteScalar());
                                if (count == 0)
                                {
                                    errorCodes.Add(String.Format("Не расшифровывается значение SourceCD = {0} (SourceName = \"{1}\")", searchKod, Convert.ToString(dr["SourceName"]).Trim()));
                                    missingValues.Add(dr);
                                }
                            }
                        }
                    }
                    if (errorCodes.Count > 0)
                    {
                        SourceDocError rde = new SourceDocError(missingValues);
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
