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
    [TableName("RIF.DBF")]
    public partial class RifRecord: AbstractRecord
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

        private string fio;
        // <summary>
        // FIO C(30)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(30)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 30); fio = value; }
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
        // CNT1 N(9,3)
        // </summary>
        [FieldName("CNT1"), FieldType('N'), FieldWidth(9), FieldDec(3)]
        public decimal Cnt1
        {
            get { return cnt1; }
            set { CheckDecimalData("Cnt1", value, 9, 3); cnt1 = value; }
        }

        private decimal cnt2;
        // <summary>
        // CNT2 N(9,3)
        // </summary>
        [FieldName("CNT2"), FieldType('N'), FieldWidth(9), FieldDec(3)]
        public decimal Cnt2
        {
            get { return cnt2; }
            set { CheckDecimalData("Cnt2", value, 9, 3); cnt2 = value; }
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

        private string iddoc;
        // <summary>
        // IDDOC C(10)
        // </summary>
        [FieldName("IDDOC"), FieldType('C'), FieldWidth(10)]
        public string Iddoc
        {
            get { return iddoc; }
            set { CheckStringData("Iddoc", value, 10); iddoc = value; }
        }

        private bool hand;
        // <summary>
        // HAND L(1)
        // </summary>
        [FieldName("HAND"), FieldType('L'), FieldWidth(1)]
        public bool Hand
        {
            get { return hand; }
            set {  hand = value; }
        }

        private string services;
        // <summary>
        // SERVICES C(30)
        // </summary>
        [FieldName("SERVICES"), FieldType('C'), FieldWidth(30)]
        public string Services
        {
            get { return services; }
            set { CheckStringData("Services", value, 30); services = value; }
        }

        private string msg;
        // <summary>
        // MSG C(30)
        // </summary>
        [FieldName("MSG"), FieldType('C'), FieldWidth(30)]
        public string Msg
        {
            get { return msg; }
            set { CheckStringData("Msg", value, 30); msg = value; }
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
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = Convert.ToDecimal(ADataRow["SUM"]); else Sum = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = Convert.ToInt64(ADataRow["TIME"]); else Time = 0;
            if (ADataRow.Table.Columns.Contains("ID_POST")) Id_post = Convert.ToInt64(ADataRow["ID_POST"]); else Id_post = 0;
            if (ADataRow.Table.Columns.Contains("CNT1")) Cnt1 = Convert.ToDecimal(ADataRow["CNT1"]); else Cnt1 = 0;
            if (ADataRow.Table.Columns.Contains("CNT2")) Cnt2 = Convert.ToDecimal(ADataRow["CNT2"]); else Cnt2 = 0;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("IDDOC")) Iddoc = ADataRow["IDDOC"].ToString(); else Iddoc = "";
            if (ADataRow.Table.Columns.Contains("HAND")) Hand = ADataRow["HAND"].ToString() == "True" ? true : false; else Hand = false;
            if (ADataRow.Table.Columns.Contains("SERVICES")) Services = ADataRow["SERVICES"].ToString(); else Services = "";
            if (ADataRow.Table.Columns.Contains("MSG")) Msg = ADataRow["MSG"].ToString(); else Msg = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            RifRecord retValue = new RifRecord();
            retValue.Ls = this.Ls;
            retValue.Fio = this.Fio;
            retValue.Dat = this.Dat;
            retValue.Sum = this.Sum;
            retValue.Usluga = this.Usluga;
            retValue.Time = this.Time;
            retValue.Id_post = this.Id_post;
            retValue.Cnt1 = this.Cnt1;
            retValue.Cnt2 = this.Cnt2;
            retValue.Oper = this.Oper;
            retValue.Iddoc = this.Iddoc;
            retValue.Hand = this.Hand;
            retValue.Services = this.Services;
            retValue.Msg = this.Msg;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO RIF (LS, FIO, DAT, SUM, USLUGA, TIME, ID_POST, CNT1, CNT2, OPER, IDDOC, HAND, SERVICES, MSG, AUTHOR) VALUES ({0}, '{1}', CTOD('{2}'), {3}, {4}, {5}, {6}, {7}, {8}, {9}, '{10}', {11}, '{12}', '{13}', '{14}')", Ls.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Sum.ToString().Replace(',','.'), Usluga.ToString(), Time.ToString(), Id_post.ToString(), Cnt1.ToString().Replace(',','.'), Cnt2.ToString().Replace(',','.'), Oper.ToString(), String.IsNullOrEmpty(Iddoc) ? "" : Iddoc.Trim(), (Hand ? 0 : 1 ), String.IsNullOrEmpty(Services) ? "" : Services.Trim(), String.IsNullOrEmpty(Msg) ? "" : Msg.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
