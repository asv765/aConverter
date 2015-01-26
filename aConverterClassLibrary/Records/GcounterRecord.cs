// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("GCOUNTER.DBF")]
    [Index("LSHET", "LSHET")]
    [Index("COUNTERID", "COUNTERID")]
    [TableDescription("Таблица подключений абонентов к групповым счетчикам")]
    public class GcounterRecord: AbstractRecord
    {
        private string counterid;
        // <summary>
        // COUNTERID C(20)
        // </summary>
        [FieldName("COUNTERID"), FieldType('C'), FieldWidth(20), FieldDescription("Уникальный код счетчика")]
        public string Counterid
        {
            get { return counterid; }
            set { CheckStringData("Counterid", value, 20); counterid = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("Лицевой счет")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Counterid = ADataRow["COUNTERID"].ToString();
            Lshet = ADataRow["LSHET"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO GCOUNTER (COUNTERID, LSHET) VALUES ('{0}', '{1}')", String.IsNullOrEmpty(Counterid) ? "" : Counterid.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim());
           return rs;
        }
    }
}
