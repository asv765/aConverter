using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public abstract class CheckCase
    {
        private string checkCaseName;
        /// <summary>
        /// Наименование варианта проверки
        /// </summary>
        public string CheckCaseName
        {
            get { return checkCaseName; }
            set { checkCaseName = value; }
        }

        //private CheckCaseClass checkCaseClass;
        ///// <summary>
        ///// Класс варианта проверки
        ///// </summary>
        //public CheckCaseClass CheckCaseClass
        //{
        //    get { return checkCaseClass; }
        //    set { checkCaseClass = value; }
        //}

        /// <summary>
        /// Наименование класса варианта проверки
        /// </summary>
        //public string CheckCaseClassName
        //{
        //    get { return checkCaseClass.ToString().Replace('_', ' '); }
        //}

        private CheckCaseStatus result = CheckCaseStatus.Анализ_не_проводился;
        /// <summary>
        /// Результаты анализа
        /// </summary>
        public CheckCaseStatus Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// Результаты анализа в виде строки
        /// </summary>
        public string ResultString
        {
            get 
            {
                string rs = result.ToString().Replace('_', ' ');
                if (Result == CheckCaseStatus.Выполняется_анализ)
                {
                    rs += "...";
                }
                return rs; 
            }
        }

        private bool needAnalize = true;
        /// <summary>
        /// Необходимо анализировать
        /// </summary>
        public bool NeedAnalize
        {
            get { return needAnalize; }
            set { needAnalize = value; }
        }


        private List<ErrorClass> errorList = new List<ErrorClass>();
        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<ErrorClass> ErrorList
        {
          get { return errorList; }
          set { errorList = value; }
        }

        private Dictionary<string, object> checkCaseParams = new Dictionary<string, object>();
        /// <summary>
        /// Параметры варианта проверки. Используются для генерации ошибки
        /// </summary>
        public Dictionary<string, object> CheckCaseParams
        {
            get { return checkCaseParams; }
            set { checkCaseParams = value; }
        }

        /// <summary>
        /// Генерирует список ошибок
        /// </summary>
        public abstract void Analize();
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
