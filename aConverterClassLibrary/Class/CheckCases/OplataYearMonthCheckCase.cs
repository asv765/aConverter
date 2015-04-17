﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class OplataYearMonthCheckCase: CheckCase
    {
        public OplataYearMonthCheckCase()
        {
            this.CheckCaseName = String.Format("Проверяется, что в файле OPLATA значение в поле MONTH находится в диапазоне от 1 до 12, а значение в поле YEAR находится в диапазоне от 2000 до {0}",
                DateTime.Now.Year);
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
                    //command.CommandText = String.Format("SELECT COUNT(*) FROM OPLATA WHERE !BETWEEN(MONTH,1,12) OR !BETWEEN(YEAR,2000,{0})",
                    //    DateTime.Now.Year);
                    command.CommandText = String.Format("SELECT COUNT(*) FROM OPLATA WHERE MONTH not BETWEEN 1 and 12 OR  YEAR not BETWEEN 2000 and {0}",
                        DateTime.Now.Year);
                    dbConn.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                        return;
                    else
                    {
                        OplataYearMonthErrorClass ec = new OplataYearMonthErrorClass(count);
                        ec.ParentCheckCase = this;
                        this.ErrorList.Add(ec);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
