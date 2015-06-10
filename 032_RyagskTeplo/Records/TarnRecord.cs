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
    [TableName("TARN.DBF")]
    public partial class TarnRecord: AbstractRecord
    {
        private Int64 service;
        // <summary>
        // SERVICE N(6)
        // </summary>
        [FieldName("SERVICE"), FieldType('N'), FieldWidth(6)]
        public Int64 Service
        {
            get { return service; }
            set { CheckIntegerData("Service", value, 6); service = value; }
        }

        private Int64 cod;
        // <summary>
        // COD N(11)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(11)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 11); cod = value; }
        }

        private string ediz;
        // <summary>
        // EDIZ C(10)
        // </summary>
        [FieldName("EDIZ"), FieldType('C'), FieldWidth(10)]
        public string Ediz
        {
            get { return ediz; }
            set { CheckStringData("Ediz", value, 10); ediz = value; }
        }

        private string name;
        // <summary>
        // NAME C(30)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(30)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 30); name = value; }
        }

        private string datn;
        // <summary>
        // DATN C(8)
        // </summary>
        [FieldName("DATN"), FieldType('C'), FieldWidth(8)]
        public string Datn
        {
            get { return datn; }
            set { CheckStringData("Datn", value, 8); datn = value; }
        }

        private string datk;
        // <summary>
        // DATK C(8)
        // </summary>
        [FieldName("DATK"), FieldType('C'), FieldWidth(8)]
        public string Datk
        {
            get { return datk; }
            set { CheckStringData("Datk", value, 8); datk = value; }
        }

        private decimal eval;
        // <summary>
        // EVAL N(15,5)
        // </summary>
        [FieldName("EVAL"), FieldType('N'), FieldWidth(15), FieldDec(5)]
        public decimal Eval
        {
            get { return eval; }
            set { CheckDecimalData("Eval", value, 15, 5); eval = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("SERVICE")) Service = Convert.ToInt64(ADataRow["SERVICE"]); else Service = 0;
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("EDIZ")) Ediz = ADataRow["EDIZ"].ToString(); else Ediz = "";
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("DATN")) Datn = ADataRow["DATN"].ToString(); else Datn = "";
            if (ADataRow.Table.Columns.Contains("DATK")) Datk = ADataRow["DATK"].ToString(); else Datk = "";
            if (ADataRow.Table.Columns.Contains("EVAL")) Eval = Convert.ToDecimal(ADataRow["EVAL"]); else Eval = 0;
        }
        
        public override AbstractRecord Clone()
        {
            TarnRecord retValue = new TarnRecord();
            retValue.Service = this.Service;
            retValue.Cod = this.Cod;
            retValue.Ediz = this.Ediz;
            retValue.Name = this.Name;
            retValue.Datn = this.Datn;
            retValue.Datk = this.Datk;
            retValue.Eval = this.Eval;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO TARN (SERVICE, COD, EDIZ, NAME, DATN, DATK, EVAL) VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', {6})", Service.ToString(), Cod.ToString(), String.IsNullOrEmpty(Ediz) ? "" : Ediz.Trim(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Datn) ? "" : Datn.Trim(), String.IsNullOrEmpty(Datk) ? "" : Datk.Trim(), Eval.ToString().Replace(',','.'));
            return rs;
        }
    }
}
