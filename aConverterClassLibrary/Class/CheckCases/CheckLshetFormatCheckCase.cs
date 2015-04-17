using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;


namespace aConverterClassLibrary
{
    public class CheckLshetFormatCheckCase: CheckCase
    {
        public CheckLshetFormatCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка соответствия формата лицевого счета (является ли лицевой счет в таблице ABONENT строкой одинаковой длины)");
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            int lshetLength = 0;

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF строкой одинаковой длины
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (MySqlConnection dbConn = new MySqlConnection("server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'"))
            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    //command.CommandText = "SELECT MIN(LEN(ALLT(LSHET))) as MIN FROM ABONENT";
                    command.CommandText = "SELECT MIN(CHAR_LENGTH(LSHET)) as MIN FROM ABONENT";
                    object result = command.ExecuteScalar();
                    int minLength = 0;
                    if (!(result is DBNull)) minLength = Convert.ToInt32(result);

                    //command.CommandText = "SELECT MAX(LEN(ALLT(LSHET))) as MIN FROM ABONENT";
                    command.CommandText = "SELECT MAX(CHAR_LENGTH(LSHET)) as MIN FROM ABONENT";
                    result = command.ExecuteScalar();
                    int maxLength = 0;
                    if (!(result is DBNull)) maxLength = Convert.ToInt32(result);

                    lshetLength = maxLength;

                    if (minLength != maxLength)
                    {
                        LshetLengthError lle = new LshetLengthError(minLength, maxLength);
                        this.ErrorList.Add(lle);
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
