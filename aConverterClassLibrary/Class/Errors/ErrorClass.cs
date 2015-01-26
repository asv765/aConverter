using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Класс ошибка
    /// </summary>
    public abstract class ErrorClass
    {
        private string errorName;
        /// <summary>
        /// Наименование ошибки
        /// </summary>
        public string ErrorName
        {
            get { return errorName; }
            set { errorName = value; }
        }

        private bool isTerminating;
        /// <summary>
        /// Признак, является ли ошибка терминальной (дальнейший анализ прекращается)
        /// </summary>
        public bool IsTerminating
        {
            get { return isTerminating; }
            set { isTerminating = value; }
        }

        private List<ErrorParam> possibleErrorParams = new List<ErrorParam>();
        /// <summary>
        /// Необходимые параметры
        /// </summary>
        public List<ErrorParam> PossibleErrorParams
        {
            get { return possibleErrorParams; }
            set { possibleErrorParams = value; }
        }

        private Dictionary<ErrorParam, object> errorParams = new Dictionary<ErrorParam, object>();
        /// <summary>
        /// Параметры ошибки. Используются для генерации и работы варианта исправления
        /// </summary>
        public Dictionary<ErrorParam, object> ErrorParams
        {
            get { return errorParams; }
            set { errorParams = value; }
        }

        private List<CorrectionCase> correctionCases = new List<CorrectionCase>();
        /// <summary>
        /// Список вариантов исправления
        /// </summary>
        public List<CorrectionCase> CorrectionCases
        {
            get { return correctionCases; }
            set { correctionCases = value; }
        }

        private List<string> detail = new List<string>();
        /// <summary>
        /// Детализация ошибки
        /// </summary>
        public List<string> Detail
        {
            get { return detail; }
            set { detail = value; }
        }

        

        private List<Statistic> statisticSets = new List<Statistic>();
        /// <summary>
        /// Связанные с ошибкой наборы статистики
        /// </summary>
        public List<Statistic> StatisticSets
        {
            get { return statisticSets; }
            set { statisticSets = value; }
        }

        /// <summary>
        /// Есть записи детализации
        /// </summary>
        public bool DetailPresent
        {
            get { return this.Detail.Count > 0; }
        }

        /// <summary>
        /// Есть связанные статистики
        /// </summary>
        public bool StatisticSetPresent
        {
            get { return this.StatisticSets.Count > 0; }
        }

        /// <summary>
        /// Генерирует список вариантов исправления
        /// </summary>
        public abstract void GenerateCorrectionCases();

        /// <summary>
        /// Признак устанавливается в true, когда все варианты исправления по ошибке отработаны
        /// успешно (она считается исправленной)
        /// </summary>
        public bool Corrected
        {
            get 
            {
                if (this.CorrectionCases.Count == 0) return false;
                foreach (CorrectionCase ccc in this.CorrectionCases)
                {
                    if (ccc.Result == CorrectionCaseStatus.Корректировка_не_производилась ||
                        ccc.Result == CorrectionCaseStatus.Ошибка_при_выполнении_корректировки)
                    {
                        return false;
                    }
                }
                return true; 
            }
        }

        private CheckCase parentCheckCase;
        /// <summary>
        /// Ссылка на породивший варианта проверки
        /// </summary>
        public CheckCase ParentCheckCase
        {
            get { return parentCheckCase; }
            set { parentCheckCase = value; }
        }

        public override string ToString()
        {
            return this.ErrorName;
        }

        public bool AllParamsPresent()
        {
            foreach (ErrorParam ep in PossibleErrorParams)
            {
                object value;
                if (!ErrorParams.TryGetValue(ep, out value)) return false;
            }
            return true;
        }
    }
}
