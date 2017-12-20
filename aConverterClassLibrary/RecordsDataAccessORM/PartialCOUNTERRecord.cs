using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTER : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$COUNTERS (COUNTERID, LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE, TAG, NAME, STATUSID, STATUSDATE, COUNTER_LEVEL, TARGETBALANCE_KOD, DISTRIBUTINGMETHOD, TARGETNEGATIVEBALANCE_KOD, GROUPCOUNTERMODULEID, KODREGIM, NOCALCCHILDBALANCES, UNTINGID) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_cOUNTERID), 
            ToSql(_lSHET), 
            ToSql(_cNTTYPE), 
            ToSql(_cNTNAME), 
            ToSql(_sETUPDATE), 
            ToSql(_sERIALNUM),
            ToSql(_sETUPPLACE),
            ToSql(_pLOMBDATE), 
            ToSql(_pLOMBNAME),
            ToSql(_lASTPOV),
            ToSql(_nEXTPOV),
            ToSql(_pRIM_), 
            ToSql(_dEACTDATE),
            ToSql(_tAG),
            ToSql(_nAME),
            ToSql(_sTATUSID),
            ToSql(_sTATUSDATE),
            ToSql(_cOUNTER_LEVEL),
            ToSql(_tARGETBALANCE_KOD),
            ToSql(_dISTRIBUTINGMETHOD),
            ToSql(_tARGETNEGATIVEBALANCE_KOD),
            ToSql(_gROUPCOUNTERMODULEID),
            ToSql(_kODREGIM),
            ToSql(_nOCALCCHILDBALANCES),
            ToSql(_uNTINGID));
    }
}
