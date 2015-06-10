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
    [TableName("HmCount.DBF")]
    public partial class HmcountRecord: AbstractRecord
    {
        private Int64 coddm;
        // <summary>
        // CODDM N(6)
        // </summary>
        [FieldName("CODDM"), FieldType('N'), FieldWidth(6)]
        public Int64 Coddm
        {
            get { return coddm; }
            set { CheckIntegerData("Coddm", value, 6); coddm = value; }
        }

        private Int64 usluga;
        // <summary>
        // USLUGA N(4)
        // </summary>
        [FieldName("USLUGA"), FieldType('N'), FieldWidth(4)]
        public Int64 Usluga
        {
            get { return usluga; }
            set { CheckIntegerData("Usluga", value, 4); usluga = value; }
        }

        private Int64 code;
        // <summary>
        // CODE N(6)
        // </summary>
        [FieldName("CODE"), FieldType('N'), FieldWidth(6)]
        public Int64 Code
        {
            get { return code; }
            set { CheckIntegerData("Code", value, 6); code = value; }
        }

        private Int64 scheme;
        // <summary>
        // SCHEME N(3)
        // </summary>
        [FieldName("SCHEME"), FieldType('N'), FieldWidth(3)]
        public Int64 Scheme
        {
            get { return scheme; }
            set { CheckIntegerData("Scheme", value, 3); scheme = value; }
        }

        private string sn;
        // <summary>
        // SN C(20)
        // </summary>
        [FieldName("SN"), FieldType('C'), FieldWidth(20)]
        public string Sn
        {
            get { return sn; }
            set { CheckStringData("Sn", value, 20); sn = value; }
        }

        private string type;
        // <summary>
        // TYPE C(20)
        // </summary>
        [FieldName("TYPE"), FieldType('C'), FieldWidth(20)]
        public string Type
        {
            get { return type; }
            set { CheckStringData("Type", value, 20); type = value; }
        }

        private Int64 koef_trans;
        // <summary>
        // KOEF_TRANS N(4)
        // </summary>
        [FieldName("KOEF_TRANS"), FieldType('N'), FieldWidth(4)]
        public Int64 Koef_trans
        {
            get { return koef_trans; }
            set { CheckIntegerData("Koef_trans", value, 4); koef_trans = value; }
        }

        private decimal koef_lose;
        // <summary>
        // KOEF_LOSE N(6,2)
        // </summary>
        [FieldName("KOEF_LOSE"), FieldType('N'), FieldWidth(6), FieldDec(2)]
        public decimal Koef_lose
        {
            get { return koef_lose; }
            set { CheckDecimalData("Koef_lose", value, 6, 2); koef_lose = value; }
        }

        private string producer;
        // <summary>
        // PRODUCER C(50)
        // </summary>
        [FieldName("PRODUCER"), FieldType('C'), FieldWidth(50)]
        public string Producer
        {
            get { return producer; }
            set { CheckStringData("Producer", value, 50); producer = value; }
        }

        private string place;
        // <summary>
        // PLACE C(20)
        // </summary>
        [FieldName("PLACE"), FieldType('C'), FieldWidth(20)]
        public string Place
        {
            get { return place; }
            set { CheckStringData("Place", value, 20); place = value; }
        }

        private DateTime dats;
        // <summary>
        // DATS D(8)
        // </summary>
        [FieldName("DATS"), FieldType('D'), FieldWidth(8)]
        public DateTime Dats
        {
            get { return dats; }
            set {  dats = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set {  date = value; }
        }

        private string reasoff;
        // <summary>
        // REASOFF C(20)
        // </summary>
        [FieldName("REASOFF"), FieldType('C'), FieldWidth(20)]
        public string Reasoff
        {
            get { return reasoff; }
            set { CheckStringData("Reasoff", value, 20); reasoff = value; }
        }

        private Int64 sc_w;
        // <summary>
        // SC_W N(3)
        // </summary>
        [FieldName("SC_W"), FieldType('N'), FieldWidth(3)]
        public Int64 Sc_w
        {
            get { return sc_w; }
            set { CheckIntegerData("Sc_w", value, 3); sc_w = value; }
        }

        private Int64 sc_dec;
        // <summary>
        // SC_DEC N(3)
        // </summary>
        [FieldName("SC_DEC"), FieldType('N'), FieldWidth(3)]
        public Int64 Sc_dec
        {
            get { return sc_dec; }
            set { CheckIntegerData("Sc_dec", value, 3); sc_dec = value; }
        }

        private Int64 sc_dir;
        // <summary>
        // SC_DIR N(2)
        // </summary>
        [FieldName("SC_DIR"), FieldType('N'), FieldWidth(2)]
        public Int64 Sc_dir
        {
            get { return sc_dir; }
            set { CheckIntegerData("Sc_dir", value, 2); sc_dir = value; }
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

        private Int64 oper;
        // <summary>
        // OPER N(4)
        // </summary>
        [FieldName("OPER"), FieldType('N'), FieldWidth(4)]
        public Int64 Oper
        {
            get { return oper; }
            set { CheckIntegerData("Oper", value, 4); oper = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("CODDM")) Coddm = Convert.ToInt64(ADataRow["CODDM"]); else Coddm = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("CODE")) Code = Convert.ToInt64(ADataRow["CODE"]); else Code = 0;
            if (ADataRow.Table.Columns.Contains("SCHEME")) Scheme = Convert.ToInt64(ADataRow["SCHEME"]); else Scheme = 0;
            if (ADataRow.Table.Columns.Contains("SN")) Sn = ADataRow["SN"].ToString(); else Sn = "";
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("KOEF_TRANS")) Koef_trans = Convert.ToInt64(ADataRow["KOEF_TRANS"]); else Koef_trans = 0;
            if (ADataRow.Table.Columns.Contains("KOEF_LOSE")) Koef_lose = Convert.ToDecimal(ADataRow["KOEF_LOSE"]); else Koef_lose = 0;
            if (ADataRow.Table.Columns.Contains("PRODUCER")) Producer = ADataRow["PRODUCER"].ToString(); else Producer = "";
            if (ADataRow.Table.Columns.Contains("PLACE")) Place = ADataRow["PLACE"].ToString(); else Place = "";
            if (ADataRow.Table.Columns.Contains("DATS")) Dats = Convert.ToDateTime(ADataRow["DATS"]); else Dats = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("REASOFF")) Reasoff = ADataRow["REASOFF"].ToString(); else Reasoff = "";
            if (ADataRow.Table.Columns.Contains("SC_W")) Sc_w = Convert.ToInt64(ADataRow["SC_W"]); else Sc_w = 0;
            if (ADataRow.Table.Columns.Contains("SC_DEC")) Sc_dec = Convert.ToInt64(ADataRow["SC_DEC"]); else Sc_dec = 0;
            if (ADataRow.Table.Columns.Contains("SC_DIR")) Sc_dir = Convert.ToInt64(ADataRow["SC_DIR"]); else Sc_dir = 0;
            if (ADataRow.Table.Columns.Contains("EDIZ")) Ediz = Convert.ToInt64(ADataRow["EDIZ"]); else Ediz = 0;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
        }
        
        public override AbstractRecord Clone()
        {
            HmcountRecord retValue = new HmcountRecord();
            retValue.Coddm = this.Coddm;
            retValue.Usluga = this.Usluga;
            retValue.Code = this.Code;
            retValue.Scheme = this.Scheme;
            retValue.Sn = this.Sn;
            retValue.Type = this.Type;
            retValue.Koef_trans = this.Koef_trans;
            retValue.Koef_lose = this.Koef_lose;
            retValue.Producer = this.Producer;
            retValue.Place = this.Place;
            retValue.Dats = this.Dats;
            retValue.Date = this.Date;
            retValue.Reasoff = this.Reasoff;
            retValue.Sc_w = this.Sc_w;
            retValue.Sc_dec = this.Sc_dec;
            retValue.Sc_dir = this.Sc_dir;
            retValue.Ediz = this.Ediz;
            retValue.Oper = this.Oper;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO HmCount (CODDM, USLUGA, CODE, SCHEME, SN, TYPE, KOEF_TRANS, KOEF_LOSE, PRODUCER, PLACE, DATS, DATE, REASOFF, SC_W, SC_DEC, SC_DIR, EDIZ, OPER) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}', {6}, {7}, '{8}', '{9}', CTOD('{10}'), CTOD('{11}'), '{12}', {13}, {14}, {15}, {16}, {17})", Coddm.ToString(), Usluga.ToString(), Code.ToString(), Scheme.ToString(), String.IsNullOrEmpty(Sn) ? "" : Sn.Trim(), String.IsNullOrEmpty(Type) ? "" : Type.Trim(), Koef_trans.ToString(), Koef_lose.ToString().Replace(',','.'), String.IsNullOrEmpty(Producer) ? "" : Producer.Trim(), String.IsNullOrEmpty(Place) ? "" : Place.Trim(), Dats == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dats.Month, Dats.Day, Dats.Year), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Reasoff) ? "" : Reasoff.Trim(), Sc_w.ToString(), Sc_dec.ToString(), Sc_dir.ToString(), Ediz.ToString(), Oper.ToString());
            return rs;
        }
    }
}
