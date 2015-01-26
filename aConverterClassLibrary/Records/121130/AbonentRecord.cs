using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

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
        [FieldName("HOUSECD"), FieldType('N'), FieldWidth(7), FieldDescription("Код дома")]
        public long Housecd
        {
            get { return housecd; }
            set { CheckIntegerData("Housecd", value, 7); housecd = value; }
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
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(200), FieldDescription("Примечание")]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 200); prim_ = value; }
        }

        private string extlshet;
        [FieldName("EXTLSHET"), FieldType('C'), FieldWidth(20), FieldDescription("Лицевой счет во внешней организации")]
        public string Extlshet
        {
            get { return extlshet; }
            set { CheckStringData("Extlshet", value, 20); extlshet = value; }
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
        [FieldName("PHONENUM"), FieldType('C'), FieldWidth(15), FieldDescription("Номер телефона")]
        public string Phonenum
        {
            get { return phonenum; }
            set { CheckStringData("Phonenum", value, 15); phonenum = value; }
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
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(1), FieldDescription("Признак удаленного абонента")]
        public int Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 1); isdeleted = value; }
        }


        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Housecd = Convert.ToInt64(ADataRow["HOUSECD"]);
            Distkod = Convert.ToInt64(ADataRow["DISTKOD"]);
            Distname = ADataRow["DISTNAME"].ToString();
            Rayonkod = Convert.ToInt64(ADataRow["RAYONKOD"]);
            Rayonname = ADataRow["RAYONNAME"].ToString();
            Townskod = Convert.ToInt64(ADataRow["TOWNSKOD"]);
            Townsname = ADataRow["TOWNSNAME"].ToString();
            Ulicakod = Convert.ToInt64(ADataRow["ULICAKOD"]);
            Ulicaname = ADataRow["ULICANAME"].ToString();
            Ndoma = ADataRow["NDOMA"].ToString();
            Korpus = Convert.ToInt64(ADataRow["KORPUS"]);
            Kvartira = ADataRow["KVARTIRA"].ToString();
            Komnata = Convert.ToInt64(ADataRow["KOMNATA"]);
            F = ADataRow["F"].ToString();
            I = ADataRow["I"].ToString();
            O = ADataRow["O"].ToString();
            Prim_ = ADataRow["PRIM_"].ToString();
            Phonenum = ADataRow["PHONENUM"].ToString();
            Postindex = ADataRow["POSTINDEX"].ToString();
            Ducd = Convert.ToInt64(ADataRow["DUCD"]);
            Duname = ADataRow["DUNAME"].ToString();
            Isdeleted = Convert.ToInt32(ADataRow["ISDELETED"]);
            Extlshet = ADataRow["EXTLSHET"].ToString();
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABONENT (LSHET, HOUSECD, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, NDOMA, KORPUS, KVARTIRA, KOMNATA, F, I, O, PRIM_, PHONENUM, POSTINDEX, DUCD, DUNAME, ISDELETED, EXTLSHET) VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', {6}, '{7}', {8}, '{9}', '{10}', {11}, '{12}', {13}, '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', {20}, '{21}', {22}, '{23}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Housecd.ToString(), Distkod.ToString(), String.IsNullOrEmpty(Distname) ? "" : Distname.Trim(), Rayonkod.ToString(), String.IsNullOrEmpty(Rayonname) ? "" : Rayonname.Trim(), Townskod.ToString(), String.IsNullOrEmpty(Townsname) ? "" : Townsname.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), Korpus.ToString(), String.IsNullOrEmpty(Kvartira) ? "" : Kvartira.Trim(), Komnata.ToString(), String.IsNullOrEmpty(F) ? "" : F.Trim(), String.IsNullOrEmpty(I) ? "" : I.Trim(), String.IsNullOrEmpty(O) ? "" : O.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), String.IsNullOrEmpty(Phonenum) ? "" : Phonenum.Trim(), String.IsNullOrEmpty(Postindex) ? "" : Postindex.Trim(), Ducd.ToString(), String.IsNullOrEmpty(Duname) ? "" : Duname.Trim(), Isdeleted.ToString(), String.IsNullOrEmpty(Extlshet) ? "" : Extlshet.Trim());
            return rs;
        }
    }
}
