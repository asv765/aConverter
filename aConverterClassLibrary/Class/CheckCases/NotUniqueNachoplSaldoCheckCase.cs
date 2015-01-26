using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NotUniqueNachoplSaldoCheckCase: CheckCase
    {
        public NotUniqueNachoplSaldoCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность записей в таблице NACHOPL.DBF, сгруппированных по LSHET, SERVICECD, MONTH, YEAR");
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF уникальным
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    dbConn.Open();

                    command.CommandText = "select lshet, servicecd, month, year, count(*) as cnt " +
                        "from nachopl " +
                        "group by lshet, servicecd, month, year " +
                        "having count(*) > 1";
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
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
