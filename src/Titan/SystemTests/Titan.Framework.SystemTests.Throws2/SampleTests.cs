using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws2
{
    public class SampleTests
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualWithoutInterfaceImplementation()
        {
            Should.Throw<TypeInitializationException>(
                () => new SimpleTestLogic());
        }
    }

    public class SimpleTestLogic : TestBase, ISimpleTestLogic
    {
        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i, decimal d)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i)
        {
            throw new NotImplementedException();
        }
    }

    public interface ISimpleTestLogic
    {
        IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i);
    }
}