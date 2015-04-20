using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Ошибка - отсутствует строка в истории оплат/начислений
    /// </summary>
    public class NachoplRowMissingError: ErrorClass
    {
        string lshet;
        int month;
        int year;
        decimal debet;
        int servicecd;
        string servicename;
        // string dbfConnectionString;
        // OleDbConnection dbConn;
        MySqlCommand dbCommand;

        public NachoplRowMissingError(string ALshet, int AMonth, int AYear, int AServiceCD, string AServiceName,
            decimal ADebet, MySqlCommand AdbCommand)
        {
            lshet = ALshet;
            month = AMonth;
            year = AYear;
            debet = ADebet;
            servicecd = AServiceCD;
            servicename = AServiceName.Trim();

            // dbfConnectionString = ADBFConnectionString;
            dbCommand = AdbCommand;

            this.ErrorName = String.Format("Отсутствует строка в истории оплат/начислений для абонента {0} за {1}.{2} по услуге с кодом {3} ({4})",
                lshet, month, year, servicecd, servicename);
            this.IsTerminating = true;

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
            AddRowToNachoplCorrectionCase artncc = new AddRowToNachoplCorrectionCase(
                lshet,
                month,
                year,
                servicecd,
                servicename,
                debet,
                dbCommand);
            artncc.ParentError = this;
            CorrectionCases.Add(artncc);
        }
    }
}
