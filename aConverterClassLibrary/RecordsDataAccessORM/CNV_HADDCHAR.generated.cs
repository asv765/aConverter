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
	public partial class CNV_HADDCHAR
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
#pragma warning restore 1591
