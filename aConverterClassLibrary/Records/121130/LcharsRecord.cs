// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("LCHARS.DBF")]
    [TableDescription("Справочник качественных характеристик абонентов")]
    [Index("LSHET", "LSHET")]
    public class LcharsRecord: AbstractRecord
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

        private Int64 lcharcd;
        // <summary>
        // LCHARCD N(5)
        // </summary>
        [FieldName("LCHARCD"), FieldType('N'), FieldWidth(5), FieldDescription("Код характеристики")]
        public Int64 Lcharcd
        {
            get { return lcharcd; }
            set { CheckIntegerData("Lcharcd", value, 5); lcharcd = value; }
        }

        private string lcharname;
        // <summary>
        // LCHARNAME C(70)
        // </summary>
        [FieldName("LCHARNAME"), FieldType('C'), FieldWidth(70), FieldDescription("Наименование характеристики")]
        public string Lcharname
        {
            get { return lcharname; }
            set { CheckStringData("Lcharname", value, 70); lcharname = value; }
        }

        private Int64 value_;
        // <summary>
        // VALUE N(4)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(4), FieldDescription("Значение")]
        public Int64 Value_
        {
            get { return value_; }
            set { CheckIntegerData("Value_", value, 4); value_ = value; }
        }

        private string valuedesc;
        // <summary>
        // VALUEDESC C(70)
        // </summary>
        [FieldName("VALUEDESC"), FieldType('C'), FieldWidth(70), FieldDescription("Расшифровка значения")]
        public string Valuedesc
        {
            get { return valuedesc; }
            set { CheckStringData("Valuedesc", value, 70); valuedesc = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldDescription("Дата начала действия значения")]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
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

        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO LCHARS (LSHET, LCHARCD, LCHARNAME, VALUE, VALUEDESC, DATE) VALUES ('{0}', {1}, '{2}', {3}, '{4}', CTOD('{5}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Lcharcd.ToString(), String.IsNullOrEmpty(Lcharname) ? "" : Lcharname.Trim(), Value_.ToString(), String.IsNullOrEmpty(Valuedesc) ? "" : Valuedesc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
           return rs;
        }
    }
}

