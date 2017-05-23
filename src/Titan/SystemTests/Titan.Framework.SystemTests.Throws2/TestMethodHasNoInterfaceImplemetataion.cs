using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws2
{
    public class TestMethodHasNoInterfaceImplemetataion
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualWithoutInterfaceImplementation()
        {
            var ex = Should.Throw<TypeInitializationException>(
                () => new SimpleTestLogic());

            var innerException = ex.InnerException;
            innerException.ShouldBeOfType<AutomationException>();
            innerException.Message.ShouldNotContain("Failed to find test logic component");
        }
    }

    public class SimpleTestLogic : TestBase, ISimpleTestLogic
    {
        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i)
        {
            return SomePrivateMethod();
        }

        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i, decimal d)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ExecutionResult> SomePrivateMethod()
        {
            //Do Somethisn
            throw new NotImplementedException();

        }
    }

    public interface ISimpleTestLogic
    {
        IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i);
    }
}