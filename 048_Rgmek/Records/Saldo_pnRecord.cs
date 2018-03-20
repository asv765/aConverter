// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _048_Rgmek
{
    [TableName("SALDO_PN.DBF")]
    public partial class Saldo_pnRecord : AbstractRecord
    {
        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(36)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(36)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 36); lshet = value; }
        }

        private string dolgtype;
        // <summary>
        // DOLGTYPE C(30)
        // </summary>
        [FieldName("DOLGTYPE"), FieldType('C'), FieldWidth(30)]
        public string Dolgtype
        {
            get { return dolgtype; }
            set { CheckStringData("Dolgtype", value, 30); dolgtype = value; }
        }

        private string servicecd;
        // <summary>
        // SERVICECD C(36)
        // </summary>
        [FieldName("SERVICECD"), FieldType('C'), FieldWidth(36)]
        public string Servicecd
        {
            get { return servicecd; }
            set { CheckStringData("Servicecd", value, 36); servicecd = value; }
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

        private DateTime period;
        // <summary>
        // PERIOD D(8)
        // </summary>
        [FieldName("PERIOD"), FieldType('D'), FieldWidth(8)]
        public DateTime Period
        {
            get { return period; }
            set { period = value; }
        }

        private decimal summa;
        // <summary>
        // SUMMA N(15,2)
        // </summary>
        [FieldName("SUMMA"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Summa
        {
            get { return summa; }
            set { CheckDecimalData("Summa", value, 15, 2); summa = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("DOLGTYPE")) Dolgtype = ADataRow["DOLGTYPE"].ToString(); else Dolgtype = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("PERIOD")) Period = Convert.ToDateTime(ADataRow["PERIOD"]); else Period = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
        }

        public override AbstractRecord Clone()
        {
            Saldo_pnRecord retValue = new Saldo_pnRecord();
            retValue.Date = this.Date;
            retValue.Lshet = this.Lshet;
            retValue.Dolgtype = this.Dolgtype;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Period = this.Period;
            retValue.Summa = this.Summa;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SALDO_PN (DATE, LSHET, DOLGTYPE, SERVICECD, SERVICENM, PERIOD, SUMMA) VALUES (CTOD('{0}'), '{1}', '{2}', '{3}', '{4}', CTOD('{5}'), {6})", Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Dolgtype) ? "" : Dolgtype.Trim(), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Period == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Period.Month, Period.Day, Period.Year), Summa.ToString().Replace(',', '.'));
            return rs;
        }
    }
}