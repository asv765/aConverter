// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("CHARS.DBF")]
    [TableDescription("Справочник количественных характеристик абонентов")]
    [Index("LSHET", "LSHET")]
    public class CharsRecord: AbstractRecord
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

        private Int64 charcd;
        // <summary>
        // CHARCD N(5)
        // </summary>
        [FieldName("CHARCD"), FieldType('N'), FieldWidth(5), FieldDescription("Код характеристики")]
        public Int64 Charcd
        {
            get { return charcd; }
            set { CheckIntegerData("Charcd", value, 5); charcd = value; }
        }

        private string charname;
        // <summary>
        // CHARNAME C(50)
        // </summary>
        [FieldName("CHARNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование")]
        public string Charname
        {
            get { return charname; }
            set { CheckStringData("Charname", value, 50); charname = value; }
        }

        private decimal value_;
        // <summary>
        // VALUE N(9,4)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(9), FieldDec(4), FieldDescription("Значение")]
        public decimal Value_
        {
            get { return value_; }
            set { CheckDecimalData("Value_", value, 9, 4); value_ = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldDescription("Дата начала действия")]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Charcd = Convert.ToInt64(ADataRow["CHARCD"]);
            Charname = ADataRow["CHARNAME"].ToString();
            Value_ = Convert.ToDecimal(ADataRow["VALUE"]);
            Date = Convert.ToDateTime(ADataRow["DATE"]);
        }

        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO CHARS (LSHET, CHARCD, CHARNAME, VALUE, DATE) VALUES ('{0}', {1}, '{2}', {3}, CTOD('{4}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Charcd.ToString(), String.IsNullOrEmpty(Charname) ? "" : Charname.Trim(), Value_.ToString().Replace(',','.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
           return rs;
        }
    }
}

