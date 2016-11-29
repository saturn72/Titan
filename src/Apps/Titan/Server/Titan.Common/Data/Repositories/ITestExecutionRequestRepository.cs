
using Titan.Common.Domain.Execution;

namespace Titan.Common.Data.Repositories
{
    public interface ITestExecutionRequestRepository
    {
        TestExecutionRequestDomainModel GetTestExecutionRequestById(long testExecutionRequestId);
        TestExecutionRequestDomainModel CreateTestExecutionRequest(TestExecutionRequestDomainModel testExecutionRequest);
    }
}
