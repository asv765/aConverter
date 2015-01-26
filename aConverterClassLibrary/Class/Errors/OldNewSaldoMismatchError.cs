using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class OldNewSaldoMismatchError: ErrorClass
    {
        decimal oldSaldo;
        string lshet;
        int serviceCD;
        string serviceName;
        int oldMonth;
        int oldYear;
        decimal newSaldo;
        int newMonth;
        int newYear;
        // string dbfConnectionString;
        OleDbCommand command;
        Dictionary<string, object> nachoplOptimization;

        public OldNewSaldoMismatchError(decimal AOldSaldo,
            string ALshet,
            int AServiceCD,
            string AServiceName,
            decimal ANewSaldo,
            int ANewMonth, int ANewYear,
            OleDbCommand Acommand,
            Dictionary<string, object> AnachoplOptimization)
        {
            oldSaldo = AOldSaldo;
            lshet = ALshet.Trim();
            serviceCD = AServiceCD;
            serviceName = AServiceName.Trim();
            nachoplOptimization = AnachoplOptimization;
            if (ANewMonth == 1)
            {
                oldMonth = 12;
                oldYear = ANewYear - 1;
            }
            else
            {
                oldMonth = ANewMonth - 1;
                oldYear = ANewYear;
            }
            newSaldo = ANewSaldo;
            newMonth = ANewMonth;
            newYear = ANewYear;

            // dbfConnectionString = ADBFConnectionString;
            command = Acommand;

            this.ErrorName = String.Format("Значение сальдо {0} для лицевого счета {1} по услуге с кодом {2} ({3}) на конец " +
                "{4}.{5} не совпадает со значением {6} на начало {7}.{8}",
                oldSaldo, lshet, serviceCD, serviceName, oldMonth, oldYear, newSaldo, newMonth, newYear);
            this.IsTerminating = false;

            PossibleErrorParams.Add(ErrorParam.Тип_корректировки_сальдо_в_NACHOPL);

            Statistic ss = new DbfStatistic(
                String.Format("История оплат/начислений для абонента {0} по услуге с кодом {1} ({2})",
                    lshet, serviceCD, serviceName),
                String.Format("SELECT * FROM NACHOPL WHERE lshet = '{0}' AND servicecd = {1} order by lshet, year, month",
                    lshet, serviceCD),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            if (!AllParamsPresent())
            {
                string message = String.Format("Для ошибки \"{0}\" заданы не все параметры",
                    this.ToString());
                throw new Exception(message);
            }
            int value = Convert.ToInt32(ErrorParams[ErrorParam.Тип_корректировки_сальдо_в_NACHOPL]);
            NachoplSaldoCorrectionType nsct = (NachoplSaldoCorrectionType)value;
            if (nsct != NachoplSaldoCorrectionType.Не_корректировать_сальдо)
            {
                bool needcreate = true;
                if (nsct == NachoplSaldoCorrectionType.Пересчитать_сальдо_вперед_с_начала_истории ||
                    nsct == NachoplSaldoCorrectionType.Пересчитать_сальдо_назад_с_конца_истории)
                {
                    string c = lshet.PadRight(10) + serviceCD.ToString().PadLeft(5);
                    if (nachoplOptimization.ContainsKey(c))
                        needcreate = false;
                    else
                        nachoplOptimization.Add(c, null);
                }
                if (needcreate)
                {
                    NachoplSaldoCorrectionCase nccc = new NachoplSaldoCorrectionCase(nsct,
                        oldSaldo,
                        lshet,
                        serviceCD,
                        serviceName,
                        newSaldo,
                        newMonth,
                        newYear,
                        command);
                    nccc.ParentError = this;
                    CorrectionCases.Add(nccc);
                }
            }
        }
    }
}
