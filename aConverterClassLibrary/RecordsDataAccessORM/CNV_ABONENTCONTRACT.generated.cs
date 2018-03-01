using System;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_ABONENTCONTRACT
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

        private string _lSHET;
        public virtual string LSHET
        {
            get
            {
                return this._lSHET;
            }
            set
            {
                this._lSHET = value;
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

        private int? _tYPEID;
        public virtual int? TYPEID
        {
            get
            {
                return this._tYPEID;
            }
            set
            {
                this._tYPEID = value;
            }
        }

        private string _dOC_SER;
        public virtual string DOC_SER
        {
            get
            {
                return this._dOC_SER;
            }
            set
            {
                this._dOC_SER = value;
            }
        }

        private string _dOC_NUMBER;
        public virtual string DOC_NUMBER
        {
            get
            {
                return this._dOC_NUMBER;
            }
            set
            {
                this._dOC_NUMBER = value;
            }
        }

        private DateTime? _dOC_DATE;
        public virtual DateTime? DOC_DATE
        {
            get
            {
                return this._dOC_DATE;
            }
            set
            {
                this._dOC_DATE = value;
            }
        }

        private int? _oRGID;
        public virtual int? ORGID
        {
            get
            {
                return this._oRGID;
            }
            set
            {
                this._oRGID = value;
            }
        }

        private DateTime? _sTARTDATE;
        public virtual DateTime? STARTDATE
        {
            get
            {
                return this._sTARTDATE;
            }
            set
            {
                this._sTARTDATE = value;
            }
        }

        private DateTime? _eNDDATE;
        public virtual DateTime? ENDDATE
        {
            get
            {
                return this._eNDDATE;
            }
            set
            {
                this._eNDDATE = value;
            }
        }

        private string _pRIM;
        public virtual string PRIM
        {
            get
            {
                return this._pRIM;
            }
            set
            {
                this._pRIM = value;
            }
        }

        private int? _cITIZENID;
        public virtual int? CITIZENID
        {
            get
            {
                return this._cITIZENID;
            }
            set
            {
                this._cITIZENID = value;
            }
        }

        private string _sERVICES;
        public virtual string SERVICES
        {
            get
            {
                return this._sERVICES;
            }
            set
            {
                this._sERVICES = value;
            }
        }
    }
}