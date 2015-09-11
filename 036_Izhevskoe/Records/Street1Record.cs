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
    [TableName("STREET1.DBF")]
    public partial class Street1Record: AbstractRecord
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

        private string nasp;
        // <summary>
        // NASP C(90)
        // </summary>
        [FieldName("NASP"), FieldType('C'), FieldWidth(90)]
        public string Nasp
        {
            get { return nasp; }
            set { CheckStringData("Nasp", value, 90); nasp = value; }
        }

        private string posel;
        // <summary>
        // POSEL C(30)
        // </summary>
        [FieldName("POSEL"), FieldType('C'), FieldWidth(30)]
        public string Posel
        {
            get { return posel; }
            set { CheckStringData("Posel", value, 30); posel = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("KST")) Kst = Convert.ToInt64(ADataRow["KST"]); else Kst = 0;
            if (ADataRow.Table.Columns.Contains("STREET")) Street = ADataRow["STREET"].ToString(); else Street = "";
            if (ADataRow.Table.Columns.Contains("NASP")) Nasp = ADataRow["NASP"].ToString(); else Nasp = "";
            if (ADataRow.Table.Columns.Contains("POSEL")) Posel = ADataRow["POSEL"].ToString(); else Posel = "";
        }
        
        public override AbstractRecord Clone()
        {
            Street1Record retValue = new Street1Record();
            retValue.Kst = this.Kst;
            retValue.Street = this.Street;
            retValue.Nasp = this.Nasp;
            retValue.Posel = this.Posel;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO STREET1 (KST, STREET, NASP, POSEL) VALUES ({0}, '{1}', '{2}', '{3}')", Kst.ToString(), String.IsNullOrEmpty(Street) ? "" : Street.Trim(), String.IsNullOrEmpty(Nasp) ? "" : Nasp.Trim(), String.IsNullOrEmpty(Posel) ? "" : Posel.Trim());
            return rs;
        }
    }
}
	