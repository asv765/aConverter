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
    [TableName("f_lits.DBF")]
    public partial class F_litsRecord : AbstractRecord
    {
        private Int64 n_lits;
        // <summary>
        // N_LITS N(5)
        // </summary>
        [FieldName("N_LITS"), FieldType('N'), FieldWidth(5)]
        public Int64 N_lits
        {
            get { return n_lits; }
            set { CheckIntegerData("N_lits", value, 5); n_lits = value; }
        }

        private string fio;
        // <summary>
        // FIO C(20)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(20)]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 20); fio = value; }
        }

        private Int64 kst;
        // <summary>
        // KST N(4)
        // </summary>
        [FieldName("KST"), FieldType('N'), FieldWidth(4)]
        public Int64 Kst
        {
            get { return kst; }
            set { CheckIntegerData("Kst", value, 4); kst = value; }
        }

        private string street;
        // <summary>
        // STREET C(18)
        // </summary>
        [FieldName("STREET"), FieldType('C'), FieldWidth(18)]
        public string Street
        {
            get { return street; }
            set { CheckStringData("Street", value, 18); street = value; }
        }

        private Int64 n_dom;
        // <summary>
        // N_DOM N(4)
        // </summary>
        [FieldName("N_DOM"), FieldType('N'), FieldWidth(4)]
        public Int64 N_dom
        {
            get { return n_dom; }
            set { CheckIntegerData("N_dom", value, 4); n_dom = value; }
        }

        private string lit;
        // <summary>
        // LIT C(1)
        // </summary>
        [FieldName("LIT"), FieldType('C'), FieldWidth(1)]
        public string Lit
        {
            get { return lit; }
            set { CheckStringData("Lit", value, 1); lit = value; }
        }

        private Int64 n_kw;
        // <summary>
        // N_KW N(4)
        // </summary>
        [FieldName("N_KW"), FieldType('N'), FieldWidth(4)]
        public Int64 N_kw
        {
            get { return n_kw; }
            set { CheckIntegerData("N_kw", value, 4); n_kw = value; }
        }

        private decimal metr;
        // <summary>
        // METR N(6,2)
        // </summary>
        [FieldName("METR"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Metr
        {
            get { return metr; }
            set { CheckDecimalData("Metr", value, 6, 2); metr = value; }
        }

        private Int64 n_jil;
        // <summary>
        // N_JIL N(3)
        // </summary>
        [FieldName("N_JIL"), FieldType('N'), FieldWidth(3)]
        public Int64 N_jil
        {
            get { return n_jil; }
            set { CheckIntegerData("N_jil", value, 3); n_jil = value; }
        }

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

        private Int64 n_lgt1;
        // <summary>
        // N_LGT1 N(3)
        // </summary>
        [FieldName("N_LGT1"), FieldType('N'), FieldWidth(3)]
        public Int64 N_lgt1
        {
            get { return n_lgt1; }
            set { CheckIntegerData("N_lgt1", value, 3); n_lgt1 = value; }
        }

        private Int64 kod_l;
        // <summary>
        // KOD_L N(3)
        // </summary>
        [FieldName("KOD_L"), FieldType('N'), FieldWidth(3)]
        public Int64 Kod_l
        {
            get { return kod_l; }
            set { CheckIntegerData("Kod_l", value, 3); kod_l = value; }
        }

        private string ind1;
        // <summary>
        // IND1 C(2)
        // </summary>
        [FieldName("IND1"), FieldType('C'), FieldWidth(2)]
        public string Ind1
        {
            get { return ind1; }
            set { CheckStringData("Ind1", value, 2); ind1 = value; }
        }

        private string ind2;
        // <summary>
        // IND2 C(2)
        // </summary>
        [FieldName("IND2"), FieldType('C'), FieldWidth(2)]
        public string Ind2
        {
            get { return ind2; }
            set { CheckStringData("Ind2", value, 2); ind2 = value; }
        }

        private Int64 kod_l1;
        // <summary>
        // KOD_L1 N(3)
        // </summary>
        [FieldName("KOD_L1"), FieldType('N'), FieldWidth(3)]
        public Int64 Kod_l1
        {
            get { return kod_l1; }
            set { CheckIntegerData("Kod_l1", value, 3); kod_l1 = value; }
        }

        private Int64 n_kom;
        // <summary>
        // N_KOM N(3)
        // </summary>
        [FieldName("N_KOM"), FieldType('N'), FieldWidth(3)]
        public Int64 N_kom
        {
            get { return n_kom; }
            set { CheckIntegerData("N_kom", value, 3); n_kom = value; }
        }

        private Int64 n_lamp2;
        // <summary>
        // N_LAMP2 N(3)
        // </summary>
        [FieldName("N_LAMP2"), FieldType('N'), FieldWidth(3)]
        public Int64 N_lamp2
        {
            get { return n_lamp2; }
            set { CheckIntegerData("N_lamp2", value, 3); n_lamp2 = value; }
        }

        private Int64 shet_1;
        // <summary>
        // SHET_1 N(11)
        // </summary>
        [FieldName("SHET_1"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_1
        {
            get { return shet_1; }
            set { CheckIntegerData("Shet_1", value, 11); shet_1 = value; }
        }

        private Int64 sald;
        // <summary>
        // SALD N(6)
        // </summary>
        [FieldName("SALD"), FieldType('N'), FieldWidth(6)]
        public Int64 Sald
        {
            get { return sald; }
            set { CheckIntegerData("Sald", value, 6); sald = value; }
        }

        private Int64 n_ros1;
        // <summary>
        // N_ROS1 N(3)
        // </summary>
        [FieldName("N_ROS1"), FieldType('N'), FieldWidth(3)]
        public Int64 N_ros1
        {
            get { return n_ros1; }
            set { CheckIntegerData("N_ros1", value, 3); n_ros1 = value; }
        }

        private Int64 n_ros2;
        // <summary>
        // N_ROS2 N(3)
        // </summary>
        [FieldName("N_ROS2"), FieldType('N'), FieldWidth(3)]
        public Int64 N_ros2
        {
            get { return n_ros2; }
            set { CheckIntegerData("N_ros2", value, 3); n_ros2 = value; }
        }

        private string n_shet1;
        // <summary>
        // N_SHET1 C(26)
        // </summary>
        [FieldName("N_SHET1"), FieldType('C'), FieldWidth(26)]
        public string N_shet1
        {
            get { return n_shet1; }
            set { CheckStringData("N_shet1", value, 26); n_shet1 = value; }
        }

        private Int64 shet_2;
        // <summary>
        // SHET_2 N(11)
        // </summary>
        [FieldName("SHET_2"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_2
        {
            get { return shet_2; }
            set { CheckIntegerData("Shet_2", value, 11); shet_2 = value; }
        }

        private Int64 shet_3;
        // <summary>
        // SHET_3 N(11)
        // </summary>
        [FieldName("SHET_3"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_3
        {
            get { return shet_3; }
            set { CheckIntegerData("Shet_3", value, 11); shet_3 = value; }
        }

        private Int64 shet_4;
        // <summary>
        // SHET_4 N(11)
        // </summary>
        [FieldName("SHET_4"), FieldType('N'), FieldWidth(11)]
        public Int64 Shet_4
        {
            get { return shet_4; }
            set { CheckIntegerData("Shet_4", value, 11); shet_4 = value; }
        }

        private string n_shet2;
        // <summary>
        // N_SHET2 C(26)
        // </summary>
        [FieldName("N_SHET2"), FieldType('C'), FieldWidth(26)]
        public string N_shet2
        {
            get { return n_shet2; }
            set { CheckStringData("N_shet2", value, 26); n_shet2 = value; }
        }

        private string n_shet3;
        // <summary>
        // N_SHET3 C(26)
        // </summary>
        [FieldName("N_SHET3"), FieldType('C'), FieldWidth(26)]
        public string N_shet3
        {
            get { return n_shet3; }
            set { CheckStringData("N_shet3", value, 26); n_shet3 = value; }
        }

        private string n_shet4;
        // <summary>
        // N_SHET4 C(26)
        // </summary>
        [FieldName("N_SHET4"), FieldType('C'), FieldWidth(26)]
        public string N_shet4
        {
            get { return n_shet4; }
            set { CheckStringData("N_shet4", value, 26); n_shet4 = value; }
        }

        private Int64 kvar;
        // <summary>
        // KVAR N(3)
        // </summary>
        [FieldName("KVAR"), FieldType('N'), FieldWidth(3)]
        public Int64 Kvar
        {
            get { return kvar; }
            set { CheckIntegerData("Kvar", value, 3); kvar = value; }
        }

        private Int64 nai;
        // <summary>
        // NAI N(3)
        // </summary>
        [FieldName("NAI"), FieldType('N'), FieldWidth(3)]
        public Int64 Nai
        {
            get { return nai; }
            set { CheckIntegerData("Nai", value, 3); nai = value; }
        }

        private Int64 mus;
        // <summary>
        // MUS N(3)
        // </summary>
        [FieldName("MUS"), FieldType('N'), FieldWidth(3)]
        public Int64 Mus
        {
            get { return mus; }
            set { CheckIntegerData("Mus", value, 3); mus = value; }
        }

        private Int64 otop;
        // <summary>
        // OTOP N(3)
        // </summary>
        [FieldName("OTOP"), FieldType('N'), FieldWidth(3)]
        public Int64 Otop
        {
            get { return otop; }
            set { CheckIntegerData("Otop", value, 3); otop = value; }
        }

        private Int64 el1;
        // <summary>
        // EL1 N(3)
        // </summary>
        [FieldName("EL1"), FieldType('N'), FieldWidth(3)]
        public Int64 El1
        {
            get { return el1; }
            set { CheckIntegerData("El1", value, 3); el1 = value; }
        }

        private Int64 el2;
        // <summary>
        // EL2 N(3)
        // </summary>
        [FieldName("EL2"), FieldType('N'), FieldWidth(3)]
        public Int64 El2
        {
            get { return el2; }
            set { CheckIntegerData("El2", value, 3); el2 = value; }
        }

        private Int64 kanal;
        // <summary>
        // KANAL N(3)
        // </summary>
        [FieldName("KANAL"), FieldType('N'), FieldWidth(3)]
        public Int64 Kanal
        {
            get { return kanal; }
            set { CheckIntegerData("Kanal", value, 3); kanal = value; }
        }

        private Int64 watr;
        // <summary>
        // WATR N(3)
        // </summary>
        [FieldName("WATR"), FieldType('N'), FieldWidth(3)]
        public Int64 Watr
        {
            get { return watr; }
            set { CheckIntegerData("Watr", value, 3); watr = value; }
        }

        private Int64 g_wat;
        // <summary>
        // G_WAT N(3)
        // </summary>
        [FieldName("G_WAT"), FieldType('N'), FieldWidth(3)]
        public Int64 G_wat
        {
            get { return g_wat; }
            set { CheckIntegerData("G_wat", value, 3); g_wat = value; }
        }

        private string n_shet5;
        // <summary>
        // N_SHET5 C(11)
        // </summary>
        [FieldName("N_SHET5"), FieldType('C'), FieldWidth(11)]
        public string N_shet5
        {
            get { return n_shet5; }
            set { CheckStringData("N_shet5", value, 11); n_shet5 = value; }
        }

        private Int64 shet_5;
        // <summary>
        // SHET_5 N(6)
        // </summary>
        [FieldName("SHET_5"), FieldType('N'), FieldWidth(6)]
        public Int64 Shet_5
        {
            get { return shet_5; }
            set { CheckIntegerData("Shet_5", value, 6); shet_5 = value; }
        }

        private Int64 priv;
        // <summary>
        // PRIV N(2)
        // </summary>
        [FieldName("PRIV"), FieldType('N'), FieldWidth(2)]
        public Int64 Priv
        {
            get { return priv; }
            set { CheckIntegerData("Priv", value, 2); priv = value; }
        }

        private string dog;
        // <summary>
        // DOG C(1)
        // </summary>
        [FieldName("DOG"), FieldType('C'), FieldWidth(1)]
        public string Dog
        {
            get { return dog; }
            set { CheckStringData("Dog", value, 1); dog = value; }
        }

        private Int64 drob;
        // <summary>
        // DROB N(4)
        // </summary>
        [FieldName("DROB"), FieldType('N'), FieldWidth(4)]
        public Int64 Drob
        {
            get { return drob; }
            set { CheckIntegerData("Drob", value, 4); drob = value; }
        }

        private Int64 prin;
        // <summary>
        // PRIN N(2)
        // </summary>
        [FieldName("PRIN"), FieldType('N'), FieldWidth(2)]
        public Int64 Prin
        {
            get { return prin; }
            set { CheckIntegerData("Prin", value, 2); prin = value; }
        }

        private Int64 jiv;
        // <summary>
        // JIV N(3)
        // </summary>
        [FieldName("JIV"), FieldType('N'), FieldWidth(3)]
        public Int64 Jiv
        {
            get { return jiv; }
            set { CheckIntegerData("Jiv", value, 3); jiv = value; }
        }

        private string prim;
        // <summary>
        // PRIM C(10)
        // </summary>
        [FieldName("PRIM"), FieldType('C'), FieldWidth(10)]
        public string Prim
        {
            get { return prim; }
            set { CheckStringData("Prim", value, 10); prim = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("N_LITS")) N_lits = Convert.ToInt64(ADataRow["N_LITS"]); else N_lits = 0;
            if (ADataRow.Table.Columns.Contains("FIO")) Fio = ADataRow["FIO"].ToString(); else Fio = "";
            if (ADataRow.Table.Columns.Contains("KST")) Kst = Convert.ToInt64(ADataRow["KST"]); else Kst = 0;
            if (ADataRow.Table.Columns.Contains("STREET")) Street = ADataRow["STREET"].ToString(); else Street = "";
            if (ADataRow.Table.Columns.Contains("N_DOM")) N_dom = Convert.ToInt64(ADataRow["N_DOM"]); else N_dom = 0;
            if (ADataRow.Table.Columns.Contains("LIT")) Lit = ADataRow["LIT"].ToString(); else Lit = "";
            if (ADataRow.Table.Columns.Contains("N_KW")) N_kw = Convert.ToInt64(ADataRow["N_KW"]); else N_kw = 0;
            if (ADataRow.Table.Columns.Contains("METR")) Metr = Convert.ToDecimal(ADataRow["METR"]); else Metr = 0;
            if (ADataRow.Table.Columns.Contains("N_JIL")) N_jil = Convert.ToInt64(ADataRow["N_JIL"]); else N_jil = 0;
            if (ADataRow.Table.Columns.Contains("N_LGT")) N_lgt = Convert.ToInt64(ADataRow["N_LGT"]); else N_lgt = 0;
            if (ADataRow.Table.Columns.Contains("N_LGT1")) N_lgt1 = Convert.ToInt64(ADataRow["N_LGT1"]); else N_lgt1 = 0;
            if (ADataRow.Table.Columns.Contains("KOD_L")) Kod_l = Convert.ToInt64(ADataRow["KOD_L"]); else Kod_l = 0;
            if (ADataRow.Table.Columns.Contains("IND1")) Ind1 = ADataRow["IND1"].ToString(); else Ind1 = "";
            if (ADataRow.Table.Columns.Contains("IND2")) Ind2 = ADataRow["IND2"].ToString(); else Ind2 = "";
            if (ADataRow.Table.Columns.Contains("KOD_L1")) Kod_l1 = Convert.ToInt64(ADataRow["KOD_L1"]); else Kod_l1 = 0;
            if (ADataRow.Table.Columns.Contains("N_KOM")) N_kom = Convert.ToInt64(ADataRow["N_KOM"]); else N_kom = 0;
            if (ADataRow.Table.Columns.Contains("N_LAMP2")) N_lamp2 = Convert.ToInt64(ADataRow["N_LAMP2"]); else N_lamp2 = 0;
            if (ADataRow.Table.Columns.Contains("SHET_1")) Shet_1 = Convert.ToInt64(ADataRow["SHET_1"]); else Shet_1 = 0;
            if (ADataRow.Table.Columns.Contains("SALD")) Sald = Convert.ToInt64(ADataRow["SALD"]); else Sald = 0;
            if (ADataRow.Table.Columns.Contains("N_ROS1")) N_ros1 = Convert.ToInt64(ADataRow["N_ROS1"]); else N_ros1 = 0;
            if (ADataRow.Table.Columns.Contains("N_ROS2")) N_ros2 = Convert.ToInt64(ADataRow["N_ROS2"]); else N_ros2 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET1")) N_shet1 = ADataRow["N_SHET1"].ToString(); else N_shet1 = "";
            if (ADataRow.Table.Columns.Contains("SHET_2")) Shet_2 = Convert.ToInt64(ADataRow["SHET_2"]); else Shet_2 = 0;
            if (ADataRow.Table.Columns.Contains("SHET_3")) Shet_3 = Convert.ToInt64(ADataRow["SHET_3"]); else Shet_3 = 0;
            if (ADataRow.Table.Columns.Contains("SHET_4")) Shet_4 = Convert.ToInt64(ADataRow["SHET_4"]); else Shet_4 = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET2")) N_shet2 = ADataRow["N_SHET2"].ToString(); else N_shet2 = "";
            if (ADataRow.Table.Columns.Contains("N_SHET3")) N_shet3 = ADataRow["N_SHET3"].ToString(); else N_shet3 = "";
            if (ADataRow.Table.Columns.Contains("N_SHET4")) N_shet4 = ADataRow["N_SHET4"].ToString(); else N_shet4 = "";
            if (ADataRow.Table.Columns.Contains("KVAR")) Kvar = Convert.ToInt64(ADataRow["KVAR"]); else Kvar = 0;
            if (ADataRow.Table.Columns.Contains("NAI")) Nai = Convert.ToInt64(ADataRow["NAI"]); else Nai = 0;
            if (ADataRow.Table.Columns.Contains("MUS")) Mus = Convert.ToInt64(ADataRow["MUS"]); else Mus = 0;
            if (ADataRow.Table.Columns.Contains("OTOP")) Otop = Convert.ToInt64(ADataRow["OTOP"]); else Otop = 0;
            if (ADataRow.Table.Columns.Contains("EL1")) El1 = Convert.ToInt64(ADataRow["EL1"]); else El1 = 0;
            if (ADataRow.Table.Columns.Contains("EL2")) El2 = Convert.ToInt64(ADataRow["EL2"]); else El2 = 0;
            if (ADataRow.Table.Columns.Contains("KANAL")) Kanal = Convert.ToInt64(ADataRow["KANAL"]); else Kanal = 0;
            if (ADataRow.Table.Columns.Contains("WATR")) Watr = Convert.ToInt64(ADataRow["WATR"]); else Watr = 0;
            if (ADataRow.Table.Columns.Contains("G_WAT")) G_wat = Convert.ToInt64(ADataRow["G_WAT"]); else G_wat = 0;
            if (ADataRow.Table.Columns.Contains("N_SHET5")) N_shet5 = ADataRow["N_SHET5"].ToString(); else N_shet5 = "";
            if (ADataRow.Table.Columns.Contains("SHET_5")) Shet_5 = Convert.ToInt64(ADataRow["SHET_5"]); else Shet_5 = 0;
            if (ADataRow.Table.Columns.Contains("PRIV")) Priv = Convert.ToInt64(ADataRow["PRIV"]); else Priv = 0;
            if (ADataRow.Table.Columns.Contains("DOG")) Dog = ADataRow["DOG"].ToString(); else Dog = "";
            if (ADataRow.Table.Columns.Contains("DROB")) Drob = Convert.ToInt64(ADataRow["DROB"]); else Drob = 0;
            if (ADataRow.Table.Columns.Contains("PRIN")) Prin = Convert.ToInt64(ADataRow["PRIN"]); else Prin = 0;
            if (ADataRow.Table.Columns.Contains("JIV")) Jiv = Convert.ToInt64(ADataRow["JIV"]); else Jiv = 0;
            if (ADataRow.Table.Columns.Contains("PRIM")) Prim = ADataRow["PRIM"].ToString(); else Prim = "";
        }

        public override AbstractRecord Clone()
        {
            F_litsRecord retValue = new F_litsRecord();
            retValue.N_lits = this.N_lits;
            retValue.Fio = this.Fio;
            retValue.Kst = this.Kst;
            retValue.Street = this.Street;
            retValue.N_dom = this.N_dom;
            retValue.Lit = this.Lit;
            retValue.N_kw = this.N_kw;
            retValue.Metr = this.Metr;
            retValue.N_jil = this.N_jil;
            retValue.N_lgt = this.N_lgt;
            retValue.N_lgt1 = this.N_lgt1;
            retValue.Kod_l = this.Kod_l;
            retValue.Ind1 = this.Ind1;
            retValue.Ind2 = this.Ind2;
            retValue.Kod_l1 = this.Kod_l1;
            retValue.N_kom = this.N_kom;
            retValue.N_lamp2 = this.N_lamp2;
            retValue.Shet_1 = this.Shet_1;
            retValue.Sald = this.Sald;
            retValue.N_ros1 = this.N_ros1;
            retValue.N_ros2 = this.N_ros2;
            retValue.N_shet1 = this.N_shet1;
            retValue.Shet_2 = this.Shet_2;
            retValue.Shet_3 = this.Shet_3;
            retValue.Shet_4 = this.Shet_4;
            retValue.N_shet2 = this.N_shet2;
            retValue.N_shet3 = this.N_shet3;
            retValue.N_shet4 = this.N_shet4;
            retValue.Kvar = this.Kvar;
            retValue.Nai = this.Nai;
            retValue.Mus = this.Mus;
            retValue.Otop = this.Otop;
            retValue.El1 = this.El1;
            retValue.El2 = this.El2;
            retValue.Kanal = this.Kanal;
            retValue.Watr = this.Watr;
            retValue.G_wat = this.G_wat;
            retValue.N_shet5 = this.N_shet5;
            retValue.Shet_5 = this.Shet_5;
            retValue.Priv = this.Priv;
            retValue.Dog = this.Dog;
            retValue.Drob = this.Drob;
            retValue.Prin = this.Prin;
            retValue.Jiv = this.Jiv;
            retValue.Prim = this.Prim;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO f_lits (N_LITS, FIO, KST, STREET, N_DOM, LIT, N_KW, METR, N_JIL, N_LGT, N_LGT1, KOD_L, IND1, IND2, KOD_L1, N_KOM, N_LAMP2, SHET_1, SALD, N_ROS1, N_ROS2, N_SHET1, SHET_2, SHET_3, SHET_4, N_SHET2, N_SHET3, N_SHET4, KVAR, NAI, MUS, OTOP, EL1, EL2, KANAL, WATR, G_WAT, N_SHET5, SHET_5, PRIV, DOG, DROB, PRIN, JIV, PRIM) VALUES ({0}, '{1}', {2}, '{3}', {4}, '{5}', {6}, {7}, {8}, {9}, {10}, {11}, '{12}', '{13}', {14}, {15}, {16}, {17}, {18}, {19}, {20}, '{21}', {22}, {23}, {24}, '{25}', '{26}', '{27}', {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, '{37}', {38}, {39}, '{40}', {41}, {42}, {43}, '{44}')", N_lits.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Kst.ToString(), String.IsNullOrEmpty(Street) ? "" : Street.Trim(), N_dom.ToString(), String.IsNullOrEmpty(Lit) ? "" : Lit.Trim(), N_kw.ToString(), Metr.ToString().Replace(',', '.'), N_jil.ToString(), N_lgt.ToString(), N_lgt1.ToString(), Kod_l.ToString(), String.IsNullOrEmpty(Ind1) ? "" : Ind1.Trim(), String.IsNullOrEmpty(Ind2) ? "" : Ind2.Trim(), Kod_l1.ToString(), N_kom.ToString(), N_lamp2.ToString(), Shet_1.ToString(), Sald.ToString(), N_ros1.ToString(), N_ros2.ToString(), String.IsNullOrEmpty(N_shet1) ? "" : N_shet1.Trim(), Shet_2.ToString(), Shet_3.ToString(), Shet_4.ToString(), String.IsNullOrEmpty(N_shet2) ? "" : N_shet2.Trim(), String.IsNullOrEmpty(N_shet3) ? "" : N_shet3.Trim(), String.IsNullOrEmpty(N_shet4) ? "" : N_shet4.Trim(), Kvar.ToString(), Nai.ToString(), Mus.ToString(), Otop.ToString(), El1.ToString(), El2.ToString(), Kanal.ToString(), Watr.ToString(), G_wat.ToString(), String.IsNullOrEmpty(N_shet5) ? "" : N_shet5.Trim(), Shet_5.ToString(), Priv.ToString(), String.IsNullOrEmpty(Dog) ? "" : Dog.Trim(), Drob.ToString(), Prin.ToString(), Jiv.ToString(), String.IsNullOrEmpty(Prim) ? "" : Prim.Trim());
            return rs;
        }
    }
}
