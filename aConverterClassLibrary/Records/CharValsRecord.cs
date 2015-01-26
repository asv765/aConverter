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
    [TableName("CHARVALS.DBF")]
    [TableDescription("Расшифровки значений качественных дпоолнительных характеристик")]
    [Index("ADDCHARCD","ADDCHARCD")]
    public partial class CharvalsRecord : AbstractRecord
    {
        private Int64 addcharcd;
        // <summary>
        // Код дополнительной характеристики
        // </summary>
        [FieldName("ADDCHARCD"), FieldType('N'), FieldWidth(5), FieldDescription("Код дополнительной характеристики")]
        public Int64 Addcharcd
        {
            get { return addcharcd; }
            set { CheckIntegerData("Addcharcd", value, 5); addcharcd = value; }
        }

        private Int64 addcvalue;
        // <summary>
        // ADDCVALUE N(11)
        // </summary>
        [FieldName("ADDCVALUE"), FieldType('N'), FieldWidth(11), FieldDescription("Значение дополнительной характеристики")]
        public Int64 Addcvalue
        {
            get { return addcvalue; }
            set { CheckIntegerData("Addcvalue", value, 11); addcvalue = value; }
        }

        private string addcname;
        // <summary>
        // ADDCNAME C(50)
        // </summary>
        [FieldName("ADDCNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Расшифровка значения дополнительной характеристики")]
        public string Addcname
        {
            get { return addcname; }
            set { CheckStringData("Addcname", value, 50); addcname = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ADDCHARCD")) Addcharcd = Convert.ToInt64(ADataRow["ADDCHARCD"]); else Addcharcd = 0;
            if (ADataRow.Table.Columns.Contains("ADDCVALUE")) Addcvalue = Convert.ToInt64(ADataRow["ADDCVALUE"]); else Addcvalue = 0;
            if (ADataRow.Table.Columns.Contains("ADDCNAME")) Addcname = ADataRow["ADDCNAME"].ToString(); else Addcname = "";
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CHARVALS (ADDCHARCD, ADDCVALUE, ADDCNAME) VALUES ({0}, {1}, '{2}')", Addcharcd.ToString(), Addcvalue.ToString(), String.IsNullOrEmpty(Addcname) ? "" : Addcname.Trim());
            return rs;
        }
    }
}
