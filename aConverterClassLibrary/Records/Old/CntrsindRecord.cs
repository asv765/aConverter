// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    [TableName("CNTRSIND.DBF")]
    [Index("LSHET", "LSHET"), Index("CNTTYPE", "CNTTYPE"), Index("ServiceCD", "ServiceCD")]
    public class CntrsindRecord: AbstractRecord
    {
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

        private Int64 cnttype;
        // <summary>
        // CNTTYPE N(8)
        // </summary>
        [FieldName("CNTTYPE"), FieldType('N'), FieldWidth(8)]
        public Int64 Cnttype
        {
            get { return cnttype; }
            set { CheckIntegerData("Cnttype", value, 8); cnttype = value; }
        }

        private Int64 servicecd;
        // <summary>
        // SERVICECD N(8)
        // </summary>
        [FieldName("SERVICECD"), FieldType('N'), FieldWidth(8)]
        public Int64 Servicecd
        {
            get { return servicecd; }
            set { CheckIntegerData("Servicecd", value, 8); servicecd = value; }
        }

        private string serialnum;
        // <summary>
        // SERIALNUM C(30)
        // </summary>
        [FieldName("SERIALNUM"), FieldType('C'), FieldWidth(30)]
        public string Serialnum
        {
            get { return serialnum; }
            set { CheckStringData("Serialnum", value, 30); serialnum = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(9,2)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(9), FieldDec(2)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 9, 2); indication = value; }
        }

        private DateTime inddate;
        // <summary>
        // INDDATE D
        // </summary>
        [FieldName("INDDATE"), FieldType('D')]
        public DateTime Inddate
        {
            get { return inddate; }
            set { CheckData("Inddate", value); inddate = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Cnttype = Convert.ToInt64(ADataRow["CNTTYPE"]);
            Servicecd = Convert.ToInt64(ADataRow["SERVICECD"]);
            Serialnum = ADataRow["SERIALNUM"].ToString();
            Indication = Convert.ToDecimal(ADataRow["INDICATION"]);
            Inddate = Convert.ToDateTime(ADataRow["INDDATE"]);
        }
    }
}

