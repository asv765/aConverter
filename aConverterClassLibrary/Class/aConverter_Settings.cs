using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using DbfClassLibrary;



namespace aConverterClassLibrary
{
    public class aConverter_RootSettings
    {
        public static int SettingsCaseId
        {
            get { return getValue<int>(aConverter_RootSettings.SettingsFileName, "SettingsCaseId", -1); }// ??????????????????????????
            set { setValue(aConverter_RootSettings.SettingsFileName, "SettingsCaseId", value); }
        }

        /// <summary>
        /// Строка подключения к БД Firebird
        /// </summary>
        public static string FirebirdStringConnection
        {
            get 
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].FirebirdStringConnection;
                return rv;
            }
            set 
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].FirebirdStringConnection = value;
                WriteSettingsCase(lsc);
            }
        }

        /// <summary>
        /// Путь к DBF-файлам, для которых будет генерироваться обертка
        /// </summary>
        public static string SourceDBFFilePath
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].SourceDBFFilePath;
                return rv;
            }
            set 
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].SourceDBFFilePath = value;
                WriteSettingsCase(lsc);
            }
        }

        /// <summary>
        /// Путь к DBF-файлам для импорта ?????????????????????????????????????????
        /// </summary>
        public static string DestDBFFilePath
        {
            get
            {
                // return @"E:\Misc\Разработка\aConverter_Data\006_Prohladny\Source\120615\Orig";
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].DestDBFFilePath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].DestDBFFilePath = value;
                WriteSettingsCase(lsc);
            }
        }


        /// <summary>
        /// Наименование промежуточной MySQL базы данных для конвертации
        /// </summary>
        public static string DestMySqlConnectionString
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].DestMySqlConnectionString;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].DestMySqlConnectionString = value;
                WriteSettingsCase(lsc);
            }
        }

        public static string DestMySqlDatabaseName
        {
            get
            {
                string cs = DestMySqlConnectionString;
                Match m = Regex.Match(cs, @"(?<=database=').*(?=')");
                return m.Success ? m.Value : "";
            }
        }

        /// <summary>
        /// Путь к шаблонам для генерации скриптов
        /// </summary>
        public static string PatternsPath
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].PatternsPath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].PatternsPath = value;
                WriteSettingsCase(lsc);
            }
        }

        /// <summary>
        /// Путь к шаблонам для генерации скриптов
        /// </summary>
        public static string ConvertPath
        {
            get
            {
                if (SettingsCaseId == -1) return @"D:\GitDiplom\aConverter\029_Kandalaksha\bin\Debug\";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].ConvertPath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].ConvertPath = value;
                WriteSettingsCase(lsc);
            }
        }

        public static string DBFConnectionString
        {
            get
            {
                string rv = String.Format(TableManager.VFPOLEDBConnectionString, aConverter_RootSettings.DestDBFFilePath);
                return rv;
            }
        }

        /// <summary>
        /// Шаблон для имени .cs файлов (%f - имя (без расширения) исходного DBF-файла)
        /// </summary>
        //public static string CoverFileNamePattern
        //{
        //    get { return getValue<string>(aConverter_RootSettings.SettingsFileName, "CoverFileNamePattern", ""); }
        //    set { setValue(aConverter_RootSettings.SettingsFileName, "CoverFileNamePattern", value); }
        //}

        /// <summary>
        /// Шаблон для содержимого .cs файлов (%s - содержимое класса) ,
        /// </summary>
        public static string CoverFileBodyPattern
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].CoverFileBodyPattern;
                // string rv = lsc[SettingsCaseId].CoverFileBodyPattern;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].CoverFileBodyPattern = value;
                WriteSettingsCase(lsc);
            }
        }


        /// <summary>
        /// Путь, по которому склыдваются сгенерированные файлы
        /// </summary>
        public static string GeneratedFilePath
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].GeneratedFilePath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].GeneratedFilePath = value;
                WriteSettingsCase(lsc);
            }
        }

        public const string SettingsFileName = "Settings.cfg";
        /// <summary>
        /// Читает список статистик с диска
        /// </summary>
        /// <returns></returns>
        public static List<Statistic> ReadStatistics()
        {
            List<Statistic> lss;

            StreamReader sr = new StreamReader("Statistics.xml", Encoding.GetEncoding(1251));
            XmlSerializer fmt = new XmlSerializer(typeof(List<Statistic>));
            lss = (List<Statistic>)fmt.Deserialize(sr);
            sr.Close();

            return lss;
        }

        /// <summary>
        /// Сохраняет список статистик на диск
        /// </summary>
        /// <param name="Statistics"></param>
        public static void WriteStatistics(List<Statistic> Statistics)
        {
            StreamWriter sw = new StreamWriter("Statistics.xml", false, Encoding.GetEncoding(1251));
            XmlSerializer fmt = new XmlSerializer(Statistics.GetType());
            fmt.Serialize(sw, Statistics);
            sw.Close();
        }

        /// <summary>
        /// Читает список вариантов настройки с диска
        /// </summary>
        /// <returns></returns>
        public static List<SettingsCase> ReadSettingsCase()
        {
            List<SettingsCase> lsc = new List<SettingsCase>();

            if (File.Exists("SettingsCase.xml"))
            {
                StreamReader sr = new StreamReader("SettingsCase.xml", Encoding.GetEncoding(1251));
                XmlSerializer fmt = null;
                // Подавляем ошибку. Глюки Studio. См.например http://stackoverflow.com/questions/3494886/filenotfoundexception-in-applicationsettingsbase
                try
                {
                    fmt = new XmlSerializer(typeof(List<SettingsCase>));
                }
                catch (FileNotFoundException)
                { }
                if (fmt != null)
                    lsc = (List<SettingsCase>)fmt.Deserialize(sr);
                else
                    throw new Exception("По неизвестной причине не был создан XmlSerializer");
                sr.Close();
            }
            for (int i = 0; i < lsc.Count; i++)
            {
                if (!String.IsNullOrEmpty(lsc[i].CoverFileBodyPattern))
                    lsc[i].CoverFileBodyPattern = lsc[i].CoverFileBodyPattern.Replace("\n", "\r\n");
            }
            return lsc;
        }

        /// <summary>
        /// Сохраняет список статистик на диск
        /// </summary>
        /// <param name="SettingsCase"></param>
        public static void WriteSettingsCase(List<SettingsCase> SettingsCase)
        {
            StreamWriter sw = new StreamWriter("SettingsCase.xml", false, Encoding.GetEncoding(1251));
            XmlSerializer fmt = new XmlSerializer(SettingsCase.GetType());
            fmt.Serialize(sw, SettingsCase);
            sw.Close();
        } 

        /// <summary>
        /// Строка подключения для VFPOLEDB
        /// </summary>
        //public static string VFPOLEDBConnectionString
        //{
        //    get { return getValue<string>(aConverter_RootSettings.SettingsFileName, "VFPOLEDBConnectionString", @"Provider=vfpoledb.1;Data Source={0};Collating Sequence=Russian"); }
        //    set { setValue(aConverter_RootSettings.SettingsFileName, "VFPOLEDBConnectionString", value); }
        //}

        /// Получает значение параметра типа T. Если параметр отсутствует в файле конфигурации,
        /// то возвращает значение по умолчанию, переданное в качестве параметра.
        /// Используется в get-терах параметров-свойств.
        protected static T getValue<T>(string AFileName, string AVariableName, T DefaultValue)
        {
            string _variableValue = "";
            if (ConfigFile.ReadVariable(SettingsFileName,
                    AVariableName, out _variableValue))
                return ParseString<T>(_variableValue);
            else
                return DefaultValue;
        }

        /// <summary>
        /// Сохраняет значение параметра AVariableName в файле конфигурации.
        /// </summary>
        /// <param name="AVariableName"></param>
        /// <param name="AValue"></param>
        /// <returns></returns>
        protected static void setValue(string AFileName, string AVariableName, object AValue)
        {

            ConfigFile.SetVariable(SettingsFileName,
                AVariableName, AValue.ToString().Replace("\r\n","\\r\\n"));
        }

        /// <summary>
        /// Преобразует строку AValue к заданному типу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="AValue"></param>
        /// <returns></returns>
        public static T ParseString<T>(string AValue)
        {
            Type genericType = typeof(T);
            if (genericType.Equals(typeof(String)))
            {
                ConstructorInfo constructor = genericType.GetConstructor(new Type[] { typeof(char[]) });
                return (T)constructor.Invoke(new object[] { AValue.Replace("\\r\\n", "\r\n").ToCharArray() });
            }
            ConstructorInfo defaultConstructor = genericType.GetConstructor(new Type[] { });
            T result;
            MethodInfo parseMethod = genericType.GetMethod("Parse", new Type[] { typeof(string), typeof(CultureInfo) });
            if (parseMethod != null)
            {
                try
                {
                    CultureInfo culture = new CultureInfo("ru-RU");
                    try
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ",";
                        result = (T)parseMethod.Invoke(null, new object[] { AValue, culture });
                    }
                    catch
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ".";
                        result = (T)parseMethod.Invoke(null, new object[] { AValue, culture });
                    }
                }
                catch
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    try
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ",";
                        result = (T)parseMethod.Invoke(null, new object[] { AValue, culture });
                    }
                    catch
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ".";
                        result = (T)parseMethod.Invoke(null, new object[] { AValue, culture });
                    }
                }
            }
            else
            {
                parseMethod = genericType.GetMethod("Parse", new Type[] { typeof(string) });
                result = (T)parseMethod.Invoke(null, new object[] { AValue });
            }
            return result;
        }
    }

    public static class ConfigFile
    {
        /// <summary>
        /// Читает значение параметра из файла конфигурации
        /// </summary>
        /// <param name="AFileName"></param>
        /// <param name="AVariableName"></param>
        /// <param name="AVariableValue"></param>
        /// <returns></returns>
        public static bool ReadVariable(string AFileName, string AVariableName, out string AVariableValue)
        {
            AVariableValue = "";
            if (!File.Exists(AFileName)) return false;
            string[] sa = File.ReadAllLines(AFileName, Encoding.GetEncoding(1251));
            // string[] sa = File.ReadAllLines(AFileName);
            for (int i = 0; i < sa.Length; i++)
            {
                string[] _configVariable = sa[i].Split((new char[] { '=' }), 2);
                if (_configVariable[0] == AVariableName)
                {
                    AVariableValue = _configVariable[1];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Сохраняет значение параметра в файле
        /// </summary>
        /// <param name="AFileName"></param>
        /// <param name="AVariableName"></param>
        /// <param name="AVariableValue"></param>
        public static void SetVariable(string AFileName, string AVariableName, string AVariableValue)
        {
            if (File.Exists(AFileName))
            {
                string[] sa = File.ReadAllLines(AFileName,Encoding.GetEncoding(1251));
                for (int i = 0; i < sa.Length; i++)
                {
                    string[] _configVariable = sa[i].Split((new char[] { '=' }), 2);
                    if (_configVariable[0] == AVariableName)
                    {
                        _configVariable[1] = AVariableValue;
                        sa[i] = _configVariable[0] + "=" + _configVariable[1];
                        lock ("ConfigFile") File.WriteAllLines(AFileName, sa, Encoding.GetEncoding(1251));
                        return;
                    }
                }
            }
            else if (!String.IsNullOrEmpty(Path.GetDirectoryName(AFileName)))
            {
                if (!Directory.Exists(Path.GetDirectoryName(AFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(AFileName));
            }
            StreamWriter sw = new StreamWriter(AFileName, true, Encoding.GetEncoding(1251));
            lock ("ConfigFile") sw.WriteLine(AVariableName + "=" + AVariableValue);
            sw.Close();
            return;
        }

        /// <summary>
        /// Удаляет параметр из файла конфигурации
        /// </summary>
        /// <param name="AFileName"></param>
        /// <param name="AVariableName"></param>
        /// <returns></returns>
        public static bool RemoveVariable(string AFileName, string AVariableName)
        {
            if (File.Exists(AFileName))
            {
                string[] sa = File.ReadAllLines(AFileName);
                for (int i = 0; i < sa.Length; i++)
                {
                    string[] _configVariable = sa[i].Split((new char[] { '=' }), 2);
                    if (_configVariable[0] == AVariableName)
                    {
                        string[] _newsa = new string[sa.Length - 1];
                        for (int j = 0; j < i; j++) _newsa[j] = sa[j];
                        for (int j = i + 1; j < sa.Length; j++) _newsa[j - 1] = sa[j];
                        lock ("ConfigFile") File.WriteAllLines(AFileName, _newsa);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Класс - вариант настройки
    /// </summary>
    [Serializable]
    public class SettingsCase
    {
        private string settingsCaseName;
        /// <summary>
        /// Наименование варианта настройки
        /// </summary>
        public string SettingsCaseName
        {
            get { return settingsCaseName; }
            set { settingsCaseName = value; }
        }


        private string firebirdStringConnection;
        /// <summary>
        /// Строка подключения к БД Firebird
        /// </summary>
        public string FirebirdStringConnection
        {
            get { return firebirdStringConnection; }
            set { firebirdStringConnection = value; }
        }

        private string sourceDBFFilePath;
        /// <summary>
        /// Исходные файлы ?????????????????????????????
        /// </summary>
        public string SourceDBFFilePath
        {
            get { return sourceDBFFilePath; }
            set { sourceDBFFilePath = value; }
        }

        private string destDBFFilePath;
        /// <summary>
        /// Путь к DBF-файлам, для которых будет генерироваться обертка ????????????????????????????????????????
        /// </summary>
        public string DestDBFFilePath
        {
            get { return destDBFFilePath; }
            set { destDBFFilePath = value; }
        }

        private string _destMySqlConnectionString;
        /// <summary>
        /// Строка подключения к MySQL базе данных
        /// </summary>
        public string DestMySqlConnectionString
        {
            get { return _destMySqlConnectionString; }
            set { _destMySqlConnectionString = value; }
        }

        private string patternsPath;
        /// <summary>
        /// Путь к файлам-шаблонам
        /// </summary>
        public string PatternsPath
        {
            get { return patternsPath; }
            set { patternsPath = value; }
        }

        private string convertPath;
        /// <summary>
        /// Путь к файлам-шаблонам
        /// </summary>
        public string ConvertPath
        {
            get { return convertPath; }
            set { convertPath = value; }
        }

        private string coverFileBodyPattern;
        /// <summary>
        /// Шаблон для содержимого .cs файлов (%s - содержимое класса) ,
        /// </summary>
        public string CoverFileBodyPattern
        {
            get { return coverFileBodyPattern; }
            set { coverFileBodyPattern = value; }
        }

        private string generatedFilePath;
        /// <summary>
        /// Путь, по которому склыдваются сгенерированные файлы
        /// </summary>
        public string GeneratedFilePath
        {
            get { return generatedFilePath; }
            set { generatedFilePath = value; }
        }

        public override string ToString()
        {
            return SettingsCaseName;
        }
    }
}
