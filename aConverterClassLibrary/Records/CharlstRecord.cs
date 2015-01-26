// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;
using DbfClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("Charlst.DBF")]
    [Index("ADDCHARCD", "ADDCHARCD")]
    public class CharlstRecord: AbstractRecord
    {
        private Int64 addcharcd;
        // <summary>
        // ADDCHARCD N(9)
        // </summary>
        [FieldName("ADDCHARCD"), FieldType('N'), FieldWidth(9)]
        public Int64 Addcharcd
        {
            get { return addcharcd; }
            set { CheckIntegerData("Addcharcd", value, 9); addcharcd = value; }
        }

        private string addcharnam;
        // <summary>
        // ADDCHARNAM C(50)
        // </summary>
        [FieldName("ADDCHARNAM"), FieldType('C'), FieldWidth(50)]
        public string Addcharnam
        {
            get { return addcharnam; }
            set { CheckStringData("Addcharnam", value, 50); addcharnam = value; }
        }

        private Int64 addchtype;
        // <summary>
        // ADDCHTYPE N(2)
        // </summary>
        [FieldName("ADDCHTYPE"), FieldType('N'), FieldWidth(1)]
        public AddCharType Addchtype
        {
            get { return (AddCharType)addchtype; }
            set { addchtype = (int)value; }
        }

        private Int64 addcharmod;
        // <summary>
        // ADDCHARMOD N(3)
        // </summary>
        [FieldName("ADDCHARMOD"), FieldType('N'), FieldWidth(1)]
        public AddCharMode Addcharmod
        {
            get { return (AddCharMode)addcharmod; }
            set { addcharmod = (int)value; }
        }

        private string shortname;
        // <summary>
        // SHORTNAME C(10)
        // </summary>
        [FieldName("SHORTNAME"), FieldType('C'), FieldWidth(50)]
        public string Shortname
        {
            get { return shortname; }
            set { CheckStringData("Shortname", value, 50); shortname = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Addcharcd = Convert.ToInt64(ADataRow["ADDCHARCD"]);
            Addcharnam = ADataRow["ADDCHARNAM"].ToString();
            addchtype = Convert.ToInt64(ADataRow["ADDCHTYPE"]);
            addcharmod = Convert.ToInt64(ADataRow["ADDCHARMOD"]);
            Shortname = ADataRow["SHORTNAME"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO charlst (ADDCHARCD, ADDCHARNAM, ADDCHTYPE, ADDCHARMOD, SHORTNAME) VALUES ({0}, '{1}', {2}, {3}, '{4}')", Addcharcd.ToString(), String.IsNullOrEmpty(Addcharnam) ? "" : Addcharnam.Trim(), addchtype.ToString(), addcharmod.ToString(), String.IsNullOrEmpty(Shortname) ? "" : Shortname.Trim());
           return rs;
        }
    }

    public enum AddCharType
    {
        Тип_не_определен = 0,
        Строковый = 1,
        Целочисленный = 2,
        Вещественный = 3,
        Дата = 4,
        Денежный = 5,
        Логический = 6, 
        Перечисляемый = 7
    }

    public enum AddCharMode
    {
        Режим_не_определен = 0,
        Характеристика_абонента = 1,
        Характеристика_гражданина = 2,
        Характеристика_льготы = 3,
        Характеристика_оборудования = 4,
        Характеристика_судебного_иска = 5,
        Характеристика_дома = 6
    }
}
