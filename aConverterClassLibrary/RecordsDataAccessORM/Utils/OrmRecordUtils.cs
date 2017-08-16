using System;
using System.Globalization;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public static class OrmRecordUtils
    {
        public static string ToSql(string value)
        {
            return value == null ? "NULL" : "'" + value.Replace("'", "''") + "'";
        }

        public static string ToSql(DateTime value)
        {
            return "'" + value.ToString("dd.MM.yyyy") + "'";
        }

        public static string ToSql(DateTime? value)
        {
            return value == null ? "NULL" : ToSql(value.Value);
        }

        public static string ToSql(int value)
        {
            return value.ToString();
        }

        public static string ToSql(int? value)
        {
            return value == null ? "NULL" : ToSql(value.Value);
        }

        public static string ToSql(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToSql(decimal? value)
        {
            return value == null ? "NULL" : ToSql(value.Value);
        }
    }
}
