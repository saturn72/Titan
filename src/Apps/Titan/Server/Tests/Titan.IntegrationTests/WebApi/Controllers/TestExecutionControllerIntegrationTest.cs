using System;
using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Saturn72.UnitTesting.Framework;
using Titan.WebApi.Controllers;

namespace Titan.IntegrationTests.WebApi.Controllers
{
    public class TestExecutionControllerIntegrationTest
    {
        [Test]
        public void TestExecutionController_Post_ThrwosOnNullModel()
        {
            var ctrl = AppEngine.Current.Resolve<TestExecutionController>();

            typeof(ArgumentException).ShouldBeThrownBy(() =>
            {
                try
                {
                    var r = ctrl.Post(null, null).Result;
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            });
        }
    }
}