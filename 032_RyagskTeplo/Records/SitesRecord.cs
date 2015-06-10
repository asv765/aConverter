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
    [TableName("SITES.DBF")]
    public partial class SitesRecord: AbstractRecord
    {
        private Int64 cod;
        // <summary>
        // COD N(6)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(6)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 6); cod = value; }
        }

        private string name;
        // <summary>
        // NAME C(50)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(50)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 50); name = value; }
        }

        private Int64 du;
        // <summary>
        // DU N(4)
        // </summary>
        [FieldName("DU"), FieldType('N'), FieldWidth(4)]
        public Int64 Du
        {
            get { return du; }
            set { CheckIntegerData("Du", value, 4); du = value; }
        }

        private Int64 uc;
        // <summary>
        // UC N(4)
        // </summary>
        [FieldName("UC"), FieldType('N'), FieldWidth(4)]
        public Int64 Uc
        {
            get { return uc; }
            set { CheckIntegerData("Uc", value, 4); uc = value; }
        }

        private Int64 codorg;
        // <summary>
        // CODORG N(4)
        // </summary>
        [FieldName("CODORG"), FieldType('N'), FieldWidth(4)]
        public Int64 Codorg
        {
            get { return codorg; }
            set { CheckIntegerData("Codorg", value, 4); codorg = value; }
        }

        private Int64 numusl;
        // <summary>
        // NUMUSL N(11)
        // </summary>
        [FieldName("NUMUSL"), FieldType('N'), FieldWidth(11)]
        public Int64 Numusl
        {
            get { return numusl; }
            set { CheckIntegerData("Numusl", value, 11); numusl = value; }
        }

        private string control1;
        // <summary>
        // CONTROL1 C(30)
        // </summary>
        [FieldName("CONTROL1"), FieldType('C'), FieldWidth(30)]
        public string Control1
        {
            get { return control1; }
            set { CheckStringData("Control1", value, 30); control1 = value; }
        }

        private string control2;
        // <summary>
        // CONTROL2 C(30)
        // </summary>
        [FieldName("CONTROL2"), FieldType('C'), FieldWidth(30)]
        public string Control2
        {
            get { return control2; }
            set { CheckStringData("Control2", value, 30); control2 = value; }
        }

        private string control3;
        // <summary>
        // CONTROL3 C(30)
        // </summary>
        [FieldName("CONTROL3"), FieldType('C'), FieldWidth(30)]
        public string Control3
        {
            get { return control3; }
            set { CheckStringData("Control3", value, 30); control3 = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("DU")) Du = Convert.ToInt64(ADataRow["DU"]); else Du = 0;
            if (ADataRow.Table.Columns.Contains("UC")) Uc = Convert.ToInt64(ADataRow["UC"]); else Uc = 0;
            if (ADataRow.Table.Columns.Contains("CODORG")) Codorg = Convert.ToInt64(ADataRow["CODORG"]); else Codorg = 0;
            if (ADataRow.Table.Columns.Contains("NUMUSL")) Numusl = Convert.ToInt64(ADataRow["NUMUSL"]); else Numusl = 0;
            if (ADataRow.Table.Columns.Contains("CONTROL1")) Control1 = ADataRow["CONTROL1"].ToString(); else Control1 = "";
            if (ADataRow.Table.Columns.Contains("CONTROL2")) Control2 = ADataRow["CONTROL2"].ToString(); else Control2 = "";
            if (ADataRow.Table.Columns.Contains("CONTROL3")) Control3 = ADataRow["CONTROL3"].ToString(); else Control3 = "";
        }
        
        public override AbstractRecord Clone()
        {
            SitesRecord retValue = new SitesRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Du = this.Du;
            retValue.Uc = this.Uc;
            retValue.Codorg = this.Codorg;
            retValue.Numusl = this.Numusl;
            retValue.Control1 = this.Control1;
            retValue.Control2 = this.Control2;
            retValue.Control3 = this.Control3;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SITES (COD, NAME, DU, UC, CODORG, NUMUSL, CONTROL1, CONTROL2, CONTROL3) VALUES ({0}, '{1}', {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}')", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), Du.ToString(), Uc.ToString(), Codorg.ToString(), Numusl.ToString(), String.IsNullOrEmpty(Control1) ? "" : Control1.Trim(), String.IsNullOrEmpty(Control2) ? "" : Control2.Trim(), String.IsNullOrEmpty(Control3) ? "" : Control3.Trim());
            return rs;
        }
    }
}
