using NUnit.Framework;
using Titan.Framework.Testing;
using Shouldly;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestSuiteContextExtensionsTests
    {
        [Test]
        [Category("non_deterministic")]
        public void TestSuiteContext_CurrentTestContext()
        {
            TestSuiteContext.Create();
            var tsc = TestSuiteContext.Instance;
            const string tcName = "Name";
            var mi = GetType().GetMethod("TestSuiteContext_CurrentTestContext");
            var parameters = new[] {"para,etsr"};
            var tc = new Titan.Framework.Testing.TestContext(mi,parameters, null,  tsc);
            tsc.CurrentTestContext().ShouldBe(tc);
        }
    }
}
