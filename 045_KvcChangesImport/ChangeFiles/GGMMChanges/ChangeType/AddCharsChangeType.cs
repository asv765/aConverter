using aConverterClassLibrary.RecordsDataAccessORM;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class AddCharsChangeType : IGGMMChangeType
    {
        public int AddCharCd;
        public string AddCharName;
        public string AddCharValue;

        public AddCharsChangeType(int addCharCd, string addCharValue, string addCharName = null)
        {
            AddCharCd = addCharCd;
            AddCharValue = addCharValue;
            AddCharName = addCharName;
        }

        public void Convert(GGMMChangeRecord record)
        {
            DigitChangesImport.AddCharsList.Add(new CNV_AADDCHAR
            {
                ADDCHARCD = AddCharCd,
                LSHET = Consts.LsDic[record.LsKvc.Ls],
                VALUE = AddCharValue
            });
        }
    }

    public class AddCharsChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 3)
            {
                int addCharCd;
                string addCharValue;
                string addCharName;
                switch (record.Графа)
                {
                    case 83:
                        addCharCd = 755;
                        addCharName = "Цель использования";
                        addCharValue = record.Значение.ToString();
                        break;
                    default:
                        return null;
                }
                return new AddCharsChangeType(addCharCd, addCharValue, addCharName);
            }
            else 
                return null;
        }
    }
}
