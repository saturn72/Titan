using System.Linq;
using Moq;
using NUnit.Framework;
using Saturn72.Common.Data.Repositories;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Domain.Testing;
using Titan.DB.Model.Repositories;

namespace Titan.DB.Model.Tests.Repositories
{
    public class TestRepositoryTests
    {
        [Test]
        public void TestRepository_GetTestById_returnsNull_onNonExist()
        {
            var uow = new Mock<IUnitOfWork<TestDomainModel, long>>();
            uow.Setup(u => u.GetById(It.IsAny<long>()))
                .Returns((TestDomainModel) null);

            //on not exists id
            var repo = new TestRepository(uow.Object);
            repo.GetTestById(123).ShouldBeNull();
        }

        [Test]
        public void AutomationClientRepository_GetById_ReturnsEntitybyId()
        {
            var entities = new[]
            {
                new TestDomainModel {Id = 1},
                new TestDomainModel {Id = 2},
                new TestDomainModel {Id = 3},
                new TestDomainModel {Id = 4}
            };

            var uow = new Mock<IUnitOfWork<TestDomainModel, long>>();
            uow.Setup(u => u.GetById(It.IsAny<long>()))
                .Returns<long>(id => entities.FirstOrDefault(e => e.Id == id));

            var repo = new TestRepository(uow.Object);
            const long eId = 3;

            repo.GetById(eId).ShouldBeTheSameAs(entities[eId - 1]);
        }
    }
}