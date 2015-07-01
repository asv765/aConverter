using aConverterClassLibrary.Class;
using aConverterClassLibrary.Properties;
using FirebirdSql.Data.FirebirdClient;
using System;

namespace aConverterClassLibrary
{
    public class NachoplOplataCheckCase: CheckCase
    {
        public NachoplOplataCheckCase()
        {
            CheckCaseName = "Проверяется, что сумма итоговой оплаты по услуге за месяц в файле OPLATA соответствует значение в поле OPLATA таблицы NACHOPL.DBF";
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();

            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    #region 1. В файле NACHOPL.DBF сумма не равна нулю, а в OPLATA.DBF записи отсутствуют
                    command.CommandText = Resources.NachoplOplataCheckCase1_NotFoundInOplata;
                    int count2 = Convert.ToInt32(command.ExecuteScalar());
                    if (count2 > 0)
                    {
                        var er = new OplataRowMissingError {ParentCheckCase = this};
                        ErrorList.Add(er);
                        Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    #endregion

                    #region 2. В NACHOPL.DBF значчение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF
                    command.CommandText = Resources.NachoplOplataCheckCase2_OplataSummMismatch;
                    command.CommandTimeout = 0;
                    count2 = Convert.ToInt32(command.ExecuteScalar());
                    if (count2 > 0)
                    {
                        var er = new NachoplOplataSummaMismatchError {ParentCheckCase = this};
                        ErrorList.Add(er);
                        Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    #endregion

                    #region 3. В OPLATA.DBF есть записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в NACHOPL.DBF
                    command.CommandText = Resources.NachoplOplataCheckCase3_NotFoundInNachopl;
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count != 0)
                    {
                        var er = new OplataExcessRowError {ParentCheckCase = this};
                        ErrorList.Add(er);
                        Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    #endregion
                }
            }
        }
    }

    public class OplataRowMissingError : ErrorClass
    {
        public OplataRowMissingError()
        {
            ErrorName = "В некоторых строках CNV$NACHOPL значение в поле OPLATA не равно нулю, но не расшифровывется в таблице CNV$OPLATA";
            IsTerminating = false;

            Statistic ss = new FdbStatistic("Записи CNV$NACHOPL в которых значение в поле OPLATA не совпадает с суммой полученной по таблице CNV$OPLATA",
                Resources.NachoplSummaNotFoundInOplataStatistic,
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var cc = new NachoplOplataSetNullCorrectionCase {ParentError = this};
            CorrectionCases.Add(cc);
        }
    }

    public class OplataExcessRowError : ErrorClass
    {
        public OplataExcessRowError()
        {
            ErrorName = "В таблице CNV$OPLATA есть записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в CNV$NACHOPL";
            IsTerminating = false;

            Statistic ss = new FdbStatistic("Записи CNV$OPLATA с ненулевой суммой, для которых отсутствует соответствующая запись в CNV$NACHOPL",
                Resources.OplataSummaNotFoundInNachoplStatistic,
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var cc = new DeleteOplataExcessRowCorrectionCase {ParentError = this};
            CorrectionCases.Add(cc);
        }
    }

    public class NachoplOplataSummaMismatchError : ErrorClass
    {
        public NachoplOplataSummaMismatchError()
        {
            ErrorName = "В некоторых строках CNV$NACHOPL значение в поле OPLATA не совпадает с суммой, полученной по таблице CNV$OPLATA";
            IsTerminating = false;

            Statistic ss = new FdbStatistic("Записи NACHOPL.DBF в которых значение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF",
                Resources.NachoplOplataSummaMismatchErrorStatistic,
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var nocc = new NachoplOplataCorrectionCase {ParentError = this};
            CorrectionCases.Add(nocc);
        }
    }

    public class NachoplOplataSetNullCorrectionCase : CorrectionCase
    {
        public NachoplOplataSetNullCorrectionCase()
        {
            CorrectionCaseName = String.Format(
                "Установить в ноль значение поля OPLATA в файле CNV$NACHOPL для записей, которые не расшифровываются в CNV$OPLATA");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbManager.ExecuteNonQuery(Resources.NachoplOplataSetNullCorrectionCase);
        }
    }

    public class DeleteOplataExcessRowCorrectionCase : CorrectionCase
    {
        public DeleteOplataExcessRowCorrectionCase()
        {
            CorrectionCaseName = String.Format(
                "Удалить из таблицы CNV$OPLATA записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в CNV$NACHOPL");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbManager.ExecuteNonQuery(Resources.DeleteOplataExcessRowCorrectionCase);
        }
    }

    public class NachoplOplataCorrectionCase : CorrectionCase
    {
        public NachoplOplataCorrectionCase()
        {
            CorrectionCaseName = String.Format(
                "Обновить значение поля OPLATA в таблице CNV$NACHOPL по данным из CNV$OPLATA");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            fbManager.ExecuteNonQuery(Resources.NachoplOplataCorrectionCase);
        }
    }

}
