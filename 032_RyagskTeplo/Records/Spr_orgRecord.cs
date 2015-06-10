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
    [TableName("SPR_ORG.DBF")]
    public partial class Spr_orgRecord: AbstractRecord
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
        // NAME C(200)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(200)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 200); name = value; }
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

        private string rs1;
        // <summary>
        // RS1 C(20)
        // </summary>
        [FieldName("RS1"), FieldType('C'), FieldWidth(20)]
        public string Rs1
        {
            get { return rs1; }
            set { CheckStringData("Rs1", value, 20); rs1 = value; }
        }

        private Int64 bank1;
        // <summary>
        // BANK1 N(6)
        // </summary>
        [FieldName("BANK1"), FieldType('N'), FieldWidth(6)]
        public Int64 Bank1
        {
            get { return bank1; }
            set { CheckIntegerData("Bank1", value, 6); bank1 = value; }
        }

        private string rs2;
        // <summary>
        // RS2 C(20)
        // </summary>
        [FieldName("RS2"), FieldType('C'), FieldWidth(20)]
        public string Rs2
        {
            get { return rs2; }
            set { CheckStringData("Rs2", value, 20); rs2 = value; }
        }

        private Int64 bank2;
        // <summary>
        // BANK2 N(6)
        // </summary>
        [FieldName("BANK2"), FieldType('N'), FieldWidth(6)]
        public Int64 Bank2
        {
            get { return bank2; }
            set { CheckIntegerData("Bank2", value, 6); bank2 = value; }
        }

        private string rs3;
        // <summary>
        // RS3 C(20)
        // </summary>
        [FieldName("RS3"), FieldType('C'), FieldWidth(20)]
        public string Rs3
        {
            get { return rs3; }
            set { CheckStringData("Rs3", value, 20); rs3 = value; }
        }

        private Int64 bank3;
        // <summary>
        // BANK3 N(6)
        // </summary>
        [FieldName("BANK3"), FieldType('N'), FieldWidth(6)]
        public Int64 Bank3
        {
            get { return bank3; }
            set { CheckIntegerData("Bank3", value, 6); bank3 = value; }
        }

        private string rs4;
        // <summary>
        // RS4 C(20)
        // </summary>
        [FieldName("RS4"), FieldType('C'), FieldWidth(20)]
        public string Rs4
        {
            get { return rs4; }
            set { CheckStringData("Rs4", value, 20); rs4 = value; }
        }

        private Int64 bank4;
        // <summary>
        // BANK4 N(6)
        // </summary>
        [FieldName("BANK4"), FieldType('N'), FieldWidth(6)]
        public Int64 Bank4
        {
            get { return bank4; }
            set { CheckIntegerData("Bank4", value, 6); bank4 = value; }
        }

        private string rs5;
        // <summary>
        // RS5 C(20)
        // </summary>
        [FieldName("RS5"), FieldType('C'), FieldWidth(20)]
        public string Rs5
        {
            get { return rs5; }
            set { CheckStringData("Rs5", value, 20); rs5 = value; }
        }

        private Int64 bank5;
        // <summary>
        // BANK5 N(6)
        // </summary>
        [FieldName("BANK5"), FieldType('N'), FieldWidth(6)]
        public Int64 Bank5
        {
            get { return bank5; }
            set { CheckIntegerData("Bank5", value, 6); bank5 = value; }
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

        private string okonh;
        // <summary>
        // OKONH C(10)
        // </summary>
        [FieldName("OKONH"), FieldType('C'), FieldWidth(10)]
        public string Okonh
        {
            get { return okonh; }
            set { CheckStringData("Okonh", value, 10); okonh = value; }
        }

        private string okpo;
        // <summary>
        // OKPO C(12)
        // </summary>
        [FieldName("OKPO"), FieldType('C'), FieldWidth(12)]
        public string Okpo
        {
            get { return okpo; }
            set { CheckStringData("Okpo", value, 12); okpo = value; }
        }

        private string okdp;
        // <summary>
        // OKDP C(20)
        // </summary>
        [FieldName("OKDP"), FieldType('C'), FieldWidth(20)]
        public string Okdp
        {
            get { return okdp; }
            set { CheckStringData("Okdp", value, 20); okdp = value; }
        }

        private string okopf;
        // <summary>
        // OKOPF C(20)
        // </summary>
        [FieldName("OKOPF"), FieldType('C'), FieldWidth(20)]
        public string Okopf
        {
            get { return okopf; }
            set { CheckStringData("Okopf", value, 20); okopf = value; }
        }

        private string okfs;
        // <summary>
        // OKFS C(20)
        // </summary>
        [FieldName("OKFS"), FieldType('C'), FieldWidth(20)]
        public string Okfs
        {
            get { return okfs; }
            set { CheckStringData("Okfs", value, 20); okfs = value; }
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

        private string addres;
        // <summary>
        // ADDRES C(70)
        // </summary>
        [FieldName("ADDRES"), FieldType('C'), FieldWidth(70)]
        public string Addres
        {
            get { return addres; }
            set { CheckStringData("Addres", value, 70); addres = value; }
        }

        private string addres_f;
        // <summary>
        // ADDRES_F C(70)
        // </summary>
        [FieldName("ADDRES_F"), FieldType('C'), FieldWidth(70)]
        public string Addres_f
        {
            get { return addres_f; }
            set { CheckStringData("Addres_f", value, 70); addres_f = value; }
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

        private string dolj_rukov;
        // <summary>
        // DOLJ_RUKOV C(30)
        // </summary>
        [FieldName("DOLJ_RUKOV"), FieldType('C'), FieldWidth(30)]
        public string Dolj_rukov
        {
            get { return dolj_rukov; }
            set { CheckStringData("Dolj_rukov", value, 30); dolj_rukov = value; }
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

        private bool mylabel;
        // <summary>
        // MYLABEL L(1)
        // </summary>
        [FieldName("MYLABEL"), FieldType('L'), FieldWidth(1)]
        public bool Mylabel
        {
            get { return mylabel; }
            set {  mylabel = value; }
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

        private string okato;
        // <summary>
        // OKATO C(20)
        // </summary>
        [FieldName("OKATO"), FieldType('C'), FieldWidth(20)]
        public string Okato
        {
            get { return okato; }
            set { CheckStringData("Okato", value, 20); okato = value; }
        }

        private string okved;
        // <summary>
        // OKVED C(20)
        // </summary>
        [FieldName("OKVED"), FieldType('C'), FieldWidth(20)]
        public string Okved
        {
            get { return okved; }
            set { CheckStringData("Okved", value, 20); okved = value; }
        }

        private string fio_kas;
        // <summary>
        // FIO_KAS C(50)
        // </summary>
        [FieldName("FIO_KAS"), FieldType('C'), FieldWidth(50)]
        public string Fio_kas
        {
            get { return fio_kas; }
            set { CheckStringData("Fio_kas", value, 50); fio_kas = value; }
        }

        private string kassa1;
        // <summary>
        // KASSA1 C(50)
        // </summary>
        [FieldName("KASSA1"), FieldType('C'), FieldWidth(50)]
        public string Kassa1
        {
            get { return kassa1; }
            set { CheckStringData("Kassa1", value, 50); kassa1 = value; }
        }

        private string kassa2;
        // <summary>
        // KASSA2 C(50)
        // </summary>
        [FieldName("KASSA2"), FieldType('C'), FieldWidth(50)]
        public string Kassa2
        {
            get { return kassa2; }
            set { CheckStringData("Kassa2", value, 50); kassa2 = value; }
        }

        private string kassa3;
        // <summary>
        // KASSA3 C(50)
        // </summary>
        [FieldName("KASSA3"), FieldType('C'), FieldWidth(50)]
        public string Kassa3
        {
            get { return kassa3; }
            set { CheckStringData("Kassa3", value, 50); kassa3 = value; }
        }

        private string kassa4;
        // <summary>
        // KASSA4 C(50)
        // </summary>
        [FieldName("KASSA4"), FieldType('C'), FieldWidth(50)]
        public string Kassa4
        {
            get { return kassa4; }
            set { CheckStringData("Kassa4", value, 50); kassa4 = value; }
        }

        private string kassa5;
        // <summary>
        // KASSA5 C(50)
        // </summary>
        [FieldName("KASSA5"), FieldType('C'), FieldWidth(50)]
        public string Kassa5
        {
            get { return kassa5; }
            set { CheckStringData("Kassa5", value, 50); kassa5 = value; }
        }

        private string email;
        // <summary>
        // EMAIL C(20)
        // </summary>
        [FieldName("EMAIL"), FieldType('C'), FieldWidth(20)]
        public string Email
        {
            get { return email; }
            set { CheckStringData("Email", value, 20); email = value; }
        }

        private string nalog;
        // <summary>
        // NALOG C(50)
        // </summary>
        [FieldName("NALOG"), FieldType('C'), FieldWidth(50)]
        public string Nalog
        {
            get { return nalog; }
            set { CheckStringData("Nalog", value, 50); nalog = value; }
        }

        private string num_usno;
        // <summary>
        // NUM_USNO C(12)
        // </summary>
        [FieldName("NUM_USNO"), FieldType('C'), FieldWidth(12)]
        public string Num_usno
        {
            get { return num_usno; }
            set { CheckStringData("Num_usno", value, 12); num_usno = value; }
        }

        private DateTime dat_usno;
        // <summary>
        // DAT_USNO D(8)
        // </summary>
        [FieldName("DAT_USNO"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat_usno
        {
            get { return dat_usno; }
            set {  dat_usno = value; }
        }

        private Int64 form;
        // <summary>
        // FORM N(2)
        // </summary>
        [FieldName("FORM"), FieldType('N'), FieldWidth(2)]
        public Int64 Form
        {
            get { return form; }
            set { CheckIntegerData("Form", value, 2); form = value; }
        }

        private string fio_ip;
        // <summary>
        // FIO_IP C(50)
        // </summary>
        [FieldName("FIO_IP"), FieldType('C'), FieldWidth(50)]
        public string Fio_ip
        {
            get { return fio_ip; }
            set { CheckStringData("Fio_ip", value, 50); fio_ip = value; }
        }

        private string cod_ifns;
        // <summary>
        // COD_IFNS C(4)
        // </summary>
        [FieldName("COD_IFNS"), FieldType('C'), FieldWidth(4)]
        public string Cod_ifns
        {
            get { return cod_ifns; }
            set { CheckStringData("Cod_ifns", value, 4); cod_ifns = value; }
        }

        private string name_ifns;
        // <summary>
        // NAME_IFNS C(150)
        // </summary>
        [FieldName("NAME_IFNS"), FieldType('C'), FieldWidth(150)]
        public string Name_ifns
        {
            get { return name_ifns; }
            set { CheckStringData("Name_ifns", value, 150); name_ifns = value; }
        }

        private Int64 typeno;
        // <summary>
        // TYPENO N(2)
        // </summary>
        [FieldName("TYPENO"), FieldType('N'), FieldWidth(2)]
        public Int64 Typeno
        {
            get { return typeno; }
            set { CheckIntegerData("Typeno", value, 2); typeno = value; }
        }

        private string uslugi;
        // <summary>
        // USLUGI C(100)
        // </summary>
        [FieldName("USLUGI"), FieldType('C'), FieldWidth(100)]
        public string Uslugi
        {
            get { return uslugi; }
            set { CheckStringData("Uslugi", value, 100); uslugi = value; }
        }

        private bool main_pos;
        // <summary>
        // MAIN_POS L(1)
        // </summary>
        [FieldName("MAIN_POS"), FieldType('L'), FieldWidth(1)]
        public bool Main_pos
        {
            get { return main_pos; }
            set {  main_pos = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("NAME_SHORT")) Name_short = ADataRow["NAME_SHORT"].ToString(); else Name_short = "";
            if (ADataRow.Table.Columns.Contains("RS1")) Rs1 = ADataRow["RS1"].ToString(); else Rs1 = "";
            if (ADataRow.Table.Columns.Contains("BANK1")) Bank1 = Convert.ToInt64(ADataRow["BANK1"]); else Bank1 = 0;
            if (ADataRow.Table.Columns.Contains("RS2")) Rs2 = ADataRow["RS2"].ToString(); else Rs2 = "";
            if (ADataRow.Table.Columns.Contains("BANK2")) Bank2 = Convert.ToInt64(ADataRow["BANK2"]); else Bank2 = 0;
            if (ADataRow.Table.Columns.Contains("RS3")) Rs3 = ADataRow["RS3"].ToString(); else Rs3 = "";
            if (ADataRow.Table.Columns.Contains("BANK3")) Bank3 = Convert.ToInt64(ADataRow["BANK3"]); else Bank3 = 0;
            if (ADataRow.Table.Columns.Contains("RS4")) Rs4 = ADataRow["RS4"].ToString(); else Rs4 = "";
            if (ADataRow.Table.Columns.Contains("BANK4")) Bank4 = Convert.ToInt64(ADataRow["BANK4"]); else Bank4 = 0;
            if (ADataRow.Table.Columns.Contains("RS5")) Rs5 = ADataRow["RS5"].ToString(); else Rs5 = "";
            if (ADataRow.Table.Columns.Contains("BANK5")) Bank5 = Convert.ToInt64(ADataRow["BANK5"]); else Bank5 = 0;
            if (ADataRow.Table.Columns.Contains("INN")) Inn = ADataRow["INN"].ToString(); else Inn = "";
            if (ADataRow.Table.Columns.Contains("KPP")) Kpp = ADataRow["KPP"].ToString(); else Kpp = "";
            if (ADataRow.Table.Columns.Contains("OKONH")) Okonh = ADataRow["OKONH"].ToString(); else Okonh = "";
            if (ADataRow.Table.Columns.Contains("OKPO")) Okpo = ADataRow["OKPO"].ToString(); else Okpo = "";
            if (ADataRow.Table.Columns.Contains("OKDP")) Okdp = ADataRow["OKDP"].ToString(); else Okdp = "";
            if (ADataRow.Table.Columns.Contains("OKOPF")) Okopf = ADataRow["OKOPF"].ToString(); else Okopf = "";
            if (ADataRow.Table.Columns.Contains("OKFS")) Okfs = ADataRow["OKFS"].ToString(); else Okfs = "";
            if (ADataRow.Table.Columns.Contains("VID_WORK")) Vid_work = ADataRow["VID_WORK"].ToString(); else Vid_work = "";
            if (ADataRow.Table.Columns.Contains("FORM_SOBS")) Form_sobs = ADataRow["FORM_SOBS"].ToString(); else Form_sobs = "";
            if (ADataRow.Table.Columns.Contains("ADDRES")) Addres = ADataRow["ADDRES"].ToString(); else Addres = "";
            if (ADataRow.Table.Columns.Contains("ADDRES_F")) Addres_f = ADataRow["ADDRES_F"].ToString(); else Addres_f = "";
            if (ADataRow.Table.Columns.Contains("FIO_RUKOV")) Fio_rukov = ADataRow["FIO_RUKOV"].ToString(); else Fio_rukov = "";
            if (ADataRow.Table.Columns.Contains("DOLJ_RUKOV")) Dolj_rukov = ADataRow["DOLJ_RUKOV"].ToString(); else Dolj_rukov = "";
            if (ADataRow.Table.Columns.Contains("FIO_GLBUH")) Fio_glbuh = ADataRow["FIO_GLBUH"].ToString(); else Fio_glbuh = "";
            if (ADataRow.Table.Columns.Contains("MYLABEL")) Mylabel = ADataRow["MYLABEL"].ToString() == "True" ? true : false; else Mylabel = false;
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = Convert.ToDecimal(ADataRow["NDS"]); else Nds = 0;
            if (ADataRow.Table.Columns.Contains("TELEFON")) Telefon = ADataRow["TELEFON"].ToString(); else Telefon = "";
            if (ADataRow.Table.Columns.Contains("FAX")) Fax = ADataRow["FAX"].ToString(); else Fax = "";
            if (ADataRow.Table.Columns.Contains("OGRN")) Ogrn = ADataRow["OGRN"].ToString(); else Ogrn = "";
            if (ADataRow.Table.Columns.Contains("OKATO")) Okato = ADataRow["OKATO"].ToString(); else Okato = "";
            if (ADataRow.Table.Columns.Contains("OKVED")) Okved = ADataRow["OKVED"].ToString(); else Okved = "";
            if (ADataRow.Table.Columns.Contains("FIO_KAS")) Fio_kas = ADataRow["FIO_KAS"].ToString(); else Fio_kas = "";
            if (ADataRow.Table.Columns.Contains("KASSA1")) Kassa1 = ADataRow["KASSA1"].ToString(); else Kassa1 = "";
            if (ADataRow.Table.Columns.Contains("KASSA2")) Kassa2 = ADataRow["KASSA2"].ToString(); else Kassa2 = "";
            if (ADataRow.Table.Columns.Contains("KASSA3")) Kassa3 = ADataRow["KASSA3"].ToString(); else Kassa3 = "";
            if (ADataRow.Table.Columns.Contains("KASSA4")) Kassa4 = ADataRow["KASSA4"].ToString(); else Kassa4 = "";
            if (ADataRow.Table.Columns.Contains("KASSA5")) Kassa5 = ADataRow["KASSA5"].ToString(); else Kassa5 = "";
            if (ADataRow.Table.Columns.Contains("EMAIL")) Email = ADataRow["EMAIL"].ToString(); else Email = "";
            if (ADataRow.Table.Columns.Contains("NALOG")) Nalog = ADataRow["NALOG"].ToString(); else Nalog = "";
            if (ADataRow.Table.Columns.Contains("NUM_USNO")) Num_usno = ADataRow["NUM_USNO"].ToString(); else Num_usno = "";
            if (ADataRow.Table.Columns.Contains("DAT_USNO")) Dat_usno = Convert.ToDateTime(ADataRow["DAT_USNO"]); else Dat_usno = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("FORM")) Form = Convert.ToInt64(ADataRow["FORM"]); else Form = 0;
            if (ADataRow.Table.Columns.Contains("FIO_IP")) Fio_ip = ADataRow["FIO_IP"].ToString(); else Fio_ip = "";
            if (ADataRow.Table.Columns.Contains("COD_IFNS")) Cod_ifns = ADataRow["COD_IFNS"].ToString(); else Cod_ifns = "";
            if (ADataRow.Table.Columns.Contains("NAME_IFNS")) Name_ifns = ADataRow["NAME_IFNS"].ToString(); else Name_ifns = "";
            if (ADataRow.Table.Columns.Contains("TYPENO")) Typeno = Convert.ToInt64(ADataRow["TYPENO"]); else Typeno = 0;
            if (ADataRow.Table.Columns.Contains("USLUGI")) Uslugi = ADataRow["USLUGI"].ToString(); else Uslugi = "";
            if (ADataRow.Table.Columns.Contains("MAIN_POS")) Main_pos = ADataRow["MAIN_POS"].ToString() == "True" ? true : false; else Main_pos = false;
        }
        
        public override AbstractRecord Clone()
        {
            Spr_orgRecord retValue = new Spr_orgRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Name_short = this.Name_short;
            retValue.Rs1 = this.Rs1;
            retValue.Bank1 = this.Bank1;
            retValue.Rs2 = this.Rs2;
            retValue.Bank2 = this.Bank2;
            retValue.Rs3 = this.Rs3;
            retValue.Bank3 = this.Bank3;
            retValue.Rs4 = this.Rs4;
            retValue.Bank4 = this.Bank4;
            retValue.Rs5 = this.Rs5;
            retValue.Bank5 = this.Bank5;
            retValue.Inn = this.Inn;
            retValue.Kpp = this.Kpp;
            retValue.Okonh = this.Okonh;
            retValue.Okpo = this.Okpo;
            retValue.Okdp = this.Okdp;
            retValue.Okopf = this.Okopf;
            retValue.Okfs = this.Okfs;
            retValue.Vid_work = this.Vid_work;
            retValue.Form_sobs = this.Form_sobs;
            retValue.Addres = this.Addres;
            retValue.Addres_f = this.Addres_f;
            retValue.Fio_rukov = this.Fio_rukov;
            retValue.Dolj_rukov = this.Dolj_rukov;
            retValue.Fio_glbuh = this.Fio_glbuh;
            retValue.Mylabel = this.Mylabel;
            retValue.Nds = this.Nds;
            retValue.Telefon = this.Telefon;
            retValue.Fax = this.Fax;
            retValue.Ogrn = this.Ogrn;
            retValue.Okato = this.Okato;
            retValue.Okved = this.Okved;
            retValue.Fio_kas = this.Fio_kas;
            retValue.Kassa1 = this.Kassa1;
            retValue.Kassa2 = this.Kassa2;
            retValue.Kassa3 = this.Kassa3;
            retValue.Kassa4 = this.Kassa4;
            retValue.Kassa5 = this.Kassa5;
            retValue.Email = this.Email;
            retValue.Nalog = this.Nalog;
            retValue.Num_usno = this.Num_usno;
            retValue.Dat_usno = this.Dat_usno;
            retValue.Form = this.Form;
            retValue.Fio_ip = this.Fio_ip;
            retValue.Cod_ifns = this.Cod_ifns;
            retValue.Name_ifns = this.Name_ifns;
            retValue.Typeno = this.Typeno;
            retValue.Uslugi = this.Uslugi;
            retValue.Main_pos = this.Main_pos;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPR_ORG (COD, NAME, NAME_SHORT, RS1, BANK1, RS2, BANK2, RS3, BANK3, RS4, BANK4, RS5, BANK5, INN, KPP, OKONH, OKPO, OKDP, OKOPF, OKFS, VID_WORK, FORM_SOBS, ADDRES, ADDRES_F, FIO_RUKOV, DOLJ_RUKOV, FIO_GLBUH, MYLABEL, NDS, TELEFON, FAX, OGRN, OKATO, OKVED, FIO_KAS, KASSA1, KASSA2, KASSA3, KASSA4, KASSA5, EMAIL, NALOG, NUM_USNO, DAT_USNO, FORM, FIO_IP, COD_IFNS, NAME_IFNS, TYPENO, USLUGI, MAIN_POS) VALUES ({0}, '{1}', '{2}', '{3}', {4}, '{5}', {6}, '{7}', {8}, '{9}', {10}, '{11}', {12}, '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', {27}, {28}, '{29}', '{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}', '{41}', '{42}', CTOD('{43}'), {44}, '{45}', '{46}', '{47}', {48}, '{49}', {50})", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Name_short) ? "" : Name_short.Trim(), String.IsNullOrEmpty(Rs1) ? "" : Rs1.Trim(), Bank1.ToString(), String.IsNullOrEmpty(Rs2) ? "" : Rs2.Trim(), Bank2.ToString(), String.IsNullOrEmpty(Rs3) ? "" : Rs3.Trim(), Bank3.ToString(), String.IsNullOrEmpty(Rs4) ? "" : Rs4.Trim(), Bank4.ToString(), String.IsNullOrEmpty(Rs5) ? "" : Rs5.Trim(), Bank5.ToString(), String.IsNullOrEmpty(Inn) ? "" : Inn.Trim(), String.IsNullOrEmpty(Kpp) ? "" : Kpp.Trim(), String.IsNullOrEmpty(Okonh) ? "" : Okonh.Trim(), String.IsNullOrEmpty(Okpo) ? "" : Okpo.Trim(), String.IsNullOrEmpty(Okdp) ? "" : Okdp.Trim(), String.IsNullOrEmpty(Okopf) ? "" : Okopf.Trim(), String.IsNullOrEmpty(Okfs) ? "" : Okfs.Trim(), String.IsNullOrEmpty(Vid_work) ? "" : Vid_work.Trim(), String.IsNullOrEmpty(Form_sobs) ? "" : Form_sobs.Trim(), String.IsNullOrEmpty(Addres) ? "" : Addres.Trim(), String.IsNullOrEmpty(Addres_f) ? "" : Addres_f.Trim(), String.IsNullOrEmpty(Fio_rukov) ? "" : Fio_rukov.Trim(), String.IsNullOrEmpty(Dolj_rukov) ? "" : Dolj_rukov.Trim(), String.IsNullOrEmpty(Fio_glbuh) ? "" : Fio_glbuh.Trim(), (Mylabel ? 0 : 1 ), Nds.ToString().Replace(',','.'), String.IsNullOrEmpty(Telefon) ? "" : Telefon.Trim(), String.IsNullOrEmpty(Fax) ? "" : Fax.Trim(), String.IsNullOrEmpty(Ogrn) ? "" : Ogrn.Trim(), String.IsNullOrEmpty(Okato) ? "" : Okato.Trim(), String.IsNullOrEmpty(Okved) ? "" : Okved.Trim(), String.IsNullOrEmpty(Fio_kas) ? "" : Fio_kas.Trim(), String.IsNullOrEmpty(Kassa1) ? "" : Kassa1.Trim(), String.IsNullOrEmpty(Kassa2) ? "" : Kassa2.Trim(), String.IsNullOrEmpty(Kassa3) ? "" : Kassa3.Trim(), String.IsNullOrEmpty(Kassa4) ? "" : Kassa4.Trim(), String.IsNullOrEmpty(Kassa5) ? "" : Kassa5.Trim(), String.IsNullOrEmpty(Email) ? "" : Email.Trim(), String.IsNullOrEmpty(Nalog) ? "" : Nalog.Trim(), String.IsNullOrEmpty(Num_usno) ? "" : Num_usno.Trim(), Dat_usno == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat_usno.Month, Dat_usno.Day, Dat_usno.Year), Form.ToString(), String.IsNullOrEmpty(Fio_ip) ? "" : Fio_ip.Trim(), String.IsNullOrEmpty(Cod_ifns) ? "" : Cod_ifns.Trim(), String.IsNullOrEmpty(Name_ifns) ? "" : Name_ifns.Trim(), Typeno.ToString(), String.IsNullOrEmpty(Uslugi) ? "" : Uslugi.Trim(), (Main_pos ? 0 : 1 ));
            return rs;
        }
    }
}
