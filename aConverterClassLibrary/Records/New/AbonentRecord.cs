// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("ABONENT.DBF")]
    public class AbonentRecord: AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 housecd;
        // <summary>
        // HOUSECD N(9)
        // </summary>
        [FieldName("HOUSECD"), FieldType('N'), FieldWidth(9)]
        public Int64 Housecd
        {
            get { return housecd; }
            set { CheckIntegerData("Housecd", value, 9); housecd = value; }
        }

        private Int64 distkod;
        // <summary>
        // DISTKOD N(7)
        // </summary>
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(7)]
        public Int64 Distkod
        {
            get { return distkod; }
            set { CheckIntegerData("Distkod", value, 7); distkod = value; }
        }

        private string distname;
        // <summary>
        // DISTNAME C(40)
        // </summary>
        [FieldName("DISTNAME"), FieldType('C'), FieldWidth(40)]
        public string Distname
        {
            get { return distname; }
            set { CheckStringData("Distname", value, 40); distname = value; }
        }

        private Int64 rayonkod;
        // <summary>
        // RAYONKOD N(7)
        // </summary>
        [FieldName("RAYONKOD"), FieldType('N'), FieldWidth(7)]
        public Int64 Rayonkod
        {
            get { return rayonkod; }
            set { CheckIntegerData("Rayonkod", value, 7); rayonkod = value; }
        }

        private string rayonname;
        // <summary>
        // RAYONNAME C(40)
        // </summary>
        [FieldName("RAYONNAME"), FieldType('C'), FieldWidth(40)]
        public string Rayonname
        {
            get { return rayonname; }
            set { CheckStringData("Rayonname", value, 40); rayonname = value; }
        }

        private Int64 townskod;
        // <summary>
        // TOWNSKOD N(7)
        // </summary>
        [FieldName("TOWNSKOD"), FieldType('N'), FieldWidth(7)]
        public Int64 Townskod
        {
            get { return townskod; }
            set { CheckIntegerData("Townskod", value, 7); townskod = value; }
        }

        private string townsname;
        // <summary>
        // TOWNSNAME C(40)
        // </summary>
        [FieldName("TOWNSNAME"), FieldType('C'), FieldWidth(40)]
        public string Townsname
        {
            get { return townsname; }
            set { CheckStringData("Townsname", value, 40); townsname = value; }
        }

        private Int64 ulicakod;
        // <summary>
        // ULICAKOD N(7)
        // </summary>
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(7)]
        public Int64 Ulicakod
        {
            get { return ulicakod; }
            set { CheckIntegerData("Ulicakod", value, 7); ulicakod = value; }
        }

        private string ulicaname;
        // <summary>
        // ULICANAME C(40)
        // </summary>
        [FieldName("ULICANAME"), FieldType('C'), FieldWidth(40)]
        public string Ulicaname
        {
            get { return ulicaname; }
            set { CheckStringData("Ulicaname", value, 40); ulicaname = value; }
        }

        private string ndoma;
        // <summary>
        // NDOMA C(20)
        // </summary>
        [FieldName("NDOMA"), FieldType('C'), FieldWidth(20)]
        public string Ndoma
        {
            get { return ndoma; }
            set { CheckStringData("Ndoma", value, 20); ndoma = value; }
        }

        private Int64 korpus;
        // <summary>
        // KORPUS N(6)
        // </summary>
        [FieldName("KORPUS"), FieldType('N'), FieldWidth(6)]
        public Int64 Korpus
        {
            get { return korpus; }
            set { CheckIntegerData("Korpus", value, 6); korpus = value; }
        }

        private string kvartira;
        // <summary>
        // KVARTIRA C(10)
        // </summary>
        [FieldName("KVARTIRA"), FieldType('C'), FieldWidth(10)]
        public string Kvartira
        {
            get { return kvartira; }
            set { CheckStringData("Kvartira", value, 10); kvartira = value; }
        }

        private Int64 komnata;
        // <summary>
        // KOMNATA N(4)
        // </summary>
        [FieldName("KOMNATA"), FieldType('N'), FieldWidth(4)]
        public Int64 Komnata
        {
            get { return komnata; }
            set { CheckIntegerData("Komnata", value, 4); komnata = value; }
        }

        private string f;
        // <summary>
        // F C(50)
        // </summary>
        [FieldName("F"), FieldType('C'), FieldWidth(50)]
        public string F
        {
            get { return f; }
            set { CheckStringData("F", value, 50); f = value; }
        }

        private string i;
        // <summary>
        // I C(50)
        // </summary>
        [FieldName("I"), FieldType('C'), FieldWidth(50)]
        public string I
        {
            get { return i; }
            set { CheckStringData("I", value, 50); i = value; }
        }

        private string o;
        // <summary>
        // O C(50)
        // </summary>
        [FieldName("O"), FieldType('C'), FieldWidth(50)]
        public string O
        {
            get { return o; }
            set { CheckStringData("O", value, 50); o = value; }
        }

        private string prim_;
        // <summary>
        // PRIM_ C(200)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(200)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 200); prim_ = value; }
        }

        private string extlshet;
        // <summary>
        // EXTLSHET C(20)
        // </summary>
        [FieldName("EXTLSHET"), FieldType('C'), FieldWidth(20)]
        public string Extlshet
        {
            get { return extlshet; }
            set { CheckStringData("Extlshet", value, 20); extlshet = value; }
        }

        private string phonenum;
        // <summary>
        // PHONENUM C(15)
        // </summary>
        [FieldName("PHONENUM"), FieldType('C'), FieldWidth(15)]
        public string Phonenum
        {
            get { return phonenum; }
            set { CheckStringData("Phonenum", value, 15); phonenum = value; }
        }

        private string postindex;
        // <summary>
        // POSTINDEX C(6)
        // </summary>
        [FieldName("POSTINDEX"), FieldType('C'), FieldWidth(6)]
        public string Postindex
        {
            get { return postindex; }
            set { CheckStringData("Postindex", value, 6); postindex = value; }
        }

        private Int64 ducd;
        // <summary>
        // DUCD N(8)
        // </summary>
        [FieldName("DUCD"), FieldType('N'), FieldWidth(8)]
        public Int64 Ducd
        {
            get { return ducd; }
            set { CheckIntegerData("Ducd", value, 8); ducd = value; }
        }

        private string duname;
        // <summary>
        // DUNAME C(50)
        // </summary>
        [FieldName("DUNAME"), FieldType('C'), FieldWidth(50)]
        public string Duname
        {
            get { return duname; }
            set { CheckStringData("Duname", value, 50); duname = value; }
        }

        private Int64 isdeleted;
        // <summary>
        // ISDELETED N(3)
        // </summary>
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(3)]
        public Int64 Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 3); isdeleted = value; }
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
            Extlshet = ADataRow["EXTLSHET"].ToString();
            Phonenum = ADataRow["PHONENUM"].ToString();
            Postindex = ADataRow["POSTINDEX"].ToString();
            Ducd = Convert.ToInt64(ADataRow["DUCD"]);
            Duname = ADataRow["DUNAME"].ToString();
            Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]);
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO ABONENT (LSHET, HOUSECD, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, NDOMA, KORPUS, KVARTIRA, KOMNATA, F, I, O, PRIM_, EXTLSHET, PHONENUM, POSTINDEX, DUCD, DUNAME, ISDELETED) VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', {6}, '{7}', {8}, '{9}', '{10}', {11}, '{12}', {13}, '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', {21}, '{22}', {23})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Housecd.ToString(), Distkod.ToString(), String.IsNullOrEmpty(Distname) ? "" : Distname.Trim(), Rayonkod.ToString(), String.IsNullOrEmpty(Rayonname) ? "" : Rayonname.Trim(), Townskod.ToString(), String.IsNullOrEmpty(Townsname) ? "" : Townsname.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), Korpus.ToString(), String.IsNullOrEmpty(Kvartira) ? "" : Kvartira.Trim(), Komnata.ToString(), String.IsNullOrEmpty(F) ? "" : F.Trim(), String.IsNullOrEmpty(I) ? "" : I.Trim(), String.IsNullOrEmpty(O) ? "" : O.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), String.IsNullOrEmpty(Extlshet) ? "" : Extlshet.Trim(), String.IsNullOrEmpty(Phonenum) ? "" : Phonenum.Trim(), String.IsNullOrEmpty(Postindex) ? "" : Postindex.Trim(), Ducd.ToString(), String.IsNullOrEmpty(Duname) ? "" : Duname.Trim(), Isdeleted.ToString());
           return rs;
        }
    }
}
