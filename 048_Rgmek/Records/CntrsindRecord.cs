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
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
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

        private string cntname;
        // <summary>
        // CNTNAME C(100)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(100)]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 100); cntname = value; }
        }

        private Int64 scalecd;
        // <summary>
        // SCALECD N(3)
        // </summary>
        [FieldName("SCALECD"), FieldType('N'), FieldWidth(3)]
        public Int64 Scalecd
        {
            get { return scalecd; }
            set { CheckIntegerData("Scalecd", value, 3); scalecd = value; }
        }

        private string scalenm;
        // <summary>
        // SCALENM C(20)
        // </summary>
        [FieldName("SCALENM"), FieldType('C'), FieldWidth(20)]
        public string Scalenm
        {
            get { return scalenm; }
            set { CheckStringData("Scalenm", value, 20); scalenm = value; }
        }

        private string indtype;
        // <summary>
        // INDTYPE C(40)
        // </summary>
        [FieldName("INDTYPE"), FieldType('C'), FieldWidth(40)]
        public string Indtype
        {
            get { return indtype; }
            set { CheckStringData("Indtype", value, 40); indtype = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(15,4)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(15), FieldDec(4)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 15, 4); indication = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("CNTNAME")) Cntname = ADataRow["CNTNAME"].ToString(); else Cntname = "";
            if (ADataRow.Table.Columns.Contains("SCALECD")) Scalecd = Convert.ToInt64(ADataRow["SCALECD"]); else Scalecd = 0;
            if (ADataRow.Table.Columns.Contains("SCALENM")) Scalenm = ADataRow["SCALENM"].ToString(); else Scalenm = "";
            if (ADataRow.Table.Columns.Contains("INDTYPE")) Indtype = ADataRow["INDTYPE"].ToString(); else Indtype = "";
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Doc = this.Doc;
            retValue.Date = this.Date;
            retValue.Counterid = this.Counterid;
            retValue.Cntname = this.Cntname;
            retValue.Scalecd = this.Scalecd;
            retValue.Scalenm = this.Scalenm;
            retValue.Indtype = this.Indtype;
            retValue.Indication = this.Indication;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND 2017 (DOC, DATE, COUNTERID, CNTNAME, SCALECD, SCALENM, INDTYPE, INDICATION) VALUES ('{0}', CTOD('{1}'), '{2}', '{3}', {4}, '{5}', '{6}', {7})", String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Scalecd.ToString(), String.IsNullOrEmpty(Scalenm) ? "" : Scalenm.Trim(), String.IsNullOrEmpty(Indtype) ? "" : Indtype.Trim(), Indication.ToString().Replace(',', '.'));
            return rs;
        }
    }
}