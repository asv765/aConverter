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
using _027_COKPToVodokanal;

namespace _027_COKPToVodokanal	
{
	public partial class ABONENT
	{
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
		
		private int? _oWNERID;
		public virtual int? OWNERID
		{
			get
			{
				return this._oWNERID;
			}
			set
			{
				this._oWNERID = value;
			}
		}
		
		private int? _iSDELETED;
		public virtual int? ISDELETED
		{
			get
			{
				return this._iSDELETED;
			}
			set
			{
				this._iSDELETED = value;
			}
		}
		
		private DateTime? _dOGOVORDT;
		public virtual DateTime? DOGOVORDT
		{
			get
			{
				return this._dOGOVORDT;
			}
			set
			{
				this._dOGOVORDT = value;
			}
		}
		
		private int? _hOUSECD;
		public virtual int? HOUSECD
		{
			get
			{
				return this._hOUSECD;
			}
			set
			{
				this._hOUSECD = value;
			}
		}
		
		private short? _fLATNO;
		public virtual short? FLATNO
		{
			get
			{
				return this._fLATNO;
			}
			set
			{
				this._fLATNO = value;
			}
		}
		
		private short? _rOOMNO;
		public virtual short? ROOMNO
		{
			get
			{
				return this._rOOMNO;
			}
			set
			{
				this._rOOMNO = value;
			}
		}
		
		private string _fIO;
		public virtual string FIO
		{
			get
			{
				return this._fIO;
			}
			set
			{
				this._fIO = value;
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
		
		private string _sECOND_NAME;
		public virtual string SECOND_NAME
		{
			get
			{
				return this._sECOND_NAME;
			}
			set
			{
				this._sECOND_NAME = value;
			}
		}
		
		private string _tEL;
		public virtual string TEL
		{
			get
			{
				return this._tEL;
			}
			set
			{
				this._tEL = value;
			}
		}
		
		private string _nOTE;
		public virtual string NOTE
		{
			get
			{
				return this._nOTE;
			}
			set
			{
				this._nOTE = value;
			}
		}
		
		private short? _dELETED;
		public virtual short? DELETED
		{
			get
			{
				return this._dELETED;
			}
			set
			{
				this._dELETED = value;
			}
		}
		
		private short? _nEEDMONTHDOLGRECOUNT;
		public virtual short? NEEDMONTHDOLGRECOUNT
		{
			get
			{
				return this._nEEDMONTHDOLGRECOUNT;
			}
			set
			{
				this._nEEDMONTHDOLGRECOUNT = value;
			}
		}
		
		private int? _cHANGEDOCUMENTCD;
		public virtual int? CHANGEDOCUMENTCD
		{
			get
			{
				return this._cHANGEDOCUMENTCD;
			}
			set
			{
				this._cHANGEDOCUMENTCD = value;
			}
		}
		
		private int? _aDDDOCUMENTCD;
		public virtual int? ADDDOCUMENTCD
		{
			get
			{
				return this._aDDDOCUMENTCD;
			}
			set
			{
				this._aDDDOCUMENTCD = value;
			}
		}
		
		private string _rEMINDING;
		public virtual string REMINDING
		{
			get
			{
				return this._rEMINDING;
			}
			set
			{
				this._rEMINDING = value;
			}
		}
		
		private string _fLATPOSTFIX;
		public virtual string FLATPOSTFIX
		{
			get
			{
				return this._fLATPOSTFIX;
			}
			set
			{
				this._fLATPOSTFIX = value;
			}
		}
		
		private DateTime? _dELETE_DATE;
		public virtual DateTime? DELETE_DATE
		{
			get
			{
				return this._dELETE_DATE;
			}
			set
			{
				this._dELETE_DATE = value;
			}
		}
		
		private string _rOOMPOSTFIX;
		public virtual string ROOMPOSTFIX
		{
			get
			{
				return this._rOOMPOSTFIX;
			}
			set
			{
				this._rOOMPOSTFIX = value;
			}
		}
		
		private string _kR;
		public virtual string KR
		{
			get
			{
				return this._kR;
			}
			set
			{
				this._kR = value;
			}
		}
		
		private int? _rEMINDINGUSERCD;
		public virtual int? REMINDINGUSERCD
		{
			get
			{
				return this._rEMINDINGUSERCD;
			}
			set
			{
				this._rEMINDINGUSERCD = value;
			}
		}
		
		private string _lASTUSER;
		public virtual string LASTUSER
		{
			get
			{
				return this._lASTUSER;
			}
			set
			{
				this._lASTUSER = value;
			}
		}
		
		private int? _tABNUMBER;
		public virtual int? TABNUMBER
		{
			get
			{
				return this._tABNUMBER;
			}
			set
			{
				this._tABNUMBER = value;
			}
		}
		
		private string _gUID;
		public virtual string GUID
		{
			get
			{
				return this._gUID;
			}
			set
			{
				this._gUID = value;
			}
		}
		
		private string _kLADR;
		public virtual string KLADR
		{
			get
			{
				return this._kLADR;
			}
			set
			{
				this._kLADR = value;
			}
		}
		
		private string _pCLOGIN;
		public virtual string PCLOGIN
		{
			get
			{
				return this._pCLOGIN;
			}
			set
			{
				this._pCLOGIN = value;
			}
		}
		
		private HOUSE _hOUSE;
		public virtual HOUSE HOUSE
		{
			get
			{
				return this._hOUSE;
			}
			set
			{
				this._hOUSE = value;
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
		
		private IList<EXTORGACCOUNT> _eXTORGACCOUNTs = new List<EXTORGACCOUNT>();
		public virtual IList<EXTORGACCOUNT> EXTORGACCOUNTs
		{
			get
			{
				return this._eXTORGACCOUNTs;
			}
		}
		
		private IList<CCHARSABONENTLIST> _cCHARSABONENTLISTs = new List<CCHARSABONENTLIST>();
		public virtual IList<CCHARSABONENTLIST> CCHARSABONENTLISTs
		{
			get
			{
				return this._cCHARSABONENTLISTs;
			}
		}
		
	}
}
#pragma warning restore 1591
