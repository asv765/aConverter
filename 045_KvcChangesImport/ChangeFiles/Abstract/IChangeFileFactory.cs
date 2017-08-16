namespace _045_KvcChangesImport.ChangeFiles.Abstract
{
    public interface IChangeFileFactory
    {
        IChangeFile Create(string fileName);
    }
}
