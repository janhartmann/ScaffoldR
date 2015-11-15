using System.Collections.Generic;
using System.Linq;
using ScaffoldR.Core.Events;

namespace ScaffoldR.Infrastructure.Events
{
    /// <summary>
    /// Fires multiple dispatch event triggers
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    internal sealed class MultipleDispatchEventTrigger<TEvent> : ITriggerEvent<TEvent> where TEvent : IEvent
    {
        private readonly IEnumerable<IHandleEvent<TEvent>> _handleEvents;

        public MultipleDispatchEventTrigger(IEnumerable<IHandleEvent<TEvent>> handleEvents)
        {
            _handleEvents = handleEvents;
        }

        public void Trigger(TEvent e)
        {
            if (_handleEvents == null || !_handleEvents.Any()) return;

            foreach (var handler in _handleEvents)
            {
                handler.Handle(e);
            }
        }
        
    }
}
