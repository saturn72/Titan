using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws
{
    public class TestMethodHasNoVirtualMethods
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualMethod()
        {
            var ex = Should.Throw<TypeInitializationException>(()=> new SimpleTestLogic());
            var innerException = ex.InnerException;
            innerException.ShouldBeOfType<AutomationException>();
            innerException.Message.ShouldNotContain("Failed to find test logic component");
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