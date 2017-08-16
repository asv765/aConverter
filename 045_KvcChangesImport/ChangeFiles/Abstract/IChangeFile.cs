using System;

namespace _045_KvcChangesImport.ChangeFiles.Abstract
{
    public interface IChangeFile
    {
        void ConvertFile(Action<IChangeRecord> convertAction);
    }
}
