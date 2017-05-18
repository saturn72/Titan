using System;
using System.Linq;
using Castle.Core.Internal;
using NUnit.Framework;
using Titan.Framework.Testing;
using Shouldly;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestContextTests
    {
        [Test]
        public void TestContext_CreateInstance()
        {
            TestSuiteContext.Create();
            const string name = "name";
            var tsc = TestSuiteContext.Instance;
            var tags = new []{"a", "b","c"};
            const string parameters = "parameters";
            var tc = new Titan.Framework.Testing.TestContext(name,parameters, tags,tsc);

            tc.Name.ShouldBe(name);
            tc.Parameters.ShouldBe(parameters);
            tc.Tags.ShouldBe(tags);
            tc.CreatedOnUtc.ShouldNotBe(default(DateTime));
            tc.TestSuiteContext.ShouldBe(tsc);
            tc.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            tsc.TestContexts.Last().ShouldBe(tc);
        }
    }
}
