using System.Threading.Tasks;
using Titan.Common.Domain.Execution;

namespace Titan.Server.Services.Execution
{
    public interface IExecutionService
    {
        Task<long> CreateExecutionRequest(TestExecutionRequestDomainModel testExecutionRequest);
    }
}