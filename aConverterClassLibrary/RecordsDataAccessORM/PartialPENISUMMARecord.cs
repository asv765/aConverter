using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_PENISUMMA : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$PENISUMMA (SERVICECD, LSHET, FDATE, FDAY, FMONTH, FYEAR, ABONENTSALDO, PENINACHISLSUMMA, ISCONTROLPOINT, NDATE, IZMEN) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_sERVICECD),
            ToSql(_lSHET),
            ToSql(_fDATE),
            ToSql(_fDAY),
            ToSql(_fMONTH),
            ToSql(_fYEAR),
            ToSql(_aBONENTSALDO),
            ToSql(_pENINACHISLSUMMA),
            ToSql(_iSCONTROLPOINT),
            ToSql(_nDATE),
            ToSql(_iZMEN));
    }
}
