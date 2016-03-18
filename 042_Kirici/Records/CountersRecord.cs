// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _042_Kirici
{
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord : AbstractRecord
    {
        private string lshet_kod;
        // <summary>
        // LSHET_KOD C(10)
        // </summary>
        [FieldName("LSHET_KOD"), FieldType('C'), FieldWidth(10)]
        public string Lshet_kod
        {
            get { return lshet_kod; }
            set { CheckStringData("Lshet_kod", value, 10); lshet_kod = value; }
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
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(3)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(3)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 3); servicecd = value; }
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

        private DateTime setupdate;
        // <summary>
        // SETUPDATE D(8)
        // </summary>
        [FieldName("SETUPDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Setupdate
        {
            get { return setupdate; }
            set { setupdate = value; }
        }

        private DateTime deactdate;
        // <summary>
        // DEACTDATE D(8)
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Deactdate
        {
            get { return deactdate; }
            set { deactdate = value; }
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
            if (ADataRow.Table.Columns.Contains("LSHET_KOD")) Lshet_kod = ADataRow["LSHET_KOD"].ToString(); else Lshet_kod = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("SERIALNUM")) Serialnum = ADataRow["SERIALNUM"].ToString(); else Serialnum = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("SETUPDATE")) Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]); else Setupdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DEACTDATE")) Deactdate = Convert.ToDateTime(ADataRow["DEACTDATE"]); else Deactdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet_kod = this.Lshet_kod;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Serialnum = this.Serialnum;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Setupdate = this.Setupdate;
            retValue.Deactdate = this.Deactdate;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET_KOD, COUNTERID, NAME, SERIALNUM, SERVICECD, SERVICENM, SETUPDATE, DEACTDATE, ISDELETED) VALUES ('{0}', '{1}', '{2}', '{3}', {4}, '{5}', CTOD('{6}'), CTOD('{7}'), {8})", String.IsNullOrEmpty(Lshet_kod) ? "" : Lshet_kod.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), Deactdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Deactdate.Month, Deactdate.Day, Deactdate.Year), Isdeleted.ToString());
            return rs;
        }
    }
}