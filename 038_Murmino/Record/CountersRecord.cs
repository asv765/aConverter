// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _038_Murmino
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
        // NAME C(20)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(20)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 20); name = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET, COUNTERID, NAME) VALUES ('{0}', '{1}', '{2}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim());
            return rs;
        }
    }
}