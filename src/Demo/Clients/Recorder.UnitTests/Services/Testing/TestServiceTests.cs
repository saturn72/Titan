
using System;
using NUnit.Framework;
using Recorder.Services.Testing;
using Saturn72.UnitTesting.Framework;

namespace Recorder.UnitTests.Services.Testing
{
public     class TestServiceTests
    {
        [Test]
        public void TestService_CreateTest_throws()
        {
            var ts = new TestService();

            //on null test model
            typeof(NullReferenceException).ShouldBeThrownBy(() => ts.Create(null));
        }

    }
}
