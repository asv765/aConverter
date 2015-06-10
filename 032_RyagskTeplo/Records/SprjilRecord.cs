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
    [TableName("SPRJIL.DBF")]
    public partial class SprjilRecord: AbstractRecord
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

        private string idf;
        // <summary>
        // IDF C(10)
        // </summary>
        [FieldName("IDF"), FieldType('C'), FieldWidth(10)]
        public string Idf
        {
            get { return idf; }
            set { CheckStringData("Idf", value, 10); idf = value; }
        }

        private string fam;
        // <summary>
        // FAM C(20)
        // </summary>
        [FieldName("FAM"), FieldType('C'), FieldWidth(20)]
        public string Fam
        {
            get { return fam; }
            set { CheckStringData("Fam", value, 20); fam = value; }
        }

        private string imia;
        // <summary>
        // IMIA C(20)
        // </summary>
        [FieldName("IMIA"), FieldType('C'), FieldWidth(20)]
        public string Imia
        {
            get { return imia; }
            set { CheckStringData("Imia", value, 20); imia = value; }
        }

        private string otch;
        // <summary>
        // OTCH C(20)
        // </summary>
        [FieldName("OTCH"), FieldType('C'), FieldWidth(20)]
        public string Otch
        {
            get { return otch; }
            set { CheckStringData("Otch", value, 20); otch = value; }
        }

        private DateTime dprop;
        // <summary>
        // DPROP D(8)
        // </summary>
        [FieldName("DPROP"), FieldType('D'), FieldWidth(8)]
        public DateTime Dprop
        {
            get { return dprop; }
            set {  dprop = value; }
        }

        private DateTime dvip;
        // <summary>
        // DVIP D(8)
        // </summary>
        [FieldName("DVIP"), FieldType('D'), FieldWidth(8)]
        public DateTime Dvip
        {
            get { return dvip; }
            set {  dvip = value; }
        }

        private DateTime d_ub;
        // <summary>
        // D_UB D(8)
        // </summary>
        [FieldName("D_UB"), FieldType('D'), FieldWidth(8)]
        public DateTime D_ub
        {
            get { return d_ub; }
            set {  d_ub = value; }
        }

        private DateTime d_pr;
        // <summary>
        // D_PR D(8)
        // </summary>
        [FieldName("D_PR"), FieldType('D'), FieldWidth(8)]
        public DateTime D_pr
        {
            get { return d_pr; }
            set {  d_pr = value; }
        }

        private Int64 pr_ub;
        // <summary>
        // PR_UB N(4)
        // </summary>
        [FieldName("PR_UB"), FieldType('N'), FieldWidth(4)]
        public Int64 Pr_ub
        {
            get { return pr_ub; }
            set { CheckIntegerData("Pr_ub", value, 4); pr_ub = value; }
        }

        private DateTime droj;
        // <summary>
        // DROJ D(8)
        // </summary>
        [FieldName("DROJ"), FieldType('D'), FieldWidth(8)]
        public DateTime Droj
        {
            get { return droj; }
            set {  droj = value; }
        }

        private string mroj;
        // <summary>
        // MROJ C(45)
        // </summary>
        [FieldName("MROJ"), FieldType('C'), FieldWidth(45)]
        public string Mroj
        {
            get { return mroj; }
            set { CheckStringData("Mroj", value, 45); mroj = value; }
        }

        private string serp;
        // <summary>
        // SERP C(6)
        // </summary>
        [FieldName("SERP"), FieldType('C'), FieldWidth(6)]
        public string Serp
        {
            get { return serp; }
            set { CheckStringData("Serp", value, 6); serp = value; }
        }

        private string nomp;
        // <summary>
        // NOMP C(8)
        // </summary>
        [FieldName("NOMP"), FieldType('C'), FieldWidth(8)]
        public string Nomp
        {
            get { return nomp; }
            set { CheckStringData("Nomp", value, 8); nomp = value; }
        }

        private DateTime dvid;
        // <summary>
        // DVID D(8)
        // </summary>
        [FieldName("DVID"), FieldType('D'), FieldWidth(8)]
        public DateTime Dvid
        {
            get { return dvid; }
            set {  dvid = value; }
        }

        private string hvid;
        // <summary>
        // HVID C(45)
        // </summary>
        [FieldName("HVID"), FieldType('C'), FieldWidth(45)]
        public string Hvid
        {
            get { return hvid; }
            set { CheckStringData("Hvid", value, 45); hvid = value; }
        }

        private Int64 lgj;
        // <summary>
        // LGJ N(4)
        // </summary>
        [FieldName("LGJ"), FieldType('N'), FieldWidth(4)]
        public Int64 Lgj
        {
            get { return lgj; }
            set { CheckIntegerData("Lgj", value, 4); lgj = value; }
        }

        private DateTime dendl;
        // <summary>
        // DENDL D(8)
        // </summary>
        [FieldName("DENDL"), FieldType('D'), FieldWidth(8)]
        public DateTime Dendl
        {
            get { return dendl; }
            set {  dendl = value; }
        }

        private Int64 pol;
        // <summary>
        // POL N(2)
        // </summary>
        [FieldName("POL"), FieldType('N'), FieldWidth(2)]
        public Int64 Pol
        {
            get { return pol; }
            set { CheckIntegerData("Pol", value, 2); pol = value; }
        }

        private string nomdoc;
        // <summary>
        // NOMDOC C(10)
        // </summary>
        [FieldName("NOMDOC"), FieldType('C'), FieldWidth(10)]
        public string Nomdoc
        {
            get { return nomdoc; }
            set { CheckStringData("Nomdoc", value, 10); nomdoc = value; }
        }

        private DateTime datdoc;
        // <summary>
        // DATDOC D(8)
        // </summary>
        [FieldName("DATDOC"), FieldType('D'), FieldWidth(8)]
        public DateTime Datdoc
        {
            get { return datdoc; }
            set {  datdoc = value; }
        }

        private bool neprop;
        // <summary>
        // NEPROP L(1)
        // </summary>
        [FieldName("NEPROP"), FieldType('L'), FieldWidth(1)]
        public bool Neprop
        {
            get { return neprop; }
            set {  neprop = value; }
        }

        private string mrab;
        // <summary>
        // MRAB C(45)
        // </summary>
        [FieldName("MRAB"), FieldType('C'), FieldWidth(45)]
        public string Mrab
        {
            get { return mrab; }
            set { CheckStringData("Mrab", value, 45); mrab = value; }
        }

        private bool voen;
        // <summary>
        // VOEN L(1)
        // </summary>
        [FieldName("VOEN"), FieldType('L'), FieldWidth(1)]
        public bool Voen
        {
            get { return voen; }
            set {  voen = value; }
        }

        private Int64 nac;
        // <summary>
        // NAC N(4)
        // </summary>
        [FieldName("NAC"), FieldType('N'), FieldWidth(4)]
        public Int64 Nac
        {
            get { return nac; }
            set { CheckIntegerData("Nac", value, 4); nac = value; }
        }

        private Int64 rodo;
        // <summary>
        // RODO N(4)
        // </summary>
        [FieldName("RODO"), FieldType('N'), FieldWidth(4)]
        public Int64 Rodo
        {
            get { return rodo; }
            set { CheckIntegerData("Rodo", value, 4); rodo = value; }
        }

        private string dolya;
        // <summary>
        // DOLYA C(10)
        // </summary>
        [FieldName("DOLYA"), FieldType('C'), FieldWidth(10)]
        public string Dolya
        {
            get { return dolya; }
            set { CheckStringData("Dolya", value, 10); dolya = value; }
        }

        private string info;
        // <summary>
        // INFO C(50)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(50)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 50); info = value; }
        }

        private DateTime datin;
        // <summary>
        // DATIN D(8)
        // </summary>
        [FieldName("DATIN"), FieldType('D'), FieldWidth(8)]
        public DateTime Datin
        {
            get { return datin; }
            set {  datin = value; }
        }

        private Int64 nomj;
        // <summary>
        // NOMJ N(3)
        // </summary>
        [FieldName("NOMJ"), FieldType('N'), FieldWidth(3)]
        public Int64 Nomj
        {
            get { return nomj; }
            set { CheckIntegerData("Nomj", value, 3); nomj = value; }
        }

        private string mr_resp;
        // <summary>
        // MR_RESP C(50)
        // </summary>
        [FieldName("MR_RESP"), FieldType('C'), FieldWidth(50)]
        public string Mr_resp
        {
            get { return mr_resp; }
            set { CheckStringData("Mr_resp", value, 50); mr_resp = value; }
        }

        private string mr_rayon;
        // <summary>
        // MR_RAYON C(50)
        // </summary>
        [FieldName("MR_RAYON"), FieldType('C'), FieldWidth(50)]
        public string Mr_rayon
        {
            get { return mr_rayon; }
            set { CheckStringData("Mr_rayon", value, 50); mr_rayon = value; }
        }

        private string mr_city;
        // <summary>
        // MR_CITY C(50)
        // </summary>
        [FieldName("MR_CITY"), FieldType('C'), FieldWidth(50)]
        public string Mr_city
        {
            get { return mr_city; }
            set { CheckStringData("Mr_city", value, 50); mr_city = value; }
        }

        private string mr_country;
        // <summary>
        // MR_COUNTRY C(50)
        // </summary>
        [FieldName("MR_COUNTRY"), FieldType('C'), FieldWidth(50)]
        public string Mr_country
        {
            get { return mr_country; }
            set { CheckStringData("Mr_country", value, 50); mr_country = value; }
        }

        private Int64 viddoc;
        // <summary>
        // VIDDOC N(4)
        // </summary>
        [FieldName("VIDDOC"), FieldType('N'), FieldWidth(4)]
        public Int64 Viddoc
        {
            get { return viddoc; }
            set { CheckIntegerData("Viddoc", value, 4); viddoc = value; }
        }

        private string codpodr;
        // <summary>
        // CODPODR C(10)
        // </summary>
        [FieldName("CODPODR"), FieldType('C'), FieldWidth(10)]
        public string Codpodr
        {
            get { return codpodr; }
            set { CheckStringData("Codpodr", value, 10); codpodr = value; }
        }

        private Int64 citizen;
        // <summary>
        // CITIZEN N(4)
        // </summary>
        [FieldName("CITIZEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Citizen
        {
            get { return citizen; }
            set { CheckIntegerData("Citizen", value, 4); citizen = value; }
        }

        private string pr_resp;
        // <summary>
        // PR_RESP C(50)
        // </summary>
        [FieldName("PR_RESP"), FieldType('C'), FieldWidth(50)]
        public string Pr_resp
        {
            get { return pr_resp; }
            set { CheckStringData("Pr_resp", value, 50); pr_resp = value; }
        }

        private string pr_rayon;
        // <summary>
        // PR_RAYON C(50)
        // </summary>
        [FieldName("PR_RAYON"), FieldType('C'), FieldWidth(50)]
        public string Pr_rayon
        {
            get { return pr_rayon; }
            set { CheckStringData("Pr_rayon", value, 50); pr_rayon = value; }
        }

        private string pr_city;
        // <summary>
        // PR_CITY C(50)
        // </summary>
        [FieldName("PR_CITY"), FieldType('C'), FieldWidth(50)]
        public string Pr_city
        {
            get { return pr_city; }
            set { CheckStringData("Pr_city", value, 50); pr_city = value; }
        }

        private string pr_country;
        // <summary>
        // PR_COUNTRY C(50)
        // </summary>
        [FieldName("PR_COUNTRY"), FieldType('C'), FieldWidth(50)]
        public string Pr_country
        {
            get { return pr_country; }
            set { CheckStringData("Pr_country", value, 50); pr_country = value; }
        }

        private string mleave;
        // <summary>
        // MLEAVE C(50)
        // </summary>
        [FieldName("MLEAVE"), FieldType('C'), FieldWidth(50)]
        public string Mleave
        {
            get { return mleave; }
            set { CheckStringData("Mleave", value, 50); mleave = value; }
        }

        private string label;
        // <summary>
        // LABEL C(20)
        // </summary>
        [FieldName("LABEL"), FieldType('C'), FieldWidth(20)]
        public string Label
        {
            get { return label; }
            set { CheckStringData("Label", value, 20); label = value; }
        }

        private string country;
        // <summary>
        // COUNTRY C(20)
        // </summary>
        [FieldName("COUNTRY"), FieldType('C'), FieldWidth(20)]
        public string Country
        {
            get { return country; }
            set { CheckStringData("Country", value, 20); country = value; }
        }

        private bool vremreg;
        // <summary>
        // VREMREG L(1)
        // </summary>
        [FieldName("VREMREG"), FieldType('L'), FieldWidth(1)]
        public bool Vremreg
        {
            get { return vremreg; }
            set {  vremreg = value; }
        }

        private string mvibil;
        // <summary>
        // MVIBIL C(50)
        // </summary>
        [FieldName("MVIBIL"), FieldType('C'), FieldWidth(50)]
        public string Mvibil
        {
            get { return mvibil; }
            set { CheckStringData("Mvibil", value, 50); mvibil = value; }
        }

        private string mv_resp;
        // <summary>
        // MV_RESP C(50)
        // </summary>
        [FieldName("MV_RESP"), FieldType('C'), FieldWidth(50)]
        public string Mv_resp
        {
            get { return mv_resp; }
            set { CheckStringData("Mv_resp", value, 50); mv_resp = value; }
        }

        private string mv_rayon;
        // <summary>
        // MV_RAYON C(50)
        // </summary>
        [FieldName("MV_RAYON"), FieldType('C'), FieldWidth(50)]
        public string Mv_rayon
        {
            get { return mv_rayon; }
            set { CheckStringData("Mv_rayon", value, 50); mv_rayon = value; }
        }

        private string mv_city;
        // <summary>
        // MV_CITY C(50)
        // </summary>
        [FieldName("MV_CITY"), FieldType('C'), FieldWidth(50)]
        public string Mv_city
        {
            get { return mv_city; }
            set { CheckStringData("Mv_city", value, 50); mv_city = value; }
        }

        private string mv_country;
        // <summary>
        // MV_COUNTRY C(50)
        // </summary>
        [FieldName("MV_COUNTRY"), FieldType('C'), FieldWidth(50)]
        public string Mv_country
        {
            get { return mv_country; }
            set { CheckStringData("Mv_country", value, 50); mv_country = value; }
        }

        private string mv_street;
        // <summary>
        // MV_STREET C(30)
        // </summary>
        [FieldName("MV_STREET"), FieldType('C'), FieldWidth(30)]
        public string Mv_street
        {
            get { return mv_street; }
            set { CheckStringData("Mv_street", value, 30); mv_street = value; }
        }

        private string mv_house;
        // <summary>
        // MV_HOUSE C(10)
        // </summary>
        [FieldName("MV_HOUSE"), FieldType('C'), FieldWidth(10)]
        public string Mv_house
        {
            get { return mv_house; }
            set { CheckStringData("Mv_house", value, 10); mv_house = value; }
        }

        private string mv_flat;
        // <summary>
        // MV_FLAT C(10)
        // </summary>
        [FieldName("MV_FLAT"), FieldType('C'), FieldWidth(10)]
        public string Mv_flat
        {
            get { return mv_flat; }
            set { CheckStringData("Mv_flat", value, 10); mv_flat = value; }
        }

        private string mv_build;
        // <summary>
        // MV_BUILD C(10)
        // </summary>
        [FieldName("MV_BUILD"), FieldType('C'), FieldWidth(10)]
        public string Mv_build
        {
            get { return mv_build; }
            set { CheckStringData("Mv_build", value, 10); mv_build = value; }
        }

        private string pr_street;
        // <summary>
        // PR_STREET C(30)
        // </summary>
        [FieldName("PR_STREET"), FieldType('C'), FieldWidth(30)]
        public string Pr_street
        {
            get { return pr_street; }
            set { CheckStringData("Pr_street", value, 30); pr_street = value; }
        }

        private string pr_house;
        // <summary>
        // PR_HOUSE C(10)
        // </summary>
        [FieldName("PR_HOUSE"), FieldType('C'), FieldWidth(10)]
        public string Pr_house
        {
            get { return pr_house; }
            set { CheckStringData("Pr_house", value, 10); pr_house = value; }
        }

        private string pr_build;
        // <summary>
        // PR_BUILD C(10)
        // </summary>
        [FieldName("PR_BUILD"), FieldType('C'), FieldWidth(10)]
        public string Pr_build
        {
            get { return pr_build; }
            set { CheckStringData("Pr_build", value, 10); pr_build = value; }
        }

        private string pr_flat;
        // <summary>
        // PR_FLAT C(10)
        // </summary>
        [FieldName("PR_FLAT"), FieldType('C'), FieldWidth(10)]
        public string Pr_flat
        {
            get { return pr_flat; }
            set { CheckStringData("Pr_flat", value, 10); pr_flat = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("IDF")) Idf = ADataRow["IDF"].ToString(); else Idf = "";
            if (ADataRow.Table.Columns.Contains("FAM")) Fam = ADataRow["FAM"].ToString(); else Fam = "";
            if (ADataRow.Table.Columns.Contains("IMIA")) Imia = ADataRow["IMIA"].ToString(); else Imia = "";
            if (ADataRow.Table.Columns.Contains("OTCH")) Otch = ADataRow["OTCH"].ToString(); else Otch = "";
            if (ADataRow.Table.Columns.Contains("DPROP")) Dprop = Convert.ToDateTime(ADataRow["DPROP"]); else Dprop = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DVIP")) Dvip = Convert.ToDateTime(ADataRow["DVIP"]); else Dvip = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("D_UB")) D_ub = Convert.ToDateTime(ADataRow["D_UB"]); else D_ub = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("D_PR")) D_pr = Convert.ToDateTime(ADataRow["D_PR"]); else D_pr = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("PR_UB")) Pr_ub = Convert.ToInt64(ADataRow["PR_UB"]); else Pr_ub = 0;
            if (ADataRow.Table.Columns.Contains("DROJ")) Droj = Convert.ToDateTime(ADataRow["DROJ"]); else Droj = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("MROJ")) Mroj = ADataRow["MROJ"].ToString(); else Mroj = "";
            if (ADataRow.Table.Columns.Contains("SERP")) Serp = ADataRow["SERP"].ToString(); else Serp = "";
            if (ADataRow.Table.Columns.Contains("NOMP")) Nomp = ADataRow["NOMP"].ToString(); else Nomp = "";
            if (ADataRow.Table.Columns.Contains("DVID")) Dvid = Convert.ToDateTime(ADataRow["DVID"]); else Dvid = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("HVID")) Hvid = ADataRow["HVID"].ToString(); else Hvid = "";
            if (ADataRow.Table.Columns.Contains("LGJ")) Lgj = Convert.ToInt64(ADataRow["LGJ"]); else Lgj = 0;
            if (ADataRow.Table.Columns.Contains("DENDL")) Dendl = Convert.ToDateTime(ADataRow["DENDL"]); else Dendl = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("POL")) Pol = Convert.ToInt64(ADataRow["POL"]); else Pol = 0;
            if (ADataRow.Table.Columns.Contains("NOMDOC")) Nomdoc = ADataRow["NOMDOC"].ToString(); else Nomdoc = "";
            if (ADataRow.Table.Columns.Contains("DATDOC")) Datdoc = Convert.ToDateTime(ADataRow["DATDOC"]); else Datdoc = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NEPROP")) Neprop = ADataRow["NEPROP"].ToString() == "True" ? true : false; else Neprop = false;
            if (ADataRow.Table.Columns.Contains("MRAB")) Mrab = ADataRow["MRAB"].ToString(); else Mrab = "";
            if (ADataRow.Table.Columns.Contains("VOEN")) Voen = ADataRow["VOEN"].ToString() == "True" ? true : false; else Voen = false;
            if (ADataRow.Table.Columns.Contains("NAC")) Nac = Convert.ToInt64(ADataRow["NAC"]); else Nac = 0;
            if (ADataRow.Table.Columns.Contains("RODO")) Rodo = Convert.ToInt64(ADataRow["RODO"]); else Rodo = 0;
            if (ADataRow.Table.Columns.Contains("DOLYA")) Dolya = ADataRow["DOLYA"].ToString(); else Dolya = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("DATIN")) Datin = Convert.ToDateTime(ADataRow["DATIN"]); else Datin = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NOMJ")) Nomj = Convert.ToInt64(ADataRow["NOMJ"]); else Nomj = 0;
            if (ADataRow.Table.Columns.Contains("MR_RESP")) Mr_resp = ADataRow["MR_RESP"].ToString(); else Mr_resp = "";
            if (ADataRow.Table.Columns.Contains("MR_RAYON")) Mr_rayon = ADataRow["MR_RAYON"].ToString(); else Mr_rayon = "";
            if (ADataRow.Table.Columns.Contains("MR_CITY")) Mr_city = ADataRow["MR_CITY"].ToString(); else Mr_city = "";
            if (ADataRow.Table.Columns.Contains("MR_COUNTRY")) Mr_country = ADataRow["MR_COUNTRY"].ToString(); else Mr_country = "";
            if (ADataRow.Table.Columns.Contains("VIDDOC")) Viddoc = Convert.ToInt64(ADataRow["VIDDOC"]); else Viddoc = 0;
            if (ADataRow.Table.Columns.Contains("CODPODR")) Codpodr = ADataRow["CODPODR"].ToString(); else Codpodr = "";
            if (ADataRow.Table.Columns.Contains("CITIZEN")) Citizen = Convert.ToInt64(ADataRow["CITIZEN"]); else Citizen = 0;
            if (ADataRow.Table.Columns.Contains("PR_RESP")) Pr_resp = ADataRow["PR_RESP"].ToString(); else Pr_resp = "";
            if (ADataRow.Table.Columns.Contains("PR_RAYON")) Pr_rayon = ADataRow["PR_RAYON"].ToString(); else Pr_rayon = "";
            if (ADataRow.Table.Columns.Contains("PR_CITY")) Pr_city = ADataRow["PR_CITY"].ToString(); else Pr_city = "";
            if (ADataRow.Table.Columns.Contains("PR_COUNTRY")) Pr_country = ADataRow["PR_COUNTRY"].ToString(); else Pr_country = "";
            if (ADataRow.Table.Columns.Contains("MLEAVE")) Mleave = ADataRow["MLEAVE"].ToString(); else Mleave = "";
            if (ADataRow.Table.Columns.Contains("LABEL")) Label = ADataRow["LABEL"].ToString(); else Label = "";
            if (ADataRow.Table.Columns.Contains("COUNTRY")) Country = ADataRow["COUNTRY"].ToString(); else Country = "";
            if (ADataRow.Table.Columns.Contains("VREMREG")) Vremreg = ADataRow["VREMREG"].ToString() == "True" ? true : false; else Vremreg = false;
            if (ADataRow.Table.Columns.Contains("MVIBIL")) Mvibil = ADataRow["MVIBIL"].ToString(); else Mvibil = "";
            if (ADataRow.Table.Columns.Contains("MV_RESP")) Mv_resp = ADataRow["MV_RESP"].ToString(); else Mv_resp = "";
            if (ADataRow.Table.Columns.Contains("MV_RAYON")) Mv_rayon = ADataRow["MV_RAYON"].ToString(); else Mv_rayon = "";
            if (ADataRow.Table.Columns.Contains("MV_CITY")) Mv_city = ADataRow["MV_CITY"].ToString(); else Mv_city = "";
            if (ADataRow.Table.Columns.Contains("MV_COUNTRY")) Mv_country = ADataRow["MV_COUNTRY"].ToString(); else Mv_country = "";
            if (ADataRow.Table.Columns.Contains("MV_STREET")) Mv_street = ADataRow["MV_STREET"].ToString(); else Mv_street = "";
            if (ADataRow.Table.Columns.Contains("MV_HOUSE")) Mv_house = ADataRow["MV_HOUSE"].ToString(); else Mv_house = "";
            if (ADataRow.Table.Columns.Contains("MV_FLAT")) Mv_flat = ADataRow["MV_FLAT"].ToString(); else Mv_flat = "";
            if (ADataRow.Table.Columns.Contains("MV_BUILD")) Mv_build = ADataRow["MV_BUILD"].ToString(); else Mv_build = "";
            if (ADataRow.Table.Columns.Contains("PR_STREET")) Pr_street = ADataRow["PR_STREET"].ToString(); else Pr_street = "";
            if (ADataRow.Table.Columns.Contains("PR_HOUSE")) Pr_house = ADataRow["PR_HOUSE"].ToString(); else Pr_house = "";
            if (ADataRow.Table.Columns.Contains("PR_BUILD")) Pr_build = ADataRow["PR_BUILD"].ToString(); else Pr_build = "";
            if (ADataRow.Table.Columns.Contains("PR_FLAT")) Pr_flat = ADataRow["PR_FLAT"].ToString(); else Pr_flat = "";
        }
        
        public override AbstractRecord Clone()
        {
            SprjilRecord retValue = new SprjilRecord();
            retValue.Ls = this.Ls;
            retValue.Idf = this.Idf;
            retValue.Fam = this.Fam;
            retValue.Imia = this.Imia;
            retValue.Otch = this.Otch;
            retValue.Dprop = this.Dprop;
            retValue.Dvip = this.Dvip;
            retValue.D_ub = this.D_ub;
            retValue.D_pr = this.D_pr;
            retValue.Pr_ub = this.Pr_ub;
            retValue.Droj = this.Droj;
            retValue.Mroj = this.Mroj;
            retValue.Serp = this.Serp;
            retValue.Nomp = this.Nomp;
            retValue.Dvid = this.Dvid;
            retValue.Hvid = this.Hvid;
            retValue.Lgj = this.Lgj;
            retValue.Dendl = this.Dendl;
            retValue.Pol = this.Pol;
            retValue.Nomdoc = this.Nomdoc;
            retValue.Datdoc = this.Datdoc;
            retValue.Neprop = this.Neprop;
            retValue.Mrab = this.Mrab;
            retValue.Voen = this.Voen;
            retValue.Nac = this.Nac;
            retValue.Rodo = this.Rodo;
            retValue.Dolya = this.Dolya;
            retValue.Info = this.Info;
            retValue.Datin = this.Datin;
            retValue.Nomj = this.Nomj;
            retValue.Mr_resp = this.Mr_resp;
            retValue.Mr_rayon = this.Mr_rayon;
            retValue.Mr_city = this.Mr_city;
            retValue.Mr_country = this.Mr_country;
            retValue.Viddoc = this.Viddoc;
            retValue.Codpodr = this.Codpodr;
            retValue.Citizen = this.Citizen;
            retValue.Pr_resp = this.Pr_resp;
            retValue.Pr_rayon = this.Pr_rayon;
            retValue.Pr_city = this.Pr_city;
            retValue.Pr_country = this.Pr_country;
            retValue.Mleave = this.Mleave;
            retValue.Label = this.Label;
            retValue.Country = this.Country;
            retValue.Vremreg = this.Vremreg;
            retValue.Mvibil = this.Mvibil;
            retValue.Mv_resp = this.Mv_resp;
            retValue.Mv_rayon = this.Mv_rayon;
            retValue.Mv_city = this.Mv_city;
            retValue.Mv_country = this.Mv_country;
            retValue.Mv_street = this.Mv_street;
            retValue.Mv_house = this.Mv_house;
            retValue.Mv_flat = this.Mv_flat;
            retValue.Mv_build = this.Mv_build;
            retValue.Pr_street = this.Pr_street;
            retValue.Pr_house = this.Pr_house;
            retValue.Pr_build = this.Pr_build;
            retValue.Pr_flat = this.Pr_flat;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPRJIL (LS, IDF, FAM, IMIA, OTCH, DPROP, DVIP, D_UB, D_PR, PR_UB, DROJ, MROJ, SERP, NOMP, DVID, HVID, LGJ, DENDL, POL, NOMDOC, DATDOC, NEPROP, MRAB, VOEN, NAC, RODO, DOLYA, INFO, DATIN, NOMJ, MR_RESP, MR_RAYON, MR_CITY, MR_COUNTRY, VIDDOC, CODPODR, CITIZEN, PR_RESP, PR_RAYON, PR_CITY, PR_COUNTRY, MLEAVE, LABEL, COUNTRY, VREMREG, MVIBIL, MV_RESP, MV_RAYON, MV_CITY, MV_COUNTRY, MV_STREET, MV_HOUSE, MV_FLAT, MV_BUILD, PR_STREET, PR_HOUSE, PR_BUILD, PR_FLAT) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', CTOD('{5}'), CTOD('{6}'), CTOD('{7}'), CTOD('{8}'), {9}, CTOD('{10}'), '{11}', '{12}', '{13}', CTOD('{14}'), '{15}', {16}, CTOD('{17}'), {18}, '{19}', CTOD('{20}'), {21}, '{22}', {23}, {24}, {25}, '{26}', '{27}', CTOD('{28}'), {29}, '{30}', '{31}', '{32}', '{33}', {34}, '{35}', {36}, '{37}', '{38}', '{39}', '{40}', '{41}', '{42}', '{43}', {44}, '{45}', '{46}', '{47}', '{48}', '{49}', '{50}', '{51}', '{52}', '{53}', '{54}', '{55}', '{56}', '{57}')", Ls.ToString(), String.IsNullOrEmpty(Idf) ? "" : Idf.Trim(), String.IsNullOrEmpty(Fam) ? "" : Fam.Trim(), String.IsNullOrEmpty(Imia) ? "" : Imia.Trim(), String.IsNullOrEmpty(Otch) ? "" : Otch.Trim(), Dprop == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dprop.Month, Dprop.Day, Dprop.Year), Dvip == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dvip.Month, Dvip.Day, Dvip.Year), D_ub == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", D_ub.Month, D_ub.Day, D_ub.Year), D_pr == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", D_pr.Month, D_pr.Day, D_pr.Year), Pr_ub.ToString(), Droj == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Droj.Month, Droj.Day, Droj.Year), String.IsNullOrEmpty(Mroj) ? "" : Mroj.Trim(), String.IsNullOrEmpty(Serp) ? "" : Serp.Trim(), String.IsNullOrEmpty(Nomp) ? "" : Nomp.Trim(), Dvid == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dvid.Month, Dvid.Day, Dvid.Year), String.IsNullOrEmpty(Hvid) ? "" : Hvid.Trim(), Lgj.ToString(), Dendl == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dendl.Month, Dendl.Day, Dendl.Year), Pol.ToString(), String.IsNullOrEmpty(Nomdoc) ? "" : Nomdoc.Trim(), Datdoc == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdoc.Month, Datdoc.Day, Datdoc.Year), (Neprop ? 0 : 1 ), String.IsNullOrEmpty(Mrab) ? "" : Mrab.Trim(), (Voen ? 0 : 1 ), Nac.ToString(), Rodo.ToString(), String.IsNullOrEmpty(Dolya) ? "" : Dolya.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), Datin == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datin.Month, Datin.Day, Datin.Year), Nomj.ToString(), String.IsNullOrEmpty(Mr_resp) ? "" : Mr_resp.Trim(), String.IsNullOrEmpty(Mr_rayon) ? "" : Mr_rayon.Trim(), String.IsNullOrEmpty(Mr_city) ? "" : Mr_city.Trim(), String.IsNullOrEmpty(Mr_country) ? "" : Mr_country.Trim(), Viddoc.ToString(), String.IsNullOrEmpty(Codpodr) ? "" : Codpodr.Trim(), Citizen.ToString(), String.IsNullOrEmpty(Pr_resp) ? "" : Pr_resp.Trim(), String.IsNullOrEmpty(Pr_rayon) ? "" : Pr_rayon.Trim(), String.IsNullOrEmpty(Pr_city) ? "" : Pr_city.Trim(), String.IsNullOrEmpty(Pr_country) ? "" : Pr_country.Trim(), String.IsNullOrEmpty(Mleave) ? "" : Mleave.Trim(), String.IsNullOrEmpty(Label) ? "" : Label.Trim(), String.IsNullOrEmpty(Country) ? "" : Country.Trim(), (Vremreg ? 0 : 1 ), String.IsNullOrEmpty(Mvibil) ? "" : Mvibil.Trim(), String.IsNullOrEmpty(Mv_resp) ? "" : Mv_resp.Trim(), String.IsNullOrEmpty(Mv_rayon) ? "" : Mv_rayon.Trim(), String.IsNullOrEmpty(Mv_city) ? "" : Mv_city.Trim(), String.IsNullOrEmpty(Mv_country) ? "" : Mv_country.Trim(), String.IsNullOrEmpty(Mv_street) ? "" : Mv_street.Trim(), String.IsNullOrEmpty(Mv_house) ? "" : Mv_house.Trim(), String.IsNullOrEmpty(Mv_flat) ? "" : Mv_flat.Trim(), String.IsNullOrEmpty(Mv_build) ? "" : Mv_build.Trim(), String.IsNullOrEmpty(Pr_street) ? "" : Pr_street.Trim(), String.IsNullOrEmpty(Pr_house) ? "" : Pr_house.Trim(), String.IsNullOrEmpty(Pr_build) ? "" : Pr_build.Trim(), String.IsNullOrEmpty(Pr_flat) ? "" : Pr_flat.Trim());
            return rs;
        }
    }
}
