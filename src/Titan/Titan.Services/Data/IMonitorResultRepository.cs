using System.Collections.Generic;
using Titan.Common.Domain.Monitor;

namespace Titan.Services.Data
{
    public interface IMonitorResultRepository
    {
        void Add(MonitorResult monitorResult);

        IEnumerable<MonitorResult> Collection { get; }
    }
}
