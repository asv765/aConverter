using System;
using System.Collections.Generic;
using System.IO;
using RsnReader;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public class CcChangeFile : CcFileInfo, IChangeFile
    {
        private readonly string _fileName;

        public CcChangeFile(string fileName) : base(fileName)
        {
            _fileName = fileName;
        }

        public void ConvertFile(Action<IChangeRecord> convertAction)
        {
            StepCounter.StepStart((int) КоличествоЛс);
            if (КоличествоЛс == 0)
            {
                StepCounter.StepFinish();
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_fileName), Encoding))
            {
                reader.ReadBytes(8); // пропуск первой служебной 001
                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    var octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var abonent = new CcChangeRecord(bytes, this);
                        convertAction(abonent);
                        StepCounter.Iterate();
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastabonent = new CcChangeRecord(bytes, this);
                convertAction(lastabonent);
            }
            StepCounter.StepFinish();
        }
    }
}
