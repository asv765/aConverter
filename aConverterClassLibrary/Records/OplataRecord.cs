// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("OPLATA.DBF")]
    [TableDescription("Оплата")]
    [Index("LSHET", "LSHET"), Index("MONTH", "MONTH(DATE_VV)"), Index("YEAR", "YEAR(DATE_VV)"), Index("SERVICECD", "SERVICECD")]
    [Index("COMPLEX", "LSHET + STR(SERVICECD,5) + STR(YEAR(DATE_VV),4) + STR(MONTH(DATE_VV),2)")]
    public class OplataRecord: AbstractRecord
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

        private Int64 month;
        // <summary>
        // MONTH N(4)
        // </summary>
        [FieldName("MONTH"), FieldType('N'), FieldWidth(2), FieldDescription("Месяц, за который осуществлена оплата")]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 2); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(6)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(4), FieldDescription("Год, за который осуществлена оплата")]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 4); year = value; }
        }

        private decimal summa;
        // <summary>
        // SUMMA N(13,2)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(18), FieldDec(2), FieldDescription("Сумма оплаты")]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 18, 2); summa = value; }
        }

        //private decimal oldsth;
        //// <summary>
        //// OLDSTH N(13,3)
        //// </summary>
        //[FieldName("OLDSTH"), FieldType('N'), FieldWidth(13), FieldDec(3), FieldDescription("Предыдущие показания счетчика")]
        //public decimal Oldsth
        //{
        //    get { return oldsth; }
        //    set { CheckDecimalData("Oldsth", value, 13, 3); oldsth = value; }
        //}

        //private decimal laststh;
        //// <summary>
        //// LASTSTH N(13,3)
        //// </summary>
        //[FieldName("LASTSTH"), FieldType('N'), FieldWidth(13), FieldDec(3), FieldDescription("Текущие (оплачиваемые) показания счетчика")]
        //public decimal Laststh
        //{
        //    get { return laststh; }
        //    set { CheckDecimalData("Laststh", value, 13, 3); laststh = value; }
        //}

        //private decimal ob_em;
        //// <summary>
        //// OB_EM N(13,3)
        //// </summary>
        //[FieldName("OB_EM"), FieldType('N'), FieldWidth(13), FieldDec(3), FieldDescription("Оплачиваемый объем")]
        //public decimal Ob_em
        //{
        //    get { return ob_em; }
        //    set { CheckDecimalData("Ob_em", value, 13, 3); ob_em = value; }
        //}

        private DateTime date;
        // <summary>
        // DATE D
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldDescription("Дата оплаты")]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        private DateTime date_vv;
        // <summary>
        // DATE_VV D
        // </summary>
        [FieldName("DATE_VV"), FieldType('D'), FieldDescription("Дата учета оплаты")]
        public DateTime Date_vv
        {
            get { return date_vv; }
            set {  date_vv = value; }
        }

        private DateTime datetind;
        // <summary>
        // DATETIND D
        // </summary>
        [FieldName("DATETIND"), FieldType('D'), FieldDescription("Дата съема показаний счетчика")]
        public DateTime Datetind
        {
            get { return datetind; }
            set {  datetind = value; }
        }

        private Int64 sourcecd;
        // <summary>
        // SOURCECD N(6)
        // </summary>
        [FieldName("SOURCECD"), FieldType('N'), FieldWidth(6), FieldDescription("Код источника оплаты")]
        public Int64 Sourcecd
        {
            get { return sourcecd; }
            set { CheckIntegerData("Sourcecd", value, 6); sourcecd = value; }
        }

        private string sourcename;
        // <summary>
        // SOURCENAME C(50)
        // </summary>
        [FieldName("SOURCENAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование источника оплаты")]
        public string Sourcename
        {
            get { return sourcename; }
            set { CheckStringData("Sourcename", value, 50); sourcename = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(5)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(5), FieldDescription("Код услуги")]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 5); servicecd = value; }
        }

        private string servicenam;
        // <summary>
        // SERVICENAM C(50)
        // </summary>
        [FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование услуги")]
        public string Servicenam
        {
            get { return servicenam; }
            set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        }

        private string prim_;
        // <summary>
        // PRIM_ C(30)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100), FieldDescription("Комментарий")]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 100); prim_ = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Documentcd = ADataRow["DOCUMENTCD"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Summa = Convert.ToDecimal(ADataRow["SUMMA"]);
            Date = Convert.ToDateTime(ADataRow["DATE"]);
            Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]);
            Datetind = Convert.ToDateTime(ADataRow["DATETIND"]);
            Sourcecd = Convert.ToInt64(ADataRow["SOURCECD"]);
            Sourcename = ADataRow["SOURCENAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
            Prim_ = ADataRow["PRIM_"].ToString();
        }

        public string GetInsertScript(string tableName)
        {
           string rs = String.Format("INSERT INTO "+tableName+" (LSHET, DOCUMENTCD, MONTH, YEAR, SUMMA, DATE, DATE_VV, DATETIND, SOURCECD, SOURCENAME, SERVICECD, SERVICENAM, PRIM_) VALUES ('{0}', '{1}', {2}, {3}, {4}, CTOD('{5}'), CTOD('{6}'), CTOD('{7}'), {8}, '{9}', {10}, '{11}', '{12}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Documentcd) ? "" : Documentcd.Trim(), Month.ToString(), Year.ToString(), Summa.ToString().Replace(',','.'), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), Date_vv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_vv.Month, Date_vv.Day, Date_vv.Year), Datetind == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datetind.Month, Datetind.Day, Datetind.Year), Sourcecd.ToString(), String.IsNullOrEmpty(Sourcename) ? "" : Sourcename.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim(), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim());
           return rs;
        }

        public override string GetInsertScript()
        {
            string rs = GetInsertScript("OPLATA");
            return rs;
        }
    }
}

