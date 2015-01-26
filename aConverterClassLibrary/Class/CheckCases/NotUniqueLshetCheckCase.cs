using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NotUniqueLshetCheckCase: CheckCase
    {
        public NotUniqueLshetCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность лицевых счетов в таблице ABONENT.DBF");
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

                    command.CommandText = "select lshet, count(*) as cnt from abonent group by lshet having count(*) > 1";
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        NotUniqueLshetError nule = new NotUniqueLshetError();
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
