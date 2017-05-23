using System;
using System.Collections.Generic;
using Shouldly;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;
using NUnit.Framework;

namespace Titan.Framework.SystemTests.Throws3
{
    public class SampleTests
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualWithoutInterfaceImplementation()
        {
            Should.Throw<TypeInitializationException>(
                () => new SimpleTestStepLogic());
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

    public interface ISimpleTestStepLogic
    {
        ExecutionResult ShouldThrowNonVirtualException(string s, object o, int i);
    }
}