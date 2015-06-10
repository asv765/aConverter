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
    [TableName("JUROP.DBF")]
    public partial class JuropRecord: AbstractRecord
    {
        private Int64 ls;
        // <summary>
        // LS N(7)
        // </summary>
        [FieldName("LS"), FieldType('N'), FieldWidth(7)]
        public Int64 Ls
        {
            get { return ls; }
            set { CheckIntegerData("Ls", value, 7); ls = value; }
        }

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

        private Int64 time;
        // <summary>
        // TIME N(6)
        // </summary>
        [FieldName("TIME"), FieldType('N'), FieldWidth(6)]
        public Int64 Time
        {
            get { return time; }
            set { CheckIntegerData("Time", value, 6); time = value; }
        }

        private string datras;
        // <summary>
        // DATRAS C(6)
        // </summary>
        [FieldName("DATRAS"), FieldType('C'), FieldWidth(6)]
        public string Datras
        {
            get { return datras; }
            set { CheckStringData("Datras", value, 6); datras = value; }
        }

        private Int64 id;
        // <summary>
        // ID N(6)
        // </summary>
        [FieldName("ID"), FieldType('N'), FieldWidth(6)]
        public Int64 Id
        {
            get { return id; }
            set { CheckIntegerData("Id", value, 6); id = value; }
        }

        private Int64 func;
        // <summary>
        // FUNC N(4)
        // </summary>
        [FieldName("FUNC"), FieldType('N'), FieldWidth(4)]
        public Int64 Func
        {
            get { return func; }
            set { CheckIntegerData("Func", value, 4); func = value; }
        }

        private string info;
        // <summary>
        // INFO C(100)
        // </summary>
        [FieldName("INFO"), FieldType('C'), FieldWidth(100)]
        public string Info
        {
            get { return info; }
            set { CheckStringData("Info", value, 100); info = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LS")) Ls = Convert.ToInt64(ADataRow["LS"]); else Ls = 0;
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = Convert.ToDateTime(ADataRow["DAT"]); else Dat = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("TIME")) Time = Convert.ToInt64(ADataRow["TIME"]); else Time = 0;
            if (ADataRow.Table.Columns.Contains("DATRAS")) Datras = ADataRow["DATRAS"].ToString(); else Datras = "";
            if (ADataRow.Table.Columns.Contains("ID")) Id = Convert.ToInt64(ADataRow["ID"]); else Id = 0;
            if (ADataRow.Table.Columns.Contains("FUNC")) Func = Convert.ToInt64(ADataRow["FUNC"]); else Func = 0;
            if (ADataRow.Table.Columns.Contains("INFO")) Info = ADataRow["INFO"].ToString(); else Info = "";
        }
        
        public override AbstractRecord Clone()
        {
            JuropRecord retValue = new JuropRecord();
            retValue.Ls = this.Ls;
            retValue.Dat = this.Dat;
            retValue.Time = this.Time;
            retValue.Datras = this.Datras;
            retValue.Id = this.Id;
            retValue.Func = this.Func;
            retValue.Info = this.Info;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO JUROP (LS, DAT, TIME, DATRAS, ID, FUNC, INFO) VALUES ({0}, CTOD('{1}'), {2}, '{3}', {4}, {5}, '{6}')", Ls.ToString(), Dat == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Dat.Month, Dat.Day, Dat.Year), Time.ToString(), String.IsNullOrEmpty(Datras) ? "" : Datras.Trim(), Id.ToString(), Func.ToString(), String.IsNullOrEmpty(Info) ? "" : Info.Trim());
            return rs;
        }
    }
}
