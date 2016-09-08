using System;
using System.Collections.Generic;
using System.Linq;
using aConverterClassLibrary.Class;
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
        public static void DropAllEntities()
        {
            var l = GetAllEntities();
            foreach (var s in l) DropEntity(s);
        }

        public static void CreateAllEntities()
        {
            var l = GetAllEntities();
            foreach (var s in l) CreateDatabaseObject(s);
        }

        public static List<string> GetAllEntities()
        {
            var l = new List<string>
            {
                "AADDCHAR",
                "ABONENT",
                "CHARLST",
                "CHARS",
                "CHARVALS",
                "CITIZENMIGRATION",
                "CITIZENS",
                "CITYZENLGOTA",
                "LGOTSUMMA",
                "CNTRSIND",
                "COUNTERS",
                "DOGOVOR",
                "EQUIPMENT",
                "GCOUNTER",
                "CHARSHOUSES",
                "HADDCHAR",
                "LCHARS",
                "NACH",
                "NACHOPL",
                "OPLATA",
                "PENI",
                "SUPPLNET",
                "DOCUMENTNUMERATORTABLE",
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
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            string dropcommand = "DELETE FROM " + tableName;
            fbManager.ExecuteNonQuery(dropcommand);
        }

        /// <summary>
        /// Создает сущность и все с ней связанное, используя скрипт из ресурсов
        /// </summary>
        /// <param name="entityName"></param>
        public static void CreateDatabaseObject(string entityName)
        {
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            string createScript =
                Properties.Resources.ResourceManager.GetString("CNV_" + entityName);
            fbm.ExecuteScript(createScript);
        }

        /// <summary>
        /// Удаляет сущность и все с ней связанное, использую скрипт из ресурсов
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static bool DropEntity(string entityName)
        {
            // Проверяем, существует ли соответствующая сущность
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            DataTable dt = fbm.GetSchema("Tables", new[] { null, null, null, "TABLE" });
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

            fbm.ExecuteScript(dropScript, true);
            return true;
        }

        public static List<string> GetAllProcedures()
        {
            var l = new List<string>();
            l.AddRange(GetAllProcedures(ProcedureType.ПроверкаЦелостности));
            l.AddRange(GetAllProcedures(ProcedureType.Конвертация));
            return l;
        }

        public static List<string> GetAllProcedures(ProcedureType procedureType)
        {
            var l = new List<string>();
            if (procedureType == ProcedureType.Конвертация)
            {
                l.Add("CNV_DOCUMENTNUMERATOR");
                l.Add("CNV_00100_REGIONDISTRICTS");
                l.Add("CNV_00200_PUNKT");
                l.Add("CNV_00300_STREET");
                l.Add("CNV_00400_DISTRICT");
                l.Add("CNV_00500_INFORMATIONOWNERS");
                l.Add("CNV_00600_HOUSES");
                l.Add("CNV_00700_ABONENTS");
                l.Add("CNV_00800_CHARS");
                l.Add("CNV_00850_CHARSHOUSES");
                l.Add("CNV_00900_LCHARS");
                l.Add("CNV_00950_COUNTERSTYPES");
                l.Add("CNV_01000_COUNTERS");
                l.Add("CNV_01300_SOURCEDOC");
                l.Add("CNV_01400_OPLATA");
                l.Add("CNV_01500_SALDO");
                l.Add("CNV_01600_NACHISLIMPORT");
                l.Add("CNV_01700_PERERASHETIMPORT");
                l.Add("CNV_02100_EXTLSHETS");
                l.Add("CNV_03000_CITIZENS_TVER");
                l.Add("CNV_03050_CITIZENSMIGR_TVER");
                l.Add("CNV_03100_TVER_ABONENTDOLYA");
                l.Add("CNV_03200_CITYZENLGOTA_TVER");
                l.Add("CNV_03300_LGOTSUMMA");   
            }
            if (procedureType == ProcedureType.ПроверкаЦелостности)
            {
                var checkCaseList = CheckCaseFactory.GenerateCheckCases();
                l.AddRange(checkCaseList.Where(checkCase => !String.IsNullOrEmpty(checkCase.StoredProcName)).Select(checkCase => checkCase.ShortStoredProcName));
            }

            return l;
        }

        public static bool DropProcedure(string procedureName)
        {
            // Проверяем, существует ли соответствующая сущность
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            DataTable dt = fbm.GetSchema("Procedures");
            bool procedurePresent = false;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow[2].ToString() == "CNV$" + procedureName)
                {
                    procedurePresent = true;
                    break;
                }
            }
            if (!procedurePresent) return false;

            string dropCommand = "DROP PROCEDURE " + "CNV$" + procedureName;
            fbm.ExecuteNonQuery(dropCommand);

            return true;
        }

        public static void DropAllProcedures()
        {
            var l = GetAllProcedures();
            l.Reverse();
            foreach (var s in l) DropProcedure(s);
            var fbm = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            // DropException("WRONG_PARAMATER_VALUE");
        }

        public static void CreateAllProcedures()
        {
            CreateDatabaseObject("ADD_WRONG_PARAMETER_EXCEPTION");
            var l = GetAllProcedures();
            foreach (var s in l) CreateDatabaseObject(s);
        }

    }

    // Также - поле PROCTYPE в CNV$CHECKCASESPDESC
    public enum ProcedureType
    {
        ПроверкаЦелостности,
        Конвертация
    }
}
