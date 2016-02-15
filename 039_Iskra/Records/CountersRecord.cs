// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _039_Iskra
{
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private string counterid;
        // <summary>
        // COUNTERID C(20)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(20)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 20); counterid = value; }
        }

        private string name;
        // <summary>
        // NAME C(30)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(30)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 30); name = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(6)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(6)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 6); servicecd = value; }
        }

        private string servicenm;
        // <summary>
        // SERVICENM C(50)
        // </summary>
        [FieldName("SERVICENM"), FieldType('C'), FieldWidth(50)]
        public string Servicenm
        {
            get { return servicenm; }
            set { CheckStringData("Servicenm", value, 50); servicenm = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET, COUNTERID, NAME, SERVICECD, SERVICENM) VALUES ('{0}', '{1}', '{2}', {3}, '{4}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim());
            return rs;
        }
    }
}