// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _048_Rgmek
{
    [TableName("LsChars.DBF")]
    public partial class LscharsRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(19)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(19)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 19); lshet = value; }
        }

        private string charcd;
        // <summary>
        // CHARCD C(36)
        // </summary>
        [FieldName("CHARCD"), FieldType('C'), FieldWidth(36)]
        public string Charcd
        {
            get { return charcd; }
            set { CheckStringData("Charcd", value, 36); charcd = value; }
        }

        private string charenm;
        // <summary>
        // CHARENM C(60)
        // </summary>
        [FieldName("CHARENM"), FieldType('C'), FieldWidth(60)]
        public string Charenm
        {
            get { return charenm; }
            set { CheckStringData("Charenm", value, 60); charenm = value; }
        }

        private decimal value_n;
        // <summary>
        // VALUE_N N(15,3)
        // </summary>
        [FieldName("VALUE_N"), FieldType('N'), FieldWidth(15), FieldDec(3)]
        public decimal Value_n
        {
            get { return value_n; }
            set { CheckDecimalData("Value_n", value, 15, 3); value_n = value; }
        }

        private string value_s;
        // <summary>
        // VALUE_S C(100)
        // </summary>
        [FieldName("VALUE_S"), FieldType('C'), FieldWidth(100)]
        public string Value_s
        {
            get { return value_s; }
            set { CheckStringData("Value_s", value, 100); value_s = value; }
        }

        private DateTime value_d;
        // <summary>
        // VALUE_D D(8)
        // </summary>
        [FieldName("VALUE_D"), FieldType('D'), FieldWidth(8)]
        public DateTime Value_d
        {
            get { return value_d; }
            set { value_d = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("CHARCD")) Charcd = ADataRow["CHARCD"].ToString(); else Charcd = "";
            if (ADataRow.Table.Columns.Contains("CHARENM")) Charenm = ADataRow["CHARENM"].ToString(); else Charenm = "";
            if (ADataRow.Table.Columns.Contains("VALUE_N")) Value_n = Convert.ToDecimal(ADataRow["VALUE_N"]); else Value_n = 0;
            if (ADataRow.Table.Columns.Contains("VALUE_S")) Value_s = ADataRow["VALUE_S"].ToString(); else Value_s = "";
            if (ADataRow.Table.Columns.Contains("VALUE_D")) Value_d = Convert.ToDateTime(ADataRow["VALUE_D"]); else Value_d = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SETUPDATE")) Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]); else Setupdate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            LscharsRecord retValue = new LscharsRecord();
            retValue.Lshet = this.Lshet;
            retValue.Charcd = this.Charcd;
            retValue.Charenm = this.Charenm;
            retValue.Value_n = this.Value_n;
            retValue.Value_s = this.Value_s;
            retValue.Value_d = this.Value_d;
            retValue.Date = this.Date;
            retValue.Setupdate = this.Setupdate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LsChars (LSHET, CHARCD, CHARENM, VALUE_N, VALUE_S, VALUE_D, DATE, SETUPDATE) VALUES ('{0}', '{1}', '{2}', {3}, '{4}', CTOD('{5}'), CTOD('{6}'), CTOD('{7}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Charcd) ? "" : Charcd.Trim(), String.IsNullOrEmpty(Charenm) ? "" : Charenm.Trim(), Value_n.ToString().Replace(',', '.'), String.IsNullOrEmpty(Value_s) ? "" : Value_s.Trim(), Value_d == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Value_d.Month, Value_d.Day, Value_d.Year), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year));
            return rs;
        }
    }
}