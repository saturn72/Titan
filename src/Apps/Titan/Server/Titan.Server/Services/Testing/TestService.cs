using System.Threading.Tasks;
using Saturn72.Extensions;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Testing;

namespace Titan.Server.Services.Testing
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public Task<TestDomainModel> GetTestByIdAsync(long testId)
        {
            Guard.GreaterThan(testId, (long) 0, "Test Id must be greater than 0");

            return Task.FromResult(_testRepository.GetTestById(testId));
        }
    }
}