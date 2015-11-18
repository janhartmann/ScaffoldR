using System.Diagnostics;
using ScaffoldR.Core.Commands;
using SimpleInjector;

namespace ScaffoldR.Infrastructure.Commands
{
    internal sealed class CommandProcessor : IProcessCommands
    {
        private readonly Container _container;

        public CommandProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public void Execute(ICommand command)
        {
            var handlerType = typeof(IHandleCommand<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            
            handler.Handle((dynamic)command);
        }
    }
}