// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _040_Skopin
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
        // COUNTERID C(8)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(8)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 8); counterid = value; }
        }

        private string name;
        // <summary>
        // NAME C(50)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        private string prim;
        // <summary>
        // PRIM C(100)
        // </summary>
        [FieldName("PRIM"), FieldType('C'), FieldWidth(100)]
        public string Prim
        {
            get { return prim; }
            set { CheckStringData("Prim", value, 100); prim = value; }
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

        private string cnttype;
        // <summary>
        // CNTTYPE C(9)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('C'), FieldWidth(9)]
        public string Cnttype
        {
            get { return cnttype; }
            set { CheckStringData("Cnttype", value, 9); cnttype = value; }
        }

        private string cntname;
        // <summary>
        // CNTNAME C(25)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(25)]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 25); cntname = value; }
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

        private DateTime reldate;
        // <summary>
        // RELDATE D(8)
        // </summary>
        [FieldName("RELDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Reldate
        {
            get { return reldate; }
            set { reldate = value; }
        }

        private Int64 digitcount;
        // <summary>
        // DIGITCOUNT N(3)
        // </summary>
        [FieldName("DIGITCOUNT"), FieldType('N'), FieldWidth(3)]
        public Int64 Digitcount
        {
            get { return digitcount; }
            set { CheckIntegerData("Digitcount", value, 3); digitcount = value; }
        }

        private decimal begind;
        // <summary>
        // BEGIND N(15,3)
        // </summary>
        [FieldName("BEGIND"), FieldType('N'), FieldWidth(15), FieldDec(3)]
        public decimal Begind
        {
            get { return begind; }
            set { CheckDecimalData("Begind", value, 15, 3); begind = value; }
        }

        private Int64 level;
        // <summary>
        // LEVEL N(2)
        // </summary>
        [FieldName("LEVEL"), FieldType('N'), FieldWidth(2)]
        public Int64 Level
        {
            get { return level; }
            set { CheckIntegerData("Level", value, 2); level = value; }
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
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("PRIM")) Prim = ADataRow["PRIM"].ToString(); else Prim = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("CNTTYPE")) Cnttype = ADataRow["CNTTYPE"].ToString(); else Cnttype = "";
            if (ADataRow.Table.Columns.Contains("CNTNAME")) Cntname = ADataRow["CNTNAME"].ToString(); else Cntname = "";
            if (ADataRow.Table.Columns.Contains("SERIALNUM")) Serialnum = ADataRow["SERIALNUM"].ToString(); else Serialnum = "";
            if (ADataRow.Table.Columns.Contains("RELDATE")) Reldate = Convert.ToDateTime(ADataRow["RELDATE"]); else Reldate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DIGITCOUNT")) Digitcount = Convert.ToInt64(ADataRow["DIGITCOUNT"]); else Digitcount = 0;
            if (ADataRow.Table.Columns.Contains("BEGIND")) Begind = Convert.ToDecimal(ADataRow["BEGIND"]); else Begind = 0;
            if (ADataRow.Table.Columns.Contains("LEVEL")) Level = Convert.ToInt64(ADataRow["LEVEL"]); else Level = 0;
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Prim = this.Prim;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Cnttype = this.Cnttype;
            retValue.Cntname = this.Cntname;
            retValue.Serialnum = this.Serialnum;
            retValue.Reldate = this.Reldate;
            retValue.Digitcount = this.Digitcount;
            retValue.Begind = this.Begind;
            retValue.Level = this.Level;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET, COUNTERID, NAME, PRIM, SERVICECD, SERVICENM, CNTTYPE, CNTNAME, SERIALNUM, RELDATE, DIGITCOUNT, BEGIND, LEVEL, ISDELETED) VALUES ('{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}', CTOD('{9}'), {10}, {11}, {12}, {13})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Prim) ? "" : Prim.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), String.IsNullOrEmpty(Cnttype) ? "" : Cnttype.Trim(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Reldate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Reldate.Month, Reldate.Day, Reldate.Year), Digitcount.ToString(), Begind.ToString().Replace(',', '.'), Level.ToString(), Isdeleted.ToString());
            return rs;
        }
    }
}