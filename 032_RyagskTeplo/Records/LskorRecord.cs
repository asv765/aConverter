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
    [TableName("LSKOR.DBF")]
    public partial class LskorRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(7)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 7); ls = value; }
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

        private string usluga;
        // <summary>
        // USLUGA C(1)
        // </summary>
        [FieldName("USLUGA"), FieldType('C'), FieldWidth(1)]
        public string Usluga
        {
            get { return usluga; }
            set { CheckStringData("Usluga", value, 1); usluga = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = Convert.ToDecimal(ADataRow["SUM"]); else Sum = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = ADataRow["USLUGA"].ToString(); else Usluga = "";
        }
        
        public override AbstractRecord Clone()
        {
            LskorRecord retValue = new LskorRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Sum = this.Sum;
            retValue.Usluga = this.Usluga;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LSKOR (LS, DAT, SUM, USLUGA) VALUES ({0}, '{1}', {2}, '{3}')", Ls.ToString(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Sum.ToString().Replace(',','.'), String.IsNullOrEmpty(Usluga) ? "" : Usluga.Trim());
            return rs;
        }
    }
}
