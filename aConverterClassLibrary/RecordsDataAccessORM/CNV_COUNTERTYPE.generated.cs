using System;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTERTYPE
    {
        private int _iD;
        public virtual int ID
        {
            get
            {
                return this._iD;
            }
            set
            {
                this._iD = value;
            }
        }

        private int _eQUIPMENTTYPEID;
        public virtual int EQUIPMENTTYPEID
        {
            get
            {
                return this._eQUIPMENTTYPEID;
            }
            set
            {
                this._eQUIPMENTTYPEID = value;
            }
        }

        private int? _pERIODKOD;
        public virtual int? PERIODKOD
        {
            get
            {
                return this._pERIODKOD;
            }
            set
            {
                this._pERIODKOD = value;
            }
        }

        private string _nAME;
        public virtual string NAME
        {
            get
            {
                return this._nAME;
            }
            set
            {
                this._nAME = value;
            }
        }

        private int? _cOEFFICIENT;
        public virtual int? COEFFICIENT
        {
            get
            {
                return this._cOEFFICIENT;
            }
            set
            {
                this._cOEFFICIENT = value;
            }
        }

        private int _dIGITCOUNT;
        public virtual int DIGITCOUNT
        {
            get
            {
                return this._dIGITCOUNT;
            }
            set
            {
                this._dIGITCOUNT = value;
            }
        }

        private int _eQUIPMENTGROUPID;
        public virtual int EQUIPMENTGROUPID
        {
            get
            {
                return this._eQUIPMENTGROUPID;
            }
            set
            {
                this._eQUIPMENTGROUPID = value;
            }
        }

        private int? _eXTORGCD;
        public virtual int? EXTORGCD
        {
            get
            {
                return this._eXTORGCD;
            }
            set
            {
                this._eXTORGCD = value;
            }
        }

        private string _dIMENSIONTYPE;
        public virtual string DIMENSIONTYPE
        {
            get
            {
                return this._dIMENSIONTYPE;
            }
            set
            {
                this._dIMENSIONTYPE = value;
            }
        }

        private decimal? _mINCONSUMPTION;
        public virtual decimal? MINCONSUMPTION
        {
            get
            {
                return this._mINCONSUMPTION;
            }
            set
            {
                this._mINCONSUMPTION = value;
            }
        }

        private decimal? _mAXCONSUMPTION;
        public virtual decimal? MAXCONSUMPTION
        {
            get
            {
                return this._mAXCONSUMPTION;
            }
            set
            {
                this._mAXCONSUMPTION = value;
            }
        }

        private decimal? _mINTEMPERATURE;
        public virtual decimal? MINTEMPERATURE
        {
            get
            {
                return this._mINTEMPERATURE;
            }
            set
            {
                this._mINTEMPERATURE = value;
            }
        }

        private decimal? _mAXTEMPERATURE;
        public virtual decimal? MAXTEMPERATURE
        {
            get
            {
                return this._mAXTEMPERATURE;
            }
            set
            {
                this._mAXTEMPERATURE = value;
            }
        }

        private int? _cOUNTERMARKMODULEID;
        public virtual int? COUNTERMARKMODULEID
        {
            get
            {
                return this._cOUNTERMARKMODULEID;
            }
            set
            {
                this._cOUNTERMARKMODULEID = value;
            }
        }

        private int? _sERVICELIFEID;
        public virtual int? SERVICELIFEID
        {
            get
            {
                return this._sERVICELIFEID;
            }
            set
            {
                this._sERVICELIFEID = value;
            }
        }

        private decimal? _aCCURACY;
        public virtual decimal? ACCURACY
        {
            get
            {
                return this._aCCURACY;
            }
            set
            {
                this._aCCURACY = value;
            }
        }
    }
}
