﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplCalculationCheckCase: CheckCase
    {
        public NachoplCalculationCheckCase()
        {
            this.CheckCaseName = "Проверяется арифметика в каждой строке таблицы NACHOPL.DBF";
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
                    command.CommandText = "SELECT * FROM NACHOPL WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)";
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
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

