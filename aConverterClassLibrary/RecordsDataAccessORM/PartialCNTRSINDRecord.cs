using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CNTRSIND : ISQLInsertable
    {
        public string InsertSQL
        {
            get
            {
                var insertCharTemplate =
                    "INSERT INTO CNV$CNTRSIND(ID, COUNTERID, DOCUMENTCD, OLDIND, OB_EM, INDICATION, INDDATE, INDTYPE)  " +
                    "VALUES(NULL, {0}, {1}, {2}, {3}, {4}, {5}, {6});";

                var sql = String.Format(insertCharTemplate,
                    RecordUtils.GetSafeStringWithNull(COUNTERID),
                    RecordUtils.GetSafeStringWithNull(DOCUMENTCD),
                    RecordUtils.GetSafeDecimalWithNull(OLDIND),
                    RecordUtils.GetSafeDecimalWithNull(OB_EM),
                    RecordUtils.GetSafeDecimalWithNull(INDICATION),
                    RecordUtils.GetSafeDateTimeWithNull(INDDATE),
                    RecordUtils.GetSafeIntWithNull(INDTYPE));

                return sql;
            }
        }
    }
}
