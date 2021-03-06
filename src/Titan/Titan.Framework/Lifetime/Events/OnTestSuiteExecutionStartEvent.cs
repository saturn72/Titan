﻿using Saturn72.Core.Services.Events;
using Titan.Framework.Testing;

namespace Titan.Framework.Lifetime.Events
{
    public class OnTestSuiteExecutionStartedEvent : EventBase
    {
        public OnTestSuiteExecutionStartedEvent(TestSuiteContext testSuiteContext)
        {
            TestSuiteContext = testSuiteContext;
        }

        public TestSuiteContext TestSuiteContext { get; }
    }
}