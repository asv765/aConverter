using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    public class SupplnetRecordUtils
    {
        /// <summary>
        /// Нормализует список подключения к транспортной сети. Если абонент по данной услуге подключается к другому узлу, то 
        /// должен отключиться от предыдущего узла
        /// </summary>
        /// <param name="lrl"></param>
        public static List<SupplnetRecord> Normalize(List<SupplnetRecord> crl)
        {
            // Сортируем список
            List<SupplnetRecord> rcrl = new List<SupplnetRecord>();
            crl.Sort(CompareSupplnet);
            rcrl.AddRange(crl);
            // Удаляем дублирующиеся строки
            SupplnetRecord oldsr = crl[0];
            for (int i = 0; i < crl.Count; i++)
            {
                if ((crl[i].Lshet == oldsr.Lshet && crl[i].Servicecd == oldsr.Servicecd) &&
                    (crl[i].Knotl1cd != oldsr.Knotl1cd || crl[i].Knotl2cd != oldsr.Knotl2cd))
                {
                    SupplnetRecord newsr = oldsr.CloneRecord();
                    newsr.Suppdate = crl[i].Suppdate;
                    newsr.Connected = false;
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
        public static int CompareSupplnet(SupplnetRecord sr1, SupplnetRecord sr2)
        {
            int compare = String.Compare(sr1.Lshet, sr2.Lshet);
            if (compare == 0)
            {
                compare = sr1.Servicecd.CompareTo(sr2.Servicecd);
                if (compare == 0) compare = DateTime.Compare(sr1.Suppdate, sr2.Suppdate);
            }
            return compare;
        }
    }

    public partial class SupplnetRecord
    {
        public SupplnetRecord CloneRecord()
        {
            SupplnetRecord sr = new SupplnetRecord()
            {
                Lshet = this.Lshet,
                Servicecd = this.Servicecd,
                Knotl1cd = this.Knotl1cd,
                Knotl1name = this.Knotl1name,
                Knotl2cd = this.Knotl2cd,
                Knotl2name = this.Knotl2name,
                Connected = this.Connected,
                Suppdate = this.Suppdate
            };

            return sr;
        }
    }
 }
