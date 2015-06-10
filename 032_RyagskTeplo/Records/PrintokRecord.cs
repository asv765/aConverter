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
    [TableName("PRINTOK.DBF")]
    public partial class PrintokRecord: AbstractRecord
    {
        private Int64 codc;
        // <summary>
        // CODC N(6)
        // </summary>
        [FieldName("CODC"), FieldType('N'), FieldWidth(6)]
        public Int64 Codc
        {
            get { return codc; }
            set { CheckIntegerData("Codc", value, 6); codc = value; }
        }

        private string name;
        // <summary>
        // NAME C(50)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        private Int64 count;
        // <summary>
        // COUNT N(11)
        // </summary>
        [FieldName("COUNT"), FieldType('N'), FieldWidth(11)]
        public Int64 Count
        {
            get { return count; }
            set { CheckIntegerData("Count", value, 11); count = value; }
        }

        private Int64 count_ls;
        // <summary>
        // COUNT_LS N(11)
        // </summary>
        [FieldName("COUNT_LS"), FieldType('N'), FieldWidth(11)]
        public Int64 Count_ls
        {
            get { return count_ls; }
            set { CheckIntegerData("Count_ls", value, 11); count_ls = value; }
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

        private Int64 recno;
        // <summary>
        // RECNO N(11)
        // </summary>
        [FieldName("RECNO"), FieldType('N'), FieldWidth(11)]
        public Int64 Recno
        {
            get { return recno; }
            set { CheckIntegerData("Recno", value, 11); recno = value; }
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
            if (ADataRow.Table.Columns.Contains("CODC")) Codc = Convert.ToInt64(ADataRow["CODC"]); else Codc = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = Convert.ToInt64(ADataRow["COUNT"]); else Count = 0;
            if (ADataRow.Table.Columns.Contains("COUNT_LS")) Count_ls = Convert.ToInt64(ADataRow["COUNT_LS"]); else Count_ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("RECNO")) Recno = Convert.ToInt64(ADataRow["RECNO"]); else Recno = 0;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            PrintokRecord retValue = new PrintokRecord();
            retValue.Codc = this.Codc;
            retValue.Name = this.Name;
            retValue.Count = this.Count;
            retValue.Count_ls = this.Count_ls;
            retValue.Dat = this.Dat;
            retValue.Recno = this.Recno;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PRINTOK (CODC, NAME, COUNT, COUNT_LS, DAT, RECNO, OPER, AUTHOR) VALUES ({0}, '{1}', {2}, {3}, CTOD('{4}'), {5}, {6}, '{7}')", Codc.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Count.ToString(), Count_ls.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Recno.ToString(), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
