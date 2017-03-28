using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public static class RecordUtils
    {
        public static string GetSafeStringWithNull(string value)
        {
            return String.IsNullOrEmpty(value) ? "NULL" : "'" + value.Trim() + "'";
        }

        public static string GetSafeDateTimeWithNull(DateTime? value)
        {
            if (value == null)
                return "NULL";
            else if (value.Value.Year < 1950)
                return "NULL";
            else
                return "'" + value.Value.ToString("d.MM.yyyy HH:mm:ss") + "'";
        }

        public static string GetSafeDateWithNull(DateTime? value)
        {
            if (value == null)
                return "NULL";
            else if (value.Value.Year < 1950)
                return "NULL";
            else
                return "'" + value.Value.ToString("d.MM.yyyy") + "'";
        }

        public static string GetSafeIntWithNull(int? value)
        {
            if (value == null)
                return "NULL";
            else
                return value.Value.ToString();
        }

        public static string GetSafeDecimalWithNull(decimal? value)
        {
            if (value == null)
                return "NULL";
            else
                return value.Value.ToString().Replace(',','.');
        }
    }
}
