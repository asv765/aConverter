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
    [TableName("KORRAS.DBF")]
    public partial class KorrasRecord: AbstractRecord
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

        private string dat;
        // <summary>
        // DAT C(6)
        // </summary>
        [FieldName("DAT"), FieldType('C'), FieldWidth(6)]
        public string Dat
        {
            get { return dat; }
            set { CheckStringData("Dat", value, 6); dat = value; }
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

        private decimal nds;
        // <summary>
        // NDS N(10,2)
        // </summary>
        [FieldName("NDS"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Nds
        {
            get { return nds; }
            set { CheckDecimalData("Nds", value, 10, 2); nds = value; }
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

        private Int64 prich;
        // <summary>
        // PRICH N(4)
        // </summary>
        [FieldName("PRICH"), FieldType('N'), FieldWidth(4)]
        public Int64 Prich
        {
            get { return prich; }
            set { CheckIntegerData("Prich", value, 4); prich = value; }
        }

        private string godmes;
        // <summary>
        // GODMES C(6)
        // </summary>
        [FieldName("GODMES"), FieldType('C'), FieldWidth(6)]
        public string Godmes
        {
            get { return godmes; }
            set { CheckStringData("Godmes", value, 6); godmes = value; }
        }

        private DateTime datras;
        // <summary>
        // DATRAS D(8)
        // </summary>
        [FieldName("DATRAS"), FieldType('D'), FieldWidth(8)]
        public DateTime Datras
        {
            get { return datras; }
            set {  datras = value; }
        }

        private Int64 timras;
        // <summary>
        // TIMRAS N(6)
        // </summary>
        [FieldName("TIMRAS"), FieldType('N'), FieldWidth(6)]
        public Int64 Timras
        {
            get { return timras; }
            set { CheckIntegerData("Timras", value, 6); timras = value; }
        }

        private string id;
        // <summary>
        // ID C(1)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(1)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 1); id = value; }
        }

        private Int64 num;
        // <summary>
        // NUM N(7)
        // </summary>
        [FieldName("NUM"), FieldType('N'), FieldWidth(7)]
        public Int64 Num
        {
            get { return num; }
            set { CheckIntegerData("Num", value, 7); num = value; }
        }

        private Int64 doc;
        // <summary>
        // DOC N(11)
        // </summary>
        [FieldName("DOC"), FieldType('N'), FieldWidth(11)]
        public Int64 Doc
        {
            get { return doc; }
            set { CheckIntegerData("Doc", value, 11); doc = value; }
        }

        private string info;
        // <summary>
        // INFO C(50)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(50)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 50); info = value; }
        }

        private string _event;
        // <summary>
        // EVENT C(10)
        // </summary>
        [FieldName("EVENT"), FieldType('C'), FieldWidth(10)]
        public string Event
        {
            get { return _event; }
            set { CheckStringData("Event", value, 10); _event = value; }
        }

        private decimal proc;
        // <summary>
        // PROC N(7,2)
        // </summary>
        [FieldName("PROC"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Proc
        {
            get { return proc; }
            set { CheckDecimalData("Proc", value, 7, 2); proc = value; }
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
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = Convert.ToDecimal(ADataRow["SUM"]); else Sum = 0;
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = Convert.ToDecimal(ADataRow["NDS"]); else Nds = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("PRICH")) Prich = Convert.ToInt64(ADataRow["PRICH"]); else Prich = 0;
            if (ADataRow.Table.Columns.Contains("GODMES")) Godmes = ADataRow["GODMES"].ToString(); else Godmes = "";
            if (ADataRow.Table.Columns.Contains("DATRAS")) Datras = Convert.ToDateTime(ADataRow["DATRAS"]); else Datras = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("TIMRAS")) Timras = Convert.ToInt64(ADataRow["TIMRAS"]); else Timras = 0;
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = Convert.ToInt64(ADataRow["DOC"]); else Doc = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("EVENT")) Event = ADataRow["EVENT"].ToString(); else Event = "";
            if (ADataRow.Table.Columns.Contains("PROC")) Proc = Convert.ToDecimal(ADataRow["PROC"]); else Proc = 0;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            KorrasRecord retValue = new KorrasRecord();
            retValue.Ls = this.Ls;
            retValue.Fio = this.Fio;
            retValue.Dat = this.Dat;
            retValue.Sum = this.Sum;
            retValue.Nds = this.Nds;
            retValue.Usluga = this.Usluga;
            retValue.Prich = this.Prich;
            retValue.Godmes = this.Godmes;
            retValue.Datras = this.Datras;
            retValue.Timras = this.Timras;
            retValue.Id = this.Id;
            retValue.Num = this.Num;
            retValue.Doc = this.Doc;
            retValue.Info = this.Info;
            retValue.Event = this.Event;
            retValue.Proc = this.Proc;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO KORRAS (LS, FIO, DAT, SUM, NDS, USLUGA, PRICH, GODMES, DATRAS, TIMRAS, ID, NUM, DOC, INFO, EVENT, PROC, OPER, AUTHOR) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, '{7}', CTOD('{8}'), {9}, '{10}', {11}, {12}, '{13}', '{14}', {15}, {16}, '{17}')", Ls.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Sum.ToString().Replace(',','.'), Nds.ToString().Replace(',','.'), Usluga.ToString(), Prich.ToString(), String.IsNullOrEmpty(Godmes) ? "" : Godmes.Trim(), Datras == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datras.Month, Datras.Day, Datras.Year), Timras.ToString(), String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Num.ToString(), Doc.ToString(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), String.IsNullOrEmpty(Event) ? "" : Event.Trim(), Proc.ToString().Replace(',','.'), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
