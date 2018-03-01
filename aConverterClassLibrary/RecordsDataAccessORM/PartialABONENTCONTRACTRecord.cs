using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_ABONENTCONTRACT : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$ABONENTCONTRACT(LSHET, NAME, TYPEID, DOC_SER, DOC_NUMBER, DOC_DATE, ORGID, STARTDATE, ENDDATE, PRIM, CITIZENID, SERVICES) " +
            "VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11});";

        public string InsertSql => string.Format(InsertSqlTemplate, 
            ToSql(_lSHET),
            ToSql(_nAME),
            ToSql(_tYPEID),
            ToSql(_dOC_SER),
            ToSql(_dOC_NUMBER),
            ToSql(_dOC_DATE),
            ToSql(_oRGID),
            ToSql(_sTARTDATE),
            ToSql(_eNDDATE),
            ToSql(_pRIM),
            ToSql(_cITIZENID),
            ToSql(_sERVICES));
    }
}
