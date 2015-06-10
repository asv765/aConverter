// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _032_RyagskTeplo
{
    [TableName("PS.DBF")]
    public partial class PsRecord: AbstractRecord
    {
        private string id1;
        // <summary>
        // ID1 C(1)
        // </summary>
        [FieldName("ID1"), FieldType('C'), FieldWidth(1)]
        public string Id1
        {
            get { return id1; }
            set { CheckStringData("Id1", value, 1); id1 = value; }
        }

        private Int64 id2;
        // <summary>
        // ID2 N(6)
        // </summary>
        [FieldName("ID2"), FieldType('N'), FieldWidth(6)]
        public Int64 Id2
        {
            get { return id2; }
            set { CheckIntegerData("Id2", value, 6); id2 = value; }
        }

        private Int64 tn;
        // <summary>
        // TN N(7)
        // </summary>
        [FieldName("TN"), FieldType('N'), FieldWidth(7)]
        public Int64 Tn
        {
            get { return tn; }
            set { CheckIntegerData("Tn", value, 7); tn = value; }
        }

        private string parms;
        // <summary>
        // PARMS M
        // </summary>
        [FieldName("PARMS"), FieldType('M')]
        public string Parms
        {
            get { return parms; }
            set { CheckStringData("Parms", value, 0); parms = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID1")) Id1 = ADataRow["ID1"].ToString(); else Id1 = "";
            if (ADataRow.Table.Columns.Contains("ID2")) Id2 = Convert.ToInt64(ADataRow["ID2"]); else Id2 = 0;
            if (ADataRow.Table.Columns.Contains("TN")) Tn = Convert.ToInt64(ADataRow["TN"]); else Tn = 0;
            if (ADataRow.Table.Columns.Contains("PARMS")) Parms = ADataRow["PARMS"].ToString(); else Parms = "";
        }
        
        public override AbstractRecord Clone()
        {
            PsRecord retValue = new PsRecord();
            retValue.Id1 = this.Id1;
            retValue.Id2 = this.Id2;
            retValue.Tn = this.Tn;
            retValue.Parms = this.Parms;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PS (ID1, ID2, TN, PARMS) VALUES ('{0}', {1}, {2}, '{3}')", String.IsNullOrEmpty(Id1) ? "" : Id1.Trim(), Id2.ToString(), Tn.ToString(), String.IsNullOrEmpty(Parms) ? "" : Parms.Trim());
            return rs;
        }
    }
}
