using Saturn72.Core.Services.Events;
using Titan.Common.Domain.Monitor;
using Titan.Services.Data;

namespace Titan.Services.Monitor
{
    public class MonitorService : IMonitorService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IMonitorRepository _monitorRepository;

        public MonitorService(IMonitorRepository monitorRepository, IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _monitorRepository = monitorRepository;
        }

        public void AddMonitorResult(MonitorResult monitorResult)
        {
            _monitorRepository.Add(monitorResult);
            _eventPublisher.DomainModelCreated(monitorResult);
        }
    }
}