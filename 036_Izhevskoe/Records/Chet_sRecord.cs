// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _036_Izhevskoe
{
    [TableName("CHET_S.DBF")]
    public partial class Chet_sRecord: AbstractRecord
    {
        private Int64 n_lits;
        // <summary>
        // N_LITS N(5)
        // </summary>
        [FieldName("N_LITS"), FieldType('N'), FieldWidth(5)]
        public Int64 N_lits
        {
            get { return n_lits; }
            set { CheckIntegerData("N_lits", value, 5); n_lits = value; }
        }

        private Int64 shet_1;
        // <summary>
        // SHET_1 N(11)
        // </summary>
        [FieldName("SHET_1"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_1
        {
            get { return shet_1; }
            set { CheckIntegerData("Shet_1", value, 11); shet_1 = value; }
        }

        private string n_shet1;
        // <summary>
        // N_SHET1 C(26)
        // </summary>
        [FieldName("N_SHET1"), FieldType('C'), FieldWidth(26)]
        public string N_shet1
        {
            get { return n_shet1; }
            set { CheckStringData("N_shet1", value, 26); n_shet1 = value; }
        }

        private Int64 shet_2;
        // <summary>
        // SHET_2 N(11)
        // </summary>
        [FieldName("SHET_2"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_2
        {
            get { return shet_2; }
            set { CheckIntegerData("Shet_2", value, 11); shet_2 = value; }
        }

        private string n_shet2;
        // <summary>
        // N_SHET2 C(26)
        // </summary>
        [FieldName("N_SHET2"), FieldType('C'), FieldWidth(26)]
        public string N_shet2
        {
            get { return n_shet2; }
            set { CheckStringData("N_shet2", value, 26); n_shet2 = value; }
        }

        private Int64 shet_3;
        // <summary>
        // SHET_3 N(11)
        // </summary>
        [FieldName("SHET_3"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_3
        {
            get { return shet_3; }
            set { CheckIntegerData("Shet_3", value, 11); shet_3 = value; }
        }

        private string n_shet3;
        // <summary>
        // N_SHET3 C(26)
        // </summary>
        [FieldName("N_SHET3"), FieldType('C'), FieldWidth(26)]
        public string N_shet3
        {
            get { return n_shet3; }
            set { CheckStringData("N_shet3", value, 26); n_shet3 = value; }
        }

        private Int64 shet_4;
        // <summary>
        // SHET_4 N(11)
        // </summary>
        [FieldName("SHET_4"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_4
        {
            get { return shet_4; }
            set { CheckIntegerData("Shet_4", value, 11); shet_4 = value; }
        }

        private string n_shet4;
        // <summary>
        // N_SHET4 C(26)
        // </summary>
        [FieldName("N_SHET4"), FieldType('C'), FieldWidth(26)]
        public string N_shet4
        {
            get { return n_shet4; }
            set { CheckStringData("N_shet4", value, 26); n_shet4 = value; }
        }

        private Int64 shet_5;
        // <summary>
        // SHET_5 N(6)
        // </summary>
        [FieldName("SHET_5"), FieldType('N'), FieldWidth(6)]
        public Int64 Shet_5
        {
            get { return shet_5; }
            set { CheckIntegerData("Shet_5", value, 6); shet_5 = value; }
        }

        private string n_shet5;
        // <summary>
        // N_SHET5 C(11)
        // </summary>
        [FieldName("N_SHET5"), FieldType('C'), FieldWidth(11)]
        public string N_shet5
        {
            get { return n_shet5; }
            set { CheckStringData("N_shet5", value, 11); n_shet5 = value; }
        }

        private Int64 god;
        // <summary>
        // GOD N(5)
        // </summary>
        [FieldName("GOD"), FieldType('N'), FieldWidth(5)]
        public Int64 God
        {
            get { return god; }
            set { CheckIntegerData("God", value, 5); god = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("SHET_1")) Shet_1 = Convert.ToInt64(ADataRow["SHET_1"]); else Shet_1 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET1")) N_shet1 = ADataRow["N_SHET1"].ToString(); else N_shet1 = "";
            if (ADataRow.Table.Columns.Contains("SHET_2")) Shet_2 = Convert.ToInt64(ADataRow["SHET_2"]); else Shet_2 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET2")) N_shet2 = ADataRow["N_SHET2"].ToString(); else N_shet2 = "";
            if (ADataRow.Table.Columns.Contains("SHET_3")) Shet_3 = Convert.ToInt64(ADataRow["SHET_3"]); else Shet_3 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET3")) N_shet3 = ADataRow["N_SHET3"].ToString(); else N_shet3 = "";
            if (ADataRow.Table.Columns.Contains("SHET_4")) Shet_4 = Convert.ToInt64(ADataRow["SHET_4"]); else Shet_4 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET4")) N_shet4 = ADataRow["N_SHET4"].ToString(); else N_shet4 = "";
            if (ADataRow.Table.Columns.Contains("SHET_5")) Shet_5 = Convert.ToInt64(ADataRow["SHET_5"]); else Shet_5 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET5")) N_shet5 = ADataRow["N_SHET5"].ToString(); else N_shet5 = "";
            if (ADataRow.Table.Columns.Contains("GOD")) God = Convert.ToInt64(ADataRow["GOD"]); else God = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Chet_sRecord retValue = new Chet_sRecord();
            retValue.N_lits = this.N_lits;
            retValue.Shet_1 = this.Shet_1;
            retValue.N_shet1 = this.N_shet1;
            retValue.Shet_2 = this.Shet_2;
            retValue.N_shet2 = this.N_shet2;
            retValue.Shet_3 = this.Shet_3;
            retValue.N_shet3 = this.N_shet3;
            retValue.Shet_4 = this.Shet_4;
            retValue.N_shet4 = this.N_shet4;
            retValue.Shet_5 = this.Shet_5;
            retValue.N_shet5 = this.N_shet5;
            retValue.God = this.God;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CHET_S (N_LITS, SHET_1, N_SHET1, SHET_2, N_SHET2, SHET_3, N_SHET3, SHET_4, N_SHET4, SHET_5, N_SHET5, GOD) VALUES ({0}, {1}, '{2}', {3}, '{4}', {5}, '{6}', {7}, '{8}', {9}, '{10}', {11})", N_lits.ToString(), Shet_1.ToString(), String.IsNullOrEmpty(N_shet1) ? "" : N_shet1.Trim(), Shet_2.ToString(), String.IsNullOrEmpty(N_shet2) ? "" : N_shet2.Trim(), Shet_3.ToString(), String.IsNullOrEmpty(N_shet3) ? "" : N_shet3.Trim(), Shet_4.ToString(), String.IsNullOrEmpty(N_shet4) ? "" : N_shet4.Trim(), Shet_5.ToString(), String.IsNullOrEmpty(N_shet5) ? "" : N_shet5.Trim(), God.ToString());
            return rs;
        }
    }
}
	