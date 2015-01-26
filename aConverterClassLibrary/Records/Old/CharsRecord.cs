// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("CHARS.DBF")]
    [Index("LSHET", "LSHET")]
    public class CharsRecord: AbstractRecord
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

        private Int64 charcd;
        // <summary>
        // CHARCD N(5)
        // </summary>
        [FieldName("CHARCD"), FieldType('N'), FieldWidth(5)]
        public Int64 Charcd
        {
            get { return charcd; }
            set { CheckIntegerData("Charcd", value, 5); charcd = value; }
        }

        private string charname;
        // <summary>
        // CHARNAME C(50)
        // </summary>
        [FieldName("CHARNAME"), FieldType('C'), FieldWidth(50)]
        public string Charname
        {
            get { return charname; }
            set { CheckStringData("Charname", value, 50); charname = value; }
        }

        private decimal value_;
        // <summary>
        // VALUE N(9,2)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(9), FieldDec(2)]
        public decimal Value_
        {
            get { return value_; }
            set { CheckDecimalData("Value_", value, 9, 2); value_ = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D
        // </summary>
        [FieldName("DATE"), FieldType('D')]
        public DateTime Date
        {
            get { return date; }
            set { CheckData("Date", value); date = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Charcd = Convert.ToInt64(ADataRow["CHARCD"]);
            Charname = ADataRow["CHARNAME"].ToString();
            Value_ = Convert.ToDecimal(ADataRow["VALUE"]);
            Date = Convert.ToDateTime(ADataRow["DATE"]);
        }
    }
}

