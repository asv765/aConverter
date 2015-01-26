// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("LSRECODE.DBF")]
    public class LsrecodeRecord: AbstractRecord
    {
        private string kvclshet;
        // <summary>
        // KVCLSHET C(19)
        // </summary>
        [FieldName("KVCLSHET"), FieldType('C'), FieldWidth(19)]
        public string Kvclshet
        {
            get { return kvclshet; }
            set { CheckStringData("Kvclshet", value, 19); kvclshet = value; }
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

        private Int64 maxnumber;
        // <summary>
        // MAXNUMBER N(10)
        // </summary>
        [FieldName("MAXNUMBER"), FieldType('N'), FieldWidth(10)]
        public Int64 Maxnumber
        {
            get { return maxnumber; }
            set { CheckIntegerData("Maxnumber", value, 10); maxnumber = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Kvclshet = ADataRow["KVCLSHET"].ToString();
            Lshet = ADataRow["LSHET"].ToString();
            Maxnumber = Convert.ToInt64(ADataRow["MAXNUMBER"]);
        }
    }
}

