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
    [TableName("STRUC.DBF")]
    public partial class StrucRecord: AbstractRecord
    {
        private string file_cod;
        // <summary>
        // FILE_COD C(10)
        // </summary>
        [FieldName("FILE_COD"), FieldType('C'), FieldWidth(10)]
        public string File_cod
        {
            get { return file_cod; }
            set { CheckStringData("File_cod", value, 10); file_cod = value; }
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

        private Int64 field_cod;
        // <summary>
        // FIELD_COD N(11)
        // </summary>
        [FieldName("FIELD_COD"), FieldType('N'), FieldWidth(11)]
        public Int64 Field_cod
        {
            get { return field_cod; }
            set { CheckIntegerData("Field_cod", value, 11); field_cod = value; }
        }

        private Int64 field_usl;
        // <summary>
        // FIELD_USL N(4)
        // </summary>
        [FieldName("FIELD_USL"), FieldType('N'), FieldWidth(4)]
        public Int64 Field_usl
        {
            get { return field_usl; }
            set { CheckIntegerData("Field_usl", value, 4); field_usl = value; }
        }

        private Int64 field_numb;
        // <summary>
        // FIELD_NUMB N(7)
        // </summary>
        [FieldName("FIELD_NUMB"), FieldType('N'), FieldWidth(7)]
        public Int64 Field_numb
        {
            get { return field_numb; }
            set { CheckIntegerData("Field_numb", value, 7); field_numb = value; }
        }

        private string field_name;
        // <summary>
        // FIELD_NAME C(20)
        // </summary>
        [FieldName("FIELD_NAME"), FieldType('C'), FieldWidth(20)]
        public string Field_name
        {
            get { return field_name; }
            set { CheckStringData("Field_name", value, 20); field_name = value; }
        }

        private string field_type;
        // <summary>
        // FIELD_TYPE C(1)
        // </summary>
        [FieldName("FIELD_TYPE"), FieldType('C'), FieldWidth(1)]
        public string Field_type
        {
            get { return field_type; }
            set { CheckStringData("Field_type", value, 1); field_type = value; }
        }

        private Int64 field_len;
        // <summary>
        // FIELD_LEN N(4)
        // </summary>
        [FieldName("FIELD_LEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Field_len
        {
            get { return field_len; }
            set { CheckIntegerData("Field_len", value, 4); field_len = value; }
        }

        private Int64 field_dec;
        // <summary>
        // FIELD_DEC N(4)
        // </summary>
        [FieldName("FIELD_DEC"), FieldType('N'), FieldWidth(4)]
        public Int64 Field_dec
        {
            get { return field_dec; }
            set { CheckIntegerData("Field_dec", value, 4); field_dec = value; }
        }

        private Int64 ediz;
        // <summary>
        // EDIZ N(3)
        // </summary>
        [FieldName("EDIZ"), FieldType('N'), FieldWidth(3)]
        public Int64 Ediz
        {
            get { return ediz; }
            set { CheckIntegerData("Ediz", value, 3); ediz = value; }
        }

        private Int64 eval;
        // <summary>
        // EVAL N(2)
        // </summary>
        [FieldName("EVAL"), FieldType('N'), FieldWidth(2)]
        public Int64 Eval
        {
            get { return eval; }
            set { CheckIntegerData("Eval", value, 2); eval = value; }
        }

        private string field_msg;
        // <summary>
        // FIELD_MSG C(150)
        // </summary>
        [FieldName("FIELD_MSG"), FieldType('C'), FieldWidth(150)]
        public string Field_msg
        {
            get { return field_msg; }
            set { CheckStringData("Field_msg", value, 150); field_msg = value; }
        }

        private string win_msg;
        // <summary>
        // WIN_MSG C(150)
        // </summary>
        [FieldName("WIN_MSG"), FieldType('C'), FieldWidth(150)]
        public string Win_msg
        {
            get { return win_msg; }
            set { CheckStringData("Win_msg", value, 150); win_msg = value; }
        }

        private string type_edit;
        // <summary>
        // TYPE_EDIT C(15)
        // </summary>
        [FieldName("TYPE_EDIT"), FieldType('C'), FieldWidth(15)]
        public string Type_edit
        {
            get { return type_edit; }
            set { CheckStringData("Type_edit", value, 15); type_edit = value; }
        }

        private string short_msg;
        // <summary>
        // SHORT_MSG C(20)
        // </summary>
        [FieldName("SHORT_MSG"), FieldType('C'), FieldWidth(20)]
        public string Short_msg
        {
            get { return short_msg; }
            set { CheckStringData("Short_msg", value, 20); short_msg = value; }
        }

        private bool show_title;
        // <summary>
        // SHOW_TITLE L(1)
        // </summary>
        [FieldName("SHOW_TITLE"), FieldType('L'), FieldWidth(1)]
        public bool Show_title
        {
            get { return show_title; }
            set {  show_title = value; }
        }

        private bool edit_title;
        // <summary>
        // EDIT_TITLE L(1)
        // </summary>
        [FieldName("EDIT_TITLE"), FieldType('L'), FieldWidth(1)]
        public bool Edit_title
        {
            get { return edit_title; }
            set {  edit_title = value; }
        }

        private bool show_win;
        // <summary>
        // SHOW_WIN L(1)
        // </summary>
        [FieldName("SHOW_WIN"), FieldType('L'), FieldWidth(1)]
        public bool Show_win
        {
            get { return show_win; }
            set {  show_win = value; }
        }

        private bool edit_win;
        // <summary>
        // EDIT_WIN L(1)
        // </summary>
        [FieldName("EDIT_WIN"), FieldType('L'), FieldWidth(1)]
        public bool Edit_win
        {
            get { return edit_win; }
            set {  edit_win = value; }
        }

        private string datn;
        // <summary>
        // DATN C(1)
        // </summary>
        [FieldName("DATN"), FieldType('C'), FieldWidth(1)]
        public string Datn
        {
            get { return datn; }
            set { CheckStringData("Datn", value, 1); datn = value; }
        }

        private string datk;
        // <summary>
        // DATK C(1)
        // </summary>
        [FieldName("DATK"), FieldType('C'), FieldWidth(1)]
        public string Datk
        {
            get { return datk; }
            set { CheckStringData("Datk", value, 1); datk = value; }
        }

        private Int64 title_len;
        // <summary>
        // TITLE_LEN N(4)
        // </summary>
        [FieldName("TITLE_LEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Title_len
        {
            get { return title_len; }
            set { CheckIntegerData("Title_len", value, 4); title_len = value; }
        }

        private Int64 win_len;
        // <summary>
        // WIN_LEN N(4)
        // </summary>
        [FieldName("WIN_LEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Win_len
        {
            get { return win_len; }
            set { CheckIntegerData("Win_len", value, 4); win_len = value; }
        }

        private string sprav;
        // <summary>
        // SPRAV C(10)
        // </summary>
        [FieldName("SPRAV"), FieldType('C'), FieldWidth(10)]
        public string Sprav
        {
            get { return sprav; }
            set { CheckStringData("Sprav", value, 10); sprav = value; }
        }

        private bool use_sprav;
        // <summary>
        // USE_SPRAV L(1)
        // </summary>
        [FieldName("USE_SPRAV"), FieldType('L'), FieldWidth(1)]
        public bool Use_sprav
        {
            get { return use_sprav; }
            set {  use_sprav = value; }
        }

        private Int64 lim_len;
        // <summary>
        // LIM_LEN N(4)
        // </summary>
        [FieldName("LIM_LEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Lim_len
        {
            get { return lim_len; }
            set { CheckIntegerData("Lim_len", value, 4); lim_len = value; }
        }

        private bool look_ras;
        // <summary>
        // LOOK_RAS L(1)
        // </summary>
        [FieldName("LOOK_RAS"), FieldType('L'), FieldWidth(1)]
        public bool Look_ras
        {
            get { return look_ras; }
            set {  look_ras = value; }
        }

        private bool archiv;
        // <summary>
        // ARCHIV L(1)
        // </summary>
        [FieldName("ARCHIV"), FieldType('L'), FieldWidth(1)]
        public bool Archiv
        {
            get { return archiv; }
            set {  archiv = value; }
        }

        private bool flag_edit;
        // <summary>
        // FLAG_EDIT L(1)
        // </summary>
        [FieldName("FLAG_EDIT"), FieldType('L'), FieldWidth(1)]
        public bool Flag_edit
        {
            get { return flag_edit; }
            set {  flag_edit = value; }
        }

        private bool nds;
        // <summary>
        // NDS L(1)
        // </summary>
        [FieldName("NDS"), FieldType('L'), FieldWidth(1)]
        public bool Nds
        {
            get { return nds; }
            set {  nds = value; }
        }

        private Int64 taborder;
        // <summary>
        // TABORDER N(6)
        // </summary>
        [FieldName("TABORDER"), FieldType('N'), FieldWidth(6)]
        public Int64 Taborder
        {
            get { return taborder; }
            set { CheckIntegerData("Taborder", value, 6); taborder = value; }
        }

        private Int64 count_usl;
        // <summary>
        // COUNT_USL N(4)
        // </summary>
        [FieldName("COUNT_USL"), FieldType('N'), FieldWidth(4)]
        public Int64 Count_usl
        {
            get { return count_usl; }
            set { CheckIntegerData("Count_usl", value, 4); count_usl = value; }
        }

        private string menu_msg;
        // <summary>
        // MENU_MSG C(30)
        // </summary>
        [FieldName("MENU_MSG"), FieldType('C'), FieldWidth(30)]
        public string Menu_msg
        {
            get { return menu_msg; }
            set { CheckStringData("Menu_msg", value, 30); menu_msg = value; }
        }

        private bool show_menu;
        // <summary>
        // SHOW_MENU L(1)
        // </summary>
        [FieldName("SHOW_MENU"), FieldType('L'), FieldWidth(1)]
        public bool Show_menu
        {
            get { return show_menu; }
            set {  show_menu = value; }
        }

        private Int64 ot_len;
        // <summary>
        // OT_LEN N(4)
        // </summary>
        [FieldName("OT_LEN"), FieldType('N'), FieldWidth(4)]
        public Int64 Ot_len
        {
            get { return ot_len; }
            set { CheckIntegerData("Ot_len", value, 4); ot_len = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("FILE_COD")) File_cod = ADataRow["FILE_COD"].ToString(); else File_cod = "";
            if (ADataRow.Table.Columns.Contains("NO_OT")) No_ot = Convert.ToInt64(ADataRow["NO_OT"]); else No_ot = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_COD")) Field_cod = Convert.ToInt64(ADataRow["FIELD_COD"]); else Field_cod = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_USL")) Field_usl = Convert.ToInt64(ADataRow["FIELD_USL"]); else Field_usl = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_NUMB")) Field_numb = Convert.ToInt64(ADataRow["FIELD_NUMB"]); else Field_numb = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_NAME")) Field_name = ADataRow["FIELD_NAME"].ToString(); else Field_name = "";
            if (ADataRow.Table.Columns.Contains("FIELD_TYPE")) Field_type = ADataRow["FIELD_TYPE"].ToString(); else Field_type = "";
            if (ADataRow.Table.Columns.Contains("FIELD_LEN")) Field_len = Convert.ToInt64(ADataRow["FIELD_LEN"]); else Field_len = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_DEC")) Field_dec = Convert.ToInt64(ADataRow["FIELD_DEC"]); else Field_dec = 0;
            if (ADataRow.Table.Columns.Contains("EDIZ")) Ediz = Convert.ToInt64(ADataRow["EDIZ"]); else Ediz = 0;
            if (ADataRow.Table.Columns.Contains("EVAL")) Eval = Convert.ToInt64(ADataRow["EVAL"]); else Eval = 0;
            if (ADataRow.Table.Columns.Contains("FIELD_MSG")) Field_msg = ADataRow["FIELD_MSG"].ToString(); else Field_msg = "";
            if (ADataRow.Table.Columns.Contains("WIN_MSG")) Win_msg = ADataRow["WIN_MSG"].ToString(); else Win_msg = "";
            if (ADataRow.Table.Columns.Contains("TYPE_EDIT")) Type_edit = ADataRow["TYPE_EDIT"].ToString(); else Type_edit = "";
            if (ADataRow.Table.Columns.Contains("SHORT_MSG")) Short_msg = ADataRow["SHORT_MSG"].ToString(); else Short_msg = "";
            if (ADataRow.Table.Columns.Contains("SHOW_TITLE")) Show_title = ADataRow["SHOW_TITLE"].ToString() == "True" ? true : false; else Show_title = false;
            if (ADataRow.Table.Columns.Contains("EDIT_TITLE")) Edit_title = ADataRow["EDIT_TITLE"].ToString() == "True" ? true : false; else Edit_title = false;
            if (ADataRow.Table.Columns.Contains("SHOW_WIN")) Show_win = ADataRow["SHOW_WIN"].ToString() == "True" ? true : false; else Show_win = false;
            if (ADataRow.Table.Columns.Contains("EDIT_WIN")) Edit_win = ADataRow["EDIT_WIN"].ToString() == "True" ? true : false; else Edit_win = false;
            if (ADataRow.Table.Columns.Contains("DATN")) Datn = ADataRow["DATN"].ToString(); else Datn = "";
            if (ADataRow.Table.Columns.Contains("DATK")) Datk = ADataRow["DATK"].ToString(); else Datk = "";
            if (ADataRow.Table.Columns.Contains("TITLE_LEN")) Title_len = Convert.ToInt64(ADataRow["TITLE_LEN"]); else Title_len = 0;
            if (ADataRow.Table.Columns.Contains("WIN_LEN")) Win_len = Convert.ToInt64(ADataRow["WIN_LEN"]); else Win_len = 0;
            if (ADataRow.Table.Columns.Contains("SPRAV")) Sprav = ADataRow["SPRAV"].ToString(); else Sprav = "";
            if (ADataRow.Table.Columns.Contains("USE_SPRAV")) Use_sprav = ADataRow["USE_SPRAV"].ToString() == "True" ? true : false; else Use_sprav = false;
            if (ADataRow.Table.Columns.Contains("LIM_LEN")) Lim_len = Convert.ToInt64(ADataRow["LIM_LEN"]); else Lim_len = 0;
            if (ADataRow.Table.Columns.Contains("LOOK_RAS")) Look_ras = ADataRow["LOOK_RAS"].ToString() == "True" ? true : false; else Look_ras = false;
            if (ADataRow.Table.Columns.Contains("ARCHIV")) Archiv = ADataRow["ARCHIV"].ToString() == "True" ? true : false; else Archiv = false;
            if (ADataRow.Table.Columns.Contains("FLAG_EDIT")) Flag_edit = ADataRow["FLAG_EDIT"].ToString() == "True" ? true : false; else Flag_edit = false;
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = ADataRow["NDS"].ToString() == "True" ? true : false; else Nds = false;
            if (ADataRow.Table.Columns.Contains("TABORDER")) Taborder = Convert.ToInt64(ADataRow["TABORDER"]); else Taborder = 0;
            if (ADataRow.Table.Columns.Contains("COUNT_USL")) Count_usl = Convert.ToInt64(ADataRow["COUNT_USL"]); else Count_usl = 0;
            if (ADataRow.Table.Columns.Contains("MENU_MSG")) Menu_msg = ADataRow["MENU_MSG"].ToString(); else Menu_msg = "";
            if (ADataRow.Table.Columns.Contains("SHOW_MENU")) Show_menu = ADataRow["SHOW_MENU"].ToString() == "True" ? true : false; else Show_menu = false;
            if (ADataRow.Table.Columns.Contains("OT_LEN")) Ot_len = Convert.ToInt64(ADataRow["OT_LEN"]); else Ot_len = 0;
        }
        
        public override AbstractRecord Clone()
        {
            StrucRecord retValue = new StrucRecord();
            retValue.File_cod = this.File_cod;
            retValue.No_ot = this.No_ot;
            retValue.Field_cod = this.Field_cod;
            retValue.Field_usl = this.Field_usl;
            retValue.Field_numb = this.Field_numb;
            retValue.Field_name = this.Field_name;
            retValue.Field_type = this.Field_type;
            retValue.Field_len = this.Field_len;
            retValue.Field_dec = this.Field_dec;
            retValue.Ediz = this.Ediz;
            retValue.Eval = this.Eval;
            retValue.Field_msg = this.Field_msg;
            retValue.Win_msg = this.Win_msg;
            retValue.Type_edit = this.Type_edit;
            retValue.Short_msg = this.Short_msg;
            retValue.Show_title = this.Show_title;
            retValue.Edit_title = this.Edit_title;
            retValue.Show_win = this.Show_win;
            retValue.Edit_win = this.Edit_win;
            retValue.Datn = this.Datn;
            retValue.Datk = this.Datk;
            retValue.Title_len = this.Title_len;
            retValue.Win_len = this.Win_len;
            retValue.Sprav = this.Sprav;
            retValue.Use_sprav = this.Use_sprav;
            retValue.Lim_len = this.Lim_len;
            retValue.Look_ras = this.Look_ras;
            retValue.Archiv = this.Archiv;
            retValue.Flag_edit = this.Flag_edit;
            retValue.Nds = this.Nds;
            retValue.Taborder = this.Taborder;
            retValue.Count_usl = this.Count_usl;
            retValue.Menu_msg = this.Menu_msg;
            retValue.Show_menu = this.Show_menu;
            retValue.Ot_len = this.Ot_len;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO STRUC (FILE_COD, NO_OT, FIELD_COD, FIELD_USL, FIELD_NUMB, FIELD_NAME, FIELD_TYPE, FIELD_LEN, FIELD_DEC, EDIZ, EVAL, FIELD_MSG, WIN_MSG, TYPE_EDIT, SHORT_MSG, SHOW_TITLE, EDIT_TITLE, SHOW_WIN, EDIT_WIN, DATN, DATK, TITLE_LEN, WIN_LEN, SPRAV, USE_SPRAV, LIM_LEN, LOOK_RAS, ARCHIV, FLAG_EDIT, NDS, TABORDER, COUNT_USL, MENU_MSG, SHOW_MENU, OT_LEN) VALUES ('{0}', {1}, {2}, {3}, {4}, '{5}', '{6}', {7}, {8}, {9}, {10}, '{11}', '{12}', '{13}', '{14}', {15}, {16}, {17}, {18}, '{19}', '{20}', {21}, {22}, '{23}', {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, '{32}', {33}, {34})", String.IsNullOrEmpty(File_cod) ? "" : File_cod.Trim(), No_ot.ToString(), Field_cod.ToString(), Field_usl.ToString(), Field_numb.ToString(), String.IsNullOrEmpty(Field_name) ? "" : Field_name.Trim(), String.IsNullOrEmpty(Field_type) ? "" : Field_type.Trim(), Field_len.ToString(), Field_dec.ToString(), Ediz.ToString(), Eval.ToString(), String.IsNullOrEmpty(Field_msg) ? "" : Field_msg.Trim(), String.IsNullOrEmpty(Win_msg) ? "" : Win_msg.Trim(), String.IsNullOrEmpty(Type_edit) ? "" : Type_edit.Trim(), String.IsNullOrEmpty(Short_msg) ? "" : Short_msg.Trim(), (Show_title ? 0 : 1 ), (Edit_title ? 0 : 1 ), (Show_win ? 0 : 1 ), (Edit_win ? 0 : 1 ), String.IsNullOrEmpty(Datn) ? "" : Datn.Trim(), String.IsNullOrEmpty(Datk) ? "" : Datk.Trim(), Title_len.ToString(), Win_len.ToString(), String.IsNullOrEmpty(Sprav) ? "" : Sprav.Trim(), (Use_sprav ? 0 : 1 ), Lim_len.ToString(), (Look_ras ? 0 : 1 ), (Archiv ? 0 : 1 ), (Flag_edit ? 0 : 1 ), (Nds ? 0 : 1 ), Taborder.ToString(), Count_usl.ToString(), String.IsNullOrEmpty(Menu_msg) ? "" : Menu_msg.Trim(), (Show_menu ? 0 : 1 ), Ot_len.ToString());
            return rs;
        }
    }
}
