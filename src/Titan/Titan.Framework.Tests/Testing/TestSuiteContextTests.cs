using System;
using Castle.Core.Internal;
using NUnit.Framework;
using Titan.Framework.Testing;
using Shouldly;

namespace Titan.Framework.Tests.Testing
{
    [Category("unit_test")]
    public class TestSuiteContextTests
    {
        [Test]
        public void TestSuiteContext_Create()
        {
            if (TestSuiteContext.Instance == null)
                TestSuiteContext.Create();
            var tsc = TestSuiteContext.Instance;
            tsc.CreatedOnUtc.ShouldNotBe(default(DateTime));
            tsc.TestSuiteId.IsNullOrEmpty().ShouldBeFalse();
        }

        [Test]
        public void TestSuiteContext_ThrowsOnMultipleCreate()
        {
            if (TestSuiteContext.Instance == null)
                TestSuiteContext.Create();
            Should.Throw<InvalidOperationException>(()=> TestSuiteContext.Create());
        }

        [Test]
        public void TestSuiteContext_Terminate()
        {
            if (TestSuiteContext.Instance == null)
                TestSuiteContext.Create();

            TestSuiteContext.Instance.Terminate();
            TestSuiteContext.Instance.DisposedOnUtc.ShouldNotBe(default(DateTime));
        }
    }
}
