using System;
using System.Data;
using System.Data.OleDb;
using aConverterClassLibrary.Class;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class NotUniqueNachoplSaldoCheckCase: CheckCase
    {
        public NotUniqueNachoplSaldoCheckCase()
        {
            this.CheckCaseName = String.Format("Проверка на уникальность записей в таблице NACHOPL, сгруппированных по LSHET, SERVICECD, MONTH, YEAR");
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();
            string query = "select lshet, servicecd, month_, year_, count(*) as cnt " +
                        "from cnv$nachopl " +
                        "group by lshet, servicecd, month_, year_ " +
                        "having count(*) > 1";
            var dt = new FbManager(aConverter_RootSettings.FirebirdStringConnection).ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                var nule = new NotUniqueNachoplSaldoError();
                ErrorList.Add(nule);
                Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
            }
        }
    }

    public class NotUniqueNachoplSaldoError : ErrorClass
    {
        public NotUniqueNachoplSaldoError()
        {
            this.ErrorName = "В таблице CNV$NACHOPL встречаются несколько записей по одному лицевому счету в одном месяце по одной услуге";
            this.IsTerminating = true;

            Statistic ss = new FdbStatistic("Задвоенные записи в CNV$NACHOPL",
                "select nachopl1.* " +
                "from cnv$nachopl nachopl1 " +
                "where exists " +
                "(select lshet from cnv$nachopl nachopl2 " +
                "where  nachopl1.lshet = nachopl2.lshet and " +
                "nachopl1.month_ = nachopl2.month_ and " +
                "nachopl1.year_ = nachopl2.year_ and " +
                "nachopl1.servicecd = nachopl2.servicecd and " +
                "nachopl1.rdb$db_key != nachopl2.rdb$db_key)",
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var cc = new DeleteNachoplRepeatedRowsCorrectionCase {ParentError = this};
            CorrectionCases.Add(cc);
        }
    }

    public class DeleteNachoplRepeatedRowsCorrectionCase : CorrectionCase
    {
        public DeleteNachoplRepeatedRowsCorrectionCase()
        {
            this.CorrectionCaseName = String.Format(
                "Удалить из таблицы CNV$NACHOPL повторяющиеся записи");
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";
            const string query = "delete " +
                                 "from cnv$nachopl nachopl1 " +
                                 "where exists " +
                                 "(select lshet from cnv$nachopl nachopl2 " +
                                 "where  nachopl1.lshet = nachopl2.lshet and " +
                                 "nachopl1.month_ = nachopl2.month_ and " +
                                 "nachopl1.year_ = nachopl2.year_ and " +
                                 "nachopl1.servicecd = nachopl2.servicecd and " +
                                 "nachopl1.rdb$db_key > nachopl2.rdb$db_key)";
            new FbManager(aConverter_RootSettings.FirebirdStringConnection).ExecuteQuery(query);
        }
    }
}
