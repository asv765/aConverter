using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Класс - вариант исправления
    /// </summary>
    public abstract class CorrectionCase
    {
        //private Dictionary<string, object> correctionCaseParams = new Dictionary<string,object>();
        ///// <summary>
        ///// Параметры варианта исправления
        ///// </summary>
        //public Dictionary<string, object> CorrectionCaseParams
        //{
        //    get { return correctionCaseParams; }
        //    set { correctionCaseParams = value; }
        //}

        private string correctionCaseName;
        /// <summary>
        /// Наименование варианта корректировки
        /// </summary>
        public string CorrectionCaseName
        {
            get { return correctionCaseName; }
            set { correctionCaseName = value; }
        }

        private CorrectionCaseStatus result = CorrectionCaseStatus.Корректировка_не_производилась;
        /// <summary>
        /// Результаты выполнения варианта корректировки
        /// </summary>
        public CorrectionCaseStatus Result
        {
            get { return result; }
            set { result = value; }
        }

        private string message;
        /// <summary>
        /// Развернутое диагностическое сообщение
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private ErrorClass parentError;
        /// <summary>
        /// Ссылка на породившую ошибку
        /// </summary>
        public ErrorClass ParentError
        {
            get { return parentError; }
            set { parentError = value; }
        }

        public abstract void Correct();

        public static int SetFdbGenerator(string AGeneratorName, string ATableName, string AFieldName)
        {
            int maxValue = 0;
            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = String.Format("SELECT MAX({0}) AS FIELD FROM {1}",
                        AFieldName, ATableName);
                    object value = command.ExecuteScalar();
                    if (value != null) maxValue = Convert.ToInt32(value);
                    command.CommandText = String.Format("SET GENERATOR {0} TO {1}",
                        AGeneratorName, maxValue);
                    command.ExecuteNonQuery();
                }
            }
            return maxValue;
        }
    }

    public enum CorrectionCaseStatus
    {
        Корректировка_не_производилась = 0,
        Корректировка_выполнена_успешно = 1,
        Ошибка_при_выполнении_корректировки = 2
    }
}
