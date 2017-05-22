using System;
using System.Collections.Generic;
using NUnit.Framework;
using Titan.Framework.Runtime;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests
{
    public class SampleTests
    {
        [Test]
        public void SimpleTest()
        {
            throw new NotImplementedException();
            var tl = new SimpleTestLogic();
            tl.SimpleFlow();
        }

        [TearDown]
        public void TearDown()
        {
            //Add teardown method that checks that all post test steps were raised    
            throw new NotImplementedException();
        }
    }

    public class SimpleTestLogic : TestBase
    {
        public virtual IEnumerable<ExecutionResult> SimpleFlow()
        {
            //create event listener and see that all events were raised
            throw new NotImplementedException("The test was not implemented");
        }

        public IEnumerable<ExecutionResult> ShouldThrowNonVirtualException()
        {
            throw new NotImplementedException();
        }
    }
}