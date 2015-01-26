// ���� ������������ aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("COUNTERS.DBF")]
    [TableDescription("����������-������������� ������������ (��������)")]
    [Index("LSHET", "LSHET")]
    [Index("COUNTERID", "COUNTERID")]
    public class CountersRecord: AbstractRecord
    {
        private string counterid;
        // <summary>
        // COUNTERID C(20)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(20)]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 20); counterid = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("������� ����")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 cnttype;
        // <summary>
        // CNTTYPE N(8)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(8), FieldDescription("��� ���� (�����) ��������")]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 8); cnttype = value; }
        }

        private string cntname;
        // <summary>
        // CNTNAME C(50)
        // </summary>
        [FieldName("CNTNAME"), FieldType('C'), FieldWidth(50), FieldDescription("������������� ���� (�����) ��������")]
        public string Cntname
        {
            get { return cntname; }
            set { CheckStringData("Cntname", value, 50); cntname = value; }
        }

        //private Int64 servicecd;
        //// <summary>
        //// SERVICECD N(8)
        //// </summary>
        //[FieldName("SERVICECD"), FieldType('N'), FieldWidth(8), FieldDescription("��� ������")]
        //public Int64 Servicecd
        //{
        //    get { return servicecd; }
        //    set { CheckIntegerData("Servicecd", value, 8); servicecd = value; }
        //}

        //private string servicenam;
        //// <summary>
        //// SERVICENAM C(50)
        //// </summary>
        //[FieldName("SERVICENAM"), FieldType('C'), FieldWidth(50), FieldDescription("������������ ������")]
        //public string Servicenam
        //{
        //    get { return servicenam; }
        //    set { CheckStringData("Servicenam", value, 50); servicenam = value; }
        //}

        private DateTime setupdate;
        // <summary>
        // SETUPDATE D
        // </summary>
        [FieldName("SETUPDATE"), FieldType('D'), FieldDescription("���� ���������")]
        public DateTime Setupdate
        {
            get { return setupdate; }
            set {  setupdate = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30), FieldDescription("�������� �����")]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private Int64 setupplace;
        // <summary>
        // SETUPPLACE N(4)
        // </summary>
        [FieldName("SETUPPLACE"), FieldType('N'), FieldWidth(4), FieldDescription("��� ����� ���������")]
        public CounterSetupPlace Setupplace
        {
            get { return (CounterSetupPlace)setupplace; }
            set { setupplace = (int)value; }
        }

        private string place;
        // <summary>
        // PLACE C(20)
        // </summary>
        [FieldName("PLACE"), FieldType('C'), FieldWidth(20), FieldDescription("����� ���������")]
        public string Place
        {
            get { return place; }
            set { CheckStringData("Place", value, 20); place = value; }
        }

        private DateTime plombdate;
        // <summary>
        // PLOMBDATE D
        // </summary>
        [FieldName("PLOMBDATE"), FieldType('D'), FieldDescription("���� �������������")]
        public DateTime Plombdate
        {
            get { return plombdate; }
            set {  plombdate = value; }
        }

        private string plombname;
        // <summary>
        // PLOMBNAME C(40)
        // </summary>
        [FieldName("PLOMBNAME"), FieldType('C'), FieldWidth(40), FieldDescription("������������ ������")]
        public string Plombname
        {
            get { return plombname; }
            set { CheckStringData("Plombname", value, 40); plombname = value; }
        }

        private DateTime lastpov;
        // <summary>
        // LASTPOV D
        // </summary>
        [FieldName("LASTPOV"), FieldType('D'), FieldDescription("���� ��������� �������")]
        public DateTime Lastpov
        {
            get { return lastpov; }
            set {  lastpov = value; }
        }

        private DateTime nextpov;
        // <summary>
        // NEXTPOV D
        // </summary>
        [FieldName("NEXTPOV"), FieldType('D'), FieldDescription("���� ��������� �������")]
        public DateTime Nextpov
        {
            get { return nextpov; }
            set {  nextpov = value; }
        }

        private string prim_;
        // <summary>
        // PRIM_ C(100)
        // </summary>
        [FieldName("PRIM_"), FieldType('C'), FieldWidth(100), FieldDescription("����������")]
        public string Prim_
        {
            get { return prim_; }
            set { CheckStringData("Prim_", value, 100); prim_ = value; }
        }

        private DateTime deactdate;
        // <summary>
        // DEACTDATE D
        // </summary>
        [FieldName("DEACTDATE"), FieldType('D'), FieldDescription("���� ������")]
        public DateTime Deactdate
        {
            get { return deactdate; }
            set {  deactdate = value; }
        }

        private string tag;
        // <summary>
        // TAG C(20)
        // </summary>
        [FieldName("TAG"), FieldType('C'), FieldWidth(30)]
        public string Tag
        {
            get { return tag; }
            set { CheckStringData("Tag", value, 30); tag = value; }
        }

        private string name;
        // <summary>
        // NAME C(20)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Counterid = ADataRow["COUNTERID"].ToString();
            Lshet = ADataRow["LSHET"].ToString();
            Cnttype = Convert.ToInt64(ADataRow["CNTTYPE"]);
            Cntname = ADataRow["CNTNAME"].ToString();
            Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]);
            Serialnum = ADataRow["SERIALNUM"].ToString();
            Setupplace = (CounterSetupPlace)Convert.ToInt64(ADataRow["SETUPPLACE"]);
            Place = ADataRow["PLACE"].ToString();
            Plombdate = Convert.ToDateTime(ADataRow["PLOMBDATE"]);
            Plombname = ADataRow["PLOMBNAME"].ToString();
            Lastpov = Convert.ToDateTime(ADataRow["LASTPOV"]);
            Nextpov = Convert.ToDateTime(ADataRow["NEXTPOV"]);
            Prim_ = ADataRow["PRIM_"].ToString();
            Deactdate = Convert.ToDateTime(ADataRow["DEACTDATE"]);
            Tag = ADataRow["TAG"].ToString();
            Name = ADataRow["NAME"].ToString();
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO counters (COUNTERID, LSHET, CNTTYPE, CNTNAME, SETUPDATE, SERIALNUM, SETUPPLACE, PLACE, PLOMBDATE, PLOMBNAME, LASTPOV, NEXTPOV, PRIM_, DEACTDATE, TAG, NAME) VALUES ('{0}', '{1}', {2}, '{3}', CTOD('{4}'), '{5}', {6}, '{7}', CTOD('{8}'), '{9}', CTOD('{10}'), CTOD('{11}'), '{12}', CTOD('{13}'), '{14}', '{15}')", String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Cnttype.ToString(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), Convert.ToInt32(Setupplace).ToString(), String.IsNullOrEmpty(Place) ? "" : Place.Trim(), Plombdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Plombdate.Month, Plombdate.Day, Plombdate.Year), String.IsNullOrEmpty(Plombname) ? "" : Plombname.Trim(), Lastpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Lastpov.Month, Lastpov.Day, Lastpov.Year), Nextpov == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Nextpov.Month, Nextpov.Day, Nextpov.Year), String.IsNullOrEmpty(Prim_) ? "" : Prim_.Trim(), Deactdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Deactdate.Month, Deactdate.Day, Deactdate.Year), String.IsNullOrEmpty(Tag) ? "" : Tag.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim());
            return rs;
        }
    }
}

