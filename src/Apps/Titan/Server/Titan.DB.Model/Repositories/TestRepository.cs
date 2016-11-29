using Saturn72.Common.Data.Repositories;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Testing;
using Titan.DB.Model.Models.Server;

namespace Titan.DB.Model.Repositories
{
    public class TestRepository : RepositoryBase<TestDomainModel, long, Test>, ITestRepository
    {
        public TestRepository(IUnitOfWork<TestDomainModel, long> unitOfWork) : base(unitOfWork)
        {
        }

        public TestDomainModel GetTestById(long testId)
        {
            return GetById(testId);
        }
    }
}