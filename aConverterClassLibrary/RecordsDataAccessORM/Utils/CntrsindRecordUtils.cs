using System;
using System.Collections.Generic;

namespace aConverterClassLibrary.RecordsDataAccessORM.Utils
{
    public class CntrsindRecordUtils
    {
        /// <summary>
        /// Прореживает список показаний
        /// </summary>
        public static List<CNTRSIND> ThinOutList(List<CNTRSIND> cirl)
        {
            // Сортируем список
            List<CNTRSIND> rcirl = new List<CNTRSIND>();
            cirl.Sort(CompareCntrsind);
            CNTRSIND oldcir = new CNTRSIND();
            // Удалем дублирующиеся строки
            foreach (CNTRSIND cir in cirl)
            {
                if (CompareCntrsind(cir, oldcir) != 0)
                {
                    rcirl.Add(cir);
                    oldcir = cir;
                }
            }
            return rcirl;
        }

        public static void RestoreHistory(ref List<CNTRSIND> cirl, RestoreHistoryType restoreHistoryType)
        {
            // Сортируем список
            // List<CntrsindRecord> rcirl = new List<CntrsindRecord>();
            cirl.Sort(CompareCntrsind);
            if (restoreHistoryType == RestoreHistoryType.С_конца_по_конечным_показаниям ||
                restoreHistoryType == RestoreHistoryType.С_конца_по_объемам)
            {
                cirl.Reverse();
            }
            CNTRSIND oldcir = new CNTRSIND();
            oldcir.COUNTERID = "Not Defined";
            // Удаляем дублирующиеся строки
            foreach (CNTRSIND cir in cirl)
            {
                if (restoreHistoryType == RestoreHistoryType.С_конца_по_конечным_показаниям)
                {
                    if (oldcir.COUNTERID == cir.COUNTERID)
                    {
                        oldcir.OLDIND = cir.INDICATION;
                        oldcir.OB_EM = oldcir.INDICATION - oldcir.OLDIND;
                    }
                    else
                    {
                        oldcir.OLDIND = 0;
                        oldcir.OB_EM = 0;
                    }
                }
                else if (restoreHistoryType == RestoreHistoryType.С_конца_по_объемам)
                {
                    throw new Exception("Непроверено. Надо написать тесты");
                    //if (oldcir.Counterid == cir.Counterid)
                    //{
                    //    cir.Indication = oldcir.Oldind;
                    //}
                    //cir.Oldind = cir.Indication - cir.Ob_em;
                }
                else if (restoreHistoryType == RestoreHistoryType.С_начала_по_объемам)
                {
                    throw new Exception("Непроверено. Надо написать тесты");
                    //if (oldcir.Counterid == cir.Counterid)
                    //{
                    //    cir.Oldind = oldcir.Indication;
                    //}
                    //cir.Indication = cir.Oldind + cir.Ob_em;
                }
                else
                {
                    throw new ArgumentException("Неверный тип восстановления истории показаний");
                }
                oldcir = cir;
            }
        }


        /// <summary>
        /// Метод-делегат для сравнения двух записей в Cntrsind
        /// </summary>
        /// <returns></returns>
        public static int CompareCntrsind(CNTRSIND cir1, CNTRSIND cir2)
        {
            int compare = System.String.CompareOrdinal(cir1.COUNTERID, cir2.COUNTERID);
            if (compare == 0) compare = DateTime.Compare((DateTime)cir1.INDDATE, (DateTime)cir2.INDDATE);
            if (compare == 0)
            {
                if ((int)cir1.INDTYPE < (int)cir2.INDTYPE)
                    compare = -1;
                else if ((int)cir1.INDTYPE > (int)cir2.INDTYPE)
                    compare = 1;
                else
                    compare = 0;
            }
            return compare;
        }
    }

    public enum RestoreHistoryType
    {
        С_конца_по_конечным_показаниям,
        С_конца_по_объемам,
        С_начала_по_объемам
    }
}
