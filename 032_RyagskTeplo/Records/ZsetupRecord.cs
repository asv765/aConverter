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
    [TableName("ZSETUP.DBF")]
    public partial class ZsetupRecord: AbstractRecord
    {
        private string name_or;
        // <summary>
        // NAME_OR C(100)
        // </summary>
        [FieldName("NAME_OR"), FieldType('C'), FieldWidth(100)]
        public string Name_or
        {
            get { return name_or; }
            set { CheckStringData("Name_or", value, 100); name_or = value; }
        }

        private DateTime dt_c;
        // <summary>
        // DT_C D(8)
        // </summary>
        [FieldName("DT_C"), FieldType('D'), FieldWidth(8)]
        public DateTime Dt_c
        {
            get { return dt_c; }
            set {  dt_c = value; }
        }

        private string fio_rukov;
        // <summary>
        // FIO_RUKOV C(50)
        // </summary>
        [FieldName("FIO_RUKOV"), FieldType('C'), FieldWidth(50)]
        public string Fio_rukov
        {
            get { return fio_rukov; }
            set { CheckStringData("Fio_rukov", value, 50); fio_rukov = value; }
        }

        private string fio_glbuh;
        // <summary>
        // FIO_GLBUH C(50)
        // </summary>
        [FieldName("FIO_GLBUH"), FieldType('C'), FieldWidth(50)]
        public string Fio_glbuh
        {
            get { return fio_glbuh; }
            set { CheckStringData("Fio_glbuh", value, 50); fio_glbuh = value; }
        }

        private string fio_kass;
        // <summary>
        // FIO_KASS C(50)
        // </summary>
        [FieldName("FIO_KASS"), FieldType('C'), FieldWidth(50)]
        public string Fio_kass
        {
            get { return fio_kass; }
            set { CheckStringData("Fio_kass", value, 50); fio_kass = value; }
        }

        private string dolj_rukov;
        // <summary>
        // DOLJ_RUKOV C(50)
        // </summary>
        [FieldName("DOLJ_RUKOV"), FieldType('C'), FieldWidth(50)]
        public string Dolj_rukov
        {
            get { return dolj_rukov; }
            set { CheckStringData("Dolj_rukov", value, 50); dolj_rukov = value; }
        }

        private Int64 panelview;
        // <summary>
        // PANELVIEW N(2)
        // </summary>
        [FieldName("PANELVIEW"), FieldType('N'), FieldWidth(2)]
        public Int64 Panelview
        {
            get { return panelview; }
            set { CheckIntegerData("Panelview", value, 2); panelview = value; }
        }

        private bool corners;
        // <summary>
        // CORNERS L(1)
        // </summary>
        [FieldName("CORNERS"), FieldType('L'), FieldWidth(1)]
        public bool Corners
        {
            get { return corners; }
            set {  corners = value; }
        }

        private Int64 hint;
        // <summary>
        // HINT N(2)
        // </summary>
        [FieldName("HINT"), FieldType('N'), FieldWidth(2)]
        public Int64 Hint
        {
            get { return hint; }
            set { CheckIntegerData("Hint", value, 2); hint = value; }
        }

        private Int64 flag_close;
        // <summary>
        // FLAG_CLOSE N(3)
        // </summary>
        [FieldName("FLAG_CLOSE"), FieldType('N'), FieldWidth(3)]
        public Int64 Flag_close
        {
            get { return flag_close; }
            set { CheckIntegerData("Flag_close", value, 3); flag_close = value; }
        }

        private Int64 corner;
        // <summary>
        // CORNER N(2)
        // </summary>
        [FieldName("CORNER"), FieldType('N'), FieldWidth(2)]
        public Int64 Corner
        {
            get { return corner; }
            set { CheckIntegerData("Corner", value, 2); corner = value; }
        }

        private Int64 colorhint;
        // <summary>
        // COLORHINT N(16)
        // </summary>
        [FieldName("COLORHINT"), FieldType('N'), FieldWidth(16)]
        public Int64 Colorhint
        {
            get { return colorhint; }
            set { CheckIntegerData("Colorhint", value, 16); colorhint = value; }
        }

        private decimal peni;
        // <summary>
        // PENI N(10,5)
        // </summary>
        [FieldName("PENI"), FieldType('N'), FieldWidth(10), FieldDec(5)]
        public decimal Peni
        {
            get { return peni; }
            set { CheckDecimalData("Peni", value, 10, 5); peni = value; }
        }

        private bool colorbut;
        // <summary>
        // COLORBUT L(1)
        // </summary>
        [FieldName("COLORBUT"), FieldType('L'), FieldWidth(1)]
        public bool Colorbut
        {
            get { return colorbut; }
            set {  colorbut = value; }
        }

        private Int64 colorwin;
        // <summary>
        // COLORWIN N(16)
        // </summary>
        [FieldName("COLORWIN"), FieldType('N'), FieldWidth(16)]
        public Int64 Colorwin
        {
            get { return colorwin; }
            set { CheckIntegerData("Colorwin", value, 16); colorwin = value; }
        }

        private bool msgbold;
        // <summary>
        // MSGBOLD L(1)
        // </summary>
        [FieldName("MSGBOLD"), FieldType('L'), FieldWidth(1)]
        public bool Msgbold
        {
            get { return msgbold; }
            set {  msgbold = value; }
        }

        private string path;
        // <summary>
        // PATH C(250)
        // </summary>
        [FieldName("PATH"), FieldType('C'), FieldWidth(250)]
        public string Path
        {
            get { return path; }
            set { CheckStringData("Path", value, 250); path = value; }
        }

        private Int64 flagk;
        // <summary>
        // FLAGK N(2)
        // </summary>
        [FieldName("FLAGK"), FieldType('N'), FieldWidth(2)]
        public Int64 Flagk
        {
            get { return flagk; }
            set { CheckIntegerData("Flagk", value, 2); flagk = value; }
        }

        private Int64 flagd;
        // <summary>
        // FLAGD N(2)
        // </summary>
        [FieldName("FLAGD"), FieldType('N'), FieldWidth(2)]
        public Int64 Flagd
        {
            get { return flagd; }
            set { CheckIntegerData("Flagd", value, 2); flagd = value; }
        }

        private Int64 panelor;
        // <summary>
        // PANELOR N(2)
        // </summary>
        [FieldName("PANELOR"), FieldType('N'), FieldWidth(2)]
        public Int64 Panelor
        {
            get { return panelor; }
            set { CheckIntegerData("Panelor", value, 2); panelor = value; }
        }

        private Int64 prints;
        // <summary>
        // PRINTS N(2)
        // </summary>
        [FieldName("PRINTS"), FieldType('N'), FieldWidth(2)]
        public Int64 Prints
        {
            get { return prints; }
            set { CheckIntegerData("Prints", value, 2); prints = value; }
        }

        private Int64 rs1;
        // <summary>
        // RS1 N(2)
        // </summary>
        [FieldName("RS1"), FieldType('N'), FieldWidth(2)]
        public Int64 Rs1
        {
            get { return rs1; }
            set { CheckIntegerData("Rs1", value, 2); rs1 = value; }
        }

        private Int64 rs2;
        // <summary>
        // RS2 N(2)
        // </summary>
        [FieldName("RS2"), FieldType('N'), FieldWidth(2)]
        public Int64 Rs2
        {
            get { return rs2; }
            set { CheckIntegerData("Rs2", value, 2); rs2 = value; }
        }

        private Int64 listl;
        // <summary>
        // LISTL N(4)
        // </summary>
        [FieldName("LISTL"), FieldType('N'), FieldWidth(4)]
        public Int64 Listl
        {
            get { return listl; }
            set { CheckIntegerData("Listl", value, 4); listl = value; }
        }

        private bool tabdrag;
        // <summary>
        // TABDRAG L(1)
        // </summary>
        [FieldName("TABDRAG"), FieldType('L'), FieldWidth(1)]
        public bool Tabdrag
        {
            get { return tabdrag; }
            set {  tabdrag = value; }
        }

        private Int64 kol_mes;
        // <summary>
        // KOL_MES N(6)
        // </summary>
        [FieldName("KOL_MES"), FieldType('N'), FieldWidth(6)]
        public Int64 Kol_mes
        {
            get { return kol_mes; }
            set { CheckIntegerData("Kol_mes", value, 6); kol_mes = value; }
        }

        private Int64 panelhv;
        // <summary>
        // PANELHV N(2)
        // </summary>
        [FieldName("PANELHV"), FieldType('N'), FieldWidth(2)]
        public Int64 Panelhv
        {
            get { return panelhv; }
            set { CheckIntegerData("Panelhv", value, 2); panelhv = value; }
        }

        private Int64 panellen;
        // <summary>
        // PANELLEN N(6)
        // </summary>
        [FieldName("PANELLEN"), FieldType('N'), FieldWidth(6)]
        public Int64 Panellen
        {
            get { return panellen; }
            set { CheckIntegerData("Panellen", value, 6); panellen = value; }
        }

        private Int64 panelh;
        // <summary>
        // PANELH N(6)
        // </summary>
        [FieldName("PANELH"), FieldType('N'), FieldWidth(6)]
        public Int64 Panelh
        {
            get { return panelh; }
            set { CheckIntegerData("Panelh", value, 6); panelh = value; }
        }

        private Int64 import;
        // <summary>
        // IMPORT N(4)
        // </summary>
        [FieldName("IMPORT"), FieldType('N'), FieldWidth(4)]
        public Int64 Import
        {
            get { return import; }
            set { CheckIntegerData("Import", value, 4); import = value; }
        }

        private string recv_or;
        // <summary>
        // RECV_OR C(80)
        // </summary>
        [FieldName("RECV_OR"), FieldType('C'), FieldWidth(80)]
        public string Recv_or
        {
            get { return recv_or; }
            set { CheckStringData("Recv_or", value, 80); recv_or = value; }
        }

        private Int64 codenp;
        // <summary>
        // CODENP N(6)
        // </summary>
        [FieldName("CODENP"), FieldType('N'), FieldWidth(6)]
        public Int64 Codenp
        {
            get { return codenp; }
            set { CheckIntegerData("Codenp", value, 6); codenp = value; }
        }

        private bool checkdate;
        // <summary>
        // CHECKDATE L(1)
        // </summary>
        [FieldName("CHECKDATE"), FieldType('L'), FieldWidth(1)]
        public bool Checkdate
        {
            get { return checkdate; }
            set {  checkdate = value; }
        }

        private Int64 sum_saldo;
        // <summary>
        // SUM_SALDO N(2)
        // </summary>
        [FieldName("SUM_SALDO"), FieldType('N'), FieldWidth(2)]
        public Int64 Sum_saldo
        {
            get { return sum_saldo; }
            set { CheckIntegerData("Sum_saldo", value, 2); sum_saldo = value; }
        }

        private Int64 post;
        // <summary>
        // POST N(2)
        // </summary>
        [FieldName("POST"), FieldType('N'), FieldWidth(2)]
        public Int64 Post
        {
            get { return post; }
            set { CheckIntegerData("Post", value, 2); post = value; }
        }

        private string fio_pasp;
        // <summary>
        // FIO_PASP C(50)
        // </summary>
        [FieldName("FIO_PASP"), FieldType('C'), FieldWidth(50)]
        public string Fio_pasp
        {
            get { return fio_pasp; }
            set { CheckStringData("Fio_pasp", value, 50); fio_pasp = value; }
        }

        private string dpi;
        // <summary>
        // DPI C(10)
        // </summary>
        [FieldName("DPI"), FieldType('C'), FieldWidth(10)]
        public string Dpi
        {
            get { return dpi; }
            set { CheckStringData("Dpi", value, 10); dpi = value; }
        }

        private string disk;
        // <summary>
        // DISK C(10)
        // </summary>
        [FieldName("DISK"), FieldType('C'), FieldWidth(10)]
        public string Disk
        {
            get { return disk; }
            set { CheckStringData("Disk", value, 10); disk = value; }
        }

        private string version;
        // <summary>
        // VERSION C(10)
        // </summary>
        [FieldName("VERSION"), FieldType('C'), FieldWidth(10)]
        public string Version
        {
            get { return version; }
            set { CheckStringData("Version", value, 10); version = value; }
        }

        private bool flagsr;
        // <summary>
        // FLAGSR L(1)
        // </summary>
        [FieldName("FLAGSR"), FieldType('L'), FieldWidth(1)]
        public bool Flagsr
        {
            get { return flagsr; }
            set {  flagsr = value; }
        }

        private Int64 flagsl;
        // <summary>
        // FLAGSL N(2)
        // </summary>
        [FieldName("FLAGSL"), FieldType('N'), FieldWidth(2)]
        public Int64 Flagsl
        {
            get { return flagsl; }
            set { CheckIntegerData("Flagsl", value, 2); flagsl = value; }
        }

        private Int64 lenlg;
        // <summary>
        // LENLG N(4)
        // </summary>
        [FieldName("LENLG"), FieldType('N'), FieldWidth(4)]
        public Int64 Lenlg
        {
            get { return lenlg; }
            set { CheckIntegerData("Lenlg", value, 4); lenlg = value; }
        }

        private string pathpu;
        // <summary>
        // PATHPU C(100)
        // </summary>
        [FieldName("PATHPU"), FieldType('C'), FieldWidth(100)]
        public string Pathpu
        {
            get { return pathpu; }
            set { CheckStringData("Pathpu", value, 100); pathpu = value; }
        }

        private string pathnov;
        // <summary>
        // PATHNOV C(100)
        // </summary>
        [FieldName("PATHNOV"), FieldType('C'), FieldWidth(100)]
        public string Pathnov
        {
            get { return pathnov; }
            set { CheckStringData("Pathnov", value, 100); pathnov = value; }
        }

        private bool cnt;
        // <summary>
        // CNT L(1)
        // </summary>
        [FieldName("CNT"), FieldType('L'), FieldWidth(1)]
        public bool Cnt
        {
            get { return cnt; }
            set {  cnt = value; }
        }

        private string kas_s;
        // <summary>
        // KAS_S C(10)
        // </summary>
        [FieldName("KAS_S"), FieldType('C'), FieldWidth(10)]
        public string Kas_s
        {
            get { return kas_s; }
            set { CheckStringData("Kas_s", value, 10); kas_s = value; }
        }

        private Int64 mod_saldo;
        // <summary>
        // MOD_SALDO N(2)
        // </summary>
        [FieldName("MOD_SALDO"), FieldType('N'), FieldWidth(2)]
        public Int64 Mod_saldo
        {
            get { return mod_saldo; }
            set { CheckIntegerData("Mod_saldo", value, 2); mod_saldo = value; }
        }

        private Int64 lensp;
        // <summary>
        // LENSP N(4)
        // </summary>
        [FieldName("LENSP"), FieldType('N'), FieldWidth(4)]
        public Int64 Lensp
        {
            get { return lensp; }
            set { CheckIntegerData("Lensp", value, 4); lensp = value; }
        }

        private Int64 flagex;
        // <summary>
        // FLAGEX N(2)
        // </summary>
        [FieldName("FLAGEX"), FieldType('N'), FieldWidth(2)]
        public Int64 Flagex
        {
            get { return flagex; }
            set { CheckIntegerData("Flagex", value, 2); flagex = value; }
        }

        private DateTime datj1;
        // <summary>
        // DATJ1 D(8)
        // </summary>
        [FieldName("DATJ1"), FieldType('D'), FieldWidth(8)]
        public DateTime Datj1
        {
            get { return datj1; }
            set {  datj1 = value; }
        }

        private DateTime datj2;
        // <summary>
        // DATJ2 D(8)
        // </summary>
        [FieldName("DATJ2"), FieldType('D'), FieldWidth(8)]
        public DateTime Datj2
        {
            get { return datj2; }
            set {  datj2 = value; }
        }

        private Int64 nppl;
        // <summary>
        // NPPL N(3)
        // </summary>
        [FieldName("NPPL"), FieldType('N'), FieldWidth(3)]
        public Int64 Nppl
        {
            get { return nppl; }
            set { CheckIntegerData("Nppl", value, 3); nppl = value; }
        }

        private Int64 blockusl;
        // <summary>
        // BLOCKUSL N(2)
        // </summary>
        [FieldName("BLOCKUSL"), FieldType('N'), FieldWidth(2)]
        public Int64 Blockusl
        {
            get { return blockusl; }
            set { CheckIntegerData("Blockusl", value, 2); blockusl = value; }
        }

        private Int64 blocklgot;
        // <summary>
        // BLOCKLGOT N(2)
        // </summary>
        [FieldName("BLOCKLGOT"), FieldType('N'), FieldWidth(2)]
        public Int64 Blocklgot
        {
            get { return blocklgot; }
            set { CheckIntegerData("Blocklgot", value, 2); blocklgot = value; }
        }

        private string decsep;
        // <summary>
        // DECSEP C(1)
        // </summary>
        [FieldName("DECSEP"), FieldType('C'), FieldWidth(1)]
        public string Decsep
        {
            get { return decsep; }
            set { CheckStringData("Decsep", value, 1); decsep = value; }
        }

        private string decdig;
        // <summary>
        // DECDIG C(1)
        // </summary>
        [FieldName("DECDIG"), FieldType('C'), FieldWidth(1)]
        public string Decdig
        {
            get { return decdig; }
            set { CheckStringData("Decdig", value, 1); decdig = value; }
        }

        private bool decexcel;
        // <summary>
        // DECEXCEL L(1)
        // </summary>
        [FieldName("DECEXCEL"), FieldType('L'), FieldWidth(1)]
        public bool Decexcel
        {
            get { return decexcel; }
            set {  decexcel = value; }
        }

        private Int64 polis;
        // <summary>
        // POLIS N(2)
        // </summary>
        [FieldName("POLIS"), FieldType('N'), FieldWidth(2)]
        public Int64 Polis
        {
            get { return polis; }
            set { CheckIntegerData("Polis", value, 2); polis = value; }
        }

        private Int64 lszap;
        // <summary>
        // LSZAP N(7)
        // </summary>
        [FieldName("LSZAP"), FieldType('N'), FieldWidth(7)]
        public Int64 Lszap
        {
            get { return lszap; }
            set { CheckIntegerData("Lszap", value, 7); lszap = value; }
        }

        private Int64 delsal;
        // <summary>
        // DELSAL N(2)
        // </summary>
        [FieldName("DELSAL"), FieldType('N'), FieldWidth(2)]
        public Int64 Delsal
        {
            get { return delsal; }
            set { CheckIntegerData("Delsal", value, 2); delsal = value; }
        }

        private string pathothe;
        // <summary>
        // PATHOTHE C(50)
        // </summary>
        [FieldName("PATHOTHE"), FieldType('C'), FieldWidth(50)]
        public string Pathothe
        {
            get { return pathothe; }
            set { CheckStringData("Pathothe", value, 50); pathothe = value; }
        }

        private Int64 fiol;
        // <summary>
        // FIOL N(3)
        // </summary>
        [FieldName("FIOL"), FieldType('N'), FieldWidth(3)]
        public Int64 Fiol
        {
            get { return fiol; }
            set { CheckIntegerData("Fiol", value, 3); fiol = value; }
        }

        private Int64 adrl;
        // <summary>
        // ADRL N(3)
        // </summary>
        [FieldName("ADRL"), FieldType('N'), FieldWidth(3)]
        public Int64 Adrl
        {
            get { return adrl; }
            set { CheckIntegerData("Adrl", value, 3); adrl = value; }
        }

        private Int64 kval;
        // <summary>
        // KVAL N(3)
        // </summary>
        [FieldName("KVAL"), FieldType('N'), FieldWidth(3)]
        public Int64 Kval
        {
            get { return kval; }
            set { CheckIntegerData("Kval", value, 3); kval = value; }
        }

        private Int64 pril;
        // <summary>
        // PRIL N(3)
        // </summary>
        [FieldName("PRIL"), FieldType('N'), FieldWidth(3)]
        public Int64 Pril
        {
            get { return pril; }
            set { CheckIntegerData("Pril", value, 3); pril = value; }
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

        private Int64 plol;
        // <summary>
        // PLOL N(3)
        // </summary>
        [FieldName("PLOL"), FieldType('N'), FieldWidth(3)]
        public Int64 Plol
        {
            get { return plol; }
            set { CheckIntegerData("Plol", value, 3); plol = value; }
        }

        private Int64 mesl;
        // <summary>
        // MESL N(3)
        // </summary>
        [FieldName("MESL"), FieldType('N'), FieldWidth(3)]
        public Int64 Mesl
        {
            get { return mesl; }
            set { CheckIntegerData("Mesl", value, 3); mesl = value; }
        }

        private Int64 nacl;
        // <summary>
        // NACL N(3)
        // </summary>
        [FieldName("NACL"), FieldType('N'), FieldWidth(3)]
        public Int64 Nacl
        {
            get { return nacl; }
            set { CheckIntegerData("Nacl", value, 3); nacl = value; }
        }

        private Int64 penl;
        // <summary>
        // PENL N(3)
        // </summary>
        [FieldName("PENL"), FieldType('N'), FieldWidth(3)]
        public Int64 Penl
        {
            get { return penl; }
            set { CheckIntegerData("Penl", value, 3); penl = value; }
        }

        private Int64 doll;
        // <summary>
        // DOLL N(3)
        // </summary>
        [FieldName("DOLL"), FieldType('N'), FieldWidth(3)]
        public Int64 Doll
        {
            get { return doll; }
            set { CheckIntegerData("Doll", value, 3); doll = value; }
        }

        private Int64 rabl;
        // <summary>
        // RABL N(3)
        // </summary>
        [FieldName("RABL"), FieldType('N'), FieldWidth(3)]
        public Int64 Rabl
        {
            get { return rabl; }
            set { CheckIntegerData("Rabl", value, 3); rabl = value; }
        }

        private Int64 km_sub;
        // <summary>
        // KM_SUB N(3)
        // </summary>
        [FieldName("KM_SUB"), FieldType('N'), FieldWidth(3)]
        public Int64 Km_sub
        {
            get { return km_sub; }
            set { CheckIntegerData("Km_sub", value, 3); km_sub = value; }
        }

        private Int64 ko_sub;
        // <summary>
        // KO_SUB N(6)
        // </summary>
        [FieldName("KO_SUB"), FieldType('N'), FieldWidth(6)]
        public Int64 Ko_sub
        {
            get { return ko_sub; }
            set { CheckIntegerData("Ko_sub", value, 6); ko_sub = value; }
        }

        private Int64 n_uc;
        // <summary>
        // N_UC N(2)
        // </summary>
        [FieldName("N_UC"), FieldType('N'), FieldWidth(2)]
        public Int64 N_uc
        {
            get { return n_uc; }
            set { CheckIntegerData("N_uc", value, 2); n_uc = value; }
        }

        private Int64 cod_org;
        // <summary>
        // COD_ORG N(6)
        // </summary>
        [FieldName("COD_ORG"), FieldType('N'), FieldWidth(6)]
        public Int64 Cod_org
        {
            get { return cod_org; }
            set { CheckIntegerData("Cod_org", value, 6); cod_org = value; }
        }

        private string pathkor;
        // <summary>
        // PATHKOR C(50)
        // </summary>
        [FieldName("PATHKOR"), FieldType('C'), FieldWidth(50)]
        public string Pathkor
        {
            get { return pathkor; }
            set { CheckStringData("Pathkor", value, 50); pathkor = value; }
        }

        private Int64 varbank;
        // <summary>
        // VARBANK N(3)
        // </summary>
        [FieldName("VARBANK"), FieldType('N'), FieldWidth(3)]
        public Int64 Varbank
        {
            get { return varbank; }
            set { CheckIntegerData("Varbank", value, 3); varbank = value; }
        }

        private Int64 kodsub;
        // <summary>
        // KODSUB N(4)
        // </summary>
        [FieldName("KODSUB"), FieldType('N'), FieldWidth(4)]
        public Int64 Kodsub
        {
            get { return kodsub; }
            set { CheckIntegerData("Kodsub", value, 4); kodsub = value; }
        }

        private string gorod;
        // <summary>
        // GOROD C(50)
        // </summary>
        [FieldName("GOROD"), FieldType('C'), FieldWidth(50)]
        public string Gorod
        {
            get { return gorod; }
            set { CheckStringData("Gorod", value, 50); gorod = value; }
        }

        private string addres;
        // <summary>
        // ADDRES C(150)
        // </summary>
        [FieldName("ADDRES"), FieldType('C'), FieldWidth(150)]
        public string Addres
        {
            get { return addres; }
            set { CheckStringData("Addres", value, 150); addres = value; }
        }

        private string pathbank;
        // <summary>
        // PATHBANK C(100)
        // </summary>
        [FieldName("PATHBANK"), FieldType('C'), FieldWidth(100)]
        public string Pathbank
        {
            get { return pathbank; }
            set { CheckStringData("Pathbank", value, 100); pathbank = value; }
        }

        private string otf3;
        // <summary>
        // OTF3 C(3)
        // </summary>
        [FieldName("OTF3"), FieldType('C'), FieldWidth(3)]
        public string Otf3
        {
            get { return otf3; }
            set { CheckStringData("Otf3", value, 3); otf3 = value; }
        }

        private bool regon1;
        // <summary>
        // REGON1 L(1)
        // </summary>
        [FieldName("REGON1"), FieldType('L'), FieldWidth(1)]
        public bool Regon1
        {
            get { return regon1; }
            set {  regon1 = value; }
        }

        private Int64 regon2;
        // <summary>
        // REGON2 N(2)
        // </summary>
        [FieldName("REGON2"), FieldType('N'), FieldWidth(2)]
        public Int64 Regon2
        {
            get { return regon2; }
            set { CheckIntegerData("Regon2", value, 2); regon2 = value; }
        }

        private bool regon3;
        // <summary>
        // REGON3 L(1)
        // </summary>
        [FieldName("REGON3"), FieldType('L'), FieldWidth(1)]
        public bool Regon3
        {
            get { return regon3; }
            set {  regon3 = value; }
        }

        private bool regon4;
        // <summary>
        // REGON4 L(1)
        // </summary>
        [FieldName("REGON4"), FieldType('L'), FieldWidth(1)]
        public bool Regon4
        {
            get { return regon4; }
            set {  regon4 = value; }
        }

        private Int64 regon5;
        // <summary>
        // REGON5 N(2)
        // </summary>
        [FieldName("REGON5"), FieldType('N'), FieldWidth(2)]
        public Int64 Regon5
        {
            get { return regon5; }
            set { CheckIntegerData("Regon5", value, 2); regon5 = value; }
        }

        private bool regoff1;
        // <summary>
        // REGOFF1 L(1)
        // </summary>
        [FieldName("REGOFF1"), FieldType('L'), FieldWidth(1)]
        public bool Regoff1
        {
            get { return regoff1; }
            set {  regoff1 = value; }
        }

        private Int64 regoff2;
        // <summary>
        // REGOFF2 N(2)
        // </summary>
        [FieldName("REGOFF2"), FieldType('N'), FieldWidth(2)]
        public Int64 Regoff2
        {
            get { return regoff2; }
            set { CheckIntegerData("Regoff2", value, 2); regoff2 = value; }
        }

        private bool regoff3;
        // <summary>
        // REGOFF3 L(1)
        // </summary>
        [FieldName("REGOFF3"), FieldType('L'), FieldWidth(1)]
        public bool Regoff3
        {
            get { return regoff3; }
            set {  regoff3 = value; }
        }

        private bool regoff4;
        // <summary>
        // REGOFF4 L(1)
        // </summary>
        [FieldName("REGOFF4"), FieldType('L'), FieldWidth(1)]
        public bool Regoff4
        {
            get { return regoff4; }
            set {  regoff4 = value; }
        }

        private bool regask;
        // <summary>
        // REGASK L(1)
        // </summary>
        [FieldName("REGASK"), FieldType('L'), FieldWidth(1)]
        public bool Regask
        {
            get { return regask; }
            set {  regask = value; }
        }

        private string fio_dir2;
        // <summary>
        // FIO_DIR2 C(50)
        // </summary>
        [FieldName("FIO_DIR2"), FieldType('C'), FieldWidth(50)]
        public string Fio_dir2
        {
            get { return fio_dir2; }
            set { CheckStringData("Fio_dir2", value, 50); fio_dir2 = value; }
        }

        private string nd_spec;
        // <summary>
        // ND_SPEC C(50)
        // </summary>
        [FieldName("ND_SPEC"), FieldType('C'), FieldWidth(50)]
        public string Nd_spec
        {
            get { return nd_spec; }
            set { CheckStringData("Nd_spec", value, 50); nd_spec = value; }
        }

        private string nd_pasp;
        // <summary>
        // ND_PASP C(50)
        // </summary>
        [FieldName("ND_PASP"), FieldType('C'), FieldWidth(50)]
        public string Nd_pasp
        {
            get { return nd_pasp; }
            set { CheckStringData("Nd_pasp", value, 50); nd_pasp = value; }
        }

        private string adres;
        // <summary>
        // ADRES C(50)
        // </summary>
        [FieldName("ADRES"), FieldType('C'), FieldWidth(50)]
        public string Adres
        {
            get { return adres; }
            set { CheckStringData("Adres", value, 50); adres = value; }
        }

        private Int64 codlgot;
        // <summary>
        // CODLGOT N(4)
        // </summary>
        [FieldName("CODLGOT"), FieldType('N'), FieldWidth(4)]
        public Int64 Codlgot
        {
            get { return codlgot; }
            set { CheckIntegerData("Codlgot", value, 4); codlgot = value; }
        }

        private Int64 kvitok;
        // <summary>
        // KVITOK N(4)
        // </summary>
        [FieldName("KVITOK"), FieldType('N'), FieldWidth(4)]
        public Int64 Kvitok
        {
            get { return kvitok; }
            set { CheckIntegerData("Kvitok", value, 4); kvitok = value; }
        }

        private string rspeni;
        // <summary>
        // RSPENI C(10)
        // </summary>
        [FieldName("RSPENI"), FieldType('C'), FieldWidth(10)]
        public string Rspeni
        {
            get { return rspeni; }
            set { CheckStringData("Rspeni", value, 10); rspeni = value; }
        }

        private Int64 numsoc;
        // <summary>
        // NUMSOC N(2)
        // </summary>
        [FieldName("NUMSOC"), FieldType('N'), FieldWidth(2)]
        public Int64 Numsoc
        {
            get { return numsoc; }
            set { CheckIntegerData("Numsoc", value, 2); numsoc = value; }
        }

        private string pathsoc;
        // <summary>
        // PATHSOC C(50)
        // </summary>
        [FieldName("PATHSOC"), FieldType('C'), FieldWidth(50)]
        public string Pathsoc
        {
            get { return pathsoc; }
            set { CheckStringData("Pathsoc", value, 50); pathsoc = value; }
        }

        private string ogrn;
        // <summary>
        // OGRN C(20)
        // </summary>
        [FieldName("OGRN"), FieldType('C'), FieldWidth(20)]
        public string Ogrn
        {
            get { return ogrn; }
            set { CheckStringData("Ogrn", value, 20); ogrn = value; }
        }

        private Int64 sr_height;
        // <summary>
        // SR_HEIGHT N(5)
        // </summary>
        [FieldName("SR_HEIGHT"), FieldType('N'), FieldWidth(5)]
        public Int64 Sr_height
        {
            get { return sr_height; }
            set { CheckIntegerData("Sr_height", value, 5); sr_height = value; }
        }

        private Int64 sr_heights;
        // <summary>
        // SR_HEIGHTS N(6)
        // </summary>
        [FieldName("SR_HEIGHTS"), FieldType('N'), FieldWidth(6)]
        public Int64 Sr_heights
        {
            get { return sr_heights; }
            set { CheckIntegerData("Sr_heights", value, 6); sr_heights = value; }
        }

        private string prefix;
        // <summary>
        // PREFIX C(50)
        // </summary>
        [FieldName("PREFIX"), FieldType('C'), FieldWidth(50)]
        public string Prefix
        {
            get { return prefix; }
            set { CheckStringData("Prefix", value, 50); prefix = value; }
        }

        private Int64 size_f3;
        // <summary>
        // SIZE_F3 N(4)
        // </summary>
        [FieldName("SIZE_F3"), FieldType('N'), FieldWidth(4)]
        public Int64 Size_f3
        {
            get { return size_f3; }
            set { CheckIntegerData("Size_f3", value, 4); size_f3 = value; }
        }

        private Int64 orient_f3;
        // <summary>
        // ORIENT_F3 N(2)
        // </summary>
        [FieldName("ORIENT_F3"), FieldType('N'), FieldWidth(2)]
        public Int64 Orient_f3
        {
            get { return orient_f3; }
            set { CheckIntegerData("Orient_f3", value, 2); orient_f3 = value; }
        }

        private string srifts_cf3;
        // <summary>
        // SRIFTS_CF3 C(50)
        // </summary>
        [FieldName("SRIFTS_CF3"), FieldType('C'), FieldWidth(50)]
        public string Srifts_cf3
        {
            get { return srifts_cf3; }
            set { CheckStringData("Srifts_cf3", value, 50); srifts_cf3 = value; }
        }

        private Int64 size_cf3;
        // <summary>
        // SIZE_CF3 N(4)
        // </summary>
        [FieldName("SIZE_CF3"), FieldType('N'), FieldWidth(4)]
        public Int64 Size_cf3
        {
            get { return size_cf3; }
            set { CheckIntegerData("Size_cf3", value, 4); size_cf3 = value; }
        }

        private Int64 orient_cf3;
        // <summary>
        // ORIENT_CF3 N(2)
        // </summary>
        [FieldName("ORIENT_CF3"), FieldType('N'), FieldWidth(2)]
        public Int64 Orient_cf3
        {
            get { return orient_cf3; }
            set { CheckIntegerData("Orient_cf3", value, 2); orient_cf3 = value; }
        }

        private bool matrix_pr;
        // <summary>
        // MATRIX_PR L(1)
        // </summary>
        [FieldName("MATRIX_PR"), FieldType('L'), FieldWidth(1)]
        public bool Matrix_pr
        {
            get { return matrix_pr; }
            set {  matrix_pr = value; }
        }

        private bool dialog_pr;
        // <summary>
        // DIALOG_PR L(1)
        // </summary>
        [FieldName("DIALOG_PR"), FieldType('L'), FieldWidth(1)]
        public bool Dialog_pr
        {
            get { return dialog_pr; }
            set {  dialog_pr = value; }
        }

        private Int64 main_usl;
        // <summary>
        // MAIN_USL N(4)
        // </summary>
        [FieldName("MAIN_USL"), FieldType('N'), FieldWidth(4)]
        public Int64 Main_usl
        {
            get { return main_usl; }
            set { CheckIntegerData("Main_usl", value, 4); main_usl = value; }
        }

        private DateTime datrif1;
        // <summary>
        // DATRIF1 D(8)
        // </summary>
        [FieldName("DATRIF1"), FieldType('D'), FieldWidth(8)]
        public DateTime Datrif1
        {
            get { return datrif1; }
            set {  datrif1 = value; }
        }

        private DateTime datrif2;
        // <summary>
        // DATRIF2 D(8)
        // </summary>
        [FieldName("DATRIF2"), FieldType('D'), FieldWidth(8)]
        public DateTime Datrif2
        {
            get { return datrif2; }
            set {  datrif2 = value; }
        }

        private Int64 lastpost;
        // <summary>
        // LASTPOST N(6)
        // </summary>
        [FieldName("LASTPOST"), FieldType('N'), FieldWidth(6)]
        public Int64 Lastpost
        {
            get { return lastpost; }
            set { CheckIntegerData("Lastpost", value, 6); lastpost = value; }
        }

        private string rec1;
        // <summary>
        // REC1 C(100)
        // </summary>
        [FieldName("REC1"), FieldType('C'), FieldWidth(100)]
        public string Rec1
        {
            get { return rec1; }
            set { CheckStringData("Rec1", value, 100); rec1 = value; }
        }

        private string rec2;
        // <summary>
        // REC2 C(100)
        // </summary>
        [FieldName("REC2"), FieldType('C'), FieldWidth(100)]
        public string Rec2
        {
            get { return rec2; }
            set { CheckStringData("Rec2", value, 100); rec2 = value; }
        }

        private string rec3;
        // <summary>
        // REC3 C(100)
        // </summary>
        [FieldName("REC3"), FieldType('C'), FieldWidth(100)]
        public string Rec3
        {
            get { return rec3; }
            set { CheckStringData("Rec3", value, 100); rec3 = value; }
        }

        private bool pertar;
        // <summary>
        // PERTAR L(1)
        // </summary>
        [FieldName("PERTAR"), FieldType('L'), FieldWidth(1)]
        public bool Pertar
        {
            get { return pertar; }
            set {  pertar = value; }
        }

        private Int64 flagu1;
        // <summary>
        // FLAGU1 N(4)
        // </summary>
        [FieldName("FLAGU1"), FieldType('N'), FieldWidth(4)]
        public Int64 Flagu1
        {
            get { return flagu1; }
            set { CheckIntegerData("Flagu1", value, 4); flagu1 = value; }
        }

        private Int64 flagu2;
        // <summary>
        // FLAGU2 N(4)
        // </summary>
        [FieldName("FLAGU2"), FieldType('N'), FieldWidth(4)]
        public Int64 Flagu2
        {
            get { return flagu2; }
            set { CheckIntegerData("Flagu2", value, 4); flagu2 = value; }
        }

        private Int64 flagu3;
        // <summary>
        // FLAGU3 N(4)
        // </summary>
        [FieldName("FLAGU3"), FieldType('N'), FieldWidth(4)]
        public Int64 Flagu3
        {
            get { return flagu3; }
            set { CheckIntegerData("Flagu3", value, 4); flagu3 = value; }
        }

        private string bank;
        // <summary>
        // BANK C(60)
        // </summary>
        [FieldName("BANK"), FieldType('C'), FieldWidth(60)]
        public string Bank
        {
            get { return bank; }
            set { CheckStringData("Bank", value, 60); bank = value; }
        }

        private string ks;
        // <summary>
        // KS C(50)
        // </summary>
        [FieldName("KS"), FieldType('C'), FieldWidth(50)]
        public string Ks
        {
            get { return ks; }
            set { CheckStringData("Ks", value, 50); ks = value; }
        }

        private string bik;
        // <summary>
        // BIK C(50)
        // </summary>
        [FieldName("BIK"), FieldType('C'), FieldWidth(50)]
        public string Bik
        {
            get { return bik; }
            set { CheckStringData("Bik", value, 50); bik = value; }
        }

        private string rs;
        // <summary>
        // RS C(50)
        // </summary>
        [FieldName("RS"), FieldType('C'), FieldWidth(50)]
        public string Rs
        {
            get { return rs; }
            set { CheckStringData("Rs", value, 50); rs = value; }
        }

        private string inn;
        // <summary>
        // INN C(50)
        // </summary>
        [FieldName("INN"), FieldType('C'), FieldWidth(50)]
        public string Inn
        {
            get { return inn; }
            set { CheckStringData("Inn", value, 50); inn = value; }
        }

        private string name;
        // <summary>
        // NAME C(50)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        private string vid_work;
        // <summary>
        // VID_WORK C(70)
        // </summary>
        [FieldName("VID_WORK"), FieldType('C'), FieldWidth(70)]
        public string Vid_work
        {
            get { return vid_work; }
            set { CheckStringData("Vid_work", value, 70); vid_work = value; }
        }

        private string form_sobs;
        // <summary>
        // FORM_SOBS C(70)
        // </summary>
        [FieldName("FORM_SOBS"), FieldType('C'), FieldWidth(70)]
        public string Form_sobs
        {
            get { return form_sobs; }
            set { CheckStringData("Form_sobs", value, 70); form_sobs = value; }
        }

        private string kpp;
        // <summary>
        // KPP C(50)
        // </summary>
        [FieldName("KPP"), FieldType('C'), FieldWidth(50)]
        public string Kpp
        {
            get { return kpp; }
            set { CheckStringData("Kpp", value, 50); kpp = value; }
        }

        private string okonh;
        // <summary>
        // OKONH C(50)
        // </summary>
        [FieldName("OKONH"), FieldType('C'), FieldWidth(50)]
        public string Okonh
        {
            get { return okonh; }
            set { CheckStringData("Okonh", value, 50); okonh = value; }
        }

        private string okpo;
        // <summary>
        // OKPO C(50)
        // </summary>
        [FieldName("OKPO"), FieldType('C'), FieldWidth(50)]
        public string Okpo
        {
            get { return okpo; }
            set { CheckStringData("Okpo", value, 50); okpo = value; }
        }

        private string okdp;
        // <summary>
        // OKDP C(50)
        // </summary>
        [FieldName("OKDP"), FieldType('C'), FieldWidth(50)]
        public string Okdp
        {
            get { return okdp; }
            set { CheckStringData("Okdp", value, 50); okdp = value; }
        }

        private string okpf;
        // <summary>
        // OKPF C(50)
        // </summary>
        [FieldName("OKPF"), FieldType('C'), FieldWidth(50)]
        public string Okpf
        {
            get { return okpf; }
            set { CheckStringData("Okpf", value, 50); okpf = value; }
        }

        private string okfs;
        // <summary>
        // OKFS C(50)
        // </summary>
        [FieldName("OKFS"), FieldType('C'), FieldWidth(50)]
        public string Okfs
        {
            get { return okfs; }
            set { CheckStringData("Okfs", value, 50); okfs = value; }
        }

        private string index;
        // <summary>
        // INDEX C(6)
        // </summary>
        [FieldName("INDEX"), FieldType('C'), FieldWidth(6)]
        public string Index
        {
            get { return index; }
            set { CheckStringData("Index", value, 6); index = value; }
        }

        private string resp;
        // <summary>
        // RESP C(50)
        // </summary>
        [FieldName("RESP"), FieldType('C'), FieldWidth(50)]
        public string Resp
        {
            get { return resp; }
            set { CheckStringData("Resp", value, 50); resp = value; }
        }

        private string rayon;
        // <summary>
        // RAYON C(50)
        // </summary>
        [FieldName("RAYON"), FieldType('C'), FieldWidth(50)]
        public string Rayon
        {
            get { return rayon; }
            set { CheckStringData("Rayon", value, 50); rayon = value; }
        }

        private string street;
        // <summary>
        // STREET C(50)
        // </summary>
        [FieldName("STREET"), FieldType('C'), FieldWidth(50)]
        public string Street
        {
            get { return street; }
            set { CheckStringData("Street", value, 50); street = value; }
        }

        private string house;
        // <summary>
        // HOUSE C(10)
        // </summary>
        [FieldName("HOUSE"), FieldType('C'), FieldWidth(10)]
        public string House
        {
            get { return house; }
            set { CheckStringData("House", value, 10); house = value; }
        }

        private string balk;
        // <summary>
        // BALK C(10)
        // </summary>
        [FieldName("BALK"), FieldType('C'), FieldWidth(10)]
        public string Balk
        {
            get { return balk; }
            set { CheckStringData("Balk", value, 10); balk = value; }
        }

        private string construc;
        // <summary>
        // CONSTRUC C(10)
        // </summary>
        [FieldName("CONSTRUC"), FieldType('C'), FieldWidth(10)]
        public string Construc
        {
            get { return construc; }
            set { CheckStringData("Construc", value, 10); construc = value; }
        }

        private string flat;
        // <summary>
        // FLAT C(10)
        // </summary>
        [FieldName("FLAT"), FieldType('C'), FieldWidth(10)]
        public string Flat
        {
            get { return flat; }
            set { CheckStringData("Flat", value, 10); flat = value; }
        }

        private string codreg;
        // <summary>
        // CODREG C(2)
        // </summary>
        [FieldName("CODREG"), FieldType('C'), FieldWidth(2)]
        public string Codreg
        {
            get { return codreg; }
            set { CheckStringData("Codreg", value, 2); codreg = value; }
        }

        private Int64 sb_usl;
        // <summary>
        // SB_USL N(11)
        // </summary>
        [FieldName("SB_USL"), FieldType('N'), FieldWidth(11)]
        public Int64 Sb_usl
        {
            get { return sb_usl; }
            set { CheckIntegerData("Sb_usl", value, 11); sb_usl = value; }
        }

        private string sb_path;
        // <summary>
        // SB_PATH C(100)
        // </summary>
        [FieldName("SB_PATH"), FieldType('C'), FieldWidth(100)]
        public string Sb_path
        {
            get { return sb_path; }
            set { CheckStringData("Sb_path", value, 100); sb_path = value; }
        }

        private string out_path;
        // <summary>
        // OUT_PATH C(100)
        // </summary>
        [FieldName("OUT_PATH"), FieldType('C'), FieldWidth(100)]
        public string Out_path
        {
            get { return out_path; }
            set { CheckStringData("Out_path", value, 100); out_path = value; }
        }

        private string nameexel;
        // <summary>
        // NAMEEXEL C(40)
        // </summary>
        [FieldName("NAMEEXEL"), FieldType('C'), FieldWidth(40)]
        public string Nameexel
        {
            get { return nameexel; }
            set { CheckStringData("Nameexel", value, 40); nameexel = value; }
        }

        private string flagrs;
        // <summary>
        // FLAGRS C(10)
        // </summary>
        [FieldName("FLAGRS"), FieldType('C'), FieldWidth(10)]
        public string Flagrs
        {
            get { return flagrs; }
            set { CheckStringData("Flagrs", value, 10); flagrs = value; }
        }

        private bool fleng;
        // <summary>
        // FLENG L(1)
        // </summary>
        [FieldName("FLENG"), FieldType('L'), FieldWidth(1)]
        public bool Fleng
        {
            get { return fleng; }
            set {  fleng = value; }
        }

        private bool flrus;
        // <summary>
        // FLRUS L(1)
        // </summary>
        [FieldName("FLRUS"), FieldType('L'), FieldWidth(1)]
        public bool Flrus
        {
            get { return flrus; }
            set {  flrus = value; }
        }

        private string kassal;
        // <summary>
        // KASSAL C(254)
        // </summary>
        [FieldName("KASSAL"), FieldType('C'), FieldWidth(254)]
        public string Kassal
        {
            get { return kassal; }
            set { CheckStringData("Kassal", value, 254); kassal = value; }
        }

        private Int64 lenul;
        // <summary>
        // LENUL N(4)
        // </summary>
        [FieldName("LENUL"), FieldType('N'), FieldWidth(4)]
        public Int64 Lenul
        {
            get { return lenul; }
            set { CheckIntegerData("Lenul", value, 4); lenul = value; }
        }

        private bool closemsg;
        // <summary>
        // CLOSEMSG L(1)
        // </summary>
        [FieldName("CLOSEMSG"), FieldType('L'), FieldWidth(1)]
        public bool Closemsg
        {
            get { return closemsg; }
            set {  closemsg = value; }
        }

        private Int64 kmprzad;
        // <summary>
        // KMPRZAD N(4)
        // </summary>
        [FieldName("KMPRZAD"), FieldType('N'), FieldWidth(4)]
        public Int64 Kmprzad
        {
            get { return kmprzad; }
            set { CheckIntegerData("Kmprzad", value, 4); kmprzad = value; }
        }

        private Int64 kmplat;
        // <summary>
        // KMPLAT N(4)
        // </summary>
        [FieldName("KMPLAT"), FieldType('N'), FieldWidth(4)]
        public Int64 Kmplat
        {
            get { return kmplat; }
            set { CheckIntegerData("Kmplat", value, 4); kmplat = value; }
        }

        private Int64 kmcount;
        // <summary>
        // KMCOUNT N(4)
        // </summary>
        [FieldName("KMCOUNT"), FieldType('N'), FieldWidth(4)]
        public Int64 Kmcount
        {
            get { return kmcount; }
            set { CheckIntegerData("Kmcount", value, 4); kmcount = value; }
        }

        private bool outfio;
        // <summary>
        // OUTFIO L(1)
        // </summary>
        [FieldName("OUTFIO"), FieldType('L'), FieldWidth(1)]
        public bool Outfio
        {
            get { return outfio; }
            set {  outfio = value; }
        }

        private string usl_bank;
        // <summary>
        // USL_BANK C(100)
        // </summary>
        [FieldName("USL_BANK"), FieldType('C'), FieldWidth(100)]
        public string Usl_bank
        {
            get { return usl_bank; }
            set { CheckStringData("Usl_bank", value, 100); usl_bank = value; }
        }

        private bool fs_in;
        // <summary>
        // FS_IN L(1)
        // </summary>
        [FieldName("FS_IN"), FieldType('L'), FieldWidth(1)]
        public bool Fs_in
        {
            get { return fs_in; }
            set {  fs_in = value; }
        }

        private string soundin;
        // <summary>
        // SOUNDIN C(100)
        // </summary>
        [FieldName("SOUNDIN"), FieldType('C'), FieldWidth(100)]
        public string Soundin
        {
            get { return soundin; }
            set { CheckStringData("Soundin", value, 100); soundin = value; }
        }

        private bool fs_par;
        // <summary>
        // FS_PAR L(1)
        // </summary>
        [FieldName("FS_PAR"), FieldType('L'), FieldWidth(1)]
        public bool Fs_par
        {
            get { return fs_par; }
            set {  fs_par = value; }
        }

        private string soundpar;
        // <summary>
        // SOUNDPAR C(100)
        // </summary>
        [FieldName("SOUNDPAR"), FieldType('C'), FieldWidth(100)]
        public string Soundpar
        {
            get { return soundpar; }
            set { CheckStringData("Soundpar", value, 100); soundpar = value; }
        }

        private bool fs_ot;
        // <summary>
        // FS_OT L(1)
        // </summary>
        [FieldName("FS_OT"), FieldType('L'), FieldWidth(1)]
        public bool Fs_ot
        {
            get { return fs_ot; }
            set {  fs_ot = value; }
        }

        private string soundot;
        // <summary>
        // SOUNDOT C(100)
        // </summary>
        [FieldName("SOUNDOT"), FieldType('C'), FieldWidth(100)]
        public string Soundot
        {
            get { return soundot; }
            set { CheckStringData("Soundot", value, 100); soundot = value; }
        }

        private bool fs_opl;
        // <summary>
        // FS_OPL L(1)
        // </summary>
        [FieldName("FS_OPL"), FieldType('L'), FieldWidth(1)]
        public bool Fs_opl
        {
            get { return fs_opl; }
            set {  fs_opl = value; }
        }

        private string soundopl;
        // <summary>
        // SOUNDOPL C(100)
        // </summary>
        [FieldName("SOUNDOPL"), FieldType('C'), FieldWidth(100)]
        public string Soundopl
        {
            get { return soundopl; }
            set { CheckStringData("Soundopl", value, 100); soundopl = value; }
        }

        private bool fs_err;
        // <summary>
        // FS_ERR L(1)
        // </summary>
        [FieldName("FS_ERR"), FieldType('L'), FieldWidth(1)]
        public bool Fs_err
        {
            get { return fs_err; }
            set {  fs_err = value; }
        }

        private string sounderr;
        // <summary>
        // SOUNDERR C(100)
        // </summary>
        [FieldName("SOUNDERR"), FieldType('C'), FieldWidth(100)]
        public string Sounderr
        {
            get { return sounderr; }
            set { CheckStringData("Sounderr", value, 100); sounderr = value; }
        }

        private bool fs_msg;
        // <summary>
        // FS_MSG L(1)
        // </summary>
        [FieldName("FS_MSG"), FieldType('L'), FieldWidth(1)]
        public bool Fs_msg
        {
            get { return fs_msg; }
            set {  fs_msg = value; }
        }

        private string soundmsg;
        // <summary>
        // SOUNDMSG C(100)
        // </summary>
        [FieldName("SOUNDMSG"), FieldType('C'), FieldWidth(100)]
        public string Soundmsg
        {
            get { return soundmsg; }
            set { CheckStringData("Soundmsg", value, 100); soundmsg = value; }
        }

        private bool fs_dlg;
        // <summary>
        // FS_DLG L(1)
        // </summary>
        [FieldName("FS_DLG"), FieldType('L'), FieldWidth(1)]
        public bool Fs_dlg
        {
            get { return fs_dlg; }
            set {  fs_dlg = value; }
        }

        private string sounddlg;
        // <summary>
        // SOUNDDLG C(100)
        // </summary>
        [FieldName("SOUNDDLG"), FieldType('C'), FieldWidth(100)]
        public string Sounddlg
        {
            get { return sounddlg; }
            set { CheckStringData("Sounddlg", value, 100); sounddlg = value; }
        }

        private bool fs_out;
        // <summary>
        // FS_OUT L(1)
        // </summary>
        [FieldName("FS_OUT"), FieldType('L'), FieldWidth(1)]
        public bool Fs_out
        {
            get { return fs_out; }
            set {  fs_out = value; }
        }

        private string soundout;
        // <summary>
        // SOUNDOUT C(100)
        // </summary>
        [FieldName("SOUNDOUT"), FieldType('C'), FieldWidth(100)]
        public string Soundout
        {
            get { return soundout; }
            set { CheckStringData("Soundout", value, 100); soundout = value; }
        }

        private Int64 kassa;
        // <summary>
        // KASSA N(6)
        // </summary>
        [FieldName("KASSA"), FieldType('N'), FieldWidth(6)]
        public Int64 Kassa
        {
            get { return kassa; }
            set { CheckIntegerData("Kassa", value, 6); kassa = value; }
        }

        private string pathkas;
        // <summary>
        // PATHKAS C(100)
        // </summary>
        [FieldName("PATHKAS"), FieldType('C'), FieldWidth(100)]
        public string Pathkas
        {
            get { return pathkas; }
            set { CheckStringData("Pathkas", value, 100); pathkas = value; }
        }

        private DateTime dat1kas;
        // <summary>
        // DAT1KAS D(8)
        // </summary>
        [FieldName("DAT1KAS"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat1kas
        {
            get { return dat1kas; }
            set {  dat1kas = value; }
        }

        private DateTime dat2kas;
        // <summary>
        // DAT2KAS D(8)
        // </summary>
        [FieldName("DAT2KAS"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat2kas
        {
            get { return dat2kas; }
            set {  dat2kas = value; }
        }

        private Int64 monpeni;
        // <summary>
        // MONPENI N(3)
        // </summary>
        [FieldName("MONPENI"), FieldType('N'), FieldWidth(3)]
        public Int64 Monpeni
        {
            get { return monpeni; }
            set { CheckIntegerData("Monpeni", value, 3); monpeni = value; }
        }

        private Int64 daypeni;
        // <summary>
        // DAYPENI N(3)
        // </summary>
        [FieldName("DAYPENI"), FieldType('N'), FieldWidth(3)]
        public Int64 Daypeni
        {
            get { return daypeni; }
            set { CheckIntegerData("Daypeni", value, 3); daypeni = value; }
        }

        private bool fiskal;
        // <summary>
        // FISKAL L(1)
        // </summary>
        [FieldName("FISKAL"), FieldType('L'), FieldWidth(1)]
        public bool Fiskal
        {
            get { return fiskal; }
            set {  fiskal = value; }
        }

        private bool fl_hvs;
        // <summary>
        // FL_HVS L(1)
        // </summary>
        [FieldName("FL_HVS"), FieldType('L'), FieldWidth(1)]
        public bool Fl_hvs
        {
            get { return fl_hvs; }
            set {  fl_hvs = value; }
        }

        private bool fl_gvs;
        // <summary>
        // FL_GVS L(1)
        // </summary>
        [FieldName("FL_GVS"), FieldType('L'), FieldWidth(1)]
        public bool Fl_gvs
        {
            get { return fl_gvs; }
            set {  fl_gvs = value; }
        }

        private bool arcedit;
        // <summary>
        // ARCEDIT L(1)
        // </summary>
        [FieldName("ARCEDIT"), FieldType('L'), FieldWidth(1)]
        public bool Arcedit
        {
            get { return arcedit; }
            set {  arcedit = value; }
        }

        private bool editpu;
        // <summary>
        // EDITPU L(1)
        // </summary>
        [FieldName("EDITPU"), FieldType('L'), FieldWidth(1)]
        public bool Editpu
        {
            get { return editpu; }
            set {  editpu = value; }
        }

        private Int64 varpu;
        // <summary>
        // VARPU N(2)
        // </summary>
        [FieldName("VARPU"), FieldType('N'), FieldWidth(2)]
        public Int64 Varpu
        {
            get { return varpu; }
            set { CheckIntegerData("Varpu", value, 2); varpu = value; }
        }

        private DateTime arcdate;
        // <summary>
        // ARCDATE D(8)
        // </summary>
        [FieldName("ARCDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Arcdate
        {
            get { return arcdate; }
            set {  arcdate = value; }
        }

        private string telefon;
        // <summary>
        // TELEFON C(30)
        // </summary>
        [FieldName("TELEFON"), FieldType('C'), FieldWidth(30)]
        public string Telefon
        {
            get { return telefon; }
            set { CheckStringData("Telefon", value, 30); telefon = value; }
        }

        private bool winmax;
        // <summary>
        // WINMAX L(1)
        // </summary>
        [FieldName("WINMAX"), FieldType('L'), FieldWidth(1)]
        public bool Winmax
        {
            get { return winmax; }
            set {  winmax = value; }
        }

        private string datn;
        // <summary>
        // DATN C(6)
        // </summary>
        [FieldName("DATN"), FieldType('C'), FieldWidth(6)]
        public string Datn
        {
            get { return datn; }
            set { CheckStringData("Datn", value, 6); datn = value; }
        }

        private string datk;
        // <summary>
        // DATK C(6)
        // </summary>
        [FieldName("DATK"), FieldType('C'), FieldWidth(6)]
        public string Datk
        {
            get { return datk; }
            set { CheckStringData("Datk", value, 6); datk = value; }
        }

        private string parm_sb;
        // <summary>
        // PARM_SB C(10)
        // </summary>
        [FieldName("PARM_SB"), FieldType('C'), FieldWidth(10)]
        public string Parm_sb
        {
            get { return parm_sb; }
            set { CheckStringData("Parm_sb", value, 10); parm_sb = value; }
        }

        private bool start1;
        // <summary>
        // START1 L(1)
        // </summary>
        [FieldName("START1"), FieldType('L'), FieldWidth(1)]
        public bool Start1
        {
            get { return start1; }
            set {  start1 = value; }
        }

        private bool start2;
        // <summary>
        // START2 L(1)
        // </summary>
        [FieldName("START2"), FieldType('L'), FieldWidth(1)]
        public bool Start2
        {
            get { return start2; }
            set {  start2 = value; }
        }

        private string path_kladr;
        // <summary>
        // PATH_KLADR C(100)
        // </summary>
        [FieldName("PATH_KLADR"), FieldType('C'), FieldWidth(100)]
        public string Path_kladr
        {
            get { return path_kladr; }
            set { CheckStringData("Path_kladr", value, 100); path_kladr = value; }
        }

        private Int64 modecount;
        // <summary>
        // MODECOUNT N(2)
        // </summary>
        [FieldName("MODECOUNT"), FieldType('N'), FieldWidth(2)]
        public Int64 Modecount
        {
            get { return modecount; }
            set { CheckIntegerData("Modecount", value, 2); modecount = value; }
        }

        private Int64 icosize;
        // <summary>
        // ICOSIZE N(4)
        // </summary>
        [FieldName("ICOSIZE"), FieldType('N'), FieldWidth(4)]
        public Int64 Icosize
        {
            get { return icosize; }
            set { CheckIntegerData("Icosize", value, 4); icosize = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("NAME_OR")) Name_or = ADataRow["NAME_OR"].ToString(); else Name_or = "";
            if (ADataRow.Table.Columns.Contains("DT_C")) Dt_c = Convert.ToDateTime(ADataRow["DT_C"]); else Dt_c = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("FIO_RUKOV")) Fio_rukov = ADataRow["FIO_RUKOV"].ToString(); else Fio_rukov = "";
            if (ADataRow.Table.Columns.Contains("FIO_GLBUH")) Fio_glbuh = ADataRow["FIO_GLBUH"].ToString(); else Fio_glbuh = "";
            if (ADataRow.Table.Columns.Contains("FIO_KASS")) Fio_kass = ADataRow["FIO_KASS"].ToString(); else Fio_kass = "";
            if (ADataRow.Table.Columns.Contains("DOLJ_RUKOV")) Dolj_rukov = ADataRow["DOLJ_RUKOV"].ToString(); else Dolj_rukov = "";
            if (ADataRow.Table.Columns.Contains("PANELVIEW")) Panelview = Convert.ToInt64(ADataRow["PANELVIEW"]); else Panelview = 0;
            if (ADataRow.Table.Columns.Contains("CORNERS")) Corners = ADataRow["CORNERS"].ToString() == "True" ? true : false; else Corners = false;
            if (ADataRow.Table.Columns.Contains("HINT")) Hint = Convert.ToInt64(ADataRow["HINT"]); else Hint = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_CLOSE")) Flag_close = Convert.ToInt64(ADataRow["FLAG_CLOSE"]); else Flag_close = 0;
            if (ADataRow.Table.Columns.Contains("CORNER")) Corner = Convert.ToInt64(ADataRow["CORNER"]); else Corner = 0;
            if (ADataRow.Table.Columns.Contains("COLORHINT")) Colorhint = Convert.ToInt64(ADataRow["COLORHINT"]); else Colorhint = 0;
            if (ADataRow.Table.Columns.Contains("PENI")) Peni = Convert.ToDecimal(ADataRow["PENI"]); else Peni = 0;
            if (ADataRow.Table.Columns.Contains("COLORBUT")) Colorbut = ADataRow["COLORBUT"].ToString() == "True" ? true : false; else Colorbut = false;
            if (ADataRow.Table.Columns.Contains("COLORWIN")) Colorwin = Convert.ToInt64(ADataRow["COLORWIN"]); else Colorwin = 0;
            if (ADataRow.Table.Columns.Contains("MSGBOLD")) Msgbold = ADataRow["MSGBOLD"].ToString() == "True" ? true : false; else Msgbold = false;
            if (ADataRow.Table.Columns.Contains("PATH")) Path = ADataRow["PATH"].ToString(); else Path = "";
            if (ADataRow.Table.Columns.Contains("FLAGK")) Flagk = Convert.ToInt64(ADataRow["FLAGK"]); else Flagk = 0;
            if (ADataRow.Table.Columns.Contains("FLAGD")) Flagd = Convert.ToInt64(ADataRow["FLAGD"]); else Flagd = 0;
            if (ADataRow.Table.Columns.Contains("PANELOR")) Panelor = Convert.ToInt64(ADataRow["PANELOR"]); else Panelor = 0;
            if (ADataRow.Table.Columns.Contains("PRINTS")) Prints = Convert.ToInt64(ADataRow["PRINTS"]); else Prints = 0;
            if (ADataRow.Table.Columns.Contains("RS1")) Rs1 = Convert.ToInt64(ADataRow["RS1"]); else Rs1 = 0;
            if (ADataRow.Table.Columns.Contains("RS2")) Rs2 = Convert.ToInt64(ADataRow["RS2"]); else Rs2 = 0;
            if (ADataRow.Table.Columns.Contains("LISTL")) Listl = Convert.ToInt64(ADataRow["LISTL"]); else Listl = 0;
            if (ADataRow.Table.Columns.Contains("TABDRAG")) Tabdrag = ADataRow["TABDRAG"].ToString() == "True" ? true : false; else Tabdrag = false;
            if (ADataRow.Table.Columns.Contains("KOL_MES")) Kol_mes = Convert.ToInt64(ADataRow["KOL_MES"]); else Kol_mes = 0;
            if (ADataRow.Table.Columns.Contains("PANELHV")) Panelhv = Convert.ToInt64(ADataRow["PANELHV"]); else Panelhv = 0;
            if (ADataRow.Table.Columns.Contains("PANELLEN")) Panellen = Convert.ToInt64(ADataRow["PANELLEN"]); else Panellen = 0;
            if (ADataRow.Table.Columns.Contains("PANELH")) Panelh = Convert.ToInt64(ADataRow["PANELH"]); else Panelh = 0;
            if (ADataRow.Table.Columns.Contains("IMPORT")) Import = Convert.ToInt64(ADataRow["IMPORT"]); else Import = 0;
            if (ADataRow.Table.Columns.Contains("RECV_OR")) Recv_or = ADataRow["RECV_OR"].ToString(); else Recv_or = "";
            if (ADataRow.Table.Columns.Contains("CODENP")) Codenp = Convert.ToInt64(ADataRow["CODENP"]); else Codenp = 0;
            if (ADataRow.Table.Columns.Contains("CHECKDATE")) Checkdate = ADataRow["CHECKDATE"].ToString() == "True" ? true : false; else Checkdate = false;
            if (ADataRow.Table.Columns.Contains("SUM_SALDO")) Sum_saldo = Convert.ToInt64(ADataRow["SUM_SALDO"]); else Sum_saldo = 0;
            if (ADataRow.Table.Columns.Contains("POST")) Post = Convert.ToInt64(ADataRow["POST"]); else Post = 0;
            if (ADataRow.Table.Columns.Contains("FIO_PASP")) Fio_pasp = ADataRow["FIO_PASP"].ToString(); else Fio_pasp = "";
            if (ADataRow.Table.Columns.Contains("DPI")) Dpi = ADataRow["DPI"].ToString(); else Dpi = "";
            if (ADataRow.Table.Columns.Contains("DISK")) Disk = ADataRow["DISK"].ToString(); else Disk = "";
            if (ADataRow.Table.Columns.Contains("VERSION")) Version = ADataRow["VERSION"].ToString(); else Version = "";
            if (ADataRow.Table.Columns.Contains("FLAGSR")) Flagsr = ADataRow["FLAGSR"].ToString() == "True" ? true : false; else Flagsr = false;
            if (ADataRow.Table.Columns.Contains("FLAGSL")) Flagsl = Convert.ToInt64(ADataRow["FLAGSL"]); else Flagsl = 0;
            if (ADataRow.Table.Columns.Contains("LENLG")) Lenlg = Convert.ToInt64(ADataRow["LENLG"]); else Lenlg = 0;
            if (ADataRow.Table.Columns.Contains("PATHPU")) Pathpu = ADataRow["PATHPU"].ToString(); else Pathpu = "";
            if (ADataRow.Table.Columns.Contains("PATHNOV")) Pathnov = ADataRow["PATHNOV"].ToString(); else Pathnov = "";
            if (ADataRow.Table.Columns.Contains("CNT")) Cnt = ADataRow["CNT"].ToString() == "True" ? true : false; else Cnt = false;
            if (ADataRow.Table.Columns.Contains("KAS_S")) Kas_s = ADataRow["KAS_S"].ToString(); else Kas_s = "";
            if (ADataRow.Table.Columns.Contains("MOD_SALDO")) Mod_saldo = Convert.ToInt64(ADataRow["MOD_SALDO"]); else Mod_saldo = 0;
            if (ADataRow.Table.Columns.Contains("LENSP")) Lensp = Convert.ToInt64(ADataRow["LENSP"]); else Lensp = 0;
            if (ADataRow.Table.Columns.Contains("FLAGEX")) Flagex = Convert.ToInt64(ADataRow["FLAGEX"]); else Flagex = 0;
            if (ADataRow.Table.Columns.Contains("DATJ1")) Datj1 = Convert.ToDateTime(ADataRow["DATJ1"]); else Datj1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATJ2")) Datj2 = Convert.ToDateTime(ADataRow["DATJ2"]); else Datj2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NPPL")) Nppl = Convert.ToInt64(ADataRow["NPPL"]); else Nppl = 0;
            if (ADataRow.Table.Columns.Contains("BLOCKUSL")) Blockusl = Convert.ToInt64(ADataRow["BLOCKUSL"]); else Blockusl = 0;
            if (ADataRow.Table.Columns.Contains("BLOCKLGOT")) Blocklgot = Convert.ToInt64(ADataRow["BLOCKLGOT"]); else Blocklgot = 0;
            if (ADataRow.Table.Columns.Contains("DECSEP")) Decsep = ADataRow["DECSEP"].ToString(); else Decsep = "";
            if (ADataRow.Table.Columns.Contains("DECDIG")) Decdig = ADataRow["DECDIG"].ToString(); else Decdig = "";
            if (ADataRow.Table.Columns.Contains("DECEXCEL")) Decexcel = ADataRow["DECEXCEL"].ToString() == "True" ? true : false; else Decexcel = false;
            if (ADataRow.Table.Columns.Contains("POLIS")) Polis = Convert.ToInt64(ADataRow["POLIS"]); else Polis = 0;
            if (ADataRow.Table.Columns.Contains("LSZAP")) Lszap = Convert.ToInt64(ADataRow["LSZAP"]); else Lszap = 0;
            if (ADataRow.Table.Columns.Contains("DELSAL")) Delsal = Convert.ToInt64(ADataRow["DELSAL"]); else Delsal = 0;
            if (ADataRow.Table.Columns.Contains("PATHOTHE")) Pathothe = ADataRow["PATHOTHE"].ToString(); else Pathothe = "";
            if (ADataRow.Table.Columns.Contains("FIOL")) Fiol = Convert.ToInt64(ADataRow["FIOL"]); else Fiol = 0;
            if (ADataRow.Table.Columns.Contains("ADRL")) Adrl = Convert.ToInt64(ADataRow["ADRL"]); else Adrl = 0;
            if (ADataRow.Table.Columns.Contains("KVAL")) Kval = Convert.ToInt64(ADataRow["KVAL"]); else Kval = 0;
            if (ADataRow.Table.Columns.Contains("PRIL")) Pril = Convert.ToInt64(ADataRow["PRIL"]); else Pril = 0;
            if (ADataRow.Table.Columns.Contains("KOLL")) Koll = Convert.ToInt64(ADataRow["KOLL"]); else Koll = 0;
            if (ADataRow.Table.Columns.Contains("PLOL")) Plol = Convert.ToInt64(ADataRow["PLOL"]); else Plol = 0;
            if (ADataRow.Table.Columns.Contains("MESL")) Mesl = Convert.ToInt64(ADataRow["MESL"]); else Mesl = 0;
            if (ADataRow.Table.Columns.Contains("NACL")) Nacl = Convert.ToInt64(ADataRow["NACL"]); else Nacl = 0;
            if (ADataRow.Table.Columns.Contains("PENL")) Penl = Convert.ToInt64(ADataRow["PENL"]); else Penl = 0;
            if (ADataRow.Table.Columns.Contains("DOLL")) Doll = Convert.ToInt64(ADataRow["DOLL"]); else Doll = 0;
            if (ADataRow.Table.Columns.Contains("RABL")) Rabl = Convert.ToInt64(ADataRow["RABL"]); else Rabl = 0;
            if (ADataRow.Table.Columns.Contains("KM_SUB")) Km_sub = Convert.ToInt64(ADataRow["KM_SUB"]); else Km_sub = 0;
            if (ADataRow.Table.Columns.Contains("KO_SUB")) Ko_sub = Convert.ToInt64(ADataRow["KO_SUB"]); else Ko_sub = 0;
            if (ADataRow.Table.Columns.Contains("N_UC")) N_uc = Convert.ToInt64(ADataRow["N_UC"]); else N_uc = 0;
            if (ADataRow.Table.Columns.Contains("COD_ORG")) Cod_org = Convert.ToInt64(ADataRow["COD_ORG"]); else Cod_org = 0;
            if (ADataRow.Table.Columns.Contains("PATHKOR")) Pathkor = ADataRow["PATHKOR"].ToString(); else Pathkor = "";
            if (ADataRow.Table.Columns.Contains("VARBANK")) Varbank = Convert.ToInt64(ADataRow["VARBANK"]); else Varbank = 0;
            if (ADataRow.Table.Columns.Contains("KODSUB")) Kodsub = Convert.ToInt64(ADataRow["KODSUB"]); else Kodsub = 0;
            if (ADataRow.Table.Columns.Contains("GOROD")) Gorod = ADataRow["GOROD"].ToString(); else Gorod = "";
            if (ADataRow.Table.Columns.Contains("ADDRES")) Addres = ADataRow["ADDRES"].ToString(); else Addres = "";
            if (ADataRow.Table.Columns.Contains("PATHBANK")) Pathbank = ADataRow["PATHBANK"].ToString(); else Pathbank = "";
            if (ADataRow.Table.Columns.Contains("OTF3")) Otf3 = ADataRow["OTF3"].ToString(); else Otf3 = "";
            if (ADataRow.Table.Columns.Contains("REGON1")) Regon1 = ADataRow["REGON1"].ToString() == "True" ? true : false; else Regon1 = false;
            if (ADataRow.Table.Columns.Contains("REGON2")) Regon2 = Convert.ToInt64(ADataRow["REGON2"]); else Regon2 = 0;
            if (ADataRow.Table.Columns.Contains("REGON3")) Regon3 = ADataRow["REGON3"].ToString() == "True" ? true : false; else Regon3 = false;
            if (ADataRow.Table.Columns.Contains("REGON4")) Regon4 = ADataRow["REGON4"].ToString() == "True" ? true : false; else Regon4 = false;
            if (ADataRow.Table.Columns.Contains("REGON5")) Regon5 = Convert.ToInt64(ADataRow["REGON5"]); else Regon5 = 0;
            if (ADataRow.Table.Columns.Contains("REGOFF1")) Regoff1 = ADataRow["REGOFF1"].ToString() == "True" ? true : false; else Regoff1 = false;
            if (ADataRow.Table.Columns.Contains("REGOFF2")) Regoff2 = Convert.ToInt64(ADataRow["REGOFF2"]); else Regoff2 = 0;
            if (ADataRow.Table.Columns.Contains("REGOFF3")) Regoff3 = ADataRow["REGOFF3"].ToString() == "True" ? true : false; else Regoff3 = false;
            if (ADataRow.Table.Columns.Contains("REGOFF4")) Regoff4 = ADataRow["REGOFF4"].ToString() == "True" ? true : false; else Regoff4 = false;
            if (ADataRow.Table.Columns.Contains("REGASK")) Regask = ADataRow["REGASK"].ToString() == "True" ? true : false; else Regask = false;
            if (ADataRow.Table.Columns.Contains("FIO_DIR2")) Fio_dir2 = ADataRow["FIO_DIR2"].ToString(); else Fio_dir2 = "";
            if (ADataRow.Table.Columns.Contains("ND_SPEC")) Nd_spec = ADataRow["ND_SPEC"].ToString(); else Nd_spec = "";
            if (ADataRow.Table.Columns.Contains("ND_PASP")) Nd_pasp = ADataRow["ND_PASP"].ToString(); else Nd_pasp = "";
            if (ADataRow.Table.Columns.Contains("ADRES")) Adres = ADataRow["ADRES"].ToString(); else Adres = "";
            if (ADataRow.Table.Columns.Contains("CODLGOT")) Codlgot = Convert.ToInt64(ADataRow["CODLGOT"]); else Codlgot = 0;
            if (ADataRow.Table.Columns.Contains("KVITOK")) Kvitok = Convert.ToInt64(ADataRow["KVITOK"]); else Kvitok = 0;
            if (ADataRow.Table.Columns.Contains("RSPENI")) Rspeni = ADataRow["RSPENI"].ToString(); else Rspeni = "";
            if (ADataRow.Table.Columns.Contains("NUMSOC")) Numsoc = Convert.ToInt64(ADataRow["NUMSOC"]); else Numsoc = 0;
            if (ADataRow.Table.Columns.Contains("PATHSOC")) Pathsoc = ADataRow["PATHSOC"].ToString(); else Pathsoc = "";
            if (ADataRow.Table.Columns.Contains("OGRN")) Ogrn = ADataRow["OGRN"].ToString(); else Ogrn = "";
            if (ADataRow.Table.Columns.Contains("SR_HEIGHT")) Sr_height = Convert.ToInt64(ADataRow["SR_HEIGHT"]); else Sr_height = 0;
            if (ADataRow.Table.Columns.Contains("SR_HEIGHTS")) Sr_heights = Convert.ToInt64(ADataRow["SR_HEIGHTS"]); else Sr_heights = 0;
            if (ADataRow.Table.Columns.Contains("PREFIX")) Prefix = ADataRow["PREFIX"].ToString(); else Prefix = "";
            if (ADataRow.Table.Columns.Contains("SIZE_F3")) Size_f3 = Convert.ToInt64(ADataRow["SIZE_F3"]); else Size_f3 = 0;
            if (ADataRow.Table.Columns.Contains("ORIENT_F3")) Orient_f3 = Convert.ToInt64(ADataRow["ORIENT_F3"]); else Orient_f3 = 0;
            if (ADataRow.Table.Columns.Contains("SRIFTS_CF3")) Srifts_cf3 = ADataRow["SRIFTS_CF3"].ToString(); else Srifts_cf3 = "";
            if (ADataRow.Table.Columns.Contains("SIZE_CF3")) Size_cf3 = Convert.ToInt64(ADataRow["SIZE_CF3"]); else Size_cf3 = 0;
            if (ADataRow.Table.Columns.Contains("ORIENT_CF3")) Orient_cf3 = Convert.ToInt64(ADataRow["ORIENT_CF3"]); else Orient_cf3 = 0;
            if (ADataRow.Table.Columns.Contains("MATRIX_PR")) Matrix_pr = ADataRow["MATRIX_PR"].ToString() == "True" ? true : false; else Matrix_pr = false;
            if (ADataRow.Table.Columns.Contains("DIALOG_PR")) Dialog_pr = ADataRow["DIALOG_PR"].ToString() == "True" ? true : false; else Dialog_pr = false;
            if (ADataRow.Table.Columns.Contains("MAIN_USL")) Main_usl = Convert.ToInt64(ADataRow["MAIN_USL"]); else Main_usl = 0;
            if (ADataRow.Table.Columns.Contains("DATRIF1")) Datrif1 = Convert.ToDateTime(ADataRow["DATRIF1"]); else Datrif1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATRIF2")) Datrif2 = Convert.ToDateTime(ADataRow["DATRIF2"]); else Datrif2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("LASTPOST")) Lastpost = Convert.ToInt64(ADataRow["LASTPOST"]); else Lastpost = 0;
            if (ADataRow.Table.Columns.Contains("REC1")) Rec1 = ADataRow["REC1"].ToString(); else Rec1 = "";
            if (ADataRow.Table.Columns.Contains("REC2")) Rec2 = ADataRow["REC2"].ToString(); else Rec2 = "";
            if (ADataRow.Table.Columns.Contains("REC3")) Rec3 = ADataRow["REC3"].ToString(); else Rec3 = "";
            if (ADataRow.Table.Columns.Contains("PERTAR")) Pertar = ADataRow["PERTAR"].ToString() == "True" ? true : false; else Pertar = false;
            if (ADataRow.Table.Columns.Contains("FLAGU1")) Flagu1 = Convert.ToInt64(ADataRow["FLAGU1"]); else Flagu1 = 0;
            if (ADataRow.Table.Columns.Contains("FLAGU2")) Flagu2 = Convert.ToInt64(ADataRow["FLAGU2"]); else Flagu2 = 0;
            if (ADataRow.Table.Columns.Contains("FLAGU3")) Flagu3 = Convert.ToInt64(ADataRow["FLAGU3"]); else Flagu3 = 0;
            if (ADataRow.Table.Columns.Contains("BANK")) Bank = ADataRow["BANK"].ToString(); else Bank = "";
            if (ADataRow.Table.Columns.Contains("KS")) Ks = ADataRow["KS"].ToString(); else Ks = "";
            if (ADataRow.Table.Columns.Contains("BIK")) Bik = ADataRow["BIK"].ToString(); else Bik = "";
            if (ADataRow.Table.Columns.Contains("RS")) Rs = ADataRow["RS"].ToString(); else Rs = "";
            if (ADataRow.Table.Columns.Contains("INN")) Inn = ADataRow["INN"].ToString(); else Inn = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("VID_WORK")) Vid_work = ADataRow["VID_WORK"].ToString(); else Vid_work = "";
            if (ADataRow.Table.Columns.Contains("FORM_SOBS")) Form_sobs = ADataRow["FORM_SOBS"].ToString(); else Form_sobs = "";
            if (ADataRow.Table.Columns.Contains("KPP")) Kpp = ADataRow["KPP"].ToString(); else Kpp = "";
            if (ADataRow.Table.Columns.Contains("OKONH")) Okonh = ADataRow["OKONH"].ToString(); else Okonh = "";
            if (ADataRow.Table.Columns.Contains("OKPO")) Okpo = ADataRow["OKPO"].ToString(); else Okpo = "";
            if (ADataRow.Table.Columns.Contains("OKDP")) Okdp = ADataRow["OKDP"].ToString(); else Okdp = "";
            if (ADataRow.Table.Columns.Contains("OKPF")) Okpf = ADataRow["OKPF"].ToString(); else Okpf = "";
            if (ADataRow.Table.Columns.Contains("OKFS")) Okfs = ADataRow["OKFS"].ToString(); else Okfs = "";
            if (ADataRow.Table.Columns.Contains("INDEX")) Index = ADataRow["INDEX"].ToString(); else Index = "";
            if (ADataRow.Table.Columns.Contains("RESP")) Resp = ADataRow["RESP"].ToString(); else Resp = "";
            if (ADataRow.Table.Columns.Contains("RAYON")) Rayon = ADataRow["RAYON"].ToString(); else Rayon = "";
            if (ADataRow.Table.Columns.Contains("STREET")) Street = ADataRow["STREET"].ToString(); else Street = "";
            if (ADataRow.Table.Columns.Contains("HOUSE")) House = ADataRow["HOUSE"].ToString(); else House = "";
            if (ADataRow.Table.Columns.Contains("BALK")) Balk = ADataRow["BALK"].ToString(); else Balk = "";
            if (ADataRow.Table.Columns.Contains("CONSTRUC")) Construc = ADataRow["CONSTRUC"].ToString(); else Construc = "";
            if (ADataRow.Table.Columns.Contains("FLAT")) Flat = ADataRow["FLAT"].ToString(); else Flat = "";
            if (ADataRow.Table.Columns.Contains("CODREG")) Codreg = ADataRow["CODREG"].ToString(); else Codreg = "";
            if (ADataRow.Table.Columns.Contains("SB_USL")) Sb_usl = Convert.ToInt64(ADataRow["SB_USL"]); else Sb_usl = 0;
            if (ADataRow.Table.Columns.Contains("SB_PATH")) Sb_path = ADataRow["SB_PATH"].ToString(); else Sb_path = "";
            if (ADataRow.Table.Columns.Contains("OUT_PATH")) Out_path = ADataRow["OUT_PATH"].ToString(); else Out_path = "";
            if (ADataRow.Table.Columns.Contains("NAMEEXEL")) Nameexel = ADataRow["NAMEEXEL"].ToString(); else Nameexel = "";
            if (ADataRow.Table.Columns.Contains("FLAGRS")) Flagrs = ADataRow["FLAGRS"].ToString(); else Flagrs = "";
            if (ADataRow.Table.Columns.Contains("FLENG")) Fleng = ADataRow["FLENG"].ToString() == "True" ? true : false; else Fleng = false;
            if (ADataRow.Table.Columns.Contains("FLRUS")) Flrus = ADataRow["FLRUS"].ToString() == "True" ? true : false; else Flrus = false;
            if (ADataRow.Table.Columns.Contains("KASSAL")) Kassal = ADataRow["KASSAL"].ToString(); else Kassal = "";
            if (ADataRow.Table.Columns.Contains("LENUL")) Lenul = Convert.ToInt64(ADataRow["LENUL"]); else Lenul = 0;
            if (ADataRow.Table.Columns.Contains("CLOSEMSG")) Closemsg = ADataRow["CLOSEMSG"].ToString() == "True" ? true : false; else Closemsg = false;
            if (ADataRow.Table.Columns.Contains("KMPRZAD")) Kmprzad = Convert.ToInt64(ADataRow["KMPRZAD"]); else Kmprzad = 0;
            if (ADataRow.Table.Columns.Contains("KMPLAT")) Kmplat = Convert.ToInt64(ADataRow["KMPLAT"]); else Kmplat = 0;
            if (ADataRow.Table.Columns.Contains("KMCOUNT")) Kmcount = Convert.ToInt64(ADataRow["KMCOUNT"]); else Kmcount = 0;
            if (ADataRow.Table.Columns.Contains("OUTFIO")) Outfio = ADataRow["OUTFIO"].ToString() == "True" ? true : false; else Outfio = false;
            if (ADataRow.Table.Columns.Contains("USL_BANK")) Usl_bank = ADataRow["USL_BANK"].ToString(); else Usl_bank = "";
            if (ADataRow.Table.Columns.Contains("FS_IN")) Fs_in = ADataRow["FS_IN"].ToString() == "True" ? true : false; else Fs_in = false;
            if (ADataRow.Table.Columns.Contains("SOUNDIN")) Soundin = ADataRow["SOUNDIN"].ToString(); else Soundin = "";
            if (ADataRow.Table.Columns.Contains("FS_PAR")) Fs_par = ADataRow["FS_PAR"].ToString() == "True" ? true : false; else Fs_par = false;
            if (ADataRow.Table.Columns.Contains("SOUNDPAR")) Soundpar = ADataRow["SOUNDPAR"].ToString(); else Soundpar = "";
            if (ADataRow.Table.Columns.Contains("FS_OT")) Fs_ot = ADataRow["FS_OT"].ToString() == "True" ? true : false; else Fs_ot = false;
            if (ADataRow.Table.Columns.Contains("SOUNDOT")) Soundot = ADataRow["SOUNDOT"].ToString(); else Soundot = "";
            if (ADataRow.Table.Columns.Contains("FS_OPL")) Fs_opl = ADataRow["FS_OPL"].ToString() == "True" ? true : false; else Fs_opl = false;
            if (ADataRow.Table.Columns.Contains("SOUNDOPL")) Soundopl = ADataRow["SOUNDOPL"].ToString(); else Soundopl = "";
            if (ADataRow.Table.Columns.Contains("FS_ERR")) Fs_err = ADataRow["FS_ERR"].ToString() == "True" ? true : false; else Fs_err = false;
            if (ADataRow.Table.Columns.Contains("SOUNDERR")) Sounderr = ADataRow["SOUNDERR"].ToString(); else Sounderr = "";
            if (ADataRow.Table.Columns.Contains("FS_MSG")) Fs_msg = ADataRow["FS_MSG"].ToString() == "True" ? true : false; else Fs_msg = false;
            if (ADataRow.Table.Columns.Contains("SOUNDMSG")) Soundmsg = ADataRow["SOUNDMSG"].ToString(); else Soundmsg = "";
            if (ADataRow.Table.Columns.Contains("FS_DLG")) Fs_dlg = ADataRow["FS_DLG"].ToString() == "True" ? true : false; else Fs_dlg = false;
            if (ADataRow.Table.Columns.Contains("SOUNDDLG")) Sounddlg = ADataRow["SOUNDDLG"].ToString(); else Sounddlg = "";
            if (ADataRow.Table.Columns.Contains("FS_OUT")) Fs_out = ADataRow["FS_OUT"].ToString() == "True" ? true : false; else Fs_out = false;
            if (ADataRow.Table.Columns.Contains("SOUNDOUT")) Soundout = ADataRow["SOUNDOUT"].ToString(); else Soundout = "";
            if (ADataRow.Table.Columns.Contains("KASSA")) Kassa = Convert.ToInt64(ADataRow["KASSA"]); else Kassa = 0;
            if (ADataRow.Table.Columns.Contains("PATHKAS")) Pathkas = ADataRow["PATHKAS"].ToString(); else Pathkas = "";
            if (ADataRow.Table.Columns.Contains("DAT1KAS")) Dat1kas = Convert.ToDateTime(ADataRow["DAT1KAS"]); else Dat1kas = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DAT2KAS")) Dat2kas = Convert.ToDateTime(ADataRow["DAT2KAS"]); else Dat2kas = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("MONPENI")) Monpeni = Convert.ToInt64(ADataRow["MONPENI"]); else Monpeni = 0;
            if (ADataRow.Table.Columns.Contains("DAYPENI")) Daypeni = Convert.ToInt64(ADataRow["DAYPENI"]); else Daypeni = 0;
            if (ADataRow.Table.Columns.Contains("FISKAL")) Fiskal = ADataRow["FISKAL"].ToString() == "True" ? true : false; else Fiskal = false;
            if (ADataRow.Table.Columns.Contains("FL_HVS")) Fl_hvs = ADataRow["FL_HVS"].ToString() == "True" ? true : false; else Fl_hvs = false;
            if (ADataRow.Table.Columns.Contains("FL_GVS")) Fl_gvs = ADataRow["FL_GVS"].ToString() == "True" ? true : false; else Fl_gvs = false;
            if (ADataRow.Table.Columns.Contains("ARCEDIT")) Arcedit = ADataRow["ARCEDIT"].ToString() == "True" ? true : false; else Arcedit = false;
            if (ADataRow.Table.Columns.Contains("EDITPU")) Editpu = ADataRow["EDITPU"].ToString() == "True" ? true : false; else Editpu = false;
            if (ADataRow.Table.Columns.Contains("VARPU")) Varpu = Convert.ToInt64(ADataRow["VARPU"]); else Varpu = 0;
            if (ADataRow.Table.Columns.Contains("ARCDATE")) Arcdate = Convert.ToDateTime(ADataRow["ARCDATE"]); else Arcdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("TELEFON")) Telefon = ADataRow["TELEFON"].ToString(); else Telefon = "";
            if (ADataRow.Table.Columns.Contains("WINMAX")) Winmax = ADataRow["WINMAX"].ToString() == "True" ? true : false; else Winmax = false;
            if (ADataRow.Table.Columns.Contains("DATN")) Datn = ADataRow["DATN"].ToString(); else Datn = "";
            if (ADataRow.Table.Columns.Contains("DATK")) Datk = ADataRow["DATK"].ToString(); else Datk = "";
            if (ADataRow.Table.Columns.Contains("PARM_SB")) Parm_sb = ADataRow["PARM_SB"].ToString(); else Parm_sb = "";
            if (ADataRow.Table.Columns.Contains("START1")) Start1 = ADataRow["START1"].ToString() == "True" ? true : false; else Start1 = false;
            if (ADataRow.Table.Columns.Contains("START2")) Start2 = ADataRow["START2"].ToString() == "True" ? true : false; else Start2 = false;
            if (ADataRow.Table.Columns.Contains("PATH_KLADR")) Path_kladr = ADataRow["PATH_KLADR"].ToString(); else Path_kladr = "";
            if (ADataRow.Table.Columns.Contains("MODECOUNT")) Modecount = Convert.ToInt64(ADataRow["MODECOUNT"]); else Modecount = 0;
            if (ADataRow.Table.Columns.Contains("ICOSIZE")) Icosize = Convert.ToInt64(ADataRow["ICOSIZE"]); else Icosize = 0;
        }
        
        public override AbstractRecord Clone()
        {
            ZsetupRecord retValue = new ZsetupRecord();
            retValue.Name_or = this.Name_or;
            retValue.Dt_c = this.Dt_c;
            retValue.Fio_rukov = this.Fio_rukov;
            retValue.Fio_glbuh = this.Fio_glbuh;
            retValue.Fio_kass = this.Fio_kass;
            retValue.Dolj_rukov = this.Dolj_rukov;
            retValue.Panelview = this.Panelview;
            retValue.Corners = this.Corners;
            retValue.Hint = this.Hint;
            retValue.Flag_close = this.Flag_close;
            retValue.Corner = this.Corner;
            retValue.Colorhint = this.Colorhint;
            retValue.Peni = this.Peni;
            retValue.Colorbut = this.Colorbut;
            retValue.Colorwin = this.Colorwin;
            retValue.Msgbold = this.Msgbold;
            retValue.Path = this.Path;
            retValue.Flagk = this.Flagk;
            retValue.Flagd = this.Flagd;
            retValue.Panelor = this.Panelor;
            retValue.Prints = this.Prints;
            retValue.Rs1 = this.Rs1;
            retValue.Rs2 = this.Rs2;
            retValue.Listl = this.Listl;
            retValue.Tabdrag = this.Tabdrag;
            retValue.Kol_mes = this.Kol_mes;
            retValue.Panelhv = this.Panelhv;
            retValue.Panellen = this.Panellen;
            retValue.Panelh = this.Panelh;
            retValue.Import = this.Import;
            retValue.Recv_or = this.Recv_or;
            retValue.Codenp = this.Codenp;
            retValue.Checkdate = this.Checkdate;
            retValue.Sum_saldo = this.Sum_saldo;
            retValue.Post = this.Post;
            retValue.Fio_pasp = this.Fio_pasp;
            retValue.Dpi = this.Dpi;
            retValue.Disk = this.Disk;
            retValue.Version = this.Version;
            retValue.Flagsr = this.Flagsr;
            retValue.Flagsl = this.Flagsl;
            retValue.Lenlg = this.Lenlg;
            retValue.Pathpu = this.Pathpu;
            retValue.Pathnov = this.Pathnov;
            retValue.Cnt = this.Cnt;
            retValue.Kas_s = this.Kas_s;
            retValue.Mod_saldo = this.Mod_saldo;
            retValue.Lensp = this.Lensp;
            retValue.Flagex = this.Flagex;
            retValue.Datj1 = this.Datj1;
            retValue.Datj2 = this.Datj2;
            retValue.Nppl = this.Nppl;
            retValue.Blockusl = this.Blockusl;
            retValue.Blocklgot = this.Blocklgot;
            retValue.Decsep = this.Decsep;
            retValue.Decdig = this.Decdig;
            retValue.Decexcel = this.Decexcel;
            retValue.Polis = this.Polis;
            retValue.Lszap = this.Lszap;
            retValue.Delsal = this.Delsal;
            retValue.Pathothe = this.Pathothe;
            retValue.Fiol = this.Fiol;
            retValue.Adrl = this.Adrl;
            retValue.Kval = this.Kval;
            retValue.Pril = this.Pril;
            retValue.Koll = this.Koll;
            retValue.Plol = this.Plol;
            retValue.Mesl = this.Mesl;
            retValue.Nacl = this.Nacl;
            retValue.Penl = this.Penl;
            retValue.Doll = this.Doll;
            retValue.Rabl = this.Rabl;
            retValue.Km_sub = this.Km_sub;
            retValue.Ko_sub = this.Ko_sub;
            retValue.N_uc = this.N_uc;
            retValue.Cod_org = this.Cod_org;
            retValue.Pathkor = this.Pathkor;
            retValue.Varbank = this.Varbank;
            retValue.Kodsub = this.Kodsub;
            retValue.Gorod = this.Gorod;
            retValue.Addres = this.Addres;
            retValue.Pathbank = this.Pathbank;
            retValue.Otf3 = this.Otf3;
            retValue.Regon1 = this.Regon1;
            retValue.Regon2 = this.Regon2;
            retValue.Regon3 = this.Regon3;
            retValue.Regon4 = this.Regon4;
            retValue.Regon5 = this.Regon5;
            retValue.Regoff1 = this.Regoff1;
            retValue.Regoff2 = this.Regoff2;
            retValue.Regoff3 = this.Regoff3;
            retValue.Regoff4 = this.Regoff4;
            retValue.Regask = this.Regask;
            retValue.Fio_dir2 = this.Fio_dir2;
            retValue.Nd_spec = this.Nd_spec;
            retValue.Nd_pasp = this.Nd_pasp;
            retValue.Adres = this.Adres;
            retValue.Codlgot = this.Codlgot;
            retValue.Kvitok = this.Kvitok;
            retValue.Rspeni = this.Rspeni;
            retValue.Numsoc = this.Numsoc;
            retValue.Pathsoc = this.Pathsoc;
            retValue.Ogrn = this.Ogrn;
            retValue.Sr_height = this.Sr_height;
            retValue.Sr_heights = this.Sr_heights;
            retValue.Prefix = this.Prefix;
            retValue.Size_f3 = this.Size_f3;
            retValue.Orient_f3 = this.Orient_f3;
            retValue.Srifts_cf3 = this.Srifts_cf3;
            retValue.Size_cf3 = this.Size_cf3;
            retValue.Orient_cf3 = this.Orient_cf3;
            retValue.Matrix_pr = this.Matrix_pr;
            retValue.Dialog_pr = this.Dialog_pr;
            retValue.Main_usl = this.Main_usl;
            retValue.Datrif1 = this.Datrif1;
            retValue.Datrif2 = this.Datrif2;
            retValue.Lastpost = this.Lastpost;
            retValue.Rec1 = this.Rec1;
            retValue.Rec2 = this.Rec2;
            retValue.Rec3 = this.Rec3;
            retValue.Pertar = this.Pertar;
            retValue.Flagu1 = this.Flagu1;
            retValue.Flagu2 = this.Flagu2;
            retValue.Flagu3 = this.Flagu3;
            retValue.Bank = this.Bank;
            retValue.Ks = this.Ks;
            retValue.Bik = this.Bik;
            retValue.Rs = this.Rs;
            retValue.Inn = this.Inn;
            retValue.Name = this.Name;
            retValue.Vid_work = this.Vid_work;
            retValue.Form_sobs = this.Form_sobs;
            retValue.Kpp = this.Kpp;
            retValue.Okonh = this.Okonh;
            retValue.Okpo = this.Okpo;
            retValue.Okdp = this.Okdp;
            retValue.Okpf = this.Okpf;
            retValue.Okfs = this.Okfs;
            retValue.Index = this.Index;
            retValue.Resp = this.Resp;
            retValue.Rayon = this.Rayon;
            retValue.Street = this.Street;
            retValue.House = this.House;
            retValue.Balk = this.Balk;
            retValue.Construc = this.Construc;
            retValue.Flat = this.Flat;
            retValue.Codreg = this.Codreg;
            retValue.Sb_usl = this.Sb_usl;
            retValue.Sb_path = this.Sb_path;
            retValue.Out_path = this.Out_path;
            retValue.Nameexel = this.Nameexel;
            retValue.Flagrs = this.Flagrs;
            retValue.Fleng = this.Fleng;
            retValue.Flrus = this.Flrus;
            retValue.Kassal = this.Kassal;
            retValue.Lenul = this.Lenul;
            retValue.Closemsg = this.Closemsg;
            retValue.Kmprzad = this.Kmprzad;
            retValue.Kmplat = this.Kmplat;
            retValue.Kmcount = this.Kmcount;
            retValue.Outfio = this.Outfio;
            retValue.Usl_bank = this.Usl_bank;
            retValue.Fs_in = this.Fs_in;
            retValue.Soundin = this.Soundin;
            retValue.Fs_par = this.Fs_par;
            retValue.Soundpar = this.Soundpar;
            retValue.Fs_ot = this.Fs_ot;
            retValue.Soundot = this.Soundot;
            retValue.Fs_opl = this.Fs_opl;
            retValue.Soundopl = this.Soundopl;
            retValue.Fs_err = this.Fs_err;
            retValue.Sounderr = this.Sounderr;
            retValue.Fs_msg = this.Fs_msg;
            retValue.Soundmsg = this.Soundmsg;
            retValue.Fs_dlg = this.Fs_dlg;
            retValue.Sounddlg = this.Sounddlg;
            retValue.Fs_out = this.Fs_out;
            retValue.Soundout = this.Soundout;
            retValue.Kassa = this.Kassa;
            retValue.Pathkas = this.Pathkas;
            retValue.Dat1kas = this.Dat1kas;
            retValue.Dat2kas = this.Dat2kas;
            retValue.Monpeni = this.Monpeni;
            retValue.Daypeni = this.Daypeni;
            retValue.Fiskal = this.Fiskal;
            retValue.Fl_hvs = this.Fl_hvs;
            retValue.Fl_gvs = this.Fl_gvs;
            retValue.Arcedit = this.Arcedit;
            retValue.Editpu = this.Editpu;
            retValue.Varpu = this.Varpu;
            retValue.Arcdate = this.Arcdate;
            retValue.Telefon = this.Telefon;
            retValue.Winmax = this.Winmax;
            retValue.Datn = this.Datn;
            retValue.Datk = this.Datk;
            retValue.Parm_sb = this.Parm_sb;
            retValue.Start1 = this.Start1;
            retValue.Start2 = this.Start2;
            retValue.Path_kladr = this.Path_kladr;
            retValue.Modecount = this.Modecount;
            retValue.Icosize = this.Icosize;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ZSETUP (NAME_OR, DT_C, FIO_RUKOV, FIO_GLBUH, FIO_KASS, DOLJ_RUKOV, PANELVIEW, CORNERS, HINT, FLAG_CLOSE, CORNER, COLORHINT, PENI, COLORBUT, COLORWIN, MSGBOLD, PATH, FLAGK, FLAGD, PANELOR, PRINTS, RS1, RS2, LISTL, TABDRAG, KOL_MES, PANELHV, PANELLEN, PANELH, IMPORT, RECV_OR, CODENP, CHECKDATE, SUM_SALDO, POST, FIO_PASP, DPI, DISK, VERSION, FLAGSR, FLAGSL, LENLG, PATHPU, PATHNOV, CNT, KAS_S, MOD_SALDO, LENSP, FLAGEX, DATJ1, DATJ2, NPPL, BLOCKUSL, BLOCKLGOT, DECSEP, DECDIG, DECEXCEL, POLIS, LSZAP, DELSAL, PATHOTHE, FIOL, ADRL, KVAL, PRIL, KOLL, PLOL, MESL, NACL, PENL, DOLL, RABL, KM_SUB, KO_SUB, N_UC, COD_ORG, PATHKOR, VARBANK, KODSUB, GOROD, ADDRES, PATHBANK, OTF3, REGON1, REGON2, REGON3, REGON4, REGON5, REGOFF1, REGOFF2, REGOFF3, REGOFF4, REGASK, FIO_DIR2, ND_SPEC, ND_PASP, ADRES, CODLGOT, KVITOK, RSPENI, NUMSOC, PATHSOC, OGRN, SR_HEIGHT, SR_HEIGHTS, PREFIX, SIZE_F3, ORIENT_F3, SRIFTS_CF3, SIZE_CF3, ORIENT_CF3, MATRIX_PR, DIALOG_PR, MAIN_USL, DATRIF1, DATRIF2, LASTPOST, REC1, REC2, REC3, PERTAR, FLAGU1, FLAGU2, FLAGU3, BANK, KS, BIK, RS, INN, NAME, VID_WORK, FORM_SOBS, KPP, OKONH, OKPO, OKDP, OKPF, OKFS, INDEX, RESP, RAYON, STREET, HOUSE, BALK, CONSTRUC, FLAT, CODREG, SB_USL, SB_PATH, OUT_PATH, NAMEEXEL, FLAGRS, FLENG, FLRUS, KASSAL, LENUL, CLOSEMSG, KMPRZAD, KMPLAT, KMCOUNT, OUTFIO, USL_BANK, FS_IN, SOUNDIN, FS_PAR, SOUNDPAR, FS_OT, SOUNDOT, FS_OPL, SOUNDOPL, FS_ERR, SOUNDERR, FS_MSG, SOUNDMSG, FS_DLG, SOUNDDLG, FS_OUT, SOUNDOUT, KASSA, PATHKAS, DAT1KAS, DAT2KAS, MONPENI, DAYPENI, FISKAL, FL_HVS, FL_GVS, ARCEDIT, EDITPU, VARPU, ARCDATE, TELEFON, WINMAX, DATN, DATK, PARM_SB, START1, START2, PATH_KLADR, MODECOUNT, ICOSIZE) VALUES ('{0}', CTOD('{1}'), '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, '{16}', {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, '{30}', {31}, {32}, {33}, {34}, '{35}', '{36}', '{37}', '{38}', {39}, {40}, {41}, '{42}', '{43}', {44}, '{45}', {46}, {47}, {48}, CTOD('{49}'), CTOD('{50}'), {51}, {52}, {53}, '{54}', '{55}', {56}, {57}, {58}, {59}, '{60}', {61}, {62}, {63}, {64}, {65}, {66}, {67}, {68}, {69}, {70}, {71}, {72}, {73}, {74}, {75}, '{76}', {77}, {78}, '{79}', '{80}', '{81}', '{82}', {83}, {84}, {85}, {86}, {87}, {88}, {89}, {90}, {91}, {92}, '{93}', '{94}', '{95}', '{96}', {97}, {98}, '{99}', {100}, '{101}', '{102}', {103}, {104}, '{105}', {106}, {107}, '{108}', {109}, {110}, {111}, {112}, {113}, CTOD('{114}'), CTOD('{115}'), {116}, '{117}', '{118}', '{119}', {120}, {121}, {122}, {123}, '{124}', '{125}', '{126}', '{127}', '{128}', '{129}', '{130}', '{131}', '{132}', '{133}', '{134}', '{135}', '{136}', '{137}', '{138}', '{139}', '{140}', '{141}', '{142}', '{143}', '{144}', '{145}', '{146}', {147}, '{148}', '{149}', '{150}', '{151}', {152}, {153}, '{154}', {155}, {156}, {157}, {158}, {159}, {160}, '{161}', {162}, '{163}', {164}, '{165}', {166}, '{167}', {168}, '{169}', {170}, '{171}', {172}, '{173}', {174}, '{175}', {176}, '{177}', {178}, '{179}', CTOD('{180}'), CTOD('{181}'), {182}, {183}, {184}, {185}, {186}, {187}, {188}, {189}, CTOD('{190}'), '{191}', {192}, '{193}', '{194}', '{195}', {196}, {197}, '{198}', {199}, {200})", String.IsNullOrEmpty(Name_or) ? "" : Name_or.Trim(), Dt_c == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dt_c.Month, Dt_c.Day, Dt_c.Year), String.IsNullOrEmpty(Fio_rukov) ? "" : Fio_rukov.Trim(), String.IsNullOrEmpty(Fio_glbuh) ? "" : Fio_glbuh.Trim(), String.IsNullOrEmpty(Fio_kass) ? "" : Fio_kass.Trim(), String.IsNullOrEmpty(Dolj_rukov) ? "" : Dolj_rukov.Trim(), Panelview.ToString(), (Corners ? 0 : 1 ), Hint.ToString(), Flag_close.ToString(), Corner.ToString(), Colorhint.ToString(), Peni.ToString().Replace(',','.'), (Colorbut ? 0 : 1 ), Colorwin.ToString(), (Msgbold ? 0 : 1 ), String.IsNullOrEmpty(Path) ? "" : Path.Trim(), Flagk.ToString(), Flagd.ToString(), Panelor.ToString(), Prints.ToString(), Rs1.ToString(), Rs2.ToString(), Listl.ToString(), (Tabdrag ? 0 : 1 ), Kol_mes.ToString(), Panelhv.ToString(), Panellen.ToString(), Panelh.ToString(), Import.ToString(), String.IsNullOrEmpty(Recv_or) ? "" : Recv_or.Trim(), Codenp.ToString(), (Checkdate ? 0 : 1 ), Sum_saldo.ToString(), Post.ToString(), String.IsNullOrEmpty(Fio_pasp) ? "" : Fio_pasp.Trim(), String.IsNullOrEmpty(Dpi) ? "" : Dpi.Trim(), String.IsNullOrEmpty(Disk) ? "" : Disk.Trim(), String.IsNullOrEmpty(Version) ? "" : Version.Trim(), (Flagsr ? 0 : 1 ), Flagsl.ToString(), Lenlg.ToString(), String.IsNullOrEmpty(Pathpu) ? "" : Pathpu.Trim(), String.IsNullOrEmpty(Pathnov) ? "" : Pathnov.Trim(), (Cnt ? 0 : 1 ), String.IsNullOrEmpty(Kas_s) ? "" : Kas_s.Trim(), Mod_saldo.ToString(), Lensp.ToString(), Flagex.ToString(), Datj1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datj1.Month, Datj1.Day, Datj1.Year), Datj2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datj2.Month, Datj2.Day, Datj2.Year), Nppl.ToString(), Blockusl.ToString(), Blocklgot.ToString(), String.IsNullOrEmpty(Decsep) ? "" : Decsep.Trim(), String.IsNullOrEmpty(Decdig) ? "" : Decdig.Trim(), (Decexcel ? 0 : 1 ), Polis.ToString(), Lszap.ToString(), Delsal.ToString(), String.IsNullOrEmpty(Pathothe) ? "" : Pathothe.Trim(), Fiol.ToString(), Adrl.ToString(), Kval.ToString(), Pril.ToString(), Koll.ToString(), Plol.ToString(), Mesl.ToString(), Nacl.ToString(), Penl.ToString(), Doll.ToString(), Rabl.ToString(), Km_sub.ToString(), Ko_sub.ToString(), N_uc.ToString(), Cod_org.ToString(), String.IsNullOrEmpty(Pathkor) ? "" : Pathkor.Trim(), Varbank.ToString(), Kodsub.ToString(), String.IsNullOrEmpty(Gorod) ? "" : Gorod.Trim(), String.IsNullOrEmpty(Addres) ? "" : Addres.Trim(), String.IsNullOrEmpty(Pathbank) ? "" : Pathbank.Trim(), String.IsNullOrEmpty(Otf3) ? "" : Otf3.Trim(), (Regon1 ? 0 : 1 ), Regon2.ToString(), (Regon3 ? 0 : 1 ), (Regon4 ? 0 : 1 ), Regon5.ToString(), (Regoff1 ? 0 : 1 ), Regoff2.ToString(), (Regoff3 ? 0 : 1 ), (Regoff4 ? 0 : 1 ), (Regask ? 0 : 1 ), String.IsNullOrEmpty(Fio_dir2) ? "" : Fio_dir2.Trim(), String.IsNullOrEmpty(Nd_spec) ? "" : Nd_spec.Trim(), String.IsNullOrEmpty(Nd_pasp) ? "" : Nd_pasp.Trim(), String.IsNullOrEmpty(Adres) ? "" : Adres.Trim(), Codlgot.ToString(), Kvitok.ToString(), String.IsNullOrEmpty(Rspeni) ? "" : Rspeni.Trim(), Numsoc.ToString(), String.IsNullOrEmpty(Pathsoc) ? "" : Pathsoc.Trim(), String.IsNullOrEmpty(Ogrn) ? "" : Ogrn.Trim(), Sr_height.ToString(), Sr_heights.ToString(), String.IsNullOrEmpty(Prefix) ? "" : Prefix.Trim(), Size_f3.ToString(), Orient_f3.ToString(), String.IsNullOrEmpty(Srifts_cf3) ? "" : Srifts_cf3.Trim(), Size_cf3.ToString(), Orient_cf3.ToString(), (Matrix_pr ? 0 : 1 ), (Dialog_pr ? 0 : 1 ), Main_usl.ToString(), Datrif1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datrif1.Month, Datrif1.Day, Datrif1.Year), Datrif2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datrif2.Month, Datrif2.Day, Datrif2.Year), Lastpost.ToString(), String.IsNullOrEmpty(Rec1) ? "" : Rec1.Trim(), String.IsNullOrEmpty(Rec2) ? "" : Rec2.Trim(), String.IsNullOrEmpty(Rec3) ? "" : Rec3.Trim(), (Pertar ? 0 : 1 ), Flagu1.ToString(), Flagu2.ToString(), Flagu3.ToString(), String.IsNullOrEmpty(Bank) ? "" : Bank.Trim(), String.IsNullOrEmpty(Ks) ? "" : Ks.Trim(), String.IsNullOrEmpty(Bik) ? "" : Bik.Trim(), String.IsNullOrEmpty(Rs) ? "" : Rs.Trim(), String.IsNullOrEmpty(Inn) ? "" : Inn.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Vid_work) ? "" : Vid_work.Trim(), String.IsNullOrEmpty(Form_sobs) ? "" : Form_sobs.Trim(), String.IsNullOrEmpty(Kpp) ? "" : Kpp.Trim(), String.IsNullOrEmpty(Okonh) ? "" : Okonh.Trim(), String.IsNullOrEmpty(Okpo) ? "" : Okpo.Trim(), String.IsNullOrEmpty(Okdp) ? "" : Okdp.Trim(), String.IsNullOrEmpty(Okpf) ? "" : Okpf.Trim(), String.IsNullOrEmpty(Okfs) ? "" : Okfs.Trim(), String.IsNullOrEmpty(Index) ? "" : Index.Trim(), String.IsNullOrEmpty(Resp) ? "" : Resp.Trim(), String.IsNullOrEmpty(Rayon) ? "" : Rayon.Trim(), String.IsNullOrEmpty(Street) ? "" : Street.Trim(), String.IsNullOrEmpty(House) ? "" : House.Trim(), String.IsNullOrEmpty(Balk) ? "" : Balk.Trim(), String.IsNullOrEmpty(Construc) ? "" : Construc.Trim(), String.IsNullOrEmpty(Flat) ? "" : Flat.Trim(), String.IsNullOrEmpty(Codreg) ? "" : Codreg.Trim(), Sb_usl.ToString(), String.IsNullOrEmpty(Sb_path) ? "" : Sb_path.Trim(), String.IsNullOrEmpty(Out_path) ? "" : Out_path.Trim(), String.IsNullOrEmpty(Nameexel) ? "" : Nameexel.Trim(), String.IsNullOrEmpty(Flagrs) ? "" : Flagrs.Trim(), (Fleng ? 0 : 1 ), (Flrus ? 0 : 1 ), String.IsNullOrEmpty(Kassal) ? "" : Kassal.Trim(), Lenul.ToString(), (Closemsg ? 0 : 1 ), Kmprzad.ToString(), Kmplat.ToString(), Kmcount.ToString(), (Outfio ? 0 : 1 ), String.IsNullOrEmpty(Usl_bank) ? "" : Usl_bank.Trim(), (Fs_in ? 0 : 1 ), String.IsNullOrEmpty(Soundin) ? "" : Soundin.Trim(), (Fs_par ? 0 : 1 ), String.IsNullOrEmpty(Soundpar) ? "" : Soundpar.Trim(), (Fs_ot ? 0 : 1 ), String.IsNullOrEmpty(Soundot) ? "" : Soundot.Trim(), (Fs_opl ? 0 : 1 ), String.IsNullOrEmpty(Soundopl) ? "" : Soundopl.Trim(), (Fs_err ? 0 : 1 ), String.IsNullOrEmpty(Sounderr) ? "" : Sounderr.Trim(), (Fs_msg ? 0 : 1 ), String.IsNullOrEmpty(Soundmsg) ? "" : Soundmsg.Trim(), (Fs_dlg ? 0 : 1 ), String.IsNullOrEmpty(Sounddlg) ? "" : Sounddlg.Trim(), (Fs_out ? 0 : 1 ), String.IsNullOrEmpty(Soundout) ? "" : Soundout.Trim(), Kassa.ToString(), String.IsNullOrEmpty(Pathkas) ? "" : Pathkas.Trim(), Dat1kas == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat1kas.Month, Dat1kas.Day, Dat1kas.Year), Dat2kas == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat2kas.Month, Dat2kas.Day, Dat2kas.Year), Monpeni.ToString(), Daypeni.ToString(), (Fiskal ? 0 : 1 ), (Fl_hvs ? 0 : 1 ), (Fl_gvs ? 0 : 1 ), (Arcedit ? 0 : 1 ), (Editpu ? 0 : 1 ), Varpu.ToString(), Arcdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Arcdate.Month, Arcdate.Day, Arcdate.Year), String.IsNullOrEmpty(Telefon) ? "" : Telefon.Trim(), (Winmax ? 0 : 1 ), String.IsNullOrEmpty(Datn) ? "" : Datn.Trim(), String.IsNullOrEmpty(Datk) ? "" : Datk.Trim(), String.IsNullOrEmpty(Parm_sb) ? "" : Parm_sb.Trim(), (Start1 ? 0 : 1 ), (Start2 ? 0 : 1 ), String.IsNullOrEmpty(Path_kladr) ? "" : Path_kladr.Trim(), Modecount.ToString(), Icosize.ToString());
            return rs;
        }
    }
}
