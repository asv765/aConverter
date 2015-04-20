using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsEDM
{
    public class SupplnetRecordUtils
    {
        /// <summary>
        /// Нормализует список подключения к транспортной сети. Если абонент по данной услуге подключается к другому узлу, то 
        /// должен отключиться от предыдущего узла
        /// </summary>
        /// <param name="lrl"></param>
        public static List<supplnet> Normalize(List<supplnet> crl)
        {
            // Сортируем список
            List<supplnet> rcrl = new List<supplnet>();
            crl.Sort(CompareSupplnet);
            rcrl.AddRange(crl);
            // Удаляем дублирующиеся строки
            supplnet oldsr = crl[0];
            for (int i = 0; i < crl.Count; i++)
            {
                if ((crl[i].LSHET == oldsr.LSHET && crl[i].SERVICECD == oldsr.SERVICECD) &&
                    (crl[i].KNOTL1CD != oldsr.KNOTL1CD || crl[i].KNOTL2CD != oldsr.KNOTL2CD))
                {
                    supplnet newsr = oldsr.CloneRecord();
                    newsr.SUPPDATE = crl[i].SUPPDATE;
                    newsr.CONNECTED = false;
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
        public static int CompareSupplnet(supplnet sr1, supplnet sr2)
        {
            int compare = String.Compare(sr1.LSHET, sr2.LSHET);
            if (compare == 0)
            {
                int g = (int)sr1.SERVICECD; 
                compare = g.CompareTo(sr2.SERVICECD);
                if (compare == 0) compare = DateTime.Compare((DateTime)sr1.SUPPDATE, (DateTime)sr2.SUPPDATE);
            }
            return compare;
        }
    }

    public partial class supplnet
    {
        public supplnet CloneRecord()
        {
            supplnet sr = new supplnet()
            {
                LSHET = this.LSHET,
                SERVICECD = this.SERVICECD,
                KNOTL1CD = this.KNOTL1CD,
                KNOTL1NAME = this.KNOTL1NAME,
                KNOTL2CD = this.KNOTL2CD,
                KNOTL2NAME = this.KNOTL2NAME,
                CONNECTED = this.CONNECTED,
                SUPPDATE = this.SUPPDATE
            };

            return sr;
        }
    }
 }
