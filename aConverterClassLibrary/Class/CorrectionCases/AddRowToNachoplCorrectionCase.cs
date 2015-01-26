using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace aConverterClassLibrary
{
    public class AddRowToNachoplCorrectionCase: CorrectionCase
    {
        string lshet;
        int month;
        int year;
        decimal debet;
        int servicecd;
        string servicename;
        // string dbfConnectionString;
        OleDbCommand command;

        public AddRowToNachoplCorrectionCase(string ALshet, int AMonth, int AYear, int AServiceCD, string AServiceName, 
            decimal ADebet, OleDbCommand Acommand)
        {
            this.CorrectionCaseName = String.Format("Добавить строку в историю оплат/начислений для абонента {0} за {1}.{2} по услуге с кодом {3} ({4}). Долг на начало и конец месяца - {5}",
                ALshet, AMonth, AYear, AServiceCD, AServiceName.Trim(), ADebet);
            lshet = ALshet;
            month = AMonth;
            year = AYear;
            debet = ADebet;
            servicecd = AServiceCD;
            servicename = AServiceName;
            // dbfConnectionString = ADBFConnectionString;
            command = Acommand;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            try
            {
                command.CommandText = String.Format("INSERT INTO NACHOPL (LSHET, ServiceCD, ServiceNam, Month, Year, Month2, Year2, BDebet, FNath, Prochl, Oplata, EDebet) " +
                    "VALUES ('{0}', {1}, '{2}', {3}, {4}, {3}, {4}, {5}, 0, 0, 0, {5})",
                    lshet,
                    servicecd, servicename,
                    month,
                    year,
                    debet.ToString().Replace(',','.'));
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }
}
