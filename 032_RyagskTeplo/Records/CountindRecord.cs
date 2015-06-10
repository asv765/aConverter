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
    [TableName("COUNTIND.DBF")]
    public partial class CountindRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(11)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(11)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 11); ls = value; }
        }

        private Int64 usluga;
        // <summary>
        // USLUGA N(4)
        // </summary>
        [FieldName("USLUGA"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga
        {
            get { return usluga; }
            set { CheckIntegerData("Usluga", value, 4); usluga = value; }
        }

        private Int64 code;
        // <summary>
        // CODE N(6)
        // </summary>
        [FieldName("CODE"), FieldType('N'), FieldWidth(6)]
        public Int64 Code
        {
            get { return code; }
            set { CheckIntegerData("Code", value, 6); code = value; }
        }

        private string dat;
        // <summary>
        // DAT C(6)
        // </summary>
        [FieldName("DAT"), FieldType('C'), FieldWidth(6)]
        public string Dat
        {
            get { return dat; }
            set { CheckStringData("Dat", value, 6); dat = value; }
        }

        private DateTime datf;
        // <summary>
        // DATF D(8)
        // </summary>
        [FieldName("DATF"), FieldType('D'), FieldWidth(8)]
        public DateTime Datf
        {
            get { return datf; }
            set {  datf = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(15,5)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 15, 5); indication = value; }
        }

        private decimal value_old;
        // <summary>
        // VALUE_OLD N(15,5)
        // </summary>
        [FieldName("VALUE_OLD"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Value_old
        {
            get { return value_old; }
            set { CheckDecimalData("Value_old", value, 15, 5); value_old = value; }
        }

        private decimal value_sred;
        // <summary>
        // VALUE_SRED N(15,5)
        // </summary>
        [FieldName("VALUE_SRED"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Value_sred
        {
            get { return value_sred; }
            set { CheckDecimalData("Value_sred", value, 15, 5); value_sred = value; }
        }

        private decimal value_norm;
        // <summary>
        // VALUE_NORM N(15,5)
        // </summary>
        [FieldName("VALUE_NORM"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Value_norm
        {
            get { return value_norm; }
            set { CheckDecimalData("Value_norm", value, 15, 5); value_norm = value; }
        }

        private Int64 type_input;
        // <summary>
        // TYPE_INPUT N(2)
        // </summary>
        [FieldName("TYPE_INPUT"), FieldType('N'), FieldWidth(2)]
        public Int64 Type_input
        {
            get { return type_input; }
            set { CheckIntegerData("Type_input", value, 2); type_input = value; }
        }

        private string info;
        // <summary>
        // INFO C(50)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(50)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 50); info = value; }
        }

        private Int64 oper;
        // <summary>
        // OPER N(4)
        // </summary>
        [FieldName("OPER"), FieldType('N'), FieldWidth(4)]
        public Int64 Oper
        {
            get { return oper; }
            set { CheckIntegerData("Oper", value, 4); oper = value; }
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
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("CODE")) Code = Convert.ToInt64(ADataRow["CODE"]); else Code = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("DATF")) Datf = Convert.ToDateTime(ADataRow["DATF"]); else Datf = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("VALUE_OLD")) Value_old = Convert.ToDecimal(ADataRow["VALUE_OLD"]); else Value_old = 0;
            if (ADataRow.Table.Columns.Contains("VALUE_SRED")) Value_sred = Convert.ToDecimal(ADataRow["VALUE_SRED"]); else Value_sred = 0;
            if (ADataRow.Table.Columns.Contains("VALUE_NORM")) Value_norm = Convert.ToDecimal(ADataRow["VALUE_NORM"]); else Value_norm = 0;
            if (ADataRow.Table.Columns.Contains("TYPE_INPUT")) Type_input = Convert.ToInt64(ADataRow["TYPE_INPUT"]); else Type_input = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            CountindRecord retValue = new CountindRecord();
            retValue.Ls = this.Ls;
            retValue.Usluga = this.Usluga;
            retValue.Code = this.Code;
            retValue.Dat = this.Dat;
            retValue.Datf = this.Datf;
            retValue.Indication = this.Indication;
            retValue.Value_old = this.Value_old;
            retValue.Value_sred = this.Value_sred;
            retValue.Value_norm = this.Value_norm;
            retValue.Type_input = this.Type_input;
            retValue.Info = this.Info;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTIND (LS, USLUGA, CODE, DAT, DATF, INDICATION, VALUE_OLD, VALUE_SRED, VALUE_NORM, TYPE_INPUT, INFO, OPER, AUTHOR) VALUES ({0}, {1}, {2}, '{3}', CTOD('{4}'), {5}, {6}, {7}, {8}, {9}, '{10}', {11}, '{12}')", Ls.ToString(), Usluga.ToString(), Code.ToString(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Datf == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datf.Month, Datf.Day, Datf.Year), Indication.ToString().Replace(',','.'), Value_old.ToString().Replace(',','.'), Value_sred.ToString().Replace(',','.'), Value_norm.ToString().Replace(',','.'), Type_input.ToString(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
