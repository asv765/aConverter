// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _036_Izhevskoe
{
    [TableName("STREET.DBF")]
    public partial class StreetRecord: AbstractRecord
    {
        private Int64 kst;
        // <summary>
        // KST N(4)
        // </summary>
        [FieldName("KST"), FieldType('N'), FieldWidth(4)]
        public Int64 Kst
        {
            get { return kst; }
            set { CheckIntegerData("Kst", value, 4); kst = value; }
        }

        private string street;
        // <summary>
        // STREET C(18)
        // </summary>
        [FieldName("STREET"), FieldType('C'), FieldWidth(18)]
        public string Street
        {
            get { return street; }
            set { CheckStringData("Street", value, 18); street = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("KST")) Kst = Convert.ToInt64(ADataRow["KST"]); else Kst = 0;
            if (ADataRow.Table.Columns.Contains("STREET")) Street = ADataRow["STREET"].ToString(); else Street = "";
        }
        
        public override AbstractRecord Clone()
        {
            StreetRecord retValue = new StreetRecord();
            retValue.Kst = this.Kst;
            retValue.Street = this.Street;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO STREET (KST, STREET) VALUES ({0}, '{1}')", Kst.ToString(), String.IsNullOrEmpty(Street) ? "" : Street.Trim());
            return rs;
        }
    }
}
	