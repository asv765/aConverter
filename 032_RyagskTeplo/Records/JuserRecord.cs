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
    [TableName("JUSER.DBF")]
    public partial class JuserRecord: AbstractRecord
    {
        private string type;
        // <summary>
        // TYPE C(12)
        // </summary>
        [FieldName("TYPE"), FieldType('C'), FieldWidth(12)]
        public string Type
        {
            get { return type; }
            set { CheckStringData("Type", value, 12); type = value; }
        }

        private string id;
        // <summary>
        // ID C(12)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(12)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 12); id = value; }
        }

        private string name;
        // <summary>
        // NAME C(24)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(24)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 24); name = value; }
        }

        private bool _readonly;
        // <summary>
        // READONLY L(1)
        // </summary>
        [FieldName("READONLY"), FieldType('L'), FieldWidth(1)]
        public bool Readonly
        {
            get { return _readonly; }
            set {  _readonly = value; }
        }

        private Int64 ckval;
        // <summary>
        // CKVAL N(7)
        // </summary>
        [FieldName("CKVAL"), FieldType('N'), FieldWidth(7)]
        public Int64 Ckval
        {
            get { return ckval; }
            set { CheckIntegerData("Ckval", value, 7); ckval = value; }
        }

        private string data;
        // <summary>
        // DATA M
        // </summary>
        [FieldName("DATA"), FieldType('M')]
        public string Data
        {
            get { return data; }
            set { CheckStringData("Data", value, 0); data = value; }
        }

        private DateTime updated;
        // <summary>
        // UPDATED D(8)
        // </summary>
        [FieldName("UPDATED"), FieldType('D'), FieldWidth(8)]
        public DateTime Updated
        {
            get { return updated; }
            set {  updated = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("READONLY")) Readonly = ADataRow["READONLY"].ToString() == "True" ? true : false; else Readonly = false;
            if (ADataRow.Table.Columns.Contains("CKVAL")) Ckval = Convert.ToInt64(ADataRow["CKVAL"]); else Ckval = 0;
            if (ADataRow.Table.Columns.Contains("DATA")) Data = ADataRow["DATA"].ToString(); else Data = "";
            if (ADataRow.Table.Columns.Contains("UPDATED")) Updated = Convert.ToDateTime(ADataRow["UPDATED"]); else Updated = DateTime.MinValue;
        }
        
        public override AbstractRecord Clone()
        {
            JuserRecord retValue = new JuserRecord();
            retValue.Type = this.Type;
            retValue.Id = this.Id;
            retValue.Name = this.Name;
            retValue.Readonly = this.Readonly;
            retValue.Ckval = this.Ckval;
            retValue.Data = this.Data;
            retValue.Updated = this.Updated;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO JUSER (TYPE, ID, NAME, READONLY, CKVAL, DATA, UPDATED) VALUES ('{0}', '{1}', '{2}', {3}, {4}, '{5}', CTOD('{6}'))", String.IsNullOrEmpty(Type) ? "" : Type.Trim(), String.IsNullOrEmpty(Id) ? "" : Id.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), (Readonly ? 0 : 1 ), Ckval.ToString(), String.IsNullOrEmpty(Data) ? "" : Data.Trim(), Updated == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Updated.Month, Updated.Day, Updated.Year));
            return rs;
        }
    }
}
