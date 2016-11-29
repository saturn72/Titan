using System;
using Moq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Testing;
using Titan.Server.Services.Testing;

namespace Titan.Server.Tests.Services.Testing
{
    public class TestServiceTests
    {
        [Test]
        public void TestService_GetTestById_ThrowsOnZreoOrNegativeId()
        {
            var srv = new TestService(null);
            //on default id
            typeof(InvalidOperationException).ShouldBeThrownBy(() => srv.GetTestByIdAsync(0));
            typeof(InvalidOperationException).ShouldBeThrownBy(() => srv.GetTestByIdAsync(-123));
        }

        [Test]
        public void TestClientService_GetTestById_ReturnsNull_OnNotExistsClient()
        {
            var repo = new Mock<ITestRepository>();
            repo.Setup(r => r.GetTestById(It.IsAny<long>()))
                .Returns((TestDomainModel) null);
            var srv = new TestService(repo.Object);
            srv.GetTestByIdAsync(123).Result.ShouldBeNull();
        }

        [Test]
        public void TestService_GetTestById_ReturnsAutoClient()
        {
            var repo = new Mock<ITestRepository>();
            repo.Setup(r => r.GetTestById(It.IsAny<long>()))
                .Returns(new TestDomainModel());

            var srv = new TestService(repo.Object);
            srv.GetTestByIdAsync(123).Result.ShouldNotBeNull();
        }
    }
}