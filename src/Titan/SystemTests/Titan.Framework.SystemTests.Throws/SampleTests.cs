using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws
{
    public class SampleTests
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualMethod()
        {
            Should.Throw<TypeInitializationException>(()=> new SimpleTestLogic());
        }
    }

    public class SimpleTestLogic:TestBase
    {
        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException()
        {
            throw new NotImplementedException();
        }
    }
}