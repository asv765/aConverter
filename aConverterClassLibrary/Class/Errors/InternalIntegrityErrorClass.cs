using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class InternalIntegrityErrorClass: ErrorClass
    {
        private string primaryTable;
        private string primaryKey;
        private string foreignTable;
        private string foreignKey;

        public InternalIntegrityErrorClass(string PrimaryTable, string PrimaryKey, 
            string ForeighnTable, string ForeignKey)
        {
            this.ErrorName = String.Format("{0}.{1} не расшифровывается в {2}.{3}",
                PrimaryTable, PrimaryKey, ForeighnTable, ForeignKey);
            
            this.primaryTable = PrimaryTable;
            this.primaryKey = PrimaryKey;
            this.foreignTable = ForeighnTable;
            this.foreignKey = ForeignKey;

            //Statistic ss = new DbfStatistic(String.Format("Записи таблицы {0}, поле {1} которой не расшифровывается в {2}.{3}", PrimaryTable, PrimaryKey, ForeighnTable, ForeignKey),
            //    String.Format("SELECT * FROM {0} WHERE {0}.{1} NOT IN (SELECT {2}.{3} FROM {2})", PrimaryTable, PrimaryKey, ForeighnTable, ForeignKey),
            //    null);
            Statistic ss = new MySQLStatistic(String.Format("Записи таблицы {0}, поле {1} которой не расшифровывается в {2}.{3}", PrimaryTable, PrimaryKey, ForeighnTable, ForeignKey),
                            String.Format("SELECT * FROM {0} WHERE {0}.{1} NOT IN (SELECT {2}.{3} FROM {2})", PrimaryTable, PrimaryKey, ForeighnTable, ForeignKey),
                            null);
            
            StatisticSets.Add(ss);

            this.IsTerminating = false;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            DeleteUnexpectedForeighnKeysCorrectionClass dufkcc = new DeleteUnexpectedForeighnKeysCorrectionClass(foreignTable, foreignKey, primaryTable, primaryKey);
            dufkcc.ParentError = this;
            CorrectionCases.Add(dufkcc);
        }
    }
}
