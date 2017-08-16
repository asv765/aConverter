using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class DebdKvHChangeType : IGGMMChangeType
    {
        public int ServiceId;
        public decimal Volume;

        public DebdKvHChangeType(int serviceId, decimal volume)
        {
            ServiceId = serviceId;
            Volume = volume;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.NachList.Add(new CNV_NACH
            {
                TYPE_ = 1,
                REGIMCD = 10,
                REGIMNAME = "Неизвестен",
                SERVICECD = ServiceId,
                SERVICENAME = ServiceId.ToString(),
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                VOLUME = Volume,
                MONTH_ = record.РасчетныйМесяц,
                MONTH2 = record.РасчетныйМесяц,
                YEAR_ = record.РасчетныйГод,
                YEAR2 = record.РасчетныйГод,
                DATE_VV = record.FileDate,
                DOCUMENTCD = $"D{record.FileDate.ToString("yyMMdd")}0",
                AUTOUSE = 0
            });
        }
    }

    public class DebdKvHChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 4 && record.Графа == 65)
            {
                var graphInfo = record.GetGraphInfo();
                var resource = Consts.resourceRecode[graphInfo.Vid];
                int serviceId = DigitChangesImport.AbonentServices[record.LsKvc.CombinedLs][resource];
                return new DebdKvHChangeType(serviceId, record.Значение / RsnHelper.GetDigitsCount(graphInfo.Vid, true));
            }
            else 
                return null;
        }
    }
}
