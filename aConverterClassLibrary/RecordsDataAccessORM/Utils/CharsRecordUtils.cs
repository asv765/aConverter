using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class CharsRecordUtils
    {
        /// <summary>
        /// Прореживает список количественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CHAR> ThinOutList(List<CHAR> crl)
        {
            // Сортируем список
            List<CHAR> rcrl = new List<CHAR>();
            crl.Sort(CompareChars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldcharcd = -1; decimal oldcharvalue = -1;
            for (int i = 0; i < crl.Count; i++)
            {
                if (crl[i].LSHET != oldlshet || crl[i].CHARCD != oldcharcd || crl[i].VALUE_ != oldcharvalue)
                {
                    rcrl.Add(crl[i]);
                    oldlshet = crl[i].LSHET;
                    oldcharcd = (Int32)crl[i].CHARCD;
                    oldcharvalue = (Int32)crl[i].VALUE_;
                }
            }
            return rcrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LcharsRecord
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareChars(CHAR cr1, CHAR cr2)
        {
            if (Convert.ToUInt64(cr1.LSHET) < Convert.ToUInt64(cr2.LSHET))
                return -1;
            else if (Convert.ToUInt64(cr1.LSHET) > Convert.ToUInt64(cr2.LSHET))
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
    }
}
