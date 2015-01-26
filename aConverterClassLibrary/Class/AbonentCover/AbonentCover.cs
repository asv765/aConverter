using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public abstract class AbonentCover
    {
        #region Преобразования значения поля в значения для вставки в SQL INSERT
        public string GetValueForInsert(int? value)
        {
            if (value == null)
                return "NULL";
            else
                return GetValueForInsert((int)value);
        }

        public string GetValueForInsert(int value)
        {
            return Convert.ToString(value);
        }

        public string GetValueForInsert(long? value)
        {
            if (value == null)
                return "NULL";
            else
                return GetValueForInsert((long)value);
        }

        public string GetValueForInsert(long value)
        {
            return Convert.ToString(value);
        }

        public string GetValueForInsert(bool? value)
        {
            if (value == null)
                return "NULL";
            else
                return GetValueForInsert((bool)value);
        }

        public string GetValueForInsert(bool value)
        {
            return ((bool)value ? "1" : "0");
        }

        public string GetValueForInsert(string value)
        {
            if (value == null)
                return "NULL";
            else
                return "'"+value+"'";
        }

        public string GetValueForInsert(DateTime? value, DateTimeType DateTimeType)
        {
            if (value == null)
                return "NULL";
            else if (value == DateTime.MinValue)
                return "NULL";
            else
                return GetValueForInsert((DateTime)value, DateTimeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="full">если true, то дата+время, иначе только дата</param>
        /// <returns></returns>
        public string GetValueForInsert(DateTime value, DateTimeType DateTimeType)
        {
            if (DateTimeType == aConverterClassLibrary.DateTimeType.Дата_и_время)
                return "'" + value.ToString() + "'";
            else
                return "'" + value.ToString("d") + "'";
        }
        #endregion

    }

    public enum DateTimeType
    {
        Дата_и_время,
        Только_дата
    }
}
