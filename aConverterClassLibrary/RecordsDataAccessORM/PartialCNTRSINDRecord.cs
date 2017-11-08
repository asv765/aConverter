using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CNTRSIND : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CNTRSIND (COUNTERID, DOCUMENTCD, OLDIND, OB_EM, INDICATION, INDDATE, INDTYPE, CASETYPE) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_cOUNTERID),
            ToSql(_dOCUMENTCD),
            ToSql(_oLDIND),
            ToSql(_oB_EM),
            ToSql(_iNDICATION),
            ToSql(_iNDDATE),
            ToSql(_iNDTYPE),
            ToSql(_cASETYPE));
    }
}
