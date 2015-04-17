using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class NotUniqueCharsValuesCheckCase: CheckCase
    {
        public NotUniqueCharsValuesCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка, что в файле CHARS отсутствуют задвоенные записи по полям LSHET, CHARCD, DATE");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF уникальным
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (MySqlConnection dbConn = new MySqlConnection("server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'"))

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select lshet, charcd, date, count(*) as cnt from chars group by lshet, charcd, date having count(*) > 1";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueCharsValuesError nucve = new NotUniqueCharsValuesError();
                        this.ErrorList.Add(nucve);
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
