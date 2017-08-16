using System;
using System.Collections.Generic;
using System.Linq;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class CounterChangeType : IGGMMChangeType
    {
        public CNV_COUNTER Counter;
        public CNV_CNTRSIND Indication;

        public CounterChangeType(CNV_COUNTER counter, CNV_CNTRSIND indication)
        {
            Counter = counter;
            Indication = indication;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.CounterList.Add(Counter);
            if (Indication != null) DigitChangesImport.CntIndList.Add(Indication);
        }
    }

    public class CounterChangeTypeFactory : IGGMMChangeTypeFactory
    {
        private static readonly Dictionary<string, List<GGMMChangeRecord>> ChangedCounters = new Dictionary<string, List<GGMMChangeRecord>>();

        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 4)
            {
                if (record.Графа == 24 || record.Графа == 26 || record.Графа == 27 || record.Графа == 29 || record.Графа == 35)
                {
                    var graphInfo = record.GetGraphInfo();
                    if (!DigitChangesImport.IsServiceActive(record.LsKvc.CombinedLs, graphInfo.Vid))
                    {
                        DigitChangesImport.ErrorLog.Add(record, $"Запрещено изменять счетчики. Вид {graphInfo.Vid} не открыт или закрыт");
                        return new SkipChangeType();
                    }

                    List<GGMMChangeRecord> changedCounters;
                    if (ChangedCounters.TryGetValue(record.LsKvc.Ls, out changedCounters))
                    {
                        var prevCounter = changedCounters.FirstOrDefault(c => c.КодСчетчика == record.КодСчетчика && c.GetGraphInfo().Vid == graphInfo.Vid);
                        if (prevCounter != null)
                        {
                            if (prevCounter.Значение != record.Значение)
                                throw new Exception($"Лс {record.LsKvc.Ls} содержит два разных изменения по счетчику {record.КодСчетчика}");
                        }
                        else
                            changedCounters.Add(record);
                    }
                    else
                        ChangedCounters.Add(record.LsKvc.Ls, new List<GGMMChangeRecord> {record});

                    string lshet = Consts.LsDic[record.LsKvc.Ls];
                    CNV_CNTRSIND indication = null;
                    string cntTag = $"{lshet.Substring(2, 6)}{graphInfo.Vid:D2}{record.КодСчетчика:D2}";
                    int cntId;
                    if (!DigitChangesImport.CounterTags.TryGetValue(cntTag, out cntId))
                        cntId = -1;
                    var counterInfo = new CounterInfo(record.Значение, graphInfo.Vid);
                    int cntType;
                    int setupPlace;
                    ConvertCounters.ConvertCounterType(graphInfo.Vid, record.КодСчетчика, counterInfo.Razryad, out cntType, out setupPlace);
                    var counter = new CNV_COUNTER
                    {
                        LSHET = lshet,
                        COUNTERID = cntId.ToString(),
                        COUNTER_LEVEL = 0,
                        NAME = $"Сч. {DigitChangesImport.VidSpr.Single(v => v.R1 == graphInfo.Vid).Sr40} {record.КодСчетчика}",
                        TAG = cntTag,
                        CNTTYPE = cntType,
                        SETUPPLACE = setupPlace,
                    };
                    var abonentCounter = DigitChangesImport.AbonentCounters[record.LsKvc.CombinedLs][Consts.resourceRecode[graphInfo.Vid]];
                    if (record.Значение == 0)
                    {
                        counter.DEACTDATE = record.FileDate.AddMonths(1);
                        abonentCounter.NewCount--;
                        abonentCounter.NewDate = record.FileDate.AddMonths(1);
                    }
                    else
                    {
                        bool newCounter = counter.COUNTERID == "-1";
                        int? nocalcchildbalances = null;
                        var cntFromSpr = DigitChangesImport.CntSpr.FirstOrDefault(cnt => cnt.R1 == graphInfo.Vid && cnt.R2 == record.КодСчетчика);
                        if (cntFromSpr != null && cntFromSpr.R4 == 0) nocalcchildbalances = 1;
                        //counter.SETUPDATE = newCounter ? record.FileDate.AddMonths(1) : record.FileDate;
                        counter.SETUPDATE = abonentCounter.IsCounter
                                    ? record.FileDate
                                    : record.FileDate.AddMonths(1);
                        counter.NOCALCCHILDBALANCES = nocalcchildbalances;

                        indication = new CNV_CNTRSIND
                        {
                            COUNTERID = newCounter ? cntTag : counter.COUNTERID,
                            //INDDATE = newCounter ? record.FileDate.AddMonths(1) : record.FileDate,
                            INDDATE = newCounter || record.Графа == 35 
                                ? record.FileDate.AddMonths(1) 
                                : abonentCounter.IsCounter 
                                    ? record.FileDate 
                                    : record.FileDate.AddMonths(1),
                            INDTYPE = newCounter ? -1 : 1,
                            INDICATION = counterInfo.Ind
                        };
                        abonentCounter.NewCount += newCounter ? 1 : 0;
                        abonentCounter.NewDate = record.FileDate.AddMonths(1);
                    }
                    return new CounterChangeType(counter, indication);
                }
                else 
                    return null;
            }
            else
                return null;
        }

        public class CounterInfo
        {
            public readonly byte Razryad;
            public readonly decimal Ind;

            public CounterInfo(int info, byte vid)
            {
                string sInfo = $"{info:D8}";
                Razryad = byte.Parse(sInfo.Substring(0, 1));
                Ind = int.Parse(sInfo.Substring(1))/RsnHelper.GetDigitsCount(vid, true);
            }
        }
    }
}
