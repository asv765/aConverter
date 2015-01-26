// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableDescription("Дополнительные характеристики абонентов")]
    [Index("LSHET", "LSHET")]
    [Index("ADDCHARCD", "ADDCHARCD")]
    [TableName("aaddchar.DBF")]
    public class AAddcharRecord: AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("Лицевой счет")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 addcharcd;
        // <summary>
        // ADDCHARCD N(7)
        // </summary>
        [FieldName("ADDCHARCD"), FieldType('N'), FieldWidth(7), FieldDescription("Код дополнительной характеристики")]
        public Int64 Addcharcd
        {
            get { return addcharcd; }
            set { CheckIntegerData("Addcharcd", value, 7); addcharcd = value; }
        }

        private string value_;
        // <summary>
        // VALUE C(30)
        // </summary>
        [FieldName("VALUE"), FieldType('C'), FieldWidth(30), FieldDescription("Значение")]
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
           string rs = String.Format("INSERT INTO aaddchar (LSHET, ADDCHARCD, VALUE) VALUES ('{0}', {1}, '{2}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Addcharcd.ToString(), String.IsNullOrEmpty(Value_) ? "" : Value_.Trim());
           return rs;
        }
    }
}
