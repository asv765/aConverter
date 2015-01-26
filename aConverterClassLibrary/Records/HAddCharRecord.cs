using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableDescription("Дополнительные характеристики домов")]
    [Index("HOUSECD", "HOUSECD")]
    [Index("ADDCHARCD", "ADDCHARCD")]
    [TableName("haddchar.DBF")]
    public class HAddcharRecord: AbstractRecord
    {
        private Int64 housecd;
        // <summary>
        // HOUSECD N(7)
        // </summary>
        [FieldName("HOUSECD"), FieldType('N'), FieldWidth(7), FieldDescription("Уникальный код дома")]
        public Int64 Housecd
        {
            get { return housecd; }
            set { CheckIntegerData("Housecd", value, 7); housecd = value; }
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
            Housecd = Convert.ToInt64(ADataRow["HOUSECD"]);
            Addcharcd = Convert.ToInt64(ADataRow["ADDCHARCD"]);
            Value_ = ADataRow["VALUE"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO haddchar (HOUSECD, ADDCHARCD, VALUE) VALUES ({0}, {1}, '{2}')", Housecd.ToString(), Addcharcd.ToString(), String.IsNullOrEmpty(Value_) ? "" : Value_.Trim());
           return rs;
        }
    }
}
