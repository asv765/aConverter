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
    [TableName("KASSA.DBF")]
    public partial class KassaRecord: AbstractRecord
    {
        private DateTime dat;
        // <summary>
        // DAT D(8)
        // </summary>
        [FieldName("DAT"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat
        {
            get { return dat; }
            set {  dat = value; }
        }

        private Int64 num;
        // <summary>
        // NUM N(11)
        // </summary>
        [FieldName("NUM"), FieldType('N'), FieldWidth(11)]
        public Int64 Num
        {
            get { return num; }
            set { CheckIntegerData("Num", value, 11); num = value; }
        }

        private string id;
        // <summary>
        // ID C(1)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(1)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 1); id = value; }
        }

        private Int64 nk;
        // <summary>
        // NK N(3)
        // </summary>
        [FieldName("NK"), FieldType('N'), FieldWidth(3)]
        public Int64 Nk
        {
            get { return nk; }
            set { CheckIntegerData("Nk", value, 3); nk = value; }
        }

        private string currency;
        // <summary>
        // CURRENCY C(10)
        // </summary>
        [FieldName("CURRENCY"), FieldType('C'), FieldWidth(10)]
        public string Currency
        {
            get { return currency; }
            set { CheckStringData("Currency", value, 10); currency = value; }
        }

        private Int64 org;
        // <summary>
        // ORG N(11)
        // </summary>
        [FieldName("ORG"), FieldType('N'), FieldWidth(11)]
        public Int64 Org
        {
            get { return org; }
            set { CheckIntegerData("Org", value, 11); org = value; }
        }

        private decimal sump;
        // <summary>
        // SUMP N(12,2)
        // </summary>
        [FieldName("SUMP"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sump
        {
            get { return sump; }
            set { CheckDecimalData("Sump", value, 12, 2); sump = value; }
        }

        private decimal sumr;
        // <summary>
        // SUMR N(12,2)
        // </summary>
        [FieldName("SUMR"), FieldType('N'), FieldWidth(12), FieldDec(2)]
        public decimal Sumr
        {
            get { return sumr; }
            set { CheckDecimalData("Sumr", value, 12, 2); sumr = value; }
        }

        private decimal nds;
        // <summary>
        // NDS N(7,2)
        // </summary>
        [FieldName("NDS"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Nds
        {
            get { return nds; }
            set { CheckDecimalData("Nds", value, 7, 2); nds = value; }
        }

        private Int64 id_client;
        // <summary>
        // ID_CLIENT N(11)
        // </summary>
        [FieldName("ID_CLIENT"), FieldType('N'), FieldWidth(11)]
        public Int64 Id_client
        {
            get { return id_client; }
            set { CheckIntegerData("Id_client", value, 11); id_client = value; }
        }

        private string client;
        // <summary>
        // CLIENT C(70)
        // </summary>
        [FieldName("CLIENT"), FieldType('C'), FieldWidth(70)]
        public string Client
        {
            get { return client; }
            set { CheckStringData("Client", value, 70); client = value; }
        }

        private string docpol;
        // <summary>
        // DOCPOL C(100)
        // </summary>
        [FieldName("DOCPOL"), FieldType('C'), FieldWidth(100)]
        public string Docpol
        {
            get { return docpol; }
            set { CheckStringData("Docpol", value, 100); docpol = value; }
        }

        private string _base;
        // <summary>
        // BASE C(70)
        // </summary>
        [FieldName("BASE"), FieldType('C'), FieldWidth(70)]
        public string Base
        {
            get { return _base; }
            set { CheckStringData("Base", value, 70); _base = value; }
        }

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

        private string sd;
        // <summary>
        // SD C(10)
        // </summary>
        [FieldName("SD"), FieldType('C'), FieldWidth(10)]
        public string Sd
        {
            get { return sd; }
            set { CheckStringData("Sd", value, 10); sd = value; }
        }

        private string sk;
        // <summary>
        // SK C(10)
        // </summary>
        [FieldName("SK"), FieldType('C'), FieldWidth(10)]
        public string Sk
        {
            get { return sk; }
            set { CheckStringData("Sk", value, 10); sk = value; }
        }

        private string ksp;
        // <summary>
        // KSP C(10)
        // </summary>
        [FieldName("KSP"), FieldType('C'), FieldWidth(10)]
        public string Ksp
        {
            get { return ksp; }
            set { CheckStringData("Ksp", value, 10); ksp = value; }
        }

        private string kau;
        // <summary>
        // KAU C(10)
        // </summary>
        [FieldName("KAU"), FieldType('C'), FieldWidth(10)]
        public string Kau
        {
            get { return kau; }
            set { CheckStringData("Kau", value, 10); kau = value; }
        }

        private string kcn;
        // <summary>
        // KCN C(10)
        // </summary>
        [FieldName("KCN"), FieldType('C'), FieldWidth(10)]
        public string Kcn
        {
            get { return kcn; }
            set { CheckStringData("Kcn", value, 10); kcn = value; }
        }

        private string msg;
        // <summary>
        // MSG C(100)
        // </summary>
        [FieldName("MSG"), FieldType('C'), FieldWidth(100)]
        public string Msg
        {
            get { return msg; }
            set { CheckStringData("Msg", value, 100); msg = value; }
        }

        private string _int;
        // <summary>
        // INT C(50)
        // </summary>
        [FieldName("INT"), FieldType('C'), FieldWidth(50)]
        public string Int
        {
            get { return _int; }
            set { CheckStringData("Int", value, 50); _int = value; }
        }

        private string pril;
        // <summary>
        // PRIL C(50)
        // </summary>
        [FieldName("PRIL"), FieldType('C'), FieldWidth(50)]
        public string Pril
        {
            get { return pril; }
            set { CheckStringData("Pril", value, 50); pril = value; }
        }

        private Int64 fc;
        // <summary>
        // FC N(2)
        // </summary>
        [FieldName("FC"), FieldType('N'), FieldWidth(2)]
        public Int64 Fc
        {
            get { return fc; }
            set { CheckIntegerData("Fc", value, 2); fc = value; }
        }

        private bool dr;
        // <summary>
        // DR L(1)
        // </summary>
        [FieldName("DR"), FieldType('L'), FieldWidth(1)]
        public bool Dr
        {
            get { return dr; }
            set {  dr = value; }
        }

        private string oper;
        // <summary>
        // OPER C(100)
        // </summary>
        [FieldName("OPER"), FieldType('C'), FieldWidth(100)]
        public string Oper
        {
            get { return oper; }
            set { CheckStringData("Oper", value, 100); oper = value; }
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

        private Int64 iddoc;
        // <summary>
        // IDDOC N(11)
        // </summary>
        [FieldName("IDDOC"), FieldType('N'), FieldWidth(11)]
        public Int64 Iddoc
        {
            get { return iddoc; }
            set { CheckIntegerData("Iddoc", value, 11); iddoc = value; }
        }

        private Int64 flag_ok;
        // <summary>
        // FLAG_OK N(2)
        // </summary>
        [FieldName("FLAG_OK"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_ok
        {
            get { return flag_ok; }
            set { CheckIntegerData("Flag_ok", value, 2); flag_ok = value; }
        }

        private string idself;
        // <summary>
        // IDSELF C(10)
        // </summary>
        [FieldName("IDSELF"), FieldType('C'), FieldWidth(10)]
        public string Idself
        {
            get { return idself; }
            set { CheckStringData("Idself", value, 10); idself = value; }
        }

        private bool div_serv;
        // <summary>
        // DIV_SERV L(1)
        // </summary>
        [FieldName("DIV_SERV"), FieldType('L'), FieldWidth(1)]
        public bool Div_serv
        {
            get { return div_serv; }
            set {  div_serv = value; }
        }

        private string time;
        // <summary>
        // TIME C(8)
        // </summary>
        [FieldName("TIME"), FieldType('C'), FieldWidth(8)]
        public string Time
        {
            get { return time; }
            set { CheckStringData("Time", value, 8); time = value; }
        }

        private string author;
        // <summary>
        // AUTHOR C(20)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(20)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 20); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NK")) Nk = Convert.ToInt64(ADataRow["NK"]); else Nk = 0;
            if (ADataRow.Table.Columns.Contains("CURRENCY")) Currency = ADataRow["CURRENCY"].ToString(); else Currency = "";
            if (ADataRow.Table.Columns.Contains("ORG")) Org = Convert.ToInt64(ADataRow["ORG"]); else Org = 0;
            if (ADataRow.Table.Columns.Contains("SUMP")) Sump = Convert.ToDecimal(ADataRow["SUMP"]); else Sump = 0;
            if (ADataRow.Table.Columns.Contains("SUMR")) Sumr = Convert.ToDecimal(ADataRow["SUMR"]); else Sumr = 0;
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = Convert.ToDecimal(ADataRow["NDS"]); else Nds = 0;
            if (ADataRow.Table.Columns.Contains("ID_CLIENT")) Id_client = Convert.ToInt64(ADataRow["ID_CLIENT"]); else Id_client = 0;
            if (ADataRow.Table.Columns.Contains("CLIENT")) Client = ADataRow["CLIENT"].ToString(); else Client = "";
            if (ADataRow.Table.Columns.Contains("DOCPOL")) Docpol = ADataRow["DOCPOL"].ToString(); else Docpol = "";
            if (ADataRow.Table.Columns.Contains("BASE")) Base = ADataRow["BASE"].ToString(); else Base = "";
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("SD")) Sd = ADataRow["SD"].ToString(); else Sd = "";
            if (ADataRow.Table.Columns.Contains("SK")) Sk = ADataRow["SK"].ToString(); else Sk = "";
            if (ADataRow.Table.Columns.Contains("KSP")) Ksp = ADataRow["KSP"].ToString(); else Ksp = "";
            if (ADataRow.Table.Columns.Contains("KAU")) Kau = ADataRow["KAU"].ToString(); else Kau = "";
            if (ADataRow.Table.Columns.Contains("KCN")) Kcn = ADataRow["KCN"].ToString(); else Kcn = "";
            if (ADataRow.Table.Columns.Contains("MSG")) Msg = ADataRow["MSG"].ToString(); else Msg = "";
            if (ADataRow.Table.Columns.Contains("INT")) Int = ADataRow["INT"].ToString(); else Int = "";
            if (ADataRow.Table.Columns.Contains("PRIL")) Pril = ADataRow["PRIL"].ToString(); else Pril = "";
            if (ADataRow.Table.Columns.Contains("FC")) Fc = Convert.ToInt64(ADataRow["FC"]); else Fc = 0;
            if (ADataRow.Table.Columns.Contains("DR")) Dr = ADataRow["DR"].ToString() == "True" ? true : false; else Dr = false;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = ADataRow["OPER"].ToString(); else Oper = "";
            if (ADataRow.Table.Columns.Contains("KOMM")) Komm = ADataRow["KOMM"].ToString() == "True" ? true : false; else Komm = false;
            if (ADataRow.Table.Columns.Contains("IDDOC")) Iddoc = Convert.ToInt64(ADataRow["IDDOC"]); else Iddoc = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_OK")) Flag_ok = Convert.ToInt64(ADataRow["FLAG_OK"]); else Flag_ok = 0;
            if (ADataRow.Table.Columns.Contains("IDSELF")) Idself = ADataRow["IDSELF"].ToString(); else Idself = "";
            if (ADataRow.Table.Columns.Contains("DIV_SERV")) Div_serv = ADataRow["DIV_SERV"].ToString() == "True" ? true : false; else Div_serv = false;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = ADataRow["TIME"].ToString(); else Time = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            KassaRecord retValue = new KassaRecord();
            retValue.Dat = this.Dat;
            retValue.Num = this.Num;
            retValue.Id = this.Id;
            retValue.Nk = this.Nk;
            retValue.Currency = this.Currency;
            retValue.Org = this.Org;
            retValue.Sump = this.Sump;
            retValue.Sumr = this.Sumr;
            retValue.Nds = this.Nds;
            retValue.Id_client = this.Id_client;
            retValue.Client = this.Client;
            retValue.Docpol = this.Docpol;
            retValue.Base = this.Base;
            retValue.Ls = this.Ls;
            retValue.Sd = this.Sd;
            retValue.Sk = this.Sk;
            retValue.Ksp = this.Ksp;
            retValue.Kau = this.Kau;
            retValue.Kcn = this.Kcn;
            retValue.Msg = this.Msg;
            retValue.Int = this.Int;
            retValue.Pril = this.Pril;
            retValue.Fc = this.Fc;
            retValue.Dr = this.Dr;
            retValue.Oper = this.Oper;
            retValue.Komm = this.Komm;
            retValue.Iddoc = this.Iddoc;
            retValue.Flag_ok = this.Flag_ok;
            retValue.Idself = this.Idself;
            retValue.Div_serv = this.Div_serv;
            retValue.Time = this.Time;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO KASSA (DAT, NUM, ID, NK, CURRENCY, ORG, SUMP, SUMR, NDS, ID_CLIENT, CLIENT, DOCPOL, BASE, LS, SD, SK, KSP, KAU, KCN, MSG, INT, PRIL, FC, DR, OPER, KOMM, IDDOC, FLAG_OK, IDSELF, DIV_SERV, TIME, AUTHOR) VALUES (CTOD('{0}'), {1}, '{2}', {3}, '{4}', {5}, {6}, {7}, {8}, {9}, '{10}', '{11}', '{12}', {13}, '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', {22}, {23}, '{24}', {25}, {26}, {27}, '{28}', {29}, '{30}', '{31}')", Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Num.ToString(), String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Nk.ToString(), String.IsNullOrEmpty(Currency) ? "" : Currency.Trim(), Org.ToString(), Sump.ToString().Replace(',','.'), Sumr.ToString().Replace(',','.'), Nds.ToString().Replace(',','.'), Id_client.ToString(), String.IsNullOrEmpty(Client) ? "" : Client.Trim(), String.IsNullOrEmpty(Docpol) ? "" : Docpol.Trim(), String.IsNullOrEmpty(Base) ? "" : Base.Trim(), Ls.ToString(), String.IsNullOrEmpty(Sd) ? "" : Sd.Trim(), String.IsNullOrEmpty(Sk) ? "" : Sk.Trim(), String.IsNullOrEmpty(Ksp) ? "" : Ksp.Trim(), String.IsNullOrEmpty(Kau) ? "" : Kau.Trim(), String.IsNullOrEmpty(Kcn) ? "" : Kcn.Trim(), String.IsNullOrEmpty(Msg) ? "" : Msg.Trim(), String.IsNullOrEmpty(Int) ? "" : Int.Trim(), String.IsNullOrEmpty(Pril) ? "" : Pril.Trim(), Fc.ToString(), (Dr ? 0 : 1 ), String.IsNullOrEmpty(Oper) ? "" : Oper.Trim(), (Komm ? 0 : 1 ), Iddoc.ToString(), Flag_ok.ToString(), String.IsNullOrEmpty(Idself) ? "" : Idself.Trim(), (Div_serv ? 0 : 1 ), String.IsNullOrEmpty(Time) ? "" : Time.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
