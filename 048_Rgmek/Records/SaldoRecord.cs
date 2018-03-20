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
    [TableName("SALDO.DBF")]
    public partial class SaldoRecord : AbstractRecord
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

        private string activcd;
        // <summary>
        // ACTIVCD C(2)
        // </summary>
        [FieldName("ACTIVCD"), FieldType('C'), FieldWidth(2)]
        public string Activcd
        {
            get { return activcd; }
            set { CheckStringData("Activcd", value, 2); activcd = value; }
        }

        private string activnm;
        // <summary>
        // ACTIVNM C(25)
        // </summary>
        [FieldName("ACTIVNM"), FieldType('C'), FieldWidth(25)]
        public string Activnm
        {
            get { return activnm; }
            set { CheckStringData("Activnm", value, 25); activnm = value; }
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

        private DateTime date_deist;
        // <summary>
        // DATE_DEIST D(8)
        // </summary>
        [FieldName("DATE_DEIST"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_deist
        {
            get { return date_deist; }
            set { date_deist = value; }
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
            if (ADataRow.Table.Columns.Contains("ACTIVCD")) Activcd = ADataRow["ACTIVCD"].ToString(); else Activcd = "";
            if (ADataRow.Table.Columns.Contains("ACTIVNM")) Activnm = ADataRow["ACTIVNM"].ToString(); else Activnm = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("DATE_DEIST")) Date_deist = Convert.ToDateTime(ADataRow["DATE_DEIST"]); else Date_deist = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
        }

        public override AbstractRecord Clone()
        {
            SaldoRecord retValue = new SaldoRecord();
            retValue.Date = this.Date;
            retValue.Lshet = this.Lshet;
            retValue.Activcd = this.Activcd;
            retValue.Activnm = this.Activnm;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Date_deist = this.Date_deist;
            retValue.Summa = this.Summa;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SALDO (DATE, LSHET, ACTIVCD, ACTIVNM, SERVICECD, SERVICENM, DATE_DEIST, SUMMA) VALUES (CTOD('{0}'), '{1}', '{2}', '{3}', '{4}', '{5}', CTOD('{6}'), {7})", Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Activcd) ? "" : Activcd.Trim(), String.IsNullOrEmpty(Activnm) ? "" : Activnm.Trim(), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Date_deist == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_deist.Month, Date_deist.Day, Date_deist.Year), Summa.ToString().Replace(',', '.'));
            return rs;
        }
    }
}