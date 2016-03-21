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
    [TableName("SUMMS.DBF")]
    public partial class SummsRecord : AbstractRecord
    {
        private string doc;
        // <summary>
        // DOC C(120)
        // </summary>
        [FieldName("DOC"), FieldType('C'), FieldWidth(120)]
        public string Doc
        {
            get { return doc; }
            set { CheckStringData("Doc", value, 120); doc = value; }
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

        private string lshet_kod;
        // <summary>
        // LSHET_KOD C(10)
        // </summary>
        [FieldName("LSHET_KOD"), FieldType('C'), FieldWidth(10)]
        public string Lshet_kod
        {
            get { return lshet_kod; }
            set { CheckStringData("Lshet_kod", value, 10); lshet_kod = value; }
        }

        private Int64 lcharcd;
        // <summary>
        // LCHARCD N(3)
        // </summary>
        [FieldName("LCHARCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Lcharcd
        {
            get { return lcharcd; }
            set { CheckIntegerData("Lcharcd", value, 3); lcharcd = value; }
        }

        private string lcharname;
        // <summary>
        // LCHARNAME C(50)
        // </summary>
        [FieldName("LCHARNAME"), FieldType('C'), FieldWidth(50)]
        public string Lcharname
        {
            get { return lcharname; }
            set { CheckStringData("Lcharname", value, 50); lcharname = value; }
        }

        private Int64 tarifcd;
        // <summary>
        // TARIFCD N(3)
        // </summary>
        [FieldName("TARIFCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Tarifcd
        {
            get { return tarifcd; }
            set { CheckIntegerData("Tarifcd", value, 3); tarifcd = value; }
        }

        private string tarifnm;
        // <summary>
        // TARIFNM C(100)
        // </summary>
        [FieldName("TARIFNM"), FieldType('C'), FieldWidth(100)]
        public string Tarifnm
        {
            get { return tarifnm; }
            set { CheckStringData("Tarifnm", value, 100); tarifnm = value; }
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

        private DateTime date_deist;
        // <summary>
        // DATE_DEIST D(8)
        // </summary>
        [FieldName("DATE_DEIST"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_deist
        {
            get { return date_deist; }
            set { date_deist = value; }
        }

        private decimal serv_col;
        // <summary>
        // SERV_COL N(15,5)
        // </summary>
        [FieldName("SERV_COL"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Serv_col
        {
            get { return serv_col; }
            set { CheckDecimalData("Serv_col", value, 15, 5); serv_col = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("LSHET_KOD")) Lshet_kod = ADataRow["LSHET_KOD"].ToString(); else Lshet_kod = "";
            if (ADataRow.Table.Columns.Contains("LCHARCD")) Lcharcd = Convert.ToInt64(ADataRow["LCHARCD"]); else Lcharcd = 0;
            if (ADataRow.Table.Columns.Contains("LCHARNAME")) Lcharname = ADataRow["LCHARNAME"].ToString(); else Lcharname = "";
            if (ADataRow.Table.Columns.Contains("TARIFCD")) Tarifcd = Convert.ToInt64(ADataRow["TARIFCD"]); else Tarifcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFNM")) Tarifnm = ADataRow["TARIFNM"].ToString(); else Tarifnm = "";
            if (ADataRow.Table.Columns.Contains("DATE_RASCH")) Date_rasch = Convert.ToDateTime(ADataRow["DATE_RASCH"]); else Date_rasch = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATE_DEIST")) Date_deist = Convert.ToDateTime(ADataRow["DATE_DEIST"]); else Date_deist = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SERV_COL")) Serv_col = Convert.ToDecimal(ADataRow["SERV_COL"]); else Serv_col = 0;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
        }

        public override AbstractRecord Clone()
        {
            SummsRecord retValue = new SummsRecord();
            retValue.Doc = this.Doc;
            retValue.Date = this.Date;
            retValue.Lshet_kod = this.Lshet_kod;
            retValue.Lcharcd = this.Lcharcd;
            retValue.Lcharname = this.Lcharname;
            retValue.Tarifcd = this.Tarifcd;
            retValue.Tarifnm = this.Tarifnm;
            retValue.Date_rasch = this.Date_rasch;
            retValue.Date_deist = this.Date_deist;
            retValue.Serv_col = this.Serv_col;
            retValue.Summa = this.Summa;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SUMMS (DOC, DATE, LSHET_KOD, LCHARCD, LCHARNAME, TARIFCD, TARIFNM, DATE_RASCH, DATE_DEIST, SERV_COL, SUMMA) VALUES ('{0}', CTOD('{1}'), '{2}', {3}, '{4}', {5}, '{6}', CTOD('{7}'), CTOD('{8}'), {9}, {10})", String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Lshet_kod) ? "" : Lshet_kod.Trim(), Lcharcd.ToString(), String.IsNullOrEmpty(Lcharname) ? "" : Lcharname.Trim(), Tarifcd.ToString(), String.IsNullOrEmpty(Tarifnm) ? "" : Tarifnm.Trim(), Date_rasch == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_rasch.Month, Date_rasch.Day, Date_rasch.Year), Date_deist == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_deist.Month, Date_deist.Day, Date_deist.Year), Serv_col.ToString().Replace(',', '.'), Summa.ToString().Replace(',', '.'));
            return rs;
        }
    }
}