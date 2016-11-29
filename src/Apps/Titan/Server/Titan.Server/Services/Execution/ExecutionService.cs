using System;
using System.Threading.Tasks;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Events;
using Saturn72.Extensions;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Execution;

namespace Titan.Server.Services.Execution
{
    public class ExecutionService : IExecutionService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger _logger;
        private readonly ITestExecutionRequestRepository _testExecutionRequestRepository;

        public ExecutionService(ITestExecutionRequestRepository testExecutionRequestRepository, ILogger logger,
            IEventPublisher eventPublisher)
        {
            _testExecutionRequestRepository = testExecutionRequestRepository;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<long> CreateExecutionRequest(TestExecutionRequestDomainModel testExecutionRequest)
        {
            Guard.NotNull(testExecutionRequest, () => { throw new ArgumentException(nameof(testExecutionRequest)); });

            var ter = await Task.Run(() =>
                    _testExecutionRequestRepository.CreateTestExecutionRequest(testExecutionRequest));
            if (ter.IsNull())
            {
                _logger.Error("Failed to create test execution request.");
                return 0;
            }

            _logger.Information("New Test Execution Request was created. Request Id: " + ter.Id);
            _eventPublisher.DomainModelCreated(ter);
            return ter.Id;
        }
    }
}