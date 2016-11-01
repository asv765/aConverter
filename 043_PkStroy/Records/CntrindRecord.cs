// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _043_PkStroy
{
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
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

        private string document;
        // <summary>
        // DOCUMENT C(250)
        // </summary>
        [FieldName("DOCUMENT"), FieldType('C'), FieldWidth(250)]
        public string Document
        {
            get { return document; }
            set { CheckStringData("Document", value, 250); document = value; }
        }

        private string counterid;
        // <summary>
        // COUNTERID C(9)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(9)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 9); counterid = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(4)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(4)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 4); servicecd = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(17)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(17)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 17); lshet = value; }
        }

        private decimal previndic;
        // <summary>
        // PREVINDIC N(16,6)
        // </summary>
        [FieldName("PREVINDIC"), FieldType('N'), FieldWidth(16), FieldDec(6)]
        public decimal Previndic
        {
            get { return previndic; }
            set { CheckDecimalData("Previndic", value, 16, 6); previndic = value; }
        }

        private decimal count;
        // <summary>
        // COUNT N(16,6)
        // </summary>
        [FieldName("COUNT"), FieldType('N'), FieldWidth(16), FieldDec(6)]
        public decimal Count
        {
            get { return count; }
            set { CheckDecimalData("Count", value, 16, 6); count = value; }
        }

        private Int64 divider;
        // <summary>
        // DIVIDER N(11)
        // </summary>
        [FieldName("DIVIDER"), FieldType('N'), FieldWidth(11)]
        public Int64 Divider
        {
            get { return divider; }
            set { CheckIntegerData("Divider", value, 11); divider = value; }
        }

        private decimal rashod;
        // <summary>
        // RASHOD N(16,6)
        // </summary>
        [FieldName("RASHOD"), FieldType('N'), FieldWidth(16), FieldDec(6)]
        public decimal Rashod
        {
            get { return rashod; }
            set { CheckDecimalData("Rashod", value, 16, 6); rashod = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(16,6)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(16), FieldDec(6)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 16, 6); indication = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DOCDATE")) Docdate = Convert.ToDateTime(ADataRow["DOCDATE"]); else Docdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOCUMENT")) Document = ADataRow["DOCUMENT"].ToString(); else Document = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("PREVINDIC")) Previndic = Convert.ToDecimal(ADataRow["PREVINDIC"]); else Previndic = 0;
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = Convert.ToDecimal(ADataRow["COUNT"]); else Count = 0;
            if (ADataRow.Table.Columns.Contains("DIVIDER")) Divider = Convert.ToInt64(ADataRow["DIVIDER"]); else Divider = 0;
            if (ADataRow.Table.Columns.Contains("RASHOD")) Rashod = Convert.ToDecimal(ADataRow["RASHOD"]); else Rashod = 0;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Docdate = this.Docdate;
            retValue.Document = this.Document;
            retValue.Counterid = this.Counterid;
            retValue.Servicecd = this.Servicecd;
            retValue.Lshet = this.Lshet;
            retValue.Previndic = this.Previndic;
            retValue.Count = this.Count;
            retValue.Divider = this.Divider;
            retValue.Rashod = this.Rashod;
            retValue.Indication = this.Indication;
            retValue.Inddate = this.Inddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (DOCDATE, DOCUMENT, COUNTERID, SERVICECD, LSHET, PREVINDIC, COUNT, DIVIDER, RASHOD, INDICATION, INDDATE) VALUES (CTOD('{0}'), '{1}', '{2}', {3}, '{4}', {5}, {6}, {7}, {8}, {9}, CTOD('{10}'))", Docdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Docdate.Month, Docdate.Day, Docdate.Year), String.IsNullOrEmpty(Document) ? "" : Document.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Previndic.ToString().Replace(',', '.'), Count.ToString().Replace(',', '.'), Divider.ToString(), Rashod.ToString().Replace(',', '.'), Indication.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year));
            return rs;
        }
    }
}