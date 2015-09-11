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
    [TableName("SUBSID.DBF")]
    public partial class SubsidRecord: AbstractRecord
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

        private decimal kvart;
        // <summary>
        // KVART N(8,2)
        // </summary>
        [FieldName("KVART"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Kvart
        {
            get { return kvart; }
            set { CheckDecimalData("Kvart", value, 8, 2); kvart = value; }
        }

        private decimal otopl;
        // <summary>
        // OTOPL N(8,2)
        // </summary>
        [FieldName("OTOPL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Otopl
        {
            get { return otopl; }
            set { CheckDecimalData("Otopl", value, 8, 2); otopl = value; }
        }

        private decimal light;
        // <summary>
        // LIGHT N(8,2)
        // </summary>
        [FieldName("LIGHT"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Light
        {
            get { return light; }
            set { CheckDecimalData("Light", value, 8, 2); light = value; }
        }

        private decimal water;
        // <summary>
        // WATER N(8,2)
        // </summary>
        [FieldName("WATER"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Water
        {
            get { return water; }
            set { CheckDecimalData("Water", value, 8, 2); water = value; }
        }

        private decimal musor;
        // <summary>
        // MUSOR N(8,2)
        // </summary>
        [FieldName("MUSOR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Musor
        {
            get { return musor; }
            set { CheckDecimalData("Musor", value, 8, 2); musor = value; }
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

        private decimal summ;
        // <summary>
        // SUMM N(8,2)
        // </summary>
        [FieldName("SUMM"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Summ
        {
            get { return summ; }
            set { CheckDecimalData("Summ", value, 8, 2); summ = value; }
        }

        private decimal kanal;
        // <summary>
        // KANAL N(8,2)
        // </summary>
        [FieldName("KANAL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Kanal
        {
            get { return kanal; }
            set { CheckDecimalData("Kanal", value, 8, 2); kanal = value; }
        }

        private decimal gass;
        // <summary>
        // GASS N(8,2)
        // </summary>
        [FieldName("GASS"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Gass
        {
            get { return gass; }
            set { CheckDecimalData("Gass", value, 8, 2); gass = value; }
        }

        private decimal g_wat;
        // <summary>
        // G_WAT N(8,2)
        // </summary>
        [FieldName("G_WAT"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal G_wat
        {
            get { return g_wat; }
            set { CheckDecimalData("G_wat", value, 8, 2); g_wat = value; }
        }

        private decimal sov_dox;
        // <summary>
        // SOV_DOX N(8,2)
        // </summary>
        [FieldName("SOV_DOX"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Sov_dox
        {
            get { return sov_dox; }
            set { CheckDecimalData("Sov_dox", value, 8, 2); sov_dox = value; }
        }

        private string inter;
        // <summary>
        // INTER C(20)
        // </summary>
        [FieldName("INTER"), FieldType('C'), FieldWidth(20)]
        public string Inter
        {
            get { return inter; }
            set { CheckStringData("Inter", value, 20); inter = value; }
        }

        private Int64 indik;
        // <summary>
        // INDIK N(2)
        // </summary>
        [FieldName("INDIK"), FieldType('N'), FieldWidth(2)]
        public Int64 Indik
        {
            get { return indik; }
            set { CheckIntegerData("Indik", value, 2); indik = value; }
        }

        private decimal minus;
        // <summary>
        // MINUS N(8,2)
        // </summary>
        [FieldName("MINUS"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Minus
        {
            get { return minus; }
            set { CheckDecimalData("Minus", value, 8, 2); minus = value; }
        }

        private string ind;
        // <summary>
        // IND C(1)
        // </summary>
        [FieldName("IND"), FieldType('C'), FieldWidth(1)]
        public string Ind
        {
            get { return ind; }
            set { CheckStringData("Ind", value, 1); ind = value; }
        }

        private decimal minus1;
        // <summary>
        // MINUS1 N(8,2)
        // </summary>
        [FieldName("MINUS1"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Minus1
        {
            get { return minus1; }
            set { CheckDecimalData("Minus1", value, 8, 2); minus1 = value; }
        }

        private Int64 v;
        // <summary>
        // V N(2)
        // </summary>
        [FieldName("V"), FieldType('N'), FieldWidth(2)]
        public Int64 V
        {
            get { return v; }
            set { CheckIntegerData("V", value, 2); v = value; }
        }

        private Int64 d;
        // <summary>
        // D N(2)
        // </summary>
        [FieldName("D"), FieldType('N'), FieldWidth(2)]
        public Int64 D
        {
            get { return d; }
            set { CheckIntegerData("D", value, 2); d = value; }
        }

        private Int64 p;
        // <summary>
        // P N(2)
        // </summary>
        [FieldName("P"), FieldType('N'), FieldWidth(2)]
        public Int64 P
        {
            get { return p; }
            set { CheckIntegerData("P", value, 2); p = value; }
        }

        private Int64 n_gass;
        // <summary>
        // N_GASS N(5)
        // </summary>
        [FieldName("N_GASS"), FieldType('N'), FieldWidth(5)]
        public Int64 N_gass
        {
            get { return n_gass; }
            set { CheckIntegerData("N_gass", value, 5); n_gass = value; }
        }

        private Int64 n_shet;
        // <summary>
        // N_SHET N(21)
        // </summary>
        [FieldName("N_SHET"), FieldType('N'), FieldWidth(21)]
        public Int64 N_shet
        {
            get { return n_shet; }
            set { CheckIntegerData("N_shet", value, 21); n_shet = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("KVART")) Kvart = Convert.ToDecimal(ADataRow["KVART"]); else Kvart = 0;
            if (ADataRow.Table.Columns.Contains("OTOPL")) Otopl = Convert.ToDecimal(ADataRow["OTOPL"]); else Otopl = 0;
            if (ADataRow.Table.Columns.Contains("LIGHT")) Light = Convert.ToDecimal(ADataRow["LIGHT"]); else Light = 0;
            if (ADataRow.Table.Columns.Contains("WATER")) Water = Convert.ToDecimal(ADataRow["WATER"]); else Water = 0;
            if (ADataRow.Table.Columns.Contains("MUSOR")) Musor = Convert.ToDecimal(ADataRow["MUSOR"]); else Musor = 0;
            if (ADataRow.Table.Columns.Contains("MES_PL")) Mes_pl = ADataRow["MES_PL"].ToString(); else Mes_pl = "";
            if (ADataRow.Table.Columns.Contains("SUMM")) Summ = Convert.ToDecimal(ADataRow["SUMM"]); else Summ = 0;
            if (ADataRow.Table.Columns.Contains("KANAL")) Kanal = Convert.ToDecimal(ADataRow["KANAL"]); else Kanal = 0;
            if (ADataRow.Table.Columns.Contains("GASS")) Gass = Convert.ToDecimal(ADataRow["GASS"]); else Gass = 0;
            if (ADataRow.Table.Columns.Contains("G_WAT")) G_wat = Convert.ToDecimal(ADataRow["G_WAT"]); else G_wat = 0;
            if (ADataRow.Table.Columns.Contains("SOV_DOX")) Sov_dox = Convert.ToDecimal(ADataRow["SOV_DOX"]); else Sov_dox = 0;
            if (ADataRow.Table.Columns.Contains("INTER")) Inter = ADataRow["INTER"].ToString(); else Inter = "";
            if (ADataRow.Table.Columns.Contains("INDIK")) Indik = Convert.ToInt64(ADataRow["INDIK"]); else Indik = 0;
            if (ADataRow.Table.Columns.Contains("MINUS")) Minus = Convert.ToDecimal(ADataRow["MINUS"]); else Minus = 0;
            if (ADataRow.Table.Columns.Contains("IND")) Ind = ADataRow["IND"].ToString(); else Ind = "";
            if (ADataRow.Table.Columns.Contains("MINUS1")) Minus1 = Convert.ToDecimal(ADataRow["MINUS1"]); else Minus1 = 0;
            if (ADataRow.Table.Columns.Contains("V")) V = Convert.ToInt64(ADataRow["V"]); else V = 0;
            if (ADataRow.Table.Columns.Contains("D")) D = Convert.ToInt64(ADataRow["D"]); else D = 0;
            if (ADataRow.Table.Columns.Contains("P")) P = Convert.ToInt64(ADataRow["P"]); else P = 0;
            if (ADataRow.Table.Columns.Contains("N_GASS")) N_gass = Convert.ToInt64(ADataRow["N_GASS"]); else N_gass = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET")) N_shet = Convert.ToInt64(ADataRow["N_SHET"]); else N_shet = 0;
        }
        
        public override AbstractRecord Clone()
        {
            SubsidRecord retValue = new SubsidRecord();
            retValue.N_lits = this.N_lits;
            retValue.Fio = this.Fio;
            retValue.Kvart = this.Kvart;
            retValue.Otopl = this.Otopl;
            retValue.Light = this.Light;
            retValue.Water = this.Water;
            retValue.Musor = this.Musor;
            retValue.Mes_pl = this.Mes_pl;
            retValue.Summ = this.Summ;
            retValue.Kanal = this.Kanal;
            retValue.Gass = this.Gass;
            retValue.G_wat = this.G_wat;
            retValue.Sov_dox = this.Sov_dox;
            retValue.Inter = this.Inter;
            retValue.Indik = this.Indik;
            retValue.Minus = this.Minus;
            retValue.Ind = this.Ind;
            retValue.Minus1 = this.Minus1;
            retValue.V = this.V;
            retValue.D = this.D;
            retValue.P = this.P;
            retValue.N_gass = this.N_gass;
            retValue.N_shet = this.N_shet;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SUBSID (N_LITS, FIO, KVART, OTOPL, LIGHT, WATER, MUSOR, MES_PL, SUMM, KANAL, GASS, G_WAT, SOV_DOX, INTER, INDIK, MINUS, IND, MINUS1, V, D, P, N_GASS, N_SHET) VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, {6}, '{7}', {8}, {9}, {10}, {11}, {12}, '{13}', {14}, {15}, '{16}', {17}, {18}, {19}, {20}, {21}, {22})", N_lits.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Kvart.ToString().Replace(',','.'), Otopl.ToString().Replace(',','.'), Light.ToString().Replace(',','.'), Water.ToString().Replace(',','.'), Musor.ToString().Replace(',','.'), String.IsNullOrEmpty(Mes_pl) ? "" : Mes_pl.Trim(), Summ.ToString().Replace(',','.'), Kanal.ToString().Replace(',','.'), Gass.ToString().Replace(',','.'), G_wat.ToString().Replace(',','.'), Sov_dox.ToString().Replace(',','.'), String.IsNullOrEmpty(Inter) ? "" : Inter.Trim(), Indik.ToString(), Minus.ToString().Replace(',','.'), String.IsNullOrEmpty(Ind) ? "" : Ind.Trim(), Minus1.ToString().Replace(',','.'), V.ToString(), D.ToString(), P.ToString(), N_gass.ToString(), N_shet.ToString());
            return rs;
        }
    }
}
	