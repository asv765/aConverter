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
    [TableName("ULICI.DBF")]
    public partial class UliciRecord: AbstractRecord
    {
        private Int64 codul;
        // <summary>
        // CODUL N(6)
        // </summary>
        [FieldName("CODUL"), FieldType('N'), FieldWidth(6)]
        public Int64 Codul
        {
            get { return codul; }
            set { CheckIntegerData("Codul", value, 6); codul = value; }
        }

        private string nameul;
        // <summary>
        // NAMEUL C(50)
        // </summary>
        [FieldName("NAMEUL"), FieldType('C'), FieldWidth(50)]
        public string Nameul
        {
            get { return nameul; }
            set { CheckStringData("Nameul", value, 50); nameul = value; }
        }

        private string statul;
        // <summary>
        // STATUL C(15)
        // </summary>
        [FieldName("STATUL"), FieldType('C'), FieldWidth(15)]
        public string Statul
        {
            get { return statul; }
            set { CheckStringData("Statul", value, 15); statul = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("CODUL")) Codul = Convert.ToInt64(ADataRow["CODUL"]); else Codul = 0;
            if (ADataRow.Table.Columns.Contains("NAMEUL")) Nameul = ADataRow["NAMEUL"].ToString(); else Nameul = "";
            if (ADataRow.Table.Columns.Contains("STATUL")) Statul = ADataRow["STATUL"].ToString(); else Statul = "";
        }
        
        public override AbstractRecord Clone()
        {
            UliciRecord retValue = new UliciRecord();
            retValue.Codul = this.Codul;
            retValue.Nameul = this.Nameul;
            retValue.Statul = this.Statul;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ULICI (CODUL, NAMEUL, STATUL) VALUES ({0}, '{1}', '{2}')", Codul.ToString(), String.IsNullOrEmpty(Nameul) ? "" : Nameul.Trim(), String.IsNullOrEmpty(Statul) ? "" : Statul.Trim());
            return rs;
        }
    }
}
