// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _033_Yrupinsk.Records
{
    [TableName("ABONENT.DBF")]
    public partial class AbonentRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(11)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(11)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 11); lshet = value; }
        }

        private Int64 distkod;
        // <summary>
        // DISTKOD N(4)
        // </summary>
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(4)]
        public Int64 Distkod
        {
            get { return distkod; }
            set { CheckIntegerData("Distkod", value, 4); distkod = value; }
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
        // RAYONKOD N(4)
        // </summary>
        [FieldName("RAYONKOD"), FieldType('N'), FieldWidth(4)]
        public Int64 Rayonkod
        {
            get { return rayonkod; }
            set { CheckIntegerData("Rayonkod", value, 4); rayonkod = value; }
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
        // TOWNSKOD N(4)
        // </summary>
        [FieldName("TOWNSKOD"), FieldType('N'), FieldWidth(4)]
        public Int64 Townskod
        {
            get { return townskod; }
            set { CheckIntegerData("Townskod", value, 4); townskod = value; }
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
        // ULICAKOD N(4)
        // </summary>
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(4)]
        public Int64 Ulicakod
        {
            get { return ulicakod; }
            set { CheckIntegerData("Ulicakod", value, 4); ulicakod = value; }
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
        // NDOMA C(10)
        // </summary>
        [FieldName("NDOMA"), FieldType('C'), FieldWidth(10)]
        public string Ndoma
        {
            get { return ndoma; }
            set { CheckStringData("Ndoma", value, 10); ndoma = value; }
        }

        private string korpus;
        // <summary>
        // KORPUS C(2)
        // </summary>
        [FieldName("KORPUS"), FieldType('C'), FieldWidth(2)]
        public string Korpus
        {
            get { return korpus; }
            set { CheckStringData("Korpus", value, 2); korpus = value; }
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
        // KOMNATA N(2)
        // </summary>
        [FieldName("KOMNATA"), FieldType('N'), FieldWidth(2)]
        public Int64 Komnata
        {
            get { return komnata; }
            set { CheckIntegerData("Komnata", value, 2); komnata = value; }
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

        private string prim;
        // <summary>
        // PRIM C(100)
        // </summary>
        [FieldName("PRIM"), FieldType('C'), FieldWidth(100)]
        public string Prim
        {
            get { return prim; }
            set { CheckStringData("Prim", value, 100); prim = value; }
        }

        private Int64 capcd;
        // <summary>
        // CAPCD N(3)
        // </summary>
        [FieldName("CAPCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Capcd
        {
            get { return capcd; }
            set { CheckIntegerData("Capcd", value, 3); capcd = value; }
        }

        private string capname;
        // <summary>
        // CAPNAME C(50)
        // </summary>
        [FieldName("CAPNAME"), FieldType('C'), FieldWidth(50)]
        public string Capname
        {
            get { return capname; }
            set { CheckStringData("Capname", value, 50); capname = value; }
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
        // DUCD N(4)
        // </summary>
        [FieldName("DUCD"), FieldType('N'), FieldWidth(4)]
        public Int64 Ducd
        {
            get { return ducd; }
            set { CheckIntegerData("Ducd", value, 4); ducd = value; }
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
        // ISDELETED N(2)
        // </summary>
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(2)]
        public Int64 Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 2); isdeleted = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("DISTKOD")) Distkod = Convert.ToInt64(ADataRow["DISTKOD"]); else Distkod = 0;
            if (ADataRow.Table.Columns.Contains("DISTNAME")) Distname = ADataRow["DISTNAME"].ToString(); else Distname = "";
            if (ADataRow.Table.Columns.Contains("RAYONKOD")) Rayonkod = Convert.ToInt64(ADataRow["RAYONKOD"]); else Rayonkod = 0;
            if (ADataRow.Table.Columns.Contains("RAYONNAME")) Rayonname = ADataRow["RAYONNAME"].ToString(); else Rayonname = "";
            if (ADataRow.Table.Columns.Contains("TOWNSKOD")) Townskod = Convert.ToInt64(ADataRow["TOWNSKOD"]); else Townskod = 0;
            if (ADataRow.Table.Columns.Contains("TOWNSNAME")) Townsname = ADataRow["TOWNSNAME"].ToString(); else Townsname = "";
            if (ADataRow.Table.Columns.Contains("ULICAKOD")) Ulicakod = Convert.ToInt64(ADataRow["ULICAKOD"]); else Ulicakod = 0;
            if (ADataRow.Table.Columns.Contains("ULICANAME")) Ulicaname = ADataRow["ULICANAME"].ToString(); else Ulicaname = "";
            if (ADataRow.Table.Columns.Contains("NDOMA")) Ndoma = ADataRow["NDOMA"].ToString(); else Ndoma = "";
            if (ADataRow.Table.Columns.Contains("KORPUS")) Korpus = ADataRow["KORPUS"].ToString(); else Korpus = "";
            if (ADataRow.Table.Columns.Contains("KVARTIRA")) Kvartira = ADataRow["KVARTIRA"].ToString(); else Kvartira = "";
            if (ADataRow.Table.Columns.Contains("KOMNATA")) Komnata = Convert.ToInt64(ADataRow["KOMNATA"]); else Komnata = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("PRIM")) Prim = ADataRow["PRIM"].ToString(); else Prim = "";
            if (ADataRow.Table.Columns.Contains("CAPCD")) Capcd = Convert.ToInt64(ADataRow["CAPCD"]); else Capcd = 0;
            if (ADataRow.Table.Columns.Contains("CAPNAME")) Capname = ADataRow["CAPNAME"].ToString(); else Capname = "";
            if (ADataRow.Table.Columns.Contains("PHONENUM")) Phonenum = ADataRow["PHONENUM"].ToString(); else Phonenum = "";
            if (ADataRow.Table.Columns.Contains("POSTINDEX")) Postindex = ADataRow["POSTINDEX"].ToString(); else Postindex = "";
            if (ADataRow.Table.Columns.Contains("DUCD")) Ducd = Convert.ToInt64(ADataRow["DUCD"]); else Ducd = 0;
            if (ADataRow.Table.Columns.Contains("DUNAME")) Duname = ADataRow["DUNAME"].ToString(); else Duname = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            AbonentRecord retValue = new AbonentRecord();
            retValue.Lshet = this.Lshet;
            retValue.Distkod = this.Distkod;
            retValue.Distname = this.Distname;
            retValue.Rayonkod = this.Rayonkod;
            retValue.Rayonname = this.Rayonname;
            retValue.Townskod = this.Townskod;
            retValue.Townsname = this.Townsname;
            retValue.Ulicakod = this.Ulicakod;
            retValue.Ulicaname = this.Ulicaname;
            retValue.Ndoma = this.Ndoma;
            retValue.Korpus = this.Korpus;
            retValue.Kvartira = this.Kvartira;
            retValue.Komnata = this.Komnata;
            retValue.Fio = this.Fio;
            retValue.Prim = this.Prim;
            retValue.Capcd = this.Capcd;
            retValue.Capname = this.Capname;
            retValue.Phonenum = this.Phonenum;
            retValue.Postindex = this.Postindex;
            retValue.Ducd = this.Ducd;
            retValue.Duname = this.Duname;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABONENT (LSHET, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, NDOMA, KORPUS, KVARTIRA, KOMNATA, FIO, PRIM, CAPCD, CAPNAME, PHONENUM, POSTINDEX, DUCD, DUNAME, ISDELETED) VALUES ('{0}', {1}, '{2}', {3}, '{4}', {5}, '{6}', {7}, '{8}', '{9}', '{10}', '{11}', {12}, '{13}', '{14}', {15}, '{16}', '{17}', '{18}', {19}, '{20}', {21})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Distkod.ToString(), String.IsNullOrEmpty(Distname) ? "" : Distname.Trim(), Rayonkod.ToString(), String.IsNullOrEmpty(Rayonname) ? "" : Rayonname.Trim(), Townskod.ToString(), String.IsNullOrEmpty(Townsname) ? "" : Townsname.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), String.IsNullOrEmpty(Korpus) ? "" : Korpus.Trim(), String.IsNullOrEmpty(Kvartira) ? "" : Kvartira.Trim(), Komnata.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(Prim) ? "" : Prim.Trim(), Capcd.ToString(), String.IsNullOrEmpty(Capname) ? "" : Capname.Trim(), String.IsNullOrEmpty(Phonenum) ? "" : Phonenum.Trim(), String.IsNullOrEmpty(Postindex) ? "" : Postindex.Trim(), Ducd.ToString(), String.IsNullOrEmpty(Duname) ? "" : Duname.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}
