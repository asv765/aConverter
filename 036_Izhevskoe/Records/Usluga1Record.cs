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
    [TableName("USLUGA1.DBF")]
    public partial class Usluga1Record: AbstractRecord
    {
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

        private string posel;
        // <summary>
        // POSEL C(30)
        // </summary>
        [FieldName("POSEL"), FieldType('C'), FieldWidth(30)]
        public string Posel
        {
            get { return posel; }
            set { CheckStringData("Posel", value, 30); posel = value; }
        }

        private string nasp;
        // <summary>
        // NASP C(90)
        // </summary>
        [FieldName("NASP"), FieldType('C'), FieldWidth(90)]
        public string Nasp
        {
            get { return nasp; }
            set { CheckStringData("Nasp", value, 90); nasp = value; }
        }

        private string ylic;
        // <summary>
        // YLIC C(90)
        // </summary>
        [FieldName("YLIC"), FieldType('C'), FieldWidth(90)]
        public string Ylic
        {
            get { return ylic; }
            set { CheckStringData("Ylic", value, 90); ylic = value; }
        }

        private string ndom;
        // <summary>
        // NDOM C(50)
        // </summary>
        [FieldName("NDOM"), FieldType('C'), FieldWidth(50)]
        public string Ndom
        {
            get { return ndom; }
            set { CheckStringData("Ndom", value, 50); ndom = value; }
        }

        private string nkorp;
        // <summary>
        // NKORP C(50)
        // </summary>
        [FieldName("NKORP"), FieldType('C'), FieldWidth(50)]
        public string Nkorp
        {
            get { return nkorp; }
            set { CheckStringData("Nkorp", value, 50); nkorp = value; }
        }

        private string nkw;
        // <summary>
        // NKW C(50)
        // </summary>
        [FieldName("NKW"), FieldType('C'), FieldWidth(50)]
        public string Nkw
        {
            get { return nkw; }
            set { CheckStringData("Nkw", value, 50); nkw = value; }
        }

        private string nkomn;
        // <summary>
        // NKOMN C(15)
        // </summary>
        [FieldName("NKOMN"), FieldType('C'), FieldWidth(15)]
        public string Nkomn
        {
            get { return nkomn; }
            set { CheckStringData("Nkomn", value, 15); nkomn = value; }
        }

        private string lchard;
        // <summary>
        // LCHARD C(15)
        // </summary>
        [FieldName("LCHARD"), FieldType('C'), FieldWidth(15)]
        public string Lchard
        {
            get { return lchard; }
            set { CheckStringData("Lchard", value, 15); lchard = value; }
        }

        private string ilchet;
        // <summary>
        // ILCHET C(24)
        // </summary>
        [FieldName("ILCHET"), FieldType('C'), FieldWidth(24)]
        public string Ilchet
        {
            get { return ilchet; }
            set { CheckStringData("Ilchet", value, 24); ilchet = value; }
        }

        private string vidgf;
        // <summary>
        // VIDGF C(25)
        // </summary>
        [FieldName("VIDGF"), FieldType('C'), FieldWidth(25)]
        public string Vidgf
        {
            get { return vidgf; }
            set { CheckStringData("Vidgf", value, 25); vidgf = value; }
        }

        private string lift;
        // <summary>
        // LIFT C(1)
        // </summary>
        [FieldName("LIFT"), FieldType('C'), FieldWidth(1)]
        public string Lift
        {
            get { return lift; }
            set { CheckStringData("Lift", value, 1); lift = value; }
        }

        private decimal opl;
        // <summary>
        // OPL N(8,2)
        // </summary>
        [FieldName("OPL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Opl
        {
            get { return opl; }
            set { CheckDecimalData("Opl", value, 8, 2); opl = value; }
        }

        private decimal otpl;
        // <summary>
        // OTPL N(8,2)
        // </summary>
        [FieldName("OTPL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Otpl
        {
            get { return otpl; }
            set { CheckDecimalData("Otpl", value, 8, 2); otpl = value; }
        }

        private Int64 kolzr;
        // <summary>
        // KOLZR N(3)
        // </summary>
        [FieldName("KOLZR"), FieldType('N'), FieldWidth(3)]
        public Int64 Kolzr
        {
            get { return kolzr; }
            set { CheckIntegerData("Kolzr", value, 3); kolzr = value; }
        }

        private string gku;
        // <summary>
        // GKU C(50)
        // </summary>
        [FieldName("GKU"), FieldType('C'), FieldWidth(50)]
        public string Gku
        {
            get { return gku; }
            set { CheckStringData("Gku", value, 50); gku = value; }
        }

        private string org;
        // <summary>
        // ORG C(30)
        // </summary>
        [FieldName("ORG"), FieldType('C'), FieldWidth(30)]
        public string Org
        {
            get { return org; }
            set { CheckStringData("Org", value, 30); org = value; }
        }

        private Int64 vidtar;
        // <summary>
        // VIDTAR N(2)
        // </summary>
        [FieldName("VIDTAR"), FieldType('N'), FieldWidth(2)]
        public Int64 Vidtar
        {
            get { return vidtar; }
            set { CheckIntegerData("Vidtar", value, 2); vidtar = value; }
        }

        private Int64 koef;
        // <summary>
        // KOEF N(11)
        // </summary>
        [FieldName("KOEF"), FieldType('N'), FieldWidth(11)]
        public Int64 Koef
        {
            get { return koef; }
            set { CheckIntegerData("Koef", value, 11); koef = value; }
        }

        private decimal tarif;
        // <summary>
        // TARIF N(8,2)
        // </summary>
        [FieldName("TARIF"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Tarif
        {
            get { return tarif; }
            set { CheckDecimalData("Tarif", value, 8, 2); tarif = value; }
        }

        private decimal fakt;
        // <summary>
        // FAKT N(8,3)
        // </summary>
        [FieldName("FAKT"), FieldType('N'), FieldWidth(8), FieldDec(3)]
        public decimal Fakt
        {
            get { return fakt; }
            set { CheckDecimalData("Fakt", value, 8, 3); fakt = value; }
        }

        private decimal sumtar;
        // <summary>
        // SUMTAR N(8,2)
        // </summary>
        [FieldName("SUMTAR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Sumtar
        {
            get { return sumtar; }
            set { CheckDecimalData("Sumtar", value, 8, 2); sumtar = value; }
        }

        private decimal sumdolg;
        // <summary>
        // SUMDOLG N(12,2)
        // </summary>
        [FieldName("SUMDOLG"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sumdolg
        {
            get { return sumdolg; }
            set { CheckDecimalData("Sumdolg", value, 12, 2); sumdolg = value; }
        }

        private decimal opldolg;
        // <summary>
        // OPLDOLG N(12,2)
        // </summary>
        [FieldName("OPLDOLG"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Opldolg
        {
            get { return opldolg; }
            set { CheckDecimalData("Opldolg", value, 12, 2); opldolg = value; }
        }

        private DateTime datdolg;
        // <summary>
        // DATDOLG D(8)
        // </summary>
        [FieldName("DATDOLG"), FieldType('D'), FieldWidth(8)]
        public DateTime Datdolg
        {
            get { return datdolg; }
            set {  datdolg = value; }
        }

        private Int64 koldolg;
        // <summary>
        // KOLDOLG N(4)
        // </summary>
        [FieldName("KOLDOLG"), FieldType('N'), FieldWidth(4)]
        public Int64 Koldolg
        {
            get { return koldolg; }
            set { CheckIntegerData("Koldolg", value, 4); koldolg = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("FAMIL")) Famil = ADataRow["FAMIL"].ToString(); else Famil = "";
            if (ADataRow.Table.Columns.Contains("IMJA")) Imja = ADataRow["IMJA"].ToString(); else Imja = "";
            if (ADataRow.Table.Columns.Contains("OTCH")) Otch = ADataRow["OTCH"].ToString(); else Otch = "";
            if (ADataRow.Table.Columns.Contains("DROG")) Drog = Convert.ToDateTime(ADataRow["DROG"]); else Drog = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("POSEL")) Posel = ADataRow["POSEL"].ToString(); else Posel = "";
            if (ADataRow.Table.Columns.Contains("NASP")) Nasp = ADataRow["NASP"].ToString(); else Nasp = "";
            if (ADataRow.Table.Columns.Contains("YLIC")) Ylic = ADataRow["YLIC"].ToString(); else Ylic = "";
            if (ADataRow.Table.Columns.Contains("NDOM")) Ndom = ADataRow["NDOM"].ToString(); else Ndom = "";
            if (ADataRow.Table.Columns.Contains("NKORP")) Nkorp = ADataRow["NKORP"].ToString(); else Nkorp = "";
            if (ADataRow.Table.Columns.Contains("NKW")) Nkw = ADataRow["NKW"].ToString(); else Nkw = "";
            if (ADataRow.Table.Columns.Contains("NKOMN")) Nkomn = ADataRow["NKOMN"].ToString(); else Nkomn = "";
            if (ADataRow.Table.Columns.Contains("LCHARD")) Lchard = ADataRow["LCHARD"].ToString(); else Lchard = "";
            if (ADataRow.Table.Columns.Contains("ILCHET")) Ilchet = ADataRow["ILCHET"].ToString(); else Ilchet = "";
            if (ADataRow.Table.Columns.Contains("VIDGF")) Vidgf = ADataRow["VIDGF"].ToString(); else Vidgf = "";
            if (ADataRow.Table.Columns.Contains("LIFT")) Lift = ADataRow["LIFT"].ToString(); else Lift = "";
            if (ADataRow.Table.Columns.Contains("OPL")) Opl = Convert.ToDecimal(ADataRow["OPL"]); else Opl = 0;
            if (ADataRow.Table.Columns.Contains("OTPL")) Otpl = Convert.ToDecimal(ADataRow["OTPL"]); else Otpl = 0;
            if (ADataRow.Table.Columns.Contains("KOLZR")) Kolzr = Convert.ToInt64(ADataRow["KOLZR"]); else Kolzr = 0;
            if (ADataRow.Table.Columns.Contains("GKU")) Gku = ADataRow["GKU"].ToString(); else Gku = "";
            if (ADataRow.Table.Columns.Contains("ORG")) Org = ADataRow["ORG"].ToString(); else Org = "";
            if (ADataRow.Table.Columns.Contains("VIDTAR")) Vidtar = Convert.ToInt64(ADataRow["VIDTAR"]); else Vidtar = 0;
            if (ADataRow.Table.Columns.Contains("KOEF")) Koef = Convert.ToInt64(ADataRow["KOEF"]); else Koef = 0;
            if (ADataRow.Table.Columns.Contains("TARIF")) Tarif = Convert.ToDecimal(ADataRow["TARIF"]); else Tarif = 0;
            if (ADataRow.Table.Columns.Contains("FAKT")) Fakt = Convert.ToDecimal(ADataRow["FAKT"]); else Fakt = 0;
            if (ADataRow.Table.Columns.Contains("SUMTAR")) Sumtar = Convert.ToDecimal(ADataRow["SUMTAR"]); else Sumtar = 0;
            if (ADataRow.Table.Columns.Contains("SUMDOLG")) Sumdolg = Convert.ToDecimal(ADataRow["SUMDOLG"]); else Sumdolg = 0;
            if (ADataRow.Table.Columns.Contains("OPLDOLG")) Opldolg = Convert.ToDecimal(ADataRow["OPLDOLG"]); else Opldolg = 0;
            if (ADataRow.Table.Columns.Contains("DATDOLG")) Datdolg = Convert.ToDateTime(ADataRow["DATDOLG"]); else Datdolg = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("KOLDOLG")) Koldolg = Convert.ToInt64(ADataRow["KOLDOLG"]); else Koldolg = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Usluga1Record retValue = new Usluga1Record();
            retValue.Famil = this.Famil;
            retValue.Imja = this.Imja;
            retValue.Otch = this.Otch;
            retValue.Drog = this.Drog;
            retValue.Posel = this.Posel;
            retValue.Nasp = this.Nasp;
            retValue.Ylic = this.Ylic;
            retValue.Ndom = this.Ndom;
            retValue.Nkorp = this.Nkorp;
            retValue.Nkw = this.Nkw;
            retValue.Nkomn = this.Nkomn;
            retValue.Lchard = this.Lchard;
            retValue.Ilchet = this.Ilchet;
            retValue.Vidgf = this.Vidgf;
            retValue.Lift = this.Lift;
            retValue.Opl = this.Opl;
            retValue.Otpl = this.Otpl;
            retValue.Kolzr = this.Kolzr;
            retValue.Gku = this.Gku;
            retValue.Org = this.Org;
            retValue.Vidtar = this.Vidtar;
            retValue.Koef = this.Koef;
            retValue.Tarif = this.Tarif;
            retValue.Fakt = this.Fakt;
            retValue.Sumtar = this.Sumtar;
            retValue.Sumdolg = this.Sumdolg;
            retValue.Opldolg = this.Opldolg;
            retValue.Datdolg = this.Datdolg;
            retValue.Koldolg = this.Koldolg;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO USLUGA1 (FAMIL, IMJA, OTCH, DROG, POSEL, NASP, YLIC, NDOM, NKORP, NKW, NKOMN, LCHARD, ILCHET, VIDGF, LIFT, OPL, OTPL, KOLZR, GKU, ORG, VIDTAR, KOEF, TARIF, FAKT, SUMTAR, SUMDOLG, OPLDOLG, DATDOLG, KOLDOLG) VALUES ('{0}', '{1}', '{2}', CTOD('{3}'), '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', {15}, {16}, {17}, '{18}', '{19}', {20}, {21}, {22}, {23}, {24}, {25}, {26}, CTOD('{27}'), {28})", String.IsNullOrEmpty(Famil) ? "" : Famil.Trim(), String.IsNullOrEmpty(Imja) ? "" : Imja.Trim(), String.IsNullOrEmpty(Otch) ? "" : Otch.Trim(), Drog == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Drog.Month, Drog.Day, Drog.Year), String.IsNullOrEmpty(Posel) ? "" : Posel.Trim(), String.IsNullOrEmpty(Nasp) ? "" : Nasp.Trim(), String.IsNullOrEmpty(Ylic) ? "" : Ylic.Trim(), String.IsNullOrEmpty(Ndom) ? "" : Ndom.Trim(), String.IsNullOrEmpty(Nkorp) ? "" : Nkorp.Trim(), String.IsNullOrEmpty(Nkw) ? "" : Nkw.Trim(), String.IsNullOrEmpty(Nkomn) ? "" : Nkomn.Trim(), String.IsNullOrEmpty(Lchard) ? "" : Lchard.Trim(), String.IsNullOrEmpty(Ilchet) ? "" : Ilchet.Trim(), String.IsNullOrEmpty(Vidgf) ? "" : Vidgf.Trim(), String.IsNullOrEmpty(Lift) ? "" : Lift.Trim(), Opl.ToString().Replace(',','.'), Otpl.ToString().Replace(',','.'), Kolzr.ToString(), String.IsNullOrEmpty(Gku) ? "" : Gku.Trim(), String.IsNullOrEmpty(Org) ? "" : Org.Trim(), Vidtar.ToString(), Koef.ToString(), Tarif.ToString().Replace(',','.'), Fakt.ToString().Replace(',','.'), Sumtar.ToString().Replace(',','.'), Sumdolg.ToString().Replace(',','.'), Opldolg.ToString().Replace(',','.'), Datdolg == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdolg.Month, Datdolg.Day, Datdolg.Year), Koldolg.ToString());
            return rs;
        }
    }
}
	