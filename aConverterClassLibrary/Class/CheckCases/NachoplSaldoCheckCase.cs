using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using aConverterClassLibrary.Class.CheckCases;

namespace aConverterClassLibrary
{
    public class NachoplSaldoCheckCase: CheckCase
    {
        public NachoplSaldoCheckCase()
        {
            this.CheckCaseName = "Проверка целостности истории сальдо в файле NACHOPL";
            this.CheckCaseClass = CheckCaseClass.Целостность_конвертируемых_данных;
        }

        public override void Analize()
        {
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();

            MariaDbConnection smon = new MariaDbConnection(aConverter_RootSettings.DestMySqlConnectionString);
            MySqlConnection dbConn = smon.Connection;
            //OleDbConnection dbConn = new OleDbConnection(aConverter_RootSettings.DBFConnectionString);
            
            dbConn.Open();
            MySqlCommand command = dbConn.CreateCommand();
            command.CommandText = "SELECT LSHET, Year, Month, ServiceCD, ServiceNam, BDEBET, EDEBET "+
                                    "FROM NACHOPL " +
                                    "ORDER BY Lshet, ServiceCD, Year, Month";
            // DataTable dt = new DataTable();
            // OleDbDataAdapter da = new OleDbDataAdapter(command);
            // da.Fill(dt);
            MySqlDataReader dr = command.ExecuteReader();
            decimal oldEndDebet = 0;
            string oldLshet = "-1";
            int oldServiceCD = -1;
            int currMonth = -1; int currYear = -1;
            //string dbfConnectionString = aConverter_RootSettings.DBFConnectionString;
            string dbfConnectionString = "server ='localhost';user id='root';password='das03071993';port='3307';database='converterdb'";

            // Список нужен для оптимизации при генерации вариантов исправления.
            // По нему проверяется, сформирован ли вариант пересчета сальдо для данного абонента по данной услуге
            // Нужен для вариантов пересчета сальдо по всей истории
            // Формат хранимых значений - lshet.PadRight(10) + servicecd.ToString().PadLeft(5)
            Dictionary<string, object> nachoplOptimization = new Dictionary<string, object>();

            // OleDbConnection dbConn = new OleDbConnection(dbfConnectionString);
            // OleDbCommand dbCommand = new OleDbCommand();
            // dbCommand.Connection = dbConn;
            // foreach (DataRow dr in dt.Rows)
            while (dr.Read()) 
            {
                if (oldLshet == dr["LSHET"].ToString().Trim() &&
                    oldServiceCD == Convert.ToInt32(dr["ServiceCD"]))
                {
                    Utils.IncreaseMonthYear(ref currMonth, ref currYear);
                    while (currMonth != Convert.ToInt32(dr["MONTH"]) ||
                        currYear != Convert.ToInt32(dr["YEAR"]))
                    {
                        // Пропуск в истории оплат/начислений
                        // Пропущена запись за currMonth, currYear
                        NachoplRowMissingError nrme = new NachoplRowMissingError(
                            dr["LSHET"].ToString().Trim(),
                            currMonth,
                            currYear,
                            Convert.ToInt32(dr["ServiceCD"]),
                            Convert.ToString(dr["ServiceNam"]),
                            oldEndDebet, command);
                        nrme.ParentCheckCase = this;
                        this.ErrorList.Add(nrme);
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        Utils.IncreaseMonthYear(ref currMonth, ref currYear);
                    }
                    // Здесь оказываемся только если расчитанный месяц совпал с месяцем в базе
                    if (oldEndDebet != Convert.ToDecimal(dr["BDEBET"]))
                    {
                        // Сальдо на начало текущего месяца (dr["BDEBET"]) не равно сальдо на конец предыдущего (oldEndDebet)
                        // MessageBox.Show("Привет!");
                        OldNewSaldoMismatchError onsme = new OldNewSaldoMismatchError(oldEndDebet,
                            dr["LSHET"].ToString(),
                            Convert.ToInt32(dr["ServiceCD"]),
                            dr["ServiceNam"].ToString(),
                            Convert.ToDecimal(dr["BDEBET"]),
                            currMonth, currYear, command, nachoplOptimization);
                        onsme.ParentCheckCase = this;
                        this.ErrorList.Add(onsme);
                        if (this.Result != CheckCaseStatus.Выявлена_терминальная_ошибка)
                            this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                }
                else
                {
                    currMonth = Convert.ToInt32(dr["MONTH"]);
                    currYear = Convert.ToInt32(dr["YEAR"]);
                    if (Convert.ToDecimal(dr["BDEBET"]) != 0)
                    {
                        // Сальдо абонента на начало в первом месяце истории (dr["MONTH"], dr["YEAR"]) не равно нулю
                        FirstSaldoIsNotNullError fsinne = new FirstSaldoIsNotNullError(
                            dr["LSHET"].ToString().Trim(),
                            currMonth,
                            currYear,
                            Convert.ToInt32(dr["ServiceCD"]),
                            Convert.ToString(dr["ServiceNam"]),
                            Convert.ToDecimal(dr["BDEBET"]), 
                            command);
                        fsinne.ParentCheckCase = this;
                        this.ErrorList.Add(fsinne);
                        if (this.Result != CheckCaseStatus.Выявлена_терминальная_ошибка)
                            this.Result = CheckCaseStatus.Выявлена_ошибка;
                    }
                    oldLshet = dr["LSHET"].ToString().Trim();
                    oldServiceCD = Convert.ToInt32(dr["ServiceCD"]);
                }
                oldEndDebet = Convert.ToDecimal(dr["EDEBET"]);
            }
            dbConn.Close();
            dbConn.Open();

            //if (dt.Rows.Count == 0)
            //    return;
            //else
            //{
            //    NachoplCalculationErrorClass ncec = new NachoplCalculationErrorClass();
            //    ncec.ParentCheckCase = this;
            //    this.ErrorList.Add(ncec);
            //    this.Result = CheckCaseStatus.Выявлена_ошибка;
            //}

            #region
    //Query.DatabaseName := ODBCName;
    //Query.SQL.Text := ''SELECT LSHET, Month, Year, ServiceCD, BDEBET, EDEBET FROM NACHOPL ORDER BY Lshet,ServiceCD,Year,Month'';
    //Query.Open;
    //oldEndDebet := 0;
    //oldLshet := ''-1'';
    //oldServiceCD := -1;
    //Counter := 0;
    //while not Query.Eof do begin
    //  if (oldLshet = Query.FieldByName(''LSHET'').AsString) and
    //     (oldServiceCD = SafeInteger(Query,''SERVICECD'')) then
    //  begin
    //    if currMonth = 12 then begin
    //      currYear := currYear + 1;
    //      currMonth := 1;
    //    end else
    //      currMonth := currMonth + 1;
    //    if (currMonth <> SafeInteger(Query,''MONTH'')) or
    //       (currYear <> SafeInteger(Query,''YEAR'')) then begin
    //       Protocol := ''Пропуск в истории оплат/начислений абонента ''+ Query.FieldByName(''LSHET'').AsString+'' ''+
    //                   ''(см. предыдущий месяц для '' + IntToStr(SafeInteger(Query,''MONTH''))+''.''+IntToStr(SafeInteger(Query,''YEAR'')) + '')'';
    //       break;
    //    end;
    //    if oldEndDebet <> SafeCurrency(Query, ''BDEBET'') then
    //    begin
    //      Protocol := Query.FieldByName(''LSHET'').AsString + '', '' +
    //                           ''предыдущий месяц EDEBET = '' + FloatToStr(oldEndDebet) + '', '' +
    //                           ''текущий месяц (''+IntToStr(currMonth) + ''.'' + IntToStr(currYear) + '') '' +
    //                           ''BDEBET = ''+ FloatToStr(SafeCurrency(Query, ''BDEBET''));
    //      break;
    //    end;
    //  end
    //  else
    //  begin
    //    if SafeCurrency(Query, ''BDEBET'') <> 0 then
    //    begin
    //      Protocol := ''Сальдо абонента '' + Query.FieldByName(''LSHET'').AsString + '' в первом месяце истории ('' +
    //                           IntToStr(SafeInteger(Query,''MONTH'')) + ''.'' + IntToStr(SafeInteger(Query,''YEAR'')) + '') '' +
    //                           ''не равно нулю'';
    //      break;
    //    end;
    //    oldLshet := Query.FieldByName(''LSHET'').AsString;
    //    oldServiceCD := SafeInteger(Query, ''SERVICECD'');
    //    currMonth := SafeInteger(Query,''MONTH'');
    //    currYear := SafeInteger(Query,''YEAR'');
    //  end;
    //  oldEndDebet := SafeCurrency(Query,''EDEBET'');
    //  Query.Next;
    //  Counter := Counter + 1;
    //  if (Counter mod 10000) = 0 then
    //    SMessage(''Проверено '' + IntToStr(Counter) + '' записей...'');
    //end;
    //Query.Close;
    //Result := Protocol;
            #endregion

        }
    }
}
