using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class NachoplSaldoCheckCase: CheckCase
    {
        public NachoplSaldoCheckCase()
        {
            CheckCaseName = "Проверка целостности истории сальдо в таблице CNV$NACHOPL";
        }

        public override void Analize()
        {
            Result = CheckCaseStatus.Ошибок_не_выявлено;
            ErrorList.Clear();

            using (var connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT LSHET, Year_, Month_, ServiceCD, ServiceNam, BDEBET, EDEBET " +
                                          "FROM CNV$NACHOPL " +
                                          "ORDER BY Lshet, ServiceCD, Year_, Month_";
                    connection.Open();
                    var dr = command.ExecuteReader();


                    decimal oldEndDebet = 0;
                    string oldLshet = "-1";
                    int oldServiceCd = -1;
                    int currMonth = -1;
                    int currYear = -1;

                    // Список нужен для оптимизации при генерации вариантов исправления.
                    // По нему проверяется, сформирован ли вариант пересчета сальдо для данного абонента по данной услуге
                    // Нужен для вариантов пересчета сальдо по всей истории
                    // Ключ будет составной - lshet (string) + servicecd (int)
                    var nachoplOptimization = new Dictionary<object, object>();

                    while (dr.Read())
                    {
                        if (oldLshet == dr["LSHET"].ToString().Trim() &&
                            oldServiceCd == Convert.ToInt32(dr["ServiceCD"]))
                        {
                            Utils.IncreaseMonthYear(ref currMonth, ref currYear);
                            while (currMonth != Convert.ToInt32(dr["MONTH_"]) ||
                                   currYear != Convert.ToInt32(dr["YEAR_"]))
                            {
                                // Пропуск в истории оплат/начислений
                                // Пропущена запись за currMonth, currYear
                                var nrme = new NachoplRowMissingError(
                                    dr["LSHET"].ToString().Trim(),
                                    currMonth,
                                    currYear,
                                    Convert.ToInt32(dr["ServiceCD"]),
                                    Convert.ToString(dr["ServiceNam"]),
                                    oldEndDebet, command) {ParentCheckCase = this};
                                ErrorList.Add(nrme);
                                Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                                Utils.IncreaseMonthYear(ref currMonth, ref currYear);
                            }
                            // Здесь оказываемся только если расчитанный месяц совпал с месяцем в базе
                            if (oldEndDebet != Convert.ToDecimal(dr["BDEBET"]))
                            {
                                // Сальдо на начало текущего месяца (dr["BDEBET"]) не равно сальдо на конец предыдущего (oldEndDebet)
                                var onsme = new OldNewSaldoMismatchError(oldEndDebet,
                                    dr["LSHET"].ToString(),
                                    Convert.ToInt32(dr["ServiceCD"]),
                                    dr["ServiceNam"].ToString(),
                                    Convert.ToDecimal(dr["BDEBET"]),
                                    currMonth, currYear, command, nachoplOptimization) {ParentCheckCase = this};
                                ErrorList.Add(onsme);
                                if (Result != CheckCaseStatus.Выявлена_терминальная_ошибка)
                                    Result = CheckCaseStatus.Выявлена_ошибка;
                            }
                        }
                        else
                        {
                            currMonth = Convert.ToInt32(dr["MONTH_"]);
                            currYear = Convert.ToInt32(dr["YEAR_"]);
                            if (Convert.ToDecimal(dr["BDEBET"]) != 0)
                            {
                                // Сальдо абонента на начало в первом месяце истории (dr["MONTH"], dr["YEAR"]) не равно нулю
                                var fsinne = new FirstSaldoIsNotNullError(
                                    dr["LSHET"].ToString().Trim(),
                                    currMonth,
                                    currYear,
                                    Convert.ToInt32(dr["ServiceCD"]),
                                    Convert.ToString(dr["ServiceNam"]),
                                    Convert.ToDecimal(dr["BDEBET"]),
                                    command) {ParentCheckCase = this};
                                ErrorList.Add(fsinne);
                                if (Result != CheckCaseStatus.Выявлена_терминальная_ошибка)
                                    Result = CheckCaseStatus.Выявлена_ошибка;
                            }
                            oldLshet = dr["LSHET"].ToString().Trim();
                            oldServiceCd = Convert.ToInt32(dr["ServiceCD"]);
                        }
                        oldEndDebet = Convert.ToDecimal(dr["EDEBET"]);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ошибка - отсутствует строка в истории оплат/начислений
    /// </summary>
    public class NachoplRowMissingError : ErrorClass
    {
        private readonly string _lshet;
        private readonly int _month;
        private readonly int _year;
        private readonly decimal _debet;
        private readonly int _servicecd;
        private readonly string _servicename;
        private readonly FbCommand _dbCommand;

        public NachoplRowMissingError(string aLshet, int aMonth, int aYear, int aServiceCd, string aServiceName,
            decimal aDebet, FbCommand adbCommand)
        {
            _lshet = aLshet;
            _month = aMonth;
            _year = aYear;
            _debet = aDebet;
            _servicecd = aServiceCd;
            _servicename = aServiceName.Trim();

            _dbCommand = adbCommand;

            ErrorName = String.Format("Отсутствует строка в истории оплат/начислений для абонента {0} за {1}.{2} по услуге с кодом {3} ({4})",
                _lshet, _month, _year, _servicecd, _servicename);
            IsTerminating = true;

            Statistic ss = new FdbStatistic(
                String.Format("История оплат/начислений для абонента {0} по услуге с кодом {1} ({2})",
                    _lshet, _servicecd, _servicename),
                String.Format("SELECT * FROM CNV$NACHOPL WHERE lshet = '{0}' AND servicecd = {1} order by lshet, year_, month_",
                    _lshet, _servicecd),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var artncc = new AddRowToNachoplCorrectionCase(
                _lshet,
                _month,
                _year,
                _servicecd,
                _servicename,
                _debet,
                _dbCommand) {ParentError = this};
            CorrectionCases.Add(artncc);
        }
    }

    public class AddRowToNachoplCorrectionCase : CorrectionCase
    {
        readonly string _lshet;
        readonly int _month;
        readonly int _year;
        readonly decimal _debet;
        readonly int _servicecd;
        readonly string _servicename;
        readonly FbCommand _command;

        public AddRowToNachoplCorrectionCase(string aLshet, int aMonth, int aYear, int aServiceCd, string aServiceName,
            decimal aDebet, FbCommand acommand)
        {
            CorrectionCaseName = String.Format("Добавить строку в историю оплат/начислений для абонента {0} за {1}.{2} по услуге с кодом {3} ({4}). Долг на начало и конец месяца - {5}",
                aLshet, aMonth, aYear, aServiceCd, aServiceName.Trim(), aDebet);
            _lshet = aLshet;
            _month = aMonth;
            _year = aYear;
            _debet = aDebet;
            _servicecd = aServiceCd;
            _servicename = aServiceName;
            _command = acommand;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";

            try
            {
                _command.CommandText = String.Format("INSERT INTO CNV$NACHOPL (LSHET, ServiceCD, ServiceNam, Month_, Year_, Month2, Year2, BDebet, FNath, Prochl, Oplata, EDebet) " +
                    "VALUES ('{0}', {1}, '{2}', {3}, {4}, {3}, {4}, {5}, 0, 0, 0, {5})",
                    _lshet,
                    _servicecd, _servicename,
                    _month,
                    _year,
                    _debet.ToString(CultureInfo.InvariantCulture).Replace(',', '.'));
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }

    public class OldNewSaldoMismatchError : ErrorClass
    {
        readonly decimal _oldSaldo;
        readonly string _lshet;
        readonly int _serviceCd;
        readonly string _serviceName;
        readonly decimal _newSaldo;
        readonly int _newMonth;
        readonly int _newYear;
        readonly FbCommand _command;
        readonly Dictionary<object, object> _nachoplOptimization;

        public OldNewSaldoMismatchError(decimal aOldSaldo,
            string aLshet,
            int aServiceCd,
            string aServiceName,
            decimal aNewSaldo,
            int aNewMonth, int aNewYear,
            FbCommand acommand,
            Dictionary<object, object> anachoplOptimization)
        {
            int oldYear;
            int oldMonth;
            _oldSaldo = aOldSaldo;
            _lshet = aLshet.Trim();
            _serviceCd = aServiceCd;
            _serviceName = aServiceName.Trim();
            _nachoplOptimization = anachoplOptimization;
            if (aNewMonth == 1)
            {
                oldMonth = 12;
                oldYear = aNewYear - 1;
            }
            else
            {
                oldMonth = aNewMonth - 1;
                oldYear = aNewYear;
            }
            _newSaldo = aNewSaldo;
            _newMonth = aNewMonth;
            _newYear = aNewYear;
            _command = acommand;

            ErrorName = String.Format("Значение сальдо {0} для лицевого счета {1} по услуге с кодом {2} ({3}) на конец " +
                "{4}.{5} не совпадает со значением {6} на начало {7}.{8}",
                _oldSaldo, _lshet, _serviceCd, _serviceName, oldMonth, oldYear, _newSaldo, _newMonth, _newYear);
            IsTerminating = false;

            PossibleErrorParams.Add(ErrorParam.Тип_корректировки_сальдо_в_NACHOPL);

            Statistic ss = new FdbStatistic(
                String.Format("История оплат/начислений для абонента {0} по услуге с кодом {1} ({2})",
                    _lshet, _serviceCd, _serviceName),
                String.Format("SELECT * FROM CNV$NACHOPL WHERE lshet = '{0}' AND servicecd = {1} order by lshet, year_, month_",
                    _lshet, _serviceCd),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            if (!AllParamsPresent())
            {
                string message = String.Format("Для ошибки \"{0}\" заданы не все параметры",
                    this);
                throw new Exception(message);
            }
            int value = Convert.ToInt32(ErrorParams[ErrorParam.Тип_корректировки_сальдо_в_NACHOPL]);
            var nsct = (NachoplSaldoCorrectionType)value;
            if (nsct != NachoplSaldoCorrectionType.Не_корректировать_сальдо)
            {
                bool needcreate = true;
                if (nsct == NachoplSaldoCorrectionType.Пересчитать_сальдо_вперед_с_начала_истории ||
                    nsct == NachoplSaldoCorrectionType.Пересчитать_сальдо_назад_с_конца_истории)
                {
                    string c = _lshet.PadRight(10) + _serviceCd.ToString(CultureInfo.InvariantCulture).PadLeft(5);
                    if (_nachoplOptimization.ContainsKey(c))
                        needcreate = false;
                    else
                        _nachoplOptimization.Add(c, null);
                }
                if (needcreate)
                {
                    var nccc = new NachoplSaldoCorrectionCase(nsct,
                        _oldSaldo,
                        _lshet,
                        _serviceCd,
                        _serviceName,
                        _newSaldo,
                        _newMonth,
                        _newYear,
                        _command) {ParentError = this};
                    CorrectionCases.Add(nccc);
                }
            }
        }
    }

    public class NachoplSaldoCorrectionCase : CorrectionCase
    {
        private readonly NachoplSaldoCorrectionType _nachoplSaldoCorrectionType;

        readonly decimal _oldSaldo;
        readonly string _lshet;
        readonly int _servicecd;
        readonly int _oldMonth;
        readonly int _oldYear;
        readonly decimal _newSaldo;
        readonly int _newMonth;
        readonly int _newYear;
        readonly FbCommand _command;

        public NachoplSaldoCorrectionCase(NachoplSaldoCorrectionType aNachoplSaldoCorrectionType,
            decimal aOldSaldo,
            string aLshet,
            int aServiceCd,
            string aServiceName,
            decimal aNewSaldo,
            int aNewMonth, int aNewYear,
            FbCommand acommand)
        {
            _nachoplSaldoCorrectionType = aNachoplSaldoCorrectionType;

            _oldSaldo = aOldSaldo;
            _lshet = aLshet.Trim();
            _servicecd = aServiceCd;
            string servicename = aServiceName.Trim();

            _oldMonth = aNewMonth;
            _oldYear = aNewYear;
            Utils.DecreaseMonthYear(ref _oldMonth, ref _oldYear);

            _newSaldo = aNewSaldo;
            _newMonth = aNewMonth;
            _newYear = aNewYear;

            _command = acommand;

            CorrectionCaseName = String.Format(
                "Скорректировать сальдо абонента {0} по услуге {1} ({2}). Тип корректировки - \"{3}\"",
                _lshet, _servicecd, servicename, _nachoplSaldoCorrectionType.ToString().Replace('_', ' '));
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";

            try
            {
                if (_nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Пересчитать_сальдо_вперед_с_начала_истории)
                {
                    _command.CommandText = String.Format(
                                            "SELECT LSHET, Year_, Month_, ServiceCD, ServiceNam, BDEBET, EDEBET " +
                                            "FROM CNV$NACHOPL " +
                                            "WHERE LSHET = '{0}' AND ServiceCD = {1} " +
                                            "ORDER BY Lshet, ServiceCD, Year_, Month_",
                                            _lshet, _servicecd);
                    var dt = new DataTable();
                    var da = new FbDataAdapter(_command);
                    da.Fill(dt);
                    decimal lastSaldo = 0;
                    if (dt.Rows.Count > 0)
                    {
                        lastSaldo = Convert.ToDecimal(dt.Rows[0]["BDEBET"]);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        int currMonth = Convert.ToInt32(dr["MONTH_"]);
                        int currYear = Convert.ToInt32(dr["YEAR_"]);
                        _command.CommandText = String.Format(
                            "UPDATE CNV$NACHOPL SET BDEBET = {0} " +
                            "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR_ = {4} AND MONTH_ = {3}",
                            lastSaldo.ToString(CultureInfo.InvariantCulture).Replace(',', '.'), _lshet, _servicecd, currMonth, currYear);
                        _command.ExecuteNonQuery();
                        _command.CommandText = String.Format(
                            "UPDATE CNV$NACHOPL SET EDEBET = BDEBET + FNATH + PROCHL - OPLATA " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                            _lshet, _servicecd, currMonth, currYear);
                        _command.ExecuteNonQuery();
                        _command.CommandText = String.Format(
                            "SELECT EDEBET FROM CNV$NACHOPL " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                            _lshet, _servicecd, currMonth, currYear);
                        lastSaldo = Convert.ToDecimal(_command.ExecuteScalar());
                    }
                }
                else if (_nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Пересчитать_сальдо_назад_с_конца_истории)
                {
                    _command.CommandText = String.Format(
                                            "SELECT LSHET, Year_, Month_, ServiceCD, ServiceNam, BDEBET, EDEBET " +
                                            "FROM CNV$NACHOPL " +
                                            "WHERE LSHET = '{0}' AND ServiceCD = {1} " +
                                            "ORDER BY Lshet, ServiceCD, Year_ DESCENDING, Month_ DESCENDING",
                                            _lshet, _servicecd);
                    var dt = new DataTable();
                    var da = new FbDataAdapter(_command);
                    da.Fill(dt);
                    decimal lastSaldo = 0;
                    if (dt.Rows.Count > 0)
                    {
                        lastSaldo = Convert.ToDecimal(dt.Rows[0]["EDEBET"]);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        int currMonth = Convert.ToInt32(dr["MONTH_"]);
                        int currYear = Convert.ToInt32(dr["YEAR_"]);
                        _command.CommandText = String.Format(
                            "UPDATE CNV$NACHOPL SET EDEBET = {0} " +
                            "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR_ = {4} AND MONTH_ = {3}",
                            lastSaldo.ToString(CultureInfo.InvariantCulture).Replace(',', '.'), _lshet, _servicecd, currMonth, currYear);
                        _command.ExecuteNonQuery();
                        _command.CommandText = String.Format(
                            "UPDATE CNV$NACHOPL SET BDEBET = EDEBET - (FNATH + PROCHL - OPLATA) " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                            _lshet, _servicecd, currMonth, currYear);
                        _command.ExecuteNonQuery();
                        _command.CommandText = String.Format(
                            "SELECT BDEBET FROM CNV$NACHOPL " +
                            "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                            _lshet, _servicecd, currMonth, currYear);
                        _command.ExecuteNonQuery();
                        lastSaldo = Convert.ToDecimal(_command.ExecuteScalar());
                    }
                }
                else if (_nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Скорректировать_сальдо_на_начало_текущего_месяца_с_суммой_изменений_в_текущем_месяце)
                {
                    _command.CommandText = String.Format(
                        "UPDATE CNV$NACHOPL SET BDEBET = {0} " +
                        "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR_ = {4} AND MONTH_ = {3}",
                        _oldSaldo.ToString(CultureInfo.InvariantCulture).Replace(',', '.'), _lshet, _servicecd, _newMonth, _newYear);
                    _command.ExecuteNonQuery();
                    _command.CommandText = String.Format(
                        "UPDATE CNV$NACHOPL SET PROCHL = EDEBET - (BDEBET + FNATH - OPLATA) " +
                        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                        _lshet, _servicecd, _newMonth, _newYear);
                    _command.ExecuteNonQuery();
                }
                else if (_nachoplSaldoCorrectionType == NachoplSaldoCorrectionType.Скорректировать_суммой_изменений_сальдо_на_конец_предыдущего_месяца)
                {
                    _command.CommandText = String.Format(
                        "UPDATE CNV$NACHOPL SET EDEBET = {0} " +
                        "WHERE LSHET = '{1}' AND SERVICECD = {2} AND YEAR_ = {4} AND MONTH_ = {3}",
                        _newSaldo.ToString(CultureInfo.InvariantCulture).Replace(',', '.'), _lshet, _servicecd, _oldMonth, _oldYear);
                    _command.ExecuteNonQuery();
                    _command.CommandText = String.Format(
                        "UPDATE CNV$NACHOPL SET PROCHL = EDEBET - (BDEBET + FNATH - OPLATA) " +
                        "WHERE LSHET = '{0}' AND SERVICECD = {1} AND YEAR_ = {3} AND MONTH_ = {2}",
                        _lshet, _servicecd, _oldMonth, _oldYear);
                    _command.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("Неизвестный тип корректировки сальдо");
                }
            }
            catch (Exception ex)
            {
                Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }

    public class FirstSaldoIsNotNullError : ErrorClass
    {
        readonly string _lshet;
        readonly int _month;
        readonly int _year;
        readonly decimal _debet;
        readonly int _servicecd;
        readonly string _servicename;
        readonly FbCommand _command;

        public FirstSaldoIsNotNullError(string aLshet, int aMonth, int aYear, int aServiceCd, string aServiceName, decimal aDebet,
            FbCommand acommand)
        {
            _lshet = aLshet;
            _month = aMonth;
            _year = aYear;
            _debet = aDebet;
            _servicecd = aServiceCd;
            _servicename = aServiceName.Trim();
            _command = acommand;

            ErrorName = String.Format("Сальдо {5} для абонента {0} на начало {1}.{2} по услуге с кодом {3} ({4}) является первым в истории оплат/начислений и не равно 0",
                _lshet, _month, _year, _servicecd, _servicename, _debet);
            IsTerminating = true;
            Utils.DecreaseMonthYear(ref _month, ref _year);

            Statistic ss = new FdbStatistic(
                String.Format("История оплат/начислений для абонента {0} по услуге с кодом {1} ({2})",
                    _lshet, _servicecd, _servicename),
                String.Format("SELECT * FROM CNV$NACHOPL WHERE lshet = '{0}' AND servicecd = {1} order by lshet, year_, month_",
                    _lshet, _servicecd),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            var artncc = new AddFirstSaldoRowCorrectionCase(
                _lshet,
                _month,
                _year,
                _servicecd,
                _servicename,
                _debet,
                _command) {ParentError = this};
            CorrectionCases.Add(artncc);
        }
    }

    public class AddFirstSaldoRowCorrectionCase : CorrectionCase
    {
        readonly string _lshet;
        readonly int _month;
        readonly int _year;
        readonly decimal _debet;
        readonly int _servicecd;
        readonly string _servicename;
        readonly FbCommand _command;

        public AddFirstSaldoRowCorrectionCase(string aLshet, int aMonth, int aYear, int aServiceCd, string aServiceName,
            decimal aDebet, FbCommand acommand)
        {
            CorrectionCaseName = String.Format("Добавить строку в историю оплат/начислений для абонента {0} за {1}.{2} по услуге с кодом {3} ({4}). Долг на конец месяца и сумма изменений - {5}",
                aLshet, aMonth, aYear, aServiceCd, aServiceName.Trim(), aDebet);
            _lshet = aLshet;
            _month = aMonth;
            _year = aYear;
            _debet = aDebet;
            _servicecd = aServiceCd;
            _servicename = aServiceName;
            _command = acommand;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            Message = "Корректировка завершилась успешно";

            try
            {
                _command.CommandText = String.Format("INSERT INTO CNV$NACHOPL (LSHET, ServiceCD, ServiceNam, Month_, Year_, Month2, Year2, BDebet, FNath, Prochl, Oplata, EDebet) " +
                    "VALUES ('{0}', {1}, '{2}', {3}, {4}, {3}, {4}, 0, 0, {5}, 0, {5})",
                    _lshet,
                    _servicecd, _servicename.Replace("'", "\""),
                    _month,
                    _year,
                    _debet.ToString(CultureInfo.InvariantCulture).Replace(',', '.'));
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
        }
    }

}
