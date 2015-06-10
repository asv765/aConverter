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
    [TableName("INFO.DBF")]
    public partial class InfoRecord: AbstractRecord
    {
        private string id;
        // <summary>
        // ID C(3)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(3)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 3); id = value; }
        }

        private string number;
        // <summary>
        // NUMBER C(20)
        // </summary>
        [FieldName("NUMBER"), FieldType('C'), FieldWidth(20)]
        public string Number
        {
            get { return number; }
            set { CheckStringData("Number", value, 20); number = value; }
        }

        private Int64 npp;
        // <summary>
        // NPP N(11)
        // </summary>
        [FieldName("NPP"), FieldType('N'), FieldWidth(11)]
        public Int64 Npp
        {
            get { return npp; }
            set { CheckIntegerData("Npp", value, 11); npp = value; }
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
        // MSG C(50)
        // </summary>
        [FieldName("MSG"), FieldType('C'), FieldWidth(50)]
        public string Msg
        {
            get { return msg; }
            set { CheckStringData("Msg", value, 50); msg = value; }
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
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NUMBER")) Number = ADataRow["NUMBER"].ToString(); else Number = "";
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("MSG")) Msg = ADataRow["MSG"].ToString(); else Msg = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
        }
        
        public override AbstractRecord Clone()
        {
            InfoRecord retValue = new InfoRecord();
            retValue.Id = this.Id;
            retValue.Number = this.Number;
            retValue.Npp = this.Npp;
            retValue.Dat = this.Dat;
            retValue.Msg = this.Msg;
            retValue.Info = this.Info;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO INFO (ID, NUMBER, NPP, DAT, MSG, INFO) VALUES ('{0}', '{1}', {2}, CTOD('{3}'), '{4}', '{5}')", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), String.IsNullOrEmpty(Number) ? "" : Number.Trim(), Npp.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), String.IsNullOrEmpty(Msg) ? "" : Msg.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim());
            return rs;
        }
    }
}
