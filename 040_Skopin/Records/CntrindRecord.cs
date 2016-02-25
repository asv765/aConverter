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
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
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

        private string counterid;
        // <summary>
        // COUNTERID C(8)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(8)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 8); counterid = value; }
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

        private DateTime inddate;
        // <summary>
        // INDDATE D(8)
        // </summary>
        [FieldName("INDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Inddate
        {
            get { return inddate; }
            set { inddate = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(15,3)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(15), FieldDec(3)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 15, 3); indication = value; }
        }

        private decimal begind;
        // <summary>
        // BEGIND N(15,3)
        // </summary>
        [FieldName("BEGIND"), FieldType('N'), FieldWidth(15), FieldDec(3)]
        public decimal Begind
        {
            get { return begind; }
            set { CheckDecimalData("Begind", value, 15, 3); begind = value; }
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
            if (ADataRow.Table.Columns.Contains("DOCUMENT")) Document = ADataRow["DOCUMENT"].ToString(); else Document = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("BEGIND")) Begind = Convert.ToDecimal(ADataRow["BEGIND"]); else Begind = 0;
            if (ADataRow.Table.Columns.Contains("BEGDATE")) Begdate = Convert.ToDateTime(ADataRow["BEGDATE"]); else Begdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ENDDATE")) Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]); else Enddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Document = this.Document;
            retValue.Counterid = this.Counterid;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Inddate = this.Inddate;
            retValue.Indication = this.Indication;
            retValue.Begind = this.Begind;
            retValue.Begdate = this.Begdate;
            retValue.Enddate = this.Enddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (DOCUMENT, COUNTERID, SERVICECD, SERVICENM, INDDATE, INDICATION, BEGIND, BEGDATE, ENDDATE) VALUES ('{0}', '{1}', {2}, '{3}', CTOD('{4}'), {5}, {6}, CTOD('{7}'), CTOD('{8}'))", String.IsNullOrEmpty(Document) ? "" : Document.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year), Indication.ToString().Replace(',', '.'), Begind.ToString().Replace(',', '.'), Begdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Begdate.Month, Begdate.Day, Begdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year));
            return rs;
        }
    }
}