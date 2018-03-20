// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace _048_Rgmek
{
    [TableName("TP_ACT.DBF")]
    public partial class Tp_actRecord : AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(19)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(19)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 19); lshet = value; }
        }

        private string housecd;
        // <summary>
        // HOUSECD C(36)
        // </summary>
        [FieldName("HOUSECD"), FieldType('C'), FieldWidth(36)]
        public string Housecd
        {
            get { return housecd; }
            set { CheckStringData("Housecd", value, 36); housecd = value; }
        }

        private string doc;
        // <summary>
        // DOC C(150)
        // </summary>
        [FieldName("DOC"), FieldType('C'), FieldWidth(150)]
        public string Doc
        {
            get { return doc; }
            set { CheckStringData("Doc", value, 150); doc = value; }
        }

        private string nomer;
        // <summary>
        // NOMER C(25)
        // </summary>
        [FieldName("NOMER"), FieldType('C'), FieldWidth(25)]
        public string Nomer
        {
            get { return nomer; }
            set { CheckStringData("Nomer", value, 25); nomer = value; }
        }

        private DateTime date;
        // <summary>
        // DATE D(8)
        // </summary>
        [FieldName("DATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string point;
        // <summary>
        // POINT C(100)
        // </summary>
        [FieldName("POINT"), FieldType('C'), FieldWidth(100)]
        public string Point
        {
            get { return point; }
            set { CheckStringData("Point", value, 100); point = value; }
        }

        private decimal instcap;
        // <summary>
        // INSTCAP N(10,2)
        // </summary>
        [FieldName("INSTCAP"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Instcap
        {
            get { return instcap; }
            set { CheckDecimalData("Instcap", value, 10, 2); instcap = value; }
        }

        private decimal estcap;
        // <summary>
        // ESTCAP N(10,2)
        // </summary>
        [FieldName("ESTCAP"), FieldType('N'), FieldWidth(10), FieldDec(2)]
        public decimal Estcap
        {
            get { return estcap; }
            set { CheckDecimalData("Estcap", value, 10, 2); estcap = value; }
        }

        private string voltage;
        // <summary>
        // VOLTAGE C(20)
        // </summary>
        [FieldName("VOLTAGE"), FieldType('C'), FieldWidth(20)]
        public string Voltage
        {
            get { return voltage; }
            set { CheckStringData("Voltage", value, 20); voltage = value; }
        }

        private string reliabil;
        // <summary>
        // RELIABIL C(15)
        // </summary>
        [FieldName("RELIABIL"), FieldType('C'), FieldWidth(15)]
        public string Reliabil
        {
            get { return reliabil; }
            set { CheckStringData("Reliabil", value, 15); reliabil = value; }
        }

        private string descrip;
        // <summary>
        // DESCRIP C(250)
        // </summary>
        [FieldName("DESCRIP"), FieldType('C'), FieldWidth(250)]
        public string Descrip
        {
            get { return descrip; }
            set { CheckStringData("Descrip", value, 250); descrip = value; }
        }

        private string balan_lim;
        // <summary>
        // BALAN_LIM C(250)
        // </summary>
        [FieldName("BALAN_LIM"), FieldType('C'), FieldWidth(250)]
        public string Balan_lim
        {
            get { return balan_lim; }
            set { CheckStringData("Balan_lim", value, 250); balan_lim = value; }
        }

        private string liabil_lim;
        // <summary>
        // LIABIL_LIM C(250)
        // </summary>
        [FieldName("LIABIL_LIM"), FieldType('C'), FieldWidth(250)]
        public string Liabil_lim
        {
            get { return liabil_lim; }
            set { CheckStringData("Liabil_lim", value, 250); liabil_lim = value; }
        }

        private string netorg_eq;
        // <summary>
        // NETORG_EQ C(250)
        // </summary>
        [FieldName("NETORG_EQ"), FieldType('C'), FieldWidth(250)]
        public string Netorg_eq
        {
            get { return netorg_eq; }
            set { CheckStringData("Netorg_eq", value, 250); netorg_eq = value; }
        }

        private string client_eq;
        // <summary>
        // CLIENT_EQ C(250)
        // </summary>
        [FieldName("CLIENT_EQ"), FieldType('C'), FieldWidth(250)]
        public string Client_eq
        {
            get { return client_eq; }
            set { CheckStringData("Client_eq", value, 250); client_eq = value; }
        }

        private string netorge_eq;
        // <summary>
        // NETORGE_EQ C(250)
        // </summary>
        [FieldName("NETORGE_EQ"), FieldType('C'), FieldWidth(250)]
        public string Netorge_eq
        {
            get { return netorge_eq; }
            set { CheckStringData("Netorge_eq", value, 250); netorge_eq = value; }
        }

        private string cliente_eq;
        // <summary>
        // CLIENTE_EQ C(250)
        // </summary>
        [FieldName("CLIENTE_EQ"), FieldType('C'), FieldWidth(250)]
        public string Cliente_eq
        {
            get { return cliente_eq; }
            set { CheckStringData("Cliente_eq", value, 250); cliente_eq = value; }
        }

        private Int64 isdeleted;
        // <summary>
        // ISDELETED N(2)
        // </summary>
        [FieldName("ISDELETED"), FieldType('N'), FieldWidth(2)]
        public Int64 Isdeleted
        {
            get { return isdeleted; }
            set { CheckIntegerData("Isdeleted", value, 2); isdeleted = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("HOUSECD")) Housecd = ADataRow["HOUSECD"].ToString(); else Housecd = "";
            if (ADataRow.Table.Columns.Contains("DOC")) Doc = ADataRow["DOC"].ToString(); else Doc = "";
            if (ADataRow.Table.Columns.Contains("NOMER")) Nomer = ADataRow["NOMER"].ToString(); else Nomer = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("POINT")) Point = ADataRow["POINT"].ToString(); else Point = "";
            if (ADataRow.Table.Columns.Contains("INSTCAP")) Instcap = Convert.ToDecimal(ADataRow["INSTCAP"]); else Instcap = 0;
            if (ADataRow.Table.Columns.Contains("ESTCAP")) Estcap = Convert.ToDecimal(ADataRow["ESTCAP"]); else Estcap = 0;
            if (ADataRow.Table.Columns.Contains("VOLTAGE")) Voltage = ADataRow["VOLTAGE"].ToString(); else Voltage = "";
            if (ADataRow.Table.Columns.Contains("RELIABIL")) Reliabil = ADataRow["RELIABIL"].ToString(); else Reliabil = "";
            if (ADataRow.Table.Columns.Contains("DESCRIP")) Descrip = ADataRow["DESCRIP"].ToString(); else Descrip = "";
            if (ADataRow.Table.Columns.Contains("BALAN_LIM")) Balan_lim = ADataRow["BALAN_LIM"].ToString(); else Balan_lim = "";
            if (ADataRow.Table.Columns.Contains("LIABIL_LIM")) Liabil_lim = ADataRow["LIABIL_LIM"].ToString(); else Liabil_lim = "";
            if (ADataRow.Table.Columns.Contains("NETORG_EQ")) Netorg_eq = ADataRow["NETORG_EQ"].ToString(); else Netorg_eq = "";
            if (ADataRow.Table.Columns.Contains("CLIENT_EQ")) Client_eq = ADataRow["CLIENT_EQ"].ToString(); else Client_eq = "";
            if (ADataRow.Table.Columns.Contains("NETORGE_EQ")) Netorge_eq = ADataRow["NETORGE_EQ"].ToString(); else Netorge_eq = "";
            if (ADataRow.Table.Columns.Contains("CLIENTE_EQ")) Cliente_eq = ADataRow["CLIENTE_EQ"].ToString(); else Cliente_eq = "";
            if (ADataRow.Table.Columns.Contains("ISDELETED")) Isdeleted = Convert.ToInt64(ADataRow["ISDELETED"]); else Isdeleted = 0;
        }

        public override AbstractRecord Clone()
        {
            Tp_actRecord retValue = new Tp_actRecord();
            retValue.Lshet = this.Lshet;
            retValue.Housecd = this.Housecd;
            retValue.Doc = this.Doc;
            retValue.Nomer = this.Nomer;
            retValue.Date = this.Date;
            retValue.Point = this.Point;
            retValue.Instcap = this.Instcap;
            retValue.Estcap = this.Estcap;
            retValue.Voltage = this.Voltage;
            retValue.Reliabil = this.Reliabil;
            retValue.Descrip = this.Descrip;
            retValue.Balan_lim = this.Balan_lim;
            retValue.Liabil_lim = this.Liabil_lim;
            retValue.Netorg_eq = this.Netorg_eq;
            retValue.Client_eq = this.Client_eq;
            retValue.Netorge_eq = this.Netorge_eq;
            retValue.Cliente_eq = this.Cliente_eq;
            retValue.Isdeleted = this.Isdeleted;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO TP_ACT (LSHET, HOUSECD, DOC, NOMER, DATE, POINT, INSTCAP, ESTCAP, VOLTAGE, RELIABIL, DESCRIP, BALAN_LIM, LIABIL_LIM, NETORG_EQ, CLIENT_EQ, NETORGE_EQ, CLIENTE_EQ, ISDELETED) VALUES ('{0}', '{1}', '{2}', '{3}', CTOD('{4}'), '{5}', {6}, {7}, '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', {17})", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(Housecd) ? "" : Housecd.Trim(), String.IsNullOrEmpty(Doc) ? "" : Doc.Trim(), String.IsNullOrEmpty(Nomer) ? "" : Nomer.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year), String.IsNullOrEmpty(Point) ? "" : Point.Trim(), Instcap.ToString().Replace(',', '.'), Estcap.ToString().Replace(',', '.'), String.IsNullOrEmpty(Voltage) ? "" : Voltage.Trim(), String.IsNullOrEmpty(Reliabil) ? "" : Reliabil.Trim(), String.IsNullOrEmpty(Descrip) ? "" : Descrip.Trim(), String.IsNullOrEmpty(Balan_lim) ? "" : Balan_lim.Trim(), String.IsNullOrEmpty(Liabil_lim) ? "" : Liabil_lim.Trim(), String.IsNullOrEmpty(Netorg_eq) ? "" : Netorg_eq.Trim(), String.IsNullOrEmpty(Client_eq) ? "" : Client_eq.Trim(), String.IsNullOrEmpty(Netorge_eq) ? "" : Netorge_eq.Trim(), String.IsNullOrEmpty(Cliente_eq) ? "" : Cliente_eq.Trim(), Isdeleted.ToString());
            return rs;
        }
    }
}