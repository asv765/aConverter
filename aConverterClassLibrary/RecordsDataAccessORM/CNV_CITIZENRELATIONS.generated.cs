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
	public partial class CNV_CITIZENRELATIONS
	{
		private int _cITIZENIDFROM;
		public virtual int CITIZENIDFROM
        {
			get
			{
				return this._cITIZENIDFROM;
			}
			set
			{
				this._cITIZENIDFROM = value;
			}
		}

        private int _cITIZENIDTO;
        public virtual int CITIZENIDTO
        {
            get
            {
                return this._cITIZENIDTO;
            }
            set
            {
                this._cITIZENIDTO = value;
            }
        }

        private int? _rELATIONID;
        public virtual int? RELATIONID
        {
            get
            {
                return this._rELATIONID;
            }
            set
            {
                this._rELATIONID = value;
            }
        }

        private string _rELATIONNAME;
        public virtual string RELATIONNAME
        {
            get
            {
                return this._rELATIONNAME;
            }
            set
            {
                this._rELATIONNAME = value;
            }
        }
    }
}
#pragma warning restore 1591
