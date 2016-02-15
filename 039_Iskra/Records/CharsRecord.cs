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
    [TableName("CHARS.DBF")]
    public partial class CharsRecord : AbstractRecord
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

        private Int64 charcd;
        // <summary>
        // CHARCD N(6)
        // </summary>
        [FieldName("CHARCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Charcd
        {
            get { return charcd; }
            set { CheckIntegerData("Charcd", value, 6); charcd = value; }
        }

        private string charname;
        // <summary>
        // CHARNAME C(50)
        // </summary>
        [FieldName("CHARNAME"), FieldType('C'), FieldWidth(50)]
        public string Charname
        {
            get { return charname; }
            set { CheckStringData("Charname", value, 50); charname = value; }
        }

        private decimal value_;
        // <summary>
        // VALUE N(10,2)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Value_
        {
            get { return value_; }
            set { CheckDecimalData("Value_", value, 10, 2); value_ = value; }
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
            if (ADataRow.Table.Columns.Contains("CHARCD")) Charcd = Convert.ToInt64(ADataRow["CHARCD"]); else Charcd = 0;
            if (ADataRow.Table.Columns.Contains("CHARNAME")) Charname = ADataRow["CHARNAME"].ToString(); else Charname = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToDecimal(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CharsRecord retValue = new CharsRecord();
            retValue.Lshet = this.Lshet;
            retValue.Charcd = this.Charcd;
            retValue.Charname = this.Charname;
            retValue.Value_ = this.Value_;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CHARS (LSHET, CHARCD, CHARNAME, VALUE, DATE) VALUES ('{0}', {1}, '{2}', {3}, CTOD('{4}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Charcd.ToString(), String.IsNullOrEmpty(Charname) ? "" : Charname.Trim(), Value_.ToString().Replace(',', '.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}