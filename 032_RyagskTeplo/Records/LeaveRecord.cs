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
    [TableName("LEAVE.DBF")]
    public partial class LeaveRecord: AbstractRecord
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

        private Int64 id_kor;
        // <summary>
        // ID_KOR N(11)
        // </summary>
        [FieldName("ID_KOR"), FieldType('N'), FieldWidth(11)]
        public Int64 Id_kor
        {
            get { return id_kor; }
            set { CheckIntegerData("Id_kor", value, 11); id_kor = value; }
        }

        private DateTime datub;
        // <summary>
        // DATUB D(8)
        // </summary>
        [FieldName("DATUB"), FieldType('D'), FieldWidth(8)]
        public DateTime Datub
        {
            get { return datub; }
            set {  datub = value; }
        }

        private DateTime datpr;
        // <summary>
        // DATPR D(8)
        // </summary>
        [FieldName("DATPR"), FieldType('D'), FieldWidth(8)]
        public DateTime Datpr
        {
            get { return datpr; }
            set {  datpr = value; }
        }

        private Int64 kol;
        // <summary>
        // KOL N(4)
        // </summary>
        [FieldName("KOL"), FieldType('N'), FieldWidth(4)]
        public Int64 Kol
        {
            get { return kol; }
            set { CheckIntegerData("Kol", value, 4); kol = value; }
        }

        private Int64 kollg;
        // <summary>
        // KOLLG N(4)
        // </summary>
        [FieldName("KOLLG"), FieldType('N'), FieldWidth(4)]
        public Int64 Kollg
        {
            get { return kollg; }
            set { CheckIntegerData("Kollg", value, 4); kollg = value; }
        }

        private string info;
        // <summary>
        // INFO C(250)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(250)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 250); info = value; }
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
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("ID_KOR")) Id_kor = Convert.ToInt64(ADataRow["ID_KOR"]); else Id_kor = 0;
            if (ADataRow.Table.Columns.Contains("DATUB")) Datub = Convert.ToDateTime(ADataRow["DATUB"]); else Datub = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATPR")) Datpr = Convert.ToDateTime(ADataRow["DATPR"]); else Datpr = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("KOL")) Kol = Convert.ToInt64(ADataRow["KOL"]); else Kol = 0;
            if (ADataRow.Table.Columns.Contains("KOLLG")) Kollg = Convert.ToInt64(ADataRow["KOLLG"]); else Kollg = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            LeaveRecord retValue = new LeaveRecord();
            retValue.Ls = this.Ls;
            retValue.Fio = this.Fio;
            retValue.Dat = this.Dat;
            retValue.Num = this.Num;
            retValue.Id = this.Id;
            retValue.Id_kor = this.Id_kor;
            retValue.Datub = this.Datub;
            retValue.Datpr = this.Datpr;
            retValue.Kol = this.Kol;
            retValue.Kollg = this.Kollg;
            retValue.Info = this.Info;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LEAVE (LS, FIO, DAT, NUM, ID, ID_KOR, DATUB, DATPR, KOL, KOLLG, INFO, AUTHOR) VALUES ({0}, '{1}', CTOD('{2}'), {3}, '{4}', {5}, CTOD('{6}'), CTOD('{7}'), {8}, {9}, '{10}', '{11}')", Ls.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Num.ToString(), String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Id_kor.ToString(), Datub == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datub.Month, Datub.Day, Datub.Year), Datpr == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datpr.Month, Datpr.Day, Datpr.Year), Kol.ToString(), Kollg.ToString(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
