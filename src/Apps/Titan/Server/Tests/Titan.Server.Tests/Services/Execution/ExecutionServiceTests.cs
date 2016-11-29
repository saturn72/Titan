using System;
using Moq;
using NUnit.Framework;
using Saturn72.Core.Domain.Logging;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Events;
using Saturn72.UnitTesting.Framework;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Execution;
using Titan.Server.Services.Execution;

namespace Titan.Server.Tests.Services.Execution
{
    public class ExecutionServiceTests
    {
        [Test]
        public void ExecutionService_CreateTestExecutionRequest_Throws_OnNullTestExeReq()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() =>
            {
                try
                {
                    new ExecutionService(null, null, null).CreateExecutionRequest(null).Wait();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            });
        }

        [Test]
        public void ExecutionService_CreateTestExecutionRequest_Fails_ToCreate()
        {
            var repo = new Mock<ITestExecutionRequestRepository>();
            repo.Setup(r => r.GetTestExecutionRequestById(It.IsAny<long>()))
                .Returns((TestExecutionRequestDomainModel) null);
            var logger = new Mock<ILogger>();
            logger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true);

            var srv = new ExecutionService(repo.Object, logger.Object, null);

            var req = new TestExecutionRequestDomainModel();
            var res = srv.CreateExecutionRequest(req).Result;

            res.ShouldEqual(0);
            //validate writes to log
            logger.Verify(
                l => l.InsertLog(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()),
                Times.Once);
        }

        [Test]
        public void ExecutionService_CreateTestExecutionRequest_Creates()
        {
            var repo = new Mock<ITestExecutionRequestRepository>();
            const long id = 123;
            repo.Setup(r => r.CreateTestExecutionRequest(It.IsAny<TestExecutionRequestDomainModel>()))
                .Returns(new TestExecutionRequestDomainModel {Id = id});
            var logger = new Mock<ILogger>();
            logger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true);

            var eventPublisher = new Mock<IEventPublisher>();

            var srv = new ExecutionService(repo.Object, logger.Object, eventPublisher.Object);

            var req = new TestExecutionRequestDomainModel();
            var res = srv.CreateExecutionRequest(req).Result;

            res.ShouldEqual(id);
            //validate writes to log
            logger.Verify(
                l => l.InsertLog(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()),
                Times.Once);

            eventPublisher.Verify(
                e => e.Publish(It.IsAny<CreatedEvent<TestExecutionRequestDomainModel, long>>()), Times.Once);
        }
    }
}