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
    [TableName("ABNLSHET.DBF")]
    public partial class AbnlshetRecord : AbstractRecord
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

        private string orgcd;
        // <summary>
        // ORGCD C(36)
        // </summary>
        [FieldName("ORGCD"), FieldType('C'), FieldWidth(36)]
        public string Orgcd
        {
            get { return orgcd; }
            set { CheckStringData("Orgcd", value, 36); orgcd = value; }
        }

        private string orgnm;
        // <summary>
        // ORGNM C(100)
        // </summary>
        [FieldName("ORGNM"), FieldType('C'), FieldWidth(100)]
        public string Orgnm
        {
            get { return orgnm; }
            set { CheckStringData("Orgnm", value, 100); orgnm = value; }
        }

        private string lsh;
        // <summary>
        // LSH C(20)
        // </summary>
        [FieldName("LSH"), FieldType('C'), FieldWidth(20)]
        public string Lsh
        {
            get { return lsh; }
            set { CheckStringData("Lsh", value, 20); lsh = value; }
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
            if (ADataRow.Table.Columns.Contains("ORGCD")) Orgcd = ADataRow["ORGCD"].ToString(); else Orgcd = "";
            if (ADataRow.Table.Columns.Contains("ORGNM")) Orgnm = ADataRow["ORGNM"].ToString(); else Orgnm = "";
            if (ADataRow.Table.Columns.Contains("LSH")) Lsh = ADataRow["LSH"].ToString(); else Lsh = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            AbnlshetRecord retValue = new AbnlshetRecord();
            retValue.Lshet = this.Lshet;
            retValue.Orgcd = this.Orgcd;
            retValue.Orgnm = this.Orgnm;
            retValue.Lsh = this.Lsh;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABNLSHET (LSHET, ORGCD, ORGNM, LSH, DATE) VALUES ('{0}', '{1}', '{2}', '{3}', CTOD('{4}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Orgcd) ? "" : Orgcd.Trim(), String.IsNullOrEmpty(Orgnm) ? "" : Orgnm.Trim(), String.IsNullOrEmpty(Lsh) ? "" : Lsh.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}