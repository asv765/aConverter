using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTERTYPE : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$COUNTERTYPES (ID, EQUIPMENTTYPEID, PERIODKOD, NAME, COEFFICIENT, DIGITCOUNT, EQUIPMENTGROUPID, EXTORGCD, DIMENSIONTYPE, MINCONSUMPTION, MAXCONSUMPTION, MINTEMPERATURE, MAXTEMPERATURE, COUNTERMARKMODULEID, SERVICELIFEID, ACCURACY) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_iD),
            ToSql(_eQUIPMENTTYPEID),
            ToSql(_pERIODKOD),
            ToSql(_nAME),
            ToSql(_cOEFFICIENT),
            ToSql(_dIGITCOUNT),
            ToSql(_eQUIPMENTGROUPID),
            ToSql(_eXTORGCD),
            ToSql(_dIMENSIONTYPE),
            ToSql(_mINCONSUMPTION),
            ToSql(_mAXCONSUMPTION),
            ToSql(_mINTEMPERATURE),
            ToSql(_mAXTEMPERATURE),
            ToSql(_cOUNTERMARKMODULEID),
            ToSql(_sERVICELIFEID),
            ToSql(_aCCURACY));
    }
}
