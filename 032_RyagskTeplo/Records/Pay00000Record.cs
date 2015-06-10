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
    [TableName("PAY00000.DBF")]
    public partial class Pay00000Record: AbstractRecord
    {
        private Int64 org;
        // <summary>
        // ORG N(6)
        // </summary>
        [FieldName("ORG"), FieldType('N'), FieldWidth(6)]
        public Int64 Org
        {
            get { return org; }
            set { CheckIntegerData("Org", value, 6); org = value; }
        }

        private string ls_org;
        // <summary>
        // LS_ORG C(7)
        // </summary>
        [FieldName("LS_ORG"), FieldType('C'), FieldWidth(7)]
        public string Ls_org
        {
            get { return ls_org; }
            set { CheckStringData("Ls_org", value, 7); ls_org = value; }
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

        private decimal saldo;
        // <summary>
        // SALDO N(12,2)
        // </summary>
        [FieldName("SALDO"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Saldo
        {
            get { return saldo; }
            set { CheckDecimalData("Saldo", value, 12, 2); saldo = value; }
        }

        private decimal sumn;
        // <summary>
        // SUMN N(12,2)
        // </summary>
        [FieldName("SUMN"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sumn
        {
            get { return sumn; }
            set { CheckDecimalData("Sumn", value, 12, 2); sumn = value; }
        }

        private decimal sumo;
        // <summary>
        // SUMO N(12,2)
        // </summary>
        [FieldName("SUMO"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sumo
        {
            get { return sumo; }
            set { CheckDecimalData("Sumo", value, 12, 2); sumo = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ORG")) Org = Convert.ToInt64(ADataRow["ORG"]); else Org = 0;
            if (ADataRow.Table.Columns.Contains("LS_ORG")) Ls_org = ADataRow["LS_ORG"].ToString(); else Ls_org = "";
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("SALDO")) Saldo = Convert.ToDecimal(ADataRow["SALDO"]); else Saldo = 0;
            if (ADataRow.Table.Columns.Contains("SUMN")) Sumn = Convert.ToDecimal(ADataRow["SUMN"]); else Sumn = 0;
            if (ADataRow.Table.Columns.Contains("SUMO")) Sumo = Convert.ToDecimal(ADataRow["SUMO"]); else Sumo = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Pay00000Record retValue = new Pay00000Record();
            retValue.Org = this.Org;
            retValue.Ls_org = this.Ls_org;
            retValue.Dat = this.Dat;
            retValue.Saldo = this.Saldo;
            retValue.Sumn = this.Sumn;
            retValue.Sumo = this.Sumo;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PAY00000 (ORG, LS_ORG, DAT, SALDO, SUMN, SUMO) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5})", Org.ToString(), String.IsNullOrEmpty(Ls_org) ? "" : Ls_org.Trim(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), Saldo.ToString().Replace(',','.'), Sumn.ToString().Replace(',','.'), Sumo.ToString().Replace(',','.'));
            return rs;
        }
    }
}
