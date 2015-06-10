// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _032_RyagskTeplo
{
    [TableName("MIGR.DBF")]
    public partial class MigrRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(11)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(11)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 11); ls = value; }
        }

        private string idf;
        // <summary>
        // IDF C(10)
        // </summary>
        [FieldName("IDF"), FieldType('C'), FieldWidth(10)]
        public string Idf
        {
            get { return idf; }
            set { CheckStringData("Idf", value, 10); idf = value; }
        }

        private string ide;
        // <summary>
        // IDE C(10)
        // </summary>
        [FieldName("IDE"), FieldType('C'), FieldWidth(10)]
        public string Ide
        {
            get { return ide; }
            set { CheckStringData("Ide", value, 10); ide = value; }
        }

        private string fam;
        // <summary>
        // FAM C(20)
        // </summary>
        [FieldName("FAM"), FieldType('C'), FieldWidth(20)]
        public string Fam
        {
            get { return fam; }
            set { CheckStringData("Fam", value, 20); fam = value; }
        }

        private string imia;
        // <summary>
        // IMIA C(20)
        // </summary>
        [FieldName("IMIA"), FieldType('C'), FieldWidth(20)]
        public string Imia
        {
            get { return imia; }
            set { CheckStringData("Imia", value, 20); imia = value; }
        }

        private string otch;
        // <summary>
        // OTCH C(20)
        // </summary>
        [FieldName("OTCH"), FieldType('C'), FieldWidth(20)]
        public string Otch
        {
            get { return otch; }
            set { CheckStringData("Otch", value, 20); otch = value; }
        }

        private Int64 _event;
        // <summary>
        // EVENT N(3)
        // </summary>
        [FieldName("EVENT"), FieldType('N'), FieldWidth(3)]
        public Int64 Event
        {
            get { return _event; }
            set { CheckIntegerData("Event", value, 3); _event = value; }
        }

        private DateTime dat_ub;
        // <summary>
        // DAT_UB D(8)
        // </summary>
        [FieldName("DAT_UB"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_ub
        {
            get { return dat_ub; }
            set {  dat_ub = value; }
        }

        private DateTime dat_pr;
        // <summary>
        // DAT_PR D(8)
        // </summary>
        [FieldName("DAT_PR"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_pr
        {
            get { return dat_pr; }
            set {  dat_pr = value; }
        }

        private Int64 pr_ub;
        // <summary>
        // PR_UB N(4)
        // </summary>
        [FieldName("PR_UB"), FieldType('N'), FieldWidth(4)]
        public Int64 Pr_ub
        {
            get { return pr_ub; }
            set { CheckIntegerData("Pr_ub", value, 4); pr_ub = value; }
        }

        private bool flag_ok;
        // <summary>
        // FLAG_OK L(1)
        // </summary>
        [FieldName("FLAG_OK"), FieldType('L'), FieldWidth(1)]
        public bool Flag_ok
        {
            get { return flag_ok; }
            set {  flag_ok = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        private string time;
        // <summary>
        // TIME C(8)
        // </summary>
        [FieldName("TIME"), FieldType('C'), FieldWidth(8)]
        public string Time
        {
            get { return time; }
            set { CheckStringData("Time", value, 8); time = value; }
        }

        private string comments;
        // <summary>
        // COMMENTS C(50)
        // </summary>
        [FieldName("COMMENTS"), FieldType('C'), FieldWidth(50)]
        public string Comments
        {
            get { return comments; }
            set { CheckStringData("Comments", value, 50); comments = value; }
        }

        private Int64 oper;
        // <summary>
        // OPER N(4)
        // </summary>
        [FieldName("OPER"), FieldType('N'), FieldWidth(4)]
        public Int64 Oper
        {
            get { return oper; }
            set { CheckIntegerData("Oper", value, 4); oper = value; }
        }

        private string author;
        // <summary>
        // AUTHOR C(20)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(20)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 20); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("IDF")) Idf = ADataRow["IDF"].ToString(); else Idf = "";
            if (ADataRow.Table.Columns.Contains("IDE")) Ide = ADataRow["IDE"].ToString(); else Ide = "";
            if (ADataRow.Table.Columns.Contains("FAM")) Fam = ADataRow["FAM"].ToString(); else Fam = "";
            if (ADataRow.Table.Columns.Contains("IMIA")) Imia = ADataRow["IMIA"].ToString(); else Imia = "";
            if (ADataRow.Table.Columns.Contains("OTCH")) Otch = ADataRow["OTCH"].ToString(); else Otch = "";
            if (ADataRow.Table.Columns.Contains("EVENT")) Event = Convert.ToInt64(ADataRow["EVENT"]); else Event = 0;
            if (ADataRow.Table.Columns.Contains("DAT_UB")) Dat_ub = Convert.ToDateTime(ADataRow["DAT_UB"]); else Dat_ub = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DAT_PR")) Dat_pr = Convert.ToDateTime(ADataRow["DAT_PR"]); else Dat_pr = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("PR_UB")) Pr_ub = Convert.ToInt64(ADataRow["PR_UB"]); else Pr_ub = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_OK")) Flag_ok = ADataRow["FLAG_OK"].ToString() == "True" ? true : false; else Flag_ok = false;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = ADataRow["TIME"].ToString(); else Time = "";
            if (ADataRow.Table.Columns.Contains("COMMENTS")) Comments = ADataRow["COMMENTS"].ToString(); else Comments = "";
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            MigrRecord retValue = new MigrRecord();
            retValue.Ls = this.Ls;
            retValue.Idf = this.Idf;
            retValue.Ide = this.Ide;
            retValue.Fam = this.Fam;
            retValue.Imia = this.Imia;
            retValue.Otch = this.Otch;
            retValue.Event = this.Event;
            retValue.Dat_ub = this.Dat_ub;
            retValue.Dat_pr = this.Dat_pr;
            retValue.Pr_ub = this.Pr_ub;
            retValue.Flag_ok = this.Flag_ok;
            retValue.Date = this.Date;
            retValue.Time = this.Time;
            retValue.Comments = this.Comments;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO MIGR (LS, IDF, IDE, FAM, IMIA, OTCH, EVENT, DAT_UB, DAT_PR, PR_UB, FLAG_OK, DATE, TIME, COMMENTS, OPER, AUTHOR) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, CTOD('{7}'), CTOD('{8}'), {9}, {10}, CTOD('{11}'), '{12}', '{13}', {14}, '{15}')", Ls.ToString(), String.IsNullOrEmpty(Idf) ? "" : Idf.Trim(), String.IsNullOrEmpty(Ide) ? "" : Ide.Trim(), String.IsNullOrEmpty(Fam) ? "" : Fam.Trim(), String.IsNullOrEmpty(Imia) ? "" : Imia.Trim(), String.IsNullOrEmpty(Otch) ? "" : Otch.Trim(), Event.ToString(), Dat_ub == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_ub.Month, Dat_ub.Day, Dat_ub.Year), Dat_pr == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_pr.Month, Dat_pr.Day, Dat_pr.Year), Pr_ub.ToString(), (Flag_ok ? 0 : 1 ), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Time) ? "" : Time.Trim(), String.IsNullOrEmpty(Comments) ? "" : Comments.Trim(), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
