using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace aConverterClassLibrary
{
    public class NachoplOplataCheckCase: CheckCase
    {
        public NachoplOplataCheckCase()
        {
            this.CheckCaseName = "Проверяется, что сумма итоговой оплаты по услуге за месяц в файле OPLATA.DBF соответствует значение в поле OPLATA таблицы NACHOPL.DBF";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            using (OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString))
            {
                dbConn.Open();
                using (OleDbCommand command = dbConn.CreateCommand())
                {
                    // Выявляем три типа ошибок
                    // 1. В файле NACHOPL.DBF сумма не равна нулю, а в OPLATA.DBF записи отсутствуют
                    // 2. В NACHOPL.DBF значчение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF
                    // 3. В OPLATA.DBF есть записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в NACHOPL.DBF
                    
                    // 1. В файле NACHOPL.DBF сумма не равна нулю, а в OPLATA.DBF записи отсутствуют
                    command.CommandText = "select COUNT(*) from nachopl n where n.oplata <> 0 and " +
                        "n.lshet + str(n.ServiceCD,5) + str(n.Year,4) + str(n.month,2) not in " +
                        "(select o.lshet + str(o.ServiceCD,5) + str(Year(o.Date_vv),4) + str(Month(o.Date_Vv),2) from oplata o)";
                    int count2 = Convert.ToInt32(command.ExecuteScalar());

                    //DataTable dt = new DataTable();
                    //OleDbDataAdapter da = new OleDbDataAdapter(command);
                    //da.Fill(dt);

                    // if (dt.Rows.Count != 0)
                    if (count2 > 0)
                    {
                        OplataRowMissingError er = new OplataRowMissingError();
                        er.ParentCheckCase = this;
                        this.ErrorList.Add(er);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    // 2. В NACHOPL.DBF значчение в поле OPLATA не совпадает с суммой полученной по файлу OPLATA.DBF
                    //command.CommandText = "select lshet, servicecd, servicenam, month, year, oplata, " +
                    //                    "(select sum(summa) " +
                    //                    "from oplata " + 
                    //                    "where oplata.lshet = nachopl.lshet and " +
                    //                    "oplata.servicecd = nachopl.servicecd and " + 
                    //                    "Month(oplata.Date_vv) = nachopl.Month and " +
                    //                    "Year(oplata.Date_vv) = nachopl.Year) as summa " +
                    //                    "from nachopl having oplata <> summa";

                    //command.CommandText = "select n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata, SUM(o.summa) as summa " +
                    //                        "from nachopl n inner join oplata o on " +
                    //                        "    n.lshet+STR(n.servicecd,5)+STR(n.year,4)+STR(n.month,2) =  " +
                    //                        "    o.lshet+STR(o.servicecd,5)+STR(YEAR(o.date_vv),4)+STR(MONTH(o.date_vv),2)  " +
                    //                        "group BY n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata " +
                    //                        "HAVING n.oplata <> SUM(o.summa)";

                    command.CommandText = "select count(*) " +
                        "from nachopl n inner join oplata o on " +
                        "    n.lshet+STR(n.servicecd,5)+STR(n.year,4)+STR(n.month,2) =  " +
                        "    o.lshet+STR(o.servicecd,5)+STR(YEAR(o.date_vv),4)+STR(MONTH(o.date_vv),2)  " +
                        "group BY n.lshet, n.servicecd, n.servicenam, n.month, n.year, n.oplata " +
                        "HAVING n.oplata <> SUM(o.summa)";

                    count2 = Convert.ToInt32(command.ExecuteScalar());
	
                    //dt = new DataTable();
                    //da = new OleDbDataAdapter(command);
                    //da.Fill(dt);

                    if (count2 > 0)
                    {
                        NachoplOplataSummaMismatchError er = new NachoplOplataSummaMismatchError();
                        er.ParentCheckCase = this;
                        this.ErrorList.Add(er);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }

                    // 3. В OPLATA.DBF есть записи с таким лицевым счетом, месяцем и услугой, для которых отсутствует запись в NACHOPL.DBF
                    command.CommandText = "select count(*) " +
                        "from oplata o " +
                        "where o.Summa <> 0  AND " +
                            "o.lshet + str(o.ServiceCD,5) + str(Year(o.Date_vv),4) + str(Month(o.Date_vv),2) not in " +
                            "(select n.lshet + str(n.ServiceCD,5) + str(n.Year,4) + str(n.month,2) from nachopl n)";
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count != 0)
                    {
                        OplataExcessRowError er = new OplataExcessRowError();
                        er.ParentCheckCase = this;
                        this.ErrorList.Add(er);
                        this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
            }
        }
    }
}
