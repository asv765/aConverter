using System;
using aConverterClassLibrary.RecordsDataAccessORM;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class LcharChangeType : IGGMMChangeType
    {
        public int LcharId;
        public string LcharName;
        public int LcharValue;

        public LcharChangeType(int lcharId, int lcharValue, string lcharName = null)
        {
            LcharId = lcharId;
            LcharValue = lcharValue;
            LcharName = lcharName;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.LcharsList.Add(new CNV_LCHAR
            {
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                LCHARCD = LcharId,
                LCHARNAME = LcharName,
                VALUE_ = LcharValue,
                DATE_ = record.FileDate
            });
        }
    }

    public class LcharChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления != 3) return null;
            int lcharId;
            string lcharName;
            int lcharValue;
            switch (record.Графа)
            {
                case 33:
                    lcharId = 30;
                    lcharName = "снятие коэффиц.03";
                    lcharValue = record.Значение > 0 ? 0 : 1;
                    break;
                case 35:
                    lcharId = 28;
                    lcharName = "снятие коэффиц.05";
                    lcharValue = record.Значение > 0 ? 0 : 1;
                    break;
                case 37:
                    lcharId = 29;
                    lcharName = "снятие коэффиц.07";
                    lcharValue = record.Значение > 0 ? 0 : 1;
                    break;
                case 51:
                    lcharId = 1;
                    lcharName = "хар. кв.";
                    switch (record.Значение)
                    {
                        case 0:
                            lcharValue = 0;
                            break;
                        case 6:
                        case 9:
                            lcharValue = 1;
                            break;
                        case 8:
                        case 10:
                            lcharValue = 2;
                            break;
                        case 11:
                        case 13:
                            lcharValue = 3;
                            break;
                        case 12:
                        case 14:
                            lcharValue = 4;
                            break;
                        case 15:
                        case 16:
                            lcharValue = 5;
                            break;
                        case 17:
                        case 18:
                            lcharValue = 6;
                            break;
                        case 19:
                            lcharValue = 7;
                            break;
                        case 20:
                            lcharValue = 8;
                            break;
                        default:
                            throw new Exception($"Неизвестная значение характеристики квартиры {record.Значение}\r\nНачисление = {record.ТипНачисления}\r\nГрафа = {record.Графа}\r\nЛС = {record.LsKvc.Ls}");
                    }
                    break;
                case 66:
                    lcharId = 53;
                    lcharName = "Тип жилья";
                    lcharValue = record.Значение;
                    break;
                case 81:
                    lcharId = 52;
                    lcharName = "Вид жил.пом.";
                    lcharValue = record.Значение == 0 ? 6 : (record.Значение - 1);
                    break;
                case 82:
                    lcharId = 37;
                    lcharName = "Форма собств.ЖФ";
                    lcharValue = record.Значение == 0 ? 6 : record.Значение - 1;
                    break;
                case 99:
                    lcharId = 99;
                    lcharName = "Закрытый л/с";
                    lcharValue = record.Значение > 0 ? 1 : 0;
                    break;
                case 206:
                    lcharId = 48;
                    lcharName = "душ без канализации";
                    lcharValue = record.Значение > 0 ? 1 : 0;
                    break;
                case 207:
                    lcharId = 48;
                    lcharName = "душ с канализацией";
                    lcharValue = record.Значение > 0 ? 2 : 0;
                    break;
                case 208: 
                    lcharId = 46;
                    lcharName = "баня без душа и кана";
                    lcharValue = record.Значение > 0 ? 1 : 0;
                    break;
                case 209:
                    lcharId = 46;
                    lcharName = "баня без душа с кана";
                    lcharValue = record.Значение > 0 ? 2 : 0;
                    break;
                case 210:
                    lcharId = 46;
                    lcharName = "баня с душем без кан";
                    lcharValue = record.Значение > 0 ? 3 : 0;
                    break;
                case 211:
                    lcharId = 46;
                    lcharName = "баня с душем с канал";
                    lcharValue = record.Значение > 0 ? 4 : 0;
                    break;
                default:
                    return null;
            }
            return new LcharChangeType(lcharId, lcharValue, lcharName);
        }
    }
}
