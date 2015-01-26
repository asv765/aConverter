using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("SUPPLNET.DBF")]
    [Index("LSHET", "LSHET")]
    [Index("SERVICECD", "SERVICECD")]
    [TableDescription("Узлы транспортной сети")]
    public partial class SupplnetRecord : AbstractRecord
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

        private Int64 knotl1cd;
        // <summary>
        // KNOTL1CD N(5)
        // </summary>
        [FieldName("KNOTL1CD"), FieldType('N'), FieldWidth(5), FieldDescription("Код узла первого уровня")]
        public Int64 Knotl1cd
        {
            get { return knotl1cd; }
            set { CheckIntegerData("Knotl1cd", value, 5); knotl1cd = value; }
        }

        private string knotl1name;
        // <summary>
        // KNOTL1NAME C(100)
        // </summary>
        [FieldName("KNOTL1NAME"), FieldType('C'), FieldWidth(100), FieldDescription("Наименование узла первого уровня")]
        public string Knotl1name
        {
            get { return knotl1name; }
            set { CheckStringData("Knotl1name", value, 100); knotl1name = value; }
        }

        private Int64 knotl2cd;
        // <summary>
        // KNOTL2CD N(5)
        // </summary>
        [FieldName("KNOTL2CD"), FieldType('N'), FieldWidth(5), FieldDescription("Код узла второго уровня")]
        public Int64 Knotl2cd
        {
            get { return knotl2cd; }
            set { CheckIntegerData("Knotl2cd", value, 5); knotl2cd = value; }
        }

        private string knotl2name;
        // <summary>
        // KNOTL2NAME C(100)
        // </summary>
        [FieldName("KNOTL2NAME"), FieldType('C'), FieldWidth(100), FieldDescription("Наименование узла второго уровня")]
        public string Knotl2name
        {
            get { return knotl2name; }
            set { CheckStringData("Knotl2name", value, 100); knotl2name = value; }
        }

        private DateTime suppdate;
        // <summary>
        // SUPPDATE D(8)
        // </summary>
        [FieldName("SUPPDATE"), FieldType('D'), FieldWidth(8), FieldDescription("Код подключения/отключения абонент от/к узла/узлу")]
        public DateTime Suppdate
        {
            get { return suppdate; }
            set { suppdate = value; }
        }

        private bool connected;
        // <summary>
        // CONNECTED L(1)
        // </summary>
        [FieldName("CONNECTED"), FieldType('L'), FieldWidth(1), FieldDescription("Признак подключения (true) или отключения (false)")]
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Knotl1cd = Convert.ToInt64(ADataRow["KNOTL1CD"]);
            Knotl1name = ADataRow["KNOTL1NAME"].ToString();
            Knotl2cd = Convert.ToInt64(ADataRow["KNOTL2CD"]);
            Knotl2name = ADataRow["KNOTL2NAME"].ToString();
            Suppdate = Convert.ToDateTime(ADataRow["SUPPDATE"]);
            Connected = ADataRow["CONNECTED"].ToString() == "True" ? true : false;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SUPPLNET (LSHET, SERVICECD, KNOTL1CD, KNOTL1NAME, KNOTL2CD, KNOTL2NAME, SUPPDATE, CONNECTED) VALUES ('{0}', {1}, {2}, '{3}', {4}, '{5}', CTOD('{6}'), {7})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Servicecd.ToString(), Knotl1cd.ToString(), String.IsNullOrEmpty(Knotl1name) ? "" : Knotl1name.Trim(), Knotl2cd.ToString(), String.IsNullOrEmpty(Knotl2name) ? "" : Knotl2name.Trim(), Suppdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Suppdate.Month, Suppdate.Day, Suppdate.Year), (Connected ? ".T." : ".F."));
            return rs;
        }
    }
}
