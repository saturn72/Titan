using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Saturn72.UnitTesting.Framework;
using Titan.Server.Services.Testing;

namespace Titan.IntegrationTests.Server.Services.Testing
{
    public class TestingServiceIntegrationTests
    {
        [Test]
        public void TestService_GetTestById_GetsTestAsync()
        {
            var srv = AppEngine.Current.Resolve<ITestService>();

            //id not exists
            srv.GetTestByIdAsync(long.MaxValue).Result.ShouldBeNull();

            //exists id
            const long cId = 1;
            srv.GetTestByIdAsync(cId).Result.ShouldNotBeNull();
        }
    }
}