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
    [TableName("ROF.DBF")]
    public partial class RofRecord: AbstractRecord
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

        private Int64 usluga1;
        // <summary>
        // USLUGA1 N(4)
        // </summary>
        [FieldName("USLUGA1"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga1
        {
            get { return usluga1; }
            set { CheckIntegerData("Usluga1", value, 4); usluga1 = value; }
        }

        private decimal sum1;
        // <summary>
        // SUM1 N(10,2)
        // </summary>
        [FieldName("SUM1"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sum1
        {
            get { return sum1; }
            set { CheckDecimalData("Sum1", value, 10, 2); sum1 = value; }
        }

        private decimal nds1;
        // <summary>
        // NDS1 N(10,2)
        // </summary>
        [FieldName("NDS1"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Nds1
        {
            get { return nds1; }
            set { CheckDecimalData("Nds1", value, 10, 2); nds1 = value; }
        }

        private Int64 usluga2;
        // <summary>
        // USLUGA2 N(4)
        // </summary>
        [FieldName("USLUGA2"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga2
        {
            get { return usluga2; }
            set { CheckIntegerData("Usluga2", value, 4); usluga2 = value; }
        }

        private decimal sum2;
        // <summary>
        // SUM2 N(10,2)
        // </summary>
        [FieldName("SUM2"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sum2
        {
            get { return sum2; }
            set { CheckDecimalData("Sum2", value, 10, 2); sum2 = value; }
        }

        private decimal nds2;
        // <summary>
        // NDS2 N(10,2)
        // </summary>
        [FieldName("NDS2"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Nds2
        {
            get { return nds2; }
            set { CheckDecimalData("Nds2", value, 10, 2); nds2 = value; }
        }

        private Int64 usluga3;
        // <summary>
        // USLUGA3 N(4)
        // </summary>
        [FieldName("USLUGA3"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga3
        {
            get { return usluga3; }
            set { CheckIntegerData("Usluga3", value, 4); usluga3 = value; }
        }

        private decimal sum3;
        // <summary>
        // SUM3 N(10,2)
        // </summary>
        [FieldName("SUM3"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sum3
        {
            get { return sum3; }
            set { CheckDecimalData("Sum3", value, 10, 2); sum3 = value; }
        }

        private decimal nds3;
        // <summary>
        // NDS3 N(10,2)
        // </summary>
        [FieldName("NDS3"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Nds3
        {
            get { return nds3; }
            set { CheckDecimalData("Nds3", value, 10, 2); nds3 = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("USLUGA1")) Usluga1 = Convert.ToInt64(ADataRow["USLUGA1"]); else Usluga1 = 0;
            if (ADataRow.Table.Columns.Contains("SUM1")) Sum1 = Convert.ToDecimal(ADataRow["SUM1"]); else Sum1 = 0;
            if (ADataRow.Table.Columns.Contains("NDS1")) Nds1 = Convert.ToDecimal(ADataRow["NDS1"]); else Nds1 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA2")) Usluga2 = Convert.ToInt64(ADataRow["USLUGA2"]); else Usluga2 = 0;
            if (ADataRow.Table.Columns.Contains("SUM2")) Sum2 = Convert.ToDecimal(ADataRow["SUM2"]); else Sum2 = 0;
            if (ADataRow.Table.Columns.Contains("NDS2")) Nds2 = Convert.ToDecimal(ADataRow["NDS2"]); else Nds2 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA3")) Usluga3 = Convert.ToInt64(ADataRow["USLUGA3"]); else Usluga3 = 0;
            if (ADataRow.Table.Columns.Contains("SUM3")) Sum3 = Convert.ToDecimal(ADataRow["SUM3"]); else Sum3 = 0;
            if (ADataRow.Table.Columns.Contains("NDS3")) Nds3 = Convert.ToDecimal(ADataRow["NDS3"]); else Nds3 = 0;
        }
        
        public override AbstractRecord Clone()
        {
            RofRecord retValue = new RofRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Usluga1 = this.Usluga1;
            retValue.Sum1 = this.Sum1;
            retValue.Nds1 = this.Nds1;
            retValue.Usluga2 = this.Usluga2;
            retValue.Sum2 = this.Sum2;
            retValue.Nds2 = this.Nds2;
            retValue.Usluga3 = this.Usluga3;
            retValue.Sum3 = this.Sum3;
            retValue.Nds3 = this.Nds3;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ROF (LS, DAT, USLUGA1, SUM1, NDS1, USLUGA2, SUM2, NDS2, USLUGA3, SUM3, NDS3) VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", Ls.ToString(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Usluga1.ToString(), Sum1.ToString().Replace(',','.'), Nds1.ToString().Replace(',','.'), Usluga2.ToString(), Sum2.ToString().Replace(',','.'), Nds2.ToString().Replace(',','.'), Usluga3.ToString(), Sum3.ToString().Replace(',','.'), Nds3.ToString().Replace(',','.'));
            return rs;
        }
    }
}
