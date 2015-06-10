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
    [TableName("COUNTERS.DBF")]
    public partial class CountersRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(11)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(11)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 11); ls = value; }
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

        private DateTime datpr;
        // <summary>
        // DATPR D(8)
        // </summary>
        [FieldName("DATPR"), FieldType('D'), FieldWidth(8)]
        public DateTime Datpr
        {
            get { return datpr; }
            set {  datpr = value; }
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

        private DateTime datcheck1;
        // <summary>
        // DATCHECK1 D(8)
        // </summary>
        [FieldName("DATCHECK1"), FieldType('D'), FieldWidth(8)]
        public DateTime Datcheck1
        {
            get { return datcheck1; }
            set {  datcheck1 = value; }
        }

        private DateTime datcheck2;
        // <summary>
        // DATCHECK2 D(8)
        // </summary>
        [FieldName("DATCHECK2"), FieldType('D'), FieldWidth(8)]
        public DateTime Datcheck2
        {
            get { return datcheck2; }
            set {  datcheck2 = value; }
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

        private string stamp;
        // <summary>
        // STAMP C(20)
        // </summary>
        [FieldName("STAMP"), FieldType('C'), FieldWidth(20)]
        public string Stamp
        {
            get { return stamp; }
            set { CheckStringData("Stamp", value, 20); stamp = value; }
        }

        private string info;
        // <summary>
        // INFO C(50)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(50)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 50); info = value; }
        }

        private bool power_on;
        // <summary>
        // POWER_ON L(1)
        // </summary>
        [FieldName("POWER_ON"), FieldType('L'), FieldWidth(1)]
        public bool Power_on
        {
            get { return power_on; }
            set {  power_on = value; }
        }

        private decimal power_val;
        // <summary>
        // POWER_VAL N(10,2)
        // </summary>
        [FieldName("POWER_VAL"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Power_val
        {
            get { return power_val; }
            set { CheckDecimalData("Power_val", value, 10, 2); power_val = value; }
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

        private string author;
        // <summary>
        // AUTHOR C(20)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(20)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 20); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("USLUGA")) Usluga = Convert.ToInt64(ADataRow["USLUGA"]); else Usluga = 0;
            if (ADataRow.Table.Columns.Contains("CODE")) Code = Convert.ToInt64(ADataRow["CODE"]); else Code = 0;
            if (ADataRow.Table.Columns.Contains("SCHEME")) Scheme = Convert.ToInt64(ADataRow["SCHEME"]); else Scheme = 0;
            if (ADataRow.Table.Columns.Contains("SN")) Sn = ADataRow["SN"].ToString(); else Sn = "";
            if (ADataRow.Table.Columns.Contains("TYPE")) Type = ADataRow["TYPE"].ToString(); else Type = "";
            if (ADataRow.Table.Columns.Contains("PRODUCER")) Producer = ADataRow["PRODUCER"].ToString(); else Producer = "";
            if (ADataRow.Table.Columns.Contains("PLACE")) Place = ADataRow["PLACE"].ToString(); else Place = "";
            if (ADataRow.Table.Columns.Contains("DATPR")) Datpr = Convert.ToDateTime(ADataRow["DATPR"]); else Datpr = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATS")) Dats = Convert.ToDateTime(ADataRow["DATS"]); else Dats = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATCHECK1")) Datcheck1 = Convert.ToDateTime(ADataRow["DATCHECK1"]); else Datcheck1 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATCHECK2")) Datcheck2 = Convert.ToDateTime(ADataRow["DATCHECK2"]); else Datcheck2 = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("REASOFF")) Reasoff = ADataRow["REASOFF"].ToString(); else Reasoff = "";
            if (ADataRow.Table.Columns.Contains("SC_W")) Sc_w = Convert.ToInt64(ADataRow["SC_W"]); else Sc_w = 0;
            if (ADataRow.Table.Columns.Contains("SC_DEC")) Sc_dec = Convert.ToInt64(ADataRow["SC_DEC"]); else Sc_dec = 0;
            if (ADataRow.Table.Columns.Contains("SC_DIR")) Sc_dir = Convert.ToInt64(ADataRow["SC_DIR"]); else Sc_dir = 0;
            if (ADataRow.Table.Columns.Contains("KOEF_TRANS")) Koef_trans = Convert.ToInt64(ADataRow["KOEF_TRANS"]); else Koef_trans = 0;
            if (ADataRow.Table.Columns.Contains("STAMP")) Stamp = ADataRow["STAMP"].ToString(); else Stamp = "";
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
            if (ADataRow.Table.Columns.Contains("POWER_ON")) Power_on = ADataRow["POWER_ON"].ToString() == "True" ? true : false; else Power_on = false;
            if (ADataRow.Table.Columns.Contains("POWER_VAL")) Power_val = Convert.ToDecimal(ADataRow["POWER_VAL"]); else Power_val = 0;
            if (ADataRow.Table.Columns.Contains("OPER")) Oper = Convert.ToInt64(ADataRow["OPER"]); else Oper = 0;
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            CountersRecord retValue = new CountersRecord();
            retValue.Ls = this.Ls;
            retValue.Usluga = this.Usluga;
            retValue.Code = this.Code;
            retValue.Scheme = this.Scheme;
            retValue.Sn = this.Sn;
            retValue.Type = this.Type;
            retValue.Producer = this.Producer;
            retValue.Place = this.Place;
            retValue.Datpr = this.Datpr;
            retValue.Dats = this.Dats;
            retValue.Datcheck1 = this.Datcheck1;
            retValue.Datcheck2 = this.Datcheck2;
            retValue.Date = this.Date;
            retValue.Reasoff = this.Reasoff;
            retValue.Sc_w = this.Sc_w;
            retValue.Sc_dec = this.Sc_dec;
            retValue.Sc_dir = this.Sc_dir;
            retValue.Koef_trans = this.Koef_trans;
            retValue.Stamp = this.Stamp;
            retValue.Info = this.Info;
            retValue.Power_on = this.Power_on;
            retValue.Power_val = this.Power_val;
            retValue.Oper = this.Oper;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO COUNTERS (LS, USLUGA, CODE, SCHEME, SN, TYPE, PRODUCER, PLACE, DATPR, DATS, DATCHECK1, DATCHECK2, DATE, REASOFF, SC_W, SC_DEC, SC_DIR, KOEF_TRANS, STAMP, INFO, POWER_ON, POWER_VAL, OPER, AUTHOR) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}', '{6}', '{7}', CTOD('{8}'), CTOD('{9}'), CTOD('{10}'), CTOD('{11}'), CTOD('{12}'), '{13}', {14}, {15}, {16}, {17}, '{18}', '{19}', {20}, {21}, {22}, '{23}')", Ls.ToString(), Usluga.ToString(), Code.ToString(), Scheme.ToString(), String.IsNullOrEmpty(Sn) ? "" : Sn.Trim(), String.IsNullOrEmpty(Type) ? "" : Type.Trim(), String.IsNullOrEmpty(Producer) ? "" : Producer.Trim(), String.IsNullOrEmpty(Place) ? "" : Place.Trim(), Datpr == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datpr.Month, Datpr.Day, Datpr.Year), Dats == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dats.Month, Dats.Day, Dats.Year), Datcheck1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datcheck1.Month, Datcheck1.Day, Datcheck1.Year), Datcheck2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datcheck2.Month, Datcheck2.Day, Datcheck2.Year), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Reasoff) ? "" : Reasoff.Trim(), Sc_w.ToString(), Sc_dec.ToString(), Sc_dir.ToString(), Koef_trans.ToString(), String.IsNullOrEmpty(Stamp) ? "" : Stamp.Trim(), String.IsNullOrEmpty(Info) ? "" : Info.Trim(), (Power_on ? 0 : 1 ), Power_val.ToString().Replace(',','.'), Oper.ToString(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
