using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_LCHARHOUSES : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$LCHARHOUSES (HOUSECD, LCHARCD, LCHARNAME, VALUE_, VALUEDESC, DATE_) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_hOUSECD), 
            ToSql(_lCHARCD), 
            ToSql(_lCHARNAME), 
            ToSql(_vALUE_), 
            ToSql(_vALUEDESC), 
            ToSql(_dATE_));
    }
}
