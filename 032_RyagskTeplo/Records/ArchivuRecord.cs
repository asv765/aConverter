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
    [TableName("ARCHIVU.DBF")]
    public partial class ArchivuRecord: AbstractRecord
    {
        private string id;
        // <summary>
        // ID C(10)
        // </summary>
        [FieldName("ID"), FieldType('C'), FieldWidth(10)]
        public string Id
        {
            get { return id; }
            set { CheckStringData("Id", value, 10); id = value; }
        }

        private string field;
        // <summary>
        // FIELD C(10)
        // </summary>
        [FieldName("FIELD"), FieldType('C'), FieldWidth(10)]
        public string Field
        {
            get { return field; }
            set { CheckStringData("Field", value, 10); field = value; }
        }

        private string key;
        // <summary>
        // KEY C(10)
        // </summary>
        [FieldName("KEY"), FieldType('C'), FieldWidth(10)]
        public string Key
        {
            get { return key; }
            set { CheckStringData("Key", value, 10); key = value; }
        }

        private string dat;
        // <summary>
        // DAT C(6)
        // </summary>
        [FieldName("DAT"), FieldType('C'), FieldWidth(6)]
        public string Dat
        {
            get { return dat; }
            set { CheckStringData("Dat", value, 6); dat = value; }
        }

        private string value_;
        // <summary>
        // VALUE C(100)
        // </summary>
        [FieldName("VALUE"), FieldType('C'), FieldWidth(100)]
        public string Value_
        {
            get { return value_; }
            set { CheckStringData("Value_", value, 100); value_ = value; }
        }

        private string author;
        // <summary>
        // AUTHOR C(30)
        // </summary>
        [FieldName("AUTHOR"), FieldType('C'), FieldWidth(30)]
        public string Author
        {
            get { return author; }
            set { CheckStringData("Author", value, 30); author = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            if (ADataRow.Table.Columns.Contains("ID")) Id = ADataRow["ID"].ToString(); else Id = "";
            if (ADataRow.Table.Columns.Contains("FIELD")) Field = ADataRow["FIELD"].ToString(); else Field = "";
            if (ADataRow.Table.Columns.Contains("KEY")) Key = ADataRow["KEY"].ToString(); else Key = "";
            if (ADataRow.Table.Columns.Contains("DAT")) Dat = ADataRow["DAT"].ToString(); else Dat = "";
            if (ADataRow.Table.Columns.Contains("VALUE")) Value_ = ADataRow["VALUE"].ToString(); else Value_ = "";
            if (ADataRow.Table.Columns.Contains("AUTHOR")) Author = ADataRow["AUTHOR"].ToString(); else Author = "";
        }
        
        public override AbstractRecord Clone()
        {
            ArchivuRecord retValue = new ArchivuRecord();
            retValue.Id = this.Id;
            retValue.Field = this.Field;
            retValue.Key = this.Key;
            retValue.Dat = this.Dat;
            retValue.Value_ = this.Value_;
            retValue.Author = this.Author;
            return retValue;
        }
        
        public override string GetInsertScript()
        {
            string rs = String.Format("INSERT INTO ARCHIVU (ID, FIELD, KEY, DAT, VALUE, AUTHOR) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", String.IsNullOrEmpty(Id) ? "" : Id.Trim(), String.IsNullOrEmpty(Field) ? "" : Field.Trim(), String.IsNullOrEmpty(Key) ? "" : Key.Trim(), String.IsNullOrEmpty(Dat) ? "" : Dat.Trim(), String.IsNullOrEmpty(Value_) ? "" : Value_.Trim(), String.IsNullOrEmpty(Author) ? "" : Author.Trim());
            return rs;
        }
    }
}
