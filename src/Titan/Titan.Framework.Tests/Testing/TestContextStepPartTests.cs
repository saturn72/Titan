using System;
using Castle.Core.Internal;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Testing;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestContextStepPartTests
    {

        [Test]
        public void TestContextStep_Create()
        {
            if (TestSuiteContext.Instance == null)
                TestSuiteContext.Create();
            var parameters = new object[] { "this is params", "wer"};
            var mi = this.GetType().GetMethod("TestContextStep_Create");

            var tc = new Framework.Testing.TestContext(mi, parameters, null, TestSuiteContext.Instance);
            var tcs = new TestContextStep(tc, mi, parameters);
            var part = new TestContextStepPart(tcs, mi, parameters);

            part.Name.ShouldBe(mi.Name);
            part.TestContextStep.ShouldBe(tcs);
            part.Parameters.ShouldBe(parameters);
            part.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            part.CreatedOnUtc.ShouldNotBe(default(DateTime));
        }
    }
}
