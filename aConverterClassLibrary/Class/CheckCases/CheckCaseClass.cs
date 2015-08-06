using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using aConverterClassLibrary.Class;

namespace aConverterClassLibrary
{
    public class CheckCase
    {
        /// <summary>
        /// Наименование хранимой процедуры, выполняющей проверку
        /// </summary>
        public string StoredProcName { get; set; }

        /// <summary>
        /// "Укороченное" название процедуры (без префикса CNV$)
        /// </summary>
        public string ShortStoredProcName
        {
            get
            {
                Match m = Regex.Match(StoredProcName, @"(?<=CNV\$).*");
                return m.Value;
            }
        }

        /// <summary>
        /// Описание варианта проверки
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на вариант проверки, от которого зависит данная. Служит для определения последовательноси и 
        /// выявления терминальных проверок.
        /// </summary>
        public CheckCase DependOn { get; set; }

        private CheckCaseStatus _result = CheckCaseStatus.Анализ_не_проводился;
        /// <summary>
        /// Результаты анализа
        /// </summary>
        public CheckCaseStatus Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// Результаты анализа в виде строки
        /// </summary>
        public string ResultString
        {
            get 
            {
                string rs = _result.ToString().Replace('_', ' ');
                if (Result == CheckCaseStatus.Выполняется_анализ)
                {
                    rs += "...";
                }
                return rs; 
            }
        }

        private bool _needTest = true;
        /// <summary>
        /// Необходимо анализировать
        /// </summary>
        public bool NeedTest
        {
            get { return _needTest; }
            set { _needTest = value; }
        }

        /// <summary>
        /// Количество строк, возвращаемых запросом, которые можно считать нормальной ситуацией (ошибки не найдены)
        /// </summary>
        public int NormalRows { get; set; }

        /// <summary>
        /// Запрос для анализа ошибок
        /// </summary>
        public string AnalyzeQuery { get; set; }

        /// <summary>
        /// Результат анализа данных
        /// </summary>
        public string AnalyzeResultDescription { get; set; }

        private string _testQuery;

        /// <summary>
        /// Запрос для тестирования на наличие ошибок
        /// </summary>
        public string TestQuery
        {
            get
            {
                if (String.IsNullOrEmpty(_testQuery) && AnalyzeQuery.ToUpper().StartsWith("SELECT "))
                   return "SELECT FIRST 1 " + AnalyzeQuery.Substring(7);
                return _testQuery;
            }
            set { _testQuery = value; }
        }

        /// <summary>
        /// Может выполнять анализ
        /// </summary>
        public bool CanAnalyze
        {
            get { return !String.IsNullOrEmpty(AnalyzeQuery); }
        }

        /// <summary>
        /// Команда для исправления ошибок
        /// </summary>
        public string FixCommand { get; set; }

        /// <summary>
        /// Описания алгоритма исправления ошибки
        /// </summary>
        public string FixDescription { get; set; }

        /// <summary>
        /// Может корректировать причины, приведшие к ошибкам
        /// </summary>
        public bool CanFix
        {
            get { return !String.IsNullOrEmpty(FixCommand); }
        }


        /// <summary>
        /// Проверяет на наличие ошибок
        /// </summary>
        /// <returns>true - есть ошибки</returns>
        public bool Test()
        {
            Result = CheckCaseStatus.Выполняется_анализ;
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            if (String.IsNullOrEmpty(AnalyzeQuery)) throw new Exception("На задан запрос для анализа");
            DataTable dt = fbm.ExecuteQuery(TestQuery);
            bool testResult = dt.Rows.Count != NormalRows;
            Result = testResult ? CheckCaseStatus.Выявлена_ошибка : CheckCaseStatus.Ошибок_не_выявлено;
            // Thread.Sleep(3000);
            return testResult;
        }


        public DataTable Analize()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            if (String.IsNullOrEmpty(AnalyzeQuery)) throw new Exception("На задан запрос для анализа");
            DataTable dt = fbm.ExecuteQuery(AnalyzeQuery);
            return dt;
        }

        public void Fix()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            if (String.IsNullOrEmpty(FixCommand)) throw new Exception("На задана команда для исправления данных");
            fbm.ExecuteNonQuery(FixCommand);
            Result = CheckCaseStatus.Ошибок_не_выявлено;
        }

    }

    public enum CheckCaseStatus
    {
        Анализ_не_проводился = 0,
        Ошибок_не_выявлено = 1,
        Выявлена_ошибка = 2,
        Выявлена_терминальная_ошибка = 3, // Обработка должна быть прекращена
        Выполняется_анализ = 4 
    }

    //public enum CheckCaseClass
    //{
    //    Целостность_конвертируемых_данных,
    //    Целостность_между_конвертируемыми_данными_и_целевой_БД,
    //    Целостность_структуры_конвертируемых_данных,
    //    Целостность_целевой_БД
    //}
}
