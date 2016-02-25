// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _039_Iskra
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

        private Int64 indcd;
        // <summary>
        // INDCD N(6)
        // </summary>
        [FieldName("INDCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Indcd
        {
            get { return indcd; }
            set { CheckIntegerData("Indcd", value, 6); indcd = value; }
        }

        private Int64 parentcd;
        // <summary>
        // PARENTCD N(6)
        // </summary>
        [FieldName("PARENTCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Parentcd
        {
            get { return parentcd; }
            set { CheckIntegerData("Parentcd", value, 6); parentcd = value; }
        }

        private Int64 tarifcd;
        // <summary>
        // TARIFCD N(6)
        // </summary>
        [FieldName("TARIFCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Tarifcd
        {
            get { return tarifcd; }
            set { CheckIntegerData("Tarifcd", value, 6); tarifcd = value; }
        }

        private string tarifnm;
        // <summary>
        // TARIFNM C(25)
        // </summary>
        [FieldName("TARIFNM"), FieldType('C'), FieldWidth(25)]
        public string Tarifnm
        {
            get { return tarifnm; }
            set { CheckStringData("Tarifnm", value, 25); tarifnm = value; }
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
            if (ADataRow.Table.Columns.Contains("INDCD")) Indcd = Convert.ToInt64(ADataRow["INDCD"]); else Indcd = 0;
            if (ADataRow.Table.Columns.Contains("PARENTCD")) Parentcd = Convert.ToInt64(ADataRow["PARENTCD"]); else Parentcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFCD")) Tarifcd = Convert.ToInt64(ADataRow["TARIFCD"]); else Tarifcd = 0;
            if (ADataRow.Table.Columns.Contains("TARIFNM")) Tarifnm = ADataRow["TARIFNM"].ToString(); else Tarifnm = "";
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Lshet = this.Lshet;
            retValue.Indcd = this.Indcd;
            retValue.Parentcd = this.Parentcd;
            retValue.Tarifcd = this.Tarifcd;
            retValue.Tarifnm = this.Tarifnm;
            retValue.Indication = this.Indication;
            retValue.Inddate = this.Inddate;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (LSHET, INDCD, PARENTCD, TARIFCD, TARIFNM, INDICATION, INDDATE) VALUES ('{0}', {1}, {2}, {3}, '{4}', {5}, CTOD('{6}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Indcd.ToString(), Parentcd.ToString(), Tarifcd.ToString(), String.IsNullOrEmpty(Tarifnm) ? "" : Tarifnm.Trim(), Indication.ToString().Replace(',', '.'), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year));
            return rs;
        }
    }
}