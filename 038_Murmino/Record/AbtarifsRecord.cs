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
    [TableName("ABTARIFS.DBF")]
    public partial class AbtarifsRecord : AbstractRecord
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

        private Int64 grtarifcd;
        // <summary>
        // GRTARIFCD N(6)
        // </summary>
        [FieldName("GRTARIFCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Grtarifcd
        {
            get { return grtarifcd; }
            set { CheckIntegerData("Grtarifcd", value, 6); grtarifcd = value; }
        }

        private string grtarifnm;
        // <summary>
        // GRTARIFNM C(50)
        // </summary>
        [FieldName("GRTARIFNM"), FieldType('C'), FieldWidth(50)]
        public string Grtarifnm
        {
            get { return grtarifnm; }
            set { CheckStringData("Grtarifnm", value, 50); grtarifnm = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("GRTARIFCD")) Grtarifcd = Convert.ToInt64(ADataRow["GRTARIFCD"]); else Grtarifcd = 0;
            if (ADataRow.Table.Columns.Contains("GRTARIFNM")) Grtarifnm = ADataRow["GRTARIFNM"].ToString(); else Grtarifnm = "";
        }

        public override AbstractRecord Clone()
        {
            AbtarifsRecord retValue = new AbtarifsRecord();
            retValue.Lshet = this.Lshet;
            retValue.Grtarifcd = this.Grtarifcd;
            retValue.Grtarifnm = this.Grtarifnm;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABTARIFS (LSHET, GRTARIFCD, GRTARIFNM) VALUES ('{0}', {1}, '{2}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Grtarifcd.ToString(), String.IsNullOrEmpty(Grtarifnm) ? "" : Grtarifnm.Trim());
            return rs;
        }
    }
}