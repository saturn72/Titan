using System;
using System.Collections.Generic;
using Shouldly;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;
using NUnit.Framework;
using Titan.Framework.Exceptions;

namespace Titan.Framework.SystemTests.Throws3
{
    public class TestStepMethodHasNoInterfaceImplemenation
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualWithoutInterfaceImplementation()
        {
            var ex = Should.Throw<TypeInitializationException>(
                () => new SimpleTestStepLogic());
            var innerException = ex.InnerException;
            innerException.ShouldBeOfType<AutomationException>();
            innerException.Message.ShouldNotContain("Failed to find test logic component");
        }
    }

    public class SimpleTestStepLogic : TestBase, ISimpleTestStepLogic
    {
        public ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i, decimal d)
        {
            throw new NotImplementedException();
        }
        public ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i)
        {
            throw new NotImplementedException();
        }
    }

    public class SimpleTestLogic
    {
        public virtual IEnumerable<ExecutionResult> ShouldThrowNonVirtualException()
        {
            throw new NotImplementedException();
        }
    }


    public interface ISimpleTestStepLogic
    {
        ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i);
    }
}