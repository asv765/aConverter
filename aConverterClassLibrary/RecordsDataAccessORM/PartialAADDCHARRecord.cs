using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_AADDCHAR : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$AADDCHAR (LSHET, ADDCHARCD, \"VALUE\") " +
            "VALUES({0}, {1}, {2});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_aDDCHARCD),
            ToSql(_vALUE));
    }
}
