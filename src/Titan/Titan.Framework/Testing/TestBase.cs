
using System;
using Saturn72.Core.Services.Events;
using Saturn72.Core.Services.App.Events;
using Titan.Framework.App;
using Titan.Framework.Lifetime;

namespace Titan.Framework.Testing
{
    public abstract class TestBase : IDisposable
    {
        private static readonly TestApp App;
        protected static IEventPublisher EventPublisher;

        static TestBase()
        {
            App = new TestApp();
            App.Start();
            TestLifetimePublisher.CreateTestSuiteContext();
            RegisterExceptionHandler();
        }

        private static void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, args) => { EventPublisher.Publish(new OnApplicationUnexpectedlyStoppedEvent(App)); };
        }

        public void Dispose()
        {
            TestLifetimePublisher.EndTestSuiteContextExecution();
            TestLifetimePublisher.DisposeTestSuiteContext();

            App.Stop();
            GC.SuppressFinalize(this);
        }
    }
}