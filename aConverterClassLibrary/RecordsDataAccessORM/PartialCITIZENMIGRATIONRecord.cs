using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CITIZENMIGRATION : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CITIZENMIGRATION (CITIZENID, DATE_, MIGRATIONTYPE, DIRECTION) " +
            "VALUES ({0}, {1}, {2}, {3});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_cITIZENID),
            ToSql(_dATE_),
            ToSql(_mIGRATIONTYPE),
            ToSql(_dIRECTION));
    }
}
