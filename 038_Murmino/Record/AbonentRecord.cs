// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _038_Murmino
{
    [TableName("ABONENT.DBF")]
    public partial class AbonentRecord : AbstractRecord
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

        private string address;
        // <summary>
        // ADDRESS C(250)
        // </summary>
        [FieldName("ADDRESS"), FieldType('C'), FieldWidth(250)]
        public string Address
        {
            get { return address; }
            set { CheckStringData("Address", value, 250); address = value; }
        }

        private Int64 distkod;
        // <summary>
        // DISTKOD N(6)
        // </summary>
        [FieldName("DISTKOD"), FieldType('N'), FieldWidth(6)]
        public Int64 Distkod
        {
            get { return distkod; }
            set { CheckIntegerData("Distkod", value, 6); distkod = value; }
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
        // RAYONKOD N(6)
        // </summary>
        [FieldName("RAYONKOD"), FieldType('N'), FieldWidth(6)]
        public Int64 Rayonkod
        {
            get { return rayonkod; }
            set { CheckIntegerData("Rayonkod", value, 6); rayonkod = value; }
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
        // TOWNSKOD N(6)
        // </summary>
        [FieldName("TOWNSKOD"), FieldType('N'), FieldWidth(6)]
        public Int64 Townskod
        {
            get { return townskod; }
            set { CheckIntegerData("Townskod", value, 6); townskod = value; }
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
        // ULICAKOD N(6)
        // </summary>
        [FieldName("ULICAKOD"), FieldType('N'), FieldWidth(6)]
        public Int64 Ulicakod
        {
            get { return ulicakod; }
            set { CheckIntegerData("Ulicakod", value, 6); ulicakod = value; }
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
        // KORPUS N(5)
        // </summary>
        [FieldName("KORPUS"), FieldType('N'), FieldWidth(5)]
        public Int64 Korpus
        {
            get { return korpus; }
            set { CheckIntegerData("Korpus", value, 5); korpus = value; }
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

        private string fio;
        // <summary>
        // FIO C(100)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(100)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 100); fio = value; }
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
        // PRIM_ C(250)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(250)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 250); prim_ = value; }
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
        // DUCD N(7)
        // </summary>
        [FieldName("DUCD"), FieldType('N'), FieldWidth(7)]
        public Int64 Ducd
        {
            get { return ducd; }
            set { CheckIntegerData("Ducd", value, 7); ducd = value; }
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
            if (ADataRow.Table.Columns.Contains("ADDRESS")) Address = ADataRow["ADDRESS"].ToString(); else Address = "";
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
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("F")) F = ADataRow["F"].ToString(); else F = "";
            if (ADataRow.Table.Columns.Contains("I")) I = ADataRow["I"].ToString(); else I = "";
            if (ADataRow.Table.Columns.Contains("O")) O = ADataRow["O"].ToString(); else O = "";
            if (ADataRow.Table.Columns.Contains("PRIM_")) Prim_ = ADataRow["PRIM_"].ToString(); else Prim_ = "";
            if (ADataRow.Table.Columns.Contains("POSTINDEX")) Postindex = ADataRow["POSTINDEX"].ToString(); else Postindex = "";
            if (ADataRow.Table.Columns.Contains("DUCD")) Ducd = Convert.ToInt64(ADataRow["DUCD"]); else Ducd = 0;
            if (ADataRow.Table.Columns.Contains("DUNAME")) Duname = ADataRow["DUNAME"].ToString(); else Duname = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            AbonentRecord retValue = new AbonentRecord();
            retValue.Lshet = this.Lshet;
            retValue.Address = this.Address;
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
            retValue.Fio = this.Fio;
            retValue.F = this.F;
            retValue.I = this.I;
            retValue.O = this.O;
            retValue.Prim_ = this.Prim_;
            retValue.Postindex = this.Postindex;
            retValue.Ducd = this.Ducd;
            retValue.Duname = this.Duname;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABONENT (LSHET, ADDRESS, DISTKOD, DISTNAME, RAYONKOD, RAYONNAME, TOWNSKOD, TOWNSNAME, ULICAKOD, ULICANAME, NDOMA, KORPUS, KVARTIRA, FIO, F, I, O, PRIM_, POSTINDEX, DUCD, DUNAME, ISDELETED) VALUES ('{0}', '{1}', {2}, '{3}', {4}, '{5}', {6}, '{7}', {8}, '{9}', '{10}', {11}, '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', {19}, '{20}', {21})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Address) ? "" : Address.Trim(), Distkod.ToString(), String.IsNullOrEmpty(Distname) ? "" : Distname.Trim(), Rayonkod.ToString(), String.IsNullOrEmpty(Rayonname) ? "" : Rayonname.Trim(), Townskod.ToString(), String.IsNullOrEmpty(Townsname) ? "" : Townsname.Trim(), Ulicakod.ToString(), String.IsNullOrEmpty(Ulicaname) ? "" : Ulicaname.Trim(), String.IsNullOrEmpty(Ndoma) ? "" : Ndoma.Trim(), Korpus.ToString(), String.IsNullOrEmpty(Kvartira) ? "" : Kvartira.Trim(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), String.IsNullOrEmpty(F) ? "" : F.Trim(), String.IsNullOrEmpty(I) ? "" : I.Trim(), String.IsNullOrEmpty(O) ? "" : O.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), String.IsNullOrEmpty(Postindex) ? "" : Postindex.Trim(), Ducd.ToString(), String.IsNullOrEmpty(Duname) ? "" : Duname.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}