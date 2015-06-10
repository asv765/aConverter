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
    [TableName("SPRRAB.DBF")]
    public partial class SprrabRecord: AbstractRecord
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

        private Int64 codf;
        // <summary>
        // CODF N(11)
        // </summary>
        [FieldName("CODF"), FieldType('N'), FieldWidth(11)]
        public Int64 Codf
        {
            get { return codf; }
            set { CheckIntegerData("Codf", value, 11); codf = value; }
        }

        private Int64 parent;
        // <summary>
        // PARENT N(11)
        // </summary>
        [FieldName("PARENT"), FieldType('N'), FieldWidth(11)]
        public Int64 Parent
        {
            get { return parent; }
            set { CheckIntegerData("Parent", value, 11); parent = value; }
        }

        private Int64 is_folder;
        // <summary>
        // IS_FOLDER N(2)
        // </summary>
        [FieldName("IS_FOLDER"), FieldType('N'), FieldWidth(2)]
        public Int64 Is_folder
        {
            get { return is_folder; }
            set { CheckIntegerData("Is_folder", value, 2); is_folder = value; }
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

        private Int64 addres;
        // <summary>
        // ADDRES N(6)
        // </summary>
        [FieldName("ADDRES"), FieldType('N'), FieldWidth(6)]
        public Int64 Addres
        {
            get { return addres; }
            set { CheckIntegerData("Addres", value, 6); addres = value; }
        }

        private Int64 kvar;
        // <summary>
        // KVAR N(6)
        // </summary>
        [FieldName("KVAR"), FieldType('N'), FieldWidth(6)]
        public Int64 Kvar
        {
            get { return kvar; }
            set { CheckIntegerData("Kvar", value, 6); kvar = value; }
        }

        private Int64 ls_uk;
        // <summary>
        // LS_UK N(7)
        // </summary>
        [FieldName("LS_UK"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls_uk
        {
            get { return ls_uk; }
            set { CheckIntegerData("Ls_uk", value, 7); ls_uk = value; }
        }

        private string kvar_a;
        // <summary>
        // KVAR_A C(5)
        // </summary>
        [FieldName("KVAR_A"), FieldType('C'), FieldWidth(5)]
        public string Kvar_a
        {
            get { return kvar_a; }
            set { CheckStringData("Kvar_a", value, 5); kvar_a = value; }
        }

        private Int64 tn;
        // <summary>
        // TN N(7)
        // </summary>
        [FieldName("TN"), FieldType('N'), FieldWidth(7)]
        public Int64 Tn
        {
            get { return tn; }
            set { CheckIntegerData("Tn", value, 7); tn = value; }
        }

        private decimal s2;
        // <summary>
        // S2 N(7,2)
        // </summary>
        [FieldName("S2"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S2
        {
            get { return s2; }
            set { CheckDecimalData("S2", value, 7, 2); s2 = value; }
        }

        private decimal s3;
        // <summary>
        // S3 N(7,2)
        // </summary>
        [FieldName("S3"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal S3
        {
            get { return s3; }
            set { CheckDecimalData("S3", value, 7, 2); s3 = value; }
        }

        private decimal dops2;
        // <summary>
        // DOPS2 N(6,2)
        // </summary>
        [FieldName("DOPS2"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Dops2
        {
            get { return dops2; }
            set { CheckDecimalData("Dops2", value, 6, 2); dops2 = value; }
        }

        private decimal sb1;
        // <summary>
        // SB1 N(7,2)
        // </summary>
        [FieldName("SB1"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Sb1
        {
            get { return sb1; }
            set { CheckDecimalData("Sb1", value, 7, 2); sb1 = value; }
        }

        private decimal sb2;
        // <summary>
        // SB2 N(6,2)
        // </summary>
        [FieldName("SB2"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Sb2
        {
            get { return sb2; }
            set { CheckDecimalData("Sb2", value, 6, 2); sb2 = value; }
        }

        private decimal sb3;
        // <summary>
        // SB3 N(7,2)
        // </summary>
        [FieldName("SB3"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Sb3
        {
            get { return sb3; }
            set { CheckDecimalData("Sb3", value, 7, 2); sb3 = value; }
        }

        private decimal s2otop;
        // <summary>
        // S2OTOP N(6,2)
        // </summary>
        [FieldName("S2OTOP"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal S2otop
        {
            get { return s2otop; }
            set { CheckDecimalData("S2otop", value, 6, 2); s2otop = value; }
        }

        private Int64 kol;
        // <summary>
        // KOL N(3)
        // </summary>
        [FieldName("KOL"), FieldType('N'), FieldWidth(3)]
        public Int64 Kol
        {
            get { return kol; }
            set { CheckIntegerData("Kol", value, 3); kol = value; }
        }

        private Int64 kolfact;
        // <summary>
        // KOLFACT N(3)
        // </summary>
        [FieldName("KOLFACT"), FieldType('N'), FieldWidth(3)]
        public Int64 Kolfact
        {
            get { return kolfact; }
            set { CheckIntegerData("Kolfact", value, 3); kolfact = value; }
        }

        private Int64 koll;
        // <summary>
        // KOLL N(3)
        // </summary>
        [FieldName("KOLL"), FieldType('N'), FieldWidth(3)]
        public Int64 Koll
        {
            get { return koll; }
            set { CheckIntegerData("Koll", value, 3); koll = value; }
        }

        private Int64 lg;
        // <summary>
        // LG N(4)
        // </summary>
        [FieldName("LG"), FieldType('N'), FieldWidth(4)]
        public Int64 Lg
        {
            get { return lg; }
            set { CheckIntegerData("Lg", value, 4); lg = value; }
        }

        private string uslugi;
        // <summary>
        // USLUGI C(120)
        // </summary>
        [FieldName("USLUGI"), FieldType('C'), FieldWidth(120)]
        public string Uslugi
        {
            get { return uslugi; }
            set { CheckStringData("Uslugi", value, 120); uslugi = value; }
        }

        private string tarifi;
        // <summary>
        // TARIFI C(60)
        // </summary>
        [FieldName("TARIFI"), FieldType('C'), FieldWidth(60)]
        public string Tarifi
        {
            get { return tarifi; }
            set { CheckStringData("Tarifi", value, 60); tarifi = value; }
        }

        private Int64 kolk;
        // <summary>
        // KOLK N(3)
        // </summary>
        [FieldName("KOLK"), FieldType('N'), FieldWidth(3)]
        public Int64 Kolk
        {
            get { return kolk; }
            set { CheckIntegerData("Kolk", value, 3); kolk = value; }
        }

        private bool prop;
        // <summary>
        // PROP L(1)
        // </summary>
        [FieldName("PROP"), FieldType('L'), FieldWidth(1)]
        public bool Prop
        {
            get { return prop; }
            set {  prop = value; }
        }

        private Int64 katk;
        // <summary>
        // KATK N(4)
        // </summary>
        [FieldName("KATK"), FieldType('N'), FieldWidth(4)]
        public Int64 Katk
        {
            get { return katk; }
            set { CheckIntegerData("Katk", value, 4); katk = value; }
        }

        private bool pr1;
        // <summary>
        // PR1 L(1)
        // </summary>
        [FieldName("PR1"), FieldType('L'), FieldWidth(1)]
        public bool Pr1
        {
            get { return pr1; }
            set {  pr1 = value; }
        }

        private bool c_gvs;
        // <summary>
        // C_GVS L(1)
        // </summary>
        [FieldName("C_GVS"), FieldType('L'), FieldWidth(1)]
        public bool C_gvs
        {
            get { return c_gvs; }
            set {  c_gvs = value; }
        }

        private bool notsj;
        // <summary>
        // NOTSJ L(1)
        // </summary>
        [FieldName("NOTSJ"), FieldType('L'), FieldWidth(1)]
        public bool Notsj
        {
            get { return notsj; }
            set {  notsj = value; }
        }

        private string ord;
        // <summary>
        // ORD C(10)
        // </summary>
        [FieldName("ORD"), FieldType('C'), FieldWidth(10)]
        public string Ord
        {
            get { return ord; }
            set { CheckStringData("Ord", value, 10); ord = value; }
        }

        private bool c_hvs;
        // <summary>
        // C_HVS L(1)
        // </summary>
        [FieldName("C_HVS"), FieldType('L'), FieldWidth(1)]
        public bool C_hvs
        {
            get { return c_hvs; }
            set {  c_hvs = value; }
        }

        private DateTime d_ord;
        // <summary>
        // D_ORD D(8)
        // </summary>
        [FieldName("D_ORD"), FieldType('D'), FieldWidth(8)]
        public DateTime D_ord
        {
            get { return d_ord; }
            set {  d_ord = value; }
        }

        private bool subsy;
        // <summary>
        // SUBSY L(1)
        // </summary>
        [FieldName("SUBSY"), FieldType('L'), FieldWidth(1)]
        public bool Subsy
        {
            get { return subsy; }
            set {  subsy = value; }
        }

        private decimal subsid;
        // <summary>
        // SUBSID N(10,2)
        // </summary>
        [FieldName("SUBSID"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Subsid
        {
            get { return subsid; }
            set { CheckDecimalData("Subsid", value, 10, 2); subsid = value; }
        }

        private Int64 etaj;
        // <summary>
        // ETAJ N(2)
        // </summary>
        [FieldName("ETAJ"), FieldType('N'), FieldWidth(2)]
        public Int64 Etaj
        {
            get { return etaj; }
            set { CheckIntegerData("Etaj", value, 2); etaj = value; }
        }

        private Int64 n_etaj;
        // <summary>
        // N_ETAJ N(3)
        // </summary>
        [FieldName("N_ETAJ"), FieldType('N'), FieldWidth(3)]
        public Int64 N_etaj
        {
            get { return n_etaj; }
            set { CheckIntegerData("N_etaj", value, 3); n_etaj = value; }
        }

        private decimal sotki;
        // <summary>
        // SOTKI N(10,2)
        // </summary>
        [FieldName("SOTKI"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sotki
        {
            get { return sotki; }
            set { CheckDecimalData("Sotki", value, 10, 2); sotki = value; }
        }

        private bool komm;
        // <summary>
        // KOMM L(1)
        // </summary>
        [FieldName("KOMM"), FieldType('L'), FieldWidth(1)]
        public bool Komm
        {
            get { return komm; }
            set {  komm = value; }
        }

        private DateTime datdog;
        // <summary>
        // DATDOG D(8)
        // </summary>
        [FieldName("DATDOG"), FieldType('D'), FieldWidth(8)]
        public DateTime Datdog
        {
            get { return datdog; }
            set {  datdog = value; }
        }

        private string nomdog;
        // <summary>
        // NOMDOG C(6)
        // </summary>
        [FieldName("NOMDOG"), FieldType('C'), FieldWidth(6)]
        public string Nomdog
        {
            get { return nomdog; }
            set { CheckStringData("Nomdog", value, 6); nomdog = value; }
        }

        private string isp;
        // <summary>
        // ISP C(20)
        // </summary>
        [FieldName("ISP"), FieldType('C'), FieldWidth(20)]
        public string Isp
        {
            get { return isp; }
            set { CheckStringData("Isp", value, 20); isp = value; }
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

        private string telef;
        // <summary>
        // TELEF C(30)
        // </summary>
        [FieldName("TELEF"), FieldType('C'), FieldWidth(30)]
        public string Telef
        {
            get { return telef; }
            set { CheckStringData("Telef", value, 30); telef = value; }
        }

        private string telsot;
        // <summary>
        // TELSOT C(30)
        // </summary>
        [FieldName("TELSOT"), FieldType('C'), FieldWidth(30)]
        public string Telsot
        {
            get { return telsot; }
            set { CheckStringData("Telsot", value, 30); telsot = value; }
        }

        private DateTime datnik;
        // <summary>
        // DATNIK D(8)
        // </summary>
        [FieldName("DATNIK"), FieldType('D'), FieldWidth(8)]
        public DateTime Datnik
        {
            get { return datnik; }
            set {  datnik = value; }
        }

        private decimal sumnik;
        // <summary>
        // SUMNIK N(10,2)
        // </summary>
        [FieldName("SUMNIK"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sumnik
        {
            get { return sumnik; }
            set { CheckDecimalData("Sumnik", value, 10, 2); sumnik = value; }
        }

        private Int64 mesnik;
        // <summary>
        // MESNIK N(3)
        // </summary>
        [FieldName("MESNIK"), FieldType('N'), FieldWidth(3)]
        public Int64 Mesnik
        {
            get { return mesnik; }
            set { CheckIntegerData("Mesnik", value, 3); mesnik = value; }
        }

        private decimal peni;
        // <summary>
        // PENI N(10,2)
        // </summary>
        [FieldName("PENI"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Peni
        {
            get { return peni; }
            set { CheckDecimalData("Peni", value, 10, 2); peni = value; }
        }

        private bool flagpeni;
        // <summary>
        // FLAGPENI L(1)
        // </summary>
        [FieldName("FLAGPENI"), FieldType('L'), FieldWidth(1)]
        public bool Flagpeni
        {
            get { return flagpeni; }
            set {  flagpeni = value; }
        }

        private decimal saldo1;
        // <summary>
        // SALDO1 N(10,2)
        // </summary>
        [FieldName("SALDO1"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo1
        {
            get { return saldo1; }
            set { CheckDecimalData("Saldo1", value, 10, 2); saldo1 = value; }
        }

        private Int64 ls1;
        // <summary>
        // LS1 N(7)
        // </summary>
        [FieldName("LS1"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls1
        {
            get { return ls1; }
            set { CheckIntegerData("Ls1", value, 7); ls1 = value; }
        }

        private string owner;
        // <summary>
        // OWNER C(30)
        // </summary>
        [FieldName("OWNER"), FieldType('C'), FieldWidth(30)]
        public string Owner
        {
            get { return owner; }
            set { CheckStringData("Owner", value, 30); owner = value; }
        }

        private decimal saldo61;
        // <summary>
        // SALDO61 N(10,2)
        // </summary>
        [FieldName("SALDO61"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo61
        {
            get { return saldo61; }
            set { CheckDecimalData("Saldo61", value, 10, 2); saldo61 = value; }
        }

        private Int64 katp;
        // <summary>
        // KATP N(3)
        // </summary>
        [FieldName("KATP"), FieldType('N'), FieldWidth(3)]
        public Int64 Katp
        {
            get { return katp; }
            set { CheckIntegerData("Katp", value, 3); katp = value; }
        }

        private decimal saldo62;
        // <summary>
        // SALDO62 N(10,2)
        // </summary>
        [FieldName("SALDO62"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo62
        {
            get { return saldo62; }
            set { CheckDecimalData("Saldo62", value, 10, 2); saldo62 = value; }
        }

        private DateTime datofsud;
        // <summary>
        // DATOFSUD D(8)
        // </summary>
        [FieldName("DATOFSUD"), FieldType('D'), FieldWidth(8)]
        public DateTime Datofsud
        {
            get { return datofsud; }
            set {  datofsud = value; }
        }

        private decimal sumsud;
        // <summary>
        // SUMSUD N(8,2)
        // </summary>
        [FieldName("SUMSUD"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Sumsud
        {
            get { return sumsud; }
            set { CheckDecimalData("Sumsud", value, 8, 2); sumsud = value; }
        }

        private decimal hvs_kub;
        // <summary>
        // HVS_KUB N(6,2)
        // </summary>
        [FieldName("HVS_KUB"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Hvs_kub
        {
            get { return hvs_kub; }
            set { CheckDecimalData("Hvs_kub", value, 6, 2); hvs_kub = value; }
        }

        private decimal kan_kub;
        // <summary>
        // KAN_KUB N(6,2)
        // </summary>
        [FieldName("KAN_KUB"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Kan_kub
        {
            get { return kan_kub; }
            set { CheckDecimalData("Kan_kub", value, 6, 2); kan_kub = value; }
        }

        private decimal gvs_kub;
        // <summary>
        // GVS_KUB N(6,2)
        // </summary>
        [FieldName("GVS_KUB"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Gvs_kub
        {
            get { return gvs_kub; }
            set { CheckDecimalData("Gvs_kub", value, 6, 2); gvs_kub = value; }
        }

        private decimal saldo2;
        // <summary>
        // SALDO2 N(10,2)
        // </summary>
        [FieldName("SALDO2"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo2
        {
            get { return saldo2; }
            set { CheckDecimalData("Saldo2", value, 10, 2); saldo2 = value; }
        }

        private decimal saldo3;
        // <summary>
        // SALDO3 N(10,2)
        // </summary>
        [FieldName("SALDO3"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo3
        {
            get { return saldo3; }
            set { CheckDecimalData("Saldo3", value, 10, 2); saldo3 = value; }
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

        private decimal saldo5;
        // <summary>
        // SALDO5 N(10,2)
        // </summary>
        [FieldName("SALDO5"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo5
        {
            get { return saldo5; }
            set { CheckDecimalData("Saldo5", value, 10, 2); saldo5 = value; }
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

        private decimal saldo8;
        // <summary>
        // SALDO8 N(10,2)
        // </summary>
        [FieldName("SALDO8"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo8
        {
            get { return saldo8; }
            set { CheckDecimalData("Saldo8", value, 10, 2); saldo8 = value; }
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

        private decimal saldo10;
        // <summary>
        // SALDO10 N(10,2)
        // </summary>
        [FieldName("SALDO10"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo10
        {
            get { return saldo10; }
            set { CheckDecimalData("Saldo10", value, 10, 2); saldo10 = value; }
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

        private decimal saldo12;
        // <summary>
        // SALDO12 N(10,2)
        // </summary>
        [FieldName("SALDO12"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo12
        {
            get { return saldo12; }
            set { CheckDecimalData("Saldo12", value, 10, 2); saldo12 = value; }
        }

        private decimal saldo13;
        // <summary>
        // SALDO13 N(10,2)
        // </summary>
        [FieldName("SALDO13"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo13
        {
            get { return saldo13; }
            set { CheckDecimalData("Saldo13", value, 10, 2); saldo13 = value; }
        }

        private decimal saldo14;
        // <summary>
        // SALDO14 N(10,2)
        // </summary>
        [FieldName("SALDO14"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo14
        {
            get { return saldo14; }
            set { CheckDecimalData("Saldo14", value, 10, 2); saldo14 = value; }
        }

        private decimal saldo17;
        // <summary>
        // SALDO17 N(10,2)
        // </summary>
        [FieldName("SALDO17"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo17
        {
            get { return saldo17; }
            set { CheckDecimalData("Saldo17", value, 10, 2); saldo17 = value; }
        }

        private decimal saldo18;
        // <summary>
        // SALDO18 N(10,2)
        // </summary>
        [FieldName("SALDO18"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo18
        {
            get { return saldo18; }
            set { CheckDecimalData("Saldo18", value, 10, 2); saldo18 = value; }
        }

        private decimal saldo19;
        // <summary>
        // SALDO19 N(10,2)
        // </summary>
        [FieldName("SALDO19"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo19
        {
            get { return saldo19; }
            set { CheckDecimalData("Saldo19", value, 10, 2); saldo19 = value; }
        }

        private decimal saldo34;
        // <summary>
        // SALDO34 N(10,2)
        // </summary>
        [FieldName("SALDO34"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo34
        {
            get { return saldo34; }
            set { CheckDecimalData("Saldo34", value, 10, 2); saldo34 = value; }
        }

        private decimal saldo35;
        // <summary>
        // SALDO35 N(10,2)
        // </summary>
        [FieldName("SALDO35"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Saldo35
        {
            get { return saldo35; }
            set { CheckDecimalData("Saldo35", value, 10, 2); saldo35 = value; }
        }

        private decimal saldo36;
        // <summary>
        // SALDO36 N(12,2)
        // </summary>
        [FieldName("SALDO36"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Saldo36
        {
            get { return saldo36; }
            set { CheckDecimalData("Saldo36", value, 12, 2); saldo36 = value; }
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

        private bool close;
        // <summary>
        // CLOSE L(1)
        // </summary>
        [FieldName("CLOSE"), FieldType('L'), FieldWidth(1)]
        public bool Close
        {
            get { return close; }
            set {  close = value; }
        }

        private DateTime datclose;
        // <summary>
        // DATCLOSE D(8)
        // </summary>
        [FieldName("DATCLOSE"), FieldType('D'), FieldWidth(8)]
        public DateTime Datclose
        {
            get { return datclose; }
            set {  datclose = value; }
        }

        private bool opl100;
        // <summary>
        // OPL100 L(1)
        // </summary>
        [FieldName("OPL100"), FieldType('L'), FieldWidth(1)]
        public bool Opl100
        {
            get { return opl100; }
            set {  opl100 = value; }
        }

        private bool fllgot;
        // <summary>
        // FLLGOT L(1)
        // </summary>
        [FieldName("FLLGOT"), FieldType('L'), FieldWidth(1)]
        public bool Fllgot
        {
            get { return fllgot; }
            set {  fllgot = value; }
        }

        private bool flold;
        // <summary>
        // FLOLD L(1)
        // </summary>
        [FieldName("FLOLD"), FieldType('L'), FieldWidth(1)]
        public bool Flold
        {
            get { return flold; }
            set {  flold = value; }
        }

        private bool flpens;
        // <summary>
        // FLPENS L(1)
        // </summary>
        [FieldName("FLPENS"), FieldType('L'), FieldWidth(1)]
        public bool Flpens
        {
            get { return flpens; }
            set {  flpens = value; }
        }

        private decimal domofon;
        // <summary>
        // DOMOFON N(10,2)
        // </summary>
        [FieldName("DOMOFON"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Domofon
        {
            get { return domofon; }
            set { CheckDecimalData("Domofon", value, 10, 2); domofon = value; }
        }

        private string sobs;
        // <summary>
        // SOBS C(30)
        // </summary>
        [FieldName("SOBS"), FieldType('C'), FieldWidth(30)]
        public string Sobs
        {
            get { return sobs; }
            set { CheckStringData("Sobs", value, 30); sobs = value; }
        }

        private string inn;
        // <summary>
        // INN C(12)
        // </summary>
        [FieldName("INN"), FieldType('C'), FieldWidth(12)]
        public string Inn
        {
            get { return inn; }
            set { CheckStringData("Inn", value, 12); inn = value; }
        }

        private string kpp;
        // <summary>
        // KPP C(20)
        // </summary>
        [FieldName("KPP"), FieldType('C'), FieldWidth(20)]
        public string Kpp
        {
            get { return kpp; }
            set { CheckStringData("Kpp", value, 20); kpp = value; }
        }

        private string bank;
        // <summary>
        // BANK C(30)
        // </summary>
        [FieldName("BANK"), FieldType('C'), FieldWidth(30)]
        public string Bank
        {
            get { return bank; }
            set { CheckStringData("Bank", value, 30); bank = value; }
        }

        private string ks;
        // <summary>
        // KS C(20)
        // </summary>
        [FieldName("KS"), FieldType('C'), FieldWidth(20)]
        public string Ks
        {
            get { return ks; }
            set { CheckStringData("Ks", value, 20); ks = value; }
        }

        private string bik;
        // <summary>
        // BIK C(12)
        // </summary>
        [FieldName("BIK"), FieldType('C'), FieldWidth(12)]
        public string Bik
        {
            get { return bik; }
            set { CheckStringData("Bik", value, 12); bik = value; }
        }

        private string rs;
        // <summary>
        // RS C(20)
        // </summary>
        [FieldName("RS"), FieldType('C'), FieldWidth(20)]
        public string Rs
        {
            get { return rs; }
            set { CheckStringData("Rs", value, 20); rs = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("CODF")) Codf = Convert.ToInt64(ADataRow["CODF"]); else Codf = 0;
            if (ADataRow.Table.Columns.Contains("PARENT")) Parent = Convert.ToInt64(ADataRow["PARENT"]); else Parent = 0;
            if (ADataRow.Table.Columns.Contains("IS_FOLDER")) Is_folder = Convert.ToInt64(ADataRow["IS_FOLDER"]); else Is_folder = 0;
            if (ADataRow.Table.Columns.Contains("FAM")) Fam = ADataRow["FAM"].ToString(); else Fam = "";
            if (ADataRow.Table.Columns.Contains("IMIA")) Imia = ADataRow["IMIA"].ToString(); else Imia = "";
            if (ADataRow.Table.Columns.Contains("OTCH")) Otch = ADataRow["OTCH"].ToString(); else Otch = "";
            if (ADataRow.Table.Columns.Contains("ADDRES")) Addres = Convert.ToInt64(ADataRow["ADDRES"]); else Addres = 0;
            if (ADataRow.Table.Columns.Contains("KVAR")) Kvar = Convert.ToInt64(ADataRow["KVAR"]); else Kvar = 0;
            if (ADataRow.Table.Columns.Contains("LS_UK")) Ls_uk = Convert.ToInt64(ADataRow["LS_UK"]); else Ls_uk = 0;
            if (ADataRow.Table.Columns.Contains("KVAR_A")) Kvar_a = ADataRow["KVAR_A"].ToString(); else Kvar_a = "";
            if (ADataRow.Table.Columns.Contains("TN")) Tn = Convert.ToInt64(ADataRow["TN"]); else Tn = 0;
            if (ADataRow.Table.Columns.Contains("S2")) S2 = Convert.ToDecimal(ADataRow["S2"]); else S2 = 0;
            if (ADataRow.Table.Columns.Contains("S3")) S3 = Convert.ToDecimal(ADataRow["S3"]); else S3 = 0;
            if (ADataRow.Table.Columns.Contains("DOPS2")) Dops2 = Convert.ToDecimal(ADataRow["DOPS2"]); else Dops2 = 0;
            if (ADataRow.Table.Columns.Contains("SB1")) Sb1 = Convert.ToDecimal(ADataRow["SB1"]); else Sb1 = 0;
            if (ADataRow.Table.Columns.Contains("SB2")) Sb2 = Convert.ToDecimal(ADataRow["SB2"]); else Sb2 = 0;
            if (ADataRow.Table.Columns.Contains("SB3")) Sb3 = Convert.ToDecimal(ADataRow["SB3"]); else Sb3 = 0;
            if (ADataRow.Table.Columns.Contains("S2OTOP")) S2otop = Convert.ToDecimal(ADataRow["S2OTOP"]); else S2otop = 0;
            if (ADataRow.Table.Columns.Contains("KOL")) Kol = Convert.ToInt64(ADataRow["KOL"]); else Kol = 0;
            if (ADataRow.Table.Columns.Contains("KOLFACT")) Kolfact = Convert.ToInt64(ADataRow["KOLFACT"]); else Kolfact = 0;
            if (ADataRow.Table.Columns.Contains("KOLL")) Koll = Convert.ToInt64(ADataRow["KOLL"]); else Koll = 0;
            if (ADataRow.Table.Columns.Contains("LG")) Lg = Convert.ToInt64(ADataRow["LG"]); else Lg = 0;
            if (ADataRow.Table.Columns.Contains("USLUGI")) Uslugi = ADataRow["USLUGI"].ToString(); else Uslugi = "";
            if (ADataRow.Table.Columns.Contains("TARIFI")) Tarifi = ADataRow["TARIFI"].ToString(); else Tarifi = "";
            if (ADataRow.Table.Columns.Contains("KOLK")) Kolk = Convert.ToInt64(ADataRow["KOLK"]); else Kolk = 0;
            if (ADataRow.Table.Columns.Contains("PROP")) Prop = ADataRow["PROP"].ToString() == "True" ? true : false; else Prop = false;
            if (ADataRow.Table.Columns.Contains("KATK")) Katk = Convert.ToInt64(ADataRow["KATK"]); else Katk = 0;
            if (ADataRow.Table.Columns.Contains("PR1")) Pr1 = ADataRow["PR1"].ToString() == "True" ? true : false; else Pr1 = false;
            if (ADataRow.Table.Columns.Contains("C_GVS")) C_gvs = ADataRow["C_GVS"].ToString() == "True" ? true : false; else C_gvs = false;
            if (ADataRow.Table.Columns.Contains("NOTSJ")) Notsj = ADataRow["NOTSJ"].ToString() == "True" ? true : false; else Notsj = false;
            if (ADataRow.Table.Columns.Contains("ORD")) Ord = ADataRow["ORD"].ToString(); else Ord = "";
            if (ADataRow.Table.Columns.Contains("C_HVS")) C_hvs = ADataRow["C_HVS"].ToString() == "True" ? true : false; else C_hvs = false;
            if (ADataRow.Table.Columns.Contains("D_ORD")) D_ord = Convert.ToDateTime(ADataRow["D_ORD"]); else D_ord = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUBSY")) Subsy = ADataRow["SUBSY"].ToString() == "True" ? true : false; else Subsy = false;
            if (ADataRow.Table.Columns.Contains("SUBSID")) Subsid = Convert.ToDecimal(ADataRow["SUBSID"]); else Subsid = 0;
            if (ADataRow.Table.Columns.Contains("ETAJ")) Etaj = Convert.ToInt64(ADataRow["ETAJ"]); else Etaj = 0;
            if (ADataRow.Table.Columns.Contains("N_ETAJ")) N_etaj = Convert.ToInt64(ADataRow["N_ETAJ"]); else N_etaj = 0;
            if (ADataRow.Table.Columns.Contains("SOTKI")) Sotki = Convert.ToDecimal(ADataRow["SOTKI"]); else Sotki = 0;
            if (ADataRow.Table.Columns.Contains("KOMM")) Komm = ADataRow["KOMM"].ToString() == "True" ? true : false; else Komm = false;
            if (ADataRow.Table.Columns.Contains("DATDOG")) Datdog = Convert.ToDateTime(ADataRow["DATDOG"]); else Datdog = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NOMDOG")) Nomdog = ADataRow["NOMDOG"].ToString(); else Nomdog = "";
            if (ADataRow.Table.Columns.Contains("ISP")) Isp = ADataRow["ISP"].ToString(); else Isp = "";
            if (ADataRow.Table.Columns.Contains("SALDO")) Saldo = Convert.ToDecimal(ADataRow["SALDO"]); else Saldo = 0;
            if (ADataRow.Table.Columns.Contains("TELEF")) Telef = ADataRow["TELEF"].ToString(); else Telef = "";
            if (ADataRow.Table.Columns.Contains("TELSOT")) Telsot = ADataRow["TELSOT"].ToString(); else Telsot = "";
            if (ADataRow.Table.Columns.Contains("DATNIK")) Datnik = Convert.ToDateTime(ADataRow["DATNIK"]); else Datnik = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUMNIK")) Sumnik = Convert.ToDecimal(ADataRow["SUMNIK"]); else Sumnik = 0;
            if (ADataRow.Table.Columns.Contains("MESNIK")) Mesnik = Convert.ToInt64(ADataRow["MESNIK"]); else Mesnik = 0;
            if (ADataRow.Table.Columns.Contains("PENI")) Peni = Convert.ToDecimal(ADataRow["PENI"]); else Peni = 0;
            if (ADataRow.Table.Columns.Contains("FLAGPENI")) Flagpeni = ADataRow["FLAGPENI"].ToString() == "True" ? true : false; else Flagpeni = false;
            if (ADataRow.Table.Columns.Contains("SALDO1")) Saldo1 = Convert.ToDecimal(ADataRow["SALDO1"]); else Saldo1 = 0;
            if (ADataRow.Table.Columns.Contains("LS1")) Ls1 = Convert.ToInt64(ADataRow["LS1"]); else Ls1 = 0;
            if (ADataRow.Table.Columns.Contains("OWNER")) Owner = ADataRow["OWNER"].ToString(); else Owner = "";
            if (ADataRow.Table.Columns.Contains("SALDO61")) Saldo61 = Convert.ToDecimal(ADataRow["SALDO61"]); else Saldo61 = 0;
            if (ADataRow.Table.Columns.Contains("KATP")) Katp = Convert.ToInt64(ADataRow["KATP"]); else Katp = 0;
            if (ADataRow.Table.Columns.Contains("SALDO62")) Saldo62 = Convert.ToDecimal(ADataRow["SALDO62"]); else Saldo62 = 0;
            if (ADataRow.Table.Columns.Contains("DATOFSUD")) Datofsud = Convert.ToDateTime(ADataRow["DATOFSUD"]); else Datofsud = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUMSUD")) Sumsud = Convert.ToDecimal(ADataRow["SUMSUD"]); else Sumsud = 0;
            if (ADataRow.Table.Columns.Contains("HVS_KUB")) Hvs_kub = Convert.ToDecimal(ADataRow["HVS_KUB"]); else Hvs_kub = 0;
            if (ADataRow.Table.Columns.Contains("KAN_KUB")) Kan_kub = Convert.ToDecimal(ADataRow["KAN_KUB"]); else Kan_kub = 0;
            if (ADataRow.Table.Columns.Contains("GVS_KUB")) Gvs_kub = Convert.ToDecimal(ADataRow["GVS_KUB"]); else Gvs_kub = 0;
            if (ADataRow.Table.Columns.Contains("SALDO2")) Saldo2 = Convert.ToDecimal(ADataRow["SALDO2"]); else Saldo2 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO3")) Saldo3 = Convert.ToDecimal(ADataRow["SALDO3"]); else Saldo3 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO4")) Saldo4 = Convert.ToDecimal(ADataRow["SALDO4"]); else Saldo4 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO5")) Saldo5 = Convert.ToDecimal(ADataRow["SALDO5"]); else Saldo5 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO6")) Saldo6 = Convert.ToDecimal(ADataRow["SALDO6"]); else Saldo6 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO7")) Saldo7 = Convert.ToDecimal(ADataRow["SALDO7"]); else Saldo7 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO8")) Saldo8 = Convert.ToDecimal(ADataRow["SALDO8"]); else Saldo8 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO9")) Saldo9 = Convert.ToDecimal(ADataRow["SALDO9"]); else Saldo9 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO10")) Saldo10 = Convert.ToDecimal(ADataRow["SALDO10"]); else Saldo10 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO11")) Saldo11 = Convert.ToDecimal(ADataRow["SALDO11"]); else Saldo11 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO12")) Saldo12 = Convert.ToDecimal(ADataRow["SALDO12"]); else Saldo12 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO13")) Saldo13 = Convert.ToDecimal(ADataRow["SALDO13"]); else Saldo13 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO14")) Saldo14 = Convert.ToDecimal(ADataRow["SALDO14"]); else Saldo14 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO17")) Saldo17 = Convert.ToDecimal(ADataRow["SALDO17"]); else Saldo17 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO18")) Saldo18 = Convert.ToDecimal(ADataRow["SALDO18"]); else Saldo18 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO19")) Saldo19 = Convert.ToDecimal(ADataRow["SALDO19"]); else Saldo19 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO34")) Saldo34 = Convert.ToDecimal(ADataRow["SALDO34"]); else Saldo34 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO35")) Saldo35 = Convert.ToDecimal(ADataRow["SALDO35"]); else Saldo35 = 0;
            if (ADataRow.Table.Columns.Contains("SALDO36")) Saldo36 = Convert.ToDecimal(ADataRow["SALDO36"]); else Saldo36 = 0;
            if (ADataRow.Table.Columns.Contains("LABEL")) Label = ADataRow["LABEL"].ToString(); else Label = "";
            if (ADataRow.Table.Columns.Contains("CLOSE")) Close = ADataRow["CLOSE"].ToString() == "True" ? true : false; else Close = false;
            if (ADataRow.Table.Columns.Contains("DATCLOSE")) Datclose = Convert.ToDateTime(ADataRow["DATCLOSE"]); else Datclose = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("OPL100")) Opl100 = ADataRow["OPL100"].ToString() == "True" ? true : false; else Opl100 = false;
            if (ADataRow.Table.Columns.Contains("FLLGOT")) Fllgot = ADataRow["FLLGOT"].ToString() == "True" ? true : false; else Fllgot = false;
            if (ADataRow.Table.Columns.Contains("FLOLD")) Flold = ADataRow["FLOLD"].ToString() == "True" ? true : false; else Flold = false;
            if (ADataRow.Table.Columns.Contains("FLPENS")) Flpens = ADataRow["FLPENS"].ToString() == "True" ? true : false; else Flpens = false;
            if (ADataRow.Table.Columns.Contains("DOMOFON")) Domofon = Convert.ToDecimal(ADataRow["DOMOFON"]); else Domofon = 0;
            if (ADataRow.Table.Columns.Contains("SOBS")) Sobs = ADataRow["SOBS"].ToString(); else Sobs = "";
            if (ADataRow.Table.Columns.Contains("INN")) Inn = ADataRow["INN"].ToString(); else Inn = "";
            if (ADataRow.Table.Columns.Contains("KPP")) Kpp = ADataRow["KPP"].ToString(); else Kpp = "";
            if (ADataRow.Table.Columns.Contains("BANK")) Bank = ADataRow["BANK"].ToString(); else Bank = "";
            if (ADataRow.Table.Columns.Contains("KS")) Ks = ADataRow["KS"].ToString(); else Ks = "";
            if (ADataRow.Table.Columns.Contains("BIK")) Bik = ADataRow["BIK"].ToString(); else Bik = "";
            if (ADataRow.Table.Columns.Contains("RS")) Rs = ADataRow["RS"].ToString(); else Rs = "";
        }
        
        public override AbstractRecord Clone()
        {
            SprrabRecord retValue = new SprrabRecord();
            retValue.Ls = this.Ls;
            retValue.Codf = this.Codf;
            retValue.Parent = this.Parent;
            retValue.Is_folder = this.Is_folder;
            retValue.Fam = this.Fam;
            retValue.Imia = this.Imia;
            retValue.Otch = this.Otch;
            retValue.Addres = this.Addres;
            retValue.Kvar = this.Kvar;
            retValue.Ls_uk = this.Ls_uk;
            retValue.Kvar_a = this.Kvar_a;
            retValue.Tn = this.Tn;
            retValue.S2 = this.S2;
            retValue.S3 = this.S3;
            retValue.Dops2 = this.Dops2;
            retValue.Sb1 = this.Sb1;
            retValue.Sb2 = this.Sb2;
            retValue.Sb3 = this.Sb3;
            retValue.S2otop = this.S2otop;
            retValue.Kol = this.Kol;
            retValue.Kolfact = this.Kolfact;
            retValue.Koll = this.Koll;
            retValue.Lg = this.Lg;
            retValue.Uslugi = this.Uslugi;
            retValue.Tarifi = this.Tarifi;
            retValue.Kolk = this.Kolk;
            retValue.Prop = this.Prop;
            retValue.Katk = this.Katk;
            retValue.Pr1 = this.Pr1;
            retValue.C_gvs = this.C_gvs;
            retValue.Notsj = this.Notsj;
            retValue.Ord = this.Ord;
            retValue.C_hvs = this.C_hvs;
            retValue.D_ord = this.D_ord;
            retValue.Subsy = this.Subsy;
            retValue.Subsid = this.Subsid;
            retValue.Etaj = this.Etaj;
            retValue.N_etaj = this.N_etaj;
            retValue.Sotki = this.Sotki;
            retValue.Komm = this.Komm;
            retValue.Datdog = this.Datdog;
            retValue.Nomdog = this.Nomdog;
            retValue.Isp = this.Isp;
            retValue.Saldo = this.Saldo;
            retValue.Telef = this.Telef;
            retValue.Telsot = this.Telsot;
            retValue.Datnik = this.Datnik;
            retValue.Sumnik = this.Sumnik;
            retValue.Mesnik = this.Mesnik;
            retValue.Peni = this.Peni;
            retValue.Flagpeni = this.Flagpeni;
            retValue.Saldo1 = this.Saldo1;
            retValue.Ls1 = this.Ls1;
            retValue.Owner = this.Owner;
            retValue.Saldo61 = this.Saldo61;
            retValue.Katp = this.Katp;
            retValue.Saldo62 = this.Saldo62;
            retValue.Datofsud = this.Datofsud;
            retValue.Sumsud = this.Sumsud;
            retValue.Hvs_kub = this.Hvs_kub;
            retValue.Kan_kub = this.Kan_kub;
            retValue.Gvs_kub = this.Gvs_kub;
            retValue.Saldo2 = this.Saldo2;
            retValue.Saldo3 = this.Saldo3;
            retValue.Saldo4 = this.Saldo4;
            retValue.Saldo5 = this.Saldo5;
            retValue.Saldo6 = this.Saldo6;
            retValue.Saldo7 = this.Saldo7;
            retValue.Saldo8 = this.Saldo8;
            retValue.Saldo9 = this.Saldo9;
            retValue.Saldo10 = this.Saldo10;
            retValue.Saldo11 = this.Saldo11;
            retValue.Saldo12 = this.Saldo12;
            retValue.Saldo13 = this.Saldo13;
            retValue.Saldo14 = this.Saldo14;
            retValue.Saldo17 = this.Saldo17;
            retValue.Saldo18 = this.Saldo18;
            retValue.Saldo19 = this.Saldo19;
            retValue.Saldo34 = this.Saldo34;
            retValue.Saldo35 = this.Saldo35;
            retValue.Saldo36 = this.Saldo36;
            retValue.Label = this.Label;
            retValue.Close = this.Close;
            retValue.Datclose = this.Datclose;
            retValue.Opl100 = this.Opl100;
            retValue.Fllgot = this.Fllgot;
            retValue.Flold = this.Flold;
            retValue.Flpens = this.Flpens;
            retValue.Domofon = this.Domofon;
            retValue.Sobs = this.Sobs;
            retValue.Inn = this.Inn;
            retValue.Kpp = this.Kpp;
            retValue.Bank = this.Bank;
            retValue.Ks = this.Ks;
            retValue.Bik = this.Bik;
            retValue.Rs = this.Rs;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPRRAB (LS, CODF, PARENT, IS_FOLDER, FAM, IMIA, OTCH, ADDRES, KVAR, LS_UK, KVAR_A, TN, S2, S3, DOPS2, SB1, SB2, SB3, S2OTOP, KOL, KOLFACT, KOLL, LG, USLUGI, TARIFI, KOLK, PROP, KATK, PR1, C_GVS, NOTSJ, ORD, C_HVS, D_ORD, SUBSY, SUBSID, ETAJ, N_ETAJ, SOTKI, KOMM, DATDOG, NOMDOG, ISP, SALDO, TELEF, TELSOT, DATNIK, SUMNIK, MESNIK, PENI, FLAGPENI, SALDO1, LS1, OWNER, SALDO61, KATP, SALDO62, DATOFSUD, SUMSUD, HVS_KUB, KAN_KUB, GVS_KUB, SALDO2, SALDO3, SALDO4, SALDO5, SALDO6, SALDO7, SALDO8, SALDO9, SALDO10, SALDO11, SALDO12, SALDO13, SALDO14, SALDO17, SALDO18, SALDO19, SALDO34, SALDO35, SALDO36, LABEL, CLOSE, DATCLOSE, OPL100, FLLGOT, FLOLD, FLPENS, DOMOFON, SOBS, INN, KPP, BANK, KS, BIK, RS) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', {7}, {8}, {9}, '{10}', {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, '{23}', '{24}', {25}, {26}, {27}, {28}, {29}, {30}, '{31}', {32}, CTOD('{33}'), {34}, {35}, {36}, {37}, {38}, {39}, CTOD('{40}'), '{41}', '{42}', {43}, '{44}', '{45}', CTOD('{46}'), {47}, {48}, {49}, {50}, {51}, {52}, '{53}', {54}, {55}, {56}, CTOD('{57}'), {58}, {59}, {60}, {61}, {62}, {63}, {64}, {65}, {66}, {67}, {68}, {69}, {70}, {71}, {72}, {73}, {74}, {75}, {76}, {77}, {78}, {79}, {80}, '{81}', {82}, CTOD('{83}'), {84}, {85}, {86}, {87}, {88}, '{89}', '{90}', '{91}', '{92}', '{93}', '{94}', '{95}')", Ls.ToString(), Codf.ToString(), Parent.ToString(), Is_folder.ToString(), String.IsNullOrEmpty(Fam) ? "" : Fam.Trim(), String.IsNullOrEmpty(Imia) ? "" : Imia.Trim(), String.IsNullOrEmpty(Otch) ? "" : Otch.Trim(), Addres.ToString(), Kvar.ToString(), Ls_uk.ToString(), String.IsNullOrEmpty(Kvar_a) ? "" : Kvar_a.Trim(), Tn.ToString(), S2.ToString().Replace(',','.'), S3.ToString().Replace(',','.'), Dops2.ToString().Replace(',','.'), Sb1.ToString().Replace(',','.'), Sb2.ToString().Replace(',','.'), Sb3.ToString().Replace(',','.'), S2otop.ToString().Replace(',','.'), Kol.ToString(), Kolfact.ToString(), Koll.ToString(), Lg.ToString(), String.IsNullOrEmpty(Uslugi) ? "" : Uslugi.Trim(), String.IsNullOrEmpty(Tarifi) ? "" : Tarifi.Trim(), Kolk.ToString(), (Prop ? 0 : 1 ), Katk.ToString(), (Pr1 ? 0 : 1 ), (C_gvs ? 0 : 1 ), (Notsj ? 0 : 1 ), String.IsNullOrEmpty(Ord) ? "" : Ord.Trim(), (C_hvs ? 0 : 1 ), D_ord == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", D_ord.Month, D_ord.Day, D_ord.Year), (Subsy ? 0 : 1 ), Subsid.ToString().Replace(',','.'), Etaj.ToString(), N_etaj.ToString(), Sotki.ToString().Replace(',','.'), (Komm ? 0 : 1 ), Datdog == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdog.Month, Datdog.Day, Datdog.Year), String.IsNullOrEmpty(Nomdog) ? "" : Nomdog.Trim(), String.IsNullOrEmpty(Isp) ? "" : Isp.Trim(), Saldo.ToString().Replace(',','.'), String.IsNullOrEmpty(Telef) ? "" : Telef.Trim(), String.IsNullOrEmpty(Telsot) ? "" : Telsot.Trim(), Datnik == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datnik.Month, Datnik.Day, Datnik.Year), Sumnik.ToString().Replace(',','.'), Mesnik.ToString(), Peni.ToString().Replace(',','.'), (Flagpeni ? 0 : 1 ), Saldo1.ToString().Replace(',','.'), Ls1.ToString(), String.IsNullOrEmpty(Owner) ? "" : Owner.Trim(), Saldo61.ToString().Replace(',','.'), Katp.ToString(), Saldo62.ToString().Replace(',','.'), Datofsud == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datofsud.Month, Datofsud.Day, Datofsud.Year), Sumsud.ToString().Replace(',','.'), Hvs_kub.ToString().Replace(',','.'), Kan_kub.ToString().Replace(',','.'), Gvs_kub.ToString().Replace(',','.'), Saldo2.ToString().Replace(',','.'), Saldo3.ToString().Replace(',','.'), Saldo4.ToString().Replace(',','.'), Saldo5.ToString().Replace(',','.'), Saldo6.ToString().Replace(',','.'), Saldo7.ToString().Replace(',','.'), Saldo8.ToString().Replace(',','.'), Saldo9.ToString().Replace(',','.'), Saldo10.ToString().Replace(',','.'), Saldo11.ToString().Replace(',','.'), Saldo12.ToString().Replace(',','.'), Saldo13.ToString().Replace(',','.'), Saldo14.ToString().Replace(',','.'), Saldo17.ToString().Replace(',','.'), Saldo18.ToString().Replace(',','.'), Saldo19.ToString().Replace(',','.'), Saldo34.ToString().Replace(',','.'), Saldo35.ToString().Replace(',','.'), Saldo36.ToString().Replace(',','.'), String.IsNullOrEmpty(Label) ? "" : Label.Trim(), (Close ? 0 : 1 ), Datclose == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datclose.Month, Datclose.Day, Datclose.Year), (Opl100 ? 0 : 1 ), (Fllgot ? 0 : 1 ), (Flold ? 0 : 1 ), (Flpens ? 0 : 1 ), Domofon.ToString().Replace(',','.'), String.IsNullOrEmpty(Sobs) ? "" : Sobs.Trim(), String.IsNullOrEmpty(Inn) ? "" : Inn.Trim(), String.IsNullOrEmpty(Kpp) ? "" : Kpp.Trim(), String.IsNullOrEmpty(Bank) ? "" : Bank.Trim(), String.IsNullOrEmpty(Ks) ? "" : Ks.Trim(), String.IsNullOrEmpty(Bik) ? "" : Bik.Trim(), String.IsNullOrEmpty(Rs) ? "" : Rs.Trim());
            return rs;
        }
    }
}
