using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Calculator.Test.Framework;
using NUnit.Framework;

namespace BasicTests
{
    [SetUpFixture]
    public class OnTestStart
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestUtil.KillAllWebDriversInstances();
            
            TestUtil.StartCalcularotServer();
        }
    }
}