// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _045_SpasskStroyDetal
{
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
        private string grkod;
        // <summary>
        // GRKOD C(8)
        // </summary>
        [FieldName("GRKOD"), FieldType('C'), FieldWidth(8)]
        public string Grkod
        {
            get { return grkod; }
            set { CheckStringData("Grkod", value, 8); grkod = value; }
        }

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

        private Int64 counterid;
        // <summary>
        // COUNTERID N(6)
        // </summary>
        [FieldName("COUNTERID"), FieldType('N'), FieldWidth(6)]
        public Int64 Counterid
        {
            get { return counterid; }
            set { CheckIntegerData("Counterid", value, 6); counterid = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(6)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(6)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 6); servicecd = value; }
        }

        private string servicenm;
        // <summary>
        // SERVICENM C(50)
        // </summary>
        [FieldName("SERVICENM"), FieldType('C'), FieldWidth(50)]
        public string Servicenm
        {
            get { return servicenm; }
            set { CheckStringData("Servicenm", value, 50); servicenm = value; }
        }

        private decimal count;
        // <summary>
        // COUNT N(14,4)
        // </summary>
        [FieldName("COUNT"), FieldType('N'), FieldWidth(14), FieldDec(4)]
        public decimal Count
        {
            get { return count; }
            set { CheckDecimalData("Count", value, 14, 4); count = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(14,4)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(14), FieldDec(4)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 14, 4); indication = value; }
        }

        private DateTime inddate;
        // <summary>
        // INDDATE D(8)
        // </summary>
        [FieldName("INDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Inddate
        {
            get { return inddate; }
            set { inddate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("GRKOD")) Grkod = ADataRow["GRKOD"].ToString(); else Grkod = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = Convert.ToInt64(ADataRow["COUNTERID"]); else Counterid = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]); else Servicecd = 0;
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("COUNT")) Count = Convert.ToDecimal(ADataRow["COUNT"]); else Count = 0;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Grkod = this.Grkod;
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Count = this.Count;
            retValue.Indication = this.Indication;
            retValue.Inddate = this.Inddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (GRKOD, LSHET, COUNTERID, SERVICECD, SERVICENM, COUNT, INDICATION, INDDATE) VALUES ('{0}', '{1}', {2}, {3}, '{4}', {5}, {6}, CTOD('{7}'))", String.IsNullOrEmpty(Grkod) ? "" : Grkod.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Counterid.ToString(), Servicecd.ToString(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Count.ToString().Replace(',', '.'), Indication.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year));
            return rs;
        }
    }
}