using System.Collections.Generic;
using Titan.Common.Domain.Monitor;

namespace Titan.Services.Monitor
{
    public interface IMonitorResultService
    {
        void AddMonitorResult(MonitorResult monitorResult);
        IEnumerable<MonitorResult> GetAllMonitorResult();
    }
}