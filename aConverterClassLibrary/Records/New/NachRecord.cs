// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("NACH.DBF")]
    public class NachRecord: AbstractRecord
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
        // MONTH N(6)
        // </summary>
        [FieldName("MONTH"), FieldType('N'), FieldWidth(6)]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 6); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(8)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(8)]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 8); year = value; }
        }

        private Int64 month2;
        // <summary>
        // MONTH2 N(6)
        // </summary>
        [FieldName("MONTH2"), FieldType('N'), FieldWidth(6)]
        public Int64 Month2
        {
            get { return month2; }
            set { CheckIntegerData("Month2", value, 6); month2 = value; }
        }

        private Int64 year2;
        // <summary>
        // YEAR2 N(8)
        // </summary>
        [FieldName("YEAR2"), FieldType('N'), FieldWidth(8)]
        public Int64 Year2
        {
            get { return year2; }
            set { CheckIntegerData("Year2", value, 8); year2 = value; }
        }

        private decimal fnath;
        // <summary>
        // FNATH N(15,2)
        // </summary>
        [FieldName("FNATH"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Fnath
        {
            get { return fnath; }
            set { CheckDecimalData("Fnath", value, 15, 2); fnath = value; }
        }

        private decimal prochl;
        // <summary>
        // PROCHL N(15,2)
        // </summary>
        [FieldName("PROCHL"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Prochl
        {
            get { return prochl; }
            set { CheckDecimalData("Prochl", value, 15, 2); prochl = value; }
        }

        private Int64 regimcd;
        // <summary>
        // REGIMCD N(8)
        // </summary>
        [FieldName("REGIMCD"), FieldType('N'), FieldWidth(8)]
        public Int64 Regimcd
        {
            get { return regimcd; }
            set { CheckIntegerData("Regimcd", value, 8); regimcd = value; }
        }

        private string regimname;
        // <summary>
        // REGIMNAME C(50)
        // </summary>
        [FieldName("REGIMNAME"), FieldType('C'), FieldWidth(50)]
        public string Regimname
        {
            get { return regimname; }
            set { CheckStringData("Regimname", value, 50); regimname = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Month2 = Convert.ToInt64(ADataRow["MONTH2"]);
            Year2 = Convert.ToInt64(ADataRow["YEAR2"]);
            Fnath = Convert.ToDecimal(ADataRow["FNATH"]);
            Prochl = Convert.ToDecimal(ADataRow["PROCHL"]);
            Regimcd = Convert.ToInt64(ADataRow["REGIMCD"]);
            Regimname = ADataRow["REGIMNAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO NACH (LSHET, MONTH, YEAR, MONTH2, YEAR2, FNATH, PROCHL, REGIMCD, REGIMNAME, SERVICECD, SERVICENAM) VALUES ('{0}', {1}, {2}, {3}, {4}, {5}, {6}, {7}, '{8}', {9}, '{10}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Month.ToString(), Year.ToString(), Month2.ToString(), Year2.ToString(), Fnath.ToString().Replace(',','.'), Prochl.ToString().Replace(',','.'), Regimcd.ToString(), String.IsNullOrEmpty(Regimname) ? "" : Regimname.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim());
           return rs;
        }
    }
}
