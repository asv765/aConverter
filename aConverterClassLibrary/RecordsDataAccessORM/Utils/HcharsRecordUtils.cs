using System;
using System.Collections.Generic;
using System.Linq;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public static class HcharsRecordUtils
    {
        /// <summary>
        /// Прореживает список количественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CNV_CHARSHOUSES> ThinOutList(List<CNV_CHARSHOUSES> crl)
        {
            // Сортируем список
            var rcrl = new List<CNV_CHARSHOUSES>();
            crl.Sort(CompareChars);
            // Удалем дублирующиеся строки
            int oldhousecd = -1; long oldcharcd = -1; decimal oldcharvalue = -1;
            for (int i = 0; i < crl.Count; i++)
            {
                var ch = crl[i];
                if (ch.HOUSECD != oldhousecd || ch.CHARCD != oldcharcd || ch.VALUE_ != oldcharvalue)
                {
                    rcrl.Add(ch);
                    oldhousecd = ch.HOUSECD;
                    oldcharcd = ch.CHARCD;
                    oldcharvalue = ch.VALUE_;
                }
            }
            crl.Clear();
            crl.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return rcrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи CHARSHOUSES
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareChars(CNV_CHARSHOUSES cr1, CNV_CHARSHOUSES cr2)
        {
            if (cr1.HOUSECD < cr2.HOUSECD)
                return -1;
            else if (cr1.HOUSECD > cr2.HOUSECD)
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
        /// Прореживает список качественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CNV_LCHARHOUSES> ThinOutList(List<CNV_LCHARHOUSES> lrl)
        {
            // Сортируем список
            var rlrl = new List<CNV_LCHARHOUSES>();
            lrl.Sort(CompareLchars);
            // Удалем дублирующиеся строки
            int? oldhousecd = -1; long oldlcharcd = -1; long oldlcharvalue = -1;
            for (int i = 0; i < lrl.Count; i++)
            {
                var t = lrl[i];
                if (t.HOUSECD != oldhousecd || t.LCHARCD != oldlcharcd || t.VALUE_ != oldlcharvalue)
                {
                    rlrl.Add(t);
                    oldhousecd = t.HOUSECD;
                    oldlcharcd = (int)t.LCHARCD;
                    oldlcharvalue = (int)t.VALUE_;
                }
            }
            lrl.Clear();
            lrl.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return rlrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LcharsRecord----------------------------------------------- н в 4
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareLchars(CNV_LCHARHOUSES lr1, CNV_LCHARHOUSES lr2)
        {
            if (lr1.HOUSECD < lr2.HOUSECD)
                return -1;
            if (lr1.HOUSECD > lr2.HOUSECD)
                return 1;
            if (lr1.LCHARCD < lr2.LCHARCD)
                return -1;
            if (lr1.LCHARCD > lr2.LCHARCD)
                return 1;
            if (lr1.DATE_ < lr2.DATE_)
                return -1;
            if (lr1.DATE_ > lr2.DATE_)
                return 1;
            return 0;
        }

        public static List<CNV_HADDCHAR> ThinOutList(List<CNV_HADDCHAR> la)
        {
            return la.GroupBy(a => new { a.HOUSECD, a.ADDCHARCD })
                .Select(ga => ga.Last())
                .ToList();
        }
    }
}
