// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _036_Izhevskoe
{
    [TableName("DAT_OT.DBF")]
    public partial class Dat_otRecord: AbstractRecord
    {
        private DateTime dat1;
        // <summary>
        // DAT1 D(8)
        // </summary>
        [FieldName("DAT1"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat1
        {
            get { return dat1; }
            set {  dat1 = value; }
        }

        private DateTime dat2;
        // <summary>
        // DAT2 D(8)
        // </summary>
        [FieldName("DAT2"), FieldType('D'), FieldWidth(8)]
        public DateTime Dat2
        {
            get { return dat2; }
            set {  dat2 = value; }
        }

        private decimal s_kvart;
        // <summary>
        // S_KVART N(8,2)
        // </summary>
        [FieldName("S_KVART"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_kvart
        {
            get { return s_kvart; }
            set { CheckDecimalData("S_kvart", value, 8, 2); s_kvart = value; }
        }

        private decimal s_otopl;
        // <summary>
        // S_OTOPL N(8,2)
        // </summary>
        [FieldName("S_OTOPL"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_otopl
        {
            get { return s_otopl; }
            set { CheckDecimalData("S_otopl", value, 8, 2); s_otopl = value; }
        }

        private decimal s_water;
        // <summary>
        // S_WATER N(8,2)
        // </summary>
        [FieldName("S_WATER"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_water
        {
            get { return s_water; }
            set { CheckDecimalData("S_water", value, 8, 2); s_water = value; }
        }

        private decimal s_light;
        // <summary>
        // S_LIGHT N(8,2)
        // </summary>
        [FieldName("S_LIGHT"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal S_light
        {
            get { return s_light; }
            set { CheckDecimalData("S_light", value, 8, 2); s_light = value; }
        }

        private Int64 kvt;
        // <summary>
        // KVT N(9)
        // </summary>
        [FieldName("KVT"), FieldType('N'), FieldWidth(9)]
        public Int64 Kvt
        {
            get { return kvt; }
            set { CheckIntegerData("Kvt", value, 9); kvt = value; }
        }

        private Int64 kvs;
        // <summary>
        // KVS N(9)
        // </summary>
        [FieldName("KVS"), FieldType('N'), FieldWidth(9)]
        public Int64 Kvs
        {
            get { return kvs; }
            set { CheckIntegerData("Kvs", value, 9); kvs = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DAT1")) Dat1 = Convert.ToDateTime(ADataRow["DAT1"]); else Dat1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DAT2")) Dat2 = Convert.ToDateTime(ADataRow["DAT2"]); else Dat2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("S_KVART")) S_kvart = Convert.ToDecimal(ADataRow["S_KVART"]); else S_kvart = 0;
            if (ADataRow.Table.Columns.Contains("S_OTOPL")) S_otopl = Convert.ToDecimal(ADataRow["S_OTOPL"]); else S_otopl = 0;
            if (ADataRow.Table.Columns.Contains("S_WATER")) S_water = Convert.ToDecimal(ADataRow["S_WATER"]); else S_water = 0;
            if (ADataRow.Table.Columns.Contains("S_LIGHT")) S_light = Convert.ToDecimal(ADataRow["S_LIGHT"]); else S_light = 0;
            if (ADataRow.Table.Columns.Contains("KVT")) Kvt = Convert.ToInt64(ADataRow["KVT"]); else Kvt = 0;
            if (ADataRow.Table.Columns.Contains("KVS")) Kvs = Convert.ToInt64(ADataRow["KVS"]); else Kvs = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Dat_otRecord retValue = new Dat_otRecord();
            retValue.Dat1 = this.Dat1;
            retValue.Dat2 = this.Dat2;
            retValue.S_kvart = this.S_kvart;
            retValue.S_otopl = this.S_otopl;
            retValue.S_water = this.S_water;
            retValue.S_light = this.S_light;
            retValue.Kvt = this.Kvt;
            retValue.Kvs = this.Kvs;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO DAT_OT (DAT1, DAT2, S_KVART, S_OTOPL, S_WATER, S_LIGHT, KVT, KVS) VALUES (CTOD('{0}'), CTOD('{1}'), {2}, {3}, {4}, {5}, {6}, {7})", Dat1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat1.Month, Dat1.Day, Dat1.Year), Dat2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat2.Month, Dat2.Day, Dat2.Year), S_kvart.ToString().Replace(',','.'), S_otopl.ToString().Replace(',','.'), S_water.ToString().Replace(',','.'), S_light.ToString().Replace(',','.'), Kvt.ToString(), Kvs.ToString());
            return rs;
        }
    }
}
	