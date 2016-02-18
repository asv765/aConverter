// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _040_Skopin
{
    [TableName("Summs.DBF")]
    public partial class SummsRecord : AbstractRecord
    {
        private string viddvig;
        // <summary>
        // VIDDVIG C(10)
        // </summary>
        [FieldName("VIDDVIG"), FieldType('C'), FieldWidth(10)]
        public string Viddvig
        {
            get { return viddvig; }
            set { CheckStringData("Viddvig", value, 10); viddvig = value; }
        }

        private string document;
        // <summary>
        // DOCUMENT C(100)
        // </summary>
        [FieldName("DOCUMENT"), FieldType('C'), FieldWidth(100)]
        public string Document
        {
            get { return document; }
            set { CheckStringData("Document", value, 100); document = value; }
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

        private decimal summa100;
        // <summary>
        // SUMMA100 N(15,2)
        // </summary>
        [FieldName("SUMMA100"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Summa100
        {
            get { return summa100; }
            set { CheckDecimalData("Summa100", value, 15, 2); summa100 = value; }
        }

        private decimal servcol;
        // <summary>
        // SERVCOL N(16,4)
        // </summary>
        [FieldName("SERVCOL"), FieldType('N'), FieldWidth(16), FieldDec(4)]
        public decimal Servcol
        {
            get { return servcol; }
            set { CheckDecimalData("Servcol", value, 16, 4); servcol = value; }
        }

        private Int64 peoplecol;
        // <summary>
        // PEOPLECOL N(3)
        // </summary>
        [FieldName("PEOPLECOL"), FieldType('N'), FieldWidth(3)]
        public Int64 Peoplecol
        {
            get { return peoplecol; }
            set { CheckIntegerData("Peoplecol", value, 3); peoplecol = value; }
        }

        private Int64 rastypecd;
        // <summary>
        // RASTYPECD N(6)
        // </summary>
        [FieldName("RASTYPECD"), FieldType('N'), FieldWidth(6)]
        public Int64 Rastypecd
        {
            get { return rastypecd; }
            set { CheckIntegerData("Rastypecd", value, 6); rastypecd = value; }
        }

        private string rastypenm;
        // <summary>
        // RASTYPENM C(25)
        // </summary>
        [FieldName("RASTYPENM"), FieldType('C'), FieldWidth(25)]
        public string Rastypenm
        {
            get { return rastypenm; }
            set { CheckStringData("Rastypenm", value, 25); rastypenm = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("VIDDVIG")) Viddvig = ADataRow["VIDDVIG"].ToString(); else Viddvig = "";
            if (ADataRow.Table.Columns.Contains("DOCUMENT")) Document = ADataRow["DOCUMENT"].ToString(); else Document = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("SUMMA100")) Summa100 = Convert.ToDecimal(ADataRow["SUMMA100"]); else Summa100 = 0;
            if (ADataRow.Table.Columns.Contains("SERVCOL")) Servcol = Convert.ToDecimal(ADataRow["SERVCOL"]); else Servcol = 0;
            if (ADataRow.Table.Columns.Contains("PEOPLECOL")) Peoplecol = Convert.ToInt64(ADataRow["PEOPLECOL"]); else Peoplecol = 0;
            if (ADataRow.Table.Columns.Contains("RASTYPECD")) Rastypecd = Convert.ToInt64(ADataRow["RASTYPECD"]); else Rastypecd = 0;
            if (ADataRow.Table.Columns.Contains("RASTYPENM")) Rastypenm = ADataRow["RASTYPENM"].ToString(); else Rastypenm = "";
            if (ADataRow.Table.Columns.Contains("BEGDATE")) Begdate = Convert.ToDateTime(ADataRow["BEGDATE"]); else Begdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ENDDATE")) Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]); else Enddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            SummsRecord retValue = new SummsRecord();
            retValue.Viddvig = this.Viddvig;
            retValue.Document = this.Document;
            retValue.Date = this.Date;
            retValue.Lshet = this.Lshet;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Summa = this.Summa;
            retValue.Summa100 = this.Summa100;
            retValue.Servcol = this.Servcol;
            retValue.Peoplecol = this.Peoplecol;
            retValue.Rastypecd = this.Rastypecd;
            retValue.Rastypenm = this.Rastypenm;
            retValue.Begdate = this.Begdate;
            retValue.Enddate = this.Enddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO Summs (VIDDVIG, DOCUMENT, DATE, LSHET, SERVICECD, SERVICENM, SUMMA, SUMMA100, SERVCOL, PEOPLECOL, RASTYPECD, RASTYPENM, BEGDATE, ENDDATE) VALUES ('{0}', '{1}', CTOD('{2}'), '{3}', {4}, '{5}', {6}, {7}, {8}, {9}, {10}, '{11}', CTOD('{12}'), CTOD('{13}'))", String.IsNullOrEmpty(Viddvig) ? "" : Viddvig.Trim(), String.IsNullOrEmpty(Document) ? "" : Document.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Summa.ToString().Replace(',', '.'), Summa100.ToString().Replace(',', '.'), Servcol.ToString().Replace(',', '.'), Peoplecol.ToString(), Rastypecd.ToString(), String.IsNullOrEmpty(Rastypenm) ? "" : Rastypenm.Trim(), Begdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Begdate.Month, Begdate.Day, Begdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year));
            return rs;
        }
    }
}