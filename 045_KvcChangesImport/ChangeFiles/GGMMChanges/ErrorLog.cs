using System.Collections.Generic;
using System.IO;
using System.Linq;
using _045_KvcChangesImport.ChangeFiles.CcChange;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges
{
    public class ErrorLog
    {
        private List<LogMessage> _log;

        public ErrorLog()
        {
            _log = new List<LogMessage>();
        }

        public void Add(GGMMChangeRecord record, string message)
        {
            if (!_log.Any(l => l.OwnerId == record.ХозяинЛс && l.Message == message))
                _log.Add(new LogMessage {OwnerId = record.ХозяинЛс, Message = message});
        }

        public void Add(CcChangeRecord record, string message)
        {
            if (!_log.Any(l => l.OwnerId == record.ХозяинЛс && l.Message == message))
                _log.Add(new LogMessage { OwnerId = record.ХозяинЛс, Message = message });
        }

        public void Add(string message)
        {
            _log.Add(new LogMessage {Message = message});
        }

        public void WriteLogToFile(string fileName)
        {
            File.WriteAllLines(fileName, _log.Select(l => l.GetFullMessage()));
        }

        private class LogMessage
        {
            public int? OwnerId;
            public string Message;

            public string GetFullMessage()
            {
                return OwnerId != null ? $"Хозяин ЛС: {OwnerId}\t{Message}" : Message;
            }
        }
    }
}
