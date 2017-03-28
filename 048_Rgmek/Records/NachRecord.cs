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
    [TableName("NACH.DBF")]
    public partial class NachRecord : AbstractRecord
    {
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

        private string doc;
        // <summary>
        // DOC C(150)
        // </summary>
        [FieldName("DOC"), FieldType('C'), FieldWidth(150)]
        public string Doc
        {
            get { return doc; }
            set { CheckStringData("Doc", value, 150); doc = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(36)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(36)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 36); lshet = value; }
        }

        private DateTime date_rasch;
        // <summary>
        // DATE_RASCH D(8)
        // </summary>
        [FieldName("DATE_RASCH"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_rasch
        {
            get { return date_rasch; }
            set { date_rasch = value; }
        }

        private Int64 resourcd;
        // <summary>
        // RESOURCD N(5)
        // </summary>
        [FieldName("RESOURCD"), FieldType('N'), FieldWidth(5)]
        public Int64 Resourcd
        {
            get { return resourcd; }
            set { CheckIntegerData("Resourcd", value, 5); resourcd = value; }
        }

        private string resournm;
        // <summary>
        // RESOURNM C(48)
        // </summary>
        [FieldName("RESOURNM"), FieldType('C'), FieldWidth(48)]
        public string Resournm
        {
            get { return resournm; }
            set { CheckStringData("Resournm", value, 48); resournm = value; }
        }

        private string tarifcd;
        // <summary>
        // TARIFCD C(36)
        // </summary>
        [FieldName("TARIFCD"), FieldType('C'), FieldWidth(36)]
        public string Tarifcd
        {
            get { return tarifcd; }
            set { CheckStringData("Tarifcd", value, 36); tarifcd = value; }
        }

        private string tarifnm;
        // <summary>
        // TARIFNM C(30)
        // </summary>
        [FieldName("TARIFNM"), FieldType('C'), FieldWidth(30)]
        public string Tarifnm
        {
            get { return tarifnm; }
            set { CheckStringData("Tarifnm", value, 30); tarifnm = value; }
        }

        private string placecd;
        // <summary>
        // PLACECD C(36)
        // </summary>
        [FieldName("PLACECD"), FieldType('C'), FieldWidth(36)]
        public string Placecd
        {
            get { return placecd; }
            set { CheckStringData("Placecd", value, 36); placecd = value; }
        }

        private decimal count;
        // <summary>
        // COUNT N(15,4)
        // </summary>
        [FieldName("COUNT"), FieldType('N'), FieldWidth(15), FieldDec(4)]
        public decimal Count
        {
            get { return count; }
            set { CheckDecimalData("Count", value, 15, 4); count = value; }
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

        private decimal summapay;
        // <summary>
        // SUMMAPAY N(15,2)
        // </summary>
        [FieldName("SUMMAPAY"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Summapay
        {
            get { return summapay; }
            set { CheckDecimalData("Summapay", value, 15, 2); summapay = value; }
        }

        private string servicecd;
        // <summary>
        // SERVICECD C(36)
        // </summary>
        [FieldName("SERVICECD"), FieldType('C'), FieldWidth(36)]
        public string Servicecd
        {
            get { return servicecd; }
            set { CheckStringData("Servicecd", value, 36); servicecd = value; }
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

        private string nachtype;
        // <summary>
        // NACHTYPE C(30)
        // </summary>
        [FieldName("NACHTYPE"), FieldType('C'), FieldWidth(30)]
        public string Nachtype
        {
            get { return nachtype; }
            set { CheckStringData("Nachtype", value, 30); nachtype = value; }
        }

        private string rasctype;
        // <summary>
        // RASCTYPE C(50)
        // </summary>
        [FieldName("RASCTYPE"), FieldType('C'), FieldWidth(50)]
        public string Rasctype
        {
            get { return rasctype; }
            set { CheckStringData("Rasctype", value, 50); rasctype = value; }
        }

        private string nds;
        // <summary>
        // NDS C(10)
        // </summary>
        [FieldName("NDS"), FieldType('C'), FieldWidth(10)]
        public string Nds
        {
            get { return nds; }
            set { CheckStringData("Nds", value, 10); nds = value; }
        }

        private decimal ndssum;
        // <summary>
        // NDSSUM N(10,2)
        // </summary>
        [FieldName("NDSSUM"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Ndssum
        {
            get { return ndssum; }
            set { CheckDecimalData("Ndssum", value, 10, 2); ndssum = value; }
        }

        private DateTime begdate;
        // <summary>
        // BEGDATE D(8)
        // </summary>
        [FieldName("BEGDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Begdate
        {
            get { return begdate; }
            set { begdate = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D(8)
        // </summary>
        [FieldName("ENDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        private decimal norma;
        // <summary>
        // NORMA N(15,7)
        // </summary>
        [FieldName("NORMA"), FieldType('N'), FieldWidth(15), FieldDec(7)]
        public decimal Norma
        {
            get { return norma; }
            set { CheckDecimalData("Norma", value, 15, 7); norma = value; }
        }

        private decimal rascval;
        // <summary>
        // RASCVAL N(10,4)
        // </summary>
        [FieldName("RASCVAL"), FieldType('N'), FieldWidth(10), FieldDec(4)]
        public decimal Rascval
        {
            get { return rascval; }
            set { CheckDecimalData("Rascval", value, 10, 4); rascval = value; }
        }

        private decimal price;
        // <summary>
        // PRICE N(8,4)
        // </summary>
        [FieldName("PRICE"), FieldType('N'), FieldWidth(8), FieldDec(4)]
        public decimal Price
        {
            get { return price; }
            set { CheckDecimalData("Price", value, 8, 4); price = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("DATE_RASCH")) Date_rasch = Convert.ToDateTime(ADataRow["DATE_RASCH"]); else Date_rasch = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("RESOURCD")) Resourcd = Convert.ToInt64(ADataRow["RESOURCD"]); else Resourcd = 0;
            if (ADataRow.Table.Columns.Contains("RESOURNM")) Resournm = ADataRow["RESOURNM"].ToString(); else Resournm = "";
            if (ADataRow.Table.Columns.Contains("TARIFCD")) Tarifcd = ADataRow["TARIFCD"].ToString(); else Tarifcd = "";
            if (ADataRow.Table.Columns.Contains("TARIFNM")) Tarifnm = ADataRow["TARIFNM"].ToString(); else Tarifnm = "";
            if (ADataRow.Table.Columns.Contains("PLACECD")) Placecd = ADataRow["PLACECD"].ToString(); else Placecd = "";
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = Convert.ToDecimal(ADataRow["COUNT"]); else Count = 0;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("SUMMAPAY")) Summapay = Convert.ToDecimal(ADataRow["SUMMAPAY"]); else Summapay = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("NACHTYPE")) Nachtype = ADataRow["NACHTYPE"].ToString(); else Nachtype = "";
            if (ADataRow.Table.Columns.Contains("RASCTYPE")) Rasctype = ADataRow["RASCTYPE"].ToString(); else Rasctype = "";
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = ADataRow["NDS"].ToString(); else Nds = "";
            if (ADataRow.Table.Columns.Contains("NDSSUM")) Ndssum = Convert.ToDecimal(ADataRow["NDSSUM"]); else Ndssum = 0;
            if (ADataRow.Table.Columns.Contains("BEGDATE")) Begdate = Convert.ToDateTime(ADataRow["BEGDATE"]); else Begdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ENDDATE")) Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]); else Enddate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NORMA")) Norma = Convert.ToDecimal(ADataRow["NORMA"]); else Norma = 0;
            if (ADataRow.Table.Columns.Contains("RASCVAL")) Rascval = Convert.ToDecimal(ADataRow["RASCVAL"]); else Rascval = 0;
            if (ADataRow.Table.Columns.Contains("PRICE")) Price = Convert.ToDecimal(ADataRow["PRICE"]); else Price = 0;
        }

        public override AbstractRecord Clone()
        {
            NachRecord retValue = new NachRecord();
            retValue.Date = this.Date;
            retValue.Doc = this.Doc;
            retValue.Lshet = this.Lshet;
            retValue.Date_rasch = this.Date_rasch;
            retValue.Resourcd = this.Resourcd;
            retValue.Resournm = this.Resournm;
            retValue.Tarifcd = this.Tarifcd;
            retValue.Tarifnm = this.Tarifnm;
            retValue.Placecd = this.Placecd;
            retValue.Count = this.Count;
            retValue.Summa = this.Summa;
            retValue.Summapay = this.Summapay;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Nachtype = this.Nachtype;
            retValue.Rasctype = this.Rasctype;
            retValue.Nds = this.Nds;
            retValue.Ndssum = this.Ndssum;
            retValue.Begdate = this.Begdate;
            retValue.Enddate = this.Enddate;
            retValue.Norma = this.Norma;
            retValue.Rascval = this.Rascval;
            retValue.Price = this.Price;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO NACH (DATE, DOC, LSHET, DATE_RASCH, RESOURCD, RESOURNM, TARIFCD, TARIFNM, PLACECD, COUNT, SUMMA, SUMMAPAY, SERVICECD, SERVICENM, NACHTYPE, RASCTYPE, NDS, NDSSUM, BEGDATE, ENDDATE, NORMA, RASCVAL, PRICE) VALUES (CTOD('{0}'), '{1}', '{2}', CTOD('{3}'), {4}, '{5}', '{6}', '{7}', '{8}', {9}, {10}, {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, CTOD('{18}'), CTOD('{19}'), {20}, {21}, {22})", Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Date_rasch == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_rasch.Month, Date_rasch.Day, Date_rasch.Year), Resourcd.ToString(), String.IsNullOrEmpty(Resournm) ? "" : Resournm.Trim(), String.IsNullOrEmpty(Tarifcd) ? "" : Tarifcd.Trim(), String.IsNullOrEmpty(Tarifnm) ? "" : Tarifnm.Trim(), String.IsNullOrEmpty(Placecd) ? "" : Placecd.Trim(), Count.ToString().Replace(',', '.'), Summa.ToString().Replace(',', '.'), Summapay.ToString().Replace(',', '.'), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), String.IsNullOrEmpty(Nachtype) ? "" : Nachtype.Trim(), String.IsNullOrEmpty(Rasctype) ? "" : Rasctype.Trim(), String.IsNullOrEmpty(Nds) ? "" : Nds.Trim(), Ndssum.ToString().Replace(',', '.'), Begdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Begdate.Month, Begdate.Day, Begdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year), Norma.ToString().Replace(',', '.'), Rascval.ToString().Replace(',', '.'), Price.ToString().Replace(',', '.'));
            return rs;
        }
    }
}