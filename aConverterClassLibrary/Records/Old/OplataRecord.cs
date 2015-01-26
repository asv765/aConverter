// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("OPLATA.DBF")]
    [Index("LSHET", "LSHET"), Index("MONTH", "MONTH(DATE_VV)"), Index("YEAR", "YEAR(DATE_VV)")]
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
        // SUMMA N(13,2)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 13, 2); summa = value; }
        }

        private decimal oldsth;
        // <summary>
        // OLDSTH N(13,3)
        // </summary>
        [FieldName("OLDSTH"), FieldType('N'), FieldWidth(13), FieldDec(3)]
        public decimal Oldsth
        {
            get { return oldsth; }
            set { CheckDecimalData("Oldsth", value, 13, 3); oldsth = value; }
        }

        private decimal laststh;
        // <summary>
        // LASTSTH N(13,3)
        // </summary>
        [FieldName("LASTSTH"), FieldType('N'), FieldWidth(13), FieldDec(3)]
        public decimal Laststh
        {
            get { return laststh; }
            set { CheckDecimalData("Laststh", value, 13, 3); laststh = value; }
        }

        private decimal ob_em;
        // <summary>
        // OB_EM N(13,3)
        // </summary>
        [FieldName("OB_EM"), FieldType('N'), FieldWidth(13), FieldDec(3)]
        public decimal Ob_em
        {
            get { return ob_em; }
            set { CheckDecimalData("Ob_em", value, 13, 3); ob_em = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D
        // </summary>
        [FieldName("DATE"), FieldType('D')]
        public DateTime Date
        {
            get { return date; }
            set { CheckData("Date", value); date = value; }
        }

        private DateTime date_vv;
        // <summary>
        // DATE_VV D
        // </summary>
        [FieldName("DATE_VV"), FieldType('D')]
        public DateTime Date_vv
        {
            get { return date_vv; }
            set { CheckData("Date_vv", value); date_vv = value; }
        }

        private DateTime datetind;
        // <summary>
        // DATETIND D
        // </summary>
        [FieldName("DATETIND"), FieldType('D')]
        public DateTime Datetind
        {
            get { return datetind; }
            set { CheckData("Datetind", value); datetind = value; }
        }

        private Int64 sourcecd;
        // <summary>
        // SOURCECD N(6)
        // </summary>
        [FieldName("SOURCECD"), FieldType('N'), FieldWidth(6)]
        public Int64 Sourcecd
        {
            get { return sourcecd; }
            set { CheckIntegerData("Sourcecd", value, 6); sourcecd = value; }
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
        // SERVICECD N(5)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(5)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 5); servicecd = value; }
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
        // PRIM_ C(30)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(30)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 30); prim_ = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Summa = Convert.ToDecimal(ADataRow["SUMMA"]);
            Oldsth = Convert.ToDecimal(ADataRow["OLDSTH"]);
            Laststh = Convert.ToDecimal(ADataRow["LASTSTH"]);
            Ob_em = Convert.ToDecimal(ADataRow["OB_EM"]);
            Date = Convert.ToDateTime(ADataRow["DATE"]);
            Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]);
            Datetind = Convert.ToDateTime(ADataRow["DATETIND"]);
            Sourcecd = Convert.ToInt64(ADataRow["SOURCECD"]);
            Sourcename = ADataRow["SOURCENAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
            Prim_ = ADataRow["PRIM_"].ToString();
        }
    }
}

