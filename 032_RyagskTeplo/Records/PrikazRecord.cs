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
    [TableName("PRIKAZ.DBF")]
    public partial class PrikazRecord: AbstractRecord
    {
        private Int64 number;
        // <summary>
        // NUMBER N(11)
        // </summary>
        [FieldName("NUMBER"), FieldType('N'), FieldWidth(11)]
        public Int64 Number
        {
            get { return number; }
            set { CheckIntegerData("Number", value, 11); number = value; }
        }

        private DateTime dat;
        // <summary>
        // DAT D(8)
        // </summary>
        [FieldName("DAT"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat
        {
            get { return dat; }
            set {  dat = value; }
        }

        private string msg;
        // <summary>
        // MSG C(100)
        // </summary>
        [FieldName("MSG"), FieldType('C'), FieldWidth(100)]
        public string Msg
        {
            get { return msg; }
            set { CheckStringData("Msg", value, 100); msg = value; }
        }

        private string info;
        // <summary>
        // INFO M
        // </summary>
        [FieldName("INFO"), FieldType('M')]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 0); info = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("NUMBER")) Number = Convert.ToInt64(ADataRow["NUMBER"]); else Number = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("MSG")) Msg = ADataRow["MSG"].ToString(); else Msg = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
        }
        
        public override AbstractRecord Clone()
        {
            PrikazRecord retValue = new PrikazRecord();
            retValue.Number = this.Number;
            retValue.Dat = this.Dat;
            retValue.Msg = this.Msg;
            retValue.Info = this.Info;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO PRIKAZ (NUMBER, DAT, MSG, INFO) VALUES ({0}, CTOD('{1}'), '{2}', '{3}')", Number.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), String.IsNullOrEmpty(Msg) ? "" : Msg.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim());
            return rs;
        }
    }
}
