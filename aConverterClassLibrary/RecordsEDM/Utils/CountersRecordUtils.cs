using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.RecordsEDM
{
    public enum CounterSetupPlace
    {
        Единственный = 0,
        Ванна_туалет = 1,
        Кухня = 2,
        Душ = 3,
        Cчетчик_4 = 4,
        Cчетчик_5 = 5,
        Cчетчик_6 = 6
    }


    //public class CountersRecordUtils
    //{
    //    public static CounterSetupPlace GetByString(string place)
    //    {
    //        string placeToFind = place.ToUpper();
    //        if (place.Contains("САНУЗЕЛ") || place.Contains("ТУАЛЕТ") || place.Contains("ВАННАЯ"))
    //        {
    //            return CounterSetupPlace.Ванна_туалет;
    //        }
    //        else if (place.Contains("САНУЗЕЛ") || place.Contains("ТУАЛЕТ") || place.Contains("ВАННАЯ"))
    //        {

    //        }

    //    }
    //}
}
