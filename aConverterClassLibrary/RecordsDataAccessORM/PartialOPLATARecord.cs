using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_OPLATA : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$OPLATA (LSHET, DOCUMENTCD, MONTH_, YEAR_, SUMMA, DATE_, DATE_VV, SOURCECD, SOURCENAME, SERVICECD, SERVICENAME, PRIM_) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_dOCUMENTCD),
            ToSql(_mONTH_),
            ToSql(_yEAR_),
            ToSql(_sUMMA),
            ToSql(_dATE_),
            ToSql(_dATE_VV),
            ToSql(_sOURCECD),
            ToSql(_sOURCENAME),
            ToSql(_sERVICECD),
            ToSql(_sERVICENAME),
            ToSql(_pRIM_));
    }
}
