// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("LCHARS.DBF")]
    [Index("LSHET", "LSHET")]
    public class LcharsRecord: AbstractRecord
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

        private Int64 lcharcd;
        // <summary>
        // LCHARCD N(5)
        // </summary>
        [FieldName("LCHARCD"), FieldType('N'), FieldWidth(5)]
        public Int64 Lcharcd
        {
            get { return lcharcd; }
            set { CheckIntegerData("Lcharcd", value, 5); lcharcd = value; }
        }

        private string lcharname;
        // <summary>
        // LCHARNAME C(50)
        // </summary>
        [FieldName("LCHARNAME"), FieldType('C'), FieldWidth(50)]
        public string Lcharname
        {
            get { return lcharname; }
            set { CheckStringData("Lcharname", value, 50); lcharname = value; }
        }

        private Int64 value_;
        // <summary>
        // VALUE N(4)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(4)]
        public Int64 Value_
        {
            get { return value_; }
            set { CheckIntegerData("Value_", value, 4); value_ = value; }
        }

        private string valuedesc;
        // <summary>
        // VALUEDESC C(50)
        // </summary>
        [FieldName("VALUEDESC"), FieldType('C'), FieldWidth(50)]
        public string Valuedesc
        {
            get { return valuedesc; }
            set { CheckStringData("Valuedesc", value, 50); valuedesc = value; }
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
            Lcharcd = Convert.ToInt64(ADataRow["LCHARCD"]);
            Lcharname = ADataRow["LCHARNAME"].ToString();
            Value_ = Convert.ToInt64(ADataRow["VALUE"]);
            Valuedesc = ADataRow["VALUEDESC"].ToString();
            Date = Convert.ToDateTime(ADataRow["DATE"]);
        }
    }
}

