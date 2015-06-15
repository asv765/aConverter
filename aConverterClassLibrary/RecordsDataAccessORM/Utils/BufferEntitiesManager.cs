using System;
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
        public static void CreateAllEntities()
        {
            DropEntity("ADDDCHAR");
            CreateEntity("ADDDCHAR");
            DropEntity("ABONENT");
            CreateEntity("ABONENT");
            DropEntity("CHARS");
            CreateEntity("CHARS");
            DropEntity("CHARVALS");
            CreateEntity("CHARVALS");
            DropEntity("CITIZENS");
            CreateEntity("CITIZENS");
            DropEntity("CNTRSIND");
            CreateEntity("CNTRSIND");
            DropEntity("COUNTERS");
            CreateEntity("COUNTERS");
            DropEntity("DOGOVOR");
            CreateEntity("DOGOVOR");
            DropEntity("EQUIPMENT");
            CreateEntity("EQUIPMENT");
            DropEntity("GCOUNTER");
            CreateEntity("GCOUNTER");
            DropEntity("HADDCHAR");
            CreateEntity("HADDCHAR");
            DropEntity("LCHARS");
            CreateEntity("LCHARS");
            DropEntity("NACH");
            CreateEntity("NACH");
            DropEntity("NACHOPL");
            CreateEntity("NACHOPL");
            DropEntity("OPLATA");
            CreateEntity("OPLATA");
            DropEntity("PENI");
            CreateEntity("PENI");
            DropEntity("SUPPLNET");
            CreateEntity("SUPPLNET");
        }

        /// <summary>
        /// Создает сущность и все с ней связанное, используя скрипт из ресурсов
        /// </summary>
        /// <param name="entityName"></param>
        public static void CreateEntity(string entityName)
        {
            using (FbConnection fc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
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
            using (FbConnection fc = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
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
            FbScript fs = new FbScript(script);
            fs.Parse();
            foreach (var result in fs.Results)
            {
                FbCommand fbc = new FbCommand(result, connection);
                fbc.ExecuteNonQuery();
            }
        }
    }
}
