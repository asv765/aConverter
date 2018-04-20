// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _048_Rgmek
{
    [TableName("sPeni.DBF")]
    public partial class SpeniRecord : AbstractRecord
    {
        private string account;
        // <summary>
        // ACCOUNT C(19)
        // </summary>
        [FieldName("ACCOUNT"), FieldType('C'), FieldWidth(19)]
        public string Account
        {
            get { return account; }
            set { CheckStringData("Account", value, 19); account = value; }
        }

        private string kind;
        // <summary>
        // KIND C(20)
        // </summary>
        [FieldName("KIND"), FieldType('C'), FieldWidth(20)]
        public string Kind
        {
            get { return kind; }
            set { CheckStringData("Kind", value, 20); kind = value; }
        }

        private decimal debt;
        // <summary>
        // DEBT N(10,2)
        // </summary>
        [FieldName("DEBT"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Debt
        {
            get { return debt; }
            set { CheckDecimalData("Debt", value, 10, 2); debt = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ACCOUNT")) Account = ADataRow["ACCOUNT"].ToString(); else Account = "";
            if (ADataRow.Table.Columns.Contains("KIND")) Kind = ADataRow["KIND"].ToString(); else Kind = "";
            if (ADataRow.Table.Columns.Contains("DEBT")) Debt = Convert.ToDecimal(ADataRow["DEBT"]); else Debt = 0;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            SpeniRecord retValue = new SpeniRecord();
            retValue.Account = this.Account;
            retValue.Kind = this.Kind;
            retValue.Debt = this.Debt;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO sPeni (ACCOUNT, KIND, DEBT, DATE) VALUES ('{0}', '{1}', {2}, CTOD('{3}'))", String.IsNullOrEmpty(Account) ? "" : Account.Trim(), String.IsNullOrEmpty(Kind) ? "" : Kind.Trim(), Debt.ToString().Replace(',', '.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}