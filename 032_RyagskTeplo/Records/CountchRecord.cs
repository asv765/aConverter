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
    [TableName("COUNTCH.DBF")]
    public partial class CountchRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(7)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 7); ls = value; }
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

        private DateTime dat;
        // <summary>
        // DAT D(8)
        // </summary>
        [FieldName("DAT"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat
        {
            get { return dat; }
            set {  dat = value; }
        }

        private DateTime dat_next;
        // <summary>
        // DAT_NEXT D(8)
        // </summary>
        [FieldName("DAT_NEXT"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_next
        {
            get { return dat_next; }
            set {  dat_next = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("CODE")) Code = Convert.ToInt64(ADataRow["CODE"]); else Code = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DAT_NEXT")) Dat_next = Convert.ToDateTime(ADataRow["DAT_NEXT"]); else Dat_next = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
        }
        
        public override AbstractRecord Clone()
        {
            CountchRecord retValue = new CountchRecord();
            retValue.Ls = this.Ls;
            retValue.Usluga = this.Usluga;
            retValue.Code = this.Code;
            retValue.Dat = this.Dat;
            retValue.Dat_next = this.Dat_next;
            retValue.Indication = this.Indication;
            retValue.Info = this.Info;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTCH (LS, USLUGA, CODE, DAT, DAT_NEXT, INDICATION, INFO) VALUES ({0}, {1}, {2}, CTOD('{3}'), CTOD('{4}'), {5}, '{6}')", Ls.ToString(), Usluga.ToString(), Code.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Dat_next == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_next.Month, Dat_next.Day, Dat_next.Year), Indication.ToString().Replace(',','.'), String.IsNullOrEmpty(Info) ? "" : Info.Trim());
            return rs;
        }
    }
}
