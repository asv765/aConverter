// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _043_PkStroy
{
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(17)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(17)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 17); lshet = value; }
        }

        private string placecd;
        // <summary>
        // PLACECD C(9)
        // </summary>
        [FieldName("PLACECD"), FieldType('C'), FieldWidth(9)]
        public string Placecd
        {
            get { return placecd; }
            set { CheckStringData("Placecd", value, 9); placecd = value; }
        }

        private string counterid;
        // <summary>
        // COUNTERID C(9)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(9)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 9); counterid = value; }
        }

        private string name;
        // <summary>
        // NAME C(100)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(100)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 100); name = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(25)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(25)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 25); serialnum = value; }
        }

        private DateTime madedate;
        // <summary>
        // MADEDATE D(8)
        // </summary>
        [FieldName("MADEDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Madedate
        {
            get { return madedate; }
            set { madedate = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(4)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(4)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 4); servicecd = value; }
        }

        private string servicenm;
        // <summary>
        // SERVICENM C(100)
        // </summary>
        [FieldName("SERVICENM"), FieldType('C'), FieldWidth(100)]
        public string Servicenm
        {
            get { return servicenm; }
            set { CheckStringData("Servicenm", value, 100); servicenm = value; }
        }

        private Int64 isdeleted;
        // <summary>
        // ISDELETED N(2)
        // </summary>
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(2)]
        public Int64 Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 2); isdeleted = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("PLACECD")) Placecd = ADataRow["PLACECD"].ToString(); else Placecd = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("SERIALNUM")) Serialnum = ADataRow["SERIALNUM"].ToString(); else Serialnum = "";
            if (ADataRow.Table.Columns.Contains("MADEDATE")) Madedate = Convert.ToDateTime(ADataRow["MADEDATE"]); else Madedate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet = this.Lshet;
            retValue.Placecd = this.Placecd;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Serialnum = this.Serialnum;
            retValue.Madedate = this.Madedate;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET, PLACECD, COUNTERID, NAME, SERIALNUM, MADEDATE, SERVICECD, SERVICENM, ISDELETED) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', CTOD('{5}'), {6}, '{7}', {8})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Placecd) ? "" : Placecd.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Madedate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Madedate.Month, Madedate.Day, Madedate.Year), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}