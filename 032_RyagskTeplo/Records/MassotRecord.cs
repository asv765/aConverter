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
    [TableName("MASSOT.DBF")]
    public partial class MassotRecord: AbstractRecord
    {
        private string id;
        // <summary>
        // ID C(3)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(3)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 3); id = value; }
        }

        private string id_ot;
        // <summary>
        // ID_OT C(10)
        // </summary>
        [FieldName("ID_OT"), FieldType('C'), FieldWidth(10)]
        public string Id_ot
        {
            get { return id_ot; }
            set { CheckStringData("Id_ot", value, 10); id_ot = value; }
        }

        private Int64 id_gr;
        // <summary>
        // ID_GR N(4)
        // </summary>
        [FieldName("ID_GR"), FieldType('N'), FieldWidth(4)]
        public Int64 Id_gr
        {
            get { return id_gr; }
            set { CheckIntegerData("Id_gr", value, 4); id_gr = value; }
        }

        private Int64 no_ot;
        // <summary>
        // NO_OT N(6)
        // </summary>
        [FieldName("NO_OT"), FieldType('N'), FieldWidth(6)]
        public Int64 No_ot
        {
            get { return no_ot; }
            set { CheckIntegerData("No_ot", value, 6); no_ot = value; }
        }

        private Int64 npp;
        // <summary>
        // NPP N(6)
        // </summary>
        [FieldName("NPP"), FieldType('N'), FieldWidth(6)]
        public Int64 Npp
        {
            get { return npp; }
            set { CheckIntegerData("Npp", value, 6); npp = value; }
        }

        private string nameot;
        // <summary>
        // NAMEOT C(100)
        // </summary>
        [FieldName("NAMEOT"), FieldType('C'), FieldWidth(100)]
        public string Nameot
        {
            get { return nameot; }
            set { CheckStringData("Nameot", value, 100); nameot = value; }
        }

        private string uslot;
        // <summary>
        // USLOT M
        // </summary>
        [FieldName("USLOT"), FieldType('M')]
        public string Uslot
        {
            get { return uslot; }
            set { CheckStringData("Uslot", value, 0); uslot = value; }
        }

        private string zag;
        // <summary>
        // ZAG M
        // </summary>
        [FieldName("ZAG"), FieldType('M')]
        public string Zag
        {
            get { return zag; }
            set { CheckStringData("Zag", value, 0); zag = value; }
        }

        private string rec;
        // <summary>
        // REC M
        // </summary>
        [FieldName("REC"), FieldType('M')]
        public string Rec
        {
            get { return rec; }
            set { CheckStringData("Rec", value, 0); rec = value; }
        }

        private string befor;
        // <summary>
        // BEFOR M
        // </summary>
        [FieldName("BEFOR"), FieldType('M')]
        public string Befor
        {
            get { return befor; }
            set { CheckStringData("Befor", value, 0); befor = value; }
        }

        private string formula;
        // <summary>
        // FORMULA M
        // </summary>
        [FieldName("FORMULA"), FieldType('M')]
        public string Formula
        {
            get { return formula; }
            set { CheckStringData("Formula", value, 0); formula = value; }
        }

        private string sum;
        // <summary>
        // SUM M
        // </summary>
        [FieldName("SUM"), FieldType('M')]
        public string Sum
        {
            get { return sum; }
            set { CheckStringData("Sum", value, 0); sum = value; }
        }

        private string parm;
        // <summary>
        // PARM M
        // </summary>
        [FieldName("PARM"), FieldType('M')]
        public string Parm
        {
            get { return parm; }
            set { CheckStringData("Parm", value, 0); parm = value; }
        }

        private string pgm;
        // <summary>
        // PGM M
        // </summary>
        [FieldName("PGM"), FieldType('M')]
        public string Pgm
        {
            get { return pgm; }
            set { CheckStringData("Pgm", value, 0); pgm = value; }
        }

        private string info;
        // <summary>
        // INFO M
        // </summary>
        [FieldName("INFO"), FieldType('M')]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 0); info = value; }
        }

        private Int64 color_tl;
        // <summary>
        // COLOR_TL N(16)
        // </summary>
        [FieldName("COLOR_TL"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_tl
        {
            get { return color_tl; }
            set { CheckIntegerData("Color_tl", value, 16); color_tl = value; }
        }

        private Int64 color_tt;
        // <summary>
        // COLOR_TT N(16)
        // </summary>
        [FieldName("COLOR_TT"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_tt
        {
            get { return color_tt; }
            set { CheckIntegerData("Color_tt", value, 16); color_tt = value; }
        }

        private Int64 color_tf;
        // <summary>
        // COLOR_TF N(16)
        // </summary>
        [FieldName("COLOR_TF"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_tf
        {
            get { return color_tf; }
            set { CheckIntegerData("Color_tf", value, 16); color_tf = value; }
        }

        private Int64 color_ml;
        // <summary>
        // COLOR_ML N(16)
        // </summary>
        [FieldName("COLOR_ML"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_ml
        {
            get { return color_ml; }
            set { CheckIntegerData("Color_ml", value, 16); color_ml = value; }
        }

        private Int64 color_mt;
        // <summary>
        // COLOR_MT N(16)
        // </summary>
        [FieldName("COLOR_MT"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_mt
        {
            get { return color_mt; }
            set { CheckIntegerData("Color_mt", value, 16); color_mt = value; }
        }

        private Int64 color_mf;
        // <summary>
        // COLOR_MF N(16)
        // </summary>
        [FieldName("COLOR_MF"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_mf
        {
            get { return color_mf; }
            set { CheckIntegerData("Color_mf", value, 16); color_mf = value; }
        }

        private Int64 color_il;
        // <summary>
        // COLOR_IL N(16)
        // </summary>
        [FieldName("COLOR_IL"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_il
        {
            get { return color_il; }
            set { CheckIntegerData("Color_il", value, 16); color_il = value; }
        }

        private Int64 color_it;
        // <summary>
        // COLOR_IT N(16)
        // </summary>
        [FieldName("COLOR_IT"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_it
        {
            get { return color_it; }
            set { CheckIntegerData("Color_it", value, 16); color_it = value; }
        }

        private Int64 color_if;
        // <summary>
        // COLOR_IF N(16)
        // </summary>
        [FieldName("COLOR_IF"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_if
        {
            get { return color_if; }
            set { CheckIntegerData("Color_if", value, 16); color_if = value; }
        }

        private Int64 color_bord;
        // <summary>
        // COLOR_BORD N(16)
        // </summary>
        [FieldName("COLOR_BORD"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_bord
        {
            get { return color_bord; }
            set { CheckIntegerData("Color_bord", value, 16); color_bord = value; }
        }

        private Int64 left;
        // <summary>
        // LEFT N(4)
        // </summary>
        [FieldName("LEFT"), FieldType('N'), FieldWidth(4)]
        public Int64 Left
        {
            get { return left; }
            set { CheckIntegerData("Left", value, 4); left = value; }
        }

        private Int64 right;
        // <summary>
        // RIGHT N(4)
        // </summary>
        [FieldName("RIGHT"), FieldType('N'), FieldWidth(4)]
        public Int64 Right
        {
            get { return right; }
            set { CheckIntegerData("Right", value, 4); right = value; }
        }

        private Int64 top;
        // <summary>
        // TOP N(4)
        // </summary>
        [FieldName("TOP"), FieldType('N'), FieldWidth(4)]
        public Int64 Top
        {
            get { return top; }
            set { CheckIntegerData("Top", value, 4); top = value; }
        }

        private Int64 bottom;
        // <summary>
        // BOTTOM N(4)
        // </summary>
        [FieldName("BOTTOM"), FieldType('N'), FieldWidth(4)]
        public Int64 Bottom
        {
            get { return bottom; }
            set { CheckIntegerData("Bottom", value, 4); bottom = value; }
        }

        private Int64 strlist;
        // <summary>
        // STRLIST N(4)
        // </summary>
        [FieldName("STRLIST"), FieldType('N'), FieldWidth(4)]
        public Int64 Strlist
        {
            get { return strlist; }
            set { CheckIntegerData("Strlist", value, 4); strlist = value; }
        }

        private Int64 orient;
        // <summary>
        // ORIENT N(2)
        // </summary>
        [FieldName("ORIENT"), FieldType('N'), FieldWidth(2)]
        public Int64 Orient
        {
            get { return orient; }
            set { CheckIntegerData("Orient", value, 2); orient = value; }
        }

        private string srift;
        // <summary>
        // SRIFT C(30)
        // </summary>
        [FieldName("SRIFT"), FieldType('C'), FieldWidth(30)]
        public string Srift
        {
            get { return srift; }
            set { CheckStringData("Srift", value, 30); srift = value; }
        }

        private Int64 size;
        // <summary>
        // SIZE N(4)
        // </summary>
        [FieldName("SIZE"), FieldType('N'), FieldWidth(4)]
        public Int64 Size
        {
            get { return size; }
            set { CheckIntegerData("Size", value, 4); size = value; }
        }

        private bool itog;
        // <summary>
        // ITOG L(1)
        // </summary>
        [FieldName("ITOG"), FieldType('L'), FieldWidth(1)]
        public bool Itog
        {
            get { return itog; }
            set {  itog = value; }
        }

        private bool itog_bold;
        // <summary>
        // ITOG_BOLD L(1)
        // </summary>
        [FieldName("ITOG_BOLD"), FieldType('L'), FieldWidth(1)]
        public bool Itog_bold
        {
            get { return itog_bold; }
            set {  itog_bold = value; }
        }

        private bool itog_cells;
        // <summary>
        // ITOG_CELLS L(1)
        // </summary>
        [FieldName("ITOG_CELLS"), FieldType('L'), FieldWidth(1)]
        public bool Itog_cells
        {
            get { return itog_cells; }
            set {  itog_cells = value; }
        }

        private bool itog_only;
        // <summary>
        // ITOG_ONLY L(1)
        // </summary>
        [FieldName("ITOG_ONLY"), FieldType('L'), FieldWidth(1)]
        public bool Itog_only
        {
            get { return itog_only; }
            set {  itog_only = value; }
        }

        private bool itog_list;
        // <summary>
        // ITOG_LIST L(1)
        // </summary>
        [FieldName("ITOG_LIST"), FieldType('L'), FieldWidth(1)]
        public bool Itog_list
        {
            get { return itog_list; }
            set {  itog_list = value; }
        }

        private bool itog_form;
        // <summary>
        // ITOG_FORM L(1)
        // </summary>
        [FieldName("ITOG_FORM"), FieldType('L'), FieldWidth(1)]
        public bool Itog_form
        {
            get { return itog_form; }
            set {  itog_form = value; }
        }

        private bool num_list;
        // <summary>
        // NUM_LIST L(1)
        // </summary>
        [FieldName("NUM_LIST"), FieldType('L'), FieldWidth(1)]
        public bool Num_list
        {
            get { return num_list; }
            set {  num_list = value; }
        }

        private bool num_cols;
        // <summary>
        // NUM_COLS L(1)
        // </summary>
        [FieldName("NUM_COLS"), FieldType('L'), FieldWidth(1)]
        public bool Num_cols
        {
            get { return num_cols; }
            set {  num_cols = value; }
        }

        private bool dat_time;
        // <summary>
        // DAT_TIME L(1)
        // </summary>
        [FieldName("DAT_TIME"), FieldType('L'), FieldWidth(1)]
        public bool Dat_time
        {
            get { return dat_time; }
            set {  dat_time = value; }
        }

        private Int64 save;
        // <summary>
        // SAVE N(2)
        // </summary>
        [FieldName("SAVE"), FieldType('N'), FieldWidth(2)]
        public Int64 Save
        {
            get { return save; }
            set { CheckIntegerData("Save", value, 2); save = value; }
        }

        private DateTime dat1;
        // <summary>
        // DAT1 D(8)
        // </summary>
        [FieldName("DAT1"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat1
        {
            get { return dat1; }
            set {  dat1 = value; }
        }

        private DateTime dat2;
        // <summary>
        // DAT2 D(8)
        // </summary>
        [FieldName("DAT2"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat2
        {
            get { return dat2; }
            set {  dat2 = value; }
        }

        private bool main_menu;
        // <summary>
        // MAIN_MENU L(1)
        // </summary>
        [FieldName("MAIN_MENU"), FieldType('L'), FieldWidth(1)]
        public bool Main_menu
        {
            get { return main_menu; }
            set {  main_menu = value; }
        }

        private bool self_menu;
        // <summary>
        // SELF_MENU L(1)
        // </summary>
        [FieldName("SELF_MENU"), FieldType('L'), FieldWidth(1)]
        public bool Self_menu
        {
            get { return self_menu; }
            set {  self_menu = value; }
        }

        private bool bold_menu;
        // <summary>
        // BOLD_MENU L(1)
        // </summary>
        [FieldName("BOLD_MENU"), FieldType('L'), FieldWidth(1)]
        public bool Bold_menu
        {
            get { return bold_menu; }
            set {  bold_menu = value; }
        }

        private Int64 sort;
        // <summary>
        // SORT N(3)
        // </summary>
        [FieldName("SORT"), FieldType('N'), FieldWidth(3)]
        public Int64 Sort
        {
            get { return sort; }
            set { CheckIntegerData("Sort", value, 3); sort = value; }
        }

        private bool descent;
        // <summary>
        // DESCENT L(1)
        // </summary>
        [FieldName("DESCENT"), FieldType('L'), FieldWidth(1)]
        public bool Descent
        {
            get { return descent; }
            set {  descent = value; }
        }

        private Int64 flagd;
        // <summary>
        // FLAGD N(4)
        // </summary>
        [FieldName("FLAGD"), FieldType('N'), FieldWidth(4)]
        public Int64 Flagd
        {
            get { return flagd; }
            set { CheckIntegerData("Flagd", value, 4); flagd = value; }
        }

        private Int64 typeot;
        // <summary>
        // TYPEOT N(4)
        // </summary>
        [FieldName("TYPEOT"), FieldType('N'), FieldWidth(4)]
        public Int64 Typeot
        {
            get { return typeot; }
            set { CheckIntegerData("Typeot", value, 4); typeot = value; }
        }

        private Int64 rowdel;
        // <summary>
        // ROWDEL N(2)
        // </summary>
        [FieldName("ROWDEL"), FieldType('N'), FieldWidth(2)]
        public Int64 Rowdel
        {
            get { return rowdel; }
            set { CheckIntegerData("Rowdel", value, 2); rowdel = value; }
        }

        private Int64 cellpad;
        // <summary>
        // CELLPAD N(4)
        // </summary>
        [FieldName("CELLPAD"), FieldType('N'), FieldWidth(4)]
        public Int64 Cellpad
        {
            get { return cellpad; }
            set { CheckIntegerData("Cellpad", value, 4); cellpad = value; }
        }

        private string srift_tit;
        // <summary>
        // SRIFT_TIT C(30)
        // </summary>
        [FieldName("SRIFT_TIT"), FieldType('C'), FieldWidth(30)]
        public string Srift_tit
        {
            get { return srift_tit; }
            set { CheckStringData("Srift_tit", value, 30); srift_tit = value; }
        }

        private string style;
        // <summary>
        // STYLE C(4)
        // </summary>
        [FieldName("STYLE"), FieldType('C'), FieldWidth(4)]
        public string Style
        {
            get { return style; }
            set { CheckStringData("Style", value, 4); style = value; }
        }

        private string style_tit;
        // <summary>
        // STYLE_TIT C(4)
        // </summary>
        [FieldName("STYLE_TIT"), FieldType('C'), FieldWidth(4)]
        public string Style_tit
        {
            get { return style_tit; }
            set { CheckStringData("Style_tit", value, 4); style_tit = value; }
        }

        private Int64 size_tit;
        // <summary>
        // SIZE_TIT N(4)
        // </summary>
        [FieldName("SIZE_TIT"), FieldType('N'), FieldWidth(4)]
        public Int64 Size_tit
        {
            get { return size_tit; }
            set { CheckIntegerData("Size_tit", value, 4); size_tit = value; }
        }

        private string srift_itog;
        // <summary>
        // SRIFT_ITOG C(30)
        // </summary>
        [FieldName("SRIFT_ITOG"), FieldType('C'), FieldWidth(30)]
        public string Srift_itog
        {
            get { return srift_itog; }
            set { CheckStringData("Srift_itog", value, 30); srift_itog = value; }
        }

        private string style_itog;
        // <summary>
        // STYLE_ITOG C(4)
        // </summary>
        [FieldName("STYLE_ITOG"), FieldType('C'), FieldWidth(4)]
        public string Style_itog
        {
            get { return style_itog; }
            set { CheckStringData("Style_itog", value, 4); style_itog = value; }
        }

        private Int64 size_itog;
        // <summary>
        // SIZE_ITOG N(4)
        // </summary>
        [FieldName("SIZE_ITOG"), FieldType('N'), FieldWidth(4)]
        public Int64 Size_itog
        {
            get { return size_itog; }
            set { CheckIntegerData("Size_itog", value, 4); size_itog = value; }
        }

        private Int64 borderw;
        // <summary>
        // BORDERW N(4)
        // </summary>
        [FieldName("BORDERW"), FieldType('N'), FieldWidth(4)]
        public Int64 Borderw
        {
            get { return borderw; }
            set { CheckIntegerData("Borderw", value, 4); borderw = value; }
        }

        private Int64 bordervs;
        // <summary>
        // BORDERVS N(4)
        // </summary>
        [FieldName("BORDERVS"), FieldType('N'), FieldWidth(4)]
        public Int64 Bordervs
        {
            get { return bordervs; }
            set { CheckIntegerData("Bordervs", value, 4); bordervs = value; }
        }

        private Int64 borderhs;
        // <summary>
        // BORDERHS N(4)
        // </summary>
        [FieldName("BORDERHS"), FieldType('N'), FieldWidth(4)]
        public Int64 Borderhs
        {
            get { return borderhs; }
            set { CheckIntegerData("Borderhs", value, 4); borderhs = value; }
        }

        private Int64 cellvs;
        // <summary>
        // CELLVS N(4)
        // </summary>
        [FieldName("CELLVS"), FieldType('N'), FieldWidth(4)]
        public Int64 Cellvs
        {
            get { return cellvs; }
            set { CheckIntegerData("Cellvs", value, 4); cellvs = value; }
        }

        private Int64 cellhs;
        // <summary>
        // CELLHS N(4)
        // </summary>
        [FieldName("CELLHS"), FieldType('N'), FieldWidth(4)]
        public Int64 Cellhs
        {
            get { return cellhs; }
            set { CheckIntegerData("Cellhs", value, 4); cellhs = value; }
        }

        private Int64 bordert;
        // <summary>
        // BORDERT N(4)
        // </summary>
        [FieldName("BORDERT"), FieldType('N'), FieldWidth(4)]
        public Int64 Bordert
        {
            get { return bordert; }
            set { CheckIntegerData("Bordert", value, 4); bordert = value; }
        }

        private bool autolist;
        // <summary>
        // AUTOLIST L(1)
        // </summary>
        [FieldName("AUTOLIST"), FieldType('L'), FieldWidth(1)]
        public bool Autolist
        {
            get { return autolist; }
            set {  autolist = value; }
        }

        private string srift_menu;
        // <summary>
        // SRIFT_MENU C(30)
        // </summary>
        [FieldName("SRIFT_MENU"), FieldType('C'), FieldWidth(30)]
        public string Srift_menu
        {
            get { return srift_menu; }
            set { CheckStringData("Srift_menu", value, 30); srift_menu = value; }
        }

        private Int64 color_menu;
        // <summary>
        // COLOR_MENU N(16)
        // </summary>
        [FieldName("COLOR_MENU"), FieldType('N'), FieldWidth(16)]
        public Int64 Color_menu
        {
            get { return color_menu; }
            set { CheckIntegerData("Color_menu", value, 16); color_menu = value; }
        }

        private Int64 fon_menu;
        // <summary>
        // FON_MENU N(16)
        // </summary>
        [FieldName("FON_MENU"), FieldType('N'), FieldWidth(16)]
        public Int64 Fon_menu
        {
            get { return fon_menu; }
            set { CheckIntegerData("Fon_menu", value, 16); fon_menu = value; }
        }

        private Int64 size_menu;
        // <summary>
        // SIZE_MENU N(4)
        // </summary>
        [FieldName("SIZE_MENU"), FieldType('N'), FieldWidth(4)]
        public Int64 Size_menu
        {
            get { return size_menu; }
            set { CheckIntegerData("Size_menu", value, 4); size_menu = value; }
        }

        private string style_menu;
        // <summary>
        // STYLE_MENU C(4)
        // </summary>
        [FieldName("STYLE_MENU"), FieldType('C'), FieldWidth(4)]
        public string Style_menu
        {
            get { return style_menu; }
            set { CheckStringData("Style_menu", value, 4); style_menu = value; }
        }

        private bool show_date;
        // <summary>
        // SHOW_DATE L(1)
        // </summary>
        [FieldName("SHOW_DATE"), FieldType('L'), FieldWidth(1)]
        public bool Show_date
        {
            get { return show_date; }
            set {  show_date = value; }
        }

        private bool show_filtr;
        // <summary>
        // SHOW_FILTR L(1)
        // </summary>
        [FieldName("SHOW_FILTR"), FieldType('L'), FieldWidth(1)]
        public bool Show_filtr
        {
            get { return show_filtr; }
            set {  show_filtr = value; }
        }

        private bool put_date;
        // <summary>
        // PUT_DATE L(1)
        // </summary>
        [FieldName("PUT_DATE"), FieldType('L'), FieldWidth(1)]
        public bool Put_date
        {
            get { return put_date; }
            set {  put_date = value; }
        }

        private bool arc;
        // <summary>
        // ARC L(1)
        // </summary>
        [FieldName("ARC"), FieldType('L'), FieldWidth(1)]
        public bool Arc
        {
            get { return arc; }
            set {  arc = value; }
        }

        private string dopsort;
        // <summary>
        // DOPSORT C(20)
        // </summary>
        [FieldName("DOPSORT"), FieldType('C'), FieldWidth(20)]
        public string Dopsort
        {
            get { return dopsort; }
            set { CheckStringData("Dopsort", value, 20); dopsort = value; }
        }

        private string itog_page;
        // <summary>
        // ITOG_PAGE C(20)
        // </summary>
        [FieldName("ITOG_PAGE"), FieldType('C'), FieldWidth(20)]
        public string Itog_page
        {
            get { return itog_page; }
            set { CheckStringData("Itog_page", value, 20); itog_page = value; }
        }

        private string itog_rep;
        // <summary>
        // ITOG_REP C(20)
        // </summary>
        [FieldName("ITOG_REP"), FieldType('C'), FieldWidth(20)]
        public string Itog_rep
        {
            get { return itog_rep; }
            set { CheckStringData("Itog_rep", value, 20); itog_rep = value; }
        }

        private Int64 varot;
        // <summary>
        // VAROT N(2)
        // </summary>
        [FieldName("VAROT"), FieldType('N'), FieldWidth(2)]
        public Int64 Varot
        {
            get { return varot; }
            set { CheckIntegerData("Varot", value, 2); varot = value; }
        }

        private bool looktxt;
        // <summary>
        // LOOKTXT L(1)
        // </summary>
        [FieldName("LOOKTXT"), FieldType('L'), FieldWidth(1)]
        public bool Looktxt
        {
            get { return looktxt; }
            set {  looktxt = value; }
        }

        private string nametxt;
        // <summary>
        // NAMETXT C(100)
        // </summary>
        [FieldName("NAMETXT"), FieldType('C'), FieldWidth(100)]
        public string Nametxt
        {
            get { return nametxt; }
            set { CheckStringData("Nametxt", value, 100); nametxt = value; }
        }

        private bool doscodir;
        // <summary>
        // DOSCODIR L(1)
        // </summary>
        [FieldName("DOSCODIR"), FieldType('L'), FieldWidth(1)]
        public bool Doscodir
        {
            get { return doscodir; }
            set {  doscodir = value; }
        }

        private bool headfirst;
        // <summary>
        // HEADFIRST L(1)
        // </summary>
        [FieldName("HEADFIRST"), FieldType('L'), FieldWidth(1)]
        public bool Headfirst
        {
            get { return headfirst; }
            set {  headfirst = value; }
        }

        private bool footlast;
        // <summary>
        // FOOTLAST L(1)
        // </summary>
        [FieldName("FOOTLAST"), FieldType('L'), FieldWidth(1)]
        public bool Footlast
        {
            get { return footlast; }
            set {  footlast = value; }
        }

        private Int64 strfirst;
        // <summary>
        // STRFIRST N(4)
        // </summary>
        [FieldName("STRFIRST"), FieldType('N'), FieldWidth(4)]
        public Int64 Strfirst
        {
            get { return strfirst; }
            set { CheckIntegerData("Strfirst", value, 4); strfirst = value; }
        }

        private bool itogsep;
        // <summary>
        // ITOGSEP L(1)
        // </summary>
        [FieldName("ITOGSEP"), FieldType('L'), FieldWidth(1)]
        public bool Itogsep
        {
            get { return itogsep; }
            set {  itogsep = value; }
        }

        private bool speed;
        // <summary>
        // SPEED L(1)
        // </summary>
        [FieldName("SPEED"), FieldType('L'), FieldWidth(1)]
        public bool Speed
        {
            get { return speed; }
            set {  speed = value; }
        }

        private bool editrep;
        // <summary>
        // EDITREP L(1)
        // </summary>
        [FieldName("EDITREP"), FieldType('L'), FieldWidth(1)]
        public bool Editrep
        {
            get { return editrep; }
            set {  editrep = value; }
        }

        private bool showfiltr;
        // <summary>
        // SHOWFILTR L(1)
        // </summary>
        [FieldName("SHOWFILTR"), FieldType('L'), FieldWidth(1)]
        public bool Showfiltr
        {
            get { return showfiltr; }
            set {  showfiltr = value; }
        }

        private bool nowdate;
        // <summary>
        // NOWDATE L(1)
        // </summary>
        [FieldName("NOWDATE"), FieldType('L'), FieldWidth(1)]
        public bool Nowdate
        {
            get { return nowdate; }
            set {  nowdate = value; }
        }

        private bool show_ico;
        // <summary>
        // SHOW_ICO L(1)
        // </summary>
        [FieldName("SHOW_ICO"), FieldType('L'), FieldWidth(1)]
        public bool Show_ico
        {
            get { return show_ico; }
            set {  show_ico = value; }
        }

        private string prim;
        // <summary>
        // PRIM M
        // </summary>
        [FieldName("PRIM"), FieldType('M')]
        public string Prim
        {
            get { return prim; }
            set { CheckStringData("Prim", value, 0); prim = value; }
        }

        private string acc;
        // <summary>
        // ACC C(4)
        // </summary>
        [FieldName("ACC"), FieldType('C'), FieldWidth(4)]
        public string Acc
        {
            get { return acc; }
            set { CheckStringData("Acc", value, 4); acc = value; }
        }

        private Int64 idmenu;
        // <summary>
        // IDMENU N(2)
        // </summary>
        [FieldName("IDMENU"), FieldType('N'), FieldWidth(2)]
        public Int64 Idmenu
        {
            get { return idmenu; }
            set { CheckIntegerData("Idmenu", value, 2); idmenu = value; }
        }

        private bool linebefor;
        // <summary>
        // LINEBEFOR L(1)
        // </summary>
        [FieldName("LINEBEFOR"), FieldType('L'), FieldWidth(1)]
        public bool Linebefor
        {
            get { return linebefor; }
            set {  linebefor = value; }
        }

        private string path_copy;
        // <summary>
        // PATH_COPY C(100)
        // </summary>
        [FieldName("PATH_COPY"), FieldType('C'), FieldWidth(100)]
        public string Path_copy
        {
            get { return path_copy; }
            set { CheckStringData("Path_copy", value, 100); path_copy = value; }
        }

        private bool month;
        // <summary>
        // MONTH L(1)
        // </summary>
        [FieldName("MONTH"), FieldType('L'), FieldWidth(1)]
        public bool Month
        {
            get { return month; }
            set {  month = value; }
        }

        private Int64 typedat;
        // <summary>
        // TYPEDAT N(2)
        // </summary>
        [FieldName("TYPEDAT"), FieldType('N'), FieldWidth(2)]
        public Int64 Typedat
        {
            get { return typedat; }
            set { CheckIntegerData("Typedat", value, 2); typedat = value; }
        }

        private string id_system;
        // <summary>
        // ID_SYSTEM C(30)
        // </summary>
        [FieldName("ID_SYSTEM"), FieldType('C'), FieldWidth(30)]
        public string Id_system
        {
            get { return id_system; }
            set { CheckStringData("Id_system", value, 30); id_system = value; }
        }

        private string file;
        // <summary>
        // FILE C(20)
        // </summary>
        [FieldName("FILE"), FieldType('C'), FieldWidth(20)]
        public string File
        {
            get { return file; }
            set { CheckStringData("File", value, 20); file = value; }
        }

        private Int64 key_1;
        // <summary>
        // KEY_1 N(2)
        // </summary>
        [FieldName("KEY_1"), FieldType('N'), FieldWidth(2)]
        public Int64 Key_1
        {
            get { return key_1; }
            set { CheckIntegerData("Key_1", value, 2); key_1 = value; }
        }

        private Int64 key_2;
        // <summary>
        // KEY_2 N(2)
        // </summary>
        [FieldName("KEY_2"), FieldType('N'), FieldWidth(2)]
        public Int64 Key_2
        {
            get { return key_2; }
            set { CheckIntegerData("Key_2", value, 2); key_2 = value; }
        }

        private Int64 key_3;
        // <summary>
        // KEY_3 N(2)
        // </summary>
        [FieldName("KEY_3"), FieldType('N'), FieldWidth(2)]
        public Int64 Key_3
        {
            get { return key_3; }
            set { CheckIntegerData("Key_3", value, 2); key_3 = value; }
        }

        private bool kvitok;
        // <summary>
        // KVITOK L(1)
        // </summary>
        [FieldName("KVITOK"), FieldType('L'), FieldWidth(1)]
        public bool Kvitok
        {
            get { return kvitok; }
            set {  kvitok = value; }
        }

        private bool indicno;
        // <summary>
        // INDICNO L(1)
        // </summary>
        [FieldName("INDICNO"), FieldType('L'), FieldWidth(1)]
        public bool Indicno
        {
            get { return indicno; }
            set {  indicno = value; }
        }

        private Int64 indiccount;
        // <summary>
        // INDICCOUNT N(4)
        // </summary>
        [FieldName("INDICCOUNT"), FieldType('N'), FieldWidth(4)]
        public Int64 Indiccount
        {
            get { return indiccount; }
            set { CheckIntegerData("Indiccount", value, 4); indiccount = value; }
        }

        private string hotkey;
        // <summary>
        // HOTKEY C(20)
        // </summary>
        [FieldName("HOTKEY"), FieldType('C'), FieldWidth(20)]
        public string Hotkey
        {
            get { return hotkey; }
            set { CheckStringData("Hotkey", value, 20); hotkey = value; }
        }

        private Int64 countls;
        // <summary>
        // COUNTLS N(3)
        // </summary>
        [FieldName("COUNTLS"), FieldType('N'), FieldWidth(3)]
        public Int64 Countls
        {
            get { return countls; }
            set { CheckIntegerData("Countls", value, 3); countls = value; }
        }

        private bool checkdup;
        // <summary>
        // CHECKDUP L(1)
        // </summary>
        [FieldName("CHECKDUP"), FieldType('L'), FieldWidth(1)]
        public bool Checkdup
        {
            get { return checkdup; }
            set {  checkdup = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("ID_OT")) Id_ot = ADataRow["ID_OT"].ToString(); else Id_ot = "";
            if (ADataRow.Table.Columns.Contains("ID_GR")) Id_gr = Convert.ToInt64(ADataRow["ID_GR"]); else Id_gr = 0;
            if (ADataRow.Table.Columns.Contains("NO_OT")) No_ot = Convert.ToInt64(ADataRow["NO_OT"]); else No_ot = 0;
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("NAMEOT")) Nameot = ADataRow["NAMEOT"].ToString(); else Nameot = "";
            if (ADataRow.Table.Columns.Contains("USLOT")) Uslot = ADataRow["USLOT"].ToString(); else Uslot = "";
            if (ADataRow.Table.Columns.Contains("ZAG")) Zag = ADataRow["ZAG"].ToString(); else Zag = "";
            if (ADataRow.Table.Columns.Contains("REC")) Rec = ADataRow["REC"].ToString(); else Rec = "";
            if (ADataRow.Table.Columns.Contains("BEFOR")) Befor = ADataRow["BEFOR"].ToString(); else Befor = "";
            if (ADataRow.Table.Columns.Contains("FORMULA")) Formula = ADataRow["FORMULA"].ToString(); else Formula = "";
            if (ADataRow.Table.Columns.Contains("SUM")) Sum = ADataRow["SUM"].ToString(); else Sum = "";
            if (ADataRow.Table.Columns.Contains("PARM")) Parm = ADataRow["PARM"].ToString(); else Parm = "";
            if (ADataRow.Table.Columns.Contains("PGM")) Pgm = ADataRow["PGM"].ToString(); else Pgm = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("COLOR_TL")) Color_tl = Convert.ToInt64(ADataRow["COLOR_TL"]); else Color_tl = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_TT")) Color_tt = Convert.ToInt64(ADataRow["COLOR_TT"]); else Color_tt = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_TF")) Color_tf = Convert.ToInt64(ADataRow["COLOR_TF"]); else Color_tf = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_ML")) Color_ml = Convert.ToInt64(ADataRow["COLOR_ML"]); else Color_ml = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_MT")) Color_mt = Convert.ToInt64(ADataRow["COLOR_MT"]); else Color_mt = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_MF")) Color_mf = Convert.ToInt64(ADataRow["COLOR_MF"]); else Color_mf = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_IL")) Color_il = Convert.ToInt64(ADataRow["COLOR_IL"]); else Color_il = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_IT")) Color_it = Convert.ToInt64(ADataRow["COLOR_IT"]); else Color_it = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_IF")) Color_if = Convert.ToInt64(ADataRow["COLOR_IF"]); else Color_if = 0;
            if (ADataRow.Table.Columns.Contains("COLOR_BORD")) Color_bord = Convert.ToInt64(ADataRow["COLOR_BORD"]); else Color_bord = 0;
            if (ADataRow.Table.Columns.Contains("LEFT")) Left = Convert.ToInt64(ADataRow["LEFT"]); else Left = 0;
            if (ADataRow.Table.Columns.Contains("RIGHT")) Right = Convert.ToInt64(ADataRow["RIGHT"]); else Right = 0;
            if (ADataRow.Table.Columns.Contains("TOP")) Top = Convert.ToInt64(ADataRow["TOP"]); else Top = 0;
            if (ADataRow.Table.Columns.Contains("BOTTOM")) Bottom = Convert.ToInt64(ADataRow["BOTTOM"]); else Bottom = 0;
            if (ADataRow.Table.Columns.Contains("STRLIST")) Strlist = Convert.ToInt64(ADataRow["STRLIST"]); else Strlist = 0;
            if (ADataRow.Table.Columns.Contains("ORIENT")) Orient = Convert.ToInt64(ADataRow["ORIENT"]); else Orient = 0;
            if (ADataRow.Table.Columns.Contains("SRIFT")) Srift = ADataRow["SRIFT"].ToString(); else Srift = "";
            if (ADataRow.Table.Columns.Contains("SIZE")) Size = Convert.ToInt64(ADataRow["SIZE"]); else Size = 0;
            if (ADataRow.Table.Columns.Contains("ITOG")) Itog = ADataRow["ITOG"].ToString() == "True" ? true : false; else Itog = false;
            if (ADataRow.Table.Columns.Contains("ITOG_BOLD")) Itog_bold = ADataRow["ITOG_BOLD"].ToString() == "True" ? true : false; else Itog_bold = false;
            if (ADataRow.Table.Columns.Contains("ITOG_CELLS")) Itog_cells = ADataRow["ITOG_CELLS"].ToString() == "True" ? true : false; else Itog_cells = false;
            if (ADataRow.Table.Columns.Contains("ITOG_ONLY")) Itog_only = ADataRow["ITOG_ONLY"].ToString() == "True" ? true : false; else Itog_only = false;
            if (ADataRow.Table.Columns.Contains("ITOG_LIST")) Itog_list = ADataRow["ITOG_LIST"].ToString() == "True" ? true : false; else Itog_list = false;
            if (ADataRow.Table.Columns.Contains("ITOG_FORM")) Itog_form = ADataRow["ITOG_FORM"].ToString() == "True" ? true : false; else Itog_form = false;
            if (ADataRow.Table.Columns.Contains("NUM_LIST")) Num_list = ADataRow["NUM_LIST"].ToString() == "True" ? true : false; else Num_list = false;
            if (ADataRow.Table.Columns.Contains("NUM_COLS")) Num_cols = ADataRow["NUM_COLS"].ToString() == "True" ? true : false; else Num_cols = false;
            if (ADataRow.Table.Columns.Contains("DAT_TIME")) Dat_time = ADataRow["DAT_TIME"].ToString() == "True" ? true : false; else Dat_time = false;
            if (ADataRow.Table.Columns.Contains("SAVE")) Save = Convert.ToInt64(ADataRow["SAVE"]); else Save = 0;
            if (ADataRow.Table.Columns.Contains("DAT1")) Dat1 = Convert.ToDateTime(ADataRow["DAT1"]); else Dat1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DAT2")) Dat2 = Convert.ToDateTime(ADataRow["DAT2"]); else Dat2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("MAIN_MENU")) Main_menu = ADataRow["MAIN_MENU"].ToString() == "True" ? true : false; else Main_menu = false;
            if (ADataRow.Table.Columns.Contains("SELF_MENU")) Self_menu = ADataRow["SELF_MENU"].ToString() == "True" ? true : false; else Self_menu = false;
            if (ADataRow.Table.Columns.Contains("BOLD_MENU")) Bold_menu = ADataRow["BOLD_MENU"].ToString() == "True" ? true : false; else Bold_menu = false;
            if (ADataRow.Table.Columns.Contains("SORT")) Sort = Convert.ToInt64(ADataRow["SORT"]); else Sort = 0;
            if (ADataRow.Table.Columns.Contains("DESCENT")) Descent = ADataRow["DESCENT"].ToString() == "True" ? true : false; else Descent = false;
            if (ADataRow.Table.Columns.Contains("FLAGD")) Flagd = Convert.ToInt64(ADataRow["FLAGD"]); else Flagd = 0;
            if (ADataRow.Table.Columns.Contains("TYPEOT")) Typeot = Convert.ToInt64(ADataRow["TYPEOT"]); else Typeot = 0;
            if (ADataRow.Table.Columns.Contains("ROWDEL")) Rowdel = Convert.ToInt64(ADataRow["ROWDEL"]); else Rowdel = 0;
            if (ADataRow.Table.Columns.Contains("CELLPAD")) Cellpad = Convert.ToInt64(ADataRow["CELLPAD"]); else Cellpad = 0;
            if (ADataRow.Table.Columns.Contains("SRIFT_TIT")) Srift_tit = ADataRow["SRIFT_TIT"].ToString(); else Srift_tit = "";
            if (ADataRow.Table.Columns.Contains("STYLE")) Style = ADataRow["STYLE"].ToString(); else Style = "";
            if (ADataRow.Table.Columns.Contains("STYLE_TIT")) Style_tit = ADataRow["STYLE_TIT"].ToString(); else Style_tit = "";
            if (ADataRow.Table.Columns.Contains("SIZE_TIT")) Size_tit = Convert.ToInt64(ADataRow["SIZE_TIT"]); else Size_tit = 0;
            if (ADataRow.Table.Columns.Contains("SRIFT_ITOG")) Srift_itog = ADataRow["SRIFT_ITOG"].ToString(); else Srift_itog = "";
            if (ADataRow.Table.Columns.Contains("STYLE_ITOG")) Style_itog = ADataRow["STYLE_ITOG"].ToString(); else Style_itog = "";
            if (ADataRow.Table.Columns.Contains("SIZE_ITOG")) Size_itog = Convert.ToInt64(ADataRow["SIZE_ITOG"]); else Size_itog = 0;
            if (ADataRow.Table.Columns.Contains("BORDERW")) Borderw = Convert.ToInt64(ADataRow["BORDERW"]); else Borderw = 0;
            if (ADataRow.Table.Columns.Contains("BORDERVS")) Bordervs = Convert.ToInt64(ADataRow["BORDERVS"]); else Bordervs = 0;
            if (ADataRow.Table.Columns.Contains("BORDERHS")) Borderhs = Convert.ToInt64(ADataRow["BORDERHS"]); else Borderhs = 0;
            if (ADataRow.Table.Columns.Contains("CELLVS")) Cellvs = Convert.ToInt64(ADataRow["CELLVS"]); else Cellvs = 0;
            if (ADataRow.Table.Columns.Contains("CELLHS")) Cellhs = Convert.ToInt64(ADataRow["CELLHS"]); else Cellhs = 0;
            if (ADataRow.Table.Columns.Contains("BORDERT")) Bordert = Convert.ToInt64(ADataRow["BORDERT"]); else Bordert = 0;
            if (ADataRow.Table.Columns.Contains("AUTOLIST")) Autolist = ADataRow["AUTOLIST"].ToString() == "True" ? true : false; else Autolist = false;
            if (ADataRow.Table.Columns.Contains("SRIFT_MENU")) Srift_menu = ADataRow["SRIFT_MENU"].ToString(); else Srift_menu = "";
            if (ADataRow.Table.Columns.Contains("COLOR_MENU")) Color_menu = Convert.ToInt64(ADataRow["COLOR_MENU"]); else Color_menu = 0;
            if (ADataRow.Table.Columns.Contains("FON_MENU")) Fon_menu = Convert.ToInt64(ADataRow["FON_MENU"]); else Fon_menu = 0;
            if (ADataRow.Table.Columns.Contains("SIZE_MENU")) Size_menu = Convert.ToInt64(ADataRow["SIZE_MENU"]); else Size_menu = 0;
            if (ADataRow.Table.Columns.Contains("STYLE_MENU")) Style_menu = ADataRow["STYLE_MENU"].ToString(); else Style_menu = "";
            if (ADataRow.Table.Columns.Contains("SHOW_DATE")) Show_date = ADataRow["SHOW_DATE"].ToString() == "True" ? true : false; else Show_date = false;
            if (ADataRow.Table.Columns.Contains("SHOW_FILTR")) Show_filtr = ADataRow["SHOW_FILTR"].ToString() == "True" ? true : false; else Show_filtr = false;
            if (ADataRow.Table.Columns.Contains("PUT_DATE")) Put_date = ADataRow["PUT_DATE"].ToString() == "True" ? true : false; else Put_date = false;
            if (ADataRow.Table.Columns.Contains("ARC")) Arc = ADataRow["ARC"].ToString() == "True" ? true : false; else Arc = false;
            if (ADataRow.Table.Columns.Contains("DOPSORT")) Dopsort = ADataRow["DOPSORT"].ToString(); else Dopsort = "";
            if (ADataRow.Table.Columns.Contains("ITOG_PAGE")) Itog_page = ADataRow["ITOG_PAGE"].ToString(); else Itog_page = "";
            if (ADataRow.Table.Columns.Contains("ITOG_REP")) Itog_rep = ADataRow["ITOG_REP"].ToString(); else Itog_rep = "";
            if (ADataRow.Table.Columns.Contains("VAROT")) Varot = Convert.ToInt64(ADataRow["VAROT"]); else Varot = 0;
            if (ADataRow.Table.Columns.Contains("LOOKTXT")) Looktxt = ADataRow["LOOKTXT"].ToString() == "True" ? true : false; else Looktxt = false;
            if (ADataRow.Table.Columns.Contains("NAMETXT")) Nametxt = ADataRow["NAMETXT"].ToString(); else Nametxt = "";
            if (ADataRow.Table.Columns.Contains("DOSCODIR")) Doscodir = ADataRow["DOSCODIR"].ToString() == "True" ? true : false; else Doscodir = false;
            if (ADataRow.Table.Columns.Contains("HEADFIRST")) Headfirst = ADataRow["HEADFIRST"].ToString() == "True" ? true : false; else Headfirst = false;
            if (ADataRow.Table.Columns.Contains("FOOTLAST")) Footlast = ADataRow["FOOTLAST"].ToString() == "True" ? true : false; else Footlast = false;
            if (ADataRow.Table.Columns.Contains("STRFIRST")) Strfirst = Convert.ToInt64(ADataRow["STRFIRST"]); else Strfirst = 0;
            if (ADataRow.Table.Columns.Contains("ITOGSEP")) Itogsep = ADataRow["ITOGSEP"].ToString() == "True" ? true : false; else Itogsep = false;
            if (ADataRow.Table.Columns.Contains("SPEED")) Speed = ADataRow["SPEED"].ToString() == "True" ? true : false; else Speed = false;
            if (ADataRow.Table.Columns.Contains("EDITREP")) Editrep = ADataRow["EDITREP"].ToString() == "True" ? true : false; else Editrep = false;
            if (ADataRow.Table.Columns.Contains("SHOWFILTR")) Showfiltr = ADataRow["SHOWFILTR"].ToString() == "True" ? true : false; else Showfiltr = false;
            if (ADataRow.Table.Columns.Contains("NOWDATE")) Nowdate = ADataRow["NOWDATE"].ToString() == "True" ? true : false; else Nowdate = false;
            if (ADataRow.Table.Columns.Contains("SHOW_ICO")) Show_ico = ADataRow["SHOW_ICO"].ToString() == "True" ? true : false; else Show_ico = false;
            if (ADataRow.Table.Columns.Contains("PRIM")) Prim = ADataRow["PRIM"].ToString(); else Prim = "";
            if (ADataRow.Table.Columns.Contains("ACC")) Acc = ADataRow["ACC"].ToString(); else Acc = "";
            if (ADataRow.Table.Columns.Contains("IDMENU")) Idmenu = Convert.ToInt64(ADataRow["IDMENU"]); else Idmenu = 0;
            if (ADataRow.Table.Columns.Contains("LINEBEFOR")) Linebefor = ADataRow["LINEBEFOR"].ToString() == "True" ? true : false; else Linebefor = false;
            if (ADataRow.Table.Columns.Contains("PATH_COPY")) Path_copy = ADataRow["PATH_COPY"].ToString(); else Path_copy = "";
            if (ADataRow.Table.Columns.Contains("MONTH")) Month = ADataRow["MONTH"].ToString() == "True" ? true : false; else Month = false;
            if (ADataRow.Table.Columns.Contains("TYPEDAT")) Typedat = Convert.ToInt64(ADataRow["TYPEDAT"]); else Typedat = 0;
            if (ADataRow.Table.Columns.Contains("ID_SYSTEM")) Id_system = ADataRow["ID_SYSTEM"].ToString(); else Id_system = "";
            if (ADataRow.Table.Columns.Contains("FILE")) File = ADataRow["FILE"].ToString(); else File = "";
            if (ADataRow.Table.Columns.Contains("KEY_1")) Key_1 = Convert.ToInt64(ADataRow["KEY_1"]); else Key_1 = 0;
            if (ADataRow.Table.Columns.Contains("KEY_2")) Key_2 = Convert.ToInt64(ADataRow["KEY_2"]); else Key_2 = 0;
            if (ADataRow.Table.Columns.Contains("KEY_3")) Key_3 = Convert.ToInt64(ADataRow["KEY_3"]); else Key_3 = 0;
            if (ADataRow.Table.Columns.Contains("KVITOK")) Kvitok = ADataRow["KVITOK"].ToString() == "True" ? true : false; else Kvitok = false;
            if (ADataRow.Table.Columns.Contains("INDICNO")) Indicno = ADataRow["INDICNO"].ToString() == "True" ? true : false; else Indicno = false;
            if (ADataRow.Table.Columns.Contains("INDICCOUNT")) Indiccount = Convert.ToInt64(ADataRow["INDICCOUNT"]); else Indiccount = 0;
            if (ADataRow.Table.Columns.Contains("HOTKEY")) Hotkey = ADataRow["HOTKEY"].ToString(); else Hotkey = "";
            if (ADataRow.Table.Columns.Contains("COUNTLS")) Countls = Convert.ToInt64(ADataRow["COUNTLS"]); else Countls = 0;
            if (ADataRow.Table.Columns.Contains("CHECKDUP")) Checkdup = ADataRow["CHECKDUP"].ToString() == "True" ? true : false; else Checkdup = false;
        }
        
        public override AbstractRecord Clone()
        {
            MassotRecord retValue = new MassotRecord();
            retValue.Id = this.Id;
            retValue.Id_ot = this.Id_ot;
            retValue.Id_gr = this.Id_gr;
            retValue.No_ot = this.No_ot;
            retValue.Npp = this.Npp;
            retValue.Nameot = this.Nameot;
            retValue.Uslot = this.Uslot;
            retValue.Zag = this.Zag;
            retValue.Rec = this.Rec;
            retValue.Befor = this.Befor;
            retValue.Formula = this.Formula;
            retValue.Sum = this.Sum;
            retValue.Parm = this.Parm;
            retValue.Pgm = this.Pgm;
            retValue.Info = this.Info;
            retValue.Color_tl = this.Color_tl;
            retValue.Color_tt = this.Color_tt;
            retValue.Color_tf = this.Color_tf;
            retValue.Color_ml = this.Color_ml;
            retValue.Color_mt = this.Color_mt;
            retValue.Color_mf = this.Color_mf;
            retValue.Color_il = this.Color_il;
            retValue.Color_it = this.Color_it;
            retValue.Color_if = this.Color_if;
            retValue.Color_bord = this.Color_bord;
            retValue.Left = this.Left;
            retValue.Right = this.Right;
            retValue.Top = this.Top;
            retValue.Bottom = this.Bottom;
            retValue.Strlist = this.Strlist;
            retValue.Orient = this.Orient;
            retValue.Srift = this.Srift;
            retValue.Size = this.Size;
            retValue.Itog = this.Itog;
            retValue.Itog_bold = this.Itog_bold;
            retValue.Itog_cells = this.Itog_cells;
            retValue.Itog_only = this.Itog_only;
            retValue.Itog_list = this.Itog_list;
            retValue.Itog_form = this.Itog_form;
            retValue.Num_list = this.Num_list;
            retValue.Num_cols = this.Num_cols;
            retValue.Dat_time = this.Dat_time;
            retValue.Save = this.Save;
            retValue.Dat1 = this.Dat1;
            retValue.Dat2 = this.Dat2;
            retValue.Main_menu = this.Main_menu;
            retValue.Self_menu = this.Self_menu;
            retValue.Bold_menu = this.Bold_menu;
            retValue.Sort = this.Sort;
            retValue.Descent = this.Descent;
            retValue.Flagd = this.Flagd;
            retValue.Typeot = this.Typeot;
            retValue.Rowdel = this.Rowdel;
            retValue.Cellpad = this.Cellpad;
            retValue.Srift_tit = this.Srift_tit;
            retValue.Style = this.Style;
            retValue.Style_tit = this.Style_tit;
            retValue.Size_tit = this.Size_tit;
            retValue.Srift_itog = this.Srift_itog;
            retValue.Style_itog = this.Style_itog;
            retValue.Size_itog = this.Size_itog;
            retValue.Borderw = this.Borderw;
            retValue.Bordervs = this.Bordervs;
            retValue.Borderhs = this.Borderhs;
            retValue.Cellvs = this.Cellvs;
            retValue.Cellhs = this.Cellhs;
            retValue.Bordert = this.Bordert;
            retValue.Autolist = this.Autolist;
            retValue.Srift_menu = this.Srift_menu;
            retValue.Color_menu = this.Color_menu;
            retValue.Fon_menu = this.Fon_menu;
            retValue.Size_menu = this.Size_menu;
            retValue.Style_menu = this.Style_menu;
            retValue.Show_date = this.Show_date;
            retValue.Show_filtr = this.Show_filtr;
            retValue.Put_date = this.Put_date;
            retValue.Arc = this.Arc;
            retValue.Dopsort = this.Dopsort;
            retValue.Itog_page = this.Itog_page;
            retValue.Itog_rep = this.Itog_rep;
            retValue.Varot = this.Varot;
            retValue.Looktxt = this.Looktxt;
            retValue.Nametxt = this.Nametxt;
            retValue.Doscodir = this.Doscodir;
            retValue.Headfirst = this.Headfirst;
            retValue.Footlast = this.Footlast;
            retValue.Strfirst = this.Strfirst;
            retValue.Itogsep = this.Itogsep;
            retValue.Speed = this.Speed;
            retValue.Editrep = this.Editrep;
            retValue.Showfiltr = this.Showfiltr;
            retValue.Nowdate = this.Nowdate;
            retValue.Show_ico = this.Show_ico;
            retValue.Prim = this.Prim;
            retValue.Acc = this.Acc;
            retValue.Idmenu = this.Idmenu;
            retValue.Linebefor = this.Linebefor;
            retValue.Path_copy = this.Path_copy;
            retValue.Month = this.Month;
            retValue.Typedat = this.Typedat;
            retValue.Id_system = this.Id_system;
            retValue.File = this.File;
            retValue.Key_1 = this.Key_1;
            retValue.Key_2 = this.Key_2;
            retValue.Key_3 = this.Key_3;
            retValue.Kvitok = this.Kvitok;
            retValue.Indicno = this.Indicno;
            retValue.Indiccount = this.Indiccount;
            retValue.Hotkey = this.Hotkey;
            retValue.Countls = this.Countls;
            retValue.Checkdup = this.Checkdup;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO MASSOT (ID, ID_OT, ID_GR, NO_OT, NPP, NAMEOT, USLOT, ZAG, REC, BEFOR, FORMULA, SUM, PARM, PGM, INFO, COLOR_TL, COLOR_TT, COLOR_TF, COLOR_ML, COLOR_MT, COLOR_MF, COLOR_IL, COLOR_IT, COLOR_IF, COLOR_BORD, LEFT, RIGHT, TOP, BOTTOM, STRLIST, ORIENT, SRIFT, SIZE, ITOG, ITOG_BOLD, ITOG_CELLS, ITOG_ONLY, ITOG_LIST, ITOG_FORM, NUM_LIST, NUM_COLS, DAT_TIME, SAVE, DAT1, DAT2, MAIN_MENU, SELF_MENU, BOLD_MENU, SORT, DESCENT, FLAGD, TYPEOT, ROWDEL, CELLPAD, SRIFT_TIT, STYLE, STYLE_TIT, SIZE_TIT, SRIFT_ITOG, STYLE_ITOG, SIZE_ITOG, BORDERW, BORDERVS, BORDERHS, CELLVS, CELLHS, BORDERT, AUTOLIST, SRIFT_MENU, COLOR_MENU, FON_MENU, SIZE_MENU, STYLE_MENU, SHOW_DATE, SHOW_FILTR, PUT_DATE, ARC, DOPSORT, ITOG_PAGE, ITOG_REP, VAROT, LOOKTXT, NAMETXT, DOSCODIR, HEADFIRST, FOOTLAST, STRFIRST, ITOGSEP, SPEED, EDITREP, SHOWFILTR, NOWDATE, SHOW_ICO, PRIM, ACC, IDMENU, LINEBEFOR, PATH_COPY, MONTH, TYPEDAT, ID_SYSTEM, FILE, KEY_1, KEY_2, KEY_3, KVITOK, INDICNO, INDICCOUNT, HOTKEY, COUNTLS, CHECKDUP) VALUES ('{0}', '{1}', {2}, {3}, {4}, '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, '{31}', {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, CTOD('{43}'), CTOD('{44}'), {45}, {46}, {47}, {48}, {49}, {50}, {51}, {52}, {53}, '{54}', '{55}', '{56}', {57}, '{58}', '{59}', {60}, {61}, {62}, {63}, {64}, {65}, {66}, {67}, '{68}', {69}, {70}, {71}, '{72}', {73}, {74}, {75}, {76}, '{77}', '{78}', '{79}', {80}, {81}, '{82}', {83}, {84}, {85}, {86}, {87}, {88}, {89}, {90}, {91}, {92}, '{93}', '{94}', {95}, {96}, '{97}', {98}, {99}, '{100}', '{101}', {102}, {103}, {104}, {105}, {106}, {107}, '{108}', {109}, {110})", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), String.IsNullOrEmpty(Id_ot) ? "" : Id_ot.Trim(), Id_gr.ToString(), No_ot.ToString(), Npp.ToString(), String.IsNullOrEmpty(Nameot) ? "" : Nameot.Trim(), String.IsNullOrEmpty(Uslot) ? "" : Uslot.Trim(), String.IsNullOrEmpty(Zag) ? "" : Zag.Trim(), String.IsNullOrEmpty(Rec) ? "" : Rec.Trim(), String.IsNullOrEmpty(Befor) ? "" : Befor.Trim(), String.IsNullOrEmpty(Formula) ? "" : Formula.Trim(), String.IsNullOrEmpty(Sum) ? "" : Sum.Trim(), String.IsNullOrEmpty(Parm) ? "" : Parm.Trim(), String.IsNullOrEmpty(Pgm) ? "" : Pgm.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), Color_tl.ToString(), Color_tt.ToString(), Color_tf.ToString(), Color_ml.ToString(), Color_mt.ToString(), Color_mf.ToString(), Color_il.ToString(), Color_it.ToString(), Color_if.ToString(), Color_bord.ToString(), Left.ToString(), Right.ToString(), Top.ToString(), Bottom.ToString(), Strlist.ToString(), Orient.ToString(), String.IsNullOrEmpty(Srift) ? "" : Srift.Trim(), Size.ToString(), (Itog ? 0 : 1 ), (Itog_bold ? 0 : 1 ), (Itog_cells ? 0 : 1 ), (Itog_only ? 0 : 1 ), (Itog_list ? 0 : 1 ), (Itog_form ? 0 : 1 ), (Num_list ? 0 : 1 ), (Num_cols ? 0 : 1 ), (Dat_time ? 0 : 1 ), Save.ToString(), Dat1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat1.Month, Dat1.Day, Dat1.Year), Dat2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat2.Month, Dat2.Day, Dat2.Year), (Main_menu ? 0 : 1 ), (Self_menu ? 0 : 1 ), (Bold_menu ? 0 : 1 ), Sort.ToString(), (Descent ? 0 : 1 ), Flagd.ToString(), Typeot.ToString(), Rowdel.ToString(), Cellpad.ToString(), String.IsNullOrEmpty(Srift_tit) ? "" : Srift_tit.Trim(), String.IsNullOrEmpty(Style) ? "" : Style.Trim(), String.IsNullOrEmpty(Style_tit) ? "" : Style_tit.Trim(), Size_tit.ToString(), String.IsNullOrEmpty(Srift_itog) ? "" : Srift_itog.Trim(), String.IsNullOrEmpty(Style_itog) ? "" : Style_itog.Trim(), Size_itog.ToString(), Borderw.ToString(), Bordervs.ToString(), Borderhs.ToString(), Cellvs.ToString(), Cellhs.ToString(), Bordert.ToString(), (Autolist ? 0 : 1 ), String.IsNullOrEmpty(Srift_menu) ? "" : Srift_menu.Trim(), Color_menu.ToString(), Fon_menu.ToString(), Size_menu.ToString(), String.IsNullOrEmpty(Style_menu) ? "" : Style_menu.Trim(), (Show_date ? 0 : 1 ), (Show_filtr ? 0 : 1 ), (Put_date ? 0 : 1 ), (Arc ? 0 : 1 ), String.IsNullOrEmpty(Dopsort) ? "" : Dopsort.Trim(), String.IsNullOrEmpty(Itog_page) ? "" : Itog_page.Trim(), String.IsNullOrEmpty(Itog_rep) ? "" : Itog_rep.Trim(), Varot.ToString(), (Looktxt ? 0 : 1 ), String.IsNullOrEmpty(Nametxt) ? "" : Nametxt.Trim(), (Doscodir ? 0 : 1 ), (Headfirst ? 0 : 1 ), (Footlast ? 0 : 1 ), Strfirst.ToString(), (Itogsep ? 0 : 1 ), (Speed ? 0 : 1 ), (Editrep ? 0 : 1 ), (Showfiltr ? 0 : 1 ), (Nowdate ? 0 : 1 ), (Show_ico ? 0 : 1 ), String.IsNullOrEmpty(Prim) ? "" : Prim.Trim(), String.IsNullOrEmpty(Acc) ? "" : Acc.Trim(), Idmenu.ToString(), (Linebefor ? 0 : 1 ), String.IsNullOrEmpty(Path_copy) ? "" : Path_copy.Trim(), (Month ? 0 : 1 ), Typedat.ToString(), String.IsNullOrEmpty(Id_system) ? "" : Id_system.Trim(), String.IsNullOrEmpty(File) ? "" : File.Trim(), Key_1.ToString(), Key_2.ToString(), Key_3.ToString(), (Kvitok ? 0 : 1 ), (Indicno ? 0 : 1 ), Indiccount.ToString(), String.IsNullOrEmpty(Hotkey) ? "" : Hotkey.Trim(), Countls.ToString(), (Checkdup ? 0 : 1 ));
            return rs;
        }
    }
}
