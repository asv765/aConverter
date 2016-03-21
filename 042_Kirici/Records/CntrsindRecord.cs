// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _042_Kirici
{
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
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

        private decimal previndic;
        // <summary>
        // PREVINDIC N(11,2)
        // </summary>
        [FieldName("PREVINDIC"), FieldType('N'), FieldWidth(11), FieldDec(2)]
        public decimal Previndic
        {
            get { return previndic; }
            set { CheckDecimalData("Previndic", value, 11, 2); previndic = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(11,2)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(11), FieldDec(2)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 11, 2); indication = value; }
        }

        private decimal rashod;
        // <summary>
        // RASHOD N(11,2)
        // </summary>
        [FieldName("RASHOD"), FieldType('N'), FieldWidth(11), FieldDec(2)]
        public decimal Rashod
        {
            get { return rashod; }
            set { CheckDecimalData("Rashod", value, 11, 2); rashod = value; }
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

        private Int64 is_rn;
        // <summary>
        // IS_RN N(2)
        // </summary>
        [FieldName("IS_RN"), FieldType('N'), FieldWidth(2)]
        public Int64 Is_rn
        {
            get { return is_rn; }
            set { CheckIntegerData("Is_rn", value, 2); is_rn = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DOCUMENT")) Document = ADataRow["DOCUMENT"].ToString(); else Document = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("PREVINDIC")) Previndic = Convert.ToDecimal(ADataRow["PREVINDIC"]); else Previndic = 0;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("RASHOD")) Rashod = Convert.ToDecimal(ADataRow["RASHOD"]); else Rashod = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("IS_RN")) Is_rn = Convert.ToInt64(ADataRow["IS_RN"]); else Is_rn = 0;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Document = this.Document;
            retValue.Counterid = this.Counterid;
            retValue.Name = this.Name;
            retValue.Previndic = this.Previndic;
            retValue.Indication = this.Indication;
            retValue.Rashod = this.Rashod;
            retValue.Inddate = this.Inddate;
            retValue.Is_rn = this.Is_rn;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (DOCUMENT, COUNTERID, NAME, PREVINDIC, INDICATION, RASHOD, INDDATE, IS_RN) VALUES ('{0}', '{1}', '{2}', {3}, {4}, {5}, CTOD('{6}'), {7})", String.IsNullOrEmpty(Document) ? "" : Document.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Previndic.ToString().Replace(',', '.'), Indication.ToString().Replace(',', '.'), Rashod.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year), Is_rn.ToString());
            return rs;
        }
    }
}