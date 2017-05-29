using System;
using System.Dynamic;
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
        [Category("non_deterministic")]
        public void TestContext_CreateInstance()
        {
            TestSuiteContext.Create();
            var tsc = TestSuiteContext.Instance;
            var tags = new []{"a", "b","c"};
            var parameters = new[] {"parameters"};
            var mi = GetType().GetMethod("TestContext_CreateInstance");
            var tc = new Titan.Framework.Testing.TestContext(mi,parameters, tags,tsc);

            tc.Name.ShouldBe(mi.Name);
            tc.Parameters.ShouldBe(parameters);
            tc.Tags.ShouldBe(tags);
            tc.CreatedOnUtc.ShouldNotBe(default(DateTime));
            tc.TestSuiteContext.ShouldBe(tsc);
            tc.ExecutionId.IsNullOrEmpty().ShouldBeFalse();
            tsc.TestContexts.Last().ShouldBe(tc);
        }
    }
}
