using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class ExternalIntegrityErrorClass: ErrorClass
    {
        private string dbfTable;
        private string dbfKey;
        private string fdbTable;
        private string fdbKey;

        public ExternalIntegrityErrorClass(string DbfTable, string DbfKey, 
            string FdbTable, string FdbKey)
        {
            this.ErrorName = String.Format("{0}.{1} не расшифровывается в {2}.{3}",
                DbfTable, DbfKey, FdbTable, FdbKey);
            
            this.dbfTable = DbfTable;
            this.dbfKey = DbfKey;
            this.fdbTable = FdbTable;
            this.fdbKey = FdbKey;

            this.IsTerminating = false;
        }

        public override void GenerateCorrectionCases()
        {
        }
    }
}
