using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.IdomChange
{
    public class IdomChangeFactory : IChangeFileFactory
    {
        public IChangeFile Create(string fileName)
        {
            return new IdomChangeFile(fileName);
        }
    }
}
