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
    [TableName("SPRONE.DBF")]
    public partial class SproneRecord: AbstractRecord
    {
        private string id;
        // <summary>
        // ID C(20)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(20)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 20); id = value; }
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

        private string codc;
        // <summary>
        // CODC C(20)
        // </summary>
        [FieldName("CODC"), FieldType('C'), FieldWidth(20)]
        public string Codc
        {
            get { return codc; }
            set { CheckStringData("Codc", value, 20); codc = value; }
        }

        private string name;
        // <summary>
        // NAME C(100)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(100)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 100); name = value; }
        }

        private string namew;
        // <summary>
        // NAMEW C(100)
        // </summary>
        [FieldName("NAMEW"), FieldType('C'), FieldWidth(100)]
        public string Namew
        {
            get { return namew; }
            set { CheckStringData("Namew", value, 100); namew = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("CODC")) Codc = ADataRow["CODC"].ToString(); else Codc = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("NAMEW")) Namew = ADataRow["NAMEW"].ToString(); else Namew = "";
        }
        
        public override AbstractRecord Clone()
        {
            SproneRecord retValue = new SproneRecord();
            retValue.Id = this.Id;
            retValue.Npp = this.Npp;
            retValue.Codc = this.Codc;
            retValue.Name = this.Name;
            retValue.Namew = this.Namew;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SPRONE (ID, NPP, CODC, NAME, NAMEW) VALUES ('{0}', {1}, '{2}', '{3}', '{4}')", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Npp.ToString(), String.IsNullOrEmpty(Codc) ? "" : Codc.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Namew) ? "" : Namew.Trim());
            return rs;
        }
    }
}
