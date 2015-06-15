using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM
{
    public class SupplnetRecordUtils
    {
        /// <summary>
        /// Нормализует список подключения к транспортной сети. Если абонент по данной услуге подключается к другому узлу, то 
        /// должен отключиться от предыдущего узла
        /// </summary>
        /// <param name="lrl"></param>
        public static List<SUPPLNET> Normalize(List<SUPPLNET> crl)
        {
            // Сортируем список
            List<SUPPLNET> rcrl = new List<SUPPLNET>();
            crl.Sort(CompareSupplnet);
            rcrl.AddRange(crl);
            // Удаляем дублирующиеся строки
            SUPPLNET oldsr = crl[0];
            for (int i = 0; i < crl.Count; i++)
            {
                if ((crl[i].LSHET == oldsr.LSHET && crl[i].SERVICECD == oldsr.SERVICECD) &&
                    (crl[i].KNOTL1CD != oldsr.KNOTL1CD || crl[i].KNOTL2CD != oldsr.KNOTL2CD))
                {
                    SUPPLNET newsr = oldsr.CloneRecord();
                    newsr.SUPPDATE = crl[i].SUPPDATE;
                    newsr.CONNECTED = 0;
                    rcrl.Add(newsr);
                    oldsr = crl[i];
                }
            }
            return rcrl;
        }

        /// <summary>
        /// Метод-делегат для сравнения двух характеристи LSuplnetRecord
        /// </summary>
        /// <param name="lr1"></param>
        /// <param name="lr2"></param>
        /// <returns></returns>
        public static int CompareSupplnet(SUPPLNET sr1, SUPPLNET sr2)
        {
            int compare = System.String.CompareOrdinal(sr1.LSHET, sr2.LSHET);
            if (compare == 0)
            {
                int g = (int)sr1.SERVICECD;
                compare = g.CompareTo(sr2.SERVICECD);
                if (compare == 0) compare = DateTime.Compare((DateTime)sr1.SUPPDATE, (DateTime)sr2.SUPPDATE);
            }
            return compare;
        }
    }
}
