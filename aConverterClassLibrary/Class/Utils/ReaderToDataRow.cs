using System;
using System.Collections.Generic;
using System.Data;

namespace aConverterClassLibrary.Class.Utils
{
    public class ReaderToDataRow : IDisposable
    {
        public readonly DataTable DataTable;
        private readonly List<DataColumn> _columnList;
        private readonly DataRow _dataRow;
        private readonly NullValuesHandledRegime _nullRegime;

        public ReaderToDataRow(IDataReader reader, NullValuesHandledRegime nullRegime = NullValuesHandledRegime.LeaveNull)
        {
            _nullRegime = nullRegime;
            var dtSchema = reader.GetSchemaTable();
            DataTable = new DataTable();

            _columnList = new List<DataColumn>();
            if (dtSchema == null) throw new Exception("Не удалось получить схему таблицы");
            foreach (DataRow drow in dtSchema.Rows)
            {
                string columnName = Convert.ToString(drow["ColumnName"]);
                var column = new DataColumn(columnName, (Type) (drow["DataType"]))
                {
                    Unique = (bool) drow["IsUnique"],
                    AllowDBNull = (bool) drow["AllowDBNull"],
                    AutoIncrement = (bool) drow["IsAutoIncrement"]
                };
                _columnList.Add(column);
                DataTable.Columns.Add(column);
            }
            _dataRow = DataTable.NewRow();
        }

        public DataRow GetDataRow(IDataReader reader)
        {
            for (int i = 0; i < _columnList.Count; i++)
            {
                object columnData;
                if (reader.IsDBNull(i))
                {
                    switch (_nullRegime)
                    {
                        case NullValuesHandledRegime.LeaveNull:
                            columnData = DBNull.Value;
                            break;
                        case NullValuesHandledRegime.DbfLikeRegime:
                            columnData = NullDbfLikeValues[_columnList[i].DataType];
                            break;
                        default:
                            throw new Exception($"Необработанный тип {_columnList[i].DataType} для null dbf");
                    }
                }
                else
                    columnData = reader[i];
                _dataRow[_columnList[i]] = columnData;
            }
            return _dataRow;
        }

        public void Dispose()
        {
            DataTable.Dispose();
            _columnList.Clear();
            _columnList.TrimExcess();
        }

        public enum NullValuesHandledRegime
        {
            LeaveNull,
            DbfLikeRegime
        }

        public Dictionary<Type, object> NullDbfLikeValues = new Dictionary<Type, object>
        {
            {typeof(int), 0},
            {typeof (long), 0},
            {typeof(double), 0},
            {typeof (decimal), 0},
            {typeof (string), ""},
            {typeof (DateTime), DateTime.MinValue}
        };
    }
}
