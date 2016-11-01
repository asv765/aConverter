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
    [TableName("LCHARS.DBF")]
    public partial class LcharsRecord : AbstractRecord
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

        private Int64 lcharcd;
        // <summary>
        // LCHARCD N(3)
        // </summary>
        [FieldName("LCHARCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Lcharcd
        {
            get { return lcharcd; }
            set { CheckIntegerData("Lcharcd", value, 3); lcharcd = value; }
        }

        private string lcharname;
        // <summary>
        // LCHARNAME C(100)
        // </summary>
        [FieldName("LCHARNAME"), FieldType('C'), FieldWidth(100)]
        public string Lcharname
        {
            get { return lcharname; }
            set { CheckStringData("Lcharname", value, 100); lcharname = value; }
        }

        private Int64 tarifcd;
        // <summary>
        // TARIFCD N(3)
        // </summary>
        [FieldName("TARIFCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Tarifcd
        {
            get { return tarifcd; }
            set { CheckIntegerData("Tarifcd", value, 3); tarifcd = value; }
        }

        private string tarifnm;
        // <summary>
        // TARIFNM C(100)
        // </summary>
        [FieldName("TARIFNM"), FieldType('C'), FieldWidth(100)]
        public string Tarifnm
        {
            get { return tarifnm; }
            set { CheckStringData("Tarifnm", value, 100); tarifnm = value; }
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
        // VALUEDESC C(50)
        // </summary>
        [FieldName("VALUEDESC"), FieldType('C'), FieldWidth(50)]
        public string Valuedesc
        {
            get { return valuedesc; }
            set { CheckStringData("Valuedesc", value, 50); valuedesc = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("LCHARCD")) Lcharcd = Convert.ToInt64(ADataRow["LCHARCD"]); else Lcharcd = 0;
            if (ADataRow.Table.Columns.Contains("LCHARNAME")) Lcharname = ADataRow["LCHARNAME"].ToString(); else Lcharname = "";
            if (ADataRow.Table.Columns.Contains("TARIFCD")) Tarifcd = Convert.ToInt64(ADataRow["TARIFCD"]); else Tarifcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFNM")) Tarifnm = ADataRow["TARIFNM"].ToString(); else Tarifnm = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToInt64(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("VALUEDESC")) Valuedesc = ADataRow["VALUEDESC"].ToString(); else Valuedesc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            LcharsRecord retValue = new LcharsRecord();
            retValue.Lshet = this.Lshet;
            retValue.Lcharcd = this.Lcharcd;
            retValue.Lcharname = this.Lcharname;
            retValue.Tarifcd = this.Tarifcd;
            retValue.Tarifnm = this.Tarifnm;
            retValue.Value_ = this.Value_;
            retValue.Valuedesc = this.Valuedesc;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LCHARS (LSHET, LCHARCD, LCHARNAME, TARIFCD, TARIFNM, VALUE, VALUEDESC, DATE) VALUES ('{0}', {1}, '{2}', {3}, '{4}', {5}, '{6}', CTOD('{7}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Lcharcd.ToString(), String.IsNullOrEmpty(Lcharname) ? "" : Lcharname.Trim(), Tarifcd.ToString(), String.IsNullOrEmpty(Tarifnm) ? "" : Tarifnm.Trim(), Value_.ToString(), String.IsNullOrEmpty(Valuedesc) ? "" : Valuedesc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}