// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("DOGOVOR.DBF")]
    [TableDescription("Договора с абонентами")]
    [Index("LSHET", "LSHET")]
    public class DogovorRecord: AbstractRecord
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

        private Int64 dogtypekod;
        // <summary>
        // DOGTYPEKOD N(7)
        // </summary>
        [FieldName("DOGTYPEKOD"), FieldType('N'), FieldWidth(7), FieldDescription("Код типа договора")]
        public Int64 Dogtypekod
        {
            get { return dogtypekod; }
            set { CheckIntegerData("Dogtypekod", value, 7); dogtypekod = value; }
        }

        private string dogtypenam;
        // <summary>
        // DOGTYPENAM C(50)
        // </summary>
        [FieldName("DOGTYPENAM"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование типа договора")]
        public string Dogtypenam
        {
            get { return dogtypenam; }
            set { CheckStringData("Dogtypenam", value, 50); dogtypenam = value; }
        }

        private string descriptio;
        // <summary>
        // DESCRIPTIO C(100)
        // </summary>
        [FieldName("DESCRIPTIO"), FieldType('C'), FieldWidth(100), FieldDescription("Описание")]
        public string Descriptio
        {
            get { return descriptio; }
            set { CheckStringData("Descriptio", value, 100); descriptio = value; }
        }

        private DateTime startdate;
        // <summary>
        // STARTDATE D
        // </summary>
        [FieldName("STARTDATE"), FieldType('D'), FieldDescription("Дата начала действия")]
        public DateTime Startdate
        {
            get { return startdate; }
            set {  startdate = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D
        // </summary>
        [FieldName("ENDDATE"), FieldType('D'), FieldDescription("Дата окончания действия")]
        public DateTime Enddate
        {
            get { return enddate; }
            set {  enddate = value; }
        }

        private string seria;
        // <summary>
        // SERIA C(12)
        // </summary>
        [FieldName("SERIA"), FieldType('C'), FieldWidth(12), FieldDescription("Серия")]
        public string Seria
        {
            get { return seria; }
            set { CheckStringData("Seria", value, 12); seria = value; }
        }

        private string nomer;
        // <summary>
        // NOMER C(12)
        // </summary>
        [FieldName("NOMER"), FieldType('C'), FieldWidth(12), FieldDescription("Номер")]
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

        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO DOGOVOR (LSHET, DOGTYPEKOD, DOGTYPENAM, DESCRIPTIO, STARTDATE, ENDDATE, SERIA, NOMER) VALUES ('{0}', {1}, '{2}', '{3}', CTOD('{4}'), CTOD('{5}'), '{6}', '{7}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Dogtypekod.ToString(), String.IsNullOrEmpty(Dogtypenam) ? "" : Dogtypenam.Trim(), String.IsNullOrEmpty(Descriptio) ? "" : Descriptio.Trim(), Startdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Startdate.Month, Startdate.Day, Startdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year), String.IsNullOrEmpty(Seria) ? "" : Seria.Trim(), String.IsNullOrEmpty(Nomer) ? "" : Nomer.Trim());
           return rs;
        }
    }
}

