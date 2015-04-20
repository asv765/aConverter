using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    /// <summary>
    /// Класс-шаблон для генерации скрипта
    /// </summary>
    public class Pattern
    {
        public const string FieldPattern = @"(?i)(?<=%)[A-Za-z]+:*\S*?(?=%)";

        private string description;
        /// <summary>
        /// Описание шаблона
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
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

        private string delete;
        /// <summary>
        /// Запросы на удаление данных
        /// </summary>
        public string Delete
        {
            get { return delete; }
            set { delete = value; }
        }

        private bool includeDeleteSection;
        /// <summary>
        /// Включить секцию удаления
        /// </summary>
        public bool IncludeDeleteSection
        {
            get { return includeDeleteSection; }
            set { includeDeleteSection = value; }
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

        private string header;
        /// <summary>
        /// Заголовок скрипта
        /// </summary>
        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private string body;
        /// <summary>
        /// Тело скрипта
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private MatchCollection bodyMatchCollection = null;
        public MatchCollection BodyMatchCollection
        {
            get { return bodyMatchCollection; }
        }

        private List<object[]> groupBodyCollection = new List<object[]>();
        /// <summary>
        /// Скрипт, выполняемый при смене значений полей-атрибутов группы. Элементами списка является массив из трех строк:
        ///   - выражение, изменение значения которого отслеживается;
        ///   - тело GroupBody;
        ///   - текущее значение выражения;
        /// </summary>
        private List<object[]> GroupBodyCollection
        {
            get { return groupBodyCollection; }
            set { groupBodyCollection = value; }
        }

        private string footer;
        /// <summary>
        /// Подвал скрипта
        /// </summary>
        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        private string fileName;
        /// <summary>
        /// Файл шаблона
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }


        public delegate void Iterate();
        /// <summary>
        /// Событие, вызывается после каждой обработанной DataRow
        /// </summary>
        public event Iterate onIterate;

        public delegate void Counted(int Count);
        /// <summary>
        /// Событие, вызывается после того как стало известно, какое количество записей должно обрабатываться
        /// </summary>
        public event Counted onCounted;

        /// <summary>
        /// Короткое (без каталога) имя файла
        /// </summary>
        public string ShortFileName
        {
            get
            {
                return Path.GetFileName(FileName);
            }
        }

        private Dictionary<string, PatternDictionary> dictionaryCollection = new Dictionary<string, PatternDictionary>();
        /// <summary>
        /// Словари, используемые в шаблоне
        /// </summary>
        public Dictionary<string, PatternDictionary> DictionaryCollection
        {
            get { return dictionaryCollection; }
            set { dictionaryCollection = value; }
        }


        public Pattern(string AfileName)
        {
            fileName = AfileName;
        }

        private bool isActive = true;
        /// <summary>
        /// Признак активности шаблона (по нему будет генериться скрипт)
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Признак того, что в шаблоне присутствуют регулярные выражения 
        /// </summary>
        private bool regexPresent = false;

        /// <summary>
        /// Признак того, что в шаблоне присутствуют условные строки
        /// </summary>
        private bool conditionStringsPresent = false;

        /// <summary>
        /// Загружает паттерн из файла
        /// </summary>
        public void LoadPattern()
        {
            string[] pattern = File.ReadAllLines(fileName, Encoding.GetEncoding(1251));

            #region Разбираем выражения #include
            List<string> deinlcudedPattern = new List<string>();
            foreach (string s in pattern)
            {
                string currentline = s;
                MatchCollection mc = Regex.Matches(currentline, @"(?i)(?<={\$i ).*?(?=})");
                foreach(Match m in mc)
                {
                    string includeFileName = m.Value;
                    if (!Path.IsPathRooted(includeFileName))
                    {
                        includeFileName = Path.GetDirectoryName(fileName) + "\\" + includeFileName;
                        string[] includeFile = File.ReadAllLines(includeFileName, Encoding.GetEncoding(1251));
                        string inset = "";
                        foreach (string si in includeFile) inset += si;
                        currentline = Regex.Replace(currentline, @"{\$i " + m.Value + "}", inset);
                    }
                }
                deinlcudedPattern.Add(currentline);
            }
            #endregion

            #region Разбираем сам шаблон
            Sections currentSection = Sections.Unknown;
            this.Description = "";
            this.ConnectionString = "";
            this.Query = "";
            this.Header = "";
            this.Body = "";
            this.Footer = "";
            this.Delete = "";
            string currentDictionaryName = "";
            foreach (string s in deinlcudedPattern)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    Match m = Regex.Match(s, @"(?i)(?<=^\[).*?(?=\])");
                    if (m.Success)
                    {
                        Match sectionName = Regex.Match(m.Value, "(?i)[A-Za-z]+");
                        // Проверяем, является имя найденной секции одним из элементов перечисления
                        if (Enum.TryParse<Sections>(sectionName.Value, true, out currentSection))
                        {
                            if (currentSection == Sections.Dictionary)
                            {
                                currentDictionaryName = m.Value.Split(',')[1];
                                if (!DictionaryCollection.ContainsKey(currentDictionaryName))
                                    DictionaryCollection.Add(currentDictionaryName, new PatternDictionary(currentDictionaryName));
                            }
                            if (currentSection == Sections.GroupBody)
                            {
                                object[] sa = new object[5];
                                sa[0] = Regex.Match(m.Value,@"(?i)(?<=[A-Za-z]+,).*").Value;
                                GroupBodyCollection.Add(sa);
                            }
                            else
                                currentDictionaryName = "";
                            continue;
                        }
                    }
                    if (currentSection == Sections.Description)
                    {
                        if (String.IsNullOrEmpty(this.Description)) this.Description = s;
                    }
                    else if (currentSection == Sections.ConnectionString)
                    {
                        if (String.IsNullOrEmpty(this.ConnectionString)) this.ConnectionString = s;
                    }
                    else if (currentSection == Sections.Query) this.Query += s + "\r\n";
                    else if (currentSection == Sections.Delete) this.Delete += s + "\r\n";
                    else if (currentSection == Sections.Header) this.Header += s + "\r\n";
                    else if (currentSection == Sections.GroupBody) GroupBodyCollection[GroupBodyCollection.Count - 1][1] += s + "\r\n";
                    else if (currentSection == Sections.Body) this.Body += s + "\r\n";
                    else if (currentSection == Sections.Footer) this.Footer += s + "\r\n";
                    else if (currentSection == Sections.Dictionary)
                    {
                        PatternDictionary currentDictionary = DictionaryCollection[currentDictionaryName];
                        // Парсим параметр до пробела
                        Match md = Regex.Match(s, "(?i)[A-Za-z]+(?==)");
                        if (md.Success)
                        {
                            string value = Regex.Match(s, "(?i)(?<==).*").Value;
                            if (md.Value.ToUpper() == "CONNECTIONSTRING")
                                currentDictionary.ConnectionString = value;
                            else if (md.Value.ToUpper() == "QUERY")
                                currentDictionary.Query = value;
                            else if (md.Value.ToUpper() == "KEYEXPRESSION")
                                currentDictionary.KeyExpression = value;
                            else if (md.Value.ToUpper() == "VALUEEXPRESSION")
                                currentDictionary.ValueExpression = value;
                        }
                    }
                }
            }
            #endregion

            #region Проверяем, присутствуют ли в шаблонах регулярные выражения
            string regexExpression = @"(?i)(?<=Regex\(~)(.+?,.+?)(?=~\))";
            regexPresent |= Regex.Match(this.Header, regexExpression).Success;
            regexPresent |= Regex.Match(this.Body, regexExpression).Success;
            foreach (object[] lsa in this.GroupBodyCollection)
            {
                regexPresent |= Regex.Match(lsa[1].ToString(), regexExpression).Success;
            }
            regexPresent |= Regex.Match(this.Footer, regexExpression).Success;
            #endregion

            #region Проверяем, присутствуют ли в шаблонах условные строки
            string cstringExpression = @"^{\$c\s*\w+}";
            conditionStringsPresent |= Regex.Match(this.Header, cstringExpression).Success;
            conditionStringsPresent |= Regex.Match(this.Body, cstringExpression).Success;
            foreach (object[] lsa in this.GroupBodyCollection)
            {
                conditionStringsPresent |= Regex.Match(lsa[1].ToString(), cstringExpression).Success;
            }
            conditionStringsPresent |= Regex.Match(this.Footer, cstringExpression).Success;
            #endregion

            #region Заполняем MatchCollection
            bodyMatchCollection = Regex.Matches(Body, FieldPattern);

            foreach (object[] oa in this.GroupBodyCollection)
            {
                oa[3] = Regex.Matches(oa[0].ToString(), Pattern.FieldPattern);
                oa[4] = Regex.Matches(oa[1].ToString(), Pattern.FieldPattern);
            }
            #endregion
        }

        public void ExecutePattern()
        {
            string queryFileName = Path.GetDirectoryName(this.FileName) + "\\" + Path.GetFileNameWithoutExtension(this.FileName) + ".sql";
            // if (File.Exists(queryFileName)) File.Delete(queryFileName);
            StreamWriter sw = new StreamWriter(queryFileName, false, Encoding.GetEncoding(1251));
            try
            {
                foreach (PatternDictionary pd in this.DictionaryCollection.Values) pd.ExecuteDictionary();

                using (MySqlConnection dbConn = new MySqlConnection(this.ConnectionString))
                {
                    dbConn.Open();
                    using (MySqlCommand oledbCommand = dbConn.CreateCommand())
                    {
                        // Считаем количество строк
                        string query = this.Query.Replace('\r', ' ').Replace('\n', ' ').Trim();
                        query = Regex.Replace(query, "(?I)(?<=^select).*?(?=from)", " 0 ");
                        query = query + " INTO CURSOR Q1";
                        oledbCommand.CommandText = String.Format("EXECSCRIPT('{0}\rRETURN RECCOUNT()')", query);
                        int count = -1;
                        try
                        {
                            count = Convert.ToInt32(oledbCommand.ExecuteScalar());
                            if (count == 0)
                            {
                                if (onCounted != null) onCounted(1);
                                if (onIterate != null) onIterate();
                                return;
                            }
                            else
                                if (onCounted != null) onCounted(count);
                        }
                        catch
                        {
                            if (onCounted != null) onCounted(1);
                        }

                        oledbCommand.CommandText = this.Query;
                        MySqlDataReader dr = oledbCommand.ExecuteReader();

                        if (!String.IsNullOrEmpty(this.Delete) && this.IncludeDeleteSection) sw.Write(this.Delete);
                        if (!String.IsNullOrEmpty(this.Header)) sw.Write(this.Header);

                        while (dr.Read()) 
                        {
                            string lbody;

                            // Проверяем, не изменились ли значения выражений в GroupBody.
                            // Если изменились, выполняем соответствующий код
                            foreach (object[] sa in GroupBodyCollection)
                            {
                                string keyvalue = PatternUtils.Parse(sa[0].ToString(), dr, regexPresent, conditionStringsPresent, DictionaryCollection, (MatchCollection)sa[3]);
                                if (sa[2] == null || keyvalue != sa[2].ToString())
                                {
                                    sa[2] = keyvalue;
                                    lbody = PatternUtils.Parse(sa[1].ToString(), dr, regexPresent, conditionStringsPresent, DictionaryCollection, (MatchCollection)sa[4]);
                                    if (!String.IsNullOrEmpty(lbody)) sw.Write(lbody);
                                }
                            }

                            // Парсим "вообще"
                            lbody = PatternUtils.Parse(this.Body, dr, regexPresent, conditionStringsPresent, DictionaryCollection, this.BodyMatchCollection);

                            if (!String.IsNullOrEmpty(lbody)) sw.Write(lbody);
                            if (count > 0 && onIterate != null) onIterate();
                        }
                        if (!String.IsNullOrEmpty(this.Footer)) sw.Write(this.Footer);
                        if (count == -1 && onIterate != null) onIterate();
                    }
                }
            }
            catch (ArgumentException ae)
            {
                sw.WriteLine(ae.ToString());
            }
            sw.Close();
        }
    }

    public enum Sections
    {
        Unknown,
        Description,
        ConnectionString,
        Query,
        Header,
        Body,
        GroupBody,
        Footer,
        Dictionary,
        Delete
    }
}
