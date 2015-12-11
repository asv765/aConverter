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
    [TableName("OPLATA.DBF")]
    public partial class OplataRecord : AbstractRecord
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
        // MONTH N(3)
        // </summary>
        [FieldName("MONTH"), FieldType('N'), FieldWidth(3)]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 3); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(5)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(5)]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 5); year = value; }
        }

        private decimal summa;
        // <summary>
        // SUMMA N(18,2)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(18), FieldDec(2)]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 18, 2); summa = value; }
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

        private Int64 sourcecd;
        // <summary>
        // SOURCECD N(7)
        // </summary>
        [FieldName("SOURCECD"), FieldType('N'), FieldWidth(7)]
        public Int64 Sourcecd
        {
            get { return sourcecd; }
            set { CheckIntegerData("Sourcecd", value, 7); sourcecd = value; }
        }

        private string sourcename;
        // <summary>
        // SOURCENAME C(50)
        // </summary>
        [FieldName("SOURCENAME"), FieldType('C'), FieldWidth(50)]
        public string Sourcename
        {
            get { return sourcename; }
            set { CheckStringData("Sourcename", value, 50); sourcename = value; }
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
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENAM")) Servicenam = ADataRow["SERVICENAM"].ToString(); else Servicenam = "";
            if (ADataRow.Table.Columns.Contains("SOURCECD")) Sourcecd = Convert.ToInt64(ADataRow["SOURCECD"]); else Sourcecd = 0;
            if (ADataRow.Table.Columns.Contains("SOURCENAME")) Sourcename = ADataRow["SOURCENAME"].ToString(); else Sourcename = "";
            if (ADataRow.Table.Columns.Contains("DATE_VV")) Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]); else Date_vv = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOCNAME")) Docname = ADataRow["DOCNAME"].ToString(); else Docname = "";
            if (ADataRow.Table.Columns.Contains("DOCNUMBER")) Docnumber = ADataRow["DOCNUMBER"].ToString(); else Docnumber = "";
            if (ADataRow.Table.Columns.Contains("DOCDATE")) Docdate = Convert.ToDateTime(ADataRow["DOCDATE"]); else Docdate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            OplataRecord retValue = new OplataRecord();
            retValue.Lshet = this.Lshet;
            retValue.Documentcd = this.Documentcd;
            retValue.Month = this.Month;
            retValue.Year = this.Year;
            retValue.Summa = this.Summa;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenam = this.Servicenam;
            retValue.Sourcecd = this.Sourcecd;
            retValue.Sourcename = this.Sourcename;
            retValue.Date_vv = this.Date_vv;
            retValue.Docname = this.Docname;
            retValue.Docnumber = this.Docnumber;
            retValue.Docdate = this.Docdate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO OPLATA (LSHET, DOCUMENTCD, MONTH, YEAR, SUMMA, SERVICECD, SERVICENAM, SOURCECD, SOURCENAME, DATE_VV, DOCNAME, DOCNUMBER, DOCDATE) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, '{6}', {7}, '{8}', CTOD('{9}'), '{10}', '{11}', CTOD('{12}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Documentcd) ? "" : Documentcd.Trim(), Month.ToString(), Year.ToString(), Summa.ToString().Replace(',', '.'), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim(), Sourcecd.ToString(), String.IsNullOrEmpty(Sourcename) ? "" : Sourcename.Trim(), Date_vv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_vv.Month, Date_vv.Day, Date_vv.Year), String.IsNullOrEmpty(Docname) ? "" : Docname.Trim(), String.IsNullOrEmpty(Docnumber) ? "" : Docnumber.Trim(), Docdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Docdate.Month, Docdate.Day, Docdate.Year));
            return rs;
        }
    }
}