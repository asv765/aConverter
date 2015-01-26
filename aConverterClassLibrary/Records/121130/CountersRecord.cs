// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("COUNTERS.DBF")]
    [TableDescription("Контрольно-измерительное оборудование (счетчики)")]
    [Index("LSHET", "LSHET")]
    public class CountersRecord: AbstractRecord
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
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(8), FieldDescription("Код типа (марки) счетчика")]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 8); cnttype = value; }
        }

        private string cntname;
        // <summary>
        // CNTNAME C(50)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименованиеи типа (марки) счетчика")]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 50); cntname = value; }
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

        //private string servicenam;
        //// <summary>
        //// SERVICENAM C(50)
        //// </summary>
        //[FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование услуги")]
        //public string Servicenam
        //{
        //    get { return servicenam; }
        //    set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        //}

        private DateTime setupdate;
        // <summary>
        // SETUPDATE D
        // </summary>
        [FieldName("SETUPDATE"), FieldType('D'), FieldDescription("Дата установки")]
        public DateTime Setupdate
        {
            get { return setupdate; }
            set {  setupdate = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30), FieldDescription("Серийный номер")]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private Int64 setupplace;
        // <summary>
        // SETUPPLACE N(4)
        // </summary>
        [FieldName("SETUPPLACE"), FieldType('N'), FieldWidth(4), FieldDescription("Код места установки")]
        public Int64 Setupplace
        {
            get { return setupplace; }
            set { CheckIntegerData("Setupplace", value, 4); setupplace = value; }
        }

        private string place;
        // <summary>
        // PLACE C(20)
        // </summary>
        [FieldName("PLACE"), FieldType('C'), FieldWidth(20), FieldDescription("Место установки")]
        public string Place
        {
            get { return place; }
            set { CheckStringData("Place", value, 20); place = value; }
        }

        private DateTime plombdate;
        // <summary>
        // PLOMBDATE D
        // </summary>
        [FieldName("PLOMBDATE"), FieldType('D'), FieldDescription("Дата пломбирования")]
        public DateTime Plombdate
        {
            get { return plombdate; }
            set {  plombdate = value; }
        }

        private string plombname;
        // <summary>
        // PLOMBNAME C(40)
        // </summary>
        [FieldName("PLOMBNAME"), FieldType('C'), FieldWidth(40), FieldDescription("Наименование пломбы")]
        public string Plombname
        {
            get { return plombname; }
            set { CheckStringData("Plombname", value, 40); plombname = value; }
        }

        private DateTime lastpov;
        // <summary>
        // LASTPOV D
        // </summary>
        [FieldName("LASTPOV"), FieldType('D'), FieldDescription("Дата последней поверки")]
        public DateTime Lastpov
        {
            get { return lastpov; }
            set {  lastpov = value; }
        }

        private DateTime nextpov;
        // <summary>
        // NEXTPOV D
        // </summary>
        [FieldName("NEXTPOV"), FieldType('D'), FieldDescription("Дата следующей поверки")]
        public DateTime Nextpov
        {
            get { return nextpov; }
            set {  nextpov = value; }
        }

        private string prim_;
        // <summary>
        // PRIM_ C(100)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100), FieldDescription("Примечание")]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 100); prim_ = value; }
        }

        private DateTime deactdate;
        // <summary>
        // DEACTDATE D
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D'), FieldDescription("Дата снятия")]
        public DateTime Deactdate
        {
            get { return deactdate; }
            set {  deactdate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Cnttype = Convert.ToInt64(ADataRow["CNTTYPE"]);
            Cntname = ADataRow["CNTNAME"].ToString();
            Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]);
            Serialnum = ADataRow["SERIALNUM"].ToString();
            Setupplace = Convert.ToInt64(ADataRow["SETUPPLACE"]);
            Place = ADataRow["PLACE"].ToString();
            Plombdate = Convert.ToDateTime(ADataRow["PLOMBDATE"]);
            Plombname = ADataRow["PLOMBNAME"].ToString();
            Lastpov = Convert.ToDateTime(ADataRow["LASTPOV"]);
            Nextpov = Convert.ToDateTime(ADataRow["NEXTPOV"]);
            Prim_ = ADataRow["PRIM_"].ToString();
            Deactdate = Convert.ToDateTime(ADataRow["DEACTDATE"]);
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO counters (LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE) VALUES ('{0}', {1}, '{2}', CTOD('{3}'), '{4}', {5}, '{6}', CTOD('{7}'), '{8}', CTOD('{9}'), CTOD('{10}'), '{11}', CTOD('{12}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Cnttype.ToString(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Setupplace.ToString(), String.IsNullOrEmpty(Place) ? "" : Place.Trim(), Plombdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Plombdate.Month, Plombdate.Day, Plombdate.Year), String.IsNullOrEmpty(Plombname) ? "" : Plombname.Trim(), Lastpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Lastpov.Month, Lastpov.Day, Lastpov.Year), Nextpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Nextpov.Month, Nextpov.Day, Nextpov.Year), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), Deactdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Deactdate.Month, Deactdate.Day, Deactdate.Year));
           return rs;
        }
    }
}

