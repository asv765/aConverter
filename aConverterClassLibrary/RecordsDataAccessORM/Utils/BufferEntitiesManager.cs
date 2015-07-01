using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System.Data;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class BufferEntitiesManager
    {
        /// <summary>
        /// (Пере)создает все нобходимые для конвертации сущность в целевой Firebird базе данных
        /// </summary>
        public static void ReCreateAllEntities()
        {
            var l = GetAllEntities();
            foreach (var s in l)
            {
                DropEntity(s);
                CreateEntity(s);
            }
        }

        public static List<string> GetAllEntities()
        {
            var l = new List<string>
            {
                "ADDDCHAR",
                "ABONENT",
                "CHARS",
                "CHARVALS",
                "CITIZENS",
                "CNTRSIND",
                "COUNTERS",
                "DOGOVOR",
                "EQUIPMENT",
                "GCOUNTER",
                "HADDCHAR",
                "LCHARS",
                "NACH",
                "NACHOPL",
                "OPLATA",
                "PENI",
                "SUPPLNET",
                "CHECKCASESPDESC"
            };
            return l;
        }

        public static void DropAllData()
        {
            var l = GetAllEntities();
            foreach (var s in l)
            {
                if (s == "CHECKCASESPDESC") continue;
                DropTableData("CNV$" + s);
            }
        }

        public static void DropTableData(string tableName)
        {
            using (var fc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fc.Open();
                string dropcommand = "DELETE FROM " + tableName;
                var fbc = new FbCommand(dropcommand, fc);
                fbc.ExecuteNonQuery();
            }            
        }

        /// <summary>
        /// Создает сущность и все с ней связанное, используя скрипт из ресурсов
        /// </summary>
        /// <param name="entityName"></param>
        public static void CreateEntity(string entityName)
        {
            using (var fc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                string createScript =
                    Properties.Resources.ResourceManager.GetString("CNV_" + entityName);
                fc.Open();
                ExecuteScript(createScript, fc);
                fc.Close();
            }
        }

        /// <summary>
        /// Удаляет сущность и все с ней связанное, использую скрипт из ресурсов
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static bool DropEntity(string entityName)
        {
            // Проверяем, существует ли соответствующая сущность
            using (var fc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                fc.Open();
                DataTable dt = fc.GetSchema("Tables", new[] { null, null, null, "TABLE" });
                bool tablePresent = false;
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow[2].ToString() == "CNV$" + entityName)
                    {
                        tablePresent = true;
                        break;
                    }
                }
                if (!tablePresent) return false;

                string dropScript =
                    Properties.Resources.ResourceManager.GetString("CNV_" + entityName + "_d");

                ExecuteScript(dropScript, fc);
                fc.Close();
            }

            return true;
        }

        /// <summary>
        /// Выполняет скрипт
        /// </summary>
        /// <param name="script"></param>
        /// <param name="connection"></param>
        public static void ExecuteScript(string script, FbConnection connection)
        {
            if (script == null) throw new ArgumentNullException("script");
            if (connection == null) throw new ArgumentNullException("connection");
            var fs = new FbScript(script);
            fs.Parse();
            foreach (var result in fs.Results)
            {
                var fbc = new FbCommand(result, connection);
                fbc.ExecuteNonQuery();
            }
        }
    }
}
