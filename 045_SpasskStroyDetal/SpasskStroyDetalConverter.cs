using System;

namespace _045_SpasskStroyDetal
{
    public static class Consts
    {
        /// <summary>
        /// Количество записей на каждый инсерт
        /// </summary>
        public const int InsertRecordCount = 1000;

        public const string RecodeTableFileName =
            @"D:\Work\C#\C#Projects\aConverter\042_Kirici\Sources\Таблица перекодировки.xlsx";

        public static string GetLs(long intls)
        {
            return String.Format("97{0:D6}", intls);
        }

        public static readonly int CurrentMonth = 06;

        public static readonly int CurrentYear = 2016;

        public const string UnknownTown = "Неизвестен";
        public const string UnknownStreet = "Неизвестна";
    }


}
