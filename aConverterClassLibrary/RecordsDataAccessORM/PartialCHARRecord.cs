using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CHAR: ISQLInsertable
    {
        public string InsertSQL
        {
            get
            {
                // INSERT INTO CNV$CHARS(ID, LSHET, CHARCD, CHARNAME, VALUE_, DATE_)
                //     VALUES(1, '6201000001', 3, 'Количество комнат', 3, '1-JAN-2017 00:00:00');

                var insertCharTemplate =
                    "INSERT INTO CNV$CHARS (ID, LSHET, CHARCD, CHARNAME, VALUE_, DATE_) " +
                    "VALUES(NULL, '{0}', {1}, '{2}', {3}, '{4}');";

                var sql = String.Format(insertCharTemplate,
                    LSHET, CHARCD, CHARNAME, VALUE_.ToString().Replace(',','.'),
                    DATE_.Value.ToString("d.MM.yyyy HH:mm:ss"));
                return sql;
            }
        }
    }
}
