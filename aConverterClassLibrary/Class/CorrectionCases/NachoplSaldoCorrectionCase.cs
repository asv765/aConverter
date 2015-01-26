using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplSaldoCorrectionCase: CorrectionCase
    {
        private NachoplSaldoCorrectionType nachoplSaldoCorrectionType;

        decimal oldSaldo;
        string lshet;
        int servicecd;
        string servicename;
        int oldMonth;
        int oldYear;
        decimal newSaldo;
        int newMonth;
        int newYear;
        // string dbfConnectionString;
        OleDbCommand command;

        public NachoplSaldoCorrectionCase(NachoplSaldoCorrectionType ANachoplSaldoCorrectionType,
            decimal AOldSaldo,
            string ALshet,
            int AServiceCD,
            string AServiceName,
            decimal ANewSaldo,
            int ANewMonth, int ANewYear,
            OleDbCommand Acommand)
        {
            nachoplSaldoCorrectionType = ANachoplSaldoCorrectionType;

            oldSaldo = AOldSaldo;
            lshet = ALshet.Trim();
            servicecd = AServiceCD;
            servicename = AServiceName.Trim();
            
            oldMonth = ANewMonth;
            oldYear = ANewYear;
            Utils.DecreaseMonthYear(ref oldMonth, ref oldYear);

            newSaldo = ANewSaldo;
            newMonth = ANewMonth;
            newYear = ANewYear;

            // dbfConnectionString = ADBFConnectionString;
            command = Acommand;

            this.CorrectionCaseName = String.Format(
                "Скорректировать сальдо абонента {0} по услуге {1} ({2}). Тип корректировки - \"{3}\"",
                lshet, servicecd, servicename, nachoplSaldoCorrectionType.ToString().Replace('_',' '));
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
                if (nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Пересчитать_сальдо_вперед_с_начала_истории)
                {
                    #region Старая версия алгоритма
                    //command.CommandText = String.Format(
                    //                        "SELECT LSHET, Year, Month, ServiceCD, ServiceNam, BDEBET, EDEBET " +
                    //                        "FROM NACHOPL " +
                    //                        "WHERE LSHET = '{0}' AND ServiceCD = {1} " +
                    //                        "ORDER BY Lshet, ServiceCD, Year, Month",
                    //                        lshet, servicecd);
                    //DataTable dt = new DataTable();
                    //OleDbDataAdapter da = new OleDbDataAdapter(command);
                    //da.Fill(dt);
                    //decimal lastSaldo = 0;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    lastSaldo = Convert.ToDecimal(dt.Rows[0]["BDEBET"]);
                    //}
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    int currMonth = Convert.ToInt32(dr["MONTH"]);
                    //    int currYear = Convert.ToInt32(dr["YEAR"]);
                    //    command.CommandText = String.Format(
                    //        "UPDATE NACHOPL SET BDEBET = {0} " +
                    //        "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR = {4} AND MONTH = {3}",
                    //        lastSaldo.ToString().Replace(',', '.'), lshet, servicecd, currMonth, currYear);
                    //    command.ExecuteNonQuery();
                    //    command.CommandText = String.Format(
                    //        "UPDATE NACHOPL SET EDEBET = BDEBET + FNATH + PROCHL - OPLATA " +
                    //        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                    //        lshet, servicecd, currMonth, currYear);
                    //    command.ExecuteNonQuery();
                    //    command.CommandText = String.Format(
                    //        "SELECT EDEBET FROM NACHOPL " +
                    //        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                    //        lshet, servicecd, currMonth, currYear);
                    //    lastSaldo = Convert.ToDecimal(command.ExecuteScalar());
                    //}
                    string ct = "";
                    ct += "set excl off\r";
                    ct += "use nachopl orde tag complex\r";
                    ct += "seek \"" + lshet.PadRight(10) + servicecd.ToString().PadLeft(5) + "\"\r";
                    ct += "od = -1\r";
                    ct += "ed = -1\r";
                    ct += String.Format("scan while lshet = \"{0}\" and servicecd={1}\r", lshet, servicecd);
                    ct += "ed = od+FNATH+PROCHL-OPLATA\r";
                    ct += "repl bdebet with od, edebet with ed\r";
                    ct += "od = ed\r";
                    ct += "endscan\r";
                    command.CommandText = String.Format("EXECSCRIPT('{0}')", ct);
                    command.ExecuteNonQuery();
                    #endregion
                }
                else if (nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Пересчитать_сальдо_назад_с_конца_истории)
                {
                    command.CommandText = String.Format(
                                            "SELECT LSHET, Year, Month, ServiceCD, ServiceNam, BDEBET, EDEBET " +
                                            "FROM NACHOPL " +
                                            "WHERE LSHET = '{0}' AND ServiceCD = {1} " +
                                            "ORDER BY Lshet, ServiceCD, Year DESCENDING, Month DESCENDING",
                                            lshet, servicecd);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);
                    decimal lastSaldo = 0;
                    if (dt.Rows.Count > 0)
                    {
                        lastSaldo = Convert.ToDecimal(dt.Rows[0]["EDEBET"]);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        int currMonth = Convert.ToInt32(dr["MONTH"]);
                        int currYear = Convert.ToInt32(dr["YEAR"]);
                        command.CommandText = String.Format(
                            "UPDATE NACHOPL SET EDEBET = {0} " +
                            "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR = {4} AND MONTH = {3}",
                            lastSaldo.ToString().Replace(',', '.'), lshet, servicecd, currMonth, currYear);
                        command.ExecuteNonQuery();
                        command.CommandText = String.Format(
                            "UPDATE NACHOPL SET BDEBET = EDEBET - (FNATH + PROCHL - OPLATA) " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                            lshet, servicecd, currMonth, currYear);
                        command.ExecuteNonQuery();
                        command.CommandText = String.Format(
                            "SELECT BDEBET FROM NACHOPL " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                            lshet, servicecd, currMonth, currYear);
                        command.ExecuteNonQuery();
                        lastSaldo = Convert.ToDecimal(command.ExecuteScalar());
                    }
                }
                else if (nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Скорректировать_сальдо_на_начало_текущего_месяца_с_суммой_изменений_в_текущем_месяце)
                {
                    command.CommandText = String.Format(
                        "UPDATE NACHOPL SET BDEBET = {0} " +
                        "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR = {4} AND MONTH = {3}",
                        oldSaldo.ToString().Replace(',', '.'), lshet, servicecd, newMonth, newYear);
                    command.ExecuteNonQuery();
                    command.CommandText = String.Format(
                        "UPDATE NACHOPL SET PROCHL = EDEBET - (BDEBET + FNATH - OPLATA) " +
                        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                        lshet, servicecd, newMonth, newYear);
                    command.ExecuteNonQuery();
                }
                else if (nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Скорректировать_суммой_изменений_сальдо_на_конец_предыдущего_месяца)
                {
                    command.CommandText = String.Format(
                        "UPDATE NACHOPL SET EDEBET = {0} " +
                        "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR = {4} AND MONTH = {3}",
                        newSaldo.ToString().Replace(',', '.'), lshet, servicecd, oldMonth, oldYear);
                    command.ExecuteNonQuery();
                    command.CommandText = String.Format(
                        "UPDATE NACHOPL SET PROCHL = EDEBET - (BDEBET + FNATH - OPLATA) " +
                        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR = {3} AND MONTH = {2}",
                        lshet, servicecd, oldMonth, oldYear);
                    command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Неизвестный тип корректировки сальдо");
                }
            }
            catch(Exception ex)
            {
                this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }

        // LSHET + STR(SERVICECD,5) + STR(YEAR,4) + STR(MONTH,2)
        //private string complex(string lshet, int servicecd, int month, int year)
        //{
        //    string r = lshet.PadRight(10);
        //    r += servicecd.ToString().PadLeft(5);
        //    r += year.ToString().PadLeft(4);
        //    r += month.ToString().PadLeft(2);
        //    return r;
        //}
    }
}
