// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("abnchars.DBF")]
    public class AbncharsRecord: AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 addcharcd;
        // <summary>
        // ADDCHARCD N(9)
        // </summary>
        [FieldName("ADDCHARCD"), FieldType('N'), FieldWidth(9)]
        public Int64 Addcharcd
        {
            get { return addcharcd; }
            set { CheckIntegerData("Addcharcd", value, 9); addcharcd = value; }
        }

        private string value_;
        // <summary>
        // VALUE C(30)
        // </summary>
        [FieldName("VALUE"), FieldType('C'), FieldWidth(30)]
        public string Value_
        {
            get { return value_; }
            set { CheckStringData("Value_", value, 30); value_ = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Addcharcd = Convert.ToInt64(ADataRow["ADDCHARCD"]);
            Value_ = ADataRow["VALUE"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO abnchars (LSHET, ADDCHARCD, VALUE) VALUES ('{0}', {1}, '{2}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Addcharcd.ToString(), String.IsNullOrEmpty(Value_) ? "" : Value_.Trim());
           return rs;
        }
    }
}
