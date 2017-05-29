using System;
using Calculator.Test.Framework.SystemBlockOptions;
using Calculator.Test.Framework.Testing;
using NUnit.Framework;
using Saturn72.Core.Infrastructure;
using Titan.Framework.Testing;

namespace BasicTests
{
    [Category("calculator_sut_tests")]
    public class CalculatorAddTests : TestBase
    {
        [Test]
        public void AddFromGuiAndValidateViaRestMonitor()
        {
            var cmd = AppEngine.Current.Resolve<CalculatorTestLogic>();
            cmd.Add(new AddOptions {X = 5, Y = 11});
            throw new NotImplementedException("dddddd");
        }
    }
}