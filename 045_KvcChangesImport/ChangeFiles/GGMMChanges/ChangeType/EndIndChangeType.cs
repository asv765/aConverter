using System;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class EndIndChangeType : IGGMMChangeType
    {
        public CNV_CNTRSIND Indication;

        public EndIndChangeType(CNV_CNTRSIND indication)
        {
            Indication = indication;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.CntIndList.Add(Indication);
        }
    }

    public class EndIndChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 4 && record.Графа == 25)
            {
                return new SkipChangeType(); //TODO заменилось на Isch
                var graphInfo = record.GetGraphInfo();
                string lshet = Consts.LsDic[record.LsKvc.Ls];
                return new EndIndChangeType(new CNV_CNTRSIND
                {
                    INDDATE = new DateTime(record.РасчетныйГод, record.РасчетныйМесяц, DateTime.DaysInMonth(record.РасчетныйГод, record.РасчетныйМесяц)),
                    INDTYPE = 1,
                    INDICATION = record.Значение / RsnHelper.GetDigitsCount(graphInfo.Vid, true),
                    COUNTERID = $"{lshet.Substring(2, 6)}{graphInfo.Vid:D2}{record.КодСчетчика:D2}"
                });
            }
            else 
                return null;
        }
    }
}
