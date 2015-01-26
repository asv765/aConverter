using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("ABONENT.DBF")]
    [Index("LSHET", "LSHET")]
    public class AbonentRecord: AbstractRecord
    {
        private string lshet;
        /// <summary>
        /// Лицевой счет Lshet C(10)
        /// </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckData("Lshet", value); lshet = value; }
        }

        private Int32 distKod;
        /// <summary>
        /// Код района города DistKod N(3)
        /// </summary>
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(3)]
        public Int32 DistKod
        {
            get { return distKod; }
            set { CheckData("DistKod", value); distKod = value; }
        }

        private string distName;
        /// <summary>
        /// Наименование района города DistName C(40)
        /// </summary>
        [FieldName("DISTNAME"), FieldType('C'), FieldWidth(40)]
        public string DistName
        {
            get { return distName; }
            set { CheckData("DistName", value); distName = value;  }
        }

        private Int32 rayonKod;
        /// <summary>
        /// Код района области RayonKod N(3) (не импортируется)
        /// </summary>
        [FieldName("RAYONKOD"), FieldType('N'), FieldWidth(3)]
        public Int32 RayonKod
        {
            get { return rayonKod; }
            set { CheckData("RayonKod", value); rayonKod = value; }
        }

        private string rayonName;
        /// <summary>
        /// Наименование района области RayonName C(40)
        /// </summary>
        [FieldName("RAYONNAME"), FieldType('C'), FieldWidth(40)]
        public string RayonName
        {
            get { return rayonName; }
            set { CheckData("RayonName", value); rayonName = value; }
        }

        private Int32 townsKod;
        /// <summary>
        /// Код населенного пункта TownsKod N(3)
        /// </summary>
        [FieldName("TOWNSKOD"), FieldType('N'), FieldWidth(3)]
        public Int32 TownsKod
        {
            get { return townsKod; }
            set { CheckData("TownsKod", value); townsKod = value; }
        }

        private string townsName;
        /// <summary>
        /// Наименование населенного пункта TownsName C(40)
        /// </summary>
        [FieldName("TOWNSNAME"), FieldType('C'), FieldWidth(40)]
        public string TownsName
        {
            get { return townsName; }
            set { CheckData("TownsName", value); townsName = value; }
        }

        private Int32 ulicaKod;
        /// <summary>
        /// Код улицы UlicaKod N(3)
        /// </summary>
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(3)]
        public Int32 UlicaKod
        {
            get { return ulicaKod; }
            set { CheckData("UlicaKod", value); ulicaKod = value; }
        }

        private string ulicaName;
        /// <summary>
        /// Наименование улицы UlicaName C(40)
        /// </summary>
        [FieldName("ULICANAME"), FieldType('C'), FieldWidth(40)]
        public string UlicaName
        {
            get { return ulicaName; }
            set { CheckData("UlicaName", value); ulicaName = value; }
        }

        private string nDoma;
        /// <summary>
        /// Номер дома (включая постфикс) - NDoma C(10)
        /// </summary>
        [FieldName("NDOMA"), FieldType('C'), FieldWidth(10)]
        public string NDoma
        {
            get { return nDoma; }
            set { CheckData("NDoma", value); nDoma = value; }
        }

        private Int32 korpus;
        /// <summary>
        /// Корпус дома - Korpus N(2)
        /// </summary>
        [FieldName("KORPUS"), FieldType('N'), FieldWidth(2)]
        public Int32 Korpus
        {
            get { return korpus; }
            set { CheckData("Korpus", value); korpus = value; }
        }

        //Kvartira C(10), ;
        private string kvartira;
        /// <summary>
        /// Номер квартиры (включая постфикс) - Kvartira C(10)
        /// </summary>
        [FieldName("KVARTIRA"), FieldType('C'), FieldWidth(10)]
        public string Kvartira
        {
            get { return kvartira; }
            set { CheckData("Kvartira", value); kvartira = value; }
        }

        private Int32 komnata;
        /// <summary>
        /// Комната - Komnata N(1)
        /// </summary>
        [FieldName("KOMNATA"), FieldType('N'), FieldWidth(1)]
        public Int32 Komnata
        {
            get { return komnata; }
            set { CheckData("Komnata", value); komnata = value; }
        }

        private string fio;
        /// <summary>
        /// Фамилия, имя, отчество (одной строкой) -  FIO C(50)
        /// </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(50)]
        public string Fio
        {
            get { return fio; }
            set { CheckData("Fio", value); fio = value; }
        }

        private string prim_;
        /// <summary>
        /// Примечание - Prim_ C(100)
        /// </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckData("Prim_", value); prim_ = value; }
        }

        private Int32 capcd;
        /// <summary>
        /// Код емкости (группового счетчика) - CapCD N(3)
        /// </summary>
        [FieldName("CAPCD"), FieldType('N'), FieldWidth(3)]
        public Int32 CapCd
        {
            get { return capcd; }
            set { CheckData("CapCd", value); capcd = value; }
        }

        private string capname;
        /// <summary>
        /// Наименование емкости (групповой установки) - CapName C(50)
        /// </summary>
        [FieldName("CAPNAME"), FieldType('C'), FieldWidth(50)]
        public string CapName
        {
            get { return capname; }
            set { CheckData("CapName", value); capname = value; }
        }

        private string phonenum;
        /// <summary>
        /// Номер телефона - PhoneNum C(15)
        /// </summary>
        [FieldName("PHONENUM"), FieldType('C'), FieldWidth(15)]
        public string PhoneNum
        {
            get { return phonenum; }
            set { CheckData("PhoneNum", value); phonenum = value; }
        }

        private string postindex;
        /// <summary>
        /// Почтовый индекс - PostIndex C(6)
        /// </summary>
        [FieldName("POSTINDEX"), FieldType('C'), FieldWidth(6)]
        public string PostIndex
        {
            get { return postindex; }
            set { CheckData("PostIndex", value); postindex = value; }
        }

        private Int32 ducd;
        /// <summary>
        /// Код домоуправления (обслуживающей организации) - DUCD N(3)
        /// </summary>
        [FieldName("DUCD"), FieldType('N'), FieldWidth(4)]
        public Int32 DuCd
        {
            get { return ducd; }
            set { CheckData("DuCd", value); ducd = value; }
        }

        private string duname;
        /// <summary>
        /// Почтовый индекс - DUNAME C(50)
        /// </summary>
        [FieldName("DUNAME"), FieldType('C'), FieldWidth(50)]
        public string DuName
        {
            get { return duname; }
            set { CheckData("DuName", value); duname = value; }
        }

        private string n_dog;
        /// <summary>
        /// Номер договора с абонентом - N_Dog C(10)
        /// </summary>
        [FieldName("N_DOG"), FieldType('C'), FieldWidth(10)]
        public string N_Dog
        {
            get { return n_dog; }
            set { CheckData("N_Dog", value); n_dog = value; }
        }

        private DateTime d_dog;
        /// <summary>
        /// Дата закдючения договора - D_Dog D
        /// </summary>
        [FieldName("D_DOG"), FieldType('D')]
        public DateTime D_Dog
        {
            get { return d_dog; }
            set { CheckData("D_Dog", value); d_dog = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            DistKod = Convert.ToInt32(ADataRow["DISTKOD"]);
            DistName = ADataRow["DISTNAME"].ToString();
            RayonKod = Convert.ToInt32(ADataRow["RAYONKOD"]);
            RayonName = ADataRow["RAYONNAME"].ToString();
            TownsKod = Convert.ToInt32(ADataRow["TOWNSKOD"]);
            TownsName = ADataRow["TOWNSNAME"].ToString();
            UlicaKod = Convert.ToInt32(ADataRow["ULICAKOD"]);
            UlicaName = ADataRow["ULICANAME"].ToString();
            NDoma = ADataRow["NDOMA"].ToString();
            Korpus = Convert.ToInt32(ADataRow["KORPUS"]);
            Kvartira = ADataRow["KVARTIRA"].ToString();
            Komnata = Convert.ToInt32(ADataRow["KOMNATA"]);
            Fio = ADataRow["FIO"].ToString();
            Prim_ = ADataRow["PRIM_"].ToString();
            CapCd = Convert.ToInt32(ADataRow["CAPCD"]);
            CapName = ADataRow["CAPNAME"].ToString();
            PhoneNum = ADataRow["PHONENUM"].ToString();
            PostIndex = ADataRow["POSTINDEX"].ToString();
            DuCd = Convert.ToInt32(ADataRow["DUCD"]);
            DuName = ADataRow["DUNAME"].ToString();
            N_Dog = ADataRow["N_DOG"].ToString();
            D_Dog = Convert.ToDateTime(ADataRow["D_DOG"]);
        }
    }
}
