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
    [TableName("BARCODE.DBF")]
    public partial class BarcodeRecord: AbstractRecord
    {
        private Int64 id;
        // <summary>
        // ID N(11)
        // </summary>
        [FieldName("ID"), FieldType('N'), FieldWidth(11)]
        public Int64 Id
        {
            get { return id; }
            set { CheckIntegerData("Id", value, 11); id = value; }
        }

        private string name;
        // <summary>
        // NAME C(50)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        private string text;
        // <summary>
        // TEXT C(50)
        // </summary>
        [FieldName("TEXT"), FieldType('C'), FieldWidth(50)]
        public string Text
        {
            get { return text; }
            set { CheckStringData("Text", value, 50); text = value; }
        }

        private string type;
        // <summary>
        // TYPE C(20)
        // </summary>
        [FieldName("TYPE"), FieldType('C'), FieldWidth(20)]
        public string Type
        {
            get { return type; }
            set { CheckStringData("Type", value, 20); type = value; }
        }

        private decimal ratio;
        // <summary>
        // RATIO N(6,2)
        // </summary>
        [FieldName("RATIO"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Ratio
        {
            get { return ratio; }
            set { CheckDecimalData("Ratio", value, 6, 2); ratio = value; }
        }

        private Int64 modul;
        // <summary>
        // MODUL N(3)
        // </summary>
        [FieldName("MODUL"), FieldType('N'), FieldWidth(3)]
        public Int64 Modul
        {
            get { return modul; }
            set { CheckIntegerData("Modul", value, 3); modul = value; }
        }

        private Int64 height;
        // <summary>
        // HEIGHT N(4)
        // </summary>
        [FieldName("HEIGHT"), FieldType('N'), FieldWidth(4)]
        public Int64 Height
        {
            get { return height; }
            set { CheckIntegerData("Height", value, 4); height = value; }
        }

        private Int64 width;
        // <summary>
        // WIDTH N(4)
        // </summary>
        [FieldName("WIDTH"), FieldType('N'), FieldWidth(4)]
        public Int64 Width
        {
            get { return width; }
            set { CheckIntegerData("Width", value, 4); width = value; }
        }

        private Int64 angle;
        // <summary>
        // ANGLE N(4)
        // </summary>
        [FieldName("ANGLE"), FieldType('N'), FieldWidth(4)]
        public Int64 Angle
        {
            get { return angle; }
            set { CheckIntegerData("Angle", value, 4); angle = value; }
        }

        private bool cs;
        // <summary>
        // CS L(1)
        // </summary>
        [FieldName("CS"), FieldType('L'), FieldWidth(1)]
        public bool Cs
        {
            get { return cs; }
            set {  cs = value; }
        }

        private string cs_method;
        // <summary>
        // CS_METHOD C(20)
        // </summary>
        [FieldName("CS_METHOD"), FieldType('C'), FieldWidth(20)]
        public string Cs_method
        {
            get { return cs_method; }
            set { CheckStringData("Cs_method", value, 20); cs_method = value; }
        }

        private string showtext;
        // <summary>
        // SHOWTEXT C(20)
        // </summary>
        [FieldName("SHOWTEXT"), FieldType('C'), FieldWidth(20)]
        public string Showtext
        {
            get { return showtext; }
            set { CheckStringData("Showtext", value, 20); showtext = value; }
        }

        private Int64 top;
        // <summary>
        // TOP N(4)
        // </summary>
        [FieldName("TOP"), FieldType('N'), FieldWidth(4)]
        public Int64 Top
        {
            get { return top; }
            set { CheckIntegerData("Top", value, 4); top = value; }
        }

        private Int64 left;
        // <summary>
        // LEFT N(4)
        // </summary>
        [FieldName("LEFT"), FieldType('N'), FieldWidth(4)]
        public Int64 Left
        {
            get { return left; }
            set { CheckIntegerData("Left", value, 4); left = value; }
        }

        private string dimension;
        // <summary>
        // DIMENSION C(10)
        // </summary>
        [FieldName("DIMENSION"), FieldType('C'), FieldWidth(10)]
        public string Dimension
        {
            get { return dimension; }
            set { CheckStringData("Dimension", value, 10); dimension = value; }
        }

        private string comments;
        // <summary>
        // COMMENTS C(50)
        // </summary>
        [FieldName("COMMENTS"), FieldType('C'), FieldWidth(50)]
        public string Comments
        {
            get { return comments; }
            set { CheckStringData("Comments", value, 50); comments = value; }
        }

        private string author;
        // <summary>
        // AUTHOR C(20)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(20)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 20); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = Convert.ToInt64(ADataRow["ID"]); else Id = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("TEXT")) Text = ADataRow["TEXT"].ToString(); else Text = "";
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("RATIO")) Ratio = Convert.ToDecimal(ADataRow["RATIO"]); else Ratio = 0;
            if (ADataRow.Table.Columns.Contains("MODUL")) Modul = Convert.ToInt64(ADataRow["MODUL"]); else Modul = 0;
            if (ADataRow.Table.Columns.Contains("HEIGHT")) Height = Convert.ToInt64(ADataRow["HEIGHT"]); else Height = 0;
            if (ADataRow.Table.Columns.Contains("WIDTH")) Width = Convert.ToInt64(ADataRow["WIDTH"]); else Width = 0;
            if (ADataRow.Table.Columns.Contains("ANGLE")) Angle = Convert.ToInt64(ADataRow["ANGLE"]); else Angle = 0;
            if (ADataRow.Table.Columns.Contains("CS")) Cs = ADataRow["CS"].ToString() == "True" ? true : false; else Cs = false;
            if (ADataRow.Table.Columns.Contains("CS_METHOD")) Cs_method = ADataRow["CS_METHOD"].ToString(); else Cs_method = "";
            if (ADataRow.Table.Columns.Contains("SHOWTEXT")) Showtext = ADataRow["SHOWTEXT"].ToString(); else Showtext = "";
            if (ADataRow.Table.Columns.Contains("TOP")) Top = Convert.ToInt64(ADataRow["TOP"]); else Top = 0;
            if (ADataRow.Table.Columns.Contains("LEFT")) Left = Convert.ToInt64(ADataRow["LEFT"]); else Left = 0;
            if (ADataRow.Table.Columns.Contains("DIMENSION")) Dimension = ADataRow["DIMENSION"].ToString(); else Dimension = "";
            if (ADataRow.Table.Columns.Contains("COMMENTS")) Comments = ADataRow["COMMENTS"].ToString(); else Comments = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            BarcodeRecord retValue = new BarcodeRecord();
            retValue.Id = this.Id;
            retValue.Name = this.Name;
            retValue.Text = this.Text;
            retValue.Type = this.Type;
            retValue.Ratio = this.Ratio;
            retValue.Modul = this.Modul;
            retValue.Height = this.Height;
            retValue.Width = this.Width;
            retValue.Angle = this.Angle;
            retValue.Cs = this.Cs;
            retValue.Cs_method = this.Cs_method;
            retValue.Showtext = this.Showtext;
            retValue.Top = this.Top;
            retValue.Left = this.Left;
            retValue.Dimension = this.Dimension;
            retValue.Comments = this.Comments;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO BARCODE (ID, NAME, TEXT, TYPE, RATIO, MODUL, HEIGHT, WIDTH, ANGLE, CS, CS_METHOD, SHOWTEXT, TOP, LEFT, DIMENSION, COMMENTS, AUTHOR) VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9}, '{10}', '{11}', {12}, {13}, '{14}', '{15}', '{16}')", Id.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Text) ? "" : Text.Trim(), String.IsNullOrEmpty(Type) ? "" : Type.Trim(), Ratio.ToString().Replace(',','.'), Modul.ToString(), Height.ToString(), Width.ToString(), Angle.ToString(), (Cs ? 0 : 1 ), String.IsNullOrEmpty(Cs_method) ? "" : Cs_method.Trim(), String.IsNullOrEmpty(Showtext) ? "" : Showtext.Trim(), Top.ToString(), Left.ToString(), String.IsNullOrEmpty(Dimension) ? "" : Dimension.Trim(), String.IsNullOrEmpty(Comments) ? "" : Comments.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
