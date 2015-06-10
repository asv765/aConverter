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
    [TableName("SPREDIZ.DBF")]
    public partial class SpredizRecord: AbstractRecord
    {
        private Int64 cod;
        // <summary>
        // COD N(4)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(4)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 4); cod = value; }
        }

        private string name_full;
        // <summary>
        // NAME_FULL C(100)
        // </summary>
        [FieldName("NAME_FULL"), FieldType('C'), FieldWidth(100)]
        public string Name_full
        {
            get { return name_full; }
            set { CheckStringData("Name_full", value, 100); name_full = value; }
        }

        private string name_short;
        // <summary>
        // NAME_SHORT C(10)
        // </summary>
        [FieldName("NAME_SHORT"), FieldType('C'), FieldWidth(10)]
        public string Name_short
        {
            get { return name_short; }
            set { CheckStringData("Name_short", value, 10); name_short = value; }
        }

        private bool count;
        // <summary>
        // COUNT L(1)
        // </summary>
        [FieldName("COUNT"), FieldType('L'), FieldWidth(1)]
        public bool Count
        {
            get { return count; }
            set {  count = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME_FULL")) Name_full = ADataRow["NAME_FULL"].ToString(); else Name_full = "";
            if (ADataRow.Table.Columns.Contains("NAME_SHORT")) Name_short = ADataRow["NAME_SHORT"].ToString(); else Name_short = "";
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = ADataRow["COUNT"].ToString() == "True" ? true : false; else Count = false;
        }
        
        public override AbstractRecord Clone()
        {
            SpredizRecord retValue = new SpredizRecord();
            retValue.Cod = this.Cod;
            retValue.Name_full = this.Name_full;
            retValue.Name_short = this.Name_short;
            retValue.Count = this.Count;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPREDIZ (COD, NAME_FULL, NAME_SHORT, COUNT) VALUES ({0}, '{1}', '{2}', {3})", Cod.ToString(), String.IsNullOrEmpty(Name_full) ? "" : Name_full.Trim(), String.IsNullOrEmpty(Name_short) ? "" : Name_short.Trim(), (Count ? 0 : 1 ));
            return rs;
        }
    }
}
