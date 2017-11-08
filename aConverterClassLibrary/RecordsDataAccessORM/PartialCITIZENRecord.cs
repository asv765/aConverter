using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_CITIZEN : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$CITIZENS (LSHET, CITIZENID, F, I, O, BIRTHDATE, STARTDATE, ENDDATE, LGOTA, LGOTANAME, DATWP, DATUP, DOCUMENTNAME, DOCTYPEID, SERIA, NOMER, DATDN, DORGNAME, DORGCD, KOLLG, HOZ, BIRTHPLACE, SOB, DOLYA, COMMENT_, PRIBYT, VREMREG, SEX , STATUSID , OWNERSHIPTYPE , OWNERSHIPNUMERATOR , OWNERSHIPDENOMINATOR , BIRTHCOUNTRY , BIRTHDISTRICT , BIRTHREGION , BIRTHCITY , BIRTHVILLAGE , CITIZENSHIP , ISMAINCITYZEN , HIDDEN , REGISTRATIONTYPE , ARRIVEDATE , LEAVEDATE , LEAVECASEID , DEATHDATE, STATUSDATE, EGRPNUMBER, EGRPDATE, UNIQUECITIZENID) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47}, {48});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_cITIZENID),
            ToSql(_f),
            ToSql(_i),
            ToSql(_o),
            ToSql(_bIRTHDATE),
            ToSql(_sTARTDATE),
            ToSql(_eNDDATE),
            ToSql(_lGOTA),
            ToSql(_lGOTANAME),
            ToSql(_dATWP),
            ToSql(_dATUP),
            ToSql(_dOCUMENTNAME),
            ToSql(_dOCTYPEID),
            ToSql(_sERIA),
            ToSql(_nOMER),
            ToSql(_dATDN),
            ToSql(_dORGNAME),
            ToSql(_dORGCD),
            ToSql(_kOLLG),
            ToSql(_hOZ),
            ToSql(_bIRTHPLACE),
            ToSql(_sOB),
            ToSql(_dOLYA),
            ToSql(_cOMMENT_),
            ToSql(_pRIBYT),
            ToSql(_vREMREG),
            ToSql(_sEX),
            ToSql(_sTATUSID),
            ToSql(_oWNERSHIPTYPE),
            ToSql(_oWNERSHIPNUMERATOR),
            ToSql(_oWNERSHIPDENOMINATOR),
            ToSql(_bIRTHCOUNTRY),
            ToSql(_bIRTHDISTRICT),
            ToSql(_bIRTHREGION),
            ToSql(_bIRTHCITY),
            ToSql(_bIRTHVILLAGE),
            ToSql(_cITIZENSHIP),
            ToSql(_iSMAINCITYZEN),
            ToSql(_hIDDEN),
            ToSql(_rEGISTRATIONTYPE),
            ToSql(_aRRIVEDATE),
            ToSql(_lEAVEDATE),
            ToSql(_lEAVECASEID),
            ToSql(_dEATHDATE),
            ToSql(_sTATUSDATE),
            ToSql(_eGRPNUMBER),
            ToSql(_eGRPDATE),
            ToSql(_uNIQUECITIZENID));
    }
}
