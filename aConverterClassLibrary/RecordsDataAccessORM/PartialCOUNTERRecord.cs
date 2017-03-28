using aConverterClassLibrary.RecordsDataAccessORM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTER: ISQLInsertable
    {
        public string InsertSQL
        {
            get
            {
                // INSERT INTO CNV$COUNTERS(ID, COUNTERID, LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE, TAG, NAME) 
                // VALUES(NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

                var insertCharTemplate =
                    "INSERT INTO CNV$COUNTERS(ID, COUNTERID, LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE, TAG, NAME, GUID_) " +
                    "VALUES(NULL, {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15} );";

                var sql = String.Format(insertCharTemplate,
                    RecordUtils.GetSafeStringWithNull(COUNTERID),
                    LSHET,
                    RecordUtils.GetSafeIntWithNull(CNTTYPE),
                    RecordUtils.GetSafeStringWithNull(CNTNAME),
                    RecordUtils.GetSafeDateTimeWithNull(SETUPDATE),
                    RecordUtils.GetSafeStringWithNull(SERIALNUM),
                    RecordUtils.GetSafeIntWithNull(SETUPPLACE),
                    RecordUtils.GetSafeDateTimeWithNull(PLOMBDATE),
                    RecordUtils.GetSafeStringWithNull(PLOMBNAME),
                    RecordUtils.GetSafeDateWithNull(LASTPOV),
                    RecordUtils.GetSafeDateWithNull(NEXTPOV),
                    RecordUtils.GetSafeStringWithNull(PRIM_),
                    RecordUtils.GetSafeDateTimeWithNull(DEACTDATE),
                    RecordUtils.GetSafeStringWithNull(TAG),
                    RecordUtils.GetSafeStringWithNull(NAME),
                    RecordUtils.GetSafeStringWithNull(GUID_));
                return sql;
            }
        }




    }
}
