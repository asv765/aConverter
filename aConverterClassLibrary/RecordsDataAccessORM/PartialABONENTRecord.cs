using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_ABONENT : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$ABONENT (LSHET, HOUSECD, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, SETTLEMENTKOD, SETTLEMENTNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, F, I, O, PRIM_, EXTLSHET, EXTLSHET2, PHONENUM, POSTINDEX, DUCD, DUNAME, ISDELETED, HOUSENO, HOUSEPOSTFIX, HOUSENOTE, KORPUSNO, KORPUSPOSTFIX, FLATNO, FLATPOSTFIX, ROOMNO, ROOMPOSTFIX, CLOSEDATE, EMAIL, STARTDATE) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_hOUSECD),
            ToSql(_dISTKOD),
            ToSql(_dISTNAME),
            ToSql(_rAYONKOD),
            ToSql(_rAYONNAME),
            ToSql(_sETTLEMENTKOD),
            ToSql(_sETTLEMENTNAME),
            ToSql(_tOWNSKOD),
            ToSql(_tOWNSNAME),
            ToSql(_uLICAKOD),
            ToSql(_uLICANAME),
            ToSql(_f),
            ToSql(_i),
            ToSql(_o),
            ToSql(_pRIM_),
            ToSql(_eXTLSHET),
            ToSql(_eXTLSHET2),
            ToSql(_pHONENUM),
            ToSql(_pOSTINDEX),
            ToSql(_dUCD),
            ToSql(_dUNAME),
            ToSql(_iSDELETED),
            ToSql(_hOUSENO),
            ToSql(_hOUSEPOSTFIX),
            ToSql(_hOUSENOTE),
            ToSql(_kORPUSNO),
            ToSql(_kORPUSPOSTFIX),
            ToSql(_fLATNO),
            ToSql(_fLATPOSTFIX),
            ToSql(_rOOMNO),
            ToSql(_rOOMPOSTFIX),
            ToSql(_cLOSEDATE),
            ToSql(_eMAIL),
            ToSql(_sTARTDATE));
    }
}
