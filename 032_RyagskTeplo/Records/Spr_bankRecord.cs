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
    [TableName("SPR_BANK.DBF")]
    public partial class Spr_bankRecord: AbstractRecord
    {
        private Int64 cod;
        // <summary>
        // COD N(6)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(6)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 6); cod = value; }
        }

        private string name;
        // <summary>
        // NAME C(200)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(200)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 200); name = value; }
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
        // BIK C(9)
        // </summary>
        [FieldName("BIK"), FieldType('C'), FieldWidth(9)]
        public string Bik
        {
            get { return bik; }
            set { CheckStringData("Bik", value, 9); bik = value; }
        }

        private string addres;
        // <summary>
        // ADDRES C(100)
        // </summary>
        [FieldName("ADDRES"), FieldType('C'), FieldWidth(100)]
        public string Addres
        {
            get { return addres; }
            set { CheckStringData("Addres", value, 100); addres = value; }
        }

        private string fio;
        // <summary>
        // FIO C(50)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(50)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 50); fio = value; }
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

        private string fax;
        // <summary>
        // FAX C(30)
        // </summary>
        [FieldName("FAX"), FieldType('C'), FieldWidth(30)]
        public string Fax
        {
            get { return fax; }
            set { CheckStringData("Fax", value, 30); fax = value; }
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

        private string info;
        // <summary>
        // INFO C(4)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(4)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 4); info = value; }
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

        private string tnp;
        // <summary>
        // TNP C(10)
        // </summary>
        [FieldName("TNP"), FieldType('C'), FieldWidth(10)]
        public string Tnp
        {
            get { return tnp; }
            set { CheckStringData("Tnp", value, 10); tnp = value; }
        }

        private string nnp;
        // <summary>
        // NNP C(50)
        // </summary>
        [FieldName("NNP"), FieldType('C'), FieldWidth(50)]
        public string Nnp
        {
            get { return nnp; }
            set { CheckStringData("Nnp", value, 50); nnp = value; }
        }

        private string rkc;
        // <summary>
        // RKC C(9)
        // </summary>
        [FieldName("RKC"), FieldType('C'), FieldWidth(9)]
        public string Rkc
        {
            get { return rkc; }
            set { CheckStringData("Rkc", value, 9); rkc = value; }
        }

        private string okpo;
        // <summary>
        // OKPO C(8)
        // </summary>
        [FieldName("OKPO"), FieldType('C'), FieldWidth(8)]
        public string Okpo
        {
            get { return okpo; }
            set { CheckStringData("Okpo", value, 8); okpo = value; }
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

        private DateTime datreg;
        // <summary>
        // DATREG D(8)
        // </summary>
        [FieldName("DATREG"), FieldType('D'), FieldWidth(8)]
        public DateTime Datreg
        {
            get { return datreg; }
            set {  datreg = value; }
        }

        private string numlic;
        // <summary>
        // NUMLIC C(20)
        // </summary>
        [FieldName("NUMLIC"), FieldType('C'), FieldWidth(20)]
        public string Numlic
        {
            get { return numlic; }
            set { CheckStringData("Numlic", value, 20); numlic = value; }
        }

        private DateTime datlic;
        // <summary>
        // DATLIC D(8)
        // </summary>
        [FieldName("DATLIC"), FieldType('D'), FieldWidth(8)]
        public DateTime Datlic
        {
            get { return datlic; }
            set {  datlic = value; }
        }

        private string numsv;
        // <summary>
        // NUMSV C(20)
        // </summary>
        [FieldName("NUMSV"), FieldType('C'), FieldWidth(20)]
        public string Numsv
        {
            get { return numsv; }
            set { CheckStringData("Numsv", value, 20); numsv = value; }
        }

        private DateTime datsv;
        // <summary>
        // DATSV D(8)
        // </summary>
        [FieldName("DATSV"), FieldType('D'), FieldWidth(8)]
        public DateTime Datsv
        {
            get { return datsv; }
            set {  datsv = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("KS")) Ks = ADataRow["KS"].ToString(); else Ks = "";
            if (ADataRow.Table.Columns.Contains("BIK")) Bik = ADataRow["BIK"].ToString(); else Bik = "";
            if (ADataRow.Table.Columns.Contains("ADDRES")) Addres = ADataRow["ADDRES"].ToString(); else Addres = "";
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("TELEFON")) Telefon = ADataRow["TELEFON"].ToString(); else Telefon = "";
            if (ADataRow.Table.Columns.Contains("FAX")) Fax = ADataRow["FAX"].ToString(); else Fax = "";
            if (ADataRow.Table.Columns.Contains("NAME_SHORT")) Name_short = ADataRow["NAME_SHORT"].ToString(); else Name_short = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("INDEX")) Index = ADataRow["INDEX"].ToString(); else Index = "";
            if (ADataRow.Table.Columns.Contains("TNP")) Tnp = ADataRow["TNP"].ToString(); else Tnp = "";
            if (ADataRow.Table.Columns.Contains("NNP")) Nnp = ADataRow["NNP"].ToString(); else Nnp = "";
            if (ADataRow.Table.Columns.Contains("RKC")) Rkc = ADataRow["RKC"].ToString(); else Rkc = "";
            if (ADataRow.Table.Columns.Contains("OKPO")) Okpo = ADataRow["OKPO"].ToString(); else Okpo = "";
            if (ADataRow.Table.Columns.Contains("OGRN")) Ogrn = ADataRow["OGRN"].ToString(); else Ogrn = "";
            if (ADataRow.Table.Columns.Contains("INN")) Inn = ADataRow["INN"].ToString(); else Inn = "";
            if (ADataRow.Table.Columns.Contains("KPP")) Kpp = ADataRow["KPP"].ToString(); else Kpp = "";
            if (ADataRow.Table.Columns.Contains("DATREG")) Datreg = Convert.ToDateTime(ADataRow["DATREG"]); else Datreg = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NUMLIC")) Numlic = ADataRow["NUMLIC"].ToString(); else Numlic = "";
            if (ADataRow.Table.Columns.Contains("DATLIC")) Datlic = Convert.ToDateTime(ADataRow["DATLIC"]); else Datlic = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NUMSV")) Numsv = ADataRow["NUMSV"].ToString(); else Numsv = "";
            if (ADataRow.Table.Columns.Contains("DATSV")) Datsv = Convert.ToDateTime(ADataRow["DATSV"]); else Datsv = DateTime.MinValue;
        }
        
        public override AbstractRecord Clone()
        {
            Spr_bankRecord retValue = new Spr_bankRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Ks = this.Ks;
            retValue.Bik = this.Bik;
            retValue.Addres = this.Addres;
            retValue.Fio = this.Fio;
            retValue.Telefon = this.Telefon;
            retValue.Fax = this.Fax;
            retValue.Name_short = this.Name_short;
            retValue.Info = this.Info;
            retValue.Index = this.Index;
            retValue.Tnp = this.Tnp;
            retValue.Nnp = this.Nnp;
            retValue.Rkc = this.Rkc;
            retValue.Okpo = this.Okpo;
            retValue.Ogrn = this.Ogrn;
            retValue.Inn = this.Inn;
            retValue.Kpp = this.Kpp;
            retValue.Datreg = this.Datreg;
            retValue.Numlic = this.Numlic;
            retValue.Datlic = this.Datlic;
            retValue.Numsv = this.Numsv;
            retValue.Datsv = this.Datsv;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPR_BANK (COD, NAME, KS, BIK, ADDRES, FIO, TELEFON, FAX, NAME_SHORT, INFO, INDEX, TNP, NNP, RKC, OKPO, OGRN, INN, KPP, DATREG, NUMLIC, DATLIC, NUMSV, DATSV) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', CTOD('{18}'), '{19}', CTOD('{20}'), '{21}', CTOD('{22}'))", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Ks) ? "" : Ks.Trim(), String.IsNullOrEmpty(Bik) ? "" : Bik.Trim(), String.IsNullOrEmpty(Addres) ? "" : Addres.Trim(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Telefon) ? "" : Telefon.Trim(), String.IsNullOrEmpty(Fax) ? "" : Fax.Trim(), String.IsNullOrEmpty(Name_short) ? "" : Name_short.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), String.IsNullOrEmpty(Index) ? "" : Index.Trim(), String.IsNullOrEmpty(Tnp) ? "" : Tnp.Trim(), String.IsNullOrEmpty(Nnp) ? "" : Nnp.Trim(), String.IsNullOrEmpty(Rkc) ? "" : Rkc.Trim(), String.IsNullOrEmpty(Okpo) ? "" : Okpo.Trim(), String.IsNullOrEmpty(Ogrn) ? "" : Ogrn.Trim(), String.IsNullOrEmpty(Inn) ? "" : Inn.Trim(), String.IsNullOrEmpty(Kpp) ? "" : Kpp.Trim(), Datreg == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datreg.Month, Datreg.Day, Datreg.Year), String.IsNullOrEmpty(Numlic) ? "" : Numlic.Trim(), Datlic == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datlic.Month, Datlic.Day, Datlic.Year), String.IsNullOrEmpty(Numsv) ? "" : Numsv.Trim(), Datsv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datsv.Month, Datsv.Day, Datsv.Year));
            return rs;
        }
    }
}
