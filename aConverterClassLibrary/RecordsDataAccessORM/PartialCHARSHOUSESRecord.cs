using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CHARSHOUSES : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CHARSHOUSES (HOUSECD, CHARCD, CHARNAME, VALUE_, DATE_) " +
            "VALUES ({0}, {1}, {2}, {3}, {4});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_hOUSECD),
            ToSql(_cHARCD),
            ToSql(_cHARNAME),
            ToSql(_vALUE_),
            ToSql(_dATE_));
    }
}
