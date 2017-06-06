using System;
using System.Linq;
using Calculator.Test.Framework.SystemBlockOptions;
using Calculator.Test.Framework.Testing;
using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Shouldly;
using Titan.Framework.Testing;
using Titan.Services.Monitor;

namespace BasicTests
{
    [Category("calculator_sut_tests")]
    public class CalculatorAddTests : TestBase
    {
        [Test]
        public void AddFromGuiAndValidateViaRestMonitor()
        {
            var calcApi = AppEngine.Current.Resolve<CalculatorTestLogic>();
            var ms = AppEngine.Current.Resolve<IMonitorResultService>();
            var msRecordsBefore = ms.GetAllMonitorResult();
            calcApi.Add(new AddOptions {X = 5, Y = 11});

            var msRecordsAfter = ms.GetAllMonitorResult();
            msRecordsAfter.Count().ShouldBe(msRecordsBefore.Count() + 2);
        }
    }
}