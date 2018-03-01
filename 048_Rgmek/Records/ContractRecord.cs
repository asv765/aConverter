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
    [TableName("CONTRACT.DBF")]
    public partial class ContractRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(19)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(19)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 19); lshet = value; }
        }

        private string contractcd;
        // <summary>
        // CONTRACTCD C(36)
        // </summary>
        [FieldName("CONTRACTCD"), FieldType('C'), FieldWidth(36)]
        public string Contractcd
        {
            get { return contractcd; }
            set { CheckStringData("Contractcd", value, 36); contractcd = value; }
        }

        private string nomer;
        // <summary>
        // NOMER C(15)
        // </summary>
        [FieldName("NOMER"), FieldType('C'), FieldWidth(15)]
        public string Nomer
        {
            get { return nomer; }
            set { CheckStringData("Nomer", value, 15); nomer = value; }
        }

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

        private string abncd;
        // <summary>
        // ABNCD C(36)
        // </summary>
        [FieldName("ABNCD"), FieldType('C'), FieldWidth(36)]
        public string Abncd
        {
            get { return abncd; }
            set { CheckStringData("Abncd", value, 36); abncd = value; }
        }

        private string abnnm;
        // <summary>
        // ABNNM C(80)
        // </summary>
        [FieldName("ABNNM"), FieldType('C'), FieldWidth(80)]
        public string Abnnm
        {
            get { return abnnm; }
            set { CheckStringData("Abnnm", value, 80); abnnm = value; }
        }

        private DateTime begdate;
        // <summary>
        // BEGDATE D(8)
        // </summary>
        [FieldName("BEGDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Begdate
        {
            get { return begdate; }
            set { begdate = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D(8)
        // </summary>
        [FieldName("ENDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        private Int64 isauto;
        // <summary>
        // ISAUTO N(2)
        // </summary>
        [FieldName("ISAUTO"), FieldType('N'), FieldWidth(2)]
        public Int64 Isauto
        {
            get { return isauto; }
            set { CheckIntegerData("Isauto", value, 2); isauto = value; }
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

        private Int64 isdeleted;
        // <summary>
        // ISDELETED N(2)
        // </summary>
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(2)]
        public Int64 Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 2); isdeleted = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("CONTRACTCD")) Contractcd = ADataRow["CONTRACTCD"].ToString(); else Contractcd = "";
            if (ADataRow.Table.Columns.Contains("NOMER")) Nomer = ADataRow["NOMER"].ToString(); else Nomer = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ABNCD")) Abncd = ADataRow["ABNCD"].ToString(); else Abncd = "";
            if (ADataRow.Table.Columns.Contains("ABNNM")) Abnnm = ADataRow["ABNNM"].ToString(); else Abnnm = "";
            if (ADataRow.Table.Columns.Contains("BEGDATE")) Begdate = Convert.ToDateTime(ADataRow["BEGDATE"]); else Begdate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ENDDATE")) Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]); else Enddate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ISAUTO")) Isauto = Convert.ToInt64(ADataRow["ISAUTO"]); else Isauto = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            ContractRecord retValue = new ContractRecord();
            retValue.Lshet = this.Lshet;
            retValue.Contractcd = this.Contractcd;
            retValue.Nomer = this.Nomer;
            retValue.Date = this.Date;
            retValue.Abncd = this.Abncd;
            retValue.Abnnm = this.Abnnm;
            retValue.Begdate = this.Begdate;
            retValue.Enddate = this.Enddate;
            retValue.Isauto = this.Isauto;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CONTRACT (LSHET, CONTRACTCD, NOMER, DATE, ABNCD, ABNNM, BEGDATE, ENDDATE, ISAUTO, SERVICECD, SERVICENM, ISDELETED) VALUES ('{0}', '{1}', '{2}', CTOD('{3}'), '{4}', '{5}', CTOD('{6}'), CTOD('{7}'), {8}, '{9}', '{10}', {11})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Contractcd) ? "" : Contractcd.Trim(), String.IsNullOrEmpty(Nomer) ? "" : Nomer.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Abncd) ? "" : Abncd.Trim(), String.IsNullOrEmpty(Abnnm) ? "" : Abnnm.Trim(), Begdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Begdate.Month, Begdate.Day, Begdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year), Isauto.ToString(), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}