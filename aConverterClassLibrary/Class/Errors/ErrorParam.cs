using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public static class ErrorParamsUtils
    {
        public static Type GetTypeByErrorParams(ErrorParam AErrorParams)
        {
            if (AErrorParams == ErrorParam.Тип_корректировки_строки_в_NACHOPL)
                return typeof(NachoplCorrectionType);
            if (AErrorParams == ErrorParam.Префикс_лицевого_счета)
                return typeof(string);
            if (AErrorParams == ErrorParam.Тип_корректировки_сальдо_в_NACHOPL)
                return typeof(NachoplSaldoCorrectionType);
            if (AErrorParams == ErrorParam.Код_марки_группового_счетчика_из_таблицы_COUNTERSTYPES)
                return typeof(string);

            throw new ArgumentException("Для типа параметра " + AErrorParams.ToString() + " не предусмотрено значений");
        }
    }

    public enum ErrorParam
    {
        Тип_корректировки_строки_в_NACHOPL = 1,
        Префикс_лицевого_счета = 2,
        Тип_корректировки_сальдо_в_NACHOPL = 3,
        Код_марки_группового_счетчика_из_таблицы_COUNTERSTYPES = 4
    }

}
