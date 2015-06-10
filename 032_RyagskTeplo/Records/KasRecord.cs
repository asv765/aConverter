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
    [TableName("KAS.DBF")]
    public partial class KasRecord: AbstractRecord
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

        private DateTime dat;
        // <summary>
        // DAT D(8)
        // </summary>
        [FieldName("DAT"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat
        {
            get { return dat; }
            set {  dat = value; }
        }

        private decimal sum;
        // <summary>
        // SUM N(10,2)
        // </summary>
        [FieldName("SUM"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sum
        {
            get { return sum; }
            set { CheckDecimalData("Sum", value, 10, 2); sum = value; }
        }

        private Int64 usluga;
        // <summary>
        // USLUGA N(4)
        // </summary>
        [FieldName("USLUGA"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga
        {
            get { return usluga; }
            set { CheckIntegerData("Usluga", value, 4); usluga = value; }
        }

        private Int64 time;
        // <summary>
        // TIME N(6)
        // </summary>
        [FieldName("TIME"), FieldType('N'), FieldWidth(6)]
        public Int64 Time
        {
            get { return time; }
            set { CheckIntegerData("Time", value, 6); time = value; }
        }

        private Int64 id_post;
        // <summary>
        // ID_POST N(4)
        // </summary>
        [FieldName("ID_POST"), FieldType('N'), FieldWidth(4)]
        public Int64 Id_post
        {
            get { return id_post; }
            set { CheckIntegerData("Id_post", value, 4); id_post = value; }
        }

        private decimal cnt1;
        // <summary>
        // CNT1 N(11,3)
        // </summary>
        [FieldName("CNT1"), FieldType('N'), FieldWidth(11), FieldDec(3)]
        public decimal Cnt1
        {
            get { return cnt1; }
            set { CheckDecimalData("Cnt1", value, 11, 3); cnt1 = value; }
        }

        private decimal cnt2;
        // <summary>
        // CNT2 N(11,3)
        // </summary>
        [FieldName("CNT2"), FieldType('N'), FieldWidth(11), FieldDec(3)]
        public decimal Cnt2
        {
            get { return cnt2; }
            set { CheckDecimalData("Cnt2", value, 11, 3); cnt2 = value; }
        }

        private string list_pu;
        // <summary>
        // LIST_PU C(100)
        // </summary>
        [FieldName("LIST_PU"), FieldType('C'), FieldWidth(100)]
        public string List_pu
        {
            get { return list_pu; }
            set { CheckStringData("List_pu", value, 100); list_pu = value; }
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
        // AUTHOR C(30)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(30)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 30); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = Convert.ToDecimal(ADataRow["SUM"]); else Sum = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = Convert.ToInt64(ADataRow["TIME"]); else Time = 0;
            if (ADataRow.Table.Columns.Contains("ID_POST")) Id_post = Convert.ToInt64(ADataRow["ID_POST"]); else Id_post = 0;
            if (ADataRow.Table.Columns.Contains("CNT1")) Cnt1 = Convert.ToDecimal(ADataRow["CNT1"]); else Cnt1 = 0;
            if (ADataRow.Table.Columns.Contains("CNT2")) Cnt2 = Convert.ToDecimal(ADataRow["CNT2"]); else Cnt2 = 0;
            if (ADataRow.Table.Columns.Contains("LIST_PU")) List_pu = ADataRow["LIST_PU"].ToString(); else List_pu = "";
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            KasRecord retValue = new KasRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Sum = this.Sum;
            retValue.Usluga = this.Usluga;
            retValue.Time = this.Time;
            retValue.Id_post = this.Id_post;
            retValue.Cnt1 = this.Cnt1;
            retValue.Cnt2 = this.Cnt2;
            retValue.List_pu = this.List_pu;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO KAS (LS, DAT, SUM, USLUGA, TIME, ID_POST, CNT1, CNT2, LIST_PU, OPER, AUTHOR) VALUES ({0}, CTOD('{1}'), {2}, {3}, {4}, {5}, {6}, {7}, '{8}', {9}, '{10}')", Ls.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Sum.ToString().Replace(',','.'), Usluga.ToString(), Time.ToString(), Id_post.ToString(), Cnt1.ToString().Replace(',','.'), Cnt2.ToString().Replace(',','.'), String.IsNullOrEmpty(List_pu) ? "" : List_pu.Trim(), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
