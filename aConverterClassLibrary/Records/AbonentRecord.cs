using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("ABONENT.DBF")]
    [TableDescription("Справочник абонентов")]
    [Index("LSHET", "LSHET")]
    [Index("HOUSECD", "HOUSECD")]
    [Index("DISTKOD","DISTKOD")]
    [Index("TOWNSKOD","TOWNSKOD")]
    [Index("ULICAKOD","ULICAKOD")]
    [Index("NDOMA","NDOMA")]
    [Index("KORPUS","KORPUS")]
    public class AbonentRecord: AbstractRecord
    {
        private string lshet;
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("Лицевой счет")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private long housecd;
        [FieldName("HOUSECD"), FieldType('N'), FieldWidth(8), FieldDescription("Код дома")]
        public long Housecd
        {
            get { return housecd; }
            set { CheckIntegerData("Housecd", value, 8); housecd = value; }
        }

        private long distkod;
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(5), FieldDescription("Код района города")]
        public long Distkod
        {
            get { return distkod; }
            set { CheckIntegerData("Distkod", value, 5); distkod = value; }
        }

        private string distname;
        [FieldName("DISTNAME"), FieldType('C'), FieldWidth(40), FieldDescription("Наименование района города")]
        public string Distname
        {
            get { return distname; }
            set { CheckStringData("Distname", value, 40); distname = value; }
        }

        private long rayonkod;
        [FieldName("RAYONKOD"), FieldType('N'), FieldWidth(5), FieldDescription("Код района области")]
        public long Rayonkod
        {
            get { return rayonkod; }
            set { CheckIntegerData("Rayonkod", value, 5); rayonkod = value; }
        }

        private string rayonname;
        [FieldName("RAYONNAME"), FieldType('C'), FieldWidth(40), FieldDescription("Наименование района области")]
        public string Rayonname
        {
            get { return rayonname; }
            set { CheckStringData("Rayonname", value, 40); rayonname = value; }
        }

        private long townskod;
        [FieldName("TOWNSKOD"), FieldType('N'), FieldWidth(5), FieldDescription("Код населенного пункта")]
        public long Townskod
        {
            get { return townskod; }
            set { CheckIntegerData("Townskod", value, 5); townskod = value; }
        }

        private string townsname;
        [FieldName("TOWNSNAME"), FieldType('C'), FieldWidth(40), FieldDescription("Наименование населенного пункта")]
        public string Townsname
        {
            get { return townsname; }
            set { CheckStringData("Townsname", value, 40); townsname = value; }
        }

        private long ulicakod;
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(5), FieldDescription("Код улицы")]
        public long Ulicakod
        {
            get { return ulicakod; }
            set { CheckIntegerData("Ulicakod", value, 5); ulicakod = value; }
        }

        private string ulicaname;
        [FieldName("ULICANAME"), FieldType('C'), FieldWidth(40), FieldDescription("Наименование улицы")]
        public string Ulicaname
        {
            get { return ulicaname; }
            set { CheckStringData("Ulicaname", value, 40); ulicaname = value; }
        }

        private string ndoma;
        [FieldName("NDOMA"), FieldType('C'), FieldWidth(20), FieldDescription("Номер дома (включая постфикс)")]
        public string Ndoma
        {
            get { return ndoma; }
            set { CheckStringData("Ndoma", value, 20); ndoma = value; }
        }

        private long korpus;
        [FieldName("KORPUS"), FieldType('N'), FieldWidth(4), FieldDescription("Корпус дома")]
        public long Korpus
        {
            get { return korpus; }
            set { CheckIntegerData("Korpus", value, 4); korpus = value; }
        }

        //Kvartira C(10), ;
        private string kvartira;
        [FieldName("KVARTIRA"), FieldType('C'), FieldWidth(10), FieldDescription("Номер квартиры (включая постфикс)")]
        public string Kvartira
        {
            get { return kvartira; }
            set { CheckStringData("Kvartira", value, 10); kvartira = value; }
        }

        private long komnata;
        [FieldName("KOMNATA"), FieldType('N'), FieldWidth(2), FieldDescription("Комната")]
        public long Komnata
        {
            get { return komnata; }
            set { CheckIntegerData("Komnata", value, 2); komnata = value; }
        }

        private string f;
        [FieldName("F"), FieldType('C'), FieldWidth(50), FieldDescription("Фамилия")]
        public string F
        {
            get { return f; }
            set { CheckStringData("F", value, 50); f = value; }
        }

        private string i;
        [FieldName("I"), FieldType('C'), FieldWidth(50), FieldDescription("Имя")]
        public string I
        {
            get { return i; }
            set { CheckStringData("I", value, 50); i = value; }
        }

        private string o;
        [FieldName("O"), FieldType('C'), FieldWidth(50), FieldDescription("Отчество")]
        public string O
        {
            get { return o; }
            set { CheckStringData("O", value, 50); o = value; }
        }

        private string prim_;
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(250), FieldDescription("Примечание")]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 250); prim_ = value; }
        }

        private string extlshet;
        [FieldName("EXTLSHET"), FieldType('C'), FieldWidth(20), FieldDescription("Лицевой счет во внешней организации")]
        public string Extlshet
        {
            get { return extlshet; }
            set { CheckStringData("Extlshet", value, 20); extlshet = value; }
        }

        private string extlshet2;
        // <summary>
        // EXTLSHET2 C(20)
        // </summary>
        [FieldName("EXTLSHET2"), FieldType('C'), FieldWidth(20)]
        public string Extlshet2
        {
            get { return extlshet2; }
            set { CheckStringData("Extlshet2", value, 20); extlshet2 = value; }
        }

        //private Int32 capcd;
        //[FieldName("CAPCD"), FieldType('N'), FieldWidth(5), FieldDescription("Код емкости (группового счетчика)")]
        //public Int32 Capcd
        //{
        //    get { return capcd; }
        //    set { CheckIntegerData("Capcd", value, 5); capcd = value; }
        //}

        //private string capname;
        //[FieldName("CAPNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование емкости (групповой установки)")]
        //public string Capname
        //{
        //    get { return capname; }
        //    set { CheckStringData("Capname", value, 50); capname = value; }
        //}

        private string phonenum;
        [FieldName("PHONENUM"), FieldType('C'), FieldWidth(100), FieldDescription("Номер телефона")]
        public string Phonenum
        {
            get { return phonenum; }
            set { CheckStringData("Phonenum", value, 100); phonenum = value; }
        }

        private string postindex;
        [FieldName("POSTINDEX"), FieldType('C'), FieldWidth(6), FieldDescription("Почтовый индекс")]
        public string Postindex
        {
            get { return postindex; }
            set { CheckStringData("Postindex", value, 6); postindex = value; }
        }

        private long ducd;
        [FieldName("DUCD"), FieldType('N'), FieldWidth(6), FieldDescription("Код домоуправления (обслуживающей организации)")]
        public long Ducd
        {
            get { return ducd; }
            set { CheckIntegerData("Ducd", value, 6); ducd = value; }
        }

        private string duname;
        [FieldName("DUNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Почтовый индекс")]
        public string Duname
        {
            get { return duname; }
            set { CheckStringData("Duname", value, 50); duname = value; }
        }

        //private string n_dog;
        //[FieldName("N_DOG"), FieldType('C'), FieldWidth(10), FieldDescription("Номер договора с абонентом")]
        //public string N_dog
        //{
        //    get { return n_dog; }
        //    set { CheckStringData("N_dog", value, 10); n_dog = value; }
        //}

        //private DateTime d_dog;
        //[FieldName("D_DOG"), FieldType('D'), FieldDescription("Дата заключения договора")]
        //public DateTime D_dog
        //{
        //    get { return d_dog; }
        //    set { d_dog = value; }
        //}

        private int isdeleted;
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(1), FieldDescription("Признак удаленного абонента (0 - не удален, 1 - удален)")]
        public int Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 1); isdeleted = value; }
        }


        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("HOUSECD")) Housecd = Convert.ToInt64(ADataRow["HOUSECD"]); else Housecd = 0;
            if (ADataRow.Table.Columns.Contains("DISTKOD")) Distkod = Convert.ToInt64(ADataRow["DISTKOD"]); else Distkod = 0;
            if (ADataRow.Table.Columns.Contains("DISTNAME")) Distname = ADataRow["DISTNAME"].ToString(); else Distname = "";
            if (ADataRow.Table.Columns.Contains("RAYONKOD")) Rayonkod = Convert.ToInt64(ADataRow["RAYONKOD"]); else Rayonkod = 0;
            if (ADataRow.Table.Columns.Contains("RAYONNAME")) Rayonname = ADataRow["RAYONNAME"].ToString(); else Rayonname = "";
            if (ADataRow.Table.Columns.Contains("TOWNSKOD")) Townskod = Convert.ToInt64(ADataRow["TOWNSKOD"]); else Townskod = 0;
            if (ADataRow.Table.Columns.Contains("TOWNSNAME")) Townsname = ADataRow["TOWNSNAME"].ToString(); else Townsname = "";
            if (ADataRow.Table.Columns.Contains("ULICAKOD")) Ulicakod = Convert.ToInt64(ADataRow["ULICAKOD"]); else Ulicakod = 0;
            if (ADataRow.Table.Columns.Contains("ULICANAME")) Ulicaname = ADataRow["ULICANAME"].ToString(); else Ulicaname = "";
            if (ADataRow.Table.Columns.Contains("NDOMA")) Ndoma = ADataRow["NDOMA"].ToString(); else Ndoma = "";
            if (ADataRow.Table.Columns.Contains("KORPUS")) Korpus = Convert.ToInt64(ADataRow["KORPUS"]); else Korpus = 0;
            if (ADataRow.Table.Columns.Contains("KVARTIRA")) Kvartira = ADataRow["KVARTIRA"].ToString(); else Kvartira = "";
            if (ADataRow.Table.Columns.Contains("KOMNATA")) Komnata = Convert.ToInt64(ADataRow["KOMNATA"]); else Komnata = 0;
            if (ADataRow.Table.Columns.Contains("F")) F = ADataRow["F"].ToString(); else F = "";
            if (ADataRow.Table.Columns.Contains("I")) I = ADataRow["I"].ToString(); else I = "";
            if (ADataRow.Table.Columns.Contains("O")) O = ADataRow["O"].ToString(); else O = "";
            if (ADataRow.Table.Columns.Contains("PRIM_")) Prim_ = ADataRow["PRIM_"].ToString(); else Prim_ = "";
            if (ADataRow.Table.Columns.Contains("EXTLSHET")) Extlshet = ADataRow["EXTLSHET"].ToString(); else Extlshet = "";
            if (ADataRow.Table.Columns.Contains("EXTLSHET2")) Extlshet2 = ADataRow["EXTLSHET2"].ToString(); else Extlshet2 = "";
            if (ADataRow.Table.Columns.Contains("PHONENUM")) Phonenum = ADataRow["PHONENUM"].ToString(); else Phonenum = "";
            if (ADataRow.Table.Columns.Contains("POSTINDEX")) Postindex = ADataRow["POSTINDEX"].ToString(); else Postindex = "";
            if (ADataRow.Table.Columns.Contains("DUCD")) Ducd = Convert.ToInt64(ADataRow["DUCD"]); else Ducd = 0;
            if (ADataRow.Table.Columns.Contains("DUNAME")) Duname = ADataRow["DUNAME"].ToString(); else Duname = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt32(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO abonent (LSHET, HOUSECD, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, NDOMA, KORPUS, KVARTIRA, KOMNATA, F, I, O, PRIM_, EXTLSHET, EXTLSHET2, PHONENUM, POSTINDEX, DUCD, DUNAME, ISDELETED) VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', {6}, '{7}', {8}, '{9}', '{10}', {11}, '{12}', {13}, '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', {22}, '{23}', {24})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Housecd.ToString(), Distkod.ToString(), String.IsNullOrEmpty(Distname) ? "" : Distname.Trim(), Rayonkod.ToString(), String.IsNullOrEmpty(Rayonname) ? "" : Rayonname.Trim(), Townskod.ToString(), String.IsNullOrEmpty(Townsname) ? "" : Townsname.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), Korpus.ToString(), String.IsNullOrEmpty(Kvartira) ? "" : Kvartira.Trim(), Komnata.ToString(), String.IsNullOrEmpty(F) ? "" : F.Trim(), String.IsNullOrEmpty(I) ? "" : I.Trim(), String.IsNullOrEmpty(O) ? "" : O.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), String.IsNullOrEmpty(Extlshet) ? "" : Extlshet.Trim(), String.IsNullOrEmpty(Extlshet2) ? "" : Extlshet2.Trim(), String.IsNullOrEmpty(Phonenum) ? "" : Phonenum.Trim(), String.IsNullOrEmpty(Postindex) ? "" : Postindex.Trim(), Ducd.ToString(), String.IsNullOrEmpty(Duname) ? "" : Duname.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}
