using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary.Records
{
    public class CntrsindRecordUtils
    {
        /// <summary>
        /// Прореживает список показаний
        /// </summary>
        public static List<CntrsindRecord> ThinOutList(List<CntrsindRecord> cirl)
        {
            // Сортируем список
            List<CntrsindRecord> rcirl = new List<CntrsindRecord>();
            cirl.Sort(CompareCntrsind);
            CntrsindRecord oldcir = new CntrsindRecord();
            // Удалем дублирующиеся строки
            foreach(CntrsindRecord cir in cirl)
            {
                if (CompareCntrsind(cir, oldcir) != 0)
                {
                    rcirl.Add(cir);
                    oldcir = cir;
                }
            }
            return rcirl;
        }

        public static void RestoreHistory(ref List<CntrsindRecord> cirl, RestoreHistoryType restoreHistoryType)
        {
            // Сортируем список
            // List<CntrsindRecord> rcirl = new List<CntrsindRecord>();
            cirl.Sort(CompareCntrsind);
            if (restoreHistoryType == RestoreHistoryType.С_конца_по_конечным_показаниям ||
                restoreHistoryType == RestoreHistoryType.С_конца_по_объемам)
            {
                cirl.Reverse();
            }
            CntrsindRecord oldcir = new CntrsindRecord();
            oldcir.Counterid = "Not Defined";
            // Удаляем дублирующиеся строки
            foreach (CntrsindRecord cir in cirl)
            {
                if (restoreHistoryType == RestoreHistoryType.С_конца_по_конечным_показаниям)
                {
                    if (oldcir.Counterid == cir.Counterid)
                    {
                        oldcir.Oldind = cir.Indication;
                        oldcir.Ob_em = oldcir.Indication - oldcir.Oldind;
                    }
                    else
                    {
                        oldcir.Oldind = 0;
                        oldcir.Ob_em = 0;
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
        public static int CompareCntrsind(CntrsindRecord cir1, CntrsindRecord cir2)
        {
            int compare = String.Compare(cir1.Counterid, cir2.Counterid);
            if (compare == 0) compare = DateTime.Compare(cir1.Inddate, cir2.Inddate);
            if (compare == 0)
            {
                if ((int)cir1.Indtype < (int)cir2.Indtype) 
                    compare = -1;
                else if ((int)cir1.Indtype > (int)cir2.Indtype) 
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
