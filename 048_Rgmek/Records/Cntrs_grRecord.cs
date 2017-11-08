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
    [TableName("CNTRS_GR.DBF")]
    public partial class Cntrs_grRecord : AbstractRecord
    {
        private string housecd;
        // <summary>
        // HOUSECD C(36)
        // </summary>
        [FieldName("HOUSECD"), FieldType('C'), FieldWidth(36)]
        public string Housecd
        {
            get { return housecd; }
            set { CheckStringData("Housecd", value, 36); housecd = value; }
        }

        private Int64 ulicakod;
        // <summary>
        // ULICAKOD N(5)
        // </summary>
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(5)]
        public Int64 Ulicakod
        {
            get { return ulicakod; }
            set { CheckIntegerData("Ulicakod", value, 5); ulicakod = value; }
        }

        private string ulicaname;
        // <summary>
        // ULICANAME C(50)
        // </summary>
        [FieldName("ULICANAME"), FieldType('C'), FieldWidth(50)]
        public string Ulicaname
        {
            get { return ulicaname; }
            set { CheckStringData("Ulicaname", value, 50); ulicaname = value; }
        }

        private string ndoma;
        // <summary>
        // NDOMA C(10)
        // </summary>
        [FieldName("NDOMA"), FieldType('C'), FieldWidth(10)]
        public string Ndoma
        {
            get { return ndoma; }
            set { CheckStringData("Ndoma", value, 10); ndoma = value; }
        }

        private string korpus;
        // <summary>
        // KORPUS C(3)
        // </summary>
        [FieldName("KORPUS"), FieldType('C'), FieldWidth(3)]
        public string Korpus
        {
            get { return korpus; }
            set { CheckStringData("Korpus", value, 3); korpus = value; }
        }

        private string korpustip;
        // <summary>
        // KORPUSTIP C(10)
        // </summary>
        [FieldName("KORPUSTIP"), FieldType('C'), FieldWidth(10)]
        public string Korpustip
        {
            get { return korpustip; }
            set { CheckStringData("Korpustip", value, 10); korpustip = value; }
        }

        private Int64 instplid;
        // <summary>
        // INSTPLID N(3)
        // </summary>
        [FieldName("INSTPLID"), FieldType('N'), FieldWidth(3)]
        public Int64 Instplid
        {
            get { return instplid; }
            set { CheckIntegerData("Instplid", value, 3); instplid = value; }
        }

        private string instplace;
        // <summary>
        // INSTPLACE C(100)
        // </summary>
        [FieldName("INSTPLACE"), FieldType('C'), FieldWidth(100)]
        public string Instplace
        {
            get { return instplace; }
            set { CheckStringData("Instplace", value, 100); instplace = value; }
        }

        private string counterid;
        // <summary>
        // COUNTERID C(36)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(36)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 36); counterid = value; }
        }

        private string name;
        // <summary>
        // NAME C(100)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(100)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 100); name = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(25)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(25)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 25); serialnum = value; }
        }

        private DateTime lastpov;
        // <summary>
        // LASTPOV D(8)
        // </summary>
        [FieldName("LASTPOV"), FieldType('D'), FieldWidth(8)]
        public DateTime Lastpov
        {
            get { return lastpov; }
            set { lastpov = value; }
        }

        private string cnttype;
        // <summary>
        // CNTTYPE C(36)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('C'), FieldWidth(36)]
        public string Cnttype
        {
            get { return cnttype; }
            set { CheckStringData("Cnttype", value, 36); cnttype = value; }
        }

        private string cntname;
        // <summary>
        // CNTNAME C(50)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(50)]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 50); cntname = value; }
        }

        private decimal precision;
        // <summary>
        // PRECISION N(4,2)
        // </summary>
        [FieldName("PRECISION"), FieldType('N'), FieldWidth(4), FieldDec(2)]
        public decimal Precision
        {
            get { return precision; }
            set { CheckDecimalData("Precision", value, 4, 2); precision = value; }
        }

        private string amperage;
        // <summary>
        // AMPERAGE C(15)
        // </summary>
        [FieldName("AMPERAGE"), FieldType('C'), FieldWidth(15)]
        public string Amperage
        {
            get { return amperage; }
            set { CheckStringData("Amperage", value, 15); amperage = value; }
        }

        private Int64 periodpov;
        // <summary>
        // PERIODPOV N(11)
        // </summary>
        [FieldName("PERIODPOV"), FieldType('N'), FieldWidth(11)]
        public Int64 Periodpov
        {
            get { return periodpov; }
            set { CheckIntegerData("Periodpov", value, 11); periodpov = value; }
        }

        private Int64 rgresid;
        // <summary>
        // RGRESID N(11)
        // </summary>
        [FieldName("RGRESID"), FieldType('N'), FieldWidth(11)]
        public Int64 Rgresid
        {
            get { return rgresid; }
            set { CheckIntegerData("Rgresid", value, 11); rgresid = value; }
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

        private DateTime deactdate;
        // <summary>
        // DEACTDATE D(8)
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Deactdate
        {
            get { return deactdate; }
            set { deactdate = value; }
        }

        private Int64 isremove;
        // <summary>
        // ISREMOVE N(2)
        // </summary>
        [FieldName("ISREMOVE"), FieldType('N'), FieldWidth(2)]
        public Int64 Isremove
        {
            get { return isremove; }
            set { CheckIntegerData("Isremove", value, 2); isremove = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("HOUSECD")) Housecd = ADataRow["HOUSECD"].ToString(); else Housecd = "";
            if (ADataRow.Table.Columns.Contains("ULICAKOD")) Ulicakod = Convert.ToInt64(ADataRow["ULICAKOD"]); else Ulicakod = 0;
            if (ADataRow.Table.Columns.Contains("ULICANAME")) Ulicaname = ADataRow["ULICANAME"].ToString(); else Ulicaname = "";
            if (ADataRow.Table.Columns.Contains("NDOMA")) Ndoma = ADataRow["NDOMA"].ToString(); else Ndoma = "";
            if (ADataRow.Table.Columns.Contains("KORPUS")) Korpus = ADataRow["KORPUS"].ToString(); else Korpus = "";
            if (ADataRow.Table.Columns.Contains("KORPUSTIP")) Korpustip = ADataRow["KORPUSTIP"].ToString(); else Korpustip = "";
            if (ADataRow.Table.Columns.Contains("INSTPLID")) Instplid = Convert.ToInt64(ADataRow["INSTPLID"]); else Instplid = 0;
            if (ADataRow.Table.Columns.Contains("INSTPLACE")) Instplace = ADataRow["INSTPLACE"].ToString(); else Instplace = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("SERIALNUM")) Serialnum = ADataRow["SERIALNUM"].ToString(); else Serialnum = "";
            if (ADataRow.Table.Columns.Contains("LASTPOV")) Lastpov = Convert.ToDateTime(ADataRow["LASTPOV"]); else Lastpov = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("CNTTYPE")) Cnttype = ADataRow["CNTTYPE"].ToString(); else Cnttype = "";
            if (ADataRow.Table.Columns.Contains("CNTNAME")) Cntname = ADataRow["CNTNAME"].ToString(); else Cntname = "";
            if (ADataRow.Table.Columns.Contains("PRECISION")) Precision = Convert.ToDecimal(ADataRow["PRECISION"]); else Precision = 0;
            if (ADataRow.Table.Columns.Contains("AMPERAGE")) Amperage = ADataRow["AMPERAGE"].ToString(); else Amperage = "";
            if (ADataRow.Table.Columns.Contains("PERIODPOV")) Periodpov = Convert.ToInt64(ADataRow["PERIODPOV"]); else Periodpov = 0;
            if (ADataRow.Table.Columns.Contains("RGRESID")) Rgresid = Convert.ToInt64(ADataRow["RGRESID"]); else Rgresid = 0;
            if (ADataRow.Table.Columns.Contains("SETUPDATE")) Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]); else Setupdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DEACTDATE")) Deactdate = Convert.ToDateTime(ADataRow["DEACTDATE"]); else Deactdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ISREMOVE")) Isremove = Convert.ToInt64(ADataRow["ISREMOVE"]); else Isremove = 0;
        }

        public override AbstractRecord Clone()
        {
            Cntrs_grRecord retValue = new Cntrs_grRecord();
            retValue.Housecd = this.Housecd;
            retValue.Ulicakod = this.Ulicakod;
            retValue.Ulicaname = this.Ulicaname;
            retValue.Ndoma = this.Ndoma;
            retValue.Korpus = this.Korpus;
            retValue.Korpustip = this.Korpustip;
            retValue.Instplid = this.Instplid;
            retValue.Instplace = this.Instplace;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Serialnum = this.Serialnum;
            retValue.Lastpov = this.Lastpov;
            retValue.Cnttype = this.Cnttype;
            retValue.Cntname = this.Cntname;
            retValue.Precision = this.Precision;
            retValue.Amperage = this.Amperage;
            retValue.Periodpov = this.Periodpov;
            retValue.Rgresid = this.Rgresid;
            retValue.Setupdate = this.Setupdate;
            retValue.Deactdate = this.Deactdate;
            retValue.Isremove = this.Isremove;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRS_GR (HOUSECD, ULICAKOD, ULICANAME, NDOMA, KORPUS, KORPUSTIP, INSTPLID, INSTPLACE, COUNTERID, NAME, SERIALNUM, LASTPOV, CNTTYPE, CNTNAME, PRECISION, AMPERAGE, PERIODPOV, RGRESID, SETUPDATE, DEACTDATE, ISREMOVE) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}', CTOD('{11}'), '{12}', '{13}', {14}, '{15}', {16}, {17}, CTOD('{18}'), CTOD('{19}'), {20})", String.IsNullOrEmpty(Housecd) ? "" : Housecd.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), String.IsNullOrEmpty(Korpus) ? "" : Korpus.Trim(), String.IsNullOrEmpty(Korpustip) ? "" : Korpustip.Trim(), Instplid.ToString(), String.IsNullOrEmpty(Instplace) ? "" : Instplace.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Lastpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Lastpov.Month, Lastpov.Day, Lastpov.Year), String.IsNullOrEmpty(Cnttype) ? "" : Cnttype.Trim(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Precision.ToString().Replace(',', '.'), String.IsNullOrEmpty(Amperage) ? "" : Amperage.Trim(), Periodpov.ToString(), Rgresid.ToString(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), Deactdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Deactdate.Month, Deactdate.Day, Deactdate.Year), Isremove.ToString());
            return rs;
        }
    }
}