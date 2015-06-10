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
    [TableName("SYSFILE.DBF")]
    public partial class SysfileRecord: AbstractRecord
    {
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

        private string imrec;
        // <summary>
        // IMREC C(20)
        // </summary>
        [FieldName("IMREC"), FieldType('C'), FieldWidth(20)]
        public string Imrec
        {
            get { return imrec; }
            set { CheckStringData("Imrec", value, 20); imrec = value; }
        }

        private string type;
        // <summary>
        // TYPE C(10)
        // </summary>
        [FieldName("TYPE"), FieldType('C'), FieldWidth(10)]
        public string Type
        {
            get { return type; }
            set { CheckStringData("Type", value, 10); type = value; }
        }

        private Int64 num;
        // <summary>
        // NUM N(4)
        // </summary>
        [FieldName("NUM"), FieldType('N'), FieldWidth(4)]
        public Int64 Num
        {
            get { return num; }
            set { CheckIntegerData("Num", value, 4); num = value; }
        }

        private string group;
        // <summary>
        // GROUP C(30)
        // </summary>
        [FieldName("GROUP"), FieldType('C'), FieldWidth(30)]
        public string Group
        {
            get { return group; }
            set { CheckStringData("Group", value, 30); group = value; }
        }

        private string name;
        // <summary>
        // NAME C(100)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(100)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 100); name = value; }
        }

        private string name_short;
        // <summary>
        // NAME_SHORT C(50)
        // </summary>
        [FieldName("NAME_SHORT"), FieldType('C'), FieldWidth(50)]
        public string Name_short
        {
            get { return name_short; }
            set { CheckStringData("Name_short", value, 50); name_short = value; }
        }

        private string msg_del;
        // <summary>
        // MSG_DEL C(50)
        // </summary>
        [FieldName("MSG_DEL"), FieldType('C'), FieldWidth(50)]
        public string Msg_del
        {
            get { return msg_del; }
            set { CheckStringData("Msg_del", value, 50); msg_del = value; }
        }

        private string name_rec;
        // <summary>
        // NAME_REC C(20)
        // </summary>
        [FieldName("NAME_REC"), FieldType('C'), FieldWidth(20)]
        public string Name_rec
        {
            get { return name_rec; }
            set { CheckStringData("Name_rec", value, 20); name_rec = value; }
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

        private string type_key;
        // <summary>
        // TYPE_KEY C(1)
        // </summary>
        [FieldName("TYPE_KEY"), FieldType('C'), FieldWidth(1)]
        public string Type_key
        {
            get { return type_key; }
            set { CheckStringData("Type_key", value, 1); type_key = value; }
        }

        private Int64 len_key;
        // <summary>
        // LEN_KEY N(3)
        // </summary>
        [FieldName("LEN_KEY"), FieldType('N'), FieldWidth(3)]
        public Int64 Len_key
        {
            get { return len_key; }
            set { CheckIntegerData("Len_key", value, 3); len_key = value; }
        }

        private string spruser;
        // <summary>
        // SPRUSER C(2)
        // </summary>
        [FieldName("SPRUSER"), FieldType('C'), FieldWidth(2)]
        public string Spruser
        {
            get { return spruser; }
            set { CheckStringData("Spruser", value, 2); spruser = value; }
        }

        private bool show_jur;
        // <summary>
        // SHOW_JUR L(1)
        // </summary>
        [FieldName("SHOW_JUR"), FieldType('L'), FieldWidth(1)]
        public bool Show_jur
        {
            get { return show_jur; }
            set {  show_jur = value; }
        }

        private bool show_dop;
        // <summary>
        // SHOW_DOP L(1)
        // </summary>
        [FieldName("SHOW_DOP"), FieldType('L'), FieldWidth(1)]
        public bool Show_dop
        {
            get { return show_dop; }
            set {  show_dop = value; }
        }

        private string cod_name;
        // <summary>
        // COD_NAME C(10)
        // </summary>
        [FieldName("COD_NAME"), FieldType('C'), FieldWidth(10)]
        public string Cod_name
        {
            get { return cod_name; }
            set { CheckStringData("Cod_name", value, 10); cod_name = value; }
        }

        private string acc;
        // <summary>
        // ACC C(10)
        // </summary>
        [FieldName("ACC"), FieldType('C'), FieldWidth(10)]
        public string Acc
        {
            get { return acc; }
            set { CheckStringData("Acc", value, 10); acc = value; }
        }

        private string path;
        // <summary>
        // PATH C(50)
        // </summary>
        [FieldName("PATH"), FieldType('C'), FieldWidth(50)]
        public string Path
        {
            get { return path; }
            set { CheckStringData("Path", value, 50); path = value; }
        }

        private string author;
        // <summary>
        // AUTHOR C(30)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(30)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 30); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("IMREC")) Imrec = ADataRow["IMREC"].ToString(); else Imrec = "";
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("GROUP")) Group = ADataRow["GROUP"].ToString(); else Group = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("NAME_SHORT")) Name_short = ADataRow["NAME_SHORT"].ToString(); else Name_short = "";
            if (ADataRow.Table.Columns.Contains("MSG_DEL")) Msg_del = ADataRow["MSG_DEL"].ToString(); else Msg_del = "";
            if (ADataRow.Table.Columns.Contains("NAME_REC")) Name_rec = ADataRow["NAME_REC"].ToString(); else Name_rec = "";
            if (ADataRow.Table.Columns.Contains("SHOW_MENU")) Show_menu = ADataRow["SHOW_MENU"].ToString() == "True" ? true : false; else Show_menu = false;
            if (ADataRow.Table.Columns.Contains("TYPE_KEY")) Type_key = ADataRow["TYPE_KEY"].ToString(); else Type_key = "";
            if (ADataRow.Table.Columns.Contains("LEN_KEY")) Len_key = Convert.ToInt64(ADataRow["LEN_KEY"]); else Len_key = 0;
            if (ADataRow.Table.Columns.Contains("SPRUSER")) Spruser = ADataRow["SPRUSER"].ToString(); else Spruser = "";
            if (ADataRow.Table.Columns.Contains("SHOW_JUR")) Show_jur = ADataRow["SHOW_JUR"].ToString() == "True" ? true : false; else Show_jur = false;
            if (ADataRow.Table.Columns.Contains("SHOW_DOP")) Show_dop = ADataRow["SHOW_DOP"].ToString() == "True" ? true : false; else Show_dop = false;
            if (ADataRow.Table.Columns.Contains("COD_NAME")) Cod_name = ADataRow["COD_NAME"].ToString(); else Cod_name = "";
            if (ADataRow.Table.Columns.Contains("ACC")) Acc = ADataRow["ACC"].ToString(); else Acc = "";
            if (ADataRow.Table.Columns.Contains("PATH")) Path = ADataRow["PATH"].ToString(); else Path = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            SysfileRecord retValue = new SysfileRecord();
            retValue.Npp = this.Npp;
            retValue.Imrec = this.Imrec;
            retValue.Type = this.Type;
            retValue.Num = this.Num;
            retValue.Group = this.Group;
            retValue.Name = this.Name;
            retValue.Name_short = this.Name_short;
            retValue.Msg_del = this.Msg_del;
            retValue.Name_rec = this.Name_rec;
            retValue.Show_menu = this.Show_menu;
            retValue.Type_key = this.Type_key;
            retValue.Len_key = this.Len_key;
            retValue.Spruser = this.Spruser;
            retValue.Show_jur = this.Show_jur;
            retValue.Show_dop = this.Show_dop;
            retValue.Cod_name = this.Cod_name;
            retValue.Acc = this.Acc;
            retValue.Path = this.Path;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SYSFILE (NPP, IMREC, TYPE, NUM, GROUP, NAME, NAME_SHORT, MSG_DEL, NAME_REC, SHOW_MENU, TYPE_KEY, LEN_KEY, SPRUSER, SHOW_JUR, SHOW_DOP, COD_NAME, ACC, PATH, AUTHOR) VALUES ({0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', {9}, '{10}', {11}, '{12}', {13}, {14}, '{15}', '{16}', '{17}', '{18}')", Npp.ToString(), String.IsNullOrEmpty(Imrec) ? "" : Imrec.Trim(), String.IsNullOrEmpty(Type) ? "" : Type.Trim(), Num.ToString(), String.IsNullOrEmpty(Group) ? "" : Group.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Name_short) ? "" : Name_short.Trim(), String.IsNullOrEmpty(Msg_del) ? "" : Msg_del.Trim(), String.IsNullOrEmpty(Name_rec) ? "" : Name_rec.Trim(), (Show_menu ? 0 : 1 ), String.IsNullOrEmpty(Type_key) ? "" : Type_key.Trim(), Len_key.ToString(), String.IsNullOrEmpty(Spruser) ? "" : Spruser.Trim(), (Show_jur ? 0 : 1 ), (Show_dop ? 0 : 1 ), String.IsNullOrEmpty(Cod_name) ? "" : Cod_name.Trim(), String.IsNullOrEmpty(Acc) ? "" : Acc.Trim(), String.IsNullOrEmpty(Path) ? "" : Path.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
