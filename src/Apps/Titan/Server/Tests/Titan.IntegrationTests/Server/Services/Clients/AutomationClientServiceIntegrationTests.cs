using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Saturn72.UnitTesting.Framework;
using Titan.Server.Services.Clients;

namespace Titan.IntegrationTests.Server.Services.Clients
{
    public class AutomationClientServiceIntegrationTests
    {
        [Test]
        public void AutomationClientService_IT_GetsClientAsync()
        {
            var srv = AppEngine.Current.Resolve<IAutomationClientService>();

            //id not exists
            srv.GetClientByIdAsync(long.MaxValue).Result.ShouldBeNull();


            //exists id
            const long cId = 2;
            srv.GetClientByIdAsync(cId).Result.ShouldNotBeNull();
        }
    }
}