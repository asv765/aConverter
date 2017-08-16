using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class AbonentRecordUtils
    {
        /// <summary>
        /// Заполняет уникальными значениями поле DISTKOD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueDistkod(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.DISTKOD == null && p.DISTNAME != null))
            {
                object o = new
                {
                    RayonKod = ar.RAYONKOD,
                    TownsKod = ar.TOWNSKOD,
                    DistName = ar.DISTNAME,
                };
                int distKod;
                if (!keysdic.TryGetValue(o, out distKod))
                {
                    distKod = startValue + (++counter);
                    keysdic.Add(o, distKod);
                }
                ar.DISTKOD = distKod;
            }
        }

        /// <summary>
        /// Заполняет уникальными значениями поле SETTLEMENTKOD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueSettlementkod(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.SETTLEMENTKOD == null && p.SETTLEMENTNAME != null))
            {
                object o = new
                {
                    SettlementName = ar.SETTLEMENTNAME,
                };
                int settlementKod;
                if (!keysdic.TryGetValue(o, out settlementKod))
                {
                    settlementKod = startValue + (++counter);
                    keysdic.Add(o, settlementKod);
                }
                ar.SETTLEMENTKOD = settlementKod;
            }
        }

        /// <summary>
        /// Заполняет уникальными значениями поле TOWNSKOD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueTownskod(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.TOWNSKOD == null))
            {
                object o = new
                {
                    RayonKod = ar.RAYONKOD,
                    SettlementKod = ar.SETTLEMENTKOD,
                    TownName = ar.TOWNSNAME
                };
                int townsKod;
                if (!keysdic.TryGetValue(o, out townsKod))
                {
                    townsKod = startValue + (++counter);
                    keysdic.Add(o, townsKod);
                }
                ar.TOWNSKOD = townsKod;
            }
        }

        /// <summary>
        /// Заполняет уникальными значениями поле RAYONKOD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueRayonkod(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.RAYONKOD == null))
            {
                object o = new
                {
                    RayonName = ar.RAYONNAME,
                };
                int rayonKod;
                if (!keysdic.TryGetValue(o, out rayonKod))
                {
                    rayonKod = startValue + (++counter);
                    keysdic.Add(o, rayonKod);
                }
                ar.RAYONKOD = rayonKod;
            }
        }

        /// <summary>
        /// Заполняет уникальными значениями поле ULICAKOD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueUlicakod(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.ULICAKOD == null))
            {
                object o = new
                {
                    RayonKod = ar.RAYONKOD,
                    TownsKod = ar.TOWNSKOD,
                    UlicaName = ar.ULICANAME
                };
                int ulicaKod;
                if (!keysdic.TryGetValue(o, out ulicaKod))
                {
                    ulicaKod = startValue + (++counter);
                    keysdic.Add(o, ulicaKod);
                }
                ar.ULICAKOD = ulicaKod;
            }
        }

        /// <summary>
        /// Заполняет уникальными значениями поле HouseCD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueHouseCd(IEnumerable<CNV_ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_ABONENT ar in list.Where(p => p.HOUSECD == null))
            {
                object o = new
                {
                    RayonKod = ar.RAYONKOD,
                    TownsKod = ar.TOWNSKOD,
                    UlicaKod = ar.ULICAKOD,
                    Houseno = ar.HOUSENO,
                    Housepostfix = ar.HOUSEPOSTFIX,
                    Housenote = ar.HOUSENOTE,
                    Korpusno = ar.KORPUSNO,
                    Korpuspostfix = ar.KORPUSPOSTFIX
                };
                int housecd;
                if (!keysdic.TryGetValue(o, out housecd))
                {
                    housecd = startValue + (++counter);
                    keysdic.Add(o, housecd);
                }
                ar.HOUSECD = housecd;
            }
        }
    } 
}
