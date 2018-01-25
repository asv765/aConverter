using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTERTYPEADDCHAR : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$COUNTERTYPEADDCHAR(COUNTERTYPEID, ADDCHARCD, VALUE_) " +
            "VALUES({0}, {1}, {2});";

        public string InsertSql => string.Format(InsertSqlTemplate, 
            ToSql(_cOUNTERTYPEID),
            ToSql(_aDDCHARCD),
            ToSql(_vALUE_));
    }
}
