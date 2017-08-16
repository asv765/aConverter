using System.Collections.Generic;
using System.Linq;
using System;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public static class CitizenRecordUtils
    {
        /// <summary>
        /// Заполняет уникальными значениями поле DORGCD списка абонентов
        /// </summary>
        /// <param name="list"></param>
        public static void SetUniqueDorgcd(IEnumerable<CNV_CITIZEN> list, int startValue)
        {
            int counter = 0;
            var keysdic = new Dictionary<object, int>();
            foreach (CNV_CITIZEN ct in list.Where(c => c.DORGCD == null && !string.IsNullOrWhiteSpace(c.DORGNAME)))
            {
                object o = new
                {
                    Dorgname = ct.DORGNAME,
                };
                int dorgcd;
                if (!keysdic.TryGetValue(o, out dorgcd))
                {
                    dorgcd = startValue + (++counter);
                    keysdic.Add(o, dorgcd);
                }
                ct.DORGCD = dorgcd;
            }
        }

        /// <summary>
        /// Прореживает список качественных характеристик
        /// </summary>
        /// <param name="lrl"></param>
        public static List<CNV_CITIZENMIGRATION> ThinOutList(List<CNV_CITIZENMIGRATION> lrl)// -----------------------------
        {
            // Сортируем список
            var rlrl = new List<CNV_CITIZENMIGRATION>();
            lrl.Sort(CompareLchars);
            // Удалем дублирующиеся строки
            int oldCitizenid = -1;  int oldDirection = -1; int oldMigration = -1;
            for (int i = 0; i < lrl.Count; i++)
            {
                var t = lrl[i];
                if (t.CITIZENID != oldCitizenid || t.DIRECTION != oldDirection /*|| t.MIGRATIONTYPE != oldMigration*/)
                {
                    rlrl.Add(t);
                    oldCitizenid = t.CITIZENID;
                    oldDirection = t.DIRECTION;
                    /*oldMigration = t.MIGRATIONTYPE;*/
                }
            }
            lrl.Clear();
            lrl.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return rlrl;
        }


        public static int CompareLchars(CNV_CITIZENMIGRATION cm1, CNV_CITIZENMIGRATION cm2)
        {
            if (cm1.CITIZENID < cm2.CITIZENID)
                return -1;
            if (cm1.CITIZENID > cm2.CITIZENID)
                return 1;
            //if (cm1.DIRECTION < cm2.DIRECTION)
            //    return -1;
            //if (cm1.DIRECTION > cm2.DIRECTION)
            //    return 1;
            //if (cm1.MIGRATIONTYPE < cm2.MIGRATIONTYPE)
            //    return -1;
            //if (cm1.MIGRATIONTYPE > cm2.MIGRATIONTYPE)
            //    return 1;
            if (cm1.DATE_ < cm2.DATE_)
                return -1;
            if (cm1.DATE_ > cm2.DATE_)
                return 1;
            return 0;
        }
    }
}
