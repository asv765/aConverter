// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _045_SpasskStroyDetal
{
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord : AbstractRecord
    {
        private string grkod;
        // <summary>
        // GRKOD C(8)
        // </summary>
        [FieldName("GRKOD"), FieldType('C'), FieldWidth(8)]
        public string Grkod
        {
            get { return grkod; }
            set { CheckStringData("Grkod", value, 8); grkod = value; }
        }

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

        private Int64 counterid;
        // <summary>
        // COUNTERID N(6)
        // </summary>
        [FieldName("COUNTERID"), FieldType('N'), FieldWidth(6)]
        public Int64 Counterid
        {
            get { return counterid; }
            set { CheckIntegerData("Counterid", value, 6); counterid = value; }
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

        private DateTime plombdate;
        // <summary>
        // PLOMBDATE D(8)
        // </summary>
        [FieldName("PLOMBDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Plombdate
        {
            get { return plombdate; }
            set { plombdate = value; }
        }

        private DateTime nextprdate;
        // <summary>
        // NEXTPRDATE D(8)
        // </summary>
        [FieldName("NEXTPRDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Nextprdate
        {
            get { return nextprdate; }
            set { nextprdate = value; }
        }

        private Int64 grcntid;
        // <summary>
        // GRCNTID N(6)
        // </summary>
        [FieldName("GRCNTID"), FieldType('N'), FieldWidth(6)]
        public Int64 Grcntid
        {
            get { return grcntid; }
            set { CheckIntegerData("Grcntid", value, 6); grcntid = value; }
        }

        private string grcntname;
        // <summary>
        // GRCNTNAME C(25)
        // </summary>
        [FieldName("GRCNTNAME"), FieldType('C'), FieldWidth(25)]
        public string Grcntname
        {
            get { return grcntname; }
            set { CheckStringData("Grcntname", value, 25); grcntname = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("GRKOD")) Grkod = ADataRow["GRKOD"].ToString(); else Grkod = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = Convert.ToInt64(ADataRow["COUNTERID"]); else Counterid = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("DIGITCOUNT")) Digitcount = Convert.ToInt64(ADataRow["DIGITCOUNT"]); else Digitcount = 0;
            if (ADataRow.Table.Columns.Contains("PLOMBDATE")) Plombdate = Convert.ToDateTime(ADataRow["PLOMBDATE"]); else Plombdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NEXTPRDATE")) Nextprdate = Convert.ToDateTime(ADataRow["NEXTPRDATE"]); else Nextprdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("GRCNTID")) Grcntid = Convert.ToInt64(ADataRow["GRCNTID"]); else Grcntid = 0;
            if (ADataRow.Table.Columns.Contains("GRCNTNAME")) Grcntname = ADataRow["GRCNTNAME"].ToString(); else Grcntname = "";
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Grkod = this.Grkod;
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Digitcount = this.Digitcount;
            retValue.Plombdate = this.Plombdate;
            retValue.Nextprdate = this.Nextprdate;
            retValue.Grcntid = this.Grcntid;
            retValue.Grcntname = this.Grcntname;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (GRKOD, LSHET, COUNTERID, NAME, DIGITCOUNT, PLOMBDATE, NEXTPRDATE, GRCNTID, GRCNTNAME) VALUES ('{0}', '{1}', {2}, '{3}', {4}, CTOD('{5}'), CTOD('{6}'), {7}, '{8}')", String.IsNullOrEmpty(Grkod) ? "" : Grkod.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Counterid.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Digitcount.ToString(), Plombdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Plombdate.Month, Plombdate.Day, Plombdate.Year), Nextprdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Nextprdate.Month, Nextprdate.Day, Nextprdate.Year), Grcntid.ToString(), String.IsNullOrEmpty(Grcntname) ? "" : Grcntname.Trim());
            return rs;
        }
    }
}