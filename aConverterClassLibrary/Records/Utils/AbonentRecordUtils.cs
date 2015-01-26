using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records.Utils
{
    public class AbonentRecordUtils
    {
        /// <summary>
        /// Заполняет уникальными значениями поле HouseCD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueHouseCd(ref List<AbonentRecord> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (AbonentRecord ar in list)
            {
                // RAYONKOD, TOWNSKOD, ULICAKOD, NDOMA, KORPUS
                object o = new {
                    RayonKod = ar.Rayonkod,
                    TownsKod = ar.Townskod,
                    UlicaKod = ar.Ulicakod,
                    Ndoma = ar.Ndoma,
                    Korpus = ar.Korpus
                };
                int housecd;
                if (!keysdic.TryGetValue(o, out housecd))
                {
                    housecd = startValue + (++counter);
                    keysdic.Add(o, housecd);
                }
                ar.Housecd = housecd;
            }
        }
    }
}
