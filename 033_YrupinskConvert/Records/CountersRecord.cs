// Файл сгенерирован aConverter
using System;
using DbfClassLibrary;

namespace _033_Yrupinsk.Records
{
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(11)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(11)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 11); lshet = value; }
        }

        private Int64 kod;
        // <summary>
        // KOD N(11)
        // </summary>
        [FieldName("KOD"), FieldType('N'), FieldWidth(11)]
        public Int64 Kod
        {
            get { return kod; }
            set { CheckIntegerData("Kod", value, 11); kod = value; }
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
            set { setupdate = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(25)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(25)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 25); serialnum = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("KOD")) Kod = Convert.ToInt64(ADataRow["KOD"]); else Kod = 0;
            if (ADataRow.Table.Columns.Contains("CNTNAME")) Cntname = ADataRow["CNTNAME"].ToString(); else Cntname = "";
            if (ADataRow.Table.Columns.Contains("SETUPDATE")) Setupdate = Convert.ToDateTime(ADataRow["SETUPDATE"]); else Setupdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SERIALNUM")) Serialnum = ADataRow["SERIALNUM"].ToString(); else Serialnum = "";
        }

        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Lshet = this.Lshet;
            retValue.Kod = this.Kod;
            retValue.Cntname = this.Cntname;
            retValue.Setupdate = this.Setupdate;
            retValue.Serialnum = this.Serialnum;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LSHET, KOD, CNTNAME, SETUPDATE, SERIALNUM) VALUES ('{0}', {1}, '{2}', CTOD('{3}'), '{4}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Kod.ToString(), String.IsNullOrEmpty(Cntname) ? "" : Cntname.Trim(), Setupdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Setupdate.Month, Setupdate.Day, Setupdate.Year), String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim());
            return rs;
        }
    }
}
