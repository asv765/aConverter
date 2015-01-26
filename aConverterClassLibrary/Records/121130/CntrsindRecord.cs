// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("CNTRSIND.DBF")]
    [TableDescription("Показания счетчиков")]
    [Index("LSHET", "LSHET"), Index("CNTTYPE", "CNTTYPE"), Index("SERIALNUM", "SERIALNUM")]
    public class CntrsindRecord: AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("Лицевой счет")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 cnttype;
        // <summary>
        // CNTTYPE N(8)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(8), FieldDescription("Код типа счетчика")]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 8); cnttype = value; }
        }

        //private Int64 servicecd;
        //// <summary>
        //// SERVICECD N(8)
        //// </summary>
        //[FieldName("SERVICECD"), FieldType('N'), FieldWidth(8), FieldDescription("Код услуги")]
        //public Int64 Servicecd
        //{
        //    get { return servicecd; }
        //    set { CheckIntegerData("Servicecd", value, 8); servicecd = value; }
        //}

        private string serialnum;
        // <summary>
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30), FieldDescription("Серийный номер счетчика")]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private decimal oldind;
        // <summary>
        // OLDIND N(11,4)
        // </summary>
        [FieldName("OLDIND"), FieldType('N'), FieldWidth(11), FieldDec(4)]
        public decimal Oldind
        {
            get { return oldind; }
            set { CheckDecimalData("Oldind", value, 11, 4); oldind = value; }
        }

        private decimal ob_em;
        // <summary>
        // OB_EM N(11,4)
        // </summary>
        [FieldName("OB_EM"), FieldType('N'), FieldWidth(11), FieldDec(4)]
        public decimal Ob_em
        {
            get { return ob_em; }
            set { CheckDecimalData("Ob_em", value, 11, 4); ob_em = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(11,4)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(11), FieldDec(4), FieldDescription("Текущие показания")]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 11, 4); indication = value; }
        }

        private DateTime inddate;
        // <summary>
        // INDDATE D
        // </summary>
        [FieldName("INDDATE"), FieldType('D'), FieldDescription("Дата съема показаний")]
        public DateTime Inddate
        {
            get { return inddate; }
            set {  inddate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Cnttype = Convert.ToInt64(ADataRow["CNTTYPE"]);
            Serialnum = ADataRow["SERIALNUM"].ToString();
            Oldind = Convert.ToDecimal(ADataRow["OLDIND"]);
            Ob_em = Convert.ToDecimal(ADataRow["OB_EM"]);
            Indication = Convert.ToDecimal(ADataRow["INDICATION"]);
            Inddate = Convert.ToDateTime(ADataRow["INDDATE"]);
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO cntrsind (LSHET, CNTTYPE, SERIALNUM, OLDIND, OB_EM, INDICATION, INDDATE) VALUES ('{0}', {1}, '{2}', {3}, {4}, {5}, CTOD('{6}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Cnttype.ToString(), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Oldind.ToString().Replace(',','.'), Ob_em.ToString().Replace(',','.'), Indication.ToString().Replace(',','.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year));
           return rs;
        }
    }
}

