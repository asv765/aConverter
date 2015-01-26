// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("GCOUNTER.DBF")]
    [Index("LSHET", "LSHET")]
    [Index("SERIALNUM", "SERIALNUM")]
    public class GcounterRecord: AbstractRecord
    {
        private string serialnum;
        // <summary>
        // SERIALNUM C(20)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(20)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 20); serialnum = value; }
        }

        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10)]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Serialnum = ADataRow["SERIALNUM"].ToString();
            Lshet = ADataRow["LSHET"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO GCOUNTER (SERIALNUM, LSHET) VALUES ('{0}', '{1}')", String.IsNullOrEmpty(Serialnum) ? "" : Serialnum.Trim(), String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim());
           return rs;
        }
    }
}
