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
    [TableName("TARIF.DBF")]
    public partial class TarifRecord: AbstractRecord
    {
        private Int64 n_lgt;
        // <summary>
        // N_LGT N(3)
        // </summary>
        [FieldName("N_LGT"), FieldType('N'), FieldWidth(3)]
        public Int64 N_lgt
        {
            get { return n_lgt; }
            set { CheckIntegerData("N_lgt", value, 3); n_lgt = value; }
        }

        private string nasv;
        // <summary>
        // NASV C(35)
        // </summary>
        [FieldName("NASV"), FieldType('C'), FieldWidth(35)]
        public string Nasv
        {
            get { return nasv; }
            set { CheckStringData("Nasv", value, 35); nasv = value; }
        }

        private decimal tarif;
        // <summary>
        // TARIF N(7,3)
        // </summary>
        [FieldName("TARIF"), FieldType('N'), FieldWidth(7), FieldDec(3)]
        public decimal Tarif
        {
            get { return tarif; }
            set { CheckDecimalData("Tarif", value, 7, 3); tarif = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LGT")) N_lgt = Convert.ToInt64(ADataRow["N_LGT"]); else N_lgt = 0;
            if (ADataRow.Table.Columns.Contains("NASV")) Nasv = ADataRow["NASV"].ToString(); else Nasv = "";
            if (ADataRow.Table.Columns.Contains("TARIF")) Tarif = Convert.ToDecimal(ADataRow["TARIF"]); else Tarif = 0;
        }
        
        public override AbstractRecord Clone()
        {
            TarifRecord retValue = new TarifRecord();
            retValue.N_lgt = this.N_lgt;
            retValue.Nasv = this.Nasv;
            retValue.Tarif = this.Tarif;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO TARIF (N_LGT, NASV, TARIF) VALUES ({0}, '{1}', {2})", N_lgt.ToString(), String.IsNullOrEmpty(Nasv) ? "" : Nasv.Trim(), Tarif.ToString().Replace(',','.'));
            return rs;
        }
    }
}
	