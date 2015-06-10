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
    [TableName("USL.DBF")]
    public partial class UslRecord: AbstractRecord
    {
        private Int64 cod;
        // <summary>
        // COD N(4)
        // </summary>
        [FieldName("COD"), FieldType('N'), FieldWidth(4)]
        public Int64 Cod
        {
            get { return cod; }
            set { CheckIntegerData("Cod", value, 4); cod = value; }
        }

        private string name;
        // <summary>
        // NAME C(40)
        // </summary>
        [FieldName("NAME"), FieldType('C'), FieldWidth(40)]
        public string Name
        {
            get { return name; }
            set { CheckStringData("Name", value, 40); name = value; }
        }

        private string short_name;
        // <summary>
        // SHORT_NAME C(20)
        // </summary>
        [FieldName("SHORT_NAME"), FieldType('C'), FieldWidth(20)]
        public string Short_name
        {
            get { return short_name; }
            set { CheckStringData("Short_name", value, 20); short_name = value; }
        }

        private Int64 flag_all;
        // <summary>
        // FLAG_ALL N(2)
        // </summary>
        [FieldName("FLAG_ALL"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_all
        {
            get { return flag_all; }
            set { CheckIntegerData("Flag_all", value, 2); flag_all = value; }
        }

        private Int64 flag_sal;
        // <summary>
        // FLAG_SAL N(2)
        // </summary>
        [FieldName("FLAG_SAL"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_sal
        {
            get { return flag_sal; }
            set { CheckIntegerData("Flag_sal", value, 2); flag_sal = value; }
        }

        private Int64 flag_sum;
        // <summary>
        // FLAG_SUM N(2)
        // </summary>
        [FieldName("FLAG_SUM"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_sum
        {
            get { return flag_sum; }
            set { CheckIntegerData("Flag_sum", value, 2); flag_sum = value; }
        }

        private Int64 flag_usl;
        // <summary>
        // FLAG_USL N(2)
        // </summary>
        [FieldName("FLAG_USL"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_usl
        {
            get { return flag_usl; }
            set { CheckIntegerData("Flag_usl", value, 2); flag_usl = value; }
        }

        private Int64 flag_r;
        // <summary>
        // FLAG_R N(2)
        // </summary>
        [FieldName("FLAG_R"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_r
        {
            get { return flag_r; }
            set { CheckIntegerData("Flag_r", value, 2); flag_r = value; }
        }

        private Int64 flag_l;
        // <summary>
        // FLAG_L N(2)
        // </summary>
        [FieldName("FLAG_L"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_l
        {
            get { return flag_l; }
            set { CheckIntegerData("Flag_l", value, 2); flag_l = value; }
        }

        private Int64 flag_kom;
        // <summary>
        // FLAG_KOM N(2)
        // </summary>
        [FieldName("FLAG_KOM"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_kom
        {
            get { return flag_kom; }
            set { CheckIntegerData("Flag_kom", value, 2); flag_kom = value; }
        }

        private Int64 flag_jil;
        // <summary>
        // FLAG_JIL N(2)
        // </summary>
        [FieldName("FLAG_JIL"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_jil
        {
            get { return flag_jil; }
            set { CheckIntegerData("Flag_jil", value, 2); flag_jil = value; }
        }

        private Int64 flag_tep;
        // <summary>
        // FLAG_TEP N(2)
        // </summary>
        [FieldName("FLAG_TEP"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_tep
        {
            get { return flag_tep; }
            set { CheckIntegerData("Flag_tep", value, 2); flag_tep = value; }
        }

        private Int64 flag_c;
        // <summary>
        // FLAG_C N(2)
        // </summary>
        [FieldName("FLAG_C"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_c
        {
            get { return flag_c; }
            set { CheckIntegerData("Flag_c", value, 2); flag_c = value; }
        }

        private Int64 flag_pen;
        // <summary>
        // FLAG_PEN N(2)
        // </summary>
        [FieldName("FLAG_PEN"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_pen
        {
            get { return flag_pen; }
            set { CheckIntegerData("Flag_pen", value, 2); flag_pen = value; }
        }

        private Int64 flag_lg;
        // <summary>
        // FLAG_LG N(2)
        // </summary>
        [FieldName("FLAG_LG"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_lg
        {
            get { return flag_lg; }
            set { CheckIntegerData("Flag_lg", value, 2); flag_lg = value; }
        }

        private Int64 flag_sub;
        // <summary>
        // FLAG_SUB N(2)
        // </summary>
        [FieldName("FLAG_SUB"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_sub
        {
            get { return flag_sub; }
            set { CheckIntegerData("Flag_sub", value, 2); flag_sub = value; }
        }

        private Int64 vardel;
        // <summary>
        // VARDEL N(3)
        // </summary>
        [FieldName("VARDEL"), FieldType('N'), FieldWidth(3)]
        public Int64 Vardel
        {
            get { return vardel; }
            set { CheckIntegerData("Vardel", value, 3); vardel = value; }
        }

        private Int64 for_ro;
        // <summary>
        // FOR_RO N(3)
        // </summary>
        [FieldName("FOR_RO"), FieldType('N'), FieldWidth(3)]
        public Int64 For_ro
        {
            get { return for_ro; }
            set { CheckIntegerData("For_ro", value, 3); for_ro = value; }
        }

        private Int64 for_rz;
        // <summary>
        // FOR_RZ N(3)
        // </summary>
        [FieldName("FOR_RZ"), FieldType('N'), FieldWidth(3)]
        public Int64 For_rz
        {
            get { return for_rz; }
            set { CheckIntegerData("For_rz", value, 3); for_rz = value; }
        }

        private Int64 codu;
        // <summary>
        // CODU N(4)
        // </summary>
        [FieldName("CODU"), FieldType('N'), FieldWidth(4)]
        public Int64 Codu
        {
            get { return codu; }
            set { CheckIntegerData("Codu", value, 4); codu = value; }
        }

        private string spi_us;
        // <summary>
        // SPI_US C(70)
        // </summary>
        [FieldName("SPI_US"), FieldType('C'), FieldWidth(70)]
        public string Spi_us
        {
            get { return spi_us; }
            set { CheckStringData("Spi_us", value, 70); spi_us = value; }
        }

        private string formula;
        // <summary>
        // FORMULA M
        // </summary>
        [FieldName("FORMULA"), FieldType('M')]
        public string Formula
        {
            get { return formula; }
            set { CheckStringData("Formula", value, 0); formula = value; }
        }

        private Int64 cod_lgot;
        // <summary>
        // COD_LGOT N(4)
        // </summary>
        [FieldName("COD_LGOT"), FieldType('N'), FieldWidth(4)]
        public Int64 Cod_lgot
        {
            get { return cod_lgot; }
            set { CheckIntegerData("Cod_lgot", value, 4); cod_lgot = value; }
        }

        private Int64 npp;
        // <summary>
        // NPP N(4)
        // </summary>
        [FieldName("NPP"), FieldType('N'), FieldWidth(4)]
        public Int64 Npp
        {
            get { return npp; }
            set { CheckIntegerData("Npp", value, 4); npp = value; }
        }

        private Int64 ediz;
        // <summary>
        // EDIZ N(4)
        // </summary>
        [FieldName("EDIZ"), FieldType('N'), FieldWidth(4)]
        public Int64 Ediz
        {
            get { return ediz; }
            set { CheckIntegerData("Ediz", value, 4); ediz = value; }
        }

        private Int64 flagon;
        // <summary>
        // FLAGON N(2)
        // </summary>
        [FieldName("FLAGON"), FieldType('N'), FieldWidth(2)]
        public Int64 Flagon
        {
            get { return flagon; }
            set { CheckIntegerData("Flagon", value, 2); flagon = value; }
        }

        private Int64 flag_kkm;
        // <summary>
        // FLAG_KKM N(2)
        // </summary>
        [FieldName("FLAG_KKM"), FieldType('N'), FieldWidth(2)]
        public Int64 Flag_kkm
        {
            get { return flag_kkm; }
            set { CheckIntegerData("Flag_kkm", value, 2); flag_kkm = value; }
        }

        private Int64 codn;
        // <summary>
        // CODN N(13)
        // </summary>
        [FieldName("CODN"), FieldType('N'), FieldWidth(13)]
        public Int64 Codn
        {
            get { return codn; }
            set { CheckIntegerData("Codn", value, 13); codn = value; }
        }

        private string spi_us_per;
        // <summary>
        // SPI_US_PER C(70)
        // </summary>
        [FieldName("SPI_US_PER"), FieldType('C'), FieldWidth(70)]
        public string Spi_us_per
        {
            get { return spi_us_per; }
            set { CheckStringData("Spi_us_per", value, 70); spi_us_per = value; }
        }

        private bool serv_sber;
        // <summary>
        // SERV_SBER L(1)
        // </summary>
        [FieldName("SERV_SBER"), FieldType('L'), FieldWidth(1)]
        public bool Serv_sber
        {
            get { return serv_sber; }
            set {  serv_sber = value; }
        }

        private Int64 cod_sber;
        // <summary>
        // COD_SBER N(13)
        // </summary>
        [FieldName("COD_SBER"), FieldType('N'), FieldWidth(13)]
        public Int64 Cod_sber
        {
            get { return cod_sber; }
            set { CheckIntegerData("Cod_sber", value, 13); cod_sber = value; }
        }

        private Int64 pu_year;
        // <summary>
        // PU_YEAR N(6)
        // </summary>
        [FieldName("PU_YEAR"), FieldType('N'), FieldWidth(6)]
        public Int64 Pu_year
        {
            get { return pu_year; }
            set { CheckIntegerData("Pu_year", value, 6); pu_year = value; }
        }

        private Int64 pu_month;
        // <summary>
        // PU_MONTH N(6)
        // </summary>
        [FieldName("PU_MONTH"), FieldType('N'), FieldWidth(6)]
        public Int64 Pu_month
        {
            get { return pu_month; }
            set { CheckIntegerData("Pu_month", value, 6); pu_month = value; }
        }

        private Int64 type;
        // <summary>
        // TYPE N(2)
        // </summary>
        [FieldName("TYPE"), FieldType('N'), FieldWidth(2)]
        public Int64 Type
        {
            get { return type; }
            set { CheckIntegerData("Type", value, 2); type = value; }
        }

        private Int64 mop_serv;
        // <summary>
        // MOP_SERV N(4)
        // </summary>
        [FieldName("MOP_SERV"), FieldType('N'), FieldWidth(4)]
        public Int64 Mop_serv
        {
            get { return mop_serv; }
            set { CheckIntegerData("Mop_serv", value, 4); mop_serv = value; }
        }

        private Int64 main_serv;
        // <summary>
        // MAIN_SERV N(4)
        // </summary>
        [FieldName("MAIN_SERV"), FieldType('N'), FieldWidth(4)]
        public Int64 Main_serv
        {
            get { return main_serv; }
            set { CheckIntegerData("Main_serv", value, 4); main_serv = value; }
        }

        private Int64 cod_oookp;
        // <summary>
        // COD_OOOKP N(13)
        // </summary>
        [FieldName("COD_OOOKP"), FieldType('N'), FieldWidth(13)]
        public Int64 Cod_oookp
        {
            get { return cod_oookp; }
            set { CheckIntegerData("Cod_oookp", value, 13); cod_oookp = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("COD")) Cod = Convert.ToInt64(ADataRow["COD"]); else Cod = 0;
            if (ADataRow.Table.Columns.Contains("NAME")) Name = ADataRow["NAME"].ToString(); else Name = "";
            if (ADataRow.Table.Columns.Contains("SHORT_NAME")) Short_name = ADataRow["SHORT_NAME"].ToString(); else Short_name = "";
            if (ADataRow.Table.Columns.Contains("FLAG_ALL")) Flag_all = Convert.ToInt64(ADataRow["FLAG_ALL"]); else Flag_all = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_SAL")) Flag_sal = Convert.ToInt64(ADataRow["FLAG_SAL"]); else Flag_sal = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_SUM")) Flag_sum = Convert.ToInt64(ADataRow["FLAG_SUM"]); else Flag_sum = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_USL")) Flag_usl = Convert.ToInt64(ADataRow["FLAG_USL"]); else Flag_usl = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_R")) Flag_r = Convert.ToInt64(ADataRow["FLAG_R"]); else Flag_r = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_L")) Flag_l = Convert.ToInt64(ADataRow["FLAG_L"]); else Flag_l = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_KOM")) Flag_kom = Convert.ToInt64(ADataRow["FLAG_KOM"]); else Flag_kom = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_JIL")) Flag_jil = Convert.ToInt64(ADataRow["FLAG_JIL"]); else Flag_jil = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_TEP")) Flag_tep = Convert.ToInt64(ADataRow["FLAG_TEP"]); else Flag_tep = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_C")) Flag_c = Convert.ToInt64(ADataRow["FLAG_C"]); else Flag_c = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_PEN")) Flag_pen = Convert.ToInt64(ADataRow["FLAG_PEN"]); else Flag_pen = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_LG")) Flag_lg = Convert.ToInt64(ADataRow["FLAG_LG"]); else Flag_lg = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_SUB")) Flag_sub = Convert.ToInt64(ADataRow["FLAG_SUB"]); else Flag_sub = 0;
            if (ADataRow.Table.Columns.Contains("VARDEL")) Vardel = Convert.ToInt64(ADataRow["VARDEL"]); else Vardel = 0;
            if (ADataRow.Table.Columns.Contains("FOR_RO")) For_ro = Convert.ToInt64(ADataRow["FOR_RO"]); else For_ro = 0;
            if (ADataRow.Table.Columns.Contains("FOR_RZ")) For_rz = Convert.ToInt64(ADataRow["FOR_RZ"]); else For_rz = 0;
            if (ADataRow.Table.Columns.Contains("CODU")) Codu = Convert.ToInt64(ADataRow["CODU"]); else Codu = 0;
            if (ADataRow.Table.Columns.Contains("SPI_US")) Spi_us = ADataRow["SPI_US"].ToString(); else Spi_us = "";
            if (ADataRow.Table.Columns.Contains("FORMULA")) Formula = ADataRow["FORMULA"].ToString(); else Formula = "";
            if (ADataRow.Table.Columns.Contains("COD_LGOT")) Cod_lgot = Convert.ToInt64(ADataRow["COD_LGOT"]); else Cod_lgot = 0;
            if (ADataRow.Table.Columns.Contains("NPP")) Npp = Convert.ToInt64(ADataRow["NPP"]); else Npp = 0;
            if (ADataRow.Table.Columns.Contains("EDIZ")) Ediz = Convert.ToInt64(ADataRow["EDIZ"]); else Ediz = 0;
            if (ADataRow.Table.Columns.Contains("FLAGON")) Flagon = Convert.ToInt64(ADataRow["FLAGON"]); else Flagon = 0;
            if (ADataRow.Table.Columns.Contains("FLAG_KKM")) Flag_kkm = Convert.ToInt64(ADataRow["FLAG_KKM"]); else Flag_kkm = 0;
            if (ADataRow.Table.Columns.Contains("CODN")) Codn = Convert.ToInt64(ADataRow["CODN"]); else Codn = 0;
            if (ADataRow.Table.Columns.Contains("SPI_US_PER")) Spi_us_per = ADataRow["SPI_US_PER"].ToString(); else Spi_us_per = "";
            if (ADataRow.Table.Columns.Contains("SERV_SBER")) Serv_sber = ADataRow["SERV_SBER"].ToString() == "True" ? true : false; else Serv_sber = false;
            if (ADataRow.Table.Columns.Contains("COD_SBER")) Cod_sber = Convert.ToInt64(ADataRow["COD_SBER"]); else Cod_sber = 0;
            if (ADataRow.Table.Columns.Contains("PU_YEAR")) Pu_year = Convert.ToInt64(ADataRow["PU_YEAR"]); else Pu_year = 0;
            if (ADataRow.Table.Columns.Contains("PU_MONTH")) Pu_month = Convert.ToInt64(ADataRow["PU_MONTH"]); else Pu_month = 0;
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = Convert.ToInt64(ADataRow["TYPE"]); else Type = 0;
            if (ADataRow.Table.Columns.Contains("MOP_SERV")) Mop_serv = Convert.ToInt64(ADataRow["MOP_SERV"]); else Mop_serv = 0;
            if (ADataRow.Table.Columns.Contains("MAIN_SERV")) Main_serv = Convert.ToInt64(ADataRow["MAIN_SERV"]); else Main_serv = 0;
            if (ADataRow.Table.Columns.Contains("COD_OOOKP")) Cod_oookp = Convert.ToInt64(ADataRow["COD_OOOKP"]); else Cod_oookp = 0;
        }
        
        public override AbstractRecord Clone()
        {
            UslRecord retValue = new UslRecord();
            retValue.Cod = this.Cod;
            retValue.Name = this.Name;
            retValue.Short_name = this.Short_name;
            retValue.Flag_all = this.Flag_all;
            retValue.Flag_sal = this.Flag_sal;
            retValue.Flag_sum = this.Flag_sum;
            retValue.Flag_usl = this.Flag_usl;
            retValue.Flag_r = this.Flag_r;
            retValue.Flag_l = this.Flag_l;
            retValue.Flag_kom = this.Flag_kom;
            retValue.Flag_jil = this.Flag_jil;
            retValue.Flag_tep = this.Flag_tep;
            retValue.Flag_c = this.Flag_c;
            retValue.Flag_pen = this.Flag_pen;
            retValue.Flag_lg = this.Flag_lg;
            retValue.Flag_sub = this.Flag_sub;
            retValue.Vardel = this.Vardel;
            retValue.For_ro = this.For_ro;
            retValue.For_rz = this.For_rz;
            retValue.Codu = this.Codu;
            retValue.Spi_us = this.Spi_us;
            retValue.Formula = this.Formula;
            retValue.Cod_lgot = this.Cod_lgot;
            retValue.Npp = this.Npp;
            retValue.Ediz = this.Ediz;
            retValue.Flagon = this.Flagon;
            retValue.Flag_kkm = this.Flag_kkm;
            retValue.Codn = this.Codn;
            retValue.Spi_us_per = this.Spi_us_per;
            retValue.Serv_sber = this.Serv_sber;
            retValue.Cod_sber = this.Cod_sber;
            retValue.Pu_year = this.Pu_year;
            retValue.Pu_month = this.Pu_month;
            retValue.Type = this.Type;
            retValue.Mop_serv = this.Mop_serv;
            retValue.Main_serv = this.Main_serv;
            retValue.Cod_oookp = this.Cod_oookp;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO USL (COD, NAME, SHORT_NAME, FLAG_ALL, FLAG_SAL, FLAG_SUM, FLAG_USL, FLAG_R, FLAG_L, FLAG_KOM, FLAG_JIL, FLAG_TEP, FLAG_C, FLAG_PEN, FLAG_LG, FLAG_SUB, VARDEL, FOR_RO, FOR_RZ, CODU, SPI_US, FORMULA, COD_LGOT, NPP, EDIZ, FLAGON, FLAG_KKM, CODN, SPI_US_PER, SERV_SBER, COD_SBER, PU_YEAR, PU_MONTH, TYPE, MOP_SERV, MAIN_SERV, COD_OOOKP) VALUES ({0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, '{20}', '{21}', {22}, {23}, {24}, {25}, {26}, {27}, '{28}', {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36})", Cod.ToString(), String.IsNullOrEmpty(Name) ? "" : Name.Trim(), String.IsNullOrEmpty(Short_name) ? "" : Short_name.Trim(), Flag_all.ToString(), Flag_sal.ToString(), Flag_sum.ToString(), Flag_usl.ToString(), Flag_r.ToString(), Flag_l.ToString(), Flag_kom.ToString(), Flag_jil.ToString(), Flag_tep.ToString(), Flag_c.ToString(), Flag_pen.ToString(), Flag_lg.ToString(), Flag_sub.ToString(), Vardel.ToString(), For_ro.ToString(), For_rz.ToString(), Codu.ToString(), String.IsNullOrEmpty(Spi_us) ? "" : Spi_us.Trim(), String.IsNullOrEmpty(Formula) ? "" : Formula.Trim(), Cod_lgot.ToString(), Npp.ToString(), Ediz.ToString(), Flagon.ToString(), Flag_kkm.ToString(), Codn.ToString(), String.IsNullOrEmpty(Spi_us_per) ? "" : Spi_us_per.Trim(), (Serv_sber ? 0 : 1 ), Cod_sber.ToString(), Pu_year.ToString(), Pu_month.ToString(), Type.ToString(), Mop_serv.ToString(), Main_serv.ToString(), Cod_oookp.ToString());
            return rs;
        }
    }
}
