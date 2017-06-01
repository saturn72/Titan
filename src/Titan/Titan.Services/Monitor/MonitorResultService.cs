using Saturn72.Core.Services.Events;
using Saturn72.Extensions;
using Titan.Common.Domain.Monitor;
using Titan.Services.Data;

namespace Titan.Services.Monitor
{
    public class MonitorResultService : IMonitorResultService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IMonitorRepository _monitorRepository;

        public MonitorResultService(IMonitorRepository monitorRepository, IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _monitorRepository = monitorRepository;
        }

        public void AddMonitorResult(MonitorResult monitorResult)
        {
            Guard.NotNull(monitorResult);

            _monitorRepository.Add(monitorResult);
            _eventPublisher.DomainModelCreated(monitorResult);
        }
    }
}