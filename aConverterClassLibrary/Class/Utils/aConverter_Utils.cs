using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace aConverterClassLibrary
{
    public class Utils
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

        /// <summary>
        /// Читает данные из Excel-листа
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="scheetName"></param>
        /// <returns></returns>
        public static DataTable ReadExcelFile(string fileName, string scheetName)
        {
            string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", fileName);
            using (OleDbConnection odconn = new OleDbConnection(connectionString))
            {
                odconn.Open();
                var _dt = odconn.GetSchema("Tables");
                string s = "";
                foreach (DataRow dr in _dt.Rows)
                {
                    s += dr[2] + "\r\n";
                }
                using (OleDbCommand odc = new OleDbCommand(String.Format("select * from `{0}$`", scheetName.Replace(".","#")) , odconn))
                {
                    OleDbDataAdapter odr = new OleDbDataAdapter(odc);
                    DataTable dt = new DataTable();
                    odr.Fill(dt);
                    return dt;
                }
            }
        }

        public static string[] SplitFio(string fio)
        {
            string[] fioa = new string[3];
            fioa[0] = Regex.Match(fio, @"^\w*").Value;
            fioa[1] = Regex.Match(fio, @"(?<=^\w+\s+)\w*").Value;
            fioa[2] = Regex.Match(fio, @"(?<=^\w+\s+\w+\s+)\w*").Value;
            return fioa;
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
