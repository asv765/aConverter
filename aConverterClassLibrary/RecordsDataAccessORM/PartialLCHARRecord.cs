using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_LCHAR : ISQLInsertable
    {
        public string InsertSQL
        {
            get
            {
                // INSERT INTO CNV$LCHARS(ID, LSHET, LCHARCD, LCHARNAME, VALUE_, VALUEDESC, DATE_) 
                // VALUES(NULL, NULL, NULL, NULL, NULL, NULL, NULL);

                var insertCharTemplate =
                    "INSERT INTO CNV$LCHARS (ID, LSHET, LCHARCD, LCHARNAME, VALUE_, VALUEDESC, DATE_) " +
                    "VALUES(NULL, '{0}', {1}, '{2}', {3}, '{4}', '{5}');";

                var sql = String.Format(insertCharTemplate,
                    LSHET, LCHARCD, LCHARNAME, VALUE_.ToString().Replace(',', '.'),
                    VALUEDESC, DATE_.Value.ToString("d.MM.yyyy HH:mm:ss"));
                return sql;
            }
        }
    }
}
