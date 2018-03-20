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
    [TableName("ABNPHONE.DBF")]
    public partial class AbnphoneRecord : AbstractRecord
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

        private string nomer;
        // <summary>
        // NOMER C(25)
        // </summary>
        [FieldName("NOMER"), FieldType('C'), FieldWidth(25)]
        public string Nomer
        {
            get { return nomer; }
            set { CheckStringData("Nomer", value, 25); nomer = value; }
        }

        private string type;
        // <summary>
        // TYPE C(15)
        // </summary>
        [FieldName("TYPE"), FieldType('C'), FieldWidth(15)]
        public string Type
        {
            get { return type; }
            set { CheckStringData("Type", value, 15); type = value; }
        }

        private string source;
        // <summary>
        // SOURCE C(30)
        // </summary>
        [FieldName("SOURCE"), FieldType('C'), FieldWidth(30)]
        public string Source
        {
            get { return source; }
            set { CheckStringData("Source", value, 30); source = value; }
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
            if (ADataRow.Table.Columns.Contains("NOMER")) Nomer = ADataRow["NOMER"].ToString(); else Nomer = "";
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("SOURCE")) Source = ADataRow["SOURCE"].ToString(); else Source = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            AbnphoneRecord retValue = new AbnphoneRecord();
            retValue.Lshet = this.Lshet;
            retValue.Nomer = this.Nomer;
            retValue.Type = this.Type;
            retValue.Source = this.Source;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABNPHONE (LSHET, NOMER, TYPE, SOURCE, ISDELETED) VALUES ('{0}', '{1}', '{2}', '{3}', {4})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Nomer) ? "" : Nomer.Trim(), String.IsNullOrEmpty(Type) ? "" : Type.Trim(), String.IsNullOrEmpty(Source) ? "" : Source.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}