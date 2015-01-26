// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("CNTRSIND.DBF")]
    [TableDescription("Показания счетчиков")]
    [Index("COUNTERID", "COUNTERID")]
    public class CntrsindRecord: AbstractRecord
    {
        private string counterid;
        // <summary>
        // COUNTERID C(20)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(20)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 20); counterid = value; }
        }

        private string documentcd;
        // <summary>
        // DOCUMENTCD C(20)
        // </summary>
        [FieldName("DOCUMENTCD"), FieldType('C'), FieldWidth(20)]
        public string Documentcd
        {
            get { return documentcd; }
            set { CheckStringData("Documentcd", value, 20); documentcd = value; }
        }

        private decimal oldind;
        // <summary>
        // OLDIND N(11,4)
        // </summary>
        [FieldName("OLDIND"), FieldType('N'), FieldWidth(14), FieldDec(4)]
        public decimal Oldind
        {
            get { return oldind; }
            set { CheckDecimalData("Oldind", value, 14, 4); oldind = value; }
        }

        private decimal ob_em;
        // <summary>
        // OB_EM N(11,4)
        // </summary>
        [FieldName("OB_EM"), FieldType('N'), FieldWidth(14), FieldDec(4)]
        public decimal Ob_em
        {
            get { return ob_em; }
            set { CheckDecimalData("Ob_em", value, 14, 4); ob_em = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(11,4)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(14), FieldDec(4), FieldDescription("Текущие показания")]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 14, 4); indication = value; }
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

        private Int64 indtype;
        // <summary>
        // INDTYPE N(2)
        // </summary>
        [FieldName("INDTYPE"), FieldType('N'), FieldWidth(2), FieldDescription("Тип показаний")]
        public IndicationType Indtype
        {
            get { return (IndicationType)indtype; }
            set { indtype = (int)value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Counterid = ADataRow["COUNTERID"].ToString();
            Documentcd = ADataRow["DOCUMENTCD"].ToString();
            Oldind = Convert.ToDecimal(ADataRow["OLDIND"]);
            Ob_em = Convert.ToDecimal(ADataRow["OB_EM"]);
            Indication = Convert.ToDecimal(ADataRow["INDICATION"]);
            Inddate = Convert.ToDateTime(ADataRow["INDDATE"]);
            indtype = Convert.ToInt64(ADataRow["INDTYPE"]);
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO cntrsind (COUNTERID, DOCUMENTCD, OLDIND, OB_EM, INDICATION, INDDATE, INDTYPE) VALUES ('{0}', '{1}', {2}, {3}, {4}, CTOD('{5}'), {6})", String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Documentcd) ? "" : Documentcd.Trim(), Oldind.ToString().Replace(',', '.'), Ob_em.ToString().Replace(',', '.'), Indication.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year), indtype.ToString());
            return rs;
        }
    }

    public enum IndicationType
    {
        Обычные = 0,
        Контрольные = 1
    }
}

