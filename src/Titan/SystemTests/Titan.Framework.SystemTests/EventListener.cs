using Saturn72.Core.Services.App.Events;
using Saturn72.Core.Services.Events;
using Titan.Framework.Lifetime.Events;

namespace Titan.Framework.SystemTests
{
    public class EventListener :
        IEventSubscriber<OnApplicationInitializeStartEvent>,
        IEventSubscriber<OnApplicationInitializeFinishEvent>,
        IEventSubscriber<OnTestSuiteCreatedEvent>,
        IEventSubscriber<OnTestSuiteExecutionStartEvent>,
        IEventSubscriber<OnTestSuiteExecutionEndEvent>,
        IEventSubscriber<OnTestSuiteDisposedEvent>
    {
        public bool OnApplicationInitializeStartEventHandler { get; set; }

        public bool OnApplicationInitializeFinishEventHandler { get; set; }

        public bool OnTestSuiteCreatedEventHandler { get; set; }
        public bool OnTestSuiteExecutionStartEventHandler { get; set; }

        public bool OnTestSuiteExecutionEndEventHandler { get; set; }

        public bool OnTestSuiteDisposedEventHandler { get; set; }

        public void HandleEvent(OnApplicationInitializeFinishEvent eventMessage)
        {
            OnApplicationInitializeFinishEventHandler = true;
        }

        public void HandleEvent(OnApplicationInitializeStartEvent eventMessage)
        {
            OnApplicationInitializeStartEventHandler = true;
        }

        public void HandleEvent(OnTestSuiteCreatedEvent eventMessage)
        {
            OnTestSuiteCreatedEventHandler = true;
        }

        public void HandleEvent(OnTestSuiteDisposedEvent eventMessage)
        {
            OnTestSuiteDisposedEventHandler = true;
        }

        public void HandleEvent(OnTestSuiteExecutionEndEvent eventMessage)
        {
            OnTestSuiteExecutionEndEventHandler = true;
        }

        public void HandleEvent(OnTestSuiteExecutionStartEvent eventMessage)
        {
            OnTestSuiteExecutionStartEventHandler = true;
        }
    }
}