using NUnit.Framework;
using Titan.Framework.Testing;
using Shouldly;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestSuiteContextExtensionsTests
    {
        [Test]
        public void TestSuiteContext_CurrentTestContext()
        {
            TestSuiteContext.Create();
            var tsc = TestSuiteContext.Instance;
            const string tcName = "Name";
            const string parameters = "para,etsr";
            var tc = new Titan.Framework.Testing.TestContext(tcName,parameters, null,  tsc);
            tsc.CurrentTestContext().ShouldBe(tc);
        }
    }
}
