// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _032_RyagskTeplo
{
    [TableName("SPRNAC.DBF")]
    public partial class SprnacRecord: AbstractRecord
    {
        private Int64 codn;
        // <summary>
        // CODN N(4)
        // </summary>
        [FieldName("CODN"), FieldType('N'), FieldWidth(4)]
        public Int64 Codn
        {
            get { return codn; }
            set { CheckIntegerData("Codn", value, 4); codn = value; }
        }

        private string namem;
        // <summary>
        // NAMEM C(30)
        // </summary>
        [FieldName("NAMEM"), FieldType('C'), FieldWidth(30)]
        public string Namem
        {
            get { return namem; }
            set { CheckStringData("Namem", value, 30); namem = value; }
        }

        private string namew;
        // <summary>
        // NAMEW C(30)
        // </summary>
        [FieldName("NAMEW"), FieldType('C'), FieldWidth(30)]
        public string Namew
        {
            get { return namew; }
            set { CheckStringData("Namew", value, 30); namew = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("CODN")) Codn = Convert.ToInt64(ADataRow["CODN"]); else Codn = 0;
            if (ADataRow.Table.Columns.Contains("NAMEM")) Namem = ADataRow["NAMEM"].ToString(); else Namem = "";
            if (ADataRow.Table.Columns.Contains("NAMEW")) Namew = ADataRow["NAMEW"].ToString(); else Namew = "";
        }
        
        public override AbstractRecord Clone()
        {
            SprnacRecord retValue = new SprnacRecord();
            retValue.Codn = this.Codn;
            retValue.Namem = this.Namem;
            retValue.Namew = this.Namew;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPRNAC (CODN, NAMEM, NAMEW) VALUES ({0}, '{1}', '{2}')", Codn.ToString(), String.IsNullOrEmpty(Namem) ? "" : Namem.Trim(), String.IsNullOrEmpty(Namew) ? "" : Namew.Trim());
            return rs;
        }
    }
}
