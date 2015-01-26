using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class TableNotFoundError: ErrorClass
    {
        private Type recordType;

        public TableNotFoundError(Type RecordType)
        {
            this.ErrorName = "На диске не найден файл " + TableManager.GetTableName(RecordType) + ".DBF";
            this.IsTerminating = true;
            recordType = RecordType;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            CreateTableCorrectionCase ctcc = new CreateTableCorrectionCase(recordType);
            ctcc.ParentError = this;
            CorrectionCases.Add(ctcc);
        }
    }
}
