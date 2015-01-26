// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("DOGOVOR.DBF")]
    [Index("LSHET", "LSHET")]
    public class DogovorRecord: AbstractRecord
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

        private Int64 dogtypekod;
        // <summary>
        // DOGTYPEKOD N(7)
        // </summary>
        [FieldName("DOGTYPEKOD"), FieldType('N'), FieldWidth(7)]
        public Int64 Dogtypekod
        {
            get { return dogtypekod; }
            set { CheckIntegerData("Dogtypekod", value, 7); dogtypekod = value; }
        }

        private string dogtypenam;
        // <summary>
        // DOGTYPENAM C(50)
        // </summary>
        [FieldName("DOGTYPENAM"), FieldType('C'), FieldWidth(50)]
        public string Dogtypenam
        {
            get { return dogtypenam; }
            set { CheckStringData("Dogtypenam", value, 50); dogtypenam = value; }
        }

        private string descriptio;
        // <summary>
        // DESCRIPTIO C(100)
        // </summary>
        [FieldName("DESCRIPTIO"), FieldType('C'), FieldWidth(100)]
        public string Descriptio
        {
            get { return descriptio; }
            set { CheckStringData("Descriptio", value, 100); descriptio = value; }
        }

        private DateTime startdate;
        // <summary>
        // STARTDATE D
        // </summary>
        [FieldName("STARTDATE"), FieldType('D')]
        public DateTime Startdate
        {
            get { return startdate; }
            set { CheckData("Startdate", value); startdate = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D
        // </summary>
        [FieldName("ENDDATE"), FieldType('D')]
        public DateTime Enddate
        {
            get { return enddate; }
            set { CheckData("Enddate", value); enddate = value; }
        }

        private string seria;
        // <summary>
        // SERIA C(12)
        // </summary>
        [FieldName("SERIA"), FieldType('C'), FieldWidth(12)]
        public string Seria
        {
            get { return seria; }
            set { CheckStringData("Seria", value, 12); seria = value; }
        }

        private string nomer;
        // <summary>
        // NOMER C(12)
        // </summary>
        [FieldName("NOMER"), FieldType('C'), FieldWidth(12)]
        public string Nomer
        {
            get { return nomer; }
            set { CheckStringData("Nomer", value, 12); nomer = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Dogtypekod = Convert.ToInt64(ADataRow["DOGTYPEKOD"]);
            Dogtypenam = ADataRow["DOGTYPENAM"].ToString();
            Descriptio = ADataRow["DESCRIPTIO"].ToString();
            Startdate = Convert.ToDateTime(ADataRow["STARTDATE"]);
            Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]);
            Seria = ADataRow["SERIA"].ToString();
            Nomer = ADataRow["NOMER"].ToString();
        }
    }
}

