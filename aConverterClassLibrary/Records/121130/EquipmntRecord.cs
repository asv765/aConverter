// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("EQUIPMNT.DBF")]
    [TableDescription("Оборудование абонентов")]
    [Index("LSHET", "LSHET")]
    public class EquipmntRecord: AbstractRecord
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

        private Int64 groupkod;
        // <summary>
        // GROUPKOD N(5)
        // </summary>
        [FieldName("GROUPKOD"), FieldType('N'), FieldWidth(5), FieldDescription("Код группы оборудования")]
        public Int64 Groupkod
        {
            get { return groupkod; }
            set { CheckIntegerData("Groupkod", value, 5); groupkod = value; }
        }

        private string groupname;
        // <summary>
        // GROUPNAME C(50)
        // </summary>
        [FieldName("GROUPNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование группы")]
        public string Groupname
        {
            get { return groupname; }
            set { CheckStringData("Groupname", value, 50); groupname = value; }
        }

        private Int64 markkod;
        // <summary>
        // MARKKOD N(6)
        // </summary>
        [FieldName("MARKKOD"), FieldType('N'), FieldWidth(6), FieldDescription("Код марки")]
        public Int64 Markkod
        {
            get { return markkod; }
            set { CheckIntegerData("Markkod", value, 6); markkod = value; }
        }

        private string markname;
        // <summary>
        // MARKNAME C(50)
        // </summary>
        [FieldName("MARKNAME"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование марки")]
        public string Markname
        {
            get { return markname; }
            set { CheckStringData("Markname", value, 50); markname = value; }
        }

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

        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO EQUIPMNT (LSHET, GROUPKOD, GROUPNAME, MARKKOD, MARKNAME, SETUPDATE, SERIALNUM) VALUES ('{0}', {1}, '{2}', {3}, '{4}', CTOD('{5}'), '{6}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Groupkod.ToString(), String.IsNullOrEmpty(Groupname) ? "" : Groupname.Trim(), Markkod.ToString(), String.IsNullOrEmpty(Markname) ? "" : Markname.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim());
           return rs;
        }
    }
}

