using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_ABONENTPHONES : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$ABONENTPHONES(LSHET, TYPEID, PHONENUMBER) " +
            "VALUES({0}, {1}, {2});";

        public string InsertSql => string.Format(InsertSqlTemplate, 
            ToSql(_lSHET),
            ToSql(_tYPEID),
            ToSql(_pHONENUMBER));
    }
}
