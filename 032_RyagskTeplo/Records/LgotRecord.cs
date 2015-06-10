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
    [TableName("LGOT.DBF")]
    public partial class LgotRecord: AbstractRecord
    {
        private Int64 codl;
        // <summary>
        // CODL N(4)
        // </summary>
        [FieldName("CODL"), FieldType('N'), FieldWidth(4)]
        public Int64 Codl
        {
            get { return codl; }
            set { CheckIntegerData("Codl", value, 4); codl = value; }
        }

        private string kat_id;
        // <summary>
        // KAT_ID C(6)
        // </summary>
        [FieldName("KAT_ID"), FieldType('C'), FieldWidth(6)]
        public string Kat_id
        {
            get { return kat_id; }
            set { CheckStringData("Kat_id", value, 6); kat_id = value; }
        }

        private string name_priv;
        // <summary>
        // NAME_PRIV C(70)
        // </summary>
        [FieldName("NAME_PRIV"), FieldType('C'), FieldWidth(70)]
        public string Name_priv
        {
            get { return name_priv; }
            set { CheckStringData("Name_priv", value, 70); name_priv = value; }
        }

        private string short_name;
        // <summary>
        // SHORT_NAME C(10)
        // </summary>
        [FieldName("SHORT_NAME"), FieldType('C'), FieldWidth(10)]
        public string Short_name
        {
            get { return short_name; }
            set { CheckStringData("Short_name", value, 10); short_name = value; }
        }

        private Int64 zak;
        // <summary>
        // ZAK N(4)
        // </summary>
        [FieldName("ZAK"), FieldType('N'), FieldWidth(4)]
        public Int64 Zak
        {
            get { return zak; }
            set { CheckIntegerData("Zak", value, 4); zak = value; }
        }

        private Int64 proc_opl;
        // <summary>
        // PROC_OPL N(4)
        // </summary>
        [FieldName("PROC_OPL"), FieldType('N'), FieldWidth(4)]
        public Int64 Proc_opl
        {
            get { return proc_opl; }
            set { CheckIntegerData("Proc_opl", value, 4); proc_opl = value; }
        }

        private Int64 kat;
        // <summary>
        // KAT N(3)
        // </summary>
        [FieldName("KAT"), FieldType('N'), FieldWidth(3)]
        public Int64 Kat
        {
            get { return kat; }
            set { CheckIntegerData("Kat", value, 3); kat = value; }
        }

        private Int64 proc1;
        // <summary>
        // PROC1 N(4)
        // </summary>
        [FieldName("PROC1"), FieldType('N'), FieldWidth(4)]
        public Int64 Proc1
        {
            get { return proc1; }
            set { CheckIntegerData("Proc1", value, 4); proc1 = value; }
        }

        private Int64 proc2;
        // <summary>
        // PROC2 N(4)
        // </summary>
        [FieldName("PROC2"), FieldType('N'), FieldWidth(4)]
        public Int64 Proc2
        {
            get { return proc2; }
            set { CheckIntegerData("Proc2", value, 4); proc2 = value; }
        }

        private Int64 proc3;
        // <summary>
        // PROC3 N(4)
        // </summary>
        [FieldName("PROC3"), FieldType('N'), FieldWidth(4)]
        public Int64 Proc3
        {
            get { return proc3; }
            set { CheckIntegerData("Proc3", value, 4); proc3 = value; }
        }

        private Int64 kat1;
        // <summary>
        // KAT1 N(2)
        // </summary>
        [FieldName("KAT1"), FieldType('N'), FieldWidth(2)]
        public Int64 Kat1
        {
            get { return kat1; }
            set { CheckIntegerData("Kat1", value, 2); kat1 = value; }
        }

        private Int64 kat2;
        // <summary>
        // KAT2 N(4)
        // </summary>
        [FieldName("KAT2"), FieldType('N'), FieldWidth(4)]
        public Int64 Kat2
        {
            get { return kat2; }
            set { CheckIntegerData("Kat2", value, 4); kat2 = value; }
        }

        private Int64 kat3;
        // <summary>
        // KAT3 N(4)
        // </summary>
        [FieldName("KAT3"), FieldType('N'), FieldWidth(4)]
        public Int64 Kat3
        {
            get { return kat3; }
            set { CheckIntegerData("Kat3", value, 4); kat3 = value; }
        }

        private Int64 flag1;
        // <summary>
        // FLAG1 N(2)
        // </summary>
        [FieldName("FLAG1"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag1
        {
            get { return flag1; }
            set { CheckIntegerData("Flag1", value, 2); flag1 = value; }
        }

        private Int64 flag2;
        // <summary>
        // FLAG2 N(2)
        // </summary>
        [FieldName("FLAG2"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag2
        {
            get { return flag2; }
            set { CheckIntegerData("Flag2", value, 2); flag2 = value; }
        }

        private Int64 flag3;
        // <summary>
        // FLAG3 N(2)
        // </summary>
        [FieldName("FLAG3"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag3
        {
            get { return flag3; }
            set { CheckIntegerData("Flag3", value, 2); flag3 = value; }
        }

        private Int64 priv;
        // <summary>
        // PRIV N(2)
        // </summary>
        [FieldName("PRIV"), FieldType('N'), FieldWidth(2)]
        public Int64 Priv
        {
            get { return priv; }
            set { CheckIntegerData("Priv", value, 2); priv = value; }
        }

        private string spuslg;
        // <summary>
        // SPUSLG C(30)
        // </summary>
        [FieldName("SPUSLG"), FieldType('C'), FieldWidth(30)]
        public string Spuslg
        {
            get { return spuslg; }
            set { CheckStringData("Spuslg", value, 30); spuslg = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("CODL")) Codl = Convert.ToInt64(ADataRow["CODL"]); else Codl = 0;
            if (ADataRow.Table.Columns.Contains("KAT_ID")) Kat_id = ADataRow["KAT_ID"].ToString(); else Kat_id = "";
            if (ADataRow.Table.Columns.Contains("NAME_PRIV")) Name_priv = ADataRow["NAME_PRIV"].ToString(); else Name_priv = "";
            if (ADataRow.Table.Columns.Contains("SHORT_NAME")) Short_name = ADataRow["SHORT_NAME"].ToString(); else Short_name = "";
            if (ADataRow.Table.Columns.Contains("ZAK")) Zak = Convert.ToInt64(ADataRow["ZAK"]); else Zak = 0;
            if (ADataRow.Table.Columns.Contains("PROC_OPL")) Proc_opl = Convert.ToInt64(ADataRow["PROC_OPL"]); else Proc_opl = 0;
            if (ADataRow.Table.Columns.Contains("KAT")) Kat = Convert.ToInt64(ADataRow["KAT"]); else Kat = 0;
            if (ADataRow.Table.Columns.Contains("PROC1")) Proc1 = Convert.ToInt64(ADataRow["PROC1"]); else Proc1 = 0;
            if (ADataRow.Table.Columns.Contains("PROC2")) Proc2 = Convert.ToInt64(ADataRow["PROC2"]); else Proc2 = 0;
            if (ADataRow.Table.Columns.Contains("PROC3")) Proc3 = Convert.ToInt64(ADataRow["PROC3"]); else Proc3 = 0;
            if (ADataRow.Table.Columns.Contains("KAT1")) Kat1 = Convert.ToInt64(ADataRow["KAT1"]); else Kat1 = 0;
            if (ADataRow.Table.Columns.Contains("KAT2")) Kat2 = Convert.ToInt64(ADataRow["KAT2"]); else Kat2 = 0;
            if (ADataRow.Table.Columns.Contains("KAT3")) Kat3 = Convert.ToInt64(ADataRow["KAT3"]); else Kat3 = 0;
            if (ADataRow.Table.Columns.Contains("FLAG1")) Flag1 = Convert.ToInt64(ADataRow["FLAG1"]); else Flag1 = 0;
            if (ADataRow.Table.Columns.Contains("FLAG2")) Flag2 = Convert.ToInt64(ADataRow["FLAG2"]); else Flag2 = 0;
            if (ADataRow.Table.Columns.Contains("FLAG3")) Flag3 = Convert.ToInt64(ADataRow["FLAG3"]); else Flag3 = 0;
            if (ADataRow.Table.Columns.Contains("PRIV")) Priv = Convert.ToInt64(ADataRow["PRIV"]); else Priv = 0;
            if (ADataRow.Table.Columns.Contains("SPUSLG")) Spuslg = ADataRow["SPUSLG"].ToString(); else Spuslg = "";
        }
        
        public override AbstractRecord Clone()
        {
            LgotRecord retValue = new LgotRecord();
            retValue.Codl = this.Codl;
            retValue.Kat_id = this.Kat_id;
            retValue.Name_priv = this.Name_priv;
            retValue.Short_name = this.Short_name;
            retValue.Zak = this.Zak;
            retValue.Proc_opl = this.Proc_opl;
            retValue.Kat = this.Kat;
            retValue.Proc1 = this.Proc1;
            retValue.Proc2 = this.Proc2;
            retValue.Proc3 = this.Proc3;
            retValue.Kat1 = this.Kat1;
            retValue.Kat2 = this.Kat2;
            retValue.Kat3 = this.Kat3;
            retValue.Flag1 = this.Flag1;
            retValue.Flag2 = this.Flag2;
            retValue.Flag3 = this.Flag3;
            retValue.Priv = this.Priv;
            retValue.Spuslg = this.Spuslg;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LGOT (CODL, KAT_ID, NAME_PRIV, SHORT_NAME, ZAK, PROC_OPL, KAT, PROC1, PROC2, PROC3, KAT1, KAT2, KAT3, FLAG1, FLAG2, FLAG3, PRIV, SPUSLG) VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, '{17}')", Codl.ToString(), String.IsNullOrEmpty(Kat_id) ? "" : Kat_id.Trim(), String.IsNullOrEmpty(Name_priv) ? "" : Name_priv.Trim(), String.IsNullOrEmpty(Short_name) ? "" : Short_name.Trim(), Zak.ToString(), Proc_opl.ToString(), Kat.ToString(), Proc1.ToString(), Proc2.ToString(), Proc3.ToString(), Kat1.ToString(), Kat2.ToString(), Kat3.ToString(), Flag1.ToString(), Flag2.ToString(), Flag3.ToString(), Priv.ToString(), String.IsNullOrEmpty(Spuslg) ? "" : Spuslg.Trim());
            return rs;
        }
    }
}
