// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _045_SpasskStroyDetal
{
    [TableName("REGISTER.DBF")]
    public partial class RegisterRecord : AbstractRecord
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

        private string doctype;
        // <summary>
        // DOCTYPE C(150)
        // </summary>
        [FieldName("DOCTYPE"), FieldType('C'), FieldWidth(150)]
        public string Doctype
        {
            get { return doctype; }
            set { CheckStringData("Doctype", value, 150); doctype = value; }
        }

        private string doccd;
        // <summary>
        // DOCCD C(8)
        // </summary>
        [FieldName("DOCCD"), FieldType('C'), FieldWidth(8)]
        public string Doccd
        {
            get { return doccd; }
            set { CheckStringData("Doccd", value, 8); doccd = value; }
        }

        private string dt_schet;
        // <summary>
        // DT_SCHET C(10)
        // </summary>
        [FieldName("DT_SCHET"), FieldType('C'), FieldWidth(10)]
        public string Dt_schet
        {
            get { return dt_schet; }
            set { CheckStringData("Dt_schet", value, 10); dt_schet = value; }
        }

        private string dt_subk1gr;
        // <summary>
        // DT_SUBK1GR C(8)
        // </summary>
        [FieldName("DT_SUBK1GR"), FieldType('C'), FieldWidth(8)]
        public string Dt_subk1gr
        {
            get { return dt_subk1gr; }
            set { CheckStringData("Dt_subk1gr", value, 8); dt_subk1gr = value; }
        }

        private string dt_subk1cd;
        // <summary>
        // DT_SUBK1CD C(10)
        // </summary>
        [FieldName("DT_SUBK1CD"), FieldType('C'), FieldWidth(10)]
        public string Dt_subk1cd
        {
            get { return dt_subk1cd; }
            set { CheckStringData("Dt_subk1cd", value, 10); dt_subk1cd = value; }
        }

        private string dt_subk1nm;
        // <summary>
        // DT_SUBK1NM C(100)
        // </summary>
        [FieldName("DT_SUBK1NM"), FieldType('C'), FieldWidth(100)]
        public string Dt_subk1nm
        {
            get { return dt_subk1nm; }
            set { CheckStringData("Dt_subk1nm", value, 100); dt_subk1nm = value; }
        }

        private string dt_subk2cd;
        // <summary>
        // DT_SUBK2CD C(10)
        // </summary>
        [FieldName("DT_SUBK2CD"), FieldType('C'), FieldWidth(10)]
        public string Dt_subk2cd
        {
            get { return dt_subk2cd; }
            set { CheckStringData("Dt_subk2cd", value, 10); dt_subk2cd = value; }
        }

        private string dt_subk2nm;
        // <summary>
        // DT_SUBK2NM C(100)
        // </summary>
        [FieldName("DT_SUBK2NM"), FieldType('C'), FieldWidth(100)]
        public string Dt_subk2nm
        {
            get { return dt_subk2nm; }
            set { CheckStringData("Dt_subk2nm", value, 100); dt_subk2nm = value; }
        }

        private string dt_subk3cd;
        // <summary>
        // DT_SUBK3CD C(10)
        // </summary>
        [FieldName("DT_SUBK3CD"), FieldType('C'), FieldWidth(10)]
        public string Dt_subk3cd
        {
            get { return dt_subk3cd; }
            set { CheckStringData("Dt_subk3cd", value, 10); dt_subk3cd = value; }
        }

        private string dt_subk3nm;
        // <summary>
        // DT_SUBK3NM C(100)
        // </summary>
        [FieldName("DT_SUBK3NM"), FieldType('C'), FieldWidth(100)]
        public string Dt_subk3nm
        {
            get { return dt_subk3nm; }
            set { CheckStringData("Dt_subk3nm", value, 100); dt_subk3nm = value; }
        }

        private string kt_schet;
        // <summary>
        // KT_SCHET C(10)
        // </summary>
        [FieldName("KT_SCHET"), FieldType('C'), FieldWidth(10)]
        public string Kt_schet
        {
            get { return kt_schet; }
            set { CheckStringData("Kt_schet", value, 10); kt_schet = value; }
        }

        private string kt_subk1gr;
        // <summary>
        // KT_SUBK1GR C(8)
        // </summary>
        [FieldName("KT_SUBK1GR"), FieldType('C'), FieldWidth(8)]
        public string Kt_subk1gr
        {
            get { return kt_subk1gr; }
            set { CheckStringData("Kt_subk1gr", value, 8); kt_subk1gr = value; }
        }

        private string kt_subk1cd;
        // <summary>
        // KT_SUBK1CD C(10)
        // </summary>
        [FieldName("KT_SUBK1CD"), FieldType('C'), FieldWidth(10)]
        public string Kt_subk1cd
        {
            get { return kt_subk1cd; }
            set { CheckStringData("Kt_subk1cd", value, 10); kt_subk1cd = value; }
        }

        private string kt_subk1nm;
        // <summary>
        // KT_SUBK1NM C(100)
        // </summary>
        [FieldName("KT_SUBK1NM"), FieldType('C'), FieldWidth(100)]
        public string Kt_subk1nm
        {
            get { return kt_subk1nm; }
            set { CheckStringData("Kt_subk1nm", value, 100); kt_subk1nm = value; }
        }

        private string kt_subk2cd;
        // <summary>
        // KT_SUBK2CD C(10)
        // </summary>
        [FieldName("KT_SUBK2CD"), FieldType('C'), FieldWidth(10)]
        public string Kt_subk2cd
        {
            get { return kt_subk2cd; }
            set { CheckStringData("Kt_subk2cd", value, 10); kt_subk2cd = value; }
        }

        private string kt_subk2nm;
        // <summary>
        // KT_SUBK2NM C(100)
        // </summary>
        [FieldName("KT_SUBK2NM"), FieldType('C'), FieldWidth(100)]
        public string Kt_subk2nm
        {
            get { return kt_subk2nm; }
            set { CheckStringData("Kt_subk2nm", value, 100); kt_subk2nm = value; }
        }

        private string kt_subk3cd;
        // <summary>
        // KT_SUBK3CD C(10)
        // </summary>
        [FieldName("KT_SUBK3CD"), FieldType('C'), FieldWidth(10)]
        public string Kt_subk3cd
        {
            get { return kt_subk3cd; }
            set { CheckStringData("Kt_subk3cd", value, 10); kt_subk3cd = value; }
        }

        private string kt_subk3nm;
        // <summary>
        // KT_SUBK3NM C(100)
        // </summary>
        [FieldName("KT_SUBK3NM"), FieldType('C'), FieldWidth(100)]
        public string Kt_subk3nm
        {
            get { return kt_subk3nm; }
            set { CheckStringData("Kt_subk3nm", value, 100); kt_subk3nm = value; }
        }

        private decimal count;
        // <summary>
        // COUNT N(18,4)
        // </summary>
        [FieldName("COUNT"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Count
        {
            get { return count; }
            set { CheckDecimalData("Count", value, 18, 4); count = value; }
        }

        private decimal summa;
        // <summary>
        // SUMMA N(18,4)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(18), FieldDec(4)]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 18, 4); summa = value; }
        }

        private string comment;
        // <summary>
        // COMMENT C(100)
        // </summary>
        [FieldName("COMMENT"), FieldType('C'), FieldWidth(100)]
        public string Comment
        {
            get { return comment; }
            set { CheckStringData("Comment", value, 100); comment = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOCTYPE")) Doctype = ADataRow["DOCTYPE"].ToString(); else Doctype = "";
            if (ADataRow.Table.Columns.Contains("DOCCD")) Doccd = ADataRow["DOCCD"].ToString(); else Doccd = "";
            if (ADataRow.Table.Columns.Contains("DT_SCHET")) Dt_schet = ADataRow["DT_SCHET"].ToString(); else Dt_schet = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK1GR")) Dt_subk1gr = ADataRow["DT_SUBK1GR"].ToString(); else Dt_subk1gr = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK1CD")) Dt_subk1cd = ADataRow["DT_SUBK1CD"].ToString(); else Dt_subk1cd = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK1NM")) Dt_subk1nm = ADataRow["DT_SUBK1NM"].ToString(); else Dt_subk1nm = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK2CD")) Dt_subk2cd = ADataRow["DT_SUBK2CD"].ToString(); else Dt_subk2cd = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK2NM")) Dt_subk2nm = ADataRow["DT_SUBK2NM"].ToString(); else Dt_subk2nm = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK3CD")) Dt_subk3cd = ADataRow["DT_SUBK3CD"].ToString(); else Dt_subk3cd = "";
            if (ADataRow.Table.Columns.Contains("DT_SUBK3NM")) Dt_subk3nm = ADataRow["DT_SUBK3NM"].ToString(); else Dt_subk3nm = "";
            if (ADataRow.Table.Columns.Contains("KT_SCHET")) Kt_schet = ADataRow["KT_SCHET"].ToString(); else Kt_schet = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK1GR")) Kt_subk1gr = ADataRow["KT_SUBK1GR"].ToString(); else Kt_subk1gr = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK1CD")) Kt_subk1cd = ADataRow["KT_SUBK1CD"].ToString(); else Kt_subk1cd = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK1NM")) Kt_subk1nm = ADataRow["KT_SUBK1NM"].ToString(); else Kt_subk1nm = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK2CD")) Kt_subk2cd = ADataRow["KT_SUBK2CD"].ToString(); else Kt_subk2cd = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK2NM")) Kt_subk2nm = ADataRow["KT_SUBK2NM"].ToString(); else Kt_subk2nm = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK3CD")) Kt_subk3cd = ADataRow["KT_SUBK3CD"].ToString(); else Kt_subk3cd = "";
            if (ADataRow.Table.Columns.Contains("KT_SUBK3NM")) Kt_subk3nm = ADataRow["KT_SUBK3NM"].ToString(); else Kt_subk3nm = "";
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = Convert.ToDecimal(ADataRow["COUNT"]); else Count = 0;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("COMMENT")) Comment = ADataRow["COMMENT"].ToString(); else Comment = "";
        }

        public override AbstractRecord Clone()
        {
            RegisterRecord retValue = new RegisterRecord();
            retValue.Date = this.Date;
            retValue.Doctype = this.Doctype;
            retValue.Doccd = this.Doccd;
            retValue.Dt_schet = this.Dt_schet;
            retValue.Dt_subk1gr = this.Dt_subk1gr;
            retValue.Dt_subk1cd = this.Dt_subk1cd;
            retValue.Dt_subk1nm = this.Dt_subk1nm;
            retValue.Dt_subk2cd = this.Dt_subk2cd;
            retValue.Dt_subk2nm = this.Dt_subk2nm;
            retValue.Dt_subk3cd = this.Dt_subk3cd;
            retValue.Dt_subk3nm = this.Dt_subk3nm;
            retValue.Kt_schet = this.Kt_schet;
            retValue.Kt_subk1gr = this.Kt_subk1gr;
            retValue.Kt_subk1cd = this.Kt_subk1cd;
            retValue.Kt_subk1nm = this.Kt_subk1nm;
            retValue.Kt_subk2cd = this.Kt_subk2cd;
            retValue.Kt_subk2nm = this.Kt_subk2nm;
            retValue.Kt_subk3cd = this.Kt_subk3cd;
            retValue.Kt_subk3nm = this.Kt_subk3nm;
            retValue.Count = this.Count;
            retValue.Summa = this.Summa;
            retValue.Comment = this.Comment;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO REGISTER (DATE, DOCTYPE, DOCCD, DT_SCHET, DT_SUBK1GR, DT_SUBK1CD, DT_SUBK1NM, DT_SUBK2CD, DT_SUBK2NM, DT_SUBK3CD, DT_SUBK3NM, KT_SCHET, KT_SUBK1GR, KT_SUBK1CD, KT_SUBK1NM, KT_SUBK2CD, KT_SUBK2NM, KT_SUBK3CD, KT_SUBK3NM, COUNT, SUMMA, COMMENT) VALUES (CTOD('{0}'), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', {19}, {20}, '{21}')", Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Doctype) ? "" : Doctype.Trim(), String.IsNullOrEmpty(Doccd) ? "" : Doccd.Trim(), String.IsNullOrEmpty(Dt_schet) ? "" : Dt_schet.Trim(), String.IsNullOrEmpty(Dt_subk1gr) ? "" : Dt_subk1gr.Trim(), String.IsNullOrEmpty(Dt_subk1cd) ? "" : Dt_subk1cd.Trim(), String.IsNullOrEmpty(Dt_subk1nm) ? "" : Dt_subk1nm.Trim(), String.IsNullOrEmpty(Dt_subk2cd) ? "" : Dt_subk2cd.Trim(), String.IsNullOrEmpty(Dt_subk2nm) ? "" : Dt_subk2nm.Trim(), String.IsNullOrEmpty(Dt_subk3cd) ? "" : Dt_subk3cd.Trim(), String.IsNullOrEmpty(Dt_subk3nm) ? "" : Dt_subk3nm.Trim(), String.IsNullOrEmpty(Kt_schet) ? "" : Kt_schet.Trim(), String.IsNullOrEmpty(Kt_subk1gr) ? "" : Kt_subk1gr.Trim(), String.IsNullOrEmpty(Kt_subk1cd) ? "" : Kt_subk1cd.Trim(), String.IsNullOrEmpty(Kt_subk1nm) ? "" : Kt_subk1nm.Trim(), String.IsNullOrEmpty(Kt_subk2cd) ? "" : Kt_subk2cd.Trim(), String.IsNullOrEmpty(Kt_subk2nm) ? "" : Kt_subk2nm.Trim(), String.IsNullOrEmpty(Kt_subk3cd) ? "" : Kt_subk3cd.Trim(), String.IsNullOrEmpty(Kt_subk3nm) ? "" : Kt_subk3nm.Trim(), Count.ToString().Replace(',', '.'), Summa.ToString().Replace(',', '.'), String.IsNullOrEmpty(Comment) ? "" : Comment.Trim());
            return rs;
        }
    }
}