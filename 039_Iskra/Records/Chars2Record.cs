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
    [TableName("CHARS2.DBF")]
    public partial class Chars2Record : AbstractRecord
    {
        private Int64 psectorcd;
        // <summary>
        // PSECTORCD N(6)
        // </summary>
        [FieldName("PSECTORCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Psectorcd
        {
            get { return psectorcd; }
            set { CheckIntegerData("Psectorcd", value, 6); psectorcd = value; }
        }

        private Int64 sectorcd;
        // <summary>
        // SECTORCD N(6)
        // </summary>
        [FieldName("SECTORCD"), FieldType('N'), FieldWidth(6)]
        public Int64 Sectorcd
        {
            get { return sectorcd; }
            set { CheckIntegerData("Sectorcd", value, 6); sectorcd = value; }
        }

        private string sectorname;
        // <summary>
        // SECTORNAME C(25)
        // </summary>
        [FieldName("SECTORNAME"), FieldType('C'), FieldWidth(25)]
        public string Sectorname
        {
            get { return sectorname; }
            set { CheckStringData("Sectorname", value, 25); sectorname = value; }
        }

        private decimal livearea;
        // <summary>
        // LIVEAREA N(10,2)
        // </summary>
        [FieldName("LIVEAREA"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Livearea
        {
            get { return livearea; }
            set { CheckDecimalData("Livearea", value, 10, 2); livearea = value; }
        }

        private decimal comearea;
        // <summary>
        // COMEAREA N(10,2)
        // </summary>
        [FieldName("COMEAREA"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Comearea
        {
            get { return comearea; }
            set { CheckDecimalData("Comearea", value, 10, 2); comearea = value; }
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
            if (ADataRow.Table.Columns.Contains("PSECTORCD")) Psectorcd = Convert.ToInt64(ADataRow["PSECTORCD"]); else Psectorcd = 0;
            if (ADataRow.Table.Columns.Contains("SECTORCD")) Sectorcd = Convert.ToInt64(ADataRow["SECTORCD"]); else Sectorcd = 0;
            if (ADataRow.Table.Columns.Contains("SECTORNAME")) Sectorname = ADataRow["SECTORNAME"].ToString(); else Sectorname = "";
            if (ADataRow.Table.Columns.Contains("LIVEAREA")) Livearea = Convert.ToDecimal(ADataRow["LIVEAREA"]); else Livearea = 0;
            if (ADataRow.Table.Columns.Contains("COMEAREA")) Comearea = Convert.ToDecimal(ADataRow["COMEAREA"]); else Comearea = 0;
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            Chars2Record retValue = new Chars2Record();
            retValue.Psectorcd = this.Psectorcd;
            retValue.Sectorcd = this.Sectorcd;
            retValue.Sectorname = this.Sectorname;
            retValue.Livearea = this.Livearea;
            retValue.Comearea = this.Comearea;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CHARS2 (PSECTORCD, SECTORCD, SECTORNAME, LIVEAREA, COMEAREA, ISDELETED) VALUES ({0}, {1}, '{2}', {3}, {4}, {5})", Psectorcd.ToString(), Sectorcd.ToString(), String.IsNullOrEmpty(Sectorname) ? "" : Sectorname.Trim(), Livearea.ToString().Replace(',', '.'), Comearea.ToString().Replace(',', '.'), Isdeleted.ToString());
            return rs;
        }
    }
}