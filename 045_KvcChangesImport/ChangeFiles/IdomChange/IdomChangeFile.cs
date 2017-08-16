using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.IdomChange
{
    public class IdomChangeFile : IdomFileInfo, IChangeFile
    {
        private readonly string _fileName;

        public IdomChangeFile(string fileName) : base(fileName)
        {
            _fileName = fileName;
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            StepCounter.StepStart((int)КоличествоЛс);
            if (КоличествоЛс == 0)
            {
                StepCounter.StepFinish();
                return;
            }
            using (var reader = new BinaryReader(File.OpenRead(_fileName), Encoding.GetEncoding(1251)))
            {
                var fileInfo = new IdomFileInfo(_fileName);
                reader.ReadBytes(8);
                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    var octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var idom = new IdomChangeRecord(bytes, fileInfo);
                        convertAction(idom);
                        StepCounter.Iterate();
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastidom = new IdomChangeRecord(bytes, fileInfo);
                convertAction(lastidom);
            }
            StepCounter.StepFinish();
        }
    }
}
