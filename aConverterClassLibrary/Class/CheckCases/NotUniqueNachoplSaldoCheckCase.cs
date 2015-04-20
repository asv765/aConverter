using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class NotUniqueNachoplSaldoCheckCase: CheckCase
    {
        public NotUniqueNachoplSaldoCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность записей в таблице NACHOPL, сгруппированных по LSHET, SERVICECD, MONTH, YEAR");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            KoneksiMariaDB smon = new KoneksiMariaDB();
            MySqlConnection dbConn = smon.mon;

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF уникальным
            //using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            using (dbConn)

            {
                using (MySqlCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select lshet, servicecd, month, year, count(*) as cnt " +
                        "from nachopl " +
                        "group by lshet, servicecd, month, year " +
                        "having count(*) > 1";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueNachoplSaldoError nule = new NotUniqueNachoplSaldoError();
                        this.ErrorList.Add(nule);
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        return;
                    }
                }
            }
            #endregion
        }
    }
}
