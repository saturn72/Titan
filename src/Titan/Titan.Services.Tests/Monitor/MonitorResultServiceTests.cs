using System;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using Saturn72.Core.Services.Events;
using Shouldly;
using Titan.Common.Domain.Monitor;
using Titan.Services.Data;
using Titan.Services.Monitor;

namespace Titan.Services.Tests.Monitor
{
    public class MonitorResultServiceTests
    {
        [Test]
        public void MonitorResultService_AddMonitorResult_Throws()
        {
            var srv = new MonitorResultService(null, null);

            Should.Throw<NullReferenceException>(() => srv.AddMonitorResult(null));
        }


        [Test]
        public void MonitorResultService_AddMonitorResult_Adds()
        {
            var mRepo = new Mock<IMonitorRepository>();
            var ep = new Mock<IEventPublisher>();
            var srv = new MonitorResultService(mRepo.Object, ep.Object);

            var expected = new MonitorResult
            {
                Actual = 3.ToString(),
                Expected = 2.ToString(),
                ComparisonTypeCode = ComparisonType.Equality.Code,
            };

            srv.AddMonitorResult(expected);
            mRepo.Verify(m => m.Add(It.Is<MonitorResult>(actual => ExpectedMonitorResultValues(actual, expected))), Times.Once);
            ep.Verify(
                e => e.Publish(It.Is<CreatedEvent<MonitorResult>>(ce => ExpectedMonitorResultValues(ce.DomainModel, expected))),
                Times.Once);
        }

        private static bool ExpectedMonitorResultValues(MonitorResult actual, MonitorResult expected)
        {
            return actual.Actual == expected.Actual
                   && actual.ActualType == expected.ActualType
                   && actual.Expected == expected.Expected
                   && actual.ExpectedType == expected.ExpectedType;
        }
    }
}
