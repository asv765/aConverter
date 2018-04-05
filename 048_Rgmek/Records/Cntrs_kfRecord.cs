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
    [TableName("CNTRS_KF.DBF")]
    public partial class Cntrs_kfRecord : AbstractRecord
    {
        private string housecd;
        // <summary>
        // HOUSECD C(36)
        // </summary>
        [FieldName("HOUSECD"), FieldType('C'), FieldWidth(36)]
        public string Housecd
        {
            get { return housecd; }
            set { CheckStringData("Housecd", value, 36); housecd = value; }
        }

        private string placecd;
        // <summary>
        // PLACECD C(36)
        // </summary>
        [FieldName("PLACECD"), FieldType('C'), FieldWidth(36)]
        public string Placecd
        {
            get { return placecd; }
            set { CheckStringData("Placecd", value, 36); placecd = value; }
        }

        private Int64 instplid;
        // <summary>
        // INSTPLID N(3)
        // </summary>
        [FieldName("INSTPLID"), FieldType('N'), FieldWidth(3)]
        public Int64 Instplid
        {
            get { return instplid; }
            set { CheckIntegerData("Instplid", value, 3); instplid = value; }
        }

        private string instplace;
        // <summary>
        // INSTPLACE C(100)
        // </summary>
        [FieldName("INSTPLACE"), FieldType('C'), FieldWidth(100)]
        public string Instplace
        {
            get { return instplace; }
            set { CheckStringData("Instplace", value, 100); instplace = value; }
        }

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

        private decimal koeff;
        // <summary>
        // KOEFF N(12,4)
        // </summary>
        [FieldName("KOEFF"), FieldType('N'), FieldWidth(12), FieldDec(4)]
        public decimal Koeff
        {
            get { return koeff; }
            set { CheckDecimalData("Koeff", value, 12, 4); koeff = value; }
        }

        private string notservcd;
        // <summary>
        // NOTSERVCD C(36)
        // </summary>
        [FieldName("NOTSERVCD"), FieldType('C'), FieldWidth(36)]
        public string Notservcd
        {
            get { return notservcd; }
            set { CheckStringData("Notservcd", value, 36); notservcd = value; }
        }

        private string notservnm;
        // <summary>
        // NOTSERVNM C(50)
        // </summary>
        [FieldName("NOTSERVNM"), FieldType('C'), FieldWidth(50)]
        public string Notservnm
        {
            get { return notservnm; }
            set { CheckStringData("Notservnm", value, 50); notservnm = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("HOUSECD")) Housecd = ADataRow["HOUSECD"].ToString(); else Housecd = "";
            if (ADataRow.Table.Columns.Contains("PLACECD")) Placecd = ADataRow["PLACECD"].ToString(); else Placecd = "";
            if (ADataRow.Table.Columns.Contains("INSTPLID")) Instplid = Convert.ToInt64(ADataRow["INSTPLID"]); else Instplid = 0;
            if (ADataRow.Table.Columns.Contains("INSTPLACE")) Instplace = ADataRow["INSTPLACE"].ToString(); else Instplace = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("KOEFF")) Koeff = Convert.ToDecimal(ADataRow["KOEFF"]); else Koeff = 0;
            if (ADataRow.Table.Columns.Contains("NOTSERVCD")) Notservcd = ADataRow["NOTSERVCD"].ToString(); else Notservcd = "";
            if (ADataRow.Table.Columns.Contains("NOTSERVNM")) Notservnm = ADataRow["NOTSERVNM"].ToString(); else Notservnm = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            Cntrs_kfRecord retValue = new Cntrs_kfRecord();
            retValue.Housecd = this.Housecd;
            retValue.Placecd = this.Placecd;
            retValue.Instplid = this.Instplid;
            retValue.Instplace = this.Instplace;
            retValue.Lshet = this.Lshet;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Koeff = this.Koeff;
            retValue.Notservcd = this.Notservcd;
            retValue.Notservnm = this.Notservnm;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRS_KF (HOUSECD, PLACECD, INSTPLID, INSTPLACE, LSHET, SERVICECD, SERVICENM, KOEFF, NOTSERVCD, NOTSERVNM, DATE) VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}', CTOD('{10}'))", String.IsNullOrEmpty(Housecd) ? "" : Housecd.Trim(), String.IsNullOrEmpty(Placecd) ? "" : Placecd.Trim(), Instplid.ToString(), String.IsNullOrEmpty(Instplace) ? "" : Instplace.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Koeff.ToString().Replace(',', '.'), String.IsNullOrEmpty(Notservcd) ? "" : Notservcd.Trim(), String.IsNullOrEmpty(Notservnm) ? "" : Notservnm.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}