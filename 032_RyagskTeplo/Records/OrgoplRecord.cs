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
    [TableName("ORGOPL.DBF")]
    public partial class OrgoplRecord: AbstractRecord
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

        private string name;
        // <summary>
        // NAME C(40)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(40)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 40); name = value; }
        }

        private bool label;
        // <summary>
        // LABEL L(1)
        // </summary>
        [FieldName("LABEL"), FieldType('L'), FieldWidth(1)]
        public bool Label
        {
            get { return label; }
            set {  label = value; }
        }

        private decimal proc;
        // <summary>
        // PROC N(6,2)
        // </summary>
        [FieldName("PROC"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Proc
        {
            get { return proc; }
            set { CheckDecimalData("Proc", value, 6, 2); proc = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("LABEL")) Label = ADataRow["LABEL"].ToString() == "True" ? true : false; else Label = false;
            if (ADataRow.Table.Columns.Contains("PROC")) Proc = Convert.ToDecimal(ADataRow["PROC"]); else Proc = 0;
        }
        
        public override AbstractRecord Clone()
        {
            OrgoplRecord retValue = new OrgoplRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Label = this.Label;
            retValue.Proc = this.Proc;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ORGOPL (COD, NAME, LABEL, PROC) VALUES ({0}, '{1}', {2}, {3})", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), (Label ? 0 : 1 ), Proc.ToString().Replace(',','.'));
            return rs;
        }
    }
}
