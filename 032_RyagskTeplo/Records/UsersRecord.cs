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
    [TableName("USERS.DBF")]
    public partial class UsersRecord: AbstractRecord
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

        private string name;
        // <summary>
        // NAME C(20)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(20)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 20); name = value; }
        }

        private string parol;
        // <summary>
        // PAROL C(20)
        // </summary>
        [FieldName("PAROL"), FieldType('C'), FieldWidth(20)]
        public string Parol
        {
            get { return parol; }
            set { CheckStringData("Parol", value, 20); parol = value; }
        }

        private string stat;
        // <summary>
        // STAT C(3)
        // </summary>
        [FieldName("STAT"), FieldType('C'), FieldWidth(3)]
        public string Stat
        {
            get { return stat; }
            set { CheckStringData("Stat", value, 3); stat = value; }
        }

        private string fam_op;
        // <summary>
        // FAM_OP C(20)
        // </summary>
        [FieldName("FAM_OP"), FieldType('C'), FieldWidth(20)]
        public string Fam_op
        {
            get { return fam_op; }
            set { CheckStringData("Fam_op", value, 20); fam_op = value; }
        }

        private string imia_op;
        // <summary>
        // IMIA_OP C(20)
        // </summary>
        [FieldName("IMIA_OP"), FieldType('C'), FieldWidth(20)]
        public string Imia_op
        {
            get { return imia_op; }
            set { CheckStringData("Imia_op", value, 20); imia_op = value; }
        }

        private string otch_op;
        // <summary>
        // OTCH_OP C(20)
        // </summary>
        [FieldName("OTCH_OP"), FieldType('C'), FieldWidth(20)]
        public string Otch_op
        {
            get { return otch_op; }
            set { CheckStringData("Otch_op", value, 20); otch_op = value; }
        }

        private string flags;
        // <summary>
        // FLAGS C(30)
        // </summary>
        [FieldName("FLAGS"), FieldType('C'), FieldWidth(30)]
        public string Flags
        {
            get { return flags; }
            set { CheckStringData("Flags", value, 30); flags = value; }
        }

        private Int64 vuf;
        // <summary>
        // VUF N(2)
        // </summary>
        [FieldName("VUF"), FieldType('N'), FieldWidth(2)]
        public Int64 Vuf
        {
            get { return vuf; }
            set { CheckIntegerData("Vuf", value, 2); vuf = value; }
        }

        private Int64 idforms;
        // <summary>
        // IDFORMS N(6)
        // </summary>
        [FieldName("IDFORMS"), FieldType('N'), FieldWidth(6)]
        public Int64 Idforms
        {
            get { return idforms; }
            set { CheckIntegerData("Idforms", value, 6); idforms = value; }
        }

        private string parm;
        // <summary>
        // PARM C(10)
        // </summary>
        [FieldName("PARM"), FieldType('C'), FieldWidth(10)]
        public string Parm
        {
            get { return parm; }
            set { CheckStringData("Parm", value, 10); parm = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = Convert.ToInt64(ADataRow["ID"]); else Id = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("PAROL")) Parol = ADataRow["PAROL"].ToString(); else Parol = "";
            if (ADataRow.Table.Columns.Contains("STAT")) Stat = ADataRow["STAT"].ToString(); else Stat = "";
            if (ADataRow.Table.Columns.Contains("FAM_OP")) Fam_op = ADataRow["FAM_OP"].ToString(); else Fam_op = "";
            if (ADataRow.Table.Columns.Contains("IMIA_OP")) Imia_op = ADataRow["IMIA_OP"].ToString(); else Imia_op = "";
            if (ADataRow.Table.Columns.Contains("OTCH_OP")) Otch_op = ADataRow["OTCH_OP"].ToString(); else Otch_op = "";
            if (ADataRow.Table.Columns.Contains("FLAGS")) Flags = ADataRow["FLAGS"].ToString(); else Flags = "";
            if (ADataRow.Table.Columns.Contains("VUF")) Vuf = Convert.ToInt64(ADataRow["VUF"]); else Vuf = 0;
            if (ADataRow.Table.Columns.Contains("IDFORMS")) Idforms = Convert.ToInt64(ADataRow["IDFORMS"]); else Idforms = 0;
            if (ADataRow.Table.Columns.Contains("PARM")) Parm = ADataRow["PARM"].ToString(); else Parm = "";
        }
        
        public override AbstractRecord Clone()
        {
            UsersRecord retValue = new UsersRecord();
            retValue.Id = this.Id;
            retValue.Name = this.Name;
            retValue.Parol = this.Parol;
            retValue.Stat = this.Stat;
            retValue.Fam_op = this.Fam_op;
            retValue.Imia_op = this.Imia_op;
            retValue.Otch_op = this.Otch_op;
            retValue.Flags = this.Flags;
            retValue.Vuf = this.Vuf;
            retValue.Idforms = this.Idforms;
            retValue.Parm = this.Parm;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO USERS (ID, NAME, PAROL, STAT, FAM_OP, IMIA_OP, OTCH_OP, FLAGS, VUF, IDFORMS, PARM) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, '{10}')", Id.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Parol) ? "" : Parol.Trim(), String.IsNullOrEmpty(Stat) ? "" : Stat.Trim(), String.IsNullOrEmpty(Fam_op) ? "" : Fam_op.Trim(), String.IsNullOrEmpty(Imia_op) ? "" : Imia_op.Trim(), String.IsNullOrEmpty(Otch_op) ? "" : Otch_op.Trim(), String.IsNullOrEmpty(Flags) ? "" : Flags.Trim(), Vuf.ToString(), Idforms.ToString(), String.IsNullOrEmpty(Parm) ? "" : Parm.Trim());
            return rs;
        }
    }
}
