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
    [TableName("tar.DBF")]
    public partial class TarRecord: AbstractRecord
    {
        private DateTime dttar;
        // <summary>
        // DTTAR D(8)
        // </summary>
        [FieldName("DTTAR"), FieldType('D'), FieldWidth(8)]
        public DateTime Dttar
        {
            get { return dttar; }
            set {  dttar = value; }
        }

        private DateTime dttarend;
        // <summary>
        // DTTAREND D(8)
        // </summary>
        [FieldName("DTTAREND"), FieldType('D'), FieldWidth(8)]
        public DateTime Dttarend
        {
            get { return dttarend; }
            set {  dttarend = value; }
        }

        private decimal kv;
        // <summary>
        // KV N(7,2)
        // </summary>
        [FieldName("KV"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Kv
        {
            get { return kv; }
            set { CheckDecimalData("Kv", value, 7, 2); kv = value; }
        }

        private decimal remo;
        // <summary>
        // REMO N(10,2)
        // </summary>
        [FieldName("REMO"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Remo
        {
            get { return remo; }
            set { CheckDecimalData("Remo", value, 10, 2); remo = value; }
        }

        private decimal jilo;
        // <summary>
        // JILO N(10,2)
        // </summary>
        [FieldName("JILO"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Jilo
        {
            get { return jilo; }
            set { CheckDecimalData("Jilo", value, 10, 2); jilo = value; }
        }

        private decimal otop;
        // <summary>
        // OTOP N(12,5)
        // </summary>
        [FieldName("OTOP"), FieldType('N'), FieldWidth(12), FieldDec(5)]
        public decimal Otop
        {
            get { return otop; }
            set { CheckDecimalData("Otop", value, 12, 5); otop = value; }
        }

        private decimal sthv1;
        // <summary>
        // STHV1 N(10,4)
        // </summary>
        [FieldName("STHV1"), FieldType('N'), FieldWidth(10), FieldDec(4)]
        public decimal Sthv1
        {
            get { return sthv1; }
            set { CheckDecimalData("Sthv1", value, 10, 4); sthv1 = value; }
        }

        private decimal sthv2;
        // <summary>
        // STHV2 N(10,2)
        // </summary>
        [FieldName("STHV2"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv2
        {
            get { return sthv2; }
            set { CheckDecimalData("Sthv2", value, 10, 2); sthv2 = value; }
        }

        private decimal sthv3;
        // <summary>
        // STHV3 N(10,2)
        // </summary>
        [FieldName("STHV3"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv3
        {
            get { return sthv3; }
            set { CheckDecimalData("Sthv3", value, 10, 2); sthv3 = value; }
        }

        private decimal sthv4;
        // <summary>
        // STHV4 N(10,2)
        // </summary>
        [FieldName("STHV4"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv4
        {
            get { return sthv4; }
            set { CheckDecimalData("Sthv4", value, 10, 2); sthv4 = value; }
        }

        private decimal sthv5;
        // <summary>
        // STHV5 N(10,5)
        // </summary>
        [FieldName("STHV5"), FieldType('N'), FieldWidth(10), FieldDec(5)]
        public decimal Sthv5
        {
            get { return sthv5; }
            set { CheckDecimalData("Sthv5", value, 10, 5); sthv5 = value; }
        }

        private decimal sthv6;
        // <summary>
        // STHV6 N(10,2)
        // </summary>
        [FieldName("STHV6"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv6
        {
            get { return sthv6; }
            set { CheckDecimalData("Sthv6", value, 10, 2); sthv6 = value; }
        }

        private decimal sthv7;
        // <summary>
        // STHV7 N(10,2)
        // </summary>
        [FieldName("STHV7"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv7
        {
            get { return sthv7; }
            set { CheckDecimalData("Sthv7", value, 10, 2); sthv7 = value; }
        }

        private decimal sthv8;
        // <summary>
        // STHV8 N(10,2)
        // </summary>
        [FieldName("STHV8"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Sthv8
        {
            get { return sthv8; }
            set { CheckDecimalData("Sthv8", value, 10, 2); sthv8 = value; }
        }

        private decimal sthv_k;
        // <summary>
        // STHV_K N(10,4)
        // </summary>
        [FieldName("STHV_K"), FieldType('N'), FieldWidth(10), FieldDec(4)]
        public decimal Sthv_k
        {
            get { return sthv_k; }
            set { CheckDecimalData("Sthv_k", value, 10, 4); sthv_k = value; }
        }

        private decimal stkn1;
        // <summary>
        // STKN1 N(10,4)
        // </summary>
        [FieldName("STKN1"), FieldType('N'), FieldWidth(10), FieldDec(4)]
        public decimal Stkn1
        {
            get { return stkn1; }
            set { CheckDecimalData("Stkn1", value, 10, 4); stkn1 = value; }
        }

        private decimal stkn2;
        // <summary>
        // STKN2 N(10,2)
        // </summary>
        [FieldName("STKN2"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn2
        {
            get { return stkn2; }
            set { CheckDecimalData("Stkn2", value, 10, 2); stkn2 = value; }
        }

        private decimal stkn3;
        // <summary>
        // STKN3 N(10,2)
        // </summary>
        [FieldName("STKN3"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn3
        {
            get { return stkn3; }
            set { CheckDecimalData("Stkn3", value, 10, 2); stkn3 = value; }
        }

        private decimal stkn4;
        // <summary>
        // STKN4 N(10,2)
        // </summary>
        [FieldName("STKN4"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn4
        {
            get { return stkn4; }
            set { CheckDecimalData("Stkn4", value, 10, 2); stkn4 = value; }
        }

        private decimal stkn5;
        // <summary>
        // STKN5 N(10,2)
        // </summary>
        [FieldName("STKN5"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn5
        {
            get { return stkn5; }
            set { CheckDecimalData("Stkn5", value, 10, 2); stkn5 = value; }
        }

        private decimal stkn7;
        // <summary>
        // STKN7 N(10,2)
        // </summary>
        [FieldName("STKN7"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn7
        {
            get { return stkn7; }
            set { CheckDecimalData("Stkn7", value, 10, 2); stkn7 = value; }
        }

        private decimal stkn8;
        // <summary>
        // STKN8 N(10,2)
        // </summary>
        [FieldName("STKN8"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Stkn8
        {
            get { return stkn8; }
            set { CheckDecimalData("Stkn8", value, 10, 2); stkn8 = value; }
        }

        private decimal stkn_k;
        // <summary>
        // STKN_K N(10,4)
        // </summary>
        [FieldName("STKN_K"), FieldType('N'), FieldWidth(10), FieldDec(4)]
        public decimal Stkn_k
        {
            get { return stkn_k; }
            set { CheckDecimalData("Stkn_k", value, 10, 4); stkn_k = value; }
        }

        private decimal musor;
        // <summary>
        // MUSOR N(8,2)
        // </summary>
        [FieldName("MUSOR"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Musor
        {
            get { return musor; }
            set { CheckDecimalData("Musor", value, 8, 2); musor = value; }
        }

        private decimal radio;
        // <summary>
        // RADIO N(8,2)
        // </summary>
        [FieldName("RADIO"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Radio
        {
            get { return radio; }
            set { CheckDecimalData("Radio", value, 8, 2); radio = value; }
        }

        private decimal tbo;
        // <summary>
        // TBO N(7,2)
        // </summary>
        [FieldName("TBO"), FieldType('N'), FieldWidth(7), FieldDec(2)]
        public decimal Tbo
        {
            get { return tbo; }
            set { CheckDecimalData("Tbo", value, 7, 2); tbo = value; }
        }

        private decimal stgv;
        // <summary>
        // STGV N(12,5)
        // </summary>
        [FieldName("STGV"), FieldType('N'), FieldWidth(12), FieldDec(5)]
        public decimal Stgv
        {
            get { return stgv; }
            set { CheckDecimalData("Stgv", value, 12, 5); stgv = value; }
        }

        private decimal gvs_c;
        // <summary>
        // GVS_C N(12,5)
        // </summary>
        [FieldName("GVS_C"), FieldType('N'), FieldWidth(12), FieldDec(5)]
        public decimal Gvs_c
        {
            get { return gvs_c; }
            set { CheckDecimalData("Gvs_c", value, 12, 5); gvs_c = value; }
        }

        private decimal ant;
        // <summary>
        // ANT N(8,2)
        // </summary>
        [FieldName("ANT"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Ant
        {
            get { return ant; }
            set { CheckDecimalData("Ant", value, 8, 2); ant = value; }
        }

        private decimal cels;
        // <summary>
        // CELS N(8,2)
        // </summary>
        [FieldName("CELS"), FieldType('N'), FieldWidth(8), FieldDec(2)]
        public decimal Cels
        {
            get { return cels; }
            set { CheckDecimalData("Cels", value, 8, 2); cels = value; }
        }

        private decimal procpeni;
        // <summary>
        // PROCPENI N(10,5)
        // </summary>
        [FieldName("PROCPENI"), FieldType('N'), FieldWidth(10), FieldDec(5)]
        public decimal Procpeni
        {
            get { return procpeni; }
            set { CheckDecimalData("Procpeni", value, 10, 5); procpeni = value; }
        }

        private decimal nds;
        // <summary>
        // NDS N(10,2)
        // </summary>
        [FieldName("NDS"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Nds
        {
            get { return nds; }
            set { CheckDecimalData("Nds", value, 10, 2); nds = value; }
        }

        private decimal np_gv;
        // <summary>
        // NP_GV N(10,3)
        // </summary>
        [FieldName("NP_GV"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal Np_gv
        {
            get { return np_gv; }
            set { CheckDecimalData("Np_gv", value, 10, 3); np_gv = value; }
        }

        private decimal np_hv;
        // <summary>
        // NP_HV N(10,3)
        // </summary>
        [FieldName("NP_HV"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal Np_hv
        {
            get { return np_hv; }
            set { CheckDecimalData("Np_hv", value, 10, 3); np_hv = value; }
        }

        private decimal domofon;
        // <summary>
        // DOMOFON N(10,2)
        // </summary>
        [FieldName("DOMOFON"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Domofon
        {
            get { return domofon; }
            set { CheckDecimalData("Domofon", value, 10, 2); domofon = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("DTTAR")) Dttar = Convert.ToDateTime(ADataRow["DTTAR"]); else Dttar = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DTTAREND")) Dttarend = Convert.ToDateTime(ADataRow["DTTAREND"]); else Dttarend = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("KV")) Kv = Convert.ToDecimal(ADataRow["KV"]); else Kv = 0;
            if (ADataRow.Table.Columns.Contains("REMO")) Remo = Convert.ToDecimal(ADataRow["REMO"]); else Remo = 0;
            if (ADataRow.Table.Columns.Contains("JILO")) Jilo = Convert.ToDecimal(ADataRow["JILO"]); else Jilo = 0;
            if (ADataRow.Table.Columns.Contains("OTOP")) Otop = Convert.ToDecimal(ADataRow["OTOP"]); else Otop = 0;
            if (ADataRow.Table.Columns.Contains("STHV1")) Sthv1 = Convert.ToDecimal(ADataRow["STHV1"]); else Sthv1 = 0;
            if (ADataRow.Table.Columns.Contains("STHV2")) Sthv2 = Convert.ToDecimal(ADataRow["STHV2"]); else Sthv2 = 0;
            if (ADataRow.Table.Columns.Contains("STHV3")) Sthv3 = Convert.ToDecimal(ADataRow["STHV3"]); else Sthv3 = 0;
            if (ADataRow.Table.Columns.Contains("STHV4")) Sthv4 = Convert.ToDecimal(ADataRow["STHV4"]); else Sthv4 = 0;
            if (ADataRow.Table.Columns.Contains("STHV5")) Sthv5 = Convert.ToDecimal(ADataRow["STHV5"]); else Sthv5 = 0;
            if (ADataRow.Table.Columns.Contains("STHV6")) Sthv6 = Convert.ToDecimal(ADataRow["STHV6"]); else Sthv6 = 0;
            if (ADataRow.Table.Columns.Contains("STHV7")) Sthv7 = Convert.ToDecimal(ADataRow["STHV7"]); else Sthv7 = 0;
            if (ADataRow.Table.Columns.Contains("STHV8")) Sthv8 = Convert.ToDecimal(ADataRow["STHV8"]); else Sthv8 = 0;
            if (ADataRow.Table.Columns.Contains("STHV_K")) Sthv_k = Convert.ToDecimal(ADataRow["STHV_K"]); else Sthv_k = 0;
            if (ADataRow.Table.Columns.Contains("STKN1")) Stkn1 = Convert.ToDecimal(ADataRow["STKN1"]); else Stkn1 = 0;
            if (ADataRow.Table.Columns.Contains("STKN2")) Stkn2 = Convert.ToDecimal(ADataRow["STKN2"]); else Stkn2 = 0;
            if (ADataRow.Table.Columns.Contains("STKN3")) Stkn3 = Convert.ToDecimal(ADataRow["STKN3"]); else Stkn3 = 0;
            if (ADataRow.Table.Columns.Contains("STKN4")) Stkn4 = Convert.ToDecimal(ADataRow["STKN4"]); else Stkn4 = 0;
            if (ADataRow.Table.Columns.Contains("STKN5")) Stkn5 = Convert.ToDecimal(ADataRow["STKN5"]); else Stkn5 = 0;
            if (ADataRow.Table.Columns.Contains("STKN7")) Stkn7 = Convert.ToDecimal(ADataRow["STKN7"]); else Stkn7 = 0;
            if (ADataRow.Table.Columns.Contains("STKN8")) Stkn8 = Convert.ToDecimal(ADataRow["STKN8"]); else Stkn8 = 0;
            if (ADataRow.Table.Columns.Contains("STKN_K")) Stkn_k = Convert.ToDecimal(ADataRow["STKN_K"]); else Stkn_k = 0;
            if (ADataRow.Table.Columns.Contains("MUSOR")) Musor = Convert.ToDecimal(ADataRow["MUSOR"]); else Musor = 0;
            if (ADataRow.Table.Columns.Contains("RADIO")) Radio = Convert.ToDecimal(ADataRow["RADIO"]); else Radio = 0;
            if (ADataRow.Table.Columns.Contains("TBO")) Tbo = Convert.ToDecimal(ADataRow["TBO"]); else Tbo = 0;
            if (ADataRow.Table.Columns.Contains("STGV")) Stgv = Convert.ToDecimal(ADataRow["STGV"]); else Stgv = 0;
            if (ADataRow.Table.Columns.Contains("GVS_C")) Gvs_c = Convert.ToDecimal(ADataRow["GVS_C"]); else Gvs_c = 0;
            if (ADataRow.Table.Columns.Contains("ANT")) Ant = Convert.ToDecimal(ADataRow["ANT"]); else Ant = 0;
            if (ADataRow.Table.Columns.Contains("CELS")) Cels = Convert.ToDecimal(ADataRow["CELS"]); else Cels = 0;
            if (ADataRow.Table.Columns.Contains("PROCPENI")) Procpeni = Convert.ToDecimal(ADataRow["PROCPENI"]); else Procpeni = 0;
            if (ADataRow.Table.Columns.Contains("NDS")) Nds = Convert.ToDecimal(ADataRow["NDS"]); else Nds = 0;
            if (ADataRow.Table.Columns.Contains("NP_GV")) Np_gv = Convert.ToDecimal(ADataRow["NP_GV"]); else Np_gv = 0;
            if (ADataRow.Table.Columns.Contains("NP_HV")) Np_hv = Convert.ToDecimal(ADataRow["NP_HV"]); else Np_hv = 0;
            if (ADataRow.Table.Columns.Contains("DOMOFON")) Domofon = Convert.ToDecimal(ADataRow["DOMOFON"]); else Domofon = 0;
        }
        
        public override AbstractRecord Clone()
        {
            TarRecord retValue = new TarRecord();
            retValue.Dttar = this.Dttar;
            retValue.Dttarend = this.Dttarend;
            retValue.Kv = this.Kv;
            retValue.Remo = this.Remo;
            retValue.Jilo = this.Jilo;
            retValue.Otop = this.Otop;
            retValue.Sthv1 = this.Sthv1;
            retValue.Sthv2 = this.Sthv2;
            retValue.Sthv3 = this.Sthv3;
            retValue.Sthv4 = this.Sthv4;
            retValue.Sthv5 = this.Sthv5;
            retValue.Sthv6 = this.Sthv6;
            retValue.Sthv7 = this.Sthv7;
            retValue.Sthv8 = this.Sthv8;
            retValue.Sthv_k = this.Sthv_k;
            retValue.Stkn1 = this.Stkn1;
            retValue.Stkn2 = this.Stkn2;
            retValue.Stkn3 = this.Stkn3;
            retValue.Stkn4 = this.Stkn4;
            retValue.Stkn5 = this.Stkn5;
            retValue.Stkn7 = this.Stkn7;
            retValue.Stkn8 = this.Stkn8;
            retValue.Stkn_k = this.Stkn_k;
            retValue.Musor = this.Musor;
            retValue.Radio = this.Radio;
            retValue.Tbo = this.Tbo;
            retValue.Stgv = this.Stgv;
            retValue.Gvs_c = this.Gvs_c;
            retValue.Ant = this.Ant;
            retValue.Cels = this.Cels;
            retValue.Procpeni = this.Procpeni;
            retValue.Nds = this.Nds;
            retValue.Np_gv = this.Np_gv;
            retValue.Np_hv = this.Np_hv;
            retValue.Domofon = this.Domofon;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO tar (DTTAR, DTTAREND, KV, REMO, JILO, OTOP, STHV1, STHV2, STHV3, STHV4, STHV5, STHV6, STHV7, STHV8, STHV_K, STKN1, STKN2, STKN3, STKN4, STKN5, STKN7, STKN8, STKN_K, MUSOR, RADIO, TBO, STGV, GVS_C, ANT, CELS, PROCPENI, NDS, NP_GV, NP_HV, DOMOFON) VALUES (CTOD('{0}'), CTOD('{1}'), {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34})", Dttar == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dttar.Month, Dttar.Day, Dttar.Year), Dttarend == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dttarend.Month, Dttarend.Day, Dttarend.Year), Kv.ToString().Replace(',','.'), Remo.ToString().Replace(',','.'), Jilo.ToString().Replace(',','.'), Otop.ToString().Replace(',','.'), Sthv1.ToString().Replace(',','.'), Sthv2.ToString().Replace(',','.'), Sthv3.ToString().Replace(',','.'), Sthv4.ToString().Replace(',','.'), Sthv5.ToString().Replace(',','.'), Sthv6.ToString().Replace(',','.'), Sthv7.ToString().Replace(',','.'), Sthv8.ToString().Replace(',','.'), Sthv_k.ToString().Replace(',','.'), Stkn1.ToString().Replace(',','.'), Stkn2.ToString().Replace(',','.'), Stkn3.ToString().Replace(',','.'), Stkn4.ToString().Replace(',','.'), Stkn5.ToString().Replace(',','.'), Stkn7.ToString().Replace(',','.'), Stkn8.ToString().Replace(',','.'), Stkn_k.ToString().Replace(',','.'), Musor.ToString().Replace(',','.'), Radio.ToString().Replace(',','.'), Tbo.ToString().Replace(',','.'), Stgv.ToString().Replace(',','.'), Gvs_c.ToString().Replace(',','.'), Ant.ToString().Replace(',','.'), Cels.ToString().Replace(',','.'), Procpeni.ToString().Replace(',','.'), Nds.ToString().Replace(',','.'), Np_gv.ToString().Replace(',','.'), Np_hv.ToString().Replace(',','.'), Domofon.ToString().Replace(',','.'));
            return rs;
        }
    }
}
