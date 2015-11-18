using System;
using ScaffoldR.Core.Commands;
using ScaffoldR.Core.Events;
using ScaffoldR.Core.Extensions;

namespace ScaffoldR.Infrastructure.Commands
{
    internal sealed class CommandEventProcessingDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly IProcessEvents _events;
        private readonly Func<IHandleCommand<TCommand>> _handlerFactory;
        
        public CommandEventProcessingDecorator(IProcessEvents events, Func<IHandleCommand<TCommand>> handlerFactory)
        {
            _events = events;
            _handlerFactory = handlerFactory;
        }

        public void Handle(TCommand command)
        {
            var handler = _handlerFactory();
            handler.Handle(command);
           
            var attribute = handler.GetType().GetRuntimeAddedAttribute<RaiseEventAttribute>();
            if (attribute != null && attribute.Enabled)
            {
                _events.Raise(command);
            }
        }
    }
}
