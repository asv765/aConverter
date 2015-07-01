// Файл сгенерирован aConverter
using System;
using DbfClassLibrary;

namespace _033_Yrupinsk.Records
{
    [TableName("CNTRSIND.DBF")]
    public partial class CntrsindRecord : AbstractRecord
    {
        private Int64 kod;
        // <summary>
        // KOD N(11)
        // </summary>
        [FieldName("KOD"), FieldType('N'), FieldWidth(11)]
        public Int64 Kod
        {
            get { return kod; }
            set { CheckIntegerData("Kod", value, 11); kod = value; }
        }

        private DateTime inddate;
        // <summary>
        // INDDATE D(8)
        // </summary>
        [FieldName("INDDATE"), FieldType('D'), FieldWidth(8)]
        public DateTime Inddate
        {
            get { return inddate; }
            set { inddate = value; }
        }

        private decimal indication;
        // <summary>
        // INDICATION N(10,3)
        // </summary>
        [FieldName("INDICATION"), FieldType('N'), FieldWidth(10), FieldDec(3)]
        public decimal Indication
        {
            get { return indication; }
            set { CheckDecimalData("Indication", value, 10, 3); indication = value; }
        }

        private decimal volume;
        // <summary>
        // VOLUME N(8,3)
        // </summary>
        [FieldName("VOLUME"), FieldType('N'), FieldWidth(8), FieldDec(3)]
        public decimal Volume
        {
            get { return volume; }
            set { CheckDecimalData("Volume", value, 8, 3); volume = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("KOD")) Kod = Convert.ToInt64(ADataRow["KOD"]); else Kod = 0;
            if (ADataRow.Table.Columns.Contains("INDDATE")) Inddate = Convert.ToDateTime(ADataRow["INDDATE"]); else Inddate = DateTime.MinValue;
            if (ADataRow.Table.Columns.Contains("INDICATION")) Indication = Convert.ToDecimal(ADataRow["INDICATION"]); else Indication = 0;
            if (ADataRow.Table.Columns.Contains("VOLUME")) Volume = Convert.ToDecimal(ADataRow["VOLUME"]); else Volume = 0;
        }

        public override AbstractRecord Clone()
        {
            CntrsindRecord retValue = new CntrsindRecord();
            retValue.Kod = this.Kod;
            retValue.Inddate = this.Inddate;
            retValue.Indication = this.Indication;
            retValue.Volume = this.Volume;
            return retValue;
        }

        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO CNTRSIND (KOD, INDDATE, INDICATION, VOLUME) VALUES ({0}, CTOD('{1}'), {2}, {3})", Kod.ToString(), Inddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Inddate.Month, Inddate.Day, Inddate.Year), Indication.ToString().Replace(',', '.'), Volume.ToString().Replace(',', '.'));
            return rs;
        }
    }
}
