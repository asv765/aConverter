using System.Collections.Generic;
using System.Linq;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class ReculcChangeType : IGGMMChangeType
    {
        public int ServiceId;
        public decimal Summa;
        public bool IsPeni;

        public ReculcChangeType(int serviceId, decimal summa, bool isPeni = false)
        {
            ServiceId = serviceId;
            Summa = summa;
            IsPeni = isPeni;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.NachList.Add(new CNV_NACH
            {
                TYPE_ = IsPeni ? 10 : 0,
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                SERVICECD = ServiceId,
                SERVICENAME = ServiceId.ToString(),
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                PROCHL = Summa,
                MONTH_ = record.РасчетныйМесяц,
                MONTH2 = record.РасчетныйМесяц,
                YEAR_ = record.РасчетныйГод,
                YEAR2 = record.РасчетныйГод,
                DATE_VV = record.FileDate,
                DOCUMENTCD = $"R{record.FileDate.ToString("yyMMdd")}0",
                AUTOUSE = 0
            });
        }
    }

    public class ReculcChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 4)
            {
                if ((record.Графа >= 11 && record.Графа <= 23) ||
                    (record.Графа == 28) ||
                    (record.Графа >= 30 && record.Графа <= 33) ||
                    (record.Графа >= 40 && record.Графа <= 41) ||
                    (record.Графа == 45) ||
                    (record.Графа >= 52 && record.Графа <= 63) ||
                    (record.Графа >= 68 && record.Графа <= 95))
                {
                    var reculc = CreateReculc(record);
                    return reculc ?? (IGGMMChangeType)new SkipChangeType();
                }
                else
                    return null;
            }
            else if (record.ТипНачисления == 5)
            {
                var reculc = CreateReculc(record);
                return reculc ?? (IGGMMChangeType)new SkipChangeType();
            }
            //else if (record.ТипНачисления == 13)
            //{
            //    var reculc = CreateReculc(record);
            //    reculc.Summa *= -1;
            //    return reculc;
            //}
            else 
                return null;
        }

        private static ReculcChangeType CreateReculc(GGMMChangeRecord record)
        {
            var graphInfo = record.GetGraphInfo();
            bool isPeni = RsnHelper.PeniResources.Contains(graphInfo.Vid);
            var resource = isPeni
                ? Consts.resourceRecode[Consts.PeniRecode[graphInfo.Vid]]
                : Consts.resourceRecode[graphInfo.Vid];
            Dictionary<int, int> abonentServices;
            int serviceId = -1;
            if (DigitChangesImport.AbonentServices.TryGetValue(record.LsKvc.CombinedLs, out abonentServices))
                if (abonentServices.ContainsKey(resource)) serviceId = abonentServices[resource];
            if (serviceId == -1)
            {
                DigitChangesImport.ErrorLog.Add(record, $"Не удалось получить услугу по ресурсу {resource} для абонента {record.LsKvc.Ls}");
                return null;
            }
            return new ReculcChangeType(serviceId, record.Значение / 100m, isPeni);
        }
    }
}
