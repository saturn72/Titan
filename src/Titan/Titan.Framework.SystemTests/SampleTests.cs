using System;
using NUnit.Framework;
using Titan.Framework.Testing;

namespace Titan.Framework.SystemTests
{
    public class SampleTests : TestBase
    {
        [Test]
        public void SimpleTest()
        {
            //create event listener and see that all events were raised
            throw new NotImplementedException("The test was not implemented");
        }

        [TearDown]
        public void TearDown()
        {
            //Add teardown method that checks that all post test steps were raised    
            throw new NotImplementedException();
        }
    }
}