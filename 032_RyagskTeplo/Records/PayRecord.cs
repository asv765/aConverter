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
    [TableName("PAY.DBF")]
    public partial class PayRecord: AbstractRecord
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

        private Int64 time;
        // <summary>
        // TIME N(6)
        // </summary>
        [FieldName("TIME"), FieldType('N'), FieldWidth(6)]
        public Int64 Time
        {
            get { return time; }
            set { CheckIntegerData("Time", value, 6); time = value; }
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

        private Int64 post1;
        // <summary>
        // POST1 N(4)
        // </summary>
        [FieldName("POST1"), FieldType('N'), FieldWidth(4)]
        public Int64 Post1
        {
            get { return post1; }
            set { CheckIntegerData("Post1", value, 4); post1 = value; }
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

        private Int64 post2;
        // <summary>
        // POST2 N(4)
        // </summary>
        [FieldName("POST2"), FieldType('N'), FieldWidth(4)]
        public Int64 Post2
        {
            get { return post2; }
            set { CheckIntegerData("Post2", value, 4); post2 = value; }
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

        private Int64 post3;
        // <summary>
        // POST3 N(4)
        // </summary>
        [FieldName("POST3"), FieldType('N'), FieldWidth(4)]
        public Int64 Post3
        {
            get { return post3; }
            set { CheckIntegerData("Post3", value, 4); post3 = value; }
        }

        private decimal sum4;
        // <summary>
        // SUM4 N(10,2)
        // </summary>
        [FieldName("SUM4"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sum4
        {
            get { return sum4; }
            set { CheckDecimalData("Sum4", value, 10, 2); sum4 = value; }
        }

        private Int64 usluga4;
        // <summary>
        // USLUGA4 N(4)
        // </summary>
        [FieldName("USLUGA4"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga4
        {
            get { return usluga4; }
            set { CheckIntegerData("Usluga4", value, 4); usluga4 = value; }
        }

        private Int64 post4;
        // <summary>
        // POST4 N(4)
        // </summary>
        [FieldName("POST4"), FieldType('N'), FieldWidth(4)]
        public Int64 Post4
        {
            get { return post4; }
            set { CheckIntegerData("Post4", value, 4); post4 = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = Convert.ToInt64(ADataRow["TIME"]); else Time = 0;
            if (ADataRow.Table.Columns.Contains("SUM1")) Sum1 = Convert.ToDecimal(ADataRow["SUM1"]); else Sum1 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA1")) Usluga1 = Convert.ToInt64(ADataRow["USLUGA1"]); else Usluga1 = 0;
            if (ADataRow.Table.Columns.Contains("POST1")) Post1 = Convert.ToInt64(ADataRow["POST1"]); else Post1 = 0;
            if (ADataRow.Table.Columns.Contains("SUM2")) Sum2 = Convert.ToDecimal(ADataRow["SUM2"]); else Sum2 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA2")) Usluga2 = Convert.ToInt64(ADataRow["USLUGA2"]); else Usluga2 = 0;
            if (ADataRow.Table.Columns.Contains("POST2")) Post2 = Convert.ToInt64(ADataRow["POST2"]); else Post2 = 0;
            if (ADataRow.Table.Columns.Contains("SUM3")) Sum3 = Convert.ToDecimal(ADataRow["SUM3"]); else Sum3 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA3")) Usluga3 = Convert.ToInt64(ADataRow["USLUGA3"]); else Usluga3 = 0;
            if (ADataRow.Table.Columns.Contains("POST3")) Post3 = Convert.ToInt64(ADataRow["POST3"]); else Post3 = 0;
            if (ADataRow.Table.Columns.Contains("SUM4")) Sum4 = Convert.ToDecimal(ADataRow["SUM4"]); else Sum4 = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA4")) Usluga4 = Convert.ToInt64(ADataRow["USLUGA4"]); else Usluga4 = 0;
            if (ADataRow.Table.Columns.Contains("POST4")) Post4 = Convert.ToInt64(ADataRow["POST4"]); else Post4 = 0;
        }
        
        public override AbstractRecord Clone()
        {
            PayRecord retValue = new PayRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Usluga = this.Usluga;
            retValue.Time = this.Time;
            retValue.Sum1 = this.Sum1;
            retValue.Usluga1 = this.Usluga1;
            retValue.Post1 = this.Post1;
            retValue.Sum2 = this.Sum2;
            retValue.Usluga2 = this.Usluga2;
            retValue.Post2 = this.Post2;
            retValue.Sum3 = this.Sum3;
            retValue.Usluga3 = this.Usluga3;
            retValue.Post3 = this.Post3;
            retValue.Sum4 = this.Sum4;
            retValue.Usluga4 = this.Usluga4;
            retValue.Post4 = this.Post4;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PAY (LS, DAT, USLUGA, TIME, SUM1, USLUGA1, POST1, SUM2, USLUGA2, POST2, SUM3, USLUGA3, POST3, SUM4, USLUGA4, POST4) VALUES ({0}, CTOD('{1}'), {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})", Ls.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Usluga.ToString(), Time.ToString(), Sum1.ToString().Replace(',','.'), Usluga1.ToString(), Post1.ToString(), Sum2.ToString().Replace(',','.'), Usluga2.ToString(), Post2.ToString(), Sum3.ToString().Replace(',','.'), Usluga3.ToString(), Post3.ToString(), Sum4.ToString().Replace(',','.'), Usluga4.ToString(), Post4.ToString());
            return rs;
        }
    }
}
