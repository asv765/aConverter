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
    [TableName("Forms.DBF")]
    public partial class FormsRecord: AbstractRecord
    {
        private string id;
        // <summary>
        // ID C(10)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(10)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 10); id = value; }
        }

        private string id1;
        // <summary>
        // ID1 C(10)
        // </summary>
        [FieldName("ID1"), FieldType('C'), FieldWidth(10)]
        public string Id1
        {
            get { return id1; }
            set { CheckStringData("Id1", value, 10); id1 = value; }
        }

        private string name;
        // <summary>
        // NAME C(30)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(30)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 30); name = value; }
        }

        private Int64 oper;
        // <summary>
        // OPER N(6)
        // </summary>
        [FieldName("OPER"), FieldType('N'), FieldWidth(6)]
        public Int64 Oper
        {
            get { return oper; }
            set { CheckIntegerData("Oper", value, 6); oper = value; }
        }

        private Int64 num;
        // <summary>
        // NUM N(4)
        // </summary>
        [FieldName("NUM"), FieldType('N'), FieldWidth(4)]
        public Int64 Num
        {
            get { return num; }
            set { CheckIntegerData("Num", value, 4); num = value; }
        }

        private string list;
        // <summary>
        // LIST M
        // </summary>
        [FieldName("LIST"), FieldType('M')]
        public string List
        {
            get { return list; }
            set { CheckStringData("List", value, 0); list = value; }
        }

        private string form;
        // <summary>
        // FORM M
        // </summary>
        [FieldName("FORM"), FieldType('M')]
        public string Form
        {
            get { return form; }
            set { CheckStringData("Form", value, 0); form = value; }
        }

        private string colors;
        // <summary>
        // COLORS M
        // </summary>
        [FieldName("COLORS"), FieldType('M')]
        public string Colors
        {
            get { return colors; }
            set { CheckStringData("Colors", value, 0); colors = value; }
        }

        private string parms;
        // <summary>
        // PARMS M
        // </summary>
        [FieldName("PARMS"), FieldType('M')]
        public string Parms
        {
            get { return parms; }
            set { CheckStringData("Parms", value, 0); parms = value; }
        }

        private string formula;
        // <summary>
        // FORMULA M
        // </summary>
        [FieldName("FORMULA"), FieldType('M')]
        public string Formula
        {
            get { return formula; }
            set { CheckStringData("Formula", value, 0); formula = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("ID1")) Id1 = ADataRow["ID1"].ToString(); else Id1 = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("LIST")) List = ADataRow["LIST"].ToString(); else List = "";
            if (ADataRow.Table.Columns.Contains("FORM")) Form = ADataRow["FORM"].ToString(); else Form = "";
            if (ADataRow.Table.Columns.Contains("COLORS")) Colors = ADataRow["COLORS"].ToString(); else Colors = "";
            if (ADataRow.Table.Columns.Contains("PARMS")) Parms = ADataRow["PARMS"].ToString(); else Parms = "";
            if (ADataRow.Table.Columns.Contains("FORMULA")) Formula = ADataRow["FORMULA"].ToString(); else Formula = "";
        }
        
        public override AbstractRecord Clone()
        {
            FormsRecord retValue = new FormsRecord();
            retValue.Id = this.Id;
            retValue.Id1 = this.Id1;
            retValue.Name = this.Name;
            retValue.Oper = this.Oper;
            retValue.Num = this.Num;
            retValue.List = this.List;
            retValue.Form = this.Form;
            retValue.Colors = this.Colors;
            retValue.Parms = this.Parms;
            retValue.Formula = this.Formula;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO Forms (ID, ID1, NAME, OPER, NUM, LIST, FORM, COLORS, PARMS, FORMULA) VALUES ('{0}', '{1}', '{2}', {3}, {4}, '{5}', '{6}', '{7}', '{8}', '{9}')", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), String.IsNullOrEmpty(Id1) ? "" : Id1.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Oper.ToString(), Num.ToString(), String.IsNullOrEmpty(List) ? "" : List.Trim(), String.IsNullOrEmpty(Form) ? "" : Form.Trim(), String.IsNullOrEmpty(Colors) ? "" : Colors.Trim(), String.IsNullOrEmpty(Parms) ? "" : Parms.Trim(), String.IsNullOrEmpty(Formula) ? "" : Formula.Trim());
            return rs;
        }
    }
}
