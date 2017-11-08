using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace aConverterClassLibrary
{
    public class aConverter_RootSettings
    {
        public static string IBScriptPath
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].IbescriptPath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].IbescriptPath = value;
                WriteSettingsCase(lsc);
            }
        }

        public static int SettingsCaseId
        {
            get { return GetValue<int>(aConverter_RootSettings.SettingsFileName, "SettingsCaseId", -1); }// ??????????????????????????
            set { SetValue(aConverter_RootSettings.SettingsFileName, "SettingsCaseId", value); }
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

        public static string FirebirdDatabasePath
        {
            get
            {
                if (!String.IsNullOrEmpty(aConverter_RootSettings.FirebirdStringConnection))
                {
                    var m = Regex.Match(aConverter_RootSettings.FirebirdStringConnection, @"(?<=Database=).*?(?=;)");
                    if (m.Success)
                        return m.Value;
                }
                return "";
            }
        }

        /// <summary>
        /// Путь к DBF-файлам, для которых будет генерироваться обертка
        /// </summary>
        public static string SourceDbfFilePath
        {
            get
            {
                if (SettingsCaseId == -1) return "";
                List<SettingsCase> lsc = ReadSettingsCase();
                string rv = lsc[SettingsCaseId].SourceDbfFilePath;
                return rv;
            }
            set
            {
                if (SettingsCaseId == -1) return;
                List<SettingsCase> lsc = ReadSettingsCase();
                lsc[SettingsCaseId].SourceDbfFilePath = value;
                WriteSettingsCase(lsc);
            }
        }

        ///// <summary>
        ///// Путь к DBF-файлам для импорта ?????????????????????????????????????????
        ///// </summary>
        //public static string DestDBFFilePath
        //{
        //    get
        //    {
        //        // return @"E:\Misc\Разработка\aConverter_Data\006_Prohladny\Source\120615\Orig";
        //        if (SettingsCaseId == -1) return "";
        //        List<SettingsCase> lsc = ReadSettingsCase();
        //        string rv = lsc[SettingsCaseId].DestDBFFilePath;
        //        return rv;
        //    }
        //    set
        //    {
        //        if (SettingsCaseId == -1) return;
        //        List<SettingsCase> lsc = ReadSettingsCase();
        //        lsc[SettingsCaseId].DestDBFFilePath = value;
        //        WriteSettingsCase(lsc);
        //    }
        //}


        ///// <summary>
        ///// Наименование промежуточной MySQL базы данных для конвертации
        ///// </summary>
        //public static string DestMySqlConnectionString
        //{
        //    get
        //    {
        //        if (SettingsCaseId == -1) return "";
        //        List<SettingsCase> lsc = ReadSettingsCase();
        //        string rv = lsc[SettingsCaseId].DestMySqlConnectionString;
        //        return rv;
        //    }
        //    set
        //    {
        //        if (SettingsCaseId == -1) return;
        //        List<SettingsCase> lsc = ReadSettingsCase();
        //        lsc[SettingsCaseId].DestMySqlConnectionString = value;
        //        WriteSettingsCase(lsc);
        //    }
        //}

        //public static string DestMySqlDatabaseName
        //{
        //    get
        //    {
        //        string cs = DestMySqlConnectionString;
        //        Match m = Regex.Match(cs, @"(?<=database=').*(?=')");
        //        return m.Success ? m.Value : "";
        //    }
        //}

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
                if (SettingsCaseId == -1) return "";
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

        //public static string DBFConnectionString
        //{
        //    get
        //    {
        //        string rv = String.Format(TableManager.VFPOLEDBConnectionString, aConverter_RootSettings.DestDBFFilePath);
        //        return rv;
        //    }
        //}

        /// <summary>
        /// Шаблон для имени .cs файлов (%f - имя (без расширения) исходного DBF-файла)
        /// </summary>
        public static string CoverFileNamePattern
        {
            get { return GetValue<string>(aConverter_RootSettings.SettingsFileName, "CoverFileNamePattern", ""); }
            set { SetValue(aConverter_RootSettings.SettingsFileName, "CoverFileNamePattern", value); }
        }

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
            var sr = new StreamReader("Statistics.xml", Encoding.GetEncoding(1251));
            var fmt = new XmlSerializer(typeof(List<Statistic>));
            var lss = (List<Statistic>)fmt.Deserialize(sr);
            sr.Close();
            return lss;
        }

        /// <summary>
        /// Сохраняет список статистик на диск
        /// </summary>
        /// <param name="statistics"></param>
        public static void WriteStatistics(List<Statistic> statistics)
        {
            var sw = new StreamWriter("Statistics.xml", false, Encoding.GetEncoding(1251));
            var fmt = new XmlSerializer(statistics.GetType());
            fmt.Serialize(sw, statistics);
            sw.Close();
        }

        /// <summary>
        /// Читает список вариантов настройки с диска
        /// </summary>
        /// <returns></returns>
        public static List<SettingsCase> ReadSettingsCase()
        {
            var lsc = new List<SettingsCase>();

            if (File.Exists("SettingsCase.xml"))
            {
                var sr = new StreamReader("SettingsCase.xml", Encoding.GetEncoding(1251));
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
            foreach (SettingsCase sc in lsc)
            {
                if (!String.IsNullOrEmpty(sc.CoverFileBodyPattern))
                    sc.CoverFileBodyPattern = sc.CoverFileBodyPattern.Replace("\n", "\r\n");
            }
            return lsc;
        }

        /// <summary>
        /// Сохраняет список статистик на диск
        /// </summary>
        /// <param name="settingsCase"></param>
        public static void WriteSettingsCase(List<SettingsCase> settingsCase)
        {
            var sw = new StreamWriter("SettingsCase.xml", false, Encoding.GetEncoding(1251));
            var fmt = new XmlSerializer(settingsCase.GetType());
            fmt.Serialize(sw, settingsCase);
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
        protected static T GetValue<T>(string aFileName, string aVariableName, T defaultValue)
        {
            string variableValue;
            if (ConfigFile.ReadVariable(SettingsFileName,
                    aVariableName, out variableValue))
                return ParseString<T>(variableValue);
            return defaultValue;
        }

        /// <summary>
        /// Сохраняет значение параметра AVariableName в файле конфигурации.
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aVariableName"></param>
        /// <param name="aValue"></param>
        /// <returns></returns>
        protected static void SetValue(string aFileName, string aVariableName, object aValue)
        {

            ConfigFile.SetVariable(SettingsFileName,
                aVariableName, aValue.ToString().Replace("\r\n", "\\r\\n"));
        }

        /// <summary>
        /// Преобразует строку AValue к заданному типу
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aValue"></param>
        /// <returns></returns>
        public static T ParseString<T>(string aValue)
        {
            Type genericType = typeof(T);
            if (genericType == typeof(String))
            {
                ConstructorInfo constructor = genericType.GetConstructor(new[] { typeof(char[]) });
                return (T)constructor.Invoke(new object[] { aValue.Replace("\\r\\n", "\r\n").ToCharArray() });
            }
            T result;
            MethodInfo parseMethod = genericType.GetMethod("Parse", new[] { typeof(string), typeof(CultureInfo) });
            if (parseMethod != null)
            {
                try
                {
                    var culture = new CultureInfo("ru-RU");
                    try
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ",";
                        result = (T)parseMethod.Invoke(null, new object[] { aValue, culture });
                    }
                    catch
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ".";
                        result = (T)parseMethod.Invoke(null, new object[] { aValue, culture });
                    }
                }
                catch
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    try
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ",";
                        result = (T)parseMethod.Invoke(null, new object[] { aValue, culture });
                    }
                    catch
                    {
                        culture.NumberFormat.NumberDecimalSeparator = ".";
                        result = (T)parseMethod.Invoke(null, new object[] { aValue, culture });
                    }
                }
            }
            else
            {
                parseMethod = genericType.GetMethod("Parse", new[] { typeof(string) });
                result = (T)parseMethod.Invoke(null, new object[] { aValue });
            }
            return result;
        }
    }

    public static class ConfigFile
    {
        /// <summary>
        /// Читает значение параметра из файла конфигурации
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aVariableName"></param>
        /// <param name="aVariableValue"></param>
        /// <returns></returns>
        public static bool ReadVariable(string aFileName, string aVariableName, out string aVariableValue)
        {
            aVariableValue = "";
            if (!File.Exists(aFileName)) return false;
            string[] sa = File.ReadAllLines(aFileName, Encoding.GetEncoding(1251));
            // string[] sa = File.ReadAllLines(AFileName);
            for (int i = 0; i < sa.Length; i++)
            {
                string[] configVariable = sa[i].Split((new[] { '=' }), 2);
                if (configVariable[0] == aVariableName)
                {
                    aVariableValue = configVariable[1];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Сохраняет значение параметра в файле
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aVariableName"></param>
        /// <param name="aVariableValue"></param>
        public static void SetVariable(string aFileName, string aVariableName, string aVariableValue)
        {
            if (File.Exists(aFileName))
            {
                string[] sa = File.ReadAllLines(aFileName, Encoding.GetEncoding(1251));
                for (int i = 0; i < sa.Length; i++)
                {
                    string[] configVariable = sa[i].Split((new[] { '=' }), 2);
                    if (configVariable[0] == aVariableName)
                    {
                        configVariable[1] = aVariableValue;
                        sa[i] = configVariable[0] + "=" + configVariable[1];
                        lock ("ConfigFile") File.WriteAllLines(aFileName, sa, Encoding.GetEncoding(1251));
                        return;
                    }
                }
            }
            else if (!String.IsNullOrEmpty(Path.GetDirectoryName(aFileName)))
            {
                if (!Directory.Exists(Path.GetDirectoryName(aFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(aFileName));
            }
            var sw = new StreamWriter(aFileName, true, Encoding.GetEncoding(1251));
            lock ("ConfigFile") sw.WriteLine(aVariableName + "=" + aVariableValue);
            sw.Close();
            return;
        }

        /// <summary>
        /// Удаляет параметр из файла конфигурации
        /// </summary>
        /// <param name="aFileName"></param>
        /// <param name="aVariableName"></param>
        /// <returns></returns>
        public static bool RemoveVariable(string aFileName, string aVariableName)
        {
            if (File.Exists(aFileName))
            {
                string[] sa = File.ReadAllLines(aFileName);
                for (int i = 0; i < sa.Length; i++)
                {
                    string[] configVariable = sa[i].Split((new char[] { '=' }), 2);
                    if (configVariable[0] == aVariableName)
                    {
                        var newsa = new string[sa.Length - 1];
                        for (int j = 0; j < i; j++) newsa[j] = sa[j];
                        for (int j = i + 1; j < sa.Length; j++) newsa[j - 1] = sa[j];
                        lock ("ConfigFile") File.WriteAllLines(aFileName, newsa);
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
        private string _settingsCaseName;
        /// <summary>
        /// Наименование варианта настройки
        /// </summary>
        public string SettingsCaseName
        {
            get { return _settingsCaseName; }
            set { _settingsCaseName = value; }
        }


        private string _firebirdStringConnection;
        /// <summary>
        /// Строка подключения к БД Firebird
        /// </summary>
        public string FirebirdStringConnection
        {
            get { return _firebirdStringConnection; }
            set { _firebirdStringConnection = value; }
        }

        private string _sourceDbfFilePath;
        /// <summary>
        /// Исходные файлы ?????????????????????????????
        /// </summary>
        public string SourceDbfFilePath
        {
            get { return _sourceDbfFilePath; }
            set { _sourceDbfFilePath = value; }
        }

        //private string destDBFFilePath;
        ///// <summary>
        ///// Путь к DBF-файлам, для которых будет генерироваться обертка ????????????????????????????????????????
        ///// </summary>
        //public string DestDBFFilePath
        //{
        //    get { return destDBFFilePath; }
        //    set { destDBFFilePath = value; }
        //}

        //private string _destMySqlConnectionString;
        ///// <summary>
        ///// Строка подключения к MySQL базе данных
        ///// </summary>
        //public string DestMySqlConnectionString
        //{
        //    get { return _destMySqlConnectionString; }
        //    set { _destMySqlConnectionString = value; }
        //}

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

        private string ibescriptPath;
        /// <summary>
        /// Путь к IBEScript
        /// </summary>
        public string IbescriptPath
        {
            get { return ibescriptPath; }
            set { ibescriptPath = value; }
        }

        public override string ToString()
        {
            return SettingsCaseName;
        }
    }
}