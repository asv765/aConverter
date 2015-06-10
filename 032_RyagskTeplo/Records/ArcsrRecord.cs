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
    [TableName("ArcSr.DBF")]
    public partial class ArcsrRecord: AbstractRecord
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

        private Int64 kol;
        // <summary>
        // KOL N(4)
        // </summary>
        [FieldName("KOL"), FieldType('N'), FieldWidth(4)]
        public Int64 Kol
        {
            get { return kol; }
            set { CheckIntegerData("Kol", value, 4); kol = value; }
        }

        private decimal saldo;
        // <summary>
        // SALDO N(10,2)
        // </summary>
        [FieldName("SALDO"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo
        {
            get { return saldo; }
            set { CheckDecimalData("Saldo", value, 10, 2); saldo = value; }
        }

        private decimal elecmop;
        // <summary>
        // ELECMOP N(15,5)
        // </summary>
        [FieldName("ELECMOP"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Elecmop
        {
            get { return elecmop; }
            set { CheckDecimalData("Elecmop", value, 15, 5); elecmop = value; }
        }

        private decimal eleclost;
        // <summary>
        // ELECLOST N(15,5)
        // </summary>
        [FieldName("ELECLOST"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Eleclost
        {
            get { return eleclost; }
            set { CheckDecimalData("Eleclost", value, 15, 5); eleclost = value; }
        }

        private decimal saldo4;
        // <summary>
        // SALDO4 N(10,2)
        // </summary>
        [FieldName("SALDO4"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo4
        {
            get { return saldo4; }
            set { CheckDecimalData("Saldo4", value, 10, 2); saldo4 = value; }
        }

        private decimal tarif4;
        // <summary>
        // TARIF4 N(15,5)
        // </summary>
        [FieldName("TARIF4"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Tarif4
        {
            get { return tarif4; }
            set { CheckDecimalData("Tarif4", value, 15, 5); tarif4 = value; }
        }

        private decimal saldo9;
        // <summary>
        // SALDO9 N(10,2)
        // </summary>
        [FieldName("SALDO9"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo9
        {
            get { return saldo9; }
            set { CheckDecimalData("Saldo9", value, 10, 2); saldo9 = value; }
        }

        private decimal tarif9;
        // <summary>
        // TARIF9 N(15,5)
        // </summary>
        [FieldName("TARIF9"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Tarif9
        {
            get { return tarif9; }
            set { CheckDecimalData("Tarif9", value, 15, 5); tarif9 = value; }
        }

        private decimal saldo6;
        // <summary>
        // SALDO6 N(10,2)
        // </summary>
        [FieldName("SALDO6"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo6
        {
            get { return saldo6; }
            set { CheckDecimalData("Saldo6", value, 10, 2); saldo6 = value; }
        }

        private decimal tarif6;
        // <summary>
        // TARIF6 N(15,5)
        // </summary>
        [FieldName("TARIF6"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Tarif6
        {
            get { return tarif6; }
            set { CheckDecimalData("Tarif6", value, 15, 5); tarif6 = value; }
        }

        private decimal saldo11;
        // <summary>
        // SALDO11 N(10,2)
        // </summary>
        [FieldName("SALDO11"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo11
        {
            get { return saldo11; }
            set { CheckDecimalData("Saldo11", value, 10, 2); saldo11 = value; }
        }

        private decimal tarif11;
        // <summary>
        // TARIF11 N(15,5)
        // </summary>
        [FieldName("TARIF11"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Tarif11
        {
            get { return tarif11; }
            set { CheckDecimalData("Tarif11", value, 15, 5); tarif11 = value; }
        }

        private decimal saldo7;
        // <summary>
        // SALDO7 N(10,2)
        // </summary>
        [FieldName("SALDO7"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo7
        {
            get { return saldo7; }
            set { CheckDecimalData("Saldo7", value, 10, 2); saldo7 = value; }
        }

        private decimal tarif7;
        // <summary>
        // TARIF7 N(15,5)
        // </summary>
        [FieldName("TARIF7"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Tarif7
        {
            get { return tarif7; }
            set { CheckDecimalData("Tarif7", value, 15, 5); tarif7 = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("KOL")) Kol = Convert.ToInt64(ADataRow["KOL"]); else Kol = 0;
            if (ADataRow.Table.Columns.Contains("SALDO")) Saldo = Convert.ToDecimal(ADataRow["SALDO"]); else Saldo = 0;
            if (ADataRow.Table.Columns.Contains("ELECMOP")) Elecmop = Convert.ToDecimal(ADataRow["ELECMOP"]); else Elecmop = 0;
            if (ADataRow.Table.Columns.Contains("ELECLOST")) Eleclost = Convert.ToDecimal(ADataRow["ELECLOST"]); else Eleclost = 0;
            if (ADataRow.Table.Columns.Contains("SALDO4")) Saldo4 = Convert.ToDecimal(ADataRow["SALDO4"]); else Saldo4 = 0;
            if (ADataRow.Table.Columns.Contains("TARIF4")) Tarif4 = Convert.ToDecimal(ADataRow["TARIF4"]); else Tarif4 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO9")) Saldo9 = Convert.ToDecimal(ADataRow["SALDO9"]); else Saldo9 = 0;
            if (ADataRow.Table.Columns.Contains("TARIF9")) Tarif9 = Convert.ToDecimal(ADataRow["TARIF9"]); else Tarif9 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO6")) Saldo6 = Convert.ToDecimal(ADataRow["SALDO6"]); else Saldo6 = 0;
            if (ADataRow.Table.Columns.Contains("TARIF6")) Tarif6 = Convert.ToDecimal(ADataRow["TARIF6"]); else Tarif6 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO11")) Saldo11 = Convert.ToDecimal(ADataRow["SALDO11"]); else Saldo11 = 0;
            if (ADataRow.Table.Columns.Contains("TARIF11")) Tarif11 = Convert.ToDecimal(ADataRow["TARIF11"]); else Tarif11 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO7")) Saldo7 = Convert.ToDecimal(ADataRow["SALDO7"]); else Saldo7 = 0;
            if (ADataRow.Table.Columns.Contains("TARIF7")) Tarif7 = Convert.ToDecimal(ADataRow["TARIF7"]); else Tarif7 = 0;
        }
        
        public override AbstractRecord Clone()
        {
            ArcsrRecord retValue = new ArcsrRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Kol = this.Kol;
            retValue.Saldo = this.Saldo;
            retValue.Elecmop = this.Elecmop;
            retValue.Eleclost = this.Eleclost;
            retValue.Saldo4 = this.Saldo4;
            retValue.Tarif4 = this.Tarif4;
            retValue.Saldo9 = this.Saldo9;
            retValue.Tarif9 = this.Tarif9;
            retValue.Saldo6 = this.Saldo6;
            retValue.Tarif6 = this.Tarif6;
            retValue.Saldo11 = this.Saldo11;
            retValue.Tarif11 = this.Tarif11;
            retValue.Saldo7 = this.Saldo7;
            retValue.Tarif7 = this.Tarif7;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ArcSr (LS, DAT, KOL, SALDO, ELECMOP, ELECLOST, SALDO4, TARIF4, SALDO9, TARIF9, SALDO6, TARIF6, SALDO11, TARIF11, SALDO7, TARIF7) VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15})", Ls.ToString(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Kol.ToString(), Saldo.ToString().Replace(',','.'), Elecmop.ToString().Replace(',','.'), Eleclost.ToString().Replace(',','.'), Saldo4.ToString().Replace(',','.'), Tarif4.ToString().Replace(',','.'), Saldo9.ToString().Replace(',','.'), Tarif9.ToString().Replace(',','.'), Saldo6.ToString().Replace(',','.'), Tarif6.ToString().Replace(',','.'), Saldo11.ToString().Replace(',','.'), Tarif11.ToString().Replace(',','.'), Saldo7.ToString().Replace(',','.'), Tarif7.ToString().Replace(',','.'));
            return rs;
        }
    }
}
