// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("EQUIPMNT.DBF")]
    [Index("LSHET", "LSHET")]
    public class EquipmntRecord: AbstractRecord
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

        private Int64 groupkod;
        // <summary>
        // GROUPKOD N(5)
        // </summary>
        [FieldName("GROUPKOD"), FieldType('N'), FieldWidth(5)]
        public Int64 Groupkod
        {
            get { return groupkod; }
            set { CheckIntegerData("Groupkod", value, 5); groupkod = value; }
        }

        private string groupname;
        // <summary>
        // GROUPNAME C(50)
        // </summary>
        [FieldName("GROUPNAME"), FieldType('C'), FieldWidth(50)]
        public string Groupname
        {
            get { return groupname; }
            set { CheckStringData("Groupname", value, 50); groupname = value; }
        }

        private Int64 markkod;
        // <summary>
        // MARKKOD N(6)
        // </summary>
        [FieldName("MARKKOD"), FieldType('N'), FieldWidth(6)]
        public Int64 Markkod
        {
            get { return markkod; }
            set { CheckIntegerData("Markkod", value, 6); markkod = value; }
        }

        private string markname;
        // <summary>
        // MARKNAME C(50)
        // </summary>
        [FieldName("MARKNAME"), FieldType('C'), FieldWidth(50)]
        public string Markname
        {
            get { return markname; }
            set { CheckStringData("Markname", value, 50); markname = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Groupkod = Convert.ToInt64(ADataRow["GROUPKOD"]);
            Groupname = ADataRow["GROUPNAME"].ToString();
            Markkod = Convert.ToInt64(ADataRow["MARKKOD"]);
            Markname = ADataRow["MARKNAME"].ToString();
            Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]);
            Serialnum = ADataRow["SERIALNUM"].ToString();
        }
    }
}

