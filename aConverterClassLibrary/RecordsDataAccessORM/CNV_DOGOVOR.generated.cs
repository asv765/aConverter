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
	public partial class CNV_DOGOVOR
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
		
		private int? _dOGTYPEKOD;
		public virtual int? DOGTYPEKOD
		{
			get
			{
				return this._dOGTYPEKOD;
			}
			set
			{
				this._dOGTYPEKOD = value;
			}
		}
		
		private string _dOGTYPENAM;
		public virtual string DOGTYPENAM
		{
			get
			{
				return this._dOGTYPENAM;
			}
			set
			{
				this._dOGTYPENAM = value;
			}
		}
		
		private string _dESCRIPTIO;
		public virtual string DESCRIPTIO
		{
			get
			{
				return this._dESCRIPTIO;
			}
			set
			{
				this._dESCRIPTIO = value;
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
		
		private string _sERIA;
		public virtual string SERIA
		{
			get
			{
				return this._sERIA;
			}
			set
			{
				this._sERIA = value;
			}
		}
		
		private string _nOMER;
		public virtual string NOMER
		{
			get
			{
				return this._nOMER;
			}
			set
			{
				this._nOMER = value;
			}
		}
		
	}
}
#pragma warning restore 1591
