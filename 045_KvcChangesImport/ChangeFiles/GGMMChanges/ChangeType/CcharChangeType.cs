using aConverterClassLibrary.RecordsDataAccessORM;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class CcharChangeType : IGGMMChangeType
    {
        public int CcharId;
        public string CcharName;
        public decimal CcharValue;

        public CcharChangeType(int ccharId, decimal ccharValue, string ccharName = null)
        {
            CcharId = ccharId;
            CcharValue = ccharValue;
            CcharName = ccharName;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.CcharsList.Add(new CNV_CHAR
            {
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                CHARCD = CcharId,
                CHARNAME = CcharName,
                VALUE_ = CcharValue,
                DATE_ = record.FileDate
            });
        }
    }

    public class CcharChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления != 3) return null;
            int ccharId;
            string ccharName;
            decimal fraction = 0;
            switch (record.Графа)
            {
                case 13:
                    ccharId = 23;
                    ccharName = "кол-во комнат";
                    break;
                case 19: 
                    ccharId = 46;
                    ccharName = "кол-во каналов";
                    break;
                case 29: 
                    ccharId = 8;
                    ccharName = "площадь зем.уч.";
                    fraction = 10m;
                    break;
                case 30: 
                    ccharId = 2;
                    ccharName = "общая площадь";
                    fraction = 10m;
                    break;
                case 50: 
                    ccharId = 7;
                    ccharName = "полив. сотки";
                    fraction = 100m;
                    break;
                case 60:
                    ccharId = 4;
                    ccharName = "площ. газ.";
                    fraction = 10m;
                    break;
                case 67: 
                    ccharId = 14;
                    ccharName = "Жилая площадь";
                    fraction = 10m;
                    break;
                case 68: 
                    ccharId = 5;
                    ccharName = "Площадь комнат";
                    fraction = 10m;
                    break;
                case 200:
                    ccharId = 40;
                    ccharName = "Число коров";
                    break;
                case 201:
                    ccharId = 42;
                    ccharName = "Число свиней";
                    break;
                case 202:
                    ccharId = 41;
                    ccharName = "Число овец";
                    break;
                case 203: 
                    ccharId = 44;
                    ccharName = "Число лошадей";
                    break;
                case 204:
                    ccharId = 45;
                    ccharName = "Число коз";
                    break;
                case 205:
                    ccharId = 43;
                    ccharName = "Число птиц";
                    break;
                case 212:
                case 213:
                case 214:
                    ccharId = 24;
                    ccharName = "Объем бассейна";
                    break;
                default:
                    return null;
            }
            decimal ccharValue = fraction == 0 ? record.Значение : record.Значение / fraction;
            return new CcharChangeType(ccharId, ccharValue, ccharName);
        }
    }
}
