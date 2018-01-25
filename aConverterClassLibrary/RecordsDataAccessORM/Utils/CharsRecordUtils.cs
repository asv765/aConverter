using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class CharsRecordUtils
    {
        /// <summary>
        /// Прореживает список количественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CNV_CHAR> ThinOutList(List<CNV_CHAR> crl, bool useIntLs = false)
        {
            // Сортируем список
            var rcrl = new List<CNV_CHAR>();
            if (useIntLs) crl.Sort(CompareCharsUsingIntLs);
            else crl.Sort(CompareChars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldcharcd = -1; decimal oldcharvalue = -1;
            for (int i = 0; i < crl.Count; i++)
            {
                var ch = crl[i];
                if (ch.LSHET != oldlshet || ch.CHARCD != oldcharcd || ch.VALUE_ != oldcharvalue)
                {   
                    rcrl.Add(ch);
                    oldlshet = ch.LSHET;
                    oldcharcd = (int)ch.CHARCD;
                    oldcharvalue = (decimal)ch.VALUE_;
                }
            }
            crl.Clear();
            crl.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return rcrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи ccharsRecord
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareCharsUsingIntLs(CNV_CHAR cr1, CNV_CHAR cr2)
        {
            if (cr1.SortLshet < cr2.SortLshet)
                return -1;
            else if (cr1.SortLshet > cr2.SortLshet)
                return 1;
            else
            {
                if (cr1.CHARCD < cr2.CHARCD)
                    return -1;
                else if (cr1.CHARCD > cr2.CHARCD)
                    return 1;
                else
                {
                    if (cr1.DATE_ < cr2.DATE_)
                        return -1;
                    else if (cr1.DATE_ > cr2.DATE_)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи ccharsRecord
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareChars(CNV_CHAR cr1, CNV_CHAR cr2)
        {
            if (long.Parse(cr1.LSHET) < long.Parse(cr2.LSHET))
                return -1;
            else if (long.Parse(cr1.LSHET) > long.Parse(cr2.LSHET))
                return 1;
            else
            {
                if (cr1.CHARCD < cr2.CHARCD)
                    return -1;
                else if (cr1.CHARCD > cr2.CHARCD)
                    return 1;
                else
                {
                    if (cr1.DATE_ < cr2.DATE_)
                        return -1;
                    else if (cr1.DATE_ > cr2.DATE_)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        /// <summary>
        /// Делает уникальный набор характеристик с ключом "лс,код характерстки, дата"
        /// </summary>        
        public static List<CNV_CHAR> CreateUniqueCchars(List<CNV_CHAR> lcc)
        {
            return lcc
                .GroupBy(lc => new { lc.LSHET, lc.CHARCD, lc.DATE_ })
                .Select(glc => glc.Last())
                .ToList();
        }
    }
}
