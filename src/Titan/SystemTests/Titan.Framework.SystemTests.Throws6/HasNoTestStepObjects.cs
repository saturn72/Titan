using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Titan.Framework.Exceptions;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests.Throws6
{
    public class SampleTests
    {
        [Test]
        public void ShouldThrowExceptionOnNonVirtualMethod()
        {
            var ex = Should.Throw<TypeInitializationException>(() => new SimpleTestStepLogic());
            var innerException = ex.InnerException;
            innerException.ShouldBeOfType<AutomationException>();
            innerException.Message.ShouldContain("Failed to find test logic component");
        }
    }

    public class SimpleTestStepLogic : TestBase
    {
        public virtual IEnumerable<ExecutionResult> ShouldThrowNonVirtualException(string s, object o, int i, decimal d)
        {
            throw new NotImplementedException();
        }
    }
}