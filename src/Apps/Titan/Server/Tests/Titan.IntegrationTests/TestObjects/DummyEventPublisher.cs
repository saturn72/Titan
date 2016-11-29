using Saturn72.Core.Services.Events;

namespace Titan.IntegrationTests.TestObjects
{
    public class DummyEventPublisher : IEventPublisher
    {
        public void Publish<TEvent>(TEvent eventMessage) where TEvent : EventBase
        {
        }
    }
}