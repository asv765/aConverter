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
    [TableName("FPLAN.DBF")]
    public partial class FplanRecord: AbstractRecord
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

        private string mes_pl;
        // <summary>
        // MES_PL C(8)
        // </summary>
        [FieldName("MES_PL"), FieldType('C'), FieldWidth(8)]
        public string Mes_pl
        {
            get { return mes_pl; }
            set { CheckStringData("Mes_pl", value, 8); mes_pl = value; }
        }

        private decimal s_kvart;
        // <summary>
        // S_KVART N(8,2)
        // </summary>
        [FieldName("S_KVART"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_kvart
        {
            get { return s_kvart; }
            set { CheckDecimalData("S_kvart", value, 8, 2); s_kvart = value; }
        }

        private decimal s_naim;
        // <summary>
        // S_NAIM N(8,2)
        // </summary>
        [FieldName("S_NAIM"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_naim
        {
            get { return s_naim; }
            set { CheckDecimalData("S_naim", value, 8, 2); s_naim = value; }
        }

        private decimal s_water;
        // <summary>
        // S_WATER N(8,2)
        // </summary>
        [FieldName("S_WATER"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_water
        {
            get { return s_water; }
            set { CheckDecimalData("S_water", value, 8, 2); s_water = value; }
        }

        private decimal s_kanal;
        // <summary>
        // S_KANAL N(8,2)
        // </summary>
        [FieldName("S_KANAL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_kanal
        {
            get { return s_kanal; }
            set { CheckDecimalData("S_kanal", value, 8, 2); s_kanal = value; }
        }

        private decimal s_musor;
        // <summary>
        // S_MUSOR N(8,2)
        // </summary>
        [FieldName("S_MUSOR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_musor
        {
            get { return s_musor; }
            set { CheckDecimalData("S_musor", value, 8, 2); s_musor = value; }
        }

        private decimal korov;
        // <summary>
        // KOROV N(8,2)
        // </summary>
        [FieldName("KOROV"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Korov
        {
            get { return korov; }
            set { CheckDecimalData("Korov", value, 8, 2); korov = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("MES_PL")) Mes_pl = ADataRow["MES_PL"].ToString(); else Mes_pl = "";
            if (ADataRow.Table.Columns.Contains("S_KVART")) S_kvart = Convert.ToDecimal(ADataRow["S_KVART"]); else S_kvart = 0;
            if (ADataRow.Table.Columns.Contains("S_NAIM")) S_naim = Convert.ToDecimal(ADataRow["S_NAIM"]); else S_naim = 0;
            if (ADataRow.Table.Columns.Contains("S_WATER")) S_water = Convert.ToDecimal(ADataRow["S_WATER"]); else S_water = 0;
            if (ADataRow.Table.Columns.Contains("S_KANAL")) S_kanal = Convert.ToDecimal(ADataRow["S_KANAL"]); else S_kanal = 0;
            if (ADataRow.Table.Columns.Contains("S_MUSOR")) S_musor = Convert.ToDecimal(ADataRow["S_MUSOR"]); else S_musor = 0;
            if (ADataRow.Table.Columns.Contains("KOROV")) Korov = Convert.ToDecimal(ADataRow["KOROV"]); else Korov = 0;
        }
        
        public override AbstractRecord Clone()
        {
            FplanRecord retValue = new FplanRecord();
            retValue.N_lits = this.N_lits;
            retValue.Mes_pl = this.Mes_pl;
            retValue.S_kvart = this.S_kvart;
            retValue.S_naim = this.S_naim;
            retValue.S_water = this.S_water;
            retValue.S_kanal = this.S_kanal;
            retValue.S_musor = this.S_musor;
            retValue.Korov = this.Korov;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO FPLAN (N_LITS, MES_PL, S_KVART, S_NAIM, S_WATER, S_KANAL, S_MUSOR, KOROV) VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, {6}, {7})", N_lits.ToString(), String.IsNullOrEmpty(Mes_pl) ? "" : Mes_pl.Trim(), S_kvart.ToString().Replace(',','.'), S_naim.ToString().Replace(',','.'), S_water.ToString().Replace(',','.'), S_kanal.ToString().Replace(',','.'), S_musor.ToString().Replace(',','.'), Korov.ToString().Replace(',','.'));
            return rs;
        }
    }
}
	