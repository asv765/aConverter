using System.Collections.Generic;
using System.Linq;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public static class AddCharsRecordUtils
    {
        public static List<CNV_AADDCHAR> ThinOutList(List<CNV_AADDCHAR> la)
        {
            return la.GroupBy(a => new {a.LSHET, a.ADDCHARCD})
                .Select(ga => ga.Last())
                .ToList();
        }
    }
}
