#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace aConverterClassLibrary.RecordsDataAccessORM	
{
	public partial class CNV_PENISUMMA
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

        private int? _sERVICECD;
        public virtual int? SERVICECD
        {
            get
            {
                return this._sERVICECD;
            }
            set
            {
                this._sERVICECD = value;
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
		
		private DateTime? _fDATE;
		public virtual DateTime? FDATE
        {
			get
			{
				return this._fDATE;
			}
			set
			{
				this._fDATE = value;
			}
		}

        private int? _fDAY;
        public virtual int? FDAY
        {
            get
            {
                return this._fDAY;
            }
            set
            {
                this._fDAY = value;
            }
        }

        private int? _fMONTH;
        public virtual int? FMONTH
        {
            get
            {
                return this._fMONTH;
            }
            set
            {
                this._fMONTH = value;
            }
        }

        private int? _fYEAR;
        public virtual int? FYEAR
        {
            get
            {
                return this._fYEAR;
            }
            set
            {
                this._fYEAR = value;
            }
        }

        private decimal? _aBONENTSALDO;
        public virtual decimal? ABONENTSALDO
        {
            get
            {
                return this._aBONENTSALDO;
            }
            set
            {
                this._aBONENTSALDO = value;
            }
        }

        private decimal? _pENINACHISLSUMMA;
        public virtual decimal? PENINACHISLSUMMA
        {
            get
            {
                return this._pENINACHISLSUMMA;
            }
            set
            {
                this._pENINACHISLSUMMA = value;
            }
        }

        private int? _iSCONTROLPOINT;
        public virtual int? ISCONTROLPOINT
        {
            get
            {
                return this._iSCONTROLPOINT;
            }
            set
            {
                this._iSCONTROLPOINT = value;
            }
        }

        private DateTime? _nDATE;
        public virtual DateTime? NDATE
        {
            get
            {
                return this._nDATE;
            }
            set
            {
                this._nDATE = value;
            }
        }

        private int? _iZMEN;
        public virtual int? IZMEN
        {
            get
            {
                return this._iZMEN;
            }
            set
            {
                this._iZMEN = value;
            }
        }
    }
}
#pragma warning restore 1591