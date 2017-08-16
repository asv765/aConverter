using System.Linq;
using aConverterClassLibrary.RecordsDataAccessORM;
using RsnReader;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class CorretPaymentChangeType : IGGMMChangeType
    {
        public int ServiceId;
        public decimal Summa;
        public bool IsPeni;

        public CorretPaymentChangeType(int serviceId, decimal summa, bool isPeni = false)
        {
            ServiceId = serviceId;
            Summa = summa;
            IsPeni = isPeni;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.PayList.Add(new CNV_OPLATA
            {
                SERVICECD = ServiceId,
                SERVICENAME = ServiceId.ToString(),
                //SOURCECD = int.Parse(sourcecd),
                SOURCENAME = $"import{record.FileDate.ToString("yyMM")}",
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                SUMMA = Summa,
                MONTH_ = record.РасчетныйМесяц,
                YEAR_ = record.РасчетныйГод,
                DATE_ = record.FileDate,
                DATE_VV = record.FileDate,
                DOCUMENTCD = $"P{record.FileDate.ToString("yyMMdd")}",
                SOURCECD = IsPeni ? 10 : 0
            });
        }
    }

    public class CorretPaymentChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 10 /*&& record.ТипНачисления != 11*/)
            {
                var graphInfo = record.GetGraphInfo();
                bool isPeni = RsnHelper.PeniResources.Contains(graphInfo.Vid);
                var resource = isPeni
                    ? Consts.resourceRecode[Consts.PeniRecode[graphInfo.Vid]]
                    : Consts.resourceRecode[graphInfo.Vid];
                int serviceId = DigitChangesImport.AbonentServices[record.LsKvc.CombinedLs][resource];
                return new CorretPaymentChangeType(serviceId, record.Значение / 100m * -1, isPeni);
            }
            else
                return null;
        }
    }
}
