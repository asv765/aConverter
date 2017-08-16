﻿using static aConverterClassLibrary.RecordsDataAccessORM.Utils.OrmRecordUtils;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_NACH : IOrmRecord
    {
        public const string InsertSqlTemplate =
            "INSERT INTO CNV$NACH (LSHET, DOCUMENTCD, MONTH_, YEAR_, MONTH2, YEAR2, FNATH, PROCHL, PROCHLVOLUME, VOLUME, REGIMCD, REGIMNAME, SERVICECD, SERVICENAME, DATE_VV, TYPE_, DOCNAME, DOCNUMBER, DOCDATE, VTYPE_, AUTOUSE, CASETYPE) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21});";

        public string InsertSql => string.Format(InsertSqlTemplate,
            ToSql(_lSHET),
            ToSql(_dOCUMENTCD),
            _mONTH_,
            _yEAR_,
            _mONTH2,
            _yEAR2,
            ToSql(_fNATH),
            ToSql(_pROCHL),
            ToSql(_pROCHLVOLUME),
            ToSql(_vOLUME),
            _rEGIMCD,
            ToSql(_rEGIMNAME),
            _sERVICECD,
            ToSql(_sERVICENAME),
            ToSql(_dATE_VV),
            _tYPE_,
            ToSql(_dOCNAME),
            ToSql(_dOCNUMBER),
            ToSql(_dOCDATE),
            _vTYPE_,
            _aUTOUSE,
            ToSql(_cASETYPE));
    }
}
