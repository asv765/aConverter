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
	public partial class EXTORGACCOUNT
	{
		private int _eXTORGCD;
		public virtual int EXTORGCD
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
		
	}
}
#pragma warning restore 1591
