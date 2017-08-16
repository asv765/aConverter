using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CHAR : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CHARS (LSHET, CHARCD, CHARNAME, VALUE_, DATE_) " +
            "VALUES ({0}, {1}, {2}, {3}, {4});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_cHARCD),
            ToSql(_cHARNAME),
            ToSql(_vALUE_),
            ToSql(_dATE_));

        public long SortLshet;
    }
}
