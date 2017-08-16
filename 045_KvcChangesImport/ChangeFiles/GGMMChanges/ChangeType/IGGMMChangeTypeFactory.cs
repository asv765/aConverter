namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public interface IGGMMChangeTypeFactory
    {
        IGGMMChangeType Create(GGMMChangeRecord record);
    }
}
