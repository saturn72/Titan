using System;
using Castle.Core.Internal;
using NUnit.Framework;
using Titan.Framework.Testing;
using Shouldly;
using TestContext = Titan.Framework.Testing.TestContext;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestContextStepTests
    {
        [Test]
        public void TestContextStep_Create()
        {
            if (TestSuiteContext.Instance == null)
                TestSuiteContext.Create();
            var parameters = new[] { "invocationParams"};
            var mi = GetType().GetMethod("TestContextStep_Create");

            var tc = new TestContext(mi, parameters, null, TestSuiteContext.Instance);

            var tcs = new TestContextStep(tc,mi,  parameters);

            tcs.Name.ShouldBe(mi.Name);
            tcs.Parameters.ShouldBe(parameters);
            tcs.TestContext.ShouldBe(tc);
            tcs.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            tcs.CreatedOnUtc.ShouldNotBe(default(DateTime));
        }
    }
}