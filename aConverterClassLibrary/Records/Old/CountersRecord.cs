// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("COUNTERS.DBF")]
    [Index("LSHET", "LSHET")]
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
        // CNTTYPE N(8)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(8)]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 8); cnttype = value; }
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

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(8)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(8)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 8); servicecd = value; }
        }

        private string servicenam;
        // <summary>
        // SERVICENAM C(50)
        // </summary>
        [FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50)]
        public string Servicenam
        {
            get { return servicenam; }
            set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        }

        private DateTime setupdate;
        // <summary>
        // SETUPDATE D
        // </summary>
        [FieldName("SETUPDATE"), FieldType('D')]
        public DateTime Setupdate
        {
            get { return setupdate; }
            set { CheckData("Setupdate", value); setupdate = value; }
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
        // SETUPPLACE N(4)
        // </summary>
        [FieldName("SETUPPLACE"), FieldType('N'), FieldWidth(4)]
        public Int64 Setupplace
        {
            get { return setupplace; }
            set { CheckIntegerData("Setupplace", value, 4); setupplace = value; }
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
        // PLOMBDATE D
        // </summary>
        [FieldName("PLOMBDATE"), FieldType('D')]
        public DateTime Plombdate
        {
            get { return plombdate; }
            set { CheckData("Plombdate", value); plombdate = value; }
        }

        private string plombname;
        // <summary>
        // PLOMBNAME C(10)
        // </summary>
        [FieldName("PLOMBNAME"), FieldType('C'), FieldWidth(10)]
        public string Plombname
        {
            get { return plombname; }
            set { CheckStringData("Plombname", value, 10); plombname = value; }
        }

        private DateTime lastpov;
        // <summary>
        // LASTPOV D
        // </summary>
        [FieldName("LASTPOV"), FieldType('D')]
        public DateTime Lastpov
        {
            get { return lastpov; }
            set { CheckData("Lastpov", value); lastpov = value; }
        }

        private DateTime nextpov;
        // <summary>
        // NEXTPOV D
        // </summary>
        [FieldName("NEXTPOV"), FieldType('D')]
        public DateTime Nextpov
        {
            get { return nextpov; }
            set { CheckData("Nextpov", value); nextpov = value; }
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
        // DEACTDATE D
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D')]
        public DateTime Deactdate
        {
            get { return deactdate; }
            set { CheckData("Deactdate", value); deactdate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Cnttype = Convert.ToInt64(ADataRow["CNTTYPE"]);
            Cntname = ADataRow["CNTNAME"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Servicenam = ADataRow["SERVICENAM"].ToString();
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
    }
}

