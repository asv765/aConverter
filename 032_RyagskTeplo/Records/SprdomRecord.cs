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
    [TableName("SPRDOM.DBF")]
    public partial class SprdomRecord: AbstractRecord
    {
        private Int64 codc;
        // <summary>
        // CODC N(6)
        // </summary>
        [FieldName("CODC"), FieldType('N'), FieldWidth(6)]
        public Int64 Codc
        {
            get { return codc; }
            set { CheckIntegerData("Codc", value, 6); codc = value; }
        }

        private string ulic;
        // <summary>
        // ULIC C(30)
        // </summary>
        [FieldName("ULIC"), FieldType('C'), FieldWidth(30)]
        public string Ulic
        {
            get { return ulic; }
            set { CheckStringData("Ulic", value, 30); ulic = value; }
        }

        private string socr;
        // <summary>
        // SOCR C(20)
        // </summary>
        [FieldName("SOCR"), FieldType('C'), FieldWidth(20)]
        public string Socr
        {
            get { return socr; }
            set { CheckStringData("Socr", value, 20); socr = value; }
        }

        private Int64 domc;
        // <summary>
        // DOMC N(8)
        // </summary>
        [FieldName("DOMC"), FieldType('N'), FieldWidth(8)]
        public Int64 Domc
        {
            get { return domc; }
            set { CheckIntegerData("Domc", value, 8); domc = value; }
        }

        private Int64 org;
        // <summary>
        // ORG N(4)
        // </summary>
        [FieldName("ORG"), FieldType('N'), FieldWidth(4)]
        public Int64 Org
        {
            get { return org; }
            set { CheckIntegerData("Org", value, 4); org = value; }
        }

        private string dom_alpha;
        // <summary>
        // DOM_ALPHA C(10)
        // </summary>
        [FieldName("DOM_ALPHA"), FieldType('C'), FieldWidth(10)]
        public string Dom_alpha
        {
            get { return dom_alpha; }
            set { CheckStringData("Dom_alpha", value, 10); dom_alpha = value; }
        }

        private DateTime dat_org;
        // <summary>
        // DAT_ORG D(8)
        // </summary>
        [FieldName("DAT_ORG"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_org
        {
            get { return dat_org; }
            set {  dat_org = value; }
        }

        private Int64 dupc;
        // <summary>
        // DUPC N(4)
        // </summary>
        [FieldName("DUPC"), FieldType('N'), FieldWidth(4)]
        public Int64 Dupc
        {
            get { return dupc; }
            set { CheckIntegerData("Dupc", value, 4); dupc = value; }
        }

        private Int64 uchac;
        // <summary>
        // UCHAC N(4)
        // </summary>
        [FieldName("UCHAC"), FieldType('N'), FieldWidth(4)]
        public Int64 Uchac
        {
            get { return uchac; }
            set { CheckIntegerData("Uchac", value, 4); uchac = value; }
        }

        private Int64 kole;
        // <summary>
        // KOLE N(3)
        // </summary>
        [FieldName("KOLE"), FieldType('N'), FieldWidth(3)]
        public Int64 Kole
        {
            get { return kole; }
            set { CheckIntegerData("Kole", value, 3); kole = value; }
        }

        private Int64 kolkv;
        // <summary>
        // KOLKV N(5)
        // </summary>
        [FieldName("KOLKV"), FieldType('N'), FieldWidth(5)]
        public Int64 Kolkv
        {
            get { return kolkv; }
            set { CheckIntegerData("Kolkv", value, 5); kolkv = value; }
        }

        private Int64 kolkm;
        // <summary>
        // KOLKM N(6)
        // </summary>
        [FieldName("KOLKM"), FieldType('N'), FieldWidth(6)]
        public Int64 Kolkm
        {
            get { return kolkm; }
            set { CheckIntegerData("Kolkm", value, 6); kolkm = value; }
        }

        private Int64 kolp;
        // <summary>
        // KOLP N(3)
        // </summary>
        [FieldName("KOLP"), FieldType('N'), FieldWidth(3)]
        public Int64 Kolp
        {
            get { return kolp; }
            set { CheckIntegerData("Kolp", value, 3); kolp = value; }
        }

        private Int64 kolj;
        // <summary>
        // KOLJ N(6)
        // </summary>
        [FieldName("KOLJ"), FieldType('N'), FieldWidth(6)]
        public Int64 Kolj
        {
            get { return kolj; }
            set { CheckIntegerData("Kolj", value, 6); kolj = value; }
        }

        private Int64 kolpriv;
        // <summary>
        // KOLPRIV N(6)
        // </summary>
        [FieldName("KOLPRIV"), FieldType('N'), FieldWidth(6)]
        public Int64 Kolpriv
        {
            get { return kolpriv; }
            set { CheckIntegerData("Kolpriv", value, 6); kolpriv = value; }
        }

        private DateTime datp;
        // <summary>
        // DATP D(8)
        // </summary>
        [FieldName("DATP"), FieldType('D'), FieldWidth(8)]
        public DateTime Datp
        {
            get { return datp; }
            set {  datp = value; }
        }

        private decimal sob;
        // <summary>
        // SOB N(12,2)
        // </summary>
        [FieldName("SOB"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sob
        {
            get { return sob; }
            set { CheckDecimalData("Sob", value, 12, 2); sob = value; }
        }

        private decimal sob_priv;
        // <summary>
        // SOB_PRIV N(12,2)
        // </summary>
        [FieldName("SOB_PRIV"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sob_priv
        {
            get { return sob_priv; }
            set { CheckDecimalData("Sob_priv", value, 12, 2); sob_priv = value; }
        }

        private decimal sob_home;
        // <summary>
        // SOB_HOME N(12,2)
        // </summary>
        [FieldName("SOB_HOME"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sob_home
        {
            get { return sob_home; }
            set { CheckDecimalData("Sob_home", value, 12, 2); sob_home = value; }
        }

        private decimal sjl;
        // <summary>
        // SJL N(12,2)
        // </summary>
        [FieldName("SJL"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sjl
        {
            get { return sjl; }
            set { CheckDecimalData("Sjl", value, 12, 2); sjl = value; }
        }

        private decimal spod;
        // <summary>
        // SPOD N(12,2)
        // </summary>
        [FieldName("SPOD"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Spod
        {
            get { return spod; }
            set { CheckDecimalData("Spod", value, 12, 2); spod = value; }
        }

        private decimal satt;
        // <summary>
        // SATT N(12,2)
        // </summary>
        [FieldName("SATT"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Satt
        {
            get { return satt; }
            set { CheckDecimalData("Satt", value, 12, 2); satt = value; }
        }

        private decimal skrov;
        // <summary>
        // SKROV N(12,2)
        // </summary>
        [FieldName("SKROV"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Skrov
        {
            get { return skrov; }
            set { CheckDecimalData("Skrov", value, 12, 2); skrov = value; }
        }

        private decimal sscale;
        // <summary>
        // SSCALE N(12,2)
        // </summary>
        [FieldName("SSCALE"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sscale
        {
            get { return sscale; }
            set { CheckDecimalData("Sscale", value, 12, 2); sscale = value; }
        }

        private decimal saren;
        // <summary>
        // SAREN N(12,2)
        // </summary>
        [FieldName("SAREN"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Saren
        {
            get { return saren; }
            set { CheckDecimalData("Saren", value, 12, 2); saren = value; }
        }

        private decimal spay;
        // <summary>
        // SPAY N(12,2)
        // </summary>
        [FieldName("SPAY"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Spay
        {
            get { return spay; }
            set { CheckDecimalData("Spay", value, 12, 2); spay = value; }
        }

        private decimal shoz;
        // <summary>
        // SHOZ N(12,2)
        // </summary>
        [FieldName("SHOZ"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Shoz
        {
            get { return shoz; }
            set { CheckDecimalData("Shoz", value, 12, 2); shoz = value; }
        }

        private decimal vdom;
        // <summary>
        // VDOM N(12,2)
        // </summary>
        [FieldName("VDOM"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Vdom
        {
            get { return vdom; }
            set { CheckDecimalData("Vdom", value, 12, 2); vdom = value; }
        }

        private decimal vpod;
        // <summary>
        // VPOD N(12,2)
        // </summary>
        [FieldName("VPOD"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Vpod
        {
            get { return vpod; }
            set { CheckDecimalData("Vpod", value, 12, 2); vpod = value; }
        }

        private decimal s_otop;
        // <summary>
        // S_OTOP N(12,2)
        // </summary>
        [FieldName("S_OTOP"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_otop
        {
            get { return s_otop; }
            set { CheckDecimalData("S_otop", value, 12, 2); s_otop = value; }
        }

        private decimal s_gvs;
        // <summary>
        // S_GVS N(12,2)
        // </summary>
        [FieldName("S_GVS"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_gvs
        {
            get { return s_gvs; }
            set { CheckDecimalData("S_gvs", value, 12, 2); s_gvs = value; }
        }

        private decimal s_hvs;
        // <summary>
        // S_HVS N(12,2)
        // </summary>
        [FieldName("S_HVS"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_hvs
        {
            get { return s_hvs; }
            set { CheckDecimalData("S_hvs", value, 12, 2); s_hvs = value; }
        }

        private decimal s_vanna;
        // <summary>
        // S_VANNA N(12,2)
        // </summary>
        [FieldName("S_VANNA"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_vanna
        {
            get { return s_vanna; }
            set { CheckDecimalData("S_vanna", value, 12, 2); s_vanna = value; }
        }

        private decimal s_dush;
        // <summary>
        // S_DUSH N(12,2)
        // </summary>
        [FieldName("S_DUSH"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_dush
        {
            get { return s_dush; }
            set { CheckDecimalData("S_dush", value, 12, 2); s_dush = value; }
        }

        private decimal s_kns;
        // <summary>
        // S_KNS N(12,2)
        // </summary>
        [FieldName("S_KNS"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_kns
        {
            get { return s_kns; }
            set { CheckDecimalData("S_kns", value, 12, 2); s_kns = value; }
        }

        private decimal smain;
        // <summary>
        // SMAIN N(12,2)
        // </summary>
        [FieldName("SMAIN"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Smain
        {
            get { return smain; }
            set { CheckDecimalData("Smain", value, 12, 2); smain = value; }
        }

        private decimal s_plitka;
        // <summary>
        // S_PLITKA N(12,2)
        // </summary>
        [FieldName("S_PLITKA"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_plitka
        {
            get { return s_plitka; }
            set { CheckDecimalData("S_plitka", value, 12, 2); s_plitka = value; }
        }

        private decimal s_jiln;
        // <summary>
        // S_JILN N(12,2)
        // </summary>
        [FieldName("S_JILN"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_jiln
        {
            get { return s_jiln; }
            set { CheckDecimalData("S_jiln", value, 12, 2); s_jiln = value; }
        }

        private decimal s_podezd;
        // <summary>
        // S_PODEZD N(12,2)
        // </summary>
        [FieldName("S_PODEZD"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_podezd
        {
            get { return s_podezd; }
            set { CheckDecimalData("S_podezd", value, 12, 2); s_podezd = value; }
        }

        private decimal s_tambur;
        // <summary>
        // S_TAMBUR N(12,2)
        // </summary>
        [FieldName("S_TAMBUR"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_tambur
        {
            get { return s_tambur; }
            set { CheckDecimalData("S_tambur", value, 12, 2); s_tambur = value; }
        }

        private decimal s_balkon;
        // <summary>
        // S_BALKON N(12,2)
        // </summary>
        [FieldName("S_BALKON"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_balkon
        {
            get { return s_balkon; }
            set { CheckDecimalData("S_balkon", value, 12, 2); s_balkon = value; }
        }

        private decimal s_ub_asf;
        // <summary>
        // S_UB_ASF N(12,2)
        // </summary>
        [FieldName("S_UB_ASF"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_ub_asf
        {
            get { return s_ub_asf; }
            set { CheckDecimalData("S_ub_asf", value, 12, 2); s_ub_asf = value; }
        }

        private decimal s_ub_grunt;
        // <summary>
        // S_UB_GRUNT N(12,2)
        // </summary>
        [FieldName("S_UB_GRUNT"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_ub_grunt
        {
            get { return s_ub_grunt; }
            set { CheckDecimalData("S_ub_grunt", value, 12, 2); s_ub_grunt = value; }
        }

        private decimal s_ub_zel;
        // <summary>
        // S_UB_ZEL N(12,2)
        // </summary>
        [FieldName("S_UB_ZEL"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal S_ub_zel
        {
            get { return s_ub_zel; }
            set { CheckDecimalData("S_ub_zel", value, 12, 2); s_ub_zel = value; }
        }

        private decimal y_sred;
        // <summary>
        // Y_SRED N(12,2)
        // </summary>
        [FieldName("Y_SRED"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Y_sred
        {
            get { return y_sred; }
            set { CheckDecimalData("Y_sred", value, 12, 2); y_sred = value; }
        }

        private Int64 katd;
        // <summary>
        // KATD N(4)
        // </summary>
        [FieldName("KATD"), FieldType('N'), FieldWidth(4)]
        public Int64 Katd
        {
            get { return katd; }
            set { CheckIntegerData("Katd", value, 4); katd = value; }
        }

        private Int64 vids;
        // <summary>
        // VIDS N(4)
        // </summary>
        [FieldName("VIDS"), FieldType('N'), FieldWidth(4)]
        public Int64 Vids
        {
            get { return vids; }
            set { CheckIntegerData("Vids", value, 4); vids = value; }
        }

        private Int64 msten;
        // <summary>
        // MSTEN N(4)
        // </summary>
        [FieldName("MSTEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Msten
        {
            get { return msten; }
            set { CheckIntegerData("Msten", value, 4); msten = value; }
        }

        private Int64 mper;
        // <summary>
        // MPER N(4)
        // </summary>
        [FieldName("MPER"), FieldType('N'), FieldWidth(4)]
        public Int64 Mper
        {
            get { return mper; }
            set { CheckIntegerData("Mper", value, 4); mper = value; }
        }

        private Int64 mkrov;
        // <summary>
        // MKROV N(4)
        // </summary>
        [FieldName("MKROV"), FieldType('N'), FieldWidth(4)]
        public Int64 Mkrov
        {
            get { return mkrov; }
            set { CheckIntegerData("Mkrov", value, 4); mkrov = value; }
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

        private decimal iznos;
        // <summary>
        // IZNOS N(6,2)
        // </summary>
        [FieldName("IZNOS"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Iznos
        {
            get { return iznos; }
            set { CheckDecimalData("Iznos", value, 6, 2); iznos = value; }
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

        private bool flras;
        // <summary>
        // FLRAS L(1)
        // </summary>
        [FieldName("FLRAS"), FieldType('L'), FieldWidth(1)]
        public bool Flras
        {
            get { return flras; }
            set {  flras = value; }
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

        private string sp_us_pos;
        // <summary>
        // SP_US_POS C(30)
        // </summary>
        [FieldName("SP_US_POS"), FieldType('C'), FieldWidth(30)]
        public string Sp_us_pos
        {
            get { return sp_us_pos; }
            set { CheckStringData("Sp_us_pos", value, 30); sp_us_pos = value; }
        }

        private string usl_org;
        // <summary>
        // USL_ORG C(120)
        // </summary>
        [FieldName("USL_ORG"), FieldType('C'), FieldWidth(120)]
        public string Usl_org
        {
            get { return usl_org; }
            set { CheckStringData("Usl_org", value, 120); usl_org = value; }
        }

        private bool tsj;
        // <summary>
        // TSJ L(1)
        // </summary>
        [FieldName("TSJ"), FieldType('L'), FieldWidth(1)]
        public bool Tsj
        {
            get { return tsj; }
            set {  tsj = value; }
        }

        private Int64 n_tm;
        // <summary>
        // N_TM N(4)
        // </summary>
        [FieldName("N_TM"), FieldType('N'), FieldWidth(4)]
        public Int64 N_tm
        {
            get { return n_tm; }
            set { CheckIntegerData("N_tm", value, 4); n_tm = value; }
        }

        private bool elplita;
        // <summary>
        // ELPLITA L(1)
        // </summary>
        [FieldName("ELPLITA"), FieldType('L'), FieldWidth(1)]
        public bool Elplita
        {
            get { return elplita; }
            set {  elplita = value; }
        }

        private bool vanna;
        // <summary>
        // VANNA L(1)
        // </summary>
        [FieldName("VANNA"), FieldType('L'), FieldWidth(1)]
        public bool Vanna
        {
            get { return vanna; }
            set {  vanna = value; }
        }

        private bool musor;
        // <summary>
        // MUSOR L(1)
        // </summary>
        [FieldName("MUSOR"), FieldType('L'), FieldWidth(1)]
        public bool Musor
        {
            get { return musor; }
            set {  musor = value; }
        }

        private bool lift;
        // <summary>
        // LIFT L(1)
        // </summary>
        [FieldName("LIFT"), FieldType('L'), FieldWidth(1)]
        public bool Lift
        {
            get { return lift; }
            set {  lift = value; }
        }

        private bool tpasport;
        // <summary>
        // TPASPORT L(1)
        // </summary>
        [FieldName("TPASPORT"), FieldType('L'), FieldWidth(1)]
        public bool Tpasport
        {
            get { return tpasport; }
            set {  tpasport = value; }
        }

        private Int64 scheme;
        // <summary>
        // SCHEME N(4)
        // </summary>
        [FieldName("SCHEME"), FieldType('N'), FieldWidth(4)]
        public Int64 Scheme
        {
            get { return scheme; }
            set { CheckIntegerData("Scheme", value, 4); scheme = value; }
        }

        private decimal balans;
        // <summary>
        // BALANS N(12,2)
        // </summary>
        [FieldName("BALANS"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Balans
        {
            get { return balans; }
            set { CheckDecimalData("Balans", value, 12, 2); balans = value; }
        }

        private DateTime datot1;
        // <summary>
        // DATOT1 D(8)
        // </summary>
        [FieldName("DATOT1"), FieldType('D'), FieldWidth(8)]
        public DateTime Datot1
        {
            get { return datot1; }
            set {  datot1 = value; }
        }

        private DateTime datot2;
        // <summary>
        // DATOT2 D(8)
        // </summary>
        [FieldName("DATOT2"), FieldType('D'), FieldWidth(8)]
        public DateTime Datot2
        {
            get { return datot2; }
            set {  datot2 = value; }
        }

        private bool korsys;
        // <summary>
        // KORSYS L(1)
        // </summary>
        [FieldName("KORSYS"), FieldType('L'), FieldWidth(1)]
        public bool Korsys
        {
            get { return korsys; }
            set {  korsys = value; }
        }

        private Int64 kol1;
        // <summary>
        // KOL1 N(6)
        // </summary>
        [FieldName("KOL1"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol1
        {
            get { return kol1; }
            set { CheckIntegerData("Kol1", value, 6); kol1 = value; }
        }

        private Int64 kol2;
        // <summary>
        // KOL2 N(6)
        // </summary>
        [FieldName("KOL2"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol2
        {
            get { return kol2; }
            set { CheckIntegerData("Kol2", value, 6); kol2 = value; }
        }

        private Int64 kol3;
        // <summary>
        // KOL3 N(6)
        // </summary>
        [FieldName("KOL3"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol3
        {
            get { return kol3; }
            set { CheckIntegerData("Kol3", value, 6); kol3 = value; }
        }

        private Int64 kol4;
        // <summary>
        // KOL4 N(6)
        // </summary>
        [FieldName("KOL4"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol4
        {
            get { return kol4; }
            set { CheckIntegerData("Kol4", value, 6); kol4 = value; }
        }

        private Int64 kol5;
        // <summary>
        // KOL5 N(6)
        // </summary>
        [FieldName("KOL5"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol5
        {
            get { return kol5; }
            set { CheckIntegerData("Kol5", value, 6); kol5 = value; }
        }

        private Int64 kol6;
        // <summary>
        // KOL6 N(6)
        // </summary>
        [FieldName("KOL6"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol6
        {
            get { return kol6; }
            set { CheckIntegerData("Kol6", value, 6); kol6 = value; }
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

        private decimal dayras;
        // <summary>
        // DAYRAS N(10,2)
        // </summary>
        [FieldName("DAYRAS"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Dayras
        {
            get { return dayras; }
            set { CheckDecimalData("Dayras", value, 10, 2); dayras = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("CODC")) Codc = Convert.ToInt64(ADataRow["CODC"]); else Codc = 0;
            if (ADataRow.Table.Columns.Contains("ULIC")) Ulic = ADataRow["ULIC"].ToString(); else Ulic = "";
            if (ADataRow.Table.Columns.Contains("SOCR")) Socr = ADataRow["SOCR"].ToString(); else Socr = "";
            if (ADataRow.Table.Columns.Contains("DOMC")) Domc = Convert.ToInt64(ADataRow["DOMC"]); else Domc = 0;
            if (ADataRow.Table.Columns.Contains("ORG")) Org = Convert.ToInt64(ADataRow["ORG"]); else Org = 0;
            if (ADataRow.Table.Columns.Contains("DOM_ALPHA")) Dom_alpha = ADataRow["DOM_ALPHA"].ToString(); else Dom_alpha = "";
            if (ADataRow.Table.Columns.Contains("DAT_ORG")) Dat_org = Convert.ToDateTime(ADataRow["DAT_ORG"]); else Dat_org = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DUPC")) Dupc = Convert.ToInt64(ADataRow["DUPC"]); else Dupc = 0;
            if (ADataRow.Table.Columns.Contains("UCHAC")) Uchac = Convert.ToInt64(ADataRow["UCHAC"]); else Uchac = 0;
            if (ADataRow.Table.Columns.Contains("KOLE")) Kole = Convert.ToInt64(ADataRow["KOLE"]); else Kole = 0;
            if (ADataRow.Table.Columns.Contains("KOLKV")) Kolkv = Convert.ToInt64(ADataRow["KOLKV"]); else Kolkv = 0;
            if (ADataRow.Table.Columns.Contains("KOLKM")) Kolkm = Convert.ToInt64(ADataRow["KOLKM"]); else Kolkm = 0;
            if (ADataRow.Table.Columns.Contains("KOLP")) Kolp = Convert.ToInt64(ADataRow["KOLP"]); else Kolp = 0;
            if (ADataRow.Table.Columns.Contains("KOLJ")) Kolj = Convert.ToInt64(ADataRow["KOLJ"]); else Kolj = 0;
            if (ADataRow.Table.Columns.Contains("KOLPRIV")) Kolpriv = Convert.ToInt64(ADataRow["KOLPRIV"]); else Kolpriv = 0;
            if (ADataRow.Table.Columns.Contains("DATP")) Datp = Convert.ToDateTime(ADataRow["DATP"]); else Datp = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SOB")) Sob = Convert.ToDecimal(ADataRow["SOB"]); else Sob = 0;
            if (ADataRow.Table.Columns.Contains("SOB_PRIV")) Sob_priv = Convert.ToDecimal(ADataRow["SOB_PRIV"]); else Sob_priv = 0;
            if (ADataRow.Table.Columns.Contains("SOB_HOME")) Sob_home = Convert.ToDecimal(ADataRow["SOB_HOME"]); else Sob_home = 0;
            if (ADataRow.Table.Columns.Contains("SJL")) Sjl = Convert.ToDecimal(ADataRow["SJL"]); else Sjl = 0;
            if (ADataRow.Table.Columns.Contains("SPOD")) Spod = Convert.ToDecimal(ADataRow["SPOD"]); else Spod = 0;
            if (ADataRow.Table.Columns.Contains("SATT")) Satt = Convert.ToDecimal(ADataRow["SATT"]); else Satt = 0;
            if (ADataRow.Table.Columns.Contains("SKROV")) Skrov = Convert.ToDecimal(ADataRow["SKROV"]); else Skrov = 0;
            if (ADataRow.Table.Columns.Contains("SSCALE")) Sscale = Convert.ToDecimal(ADataRow["SSCALE"]); else Sscale = 0;
            if (ADataRow.Table.Columns.Contains("SAREN")) Saren = Convert.ToDecimal(ADataRow["SAREN"]); else Saren = 0;
            if (ADataRow.Table.Columns.Contains("SPAY")) Spay = Convert.ToDecimal(ADataRow["SPAY"]); else Spay = 0;
            if (ADataRow.Table.Columns.Contains("SHOZ")) Shoz = Convert.ToDecimal(ADataRow["SHOZ"]); else Shoz = 0;
            if (ADataRow.Table.Columns.Contains("VDOM")) Vdom = Convert.ToDecimal(ADataRow["VDOM"]); else Vdom = 0;
            if (ADataRow.Table.Columns.Contains("VPOD")) Vpod = Convert.ToDecimal(ADataRow["VPOD"]); else Vpod = 0;
            if (ADataRow.Table.Columns.Contains("S_OTOP")) S_otop = Convert.ToDecimal(ADataRow["S_OTOP"]); else S_otop = 0;
            if (ADataRow.Table.Columns.Contains("S_GVS")) S_gvs = Convert.ToDecimal(ADataRow["S_GVS"]); else S_gvs = 0;
            if (ADataRow.Table.Columns.Contains("S_HVS")) S_hvs = Convert.ToDecimal(ADataRow["S_HVS"]); else S_hvs = 0;
            if (ADataRow.Table.Columns.Contains("S_VANNA")) S_vanna = Convert.ToDecimal(ADataRow["S_VANNA"]); else S_vanna = 0;
            if (ADataRow.Table.Columns.Contains("S_DUSH")) S_dush = Convert.ToDecimal(ADataRow["S_DUSH"]); else S_dush = 0;
            if (ADataRow.Table.Columns.Contains("S_KNS")) S_kns = Convert.ToDecimal(ADataRow["S_KNS"]); else S_kns = 0;
            if (ADataRow.Table.Columns.Contains("SMAIN")) Smain = Convert.ToDecimal(ADataRow["SMAIN"]); else Smain = 0;
            if (ADataRow.Table.Columns.Contains("S_PLITKA")) S_plitka = Convert.ToDecimal(ADataRow["S_PLITKA"]); else S_plitka = 0;
            if (ADataRow.Table.Columns.Contains("S_JILN")) S_jiln = Convert.ToDecimal(ADataRow["S_JILN"]); else S_jiln = 0;
            if (ADataRow.Table.Columns.Contains("S_PODEZD")) S_podezd = Convert.ToDecimal(ADataRow["S_PODEZD"]); else S_podezd = 0;
            if (ADataRow.Table.Columns.Contains("S_TAMBUR")) S_tambur = Convert.ToDecimal(ADataRow["S_TAMBUR"]); else S_tambur = 0;
            if (ADataRow.Table.Columns.Contains("S_BALKON")) S_balkon = Convert.ToDecimal(ADataRow["S_BALKON"]); else S_balkon = 0;
            if (ADataRow.Table.Columns.Contains("S_UB_ASF")) S_ub_asf = Convert.ToDecimal(ADataRow["S_UB_ASF"]); else S_ub_asf = 0;
            if (ADataRow.Table.Columns.Contains("S_UB_GRUNT")) S_ub_grunt = Convert.ToDecimal(ADataRow["S_UB_GRUNT"]); else S_ub_grunt = 0;
            if (ADataRow.Table.Columns.Contains("S_UB_ZEL")) S_ub_zel = Convert.ToDecimal(ADataRow["S_UB_ZEL"]); else S_ub_zel = 0;
            if (ADataRow.Table.Columns.Contains("Y_SRED")) Y_sred = Convert.ToDecimal(ADataRow["Y_SRED"]); else Y_sred = 0;
            if (ADataRow.Table.Columns.Contains("KATD")) Katd = Convert.ToInt64(ADataRow["KATD"]); else Katd = 0;
            if (ADataRow.Table.Columns.Contains("VIDS")) Vids = Convert.ToInt64(ADataRow["VIDS"]); else Vids = 0;
            if (ADataRow.Table.Columns.Contains("MSTEN")) Msten = Convert.ToInt64(ADataRow["MSTEN"]); else Msten = 0;
            if (ADataRow.Table.Columns.Contains("MPER")) Mper = Convert.ToInt64(ADataRow["MPER"]); else Mper = 0;
            if (ADataRow.Table.Columns.Contains("MKROV")) Mkrov = Convert.ToInt64(ADataRow["MKROV"]); else Mkrov = 0;
            if (ADataRow.Table.Columns.Contains("USLUGI")) Uslugi = ADataRow["USLUGI"].ToString(); else Uslugi = "";
            if (ADataRow.Table.Columns.Contains("IZNOS")) Iznos = Convert.ToDecimal(ADataRow["IZNOS"]); else Iznos = 0;
            if (ADataRow.Table.Columns.Contains("TARIFI")) Tarifi = ADataRow["TARIFI"].ToString(); else Tarifi = "";
            if (ADataRow.Table.Columns.Contains("FLRAS")) Flras = ADataRow["FLRAS"].ToString() == "True" ? true : false; else Flras = false;
            if (ADataRow.Table.Columns.Contains("LABEL")) Label = ADataRow["LABEL"].ToString(); else Label = "";
            if (ADataRow.Table.Columns.Contains("SP_US_POS")) Sp_us_pos = ADataRow["SP_US_POS"].ToString(); else Sp_us_pos = "";
            if (ADataRow.Table.Columns.Contains("USL_ORG")) Usl_org = ADataRow["USL_ORG"].ToString(); else Usl_org = "";
            if (ADataRow.Table.Columns.Contains("TSJ")) Tsj = ADataRow["TSJ"].ToString() == "True" ? true : false; else Tsj = false;
            if (ADataRow.Table.Columns.Contains("N_TM")) N_tm = Convert.ToInt64(ADataRow["N_TM"]); else N_tm = 0;
            if (ADataRow.Table.Columns.Contains("ELPLITA")) Elplita = ADataRow["ELPLITA"].ToString() == "True" ? true : false; else Elplita = false;
            if (ADataRow.Table.Columns.Contains("VANNA")) Vanna = ADataRow["VANNA"].ToString() == "True" ? true : false; else Vanna = false;
            if (ADataRow.Table.Columns.Contains("MUSOR")) Musor = ADataRow["MUSOR"].ToString() == "True" ? true : false; else Musor = false;
            if (ADataRow.Table.Columns.Contains("LIFT")) Lift = ADataRow["LIFT"].ToString() == "True" ? true : false; else Lift = false;
            if (ADataRow.Table.Columns.Contains("TPASPORT")) Tpasport = ADataRow["TPASPORT"].ToString() == "True" ? true : false; else Tpasport = false;
            if (ADataRow.Table.Columns.Contains("SCHEME")) Scheme = Convert.ToInt64(ADataRow["SCHEME"]); else Scheme = 0;
            if (ADataRow.Table.Columns.Contains("BALANS")) Balans = Convert.ToDecimal(ADataRow["BALANS"]); else Balans = 0;
            if (ADataRow.Table.Columns.Contains("DATOT1")) Datot1 = Convert.ToDateTime(ADataRow["DATOT1"]); else Datot1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATOT2")) Datot2 = Convert.ToDateTime(ADataRow["DATOT2"]); else Datot2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("KORSYS")) Korsys = ADataRow["KORSYS"].ToString() == "True" ? true : false; else Korsys = false;
            if (ADataRow.Table.Columns.Contains("KOL1")) Kol1 = Convert.ToInt64(ADataRow["KOL1"]); else Kol1 = 0;
            if (ADataRow.Table.Columns.Contains("KOL2")) Kol2 = Convert.ToInt64(ADataRow["KOL2"]); else Kol2 = 0;
            if (ADataRow.Table.Columns.Contains("KOL3")) Kol3 = Convert.ToInt64(ADataRow["KOL3"]); else Kol3 = 0;
            if (ADataRow.Table.Columns.Contains("KOL4")) Kol4 = Convert.ToInt64(ADataRow["KOL4"]); else Kol4 = 0;
            if (ADataRow.Table.Columns.Contains("KOL5")) Kol5 = Convert.ToInt64(ADataRow["KOL5"]); else Kol5 = 0;
            if (ADataRow.Table.Columns.Contains("KOL6")) Kol6 = Convert.ToInt64(ADataRow["KOL6"]); else Kol6 = 0;
            if (ADataRow.Table.Columns.Contains("KAT")) Kat = Convert.ToInt64(ADataRow["KAT"]); else Kat = 0;
            if (ADataRow.Table.Columns.Contains("DAYRAS")) Dayras = Convert.ToDecimal(ADataRow["DAYRAS"]); else Dayras = 0;
        }
        
        public override AbstractRecord Clone()
        {
            SprdomRecord retValue = new SprdomRecord();
            retValue.Codc = this.Codc;
            retValue.Ulic = this.Ulic;
            retValue.Socr = this.Socr;
            retValue.Domc = this.Domc;
            retValue.Org = this.Org;
            retValue.Dom_alpha = this.Dom_alpha;
            retValue.Dat_org = this.Dat_org;
            retValue.Dupc = this.Dupc;
            retValue.Uchac = this.Uchac;
            retValue.Kole = this.Kole;
            retValue.Kolkv = this.Kolkv;
            retValue.Kolkm = this.Kolkm;
            retValue.Kolp = this.Kolp;
            retValue.Kolj = this.Kolj;
            retValue.Kolpriv = this.Kolpriv;
            retValue.Datp = this.Datp;
            retValue.Sob = this.Sob;
            retValue.Sob_priv = this.Sob_priv;
            retValue.Sob_home = this.Sob_home;
            retValue.Sjl = this.Sjl;
            retValue.Spod = this.Spod;
            retValue.Satt = this.Satt;
            retValue.Skrov = this.Skrov;
            retValue.Sscale = this.Sscale;
            retValue.Saren = this.Saren;
            retValue.Spay = this.Spay;
            retValue.Shoz = this.Shoz;
            retValue.Vdom = this.Vdom;
            retValue.Vpod = this.Vpod;
            retValue.S_otop = this.S_otop;
            retValue.S_gvs = this.S_gvs;
            retValue.S_hvs = this.S_hvs;
            retValue.S_vanna = this.S_vanna;
            retValue.S_dush = this.S_dush;
            retValue.S_kns = this.S_kns;
            retValue.Smain = this.Smain;
            retValue.S_plitka = this.S_plitka;
            retValue.S_jiln = this.S_jiln;
            retValue.S_podezd = this.S_podezd;
            retValue.S_tambur = this.S_tambur;
            retValue.S_balkon = this.S_balkon;
            retValue.S_ub_asf = this.S_ub_asf;
            retValue.S_ub_grunt = this.S_ub_grunt;
            retValue.S_ub_zel = this.S_ub_zel;
            retValue.Y_sred = this.Y_sred;
            retValue.Katd = this.Katd;
            retValue.Vids = this.Vids;
            retValue.Msten = this.Msten;
            retValue.Mper = this.Mper;
            retValue.Mkrov = this.Mkrov;
            retValue.Uslugi = this.Uslugi;
            retValue.Iznos = this.Iznos;
            retValue.Tarifi = this.Tarifi;
            retValue.Flras = this.Flras;
            retValue.Label = this.Label;
            retValue.Sp_us_pos = this.Sp_us_pos;
            retValue.Usl_org = this.Usl_org;
            retValue.Tsj = this.Tsj;
            retValue.N_tm = this.N_tm;
            retValue.Elplita = this.Elplita;
            retValue.Vanna = this.Vanna;
            retValue.Musor = this.Musor;
            retValue.Lift = this.Lift;
            retValue.Tpasport = this.Tpasport;
            retValue.Scheme = this.Scheme;
            retValue.Balans = this.Balans;
            retValue.Datot1 = this.Datot1;
            retValue.Datot2 = this.Datot2;
            retValue.Korsys = this.Korsys;
            retValue.Kol1 = this.Kol1;
            retValue.Kol2 = this.Kol2;
            retValue.Kol3 = this.Kol3;
            retValue.Kol4 = this.Kol4;
            retValue.Kol5 = this.Kol5;
            retValue.Kol6 = this.Kol6;
            retValue.Kat = this.Kat;
            retValue.Dayras = this.Dayras;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPRDOM (CODC, ULIC, SOCR, DOMC, ORG, DOM_ALPHA, DAT_ORG, DUPC, UCHAC, KOLE, KOLKV, KOLKM, KOLP, KOLJ, KOLPRIV, DATP, SOB, SOB_PRIV, SOB_HOME, SJL, SPOD, SATT, SKROV, SSCALE, SAREN, SPAY, SHOZ, VDOM, VPOD, S_OTOP, S_GVS, S_HVS, S_VANNA, S_DUSH, S_KNS, SMAIN, S_PLITKA, S_JILN, S_PODEZD, S_TAMBUR, S_BALKON, S_UB_ASF, S_UB_GRUNT, S_UB_ZEL, Y_SRED, KATD, VIDS, MSTEN, MPER, MKROV, USLUGI, IZNOS, TARIFI, FLRAS, LABEL, SP_US_POS, USL_ORG, TSJ, N_TM, ELPLITA, VANNA, MUSOR, LIFT, TPASPORT, SCHEME, BALANS, DATOT1, DATOT2, KORSYS, KOL1, KOL2, KOL3, KOL4, KOL5, KOL6, KAT, DAYRAS) VALUES ({0}, '{1}', '{2}', {3}, {4}, '{5}', CTOD('{6}'), {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, CTOD('{15}'), {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47}, {48}, {49}, '{50}', {51}, '{52}', {53}, '{54}', '{55}', '{56}', {57}, {58}, {59}, {60}, {61}, {62}, {63}, {64}, {65}, CTOD('{66}'), CTOD('{67}'), {68}, {69}, {70}, {71}, {72}, {73}, {74}, {75}, {76})", Codc.ToString(), String.IsNullOrEmpty(Ulic) ? "" : Ulic.Trim(), String.IsNullOrEmpty(Socr) ? "" : Socr.Trim(), Domc.ToString(), Org.ToString(), String.IsNullOrEmpty(Dom_alpha) ? "" : Dom_alpha.Trim(), Dat_org == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_org.Month, Dat_org.Day, Dat_org.Year), Dupc.ToString(), Uchac.ToString(), Kole.ToString(), Kolkv.ToString(), Kolkm.ToString(), Kolp.ToString(), Kolj.ToString(), Kolpriv.ToString(), Datp == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datp.Month, Datp.Day, Datp.Year), Sob.ToString().Replace(',','.'), Sob_priv.ToString().Replace(',','.'), Sob_home.ToString().Replace(',','.'), Sjl.ToString().Replace(',','.'), Spod.ToString().Replace(',','.'), Satt.ToString().Replace(',','.'), Skrov.ToString().Replace(',','.'), Sscale.ToString().Replace(',','.'), Saren.ToString().Replace(',','.'), Spay.ToString().Replace(',','.'), Shoz.ToString().Replace(',','.'), Vdom.ToString().Replace(',','.'), Vpod.ToString().Replace(',','.'), S_otop.ToString().Replace(',','.'), S_gvs.ToString().Replace(',','.'), S_hvs.ToString().Replace(',','.'), S_vanna.ToString().Replace(',','.'), S_dush.ToString().Replace(',','.'), S_kns.ToString().Replace(',','.'), Smain.ToString().Replace(',','.'), S_plitka.ToString().Replace(',','.'), S_jiln.ToString().Replace(',','.'), S_podezd.ToString().Replace(',','.'), S_tambur.ToString().Replace(',','.'), S_balkon.ToString().Replace(',','.'), S_ub_asf.ToString().Replace(',','.'), S_ub_grunt.ToString().Replace(',','.'), S_ub_zel.ToString().Replace(',','.'), Y_sred.ToString().Replace(',','.'), Katd.ToString(), Vids.ToString(), Msten.ToString(), Mper.ToString(), Mkrov.ToString(), String.IsNullOrEmpty(Uslugi) ? "" : Uslugi.Trim(), Iznos.ToString().Replace(',','.'), String.IsNullOrEmpty(Tarifi) ? "" : Tarifi.Trim(), (Flras ? 0 : 1 ), String.IsNullOrEmpty(Label) ? "" : Label.Trim(), String.IsNullOrEmpty(Sp_us_pos) ? "" : Sp_us_pos.Trim(), String.IsNullOrEmpty(Usl_org) ? "" : Usl_org.Trim(), (Tsj ? 0 : 1 ), N_tm.ToString(), (Elplita ? 0 : 1 ), (Vanna ? 0 : 1 ), (Musor ? 0 : 1 ), (Lift ? 0 : 1 ), (Tpasport ? 0 : 1 ), Scheme.ToString(), Balans.ToString().Replace(',','.'), Datot1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datot1.Month, Datot1.Day, Datot1.Year), Datot2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datot2.Month, Datot2.Day, Datot2.Year), (Korsys ? 0 : 1 ), Kol1.ToString(), Kol2.ToString(), Kol3.ToString(), Kol4.ToString(), Kol5.ToString(), Kol6.ToString(), Kat.ToString(), Dayras.ToString().Replace(',','.'));
            return rs;
        }
    }
}
