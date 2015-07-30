using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// Количество строк, возвращаемых хранимой процедурой, которые можно считать нормальной ситуацией (ошибки не найдены)
        /// </summary>
        public int NormalRows { get; set; }

        /// <summary>
        /// Может выдавать тестовую информацию
        /// </summary>
        public bool CanTest { get; set; }

        /// <summary>
        /// Может выполнять анализ
        /// </summary>
        public bool CanAnalyze { get; set; }

        /// <summary>
        /// Может корректировать причины, приведшие к ошибкам
        /// </summary>
        public bool CanFix { get; set; }

        /// <summary>
        /// Проверяет на наличие ошибок
        /// </summary>
        /// <returns>true - есть ошибки</returns>
        public bool Test()
        {
            Result = CheckCaseStatus.Выполняется_анализ;
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            string query = String.Format("SELECT * FROM {0}({1})", StoredProcName, 0);
            DataTable dt = fbm.ExecuteQuery(query);
            bool testResult = dt.Rows.Count > NormalRows;
            Result = testResult ? CheckCaseStatus.Выявлена_ошибка : CheckCaseStatus.Ошибок_не_выявлено;
            return testResult;
        }

        public DataTable Analize()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            string query = String.Format("SELECT * FROM {0}({1})", StoredProcName, 1);
            DataTable dt = fbm.ExecuteQuery(query);
            return dt;
        }

        public void Fix()
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            string query = String.Format("EXECUTE PROCEDURE {0}({1})", StoredProcName, 2);
            fbm.ExecuteNonQuery(query);
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
