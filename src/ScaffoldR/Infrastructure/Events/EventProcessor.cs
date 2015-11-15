using ScaffoldR.Core.Events;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Events
{
    internal sealed class EventProcessor : IProcessEvents
    {
        private readonly Container _container;

        public EventProcessor(Container container)
        {
            _container = container;
        }

        public void Raise(IEvent evt)
        {
            var triggerType = typeof(ITriggerEvent<>).MakeGenericType(evt.GetType());
            dynamic trigger = _container.GetInstance(triggerType);
            trigger.Trigger((dynamic)evt);
        }
    }
}
