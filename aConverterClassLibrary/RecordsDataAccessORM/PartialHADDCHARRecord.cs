using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_HADDCHAR : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$HADDCHAR(HOUSECD, ADDCHARCD, VALUE_) " +
            "VALUES ({0}, {1}, {2});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_hOUSECD), 
            ToSql(_aDDCHARCD),
            ToSql(_vALUE_));
    }
}
