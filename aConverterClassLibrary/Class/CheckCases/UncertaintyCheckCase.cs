﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class UncertaintyCheckCase: CheckCase
    {
        private string tableName;
        private string fieldCD;
        private string fieldDescription;
        private UncertaintyCDType uncertaintyCDType;

        public UncertaintyCheckCase(string ATableName, string AFieldCD, string AFieldDescription,
            UncertaintyCDType AUncertaintyCDType)
        {
            tableName = ATableName;
            fieldCD = AFieldCD;
            fieldDescription = AFieldDescription;
            uncertaintyCDType = AUncertaintyCDType;

            this.CheckCaseName = String.Format("Проверяется, что {0}.{1} имеет только одинаковые значения для всех уникальных значений {0}.{2}",
                tableName, fieldCD, fieldDescription);
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
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
                    command.CommandText = String.Format(
                        "select cd " +
                        "from "+
                            "(select {1} as cd, {2} " +
                            "from {0} " +
                            "group by 1, 2) b " +
                        "group by 1 " +
                        "having count(*) > 1",
                        tableName, fieldCD, fieldDescription);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        UncertaintyError er = new UncertaintyError(tableName, fieldCD, fieldDescription, uncertaintyCDType);
                        er.ParentCheckCase = this;
                        this.ErrorList.Add(er);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }

    public enum UncertaintyCDType
    {
        Число,
        Строка
    }
}
