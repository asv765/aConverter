using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_EXTLSHET : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$EXTLSHET(LSHET, EXTLSHET, EXTORGCD, EXTORGNAME) " +
            "VALUES({0}, {1}, {2}, {3});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_eXTLSHET),
            ToSql(_eXTORGCD),
            ToSql(_eXTORGNAME));
    }
}
