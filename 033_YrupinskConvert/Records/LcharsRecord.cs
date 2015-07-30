// Файл сгенерирован aConverter
using System;
using DbfClassLibrary;

namespace _033_Yrupinsk.Records
{
    [TableName("LCHARS.DBF")]
    public partial class LcharsRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(11)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(11)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 11); lshet = value; }
        }

        private Int64 lcharcd;
        // <summary>
        // LCHARCD N(6)
        // </summary>
        [FieldName("LCHARCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Lcharcd
        {
            get { return lcharcd; }
            set { CheckIntegerData("Lcharcd", value, 6); lcharcd = value; }
        }

        private string lcharname;
        // <summary>
        // LCHARNAME C(50)
        // </summary>
        [FieldName("LCHARNAME"), FieldType('C'), FieldWidth(50)]
        public string Lcharname
        {
            get { return lcharname; }
            set { CheckStringData("Lcharname", value, 50); lcharname = value; }
        }

        private Int64 value_;
        // <summary>
        // VALUE N(3)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(3)]
        public Int64 Value_
        {
            get { return value_; }
            set { CheckIntegerData("Value_", value, 3); value_ = value; }
        }

        private string valuedesc;
        // <summary>
        // VALUEDESC C(60)
        // </summary>
        [FieldName("VALUEDESC"), FieldType('C'), FieldWidth(60)]
        public string Valuedesc
        {
            get { return valuedesc; }
            set { CheckStringData("Valuedesc", value, 60); valuedesc = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D(8)
        // </summary>
        [FieldName("ENDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("LCHARCD")) Lcharcd = Convert.ToInt64(ADataRow["LCHARCD"]); else Lcharcd = 0;
            if (ADataRow.Table.Columns.Contains("LCHARNAME")) Lcharname = ADataRow["LCHARNAME"].ToString(); else Lcharname = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToInt64(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("VALUEDESC")) Valuedesc = ADataRow["VALUEDESC"].ToString(); else Valuedesc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ENDDATE")) Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]); else Enddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            LcharsRecord retValue = new LcharsRecord();
            retValue.Lshet = this.Lshet;
            retValue.Lcharcd = this.Lcharcd;
            retValue.Lcharname = this.Lcharname;
            retValue.Value_ = this.Value_;
            retValue.Valuedesc = this.Valuedesc;
            retValue.Date = this.Date;
            retValue.Enddate = this.Enddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LCHARS (LSHET, LCHARCD, LCHARNAME, VALUE, VALUEDESC, DATE, ENDDATE) VALUES ('{0}', {1}, '{2}', {3}, '{4}', CTOD('{5}'), CTOD('{6}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Lcharcd.ToString(), String.IsNullOrEmpty(Lcharname) ? "" : Lcharname.Trim(), Value_.ToString(), String.IsNullOrEmpty(Valuedesc) ? "" : Valuedesc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year));
            return rs;
        }
    }
}
