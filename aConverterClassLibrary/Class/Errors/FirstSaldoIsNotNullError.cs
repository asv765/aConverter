using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class FirstSaldoIsNotNullError: ErrorClass
    {
        string lshet;
        int month;
        int year;
        decimal debet;
        int servicecd;
        string servicename;
        // string dbfConnectionString;
        MySqlCommand command;

        public FirstSaldoIsNotNullError(string ALshet, int AMonth, int AYear, int AServiceCD, string AServiceName, decimal ADebet,
            MySqlCommand Acommand)
        {
            lshet = ALshet;
            month = AMonth;
            year = AYear;
            debet = ADebet;
            servicecd = AServiceCD;
            servicename = AServiceName.Trim();
            // dbfConnectionString = ADBFConnectionString;
            command = Acommand;

            this.ErrorName = String.Format("Сальдо {5} для абонента {0} на начало {1}.{2} по услуге с кодом {3} ({4}) является первым в истории оплат/начислений и не равно 0",
                lshet, month, year, servicecd, servicename, debet);
            this.IsTerminating = true;
            Utils.DecreaseMonthYear(ref month, ref year);

            Statistic ss = new DbfStatistic(
                String.Format("История оплат/начислений для абонента {0} по услуге с кодом {1} ({2})", 
                    lshet, servicecd, servicename),
                String.Format("SELECT * FROM NACHOPL WHERE lshet = '{0}' AND servicecd = {1} order by lshet, year, month",
                    lshet, servicecd),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            AddFirstSaldoRowCorrectionCase artncc = new AddFirstSaldoRowCorrectionCase(
                lshet,
                month,
                year,
                servicecd,
                servicename,
                debet,
                command);
            artncc.ParentError = this;
            CorrectionCases.Add(artncc);
        }
    }}
