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
    [TableName("CHARS.DBF")]
    public partial class CharsRecord : AbstractRecord
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

        private Int64 charcd;
        // <summary>
        // CHARCD N(3)
        // </summary>
        [FieldName("CHARCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Charcd
        {
            get { return charcd; }
            set { CheckIntegerData("Charcd", value, 3); charcd = value; }
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
        // VALUE N(8,2)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Value_
        {
            get { return value_; }
            set { CheckDecimalData("Value_", value, 8, 2); value_ = value; }
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
            if (ADataRow.Table.Columns.Contains("LSHET_KOD")) Lshet_kod = ADataRow["LSHET_KOD"].ToString(); else Lshet_kod = "";
            if (ADataRow.Table.Columns.Contains("CHARCD")) Charcd = Convert.ToInt64(ADataRow["CHARCD"]); else Charcd = 0;
            if (ADataRow.Table.Columns.Contains("CHARNAME")) Charname = ADataRow["CHARNAME"].ToString(); else Charname = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToDecimal(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CharsRecord retValue = new CharsRecord();
            retValue.Lshet_kod = this.Lshet_kod;
            retValue.Charcd = this.Charcd;
            retValue.Charname = this.Charname;
            retValue.Value_ = this.Value_;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CHARS (LSHET_KOD, CHARCD, CHARNAME, VALUE, DATE) VALUES ('{0}', {1}, '{2}', {3}, CTOD('{4}'))", String.IsNullOrEmpty(Lshet_kod) ? "" : Lshet_kod.Trim(), Charcd.ToString(), String.IsNullOrEmpty(Charname) ? "" : Charname.Trim(), Value_.ToString().Replace(',', '.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}