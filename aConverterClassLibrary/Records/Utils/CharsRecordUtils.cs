using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary.Records;
using System.Data;
using System.Data.OleDb;

namespace aConverterClassLibrary.Records
{
    public class CharsRecordUtils
    {
        /// <summary>
        /// Прореживает список количественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CharsRecord> ThinOutList(List<CharsRecord> crl)
        {
            // Сортируем список
            List<CharsRecord> rcrl = new List<CharsRecord>();
            crl.Sort(CompareChars);
            // Удалем дублирующиеся строки
            string oldlshet = ""; long oldcharcd = -1; decimal oldcharvalue = -1;
            for (int i = 0; i < crl.Count; i++)
            {
                if (crl[i].Lshet != oldlshet || crl[i].Charcd != oldcharcd || crl[i].Value_ != oldcharvalue)
                {
                    rcrl.Add(crl[i]);
                    oldlshet = crl[i].Lshet;
                    oldcharcd = crl[i].Charcd;
                    oldcharvalue = crl[i].Value_;
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
        public static int CompareChars(CharsRecord cr1, CharsRecord cr2)
        {
            if (Convert.ToUInt64(cr1.Lshet) < Convert.ToUInt64(cr2.Lshet))
                return -1;
            else if (Convert.ToUInt64(cr1.Lshet) > Convert.ToUInt64(cr2.Lshet))
                return 1;
            else
            {
                if (cr1.Charcd < cr2.Charcd)
                    return -1;
                else if (cr1.Charcd > cr2.Charcd)
                    return 1;
                else
                {
                    if (cr1.Date < cr2.Date)
                        return -1;
                    else if (cr1.Date > cr2.Date)
                        return 1;
                    else
                        return 0;
                }
            }
        }
    }
}
