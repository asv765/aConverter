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
    [TableName("PROMEG1.DBF")]
    public partial class Promeg1Record: AbstractRecord
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

        private decimal metr;
        // <summary>
        // METR N(10,2)
        // </summary>
        [FieldName("METR"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Metr
        {
            get { return metr; }
            set { CheckDecimalData("Metr", value, 10, 2); metr = value; }
        }

        private Int64 nom;
        // <summary>
        // NOM N(5)
        // </summary>
        [FieldName("NOM"), FieldType('N'), FieldWidth(5)]
        public Int64 Nom
        {
            get { return nom; }
            set { CheckIntegerData("Nom", value, 5); nom = value; }
        }

        private Int64 kst;
        // <summary>
        // KST N(4)
        // </summary>
        [FieldName("KST"), FieldType('N'), FieldWidth(4)]
        public Int64 Kst
        {
            get { return kst; }
            set { CheckIntegerData("Kst", value, 4); kst = value; }
        }

        private string fio;
        // <summary>
        // FIO C(50)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(50)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 50); fio = value; }
        }

        private string street;
        // <summary>
        // STREET C(18)
        // </summary>
        [FieldName("STREET"), FieldType('C'), FieldWidth(18)]
        public string Street
        {
            get { return street; }
            set { CheckStringData("Street", value, 18); street = value; }
        }

        private Int64 n_dom;
        // <summary>
        // N_DOM N(4)
        // </summary>
        [FieldName("N_DOM"), FieldType('N'), FieldWidth(4)]
        public Int64 N_dom
        {
            get { return n_dom; }
            set { CheckIntegerData("N_dom", value, 4); n_dom = value; }
        }

        private Int64 n_kw;
        // <summary>
        // N_KW N(4)
        // </summary>
        [FieldName("N_KW"), FieldType('N'), FieldWidth(4)]
        public Int64 N_kw
        {
            get { return n_kw; }
            set { CheckIntegerData("N_kw", value, 4); n_kw = value; }
        }

        private string strok;
        // <summary>
        // STROK C(8)
        // </summary>
        [FieldName("STROK"), FieldType('C'), FieldWidth(8)]
        public string Strok
        {
            get { return strok; }
            set { CheckStringData("Strok", value, 8); strok = value; }
        }

        private string lit;
        // <summary>
        // LIT C(1)
        // </summary>
        [FieldName("LIT"), FieldType('C'), FieldWidth(1)]
        public string Lit
        {
            get { return lit; }
            set { CheckStringData("Lit", value, 1); lit = value; }
        }

        private string kateg;
        // <summary>
        // KATEG M
        // </summary>
        [FieldName("KATEG"), FieldType('M')]
        public string Kateg
        {
            get { return kateg; }
            set { CheckStringData("Kateg", value, 0); kateg = value; }
        }

        private decimal s_kvart;
        // <summary>
        // S_KVART N(10,3)
        // </summary>
        [FieldName("S_KVART"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_kvart
        {
            get { return s_kvart; }
            set { CheckDecimalData("S_kvart", value, 10, 3); s_kvart = value; }
        }

        private decimal s_otopl;
        // <summary>
        // S_OTOPL N(10,3)
        // </summary>
        [FieldName("S_OTOPL"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_otopl
        {
            get { return s_otopl; }
            set { CheckDecimalData("S_otopl", value, 10, 3); s_otopl = value; }
        }

        private decimal s_water;
        // <summary>
        // S_WATER N(10,3)
        // </summary>
        [FieldName("S_WATER"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_water
        {
            get { return s_water; }
            set { CheckDecimalData("S_water", value, 10, 3); s_water = value; }
        }

        private decimal s_kanal;
        // <summary>
        // S_KANAL N(10,3)
        // </summary>
        [FieldName("S_KANAL"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_kanal
        {
            get { return s_kanal; }
            set { CheckDecimalData("S_kanal", value, 10, 3); s_kanal = value; }
        }

        private decimal s_light;
        // <summary>
        // S_LIGHT N(10,3)
        // </summary>
        [FieldName("S_LIGHT"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_light
        {
            get { return s_light; }
            set { CheckDecimalData("S_light", value, 10, 3); s_light = value; }
        }

        private decimal s_naim;
        // <summary>
        // S_NAIM N(10,3)
        // </summary>
        [FieldName("S_NAIM"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal S_naim
        {
            get { return s_naim; }
            set { CheckDecimalData("S_naim", value, 10, 3); s_naim = value; }
        }

        private decimal p_naim;
        // <summary>
        // P_NAIM N(10,3)
        // </summary>
        [FieldName("P_NAIM"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_naim
        {
            get { return p_naim; }
            set { CheckDecimalData("P_naim", value, 10, 3); p_naim = value; }
        }

        private decimal p_kvart;
        // <summary>
        // P_KVART N(10,3)
        // </summary>
        [FieldName("P_KVART"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_kvart
        {
            get { return p_kvart; }
            set { CheckDecimalData("P_kvart", value, 10, 3); p_kvart = value; }
        }

        private decimal p_otopl;
        // <summary>
        // P_OTOPL N(10,3)
        // </summary>
        [FieldName("P_OTOPL"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_otopl
        {
            get { return p_otopl; }
            set { CheckDecimalData("P_otopl", value, 10, 3); p_otopl = value; }
        }

        private decimal p_water;
        // <summary>
        // P_WATER N(10,3)
        // </summary>
        [FieldName("P_WATER"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_water
        {
            get { return p_water; }
            set { CheckDecimalData("P_water", value, 10, 3); p_water = value; }
        }

        private decimal p_kanal;
        // <summary>
        // P_KANAL N(10,3)
        // </summary>
        [FieldName("P_KANAL"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_kanal
        {
            get { return p_kanal; }
            set { CheckDecimalData("P_kanal", value, 10, 3); p_kanal = value; }
        }

        private decimal p_light;
        // <summary>
        // P_LIGHT N(10,3)
        // </summary>
        [FieldName("P_LIGHT"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal P_light
        {
            get { return p_light; }
            set { CheckDecimalData("P_light", value, 10, 3); p_light = value; }
        }

        private decimal p_ob;
        // <summary>
        // P_OB N(10,2)
        // </summary>
        [FieldName("P_OB"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal P_ob
        {
            get { return p_ob; }
            set { CheckDecimalData("P_ob", value, 10, 2); p_ob = value; }
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

        private string strok1;
        // <summary>
        // STROK1 C(26)
        // </summary>
        [FieldName("STROK1"), FieldType('C'), FieldWidth(26)]
        public string Strok1
        {
            get { return strok1; }
            set { CheckStringData("Strok1", value, 26); strok1 = value; }
        }

        private Int64 pok_c1;
        // <summary>
        // POK_C1 N(8)
        // </summary>
        [FieldName("POK_C1"), FieldType('N'), FieldWidth(8)]
        public Int64 Pok_c1
        {
            get { return pok_c1; }
            set { CheckIntegerData("Pok_c1", value, 8); pok_c1 = value; }
        }

        private Int64 pok_c2;
        // <summary>
        // POK_C2 N(8)
        // </summary>
        [FieldName("POK_C2"), FieldType('N'), FieldWidth(8)]
        public Int64 Pok_c2
        {
            get { return pok_c2; }
            set { CheckIntegerData("Pok_c2", value, 8); pok_c2 = value; }
        }

        private decimal s_gwatr;
        // <summary>
        // S_GWATR N(8,2)
        // </summary>
        [FieldName("S_GWATR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_gwatr
        {
            get { return s_gwatr; }
            set { CheckDecimalData("S_gwatr", value, 8, 2); s_gwatr = value; }
        }

        private string ind1;
        // <summary>
        // IND1 C(1)
        // </summary>
        [FieldName("IND1"), FieldType('C'), FieldWidth(1)]
        public string Ind1
        {
            get { return ind1; }
            set { CheckStringData("Ind1", value, 1); ind1 = value; }
        }

        private decimal p_gwatr;
        // <summary>
        // P_GWATR N(8,2)
        // </summary>
        [FieldName("P_GWATR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal P_gwatr
        {
            get { return p_gwatr; }
            set { CheckDecimalData("P_gwatr", value, 8, 2); p_gwatr = value; }
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

        private decimal p_musor;
        // <summary>
        // P_MUSOR N(8,2)
        // </summary>
        [FieldName("P_MUSOR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal P_musor
        {
            get { return p_musor; }
            set { CheckDecimalData("P_musor", value, 8, 2); p_musor = value; }
        }

        private decimal p_korov;
        // <summary>
        // P_KOROV N(8,2)
        // </summary>
        [FieldName("P_KOROV"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal P_korov
        {
            get { return p_korov; }
            set { CheckDecimalData("P_korov", value, 8, 2); p_korov = value; }
        }

        private decimal s_korov;
        // <summary>
        // S_KOROV N(8,2)
        // </summary>
        [FieldName("S_KOROV"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_korov
        {
            get { return s_korov; }
            set { CheckDecimalData("S_korov", value, 8, 2); s_korov = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("METR")) Metr = Convert.ToDecimal(ADataRow["METR"]); else Metr = 0;
            if (ADataRow.Table.Columns.Contains("NOM")) Nom = Convert.ToInt64(ADataRow["NOM"]); else Nom = 0;
            if (ADataRow.Table.Columns.Contains("KST")) Kst = Convert.ToInt64(ADataRow["KST"]); else Kst = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("STREET")) Street = ADataRow["STREET"].ToString(); else Street = "";
            if (ADataRow.Table.Columns.Contains("N_DOM")) N_dom = Convert.ToInt64(ADataRow["N_DOM"]); else N_dom = 0;
            if (ADataRow.Table.Columns.Contains("N_KW")) N_kw = Convert.ToInt64(ADataRow["N_KW"]); else N_kw = 0;
            if (ADataRow.Table.Columns.Contains("STROK")) Strok = ADataRow["STROK"].ToString(); else Strok = "";
            if (ADataRow.Table.Columns.Contains("LIT")) Lit = ADataRow["LIT"].ToString(); else Lit = "";
            if (ADataRow.Table.Columns.Contains("KATEG")) Kateg = ADataRow["KATEG"].ToString(); else Kateg = "";
            if (ADataRow.Table.Columns.Contains("S_KVART")) S_kvart = Convert.ToDecimal(ADataRow["S_KVART"]); else S_kvart = 0;
            if (ADataRow.Table.Columns.Contains("S_OTOPL")) S_otopl = Convert.ToDecimal(ADataRow["S_OTOPL"]); else S_otopl = 0;
            if (ADataRow.Table.Columns.Contains("S_WATER")) S_water = Convert.ToDecimal(ADataRow["S_WATER"]); else S_water = 0;
            if (ADataRow.Table.Columns.Contains("S_KANAL")) S_kanal = Convert.ToDecimal(ADataRow["S_KANAL"]); else S_kanal = 0;
            if (ADataRow.Table.Columns.Contains("S_LIGHT")) S_light = Convert.ToDecimal(ADataRow["S_LIGHT"]); else S_light = 0;
            if (ADataRow.Table.Columns.Contains("S_NAIM")) S_naim = Convert.ToDecimal(ADataRow["S_NAIM"]); else S_naim = 0;
            if (ADataRow.Table.Columns.Contains("P_NAIM")) P_naim = Convert.ToDecimal(ADataRow["P_NAIM"]); else P_naim = 0;
            if (ADataRow.Table.Columns.Contains("P_KVART")) P_kvart = Convert.ToDecimal(ADataRow["P_KVART"]); else P_kvart = 0;
            if (ADataRow.Table.Columns.Contains("P_OTOPL")) P_otopl = Convert.ToDecimal(ADataRow["P_OTOPL"]); else P_otopl = 0;
            if (ADataRow.Table.Columns.Contains("P_WATER")) P_water = Convert.ToDecimal(ADataRow["P_WATER"]); else P_water = 0;
            if (ADataRow.Table.Columns.Contains("P_KANAL")) P_kanal = Convert.ToDecimal(ADataRow["P_KANAL"]); else P_kanal = 0;
            if (ADataRow.Table.Columns.Contains("P_LIGHT")) P_light = Convert.ToDecimal(ADataRow["P_LIGHT"]); else P_light = 0;
            if (ADataRow.Table.Columns.Contains("P_OB")) P_ob = Convert.ToDecimal(ADataRow["P_OB"]); else P_ob = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("STROK1")) Strok1 = ADataRow["STROK1"].ToString(); else Strok1 = "";
            if (ADataRow.Table.Columns.Contains("POK_C1")) Pok_c1 = Convert.ToInt64(ADataRow["POK_C1"]); else Pok_c1 = 0;
            if (ADataRow.Table.Columns.Contains("POK_C2")) Pok_c2 = Convert.ToInt64(ADataRow["POK_C2"]); else Pok_c2 = 0;
            if (ADataRow.Table.Columns.Contains("S_GWATR")) S_gwatr = Convert.ToDecimal(ADataRow["S_GWATR"]); else S_gwatr = 0;
            if (ADataRow.Table.Columns.Contains("IND1")) Ind1 = ADataRow["IND1"].ToString(); else Ind1 = "";
            if (ADataRow.Table.Columns.Contains("P_GWATR")) P_gwatr = Convert.ToDecimal(ADataRow["P_GWATR"]); else P_gwatr = 0;
            if (ADataRow.Table.Columns.Contains("S_MUSOR")) S_musor = Convert.ToDecimal(ADataRow["S_MUSOR"]); else S_musor = 0;
            if (ADataRow.Table.Columns.Contains("P_MUSOR")) P_musor = Convert.ToDecimal(ADataRow["P_MUSOR"]); else P_musor = 0;
            if (ADataRow.Table.Columns.Contains("P_KOROV")) P_korov = Convert.ToDecimal(ADataRow["P_KOROV"]); else P_korov = 0;
            if (ADataRow.Table.Columns.Contains("S_KOROV")) S_korov = Convert.ToDecimal(ADataRow["S_KOROV"]); else S_korov = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Promeg1Record retValue = new Promeg1Record();
            retValue.N_lits = this.N_lits;
            retValue.Metr = this.Metr;
            retValue.Nom = this.Nom;
            retValue.Kst = this.Kst;
            retValue.Fio = this.Fio;
            retValue.Street = this.Street;
            retValue.N_dom = this.N_dom;
            retValue.N_kw = this.N_kw;
            retValue.Strok = this.Strok;
            retValue.Lit = this.Lit;
            retValue.Kateg = this.Kateg;
            retValue.S_kvart = this.S_kvart;
            retValue.S_otopl = this.S_otopl;
            retValue.S_water = this.S_water;
            retValue.S_kanal = this.S_kanal;
            retValue.S_light = this.S_light;
            retValue.S_naim = this.S_naim;
            retValue.P_naim = this.P_naim;
            retValue.P_kvart = this.P_kvart;
            retValue.P_otopl = this.P_otopl;
            retValue.P_water = this.P_water;
            retValue.P_kanal = this.P_kanal;
            retValue.P_light = this.P_light;
            retValue.P_ob = this.P_ob;
            retValue.Dat = this.Dat;
            retValue.Strok1 = this.Strok1;
            retValue.Pok_c1 = this.Pok_c1;
            retValue.Pok_c2 = this.Pok_c2;
            retValue.S_gwatr = this.S_gwatr;
            retValue.Ind1 = this.Ind1;
            retValue.P_gwatr = this.P_gwatr;
            retValue.S_musor = this.S_musor;
            retValue.P_musor = this.P_musor;
            retValue.P_korov = this.P_korov;
            retValue.S_korov = this.S_korov;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PROMEG1 (N_LITS, METR, NOM, KST, FIO, STREET, N_DOM, N_KW, STROK, LIT, KATEG, S_KVART, S_OTOPL, S_WATER, S_KANAL, S_LIGHT, S_NAIM, P_NAIM, P_KVART, P_OTOPL, P_WATER, P_KANAL, P_LIGHT, P_OB, DAT, STROK1, POK_C1, POK_C2, S_GWATR, IND1, P_GWATR, S_MUSOR, P_MUSOR, P_KOROV, S_KOROV) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}', {6}, {7}, '{8}', '{9}', '{10}', {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, CTOD('{24}'), '{25}', {26}, {27}, {28}, '{29}', {30}, {31}, {32}, {33}, {34})", N_lits.ToString(), Metr.ToString().Replace(',','.'), Nom.ToString(), Kst.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Street) ? "" : Street.Trim(), N_dom.ToString(), N_kw.ToString(), String.IsNullOrEmpty(Strok) ? "" : Strok.Trim(), String.IsNullOrEmpty(Lit) ? "" : Lit.Trim(), String.IsNullOrEmpty(Kateg) ? "" : Kateg.Trim(), S_kvart.ToString().Replace(',','.'), S_otopl.ToString().Replace(',','.'), S_water.ToString().Replace(',','.'), S_kanal.ToString().Replace(',','.'), S_light.ToString().Replace(',','.'), S_naim.ToString().Replace(',','.'), P_naim.ToString().Replace(',','.'), P_kvart.ToString().Replace(',','.'), P_otopl.ToString().Replace(',','.'), P_water.ToString().Replace(',','.'), P_kanal.ToString().Replace(',','.'), P_light.ToString().Replace(',','.'), P_ob.ToString().Replace(',','.'), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), String.IsNullOrEmpty(Strok1) ? "" : Strok1.Trim(), Pok_c1.ToString(), Pok_c2.ToString(), S_gwatr.ToString().Replace(',','.'), String.IsNullOrEmpty(Ind1) ? "" : Ind1.Trim(), P_gwatr.ToString().Replace(',','.'), S_musor.ToString().Replace(',','.'), P_musor.ToString().Replace(',','.'), P_korov.ToString().Replace(',','.'), S_korov.ToString().Replace(',','.'));
            return rs;
        }
    }
}
	