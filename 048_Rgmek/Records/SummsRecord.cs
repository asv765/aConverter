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
    [TableName("SUMMS.DBF")]
    public partial class SummsRecord : AbstractRecord
    {
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

        private string vid_dvig;
        // <summary>
        // VID_DVIG C(6)
        // </summary>
        [FieldName("VID_DVIG"), FieldType('C'), FieldWidth(6)]
        public string Vid_dvig
        {
            get { return vid_dvig; }
            set { CheckStringData("Vid_dvig", value, 6); vid_dvig = value; }
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

        private DateTime date_rasch;
        // <summary>
        // DATE_RASCH D(8)
        // </summary>
        [FieldName("DATE_RASCH"), FieldType('D'), FieldWidth(8)]
        public DateTime Date_rasch
        {
            get { return date_rasch; }
            set { date_rasch = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("VID_DVIG")) Vid_dvig = ADataRow["VID_DVIG"].ToString(); else Vid_dvig = "";
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("ACTIVCD")) Activcd = ADataRow["ACTIVCD"].ToString(); else Activcd = "";
            if (ADataRow.Table.Columns.Contains("ACTIVNM")) Activnm = ADataRow["ACTIVNM"].ToString(); else Activnm = "";
            if (ADataRow.Table.Columns.Contains("SERVICECD")) Servicecd = ADataRow["SERVICECD"].ToString(); else Servicecd = "";
            if (ADataRow.Table.Columns.Contains("SERVICENM")) Servicenm = ADataRow["SERVICENM"].ToString(); else Servicenm = "";
            if (ADataRow.Table.Columns.Contains("DATE_DEIST")) Date_deist = Convert.ToDateTime(ADataRow["DATE_DEIST"]); else Date_deist = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("SUMMA")) Summa = Convert.ToDecimal(ADataRow["SUMMA"]); else Summa = 0;
            if (ADataRow.Table.Columns.Contains("OPERTYPE")) Opertype = ADataRow["OPERTYPE"].ToString(); else Opertype = "";
            if (ADataRow.Table.Columns.Contains("DATE_RASCH")) Date_rasch = Convert.ToDateTime(ADataRow["DATE_RASCH"]); else Date_rasch = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            SummsRecord retValue = new SummsRecord();
            retValue.Doc = this.Doc;
            retValue.Date = this.Date;
            retValue.Vid_dvig = this.Vid_dvig;
            retValue.Lshet = this.Lshet;
            retValue.Activcd = this.Activcd;
            retValue.Activnm = this.Activnm;
            retValue.Servicecd = this.Servicecd;
            retValue.Servicenm = this.Servicenm;
            retValue.Date_deist = this.Date_deist;
            retValue.Summa = this.Summa;
            retValue.Opertype = this.Opertype;
            retValue.Date_rasch = this.Date_rasch;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SUMMS (DOC, DATE, VID_DVIG, LSHET, ACTIVCD, ACTIVNM, SERVICECD, SERVICENM, DATE_DEIST, SUMMA, OPERTYPE, DATE_RASCH) VALUES ('{0}', CTOD('{1}'), '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', CTOD('{8}'), {9}, '{10}', CTOD('{11}'))", String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Vid_dvig) ? "" : Vid_dvig.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Activcd) ? "" : Activcd.Trim(), String.IsNullOrEmpty(Activnm) ? "" : Activnm.Trim(), String.IsNullOrEmpty(Servicecd) ? "" : Servicecd.Trim(), String.IsNullOrEmpty(Servicenm) ? "" : Servicenm.Trim(), Date_deist == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_deist.Month, Date_deist.Day, Date_deist.Year), Summa.ToString().Replace(',', '.'), String.IsNullOrEmpty(Opertype) ? "" : Opertype.Trim(), Date_rasch == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date_rasch.Month, Date_rasch.Day, Date_rasch.Year));
            return rs;
        }
    }
}