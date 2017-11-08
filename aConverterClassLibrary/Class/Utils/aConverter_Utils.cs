using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using aConverterClassLibrary.Class.Utils;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public static class Utils
    {
        public static string CurrentPath
        {
            get
            {
                Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
                string returnValue = Path.GetDirectoryName(CurrentAssembly.CodeBase) + "\\";
                if (returnValue.Substring(0, 6) == "file:\\")
                {
                    returnValue = returnValue.Remove(0, 6);
                }
                return returnValue;
            }
        }

        public static decimal SafeDecimal(decimal AValue)
        {
            return AValue;
        }

        public static decimal SafeDecimal(decimal? AValue)
        {
            return AValue == null ? 0 : (decimal)AValue;
        }

        public static int SafeInt(int? AValue)
        {
            return AValue == null ? 0 : (int)AValue;
        }

        #region Определяет и возвращает версию текущей сборки...
        /// <summary>
        /// Возвращает версию платежного терминала...
        /// </summary>
        /// <param name="fileName">Имя файла с полным путем</param>
        /// <returns></returns>
        public static Version GetVersion(string fileName)
        {
            // Dont Compile!
            // Вобщем надо разбираться с GAC. Он просто грузит сборку из кэша,
            // так как уникальный идентификатор у них совпадает.

            // Если файла не существует, то его версия принимается равной 0.0.0.0
            Version returnVersion = new Version("0.0.0.0");
            if (File.Exists(fileName))
            {
                Assembly assembly = null;
                
                //AppDomain tempDomain = AppDomain.CreateDomain("temporaryDomain"); 
                byte[] rawAssembly = File.ReadAllBytes(fileName);
                //assembly = tempDomain.Load(rawAssembly); 
                assembly = Assembly.Load(rawAssembly);
                returnVersion = (Version)assembly.GetName().Version.Clone();

            }
            return returnVersion;
        }
        /// <summary>
        /// Определяет и возвращает версию текущей сборки...
        /// </summary>
        /// <returns></returns>
        public static Version GetVersion()
        {
            // Если файла не существует, то его версия принимается равной 0.0.0.0
            Version returnVersion = new Version("0.0.0.0");
            returnVersion = (Version)Assembly.GetExecutingAssembly().GetName().Version.Clone();
            return returnVersion;
        }
        #endregion

        public static void IncreaseMonthYear(ref int Month, ref int Year)
        {
            if (Month == 12)
            {
                Year = Year + 1;
                Month = 1;
            }
            else
            {
                Month = Month + 1;
            }
        }

        public static void DecreaseMonthYear(ref int Month, ref int Year)
        {
            if (Month == 1)
            {
                Month = 12;
                Year = Year - 1;
            }
            else
            {
                Month = Month - 1;
            }
        }

        private static void ConnectToExcel(string fileName, string sheetName, out OleDbConnection odconn, out OleDbCommand cmd)
        {
            string excelProvider = GetExcelProvider();
            if (excelProvider == null)
                throw new Exception($"Не установлен ни один из поставщиков Excel\r\n{String.Join("\r\n", ExcelProviders)}");
            string connectionString = String.Format("Provider={1};Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", fileName, excelProvider);
            odconn = null;
            cmd = null;
            try
            {
                odconn = new OleDbConnection(connectionString);
                odconn.Open();
                if (sheetName == null) sheetName = odconn.GetSchema("Tables").Rows[0][2].ToString().Replace("$", "");
                cmd = new OleDbCommand(String.Format("select * from `{0}$`", sheetName.Replace(".", "#")), odconn);
            }
            catch
            {
                cmd?.Dispose();
                odconn?.Dispose();
            }
        }

        /// <summary>
        /// Читает данные из Excel-листа
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="sheetName">Имя листа (если передать null, возмется первый лист)</param>
        /// <returns></returns>
        public static DataTable ReadExcelFile(string fileName, string sheetName)
        {
            OleDbConnection connection;
            OleDbCommand cmd;
            ConnectToExcel(fileName, sheetName, out connection, out cmd);
            using (connection)
            using (cmd)
            {
                var odr = new OleDbDataAdapter(cmd);
                var dt = new DataTable();
                odr.Fill(dt);
                return dt;
            }
        }

        public static void ReadExcelFileByRow(string fileName, string sheetName, Action<DataRow> drAction)
        {
            OleDbConnection connection;
            OleDbCommand cmd;
            ConnectToExcel(fileName, sheetName, out connection, out cmd);
            using (connection)
            using (cmd)
            using (var reader = cmd.ExecuteReader())
            using (var readerToDataRow = new ReaderToDataRow(reader))
            {
                while (reader.Read())
                {
                    drAction(readerToDataRow.GetDataRow(reader));
                }
            }
        }

        private static readonly string[] ExcelProviders =
        {
            "Microsoft.ACE.OLEDB.16.0",
            "Microsoft.ACE.OLEDB.15.0",
            "Microsoft.ACE.OLEDB.12.0",
            "Microsoft.Jet.OLEDB.4.0",
        };

        public static string GetExcelProvider()
        {
            string[] installedProviders;
            using (var dt = new OleDbEnumerator().GetElements())
            {
                installedProviders = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    installedProviders[i] = dt.Rows[i]["SOURCES_NAME"].ToString().Trim().ToLower();
                }
            }

            string resultProvider = null;
            foreach (var excelProvider in ExcelProviders)
            {
                if (installedProviders.Contains(excelProvider.Trim().ToLower()))
                {
                    resultProvider = excelProvider;
                    break;
                }
            }
            return resultProvider;
        }

        public static string[] SplitFio(string fio)
        {
            string[] fioa = new string[3];
            fioa[0] = Regex.Match(fio, @"\w+").Value;
            fioa[1] = Regex.Match(fio, @"(?<=\w+\W+)\w+").Value;
            fioa[2] = Regex.Match(fio, @"(?<=\w+\W+\w+\W+)\w+").Value;
            return fioa;
        }

        public static string GetDescription<TEnum>(this TEnum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);
            return descriptionAttribute.Length > 0 ? descriptionAttribute[0].Description : enumValue.ToString();
        }

        public static string GetMD5Hash(string input)
        {
            if (input == null) return null;

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static void ExecuteQueryByRow(this TableManager tmsource, string sql, Action<DataRow> drAction)
        {
            using (var reader = tmsource.ExecuteQueryToReader(sql))
            using (var readerToDataRow = new ReaderToDataRow(reader))
            {
                while (reader.Read())
                {
                    drAction(readerToDataRow.GetDataRow(reader));
                }
            }
        }

        public static OleDbCommand CreateCommand(this OleDbConnection connection, string query)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }
    }

    public class ParseAddress
    {
        protected string postIndex = "";
        public string PostIndex
        {
            get { return postIndex; }
        }

        protected string townName = "";
        public string TownName
        {
            get { return townName; }
        }

        protected string rayonName = "";
        public string RayonName
        {
            get { return rayonName; }
        }

        protected string distName = "";
        public string DistName
        {
            get { return distName; }
        }

        protected string ulicaName = "";
        public string UlicaName
        {
            get { return ulicaName; }
        }

        protected string nDoma = "";
        public string NDoma
        {
            get { return nDoma; }
        }

        protected string korpus = "";
        public string Korpus
        {
            get { return korpus; }
        }

        protected string kvartira = "";
        public string Kvartira
        {
            get { return kvartira; }
        }

        protected string komnata = "";
        public string Komnata
        {
            get { return komnata; }
        }

        public string RegexPostIndex = "";
        public string RegexTownName = "";
        public string RegexRayonName = "";
        public string RegexDistName = "";
        public string RegexUlicaName = "";
        public string RegexNDoma = "";
        public string RegexKorpus = "";
        public string RegexKvartira = "";
        public string RegexKomnata = "";

        protected bool suspendPostIndex = false;
        public bool SuspendPostIndex
        {
            get { return suspendPostIndex; }
        }

        protected bool suspendTownName = false;
        public bool SuspendTownName
        {
            get { return suspendTownName; }
        }

        protected bool suspendRayonName = false;
        public bool SuspendRayonName
        {
            get { return suspendRayonName; }
        }

        protected bool suspendDistName = false;
        public bool SuspendDistName
        {
            get { return suspendRayonName; }
        }

        protected bool suspendUlicaName = false;
        public bool SuspendUlicaName
        {
            get { return suspendUlicaName; }
        }

        protected bool suspendNDoma = false;
        public bool SuspendNDoma
        {
            get { return suspendNDoma; }
        }

        protected bool suspendKorpus = false;
        public bool SuspendKorpus
        {
            get { return suspendKorpus; }
        }

        protected bool suspendKvartira = false;
        public bool SuspendKvartira
        {
            get { return suspendKvartira; }
        }

        protected bool suspendKomnata = false;
        public bool SuspendKomnata
        {
            get { return suspendKomnata; }
        }

        private void clearData()
        {
            postIndex = "";
            townName = "";
            rayonName = "";
            distName = "";
            ulicaName = "";
            nDoma = "";
            korpus = "";
            kvartira = "";
            komnata = "";
        }


        public virtual void Parse(string adr)
        {
            clearData();
            regexValue(adr, RegexPostIndex, out suspendPostIndex, out postIndex);
            regexValue(adr, RegexTownName, out suspendTownName, out townName);
            regexValue(adr, RegexRayonName, out suspendRayonName, out rayonName);
            regexValue(adr, RegexDistName, out suspendDistName, out distName);
            regexValue(adr, RegexUlicaName, out suspendUlicaName, out ulicaName);
            regexValue(adr, RegexNDoma, out suspendNDoma, out nDoma);
            regexValue(adr, RegexKorpus, out suspendKorpus, out korpus);
            regexValue(adr, RegexKvartira, out suspendKvartira, out kvartira);
            regexValue(adr, RegexKomnata, out suspendKomnata, out komnata);
        }

        private void regexValue(string adr, string regexExpression, out bool suspend, out string value)
        {
            value = "";
            suspend = false;

            if (!String.IsNullOrEmpty(regexExpression))
            {
                Match m = Regex.Match(adr, regexExpression);
                if (m.Success)
                {
                    value = m.Value;
                    suspend = true;
                }
            }
        }
    }
}
