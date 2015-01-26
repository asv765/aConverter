// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace _011_Vodokanal_Ryazan.Records
{
    [TableName("addchars.DBF")]
    public class AddcharsRecord: AbstractRecord
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
        // ADDCHARCD N(7)
        // </summary>
        [FieldName("ADDCHARCD"), FieldType('N'), FieldWidth(7)]
        public Int64 Addcharcd
        {
            get { return addcharcd; }
            set { CheckIntegerData("Addcharcd", value, 7); addcharcd = value; }
        }

        private string addcharnam;
        // <summary>
        // ADDCHARNAM C(30)
        // </summary>
        [FieldName("ADDCHARNAM"), FieldType('C'), FieldWidth(30)]
        public string Addcharnam
        {
            get { return addcharnam; }
            set { CheckStringData("Addcharnam", value, 30); addcharnam = value; }
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
            Addcharnam = ADataRow["ADDCHARNAM"].ToString();
            Value_ = ADataRow["VALUE"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO addchars (LSHET, ADDCHARCD, ADDCHARNAM, VALUE) VALUES ('{0}', {1}, '{2}', '{3}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Addcharcd.ToString(), String.IsNullOrEmpty(Addcharnam) ? "" : Addcharnam.Trim(), String.IsNullOrEmpty(Value_) ? "" : Value_.Trim());
           return rs;
        }
    }
}
