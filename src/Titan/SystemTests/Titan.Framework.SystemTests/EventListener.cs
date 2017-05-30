using Saturn72.Core.Services.App.Events;
using Saturn72.Core.Services.Events;
using Titan.Framework.Lifetime.Events;

namespace Titan.Framework.SystemTests
{
    public class EventListener :
        //APP events
        IEventSubscriber<OnApplicationInitializeStartEvent>,
        IEventSubscriber<OnApplicationInitializeFinishEvent>,
        IEventSubscriber<OnApplicationStopStartEvent>,
        IEventSubscriber<OnApplicationStopFinishEvent>,
        //Test suite events
        IEventSubscriber<OnTestSuiteCreatedEvent>,
        IEventSubscriber<OnBeforeTestSuiteExecutionStartEvent>,
        IEventSubscriber<OnTestSuiteExecutionStartedEvent>,
        IEventSubscriber<OnTestSuiteExecutionEndEvent>,
        IEventSubscriber<OnTestSuiteDisposedEvent>,
        //Test context events
        IEventSubscriber<OnTestContextCreatedEvent>,
        IEventSubscriber<OnTestContextExecutionStartEvent>,
        IEventSubscriber<OnBeforeTestContextExecutionStartEvent>,
        IEventSubscriber<OnTestContextExecutionEndEvent>,
        IEventSubscriber<OnTestContextDisposedEvent>,

        //test context step
        IEventSubscriber<OnTestContextStepCreatedEvent>,
        IEventSubscriber<OnBeforeTestContextStepExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepExecutionEndEvent>,
        IEventSubscriber<OnTestContextStepDisposedEvent>,

        //test context step part
        IEventSubscriber<OnTestContextStepPartCreatedEvent>,
        IEventSubscriber<OnBeforeTestContextStepPartExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepPartExecutionStartEvent>,
        IEventSubscriber<OnTestContextStepPartExecutionEndEvent>,
        IEventSubscriber<OnTestContextStepPartDisposedEvent>

    {
        public static bool OnApplicationInitializeStartEventHandler { get; set; }
        public static bool OnApplicationInitializeFinishEventHandler { get; set; }
        public static bool OnTestSuiteCreatedEventHandler { get; set; }
        public static bool OnTestSuiteExecutionStartEventHandler { get; set; }
        public static bool OnTestSuiteExecutionEndEventHandler { get; set; }
        public static bool OnTestSuiteDisposedEventHandler { get; set; }
        public static bool OnTestContextCreatedEventHandler { get; set; }
        public static bool OnTestContextExecutionStartEventHandler { get; set; }
        public static bool OnTestContextExecutionEndEventHandler { get; set; }
        public static bool OnTestContextDisposedEventHandler { get; set; }
        public static bool OnApplicationStopStartEventHandler { get; set; }
        public static bool OnApplicationStopFinishEventHandler { get; set; }
        public static bool OnTestContextStepExecutionEndEventHandler { get; set; }
        public static bool OnTestContextStepCreatedEventHandler { get; set; }
        public static bool OnTestContextStepExecutionStartEventHandler { get; set; }
        public static bool OnTestContextStepDisposedEventHandler { get; set; }
        public static bool OnTestContextStepPartCreatedEventHandler { get; set; }
        public static bool OnTestContextStepPartExecutionStartEventHandler { get; set; }
        public static bool OnTestContextStepPartExecutionEndEventHandler { get; set; }
        public static bool OnTestContextStepPartDisposedEventHandler { get; set; }

        public static bool OnBeforeTestContextStepExecutionStartEventHandler { get; set; }

        public static bool OnBeforeTestSuiteExecutionStartEventHandler { get; set; }

        public static bool OnBeforeTestContextExecutionStartEventHandler { get; set; }

        public static bool OnBeforeTestContextStepPartExecutionStartEventHandler { get; set; }


        public void HandleEvent(OnApplicationInitializeFinishEvent eventMessage)
        {
            OnApplicationInitializeFinishEventHandler = true;
        }


        public void HandleEvent(OnApplicationInitializeStartEvent eventMessage)
        {
            OnApplicationInitializeStartEventHandler = true;
        }

        public void HandleEvent(OnApplicationStopFinishEvent eventMessage)
        {
            OnApplicationStopFinishEventHandler = true;
        }

        public void HandleEvent(OnApplicationStopStartEvent eventMessage)
        {
            OnApplicationStopStartEventHandler = true;
        }

        public void HandleEvent(OnBeforeTestContextExecutionStartEvent eventMessage)
        {
            OnBeforeTestContextExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnBeforeTestContextStepExecutionStartEvent eventMessage)
        {
            OnBeforeTestContextStepExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnBeforeTestContextStepPartExecutionStartEvent eventMessage)
        {
            OnBeforeTestContextStepPartExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnBeforeTestSuiteExecutionStartEvent eventMessage)
        {
            OnBeforeTestSuiteExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnTestContextCreatedEvent eventMessage)
        {
            OnTestContextCreatedEventHandler = true;
        }

        public void HandleEvent(OnTestContextDisposedEvent eventMessage)
        {
            OnTestContextDisposedEventHandler = true;
        }

        public void HandleEvent(OnTestContextExecutionEndEvent eventMessage)
        {
            OnTestContextExecutionEndEventHandler = true;
        }

        public void HandleEvent(OnTestContextExecutionStartEvent eventMessage)
        {
            OnTestContextExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepCreatedEvent eventMessage)
        {
            OnTestContextStepCreatedEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepDisposedEvent eventMessage)
        {
            OnTestContextStepDisposedEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepExecutionEndEvent eventMessage)
        {
            OnTestContextStepExecutionEndEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepExecutionStartEvent eventMessage)
        {
            OnTestContextStepExecutionStartEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepPartCreatedEvent eventMessage)
        {
            OnTestContextStepPartCreatedEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepPartDisposedEvent eventMessage)
        {
            OnTestContextStepPartDisposedEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepPartExecutionEndEvent eventMessage)
        {
            OnTestContextStepPartExecutionEndEventHandler = true;
        }

        public void HandleEvent(OnTestContextStepPartExecutionStartEvent eventMessage)
        {
            OnTestContextStepPartExecutionStartEventHandler = true;
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

        public void HandleEvent(OnTestSuiteExecutionStartedEvent eventMessage)
        {
            OnTestSuiteExecutionStartEventHandler = true;
        }
    }
}