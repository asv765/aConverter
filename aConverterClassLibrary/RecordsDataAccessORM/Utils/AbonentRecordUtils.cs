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
