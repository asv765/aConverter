// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("NACH.DBF")]
    [TableDescription("Расшифровка сумм начислений (заполнять не обязательно)")]
    [Index("LSHET", "LSHET")]
    public class NachRecord: AbstractRecord
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
        [FieldName("MONTH"), FieldType('N'), FieldWidth(4), FieldDescription("Месяц, в котором выполнено начисление")]
        public Int64 Month
        {
            get { return month; }
            set { CheckIntegerData("Month", value, 4); month = value; }
        }

        private Int64 year;
        // <summary>
        // YEAR N(6)
        // </summary>
        [FieldName("YEAR"), FieldType('N'), FieldWidth(6), FieldDescription("Год, в котором выполнено начисление")]
        public Int64 Year
        {
            get { return year; }
            set { CheckIntegerData("Year", value, 6); year = value; }
        }

        private Int64 month2;
        // <summary>
        // MONTH2 N(4)
        // </summary>
        [FieldName("MONTH2"), FieldType('N'), FieldWidth(4), FieldDescription("Месяц, за который выполнено начисление")]
        public Int64 Month2
        {
            get { return month2; }
            set { CheckIntegerData("Month2", value, 4); month2 = value; }
        }

        private Int64 year2;
        // <summary>
        // YEAR2 N(6)
        // </summary>
        [FieldName("YEAR2"), FieldType('N'), FieldWidth(6), FieldDescription("Год, за который выполнено начисление")]
        public Int64 Year2
        {
            get { return year2; }
            set { CheckIntegerData("Year2", value, 6); year2 = value; }
        }

        private decimal fnath;
        // <summary>
        // FNATH N(13,2)
        // </summary>
        [FieldName("FNATH"), FieldType('N'), FieldWidth(18), FieldDec(4), FieldDescription("Сумма начисления")]
        public decimal Fnath
        {
            get { return fnath; }
            set { CheckDecimalData("Fnath", value, 18, 4); fnath = value; }
        }

        //private decimal lgnath;
        //// <summary>
        //// LGNATH N(13,2)
        //// </summary>
        //[FieldName("LGNATH"), FieldType('N'), FieldWidth(13), FieldDec(2), FieldDescription("Сумма начисления по льготам (выпадающие доходы)")]
        //public decimal Lgnath
        //{
        //    get { return lgnath; }
        //    set { CheckDecimalData("Lgnath", value, 13, 2); lgnath = value; }
        //}

        private decimal prochl;
        // <summary>
        // PROCHL N(13,2)
        // </summary>
        [FieldName("PROCHL"), FieldType('N'), FieldWidth(18), FieldDec(4), FieldDescription("Сумма перерасчета")]
        public decimal Prochl
        {
            get { return prochl; }
            set { CheckDecimalData("Prochl", value, 18, 4); prochl = value; }
        }

        private decimal volume;
        // <summary>
        // VOLUME N(13,2)
        // </summary>
        [FieldName("VOLUME"), FieldType('N'), FieldWidth(18), FieldDec(4), FieldDescription("Начисленный объем")]
        public decimal Volume
        {
            get { return volume; }
            set { CheckDecimalData("Volume", value, 18, 4); volume = value; }
        }

        private Int64 regimcd;
        // <summary>
        // REGIMCD N(6)
        // </summary>
        [FieldName("REGIMCD"), FieldType('N'), FieldWidth(6), FieldDescription("Код режима потребления")]
        public Int64 Regimcd
        {
            get { return regimcd; }
            set { CheckIntegerData("Regimcd", value, 6); regimcd = value; }
        }

        private string regimname;
        // <summary>
        // REGIMNAME C(50)
        // </summary>
        [FieldName("REGIMNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование режима потребления")]
        public string Regimname
        {
            get { return regimname; }
            set { CheckStringData("Regimname", value, 50); regimname = value; }
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
        // SERVICENAM C(50)s
        // </summary>
        [FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование услуги")]
        public string Servicenam
        {
            get { return servicenam; }
            set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        }

        private DateTime date_vv;
        // <summary>
        // DATE_VV D
        // </summary>
        [FieldName("DATE_VV"), FieldType('D'), FieldDescription("Дата учета начисления/объема")]
        public DateTime Date_vv
        {
            get { return date_vv; }
            set { date_vv = value; }
        }

        private Int64 type;
        // <summary>
        // TYPE N(2), 0 - по нормам, 1 - по счетчику
        // </summary>
        [FieldName("TYPE"), FieldType('N'), FieldWidth(2)]
        public Int64 Type
        {
            get { return type; }
            set { CheckIntegerData("Type", value, 2); type = value; }
        }

        private string docname;
        // <summary>
        // DOCNAME C(50)
        // </summary>
        [FieldName("DOCNAME"), FieldType('C'), FieldWidth(150), FieldDescription("Наименование документа")]
        public string Docname
        {
            get { return docname; }
            set { CheckStringData("Docname", value, 150); docname = value; }
        }

        private string docnumber;
        // <summary>
        // DOCNUMBER C(10)
        // </summary>
        [FieldName("DOCNUMBER"), FieldType('C'), FieldWidth(10), FieldDescription("Номер документа")]
        public string Docnumber
        {
            get { return docnumber; }
            set { CheckStringData("Docnumber", value, 10); docnumber = value; }
        }

        private DateTime docdate;
        // <summary>
        // DOCDATE D(8)
        // </summary>
        [FieldName("DOCDATE"), FieldType('D'), FieldWidth(8), FieldDescription("Дата документа на изменение")]
        public DateTime Docdate
        {
            get { return docdate; }
            set { docdate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Documentcd = ADataRow["DOCUMENTCD"].ToString();
            Month = Convert.ToInt64(ADataRow["MONTH"]);
            Year = Convert.ToInt64(ADataRow["YEAR"]);
            Month2 = Convert.ToInt64(ADataRow["MONTH2"]);
            Year2 = Convert.ToInt64(ADataRow["YEAR2"]);
            Fnath = Convert.ToDecimal(ADataRow["FNATH"]);
            Prochl = Convert.ToDecimal(ADataRow["PROCHL"]);
            Volume = Convert.ToDecimal(ADataRow["VOLUME"]);
            Regimcd = Convert.ToInt64(ADataRow["REGIMCD"]);
            Regimname = ADataRow["REGIMNAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
            Date_vv = Convert.ToDateTime(ADataRow["DATE_VV"]);
            Type = Convert.ToInt64(ADataRow["TYPE"]);
            Docname = ADataRow["DOCNAME"].ToString();
            Docnumber = ADataRow["DOCNUMBER"].ToString();
            Docdate = Convert.ToDateTime(ADataRow["DOCDATE"]);
        }

        //public override AbstractRecord CloneRecord()
        //{
        //    NachRecord retValue = new NachRecord();
        //    retValue.Lshet = this.Lshet;
        //    retValue.Documentcd = this.Documentcd;
        //    retValue.Month = this.Month;
        //    retValue.Year = this.Year;
        //    retValue.Month2 = this.Month2;
        //    retValue.Year2 = this.Year2;
        //    retValue.Fnath = this.Fnath;
        //    retValue.Prochl = this.Prochl;
        //    retValue.Volume = this.Volume;
        //    retValue.Regimcd = this.Regimcd;
        //    retValue.Regimname = this.Regimname;
        //    retValue.Servicecd = this.Servicecd;
        //    retValue.Servicenam = this.Servicenam;
        //    retValue.Date_vv = this.Date_vv;
        //    retValue.Type = this.Type;
        //    retValue.Docname = this.Docname;
        //    retValue.Docnumber = this.Docnumber;
        //    retValue.Docdate = this.Docdate;
        //    return retValue;
        //}

        public string GetInsertScript(string tableName)
        {
            string rs = String.Format("INSERT INTO " + tableName + " (LSHET, DOCUMENTCD, MONTH, YEAR, MONTH2, YEAR2, FNATH, PROCHL, VOLUME, REGIMCD, REGIMNAME, SERVICECD, SERVICENAM, DATE_VV, TYPE, DOCNAME, DOCNUMBER, DOCDATE) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, '{10}', {11}, '{12}', CTOD('{13}'), {14}, '{15}', '{16}', CTOD('{17}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Documentcd) ? "" : Documentcd.Trim(), Month.ToString(), Year.ToString(), Month2.ToString(), Year2.ToString(), Fnath.ToString().Replace(',', '.'), Prochl.ToString().Replace(',', '.'), Volume.ToString().Replace(',', '.'), Regimcd.ToString(), String.IsNullOrEmpty(Regimname) ? "" : Regimname.Trim(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenam) ? "" : Servicenam.Trim(), Date_vv == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_vv.Month, Date_vv.Day, Date_vv.Year), Type.ToString(), String.IsNullOrEmpty(Docname) ? "" : Docname.Trim(), String.IsNullOrEmpty(Docnumber) ? "" : Docnumber.Trim(), Docdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Docdate.Month, Docdate.Day, Docdate.Year));
            return rs;
        }

        public override string GetInsertScript()
        {
            string rs = GetInsertScript("NACH");
            return rs;
        }
    }
}

