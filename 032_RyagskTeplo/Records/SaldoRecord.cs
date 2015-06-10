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
    [TableName("SALDO.DBF")]
    public partial class SaldoRecord: AbstractRecord
    {
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

        private string id;
        // <summary>
        // ID C(10)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(10)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 10); id = value; }
        }

        private Int64 num;
        // <summary>
        // NUM N(6)
        // </summary>
        [FieldName("NUM"), FieldType('N'), FieldWidth(6)]
        public Int64 Num
        {
            get { return num; }
            set { CheckIntegerData("Num", value, 6); num = value; }
        }

        private decimal sumo;
        // <summary>
        // SUMO N(15,2)
        // </summary>
        [FieldName("SUMO"), FieldType('N'), FieldWidth(15), FieldDec(2)]
        public decimal Sumo
        {
            get { return sumo; }
            set { CheckDecimalData("Sumo", value, 15, 2); sumo = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("NUM")) Num = Convert.ToInt64(ADataRow["NUM"]); else Num = 0;
            if (ADataRow.Table.Columns.Contains("SUMO")) Sumo = Convert.ToDecimal(ADataRow["SUMO"]); else Sumo = 0;
        }
        
        public override AbstractRecord Clone()
        {
            SaldoRecord retValue = new SaldoRecord();
            retValue.Dat = this.Dat;
            retValue.Id = this.Id;
            retValue.Num = this.Num;
            retValue.Sumo = this.Sumo;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO SALDO (DAT, ID, NUM, SUMO) VALUES (CTOD('{0}'), '{1}', {2}, {3})", Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), String.IsNullOrEmpty(Id) ? "" : Id.Trim(), Num.ToString(), Sumo.ToString().Replace(',','.'));
            return rs;
        }
    }
}
