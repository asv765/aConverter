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
    [TableName("HmValue.DBF")]
    public partial class HmvalueRecord: AbstractRecord
    {
        private Int64 coddm;
        // <summary>
        // CODDM N(6)
        // </summary>
        [FieldName("CODDM"), FieldType('N'), FieldWidth(6)]
        public Int64 Coddm
        {
            get { return coddm; }
            set { CheckIntegerData("Coddm", value, 6); coddm = value; }
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

        private decimal value_;
        // <summary>
        // VALUE N(15,5)
        // </summary>
        [FieldName("VALUE"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Value_
        {
            get { return value_; }
            set { CheckDecimalData("Value_", value, 15, 5); value_ = value; }
        }

        private decimal water;
        // <summary>
        // WATER N(10,2)
        // </summary>
        [FieldName("WATER"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Water
        {
            get { return water; }
            set { CheckDecimalData("Water", value, 10, 2); water = value; }
        }

        private decimal loss;
        // <summary>
        // LOSS N(10,2)
        // </summary>
        [FieldName("LOSS"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Loss
        {
            get { return loss; }
            set { CheckDecimalData("Loss", value, 10, 2); loss = value; }
        }

        private string info;
        // <summary>
        // INFO C(20)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(20)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 20); info = value; }
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
            if (ADataRow.Table.Columns.Contains("CODDM")) Coddm = Convert.ToInt64(ADataRow["CODDM"]); else Coddm = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("CODE")) Code = Convert.ToInt64(ADataRow["CODE"]); else Code = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("DATF")) Datf = Convert.ToDateTime(ADataRow["DATF"]); else Datf = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = Convert.ToDecimal(ADataRow["VALUE"]); else Value_ = 0;
            if (ADataRow.Table.Columns.Contains("WATER")) Water = Convert.ToDecimal(ADataRow["WATER"]); else Water = 0;
            if (ADataRow.Table.Columns.Contains("LOSS")) Loss = Convert.ToDecimal(ADataRow["LOSS"]); else Loss = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            HmvalueRecord retValue = new HmvalueRecord();
            retValue.Coddm = this.Coddm;
            retValue.Usluga = this.Usluga;
            retValue.Code = this.Code;
            retValue.Dat = this.Dat;
            retValue.Datf = this.Datf;
            retValue.Indication = this.Indication;
            retValue.Value_ = this.Value_;
            retValue.Water = this.Water;
            retValue.Loss = this.Loss;
            retValue.Info = this.Info;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO HmValue (CODDM, USLUGA, CODE, DAT, DATF, INDICATION, VALUE, WATER, LOSS, INFO, OPER, AUTHOR) VALUES ({0}, {1}, {2}, '{3}', CTOD('{4}'), {5}, {6}, {7}, {8}, '{9}', {10}, '{11}')", Coddm.ToString(), Usluga.ToString(), Code.ToString(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Datf == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datf.Month, Datf.Day, Datf.Year), Indication.ToString().Replace(',','.'), Value_.ToString().Replace(',','.'), Water.ToString().Replace(',','.'), Loss.ToString().Replace(',','.'), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
