using System.Collections.Generic;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.IdomChange
{
    public class IdomChangeRecord : IdomRecord, IChangeRecord
    {
        public readonly string FileName;

        public IdomChangeRecord(List<byte[]> bytes, IdomFileInfo fileInfo) : base(bytes, fileInfo)
        {
            FileName = fileInfo.FileName;
        }
    }
}
