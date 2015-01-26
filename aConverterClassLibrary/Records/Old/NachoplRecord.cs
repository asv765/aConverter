// ���� ������������ aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("NACHOPL.DBF")]
    [Index("LSHET", "LSHET")]
    public class NachoplRecord: AbstractRecord
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

        private Int64 month2;
        // <summary>
        // MONTH2 N(4)
        // </summary>
        [FieldName("MONTH2"), FieldType('N'), FieldWidth(4)]
        public Int64 Month2
        {
            get { return month2; }
            set { CheckIntegerData("Month2", value, 4); month2 = value; }
        }

        private Int64 year2;
        // <summary>
        // YEAR2 N(6)
        // </summary>
        [FieldName("YEAR2"), FieldType('N'), FieldWidth(6)]
        public Int64 Year2
        {
            get { return year2; }
            set { CheckIntegerData("Year2", value, 6); year2 = value; }
        }

        private decimal bdebet;
        // <summary>
        // BDEBET N(13,2)
        // </summary>
        [FieldName("BDEBET"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Bdebet
        {
            get { return bdebet; }
            set { CheckDecimalData("Bdebet", value, 13, 2); bdebet = value; }
        }

        private decimal fnath;
        // <summary>
        // FNATH N(13,2)
        // </summary>
        [FieldName("FNATH"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Fnath
        {
            get { return fnath; }
            set { CheckDecimalData("Fnath", value, 13, 2); fnath = value; }
        }

        private decimal lgnath;
        // <summary>
        // LGNATH N(13,2)
        // </summary>
        [FieldName("LGNATH"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Lgnath
        {
            get { return lgnath; }
            set { CheckDecimalData("Lgnath", value, 13, 2); lgnath = value; }
        }

        private decimal prochl;
        // <summary>
        // PROCHL N(13,2)
        // </summary>
        [FieldName("PROCHL"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Prochl
        {
            get { return prochl; }
            set { CheckDecimalData("Prochl", value, 13, 2); prochl = value; }
        }

        private decimal lgproch;
        // <summary>
        // LGPROCH N(13,2)
        // </summary>
        [FieldName("LGPROCH"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Lgproch
        {
            get { return lgproch; }
            set { CheckDecimalData("Lgproch", value, 13, 2); lgproch = value; }
        }

        private decimal oplata;
        // <summary>
        // OPLATA N(13,2)
        // </summary>
        [FieldName("OPLATA"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Oplata
        {
            get { return oplata; }
            set { CheckDecimalData("Oplata", value, 13, 2); oplata = value; }
        }

        private decimal edebet;
        // <summary>
        // EDEBET N(13,2)
        // </summary>
        [FieldName("EDEBET"), FieldType('N'), FieldWidth(13), FieldDec(2)]
        public decimal Edebet
        {
            get { return edebet; }
            set { CheckDecimalData("Edebet", value, 13, 2); edebet = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Month2 = Convert.ToInt64(ADataRow["MONTH2"]);
            Year2 = Convert.ToInt64(ADataRow["YEAR2"]);
            Bdebet = Convert.ToDecimal(ADataRow["BDEBET"]);
            Fnath = Convert.ToDecimal(ADataRow["FNATH"]);
            Lgnath = Convert.ToDecimal(ADataRow["LGNATH"]);
            Prochl = Convert.ToDecimal(ADataRow["PROCHL"]);
            Lgproch = Convert.ToDecimal(ADataRow["LGPROCH"]);
            Oplata = Convert.ToDecimal(ADataRow["OPLATA"]);
            Edebet = Convert.ToDecimal(ADataRow["EDEBET"]);
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
        }
    }
}

