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
    [TableName("AB_TARIF.DBF")]
    public partial class Ab_tarifRecord : AbstractRecord
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

        private Int64 nachcd;
        // <summary>
        // NACHCD N(6)
        // </summary>
        [FieldName("NACHCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Nachcd
        {
            get { return nachcd; }
            set { CheckIntegerData("Nachcd", value, 6); nachcd = value; }
        }

        private string nachname;
        // <summary>
        // NACHNAME C(35)
        // </summary>
        [FieldName("NACHNAME"), FieldType('C'), FieldWidth(35)]
        public string Nachname
        {
            get { return nachname; }
            set { CheckStringData("Nachname", value, 35); nachname = value; }
        }

        private Int64 grtarifcd;
        // <summary>
        // GRTARIFCD N(6)
        // </summary>
        [FieldName("GRTARIFCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Grtarifcd
        {
            get { return grtarifcd; }
            set { CheckIntegerData("Grtarifcd", value, 6); grtarifcd = value; }
        }

        private Int64 tarifcd;
        // <summary>
        // TARIFCD N(6)
        // </summary>
        [FieldName("TARIFCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Tarifcd
        {
            get { return tarifcd; }
            set { CheckIntegerData("Tarifcd", value, 6); tarifcd = value; }
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
        // VALUE N(5)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(5)]
        public Int64 Value_
        {
            get { return value_; }
            set { CheckIntegerData("Value_", value, 5); value_ = value; }
        }

        private string valuedesc;
        // <summary>
        // VALUEDESC C(100)
        // </summary>
        [FieldName("VALUEDESC"), FieldType('C'), FieldWidth(100)]
        public string Valuedesc
        {
            get { return valuedesc; }
            set { CheckStringData("Valuedesc", value, 100); valuedesc = value; }
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
            if (ADataRow.Table.Columns.Contains("NACHCD")) Nachcd = Convert.ToInt64(ADataRow["NACHCD"]); else Nachcd = 0;
            if (ADataRow.Table.Columns.Contains("NACHNAME")) Nachname = ADataRow["NACHNAME"].ToString(); else Nachname = "";
            if (ADataRow.Table.Columns.Contains("GRTARIFCD")) Grtarifcd = Convert.ToInt64(ADataRow["GRTARIFCD"]); else Grtarifcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFCD")) Tarifcd = Convert.ToInt64(ADataRow["TARIFCD"]); else Tarifcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFNM")) Tarifnm = ADataRow["TARIFNM"].ToString(); else Tarifnm = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToInt64(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("VALUEDESC")) Valuedesc = ADataRow["VALUEDESC"].ToString(); else Valuedesc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            Ab_tarifRecord retValue = new Ab_tarifRecord();
            retValue.Lshet = this.Lshet;
            retValue.Nachcd = this.Nachcd;
            retValue.Nachname = this.Nachname;
            retValue.Grtarifcd = this.Grtarifcd;
            retValue.Tarifcd = this.Tarifcd;
            retValue.Tarifnm = this.Tarifnm;
            retValue.Value_ = this.Value_;
            retValue.Valuedesc = this.Valuedesc;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO AB_TARIF (LSHET, NACHCD, NACHNAME, GRTARIFCD, TARIFCD, TARIFNM, VALUE, VALUEDESC, DATE) VALUES ('{0}', {1}, '{2}', {3}, {4}, '{5}', {6}, '{7}', CTOD('{8}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Nachcd.ToString(), String.IsNullOrEmpty(Nachname) ? "" : Nachname.Trim(), Grtarifcd.ToString(), Tarifcd.ToString(), String.IsNullOrEmpty(Tarifnm) ? "" : Tarifnm.Trim(), Value_.ToString(), String.IsNullOrEmpty(Valuedesc) ? "" : Valuedesc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}