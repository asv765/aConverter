// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _044_TeplComp
{
    [TableName("NACHOPL.DBF")]
    public partial class NachoplRecord : AbstractRecord
    {
        private string grkod;
        // <summary>
        // GRKOD C(8)
        // </summary>
        [FieldName("GRKOD"), FieldType('C'), FieldWidth(8)]
        public string Grkod
        {
            get { return grkod; }
            set { CheckStringData("Grkod", value, 8); grkod = value; }
        }

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

        private Int64 month2;
        // <summary>
        // MONTH2 N(3)
        // </summary>
        [FieldName("MONTH2"), FieldType('N'), FieldWidth(3)]
        public Int64 Month2
        {
            get { return month2; }
            set { CheckIntegerData("Month2", value, 3); month2 = value; }
        }

        private Int64 year2;
        // <summary>
        // YEAR2 N(5)
        // </summary>
        [FieldName("YEAR2"), FieldType('N'), FieldWidth(5)]
        public Int64 Year2
        {
            get { return year2; }
            set { CheckIntegerData("Year2", value, 5); year2 = value; }
        }

        private decimal bdebet;
        // <summary>
        // BDEBET N(18,4)
        // </summary>
        [FieldName("BDEBET"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Bdebet
        {
            get { return bdebet; }
            set { CheckDecimalData("Bdebet", value, 18, 4); bdebet = value; }
        }

        private decimal fnathall;
        // <summary>
        // FNATHALL N(18,4)
        // </summary>
        [FieldName("FNATHALL"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Fnathall
        {
            get { return fnathall; }
            set { CheckDecimalData("Fnathall", value, 18, 4); fnathall = value; }
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

        private decimal lgnach;
        // <summary>
        // LGNACH N(18,4)
        // </summary>
        [FieldName("LGNACH"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Lgnach
        {
            get { return lgnach; }
            set { CheckDecimalData("Lgnach", value, 18, 4); lgnach = value; }
        }

        private decimal oplata;
        // <summary>
        // OPLATA N(18,4)
        // </summary>
        [FieldName("OPLATA"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Oplata
        {
            get { return oplata; }
            set { CheckDecimalData("Oplata", value, 18, 4); oplata = value; }
        }

        private decimal edebet;
        // <summary>
        // EDEBET N(18,4)
        // </summary>
        [FieldName("EDEBET"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Edebet
        {
            get { return edebet; }
            set { CheckDecimalData("Edebet", value, 18, 4); edebet = value; }
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

        private string servicenm;
        // <summary>
        // SERVICENM C(50)
        // </summary>
        [FieldName("SERVICENM"), FieldType('C'), FieldWidth(50)]
        public string Servicenm
        {
            get { return servicenm; }
            set { CheckStringData("Servicenm", value, 50); servicenm = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("GRKOD")) Grkod = ADataRow["GRKOD"].ToString(); else Grkod = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("MONTH")) Month = Convert.ToInt64(ADataRow["MONTH"]); else Month = 0;
            if (ADataRow.Table.Columns.Contains("YEAR")) Year = Convert.ToInt64(ADataRow["YEAR"]); else Year = 0;
            if (ADataRow.Table.Columns.Contains("MONTH2")) Month2 = Convert.ToInt64(ADataRow["MONTH2"]); else Month2 = 0;
            if (ADataRow.Table.Columns.Contains("YEAR2")) Year2 = Convert.ToInt64(ADataRow["YEAR2"]); else Year2 = 0;
            if (ADataRow.Table.Columns.Contains("BDEBET")) Bdebet = Convert.ToDecimal(ADataRow["BDEBET"]); else Bdebet = 0;
            if (ADataRow.Table.Columns.Contains("FNATHALL")) Fnathall = Convert.ToDecimal(ADataRow["FNATHALL"]); else Fnathall = 0;
            if (ADataRow.Table.Columns.Contains("FNATH")) Fnath = Convert.ToDecimal(ADataRow["FNATH"]); else Fnath = 0;
            if (ADataRow.Table.Columns.Contains("PROCHL")) Prochl = Convert.ToDecimal(ADataRow["PROCHL"]); else Prochl = 0;
            if (ADataRow.Table.Columns.Contains("LGNACH")) Lgnach = Convert.ToDecimal(ADataRow["LGNACH"]); else Lgnach = 0;
            if (ADataRow.Table.Columns.Contains("OPLATA")) Oplata = Convert.ToDecimal(ADataRow["OPLATA"]); else Oplata = 0;
            if (ADataRow.Table.Columns.Contains("EDEBET")) Edebet = Convert.ToDecimal(ADataRow["EDEBET"]); else Edebet = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
        }

        public override AbstractRecord Clone()
        {
            NachoplRecord retValue = new NachoplRecord();
            retValue.Grkod = this.Grkod;
            retValue.Lshet = this.Lshet;
            retValue.Month = this.Month;
            retValue.Year = this.Year;
            retValue.Month2 = this.Month2;
            retValue.Year2 = this.Year2;
            retValue.Bdebet = this.Bdebet;
            retValue.Fnathall = this.Fnathall;
            retValue.Fnath = this.Fnath;
            retValue.Prochl = this.Prochl;
            retValue.Lgnach = this.Lgnach;
            retValue.Oplata = this.Oplata;
            retValue.Edebet = this.Edebet;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO NACHOPL (GRKOD, LSHET, MONTH, YEAR, MONTH2, YEAR2, BDEBET, FNATHALL, FNATH, PROCHL, LGNACH, OPLATA, EDEBET, SERVICECD, SERVICENM) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, '{14}')", String.IsNullOrEmpty(Grkod) ? "" : Grkod.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Month.ToString(), Year.ToString(), Month2.ToString(), Year2.ToString(), Bdebet.ToString().Replace(',', '.'), Fnathall.ToString().Replace(',', '.'), Fnath.ToString().Replace(',', '.'), Prochl.ToString().Replace(',', '.'), Lgnach.ToString().Replace(',', '.'), Oplata.ToString().Replace(',', '.'), Edebet.ToString().Replace(',', '.'), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim());
            return rs;
        }
    }
}