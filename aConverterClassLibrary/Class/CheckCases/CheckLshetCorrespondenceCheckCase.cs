using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class CheckLshetCorrespondenceCheckCase: CheckCase
    {
        public CheckLshetCorrespondenceCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка соответствия формата лицевого счета данным о формате в целевой БД (количество символов и префикс)");
            this.CheckCaseClass = CheckCaseClass.Целостность_между_конвертируемыми_данными_и_целевой_БД;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            int lshetLength = 0;

            KoneksiMariaDB smon = new KoneksiMariaDB();
            MySqlConnection dbConn = smon.mon;

            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)
            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    //command.CommandText = "SELECT MAX(LEN(ALLT(LSHET))) as MAX FROM ABONENT";
                    command.CommandText = "SELECT MAX(CHAR_LENGTH(LSHET)) as MAX FROM ABONENT";
                    object result = command.ExecuteScalar();
                    if (!(result is DBNull)) lshetLength = Convert.ToInt32(result);
                }
            }

            #region Проверяем, совпадает ли длина лицевого счета в ABONENT.DBF со значением переменной LSHET_DIGITS_COUNT
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT VARIABLEVALUE FROM SYSTEMVARIABLES WHERE VARIABLENAME = 'LSHET_DIGITS_COUNT'";
                    connection.Open();
                    int lshet_digits_count = Convert.ToInt32(command.ExecuteScalar());
                    if (lshet_digits_count != lshetLength)
                    {
                        LshetLengthMismatchError llme = new LshetLengthMismatchError(lshet_digits_count, lshetLength);
                        this.ErrorList.Add(llme);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                        return;
                    }
                }
            }
            #endregion

            #region Проверяем, совпадает ли префикс лицевого счета
            string lshet_prefix = "";

            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT VARIABLEVALUE FROM SYSTEMVARIABLES WHERE VARIABLENAME = 'LSHET_PREFIX'";
                    connection.Open();
                    lshet_prefix = Convert.ToString(command.ExecuteScalar()).Trim();
                }
            }
            int mismatchedLshets = 0;
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)
            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();
                    //command.CommandText = String.Format("select COUNT(*) FROM ABONENT WHERE SUBST(LSHET, 1, {0}) <> '{1}'", lshet_prefix.Length, lshet_prefix);
                    command.CommandText = String.Format("select COUNT(*) FROM ABONENT WHERE SUBSTR(LSHET, 1, {0}) <> '{1}'", lshet_prefix.Length, lshet_prefix);
                    mismatchedLshets = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            if (mismatchedLshets != 0)
            {
                LshetPrefixMismatchError lpme = new LshetPrefixMismatchError(lshet_prefix);
                this.ErrorList.Add(lpme);
                this.Result = CheckCaseStatus.Выявлена_ошибка;
                return;
            }
            #endregion
        }
    }
}
