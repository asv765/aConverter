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
	public partial class CCHARSABONENTLIST
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
		
		private int _kODCCHARSLIST;
		public virtual int KODCCHARSLIST
		{
			get
			{
				return this._kODCCHARSLIST;
			}
			set
			{
				this._kODCCHARSLIST = value;
			}
		}
		
		private DateTime _aBONENTCCHARDATE;
		public virtual DateTime ABONENTCCHARDATE
		{
			get
			{
				return this._aBONENTCCHARDATE;
			}
			set
			{
				this._aBONENTCCHARDATE = value;
			}
		}
		
		private int? _dOCUMENTCD;
		public virtual int? DOCUMENTCD
		{
			get
			{
				return this._dOCUMENTCD;
			}
			set
			{
				this._dOCUMENTCD = value;
			}
		}
		
		private decimal? _sIGNIFICANCE;
		public virtual decimal? SIGNIFICANCE
		{
			get
			{
				return this._sIGNIFICANCE;
			}
			set
			{
				this._sIGNIFICANCE = value;
			}
		}
		
		private ABONENT _aBONENT;
		public virtual ABONENT ABONENT
		{
			get
			{
				return this._aBONENT;
			}
			set
			{
				this._aBONENT = value;
			}
		}
		
		private CCHARSLIST _cCHARSLIST;
		public virtual CCHARSLIST CCHARSLIST
		{
			get
			{
				return this._cCHARSLIST;
			}
			set
			{
				this._cCHARSLIST = value;
			}
		}
		
	}
}
#pragma warning restore 1591
