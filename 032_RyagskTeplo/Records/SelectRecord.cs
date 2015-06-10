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
    [TableName("SELECT.DBF")]
    public partial class SelectRecord: AbstractRecord
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

        private Int64 npp;
        // <summary>
        // NPP N(6)
        // </summary>
        [FieldName("NPP"), FieldType('N'), FieldWidth(6)]
        public Int64 Npp
        {
            get { return npp; }
            set { CheckIntegerData("Npp", value, 6); npp = value; }
        }

        private string name;
        // <summary>
        // NAME C(60)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(60)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 60); name = value; }
        }

        private string eval;
        // <summary>
        // EVAL C(100)
        // </summary>
        [FieldName("EVAL"), FieldType('C'), FieldWidth(100)]
        public string Eval
        {
            get { return eval; }
            set { CheckStringData("Eval", value, 100); eval = value; }
        }

        private Int64 flag;
        // <summary>
        // FLAG N(4)
        // </summary>
        [FieldName("FLAG"), FieldType('N'), FieldWidth(4)]
        public Int64 Flag
        {
            get { return flag; }
            set { CheckIntegerData("Flag", value, 4); flag = value; }
        }

        private bool frep;
        // <summary>
        // FREP L(1)
        // </summary>
        [FieldName("FREP"), FieldType('L'), FieldWidth(1)]
        public bool Frep
        {
            get { return frep; }
            set {  frep = value; }
        }

        private bool ffil;
        // <summary>
        // FFIL L(1)
        // </summary>
        [FieldName("FFIL"), FieldType('L'), FieldWidth(1)]
        public bool Ffil
        {
            get { return ffil; }
            set {  ffil = value; }
        }

        private bool fkor;
        // <summary>
        // FKOR L(1)
        // </summary>
        [FieldName("FKOR"), FieldType('L'), FieldWidth(1)]
        public bool Fkor
        {
            get { return fkor; }
            set {  fkor = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("EVAL")) Eval = ADataRow["EVAL"].ToString(); else Eval = "";
            if (ADataRow.Table.Columns.Contains("FLAG")) Flag = Convert.ToInt64(ADataRow["FLAG"]); else Flag = 0;
            if (ADataRow.Table.Columns.Contains("FREP")) Frep = ADataRow["FREP"].ToString() == "True" ? true : false; else Frep = false;
            if (ADataRow.Table.Columns.Contains("FFIL")) Ffil = ADataRow["FFIL"].ToString() == "True" ? true : false; else Ffil = false;
            if (ADataRow.Table.Columns.Contains("FKOR")) Fkor = ADataRow["FKOR"].ToString() == "True" ? true : false; else Fkor = false;
        }
        
        public override AbstractRecord Clone()
        {
            SelectRecord retValue = new SelectRecord();
            retValue.Id = this.Id;
            retValue.Npp = this.Npp;
            retValue.Name = this.Name;
            retValue.Eval = this.Eval;
            retValue.Flag = this.Flag;
            retValue.Frep = this.Frep;
            retValue.Ffil = this.Ffil;
            retValue.Fkor = this.Fkor;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SELECT (ID, NPP, NAME, EVAL, FLAG, FREP, FFIL, FKOR) VALUES ('{0}', {1}, '{2}', '{3}', {4}, {5}, {6}, {7})", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Npp.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Eval) ? "" : Eval.Trim(), Flag.ToString(), (Frep ? 0 : 1 ), (Ffil ? 0 : 1 ), (Fkor ? 0 : 1 ));
            return rs;
        }
    }
}
