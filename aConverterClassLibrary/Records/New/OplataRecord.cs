// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("OPLATA.DBF")]
    public class OplataRecord: AbstractRecord
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

        private Int64 month;
        // <summary>
        // MONTH N(4)
        // </summary>
        [FieldName("MONTH"), FieldType('N'), FieldWidth(4)]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 4); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(6)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(6)]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 6); year = value; }
        }

        private decimal summa;
        // <summary>
        // SUMMA N(15,2)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 15, 2); summa = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        private DateTime date_vv;
        // <summary>
        // DATE_VV D(8)
        // </summary>
        [FieldName("DATE_VV"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_vv
        {
            get { return date_vv; }
            set {  date_vv = value; }
        }

        private DateTime datetind;
        // <summary>
        // DATETIND D(8)
        // </summary>
        [FieldName("DATETIND"), FieldType('D'), FieldWidth(8)]
        public DateTime Datetind
        {
            get { return datetind; }
            set {  datetind = value; }
        }

        private Int64 sourcecd;
        // <summary>
        // SOURCECD N(8)
        // </summary>
        [FieldName("SOURCECD"), FieldType('N'), FieldWidth(8)]
        public Int64 Sourcecd
        {
            get { return sourcecd; }
            set { CheckIntegerData("Sourcecd", value, 8); sourcecd = value; }
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

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(7)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(7)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 7); servicecd = value; }
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

        private string prim_;
        // <summary>
        // PRIM_ C(100)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 100); prim_ = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Summa = Convert.ToDecimal(ADataRow["SUMMA"]);
            Date = Convert.ToDateTime(ADataRow["DATE"]);
            Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]);
            Datetind = Convert.ToDateTime(ADataRow["DATETIND"]);
            Sourcecd = Convert.ToInt64(ADataRow["SOURCECD"]);
            Sourcename = ADataRow["SOURCENAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
            Prim_ = ADataRow["PRIM_"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO OPLATA (LSHET, MONTH, YEAR, SUMMA, DATE, DATE_VV, DATETIND, SOURCECD, SOURCENAME, SERVICECD, SERVICENAM, PRIM_) VALUES ('{0}', {1}, {2}, {3}, CTOD('{4}'), CTOD('{5}'), CTOD('{6}'), {7}, '{8}', {9}, '{10}', '{11}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Month.ToString(), Year.ToString(), Summa.ToString().Replace(',','.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), Date_vv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_vv.Month, Date_vv.Day, Date_vv.Year), Datetind == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datetind.Month, Datetind.Day, Datetind.Year), Sourcecd.ToString(), String.IsNullOrEmpty(Sourcename) ? "" : Sourcename.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim());
           return rs;
        }
    }
}
