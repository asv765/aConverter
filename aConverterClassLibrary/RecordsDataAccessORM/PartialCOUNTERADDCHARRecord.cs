using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTERADDCHAR : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$COUNTERADDCHAR (COUNTERID, ADDCHARCD, VALUE_) " +
            "VALUES ({0}, {1}, {2});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_cOUNTERID),
            ToSql(_aDDCHARCD),
            ToSql(_vALUE_));
    }
}
