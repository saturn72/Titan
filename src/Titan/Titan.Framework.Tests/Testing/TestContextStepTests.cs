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
            const string parameters = "invocationParams";

            var tc = new TestContext("name", parameters, null, TestSuiteContext.Instance);

            const string testStepName = "testStepName";
            var tcs = new TestContextStep(testStepName, tc, parameters);

            tcs.Name.ShouldBe(testStepName);
            tcs.Parameters.ShouldBe(parameters);
            tcs.TestContext.ShouldBe(tc);
            tcs.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            tcs.CreatedOnUtc.ShouldNotBe(default(DateTime));
        }
    }
}