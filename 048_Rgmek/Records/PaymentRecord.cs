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
    [TableName("PAYMENT.DBF")]
    public partial class PaymentRecord : AbstractRecord
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

        private string doc;
        // <summary>
        // DOC C(150)
        // </summary>
        [FieldName("DOC"), FieldType('C'), FieldWidth(150)]
        public string Doc
        {
            get { return doc; }
            set { CheckStringData("Doc", value, 150); doc = value; }
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

        private string paypostcd;
        // <summary>
        // PAYPOSTCD C(36)
        // </summary>
        [FieldName("PAYPOSTCD"), FieldType('C'), FieldWidth(36)]
        public string Paypostcd
        {
            get { return paypostcd; }
            set { CheckStringData("Paypostcd", value, 36); paypostcd = value; }
        }

        private string paypostnm;
        // <summary>
        // PAYPOSTNM C(50)
        // </summary>
        [FieldName("PAYPOSTNM"), FieldType('C'), FieldWidth(50)]
        public string Paypostnm
        {
            get { return paypostnm; }
            set { CheckStringData("Paypostnm", value, 50); paypostnm = value; }
        }

        private Int64 methodcd;
        // <summary>
        // METHODCD N(3)
        // </summary>
        [FieldName("METHODCD"), FieldType('N'), FieldWidth(3)]
        public Int64 Methodcd
        {
            get { return methodcd; }
            set { CheckIntegerData("Methodcd", value, 3); methodcd = value; }
        }

        private string methodnm;
        // <summary>
        // METHODNM C(50)
        // </summary>
        [FieldName("METHODNM"), FieldType('C'), FieldWidth(50)]
        public string Methodnm
        {
            get { return methodnm; }
            set { CheckStringData("Methodnm", value, 50); methodnm = value; }
        }

        private Int64 resourcd;
        // <summary>
        // RESOURCD N(5)
        // </summary>
        [FieldName("RESOURCD"), FieldType('N'), FieldWidth(5)]
        public Int64 Resourcd
        {
            get { return resourcd; }
            set { CheckIntegerData("Resourcd", value, 5); resourcd = value; }
        }

        private string resournm;
        // <summary>
        // RESOURNM C(48)
        // </summary>
        [FieldName("RESOURNM"), FieldType('C'), FieldWidth(48)]
        public string Resournm
        {
            get { return resournm; }
            set { CheckStringData("Resournm", value, 48); resournm = value; }
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

        private string opertype;
        // <summary>
        // OPERTYPE C(30)
        // </summary>
        [FieldName("OPERTYPE"), FieldType('C'), FieldWidth(30)]
        public string Opertype
        {
            get { return opertype; }
            set { CheckStringData("Opertype", value, 30); opertype = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("ACTIVCD")) Activcd = ADataRow["ACTIVCD"].ToString(); else Activcd = "";
            if (ADataRow.Table.Columns.Contains("ACTIVNM")) Activnm = ADataRow["ACTIVNM"].ToString(); else Activnm = "";
            if (ADataRow.Table.Columns.Contains("PAYPOSTCD")) Paypostcd = ADataRow["PAYPOSTCD"].ToString(); else Paypostcd = "";
            if (ADataRow.Table.Columns.Contains("PAYPOSTNM")) Paypostnm = ADataRow["PAYPOSTNM"].ToString(); else Paypostnm = "";
            if (ADataRow.Table.Columns.Contains("METHODCD")) Methodcd = Convert.ToInt64(ADataRow["METHODCD"]); else Methodcd = 0;
            if (ADataRow.Table.Columns.Contains("METHODNM")) Methodnm = ADataRow["METHODNM"].ToString(); else Methodnm = "";
            if (ADataRow.Table.Columns.Contains("RESOURCD")) Resourcd = Convert.ToInt64(ADataRow["RESOURCD"]); else Resourcd = 0;
            if (ADataRow.Table.Columns.Contains("RESOURNM")) Resournm = ADataRow["RESOURNM"].ToString(); else Resournm = "";
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("OPERTYPE")) Opertype = ADataRow["OPERTYPE"].ToString(); else Opertype = "";
        }

        public override AbstractRecord Clone()
        {
            PaymentRecord retValue = new PaymentRecord();
            retValue.Date = this.Date;
            retValue.Doc = this.Doc;
            retValue.Lshet = this.Lshet;
            retValue.Activcd = this.Activcd;
            retValue.Activnm = this.Activnm;
            retValue.Paypostcd = this.Paypostcd;
            retValue.Paypostnm = this.Paypostnm;
            retValue.Methodcd = this.Methodcd;
            retValue.Methodnm = this.Methodnm;
            retValue.Resourcd = this.Resourcd;
            retValue.Resournm = this.Resournm;
            retValue.Summa = this.Summa;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Opertype = this.Opertype;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PAYMENT (DATE, DOC, LSHET, ACTIVCD, ACTIVNM, PAYPOSTCD, PAYPOSTNM, METHODCD, METHODNM, RESOURCD, RESOURNM, SUMMA, SERVICECD, SERVICENM, OPERTYPE) VALUES (CTOD('{0}'), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', {9}, '{10}', {11}, '{12}', '{13}', '{14}')", Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Activcd) ? "" : Activcd.Trim(), String.IsNullOrEmpty(Activnm) ? "" : Activnm.Trim(), String.IsNullOrEmpty(Paypostcd) ? "" : Paypostcd.Trim(), String.IsNullOrEmpty(Paypostnm) ? "" : Paypostnm.Trim(), Methodcd.ToString(), String.IsNullOrEmpty(Methodnm) ? "" : Methodnm.Trim(), Resourcd.ToString(), String.IsNullOrEmpty(Resournm) ? "" : Resournm.Trim(), Summa.ToString().Replace(',', '.'), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), String.IsNullOrEmpty(Opertype) ? "" : Opertype.Trim());
            return rs;
        }
    }
}