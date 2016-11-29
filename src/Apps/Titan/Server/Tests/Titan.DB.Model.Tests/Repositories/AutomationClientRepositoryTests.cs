using System.Linq;
using Moq;
using NUnit.Framework;
using Saturn72.Common.Data.Repositories;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Domain.Clients;
using Titan.DB.Model.Repositories;

namespace Titan.DB.Model.Tests.Repositories
{
    public class AutomationClientRepositoryTests
    {
        [Test]
        public void AutomationClientRepository_GetById_ReturnsNull()
        {
            var uow = new Mock<IUnitOfWork<AutomationClientDomainModel, long>>();
            uow.Setup(u => u.GetById(It.IsAny<long>()))
                .Returns((AutomationClientDomainModel) null);
            //on not exists id
            var repo = new AutomationClientRepository(uow.Object);
            repo.GetAutoClientById(123).ShouldBeNull();
        }

        [Test]
        public void AutomationClientRepository_GetById_ReturnsEntitybyId()
        {
            var entities = new[]
            {
                new AutomationClientDomainModel {Id = 1},
                new AutomationClientDomainModel {Id = 2},
                new AutomationClientDomainModel {Id = 3},
                new AutomationClientDomainModel {Id = 4}
            };

            var uow = new Mock<IUnitOfWork<AutomationClientDomainModel, long>>();
            uow.Setup(u => u.GetById(It.IsAny<long>()))
                .Returns<long>(id => entities.FirstOrDefault(e => e.Id == id));

            var repo = new AutomationClientRepository(uow.Object);
            const long eId = 3;

            repo.GetAutoClientById(eId).ShouldBeTheSameAs(entities[eId - 1]);
        }
    }
}