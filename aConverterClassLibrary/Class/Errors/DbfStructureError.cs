using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class DbfStructureError: ErrorClass
    {
        private Type recordType;
        List<DbfField> modifiedField;
        List<DbfField> addedField;

        public DbfStructureError(Type RecordType, List<DbfField> AModifiedField, List<DbfField> AAddedField)
        {
            this.ErrorName = "Структура файла " + TableManager.GetTableName(RecordType) + ".DBF отличается от типовой";
            this.IsTerminating = true;
            recordType = RecordType;
            modifiedField = AModifiedField;
            addedField = AAddedField;

            Statistic ss = new DbfStatistic("Файл " + TableManager.GetTableName(RecordType) + ".DBF",
                "select * from " + TableManager.GetTableName(RecordType),
                null);
            StatisticSets.Add(ss);
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            CorrectStructureCorrectionCase cscc = new CorrectStructureCorrectionCase(recordType, modifiedField, addedField);
            cscc.ParentError = this;
            CorrectionCases.Add(cscc);
        }
    }
}
