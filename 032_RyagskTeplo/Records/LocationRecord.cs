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
    [TableName("LOCATION.DBF")]
    public partial class LocationRecord: AbstractRecord
    {
        private Int64 id_post;
        // <summary>
        // ID_POST N(6)
        // </summary>
        [FieldName("ID_POST"), FieldType('N'), FieldWidth(6)]
        public Int64 Id_post
        {
            get { return id_post; }
            set { CheckIntegerData("Id_post", value, 6); id_post = value; }
        }

        private Int64 id_org;
        // <summary>
        // ID_ORG N(6)
        // </summary>
        [FieldName("ID_ORG"), FieldType('N'), FieldWidth(6)]
        public Int64 Id_org
        {
            get { return id_org; }
            set { CheckIntegerData("Id_org", value, 6); id_org = value; }
        }

        private string name_post;
        // <summary>
        // NAME_POST C(50)
        // </summary>
        [FieldName("NAME_POST"), FieldType('C'), FieldWidth(50)]
        public string Name_post
        {
            get { return name_post; }
            set { CheckStringData("Name_post", value, 50); name_post = value; }
        }

        private string dop_post;
        // <summary>
        // DOP_POST C(50)
        // </summary>
        [FieldName("DOP_POST"), FieldType('C'), FieldWidth(50)]
        public string Dop_post
        {
            get { return dop_post; }
            set { CheckStringData("Dop_post", value, 50); dop_post = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID_POST")) Id_post = Convert.ToInt64(ADataRow["ID_POST"]); else Id_post = 0;
            if (ADataRow.Table.Columns.Contains("ID_ORG")) Id_org = Convert.ToInt64(ADataRow["ID_ORG"]); else Id_org = 0;
            if (ADataRow.Table.Columns.Contains("NAME_POST")) Name_post = ADataRow["NAME_POST"].ToString(); else Name_post = "";
            if (ADataRow.Table.Columns.Contains("DOP_POST")) Dop_post = ADataRow["DOP_POST"].ToString(); else Dop_post = "";
        }
        
        public override AbstractRecord Clone()
        {
            LocationRecord retValue = new LocationRecord();
            retValue.Id_post = this.Id_post;
            retValue.Id_org = this.Id_org;
            retValue.Name_post = this.Name_post;
            retValue.Dop_post = this.Dop_post;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LOCATION (ID_POST, ID_ORG, NAME_POST, DOP_POST) VALUES ({0}, {1}, '{2}', '{3}')", Id_post.ToString(), Id_org.ToString(), String.IsNullOrEmpty(Name_post) ? "" : Name_post.Trim(), String.IsNullOrEmpty(Dop_post) ? "" : Dop_post.Trim());
            return rs;
        }
    }
}
