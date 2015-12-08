// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _038_Murmino
{
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
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
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("COUNTERID")) Counterid = ADataRow["COUNTERID"].ToString(); else Counterid = "";
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Lshet = this.Lshet;
            retValue.Counterid = this.Counterid;
            retValue.Indication = this.Indication;
            retValue.Inddate = this.Inddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (LSHET, COUNTERID, INDICATION, INDDATE) VALUES ('{0}', '{1}', {2}, CTOD('{3}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), Indication.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year));
            return rs;
        }
    }
}