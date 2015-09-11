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
    [TableName("LGTN_F.DBF")]
    public partial class Lgtn_fRecord: AbstractRecord
    {
        private Int64 nom;
        // <summary>
        // NOM N(3)
        // </summary>
        [FieldName("NOM"), FieldType('N'), FieldWidth(3)]
        public Int64 Nom
        {
            get { return nom; }
            set { CheckIntegerData("Nom", value, 3); nom = value; }
        }

        private string ind_l;
        // <summary>
        // IND_L C(2)
        // </summary>
        [FieldName("IND_L"), FieldType('C'), FieldWidth(2)]
        public string Ind_l
        {
            get { return ind_l; }
            set { CheckStringData("Ind_l", value, 2); ind_l = value; }
        }

        private string kateg;
        // <summary>
        // KATEG M
        // </summary>
        [FieldName("KATEG"), FieldType('M')]
        public string Kateg
        {
            get { return kateg; }
            set { CheckStringData("Kateg", value, 0); kateg = value; }
        }

        private Int64 kvart;
        // <summary>
        // KVART N(4)
        // </summary>
        [FieldName("KVART"), FieldType('N'), FieldWidth(4)]
        public Int64 Kvart
        {
            get { return kvart; }
            set { CheckIntegerData("Kvart", value, 4); kvart = value; }
        }

        private Int64 otopl;
        // <summary>
        // OTOPL N(4)
        // </summary>
        [FieldName("OTOPL"), FieldType('N'), FieldWidth(4)]
        public Int64 Otopl
        {
            get { return otopl; }
            set { CheckIntegerData("Otopl", value, 4); otopl = value; }
        }

        private Int64 light;
        // <summary>
        // LIGHT N(4)
        // </summary>
        [FieldName("LIGHT"), FieldType('N'), FieldWidth(4)]
        public Int64 Light
        {
            get { return light; }
            set { CheckIntegerData("Light", value, 4); light = value; }
        }

        private Int64 water;
        // <summary>
        // WATER N(4)
        // </summary>
        [FieldName("WATER"), FieldType('N'), FieldWidth(4)]
        public Int64 Water
        {
            get { return water; }
            set { CheckIntegerData("Water", value, 4); water = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("NOM")) Nom = Convert.ToInt64(ADataRow["NOM"]); else Nom = 0;
            if (ADataRow.Table.Columns.Contains("IND_L")) Ind_l = ADataRow["IND_L"].ToString(); else Ind_l = "";
            if (ADataRow.Table.Columns.Contains("KATEG")) Kateg = ADataRow["KATEG"].ToString(); else Kateg = "";
            if (ADataRow.Table.Columns.Contains("KVART")) Kvart = Convert.ToInt64(ADataRow["KVART"]); else Kvart = 0;
            if (ADataRow.Table.Columns.Contains("OTOPL")) Otopl = Convert.ToInt64(ADataRow["OTOPL"]); else Otopl = 0;
            if (ADataRow.Table.Columns.Contains("LIGHT")) Light = Convert.ToInt64(ADataRow["LIGHT"]); else Light = 0;
            if (ADataRow.Table.Columns.Contains("WATER")) Water = Convert.ToInt64(ADataRow["WATER"]); else Water = 0;
        }
        
        public override AbstractRecord Clone()
        {
            Lgtn_fRecord retValue = new Lgtn_fRecord();
            retValue.Nom = this.Nom;
            retValue.Ind_l = this.Ind_l;
            retValue.Kateg = this.Kateg;
            retValue.Kvart = this.Kvart;
            retValue.Otopl = this.Otopl;
            retValue.Light = this.Light;
            retValue.Water = this.Water;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO LGTN_F (NOM, IND_L, KATEG, KVART, OTOPL, LIGHT, WATER) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6})", Nom.ToString(), String.IsNullOrEmpty(Ind_l) ? "" : Ind_l.Trim(), String.IsNullOrEmpty(Kateg) ? "" : Kateg.Trim(), Kvart.ToString(), Otopl.ToString(), Light.ToString(), Water.ToString());
            return rs;
        }
    }
}
	