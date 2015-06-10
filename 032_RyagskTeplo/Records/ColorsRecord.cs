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
    [TableName("COLORS.DBF")]
    public partial class ColorsRecord: AbstractRecord
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

        private Int64 user;
        // <summary>
        // USER N(6)
        // </summary>
        [FieldName("USER"), FieldType('N'), FieldWidth(6)]
        public Int64 User
        {
            get { return user; }
            set { CheckIntegerData("User", value, 6); user = value; }
        }

        private Int64 npp;
        // <summary>
        // NPP N(4)
        // </summary>
        [FieldName("NPP"), FieldType('N'), FieldWidth(4)]
        public Int64 Npp
        {
            get { return npp; }
            set { CheckIntegerData("Npp", value, 4); npp = value; }
        }

        private string condition;
        // <summary>
        // CONDITION C(100)
        // </summary>
        [FieldName("CONDITION"), FieldType('C'), FieldWidth(100)]
        public string Condition
        {
            get { return condition; }
            set { CheckStringData("Condition", value, 100); condition = value; }
        }

        private Int64 color;
        // <summary>
        // COLOR N(16)
        // </summary>
        [FieldName("COLOR"), FieldType('N'), FieldWidth(16)]
        public Int64 Color
        {
            get { return color; }
            set { CheckIntegerData("Color", value, 16); color = value; }
        }

        private Int64 color_a;
        // <summary>
        // COLOR_A N(16)
        // </summary>
        [FieldName("COLOR_A"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_a
        {
            get { return color_a; }
            set { CheckIntegerData("Color_a", value, 16); color_a = value; }
        }

        private string srift;
        // <summary>
        // SRIFT C(30)
        // </summary>
        [FieldName("SRIFT"), FieldType('C'), FieldWidth(30)]
        public string Srift
        {
            get { return srift; }
            set { CheckStringData("Srift", value, 30); srift = value; }
        }

        private Int64 size;
        // <summary>
        // SIZE N(4)
        // </summary>
        [FieldName("SIZE"), FieldType('N'), FieldWidth(4)]
        public Int64 Size
        {
            get { return size; }
            set { CheckIntegerData("Size", value, 4); size = value; }
        }

        private bool bold;
        // <summary>
        // BOLD L(1)
        // </summary>
        [FieldName("BOLD"), FieldType('L'), FieldWidth(1)]
        public bool Bold
        {
            get { return bold; }
            set {  bold = value; }
        }

        private bool italic;
        // <summary>
        // ITALIC L(1)
        // </summary>
        [FieldName("ITALIC"), FieldType('L'), FieldWidth(1)]
        public bool Italic
        {
            get { return italic; }
            set {  italic = value; }
        }

        private bool underline;
        // <summary>
        // UNDERLINE L(1)
        // </summary>
        [FieldName("UNDERLINE"), FieldType('L'), FieldWidth(1)]
        public bool Underline
        {
            get { return underline; }
            set {  underline = value; }
        }

        private bool strikeout;
        // <summary>
        // STRIKEOUT L(1)
        // </summary>
        [FieldName("STRIKEOUT"), FieldType('L'), FieldWidth(1)]
        public bool Strikeout
        {
            get { return strikeout; }
            set {  strikeout = value; }
        }

        private Int64 column;
        // <summary>
        // COLUMN N(3)
        // </summary>
        [FieldName("COLUMN"), FieldType('N'), FieldWidth(3)]
        public Int64 Column
        {
            get { return column; }
            set { CheckIntegerData("Column", value, 3); column = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("USER")) User = Convert.ToInt64(ADataRow["USER"]); else User = 0;
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("CONDITION")) Condition = ADataRow["CONDITION"].ToString(); else Condition = "";
            if (ADataRow.Table.Columns.Contains("COLOR")) Color = Convert.ToInt64(ADataRow["COLOR"]); else Color = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_A")) Color_a = Convert.ToInt64(ADataRow["COLOR_A"]); else Color_a = 0;
            if (ADataRow.Table.Columns.Contains("SRIFT")) Srift = ADataRow["SRIFT"].ToString(); else Srift = "";
            if (ADataRow.Table.Columns.Contains("SIZE")) Size = Convert.ToInt64(ADataRow["SIZE"]); else Size = 0;
            if (ADataRow.Table.Columns.Contains("BOLD")) Bold = ADataRow["BOLD"].ToString() == "True" ? true : false; else Bold = false;
            if (ADataRow.Table.Columns.Contains("ITALIC")) Italic = ADataRow["ITALIC"].ToString() == "True" ? true : false; else Italic = false;
            if (ADataRow.Table.Columns.Contains("UNDERLINE")) Underline = ADataRow["UNDERLINE"].ToString() == "True" ? true : false; else Underline = false;
            if (ADataRow.Table.Columns.Contains("STRIKEOUT")) Strikeout = ADataRow["STRIKEOUT"].ToString() == "True" ? true : false; else Strikeout = false;
            if (ADataRow.Table.Columns.Contains("COLUMN")) Column = Convert.ToInt64(ADataRow["COLUMN"]); else Column = 0;
        }
        
        public override AbstractRecord Clone()
        {
            ColorsRecord retValue = new ColorsRecord();
            retValue.Id = this.Id;
            retValue.User = this.User;
            retValue.Npp = this.Npp;
            retValue.Condition = this.Condition;
            retValue.Color = this.Color;
            retValue.Color_a = this.Color_a;
            retValue.Srift = this.Srift;
            retValue.Size = this.Size;
            retValue.Bold = this.Bold;
            retValue.Italic = this.Italic;
            retValue.Underline = this.Underline;
            retValue.Strikeout = this.Strikeout;
            retValue.Column = this.Column;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COLORS (ID, USER, NPP, CONDITION, COLOR, COLOR_A, SRIFT, SIZE, BOLD, ITALIC, UNDERLINE, STRIKEOUT, COLUMN) VALUES ('{0}', {1}, {2}, '{3}', {4}, {5}, '{6}', {7}, {8}, {9}, {10}, {11}, {12})", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), User.ToString(), Npp.ToString(), String.IsNullOrEmpty(Condition) ? "" : Condition.Trim(), Color.ToString(), Color_a.ToString(), String.IsNullOrEmpty(Srift) ? "" : Srift.Trim(), Size.ToString(), (Bold ? 0 : 1 ), (Italic ? 0 : 1 ), (Underline ? 0 : 1 ), (Strikeout ? 0 : 1 ), Column.ToString());
            return rs;
        }
    }
}
