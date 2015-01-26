// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using aConverterClassLibrary;

namespace aConverterClassLibrary.Records
{
    [TableName("abnchlst.DBF")]
    public class AbnchlstRecord: AbstractRecord
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
        [FieldName("ADDCHTYPE"), FieldType('N'), FieldWidth(2)]
        public Int64 Addchtype
        {
            get { return addchtype; }
            set { CheckIntegerData("Addchtype", value, 2); addchtype = value; }
        }

        private Int64 addcharmod;
        // <summary>
        // ADDCHARMOD N(3)
        // </summary>
        [FieldName("ADDCHARMOD"), FieldType('N'), FieldWidth(3)]
        public Int64 Addcharmod
        {
            get { return addcharmod; }
            set { CheckIntegerData("Addcharmod", value, 3); addcharmod = value; }
        }

        private string shortname;
        // <summary>
        // SHORTNAME C(10)
        // </summary>
        [FieldName("SHORTNAME"), FieldType('C'), FieldWidth(10)]
        public string Shortname
        {
            get { return shortname; }
            set { CheckStringData("Shortname", value, 10); shortname = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Addcharcd = Convert.ToInt64(ADataRow["ADDCHARCD"]);
            Addcharnam = ADataRow["ADDCHARNAM"].ToString();
            Addchtype = Convert.ToInt64(ADataRow["ADDCHTYPE"]);
            Addcharmod = Convert.ToInt64(ADataRow["ADDCHARMOD"]);
            Shortname = ADataRow["SHORTNAME"].ToString();
        }
        
        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO abnchlst (ADDCHARCD, ADDCHARNAM, ADDCHTYPE, ADDCHARMOD, SHORTNAME) VALUES ({0}, '{1}', {2}, {3}, '{4}')", Addcharcd.ToString(), String.IsNullOrEmpty(Addcharnam) ? "" : Addcharnam.Trim(), Addchtype.ToString(), Addcharmod.ToString(), String.IsNullOrEmpty(Shortname) ? "" : Shortname.Trim());
           return rs;
        }
    }
}
