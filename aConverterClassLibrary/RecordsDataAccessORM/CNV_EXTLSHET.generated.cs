using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public partial class CNV_EXTLSHET
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

        private string _eXTLSHET;
        public virtual string EXTLSHET
        {
            get
            {
                return this._eXTLSHET;
            }
            set
            {
                this._eXTLSHET = value;
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

        private string _eXTORGNAME;
        public virtual string EXTORGNAME
        {
            get
            {
                return this._eXTORGNAME;
            }
            set
            {
                this._eXTORGNAME = value;
            }
        }
    }
}
