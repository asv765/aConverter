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
using _037_COKPToVodokanal;

namespace _037_COKPToVodokanal	
{
	public partial class LCHARSLIST
	{
		private int _kOD;
		public virtual int KOD
		{
			get
			{
				return this._kOD;
			}
			set
			{
				this._kOD = value;
			}
		}
		
		private int? _bALANCE_KOD;
		public virtual int? BALANCE_KOD
		{
			get
			{
				return this._bALANCE_KOD;
			}
			set
			{
				this._bALANCE_KOD = value;
			}
		}
		
		private int? _kODGROUPSLCHARSLIST;
		public virtual int? KODGROUPSLCHARSLIST
		{
			get
			{
				return this._kODGROUPSLCHARSLIST;
			}
			set
			{
				this._kODGROUPSLCHARSLIST = value;
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
		
		private string _sHORTNAME;
		public virtual string SHORTNAME
		{
			get
			{
				return this._sHORTNAME;
			}
			set
			{
				this._sHORTNAME = value;
			}
		}
		
		private short? _vISIBLE;
		public virtual short? VISIBLE
		{
			get
			{
				return this._vISIBLE;
			}
			set
			{
				this._vISIBLE = value;
			}
		}
		
		private int? _dOCTYPEID;
		public virtual int? DOCTYPEID
		{
			get
			{
				return this._dOCTYPEID;
			}
			set
			{
				this._dOCTYPEID = value;
			}
		}
		
		private short? _rEADONLY;
		public virtual short? READONLY
		{
			get
			{
				return this._rEADONLY;
			}
			set
			{
				this._rEADONLY = value;
			}
		}
		
		private int? _rEASONID;
		public virtual int? REASONID
		{
			get
			{
				return this._rEASONID;
			}
			set
			{
				this._rEASONID = value;
			}
		}
		
		private short? _iSCALCULATED;
		public virtual short? ISCALCULATED
		{
			get
			{
				return this._iSCALCULATED;
			}
			set
			{
				this._iSCALCULATED = value;
			}
		}
		
		private int? _mODULEID;
		public virtual int? MODULEID
		{
			get
			{
				return this._mODULEID;
			}
			set
			{
				this._mODULEID = value;
			}
		}
		
		private short? _cHARTYPE;
		public virtual short? CHARTYPE
		{
			get
			{
				return this._cHARTYPE;
			}
			set
			{
				this._cHARTYPE = value;
			}
		}
		
		private short? _fIXCHANGES4PAYSYSTEM;
		public virtual short? FIXCHANGES4PAYSYSTEM
		{
			get
			{
				return this._fIXCHANGES4PAYSYSTEM;
			}
			set
			{
				this._fIXCHANGES4PAYSYSTEM = value;
			}
		}
		
		private int? _oRDERPRIORITY;
		public virtual int? ORDERPRIORITY
		{
			get
			{
				return this._oRDERPRIORITY;
			}
			set
			{
				this._oRDERPRIORITY = value;
			}
		}
		
		private IList<LCHARSABONENTLIST> _lCHARSABONENTLISTs = new List<LCHARSABONENTLIST>();
		public virtual IList<LCHARSABONENTLIST> LCHARSABONENTLISTs
		{
			get
			{
				return this._lCHARSABONENTLISTs;
			}
		}
		
	}
}
#pragma warning restore 1591
