using System;
using System.Diagnostics;
using ScaffoldR.Core.Commands;

namespace ScaffoldR.Infrastructure.Commands
{
    internal sealed class CommandNotNullDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly Func<IHandleCommand<TCommand>> _handlerFactory;

        public CommandNotNullDecorator(Func<IHandleCommand<TCommand>> handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public void Handle(TCommand command)
        {
            if (Equals(command, null)) throw new ArgumentNullException("command");
            
            _handlerFactory().Handle(command);
        }
    }
}