// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _038_Murmino
{
    [TableName("NACH.DBF")]
    public partial class NachRecord : AbstractRecord
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

        private string documentcd;
        // <summary>
        // DOCUMENTCD C(20)
        // </summary>
        [FieldName("DOCUMENTCD"), FieldType('C'), FieldWidth(20)]
        public string Documentcd
        {
            get { return documentcd; }
            set { CheckStringData("Documentcd", value, 20); documentcd = value; }
        }

        private Int64 month;
        // <summary>
        // MONTH N(5)
        // </summary>
        [FieldName("MONTH"), FieldType('N'), FieldWidth(5)]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 5); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(7)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(7)]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 7); year = value; }
        }

        private Int64 month2;
        // <summary>
        // MONTH2 N(5)
        // </summary>
        [FieldName("MONTH2"), FieldType('N'), FieldWidth(5)]
        public Int64 Month2
        {
            get { return month2; }
            set { CheckIntegerData("Month2", value, 5); month2 = value; }
        }

        private Int64 year2;
        // <summary>
        // YEAR2 N(7)
        // </summary>
        [FieldName("YEAR2"), FieldType('N'), FieldWidth(7)]
        public Int64 Year2
        {
            get { return year2; }
            set { CheckIntegerData("Year2", value, 7); year2 = value; }
        }

        private decimal fnath;
        // <summary>
        // FNATH N(18,4)
        // </summary>
        [FieldName("FNATH"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Fnath
        {
            get { return fnath; }
            set { CheckDecimalData("Fnath", value, 18, 4); fnath = value; }
        }

        private decimal prochl;
        // <summary>
        // PROCHL N(18,4)
        // </summary>
        [FieldName("PROCHL"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Prochl
        {
            get { return prochl; }
            set { CheckDecimalData("Prochl", value, 18, 4); prochl = value; }
        }

        private decimal volume;
        // <summary>
        // VOLUME N(18,4)
        // </summary>
        [FieldName("VOLUME"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Volume
        {
            get { return volume; }
            set { CheckDecimalData("Volume", value, 18, 4); volume = value; }
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

        private string servicenam;
        // <summary>
        // SERVICENAM C(50)
        // </summary>
        [FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50)]
        public string Servicenam
        {
            get { return servicenam; }
            set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        }

        private DateTime date_vv;
        // <summary>
        // DATE_VV D(8)
        // </summary>
        [FieldName("DATE_VV"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_vv
        {
            get { return date_vv; }
            set { date_vv = value; }
        }

        private string docname;
        // <summary>
        // DOCNAME C(150)
        // </summary>
        [FieldName("DOCNAME"), FieldType('C'), FieldWidth(150)]
        public string Docname
        {
            get { return docname; }
            set { CheckStringData("Docname", value, 150); docname = value; }
        }

        private string docnumber;
        // <summary>
        // DOCNUMBER C(10)
        // </summary>
        [FieldName("DOCNUMBER"), FieldType('C'), FieldWidth(10)]
        public string Docnumber
        {
            get { return docnumber; }
            set { CheckStringData("Docnumber", value, 10); docnumber = value; }
        }

        private DateTime docdate;
        // <summary>
        // DOCDATE D(8)
        // </summary>
        [FieldName("DOCDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Docdate
        {
            get { return docdate; }
            set { docdate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("DOCUMENTCD")) Documentcd = ADataRow["DOCUMENTCD"].ToString(); else Documentcd = "";
            if (ADataRow.Table.Columns.Contains("MONTH")) Month = Convert.ToInt64(ADataRow["MONTH"]); else Month = 0;
            if (ADataRow.Table.Columns.Contains("YEAR")) Year = Convert.ToInt64(ADataRow["YEAR"]); else Year = 0;
            if (ADataRow.Table.Columns.Contains("MONTH2")) Month2 = Convert.ToInt64(ADataRow["MONTH2"]); else Month2 = 0;
            if (ADataRow.Table.Columns.Contains("YEAR2")) Year2 = Convert.ToInt64(ADataRow["YEAR2"]); else Year2 = 0;
            if (ADataRow.Table.Columns.Contains("FNATH")) Fnath = Convert.ToDecimal(ADataRow["FNATH"]); else Fnath = 0;
            if (ADataRow.Table.Columns.Contains("PROCHL")) Prochl = Convert.ToDecimal(ADataRow["PROCHL"]); else Prochl = 0;
            if (ADataRow.Table.Columns.Contains("VOLUME")) Volume = Convert.ToDecimal(ADataRow["VOLUME"]); else Volume = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENAM")) Servicenam = ADataRow["SERVICENAM"].ToString(); else Servicenam = "";
            if (ADataRow.Table.Columns.Contains("DATE_VV")) Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]); else Date_vv = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOCNAME")) Docname = ADataRow["DOCNAME"].ToString(); else Docname = "";
            if (ADataRow.Table.Columns.Contains("DOCNUMBER")) Docnumber = ADataRow["DOCNUMBER"].ToString(); else Docnumber = "";
            if (ADataRow.Table.Columns.Contains("DOCDATE")) Docdate = Convert.ToDateTime(ADataRow["DOCDATE"]); else Docdate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            NachRecord retValue = new NachRecord();
            retValue.Lshet = this.Lshet;
            retValue.Documentcd = this.Documentcd;
            retValue.Month = this.Month;
            retValue.Year = this.Year;
            retValue.Month2 = this.Month2;
            retValue.Year2 = this.Year2;
            retValue.Fnath = this.Fnath;
            retValue.Prochl = this.Prochl;
            retValue.Volume = this.Volume;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenam = this.Servicenam;
            retValue.Date_vv = this.Date_vv;
            retValue.Docname = this.Docname;
            retValue.Docnumber = this.Docnumber;
            retValue.Docdate = this.Docdate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO NACH (LSHET, DOCUMENTCD, MONTH, YEAR, MONTH2, YEAR2, FNATH, PROCHL, VOLUME, SERVICECD, SERVICENAM, DATE_VV, DOCNAME, DOCNUMBER, DOCDATE) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, '{10}', CTOD('{11}'), '{12}', '{13}', CTOD('{14}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Documentcd) ? "" : Documentcd.Trim(), Month.ToString(), Year.ToString(), Month2.ToString(), Year2.ToString(), Fnath.ToString().Replace(',', '.'), Prochl.ToString().Replace(',', '.'), Volume.ToString().Replace(',', '.'), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim(), Date_vv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_vv.Month, Date_vv.Day, Date_vv.Year), String.IsNullOrEmpty(Docname) ? "" : Docname.Trim(), String.IsNullOrEmpty(Docnumber) ? "" : Docnumber.Trim(), Docdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Docdate.Month, Docdate.Day, Docdate.Year));
            return rs;
        }
    }
}