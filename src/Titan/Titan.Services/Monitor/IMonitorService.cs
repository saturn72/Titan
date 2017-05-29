using Titan.Common.Domain.Monitor;

namespace Titan.Services.Monitor
{
    public interface IMonitorService
    {
        void AddMonitorResult(MonitorResult monitorResult);
    }
}
