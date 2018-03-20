using System;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_ABONENTPHONES
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

        private string _pHONENUMBER;
        public virtual string PHONENUMBER
        {
            get
            {
                return this._pHONENUMBER;
            }
            set
            {
                this._pHONENUMBER = value;
            }
        }
    }
}