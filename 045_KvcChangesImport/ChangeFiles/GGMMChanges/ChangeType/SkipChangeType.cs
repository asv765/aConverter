namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class SkipChangeType : IGGMMChangeType
    {
        public void Convert(GGMMChangeRecord record)
        {
           return; 
        }
    }

    public class SkipChangeTypeFactory : IGGMMChangeTypeFactory
    {
        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 3)
            {
                if ((record.Графа < 13) ||
                    (record.Графа > 13 && record.Графа < 19) ||
                    (record.Графа == 28) ||
                    (record.Графа == 31) ||
                    (record.Графа == 41 || record.Графа == 42) ||
                    (record.Графа == 57) ||
                    (record.Графа == 61) ||
                    (record.Графа == 84))
                    return new SkipChangeType();
                else
                    return null;
            }
            else if (record.ТипНачисления == 4)
            {
                if (record.Графа == 44 || record.Графа == 46 || record.Графа == 49)
                    return new SkipChangeType();
                else
                    return null;
            }
            else if (record.ТипНачисления == 11 || record.ТипНачисления == 12 || record.ТипНачисления == 13)
                return new SkipChangeType();
            else
                return null;
        }
    }
}
