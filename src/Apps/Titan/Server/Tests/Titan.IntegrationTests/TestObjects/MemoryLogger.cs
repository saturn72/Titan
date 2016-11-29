using System;
using System.Collections.Generic;
using System.Linq;
using Saturn72.Core.Domain.Logging;
using Saturn72.Core.Logging;

namespace Titan.IntegrationTests.TestObjects
{
    public class MemoryLogger : ILogger
    {
        private ICollection<LogRecord> _logRecords;
        private long _index = 0;
        protected ICollection<LogRecord> Logrecords => _logRecords ?? (_logRecords = new List<LogRecord>());

        public bool IsEnabled(LogLevel level)
        {
            return true;
        }

        public void DeleteLog(LogRecord logRecord)
        {
            _logRecords.Remove(logRecord);
        }

        public void ClearLog()
        {
            _logRecords = new List<LogRecord>();
        }

        public IEnumerable<LogRecord> GetAllLogs()
        {
            return _logRecords;
        }

        public LogRecord GetLogRecordById(object logrecordId)
        {
            return _logRecords.FirstOrDefault(l => l.Id == logrecordId);
        }

        public IEnumerable<LogRecord> GetLogRecordsByIds(object[] logRecordIds)
        {
            return _logRecords.Where(l => logRecordIds.Contains(l.Id));

        }

        public LogRecord InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "",
            Guid contextId = new Guid())
        {
            var lr = new LogRecord
            {
                Id = _index++,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                ContextId = contextId
            };

            _logRecords.Add(lr);
            return lr;
        }
    }
}