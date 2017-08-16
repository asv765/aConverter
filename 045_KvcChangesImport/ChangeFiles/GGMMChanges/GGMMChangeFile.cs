using System;
using System.IO;
using System.Text;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges
{
    public class GGMMChangeFile : ChangeFileInfo, IChangeFile
    {
        private string _fileName;

        public GGMMChangeFile(string fileName) : base(fileName)
        {
            _fileName = fileName;
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                var fileInfo = new ChangeFileInfo(_fileName);
                int totalCount = (int)(reader.BaseStream.Length / 24);
                StepCounter.StepStart(totalCount + 1);
                int j = 1;
                reader.ReadBytes(24);
                while (j < totalCount)
                {
                    j++;
                    var change = new GGMMChangeRecord(reader.ReadBytes(24), fileInfo);
                    convertAction(change);
                    StepCounter.Iterate();
                }
            }
            StepCounter.StepFinish();
        }
    }
}
