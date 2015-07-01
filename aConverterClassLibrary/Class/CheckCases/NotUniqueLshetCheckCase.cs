using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class NotUniqueLshetCheckCase: CheckCase
    {
        public NotUniqueLshetCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность лицевых счетов в таблице CNV$ABONENT");
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();

            #region Проверяем, является ли лицевой счет в таблице ABONENT.DBF уникальным
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "select lshet, count(*) as cnt from cnv$abonent group by lshet having count(*) > 1";

                    var dt = new DataTable();
                    var da = new FbDataAdapter(command);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var nule = new NotUniqueLshetError();
                        ErrorList.Add(nule);
                        Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                    }
                }
            }
            #endregion
        }
    }

    public class NotUniqueLshetError : ErrorClass
    {
        public NotUniqueLshetError()
        {
            this.ErrorName = "В таблице CNV$ABONENT встречаются не уникальные лицевые счета";
            this.IsTerminating = true;

            Statistic ss = new FdbStatistic("Задвоенные лицевые счета",
                "select lshet, count(*) as cnt from cnv$abonent group by lshet having count(*) > 1",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
