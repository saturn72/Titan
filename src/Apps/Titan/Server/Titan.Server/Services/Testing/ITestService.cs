using System.Threading.Tasks;
using Titan.Common.Domain.Testing;

namespace Titan.Server.Services.Testing
{
    public interface ITestService
    {
        Task<TestDomainModel> GetTestByIdAsync(long testId);
    }
}