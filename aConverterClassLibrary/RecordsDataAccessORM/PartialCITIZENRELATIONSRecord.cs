using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CITIZENRELATIONS : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CITIZENRELATIONS(CITIZENIDFROM, CITIZENIDTO, RELATIONID, RELATIONNAME) " +
            "VALUES ({0}, {1}, {2}, {3});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_cITIZENIDFROM),
            ToSql(_cITIZENIDTO),
            ToSql(_rELATIONID),
            ToSql(_rELATIONNAME));
    }
}
