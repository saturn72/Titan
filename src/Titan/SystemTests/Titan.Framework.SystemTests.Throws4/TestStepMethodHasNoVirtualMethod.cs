using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws4
{
    public class TestStepMethodHasNoVirtualMethod
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualMethod()
        {
            var ex = Should.Throw<TypeInitializationException>(() => new SimpleTestStepLogic());
            var innerException = ex.InnerException;
            innerException.ShouldBeOfType<AutomationException>();
            innerException.Message.ShouldNotContain("Failed to find test logic component");
        }
    }

    public class SimpleTestLogic
    {
        public IEnumerable<ExecutionResult> SimpleTest()
        {
            throw new NotImplementedException();
        }
    }
    public class SimpleTestStepLogic : TestBase
    {
        public ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i, decimal d)
        {
            //    return SomePrivateMethod();
            //}

            //private ExecutionResult SomePrivateMethod()
            //{
            throw new NotImplementedException();
        }

        public ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i)
        {
            throw new NotImplementedException();
        }
    }
}