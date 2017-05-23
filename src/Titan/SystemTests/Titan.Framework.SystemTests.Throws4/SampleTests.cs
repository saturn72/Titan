using System;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws4
{
    public class SampleTests
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualMethod()
        {
            Should.Throw<TypeInitializationException>(() => new SimpleTestLogic());
        }
    }

    public class SimpleTestLogic : TestBase
    {
        public ExecutionResult ShouldThrowNonVirtualException()
        {
            throw new NotImplementedException();
        }
    }
}