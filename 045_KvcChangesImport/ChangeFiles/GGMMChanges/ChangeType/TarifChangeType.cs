using aConverterClassLibrary.RecordsDataAccessORM;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class TarifChangeType : IGGMMChangeType
    {
        public int CcharCd;
        public string CcharName;
        public decimal CcharValue;

        public TarifChangeType(int ccharCd, decimal value, string ccharName = null)
        {
            CcharCd = ccharCd;
            CcharName = ccharName;
            CcharValue = value;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.CcharsList.Add(new CNV_CHAR
            {
                CHARCD = CcharCd,
                CHARNAME = CcharName,
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                DATE_ = record.FileDate,
                VALUE_ = CcharValue,
            });
        }
    }

    public class TarifChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            int ccharCd;
            string ccharName;
            if (record.ТипНачисления == 4) //TODO может быть так, что сразу две характеристики в двух типах начислений
            {
                switch (record.Графа)
                {
                    case 34:
                        ccharCd = 55;
                        ccharName = "тариф сч-ки 05";
                        break;
                    case 36:
                        ccharCd = 56;
                        ccharName = "тариф сч-ки 07";
                        break;
                    case 37:
                        ccharCd = 59;
                        ccharName = "тариф сч-ки 06";
                        break;
                    case 39:
                        ccharCd = 58;
                        ccharName = "тариф сч-ки 03";
                        break;
                    default:
                        return null;
                }
            }
            else if (record.ТипНачисления == 6)
            {
                if (record.Графа >= 34 && record.Графа <= 39)
                {
                    switch (record.Графа)
                    {
                        case 34:
                            ccharCd = 55;
                            ccharName = "тариф сч-ки 05";
                            break;
                        case 36:
                            ccharCd = 56;
                            ccharName = "тариф сч-ки 07";
                            break;
                        case 37:
                            ccharCd = 59;
                            ccharName = "тариф сч-ки 06";
                            break;
                        case 39:
                            ccharCd = 58;
                            ccharName = "тариф сч-ки 03";
                            break;
                        default:
                            return null;
                    }
                }
                else 
                    return null;
            }
            else
                return null;

            return new TarifChangeType(ccharCd, record.Значение / 100m, ccharName);
        }
    }
}
