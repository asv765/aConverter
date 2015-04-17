using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class NotUniqueLcharsValuesCheckCase: CheckCase
    {
        public NotUniqueLcharsValuesCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка, что в файле LCHARS отсутствуют задвоенные записи по полям LSHET, LCHARCD, DATE");
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

                    command.CommandText = "select lshet, lcharcd, date, count(*) as cnt from lchars group by lshet, lcharcd, date having count(*) > 1";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueLcharsValuesError nucve = new NotUniqueLcharsValuesError();
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
