using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Ошибка - Фамилия Имя и Отчество не разбиты по полям F, I и O соответственно
    /// </summary>
    public class FioErrorClass : ErrorClass
    {
        public FioErrorClass(int fcount)
        {

            this.ErrorName = String.Format("В таблице ABONENT.DBF заполнено {0} полей F, но не заполнены поля I и O. Возможно в поле F ФИО записано одной строкой", fcount);
            this.IsTerminating = true;

            Statistic ss = new MySQLStatistic("Поля F, I, O из таблицы ABONENT.DBF", "SELECT LSHET, F, I, O FROM ABONENT", null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            SplitFIOCorrectionCase cc = new SplitFIOCorrectionCase();
            cc.ParentError = this;
            CorrectionCases.Add(cc);
        }
    }
}
