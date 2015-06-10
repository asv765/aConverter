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
    [TableName("SERVPAY.DBF")]
    public partial class ServpayRecord: AbstractRecord
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
        // FIO C(50)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(50)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 50); fio = value; }
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

        private Int64 codserv;
        // <summary>
        // CODSERV N(4)
        // </summary>
        [FieldName("CODSERV"), FieldType('N'), FieldWidth(4)]
        public Int64 Codserv
        {
            get { return codserv; }
            set { CheckIntegerData("Codserv", value, 4); codserv = value; }
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

        private string fio_isp;
        // <summary>
        // FIO_ISP C(50)
        // </summary>
        [FieldName("FIO_ISP"), FieldType('C'), FieldWidth(50)]
        public string Fio_isp
        {
            get { return fio_isp; }
            set { CheckStringData("Fio_isp", value, 50); fio_isp = value; }
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
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("CODSERV")) Codserv = Convert.ToInt64(ADataRow["CODSERV"]); else Codserv = 0;
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = Convert.ToDecimal(ADataRow["SUM"]); else Sum = 0;
            if (ADataRow.Table.Columns.Contains("FIO_ISP")) Fio_isp = ADataRow["FIO_ISP"].ToString(); else Fio_isp = "";
            if (ADataRow.Table.Columns.Contains("COMMENTS")) Comments = ADataRow["COMMENTS"].ToString(); else Comments = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            ServpayRecord retValue = new ServpayRecord();
            retValue.Ls = this.Ls;
            retValue.Fio = this.Fio;
            retValue.Dat = this.Dat;
            retValue.Codserv = this.Codserv;
            retValue.Sum = this.Sum;
            retValue.Fio_isp = this.Fio_isp;
            retValue.Comments = this.Comments;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SERVPAY (LS, FIO, DAT, CODSERV, SUM, FIO_ISP, COMMENTS, AUTHOR) VALUES ({0}, '{1}', CTOD('{2}'), {3}, {4}, '{5}', '{6}', '{7}')", Ls.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Codserv.ToString(), Sum.ToString().Replace(',','.'), String.IsNullOrEmpty(Fio_isp) ? "" : Fio_isp.Trim(), String.IsNullOrEmpty(Comments) ? "" : Comments.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
