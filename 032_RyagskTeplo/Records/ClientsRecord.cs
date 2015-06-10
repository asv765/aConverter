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
    [TableName("Clients.DBF")]
    public partial class ClientsRecord: AbstractRecord
    {
        private Int64 cod;
        // <summary>
        // COD N(11)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(11)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 11); cod = value; }
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

        private Int64 typec;
        // <summary>
        // TYPEC N(3)
        // </summary>
        [FieldName("TYPEC"), FieldType('N'), FieldWidth(3)]
        public Int64 Typec
        {
            get { return typec; }
            set { CheckIntegerData("Typec", value, 3); typec = value; }
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

        private string telefons;
        // <summary>
        // TELEFONS C(50)
        // </summary>
        [FieldName("TELEFONS"), FieldType('C'), FieldWidth(50)]
        public string Telefons
        {
            get { return telefons; }
            set { CheckStringData("Telefons", value, 50); telefons = value; }
        }

        private string addresf;
        // <summary>
        // ADDRESF C(100)
        // </summary>
        [FieldName("ADDRESF"), FieldType('C'), FieldWidth(100)]
        public string Addresf
        {
            get { return addresf; }
            set { CheckStringData("Addresf", value, 100); addresf = value; }
        }

        private string kpp;
        // <summary>
        // KPP C(12)
        // </summary>
        [FieldName("KPP"), FieldType('C'), FieldWidth(12)]
        public string Kpp
        {
            get { return kpp; }
            set { CheckStringData("Kpp", value, 12); kpp = value; }
        }

        private string okpo;
        // <summary>
        // OKPO C(20)
        // </summary>
        [FieldName("OKPO"), FieldType('C'), FieldWidth(20)]
        public string Okpo
        {
            get { return okpo; }
            set { CheckStringData("Okpo", value, 20); okpo = value; }
        }

        private string koddoku;
        // <summary>
        // KODDOKU C(2)
        // </summary>
        [FieldName("KODDOKU"), FieldType('C'), FieldWidth(2)]
        public string Koddoku
        {
            get { return koddoku; }
            set { CheckStringData("Koddoku", value, 2); koddoku = value; }
        }

        private string sdoc;
        // <summary>
        // SDOC C(8)
        // </summary>
        [FieldName("SDOC"), FieldType('C'), FieldWidth(8)]
        public string Sdoc
        {
            get { return sdoc; }
            set { CheckStringData("Sdoc", value, 8); sdoc = value; }
        }

        private string ndoc;
        // <summary>
        // NDOC C(8)
        // </summary>
        [FieldName("NDOC"), FieldType('C'), FieldWidth(8)]
        public string Ndoc
        {
            get { return ndoc; }
            set { CheckStringData("Ndoc", value, 8); ndoc = value; }
        }

        private string vidanke;
        // <summary>
        // VIDANKE C(30)
        // </summary>
        [FieldName("VIDANKE"), FieldType('C'), FieldWidth(30)]
        public string Vidanke
        {
            get { return vidanke; }
            set { CheckStringData("Vidanke", value, 30); vidanke = value; }
        }

        private DateTime vidanko;
        // <summary>
        // VIDANKO D(8)
        // </summary>
        [FieldName("VIDANKO"), FieldType('D'), FieldWidth(8)]
        public DateTime Vidanko
        {
            get { return vidanko; }
            set {  vidanko = value; }
        }

        private DateTime dataroj;
        // <summary>
        // DATAROJ D(8)
        // </summary>
        [FieldName("DATAROJ"), FieldType('D'), FieldWidth(8)]
        public DateTime Dataroj
        {
            get { return dataroj; }
            set {  dataroj = value; }
        }

        private Int64 ls;
        // <summary>
        // LS N(7)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 7); ls = value; }
        }

        private string addresu;
        // <summary>
        // ADDRESU C(100)
        // </summary>
        [FieldName("ADDRESU"), FieldType('C'), FieldWidth(100)]
        public string Addresu
        {
            get { return addresu; }
            set { CheckStringData("Addresu", value, 100); addresu = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("TYPEC")) Typec = Convert.ToInt64(ADataRow["TYPEC"]); else Typec = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("INN")) Inn = ADataRow["INN"].ToString(); else Inn = "";
            if (ADataRow.Table.Columns.Contains("TELEFONS")) Telefons = ADataRow["TELEFONS"].ToString(); else Telefons = "";
            if (ADataRow.Table.Columns.Contains("ADDRESF")) Addresf = ADataRow["ADDRESF"].ToString(); else Addresf = "";
            if (ADataRow.Table.Columns.Contains("KPP")) Kpp = ADataRow["KPP"].ToString(); else Kpp = "";
            if (ADataRow.Table.Columns.Contains("OKPO")) Okpo = ADataRow["OKPO"].ToString(); else Okpo = "";
            if (ADataRow.Table.Columns.Contains("KODDOKU")) Koddoku = ADataRow["KODDOKU"].ToString(); else Koddoku = "";
            if (ADataRow.Table.Columns.Contains("SDOC")) Sdoc = ADataRow["SDOC"].ToString(); else Sdoc = "";
            if (ADataRow.Table.Columns.Contains("NDOC")) Ndoc = ADataRow["NDOC"].ToString(); else Ndoc = "";
            if (ADataRow.Table.Columns.Contains("VIDANKE")) Vidanke = ADataRow["VIDANKE"].ToString(); else Vidanke = "";
            if (ADataRow.Table.Columns.Contains("VIDANKO")) Vidanko = Convert.ToDateTime(ADataRow["VIDANKO"]); else Vidanko = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATAROJ")) Dataroj = Convert.ToDateTime(ADataRow["DATAROJ"]); else Dataroj = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("ADDRESU")) Addresu = ADataRow["ADDRESU"].ToString(); else Addresu = "";
        }
        
        public override AbstractRecord Clone()
        {
            ClientsRecord retValue = new ClientsRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Typec = this.Typec;
            retValue.Fio = this.Fio;
            retValue.Inn = this.Inn;
            retValue.Telefons = this.Telefons;
            retValue.Addresf = this.Addresf;
            retValue.Kpp = this.Kpp;
            retValue.Okpo = this.Okpo;
            retValue.Koddoku = this.Koddoku;
            retValue.Sdoc = this.Sdoc;
            retValue.Ndoc = this.Ndoc;
            retValue.Vidanke = this.Vidanke;
            retValue.Vidanko = this.Vidanko;
            retValue.Dataroj = this.Dataroj;
            retValue.Ls = this.Ls;
            retValue.Addresu = this.Addresu;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO Clients (COD, NAME, TYPEC, FIO, INN, TELEFONS, ADDRESF, KPP, OKPO, KODDOKU, SDOC, NDOC, VIDANKE, VIDANKO, DATAROJ, LS, ADDRESU) VALUES ({0}, '{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', CTOD('{13}'), CTOD('{14}'), {15}, '{16}')", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Typec.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Inn) ? "" : Inn.Trim(), String.IsNullOrEmpty(Telefons) ? "" : Telefons.Trim(), String.IsNullOrEmpty(Addresf) ? "" : Addresf.Trim(), String.IsNullOrEmpty(Kpp) ? "" : Kpp.Trim(), String.IsNullOrEmpty(Okpo) ? "" : Okpo.Trim(), String.IsNullOrEmpty(Koddoku) ? "" : Koddoku.Trim(), String.IsNullOrEmpty(Sdoc) ? "" : Sdoc.Trim(), String.IsNullOrEmpty(Ndoc) ? "" : Ndoc.Trim(), String.IsNullOrEmpty(Vidanke) ? "" : Vidanke.Trim(), Vidanko == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Vidanko.Month, Vidanko.Day, Vidanko.Year), Dataroj == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dataroj.Month, Dataroj.Day, Dataroj.Year), Ls.ToString(), String.IsNullOrEmpty(Addresu) ? "" : Addresu.Trim());
            return rs;
        }
    }
}
