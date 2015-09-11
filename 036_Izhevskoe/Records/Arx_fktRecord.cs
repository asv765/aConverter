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
    [TableName("ARX_FKT.DBF")]
    public partial class Arx_fktRecord: AbstractRecord
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

        private string opl;
        // <summary>
        // OPL C(1)
        // </summary>
        [FieldName("OPL"), FieldType('C'), FieldWidth(1)]
        public string Opl
        {
            get { return opl; }
            set { CheckStringData("Opl", value, 1); opl = value; }
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
        // S_KVART N(7,2)
        // </summary>
        [FieldName("S_KVART"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S_kvart
        {
            get { return s_kvart; }
            set { CheckDecimalData("S_kvart", value, 7, 2); s_kvart = value; }
        }

        private decimal s_naim;
        // <summary>
        // S_NAIM N(7,2)
        // </summary>
        [FieldName("S_NAIM"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S_naim
        {
            get { return s_naim; }
            set { CheckDecimalData("S_naim", value, 7, 2); s_naim = value; }
        }

        private decimal s_water;
        // <summary>
        // S_WATER N(7,2)
        // </summary>
        [FieldName("S_WATER"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S_water
        {
            get { return s_water; }
            set { CheckDecimalData("S_water", value, 7, 2); s_water = value; }
        }

        private decimal s_musor;
        // <summary>
        // S_MUSOR N(7,2)
        // </summary>
        [FieldName("S_MUSOR"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S_musor
        {
            get { return s_musor; }
            set { CheckDecimalData("S_musor", value, 7, 2); s_musor = value; }
        }

        private decimal korov;
        // <summary>
        // KOROV N(7,2)
        // </summary>
        [FieldName("KOROV"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Korov
        {
            get { return korov; }
            set { CheckDecimalData("Korov", value, 7, 2); korov = value; }
        }

        private decimal poliv;
        // <summary>
        // POLIV N(7,2)
        // </summary>
        [FieldName("POLIV"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Poliv
        {
            get { return poliv; }
            set { CheckDecimalData("Poliv", value, 7, 2); poliv = value; }
        }

        private Int64 kvs;
        // <summary>
        // KVS N(7)
        // </summary>
        [FieldName("KVS"), FieldType('N'), FieldWidth(7)]
        public Int64 Kvs
        {
            get { return kvs; }
            set { CheckIntegerData("Kvs", value, 7); kvs = value; }
        }

        private Int64 chet;
        // <summary>
        // CHET N(9)
        // </summary>
        [FieldName("CHET"), FieldType('N'), FieldWidth(9)]
        public Int64 Chet
        {
            get { return chet; }
            set { CheckIntegerData("Chet", value, 9); chet = value; }
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

        private Int64 indik;
        // <summary>
        // INDIK N(3)
        // </summary>
        [FieldName("INDIK"), FieldType('N'), FieldWidth(3)]
        public Int64 Indik
        {
            get { return indik; }
            set { CheckIntegerData("Indik", value, 3); indik = value; }
        }

        private decimal summ;
        // <summary>
        // SUMM N(8,3)
        // </summary>
        [FieldName("SUMM"), FieldType('N'), FieldWidth(8), FieldDec(3)]
        public decimal Summ
        {
            get { return summ; }
            set { CheckDecimalData("Summ", value, 8, 3); summ = value; }
        }

        private Int64 pok_c;
        // <summary>
        // POK_C N(8)
        // </summary>
        [FieldName("POK_C"), FieldType('N'), FieldWidth(8)]
        public Int64 Pok_c
        {
            get { return pok_c; }
            set { CheckIntegerData("Pok_c", value, 8); pok_c = value; }
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

        private Int64 ind_c;
        // <summary>
        // IND_C N(2)
        // </summary>
        [FieldName("IND_C"), FieldType('N'), FieldWidth(2)]
        public Int64 Ind_c
        {
            get { return ind_c; }
            set { CheckIntegerData("Ind_c", value, 2); ind_c = value; }
        }

        private Int64 kvs_l;
        // <summary>
        // KVS_L N(5)
        // </summary>
        [FieldName("KVS_L"), FieldType('N'), FieldWidth(5)]
        public Int64 Kvs_l
        {
            get { return kvs_l; }
            set { CheckIntegerData("Kvs_l", value, 5); kvs_l = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("OPL")) Opl = ADataRow["OPL"].ToString(); else Opl = "";
            if (ADataRow.Table.Columns.Contains("MES_PL")) Mes_pl = ADataRow["MES_PL"].ToString(); else Mes_pl = "";
            if (ADataRow.Table.Columns.Contains("S_KVART")) S_kvart = Convert.ToDecimal(ADataRow["S_KVART"]); else S_kvart = 0;
            if (ADataRow.Table.Columns.Contains("S_NAIM")) S_naim = Convert.ToDecimal(ADataRow["S_NAIM"]); else S_naim = 0;
            if (ADataRow.Table.Columns.Contains("S_WATER")) S_water = Convert.ToDecimal(ADataRow["S_WATER"]); else S_water = 0;
            if (ADataRow.Table.Columns.Contains("S_MUSOR")) S_musor = Convert.ToDecimal(ADataRow["S_MUSOR"]); else S_musor = 0;
            if (ADataRow.Table.Columns.Contains("KOROV")) Korov = Convert.ToDecimal(ADataRow["KOROV"]); else Korov = 0;
            if (ADataRow.Table.Columns.Contains("POLIV")) Poliv = Convert.ToDecimal(ADataRow["POLIV"]); else Poliv = 0;
            if (ADataRow.Table.Columns.Contains("KVS")) Kvs = Convert.ToInt64(ADataRow["KVS"]); else Kvs = 0;
            if (ADataRow.Table.Columns.Contains("CHET")) Chet = Convert.ToInt64(ADataRow["CHET"]); else Chet = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDIK")) Indik = Convert.ToInt64(ADataRow["INDIK"]); else Indik = 0;
            if (ADataRow.Table.Columns.Contains("SUMM")) Summ = Convert.ToDecimal(ADataRow["SUMM"]); else Summ = 0;
            if (ADataRow.Table.Columns.Contains("POK_C")) Pok_c = Convert.ToInt64(ADataRow["POK_C"]); else Pok_c = 0;
            if (ADataRow.Table.Columns.Contains("S_KANAL")) S_kanal = Convert.ToDecimal(ADataRow["S_KANAL"]); else S_kanal = 0;
            if (ADataRow.Table.Columns.Contains("IND_C")) Ind_c = Convert.ToInt64(ADataRow["IND_C"]); else Ind_c = 0;
            if (ADataRow.Table.Columns.Contains("KVS_L")) Kvs_l = Convert.ToInt64(ADataRow["KVS_L"]); else Kvs_l = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Arx_fktRecord retValue = new Arx_fktRecord();
            retValue.N_lits = this.N_lits;
            retValue.Opl = this.Opl;
            retValue.Mes_pl = this.Mes_pl;
            retValue.S_kvart = this.S_kvart;
            retValue.S_naim = this.S_naim;
            retValue.S_water = this.S_water;
            retValue.S_musor = this.S_musor;
            retValue.Korov = this.Korov;
            retValue.Poliv = this.Poliv;
            retValue.Kvs = this.Kvs;
            retValue.Chet = this.Chet;
            retValue.Dat = this.Dat;
            retValue.Indik = this.Indik;
            retValue.Summ = this.Summ;
            retValue.Pok_c = this.Pok_c;
            retValue.S_kanal = this.S_kanal;
            retValue.Ind_c = this.Ind_c;
            retValue.Kvs_l = this.Kvs_l;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ARX_FKT (N_LITS, OPL, MES_PL, S_KVART, S_NAIM, S_WATER, S_MUSOR, KOROV, POLIV, KVS, CHET, DAT, INDIK, SUMM, POK_C, S_KANAL, IND_C, KVS_L) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, CTOD('{11}'), {12}, {13}, {14}, {15}, {16}, {17})", N_lits.ToString(), String.IsNullOrEmpty(Opl) ? "" : Opl.Trim(), String.IsNullOrEmpty(Mes_pl) ? "" : Mes_pl.Trim(), S_kvart.ToString().Replace(',','.'), S_naim.ToString().Replace(',','.'), S_water.ToString().Replace(',','.'), S_musor.ToString().Replace(',','.'), Korov.ToString().Replace(',','.'), Poliv.ToString().Replace(',','.'), Kvs.ToString(), Chet.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Indik.ToString(), Summ.ToString().Replace(',','.'), Pok_c.ToString(), S_kanal.ToString().Replace(',','.'), Ind_c.ToString(), Kvs_l.ToString());
            return rs;
        }
    }
}
	