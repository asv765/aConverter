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
    public class CheckLshetFormatCheckCase: CheckCase
    {
        public CheckLshetFormatCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка соответствия формата лицевого счета (является ли лицевой счет в таблице ABONENT строкой одинаковой длины)");
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();

            #region Проверяем, является ли лицевой счет в таблице CNV$ABONENT строкой одинаковой длины
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "SELECT MIN(CHAR_LENGTH(LSHET)) as MIN_ FROM CNV$ABONENT";
                    object result = command.ExecuteScalar();
                    int minLength = 0;
                    if (!(result is DBNull)) minLength = Convert.ToInt32(result);

                    command.CommandText = "SELECT MAX(CHAR_LENGTH(LSHET)) as MAX_ FROM CNV$ABONENT";
                    result = command.ExecuteScalar();
                    int maxLength = 0;
                    if (!(result is DBNull)) maxLength = Convert.ToInt32(result);

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

    public class LshetLengthError : ErrorClass
    {
        public LshetLengthError(int minLength, int maxLength)
        {
            this.ErrorName = String.Format("В таблице CNV$ABONENT встречаются лицевые счета разной длины: {0} и {1} ",
                minLength, maxLength);
            this.IsTerminating = true;

            Statistic ss = new FdbStatistic("Таблица CNV$ABONENT, длина лицевых счетов",
                "select CHAR_LENGTH(LSHET) as len, LSHET from CNV$ABONENT order by CHAR_LENGTH(LSHET)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
