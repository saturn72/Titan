﻿#region

using System;
using System.Collections.Generic;
using Saturn72.Core.Domain.Logging;
using Saturn72.Core.Domain.Users;

#endregion

namespace Saturn72.Core.Logging
{
    /// <summary>
    ///     Represents system logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Determines whether a logRecord level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        ///     Deletes a logRecord item
        /// </summary>
        /// <param name="logRecord">Log item</param>
        void DeleteLog(LogRecord logRecord);

        /// <summary>
        ///     Clears a logRecord
        /// </summary>
        void ClearLog();

        /// <summary>
        ///     Gets all logRecord items
        /// </summary>
        /// <returns>LogRecord{} <see cref="LogRecord{object}" /></returns>
        IEnumerable<LogRecord> GetAllLogs();

        /// <summary>
        ///     Gets a logRecord item
        /// </summary>
        /// <param name="logrecordId">Log item identifier</param>
        /// <returns>Log item</returns>
        LogRecord GetLogRecordById(object logrecordId);

        /// <summary>
        ///     Get logRecord items by identifiers
        /// </summary>
        /// <param name="logRecordIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        IEnumerable<LogRecord> GetLogRecordsByIds(object[] logRecordIds);

        /// <summary>
        ///     Inserts a logRecord item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="contextId">Operation context id</param>
        /// <returns>A logRecord item</returns>
        LogRecord InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "",
            Guid contextId = default(Guid));
    }
}