using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class WrongFileError: ErrorClass
    {
        public WrongFileError(Type RecordType)
        {
            this.ErrorName = "Файл " + TableManager.GetTableName(RecordType) + ".DBF не является файлом DBF";
            this.IsTerminating = true;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
        }
    }
}
