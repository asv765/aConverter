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
    [TableName("ZAKON.DBF")]
    public partial class ZakonRecord: AbstractRecord
    {
        private Int64 kat;
        // <summary>
        // KAT N(3)
        // </summary>
        [FieldName("KAT"), FieldType('N'), FieldWidth(3)]
        public Int64 Kat
        {
            get { return kat; }
            set { CheckIntegerData("Kat", value, 3); kat = value; }
        }

        private string namezk;
        // <summary>
        // NAMEZK C(50)
        // </summary>
        [FieldName("NAMEZK"), FieldType('C'), FieldWidth(50)]
        public string Namezk
        {
            get { return namezk; }
            set { CheckStringData("Namezk", value, 50); namezk = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("KAT")) Kat = Convert.ToInt64(ADataRow["KAT"]); else Kat = 0;
            if (ADataRow.Table.Columns.Contains("NAMEZK")) Namezk = ADataRow["NAMEZK"].ToString(); else Namezk = "";
        }
        
        public override AbstractRecord Clone()
        {
            ZakonRecord retValue = new ZakonRecord();
            retValue.Kat = this.Kat;
            retValue.Namezk = this.Namezk;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ZAKON (KAT, NAMEZK) VALUES ({0}, '{1}')", Kat.ToString(), String.IsNullOrEmpty(Namezk) ? "" : Namezk.Trim());
            return rs;
        }
    }
}
