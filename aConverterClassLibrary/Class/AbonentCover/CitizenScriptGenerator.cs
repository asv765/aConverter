using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace aConverterClassLibrary
{
    public class CitizenScriptGenerator
    {
        #region Генераторы
        private const string citizenIdGeneratorName = "CITYZENS_GEN";
        private int citizenIdGenerator;
        public int GetCurrentCitizenId()
        {
            return citizenIdGenerator;
        }
        public int GetNextCitizenId()
        {
            citizenIdGenerator++;
            return citizenIdGenerator;
        }

        private const string documentIdGeneratorName = "DOCUMENTS_GEN";
        private int documentIdGenerator;
        public int GetCurrentDocumentId()
        {
            return documentIdGenerator;
        }
        public int GetNextDocumentId()
        {
            documentIdGenerator++;
            return documentIdGenerator;
        }

        private const string migrationIdGeneratorName = "CITYZENMIGRATION_G";
        private int migrationIdGenerator;
        public int GetCurrentMigrationId()
        {
            return migrationIdGenerator;
        }
        public int GetNextMigrationId()
        {
            migrationIdGenerator++;
            return migrationIdGenerator;
        }
        #endregion

        private Dictionary<int, Citizen> citizens = new Dictionary<int, Citizen>();
        /// <summary>
        /// Список граждан
        /// </summary>
        public Dictionary<int, Citizen> Citizens
        {
            get { return citizens; }
            set { citizens = value; }
        }

        public CitizenScriptGenerator(int CitizenIdGenerator,
            int DocumentIdGenerator,
            int MigrationIdGenerator)
        {
            citizenIdGenerator = CitizenIdGenerator;
            documentIdGenerator = DocumentIdGenerator;
            migrationIdGenerator = MigrationIdGenerator;
        }

        /// <summary>
        /// Метод обращается к базе Firebird и считывает значение трех генераторов. Не реализовано
        /// </summary>
        /// <param name="ConnectionString"></param>
        public void ReadGeneratorValues(string ConnectionString)
        {
        }

        public void GenerateScript(string path)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(1251));

            //sw.WriteLine("DELETE FROM RELATIVES;");
            //sw.WriteLine("DELETE FROM CITYZENMIGRATION;");
            sw.WriteLine(String.Format("UPDATE CITYZENS SET CITYZEN_ID = CITYZEN_ID - 6770 + {0} + 1;", citizenIdGenerator));
            //sw.WriteLine(String.Format("INSERT INTO RELATIVES (RELATIVE1, RELATIVE2, RELATIONID, OTHERRELATION) VALUES ({0}, {1}, 5, NULL);",
            //    6779 - 6770 + citizenIdGenerator + 1,
            //    6778 - 6770 + citizenIdGenerator + 1));
            //sw.WriteLine(String.Format("INSERT INTO RELATIVES (RELATIVE1, RELATIVE2, RELATIONID, OTHERRELATION) VALUES ({0}, {1}, 1, NULL);",
            //    6789 - 6770 + citizenIdGenerator + 1,
            //    6788 - 6770 + citizenIdGenerator + 1));
            //sw.WriteLine(String.Format("INSERT INTO RELATIVES (RELATIVE1, RELATIVE2, RELATIONID, OTHERRELATION) VALUES ({0}, {1}, 5, NULL);",
            //    6790 - 6770 + citizenIdGenerator + 1,
            //    6788 - 6770 + citizenIdGenerator + 1));
            //sw.WriteLine(String.Format("INSERT INTO CITYZENMIGRATION (MIGRATIONID, DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, MIGRATIONTYPE, DIRECTION, REGDATE) VALUES "+
            //    "(406, NULL, {0}, '19-JAN-2012', NULL, 1, NULL);",
            //    6788 - 6770 + citizenIdGenerator + 1));

            foreach (Citizen c in this.Citizens.Values)
            {
                sw.WriteLine(c.Script);
            }
            // sw.WriteLine(String.Format("ALTER SEQUENCE {0} RESTART WITH {1};", citizenIdGeneratorName, citizenIdGenerator));
            // sw.WriteLine(String.Format("ALTER SEQUENCE {0} RESTART WITH {1};\r\n", documentIdGeneratorName, documentIdGenerator));
            
            sw.WriteLine(String.Format("ALTER SEQUENCE {0} RESTART WITH 0;", citizenIdGeneratorName, citizenIdGenerator));
            sw.WriteLine(String.Format("SELECT FIRST 1 GEN_ID({0}, (SELECT MAX(cityzens.cityzen_id) + 1 FROM cityzens) ) " +
                                        "FROM rdb$generators WHERE rdb$generators.rdb$generator_name = '{0}';", citizenIdGeneratorName));
            sw.WriteLine(String.Format("ALTER SEQUENCE {0} RESTART WITH {1};", migrationIdGeneratorName, migrationIdGenerator));

            sw.Close();
        }
    }
}
