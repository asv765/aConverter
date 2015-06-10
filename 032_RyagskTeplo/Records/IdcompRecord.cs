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
    [TableName("IDCOMP.DBF")]
    public partial class IdcompRecord: AbstractRecord
    {
        private Int64 id;
        // <summary>
        // ID N(6)
        // </summary>
        [FieldName("ID"), FieldType('N'), FieldWidth(6)]
        public Int64 Id
        {
            get { return id; }
            set { CheckIntegerData("Id", value, 6); id = value; }
        }

        private string computer;
        // <summary>
        // COMPUTER C(50)
        // </summary>
        [FieldName("COMPUTER"), FieldType('C'), FieldWidth(50)]
        public string Computer
        {
            get { return computer; }
            set { CheckStringData("Computer", value, 50); computer = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        private string number;
        // <summary>
        // NUMBER C(10)
        // </summary>
        [FieldName("NUMBER"), FieldType('C'), FieldWidth(10)]
        public string Number
        {
            get { return number; }
            set { CheckStringData("Number", value, 10); number = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = Convert.ToInt64(ADataRow["ID"]); else Id = 0;
            if (ADataRow.Table.Columns.Contains("COMPUTER")) Computer = ADataRow["COMPUTER"].ToString(); else Computer = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("NUMBER")) Number = ADataRow["NUMBER"].ToString(); else Number = "";
        }
        
        public override AbstractRecord Clone()
        {
            IdcompRecord retValue = new IdcompRecord();
            retValue.Id = this.Id;
            retValue.Computer = this.Computer;
            retValue.Date = this.Date;
            retValue.Number = this.Number;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO IDCOMP (ID, COMPUTER, DATE, NUMBER) VALUES ({0}, '{1}', CTOD('{2}'), '{3}')", Id.ToString(), String.IsNullOrEmpty(Computer) ? "" : Computer.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Number) ? "" : Number.Trim());
            return rs;
        }
    }
}
