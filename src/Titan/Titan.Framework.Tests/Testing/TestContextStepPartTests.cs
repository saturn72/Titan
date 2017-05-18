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
            const string parameters = "this is params";
            var tc = new Framework.Testing.TestContext("tc_name",parameters, null, TestSuiteContext.Instance);
            const string prms = "parameters";
            var tcs = new TestContextStep("tcs_name", tc, prms);
            const string testCtxStepPartName = "tcsp_name";

            var part = new TestContextStepPart(testCtxStepPartName, tcs, parameters);

            part.Name.ShouldBe(testCtxStepPartName);
            part.TestContextStep.ShouldBe(tcs);
            part.Parameters.ShouldBe(parameters);
            part.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            part.CreatedOnUtc.ShouldNotBe(default(DateTime));
        }
    }
}
