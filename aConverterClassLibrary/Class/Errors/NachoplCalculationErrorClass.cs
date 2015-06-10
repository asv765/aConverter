using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class NachoplCalculationErrorClass: ErrorClass
    {
        public NachoplCalculationErrorClass()
        {
            this.ErrorName = "В некоторых строках NACHOPL.DBF значение EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)";
            this.IsTerminating = false;

            PossibleErrorParams.Add(ErrorParam.Тип_корректировки_строки_в_NACHOPL);

            List<string> rl = new List<string>();
            rl.Add("LSHET;Лицевой счет");
            Statistic ss = new MySQLStatistic("Записи с ошибкой в арифметике в таблице NACHOPL.DBF", 
                "SELECT * FROM NACHOPL WHERE EDEBET <> (BDEBET + FNATH + PROCHL - OPLATA)",
                rl);
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
            int value = Convert.ToInt32(ErrorParams[ErrorParam.Тип_корректировки_строки_в_NACHOPL]);
            NachoplCorrectionType nct = (NachoplCorrectionType)value;
            NachoplCalculationCorrectionCase nccc = new NachoplCalculationCorrectionCase(aConverter_RootSettings.DBFConnectionString, nct);
            nccc.ParentError = this;
            CorrectionCases.Add(nccc);
        }
    }
}
