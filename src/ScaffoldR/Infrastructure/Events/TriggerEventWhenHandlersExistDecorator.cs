using System;
using System.Collections.Generic;
using System.Linq;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Tasks;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Events
{
    internal sealed class TriggerEventWhenHandlersExistDecorator<TEvent> : ITriggerEvent<TEvent> where TEvent : IEvent
    {
        private readonly Container _container;
        private readonly ITaskFactory _taskFactory;
        private readonly Func<ITriggerEvent<TEvent>> _factory;
        private readonly IEnumerable<IHandleEvent<TEvent>> _handleEvents;

        public TriggerEventWhenHandlersExistDecorator(Container container, ITaskFactory taskFactory, Func<ITriggerEvent<TEvent>> factory, IEnumerable<IHandleEvent<TEvent>> handleEvents)
        {
            _container = container;
            _taskFactory = taskFactory;
            _factory = factory;
            _handleEvents = handleEvents;
        }

        public void Trigger(TEvent evt)
        {
            if (_handleEvents != null && _handleEvents.Any())
            {
                _taskFactory.StartTask(() =>
                {
                    if (_container.GetCurrentLifetimeScope() != null)
                    {
                        _factory().Trigger(evt);
                    }
                    else
                    {
                        using (_container.BeginLifetimeScope())
                        {
                            _factory().Trigger(evt);
                        }
                    }
                });
            }
        }
    }
}
