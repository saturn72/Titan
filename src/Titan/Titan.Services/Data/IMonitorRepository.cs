using Titan.Common.Domain.Monitor;

namespace Titan.Services.Data
{
    public interface IMonitorRepository
    {
        void Add(MonitorResult monitorResult);
    }
}
