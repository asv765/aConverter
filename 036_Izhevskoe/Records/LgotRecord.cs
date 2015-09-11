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
    [TableName("LGOT.DBF")]
    public partial class LgotRecord: AbstractRecord
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
        // FIO C(60)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(60)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 60); fio = value; }
        }

        private string famil;
        // <summary>
        // FAMIL C(50)
        // </summary>
        [FieldName("FAMIL"), FieldType('C'), FieldWidth(50)]
        public string Famil
        {
            get { return famil; }
            set { CheckStringData("Famil", value, 50); famil = value; }
        }

        private string imja;
        // <summary>
        // IMJA C(50)
        // </summary>
        [FieldName("IMJA"), FieldType('C'), FieldWidth(50)]
        public string Imja
        {
            get { return imja; }
            set { CheckStringData("Imja", value, 50); imja = value; }
        }

        private string otch;
        // <summary>
        // OTCH C(50)
        // </summary>
        [FieldName("OTCH"), FieldType('C'), FieldWidth(50)]
        public string Otch
        {
            get { return otch; }
            set { CheckStringData("Otch", value, 50); otch = value; }
        }

        private DateTime drog;
        // <summary>
        // DROG D(8)
        // </summary>
        [FieldName("DROG"), FieldType('D'), FieldWidth(8)]
        public DateTime Drog
        {
            get { return drog; }
            set {  drog = value; }
        }

        private Int64 kod_l;
        // <summary>
        // KOD_L N(3)
        // </summary>
        [FieldName("KOD_L"), FieldType('N'), FieldWidth(3)]
        public Int64 Kod_l
        {
            get { return kod_l; }
            set { CheckIntegerData("Kod_l", value, 3); kod_l = value; }
        }

        private string ind1;
        // <summary>
        // IND1 C(2)
        // </summary>
        [FieldName("IND1"), FieldType('C'), FieldWidth(2)]
        public string Ind1
        {
            get { return ind1; }
            set { CheckStringData("Ind1", value, 2); ind1 = value; }
        }

        private Int64 god_r;
        // <summary>
        // GOD_R N(5)
        // </summary>
        [FieldName("GOD_R"), FieldType('N'), FieldWidth(5)]
        public Int64 God_r
        {
            get { return god_r; }
            set { CheckIntegerData("God_r", value, 5); god_r = value; }
        }

        private string ser;
        // <summary>
        // SER C(7)
        // </summary>
        [FieldName("SER"), FieldType('C'), FieldWidth(7)]
        public string Ser
        {
            get { return ser; }
            set { CheckStringData("Ser", value, 7); ser = value; }
        }

        private Int64 n_udos;
        // <summary>
        // N_UDOS N(11)
        // </summary>
        [FieldName("N_UDOS"), FieldType('N'), FieldWidth(11)]
        public Int64 N_udos
        {
            get { return n_udos; }
            set { CheckIntegerData("N_udos", value, 11); n_udos = value; }
        }

        private DateTime dat_v;
        // <summary>
        // DAT_V D(8)
        // </summary>
        [FieldName("DAT_V"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_v
        {
            get { return dat_v; }
            set {  dat_v = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("FAMIL")) Famil = ADataRow["FAMIL"].ToString(); else Famil = "";
            if (ADataRow.Table.Columns.Contains("IMJA")) Imja = ADataRow["IMJA"].ToString(); else Imja = "";
            if (ADataRow.Table.Columns.Contains("OTCH")) Otch = ADataRow["OTCH"].ToString(); else Otch = "";
            if (ADataRow.Table.Columns.Contains("DROG")) Drog = Convert.ToDateTime(ADataRow["DROG"]); else Drog = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("KOD_L")) Kod_l = Convert.ToInt64(ADataRow["KOD_L"]); else Kod_l = 0;
            if (ADataRow.Table.Columns.Contains("IND1")) Ind1 = ADataRow["IND1"].ToString(); else Ind1 = "";
            if (ADataRow.Table.Columns.Contains("GOD_R")) God_r = Convert.ToInt64(ADataRow["GOD_R"]); else God_r = 0;
            if (ADataRow.Table.Columns.Contains("SER")) Ser = ADataRow["SER"].ToString(); else Ser = "";
            if (ADataRow.Table.Columns.Contains("N_UDOS")) N_udos = Convert.ToInt64(ADataRow["N_UDOS"]); else N_udos = 0;
            if (ADataRow.Table.Columns.Contains("DAT_V")) Dat_v = Convert.ToDateTime(ADataRow["DAT_V"]); else Dat_v = DateTime.MinValue;
        }
        
        public override AbstractRecord Clone()
        {
            LgotRecord retValue = new LgotRecord();
            retValue.N_lits = this.N_lits;
            retValue.Fio = this.Fio;
            retValue.Famil = this.Famil;
            retValue.Imja = this.Imja;
            retValue.Otch = this.Otch;
            retValue.Drog = this.Drog;
            retValue.Kod_l = this.Kod_l;
            retValue.Ind1 = this.Ind1;
            retValue.God_r = this.God_r;
            retValue.Ser = this.Ser;
            retValue.N_udos = this.N_udos;
            retValue.Dat_v = this.Dat_v;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LGOT (N_LITS, FIO, FAMIL, IMJA, OTCH, DROG, KOD_L, IND1, GOD_R, SER, N_UDOS, DAT_V) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', CTOD('{5}'), {6}, '{7}', {8}, '{9}', {10}, CTOD('{11}'))", N_lits.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Famil) ? "" : Famil.Trim(), String.IsNullOrEmpty(Imja) ? "" : Imja.Trim(), String.IsNullOrEmpty(Otch) ? "" : Otch.Trim(), Drog == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Drog.Month, Drog.Day, Drog.Year), Kod_l.ToString(), String.IsNullOrEmpty(Ind1) ? "" : Ind1.Trim(), God_r.ToString(), String.IsNullOrEmpty(Ser) ? "" : Ser.Trim(), N_udos.ToString(), Dat_v == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_v.Month, Dat_v.Day, Dat_v.Year));
            return rs;
        }
    }
}
	