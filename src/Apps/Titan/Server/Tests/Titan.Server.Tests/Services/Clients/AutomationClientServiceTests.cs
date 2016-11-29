using System;
using Moq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Clients;
using Titan.Server.Services.Clients;

namespace Titan.Server.Tests.Services.Clients
{
    public class AutomationClientServiceTests
    {
        [Test]
        public void AutomationClientService_GetClientById_throwsOnIllegalClientId()
        {
            var srv = new AutomationClientService(null);
            //on default id
            typeof(InvalidOperationException).ShouldBeThrownBy(() => srv.GetClientByIdAsync(0));
            typeof(InvalidOperationException).ShouldBeThrownBy(() => srv.GetClientByIdAsync(-123));
        }


        [Test]
        public void AutomationClientService_GetClientById_ReturnsNull_OnNotExistsClient()
        {
            var repo = new Mock<IAutomationClientRepository>();
            repo.Setup(r => r.GetAutoClientById(It.IsAny<long>()))
                .Returns((AutomationClientDomainModel) null);
            var srv = new AutomationClientService(repo.Object);
            srv.GetClientByIdAsync(123).Result.ShouldBeNull();
        }

        [Test]
        public void AutomationClientService_GetClientById_ReturnsAutoClient()
        {
            var repo = new Mock<IAutomationClientRepository>();
            repo.Setup(r => r.GetAutoClientById(It.IsAny<long>()))
                .Returns(new AutomationClientDomainModel());

            var srv = new AutomationClientService(repo.Object);
            srv.GetClientByIdAsync(123).Result.ShouldNotBeNull();
        }
    }
}