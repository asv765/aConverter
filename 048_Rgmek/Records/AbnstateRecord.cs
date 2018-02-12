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
    [TableName("ABNSTATE.DBF")]
    public partial class AbnstateRecord : AbstractRecord
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

        private string state;
        // <summary>
        // STATE C(10)
        // </summary>
        [FieldName("STATE"), FieldType('C'), FieldWidth(10)]
        public string State
        {
            get { return state; }
            set { CheckStringData("State", value, 10); state = value; }
        }

        private string reason;
        // <summary>
        // REASON C(150)
        // </summary>
        [FieldName("REASON"), FieldType('C'), FieldWidth(150)]
        public string Reason
        {
            get { return reason; }
            set { CheckStringData("Reason", value, 150); reason = value; }
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

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("LSHET")) Lshet = ADataRow["LSHET"].ToString(); else Lshet = "";
            if (ADataRow.Table.Columns.Contains("STATE")) State = ADataRow["STATE"].ToString(); else State = "";
            if (ADataRow.Table.Columns.Contains("REASON")) Reason = ADataRow["REASON"].ToString(); else Reason = "";
            if (ADataRow.Table.Columns.Contains("DATE")) Date = Convert.ToDateTime(ADataRow["DATE"]); else Date = DateTime.MinValue;
        }

        public override AbstractRecord Clone()
        {
            AbnstateRecord retValue = new AbnstateRecord();
            retValue.Lshet = this.Lshet;
            retValue.State = this.State;
            retValue.Reason = this.Reason;
            retValue.Date = this.Date;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ABNSTATE (LSHET, STATE, REASON, DATE) VALUES ('{0}', '{1}', '{2}', CTOD('{3}'))", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), String.IsNullOrEmpty(State) ? "" : State.Trim(), String.IsNullOrEmpty(Reason) ? "" : Reason.Trim(), Date == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Date.Month, Date.Day, Date.Year));
            return rs;
        }
    }
}