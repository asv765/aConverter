// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("COUNTERS.DBF")]
    public class CountersRecord: AbstractRecord
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

        private Int64 cnttype;
        // <summary>
        // CNTTYPE N(10)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(10)]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 10); cnttype = value; }
        }

        private string cntname;
        // <summary>
        // CNTNAME C(50)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(50)]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 50); cntname = value; }
        }

        private DateTime setupdate;
        // <summary>
        // SETUPDATE D(8)
        // </summary>
        [FieldName("SETUPDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Setupdate
        {
            get { return setupdate; }
            set {  setupdate = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private Int64 setupplace;
        // <summary>
        // SETUPPLACE N(6)
        // </summary>
        [FieldName("SETUPPLACE"), FieldType('N'), FieldWidth(6)]
        public Int64 Setupplace
        {
            get { return setupplace; }
            set { CheckIntegerData("Setupplace", value, 6); setupplace = value; }
        }

        private string place;
        // <summary>
        // PLACE C(20)
        // </summary>
        [FieldName("PLACE"), FieldType('C'), FieldWidth(20)]
        public string Place
        {
            get { return place; }
            set { CheckStringData("Place", value, 20); place = value; }
        }

        private DateTime plombdate;
        // <summary>
        // PLOMBDATE D(8)
        // </summary>
        [FieldName("PLOMBDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Plombdate
        {
            get { return plombdate; }
            set {  plombdate = value; }
        }

        private string plombname;
        // <summary>
        // PLOMBNAME C(40)
        // </summary>
        [FieldName("PLOMBNAME"), FieldType('C'), FieldWidth(40)]
        public string Plombname
        {
            get { return plombname; }
            set { CheckStringData("Plombname", value, 40); plombname = value; }
        }

        private DateTime lastpov;
        // <summary>
        // LASTPOV D(8)
        // </summary>
        [FieldName("LASTPOV"), FieldType('D'), FieldWidth(8)]
        public DateTime Lastpov
        {
            get { return lastpov; }
            set {  lastpov = value; }
        }

        private DateTime nextpov;
        // <summary>
        // NEXTPOV D(8)
        // </summary>
        [FieldName("NEXTPOV"), FieldType('D'), FieldWidth(8)]
        public DateTime Nextpov
        {
            get { return nextpov; }
            set {  nextpov = value; }
        }

        private string prim_;
        // <summary>
        // PRIM_ C(100)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100)]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 100); prim_ = value; }
        }

        private DateTime deactdate;
        // <summary>
        // DEACTDATE D(8)
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D'), FieldWidth(8)]
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
           string rs = String.Format("INSERT INTO COUNTERS (LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE) VALUES ('{0}', {1}, '{2}', CTOD('{3}'), '{4}', {5}, '{6}', CTOD('{7}'), '{8}', CTOD('{9}'), CTOD('{10}'), '{11}', CTOD('{12}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Cnttype.ToString(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Setupplace.ToString(), String.IsNullOrEmpty(Place) ? "" : Place.Trim(), Plombdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Plombdate.Month, Plombdate.Day, Plombdate.Year), String.IsNullOrEmpty(Plombname) ? "" : Plombname.Trim(), Lastpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Lastpov.Month, Lastpov.Day, Lastpov.Year), Nextpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Nextpov.Month, Nextpov.Day, Nextpov.Year), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), Deactdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Deactdate.Month, Deactdate.Day, Deactdate.Year));
           return rs;
        }
    }
}
