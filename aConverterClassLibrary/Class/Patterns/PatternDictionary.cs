using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;

namespace aConverterClassLibrary
{
    public class PatternDictionary
    {
        private string dictionaryName;
        /// <summary>
        /// Имя словаря
        /// </summary>
        public string DictionaryName
        {
            get { return dictionaryName; }
            set { dictionaryName = value; }
        }

        public PatternDictionary(string ADictionaryName)
        {
            DictionaryName = ADictionaryName;
        }

        private string connectionString;
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        private string query;
        /// <summary>
        /// Запрос к данным
        /// </summary>
        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        private string keyExpression;
        /// <summary>
        /// Формат выражения ключа
        /// </summary>
        public string KeyExpression
        {
            get { return keyExpression; }
            set { keyExpression = value; }
        }

        private string valueExpression;
        /// <summary>
        /// Выражение по ключу
        /// </summary>
        public string ValueExpression
        {
            get { return valueExpression; }
            set { valueExpression = value; }
        }

        private Dictionary<string, string> dictionary = new Dictionary<string,string>();
        /// <summary>
        /// Собственно словарь
        /// </summary>
        public Dictionary<string, string> Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }


        public void ExecuteDictionary()
        {
            using (FbConnection dbConn = new FbConnection(this.ConnectionString))
            {
                dbConn.Open();
                using (FbCommand fbCommand = dbConn.CreateCommand())
                {
                    fbCommand.CommandText = this.Query;
                    DbDataReader dr = fbCommand.ExecuteReader();
                    // DataTable dt = new DataTable();
                    // FbDataAdapter da = new FbDataAdapter(fbCommand);
                    // da.Fill(dt);

                    this.Dictionary.Clear();
                    // foreach (DataRow dr in dt.Rows)
                    while(dr.Read())
                    {
                        string key = PatternUtils.Parse(this.KeyExpression, dr, true, true, null);
                        string value = PatternUtils.Parse(this.ValueExpression, dr, true, true, null);
                        this.Dictionary.Add(key, value);
                    }
                    dr.Close();
                }
            }
        }
    }
}
