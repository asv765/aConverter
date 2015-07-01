using System;
using System.Data;
using System.Text.RegularExpressions;
using aConverterClassLibrary.Class;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class FioCheckCase : CheckCase
    {
        public FioCheckCase()
        {
            this.CheckCaseName = "Проверяется, заполненны ли надлежащим образом поля F, I и O";
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();

            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM cnv$abonent WHERE (F IS NOT NULL) AND (F <> '')";
                    int fcount = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "SELECT COUNT(*) FROM cnv$abonent WHERE (I IS NOT NULL) AND (I <> '')";
                    int icount = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "SELECT COUNT(*) FROM cnv$abonent WHERE (O IS NOT NULL) AND (O <> '')";
                    int ocount = Convert.ToInt32(command.ExecuteScalar());

                    if (fcount != 0 && icount == 0 && ocount == 0)
                    {
                        var ec = new FioErrorClass(fcount) {ParentCheckCase = this};
                        ErrorList.Add(ec);
                        Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ошибка - Фамилия Имя и Отчество не разбиты по полям F, I и O соответственно
    /// </summary>
    public class FioErrorClass : ErrorClass
    {
        public FioErrorClass(int fcount)
        {

            this.ErrorName = String.Format("В таблице CNV$ABONENT заполнено {0} полей F, но не заполнены поля I и O. Возможно в поле F ФИО записано одной строкой", fcount);
            this.IsTerminating = true;

            Statistic ss = new FdbStatistic("Поля F, I, O из таблицы CNV$ABONENT", "SELECT LSHET, F, I, O FROM CNV$ABONENT", null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var cc = new SplitFioCorrectionCase {ParentError = this};
            CorrectionCases.Add(cc);
        }
    }

    public class SplitFioCorrectionCase : CorrectionCase
    {
        public SplitFioCorrectionCase()
        {
            CorrectionCaseName = String.Format("Разбить поле ФИО в поле F таблицы CNV$ABONENT на Фамилию (F), Имя (I) и Отчество (O)");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";
            var fm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            try
            {
                var dt = fm.GetDataTable("CNV$ABONENT");
                foreach (DataRow dr in dt.Rows)
                {
                    var lshet = dr["LSHET"].ToString();
                    var f = dr["F"].ToString();

                    var i = Regex.Match(f, @"(?<=^\w+\s+)\w*").Value;
                    var o = Regex.Match(f, @"(?<=^\w+\s+\w+\s+)\w*").Value;
                    f = Regex.Match(f, @"^\w*").Value;

                    string update = String.Format("UPDATE CNV$ABONENT SET F = '{0}', I = '{1}', O = '{2}' WHERE LSHET = '{3}'", f, i, o, lshet);

                    fm.ExecuteNonQuery(update);
                }
            }
            catch (Exception ex)
            {
                Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }


}
