using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_COUNTERADDCHAR
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

        private string _cOUNTERID;
        public virtual string COUNTERID
        {
            get
            {
                return this._cOUNTERID;
            }
            set
            {
                this._cOUNTERID = value;
            }
        }

        private int? _aDDCHARCD;
        public virtual int? ADDCHARCD
        {
            get
            {
                return this._aDDCHARCD;
            }
            set
            {
                this._aDDCHARCD = value;
            }
        }

        private string _vALUE_;
        public virtual string VALUE_
        {
            get
            {
                return this._vALUE_;
            }
            set
            {
                this._vALUE_ = value;
            }
        }
    }
}
