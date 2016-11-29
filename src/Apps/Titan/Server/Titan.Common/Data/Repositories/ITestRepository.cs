using Titan.Common.Domain.Testing;

namespace Titan.Common.Data.Repositories
{
    public interface ITestRepository
    {
        TestDomainModel GetTestById(long testId);
    }
}