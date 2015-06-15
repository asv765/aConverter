using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class AbonentRecordUtils
    {
        /// <summary>
        /// Заполняет уникальными значениями поле HouseCD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueHouseCd(ref List<ABONENT> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (ABONENT ar in list)
            {
                // RAYONKOD, TOWNSKOD, ULICAKOD, NDOMA, KORPUS
                object o = new
                {
                    RayonKod = ar.RAYONKOD,
                    TownsKod = ar.TOWNSKOD,
                    UlicaKod = ar.ULICAKOD,
                    Ndoma = ar.NDOMA,
                    Korpus = ar.KORPUS
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
